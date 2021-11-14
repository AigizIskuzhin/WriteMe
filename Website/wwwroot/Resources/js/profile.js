const profilePostsContainerId = "profile_posts_container";

Document.prototype.createElementFromString = function (str) {
    const element = new DOMParser().parseFromString(str, 'text/html');
    const child = element.documentElement.querySelector('body').firstChild;
    return child;
};

let postModel = (postId, ownerId, titleText, descriptionText) => {
    return {
        "Id": postId,
        "OwnerId": ownerId,
        "Title": titleText,
        "Description": descriptionText
    }
}

function OnEditPostClick(btn) {
    if (!btn || !btn.classList.contains('edit_post_btn')) return;

    var post = btn.offsetParent.offsetParent;

    if (!post || !post.classList.contains('post')) return;

    var postId = post.getAttribute('data-post-id');
    var ownerId = post.getAttribute('data-owner-id');

    var postTitle = post.querySelector('.post_title');
    var titleText = "";
    if (postTitle) titleText = postTitle.innerText;

    var postDescription = post.querySelector('.post_description');
    var descriptionText = "";
    if (postDescription) descriptionText = postDescription.innerText;

    var url = "Modals/editpost";

    var model = postModel(postId, ownerId, titleText, descriptionText);

    AjaxPostQueryWithViewResult(url,
        model,
        modalWrapperId,
        () => {
            ShowModalWrapper();
            LoadEditPostForm('edit_post_btn', 'Title', 'Description', postId, ownerId);
        });
}

function OnRemovePostClick(btn) {
    if (!btn || !btn.classList.contains('delete_post_btn')) return;

    var post = btn.offsetParent.offsetParent;

    if (!post || !post.classList.contains('post')) return;

    var postId = post.getAttribute('data-post-id');
    var ownerId = post.getAttribute('data-owner-id');
    $.ajax({
        url: "profile/deletepost",
        type: "POST",
        data: {
            "id": postId
        },
        success: function (isDeleted) {
            if (isDeleted) {
                document.getElementById(profilePostsContainerId).removeChild(post);
            }
        }
    });
}

function OnReportPostClick(btn) {
    if (!btn || !btn.classList.contains('report_post_btn')) return;

    var post = btn.offsetParent.offsetParent;

    if (!post || !post.classList.contains('post')) return;

    var postId = post.getAttribute('data-post-id');
    var ownerId = post.getAttribute('data-owner-id');

    var model = {
        "postId" : postId
    }
    var url = "modals/SendReportModal"
    AjaxPostQueryWithViewResult(url,
        model,
        modalWrapperId,
        () => {
            ShowModalWrapper();
            LoadReportPostForm('send_report', 'report_types', 'report_msg', postId);
        });
}


let OnCreatePostClick = (inputId) => {
    var input = document.getElementById(inputId);
    if (input) {
        input.addEventListener('click',
            () => {
                var url = "modals/createpost";
                AjaxPostQueryWithViewResult(url,
                    null,
                    modalWrapperId,
                    () => {
                        ShowModalWrapper();
                        LoadCreatePostForm('create_post_btn', 'Title', 'Description');
                    });
            });
    }
}

function AjaxPostQueryWithViewResult(url, model, containerId, delegate) {
    window.$.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function (viewResult) {
            var container = document.getElementById(containerId);
            if (container)
                container.innerHTML = viewResult;
            delegate();
        }
    });
}

function AjaxPostQuery(url, model, delegate) {
    window.$.ajax({
        url: url,
        type: "POST",
        data: model,
        success: function() {
            if (delegate)
                delegate();
        }
    });
}

let LoadEditPostForm = (submitId, titleId, descriptionId, postId, ownerId) => {
    var submit = document.getElementById(submitId);
    var titleInput = document.getElementById(titleId);
    var descriptionInput = document.getElementById(descriptionId);
    if (submit && titleInput && descriptionInput && postId && ownerId) {
        descriptionInput.addEventListener('input', () => {
            if (descriptionInput.value == "")
                submit.disabled = true;
            else submit.disabled = false;
        });
        submit.addEventListener('click',
            () => {
                var title = titleInput.value;
                var description = descriptionInput.value;

                var model = postModel(postId, ownerId, title, description);

                var oldPostId = 'post-' + postId;

                var post = document.getElementById(oldPostId);

                var url = "profile/editpost";
                window.$.ajax({
                    url: url,
                    type: "POST",
                    data: model,
                    success: function (viewResult) {
                        var newPost = document.createElementFromString(viewResult);
                        post.innerHTML = newPost.innerHTML;
                        HideModalWrapper();
                        LoadPostEvents(post);
                    }
                });
            });
    }
}
let LoadReportPostForm = (sendBtnId, reportTypesId, reportMsgId, postId) => {
    var sendBtn = document.getElementById(sendBtnId);
    var reportTypes = document.getElementById(reportTypesId);
    var reportMsg = document.getElementById(reportMsgId);

    if (sendBtn && reportTypes && reportMsg) {
        sendBtn.addEventListener("click", function () {
            var selectedReportType = reportTypes.options[reportTypes.selectedIndex].value;
            var msg = reportMsg.value;

            var model = {
                "postId": postId,
                "reportTypeId": selectedReportType,
                "msg": msg
            };
            var url = "profile/SendReportForPost";

            AjaxPostQuery(url,
                model,
                () => {
                    HideModalWrapper();
                });
        });
    }
}

let LoadCreatePostForm = (submitId, titleId, descriptionId) => {
    var submit = document.getElementById(submitId);
    var titleInput = document.getElementById(titleId);
    var descriptionInput = document.getElementById(descriptionId);
    if (submit && titleInput && descriptionInput) {
        descriptionInput.addEventListener('input', () => {
            if (descriptionInput.value == "")
                submit.disabled = true;
            else submit.disabled = false;
        });
        submit.addEventListener('click',
            () => {
                var title = titleInput.value;
                var description = descriptionInput.value;
                var model = postModel(null, null, title, description);
                var url = "profile/UploadPost";
                window.$.ajax({
                    url: url,
                    type: "POST",
                    data: model,
                    success: function (viewResult) {
                        let post = document.createElementFromString(viewResult);
                        var posts = document.getElementById(profilePostsContainerId);

                        if (posts.children[0].classList.contains('post-smoothpad')) {
                            posts.innerHTML = "";
                            posts.appendChild(post);
                        }
                        else posts.insertBefore(element, posts.firstChild);

                        HideModalWrapper();

                        LoadPostEvents(post);
                    }
                });
            });
    }
}

function LoadPostEvents(post) {
    var editBtn = post.querySelector('.edit_post_btn');
    if (editBtn)
        editBtn.addEventListener('click', () => { OnEditPostClick(editBtn) });

    var deleteBtn = post.querySelector('.delete_post_btn');
    if (deleteBtn)
        deleteBtn.addEventListener('click', () => { OnRemovePostClick(deleteBtn)});

    var reportBtn = post.querySelector('.report_post_btn');
    if (reportBtn)
        reportBtn.addEventListener('click', () => { OnReportPostClick(reportBtn)});
}

function LoadPostsEvents() {
    document.getElementById(profilePostsContainerId)
        .querySelectorAll('.post')
        .forEach(post => LoadPostEvents(post));
}

let SearchPosts = (inputId, containerId) => {
    let input = document.getElementById(inputId);
    let resultContainer = document.getElementById(containerId);
    if (input && resultContainer) {
        input.addEventListener('input',
            () => {
                var model = {
                    "filterText": input.value
                };
                var url = "profile/SearchUserPosts";

                AjaxPostQueryWithViewResult(url,
                    model,
                    profilePostsContainerId,
                    LoadPostsEvents);
            });
    }
}

let uploadPost = (inputId, containerId) => {
    let input = document.getElementById(inputId);
    let resultContainer = document.getElementById(containerId);
    let request;
    if (input && resultContainer) {
        input.addEventListener('click', () => {
            if (request && request.readyState != 4)
                request.abort();
            var url = "profile/uploadpost";
            request = $.ajax({
                url: url,
                type: "POST",
                data: {
                    "filterText": input.value
                },
                success: function (view) {
                    resultContainer.innerHTML = view;
                }
            });
        });
    }
}
uploadPost('im_dialogs_search', 'profile_posts_container');


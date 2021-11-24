let SearchUsers = (inputId, containerId, url, id) => {
    let input = document.getElementById(inputId);
    let resultContainer = document.getElementById(containerId);
    if (input && resultContainer) {
        input.addEventListener('input',
            () => {
                var model = {
                    "filter": input.value
                };
                if (id) {
                    model = {
                        "filter": input.value,
                        "userId":id
                    }
                }
                AjaxPostQueryWithViewResult(url,
                    model, containerId);
            });
    }
}
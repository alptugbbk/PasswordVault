function submitAjax(url, method, formData, successMessage, errorMessage, redirectUrl = null) {
    $.ajax({
        url: url,
        method: method,
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            console.log(response.error);
            if (response.success) {
                $("#success-message").text(successMessage).removeClass("d-none");
                $("#error-message").addClass("d-none");

                if (redirectUrl) {
                    setTimeout(function () {
                        window.location.href = redirectUrl;
                    }, 1000);
                }

            } else {
                $("#error-message").text(response.error || errorMessage).removeClass("d-none");
                $("#success-message").addClass("d-none");
            }
            hideMessages();
        },
        error: function () {
            $("#error-message").text("An error occurred, please try again").removeClass("d-none");
            $("#success-message").addClass("d-none");
            hideMessages();
        }
    });
}

function hideMessages() {
    setTimeout(function () {
        $("#success-message").addClass("d-none");
        $("#error-message").addClass("d-none");
    }, 3000);
}

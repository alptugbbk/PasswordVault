
$("#login-form").on('submit', function (event) {
    event.preventDefault();

    const formData = {
        UserName: $("#username").val(),
        Password: $("#password").val(),
        RememberMe: $("#rememberme").is(":checked")
    };

    submitAjax('/Auth/Login', 'POST' ,formData, "Successfully logged in", "Invalid username or password", '/platform/pindex');
});



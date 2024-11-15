$(document).ready(function () {

    $('#toggle-password').on('click', function () {

        var passwordField = $('#password');


        var type = passwordField.attr('type') === 'password' ? 'text' : 'password';
        passwordField.attr('type', type);


        var icon = $(this);
        icon.toggleClass('bi-eye bi-eye-slash');
    });
});


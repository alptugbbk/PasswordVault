$(document).ready(function () {
	$("#forgot-password-form").on('submit', function (event) {
		event.preventDefault();

		const email = $("#email").val();

		var forgotPasswordModel = {
			Email: email
		};

		submitAjax('/Auth/ForgotPassword', 'POST', forgotPasswordModel, "Sent Mail", "Make sure your email occured");

	});
});
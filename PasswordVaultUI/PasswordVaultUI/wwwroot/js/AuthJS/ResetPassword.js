$(document).ready(function () {
	$("#forgot-password-form").on('submit', function (event) {
		event.preventDefault();

		const userid = $("#userid").val();
		const newpassword = $("#newpassword").val();
		const confirmpassword = $("#confirmpassword").val();

		if (newpassword != confirmpassword) {
			$("#message").text("Please check your password again").css("color", "red");
		}

		var forgotPasswordModel = {
			userid: userid,
			NewPassword: newpassword
		};

		submitAjax('/auth/resetpassword', 'POST', forgotPasswordModel, "Password change succesfull", "", '/auth/login');

	});
});
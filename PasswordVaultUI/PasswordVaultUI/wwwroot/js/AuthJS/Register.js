$("#register-form").on("submit", function (event) {
	event.preventDefault();
	if (!this.checkValidity()) {
		return;
	}

	const firstname = $("#firstname").val()
	const lastname = $("#lastname").val()
	const username = $("#username").val();
	const email = $("#email").val();
	const password = $("#password").val();

	var registerViewModel = {
		FirstName: firstname,
		LastName: lastname,
		UserName: username,
		Email: email,
		Password: password
	}

	submitAjax('/auth/register', 'POST', registerViewModel, "Successfully registered", "User already registered!", '/auth/login');

});


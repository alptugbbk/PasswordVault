// LOGOUT
$('#btnlogout').on('click', function () {
	$.ajax({
		url: '/platform/logout',
		method: 'POST',
		success: function (response) {
			console.log(response);
			window.location.href = response.redirectUrl;
		},
		error: function (e) {
			console.log(e.statusText);
		}
	});
});



// ADD
$(document).ready(function () {
	$('#platform-form').on('submit', function (event) {
		event.preventDefault();
		const name = $("#name").val();
		const username = $("#username").val();
		const password = $("#password").val();

		var data = {
			Name: name,
			UserName: username,
			Password: password,
		}

		$.ajax({
			url: '/Platform/CreatePlatform',
			method: 'POST',
			contentType: 'application/json',
			data: JSON.stringify(data),
			success: function (response) {
				console.log(response);
				alert('Platform successfully added');


				const platformName = response.platformRequestDto.name;
				const platformUsername = response.platformRequestDto.userName;
				const platformPassword = response.platformRequestDto.password;


				const newListItem = `<li>
												<strong>Platform: </strong> ${platformName} <br>
												<strong>Username: </strong> ${platformUsername} <br>
												<strong>Password: </strong> ${platformPassword} <br>
											</li>`;

				$('#user-list').append(newListItem);

				$("#name").val('');
				$("#username").val('');
				$("#password").val('');
			},

			error: function (xhr) {
				console.error(xhr);
				$("#error-message").text('add user error' + xhr.responseText);
			}
		});
	});
});


// GET
function loadUserPlatformList() {
	$('#user-list').empty();
	$.ajax({
		url: '/Platform/GetPlatform',
		method: 'POST',
		contentType: 'application/json',
		success: function (response) {
			console.log(response);
			$.each(response, function (index, platform) {

				const newListItem = $("<li>").html(`
											  <strong>Platform:</strong> ${platform.name} <br>
											  <strong>Username:</strong> ${platform.userName} <br>
											  <strong>Password:</strong> ${platform.password} <br>
												 `).css('cursor', 'pointer').on('click', function () {
					$('#listid').val(platform.id);
					$('#name').val(platform.name);
					$('#username').val(platform.userName);
					$('#password').val(platform.password);
				});

				$('#user-list').append(newListItem);

			});
		},
		error: function (e) {
			console.log(e);
			$('#error-message').text("list  fail");
		}
	});
}

loadUserPlatformList();


// UPDATE
$('#btn-update').on('click', function (event) {
	event.preventDefault();
	const listid = $("#listid").val().trim();
	const name = $("#name").val().trim();
	const username = $("#username").val().trim();
	const password = $("#password").val().trim();

	if (!name || !username || !password) {
		alert("Please fill in all fields.");
		return;
	}

	if (confirm("Are you sure you want to update?")) {
		const updateData = {
			Id: listid,
			Name: name,
			UserName: username,
			Password: password
		};

		$('#btn-update').prop('disabled', true);

		$.ajax({
			url: '/platform/updateplatform',
			method: 'POST',
			contentType: 'application/json',
			data: JSON.stringify(updateData),
			success: function (response) {
				alert("Update successful!");
				loadUserPlatformList();
				$("#name, #username, #password, #listid").val('');
			},
			error: function (e) {
				alert("Failed to update: " + e.responseText);
			},
			complete: function () {
				$('#btn-update').prop('disabled', false);
			}
		});
	}
});


// DELETE
$('#btn-delete').on('click', function (event) {
	event.preventDefault();
	const listid = $("#listid").val().trim();

	if (!listid) {
		alert("Please select the platform you want to delete.");
		return;
	}

	const deleteData = {
		Id: listid
	};

	if (confirm("Are you sure you want to delete this platform?")) {
		$.ajax({
			url: '/Platform/DeletePlatform',
			method: 'POST',
			contentType: 'application/json',
			data: JSON.stringify(deleteData),
			success: function () {
				alert('Platform successfully deleted');

				loadUserPlatformList();

				$("#listid, #name, #username, #password").val('');
			},
			error: function (e) {
				console.log(e);
				$('#error-message').text("The deletion failed.");
			}
		});
	}
});



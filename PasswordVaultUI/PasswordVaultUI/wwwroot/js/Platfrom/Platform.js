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

                alert('Platform successfully added');

                $('#tableBody').empty();

                loadUserPlatformList();

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
    $.ajax({
        url: '/Platform/GetPlatform',
        method: 'POST',
        contentType: 'application/json',
        success: function (response) {
            $.each(response, function (index, platform) {
                const newRow = $("<tr>").html(`
                    <td data-label="Platform">${platform.name}</td>
                    <td data-label="Username">${platform.userName}</td>
                    <td data-label="Password">
                        <div class="password-container">
                            <input type="password" value="${platform.password}" disabled>
                            <button class="toggle-btn">Show</button>
                        </div>
                    </td>
                    <td data-label="Action">
                        <button class="btn-delete" data-id="${platform.id}">DELETE</button>
                    </td>
                `).data('platformId', platform.id);

                
                newRow.css('cursor', 'pointer').on('click', function () {
                    $('#listid').val(platform.id);
                    $('#name').val(platform.name);
                    $('#username').val(platform.userName);
                    $('#password').val(platform.password);
                });

                $('#tableBody').append(newRow);
            });
        },
        error: function (e) {
            console.log(e);
            $('#error-message').text("list fail");
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
                $('#tableBody').empty();
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
$(document).on('click', '.btn-delete', function () {
    const platformId = $(this).data('id');

    if (!platformId) {
        alert("Please select the platform you want to delete.");
        return;
    }

    const deleteData = {
        Id: platformId
    };

    if (confirm("Are you sure you want to delete this platform?")) {
        $.ajax({
            url: '/Platform/DeletePlatform',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(deleteData ),
            success: function () {
                alert('Platform successfully deleted');

                $('#tableBody').empty();

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




// Toggle password table
$(document).on('click', '.toggle-btn', function () {
    const input = $(this).prev('input');
    if (input.attr('type') === 'password') {
        input.attr('type', 'text');
        $(this).text('Hide');
    } else {
        input.attr('type', 'password');
        $(this).text('Show');
    }
});


// search table
document.getElementById('searchInput').addEventListener('input', function () {
    const searchValue = this.value.toLowerCase();
    const rows = document.querySelectorAll('#tableBody tr');

    rows.forEach(row => {
        const platform = row.cells[0].textContent.toLowerCase();
        row.style.display = platform.includes(searchValue) ? '' : 'none';
    });
});







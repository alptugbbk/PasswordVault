
$(document).ready(function () {

    loadNavbarProfilePicture();   
    
    $('#profileModal').on('show.bs.modal', function () {
        loadProfilePictureInModal();
    });


    $('#profileImageUpload').on('change', previewImage);


    $('#saveProfileBtn').on('click', saveProfile);
});

function loadNavbarProfilePicture() {

    $.ajax({
        url: '/Platform/GetProfilePicture',
        method: 'POST',
        success: function (response) {

            $('#navbarProfileImage').attr('src', response.path + '?t=' + new Date().getTime());

        },
        error: function () {
            
        }
    });
}


function loadProfilePictureInModal() {
    $.ajax({
        url: '/Platform/GetProfilePicture',
        method: 'POST',
        success: function (response) {
                $('#profileImagePreview').attr('src', response.path + '?t=' + new Date().getTime());
        },
        error: function () {

        }
    });
}


function previewImage(event) {
    const reader = new FileReader();
    reader.onload = function () {
        $('#profileImagePreview').attr('src', reader.result);
    };
    reader.readAsDataURL(event.target.files[0]);
}

function saveProfile() {

    const formData = new FormData();
    const fileInput = document.getElementById("profileImageUpload");
    const file = fileInput.files[0];

    if (file) {
        formData.append("profilePicture", file);

        $.ajax({
            url: "/Platform/UploadProfilePicture",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    alert("Photo successfully saved");
                    $('#profileModal').modal('hide');
                    $('#profileImagePreview').attr('src', response.path);
                    loadNavbarProfilePicture();
                } else {
                    alert("Photo upload failed");
                }
            },
            error: function () {
                alert("An error occurred while uploading the photo");
            }
        });
    } else {
        alert("Please select a photo");
    }
}

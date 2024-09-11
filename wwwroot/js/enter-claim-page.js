document.addEventListener('DOMContentLoaded', function () {
    const hamburgerMenu = document.getElementById('hamburgerMenu');
    const claimForm = document.getElementById('claimForm');
    const uploadButton = document.querySelector('.upload-button');
    const fileInput = document.getElementById('SupportingDocument');

    hamburgerMenu.addEventListener('click', function () {
        // Navigate to the menu page
        window.location.href = '/Menu';
    });

    uploadButton.addEventListener('click', function () {
        fileInput.click();
    });

    fileInput.addEventListener('change', function () {
        if (this.files && this.files[0]) {
            uploadButton.textContent = this.files[0].name;
        }
    });

    claimForm.addEventListener('submit', function (e) {
        e.preventDefault();

        // Collect form data
        const formData = new FormData(claimForm);

        // Send form data to server
        fetch('/Lecturer/SubmitClaim', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Claim submitted successfully!');
                    window.location.href = '/Lecturer/Dashboard';
                } else {
                    alert('Error submitting claim. Please try again.');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('An error occurred. Please try again.');
            });
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const hamburgerMenu = document.getElementById('hamburgerMenu');
    const uploadButton = document.querySelector('.upload-button');
    const fileInput = document.getElementById('SupportingDocument');
    const fileNameSpan = document.getElementById('fileName');

    hamburgerMenu.addEventListener('click', function () {
        window.location.href = '/Menu/HamburgerMenu';
    });

    uploadButton.addEventListener('click', function () {
        fileInput.click();
    });

    fileInput.addEventListener('change', function () {
        if (this.files && this.files[0]) {
            fileNameSpan.textContent = this.files[0].name;
        }
    });
});
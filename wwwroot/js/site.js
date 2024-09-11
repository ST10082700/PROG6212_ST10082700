// Hamburger menu functionality
document.addEventListener('DOMContentLoaded', function () {
    const hamburgerButton = document.querySelector('.hamburger-menu');
    if (hamburgerButton) {
        hamburgerButton.addEventListener('click', function (e) {
            e.preventDefault();
            window.location.href = this.getAttribute('href');
        });
    }
});
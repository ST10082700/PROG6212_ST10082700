document.addEventListener('DOMContentLoaded', function () {
    const hamburgerMenu = document.getElementById('hamburgerMenu');

    hamburgerMenu.addEventListener('click', function () {
        // Navigate to the menu page
        window.location.href = '/Menu/HamburgerMenu';
    });
});
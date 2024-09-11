document.addEventListener('DOMContentLoaded', function () {
    const closeMenuButton = document.getElementById('closeMenuButton');
    const homeButton = document.getElementById('homeButton');
    const logoutButton = document.getElementById('logoutButton');
    const claimsButton = document.getElementById('claimsButton');

    closeMenuButton.addEventListener('click', function () {
        window.history.back();
    });

    
    homeButton.addEventListener('click', function () {
        window.location.href = '/Lecturer/Dashboard';
    });

    /*

    logoutButton.addEventListener('click', function () {
        window.location.href = '/Account/Logout';
    });

    claimsButton.addEventListener('click', function () {
        window.location.href = '/Lecturer/SubmittedClaims';
    }); */ 
});
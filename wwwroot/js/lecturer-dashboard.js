document.addEventListener('DOMContentLoaded', function () {
    const hamburgerMenu = document.getElementById('hamburgerMenu');
    const enterClaimButton = document.getElementById('enterClaimButton');
    const viewClaimsButton = document.getElementById('viewClaimsButton');

    hamburgerMenu.addEventListener('click', function () {
        window.location.href = '/Menu/HamburgerMenu';
    });

    enterClaimButton.addEventListener('click', function () {
        window.location.href = '/Lecturer/EnterClaimDetails';
    });

    /*
    viewClaimsButton.addEventListener('click', function () {
        window.location.href = '/Lecturer/SubmittedClaims';
    });
    */
});
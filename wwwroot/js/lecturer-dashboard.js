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

    
    if (viewClaimsButton) {
        viewClaimsButton.addEventListener('click', function () {
            window.location.href = '/Lecturer/ViewSubmittedClaims';
        });
    } else {
        console.error('viewClaimsButton element not found');
    }
    
});
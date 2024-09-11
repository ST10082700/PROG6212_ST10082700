document.addEventListener('DOMContentLoaded', function () {
    console.log('DOM fully loaded and parsed');

    const topLoginBtn = document.getElementById('topLoginBtn');
    const getStartedBtn = document.getElementById('getStartedBtn');

    function navigateToLogin() {
        console.log('Navigate to login function called');
        window.location.href = loginUrl; // This will be defined in the view
    }

    if (topLoginBtn) {
        topLoginBtn.addEventListener('click', navigateToLogin);
        console.log('Event listener added to top login button');
    } else {
        console.error('Top login button not found');
    }

    if (getStartedBtn) {
        getStartedBtn.addEventListener('click', navigateToLogin);
        console.log('Event listener added to get started button');
    } else {
        console.error('Get started button not found');
    }
});
$(document).ready(function (e) {
    $('#aEmail').on('keyup', function () {
        var email = $(this).val();
        var eerror = $('#aEmailError');

        if (email === '') {
            eerror.text('Email address is required');
        } else if (!validateEmail(email)) {
            eerror.text('Invalid email address');
        } else {
            eerror.text('');
        }
    });

    function validateEmail(email) {
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

})
!(function () {
    $.selector_cache('#NewPassword').on('change', function () {
        if (0 < $.selector_cache('#NewPassword').prop('value').length) {
            $.selector_cache('#nPasswordErr').removeClass('text-danger').removeClass('text-bold-700');
        }
    });
    $.selector_cache('#ConfirmPassword').on('change', function () {
        if (0 < $.selector_cache('#ConfirmPassword').prop('value').length) {
            $.selector_cache('#cPasswordErr').removeClass('text-danger').removeClass('text-bold-700');
        }
    });
    $.selector_cache('#ResetPassId').on('click', function () {
        var numberOfErrors = 0;
        var nPassword = $.selector_cache('#NewPassword').prop('value');
        if (nPassword) {
            pLen = parseInt($.selector_cache('#setting').data('length'));
            pLen = isNaN(pLen) ? 6 : pLen;
            if (pLen > nPassword.length) {
                numberOfErrors++;
                $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
                $.selector_cache('#NewPasswordErr').html("Password must be atleast " + pLen + " characters long.");
            }
            pattern = /(?=.*[#@&$%+!]).*$/;
            if ($.selector_cache('#setting').data('spl-char') === 'True' && !pattern.test(nPassword)) {
                numberOfErrors++;
                $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
                $.selector_cache('#NewPasswordErr').html("Password must have atleast one of the following special characters: #@&$%+!");
            }
            pattern = /(?=.*[a-z])(?=.*[A-Z]).*$/;
            if ($.selector_cache('#setting').data('case') === 'True' && !pattern.test(nPassword)) {
                numberOfErrors++;
                $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
                $.selector_cache('#NewPasswordErr').html("Password must have atleast one uppercase & one lowercase character.");
            }
            pattern = /(?=.*[0-9]).*$/;
            if ($.selector_cache('#setting').data('numeric') === 'True' && !pattern.test(nPassword)) {
                numberOfErrors++;
                $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
                $.selector_cache('#NewPasswordErr').html("Password must have atleast one numeric character.");
            }
        }
        else {
            numberOfErrors++;
            $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#NewPassword').addClass('error').attr("placeholder", "Cannot be empty");
        }
        var cPassword = $.selector_cache('#ConfirmPassword').prop('value');
        if (cPassword) {
            pLen = parseInt($.selector_cache('#setting').data('length'));
            if (isNaN(pLen) ? 6 > cPassword.length : pLen > cPassword.length) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
            pattern = /(?=.*[#@&$%+!]).*$/;
            if ($.selector_cache('#setting').data('spl-char') === 'True' && !pattern.test(cPassword)) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
            pattern = /(?=.*[a-z])(?=.*[A-Z]).*$/;
            if ($.selector_cache('#setting').data('case') === 'True' && !pattern.test(cPassword)) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
            pattern = /(?=.*[0-9]).*$/;
            if ($.selector_cache('#setting').data('numeric') === 'True' && !pattern.test(cPassword)) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
        }
        else {
            numberOfErrors++;
            $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#ConfirmPassword').addClass('error').attr("placeholder", "Cannot be empty");
        }
        if ($('#NewPassword').val() === $('#ConfirmPassword').val()) {
            //$('#ConfirmPassword').html('Matching').popover('hide');
        }
        else {
            numberOfErrors++;
            //$('#ConfirmPassword').html('Not Matching').popover('show');
            $.selector_cache('#NewPasswordErr').html("Not Matching");
        }
        if (numberOfErrors === 0)
            $.selector_cache('#ResetUpForm').submit();
    });
    $('.NewPassword').mousedown(function () {
        $('#NewPassword').prop('type', 'text');
    });
    $('.NewPassword').mouseup(function () {
        $('#NewPassword').prop('type', 'password');
    });
    $('.ConfirmPassword').mousedown(function () {
        $('#ConfirmPassword').prop('type', 'text');
    });
    $('.ConfirmPassword').mouseup(function () {
        $('#ConfirmPassword').prop('type', 'password');
    });
})();

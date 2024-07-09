!(function () {
    $.selector_cache('#ChangePassId').on('click', function () {
        var numberOfErrors = 0, pLen, pattern;
        var oPassword = $.selector_cache('#OldPassword').prop('value').trim();
        if (!oPassword) {
            numberOfErrors++;
            $.selector_cache('#oPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#OldPassword').addClass('error').attr("placeholder", "Cannot be empty");            
        }
        var OlddbPassword = $.selector_cache('#OlddbPassword').prop('value').trim();
        if (oPassword != OlddbPassword) {
            numberOfErrors++;
            $.selector_cache('#OlddbPasswordErr').html("Old password does not Matched that you have entered");
            $.selector_cache('#NewPasswordErr').html('');
        }
        var nPassword = $.selector_cache('#NewPassword').prop('value').trim();
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
                $.selector_cache('#NewPasswordErr').html('Password must have atleast one numeric character.');
            }
        }
        else {
            numberOfErrors++;
            $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#NewPassword').addClass('error').attr("placeholder", "Cannot be empty");
        }
        var cPassword = $.selector_cache('#ConfirmPassword').prop('value').trim();
        if (cPassword === "") {
            numberOfErrors++;
            $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#ConfirmPassword').addClass('error').attr("placeholder", "Cannot be empty");
        }
        if (cPassword) {
            pLen = parseInt($.selector_cache('#setting').data('length'));
            if (isNaN(pLen) ? 6 > cPassword.length : pLen > cPassword.length) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
            pattern = /[#@&$%+!]/;
            if ($.selector_cache('#ConfirmPassword').data('spclChar') === 'True' && !pattern.test(cPassword)) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
            pattern = /[A-Za-z]/;
            if ($.selector_cache('#ConfirmPassword').data('numeric') === 'True' && !pattern.test(cPassword)) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
            pattern = /(?=.*[0-9]).*$/;
            if ($.selector_cache('#ConfirmPassword').data('case') === 'True' && !pattern.test(cPassword)) {
                numberOfErrors++;
                $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            }
        }
        else {
            numberOfErrors++;
            $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#ConfirmPassword').addClass('error').attr("placeholder", "Cannot be empty");
        }
        if (nPassword !== cPassword) {
            numberOfErrors++;
            $.selector_cache('#nPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#cPasswordErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#NewPasswordErr').html("Does not match");
        }
        if (numberOfErrors === 0)
            $.selector_cache('#ChangePassForm').submit();
    });
    $.selector_cache('#OldPassword').on('change', function () {
        if (0 < $.selector_cache('#OldPassword').prop('value').trim().length) {
            $.selector_cache('#oPasswordErr').removeClass('text-danger').removeClass('text-bold-700');
        }
    });
    $.selector_cache('#NewPassword').on('change', function () {
        if (0 < $.selector_cache('#NewPassword').prop('value').trim().length) {
            $.selector_cache('#nPasswordErr').removeClass('text-danger').removeClass('text-bold-700');
        }
    });
    $.selector_cache('#ConfirmPassword').on('change', function () {
        if (0 < $.selector_cache('#ConfirmPassword').prop('value').trim().length) {
            $.selector_cache('#cPasswordErr').removeClass('text-danger').removeClass('text-bold-700');
        }
    });
    $('.OldPassword').mousedown(function () {
        $('#OldPassword').prop('type', 'text');
    });
    $('.OldPassword').mouseup(function () {
        $('#OldPassword').prop('type', 'password');
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
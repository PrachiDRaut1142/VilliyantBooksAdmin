!(function (e) {
    $.selector_cache('#UserID').on('change', function () {
        if (0 < $.selector_cache('#UserID').prop('value').length) {
            $.selector_cache('#UserIDErr').removeClass('text-danger').removeClass('text-bold-700');
        }
    });
    $.selector_cache('#ResetPassword').on('click', function () {
        var numberOfErrors = 0;
        var fpEmail = $.selector_cache('#UserID').prop('value');
        if (fpEmail === "") {
            numberOfErrors++;
            $.selector_cache('#UserIDErr').addClass('text-danger').addClass('text-bold-700');
            $.selector_cache('#UserID').addClass('error').attr("placeholder", "Cannot be empty");
        }
        else {
            if ($.selector_cache('#errorMessage').prop('value').length > 0) {
                numberOfErrors++;
                $.selector_cache('#NewPasswordErr').html("Incorrct User Name");
            }
        }
        if (numberOfErrors === 0) {
            $.selector_cache('#ResetUpForm').submit();
        }
    });
})();
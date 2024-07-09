!(function ($) {
    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal.fire('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');
    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal.fire('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }
    $('#EmailId').on('change', function () {
        var id = $('#id').val();
        $.ajax({
            url: "/User/CheckUniqueEmailIdTest?EmailId=" + this.value  + '&Id=' + id,
            type: "POST",
            success: function (result) {
                if (result.Data != "") {
                    swal.fire("error", "Email Id Already Exists!", "error").then(okay => {
                        if (okay) {
                            $('#EmailId').val('');
                        }
                    });
                }
            }
        });
    });
    var req = 'This Field is is required';
    $('#FullName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnFullName').html(req).css('display', 'block');
            }
        }
        else if ($('#FullName').val() != null || $('#FullName').val() != '') {
            $('#SpnFullName').css('display', 'none');
        }
        else {
            $('#SpnFullName').html(req).css('display', 'block');
        }
    });
    $('#LoginId').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnLoginId').html(req).css('display', 'block');
            }
        }
        else if ($('#LoginId').val() != null || $('#LoginId').val() != '') {
            $('#SpnLoginId').css('display', 'none');
        }
        else {
            $('#SpnLoginId').html(req).css('display', 'block');
        }
    });
    $('#EmailId').on('keyup', function (e) {
        var email = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var $EamilAddress = $('#EmailId').val();
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnEmailId').html(req).css('display', 'block');
            }
        }
        if (email.test($EamilAddress) == false && $('#EmailId').val().length > 0) {
            $('#SpnEmailId').html('Invalid Email Address').css('display', 'block');
        }
        else {
            $('#SpnEmailId').css('display', 'none');
        }
    });
    $('#ContactNo').on('keypress', function (e) {
        var len = $('#ContactNo').val();
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);

        if ($(this).val().length >= 10) {
            e.preventDefault();
            return false;
        }
        else if (regex.test(str)) {
            $('#SpnContactNo').css('display', 'none');
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });
    $('#Address1').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnAddress1').html(req).css('display', 'block');
            }
        }
        else if ($('#Address1').val() != null || $('#Address1').val() != '') {
            $('#SpnAddress1').css('display', 'none');
        }
        else {
            $('#SpnAddress1').html(req).css('display', 'block');
        }
    });
    $('#UserRole').on('change', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnUserRole').html(req).css('display', 'block');
            }
        }
        else if ($('#UserRole').val() != null || $('#UserRole').val() != '') {
            $('#SpnUserRole').css('display', 'none');
        }
        else {
            $('#SpnUserRole').html(req).css('display', 'block');
        }
    });
    $('#Branch').on('change', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#spnbranch').html(req).css('display', 'block');
            }
        }
        else if ($('#Branch').val() != null || $('#Branch').val() != '' || $('#Branch').val() != '0') {
            $('#spnbranch').css('display', 'none');
        }
        else {
            $('#spnbranch').html(req).css('display', 'block');
        }
    });
    $('#nextBtn').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        var email = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var $EamilAddress = $('#EmailId').val();
        if ($('#FullName').val() === "" || $('#FullName').val() === null) {
            $('#SpnFullName').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#UserRole').val() === "" || $('#UserRole').val() === null) {
            $('#SpnUserRole').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Branch').val() === "" || $('#Branch').val() === null || $('#Branch').val() === "0") {
            $('#spnbranch').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#LoginId').val() === "" || $('#LoginId').val() === null) {
            $('#SpnLoginId').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (!email.test($('#EmailId').prop('value').trim()) || $('#EmailId').prop('value').length > 50 || email.test($EamilAddress) == false) {
            nErrorCount += 1;
            if (email.test($EamilAddress) == false && $('#EmailId').val().length > 0) {
                $('#SpnEmailId').html('Invalid Email-ID!').css('display', 'block');
            }
            else {
                $('#SpnEmailId').html(req).css('display', 'block');
            }
        }
        if ($('#ContactNo').val() === "" || $('#ContactNo').val() === null) {
            $('#SpnContactNo').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        else if ($('#ContactNo').val().length != 10) {
            $('#SpnContactNo').html('Please Enter valid No.').css('display', 'block');
            nErrorCount += 1;
        }
        else {
            $('#SpnContactNo').css('display', 'none');
        }
        if ($('#Address1').val() === "" || $('#Address1').val() === null) {
            $('#SpnAddress1').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        //if ($('#Addharno').val() === "" || $('#Addharno').val() === null) {
        //    $('#SpnAddharno').html(req).css('display', 'block');
        //    nErrorCount += 1;
        //}
        if (0 === nErrorCount) {
            $('#EditUser').submit();
        }
    });

    $.selector_cache('#resendVEBtn').on('click', function (e) {
        swal.fire({
            title: "Are you sure?",
            text: "A new email will be sent and the old email sent will be invalid!",
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, send it!"
        }).then(function (e) {
            if (e.value) {
                $.ajax({
                    url: '/User/ResendVerificationEmail?empId=' + $.selector_cache('#id').prop('value'),
                    type: 'get',
                    success: function (data) {
                        if (data && data.IsSuccess) {
                            swal.fire('Success!', 'Verification email sent successfully.', 'success');
                        } else if (data !== null) {
                            swal.fire('Error!', data.returnMessage, 'error');
                        } else {
                            swal.fire('Error!', 'There was some error. Try again later.', 'error');
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal.fire('Error!', 'There was some error. Try again later.', 'error');
                    }
                });
            }
        });
    });

    $.selector_cache('#rpModalSubmitBtn').on('click', function (e) {
        var numberOfErrors = 0;
        var nPassword = $.selector_cache('#Password').prop('value').trim();
        if (nPassword) {
            pLen = parseInt($.selector_cache('#setting').data('length'));
            if (isNaN(pLen) ? 6 > nPassword.length : pLen > nPassword.length) {
                numberOfErrors++;
                $.selector_cache('#PasswordErr').html("Password must be atleast " + pLen + " characters long.");
                return;
            }
            pattern = /(?=.*[#@&$%+!]).*$/;
            if ($.selector_cache('#setting').data('spcl-char') === 'True' && !pattern.test(nPassword)) {
                numberOfErrors++;
                $.selector_cache('#PasswordErr').html('Password must have atleast one of the following special characters: #@&$%+!');
                return;
            }
            pattern = /(?=.*[a-z])(?=.*[A-Z]).*$/;
            if ($.selector_cache('#setting').data('case') === 'True' && !pattern.test(nPassword)) {
                numberOfErrors++;
                $.selector_cache('#PasswordErr').html('Password must have atleast one uppercase & one lowercase character.');
                return;
            }
            pattern = /(?=.*[0-9]).*$/;
            if ($.selector_cache('#setting').data('numeric') === 'True' && !pattern.test(nPassword)) {
                numberOfErrors++;
                $.selector_cache('#PasswordErr').html('Password must have atleast one numeric character.');
                return;
            }
        }
        else {
            numberOfErrors++;
            $.selector_cache('#PasswordErr').html('Cannot be empty');
        }
        if (0 !== numberOfErrors)
            return;
        $.ajax({
            url: '/User/ResetPassword',
            type: 'post',
            data: {
                'empId': $.selector_cache('#EmpId').prop('value'),
                'password': $.selector_cache('#Password').prop('value')
            },
            success: function (data) {
                if (data && data.IsSuccess) {
                    swal.fire('Success!', 'Password reset successfully.', 'success');
                } else if (data !== null) {
                    swal.fire('Error!', data.returnMessage, 'error');
                } else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            },
            complete: function () {
                $.selector_cache('#rpModal').modal('hide');
            }
        });
    });
})(jQuery);
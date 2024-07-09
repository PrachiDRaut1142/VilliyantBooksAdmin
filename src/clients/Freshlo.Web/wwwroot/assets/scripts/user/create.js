!(function ($) {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
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
    $('#Addharno').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnAddharno').html(req).css('display', 'block');
            }
        }
        else if ($('#Addharno').val() != null || $('#Addharno').val() != '') {
            $('#SpnAddharno').css('display', 'none');
        }
        else {
            $('#SpnAddharno').html(req).css('display', 'block');
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
        else if ($('#Branch').val() != null || $('#Branch').val() != ''|| $('#Branch').val() != '0') {
            $('#spnbranch').css('display', 'none');
        }
        else {
            $('#spnbranch').html(req).css('display', 'block');
        }
    });

    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal.fire('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');
    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal.fire('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }
    $(document).ready(function () {
        $(".showpassword").click(function () {
            if ($("#Password").attr("type") == "password") {
                $("#Password").attr("type", "text");

            } else {
                $("#Password").attr("type", "password");
            }
        });
        var sVisible = $('#IsfirstLoginTime').prop('checked');
        if (sVisible == true) {
            $('#IsfirstLogin').val(1);
        }
        else {
            $('#IsfirstLogin').val(0);
        }
        $('#IsfirstLoginTime').on('click', function () {
            var visibilty = $(this).prop('checked');
            if (visibilty == true) {
                $('#IsfirstLogin').val(1);
            }
            else {
                $('#IsfirstLogin').val(0);
            }
        });
    });

    $('#EmailId').on('change', function () {
        $.ajax({
            url: "/User/CheckUniqueEmailId?EmailId=" + this.value,
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

    $('#ContactNo').on('change', function () {
        $.ajax({
            url: "/User/CheckUniqueContactNo?phoneNo=" + this.value,
            type: "POST",
            success: function (result) {
                if (result.Data != "") {
                    swal.fire("error", "Contact Number Already Exists!", "error").then(okay => {
                        if (okay) {
                            $('#ContactNo').val('');
                        }
                    });
                }
            }
        });
    });

    $('#LoginId').on('change', function () {
        $.ajax({
            url: "/User/CheckUniqueloginId?loginId=" + this.value,
            type: "POST",
            success: function (result) {
                if (result.Data != "") {
                    swal.fire("error", "Login Id Already Exists!", "error").then(okay => {
                        if (okay) {
                            $('#LoginId').val('');
                        }
                    });
                }
            }
        });
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
        if ($('#Addharno').val() === "" || $('#Addharno').val() === null) {
            $('#SpnAddharno').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
            $('#CreateUser').submit();
        }
    });
    $('#clearBtn').on('click', function () {
        $('#FullName').val('');
        $('#UserRole').val('');
        $('#Branch').val('');
        $('#LoginId').val('');
        $('#EmailId').val('');
        $('#ContactNo').val('');
        $('#Address1').val('');
        $('#Addharno').val('');
        $('#Address2').val('');
        $('#City').val('');
        $('#country').val('');

        $('#SpnFullName').css('display', 'none');
        $('#SpnLoginId').css('display', 'none');
        $('#SpnEmailId').css('display', 'none');
        $('#SpnContactNo').css('display', 'none');
        $('#SpnAddress1').css('display', 'none');
        $('#SpnAddharno').css('display', 'none');
        $('#SpnUserRole').css('display', 'none');
        $('#spnbranch').css('display', 'none');
    });
})(jQuery);
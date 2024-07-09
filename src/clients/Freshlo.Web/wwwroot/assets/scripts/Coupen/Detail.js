$(function () {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    var req = 'This Field is is required';
    $('#CouponTitle').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCTitle').html(req).css('display', 'block');
            }
        }
        else if ($('#CTitle').val() != null || $('#CTitle').val() != '') {
            $('#SpnCTitle').css('display', 'none');
        }
        else {
            $('#SpnCTitle').html(req).css('display', 'block');
        }
    });
    $('#CoupenCode').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCoupenCode').html(req).css('display', 'block');
            }
        }
        else if ($('#CoupenCode').val() != null || $('#CoupenCode').val() != '') {
            $('#SpnCoupenCode').css('display', 'none');
            $.ajax({
                url: "/Coupen/CheckUniqueCouponcode?CoupenCode=" + this.value,
                type: "POST",
                success: function (result) {
                    if (result.Data != "") {
                        swal.fire("error", "Couponcode Already Exists!", "error").then(okay => {
                            if (okay) {
                                $('#CoupenCode').val('');
                            }
                        });
                    }
                }
            });
        }
        else {
            $('#SpnCoupenCode').html(req).css('display', 'block');
        }
    });
    $('#ShortDescription').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnShortD').html(req).css('display', 'block');
            }
        }
        else if ($('#ShortDescription').val() != null || $('#ShortDescription').val() != '') {
            $('#SpnShortD').css('display', 'none');
        }
        else {
            $('#SpnShortD').html(req).css('display', 'block');
        }
    });
    $('#MinOrderValue').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnMOV').html(req).css('display', 'block');
            }
        }
        else if ($('#MinOrderValue').val() != null || $('#MinOrderValue').val() != '') {
            $('#SpnMOV').css('display', 'none');
        }
        else {
            $('#SpnMOV').html(req).css('display', 'block');
        }
    });
    $('#MaxDiscount').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpMaxD').html(req).css('display', 'block');
            }
        }
        else if ($('#MaxDiscount').val() != null || $('#MaxDiscount').val() != '') {
            $('#SpMaxD').css('display', 'none');
        }
        else {
            $('#SpMaxD').html(req).css('display', 'block');
        }
    });
    $('#DiscountPercnt').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnDisPer').html(req).css('display', 'block');
            }
        }
        else if ($('#DiscountPercnt').val() != null || $('#DiscountPercnt').val() != '') {
            $('#SpnDisPer').css('display', 'none');
        }
        else {
            $('#SpnDisPer').html(req).css('display', 'block');
        }
    });
    $('#UsageAllowedperUser').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnUsagePerAllow').html(req).css('display', 'block');
            }
        }
        else if ($('#UsageAllowedperUser').val() != null || $('#UsageAllowedperUser').val() != '') {
            $('#SpnUsagePerAllow').css('display', 'none');
        }
        else {
            $('#SpnUsagePerAllow').html(req).css('display', 'block');
        }
    });
    if ($('#viewMessage1').prop('value')) {
        Swal.fire('Success', $('#viewMessage1').prop('value'), 'success');
    }
    if ($('#errorMessage1').prop('value')) {
        Swal.fire('Error', $('#errorMessage1').prop('value'), 'error');
    }
    $('#submitCuopen').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('#CouponTitle').val() === "" || $('#CouponTitle').val() === null) {
            $('#SpnCTitle').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#CoupenCode').val() === "" || $('#CoupenCode').val() === null) {
            $('#SpnCoupenCode').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#ShortDescription').val() === "" || $('#ShortDescription').val() === null) {
            $('#SpnShortD').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#MinOrderValue').val() === "" || $('#MinOrderValue').val() === null) {
            $('#SpnMOV').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#MaxDiscount').val() === "" || $('#MaxDiscount').val() === null) {
            $('#SpMaxD').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#DiscountPercnt').val() === "" || $('#DiscountPercnt').val() === null) {
            $('#SpnDisPer').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#UsageAllowedperUser').val() === "" || $('#UsageAllowedperUser').val() === null) {
            $('#SpnUsagePerAllow').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#errorMessage1').prop('value')) {
            Swal.fire('Error', $('#errorMessage1').prop('value'), 'error');
            nErrorCount++;
        }
        if (0 === nErrorCount) {
            $('#coupenIdUpdateFrm').submit();
        }
    });
});
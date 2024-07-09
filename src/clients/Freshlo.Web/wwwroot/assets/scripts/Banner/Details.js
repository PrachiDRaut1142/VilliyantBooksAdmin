$(function () {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    $('#bannerName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#Spnbannername').html(req).css('display', 'block');
            }
        }
        else if ($('#bannerName').val() != null || $('#bannerName').val() != '') {
            $('#Spnbannername').css('display', 'none');
        }
        else {
            $('#Spnbannername').html(req).css('display', 'block');
        }
    });
    //$('#TriggerId').on('change', function () {
    //    if ($('#TriggerId').val() > '0' || $('#TriggerId').val() != '' || $('#TriggerId').val() != null) {
    //        $('#SpnTriggerId').css('display', 'none');
    //    }
    //    else {
    //        $('#SpnTriggerId').html(req).css('display', 'block');
    //    }
    //});
    //$('#bannerSize').on('change', function () {
    //    if ($('#bannerSize').val() > '0' || $('#bannerSize').val() != '' || $('#bannerSize').val() != null) {
    //        $('#SpnbannerSize').css('display', 'none');
    //    }
    //    else {
    //        $('#SpnbannerSize').html(req).css('display', 'block');
    //    }
    //});
    //$('#ActionType').on('change', function () {
    //    if ($('#ActionType').val() > '0' || $('#ActionType').val() != '' || $('#ActionType').val() != null) {
    //        $('#SpnActionType').css('display', 'none');
    //    }
    //    else {
    //        $('#SpnActionType').html(req).css('display', 'block');
    //    }
    //});
    $('#VideoLink').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnVideoLink').html(req).css('display', 'block');
            }
        }
        else if ($('#VideoLink').val() != null || $('#VideoLink').val() != '') {
            $('#SpnVideoLink').css('display', 'none');
        }
        else {
            $('#SpnVideoLink').html(req).css('display', 'block');
        }
    });
    $('#TextLink').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnTextLink').html(req).css('display', 'block');
            }
        }
        else if ($('#TextLink').val() != null || $('#TextLink').val() != '') {
            $('#SpnTextLink').css('display', 'none');
        }
        else {
            $('#SpnTextLink').html(req).css('display', 'block');
        }
    });
    var branchMappedId = $('#BranchMapped1').val();
    if (branchMappedId == "Yes") {
        $("#BranchMapped").prop('checked', false);
    }
    else {
        $("#BranchMapped").prop('checked', true);
    }

    if ($('#BranchMapped').prop('checked') == true) {
        $('.Branchshow').css('display', 'flex');
    }
    else {
        $('.Branchshow').css('display', 'none');
    }

    $('#BranchMapped').on('click', function () {
        if ($(this).prop('checked') == true) {
            $('.Branchshow').css('display', 'flex')
        }
        else {
            $('.Branchshow').css('display', 'none')
        }
    });

    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');
    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }

    $('#ActionType').on('change', function () {
        var trigger = $('#ActionType').val();
        if (trigger != "Tag") {
            $('#TriggerIddiv').css('display', 'block');
            $('#LinkRowId').css('display', 'none');
            $('#tagRowId').css('display', 'none');
            $('#VideoLinks').css('display', 'none');
            $.ajax({
                url: "/Banner/GetActionTriggerlist?trigger=" + trigger,
                type: "Get",
                success: function (getactionlist) {
                    if (getactionlist.length > 0) {
                        var html = "";
                        html = '<option value="0" selected >Select Trigger Name</option>';
                        $.each(getactionlist, function (index, value) {
                            html += '<option value="' + value.value + '">' + value.text + '</option>';
                        });
                        $('#TriggerId').html(html);
                    }
                }
            });
        }
        if (trigger == "Tag") {
            $('#TriggerIddiv').css('display', 'none');
            $('#tagRowId').css('display', 'block');
            $('#LinkRowId').css('display', 'none');
            $('#VideoLinks').css('display', 'none');
        }
        if (trigger == "Inner Web View" || trigger == "Third Party Link") {
            $('#TriggerIddiv').css('display', 'none');
            $('#tagRowId').css('display', 'none');
            $('#LinkRowId').css('display', 'block');
            $('#VideoLinks').css('display', 'none');
        }
        if (trigger == "Video Link") {
            $('#VideoLinks').css('display', 'block');
            $('#TriggerIddiv').css('display', 'none');
            $('#tagRowId').css('display', 'none');
            $('#LinkRowId').css('display', 'none');
        }
    });

    var trigger = $('#ActionType').val();
    if (trigger == "Tag") {
        $('#VideoLinks').css('display', 'none');
        $('#TriggerIddiv').css('display', 'none');
        $('#tagRowId').css('display', 'block');
        $('#LinkRowId').css('display', 'none');
    }
    if (trigger == "Video Link") {
        $('#VideoLinks').css('display', 'block');
        $('#TriggerIddiv').css('display', 'none');
        $('#tagRowId').css('display', 'none');
        $('#LinkRowId').css('display', 'none');
    }
    if (trigger == "Inner Web View" || trigger == "Third Party Link") {
        $('#VideoLinks').css('display', 'none');
        $('#TriggerIddiv').css('display', 'none');
        $('#tagRowId').css('display', 'none');
        $('#LinkRowId').css('display', 'block');
    }
    if ($('#place').val() == 'Top') {
        $('#PlaceBanner').css('display', 'none');
        $("#ScrollBaner").val("Y").prop('selected', true);
    }
    else {
        $('#PlaceBanner').css('display', 'block');
        $("#ScrollBaner").val("N").prop('selected', true);
    }

    $('#ScrollBaner').on('change', function () {
        var bannerplace = $('#ScrollBaner').val();
        if (bannerplace == "N") {
            $('#PlaceBanner').css('display', 'block');
            $('#Place').prop('disabled', false);
        }
        else {
            $('#PlaceBanner').css('display', 'none');
            $('#Place').prop('disabled', true);
        }
    });

    $("#imageFiles").change(function () {
        readURL(this);
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imgfiles').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    $('#UpdateBanner').on('click', function () {
        var req = 'This field is required';
        var ActionType = $('#ActionType').val();
        var TextLink = $('#TextLink').val();
        var VideoLink = $('#VideoLink').val();
        var TriggerId = $('#TriggerId').val();
        var nErrorCount = 0;
        if ($('#bannerName').val() === null || $('#bannerName').val() === '') {
            $('#Spnbannername').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (ActionType != '0') {
            if (ActionType == 'Inner Web View' && (TextLink === '0' || TextLink === '' || TextLink === null)) {
                $('#SpnTextLink').html(req).css('display', 'block');
                nErrorCount += 1;
            }
            else if (ActionType == 'Video Link' && (VideoLink === '0' || VideoLink === '' || VideoLink === null)) {
                $('#SpnVideoLink').html(req).css('display', 'block');
                nErrorCount += 1;
            }
            else if (ActionType == 'Third Party Link' && (TextLink === '0' || TextLink === '' || TextLink === null)) {
                $('#SpnTextLink').html(req).css('display', 'block');
                nErrorCount += 1;
            }
            else {
                $('#SpnVideoLink').html(req).css('display', 'none');
            }
        }
        if (0 === nErrorCount) {
            $('#bannerUpdate').submit();
        }
    });

    var scrollbannervalue = $('#scrollBanner').val();
    if (scrollbannervalue == "0") {
        $('#PlaceBanner').css('display', 'block');
        $("#ScrollBaner option[value='" + N + "']").prop('selected', true);
    }
    else {
        $('#PlaceBanner').css('display', 'none');
        $('#ScrollBaner option[value=Y]').attr('selected', 'selected');
    }
});
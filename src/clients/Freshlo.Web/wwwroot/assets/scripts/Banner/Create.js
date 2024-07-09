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
    $('#bannerSize').on('change', function () {
        if ($('#bannerSize').val() > '0' || $('#bannerSize').val() != '' || $('#bannerSize').val() != null) {
            $('#SpnbannerSize').css('display', 'none');
        }
        else {
            $('#SpnbannerSize').html(req).css('display', 'block');
        }
    });
    //$('#bannerPlace').on('change', function () {
    //    var bannerplace = $('#ScrollBaner').val();
    //    if (bannerplace == "N") {
    //        if ($('#bannerPlace').val() > '0' || $('#bannerPlace').val() != '' || $('#bannerPlace').val() != null) {
    //            $('#SpnbannerPlace').css('display', 'none');
    //        }
    //        else {
    //            $('#SpnbannerPlace').html(req).css('display', 'block');
    //        }
    //    }
    //    else {
    //        $('#SpnbannerPlace').css('display', 'none');}
    //});
    $('#ActionType').on('change', function () {
        if ($('#ActionType').val() > '0' || $('#ActionType').val() != '' || $('#ActionType').val() != null) {
            $('#SpnActionType').css('display', 'none');
        }
        else {
            $('#SpnActionType').html(req).css('display', 'block');
        }
    });
    $('#TriggerId').on('change', function () {
        if ($('#TriggerId').val() > '0' || $('#TriggerId').val() != '' || $('#TriggerId').val() != null) {
            $('#SpnTriggerId').css('display', 'none');
        }
        else {
            $('#SpnTriggerId').html(req).css('display', 'block');
        }
    });
    $('#imageFiles').on('change', function (e) {
        if ($('#imageFiles').val() != "" || $('#imageFiles').val() != null) {
            $('#SpnimageFiles').css('display', 'none');
        }
        else {
            $('#SpnimageFiles').html(req).css('display', 'block');
        }        
    });
    $('#BannerClear').on('click', function () {
        $('#bannerName').val('');
        $('#bannerSize').val(0);
        $('#bannerPlace').val(0);
        $('#ActionType').val(0);
        $('#imageFiles').val('');
        $('#TextLink').val('');
        $('#VideoLink').val('');
        $('#TriggerId').val('');
        $('#Spnbannername').css('display', 'none');
        $('#SpnTextLink').css('display', 'none');
        $('#SpnVideoLink').css('display', 'none');
        $('#SpnbannerSize').css('display', 'none');
        $('#SpnbannerPlace').css('display', 'none');
        $('#SpnActionType').css('display', 'none');
        $('#SpnTriggerId').css('display', 'none');
        $('#SpnimageFiles').css('display', 'none');
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
                    var html = "";
                    if (getactionlist.length > 0) {
                        html = '<option value="0" selected >Choose Action</option>';
                        $.each(getactionlist, function (index, value) {
                            html += '<option value="' + value.value + '">' + value.text + '</option>';
                        });
                        $('#TriggerId').html(html);
                    }
                    else {
                        $('#TriggerId').html('No Data');
    
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

    $('#ScrollBaner').on('change', function () {
        var bannerplace = $('#ScrollBaner').val();
        if (bannerplace == "N") {
            $('#PlaceBanner').css('display', 'block');
        }
        else {
            //$('#PlaceBanner').val('SB');
            $('#PlaceBanner').css('display', 'none');
            //$('#PlaceBanner option[value="SB"]').attr('selected', 'selected');
        }
    });

    $('#submitBanner').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        var ActionType = $('#ActionType').val();
        var TextLink = $('#TextLink').val();
        var VideoLink = $('#VideoLink').val();
        var TriggerId = $('#TriggerId').val();
        var bannername = $('#bannerName').val();
        if ($('#bannerName').val() === null || $('#bannerName').val() === '')
        {
            $('#Spnbannername').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#bannerSize').val() === '0' || $('#bannerSize').val() === '' || $('#bannerSize').val() === null)
        {
            $('#SpnbannerSize').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        //if ($('#bannerPlace').val() === '0' || $('#bannerPlace').val() === '' || $('#bannerPlace').val() === null)
        //{
        //    $('#SpnbannerPlace').html(req).css('display', 'block');
        //    nErrorCount += 1;
        //}
        if ($('#ActionType').val() === '0' || $('#ActionType').val() === '' || $('#ActionType').val() === null)
        {
            $('#SpnActionType').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#imageFiles').val() === "" || $('#imageFiles').val() === null)
        {
            $('#SpnimageFiles').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        
        if (ActionType != '0')
        {
            if (ActionType == 'Offer' && (TriggerId === '0' || TriggerId === '' || TriggerId === null)) {
                $('#SpnTriggerId').html(req).css('display', 'block');
                nErrorCount += 1;
            }
            else if (ActionType == 'MainCategory' && (TriggerId === '0' || TriggerId === '' || TriggerId === null)) {
                $('#SpnTriggerId').html(req).css('display', 'block');
                nErrorCount += 1;
            }
            else if (ActionType == 'Custom Item' && (TriggerId === '0' || TriggerId === '' || TriggerId === null)) {
                $('#SpnTriggerId').html(req).css('display', 'block');
                nErrorCount += 1;
            }
            else if (ActionType == 'Inner Web View' && (TextLink === '0' || TextLink === '' || TextLink === null)) {
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
                $('#SpnTriggerId').html(req).css('display', 'none');
                $('#SpnVideoLink').html(req).css('display', 'none');
            }            
        }
        if (0 === nErrorCount) {
            swal.fire('Success!', bannername + 'Created Successfully', 'success');
            $('#bannerIdcreratFrm').submit();
        }
        else {
            swal.fire('Error!', 'There was some error. Try again later.', 'error');
        }
    });

    $('#BranchMapped').on('click', function () {
        if ($(this).prop('checked') == true) {
            $('.Branchshow').css('display','flex')
        }
        else {
            $('.Branchshow').css('display', 'none')
        }

    });


    
});
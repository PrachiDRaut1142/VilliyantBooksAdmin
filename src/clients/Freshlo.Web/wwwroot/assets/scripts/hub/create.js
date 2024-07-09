$(function () {
    var req = 'This Field is is required';
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    $('#HubName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnHubName').html(req).css('display', 'block');
            }
        }
        else if ($('#HubName').val() != null || $('#HubName').val() != '') {
            $('#SpnHubName').css('display', 'none');
        }
        else {
            $('#SpnHubName').html(req).css('display', 'block');
        }
    });
    $('#BrnachEmail').on('keyup', function (e) {
        var email = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var $EamilAddress = $('#BrnachEmail').val();
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnBrnachEmail').html(req).css('display', 'block');
            }
        }
        if (email.test($EamilAddress) == false && $('#BrnachEmail').val().length > 0) {
            $('#SpnBrnachEmail').html('Invalid Email Address').css('display', 'block');
        }
        else {
            $('#SpnBrnachEmail').css('display', 'none');
        }
    });
    $('#BranchNotifyEmail').on('keyup', function (e) {
        var email = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var $EamilAddress = $('#BranchNotifyEmail').val();
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnBranchNotifyEmail').html(req).css('display', 'block');
            }
        }
        if (email.test($EamilAddress) == false && $('#BranchNotifyEmail').val().length > 0) {
            $('#SpnBranchNotifyEmail').html('Invalid Email Address').css('display', 'block');
        }
        else {
            $('#SpnBranchNotifyEmail').css('display', 'none');
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

    if ($('#viewMessage1').prop('value')) {
        Swal.fire('Success', $('#viewMessage1').prop('value'), 'success');
    }
    if ($('#errorMessage1').prop('value')) {
        Swal.fire('Error', $('#errorMessage1').prop('value'), 'error');
    }
    $('#submitHub').on('click', function () {
        var nErrorCount = 0;
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
        if ($('#HubName').val() === "" || $('#HubName').val() === null) {
            $('#SpnHubName').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#errorMessage1').prop('value')) {
            Swal.fire('Error', $('#errorMessage1').prop('value'), 'error');
            nErrorCount++;
        }
        if (0 === nErrorCount) {
            $('#HubCreate').submit();
        }
    });
    $('#HubCreateClear').on('click', function () {
        $('#HubName').val('');
        $('#BrnachEmail').val('');
        $('#BranchNotifyEmail').val('');
        $('#ContactNo').val('');
        $('#currency').val('INR');
        $('#Area').val('');        
        $('#BuildingName').val('');
        $('#RoomNo').val('');
        $('#Sector').val('');
        $('#Landmark').val('');
        $('#MapCode').val('');
        $('#City').val('');
        $('#State').val('');
        $('#Country').val('');

        $('#SpnHubName').css('display', 'none');
        $('#SpnBrnachEmail').css('display', 'none');
        $('#SpnBranchNotifyEmail').css('display', 'none');
        $('#SpnContactNo').css('display', 'none');
    });
});
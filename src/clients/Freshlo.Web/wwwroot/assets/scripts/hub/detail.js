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
    if ($('#viewMessage1').prop('value')) {
        Swal.fire('Success', $('#viewMessage1').prop('value'), 'success');
    }

    if ($('#errorMessage1').prop('value')) {
        Swal.fire('Error', $('#errorMessage1').prop('value'), 'error');
    }

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

    $(document).ready(function () {
        
        var IsFacebookEnable = $('#IsFacebookEnable').is(':checked');
        if (IsFacebookEnable == true) {
            $('.facebookLink').css('display', 'block');
        }
        else {
            $('.facebookLink').css('display', 'none');
        }
        var IsInstaEnable = $('#IsInstaEnable').is(':checked');
        if (IsInstaEnable == true) {
            $('.instaLink').css('display', 'block');
        }
        else {
            $('.instaLink').css('display', 'none');
        }
        var IsTwitterEnable = $('#IsTwitterEnable').is(':checked');
        if (IsTwitterEnable == true) {
            $('.twitterLink').css('display', 'block');
        }
        else {
            $('.twitterLink').css('display', 'none');
        }
        var IsSnapchatEnable = $('#IsSnapchatEnable').is(':checked');
        if (IsSnapchatEnable == true) {
            $('.snapChatLink').css('display', 'block');
        }
        else {
            $('.snapChatLink').css('display', 'none');
        }
        var IsLinkedInEnable = $('#IsLinkedInEnable').is(':checked');
        if (IsLinkedInEnable == true) {
            $('.linkedInLink').css('display', 'block');
        }
        else {
            $('.linkedInLink').css('display', 'none');
        }
        var IsGoogleMapEnable = $('#IsGoogleMapEnable').is(':checked');
        if (IsGoogleMapEnable == true) {
            $('.googleMapLink').css('display', 'block');
        }
        else {
            $('.googleMapLink').css('display', 'none');
        }
        var IsPrinterestEnable = $('#IsPrinterestEnable').is(':checked');
        if (IsPrinterestEnable == true) {
            $('.printeresterLink').css('display', 'block');
        }
        else {
            $('.printeresterLink').css('display', 'none');
        }
        var IsWhatsAppEnable = $('#IsWhatsAppEnable').is(':checked');
        if (IsWhatsAppEnable == true) {
            $('.WhatsAppLink').css('display', 'block');
        }
        else {
            $('.WhatsAppLink').css('display', 'none');
        }
        var IsYoutubeEnable = $('#IsYoutubeEnable').is(':checked');
        if (IsYoutubeEnable == true) {
            $('.IsyouTubeLink').css('display', 'block');
        }
        else {
            $('.IsyouTubeLink').css('display', 'none');
        }
        var IsGoogleReviewEnable = $('#IsGoogleReviewEnable').is(':checked');
        if (IsGoogleReviewEnable == true) {
            $('.GoogleReviewLink').css('display', 'block');
        }
        else {
            $('.GoogleReviewLink').css('display', 'none');
        }
    });
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
            $('#UpdateHub').submit();
        }
    });
    $('#IsFacebookEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsFacebookEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Facebook Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsFacebookEnable = $('#IsFacebookEnable').is(':checked');
                                if (IsFacebookEnable == true) {
                                    $('.facebookLink').css('display', 'block');
                                }
                                else {
                                    $('.facebookLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Facebook Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsFacebookEnable = $('#IsFacebookEnable').is(':checked');
                                if (IsFacebookEnable == true) {
                                    $('.facebookLink').css('display', 'block');
                                }
                                else {
                                    $('.facebookLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $('#IsInstaEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsInstaEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Instagram Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsInstaEnable = $('#IsInstaEnable').is(':checked');
                                if (IsInstaEnable == true) {
                                    $('.instaLink').css('display', 'block');
                                }
                                else {
                                    $('.instaLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Instagram Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsInstaEnable = $('#IsInstaEnable').is(':checked');
                                if (IsInstaEnable == true) {
                                    $('.instaLink').css('display', 'block');
                                }
                                else {
                                    $('.instaLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $('#IsTwitterEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsTwitterEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Twitter Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsTwitterEnable = $('#IsTwitterEnable').is(':checked');
                                if (IsTwitterEnable == true) {
                                    $('.twitterLink').css('display', 'block');
                                }
                                else {
                                    $('.twitterLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Twitter Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsTwitterEnable = $('#IsTwitterEnable').is(':checked');
                                if (IsTwitterEnable == true) {
                                    $('.twitterLink').css('display', 'block');
                                }
                                else {
                                    $('.twitterLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $('#IsSnapchatEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsSnapchatEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "SnapChat Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsSnapchatEnable = $('#IsSnapchatEnable').is(':checked');
                                if (IsSnapchatEnable == true) {
                                    $('.snapChatLink').css('display', 'block');
                                }
                                else {
                                    $('.snapChatLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "SnapChat Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsSnapchatEnable = $('#IsSnapchatEnable').is(':checked');
                                if (IsSnapchatEnable == true) {
                                    $('.snapChatLink').css('display', 'block');
                                }
                                else {
                                    $('.snapChatLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $('#IsLinkedInEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsLinkedInEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "LinkedIn Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsLinkedInEnable = $('#IsLinkedInEnable').is(':checked');
                                if (IsLinkedInEnable == true) {
                                    $('.linkedInLink').css('display', 'block');
                                }
                                else {
                                    $('.linkedInLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "LinkedIn Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsLinkedInEnable = $('#IsLinkedInEnable').is(':checked');
                                if (IsLinkedInEnable == true) {
                                    $('.linkedInLink').css('display', 'block');
                                }
                                else {
                                    $('.linkedInLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $('#IsGoogleMapEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsGoogleMapEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Google Map Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsGoogleMapEnable = $('#IsGoogleMapEnable').is(':checked');
                                if (IsGoogleMapEnable == true) {
                                    $('.googleMapLink').css('display', 'block');
                                }
                                else {
                                    $('.googleMapLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Google Map Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsGoogleMapEnable = $('#IsGoogleMapEnable').is(':checked');
                                if (IsGoogleMapEnable == true) {
                                    $('.googleMapLink').css('display', 'block');
                                }
                                else {
                                    $('.googleMapLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    $('#IsPrinterestEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsPrinterestEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Printrest Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsPrinterestEnable = $('#IsPrinterestEnable').is(':checked');
                                if (IsPrinterestEnable == true) {
                                    $('.printeresterLink').css('display', 'block');
                                }
                                else {
                                    $('.printeresterLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Printrest Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsPrinterestEnable = $('#IsPrinterestEnable').is(':checked');
                                if (IsPrinterestEnable == true) {
                                    $('.printeresterLink').css('display', 'block');
                                }
                                else {
                                    $('.printeresterLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });

    $('#IsWhatsAppEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsWhatsAppEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "WhatsApp Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsWhatsAppEnable = $('#IsWhatsAppEnable').is(':checked');
                                if (IsWhatsAppEnable == true) {
                                    $('.WhatsAppLink').css('display', 'block');
                                }
                                else {
                                    $('.WhatsAppLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "WhatsApp Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsWhatsAppEnable = $('#IsWhatsAppEnable').is(':checked');
                                if (IsWhatsAppEnable == true) {
                                    $('.WhatsAppLink').css('display', 'block');
                                }
                                else {
                                    $('.WhatsAppLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });

    $('#IsYoutubeEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsYoutubeEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Youtube Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsYoutubeEnable = $('#IsYoutubeEnable').is(':checked');
                                if (IsYoutubeEnable == true) {
                                    $('.IsyouTubeLink').css('display', 'block');
                                }
                                else {
                                    $('.IsyouTubeLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Youtube Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsYoutubeEnable = $('#IsYoutubeEnable').is(':checked');
                                if (IsYoutubeEnable == true) {
                                    $('.IsyouTubeLink').css('display', 'block');
                                }
                                else {
                                    $('.IsyouTubeLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });

    $('#IsGoogleReviewEnable').on('click', function () {
        var sVisible = $(this).prop('checked');
        var branchId = $('#id').val();
        $.ajax({
            url: "/Hub/IsGoogleReviewEnable?isEnable=" + sVisible + "&branchId=" + branchId,
            type: "GET",
            success: function (value) {
                if (value.IsSuccess === true) {
                    if (sVisible === true) {
                        swal.fire("success", "Google Review Enabled!", "success").then(okay => {
                            if (okay) {
                                var IsGoogleReviewEnable = $('#IsGoogleReviewEnable').is(':checked');
                                if (IsGoogleReviewEnable == true) {
                                    $('.GoogleReviewLink').css('display', 'block');
                                }
                                else {
                                    $('.GoogleReviewLink').css('display', 'none');
                                }
                            }
                        });
                    } else {
                        swal.fire("success", "Google Review Disabled!", "success").then(okay => {
                            if (okay) {
                                var IsGoogleReviewEnable = $('#IsGoogleReviewEnable').is(':checked');
                                if (IsGoogleReviewEnable == true) {
                                    $('.GoogleReviewLink').css('display', 'block');
                                }
                                else {
                                    $('.GoogleReviewLink').css('display', 'none');
                                }
                            }
                        });
                    }
                }
            }
        });
    });
});
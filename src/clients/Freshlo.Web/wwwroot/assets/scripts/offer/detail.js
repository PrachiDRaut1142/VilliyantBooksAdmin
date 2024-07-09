$(function () {
    var startTime = $('#OffStart-time').val();
    var endTime = $('#OffEnd-time').val();
    $('#OffStart-Time').val(startTime);
    $('#OffEnd-Time').val(endTime);
});

!(function ($) {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });

    //Disable all keyboard keys
    $(".dateTimepicker").keydown(false);

    $('#OfferHeading').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnOfferHeading').html(req).css('display', 'block');
            }
        }
        else if ($('#OfferHeading').val() != null || $('#OfferHeading').val() != '') {
            $('#SpnOfferHeading').css('display', 'none');
        }
        else {
            $('#SpnOfferHeading').html(req).css('display', 'block');
        }
    });
    $('#DiscountPerctg').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnDiscountPerctg').html(req).css('display', 'block');
            }
        }
        if ((e.which != 46 || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
            e.preventDefault();
            $('#DiscountPerctg').val('');
        }
        else if ($('#DiscountPerctg').val() != null || $('#DiscountPerctg').val() != '') {
            $('#SpnDiscountPerctg').css('display', 'none');
        }
        else {
            $('#SpnDiscountPerctg').html(req).css('display', 'block');
        }
    });
    $('#OffStartdate').on('change', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#EventStartTimeErr').html(req).css('display', 'block');
            }
        }
        else if ($('#OffStartdate').val() != null || $('#OffStartdate').val() != '') {
            $('#EventStartTimeErr').css('display', 'none');
        }
        else {
            $('#EventStartTimeErr').html(req).css('display', 'block');
        }
    });
    $('#OffEndtdate').on('change', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#EventEndTimeErr').html(req).css('display', 'block');
            }
        }
        else if ($('#OffEndtdate').val() != null || $('#OffEndtdate').val() != '') {
            $('#EventEndTimeErr').css('display', 'none');
        }
        else {
            $('#EventEndTimeErr').html(req).css('display', 'block');
        }
    });

    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal.fire('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');
    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal.fire('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }
    $('.availableitem').on('shown.bs.tab', function () {
        AvailableItem();
    });
    function AvailableItem() {
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Offer/_GetAllPricelist/',
            type: 'GET',
            success: function (result) {
                $('#PricelistBody-Id').html(result.Data);
                $('#OffertblId').DataTable();
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    } 
    $('.MappedOfferitem').on('shown.bs.tab', function () {
        $('#m_portlet_loader').css('display', 'block');
        var Id = $('#OfferId').prop('value');
        $.ajax({
            url: '/Offer/_GetOfferMapPricelist/?Id=' + Id,
            type: 'GET',
            success: function (result) {
                $('#MappedOfferItem-Id').html(result.Data);
                $('#MappedOfferItem').DataTable();
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    });
    $(document).ready(function () {
        var startTime = $('#OffStart-time').val();
        var endTime = $('#OffEnd-time').val();
        $('#OffStart-Time').val(startTime);
        $('#OffEnd-Time').val(endTime);

    });
    $(document).ready(function () {
        $.selector_cache('#MappedOfferItem-Id').on('click', '.removeOfferItem', function () {
            var OfferItemId = $(this);
            var removeOfferItemId = OfferItemId.data('id');
            swal.fire({
                title: "Are you sure ?",
                text: "you want to remove this Item From Offer",
                showCancelButton: true,
                confirmButtonText: "Yes remove it !",
                confirmButton: true,
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        type: "POST",
                        url: '/Offer/DeleteOfferItem/',
                        data: {
                            Id: removeOfferItemId
                        },
                        success: function (data) {
                            if (data && data.IsSuccess) {
                                $.ajax({
                                    url: '/Offer/_GetOfferMapPricelist/?Id=' + $.selector_cache('#OfferId').prop('value'),
                                    type: "GET",
                                    success: function (data) {
                                        if (data && data.IsSuccess) {
                                            swal.fire('Success', "Succesfully remove this Item From Offer", 'sucess');
                                            $('#MappedOfferItem-Id').html(data.Data);
                                        }
                                        else {
                                            swal.fire('Error', data.ReturnMessage, "Error");
                                        }
                                    }
                                });
                            }
                            else if (data == null) {
                                swal.fire('Error', "Data is Empty", 'Error');
                            }
                            else {
                                swal.fire('Error', 'These was some error try again later', 'error');
                            }
                        }
                    });
                }
                else {
                    swal.fire("Error", "Something Went Wrong Please Try After Sometimes");
                }
            });
        });
    });

    $(document).ready(function () {
        $.selector_cache('#PricelistBody-Id').on('click', '.addItem', function () {
            var itemId = $(this)[0].dataset.id;
            var startDate = $('#IdStartDate').val();
            var Id = $('#OfferId').val();
            var OfferId = $('#OfferIdS').val();
            swal({
                title: "Are you sure ?",
                text: "you want to add this Item to the Offer",
                showCancelButton: true,
                confirmButtonText: "Yes Add it !",
                confirmButton: true,
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        ItemId: itemId,
                        Startdate: startDate,
                        Id: Id,
                        OfferId: OfferId
                    };
                    $.ajax({
                        url: '/Offer/AddToItem',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (data) {
                            if (data && data.IsSuccess) {
                                $.ajax({
                                    url: '/Offer/_GetAllPricelist/',
                                    type: "GET",
                                    success: function (data) {
                                        if (data && data.IsSuccess) {
                                            swal.fire('Success', "Succesfully add this Item From Offer", 'sucess');
                                            AvailableItem1();
                                        }
                                        else {
                                            swal.fire('Error', data.ReturnMessage, "Error");
                                        }
                                    }
                                });
                            }
                            else if (data == null) {
                                swal.fire('Error', "Data is Empty", 'Error');
                            }
                            else {
                                swal.fire('Error', 'These was some error try again later', 'error');
                            }
                        }
                    });
                }
                else {
                    swal.fire("Error", "Something Went Wrong Please Try After Sometimes");
                }
            });
        });
    });

    $('#UpdateOffer').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('#OfferHeading').val() === "" || $('#OfferHeading').val() === null) {
            $('#SpnOfferHeading').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#DiscountPerctg').val() === "" || $('#DiscountPerctg').val() === null) {
            $('#SpnDiscountPerctg').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#OffStartdate').val() === "" || $('#OffStartdate').val() === null) {
            $('#EventStartTimeErr').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#OffEndtdate').val() === "" || $('#OffEndtdate').val() === null) {
            $('#EventEndTimeErr').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
            $('#UpdateOfferFrm').submit();
        }
    });

    $('.filter').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        var tab = $(this)[0].id;
        var MainCategoryId = $.selector_cache("#" + tab + "_MainCategoryId").prop('value');
        var CategoryId = $.selector_cache("#" + tab + "_CategoryId").prop('value');
        var BrandId = $.selector_cache("#" + tab + "_BrandId").prop('value');
        var VendorId = $.selector_cache("#" + tab + "_VendorId").prop('value');
        var Id = $('#Ids').val(); 
        var info = {
            MainCategoryId: MainCategoryId,
            CategoryId: CategoryId,
            BrandId: BrandId,
            VendorId: VendorId,
        }
        if (tab == "availableitem") {
            $.ajax({
                url: '/Offer/_GetAllPricelist/',
                type: 'GET',
                data: info,
                success: function (result) {
                    $('#OffertblId').DataTable().destroy();
                    $('#PricelistBody-Id').html(result.Data);
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        }
        else {
            $.ajax({
                url: '/Offer/_GetOfferMapPricelist/?id=' + Id,
                type: 'GET',
                data: info,
                success: function (result) {
                    $('#MappedOfferItem').DataTable().destroy();
                    $('#MappedOfferItem-Id').html(result.Data);
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        }
        $('#m_portlet_loader').css('display', 'none');
    });

    $('.clear').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        var tab = $(this)[0].id;
        $("#" + tab + "_MainCategoryId option[value=0]").prop('selected', true);
        $("#" + tab + "_CategoryId option[value=0]").prop('selected', true);
        $("#" + tab + "_BrandId option[value=0]").prop('selected', true);
        $("#" + tab + "_VendorId option[value=0]").prop('selected', true);
        var MainCategoryId = $.selector_cache("#_MainCategoryId ").prop('value');
        var CategoryId = $.selector_cache("#_CategoryId").prop('value');
        var BrandId = $.selector_cache("#" + tab + "_BrandId").prop('value');
        var VendorId = $.selector_cache("#" + tab + "_VendorId").prop('value');
        var Id = $('#Ids').val();
        var info = {
            MainCategoryId: MainCategoryId,
            CategoryId: CategoryId,
            BrandId: BrandId,
            VendorId: VendorId,
        }
        if (tab == "availableitem") {
            $.ajax({
                url: '/Offer/_GetAllPricelist/',
                type: 'GET',
                data: info,
                success: function (result) {
                    $('#OffertblId').DataTable().destroy();
                    $('#PricelistBody-Id').html(result.Data);
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        }
        else {
            $.ajax({
                url: '/Offer/_GetOfferMapPricelist/?id=' + Id,
                type: 'GET',
                data: info,
                success: function (result) {
                    $('#MappedOfferItem').DataTable().destroy();
                    $('#MappedOfferItem-Id').html(result.Data);
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        }
        $('#m_portlet_loader').css('display', 'none');
    });

    $('#OffertblId').on('change', '#selectallid', function (e) {
        var table = $(e.target).closest('table');
        $('td input:checkbox', table).prop('checked', this.checked);
    });

    var getChecked = function () {
        return $('#PricelistBody-Id').find('input[type="checkbox"]')
            .filter(':checked')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    $(document).on('click', '#AddItemId', function () {
        var id = getChecked();
        var itemIds = id;
        var offerIds = $('#OfferIds').val();
        var createdBy = $('#CreatedBy').val();
        if (id.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to add item !',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Create it!'
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        ItemIds: itemIds,
                        OfferId: offerIds,
                        CreatedBy:createdBy
                    };            
                    $.ajax({
                        url: '/Offer/AddToItemList/',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (result) {
                            if (result.IsSuccess == true) {
                                swal({
                                    text: "Successfully Added to Offer",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    location.reload();
                                })
                            }
                        }
                    });
                }
            });
        }
    });

    $('#MappedOfferItem').on('change', '#selectallid', function (e) {
        var table = $(e.target).closest('table');
        $('td input:checkbox', table).prop('checked', this.checked);
    });

    var getCheckedall = function () {
        return $('#MappedOfferItem-Id').find('input[type="checkbox"]')
            .filter(':checked')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    $(document).on('click', '#deleteId', function () {
        var id = getCheckedall();
        if (id.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to delete!',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: '/Offer/DeleteMappingItem?ids=' + id,
                        type: 'GET',
                        success: function (result) {
                            if (result.IsSuccess == true) {
                                swal({
                                    text: "Successfully remove to Offer",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    location.reload();
                                })   
                            }
                        }
                    });
                }
            });
        }
    });
})(jQuery);
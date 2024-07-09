!(function ($) {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    if ($('#offer_limit').prop("checked") == true) {
        var currentValue = "YES";

        $('#toggleValue').val(currentValue);
        if (currentValue == 'YES') {
            $("#yesLimitedOffer").css("display", "block");
        }
        else {
            $("#yesLimitedOffer").css("display", "none");
        }
    }
    //float-number Validation
    //$('.float-number').keypress(function (event) {
    //    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
    //        event.preventDefault();
    //    }
    //});

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
    $('#submitOffer').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('#choose_offer').val() === "" || $('#choose_offer').val() === null) {
            $('#SpnOfferoffer').html(req).css('display', 'block');
            nErrorCount += 1;
        } if ($('#OfferHeading').val() === "" || $('#OfferHeading').val() === null) {
            $('#SpnOfferHeading').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        //if ($('#DiscountPerctg').val() === "" || $('#DiscountPerctg').val() === null) {
        //    $('#SpnDiscountPerctg').html(req).css('display', 'block');
        //    nErrorCount += 1;
        //}
        //if ($('#OffStartdate').val() === "" || $('#OffStartdate').val() === null) {
        //    $('#EventStartTimeErr').html(req).css('display', 'block');
        //    nErrorCount += 1;
        //}
        //if ($('#OffEndtdate').val() === "" || $('#OffEndtdate').val() === null) {
        //    $('#EventEndTimeErr').html(req).css('display', 'block');
        //    nErrorCount += 1;
        //}
        if ($('#imageFiles').val() === "" || $('#imageFiles').val() === null) {
            $('#SpnimageFiles').html(req).css('display', 'block');
            nErrorCount += 1;
        }

        //if ($('#BuyItemId').val() === "" || $('#BuyItemId').val() === null) {
        //    $('#SpnimageFiles').html(req).css('display', 'block');
        //    nErrorCount += 1;
        //}
        //var buyIds = $('#OfferIds').val();
        //if (buyIds) {            
        //    for (var i = 0; i < buyIds.length; i++) {
        //        alert(buyIds[i]);                
        //    }
        //}
        if (0 === nErrorCount) {
            $('#OfferFrm').submit();
        }
    });
    $('#clearBtn').on('click', function () {
        $('#OfferHeading').val('');
        $('#DiscountPerctg').val('');
        $('#OffStartdate').val('');
        $('#OffEndtdate').val('');
        $('#OfferDescription').val('');
        $('#SpnOfferHeading').css('display', 'none');
        $('#SpnDiscountPerctg').css('display', 'none');
        $('#EventStartTimeErr').css('display', 'none');
        $('#EventEndTimeErr').css('display', 'none');
    });


    $("#choose_offer").on('change', function () {
        var value = $('#choose_offer :selected').val();

        if (value == "1") {
            $("#flash_deal_view").css("display", "block");
            $(".flashdisc").css("display", "block");
            $(".bundledisc").css("display", "none");
            $(".bulkdisc").css("display", "none");
            /*$("#bundle_offer_view").css("display", "none");*/
            $("#free_item_view").css("display", "none");
            $("#bulk_offer_view").css("display", "none");
            $("#buy_get_any_view").css("display", "none");
            $("#bogo_offer_view").css("display", "none");
            $("#chooseItem").html("Choose Qualifing Items");
            $("#free_Specified_Product_view").css("display", "none");
            $("#AddItem").html("Add Item");

        } else if (value == "2") {
            $("#flash_deal_view").css("display", "block");
            $(".flashdisc").css("display", "none");
            $(".bundledisc").css("display", "block");
            $(".bulkdisc").css("display", "none");
            /*$("#bundle_offer_view").css("display", "block");*/
            $("#free_item_view").css("display", "none");
            $("#bulk_offer_view").css("display", "none");
            $("#buy_get_any_view").css("display", "none");
            $("#bogo_offer_view").css("display", "none");
            $("#free_Specified_Product_view").css("display", "none");
            $("#chooseItem").html("Choose Qualifing Items");
            $("#AddItem").html("Add Item");

        } else if (value == "3") {
            /*$("#bundle_offer_view").css("display", "none");*/
            $("#flash_deal_view").css("display", "none");
            $("#free_item_view").css("display", "block");
            $("#bulk_offer_view").css("display", "none");
            $("#buy_get_any_view").css("display", "none");
            $("#bogo_offer_view").css("display", "none");
            $("#free_Specified_Product_view").css("display", "none");
            $("#chooseItem").html("Choose Free Items");
            $("#AddItem").html("Add Free Item");

        } else if (value == "5") {
            /*$("#bundle_offer_view").css("display", "none");*/
            $("#flash_deal_view").css("display", "block");
            $(".flashdisc").css("display", "none");
            $(".bundledisc").css("display", "none");
            $(".bulkdisc").css("display", "block");
            $("#free_item_view").css("display", "none");
            $("#bulk_offer_view").css("display", "block");
            $("#buy_get_any_view").css("display", "none");
            $("#bogo_offer_view").css("display", "none");
            $("#free_Specified_Product_view").css("display", "none");
            $("#chooseItem").html("Choose Qualifing Items");
            $("#AddItem").html("Add Item");

        } else if (value == "6") {
            /*$("#bundle_offer_view").css("display", "none");*/
            $("#flash_deal_view").css("display", "none");
            $("#free_item_view").css("display", "none");
            $("#bulk_offer_view").css("display", "none");
            $("#buy_get_any_view").css("display", "block");
            $("#bogo_offer_view").css("display", "none");
            $("#free_Specified_Product_view").css("display", "none");
            $("#chooseItem").html("Choose Qualifing Items");
            $("#AddItem").html("Add Item");

        } else if (value == "4") {
            /*$("#bundle_offer_view").css("display", "none");*/
            $("#flash_deal_view").css("display", "none");
            $("#free_item_view").css("display", "none");
            $("#bulk_offer_view").css("display", "none");
            $("#buy_get_any_view").css("display", "none");
            $("#bogo_offer_view").css("display", "none");
            $("#free_Specified_Product_view").css("display", "block");
            $("#chooseItem").html("Choose Qualifing Items");
            $("#AddItem").html("Add Item");
        } else {
            /*$("#bundle_offer_view").css("display", "none");*/
            $("#flash_deal_view").css("display", "none");
            $("#free_item_view").css("display", "none");
            $("#bulk_offer_view").css("display", "none");
            $("#buy_get_any_view").css("display", "none");
            $("#bogo_offer_view").css("display", "block");
            $("#free_Specified_Product_view").css("display", "none");
            $("#chooseItem").html("Choose Qualifing Items");
            $("#AddItem").html("Add Item");
        }


        if (value == "FlatDiscounts") {
            $(".flat-discount-view").css("display", "block");
            $(".free-offer-view").css("display", "none");
            $(".combo-offer-view").css("display", "none");

        } else if (value == "ComboOffer") {
            $(".combo-offer-view").css("display", "block");
            $(".flat-discount-view").css("display", "none");
            $(".free-offer-view").css("display", "none");

        } else {
            $(".free-offer-view").css("display", "block");
            $(".combo-offer-view").css("display", "none");
            $(".flat-discount-view").css("display", "none");
        }
    });

    /* SELECT FREE ITEM METHOD */
    $(".free-value-view").css("display", "none");
    $("#select_freeItem_method").on('change', function () {
        var value = $('#select_freeItem_method :selected').val();
        if (value == "productQty") {
            $(".free-quantity-view").css("display", "block");
            $(".free-value-view").css("display", "none");
        } else {
            $(".free-quantity-view").css("display", "none");
            $(".free-value-view").css("display", "block");
        }
    });

    /* SELECT COMBO METHOD */
    $(".discount_group_view").css("display", "none");
    $("#select_combo_method").on('change', function () {
        var value = $('#select_combo_method :selected').val();
        if (value == "ProductQty") {
            $(".quantity_discount_view").css("display", "block");
            $(".discount_group_view").css("display", "none");
        } else {
            $(".discount_group_view").css("display", "block");
            $(".quantity_discount_view").css("display", "none");
        }
    });

    $('.common-select-search').select2({
        containerCssClass: "select2-data-ajax",
        placeholder: '--Select--',
        ajax: {
            url: '/Sale/ItemSelectList',
            type: "get",
            dataType: 'json',
            delay: 250,
            data: function (e) {
                return {
                    q: e.term,
                    page: e.page
                };
            },
            processResults: function (data, t) {
                var results = [];
                for (i = 0; i < data.Data.length; i++) {
                    results.push({
                        id: data.Data[i].itemId,
                        text: data.Data[i].pluName,
                        measurement: data.Data[i].measurement,
                        weight: data.Data[i].weight,
                        sellingPrice: data.Data[i].sellingPrice,
                        category: data.Data[i].category,
                        sellingPrice: data.Data[i].sellingPrice,
                        itemType: data.Data[i].itemType,
                        CategoryName: data.Data[i].categoryName,
                        MarketPrice: data.Data[i].marketPrice,
                        itemstock: data.Data[i].totalStock,
                        stockId: data.Data[i].stockId,
                        Purchaseprice: data.Data[i].purchaseprice,
                        DiscountPerctg: data.Data[i].discountPerctg,
                        CoupenDisc: data.Data[i].coupenDisc
                    });
                }
                return {
                    results: results,
                    pagination: {
                        more: false
                    }
                };
            },
            cache: true
        }
    });


    $("#select_branch").select2({
        placeholder: "Select Branch",
        allowClear: true
    });

    $(".choose_qualified").select2({
        placeholder: "Select Qualified Items",
        allowClear: true
    });

    $("#choose_grouped_item").select2({
        placeholder: "Select Grouped of Items",
        allowClear: true
    });

    $("#choose_list_free_item").select2({
        placeholder: "Select List of Free Item",
        allowClear: true
    });

    $("#BuyItemId").select2({
        placeholder: "Select Specific Items",
        allowClear: true
    });
    $("#choose_qualified1").select2({
        placeholder: "Select Specific Items",
        allowClear: true
    });


    $("#GetAnyItemId").select2({
        placeholder: "Select Specific Items",
        allowClear: true
    });

    $("#offer_limit").click(function () {
        if ($(this).prop("checked") == true) {
            $("#OffStartdate, #OffEndtdate").prop('disabled', false);
        } else {

            $("#OffStartdate, #OffEndtdate").prop('disabled', true);
        }
    })


    $('#choose_item_tb').DataTable();


    $('#choose_item_tb').on('change', '#selectallid', function (e) {
        var table = $(e.target).closest('table');
        $('td input:checkbox', table).prop('checked', this.checked);
    });
    // Add offer item 
    $('#AddItem').click(function () {
        AddItemList(0);
        $('.modal-footer').css("display", "block");
    });
    $('#AllSelect').click(function () {
        var myTable = $('#item_add_table').DataTable();
        var isChecked = this.checked;
        if (isChecked) {
            $('td input:checkbox', myTable.rows().nodes()).prop('checked', true);
        } else {
            $('td input:checkbox', myTable.rows().nodes()).prop('checked', false);
        }

    });
    $('#AddItemtoOffer').on('click', '.messageCheckbox', function () {
        $('#AllSelect').prop('checked', false);
    });
    $('#addItemId').click(function () {
        var checkedValues = [];
        var myTable = $('#item_add_table').DataTable();
        $('td input.messageCheckbox:checked', myTable.rows().nodes()).each(function () {
            checkedValues.push($(this).val());
        });
        var length = checkedValues.length;
        if (length > 0) {
            $(".selectedItems").css("display", "block");
            $("#selectedItems").text(length + " items Selected");
        }
        else {
            $(".selectedItems").css("display", "none");
        }
        $('#items').val(checkedValues);
        $('#add_item_md').modal('hide');
    });
    $('#MainCategoryId').on('change', function () {
        var maincategoryId = this.value;
        if (maincategoryId != null) {
            $('#Filter-Id').removeAttr('disabled');
            $('#Clear-Id').removeAttr('disabled');
        }
        else {
            $('#Filter-Id').attr('disabled');
            $('#Clear-Id').attr('disabled');
        }
        $.ajax({
            url: "/ItemMaster/GetCategorylist?mainCatId=" + maincategoryId,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    $.each(categoryList, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#CategoryId').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    });
    $('#Filter-Id').on('click', function () {
        var MainCategoryId = $('#MainCategoryId').val();
        var CategoryId = $('#CategoryId').val();
        var item = {
            MainCatName: MainCategoryId,
            CatName: CategoryId,
            type: 1
        };
        AddItemList(item)
    });

    function AddItemList(item) {
        var VFS = $('#items').val();
        if (VFS == '') {
            VFS = 'ITM0101';
        }
        $.ajax({
            url: '/Offer/_AddItemOfferModel',
            type: 'Get',
            contentType: "application/json; charset=utf-8",
            traditional: true,
            data: {
                MainCatName: item.MainCatName,
                CatName: item.CatName,
                type: item.type,
                selectedItem: item.selectedItem,
                vfs: VFS
            },
            success: function (data) {
                $('#item_add_table').DataTable().destroy();
                $('#AddItemtoOffer').html(data);
                $('#add_item_md').modal('show');
                $('#item_add_table').DataTable();
            }
        });
    }
    $('.addclose').on('click', function () {
        $('#MainCategoryId').val('0');
        var html = '<option value="0">Sub Category</option>';
        $("#CategoryId").html(html);
        AddItemList(0);
    });
    $('#Clear-Id').on('click', function () {
        $('#MainCategoryId').val('0');
        var html = '<option value="0">Sub Category</option>';
        $("#CategoryId").html(html);
        $('#Filter-Id').prop('disabled', true);
        $('#Clear-Id').prop('disabled', true);
        AddItemList(0);
    });

    $('#offer_limit').on('click', function () {
        var currentValue = $(this).is(":checked") ? "YES" : "NO";
        $('#toggleValue').val(currentValue);
        if (currentValue == 'YES') {
            $("#yesLimitedOffer").css("display", "block");
        }
        else {
            $("#yesLimitedOffer").css("display", "none");
        }
    });
    $('#selectedItems').on('click', function () {
        var item = {
            selectedItem: 1
        };
        AddItemList(item);
        $("#exampleModalLongTitle").text("Selected Item");
        $('.modal-footer').css("display", "none");
    });

    // get free item
    $('#GetItem').click(function () {
        GetItemList(0);
        $('.modal-footer').css("display", "block");
    });    
    $('#getItems').click(function () {
        var checkedValues = [];
        var myTable = $('#item_get_table').DataTable();
        $('td input.messageCheckbox:checked', myTable.rows().nodes()).each(function () {
            checkedValues.push($(this).val());
        });
        $('#GetItemId').val(checkedValues);
        var length = checkedValues.length;
        if (length > 0) {
            $(".getselectedItems").css("display", "block");
            $("#getselectedItems").text(length + " items Selected");
        }
        else {
            $(".getselectedItems").css("display", "none");
        }
        
        $('#get_item_md').modal('hide');
    });
    $('#GetItemtoOffer').on('click', '.messageCheckbox', function () {
        $('#AllSelect').prop('checked', false);
    });
    $('#getselectedItems').on('click', function () {
        var item = {
            selectedItem: 1
        };
        GetItemList(item);
        $("#getexampleModalLongTitle").text("Selected Item");
        $('.modal-footer').css("display", "none");
    });
    
    $('.getclose').on('click', function () {
        $('#getMainCategoryId').val('0');
        var html = '<option value="0">Sub Category</option>';
        $("#getCategoryId").html(html);
        GetItemList(0);
    });
    $('#GetClear-Id').on('click', function () {
        $('#getMainCategoryId').val('0');
        var html = '<option value="0">Sub Category</option>';
        $("#getCategoryId").html(html);
        $('#GetFilter-Id').prop('disabled', true);
        $('#GetClear-Id').prop('disabled', true);
        GetItemList(0);
    });
    $('#GetFilter-Id').on('click', function () {
        var MainCategoryId = $('#getMainCategoryId').val();
        var CategoryId = $('#getCategoryId').val();
        var item = {
            MainCatName: MainCategoryId,
            CatName: CategoryId,
            type: 1
        };
        GetItemList(item)
    });
    function GetItemList(item) {
        var VFS = $('#GetItemId').val();
        if (VFS == '') {
            VFS = 'ITM0101';
        }
        $.ajax({
            url: '/Offer/_GetItemOfferModel',
            type: 'Get',
            contentType: "application/json; charset=utf-8",
            traditional: true,
            data: {
                MainCatName: item.MainCatName,
                CatName: item.CatName,
                type: item.type,
                selectedItem: item.selectedItem,
                vfs: VFS
            },
            success: function (data) {
                $('#item_get_table').DataTable().destroy();
                $('#GetItemtoOffer').html(data);
                $('#get_item_md').modal('show');
                $('#item_get_table').DataTable();
            }
        });
    }
    $('#GetAllSelect').click(function () {
        var myTable = $('#item_get_table').DataTable();
        var isChecked = this.checked;
        if (isChecked) {
            $('td input:checkbox', myTable.rows().nodes()).prop('checked', true);
        } else {
            $('td input:checkbox', myTable.rows().nodes()).prop('checked', false);
        }

    });
    $('#getMainCategoryId').on('change', function () {
        var maincategoryId = this.value;
        if (maincategoryId != null) {
            $('#GetFilter-Id').removeAttr('disabled');
            $('#GetClear-Id').removeAttr('disabled');
        }
        else {
            $('#GetFilter-Id').attr('disabled');
            $('#GetClear-Id').attr('disabled');
        }
        $.ajax({
            url: "/ItemMaster/GetCategorylist?mainCatId=" + maincategoryId,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    $.each(categoryList, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#getCategoryId').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    });
})(jQuery);

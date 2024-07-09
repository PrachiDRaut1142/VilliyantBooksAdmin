var SubmitError = 0;
!(function ($) {
    // Scroll Top Script
    $(document).ready(function () {
        $("#showdiv").css("display", "none");
        $('#m_portlet_loader').css('display', 'block');
        var topOfOthDiv = $("#toptouchdiv").offset().top;
        $(window).scroll(function () {
            if ($(window).scrollTop() > topOfOthDiv) {
                $("#showdiv").css('display', 'block');
            }
            else {
                $("#showdiv").css('display', 'none');
            }
        });
        $('#m_portlet_loader').css('display', 'none');
    });
    $("document").ready(function () {
        var target = $('a[data-toggle="tab"]').attr("href");// activated tab
        $('#SearchItem').prop('value', "");
        if (target == "#items") {
            var maincategory = "MNCTG01";
        }
        else {
            var maincategory = target.slice(6);
        }
        $.selector_cache('#activeTab').prop('value', target);
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        $.ajax({
            type: 'GET',
            url: '/Sale/_ItemList/',
            data: { 'ItemName': ItemName, 'maincategory': maincategory },
            success: function (data) {
                if (data !== null && data.IsSuccess) {
                    $('#ItemDetails_' + maincategory).html(data.Data);

                } else if (data !== null) {
                    swal('Error!', data.ReturnMessage, 'error');
                } else {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
    });
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href");// activated tab
        $('#SearchItem').prop('value', "");
        if (target == "#items") {
            var maincategory = "MNCTG01";
        }
        else {
            var maincategory = target.slice(6);
        }
        $.selector_cache('#activeTab').prop('value', target);
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        $.ajax({
            type: 'GET',
            url: '/Sale/_ItemList/',
            data: { 'ItemName': ItemName, 'maincategory': maincategory },
            success: function (data) {
                if (data !== null && data.IsSuccess) {
                    $('#ItemDetails_' + maincategory).html(data.Data);

                } else if (data !== null) {
                    swal('Error!', data.ReturnMessage, 'error');
                } else {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
        $.selector_cache('#SearchItem').on('keyup', $.delay(function () {
            var ItemName = $.selector_cache("#SearchItem").prop('value');
            $.ajax({
                type: 'GET',
                url: '/Sale/_ItemList/',
                data: { 'ItemName': ItemName, 'maincategory': maincategory },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $('#ItemDetails_' + maincategory).html(data.Data);
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                    } else {
                        swal('Error!', 'There was some error. Try again later.', 'error');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        }, 500));
    });
    $('#temp55').select2({
        containerCssClass: "select2-data-ajax",
        placeholder: 'Select Product',
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

    $('#temp55').on('change', function (e) {
        var test = $(this).select2('data');
        var parameter = test[0].id;
        var weight = test[0].weight;
        $('#SPurchaseprice').text(test[0].Purchaseprice);
        $('#SstockId').text(test[0].stockId);
        var weight = test[0].weight;
        var discountPrice = test[0].DiscountPerctg;
        $('#itemstock').text(test[0].itemstock);
        $('#SItemId').text(test[0].id);
        $('#SCategoryName').text(test[0].CategoryName);
        $('#SCategoryId').text(test[0].category);
        $('#SItemName').text(test[0].text);
        $('#Smeaure').text(" " + test[0].measurement);
        $('#ShiddenWeight').val(test[0].weight);
        $('#SCoupenDisc').text(test[0].CoupenDisc);
        if (discountPrice == null) {
            $('#SearchAmount').val(parseFloat(test[0].sellingPrice));
            $('#Sprice').text(test[0].sellingPrice);
        }
        else {
            $('#SearchAmount').val(parseFloat(test[0].DiscountPerctg));
            $('#Sprice').text(test[0].DiscountPerctg);
        }
        $('#ItemType').text(test[0].itemType);
        $('#MarketPrice').text(test[0].MarketPrice);
        $('#spnProductMsg').text('');
        $('#ShdMarket').text(test[0].MarketPrice);
        $('#SearchDiscount').val('');
        $('#NetWeightId').val('');
        $('#SearchQuantity').val('');
        $('#tempSearchAmount').text(parseFloat(test[0].sellingPrice));
        $('#SItemtype').text(test[0].itemType);
        $('#spnProductMsg').text(test[0].weight + ' ' + test[0].measurement + ' X Rs. ' + test[0].sellingPrice);
        if (test[0].itemType === "Packed") {
            $('#NetWeightId').prop('disabled', true);
            $('#SearchQuantity').prop('disabled', false);
            $('#NetWeightId').val(test[0].weight);
        }
        else {
            $('#NetWeightId').prop('disabled', false);
            $('#SearchQuantity').prop('disabled', false);
            $('#SearchQuantity').val(1);
            //var price = parseFloat((test[0].sellingPrice * weighing) / parseFloat(weight)).toFixed(2);
        }
        $('#item_image').prop('value', 'https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/icon/' + test[0].id + '.png');
        var test2 = '';
        $('.clearitem').val('');
        $.ajax({
            url: "/Sale/ItemvariantList?ItemId=" + parameter,
            type: "GET",
            success: function (itemvList) {
                if (itemvList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected disabled>Select Size</option>';
                    $.each(itemvList, function (index) {
                        html += '<option value="' + itemvList[index].priceId + '" data-sellingprice="' + itemvList[index].sellingPrice + '" data-purchasePrice="' + itemvList[index].purchasePrice + '"  data-MarketPrice="' + itemvList[index].marketPrice + '" data-ItemId="' + itemvList[index].itemId + '" data-id="' + itemvList[index].pId + '" >' + itemvList[index].size + '</option>';
                    });
                    $('#Varaint').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function () {
                alert('no');
            }
        });
    });

    $.selector_cache('#Varaint').on('change', function (e) {
        var PriceId = this.value;
        var sellingPrice = $('select.Varaint').find(':selected').data('sellingprice');
        var purchasePrice = $('select.Varaint').find(':selected').data('purchaseprice');
        var MarketPrice = $('select.Varaint').find(':selected').data('marketprice');
        var ItemId = $('select.Varaint').find(':selected').data('itemid');
        var pid = $('select.Varaint').find(':selected').data('id');
        var size = $('select.Varaint').find(':selected').text();
        $('#SPurchaseprice').text(purchasePrice);
        var discountPrice = null;
        if (discountPrice == null) {
            $('#SearchAmount').val(parseFloat(sellingPrice));
            $('#Sprice').text(sellingPrice);
        }
        else {
            $('#SearchAmount').val(sellingPrice);
            $('#Sprice').text(sellingPrice);
        }
        $('#MarketPrice').text(MarketPrice);
        $('#ShdMarket').text(MarketPrice);
        $('#tempSearchAmount').text(parseFloat(sellingPrice));
        $('#PriceId').text(PriceId);
        $('#SpnSize').text(size);

    });

    $.selector_cache('#VaraintId').on('change', function (e) {
        var PriceId = this.value;
        var sellingPrice = $('select.VaraintId').find(':selected').data('sellingprice');
        var purchasePrice = $('select.VaraintId').find(':selected').data('purchaseprice');
        var MarketPrice = $('select.VaraintId').find(':selected').data('marketprice');
        var ItemId = $('select.VaraintId').find(':selected').data('itemid');
        var pid = $('select.VaraintId').find(':selected').data('id');
        var size = $('select.VaraintId').find(':selected').text();
        if (size != "") {
            if (size == 'NOVAR') {
                $('#SpnItemSize').text('');
                $('#SpnItemPrice').text(sellingPrice);
                $('#actualPrice').val(sellingPrice);
                $('#MarketPrice').val(MarketPrice);
                $('#PurchasePrice').val(purchasePrice);
                $('#tempSearchAmount').val(sellingPrice);
                $('#SpnPriceId').text(PriceId);
                $('#SpnSize').text(size);
            }
            else {
                $('#SpnItemSize').text(size);
                $('#SpnItemPrice').text(sellingPrice);
                $('#actualPrice').val(sellingPrice);
                $('#MarketPrice').val(MarketPrice);
                $('#PurchasePrice').val(purchasePrice);
                $('#tempSearchAmount').val(sellingPrice);
                $('#SpnPriceId').text(PriceId);
                $('#SpnSize').text(size);
            }
        }
    });

    $.selector_cache('#SearchItem').on('keyup', $.delay(function () {
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        $.ajax({
            type: 'GET',
            url: '/Sale/_ItemList/',
            data: { 'ItemName': ItemName },
            success: function (data) {
                if (data !== null && data.IsSuccess) {
                    $.selector_cache('#ItemDetails').html(data.Data);
                } else if (data !== null) {
                    swal('Error!', data.ReturnMessage, 'error');
                } else {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
    }, 500));

    $(document).ready(function (e) {
        var test = $('#GetcustomerNo').select2('data');
        var CustomerId = test[0].id;
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
            success: function (result) {
                if (result.GetCustomerDetail != null) {
                    $CustomerId.val(result.GetCustomerDetail.CustomerId);
                    $Name.val(result.GetCustomerDetail.Name);
                    $EmailId.val(result.GetCustomerDetail.EmailId);
                    $ContactNo.val(result.GetCustomerDetail.ContactNo);
                    $BuildingName.val(result.GetCustomerDetail.buildingName);
                    $RoomNo.val(result.GetCustomerDetail.RoomNo);
                    $Sector.val(result.GetCustomerDetail.Sector);
                    $Landmark.val(result.GetCustomerDetail.Landmark);
                    $Locality.val(result.GetCustomerDetail.Locality);
                    $AddressType.val(result.GetCustomerDetail.AddressType);
                    $ZipCode.val(result.GetCustomerDetail.ZipCode);
                    $City.val(result.GetCustomerDetail.City);
                    $State.val(result.GetCustomerDetail.State);
                    $Country.val(result.GetCustomerDetail.Country);
                    $cusdetailId.text(result.GetCustomerDetail.CustomerId);
                    $custdetailorgid.text(result.GetCustomerDetail.Id);
                    $customerName.text(result.GetCustomerDetail.Name);
                    $AddId.val(result.GetCustomerDetail.AddId);
                    $custId.val(result.GetCustomerDetail.Id);
                    $Type.val(result.GetCustomerDetail.Type);
                    result.GetCustomerDetail.ZipCode != null ? $('#AddDiv').html(": " + result.GetCustomerDetail.Address1) : $('#AddDiv').html(":      -");
                    result.GetCustomerDetail.ContactNo === null ? "NA" : $customermobileno.text(result.GetCustomerDetail.ContactNo);
                    $('#customerappinstall').text("No");
                    $('#spnappinstall').removeClass("m-badge--success");
                    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
                    $('#AddDiv').html(": " + result.GetCustomerDetail.Address1);
                    $('#spnaddress').html(result.GetCustomerDetail.AddId);
                    if (result.GetCustomerDetail.BuildingName !== null) {
                        $.ajax({
                            url: '/Sale/CoupenList',
                            type: 'Get',
                            data: { CustomerId: result.GetCustomerDetail.CustomerId },
                            success: function (data) {
                                var select = $('#selectCoupen');
                                select.empty().append($("<option />").val("0").text("Select Coupen").attr("selected", "true"));
                                select.append($("<option />").val("1").text("Apply Cash Discount").attr("selected", "true"));
                                $.each(data, function () {
                                    select.append($("<option />").val(this.value).text(this.text));
                                });
                            }
                        });
                        $.ajax({
                            url: '/Sale/_CustMultiAdd',
                            type: 'Get',
                            data: { Id: CustomerId },
                            success: function (data) {
                                $('#tblcustaddressdetail').html(data);
                            }
                        });
                    }
                    else {
                        $('#tblcustaddressdetail').html(' ');
                    }
                }
                else {
                    swal("Customer Details Is Not Proper!", "", "warning");
                }
            }
        });
    });

    $.selector_cache('#GetcustomerNo').on('change', function (e) {
        var test = $('#GetcustomerNo').select2('data');
        var CustomerId = test[0].id;
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
            success: function (result) {
                if (result.GetCustomerDetail != null) {
                    $CustomerId.val(result.GetCustomerDetail.CustomerId);
                    $Name.val(result.GetCustomerDetail.Name);
                    $EmailId.val(result.GetCustomerDetail.EmailId);
                    $ContactNo.val(result.GetCustomerDetail.ContactNo);
                    $BuildingName.val(result.GetCustomerDetail.buildingName);
                    $RoomNo.val(result.GetCustomerDetail.RoomNo);
                    $Sector.val(result.GetCustomerDetail.Sector);
                    $Landmark.val(result.GetCustomerDetail.Landmark);
                    $Locality.val(result.GetCustomerDetail.Locality);
                    $AddressType.val(result.GetCustomerDetail.AddressType);
                    $ZipCode.val(result.GetCustomerDetail.ZipCode);
                    $City.val(result.GetCustomerDetail.City);
                    $State.val(result.GetCustomerDetail.State);
                    $Country.val(result.GetCustomerDetail.Country);
                    $cusdetailId.text(result.GetCustomerDetail.CustomerId);
                    $custdetailorgid.text(result.GetCustomerDetail.Id);
                    $customerName.text(result.GetCustomerDetail.Name);
                    $AddId.val(result.GetCustomerDetail.AddId);
                    $custId.val(result.GetCustomerDetail.Id);
                    $Type.val(result.GetCustomerDetail.Type);
                    result.GetCustomerDetail.ZipCode != null ? $('#AddDiv').html(": " + result.GetCustomerDetail.Address1) : $('#AddDiv').html(":      -");
                    result.GetCustomerDetail.ContactNo === null ? "NA" : $customermobileno.text(result.GetCustomerDetail.ContactNo);
                    $('#customerappinstall').text("No");
                    $('#spnappinstall').removeClass("m-badge--success");
                    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
                    $('#AddDiv').html(": " + result.GetCustomerDetail.Address1);
                    $('#spnaddress').html(result.GetCustomerDetail.AddId);
                    if (result.GetCustomerDetail.BuildingName !== null) {
                        $.ajax({
                            url: '/Sale/CoupenList',
                            type: 'Get',
                            data: { CustomerId: result.GetCustomerDetail.CustomerId },
                            success: function (data) {
                                var select = $('#selectCoupen');
                                select.empty().append($("<option />").val("0").text("Select Coupen").attr("selected", "true"));
                                select.append($("<option />").val("1").text("Apply Cash Discount").attr("selected", "true"));
                                $.each(data, function () {
                                    select.append($("<option />").val(this.value).text(this.text));
                                });
                            }
                        });
                        $.ajax({
                            url: '/Sale/_CustMultiAdd',
                            type: 'Get',
                            data: { Id: CustomerId },
                            success: function (data) {
                                $('#tblcustaddressdetail').html(data);
                            }
                        });
                    }
                    else {
                        $('#tblcustaddressdetail').html(' ');
                    }
                }
                else {
                    swal("Customer Details Is DSFASDFASD Proper!", "", "warning");
                }
            }
        });
    });

    $(document).ready(function (e) {
        var test = $('#GetcustomerNo1').select2('data');
        var CustomerId = test[0].id;
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
            success: function (result) {
                if (result.GetCustomerDetail != null) {
                    $CustomerId.val(result.GetCustomerDetail.CustomerId);
                    $Name.val(result.GetCustomerDetail.Name);
                    $EmailId.val(result.GetCustomerDetail.EmailId);
                    $ContactNo.val(result.GetCustomerDetail.ContactNo);
                    $BuildingName.val(result.GetCustomerDetail.buildingName);
                    $RoomNo.val(result.GetCustomerDetail.RoomNo);
                    $Sector.val(result.GetCustomerDetail.Sector);
                    $Landmark.val(result.GetCustomerDetail.Landmark);
                    $Locality.val(result.GetCustomerDetail.Locality);
                    $AddressType.val(result.GetCustomerDetail.AddressType);
                    $ZipCode.val(result.GetCustomerDetail.ZipCode);
                    $City.val(result.GetCustomerDetail.City);
                    $State.val(result.GetCustomerDetail.State);
                    $Country.val(result.GetCustomerDetail.Country);
                    $cusdetailId1.text(result.GetCustomerDetail.CustomerId);
                    $custdetailorgid1.text(result.GetCustomerDetail.Id);
                    $customerName1.text(result.GetCustomerDetail.Name);
                    $AddId.val(result.GetCustomerDetail.AddId);
                    $custId.val(result.GetCustomerDetail.Id);
                    $Type.val(result.GetCustomerDetail.Type);
                    result.GetCustomerDetail.ZipCode != null ? $('#AddDiv1').html(": " + result.GetCustomerDetail.Address1) : $('#AddDiv').html(":      -");
                    result.GetCustomerDetail.ContactNo === null ? "NA" : $customermobileno.text(result.GetCustomerDetail.ContactNo);
                    $('#customerappinstall').text("No");
                    $('#spnappinstall').removeClass("m-badge--success");
                    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
                    $('#AddDiv1').html(": " + result.GetCustomerDetail.Address1);
                    $('#spnaddress').html(result.GetCustomerDetail.AddId);
                    if (result.GetCustomerDetail.BuildingName !== null) {
                        $.ajax({
                            url: '/Sale/CoupenList',
                            type: 'Get',
                            data: { CustomerId: result.GetCustomerDetail.CustomerId },
                            success: function (data) {
                                var select = $('#selectCoupen');
                                select.empty().append($("<option />").val("0").text("Select Coupen").attr("selected", "true"));
                                select.append($("<option />").val("1").text("Apply Cash Discount").attr("selected", "true"));
                                $.each(data, function () {
                                    select.append($("<option />").val(this.value).text(this.text));
                                });
                            }
                        });
                        $.ajax({
                            url: '/Sale/_CustMultiAdd',
                            type: 'Get',
                            data: { Id: CustomerId },
                            success: function (data) {
                                $('#tblcustaddressdetail').html(data);
                            }
                        });
                    }
                    else {
                        $('#tblcustaddressdetail').html(' ');
                    }
                }
                else {
                    swal("Customer Details Is Not Proper!", "", "warning");
                }
            }
        });
    });

    $.selector_cache('#GetcustomerNo1').on('change', function (e) {
        var test = $('#GetcustomerNo1').select2('data');
        var CustomerId = test[0].id;
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
            success: function (result) {
                if (result.GetCustomerDetail != null) {
                    $CustomerId.val(result.GetCustomerDetail.CustomerId);
                    $Name.val(result.GetCustomerDetail.Name);
                    $EmailId.val(result.GetCustomerDetail.EmailId);
                    $ContactNo.val(result.GetCustomerDetail.ContactNo);
                    $BuildingName.val(result.GetCustomerDetail.buildingName);
                    $RoomNo.val(result.GetCustomerDetail.RoomNo);
                    $Sector.val(result.GetCustomerDetail.Sector);
                    $Landmark.val(result.GetCustomerDetail.Landmark);
                    $Locality.val(result.GetCustomerDetail.Locality);
                    $AddressType.val(result.GetCustomerDetail.AddressType);
                    $ZipCode.val(result.GetCustomerDetail.ZipCode);
                    $City.val(result.GetCustomerDetail.City);
                    $State.val(result.GetCustomerDetail.State);
                    $Country.val(result.GetCustomerDetail.Country);
                    $cusdetailId1.text(result.GetCustomerDetail.CustomerId);
                    $custdetailorgid1.text(result.GetCustomerDetail.Id);
                    $customerName1.text(result.GetCustomerDetail.Name);
                    $AddId.val(result.GetCustomerDetail.AddId);
                    $custId.val(result.GetCustomerDetail.Id);
                    $Type.val(result.GetCustomerDetail.Type);
                    result.GetCustomerDetail.ZipCode != null ? $('#AddDiv1').html(": " + result.GetCustomerDetail.Address1) : $('#AddDiv').html(":      -");
                    result.GetCustomerDetail.ContactNo === null ? "NA" : $customermobileno.text(result.GetCustomerDetail.ContactNo);
                    $('#customerappinstall').text("No");
                    $('#spnappinstall').removeClass("m-badge--success");
                    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
                    $('#AddDiv1').html(": " + result.GetCustomerDetail.Address1);
                    $('#spnaddress').html(result.GetCustomerDetail.AddId);
                    if (result.GetCustomerDetail.BuildingName !== null) {
                        $.ajax({
                            url: '/Sale/CoupenList',
                            type: 'Get',
                            data: { CustomerId: result.GetCustomerDetail.CustomerId },
                            success: function (data) {
                                var select = $('#selectCoupen');
                                select.empty().append($("<option />").val("0").text("Select Coupen").attr("selected", "true"));
                                select.append($("<option />").val("1").text("Apply Cash Discount").attr("selected", "true"));
                                $.each(data, function () {
                                    select.append($("<option />").val(this.value).text(this.text));
                                });
                            }
                        });
                        $.ajax({
                            url: '/Sale/_CustMultiAdd',
                            type: 'Get',
                            data: { Id: CustomerId },
                            success: function (data) {
                                $('#tblcustaddressdetail').html(data);
                            }
                        });
                    }
                    else {
                        $('#tblcustaddressdetail').html(' ');
                    }
                }
                else {
                    swal("Customer Details Is DSFASDFASD Proper!", "", "warning");
                }
            }
        });
    });
})(jQuery);

//checkbox barcode
$('#checkpluname').on('click', function () {
    if ($(this).prop('checked') == true) {
        $('#uniqueBarcodeScanner').focus();
    }
    else {
        $('#uniqueBarcodeScanner').val('');
    }
});

//onchange sale add cart 
$('#CartListbody').on('change', '.weight', function () {
    var weight = this.value;
    var trid = $(this).closest('tr').attr('id');
    $('#spnNetweight_' + trid).text(weight);
    var discount = $('#discount_' + trid).text();
    var SellingPrice = $('#unitprice_' + trid).text();
    var Coupendiscount = $('#Discountamount').val();
    var NetWeight = this.value;
    var ItemActualWeight = $('#weightId_' + trid).text();
    var price = parseFloat((SellingPrice * NetWeight) / ItemActualWeight).toFixed(2);
    $('#AddedDiscount_' + trid).text(price);
    //price = Math.round(price);
    if ($('#coupenDisc_' + trid).text() == 1) {
        var discountpercent = (parseFloat(Coupendiscount) * parseFloat(price) / 100).toFixed(2);
        var Calculatebydiscount = (parseFloat(price) - parseFloat(discountpercent)).toFixed(2);
        $('#actualPrice_' + trid).text(Calculatebydiscount);
        $('#discount_' + trid).text(discountpercent);
    }
    else {
        $('#actualPrice_' + trid).text(parseFloat(price) - parseFloat(discount));
    }
    AddTotalItem();
    //$('#TotalPriceList_' + trid).val(price);
});

$('#CartListbody').on('change', '.Quantity', function () {
    var quantity = this.value;
    var trid = $(this).closest('tr').attr('id');
    var tritemid = $(this).closest('tr').attr('data-itemid');
    $('#spanquantity_' + trid).text(quantity);
    var discount = $('#discount_' + trid).text();
    var Coupendiscount = $('#Discountamount').val();
    var quantity = this.value;
    var price = $('#unitprice_' + trid).text();
    var netprice = parseFloat(quantity * price).toFixed(2);
    $('#AddedDiscount_' + trid).text(netprice);
    //netprice = Math.round(netprice);
    if ($('#coupenDisc_' + tritemid).text() == 1) {
        var discountpercent = (parseFloat(Coupendiscount) * parseFloat(netprice) / 100).toFixed(2);
        var Calculatebydiscount = (parseFloat(netprice) - parseFloat(discountpercent)).toFixed(2);
        $('#actualPrice_' + trid).text(Calculatebydiscount);
        $('#discount_' + trid).text(discountpercent);
    }
    else {
        $('#actualPrice_' + trid).text(parseFloat(netprice) - parseFloat(discount));
    }
    AddTotalItem();
    // var totalsubprice = parseFloat(netprice) + parseFloat(totalSaleCOst);
});

function totalcostcalculation() {
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    $.makeArray($('#CartListbody tr[id]').map(function () {
        var totalCost = $('#actualPrice_' + this.id).text();
        var actualquantityid = $('#Actualquantity_' + this.id).text();
        var quantity = $('#quantity_' + this.id).val();
        totalSaleCOst += parseFloat(totalCost);
        totalQuantity += parseFloat(quantity);
    }));
    $('#totalCartQuantity').text(totalQuantity);
    var discountprice = 0;
    var discountpercent = "";
    var acualamtbydiscount = "";
    var discount = $('#DisAmountId').text();
    if (discount != 0) {
        var selectcoupen = $('#selectCoupen').val();
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var coupenId = coupen[1];
        var maxdiscount = coupen[2];
        discountpercent = parseFloat(coupendiscount) * parseFloat(totalSaleCOst) / 100;
        discountpercent = Math.round(discountpercent);
        discountpercent = discountpercent > maxdiscount ? maxdiscount : discountpercent;
        $('#DisAmountId').text(discountpercent);
        acualamtbydiscount = parseFloat(totalSaleCOst) - parseFloat(discountpercent);
        $('#DiscountTotalamt').val(acualamtbydiscount);
        acualamtbydiscount = Math.round(acualamtbydiscount);
        $('#ActualDiscountAmt').val(discountpercent);
        //var subamt = $('#AmountId').text();
        //var totaldiscount = parseFloat(totalSaleCOst) - parseFloat(acualamtbydiscount);
        //discountprice = Math.round(totaldiscount);
        $('#TDisAmountId').text(acualamtbydiscount);
        $('#amount1').val(acualamtbydiscount);
    }
    else {
        $('#TDisAmountId').text(totalSaleCOst);
        $('#amount1').val(totalSaleCOst);
    }
    $('#AmountId').text(totalSaleCOst);
}

$('#amount1, #amount2').on('input', function () {
    var qty = parseFloat($.selector_cache('#amount1').val());
    var price = parseFloat($.selector_cache('#amount2').val());
    var discountpercent = $('#DiscountTotalamt').val();
    var returnamt2 = $('#amount2').val();
    if ($.selector_cache('#amount2').val()) {
        $('#total').text((price - qty).toFixed(2));
    }
    else {
        $('#total').text(qty.toFixed(2));
    }
    //if (discountpercent != "") {
    //    var returnamt = parseFloat(returnamt2) - parseFloat(discountpercent);
    //    returnamt = Math.round(returnamt);
    //    if (returnamt) {
    //        $('#total').text(returnamt);
    //    }
    //    else {
    //        $('#total').text(qty.toFixed(2));
    //    }
    //}
    //else {
    //    if ($.selector_cache('#amount2').val()) {
    //        $('#total').text((price - qty).toFixed(2));
    //    }
    //    else {
    //        $('#total').text(qty.toFixed(2));
    //    }
    //}
    if ($('#total').text() > 0) {
        $('#total').removeClass("text-danger");
        $('#total').addClass("text-success");
    }
    else {
        $('#total').removeClass("text-success");
        $('#total').addClass("text-danger");
    }
});

// item search end 
$.selector_cache('#GetcustomerNo').select2({
    containerCssClass: "select2-data-ajax",
    placeholder: 'Select Customer Name/Number',
    ajax: {
        url: '/Sale/GetCustomerContactNo',
        dataType: "json",
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
                    id: data.Data[i].id1,
                    text:   data.Data[i].contactNo + '-' + data.Data[i].name
                });
            }
            if (results.length === 0) {
                swal({
                    title: "Add Custumer?",
                    type: "warning",
                    showCancelButton: !0,
                    confirmButtonText: "Yes,create!"
                }).then(function (e) {
                    if (e.value) {
                        var regex = /^[a-zA-Z]+$/;
                        if (!t.term.match(regex)) {
                            $ContactNo.val(t.term);
                        }
                        else {
                            $ContactNo.val('');
                        }
                        var regex = /^[0-9]+$/;
                        if (!t.term.match(regex)) {
                            $Name.val(t.term);
                        }
                        else {
                            $Name.val('');
                        }
                        $CustomerId.val('');
                        $EmailId.val('');
                        $Ext.val('');
                        $BuildingName.val('');
                        $RoomNo.val('');
                        $Sector.val('');
                        $Landmark.val('');
                        $Locality.val('');
                        $AddressType.val(0);
                        $ZipCode.val('');
                        $City.val('');
                        $State.val('');
                        $Country.val('');
                        $cusdetailId.text('');
                        $customerName.text('');
                        $AddId.val('');
                        $custId.val('');
                        $Type.val('');
                        $customermobileno.text('');
                        $('#CustomerId').val('');
                        $('#Id').val('');
                        $('#create_new_address').modal('show');
                    }
                });
            }
            return {
                results: results,
                pagination: {
                    more: false
                }
            };
        },
        cache: !0
    },
    escapeMarkup: function (e) {
        return e;
    },
    minimumInputLength: 1
});

$.selector_cache('#GetcustomerNo1').select2({
    containerCssClass: "select2-data-ajax",
    placeholder: 'Select Customer Name/Number',
    ajax: {
        url: '/Sale/GetCustomerContactNo',
        dataType: "json",
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
                    id: data.Data[i].id,
                    text: 'CI0' + data.Data[i].id + '-' + data.Data[i].contactNo + '-' + data.Data[i].name
                });
            }
            if (results.length === 0) {
                swal({
                    title: "Add Custumer?",
                    type: "warning",
                    showCancelButton: !0,
                    confirmButtonText: "Yes,create!"
                }).then(function (e) {
                    if (e.value) {
                        var regex = /^[a-zA-Z]+$/;
                        if (!t.term.match(regex)) {
                            $ContactNo.val(t.term);
                        }
                        else {
                            $ContactNo.val('');
                        }
                        var regex = /^[0-9]+$/;
                        if (!t.term.match(regex)) {
                            $Name.val(t.term);
                        }
                        else {
                            $Name.val('');
                        }
                        $CustomerId.val('');
                        $EmailId.val('');
                        $Ext.val('');
                        $BuildingName.val('');
                        $RoomNo.val('');
                        $Sector.val('');
                        $Landmark.val('');
                        $Locality.val('');
                        $AddressType.val(0);
                        $ZipCode.val('');
                        $City.val('');
                        $State.val('');
                        $Country.val('');
                        $cusdetailId1.text('');
                        $customerName1.text('');
                        $AddId.val('');
                        $custId.val('');
                        $Type.val('');
                        $customermobileno.text('');
                        $('#CustomerId').val('');
                        $('#Id').val('');
                        $('#create_new_address').modal('show');
                    }
                });
            }
            return {
                results: results,
                pagination: {
                    more: false
                }
            };
        },
        cache: !0
    },
    escapeMarkup: function (e) {
        return e;
    },
    minimumInputLength: 1
});

$.selector_cache('#Deliverycharges').on('change', function () {
    var deliverycharges = $('#Deliverycharges').val();
    var ProductAmount = $('#Amountspn').text();
    var totalamount = parseFloat(deliverycharges) + parseFloat(ProductAmount);
    $('#AmountId').text(totalamount);
    $('#amount1').val(totalamount);
});

//Barcode Scanner
$('#uniqueBarcodeScanner').on('change', function () {
    var dicData = {
        BarcodeData: $('#uniqueBarcodeScanner').val()
    };
    //$('#uniqueBarcodeScanner').val('');
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var itemid = "";
    var barcode = "";
    var notequalcount = 0;
    var cartlength = 0;
    $.ajax({
        url: '/Sale/_AddCart',
        type: 'POST',
        data: dicData,
        success: function (data) {
            $('#spn_html').html(data);
            if ($('#spn_html div').length == 0) {
                swal("This Plucode is Not Exits!", "", "warning");
            }
            var trid = $('#CartListbody').closest('tr').attr('id');
            var existing = $('#CartListbody').html();
            var array = [];
            var barcodeloop = [];
            var count = 0;
            $.makeArray($('#CartListbody tr[id]').map(function () {
                itemid = this.id;
                barcode = $('#Barcode_' + this.id).val();
                barcodeloop.push(barcode);
                if (barcode == dicData.BarcodeData) {
                    count++;
                    var itemtype = $('#ItemType_' + this.id).text();
                    if (itemtype == "Packed") {
                        var span = $('#Actualquantity_' + this.id).text();
                        var input = $('#quantity_' + this.id).val();
                        var newquantity = parseFloat(span) + parseFloat(input);
                        $('#quantity_' + this.id).val(newquantity);
                        $('#spanquantity_' + this.id).text(newquantity);
                        var price = $('#unitprice_' + this.id).text();
                        var netprice = parseFloat(newquantity * price).toFixed(2);
                        netprice = Math.round(netprice);
                        $('#actualPrice_' + this.id).text(parseFloat(netprice));
                        //  totalcostcalculation();
                    }
                    if (itemtype == "Loose") {
                        var span = $('#ActualNetweight_' + this.id).text();
                        var input = $('#Netweight_' + this.id).val();
                        var exitingweight = parseFloat(span) + parseFloat(input);
                        $('#Netweight_' + this.id).val(exitingweight);
                        $('#spnNetweight_' + this.id).text(exitingweight);
                        var SellingPrice = $('#unitprice_' + this.id).text();
                        var NetWeight = exitingweight;
                        var ItemActualWeight = $('#weightId_' + this.id).text();
                        var price = parseFloat((SellingPrice * NetWeight) / ItemActualWeight).toFixed(2);
                        price = Math.round(price);
                        $('#actualPrice_' + this.id).text(price);
                        // totalcostcalculation();
                    }
                    array.push(existing);
                    //$('#uniqueBarcodeScanner').val('');
                }
                else {
                    notequalcount++;
                }
            }));
            if (notequalcount > 0) {
                array.push(existing);
                //  totalcostcalculation();
            }
            if (count == 0) {
                array.push(data);
                $('#CartListbody').html(array);
                SaveHomeDelivery(dicData);
            }
            //var bhbn = $('.hiddenerror').val();
            var length = $("#CartListbody").find("tr").length;
            //var rows = $('#CartListbody tr.lengthcount').length;
            //$("#hiddenerror").addClass("length_" + length);
            //if ($('.length_' + length).val() == "This Plucode is Not Exits") {
            //    //alert("Already Exite");
            //    swal("This Plucode is Not Exits!", "", "warning");
            //}
            //$('#hiddenerrorcount').val(rows);
            //length = parseInt(length) - parseInt(rows);
            $('#uniqueBarcodeScanner').val('');
            $('#Additem').modal('hide');
            $.makeArray($('#CartListbody tr[id]').map(function () {
                var exitingquantity = $('#spanquantity_' + this.id).text();
                $('#quantity_' + this.id).val(exitingquantity);
                var exitingweight = $('#spnNetweight_' + this.id).text();
                $('#Netweight_' + this.id).val(exitingweight);
                var quantity = $('#quantity_' + this.id).val();
                var totalCost = $('#actualPrice_' + this.id).text();
                totalSaleCOst += parseFloat(totalCost);
                totalQuantity += parseFloat(quantity);
                $('#imgId_' + this.id).prop('src', "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/Product-image/" + this.id + ".png")
            }));
            $('#TotalCartItemId').text(length);
            $('#totalCartQuantity').text(totalQuantity);
            $('#AmountId').text(totalSaleCOst);
            $('#Amountspn').text(totalSaleCOst);
            $('#amount1').prop('value', totalSaleCOst);
            $('#TDisAmountId').text(totalSaleCOst);
            if ($('#selectCoupen').val() != 0) {
                var selectcoupen = $('#selectCoupen').val();
                var coupen = selectcoupen.split(',');
                var coupendiscount = coupen[0];
                var coupenId = coupen[1];
                var maxdiscount = coupen[2];
                var discountamt = $('#Discountamount').val();
                var returnamt2 = $('#amount2').val();
                var customeramt = $('#AmountId').text();
                var discountpercent = parseFloat(coupendiscount) * parseFloat(customeramt) / 100;
                discountpercent = Math.round(discountpercent);
                discountpercent = discountpercent > maxdiscount ? maxdiscount : discountpercent;
                var acualamtbydiscount = parseFloat(customeramt) - parseFloat(discountpercent);
                $('#DiscountTotalamt').val(acualamtbydiscount);
                acualamtbydiscount = Math.round(acualamtbydiscount);
                $('#ActualDiscountAmt').val(discountpercent);
                $('#DisAmountId').text(discountpercent);
                $('#TDisAmountId').text(acualamtbydiscount);
                if (returnamt2 != "") {
                    var returnamt = parseFloat(returnamt2) - parseFloat(acualamtbydiscount);
                    returnamt = Math.round(returnamt);
                    $('#total').text(returnamt);
                }
            }
            $('.hiddenerror').val('');
        }
    });
});

$(".transaction_value").click(function () {
    if ($(this).is(":checked")) {
        $(".transaction").css("display", "block");
        $(".transaction_Show").css("display", "none");
        $('#total').css("display", "none");
    } else {
        $(".transaction").css("display", "none");
        $(".transaction_Show").css("display", "block");
        $('#total').css("display", "block");
    }
});

var $ContactNo = $('#ContactNo'),
    $btnCustomerAdd = $('#btnCustomerAdd'),
    $CustomerDetailId = $('#CustomerDetailId'),
    $btnSearchCustomer = $('#btnSearchCustomer'),
    $GetcustomerNo = $('#GetcustomerNo'),
    $CustomerId = $('#CustomerId'),
    $Name = $('#Name'),
    $EmailId = $('#EmailId'),
    $Ext = $('#Ext'),
    $BuildingName = $('#BuildingName'),
    $RoomNo = $('#RoomNo'),
    $Sector = $('#Sector'),
    $Landmark = $('#Landmark'),
    $Locality = $('#Locality'),
    $AddressType = $('#AddressType'),
    $ZipCode = $('#ZipCode'),
    $City = $('#City'),
    $State = $('#State'),
    $Country = $('#Country'),
    $AddId = $('#AddId'),
    $custId = $('#Id'),
    $Type = $('#Type'),
    //$Address1 = $('#Address1'),
    //$Address2 = $('#Address2'),
    //Customerdetail Id
    $cusdetailId = $('#cusdetailId'),
    $cusdetailId1 = $('#cusdetailId1'),
    $custdetailorgid = $('#custdetailorgid'),
    $custdetailorgid1 = $('#custdetailorgid1'),
    $customerName = $('#customerName'),
    $customerName1 = $('#customerName1'),
    $customermobileno = $('#customermobileno'),
    $customermobileno1 = $('#customermobileno1'),
    $customerappinstall = $('#customerappinstall'),
    numberOfErrors = 0;
$ContactNo_err = $('#ContactNo_err');

$ContactNo.on('keyup', function () {
    $ContactNo.css('border-color', '');
    $ContactNo_err.html('');
});

function validateMobileNumber(inputVal) {
    return !(/^[0-9]{10}$/.test(inputVal));
}
//$('#btnCustomerAdd').on('click', function () {
//    alert('check');
//    $('#CustomerDetailId').submit();
//    var Id = $('#custdetailorgid').text();
//    GetAddress(Id);
//});
$("#btnCustomerAdd").on('click', function () {
    //var customerId = $('#CustomerId').val();
    Swal.fire({
        title: 'Are you sure?',
        text: 'you want to update Address',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, change it!'
    }).then((result) => {
        var customerId = $('#CustomerId').val();
        var AddId = $('#addid').prop('value');
        var AddressType = $("#AddressType").prop('value');
        var Address2 = $('#Address2').prop('value');
        var Locality = $("#Locality").prop('value');
        var Sector = $("#Sector").prop('value');
        var Landmark = $("#Landmark").prop('value');
        var City = $("#City").prop('value');
        var State = $("#State").prop('value');
        var Country = $("#Country").prop('value');
        if (result.value) {
            var info = {
                customerId: customerId,
                AddId: AddId,
                AddressType: AddressType,
                Address2: Address2,
                Locality: Locality,
                Sector: Sector,
                Landmark: Landmark,
                City: City,
                State: State,
                Country: Country
            }
            var custDetail = {
                Id: $custId.val(),
                CustomerId: $CustomerId.val(),
                Name: $Name.val(),
                Ext: $Ext.val(),
                ContactNo: $ContactNo.val(),
                EmailId: $EmailId.val(),
                BuildingName: $BuildingName.val(),
                RoomNo: $RoomNo.val(),
                Sector: $Sector.val(),
                Landmark: $Landmark.val(),
                Locality: $Locality.val(),
                AddressType: $AddressType.val(),
                ZipCode: $ZipCode.val(),
                City: $City.val(),
                State: $State.val(),
                Country: $Country.val(),
                Addids: $('#addid').val(),
                Addid: $('#compAddId').val()
            };
            if (customerId != "") {
                $.ajax({
                    url: '/Customer/updateaddress/',
                    type: 'GET',
                    data: info,
                    success: function (result) {
                        if (result.result == 0) {
                            $('#create_new_address').modal('hide');
                            $.ajax({
                                url: '/Sale/_CustMultiAdd',
                                type: 'Get',
                                data: { Id: $('#custdetailorgid').text() },
                                success: function (data) {
                                    $('#tblcustaddressdetail').html(data);
                                }
                            });
                        }
                        else {
                            $('#create_new_address').modal('hide');
                            $.ajax({
                                url: '/Sale/_CustMultiAdd',
                                type: 'Get',
                                data: { Id: $('#custdetailorgid').text() },
                                success: function (data) {
                                    $('#tblcustaddressdetail').html(data);
                                }
                            });
                        }
                    }
                });
            }
            else {
                $.ajax({
                    url: '/Sale/CreateCustomers',
                    type: 'Post',
                    dataType: 'json',
                    data: custDetail,
                    success: function (data) {
                        if (data.customerId !== null && data.addId !== null) {
                            var custdata = data.customerId.split('_');
                            var custId = custdata[0];
                            $('#create_new_address').modal('hide');
                            $.ajax({
                                url: '/Sale/_CustMultiAdd',
                                type: 'Get',
                                data: { Id: custId },
                                success: function (data) {
                                    $('#tblcustaddressdetail').html(data);
                                    $GetcustomerNo.val('');
                                    $.ajax({
                                        type: 'GET',
                                        url: '/Sale/GetCustomerData?Type=Default&Id=' + custId,
                                        success: function (result) {
                                            $CustomerId.val(result.GetCustomerDetail.CustomerId);
                                            $Name.val(result.GetCustomerDetail.Name);
                                            $EmailId.val(result.GetCustomerDetail.EmailId);
                                            $Ext.val(result.GetCustomerDetail.Ext);
                                            $ContactNo.val(result.GetCustomerDetail.ContactNo);
                                            //$BuildingName.val(result.getCustomerDetail.buildingName);
                                            //$RoomNo.val(result.getCustomerDetail.roomNo);  
                                            //$Sector.val(result.getCustomerDetail.sector); 
                                            //$Landmark.val(result.getCustomerDetail.landmark);  
                                            //$Locality.val(result.getCustomerDetail.locality);  
                                            $AddressType.val(result.GetCustomerDetail.AddressType);
                                            $ZipCode.val(result.GetCustomerDetail.ZipCode);
                                            $City.val(result.GetCustomerDetail.City);
                                            $State.val(result.GetCustomerDetail.State);
                                            $Country.val(result.GetCustomerDetail.Country);
                                            $cusdetailId.text(result.GetCustomerDetail.CustomerId);
                                            $customerName.text(result.GetCustomerDetail.Name);
                                            $AddId.val(result.GetCustomerDetail.AddId);
                                            $custId.val(result.GetCustomerDetail.Id);
                                            $custdetailorgid.text(result.GetCustomerDetail.Id);
                                            $Type.val(result.GetCustomerDetail.Type);
                                            result.GetCustomerDetail.ContactNo === null ? "NA" : $customermobileno.text(result.GetCustomerDetail.ContactNo);
                                        }
                                    });
                                }
                            });
                        }
                        else {
                            $('#create_new_address').modal('show');
                        }
                    }
                });
            }
        }
    });
});
//$btnCustomerAdd.on('click', function () {
//    var numberOfErrors = 0;
//    if ($ContactNo.val().trim().length > 1) {
//        if (validateMobileNumber($ContactNo.val())) {
//            numberOfErrors++;
//            $ContactNo_err.html("Please Enter valid Phone Number").css('display', 'block');
//        }
//    }
//    var custDetail = {
//        Id: $custId.val(),
//        CustomerId: $CustomerId.val(),
//        Name: $Name.val(),
//        Ext: $Ext.val(),
//        ContactNo: $ContactNo.val(),
//        EmailId: $EmailId.val(),
//        BuildingName: $BuildingName.val(),
//        RoomNo: $RoomNo.val(),
//        Sector: $Sector.val(),
//        Landmark: $Landmark.val(),
//        Locality: $Locality.val(),
//        AddressType: $AddressType.val(),
//        ZipCode: $ZipCode.val(),
//        City: $City.val(),
//        State: $State.val(),
//        Country: $Country.val(),
//        Addids: $('#addid').val(),
//        Addid: $('#compAddId').val()
//    };
//    var data = $('#GetcustomerNo').val();
//    //if ($ContactNo.prop('value').trim().length < 1) {
//    //    numberOfErrors++;
//    //    $ContactNo_err.html('Cannot be empty').css('display', 'block');
//    //}
//    if (numberOfErrors === 0) {
//        swal({
//            title: "Are you sure?",
//            text: "You want to Create the Customer",
//            type: "warning",
//            showCancelButton: !0,
//            confirmButtonText: "Yes, send it!"
//        }).then(function (e) {
//            if (e.value) {
//                $.ajax({
//                    url: '/Sale/CreateCustomers',
//                    type: 'Post',
//                    dataType: 'json',
//                    data: custDetail,
//                    success: function (data) {
//                        if (data.customerId !== null && data.addId !== null) {
//                            var custdata = data.customerId.split('_');
//                            var custId = custdata[0];
//                            $('#create_new_address').modal('hide');
//                            $.ajax({
//                                url: '/Sale/_CustMultiAdd',
//                                type: 'Get',
//                                data: { Id: custId },
//                                success: function (data) {
//                                    $('#tblcustaddressdetail').html(data);
//                                    $GetcustomerNo.val('');
//                                    $.ajax({
//                                        type: 'GET',
//                                        url: '/Sale/GetCustomerData?Type=Default&Id=' + custId,
//                                        success: function (result) {
//                                            $CustomerId.val(result.getCustomerDetail.customerId);
//                                            $Name.val(result.getCustomerDetail.name);
//                                            $EmailId.val(result.getCustomerDetail.emailId);
//                                            $Ext.val(result.getCustomerDetail.ext);
//                                            $ContactNo.val(result.getCustomerDetail.contactNo);
//                                            //$BuildingName.val(result.getCustomerDetail.buildingName);
//                                            //$RoomNo.val(result.getCustomerDetail.roomNo);  
//                                            //$Sector.val(result.getCustomerDetail.sector); 
//                                            //$Landmark.val(result.getCustomerDetail.landmark);  
//                                            //$Locality.val(result.getCustomerDetail.locality);  
//                                            $AddressType.val(result.getCustomerDetail.addressType);
//                                            $ZipCode.val(result.getCustomerDetail.zipCode);
//                                            $City.val(result.getCustomerDetail.city);
//                                            $State.val(result.getCustomerDetail.state);
//                                            $Country.val(result.getCustomerDetail.country);
//                                            $cusdetailId.text(result.getCustomerDetail.customerId);
//                                            $customerName.text(result.getCustomerDetail.name);
//                                            $AddId.val(result.getCustomerDetail.addId);
//                                            $custId.val(result.getCustomerDetail.id);
//                                            $custdetailorgid.text(result.getCustomerDetail.id);
//                                            $Type.val(result.getCustomerDetail.type);
//                                            result.getCustomerDetail.contactNo === null ? "NA" : $customermobileno.text(result.getCustomerDetail.contactNo);
//                                        }
//                                    });
//                                }
//                            });
//                        }
//                        else {
//                            $('#create_new_address').modal('show');
//                        }
//                    }
//                });
//            }
//        });
//    }
//});
$ContactNo.on('change', function () {
    $.ajax({
        type: 'GET',
        url: '/Sale/ValidateContactNo?Ext=' + $Ext.val() + '&ContactNo=' + $ContactNo.val(),
        success: function (result) {
            if (result !== null) {
                if (result.contactNo === $ContactNo.val()) {
                    $ContactNo.css('border-color', 'red');
                    $ContactNo_err.html("Contact Number Already Exit").css('display', 'block');
                    numberOfErrors++;
                }
            }
        },
        error: function (jqXHR, exception) {
        }
    });
});

$.selector_cache('#btnaddAddress').on('click', function () {
    $('#CustomerId').val($('#cusdetailId').text());
    $('#Id').val($('#custdetailorgid').text());
    $BuildingName.val('');
    $RoomNo.val('');
    $Sector.val('');
    $Landmark.val('');
    $Locality.val('');
    $ZipCode.val('');
    $City.val('');
    $State.val('');
    $Country.val('');
    $AddressType.val(0);
});

//$btnSearchCustomer.on('click', function () {
//    $.ajax({
//        type: 'GET',
//        url: '/Sale/GetCustomerData?Type=Default&Id=' + $GetcustomerNo.val(),
//        success: function (result) {
//            $CustomerId.val(result.getCustomerDetail.customerId);
//            $Name.val(result.getCustomerDetail.name);
//            $EmailId.val(result.getCustomerDetail.emailId);
//            $Ext.val(result.getCustomerDetail.ext);
//            $ContactNo.val(result.getCustomerDetail.contactNo);
//            //$BuildingName.val(result.getCustomerDetail.buildingName);
//            //$RoomNo.val(result.getCustomerDetail.roomNo);  
//            //$Sector.val(result.getCustomerDetail.sector); 
//            //$Landmark.val(result.getCustomerDetail.landmark);  
//            //$Locality.val(result.getCustomerDetail.locality);  
//            //$AddressType.val(result.getCustomerDetail.addressType);  
//            $ZipCode.val(result.getCustomerDetail.zipCode);
//            $City.val(result.getCustomerDetail.city);
//            $State.val(result.getCustomerDetail.state);
//            $Country.val(result.getCustomerDetail.country);
//            $cusdetailId.text(result.getCustomerDetail.customerId);
//            $customerName.text(result.getCustomerDetail.name);
//            $AddId.val(result.getCustomerDetail.addId);
//            $custId.val(result.getCustomerDetail.id);
//            $Type.val(result.getCustomerDetail.type);
//            $customermobileno.text(result.getCustomerDetail.ext + " " + result.getCustomerDetail.contactNo);
//            //$('#customerappinstall').text(result.getCustomerDetail.address1 && result.getCustomerDetail.address2 != null ? "Yes" : "No");
//            //if (result.getCustomerDetail.address1 && result.getCustomerDetail.address2 != null) {
//            //    $('#spnappinstall').removeClass(" m-badge--danger");
//            //    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--success");
//            //}
//            //else {
//            //    $('#spnappinstall').removeClass("m-badge--success");
//            //    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
//            //}
//            if (result.getCustomerDetail.buildingName != null) {
//                $.ajax({
//                    url: '/Sale/_CustMultiAdd',
//                    type: 'Get',
//                    data: { Id: $GetcustomerNo.val() },
//                    success: function (data) {
//                        $('#tblcustaddressdetail').html(data);
//                    }
//                });
//            }
//            else {
//                $('#tblcustaddressdetail').html(' ');
//            }
//        }
//    });
//});

function CustChekbox(param) {
    var paraAry = param.split(',');
    var addrsId = paraAry[0];
    var custId = paraAry[1];
    var type = paraAry[2];
    var Ids = paraAry[3];
    $.ajax({
        type: 'GET',
        url: '/Sale/SetDefault?custermId=' + custId + '&addressId=' + addrsId + '&Type=' + type,
        success: function (result) {
            $.ajax({
                url: '/Sale/_CustMultiAdd',
                type: 'Get',
                data: { Id: Ids },
                success: function (data) {
                    $('#tblcustaddressdetail').html(data);
                }
            });
        }
    });
}

function GetAddress(customerId) {
    $.ajax({
        url: '/Sale/_CustMultiAdd',
        type: 'Get',
        data: { Id: customerId },
        success: function (data) {
            $('#tblcustaddressdetail').html(data);
        }
    });
}

function CustDelete(param) {
    swal({
        title: "Are you sure?",
        text: "You want to Delete the Record",
        type: "warning",
        showCancelButton: !0,
        confirmButtonText: "Yes, send it!"
    }).then(function (e) {
        if (e.value) {
            $.ajax({
                type: 'POST',
                url: '/Sale/DeleteCustAddress/',
                data: { custAdressId: param },
                success: function (result) {
                    $.ajax({
                        url: '/Sale/_CustMultiAdd',
                        type: 'Get',
                        data: { Id: $custdetailorgid.text() },
                        success: function (data) {
                            $('#tblcustaddressdetail').html(data);
                        }
                    });
                }
            });
        }
    });
}

function GetItemDetails(parameter) {
    $('#SpnItemId').text('');
    $('#SpnItemName').text('');
    $('#SpnItemWeight').text('');
    $('#SpnItemPrice').text('');
    $('#weightId').val('');
    $('#discountId').val('');
    $('#quantityId').val('');
    $('#tempSearchAmount').text('');
    var price = 0.0;
    var sellingprice = 0.0;
    var weighing = 0.0;
    var marketprice = 0;
    if ($('#' + parameter).length > 0) {
        var itemtype = $('#itemTp_' + parameter).text();
        sellingprice = $('#unitprice_' + parameter).text();
        var quantity = $('#quantity_' + parameter).val();
        var discount = $('#discountId_' + parameter).text();
        var actualPrice = $('#actualPrice_' + parameter).text();
        var itemStock = $('#Totalitemstock_' + parameter).text();
        var stockId = $('#stockId_' + parameter).text();
        $('#SpnItemId').text(parameter);
        $('#SpnCategory').text($('#catName_' + parameter).text());
        $('#SpnCategoryId').text($('#cateId_' + parameter).text());
        $('#SpnItemName').text($('#SpnPluname_' + parameter).text());
        $('#SpnItemWeight').text($('#weightId_' + parameter).text());
        $('#meaure').text($('#measure_' + parameter).text());
        $('#hiddenWeight').text($('#weightId_' + parameter).text());
        $('#SpnItemPrice').text(sellingprice);
        $('#ItemType').text($('#itemTp_' + parameter).text());
        $('#hdMarket').text($('#Market_' + parameter).text());
        $('#tempSearchAmount').text(sellingprice);
        $('#actualPrice').val(parseFloat(sellingprice));
        $('#weightId').val($('#Netweight_' + parameter).val());
        $('#quantityId').val(quantity).prop('disabled', true);
        $('#actualPrice').val(parseFloat(actualPrice));
        $('#TotalItemstock').text(itemStock);
        $('#discountId').val(discount);
        $('#PurchasePrice').text($('#purchaseprice_' + parameter).text());
        $('#stockId').text(stockId);
        if (itemtype === "Packed") {
            $('#weightId').prop('disabled', true);
            $('#quantityId').prop('disabled', false);
        }
        else {
            $('#weightId').prop('disabled', false);
            $('#quantityId').prop('disabled', true);
        }
        $.ajax({
            url: "/Sale/ItemvariantList?ItemId=" + parameter,
            type: "GET",
            success: function (itemvList) {
                if (itemvList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected disabled>Select Size</option>';
                    $.each(itemvList, function (index) {
                        html += '<option value="' + itemvList[index].priceId + '" data-sellingprice="' + itemvList[index].sellingPrice + '" data-purchasePrice="' + itemvList[index].purchasePrice + '"  data-MarketPrice="' + itemvList[index].marketPrice + '" data-ItemId="' + itemvList[index].itemId + '" data-id="' + itemvList[index].pId + '" >' + itemvList[index].size + '</option>';
                    });
                    $('#VaraintId').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function () {
                alert('no');
            }
        });
    }
    else {
        $.ajax({
            url: "/Sale/ItemDetails?itemId=" + parameter,
            type: "GET",
            success: function (data) {
                $('#SpnItemId').text(data.itemDetails.itemId);
                $('#SpnCategory').text(data.itemDetails.categoryName);
                $('#SpnCategoryId').text(data.itemDetails.category);
                $('#SpnItemName').text(data.itemDetails.pluName);
                $('#SpnItemWeight').text(data.itemDetails.weight);
                $('#meaure').text(" " + data.itemDetails.measurement);
                $('#hiddenWeight').text(data.itemDetails.weight);
                $('#ItemType').text(data.itemDetails.itemType);
                $('#TotalItemstock').text(data.itemDetails.totalStock);
                $('#stockId').text(data.itemDetails.id);
                $('#spnPlucode').text(data.itemDetails.pluCode);
                sellingprice = data.itemDetails.sellingPrice;
                marketprice = data.itemDetails.marketPrice;
                $('#MarketPrice').text(marketprice);
                $('#hdMarket').text(marketprice);
                $('#PurchasePrice').text(data.itemDetails.purchaseprice);
                if (data.itemDetails.discountPerctg != null) {
                    $('#SpnItemPrice').text(parseFloat(data.itemDetails.discountPerctg));
                    $('#actualPrice').val(parseFloat(data.itemDetails.discountPerctg));
                    $('#tempSearchAmount').text(parseFloat(data.itemDetails.discountPerctg));
                }
                else {

                    $('#SpnItemPrice').text(parseFloat(data.itemDetails.sellingPrice));
                    $('#actualPrice').val(parseFloat(data.itemDetails.sellingPrice));
                    $('#tempSearchAmount').text(parseFloat(data.itemDetails.sellingPrice));
                }
                //$('#discountId').val(0);
                if (data.itemDetails.itemType === "Packed") {
                    $('#weightId').val(data.itemDetails.weight);
                    $('#weightId').prop('disabled', true);
                    $('#quantityId').prop('disabled', false);
                }
                else {
                    $('#weightId').prop('disabled', false);
                    $('#quantityId').val(1).prop('disabled', true);
                    //price = parseFloat((data.itemDetails.sellingPrice * weighing) / data.itemDetails.weight).toFixed(2);
                }
                $('#MItemType').text(data.itemDetails.itemType);
                $('#item_image').prop('value', 'https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/icon/' + parameter + '.png');
            }
        });
        $.ajax({
            url: "/Sale/ItemvariantList?ItemId=" + parameter,
            type: "GET",
            success: function (itemvList) {
                if (itemvList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected disabled>Select Size</option>';
                    $.each(itemvList, function (index) {
                        html += '<option value="' + itemvList[index].priceId + '" data-sellingprice="' + itemvList[index].sellingPrice + '" data-purchasePrice="' + itemvList[index].purchasePrice + '"  data-MarketPrice="' + itemvList[index].marketPrice + '" data-ItemId="' + itemvList[index].itemId + '" data-id="' + itemvList[index].pId + '" >' + itemvList[index].size + '</option>';
                    });
                    $('#VaraintId').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function () {
                alert('no');
            }
        });
    }
    $('#Additem').modal('show');
}

$("#discountId").on('change', function () {
    var discount = $('#discountId').val();
    var price = $('#tempSearchAmount').text();
    var price1 = $('#SpnItemPrice').text();
    var tempPrice = 0.0;
    var discountedPrice = 0.0;
    if ($('#discountId').val() != '') {
        discountedPrice = parseFloat(price - discount).toFixed(2);
        discountedPrice = Math.round(discountedPrice);
        $('#actualPrice').val(discountedPrice);
    }
    else {
        if ($('#ItemType').text() == 'Packed') {
            var quantity = $('#quantityId').val();
            var netprice = parseFloat(quantity * price1).toFixed(2);
            $('#actualPrice').val(netprice);
        }
        else {
            var weighing = $('#weightId').val();
            var weight = $('#SpnItemWeight').text();
            var netprice = parseFloat((price1 * weighing) / weight).toFixed(2);
            $('#actualPrice').val(netprice);
        }
    }
});

$("#quantityId").on('change', function () {
    $('#discountId').prop('value', '');
    var quantity = $('#quantityId').val();
    var price = $('#SpnItemPrice').text();
    var market = $('#MarketPrice').text();
    var netprice = 0.0;
    if ($('#quantityId').val() == '') {
        $('#actualPrice').val(parseFloat(price));
        $('#hdMarket').text(parseFloat(market));
    }
    else {
        netprice = parseFloat(quantity * price).toFixed(2);
        market = parseFloat(quantity * market).toFixed(2);
        market = Math.round(market);
        netprice = Math.round(netprice);
        $('#actualPrice').val(netprice);
        $('#hdMarket').text(market);
        $('#tempSearchAmount').text(parseFloat(netprice));
    }
});

$("#weightId").on('change', function () {
    $('#discountId').prop('value', '');
    var weight = $('#SpnItemWeight').text();
    var sellingPrice = $('#SpnItemPrice').text();
    var weighing = $('#weightId').val();
    var market = $('#MarketPrice').text();
    if ($('#weightId').val() == '') {
        $('#actualPrice').val(parseFloat(sellingPrice));
        $('#hdMarket').text(parseFloat(market));
    }
    else {
        var price = parseFloat((sellingPrice * weighing) / weight).toFixed(2);
        market = parseFloat((market * weighing) / weight).toFixed(2);
        market = Math.round(market);
        price = Math.round(price);
        $('#actualPrice').val(parseFloat(price));
        $('#hdMarket').text(market);
        $('#tempSearchAmount').text(parseFloat(price));
    }
});

$('#AddCartId').on('click', function () {
    var weightid = $('#weightId').val();
    var totakCartquant = $('#totalCartQuantity').text();
    var Amount = $('#AmountId').text();
    var amount1 = $('#amount1').val();
    var actualPrice = $('#actualPrice').val();
    var quantity = $('#quantityId').val();
    var remark = $('#remark').val();
    var discount = $('#discountId').val();
    var weight = $('#SpnItemWeight').text();
    var total = $('#TotalCartItemId').text();
    var pluName = $('#SpnItemName').text();
    var itemId = $('#SpnItemId').text();
    var priceId = $('#SpnPriceId').text();
    var plucode = $('#spnPlucode').text();
    var tableId = $('#selecttableId').val();
    var coupenDisc = 1;
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    if (!$('#discountId').prop('value').trim()) {
        discount = 0;
    }
    if (!$('#weightId').prop('value').trim()) {
        $('#weightId').prop('value', weight);
    }
    if (!$('#quantityId').prop('value').trim()) {
        quantity = 1;
    }
    if (!$('#remark').prop('value').trim()) {
        remark = "No Remark";
    }
    var AddedDiscount = parseFloat(actualPrice) + parseFloat(discount);
    var dicData = {
        ItemId: itemId,
        PluName: $('#SpnItemName').text(),
        Category: $('#SpnCategoryId').text(),
        Measurement: $('#meaure').text(),
        Weight: weight,
        SellingPrice: $('#SpnItemPrice').text(),
        MarketPrice: $('#hdMarket').text(),
        CategoryName: $('#SpnCategory').text(),
        Quantity: quantity,
        Remark: remark,
        NetWeight: $('#weightId').val(),
        Discount: discount,
        ActualCost: actualPrice,
        ItemType: $('#ItemType').text(),
        itemstock: $('#TotalItemstock').text(),
        ItemType: $('#MItemType').text(),
        stockId: $('#stockId').text(),
        CreatedBy: $('#CreatedBy').text(),
        Purchaseprice: $('#PurchasePrice').text(),
        PluCode: $('#spnPlucode').text(),
        CoupenDisc: coupenDisc,
        AddedDiscount: AddedDiscount,
        TableId: tableId,
        PriceId: priceId,
    };
    $('#' + dicData.ItemId).remove();
    $('#divId_' + dicData.ItemId).remove();
    $.ajax({
        url: '/Sale/_AddCart',
        type: 'POST',
        data: dicData,
        success: function (data) {
            $('#stockId').text($('#stockId_' + itemId).text());
            var existing = $('#CartListbody').html();
            var array = [];
            array.push(existing);
            array.push(data);
            $('#CartListbody').html(array);
            //$('#imgId_' + dicData.ItemId).prop('src', "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/Product-image/" + dicData.ItemId + ".png")
            $('#Additem').modal('hide');
            SaveHomeDelivery(dicData);
            $.makeArray($('#CartListbody tr[id]').map(function () {
                var exitingquantity = $('#spanquantity_' + this.id).text();
                $('#quantity_' + this.id).val(exitingquantity);
                var exitingremark = $('#spnRemark_' + this.id).text();
                $('#remark_' + this.id).val(exitingremark);
                var exitingweight = $('#spnNetweight_' + this.id).text();
                $('#Netweight_' + this.id).val(exitingweight);
                var quantity = $('#quantity_' + this.id).val();
                var remark = $('#remark_' + this.id).val();
                var totalCost = $('#actualPrice_' + this.id).text();
                totalSaleCOst += parseFloat(totalCost);
                totalQuantity += parseFloat(quantity);
            }));
            var length = $("#CartListbody").find("tr").length;
            $('#TotalCartItemId').text(length);
            $('#totalCartQuantity').text(totalQuantity);
            $('#AmountId').text(totalSaleCOst);
            $('#Amountspn').text(totalSaleCOst);
            $('#amount1').prop('value', totalSaleCOst);
            $('#TDisAmountId').text(totalSaleCOst);//ToDo
            AddTotalItem();
            CoupenCode();
            //if ($('#selectcoupen').val() != 0) {
            //    var selectcoupen = $('#selectcoupen').val();
            //    var coupen = selectcoupen.split(',');
            //    var coupendiscount = coupen[0];
            //    var coupenid = coupen[1];
            //    var maxdiscount = coupen[2];
            //    var discountamt = $('#discountamount').val();
            //    var returnamt2 = $('#amount2').val();
            //    var customeramt = $('#amountid').text();
            //    var discountpercent = parsefloat(coupendiscount) * parsefloat(customeramt) / 100;
            //    discountpercent = math.round(discountpercent);
            //    discountpercent = discountpercent > maxdiscount ? maxdiscount : discountpercent;
            //    var acualamtbydiscount = parsefloat(customeramt) - parsefloat(discountpercent);
            //    $('#discounttotalamt').val(acualamtbydiscount);
            //    acualamtbydiscount = math.round(acualamtbydiscount);
            //    $('#actualdiscountamt').val(discountpercent);
            //    $('#disamountid').text(discountpercent);
            //    $('#tdisamountid').text(acualamtbydiscount);
            //    if (returnamt2 != "") {
            //        var returnamt = parsefloat(returnamt2) - parsefloat(acualamtbydiscount);
            //        returnamt = math.round(returnamt);
            //        $('#total').text(returnamt);
            //    }
            //}
        }
    });
});

function SaveHomeDelivery(dicData) {
    $.ajax({
        url: '/Sale/_HomeDelivery',
        type: 'POST',
        data: dicData,
        success: function (data) {
            var array = []
            var existing = $('#HomeDeliveryId').html();
            array.push(existing);
            array.push(data);
            $('#HomeDeliveryId').html(array);
        }
    });
}
//function AddCartId(ItemId, weightId, quantityId, discountId, actualPrice, category, pluName, measure, unitprice, categoryId) {
//    if ($(discountId).val() == '') {
//        $(discountId).val(0);
//    }
//    if (!$(weightId).prop('value').trim()) {
//        $(weightId).prop('value',1);
//    }
//    if (!$(quantityId).prop('value').trim()) {
//        $(quantityId).prop('value', 1);
//    }
//    var itemId = $(ItemId).text();
//    var weighing = $(weightId).val();
//    var quantity = $(quantityId).val();
//    var discount = $(discountId).val();
//    var actualPrice = $(actualPrice).val();
//    var category = $(category).text();
//    var pluname = $(pluName).text();
//    var measure = $(measure).text();
//    var unitprice = $(unitprice).text();
//    var categoryId = $(categoryId).text();
//    var total = 0;
//    var totakCartquant = $('#totalCartQuantity').text();
//    var Amount = $('#AmountId').text();
//    var amount1 = $('#amount1').val();
//    var market = $('#hdMarket').text();
//    var item_image = $('#item_image').val();
//    var weight = $(SpnItemWeight).text();
//    $('#CartListbody').append(
//        '<tr id=' + itemId + '>' +
//        '<td>' +
//        '<div class="m-widget4__img m-widget4__img--logo">' +
//        '<img src="' + item_image + '" alt="' + pluname + '" style="width:60px;">' +
//        '</div>' +
//        '</td>' +
//        '<td>' +
//        '<h6 class="mb-1">' + pluname + '</h6>' +
//        '<span class="display-block font-small-3 text-bold-500 text-muted">' + category + '</span>' +
//        '<span hidden id="cateId_' + itemId + '">' + categoryId + '</span>' +
//        '</td>' +
//        '<td>' +
//        '<span>' + weight + measure + 'X Rs.' + unitprice + '</span>' +
//        '<span hidden id="measure_' + itemId + '">' + measure + '</span>' +
//        '<span hidden id="unitprice_' + itemId + '" >' + unitprice + '</span>' +
//        '<span hidden id="delivery_' + itemId + '" >no</span>' +
//        '<span  hidden id="Market_' + itemId + '" >' + market + '</span>' +
//        '</td>' +
//        '<td><span id="weight_' + itemId + '" class="text-bold-400"> ' + weighing + ' </span>' +
//        '<td><span id="quantity_' + itemId + '" class="text-bold-400"> ' + quantity + ' </span>' +
//        '</td > ' +
//        '<td>' +
//        '<span id="discount_' + itemId + '" >' + discount + '%</span>' +
//        '<span hidden id="discountId_' + itemId + '" >' + discount + '</span>' +
//        '</td>' +
//        '<td><span id="actualPrice_' + itemId + '" class="text-bold-500 text-info">' + actualPrice + '</span>' +
//        '</td > ' +
//        '<td>' +
//        '<a href="#" id="removeId" onclick=remove(\'' + itemId + '\') class="text-danger font-medium-1 text-bold-600">X</a>' +
//        '</td>' +
//        '</tr>'
//    );
//    $('#HomeDeliveryId').append(
//        '<div class="col-md-2" id="divId' + itemId + '" >' +
//        '<div class="text-center">' +
//        '<div class="mb-3 item_list">' +
//        '<label class="m-checkbox m-checkbox--solid m-checkbox--success display-block">' +
//        '<input type="checkbox" onclick="CheckboxId(\'' + itemId + '\')" id="' + itemId + '" value="' + itemId + '" name="Check">' +
//        '<img src="' + item_image + '" alt="' + pluname + '" style="width:60px;" class="img-fluid" />' +
//        '<h6 class="mb-0 font-medium-1">' + pluname + '</h6>' +
//        '<span></span>' +
//        '</label>' +
//        '</div>' +
//        '</div>' +
//        '</div>'
//    );
//    $('#Additem').modal('hide');
//    total = $('#TotalCartItemId').text();
//    total = parseInt(total) + parseInt(1);
//    $('#TotalCartItemId').text(total);
//    totakCartquant = parseFloat(totakCartquant) + parseFloat(quantity);
//    $('#totalCartQuantity').text(totakCartquant);
//    Amount = parseFloat(Amount) + parseFloat(actualPrice);
//    $('#AmountId').text(Amount);
//    $('#Amountspn').text(Amount);
//    amount1 = parseFloat(amount1) + parseFloat(actualPrice);
//    $('#amount1').val(amount1);
//}
function remove(param) {
    var totalItem = $('#TotalCartItemId').text();
    var totalquan = $('#totalCartQuantity').text();
    var cartQuan = $('#quantity_' + param).val();
    var actualprice = $('#actualPrice_' + param).text();
    var AmountId = $('#AmountId').text();
    var amount1 = $('#amount1').val();
    var amountspn = $('#TDisAmountId').text();
    var discountAmount = $('#DisAmountId').text();
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    totalItem = parseInt(totalItem) - parseInt(1);
    $('#TotalCartItemId').text(totalItem);
    var minusweigh = parseInt(totalquan) - parseInt(cartQuan);
    $('#totalCartQuantity').text(minusweigh);
    AmountId = parseFloat(AmountId) - parseFloat(actualprice);
    $('#AmountId').text(AmountId);
    $('#TDisAmountId').text(AmountId);
    $('#amount1').val(AmountId);
    $('#' + param).remove();
    $('#divId_' + param).remove();
    AddTotalItem();
    CoupenCode();
}

function CheckboxId(param) {
    var total = $('#TotalItemId').text();
    var checkedValues = $('input:checkbox[id="' + param + '"]:checked').length;
    if (checkedValues === 1) {
        $("input:checkbox[name='Check']").prop('checked', true);
        var checkboxdata = [];
        $('.deleveryall').text('yes');
        $('#delivery_' + param).text('yes');
        $('#divdelevery').css('display', 'block');
        $('#Deliverycharges').prop('value', 15);
    }
    else {
        $("input:checkbox[name='Check']").prop('checked', false);
        $('.deleveryall').text('no');
        $('#delivery_' + param).text('no');
        $('#divdelevery').css('display', 'none');
        $('#Deliverycharges').prop('value', 0);
    }
    var checkedValues = $('input:checkbox[name=Check]:checked').length;
    $('#TotalItemId').text(checkedValues);
    var deliverycharges = $('#Deliverycharges').val();
    var ProductAmount = $('#Amountspn').text();
    var disamount = $("#DisAmountId").text();
    var totalamount = parseFloat(deliverycharges) + parseFloat(ProductAmount) - parseFloat(disamount);
    totalamount = Math.round(totalamount);
    $('#TDisAmountId').text(totalamount);
    $('#amount1').val(totalamount);
}

$.selector_cache('#SearchItemName').on('keyup', $.delay(function () {
    var ItemName = $.selector_cache("#SearchItemName").prop('value');
    $.ajax({
        url: "/Sale/ItemDetails?itemId=" + ItemName,
        type: "GET",
        success: function (data) {
            var weighingValue = $('#WeighingNumber').html();
            var searchquantity = $('#SearchQuantity').val();
            var netWeight = $('#NetWeightId').val();
            var searchDiscount = $('#SearchDiscount').val();
            var amount = $('#SearchAmount').val();
            if (data.itemDetails.itemType === "Packed") {
                $('#NetWeightId').prop('disabled', true);
                $('#SearchQuantity').val(1).prop('disabled', false);
                $('#NetWeightId').val(data.itemDetails.weight);
                $('#SearchAmount').val(parseFloat(data.itemDetails.sellingPrice));
            }
            else {
                $('#NetWeightId').prop('disabled', false);
                $('#SearchQuantity').val(1).prop('disabled', true);
                $('#NetWeightId').val(0);
                var weighing = $('#NetWeightId').val();
                price = parseFloat(((data.itemDetails.sellingPrice * weighing) / data.itemDetails.weight).toFixed(2));
                $('#SearchAmount').val(parseFloat(price));
            }
        }
    });
}, 500));

$('#selectId').select2({
    containerCssClass: "select2-data-ajax",
    ajax: {
        url: "/Sale/ItemDetails",
        dataType: "json",
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
                    id: data.Data[i].value,
                    text: data.Data[i].text
                });
            }
            return {
                results: results,
                pagination: {
                    more: false
                }
            };
        },
        cache: !0
    },
    escapeMarkup: function (e) {
        return e;
    },
    minimumInputLength: 1
});

$("#SearchDiscount").on('change', function () {
    var discount = $('#SearchDiscount').val();
    var price = $('#tempSearchAmount').text();
    var price1 = $('#Sprice').text();
    var tempPrice = 0.0;
    var discountedPrice = 0.0;
    var type = $('#SItemtype').text();
    //tempPrice = parseFloat(discount * price / 100).toFixed(2);
    if ($('#SearchDiscount').val() != '') {
        discountedPrice = parseFloat(price - discount).toFixed(2);
        discountedPrice = Math.round(discountedPrice);
        $('#SearchAmount').val(discountedPrice);
    }
    else {
        if ($('#SItemtype').text() == 'Packed') {
            var quantity = $('#SearchQuantity').val();
            var netprice = parseFloat(quantity * price1).toFixed(2);
            $('#SearchAmount').val(netprice);
        }
        else {
            var weighing = $('#NetWeightId').val();
            var weight = $('#ShiddenWeight').val();
            var netprice = parseFloat((price1 * weighing) / weight).toFixed(2);
            $('#SearchAmount').val(netprice);
        }
    }
});

$("#SearchQuantity").on('change', function () {
    $('#SearchDiscount').prop('value', '');
    var quantity = $('#SearchQuantity').val();
    var price = $('#Sprice').text();
    var marketPrice = $('#MarketPrice').text();
    var netprice = 0.0;
    if ($('#SearchQuantity').val() == '') {
        $('#SearchAmount').val(parseFloat(price));
        $('#ShdMarket').text(parseFloat(marketPrice));
    }
    else {
        netprice = parseFloat(quantity * price).toFixed(2);
        marketPrice = parseFloat(quantity * marketPrice).toFixed(2);
        //marketPrice = Math.round(marketPrice);
        //netprice = Math.round(netprice);
        $('#SearchAmount').val(netprice);
        $('#ShdMarket').text(marketPrice);
        $('#tempSearchAmount').text(netprice);
    }
});

$("#NetWeightId").on('change', function () {
    $('#SearchDiscount').prop('value', '');
    var weight = $('#ShiddenWeight').val();
    var sellingPrice = $('#Sprice').text();
    var weighing = $('#NetWeightId').val();
    var marketPrice = $('#MarketPrice').text();
    if ($('#NetWeightId').val() == '') {
        $('#SearchAmount').val(parseFloat(sellingPrice));
        $('#ShdMarket').text(parseFloat(marketPrice));
    }
    else {
        var price = parseFloat((sellingPrice * weighing) / weight).toFixed(2);
        marketPrice = parseFloat((marketPrice * weighing) / weight).toFixed(2);
        marketPrice = Math.round(marketPrice);
        price = Math.round(price);
        $('#SearchAmount').val(parseFloat(price));
        $('#ShdMarket').text(parseFloat(marketPrice));
        $('#tempSearchAmount').text(price);
    }
});

//function AddSearchId(ItemId, weightId, quantityId, discountId, actualPrice, category, pluName, measure, unitprice, categoryId) {
//    var numberoferror = 0;
//    var data = $('#SearchQuantity').val();
//    var value = $('#SearchQuantity').val().trim().length;
//    if ($('#SearchQuantity').val().trim().length == 0) {
//        //$('#SearchQuantity').css('border-color', 'red');
//        $('#SpnSearchquantity_Err').html("Cannot be Empty").css('display', 'block');
//        numberoferror++;
//    }
//    if ($('#SearchDiscount').val().trim().length == 0) {
//        $(discountId).val(0);
//    }
//    if (numberoferror == 0) {
//        var productamount = $('#SearchAmount').val();
//        if (productamount != "Rs.00.00") {
//            var itemId = $(ItemId).text();
//            var weight = $(weightId).val();
//            var quantity = $(quantityId).val();
//            var discount = $(discountId).val();
//            var actualPrice = $(actualPrice).val();
//            var category = $(category).val();
//            var pluname = $(pluName).text();
//            var measure = $(measure).text();
//            var unitprice = $(unitprice).text();
//            var categoryId = $(categoryId).text();
//            var total = 0;
//            var totakCartquant = $('#totalCartQuantity').text();
//            var Amount = $('#AmountId').text();
//            var amount1 = $('#amount1').val();
//            var market = $('#ShdMarket').text();
//            var item_image = $('#item_image').val();
//            var weighing = $('#NetWeightId').val();
//            $('#CartListbody').append(
//                '<tr id=' + itemId + '>' +
//                '<td>' +
//                '<div class="m-widget4__img m-widget4__img--logo">' +
//                '<img src="' + item_image + '" alt="' + pluname + '" style="width:60px;">' +
//                '</div>' +
//                '</td>' +
//                '<td>' +
//                '<h6 class="mb-1">' + pluname + '</h6>' +
//                '<span class="display-block font-small-3 text-bold-500 text-muted">' + category + '</span>' +
//                '<span hidden id="cateId_' + itemId + '">' + categoryId + '</span>' +
//                '</td>' +
//                '<td>' +
//                '<span>' + weight + measure + 'X Rs.' + unitprice + '</span>' +
//                '<span hidden id="measure_' + itemId + '">' + measure + '</span>' +
//                '<span hidden id="unitprice_' + itemId + '" >' + unitprice + '</span>' +
//                '<span hidden id="delivery_' + itemId + '" >no</span>' +
//                '<span hidden id="Market_' + itemId + '" >' + market + '</span>' +
//                '</td>' +
//                '<td><span id="weight_' + itemId + '" class="text-bold-400"> ' + weighing + ' </span>' +
//                '<td><span id="quantity_' + itemId + '" class="text-bold-400"> ' + quantity + ' </span>' +
//                '</td > ' +
//                '<td>' +
//                '<span id="discount_' + itemId + '" >' + discount + '%</span>' +
//                '<span hidden id="discountId_' + itemId + '" >' + discount + '</span>' +
//                '</td>' +
//                '<td><span id="actualPrice_' + itemId + '" class="text-bold-500 text-info">' + actualPrice + '</span>' +
//                '</td > ' +
//                '<td>' +
//                '<a href="#" id="removeId" onclick=remove(\'' + itemId + '\') class="text-danger font-medium-1 text-bold-600">X</a>' +
//                '</td>' +
//                '</tr>'
//            );
//            $('#HomeDeliveryId').append(
//                '<div class="col-md-2" id="divId' + itemId + '" >' +
//                '<div class="text-center">' +
//                '<div class="mb-3 item_list">' +
//                '<label class="m-checkbox m-checkbox--solid m-checkbox--success display-block">' +
//                '<input type="checkbox" onclick="CheckboxId(\'' + itemId + '\')" id="' + itemId + '" value="' + itemId + '" name="Check">' +
//                '<img src="' + item_image + '" alt="' + pluname + '" style="width:60px;" class="img-fluid" />' +
//                '<h6 class="mb-0 font-medium-1">' + pluname + '</h6>' +
//                '<span></span>' +
//                '</label>' +
//                '</div>' +
//                '</div>' +
//                '</div>'
//            );
//            $('#Additem').modal('hide');
//            total = $('#TotalCartItemId').text();
//            total = parseInt(total) + parseInt(1);
//            $('#TotalCartItemId').text(total);
//            totakCartquant = parseFloat(totakCartquant) + parseFloat(quantity);
//            $('#totalCartQuantity').text(totakCartquant);
//            Amount = parseFloat(Amount) + parseFloat(actualPrice);
//            $('#AmountId').text(Amount);
//            amount1 = parseFloat(amount1) + parseFloat(actualPrice);
//            $('#amount1').val(amount1);
//            swal("Item Added to the cart", "", "success");
//        }
//        else {
//            swal("Select the Product Name", "", "warning");
//        }
//    }
//}

function ClearError() {
    $('#SpnDeliverydate').text('');
}

function CustEdit(param) {
    var paramarray = param.split(',');
    var custid = paramarray[0];
    var addid = paramarray[1];
    $.ajax({
        url: "/Sale/GetcustDetail?Type=" + 'Default' + "&custId=" + custid,
        type: "GET",
        success: function (result) {
            $CustomerId.val(result.Data.customerId);
            $Name.val(result.Data.name);
            $EmailId.val(result.Data.emailId);
            //$Ext.val(result.getCustomerDetail.ext);
            $ContactNo.val(result.Data.contactNo);
            $BuildingName.val(result.Data.buildingName);
            $RoomNo.val(result.Data.roomNo);
            $Sector.val(result.Data.sector);
            $Landmark.val(result.Data.landmark);
            $Locality.val(result.Data.locality);
            $AddressType.val(result.Data.addressType);
            $ZipCode.val(result.Data.zipCode);
            $City.val(result.Data.city);
            $State.val(result.Data.state);
            $Country.val(result.Data.country);
            $('#addid').val(result.Data.addId);
            $('#compAddId').val(addid);
            $('#create_new_address').modal('show');
        }
    });
}

$('#AddSearchId1').on('click', function () {
    var purchaseprice = $('#SPurchaseprice').text();
    var stockid = $('#SstockId').text();
    var itemId = $('#SItemId').text();
    var itemName = $('#SItemName').text();
    var weightid = $('#NetWeightId').val();
    var totakCartquant = $('#totalCartQuantity').text();
    var Amount = $('#AmountId').text();
    var amount1 = $('#amount1').val();
    var actualPrice = $('#SearchAmount').val();
    var quantity = $('#SearchQuantity').val();
    var discount = $('#SearchDiscount').val();
    var weight = $('#ShiddenWeight').val();
    var categoryId = $('#SCategoryId').text();
    var categoryname = $('#SCategoryName').text();
    var discountAmount = $('#DisAmountId').text();
    var coupenDisc = $('#SCoupenDisc').text();
    var Size = $('#SpnSize').text();
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    var DiscountPrice = 0;
    if ($('#SearchAmount').prop('value').trim()) {
        if (!$('#SearchDiscount').prop('value').trim()) {
            discount = 0;
        }
        if (!$('#NetWeightId').prop('value').trim()) {
            weightid = weight;
        }
        if (!$('#SearchQuantity').prop('value').trim()) {
            quantity = 1;
        }
        var AddedDiscount = parseFloat(actualPrice) + parseFloat(discount);
        var dicData = {
            ItemId: itemId,
            PluName: itemName,
            Category: categoryId,
            Measurement: $('#Smeaure').text(),
            Weight: weight,
            SellingPrice: $('#Sprice').text(),
            MarketPrice: $('#ShdMarket').text(),
            CategoryName: categoryname,
            Quantity: quantity,
            NetWeight: weightid,
            Discount: discount,
            ActualCost: actualPrice,
            ItemType: $('#ItemType').text(),
            itemstock: $('#itemstock').text(),
            stockId: $('#SstockId').text(),
            Purchaseprice: $('#SPurchaseprice').text(),
            CoupenDisc: coupenDisc,
            ImagePath: "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/icon/" + itemId + ".png",
            AddedDiscount: AddedDiscount,
            PriceId: $('#PriceId').text(),
            Size: Size,
        };
        $('#' + itemId).remove();
        $('#divId_' + itemId).remove();
        $.ajax({
            url: '/Sale/_AddCart',
            type: 'POST',
            data: dicData,
            success: function (data) {
                $('#SstockId').text($('#stockId_' + itemId).text());
                var existing = $('#CartListbody').html();
                var array = [];
                array.push(existing);
                array.push(data);
                $('#CartListbody').html(array);
                $('#Additem').modal('hide');
                SaveHomeDelivery(dicData);
                $('#NetWeightId').prop('value', '');
                $('#SearchDiscount').prop('value', 0);
                $('#SearchQuantity').prop('value', '');
                $('#SearchAmount').prop('value', '');
                AddTotalItem();
                //var acualamtbydiscount = parseFloat(ItemWithCoupen) - parseFloat(discountAmount);
                //acualamtbydiscount = Math.round(acualamtbydiscount);
                //var TotalCalculatedAmount = parseFloat(ItemWithoutCoupen) + parseFloat(acualamtbydiscount);
                //$('#amount1').val(TotalCalculatedAmount);
                //$('#TDisAmountId').text(TotalCalculatedAmount);
                CoupenCode();
            }
        });
    }
});

$.selector_cache('#deliverytxt').on('keyup', $.delay(function () {
    var tmp = $.selector_cache('#HomeDeliveryId')[0].children;
    var ItemName = $.selector_cache("#deliverytxt").prop('value');
    for (var i = 0; i < tmp.length; i++) {
        var value = tmp[i];
        var name = value.innerText;
        var id = value.id;
        if (name.toLowerCase().indexOf(ItemName.toLowerCase()) > -1) {
            $('#' + id).css('display', 'block');
        }
        else {
            $('#' + id).css('display', 'none');
        }
    }
    //tmp.each(tmp, function (key, value) {
    //    alert(key + ": " + value);
    //});
    //$.ajax({
    //    type: 'GET',
    //    url: '/Sale/getItemdata',
    //    data: { 'ItemName': ItemName },
    //    success: function (data) {
    //        alert(data);
    //        if (data !== null ) {
    //            $.selector_cache('#HomeDeliveryId').html(data.Data);
    //        } 
    //         else {
    //            swal('Error!', 'There was some error. Try again later.', 'error');
    //        }
    //    },
    //    error: function (xhr, ajaxOptions, thrownError) {
    //        alert();
    //        swal('Error!', 'There was some error. Try again later.', 'error');
    //    }
    //});
}, 300));

$('#Discountamount').on('change', function () {
    Discountapply();
});

function Discountapply() {
    var returnamt2 = $('#amount2').val();
    var discountamt = $('#Discountamount').val();
    var customeramt = $('#AmountId').text();
    var deliveryCharge = $('#Deliverycharges').val();
    var discountpercent = parseFloat(discountamt) * parseFloat(customeramt) / 100;
    discountpercent = Math.round(discountpercent);
    var acualamtbydiscount = parseFloat(customeramt) - parseFloat(discountpercent) + parseFloat(deliveryCharge);
    acualamtbydiscount = Math.round(acualamtbydiscount);
    $('#ActualDiscountAmt').val(discountpercent);
    $('#DiscountTotalamt').val(parseFloat(customeramt) - parseFloat(discountpercent));
    $('#DisAmountId').text(discountpercent);
    $('#TDisAmountId').text(acualamtbydiscount);
    $('#amount1').val(acualamtbydiscount);
    if (returnamt2 != "") {
        var returnamt = parseFloat(returnamt2) - parseFloat(acualamtbydiscount);
        returnamt = Math.round(returnamt);
        $('#total').text(returnamt);
    }
    if ($('#total').text() > 0) {
        $('#total').removeClass("text-danger");
        $('#total').addClass("text-success");
    }
    else {
        $('#total').removeClass("text-success");
        $('#total').addClass("text-danger");
    }
}

$.selector_cache('#selectCoupen').on('change', function () {
    if ($(this).val() == 1) {
        $.makeArray($('#CartListbody tr[id]').map(function () {
            var totalCost = $('#AddedDiscount_' + this.id).text();
            $('#discount_' + this.id).text(0);
            $('#discountId_' + this.id).text(0);
            $('#actualPrice_' + this.id).text(totalCost);
        }));
    }
    CoupenCode();
    Discountapply();
});

//$('#mymoney').on('click', function () {

//    SubmitError = 0;
//    if ($('#TotalItemId').text() > 0) {
//        Submit();
//    }
//    else {
//        if ($('#TotalCartItemId').text() > 0) {
//            swal({
//                title: "Are you sure you want to create sales order?",
//                type: "warning",
//                showCancelButton: !0,
//                confirmButtonText: "Yes,create!"
//            }).then(function (e) {
//                if (e.value) {
//                    mymoney();
//                }
//            });
//        } else {
//            if ($('#CartListbody').text() === "") {
//                swal("No item in list to create order", "", "warning");
//            }
//        }
//    }
//});

function Submit() {
    var orderstatus = "Ordered";
    var paymentStatus = $('#PaymentStatus').val();
    var deliveryDate = "";
    var customerdata = "";
    var drpsalesperson = "";
    var paymentmode = $('#PaymentMode').val();
    var deliverytime = $('#SlotId').val();
    var CashReceived = $('#amount2').val();
    var CashRemaining = $('#total').text();
    var discountper = $('#Discountamount').val();
    var Totaldisamt = $('#DiscountTotalamt').val();
    var ActualDisc = $('#ActualDiscountAmt').val();
    var coupenID = $('#coupenIds').val();
    var tableId = $('#selecttableId').val();
    var customerId = $('#cusdetailId').text();
    var Address = "";
    var Branch = "";
    var DiscountedAmount = $('#Cashamount').val();
    var checkedValues = $('input:checkbox[name=Check]:checked').length;
    if (checkedValues === 0) {
        swal("No item in list to create order", "", "warning");
        SubmitError++;
    }
    if (checkedValues > 0) {
        var deliveryCharge = $('#Deliverycharges').val();
        if ($('#DeliveryDateId').val() === "") {
            $('#SpnDeliverydate').html("This field is required").css('display', 'block');
            SubmitError++;
        }
        if ($('#PaymentStatus').val() === "") {
            $('#SpnPaymentstatus').html("This field is required").css('display', 'block');
            SubmitError++;
        }
        if ($('#SlotId').val() === "") {
            $('#SpnSlotId').html("This field is required").css('display', 'block');
            SubmitError++;
        }
        if ($('#spnaddress').text() === "" || $('#spnaddress').text() === null) {
            swal({
                title: "Address Required",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes,create!"
            }).then(function (e) {
                if (e.value) {
                    $('#create_new_address').modal('show');
                }
                //swal("Address Required", "", "warning");
            });
            SubmitError++;
        }
    }
    if (0 === SubmitError) {
        swal({
            title: "Are you sure you want to home deliver?",
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes!"
        }).then(function (e) {
            if (e.value) {
                var totalCount = 0;
                var unitCost = "";
                var quantity = 0;
                var weight;
                var totalQuantity = 0;
                var totalCost = 0;
                var category;
                var stock = 0;
                var measure;
                var totalSaleCOst = 0.0;
                var selltype;
                var salesPerson = "";
                var marketPrice = 0;
                var totalmarketPrice = 0;
                var discount = 0;
                var Hub = "NA";
                var homeDelivery = "";
                var stocksid = 0;
                var purchaseprice = 0;
                var stockListss = new Array();
                var stockData = {};
                var deliveryTypes = "";
                var delidate = $('#DeliveryDateId').val();
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1;
                var yyyy = today.getFullYear();
                if (mm < 10) mm = '0' + mm;
                today = mm + '/' + dd + '/' + yyyy;
                if (delidate == today) {
                    deliveryTypes = "Express";
                    //$('#Deliverycharges').val(30);
                }
                else {
                    deliveryTypes = "Regular";
                }
                var itemDetails = $.makeArray($('#CartListbody tr[id]').map(function () {
                    if ($('#delivery_' + this.id).text() == "yes") {
                        try {
                            totalCount++;
                            itemId = $('#ItemId_' + this.id).text();
                            unitCost = $('#unitprice_' + this.id).text();
                            quantity = $('#quantity_' + this.id).val();
                            weight = $('#Netweight_' + this.id).val();
                            measure = $('#measure_' + this.id).text();
                            category = $('#cateId_' + itemId).text(); // itemId
                            totalCost = $('#actualPrice_' + this.id).text();
                            marketPrice = $('#Market_' + this.id).text();
                            stock = $('#Totalitemstock_' + itemId).text(); // itemId
                            purchaseprice = $('#purchaseprice_' + this.id).text();
                            discount = $('#discountId_' + this.id).text();  // itemId first change to priceId
                            homeDelivery = $('#delivery_' + this.id).text();
                            totalQuantity += parseFloat(quantity);
                            totalSaleCOst += parseFloat(totalCost);
                            totalmarketPrice += parseFloat(marketPrice);
                            ItemType = $('#ItemType_' + itemId).text(); // itemId
                            stocksid = $('#stockId_' + itemId).text(); // itemId
                            stockData.unitCost = $('#unitprice_' + this.id).text();
                            stockListss.push(stockData);
                        } catch (e) {
                            swal('Error', 'Error message', 'error');
                        }
                        //return this.id + "_" + unitCost + "_" + quantity + "_" + category + "_" + totalCost + "_" + weight + "_" + measure + "_" + stock + "_" + discount + "_" + stocksid + "_" + ItemType + "_" + purchaseprice + "|";
                        return itemId + "_" + this.id + "_" + unitCost + "_" + quantity + "_" + remark + "_" + category + "_" + totalCost + "_" + weight + "_" + measure + "_" + stock + "_" + discount + "_" + stocksid + "_" + ItemType + "_" + purchaseprice + "|";
                    }
                }));
                $('#SavedPriceId').text(Math.round(parseFloat(totalmarketPrice).toFixed(2) - parseFloat(totalSaleCOst).toFixed(2)));
                var CheckedCount = $('input[name="Check"]:checked').length;
                var TotalCount = $('input[name="Check"]').length;
                var actualdisc = "";
                actualdisc = CheckedCount == TotalCount ? $('#ActualDiscountAmt').val() : 0;
                var dicData = {
                    //CustomerId: $('#spncustid').text(),
                    CustomerId: $('#cusdetailId').text(),
                    OrderdStatus: orderstatus,
                    DeliveryDate: $('#DeliveryDateId').val(),
                    SlotId: deliverytime,
                    PaymentStatus: paymentStatus,
                    PaymentMode: paymentmode,
                    AddressId: $('#spnaddress').text(),
                    HubId: Hub,
                    Branch: Branch,
                    TotalPrice: totalSaleCOst,
                    TotaldisAmt: Totaldisamt,
                    SalesPerson: salesPerson,
                    TotalQuantity: totalQuantity,
                    PLU_Count: totalCount,
                    MultipleItem: itemDetails,
                    DeliveryCharges: deliveryCharge,
                    coupenId: coupenID,
                    DeliveryType: deliveryTypes,
                    ActualDiscAmt: actualdisc,
                    Remaining_Amount: $('#total').text(),
                    DiscountedAmount: DiscountedAmount,
                    tableId: tableId
                };
                if (unitCost !== "" && totalQuantity !== "" && totalCost !== "" && totalSaleCOst !== "") {
                    $.ajax({
                        url: '/Sale/InsertSale',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (salesId) {
                            if (salesId !== false) {
                                $('#HomeSalesId').val(salesId);
                                //window.location.href = "/Print/Delivery?id=" + salesId + "&saved=" + $('#SavedPriceId').text();
                                $('#submitId').prop('disabled', true);
                                $('#amount1').val(parseFloat($('#amount1').val()) + parseFloat($('#Deliverycharges').val()));
                                swal('Item added for home delivery', '', '');
                                //if ($('#TotalCartItemId').text() > $('#TotalItemId').text())
                                //    mymoney();
                                //else {
                                //    var homedeliverycount = $('#TotalItemId').text();
                                //    if (homedeliverycount != 0) {
                                //if (stockListss.length == homedeliverycount) {
                                $('#SavedPriceId').text(Math.round(parseFloat(totalmarketPrice).toFixed(2) - parseFloat(totalSaleCOst).toFixed(2)));
                                window.location.href = "/Print/Index?id1=" + salesId + "&received=" + CashReceived + "&remaining=" + CashRemaining + "&saved=" + $('#SavedPriceId').text() +
                                    "&AmountTotal=" + $('#AmountId').text() + "&quant=" + $('#totalCartQuantity').text() + "&item=" + $('#TotalCartItemId').text() + '&DiscountPer=' + discountper + '&TotalDiscountAmt=' + Totaldisamt +
                                    '&ActualDiscountAmt=' + $('#ActualDiscountAmt').val();
                                //}
                                //totalSaleCOst = 0;
                                //totalUnitCost = 0;
                                //totalmarketPrice = 0;
                                //    }
                                //}
                            }
                            else {
                                swal('Oops!', 'Something went wrong!', 'warning');
                            }
                        }
                    });
                } else {
                    swal('Oops!', 'Fields can not be Empty', 'warning');
                }
            }
        });
    }
}

async function audio() {
    document.getElementById('myaudio').play();
    document.getElementById('myaudio').muted = false;
    await sleep(2000);
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

$('#Cashamount').on('change', function () {
    var value = $(this).val();
    var customeramt = $('#AmountId').text();
    var deliveryCharge = $('#Deliverycharges').val();
    if (customeramt == 0.0) {
        $('#SpnDisCashAmnt').css('display', 'block');
        $('#Cashamount').prop('value', 0);
        $('#DisAmountId').text(0);
        $('#amount1').val(0);
        $('#TDisAmountId').text(0);
    }
    else {
        var acualamtbydiscount = parseFloat(customeramt) - parseFloat(value) + parseFloat(deliveryCharge);
        acualamtbydiscount = Math.round(acualamtbydiscount);
        $('#amount1').val(acualamtbydiscount);
        $('#TDisAmountId').text(acualamtbydiscount);
        $('#DisAmountId').text(value);
        $('#Discountamount').val(0);
        $('#ActualDiscountAmt').val(value);
        $('#DiscountTotalamt').val(parseFloat(customeramt) - parseFloat(value));
    }
});

$('#Cashamount').on('click', function () {
    var customeramt = $('#AmountId').text();
    if (customeramt == 0.0) {
        $('#SpnDisCashAmnt').css('display', 'block');
        $('#Cashamount').prop('value', 0);
        $('#DisAmountId').text(0);
        $('#amount1').val(0);
        $('#TDisAmountId').text(0);
    }
    else $('#SpnDisCashAmnt').css('display', 'none');
});

function CoupenCode() {
    var selectcoupen = $('#selectCoupen').val();
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    if (selectcoupen == "1") {
        $('#divDisAmount').css('display', 'none');
        $('#divCashDisAmount').css('display', 'block');
        var cashdiscAmt = $('#DisAmountId').text();
        if (cashdiscAmt == 0) {
            $('#Cashamount').prop('value', 0);
        }
        $('#Discountamount').prop('value', 0)
    }
    else {
        $('#divDisAmount').css('display', 'block');
        $('#divCashDisAmount').css('display', 'none');
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var coupenId = coupen[1];
        var maxdiscount = coupen[2];
        $('#Discountamount').val(coupendiscount);
        var returnamt2 = $('#amount2').val();
        // var customeramt = $('#AmountId').text();
        $.makeArray($('#CartListbody tr[id]').map(function () {
            ItemId = $('#ItemId_' + this.id).text();
            if ($('#coupenDisc_' + ItemId).text() == 1) {
                var ItemCoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithCoupen += parseFloat(ItemCoupenCost);
                var discountpercent = (parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2);
                var discountedPrice = (parseFloat(ItemCoupenCost) - parseFloat(discountpercent)).toFixed(2);
                $('#actualPrice_' + this.id).text(discountedPrice);
                $('#discount_' + this.id).text(discountpercent);
                $('#discountId_' + this.id).text(discountpercent);
            }
            else {
                var ItemWithoutoupenCost = $('#AddedDiscount_' + this.id).text();
                var discount = $('#discount_' + this.id).text();
                var discountedPrice = parseFloat(ItemWithoutoupenCost) - parseFloat(discount);
                ItemWithoutCoupen += parseFloat(discountedPrice);
            }
        }));
        $('#ItemWithCoupen').text(ItemWithCoupen);
        $('#ItemWitoutCoupen').text(ItemWithoutCoupen);
        var customeramt = $('#ItemWithCoupen').text();
        $('#coupenIds').val(coupenId);
        var discountpercent = (parseFloat(coupendiscount) * parseFloat(customeramt) / 100).toFixed(2);
        //discountpercent = Math.round(discountpercent);
        discountpercent = parseFloat(discountpercent) > parseFloat(maxdiscount) ? parseFloat(maxdiscount) : parseFloat(discountpercent);
        var acualamtbydiscount = (parseFloat(customeramt) - parseFloat(discountpercent)).toFixed(2);
        //acualamtbydiscount = Math.round(acualamtbydiscount);
        var TotalWithCoupen = parseFloat($('#ItemWitoutCoupen').text()) + parseFloat(acualamtbydiscount);
        //alert(TotalWithCoupen);   
        $('#DiscountTotalamt').val(TotalWithCoupen);
        TotalWithCoupen = Math.round(TotalWithCoupen);
        $('#amount1').val(TotalWithCoupen);
        $('#ActualDiscountAmt').val(discountpercent);
        $('#DisAmountId').text(discountpercent);
        $('#TDisAmountId').text(TotalWithCoupen);
        AddTotalAfterCoupen();
        if (returnamt2 != "") {
            var returnamt = parseFloat(returnamt2) - parseFloat(TotalWithCoupen);
            returnamt = Math.round(returnamt);
            $('#total').text(returnamt);
        }
        if ($('#total').text() > 0) {
            $('#total').removeClass("text-danger");
            $('#total').addClass("text-success");
        }
        else {
            $('#total').removeClass("text-success");
            $('#total').addClass("text-danger");
        }
    }
    //$('#TDisAmountId').text((parseFloat($('#AmountId').text()) + parseFloat($('#DisAmountId').text())).toFixed(2));
}

function AddTotalItem() {
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var DiscountPrice = 0;
    $.makeArray($('#CartListbody tr[id]').map(function () {
        ItemId = $('#ItemId_' + this.id).text();
        var quantity = $('#quantity_' + this.id).val();
        var remark = $('#remark_' + this.id).val();
        var totalCost = $('#actualPrice_' + this.id).text();
        var discount = $('#discount_' + this.id).text();
        totalSaleCOst += parseFloat(totalCost);
        totalQuantity += parseFloat(quantity);
        DiscountPrice += parseFloat(discount);
    }));
    var selectcoupen = $('#selectCoupen').val();
    if (selectcoupen == "1") {
        var length = $("#CartListbody").find("tr").length;
        $('#TotalCartItemId').text(length);
        $('#totalCartQuantity').text(totalQuantity);
        var remark = $('#remark_' + this.id).val();
        $('#totalCartremark').text(remark);
        var cashdiscAmt = $('#DisAmountId').text();
        totaldiscAmountId = totalSaleCOst - cashdiscAmt;
        $('#Cashamount').prop('value', cashdiscAmt);
        $('#ActualDiscountAmt').val(Math.round(cashdiscAmt));
        $('#DisAmountId').text(Math.round(cashdiscAmt));
        $('#amount1').prop('value', Math.round(totaldiscAmountId));
        $('#TDisAmountId').text(totaldiscAmountId);
        $('#DiscountTotalamt').text(totaldiscAmountId);
        $('#AmountId').text(Math.round(totalSaleCOst));
        $('#Amountspn').text(Math.round(totalSaleCOst));
    }
    else {
        var length = $("#CartListbody").find("tr").length;
        $('#TotalCartItemId').text(length);
        $('#totalCartQuantity').text(totalQuantity);
        var remark = $('#remark_' + this.id).val();
        $('#totalCartremark').text(remark);
        $('#AmountId').text(Math.round(totalSaleCOst));
        $('#Amountspn').text(Math.round(totalSaleCOst));
        $('#amount1').prop('value', Math.round(totalSaleCOst));
        $('#TDisAmountId').text(Math.round(totalSaleCOst));
        $('#DiscountTotalamt').prop('value', totalSaleCOst);
    }
}

function AddTotalAfterCoupen() {
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var DiscountPrice = 0;
    $.makeArray($('#CartListbody tr[id]').map(function () {
        var quantity = $('#quantity_' + this.id).val();
        var totalCost = $('#AddedDiscount_' + this.id).text();
        var discount = $('#discount_' + this.id).text();
        totalSaleCOst += parseFloat(totalCost);
        totalQuantity += parseFloat(quantity);
        DiscountPrice += parseFloat(discount);
    }));
    $('#AmountId').text(Math.round(totalSaleCOst));
    //$('#Amountspn').text(totalSaleCOst);
    //$('#amount1').prop('value', totalSaleCOst);
}

$("#create2add1").on('click', function () {
    Swal.fire({
        title: 'Are you sure?',
        text: 'you want to Create new Address',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Add it!'
    }).then((result) => {
        var CustomerId = $('#CustomerId').val();
        //var AddId = $('#addid').prop('value');
        var AddressType = $("#AddressType").prop('value');
        var Address1 = $('#Address1').prop('value');
        var Locality = $("#Locality").prop('value');
        var Sector = $("#Sector").prop('value');
        var Landmark = $("#Landmark").prop('value');
        var City = $("#City").prop('value');
        var State = $("#State").prop('value');
        var Country = $("#Country").prop('value');
        if (result.value) {
            var info = {
                CustomerId: CustomerId,
                AddressType: AddressType,
                Address1: Address1,
                Locality: Locality,
                Sector: Sector,
                Landmark: Landmark,
                City: City,
                State: State,
                Country: Country
            }
            if (CustomerId != "") {
                $.ajax({
                    url: '/Customer/editAddress/',
                    type: 'GET',
                    data: info,
                    success: function (result) {
                        if (result.result == 0) {
                            $('#create_new_address').modal('hide');
                            $('#tblcustaddressdetail').html(data);
                        }
                        else {
                            $('#create_new_address').modal('hide');
                            $.ajax({
                                url: '/Sale/_CustMultiAdd',
                                type: 'Get',
                                data: { Id: $('#custdetailorgid').text() },
                                success: function (data) {
                                    $('#tblcustaddressdetail').html(data);
                                }
                            });
                        }
                    }
                });
            }
        }
    });
});
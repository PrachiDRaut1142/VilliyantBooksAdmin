!(function ($) {
    $(".card").on('click','#update_Items', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateListUpdateItemPanelData?id=' + Id,
            type: 'GET',
            success: function (result) {
                $('.salesDetailsUpdatePanel').html(result);
                $('.itemstbas a[href="#catgory_MNCTG02"]').tab('show');
                $(".itemUpdate").toggleClass("toggle-panel");
            }
        });
        $.ajax({
            type: 'GET',
            url: '/Sale/_SalesDetailsUpdatePanelData?Id=' + Id,
            success: function (result) {
                $('.OrderInfoDetails').html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
        $.ajax({
            type: 'GET',
            url: '/Sale/_SaleDetailsProductListUpdateItemPanelData?Id=' + Id,
            success: function (result) {
                $('.PdlistData').html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
    });

    $("#close_update_item").click(function () {
        $(".itemUpdate").removeClass("toggle-panel");
        $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
        window.location.reload();
    });

    $.selector_cache('#findProduct').on('keyup', $.delay(function () {
        var ItemName = $.selector_cache("#findProduct").prop('value');
        if (ItemName != "") {
            $('#editclearinputElement').css('display', 'block');
            $.ajax({
                type: 'GET',
                url: '/Sale/_ItemListUpdateData/',
                data: { 'ItemName': ItemName, 'maincategory': "" },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        //$('#CartBodyListabc_CTG01200').css('display', 'none');
                        $('.EditMainCategorytabs').css('display', 'none');
                        $('.fixed-right-height').css("margin-top", "0px");
                        $('#Editcategoryesd').css('display', 'none');
                        $('.PdlistData').removeClass("col-md-4");
                        $('.PdlistData').addClass("col-md-6");
                        $(".Edititemtabpane").removeClass("active show");
                        $(".Edititemtabpane").first().addClass("active show");
                        //$('#CartBodyListupdate22').html(data.Data);
                        $('#CartBodyListabc_CTG01200').empty();
                        $('#CartBodyListabc_CTG01200').html(data.Data);
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
        }
        else {
            getEditActiveData();
            $('.EditMainCategorytabs').css('display', 'block');
            $('.fixed-right-height').css("margin-top", "-2.8rem");
            $('#Editcategoryesd').css('display', 'block');
            $('.PdlistData').removeClass("col-md-6");
            $('.PdlistData').addClass("col-md-4");
            //$('#CartBodyListupdate22').html("");
            $('#editclearinputElement').css('display', 'block');
        }
    }, 500));

    $('#editclearinputElement').on('click', function () {
        $("#findProduct").val("");
        getEditActiveData();
        $('.EditMainCategorytabs').css('display', 'block');
        $('.fixed-right-height').css("margin-top", "-2.8rem");
        $('#Editcategoryesd').css('display', 'block');
        $('.PdlistData').removeClass("col-md-6");
        $('.PdlistData').addClass("col-md-4");
        $('#CartBodyListupdate22').html("");
        $('#editclearinputElement').css('display', 'none');
    });
})(jQuery);

$(document).on('click', '#EditRemark', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    $('#editremarksclosed_' + Id).css('display', 'flex');
});

$(document).on('click', '#ClosedRemark', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    $('#editremarksclosed_' + Id).css('display', 'none');
});

$("document").ready(function () {
    var target = $('.Editcategorytabs a[data-toggle="tab"]').attr("href");
    $('#SearchItem').prop('value', "");
    var ItemName = $.selector_cache("#SearchItem").prop('value');
    var maincategory = target.slice(4);
    $.selector_cache('#activeTab').prop('value', target);
    $.ajax({
        type: 'GET',
        url: '/Sale/_ItemListUpdateData/',
        data: { 'ItemName': ItemName, 'maincategory': maincategory },
        success: function (data) {
            if (data !== null && data.IsSuccess) {
                $('#CartBodyListabc_' + maincategory).html(data.Data);
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

$('.Editmaincategory').on('shown.bs.tab', function (e) {
    $('#SearchItem').prop('value', "");
    var ItemName = $.selector_cache("#SearchItem").prop('value');
    var catId = "";
    var catName = "";
    var target = $(e.target).attr("href");
    var maincategory = target.slice(5);
    $.selector_cache('#activeTab').prop('value', target);
    $('.Edititemtabpane ').removeClass('active');
    $('.Editcategory').removeClass('active show');
    $.ajax({
        url: "/Sale/CategoryListMainCatWise?MainCatId=" + maincategory,
        type: "GET",
        success: function (itemvList) {
            if (itemvList.length > 0) {
                catId = itemvList[0].categoryId;
                catName = itemvList[0].category;
                var categoryId = '#CE_' + catId;
                $('#MCE_' + maincategory).addClass('active show');
                $('.Editcategory a[href="' + categoryId + '"] ').addClass('active show');
                $('#CE_' + catId).addClass('active show');
            }
            if (catId != "") {
                $.ajax({
                    type: 'GET',
                    url: '/Sale/_ItemListUpdateData/',
                    data: { 'ItemName': ItemName, 'maincategory': catId },
                    success: function (data) {
                        if (data !== null && data.IsSuccess) {
                            $('.Editcategory a[href="' + categoryId + '"] ').addClass('active show');
                            $('#CartBodyListabc_' + catId).html(data.Data);
                            $('#divCE_' + catId).addClass('active show');
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
            }
        },
    });
});

$('.Editcategory').on('shown.bs.tab', function (e) {
    $('.Edititemtabpane ').removeClass('active show');
    $('#SearchItem').prop('value', "");
    var ItemName = $.selector_cache("#SearchItem").prop('value');
    var target = $(e.target).attr("href");
    var maincategory = target.slice(4);
    $.ajax({
        type: 'GET',
        url: '/Sale/_ItemListUpdateData/',
        data: { 'ItemName': ItemName, 'maincategory': maincategory },
        success: function (data) {
            if (data !== null && data.IsSuccess) {
                $('#divCE_'+maincategory+'').addClass('active show');
                $('#CartBodyListabc_' + maincategory).html(data.Data);              
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

$(document).on('click', '.btn-numbers', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    fieldName = $(this).attr('data-field');
    type = $(this).attr('data-type');
    var input1 = $("input[name='" + fieldName + "']");
    var input = $("#input-numbers_" + Id);
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        if (type == 'minus') {
            if (currentVal > input.attr('min')) {
                input.val(currentVal - 1).change();
                var quantity = $('#input-numbers_' + Id).val();
                var price = $('#SpnItemPrices_' + Id).text();
                var market = $('#MarketPrices_' + Id).text();
                var netprice = 0.0;
                if ($('#input-numbers_' + Id).val() == '') {
                    $('#actualPrices_' + Id).val(parseFloat(price));
                    $('#hdMarkets_' + Id).text(parseFloat(market));
                }
                else {
                    netprice = parseFloat(quantity * price).toFixed(2);
                    market = parseFloat(quantity * market).toFixed(2);
                    market = Math.round(market);
                    netprice = Math.round(netprice);
                    $('#actualPrices_' + Id).val(netprice);
                    $('#hdMarkets_' + Id).text(market);
                    $('#tempSearchAmount').text(parseFloat(netprice));
                }
            }
            if (parseInt(input.val()) == input.attr('min')) {
                $(this).attr('disabled', true);
            }
        } else if (type == 'plus') {

            if (currentVal < input.attr('max')) {
                input.val(currentVal + 1).change();
                var quantity = $('#input-numbers_' + Id).val();
                var price = $('#SpnItemPrices_' + Id).text();
                var market = $('#MarketPrices_' + Id).text();
                var netprice = 0.0;
                if ($('#input-numbers_' + Id).val() == '') {
                    $('#actualPrices_' + Id).val(parseFloat(price));
                    $('#hdMarkets_' + Id).text(parseFloat(market));
                }
                else {
                    netprice = parseFloat(quantity * price).toFixed(2);
                    market = parseFloat(quantity * market).toFixed(2);
                    market = Math.round(market);
                    netprice = Math.round(netprice);
                    $('#actualPrices_' + Id).val(netprice);
                    $('#hdMarkets_' + Id).text(market);
                    $('#tempSearchAmount').text(parseFloat(netprice));
                }
            }
            if (parseInt(input.val()) == input.attr('max')) {
                $(this).attr('disabled', true);
            }

        }
    } else {
        input.val(0);
    }
});

$(document).focusin('.input-numbers', function () {
    $(this).data('oldValue', $(this).val());
})

$(document).on('change', '.input-numbers', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    minValue = parseInt($(this).attr('min'));
    maxValue = parseInt($(this).attr('max'));
    valueCurrent = parseInt($(this).val());
    name = $(this).attr('name');
    if (valueCurrent >= minValue) {
        var quantity = $('#input-numbers_' + Id).val();
        var price = $('#SpnItemPrices_' + Id).text();
        var market = $('#MarketPrices_' + Id).text();
        var netprice = 0.0;
        if ($('#input-numbers_' + Id).val() == '') {
            $('#actualPrices_' + Id).val(parseFloat(price));
            $('#hdMarkets_' + Id).text(parseFloat(market));
        }
        else {
            netprice = parseFloat(quantity * price).toFixed(2);
            market = parseFloat(quantity * market).toFixed(2);
            market = Math.round(market);
            netprice = Math.round(netprice);
            $('#actualPrices_' + Id).val(netprice);
            $('#hdMarkets_' + Id).text(market);
            $('#tempSearchAmount').text(parseFloat(netprice));
        }
        $(".btn-numbers[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
    } else {
        alert('Sorry, the minimum value was reached');
        //$(this).val($(this).data('oldValue'));
        $(this).val(1);
        $('#actualPrices_' + Id).val("00.00");
    }
    if (valueCurrent <= maxValue) {
        var quantity = $('#input-numbers_' + Id).val();
        var price = $('#SpnItemPrices_' + Id).text();
        var market = $('#MarketPrices_' + Id).text();
        var netprice = 0.0;
        if ($('#input-numbers_' + Id).val() == '') {
            $('#actualPrices_' + Id).val(parseFloat(price));
            $('#hdMarkets_' + Id).text(parseFloat(market));
        }
        else {
            netprice = parseFloat(quantity * price).toFixed(2);
            market = parseFloat(quantity * market).toFixed(2);
            market = Math.round(market);
            netprice = Math.round(netprice);
            $('#actualPrices_' + Id).val(netprice);
            $('#hdMarkets_' + Id).text(market);
            $('#tempSearchAmount').text(parseFloat(netprice));
        }
        $(".btn-numbers[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
    } else {
        alert('Sorry, the maximum value was reached');
        //$(this).val($(this).data('oldValue'));
        $(this).val(1);
        $('#actualPrices_' + Id).val("00.00");
    }
});

$(document).on('click', '#AddCartIds', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    var ItemId = $(this).attr('data-Itemid');
    var Size = $(this).attr('data-size');
    var Measurment = $(this).attr('data-measurment');
    fieldName = $(this).attr('data-field');
    var input1 = $("input[name='" + fieldName + "']");
    var input = $("#input-numbers_" + ItemId);
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        var quantity = $('#input-numbers_' + ItemId).val();
        var price = $('#SpnItemPrices_' + Id).text();
        var market = $('#MarketPrices_' + Id).text();
        var netprice = 0.0;
        if ($('#input-numbers_' + ItemId).val() == '') {
            $('#actualPrices_' + Id).val(parseFloat(price));
            $('#hdMarkets_' + Id).text(parseFloat(market));
        }
        else {
            netprice = parseFloat(quantity * price).toFixed(2);
            market = parseFloat(quantity * market).toFixed(2);
            //market = Math.round(market);
            //netprice = Math.round(netprice);
            $('#actualPrices_' + Id).val(netprice);
            $('#hdMarkets_' + Id).text(market);
            $('#tempSearchAmount').text(parseFloat(netprice));
        }
    }
    var weightid = $('#priceweightIds_' + Id).val();
    var totakCartquant = $('#totalCartQuantity').text();
    var Amount = $('#AmountId').text();
    var amount1 = $('#amount1').val();
    var actualPrice = $('#actualPrices_' + Id).val();
    var quantity = $('#input-numbers_' + ItemId).val();
    var remark = $('#remarkIds_' + ItemId).val();
    var discount = $('#discountIds_' + ItemId).val();
    var weight = $('#SpnItemWeights_' + ItemId).text();
    var total = $('#TotalCartItemId').text();
    var pluName = $('#SpnItemNames_' + Id).text();
    var itemId = $('#SpnItemIds_' + ItemId).text();
    var priceId = $('#SpnItempriceIds_' + Id).text();
    var plucode = $('#spnpricePlucodes_' + Id).text();
    var coupenDisc = 1;
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var SalesId = $('#SalesOrderId').text();
    var TableId = $('#TableId').text()
    if (!$('#discountIds_' + ItemId).prop('value')) {
        discount = 0;
    }
    if (!$('#priceweightIds_' + Id).prop('value')) {
        $('#priceweightIds_' + Id).prop('value', weight);
    }
    if (!$('#input-numbers_' + ItemId).prop('value')) {
        quantity = 1;
    }
    if (!$('#remarkIds_' + ItemId).prop('value')) {
        remark = "No Remark";
    }
    var tableCount = $('#CartListbodyupdate').find('tr').length;
    var AddedDiscount = parseFloat(actualPrice) + parseFloat(discount);
    var dicData = {
        PriceId: Id,
        ItemId: ItemId,
        PluName: $('#SpnItemNames_' + Id).text(),
        Category: $('#SpnCategoryIds_' + ItemId).text(),
        Measurement: $('#pricemeaures_' + Id).text(),
        Weight: weight,
        SellingPrice: $('#SpnItemPrices_' + Id).text(),
        MarketPrice: $('#hdMarkets_' + Id).text(),
        CategoryName: $('#SpnCategorys_' + ItemId).text(),
        Quantity: quantity,
        Remark: remark,
        Discount: discount,
        ActualCost: actualPrice,
        ItemType: $('#MItemTypes_' + ItemId).text(),
        itemstock: $('#TotalItemstocks_' + ItemId).text(),
        ItemType: $('#MItemTypes_' + ItemId).text(),
        stockId: $('#stockIds_' + ItemId).text(),
        CreatedBy: $('#CreatedBy').text(),
        Purchaseprice: $('#PurchasePrices_' + Id).text(),
        PluCode: $('#spnPlucodes_' + ItemId).text(),
        CoupenDisc: coupenDisc,
        AddedDiscount: AddedDiscount,
        Id: Id,
        ItemStatus: "Pending",
        NetWeight: $('#priceweightIds_' + Id).val(),
        Discount: discount,
        ItemStatus: "Pending",
        CoupenDisc: coupenDisc,
        Tax_Value: $('#totalGst').text(),
        SalesListTableCoumt: tableCount,
        SalesId: $('#SalesOrderId').text(),
        CustomerId: $('#CustId').text(),
        TableId: $('#TableId').text(),
        Size:Size,
    };
    $('#' + dicData.PriceId).remove();
    $('#divId_' + dicData.PriceId).remove();
    $.ajax({
        url: '/Sale/_AddSalesList',
        type: 'POST',
        data: dicData,
        success: function (data) {
            if (data == true) {              
                swal({
                    text: "Item added Successfully",
                    type: "success",
                    confirmButtonText: "OK",
                    timer:500,
                }).then(() => {
                    var existing = $('#CartListbodyupdate').html();
                    var array = [];
                    array.push(existing);
                    array.push(data);
                    $('#CartListbodyupdate').html(array[0]);
                    var itemprice = "";
                    var firstbillamount = $('#billSubAmount').text();
                    var totalsalescountdata = $('#TotalsalesListCount').text();
                    var totalSaleCOst = 0;
                    $.makeArray($('#CartListbodyupdate tr[id]').map(function () {
                        var id = this.id;
                        if ($('#appendata_' + this.id).text() == 1) {
                            var exitingquantity = $('#spanquantity_' + id).text();
                            $('#quantity_' + this.id).val(exitingquantity);
                            var exitingweight = $('#spnNetweights_' + id).text();
                            $('#Netweight_' + this.id).val(exitingweight);
                            var exitingstatus = $('#spnItemstatus_' + id).text();
                            $('#Itemstatus_' + this.id).val(exitingstatus);
                            itemprice = $('#TotalPrice_' + id).text();
                            totalSaleCOst += parseFloat(itemprice);
                        }
                    }));
                    var currency = $('#currency').text();
                    var subamt = parseFloat(totalSaleCOst) + parseFloat(firstbillamount);
                    $('#billSubAmount').text(subamt);
                    //var discount = $('#billDiscountAmt').text().split('.')[1];
                    var maxdiscount = $('#MaxDiscount').text();
                    var discountper = $('#billDiscountAmt').text();
                    var coupendiscount = $('#Coupendisc').val() == "" ? 0 : $('#Coupendisc').val();
                    //discountper = parseFloat(coupendiscount) * parseFloat(totalSaleCOst) / 100;
                    //discountper = Math.round(discountper);
                    //discountper = discountper > maxdiscount ? maxdiscount : discountper;
                    var totalcombinediscount = discountper;
                    $('#billDiscountAmt').text(totalcombinediscount);
                    var deliveryCharge = $('#billDeliveryCharge').text();
                    var WallentAmount = $('#WallentAmount').text();
                    var TotalBillAmout = (parseFloat(subamt) - parseFloat(discountper)) + parseFloat(deliveryCharge);
                    var TotalAmout = (parseFloat(subamt) - parseFloat(discountper)) + parseFloat(deliveryCharge) - parseFloat(WallentAmount);
                    var FinalAmount = TotalAmout;
                    $('#billCalculationtAmt').text(TotalBillAmout);
                    $('#billFinalAmount').text(FinalAmount);
                    $('.clearitem').val('');
                    $('.clearitem').text('');
                    CoupenCode();
                    TotalofTable();
                    $(".itemUpdate").show();
                    $('.PdlistData').load('/Sale/_SaleDetailsProductListUpdateItemPanelData?id=' + SalesId);
                })
            }
        }
    });  
});

function CoupenCode() {
    var selectcoupen = $('#selectCoupen').val();
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    if (selectcoupen == "1") {
        $('#divDisAmount').css('display', 'none');
        $('#divCashDisAmount').css('display', 'block');
        $('#Cashamount').prop('value', 0);
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
        $.makeArray($('#CartListbodyupdate tr[id]').map(function () {
            if ($('#coupenDisc_' + this.id).text() == 1) {
                var ItemCoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithCoupen += parseFloat(ItemCoupenCost);
                var discountpercent = (parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2);
                var discountedPrice = (parseFloat(ItemCoupenCost) - parseFloat(discountpercent)).toFixed(2);
                $('#unitprice_' + this.id).text(discountedPrice);
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

function TotalofTable() {
    var totalSaleCOst = 0;
    var totalDiscount = 0;
    var totalCost = 0;
    var totalGST = 0;
    var deliverycharge = $('#billDeliveryCharge').text();
    var discount = $('#billDiscountAmt').text();
    var wallet = $('#WallentAmount').text();
    $.makeArray($('#CartListbodyupdate tr[id]').map(function () {
        var id = this.id;
        var itemprice = $('#TotalPrice_' + this.id).text();
        var addeddiscount = $('#AddedDiscount_' + this.id).text();
        var discount = $('#discount_' + this.id).text();
        totalDiscount += parseFloat(discount);
        totalCost += parseFloat(addeddiscount);
        totalSaleCOst += parseFloat(itemprice);
        var gstValue = $('#taxValue_' + this.id).text();
        totalGST += parseFloat(gstValue);
    }));
    //totalSaleCOst = Math.round(totalSaleCOst);
    $('#billDiscountAmt').text(parseFloat(totalDiscount).toFixed(2));
    $('#billSubAmount').text((((totalSaleCOst + parseFloat(totalDiscount)).toFixed(2))));
    $('#TotalCartDiscValue').text((totalSaleCOst));
    $('#billCalculationtAmt').text((parseFloat(totalSaleCOst) + parseFloat(deliverycharge)).toFixed(2));
    $('#billFinalAmount').text(Math.round(parseFloat(totalSaleCOst) + parseFloat(deliverycharge) - parseFloat(wallet)) + ".00");
    //$('#GSTValueId').text(totalGST);
}

$(document).on('click', '.removeSaleList', function (e) {
    e.preventDefault();
    var SalesOrderId = $('#SalesOrderId').text();
    var SalesListId = $(this).data('id');
    var KitchenListId = $(this).attr('data-ktid');
    $.ajax({
        url: '/Sale/_RemoveSalesKitchenList',
        type: 'POST',
        data: { SalesOrderId: SalesOrderId, SalesId: SalesListId, KitchenListId: KitchenListId },
        success: function (data) {
            if (data == true) {             
                swal({
                    text: 'Item Deleted Successfully',
                    type: 'success',
                    timer: 1000,
                    confirmButtonText: "OK"
                }).then(() => {
                    $('#' + SalesListId).remove();
                    CoupenCode();
                    TotalofTable();
                });                     
            }
            else {
                swal("Something went wrong!");
            }
        }
    });
});

$(document).on('click', '.pendingkot', function (e) {
    e.preventDefault();
    var SalesId = $(this)[0].dataset.id;
    $.ajax({
        url: '/Sale/_SaleDetailsTblStateListUpdateItemPanelData?Id=' + SalesId,
        type: 'GET',
        success: function (result) {
            $('.salesDetailsUpdatePanel').html(result);
            $('#openPendingKOT').modal({ backdrop: 'static', keyboard: false })
        }
    });
});

$(document).on('click', '#updateKOT', function () {
    getallSaelsOrerId();
    getallKitchenListId();
    getallSalesListId();
    var id = getallSaelsOrerId();
    var kitchenListId = getallKitchenListId();
    var salesListId = getallSalesListId();
    var salesId = id;
    var salesIds = salesId[0];
    var kitchenListIds = kitchenListId;
    var SalesListIds = salesListId;
    var status = 1;
    var Itemstatus = 'Ready';
    if (id.length > 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to add this item into KOT Print !',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Add it!'
        }).then((result) => {
            if (result.value) {
                var dicData = {
                    salesId: salesId,
                    kitChenListId: kitchenListIds,
                    salesListId: SalesListIds,
                    KOT_Status: status,
                    status: Itemstatus
                };
                $.ajax({
                    url: '/Sale/ApprovalAllKitchenKOTStatusUpdate/',
                    type: 'POST',
                    dataType: 'JSON',
                    data: dicData,
                    success: function (result) {
                        if (result.IsSuccess == true && result.Data != 0) {
                            window.location.href = "/Print/KOTPrint?id1=" + salesIds + "&id2=" + kitchenListId + "&remaining=" + null + "&saved=" + null + "&AmountTotal=" + null + "&quant=" + null + "&item=" + null + '&DiscountPer=' + null + '&TotalDiscountAmt=' + null + '&ActualDiscountAmt=' + null;
                        }
                        else {
                            $('.salesDetailsUpdatePanel').load('/Sale/_SaleDetailsTblStateListUpdateItemPanelData?id=' + salesId);
                        }
                    }
                });
            }
        });
    }
});

var getallSaelsOrerId = function () {
    return $('#KotListView').find('.SalesOrderId')
        .toArray()
        .map(function (x) {
            return $(x).attr('id');
        });
}

var getallKitchenListId = function () {
    return $('#KotListView').find('.KitchListId')
        .toArray()
        .map(function (x) {
            return $(x).attr('id');
        });
}

var getallSalesListId = function () {
    return $('#KotListView').find('.SalesListId')
        .toArray()
        .map(function (x) {
            return $(x).attr('id');
        });
}

window.onclick = function (event) {
    if (event.target.id != "image_in_modal_div") {
        $("#openPendingKOT").hide();
    }
}

$(document).on('click', '.removeSaleDisable', function () {
    swal('"Oops!", Your canot delete once KOT is printed', 'Contact your manager.');
});

function getEditActiveData() {
    var target = $('.Editcategorytabs a[data-toggle="tab"]').attr("href");
    $('#SearchItem').prop('value', "");
    var ItemName = $.selector_cache("#SearchItem").prop('value');
    var maincategory = target.slice(4);
    $.selector_cache('#activeTab').prop('value', target);
    $.ajax({
        type: 'GET',
        url: '/Sale/_ItemListUpdateData/',
        data: { 'ItemName': ItemName, 'maincategory': maincategory },
        success: function (data) {
            if (data !== null && data.IsSuccess) {
                $('#CartBodyListabc_' + maincategory).html(data.Data);
                $('#CartBodyListabc_' + maincategory).css('display', 'block');
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
}

$(document).ready(function () {
    $(".payment_mode li").on('click', function () {
        var status = $(this).text();
        if (status == 'Cash') {
            $('#display143').css('display', 'flex');
            $('#display15432').css('display', 'none');
            $('#display7975').css('display', 'none');
        } else if (status == 'Card Payment') {
            $('#display143').css('display', 'none');
            $('#display15432').css('display', 'flex');
        } else if (status == 'GPay') {
            $('#display143').css('display', 'none');
            $('#display15432').css('display', 'flex');
        } else if (status == 'PhonePe') {
            $('#display143').css('display', 'none');
            $('#display15432').css('display', 'flex');
        } else {
            $('#display143').css('display', 'none');
            $('#display15432').css('display', 'flex');
        }
    });  
    $("#value12").on('change', function () {
        var a = $('#valu143').val();
        var b = $('#value12').val();
        var c = b - a;
        var isNeg = c < 0;
        $('#valu134').val(isNeg ? Math.abs(c) : c);
    });
    $("#Card12").on('change', function () {
        var a = $('#cash12').val();
        var b = $('#Card12').val();
        var c = - a - b;
        var d = $('#valu143').val();
        var e = - d - c;
        var isNeg = e < 0;
        $('#valu79').val(isNeg ? Math.abs(e) : e);
    });
    $("#flexCheckChecked").on('click', function () {
        var check = $('input[name="Check[]"]:checked').length; 
        if (check == 1) {
            $('#display7975').css('display', 'flex');
        }
        else if (check == 0)  {
            $('#display7975').css('display', 'none');
        }
    });
});
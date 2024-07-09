function mymoney1() {
    if (SubmitError == 0) {
        if (!$('#OrderStatusId').val()) {
            var orderstatus = "Ordered";
        }
        else {
            orderstatus = $('#OrderStatusId').val();
        }
        var paymentStatus = $('#PaymentStatus').val();
        var DeliveryType = "HOD";
        var customerdata = "";
        var Address = "";
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        today = mm + '/' + dd + '/' + yyyy;
        var deliveryDate = today;
        var deliveryCharge = 0;
        var nErrorCount = 0;
        //if ($('#spncustid').text() == "") {
        //    customerdata = "NA";
        //}
        //else {
        //    customerdata = $('#spncustid').text();
        //}
        if ($('#spnaddress').text() == "") {
            Address = "NA";
        }
        else {
            Address = $('#spnaddress').text();
        }
        var drpsalesperson = "";
        var paymentmode = $('#PaymentMode').val();
        var deliverytime = "NA";
        var Branch = "HID01";
        var TotalAmount = $('#amount1').val();
        var CashReceived = $('#amount2').val();
        var CashRemaining = $('#total').text();
        var salesId = $('#HomeSalesId').val();
        var TotalCartItem = $('#TotalCartItemId').text();
        var TotalQuantity = $('#totalCartQuantity').text();
        var remark = $('#totalCartremark').text();
        var Amountspn = $('#Amountspn').text();
        var discountper = $('#Discountamount').val();
        var Totaldisamt = $('#DiscountTotalamt').val();
        var ActualDisc = $('#ActualDiscountAmt').val();
        var coupenID = $('#coupenIds').val();
        var DiscountedCash = $('#Cashamount').val();
        if ($('#PaymentMode').val() == 0) {
            nErrorCount++;
            $('#SpnPaymentMode').html("This field is required").css('display', 'block');
        }
        if ($('#PaymentStatus').val() == 0) {
            nErrorCount++;
            $('#SpnPaymentstatus').html("This field is required").css('display', 'block');
        }
        //if ($('#selecttableId').val() == null) {
        //    nErrorCount++;
        //    $('#SpnTableId').html("This field is required").css('display', 'block');
        //}
        if (nErrorCount === 0) {
            !$('#SearchDiscount').prop('value').trim()
            if ($('#TotalItemId').text() > 0 && salesId == "") {
                swal("Item selected for home delivery but Sale not created!", "", "error");
            }
            else {
                if (0 === nErrorCount) {
                    var totalCount = 0;
                    var unitCost = "";
                    var quantity = 0;
                    var weight;
                    var totalQuantity = $('#totalCartQuantity').text();
                    var totalCost = 0;
                    var category;
                    var stock = 0;
                    var measure;
                    var totalSaleCOst = 0.0;
                    var selltype;
                    var salesPerson = "";
                    var discount = 0;
                    var Hub = "NA";
                    var totalUnitCost = 0;
                    var homeDelivery = "";
                    var totalCount = 0;
                    var marketPrice = 0;
                    var totalmarketPrice = 0.0;
                    var savedPrice;
                    var ItemType = "";
                    var stocksid = 0;
                    var purchaseprice = 0;
                    var stockListss = new Array();
                    var stockData = {};
                    var itemDetails = $.makeArray($('#CartListbody tr[id]').map(function () {
                        var data = $('#delivery_' + this.id).text();                        
                        if (data == "no") {
                            try {
                                totalCount++;
                                ItemId = $('#ItemId_' + this.id).text();
                                unitCost = $('#unitprice_' + this.id).text();
                                quantity = $('#quantity_' + this.id).val();
                                remark = $('#remark_' + ItemId).val();
                                weight = $('#Netweight_' + this.id).val();
                                measure = $('#measure_' + this.id).text();
                                category = $('#cateId_' + ItemId).text();
                                totalCost = $('#actualPrice_' + this.id).text();
                                stock = $('#Totalitemstock_' + this.id).text();
                                marketPrice = $('#Market_' + this.id).text();
                                purchaseprice = $('#purchaseprice_' + this.id).text();
                                discount = $('#discountId_' + this.id).text();
                                homeDelivery = $('#delivery_' + this.id).text();
                                ItemType = $('#ItemType_' + ItemId).text();
                                stocksid = $('#stockId_' + ItemId).text();
                                stockData.unitCost = $('#unitprice_' + this.id).text();
                                stockListss.push(stockData);
                                if (ItemType == "Loose") {
                                    totalQuantity += parseFloat(weight);
                                }
                                else {
                                    totalQuantity += parseFloat(quantity);
                                }
                                totalSaleCOst += parseFloat(totalCost);
                                totalUnitCost += parseFloat(unitCost);
                                totalmarketPrice += parseFloat(marketPrice);
                            }
                            catch (e) {}
                            //return this.id + "_" + unitCost + "_" + quantity + "_" + remark + "_" + category + "_" + totalCost + "_" + weight + "_" + measure + "_" + stock + "_" + discount + "_" + stocksid + "_" + ItemType + "_" + purchaseprice + "|";
                            return ItemId + "_" + this.id + "_" + unitCost + "_" + quantity + "_" + remark + "_" + category + "_" + totalCost + "_" + weight + "_" + measure + "_" + stock + "_" + discount + "_" + stocksid + "_" + ItemType + "_" + purchaseprice + "|";
                        }
                    }));
                    var dicData = {
                        CustomerId: $('#cusdetailId').text(),
                        OrderdStatus: orderstatus,
                        SlotId: deliverytime,
                        PaymentStatus: paymentStatus,
                        PaymentMode: paymentmode,
                        AddressId: Address,
                        HubId: Hub,
                        Branch: Branch,
                        TotalPrice: totalSaleCOst,
                        SalesPerson: salesPerson,
                        TotalQuantity: $('#totalCartQuantity').text(),
                        Remark: remark,
                        PLU_Count: totalCount,
                        MultipleItem: itemDetails,
                        MultipleItem1: itemDetails,
                        DeliveryCharges: deliveryCharge,
                        Multiplestockrecord: itemDetails,
                        TotaldisAmt: Totaldisamt,
                        ActualDiscAmt: ActualDisc,
                        coupenId: coupenID,
                        DeliveryType: DeliveryType,
                        Remaining_Amount: $('#total').text(),
                        DiscountedCash: DiscountedCash,
                        tableId: $('#selecttableId').val()
                    };
                    $('#SavedPriceId').text(Math.round(parseFloat(totalmarketPrice).toFixed(2) - parseFloat(totalSaleCOst).toFixed(2)));
                    if (unitCost !== "" && totalQuantity !== "" && totalCost !== "" && totalSaleCOst !== "") {
                        $.ajax({
                            url: '/Sale/InsertSale',
                            type: 'POST',
                            dataType: 'JSON',
                            data: dicData,
                            success: function (data) {
                                var tempId = null;
                                if (data !== false) {
                                    if (data != null && tempId == null) {
                                        audio();
                                    }
                                    if (salesId == "") {
                                        setTimeout(function () {
                                            window.location.href = "/Print/Index?id1=" + data + "&received=" + CashReceived + "&remaining=" + CashRemaining + "&saved=" + $('#SavedPriceId').text() + "&AmountTotal=" + $('#AmountId').text() + "&quant=" + TotalQuantity + "&item=" + TotalCartItem + '&DiscountPer=' + discountper + '&TotalDiscountAmt=' + Totaldisamt + '&ActualDiscountAmt=' + $('#ActualDiscountAmt').val();
                                        }, 2000)
                                        window.location = "/Sale/Manage/";
                                    }
                                    else {
                                        setTimeout(function () {
                                            window.location.href = "/Print/Index?id1=" + data + "&id2=" + salesId + "&received=" + CashReceived + "&remaining=" + CashRemaining + "&saved=" + $('#SavedPriceId').text() + "&AmountTotal=" + $('#AmountId').text() + "&quant=" + TotalQuantity + "&item=" + TotalCartItem + '&DiscountPer=' + discountper + '&TotalDiscountAmt=' + Totaldisamt + '&ActualDiscountAmt=' + $('#ActualDiscountAmt').val();
                                        }, 2000)
                                        window.location = "/Sale/Manage/";
                                    }
                                }
                                else {
                                    swal('Oops!', 'Something went wrong!', 'warning');
                                }
                            }
                        });
                    }
                }
                else {
                    swal("No Item In Cart!", "", "warning");
                }
            }
        }
    }
}

$('#mymoney').on('click', function () {
    SubmitError = 0;
    if ($('#TotalItemId').text() > 0) {
        Submit();
    }
    else {
        if ($('#TotalCartItemId').text() > 0) {
            swal({
                title: "Are you sure you want to create sales order?",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes,create!"
            }).then(function (e) {
                if (e.value) {
                    mymoney1();
                }
            });
        } else {
            if ($('#CartListbody').text() === "") {
                swal("No item in list to create order", "", "warning");
            }
        }
    }
});

$('#amount2').on('keyup', function () {
    var qty = parseFloat($.selector_cache('#TDisAmountId').text());
    var price = parseFloat($.selector_cache('#amount2').val());
    var discountpercent = $('#DiscountTotalamt').val();
    var returnamt2 = $('#amount2').val();
    if ($.selector_cache('#amount2').val()) {
        var total = price - qty;
        var isNeg = Math.round(total) < 0;       
        $('#total').val(isNeg ? Math.abs(total) : total);
    }
    else {
        $('#total').val(qty);
    }    
    if ( qty < price ) {
        $('#total').removeClass("text-danger");
        $('#total').addClass("text-success");
    }
    else {
        $('#total').removeClass("text-success");
        $('#total').addClass("text-danger");
    }
});
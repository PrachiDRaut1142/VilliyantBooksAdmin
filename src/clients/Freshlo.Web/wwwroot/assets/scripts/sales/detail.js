$(function () {
    // get Previous Url
    $(document).ready(function () {
        var getPreviousurl = document.referrer;
        $('#getprevurl').val(getPreviousurl);

    });

    $(document).ready(function () {
        if ($('#OrderStatusId').val() == "Delivered") {
            $('#ChanePaymentStatus').prop('disabled', true);
        }
    });

    if ($('#OrderStatusId').val() == "Out for delivery" || $('#OrderStatusId').val() == "Dispatched") {
        $('#DeliveryPerson_li').css('display', 'block');
    }

    var totalPr = $('#totalPr').val();
    var discount = $('#discount').val();
    var deliveryChr = $('#deliveryChr').val();
    var walletAmount = $('#walletAmount').val();
    var TaxationPrice = $('#TaxationPrice').val();
    var gsttax = $('#CGSTValue').val();

    var data = (parseFloat(totalPr) + parseFloat(deliveryChr)).toFixed(2) - parseFloat(discount).toFixed(2)

    //var total = gsttax
    //alert(total)
    //totalPr = Math.round(totalPr);
    $('#billSubAmount').text((parseFloat(totalPr)));
    $('#billCalculationtAmt').text(data);
    $('#billFinalAmount').text((parseFloat(totalPr) + parseFloat(deliveryChr) - parseFloat(walletAmount)) + ".00");
    //$('#billFinalAmountDelivery').text(Math.round(parseFloat(TaxationPrice) - parseFloat(deliveryChr) - parseFloat(walletAmount)) + ".00");
    $('#billFinalAmountDelivery').text((parseFloat(TaxationPrice)) + ".00");
    //CoupenCode();
    //TotalofTable();
});

!(function () {
    $('#temp55').select2({
        containerCssClass: "select2-data-ajax",
        placeholder: 'Select Product',
        ajax: {
            url: '/Sale/ItemSelectList',
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
                        CoupenDisc: data.Data[i].coupenDisc,
                        Item_Id: data.Data[i].id,
                        Tax_Value: data.Data[i].tax_Value
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

    $.selector_cache('#temp55').on('change', function (e) {
        var test = $(this).select2('data');
        var parameter = test[0].id;
        var weight = test[0].weight;
        var item_Id = test[0].Item_Id;
        $('#SPurchaseprice').text(test[0].Purchaseprice);
        $('#SstockId').text(test[0].stockId);
        var weight = test[0].weight;
        var discountPrice = test[0].DiscountPerctg;
        $('#itemstock').text(test[0].itemstock);
        $('#SItemId').text(test[0].id);
        $('#SItem_Id').text(item_Id);
        $('#SCategoryName').text(test[0].CategoryName);
        $('#SCategoryId').text(test[0].category);
        $('#SItemName').text(test[0].text);
        $('#Smeaure').text(" " + test[0].measurement);
        $('#ShiddenWeight').val(test[0].weight);
        $('#SCoupenDisc').text(test[0].CoupenDisc);
        $('#STaxValue').text(test[0].Tax_Value);
        $('#totalGst').text(test[0].Tax_Value);
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
        if (test[0].itemType === "Out for delivery") {
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
        $.ajax({
            url: "/Sale/ItemvariantList?ItemId=" + parameter,
            type: "GET",
            success: function (itemvList) {
                if (itemvList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected disabled>Select Size</option>';
                    $.each(itemvList, function (index) {
                        html += '<option value="' + itemvList[index].priceId + '" data-sellingprice="' + itemvList[index].sellingPrice + '" data-purchasePrice="' + itemvList[index].purchasePrice + '"  data-MarketPrice="'+itemvList[index].marketPrice+'" data-ItemId="'+itemvList[index].itemId+'" data-id="'+itemvList[index].pId+'" >' + itemvList[index].size + '</option>';
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
            if (size == 'NOVAR' ) {
                $('#SpnItemSize').text('');
                $('#SpnItemPrice').text(sellingPrice);
                $('#actualPrice').val(sellingPrice);
                $('#MarketPrice').val(MarketPrice);
                $('#PurchasePrice').val(purchasePrice);     
                $('#tempSearchAmount').val(sellingPrice);     
                $('#SpnPriceId').text(PriceId);
            }
            else {
                $('#SpnItemSize').text(size);
                $('#SpnItemPrice').text(sellingPrice);
                $('#actualPrice').val(sellingPrice);   
                $('#MarketPrice').val(MarketPrice);
                $('#PurchasePrice').val(purchasePrice);
                $('#tempSearchAmount').val(sellingPrice);     
                $('#SpnPriceId').text(PriceId);
            }      
        }
    });

    $("#SearchQuantity").on('change', function () {
        $('#SearchDiscount').prop('value', '');
        var quantity = $('#SearchQuantity').val();
        var price = $('#Sprice').text();
        var marketPrice = $('#MarketPrice').text();
        var GSTValue = $('#STaxValue').text();
        var netprice = 0.0;
        if ($('#SearchQuantity').val() == '') {
            $('#SearchAmount').val(parseFloat(price));
            $('#ShdMarket').text(parseFloat(marketPrice));
            $('#totalGst').text(parseFloat(GSTValue));
        }
        else {
            netprice = parseFloat(quantity * price).toFixed(2);
            marketPrice = parseFloat(quantity * marketPrice).toFixed(2);
            marketPrice = Math.round(marketPrice);
            netprice = Math.round(netprice);
            GSTValue = parseFloat(quantity * GSTValue).toFixed(2);
            $('#SearchAmount').val(netprice);
            $('#ShdMarket').text(marketPrice);
            $('#tempSearchAmount').text(netprice);
            $('#totalGst').text(parseFloat(GSTValue));
        }
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
            if ($('#SItemtype').text() == 'Out for delivery') {
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

    $('#NetWeightId').on('change', function () {
        $('#SearchDiscount').prop('value', '');
        var weight = $('#ShiddenWeight').val();
        var sellingPrice = $('#Sprice').text();
        var weighing = $('#NetWeightId').val();
        var marketPrice = $('#MarketPrice').text();
        var GSTValue = $('#STaxValue').text();
        if ($('#NetWeightId').val() == '') {
            $('#SearchAmount').val(parseFloat(sellingPrice));
            $('#ShdMarket').text(parseFloat(marketPrice));
            $('#totalGst').text(parseFloat(GSTValue));
        }
        else {
            var price = parseFloat((sellingPrice * weighing) / weight).toFixed(2);
            marketPrice = parseFloat((marketPrice * weighing) / weight).toFixed(2);
            marketPrice = Math.round(marketPrice);
            price = Math.round(price);
            GSTValue = parseFloat((GSTValue * weighing) / weight).toFixed(2);
            $('#SearchAmount').val(parseFloat(price));
            $('#ShdMarket').text(parseFloat(marketPrice));
            $('#tempSearchAmount').text(price);
            $('#totalGst').text(parseFloat(GSTValue));
        }
    });

    $("#AddSearchId").on('click', function () {        
        $('#spnProductMsg').text('');
        var coupenDisc = $('#SCoupenDisc').text();
        var selectcoupen = $('#CoupenCodeId').val();
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var ItemCoupenCost = $('#SearchAmount').val();
        var discountpercent = ((parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2));
        //var TotalAmout = (parseFloat(subamt) - parseFloat(discountpercent));
        $('#SearchDiscount').val(discountpercent);
        var discount = $('#SearchDiscount').val();
        if ($('#NetWeightId').val() == "") {
            $('#NetWeightId').val($('#ShiddenWeight').val());
        }
        if ($('#SearchQuantity').val() == "") {
            $('#SearchQuantity').val(1);
        }
        if (!$('#SearchDiscount').prop('value').trim()) {
            discount = 0;
        }
        //var SalesId = $('#SalesOrderId').text();
        //var CustomerId = $('#CustomerIds').text();
        var ItemIdss = $('#SItemId').text();
        var TableId = $('#TableId').val();
        var tableCount = $('#tblsaleslist').find('tr').length;
        //alert(tableCount)
        if ($('#SearchAmount').prop('value').trim()) {
            var dicData = {                
                ItemId: $('#SItemId').text(),
                PluName: $('#SItemName').text(),
                Category: $('#SCategoryId').text(),
                Measurement: $('#Smeaure').text(),
                Weight: $('#ShiddenWeight').val(),
                SellingPrice: $('#Sprice').text(),
                CategoryName: $('#SCategoryName').text(),
                Quantity: $('#SearchQuantity').val(),
                NetWeight: $('#NetWeightId').val(),
                ItemStatus: "Pending",
                Discount: discount,
                ActualCost: parseFloat($('#SearchAmount').val()) - parseFloat(discount),
                ItemType: $('#SItemtype').text(),
                itemstock: $('#itemstock').text(),
                stockId: $('#SstockId').text(),
                Purchaseprice: $('#SPurchaseprice').text(),
                CoupenDisc: coupenDisc,
                Tax_Value: $('#totalGst').text(),
                SalesListTableCoumt: tableCount,
                //DeliveryDate: $('#spnDeliveryDate').text(),
                CustomerId:$('#CustId').text(),
                SalesId: $('#SalesOrderId').text(),
                ImagePath: "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/icon/" + ItemIdss + ".png",
                Id: $("#SItem_Id").text(),
                AddedDiscount: parseFloat($('#SearchAmount').val()) + parseFloat(discount),
                TableId: $('#TableId').val(),
                PriceId: $('#PriceId').text(),
            };
            $.ajax({
                url: '/Sale/_AddSalesList',
                type: 'POST',
                data: { itemsaleList: dicData/*, salesId: SalesId, customerId:CustomerId*/ },
                success: function (data) {
                    if (data == true) {
                        swal({
                            text: "Item added Successfully",
                            type: "success",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            var existing = $('#tblsaleslist').html();
                            //alert(existing);
                            var array = [];
                            array.push(existing);
                            array.push(data);
                            $('#tblsaleslist').html(array);
                            var itemprice = "";
                            var firstbillamount = $('#billSubAmount').text();
                            var totalsalescountdata = $('#TotalsalesListCount').text();
                            var totalSaleCOst = 0;
                            $.makeArray($('#tblsaleslist tr[id]').map(function () {
                                var id = this.id;
                                if ($('#appendata_' + this.id).text() == 1) {
                                    var exitingquantity = $('#spanquantity_' + id).text();
                                    $('#quantity_' + this.id).val(exitingquantity);
                                    var exitingweight = $('#spnNetweight_' + id).text();
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
                            $('#billCalculationtAmt').text((TotalBillAmout));
                            $('#billFinalAmount').text(FinalAmount);
                            $('.clearitem').val('');
                            $('.clearitem').text('');
                            CoupenCode();
                            TotalofTable();
                            window.location.reload();
                        })
                    }
                }
            });
        }
    });   

    $("#UpdateSalesOrder").on('click', function () {
        var count = $('#tblsaleslist tr').length;
        $('#editsalesListCount').val(count);
        var SalesId = $('#SalesId').text();
        $('#DiscountValue').prop('value',$('#billDiscountAmt').text());
        $('#TotalsalesOrderCost').val($('#TotalCartDiscValue').text());
        $('#SalesDetailformid').submit();
        //window.location.reload();
    });


$("#tblsaleslist").on('click', '.removeSaleList', function (e) {
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
            $('#' + SalesListId).remove();
            CoupenCode();
            TotalofTable();
            window.location.reload();
        }
        else {
            swal("Something went wrong!");
        }
    }
});
});

 $('#tblsaleslist').on('change', '.weight', function () {
        var weight = this.value;
        var trid = $(this).closest('tr').attr('id');
        var discount = $('#discount_' + trid).text();
        var SellingPrice = $('#PercePerItemId_' + trid).val();
        $('#spnNetweight_' + trid).text(weight);
        var Coupendiscount = $('#Coupendisc').val();
        if (Coupendiscount == "")Coupendiscount = 0;
        var NetWeight = this.value;
        var ItemActualWeight = $('#ItemWeight_' + trid).val();
        var price = parseFloat((SellingPrice * NetWeight) / ItemActualWeight).toFixed(2);
        $('#AddedDiscount_' + trid).text(price);
        //price = Math.round(price);
        if ($('#coupenDisc_' + trid).text() == 1) {
            var discountpercent = (parseFloat(Coupendiscount) * parseFloat(price) / 100).toFixed(2);
            var Calculatebydiscount = (parseFloat(price) - parseFloat(discountpercent)).toFixed(2);
            $('#TotalPrice_' + trid).text(Calculatebydiscount);
            $('#TotalPriceList_' + trid).val(Calculatebydiscount);
            $('#discount_' + trid).text(discountpercent);
            $('#DiscountListId_' + trid).val(discountpercent);
        }
        else {
            $('#TotalPrice_' + trid).text((parseFloat(price) + parseFloat(discount)).toFixed(2));
            $('#TotalPriceList_' + trid).val((parseFloat(price) + parseFloat(discount)).toFixed(2));
        }
        TotalofTable();
    });

 $('#tblsaleslist').on('change', '.Quantity', function () {
        var quantity = this.value;
        var trid = $(this).closest('tr').attr('id');
        var discount = $('#discount_' + trid).text();
        var Coupendiscount = $('#Coupendisc').val();
        if (Coupendiscount == "")Coupendiscount = 0;
        $('#spanquantity_' + trid).text(quantity);
        var quantity = this.value;
        var price = $('#PercePerItemId_' + trid).val();
        var netprice = parseFloat(quantity * price).toFixed(2);
        $('#AddedDiscount_' + trid).text(netprice);
        //netprice = Math.round(netprice);
        if ($('#coupenDisc_' + trid).text() == 1) {
            var discountpercent = (parseFloat(Coupendiscount) * parseFloat(netprice) / 100).toFixed(2);
            var Calculatebydiscount = (parseFloat(netprice) - parseFloat(discountpercent)).toFixed(2);
            $('#TotalPrice_' + trid).text(Calculatebydiscount);
            $('#TotalPriceList_' + trid).val(Calculatebydiscount);
            $('#discount_' + trid).text(discountpercent);
            $('#DiscountListId_' + trid).val(discountpercent);
        }
        else {
            $('#TotalPrice_' + trid).text((parseFloat(netprice) - parseFloat(discount)).toFixed(2));
            $('#TotalPriceList_' + trid).val((parseFloat(netprice) - parseFloat(discount)).toFixed(2));
        }        
        TotalofTable();
    });
  
 $('#AssignDeliveryPerson').on('click', '#BtnAssignDelivery', function () {
        var Empid = $('#DeliveryPersonId').val();
        var id = $('#sales-Id').val();
        var number = $('#CustomerNo').val();
        var customerName = $('#CustomerName').text();
        var numErr = 0;
        if ($('#DeliveryPersonId').prop('value') == null) {
            $('#DeliveryPersonErr').css('display', 'block');
            numErr++;
        }     
        var SalesOrderArray = {
            SalesPerson: $('#DeliveryPersonId').val(),
            OrderdStatus: "Out for delivery",
            SalesOrderId: $('#SalesOrderId1').val(),
            DeliveryNotes: $('#DeliveryNotes').val(),
            Id: id,
            ContactNo: number,
            CustomerName: customerName
        };
        if (numErr == 0) {
            $.ajax({
                url: '/Sale/AssignProductForDelivery',
                type: 'POST',
                data: SalesOrderArray,
                success: function (data) {
                    if (data.ReturnMessage == "Success") {
                        swal({
                            text: "Successfuly assign to delivery",
                            type: "success",
                            confirmButtonText: "OK"
                        });
                        $('#AssignDeliveryPerson').modal('hide');
                    }
                }
            });
        }        
    });

    $('#AssignAWBshipped').on('click', '#btnout', function () {
        var id = $('#sales-Id').val();
    
        var AWBNumber = $('#AWBShipped').val();       
        var AWBShippedlink = $('#AWBShippedlink').val();
        var numErr = 0;
        if (AWBNumber == null) {
            $('#AWErrorErr').css('display', 'block');
            numErr++;
        }
        if (AWBShippedlink == null) {
            $('#AWErrorErr').css('display', 'block');
            numErr++;
        }
        var SalesOrderArray = {
            OrderdStatus: "Shipped",
            SalesOrderId: $('#SalesOrderId12').val(),
            Id: id,
            AWBnumber: AWBNumber,           
            AWBshippedlink: AWBShippedlink,
        };
      
        if (numErr == 0) {
            $.ajax({
                url: '/Sale/AssignShipped',
                type: 'POST',
                data: SalesOrderArray,
                success: function (data) {
                    if (data.ReturnMessage == "Success") {
                        swal({
                            text: "Successfuly Added in AWB",
                            type: "success",
                            confirmButtonText: "OK"
                        });
                        $('#AssignAWBshipped').modal('hide');
                       
                    }
                    window.location.href = "/Sale/Detail?Id=" + SalesOrderArray.SalesOrderId
                }
            });
        }
    });

 $('#changeDeliveryDate').on('click', '#btnUpdateDeliveryDate', function () {
        var newdeliveryd = $('.newDeliveryDate').val();
        var ChangeDeliveryDateofProduct = {
            DeliveryDate: $('.newDeliveryDate').val(),
            SalesOrderId: $('#SalesOrderId').val(),
        };
        $.ajax({
            url: '/Sale/UpdateDeliveryDate',
            type: 'POST',
            data: ChangeDeliveryDateofProduct,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Delivery Date Change Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        location.reload();
                    })
                }
            }
        });
    });

 $('#updateDeliveryCharges').on('click', '#btnChangeDeliveryCharge', function () {
        var newdeliveryCharge = $('#DeliveryCharge').val();
        var ChangeDeliveryChargeofProduct = {
            DeliveryCharges: $('#DeliveryCharge').val(),
            SalesOrderId: $('.salesIds').val(),
        };
        $.ajax({
            url: '/Sale/UpdateDeliveryCharge',
            type: 'POST',
            data: ChangeDeliveryChargeofProduct,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Delivery Charge Change Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        location.reload();
                    })
                }
            }
        });
    });

 $('#applyCouponCode').on('click', '#btnCouponSubmit', function () {
          CoupenCode();
          TotalofTable();
          if ($('#CoupenId').val() != 0) {
              var couponId = $('#CoupenId').prop('value');
              var SalesOrderId = $('#SalesOrderId').text();
              var CustomerId = $('#CustId').text();
              var mainArray = [];
              var mainsubArray = [];
              var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
                  //alert(this.id);
                  var Array1 = {
                      'SalesId': SalesOrderId,
                      'CustomerId': CustomerId,
                      'Category': $('#Category_' + this.id).val(),
                      'ItemId': $('#ItemId_' + this.id).val(),
                      'PriceId': $('#PriceId_' + this.id).val(),
                      'Measurement': $('#Measurements_' + this.id).val(),
                      'QuantityValue': $('#quantity_' + this.id).val(),
                      'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
                      'TotalPrice': $('#TotalPriceList_' + this.id).val(),
                      'Discount': $('#DiscountListId_' + this.id).val(),
                      'Weight': $('#ItemWeight_' + this.id).val(),
                      'Status': $('#ItemStatus_' + this.id).val()
                  };
                  mainArray.push(Array1);
              }));
              var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
                  //alert(this.id);
                  var Array2 = {
                      'SalesOrderId': SalesOrderId,
                      'CustomerId': CustomerId,
                      'TableId': $('#Table_' + this.id).val(),
                      'ItemId': $('#ItemId_' + this.id).val(),
                      'PriceId': $('#PriceId_' + this.id).val(),
                      'TotalQty': $('#quantity_' + this.id).val(),
                      'Remark': $('#Remark_' + this.id).val(),
                      'Status': $('#ItemStatus_' + this.id).val(),
                  };
                  mainsubArray.push(Array2);
              }));
              $.ajax({
                  url: '/Sale/ApplyCoupen',
                  type: 'POST',
                  data: {
                      info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId
                  },
                  success: function (data) {
                      if (data.IsSuccess == true) {
                          window.location.reload();
                      }
                      else
                          swal.fire('Error!', 'Something went wrong.', 'error');
                  },
                  error: function (xhr, ajaxOptions, thrownError) {
                      swal.fire('Error!', 'There was some error. Try again later.', 'error');
                  }
              });
          }
    });

 $("#PaymentStatusId li").on('click',function () {
        var paymentstatus = $(this).attr('data-value');
        $('#PaymentStatusValue').val(paymentstatus);
        $('PaymentStatusValueSet').val("");
    });

$("#PaymentModeId li").on('click',function () {
        var paymentmode = $(this).attr('data-value');
        $('#PaymentModeValue').val(paymentmode);
        $('PaymentModeValueSet').val("");

    });

$('#changepaymentStatus').on('click', '#BtnPaymentStatus', function () {
        var PaymentStatusData = "";
        var a = $('#PaymentStatusValue').val();
        var b = $('#PaymentStatusValueSet').val();
        if (a == "") {
            PaymentStatusData = b;
        }
        else {
            PaymentStatusData = a;
        }
        var PaymentModeData = "";
        var c = $('#PaymentModeValue').val();
        var d = $('#PaymentModeValueSet').val();
        if (c == "") {
            PaymentModeData = d;
        }
        else {
            PaymentModeData = c;
        }
        var ChangePaymentStatusofProduct = {
            PaymentStatus: PaymentStatusData,
            SalesOrderId: $('#SalesOrderId').text(),
            CustomerId: $('#CustomerId').val(),
            PaymentMode: PaymentModeData
        };
        $.ajax({
            url: '/Sale/UpdatePaymentStatus',
            type: 'POST',
            data: ChangePaymentStatusofProduct,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Payment Status Change Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        location.reload();
                    })
                }
            }
        });
    });

 $('#Refresh').on('click', function () {
        var transactionId = $('#TransactionId').val();
        var Id = $('#SalesOrderId').val();
        $.ajax({
            type: 'Get',
            url: '/Sale/GetApiAsync?transactionId=' + transactionId,
            data: transactionId,
            success: function (result) {
                if (result == true) {
                    $(Document).ready(function () {
                        window.location.href = "/Sale/Detail?Id=" + Id
                        if (data.transactionId != "NA") {
                            $('#BtnPaymentStatus').attr('disabled', true);
                        }
                    });
                }
            }
        });
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
            //market = Math.round(market);
            //netprice = Math.round(netprice);
            $('#actualPrice').val(netprice);
            $('#hdMarket').text(market);
            $('#tempSearchAmount').text(parseFloat(netprice));
        }
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

 $('.btn-number').click(function (e) {
        e.preventDefault();
        fieldName = $(this).attr('data-field');
        type = $(this).attr('data-type');
        var input = $("input[name='" + fieldName + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if (type == 'minus') {
                if (currentVal > input.attr('min')) {
                    input.val(currentVal - 1).change();
                }
                if (parseInt(input.val()) == input.attr('min')) {
                    $(this).attr('disabled', true);
                }
            } else if (type == 'plus') {
                if (currentVal < input.attr('max')) {
                    input.val(currentVal + 1).change();
                }
                if (parseInt(input.val()) == input.attr('max')) {
                    $(this).attr('disabled', true);
                } 
            }
        } else {
            input.val(0);
        }
    });

 $('.input-number').focusin(function () {
        $(this).data('oldValue', $(this).val());
    });

 $('.input-number').change(function () {
        minValue = parseInt($(this).attr('min'));
        maxValue = parseInt($(this).attr('max'));
        valueCurrent = parseInt($(this).val());
        name = $(this).attr('name');
        if (valueCurrent >= minValue) {
            $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            alert('Sorry, the minimum value was reached');
            $(this).val($(this).data('oldValue'));
        }
        if (valueCurrent <= maxValue) {
            $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            alert('Sorry, the maximum value was reached');
            $(this).val($(this).data('oldValue'));
        }
    });

 $('#AddCartId').on('click', function () {
        var weightid = $('#weightId').val();
        var actualPrice = $('#actualPrice').val();
        var quantity = $('#quantityId').val();
        var remark = $('#remark').val();
        var discount = $('#discountId').val();
        var weight = $('#SpnItemWeight').text();
        var pluName = $('#SpnItemName').text();
        var itemId = $('#SpnItemId').text();
        var priceId = $('#SpnPriceId').text();
        var plucode = $('#spnPlucode').text();
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
        $('#spnProductMsg').text('');
        var coupenDisc = $('#SCoupenDisc').text();
        var selectcoupen = $('#CoupenCodeId').val();
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var ItemCoupenCost = $('#actualPrice').val();
        var discountpercent = ((parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2));
        var discount = discountpercent;
        var actualPrice = $('#actualPrice').val();
        var ItemIdss = $('#SpnItemId').text();
        var tableCount = $('#tblsaleslist').find('tr').length;
        var AddedDiscount = parseFloat(actualPrice) + parseFloat(discount);
        var TableId = $('#TableId').val();
        var dicData = {
            ItemId: $('#SpnItemId').text(),
            PluName: $('#SpnItemName').text(),
            Category: $('#SpnCategoryId').text(),
            Measurement: $('#meaure').text(),
            Weight: $('#weightId').val(),
            SellingPrice: $('#SpnItemPrice').text(),
            MarketPrice: $('#hdMarket').text(),
            CategoryName: $('#SpnCategory').text(),
            Quantity: quantity,
            Remark: remark,
            NetWeight: $('#weightId').val(),
            Discount: discount,
            ItemStatus: "Pending",
            ActualCost: parseFloat($('#actualPrice').val()) - parseFloat(discount),
            ItemType: $('#ItemType').text(),
            itemstock: $('#TotalItemstock').text(),
            ItemType: $('#MItemType').text(),
            stockId: $('#stockId').text(),
            CreatedBy: $('#CreatedBy').text(),
            Purchaseprice: $('#PurchasePrice').text(),
            PluCode: $('#spnPlucode').text(),
            CoupenDisc: coupenDisc,
            Tax_Value: $('#totalGst').text(),
            SalesListTableCoumt: tableCount,
            AddedDiscount: AddedDiscount,
            SalesId: $('#SalesOrderId').text(),
            CustomerId: $('#CustId').text(),
            ImagePath: "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/icon/" + ItemIdss + ".png",
            Id: $("#SItem_Id").text(),
            TableId: $('#TableId').val(),
            PriceId: priceId,
        };
        $('#' + dicData.ItemId).remove();
        $('#divId_' + dicData.ItemId).remove();
        $.ajax({
            url: '/Sale/_AddSalesList',
            type: 'POST',
            data: dicData,
            success: function (data) {
                if (data == true) {
                    swal({
                        text: "Item added Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        var existing = $('#tblsaleslist').html();
                        //alert(existing);
                        var array = [];
                        array.push(existing);
                        array.push(data);
                        $('#tblsaleslist').html(array);
                        var itemprice = "";
                        var firstbillamount = $('#billSubAmount').text();
                        var totalsalescountdata = $('#TotalsalesListCount').text();
                        var totalSaleCOst = 0;
                        $.makeArray($('#tblsaleslist tr[id]').map(function () {
                            var id = this.id;
                            if ($('#appendata_' + this.id).text() == 1) {
                                var exitingquantity = $('#spanquantity_' + id).text();
                                $('#quantity_' + this.id).val(exitingquantity);
                                var exitingweight = $('#spnNetweight_' + id).text();
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
                        $('#billCalculationtAmt').text((TotalBillAmout));
                        $('#billFinalAmount').text(FinalAmount);
                        $('.clearitem').val('');
                        $('.clearitem').text('');
                        CoupenCode();
                        TotalofTable();
                        location.reload();
                    })
                }
            }
        });
    });

    $('#PrintBill').on('click', function () {
        var transactionId = $('#TransactionId').val();
        var Id = $('#SalesOrderId').val();
        var Orderstatus = "Billing";
        $('#OrderStatusId').val(Orderstatus);
        window.location.href = "/Print/Index?id1=" + Id + "&Orderstatus=" + Orderstatus;
    })
})();

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
        if (itemtype === "Out for delivery") {
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
                if (data.itemDetails.itemType === "Out for delivery") {

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

function CoupenCode() {
    var selectcoupen = $('#CoupenCodeId').val();
    var coupen = selectcoupen.split(',');
    var coupendiscount = coupen[0];
    var coupenId = coupen[1];
    var maxdiscount = coupen[2];
    $('#Coupendisc').val(coupendiscount);
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    var DeliveryCharge = $('#billDeliveryCharge').text();
    var WallentAmount = $('#WallentAmount').text();
    $.makeArray($('#tblsaleslist tr[id]').map(function () {
        if ($('#coupenDisc_' + this.id).text() == 1) {
            var ItemCoupenCost = $('#AddedDiscount_' + this.id).text();
            ItemWithCoupen += parseFloat(ItemCoupenCost);
        }
        else {
            var ItemWithoutoupenCost = $('#AddedDiscount_' + this.id).text();
            ItemWithoutCoupen += parseFloat(ItemWithoutoupenCost);
        }
    }));
    if (selectcoupen != 0) {
         ItemWithCoupen = 0;
         ItemWithoutCoupen = 0;
        $.makeArray($('#tblsaleslist tr[id]').map(function () {
            var netWeight = $('#spnNetweight_' + this.id).text();
            var quantity = $('#spanquantity_' + this.id).text();
            var itemWeight = $('#ItemWeight_' + this.id).val();
            var pricePerMeasure = $('#PercePerItemId_' + this.id).val();
            var totalItem = (parseFloat(netWeight) * parseFloat(quantity) / parseFloat(itemWeight));
            if ($('#coupenDisc_' + this.id).text() == 1) {
                var ItemCoupenCost = parseFloat(totalItem) * parseFloat(pricePerMeasure);
                $('#AddedDiscount_' + this.id).text(ItemCoupenCost);
                var discountpercent = ((parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2));
                var discountedPrice = ((parseFloat(ItemCoupenCost) - parseFloat(discountpercent)).toFixed(2));
                $('#discount_' + this.id).text(discountpercent);
                $('#DiscountListId_' + this.id).val(discountpercent);
                $('#TotalPrice_' + this.id).text(discountedPrice);
                $('#TotalPriceList_' + this.id).val(discountedPrice);
                ItemWithCoupen += parseFloat(ItemCoupenCost);
            }
            else {
                var ItemWithoutoupenCost = parseFloat(totalItem) * parseFloat(pricePerMeasure);
                $('#AddedDiscount_' + this.id).text(ItemWithoutoupenCost);
                var discount = $('#discount_' + this.id).text();
                var discountedPrice = (parseFloat(ItemWithoutoupenCost) - parseFloat(discount)).toFixed(2);
                ItemWithoutCoupen += parseFloat(discountedPrice)
            }
        }));
        $('#ItemWithCoupen').text(ItemWithCoupen);
        $('#ItemWitoutCoupen').text(ItemWithoutCoupen);
        var customeramt = $('#ItemWithCoupen').text();
        var discountpercent = (parseFloat(coupendiscount) * parseFloat(customeramt) / 100).toFixed(2);
        //discountpercent = Math.round(discountpercent);
        discountpercent = parseFloat(discountpercent) > parseFloat(maxdiscount) ? parseFloat(maxdiscount) : parseFloat(discountpercent);
        var acualamtbydiscount = (parseFloat(customeramt) - parseFloat(discountpercent)).toFixed(2);
        // acualamtbydiscount = Math.round(acualamtbydiscount);
        var TotalWithCoupen = (parseFloat($('#ItemWitoutCoupen').text()) + parseFloat(acualamtbydiscount)).toFixed(2);
        //TotalWithCoupen = Math.round(TotalWithCoupen);
        var AmountWithoutWallet = parseFloat(TotalWithCoupen) + parseFloat(DeliveryCharge);
        AmountWithoutWallet = parseFloat(AmountWithoutWallet).toFixed(2);
        var TotalAmount = parseFloat(TotalWithCoupen) + parseFloat(DeliveryCharge) - parseFloat(WallentAmount);
        //TotalAmount = Math.round(TotalAmount);
        var combineamt = acualamtbydiscount;
        $('#TotalCartDiscValue').text(TotalWithCoupen);
        //alert(TotalWithCoupen);
        $('#billSubAmount').text(((parseFloat(TotalWithCoupen)) + parseFloat(discountpercent)).toFixed(2));
        $('#billCalculationtAmt').text(AmountWithoutWallet); 
        $('#billDiscountAmt').text(discountpercent);
        $('#billFinalAmount').text(TotalAmount);
        var text = $("#CoupenCodeId option:selected").text();
        var value = $("#CoupenCodeId option:selected").val();
        if (value == 0) $('#CoupenCode').text("");
        else $('#CoupenCode').text(text);
        $('#CoupenId').val(coupenId);
        $('#applyCouponCode').modal('hide');
    }
    if (ItemWithCoupen == 0) {
        swal.fire("warning", "No item in cart are allowed to coupen!", "warning");
        $("#CoupenCodeId option:first").attr('selected', 'selected');
        $('#CoupenCode').text('');
        $('#CoupenId').val(0);
    }   
}

function TotalofTable() {
    var totalSaleCOst = 0;
    var totalDiscount = 0;
    var totalCost = 0;
    var totalGST = 0;
    var deliverycharge = $('#billDeliveryCharge').text();
    var discount = $('#billDiscountAmt').text();
    var wallet = $('#WallentAmount').text();
    $.makeArray($('#tblsaleslist tr[id]').map(function () {
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
    $('#billFinalAmount').text((parseFloat(totalSaleCOst) + parseFloat(deliverycharge) - parseFloat(wallet))+".00");
    //$('#GSTValueId').text(totalGST);
}

function totalcostcalculation(Price) {
    var totalSaleCOst = 0;
    var discountprice = 0;
    var discountpercent = "";
    var acualamtbydiscount = "";
    var discount = $('#billDiscountAmt').text();
    var billSubAmount = $('#billSubAmount').text();
    var billDiscountAmt = $('#billDiscountAmt').text();
    var billDeliveryCharge = $('#billDeliveryCharge').text();
    var WallentAmount = $('#WallentAmount').text();
   (totalSaleCOst) = parseFloat(billSubAmount) + parseFloat(Price);
    //$.makeArray($('#tblsaleslist tr[id]').map(function () {
    //    var totalCost = $('#TotalPrice_' + this.id).text();
    //    totalSaleCOst += parseFloat(totalCost);
    //}));

    if (discount != 0) {
        $('#billSubAmount').text((totalSaleCOst));
        var totaldiscountamt = parseFloat(totalSaleCOst) - parseFloat(discount);
        var TotalBillAmout = parseFloat(totaldiscountamt) + parseFloat(billDeliveryCharge);
        var totaldelivery = parseFloat(totaldiscountamt) + parseFloat(billDeliveryCharge) - parseFloat(WallentAmount);
        $('#billFinalAmount').text(totaldelivery);
        $('#billCalculationtAmt').text((TotalBillAmout));
    }
    else {
        $('#billSubAmount').text((totalSaleCOst));
        var TotalBillAmout = parseFloat(totaldiscountamt) + parseFloat(billDeliveryCharge);
        var totalcalculateAmt = parseFloat(totalSaleCOst) + parseFloat(billDeliveryCharge) - parseFloat(WallentAmount);
        $('#billFinalAmount').text(totalcalculateAmt);
        $('#billCalculationtAmt').text((TotalBillAmout));
    }
}

$(document).on('click', '.removeSaleDisable', function () {
    swal('"Oops!", Your canot delete once KOT is printed', 'Contact your manager.');
});


$(function () {
    $('#PurchaseSubmit').on('click', function (e) {
        $('#PurchaseFormId').submit();
    });
    $('#TransprtChrg,#AgentCom,#OtherEx').on('change', function () {
        var subPrice = $('#billSubAmount').text();
        var transportChrg = $('#TransprtChrg').prop('value');
        var AgentCom = $('#AgentCom').prop('value');
        var OtherEx = $('#OtherEx').prop('value');
        var totalPay = parseFloat(subPrice) + parseFloat(transportChrg) + parseFloat(AgentCom) - parseFloat(OtherEx);
        $('#purchaseAmnt').text(totalPay);
    });
    $('#SearchQuantity').select2({
        containerCssClass: "select2-data-ajax",
        placeholder: 'Select Product',
        ajax: {
            url: '/Purchase/ItemSelectList',
            dataType: "json",
            delay: 250,
            data: function (e) {
                return {
                    name: e.term,
                    page: e.page,
                    purchaseId: $('#PurchaseId').val()
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
                        ProfitMargin: data.Data[i].profitMargin,
                        Approval: data.Data[i].approval
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
    $.selector_cache('#SearchQuantity').on('change', function (e) {
        var test = $(this).select2('data');
        var parameter = test[0].id;
        var name = test[0].text;
        var weight = test[0].weight;
        var measure = test[0].measurement;
        var purchasePrice = test[0].Purchaseprice;
        var sellingPrice = test[0].sellingPrice;
        var marketPrice = test[0].MarketPrice;
        var itemType = test[0].itemType;
        var weight = test[0].weight;
        var profitMargin = test[0].ProfitMargin;
        var approval = test[0].Approval;
        //////////////////////////////////
        if (itemType == "Packed") $('#spnProductMeas').text("1 " + measure + " X Rs. " + sellingPrice + "");
        else $('#spnProductMeas').text("" + weight + " " + measure + " X Rs.  " + sellingPrice + "");
        $('#measure').text("" + measure + "");
        $('#purchasePrice').prop('value', purchasePrice);
        $('#sellingPrice').prop('value', sellingPrice);
        $('#marketPrice').prop('value', marketPrice);
        $('#itemId').text(parameter);
        $('#productName').text(name);
        $('#profitPer').val(profitMargin);
        $('#itemWight').prop('value', weight);
        //////////////////////////////////
        if (approval == "Pending") {
            swal({
                title: "Item is Unapproved!",
                text: "Would u like to Approve it?",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes!"
            }).then(function (e) {
                if (e.value) {
                    $.ajax({
                        url: '/ItemMaster/ApprovetheItem',
                        type: 'POST',
                        dataType: "json",
                        data: {
                            "itemId": parameter,
                            "approval": 'Approved'
                        },
                        success: function (data) {
                            if (data.IsSuccess == true) {
                                swal.fire("Item Approved!", "", "success");
                            }
                            else swal.fire("error", "Fail to Approve!", "error");
                        }
                    });
                }
            });
        }
    });
    $.selector_cache('#addItemBtn').on('click', function () {
        var Error = 0;
        var id = $('#itemId').text();
        var measure = $('#measure').text();
        var purchasePrice = $('#purchasePrice').prop('value');
        var sellingPrice = $('#sellingPrice').prop('value');
        var marketPrice = $('#marketPrice').prop('value');
        var Product = $('#productName').text();
        var totalPurchase = $('#ttl_PurchaseCost').prop('value');
        var total_Qty = $("#ttl_Qty").prop('value');
        var weight = $('#itemWight').prop('value');
        var profitPer = $('#profitPer').val();
        var html = "";
        if (purchasePrice == "" || purchasePrice < 1) {
            $('#SpnpurchasePrice').css('display', 'block');
            Error++;
        }
        if (total_Qty == "" || total_Qty < 1) {
            $('#Spnttl_Qty').css('display', 'block');
            Error++;
        }
        if (totalPurchase == "" || totalPurchase < 1) {
            $('#Spnttl_PurchaseCost').css('display', 'block');
            Error++;
        }
        if (sellingPrice == "" || sellingPrice < 1) {
            $('#SpnsellingPrice').css('display', 'block');
            Error++;
        }
        if (id != "" && Error == 0) {
            $('#Itemtbl_Body').append('<tr id=' + id + '>' +
                '<td>' +
                '<a href="#" class="text-link">' + Product + '</a>' +
                '<input type="hidden" id="tblweight_' + id + '" value="' + weight + '" />' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + total_Qty + '" id="ttlQty_' + id + '" class="form-control m-input m-input--solid tblTtlQty" style="width:100px;">' +
                '</td>' +
                '<td id="measure_' + id + '">' + measure + '</td>' +
                '<td>' +
                '<input type="text" value="' + totalPurchase + '" id="ttlPurchase_' + id + '" class="form-control m-input m-input--solid tblTtlItemCost" style="width:100px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + purchasePrice + '" id="purPrice_' + id + '" class="form-control m-input m-input--solid tblPurchasePrice" style="width:100px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + sellingPrice + '" id="sellPrice_' + id + '"  class="form-control m-input m-input--solid tblSellingPrice" style="width:100px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + marketPrice + '" id="marPrice_' + id + '" class="form-control m-input m-input--solid" style="width:100px;">' +
                '<input type="hidden" value="' + profitPer + '" id="profitPer_' + id + '" class="form-control m-input m-input--solid tblProfitPer">' +
                '</td>' +
                '<td>' +
                '<a href="#" onclick=remove(\'' + id + '\')><i class="flaticon-delete text-danger" ></i></a>' +
                '</td>' +
                '</tr>');
            $('#ProductTblId').dataTable();
            $('.clearitem').val('');
            $('.clearitem').text('');
            TotalSum();
        }
    });
    $('#purchasePrice').on('change', function () {
        $('.error-empty-cls').css('display', 'none');
        var purchasePrice = $(this).val();
        var percentage = $('#profitPer').val();
        var sellingPrice = $('#sellingPrice').val();
        var PricePer = ((sellingPrice - purchasePrice) / purchasePrice) * 100;
        $('#profitPer').val(PricePer.toFixed(2));
        //var Percentvalue = (percentage / 100) * purchasePrice;
        //var addValue = (parseFloat(Percentvalue) + parseFloat(purchasePrice)).toFixed(2);
        //$('#sellingPrice').prop('value', Math.round(addValue));
    });
    function TotalSum() {
        var total_Qty = 0;
        var total_Purchase = 0;
        $.makeArray($('#Itemtbl_Body tr[id]').map(function () {
            var Qty = $('#ttlQty_' + this.id).val();
            var Purchase = $('#ttlPurchase_' + this.id).val();
            total_Qty += parseFloat(Qty);
            total_Purchase += parseFloat(Purchase);
        }));
        var items = $("table[id='ProductTblId'] > tbody > tr").length
        $('#TotalItem').val(items);
        $('#TotalQuantity').val(total_Qty);
        $('#TotalPrice').val(total_Purchase);
    }
    $('#SubmitPurchaseList').on('click', function () {
        TotalSum();
        var procurement = $('#procuremntId').prop('value');
        var hub = $('#hubId').prop('value');
        var TotalQuantity = $('#TotalQuantity').prop('value');
        var TotalPrice = $('#TotalPrice').prop('value');
        var TotaItem = $('#TotalItem').prop('value');
        var purchaseId = $('#PurchaseId').prop('value');
        //List
        var MapArr = [];
        $.each($('#Itemtbl_Body tr[id]').map(function () {
            var Array = {
                ItemId: this.id,
                Measurement: $('#measure_' + this.id).text(),
                TotalQuantity: $('#ttlQty_' + this.id).val(),
                Procured_POPrice: $('#ttlPurchase_' + this.id).val(),
                Purchase_Price: $('#purPrice_' + this.id).val(),
                Selling_Price: $('#sellPrice_' + this.id).val(),
                Market_Price: $('#marPrice_' + this.id).val(),
                Profit_Per: $('#profitPer_' + this.id).val()
            };
            MapArr.push(Array);
        }));
        var dicData = {
            Procurement_Type: procurement,
            Branch: hub,
            TotalQuantity: TotalQuantity,
            Total_Price: TotalPrice,
            Plu_Count: TotaItem,
            List: MapArr,
            PurchaseId: purchaseId
        };
        $.ajax({
            url: '/Purchase/UpdatePurchaseList',
            type: 'POST',
            dataType: 'JSON',
            data: dicData,
            success: function (data) {
                if (data.IsSuccess == true) {
                    location.href = "/Purchase/Detail/" + data.Data + "";
                }
                else {
                    swal.fire('Oops!', 'Something went wrong!', 'warning');
                }
            }
        });
    });
});
function remove(id) {
    $('#' + id).remove();
    $('#ProductTblId').dataTable();
    swal.fire('success', 'One item removed!', 'success');
}
$('#sellingPrice').on('change', function () {
    var sellingPrice = $(this).val();
    var purchasePrice = $('#purchasePrice').val();
    var PricePer = ((sellingPrice - purchasePrice) / purchasePrice) * 100;
    $('#profitPer').val(PricePer.toFixed(2));
});
//$('#profitPer').on('change', function () {
//    var percentage = $(this).val();
//    var purchasePrice = $('#purchasePrice').val();
//    var sellingPrice = $('#sellingPrice').val();
//    var Percentvalue = (percentage / 100) * purchasePrice;
//    var addValue = (parseFloat(Percentvalue) + parseFloat(purchasePrice)).toFixed(2);
//    $('#sellingPrice').prop('value', Math.round(addValue));
//});
//Finance
$('#financeCreate').on('click', function () {
    var ReferenceNo = $('#ReferenceNo').prop('value');
    if (ReferenceNo != "") {
        swal({
            title: "Already created Finance for PO",
            text: "Do you want to create it again?",
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes!"
        }).then(function (e) {
            var paymentType = 1;
            var referenceNo = $('#PurchaseId').prop('value');
            var paymentStatus = 1;
            var totalAmount = $('#purchaseAmnt').text();
            var paymentMode = 2;
            var Entry_Type = 2;
            var dicData = {
                Reference_No: referenceNo,
                Outward_Payment_Type: paymentType,
                Payment_Status: paymentStatus,
                Total_Amount: totalAmount,
                Payment_Mode: paymentMode,
                Entry_Type: Entry_Type
            };
            if (e.value) {
                $.ajax({
                    url: '/Purchase/CreateFinance',
                    type: 'POST',
                    dataType: 'JSON',
                    data: dicData,
                    success: function (data) {
                        if (data.IsSuccess == true && data.Data > 0) {
                            swal.fire("success", "Finance entry created!", "success");
                        }
                        else {
                            swal.fire('Oops!', 'Something went wrong!', 'warning');
                        }
                    }
                });
            }
        });
    }
    else {
        swal({
            title: "Create Finance Entry?",
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes!"
        }).then(function (e) {
            var paymentType = 1;
            var referenceNo = $('#PurchaseId').prop('value');
            var paymentStatus = 1;
            var totalAmount = $('#purchaseAmnt').text();
            var paymentMode = 2;
            var Entry_Type = 2;
            var dicData = {
                Reference_No: referenceNo,
                Outward_Payment_Type: paymentType,
                Payment_Status: paymentStatus,
                Total_Amount: totalAmount,
                Payment_Mode: paymentMode,
                Entry_Type: Entry_Type
            };
            if (e.value) {
                $.ajax({
                    url: '/Purchase/CreateFinance',
                    type: 'POST',
                    dataType: 'JSON',
                    data: dicData,
                    success: function (data) {
                        if (data.IsSuccess == true && data.Data > 0) {
                            swal.fire("success", "Finance entry created!", "success");
                        }
                        else {
                            swal.fire('Oops!', 'Something went wrong!', 'warning');
                        }
                    }
                });
            }
        });
    }
});
$('#PDFId').on('click', function () {
    var PurchaseId = $('#PurchaseId').prop('value');
    window.location.href = '/Purchase/PDf?purchaseId=' + PurchaseId;
});
$('#ttl_Qty').on('change', function () {
    $('.error-empty-cls').css('display', 'none');
    var ttl_Qty = $(this).val();
    var ttl_PurchaseCost = $('#ttl_PurchaseCost').prop('value');
    var weight = $('#itemWight').prop('value');
    var price = parseFloat(weight) * parseFloat(ttl_PurchaseCost) / parseFloat(ttl_Qty);
    $('#purchasePrice').prop('value', price.toFixed(2));
    var percentage = $('#profitPer').val();
    var sellingPrice = $('#sellingPrice').val();
    var PricePer = ((sellingPrice - price.toFixed(2)) / price.toFixed(2)) * 100;
    $('#profitPer').val(PricePer.toFixed(2));
});
$('#ttl_PurchaseCost').on('change', function () {
    $('.error-empty-cls').css('display', 'none');
    var ttl_Qty = $('#ttl_Qty').prop('value');
    var ttl_PurchaseCost = $(this).val();
    var weight = $('#itemWight').prop('value');
    var price = parseFloat(weight) * parseFloat(ttl_PurchaseCost) / parseFloat(ttl_Qty);
    $('#purchasePrice').prop('value', price.toFixed(2));
    var percentage = $('#profitPer').val();
    var sellingPrice = $('#sellingPrice').val();
    var PricePer = ((sellingPrice - price.toFixed(2)) / price.toFixed(2)) * 100;
    $('#profitPer').val(PricePer.toFixed(2));
});
$('#Itemtbl_Body').on('change', '.tblTtlQty', function () {
    id = $(this).closest('tr').attr('id');
    var ttl_Qty = $('#ttlQty_' + id).prop('value');
    var ttl_PurchaseCost = $('#ttlPurchase_' + id).prop('value');
    var weight = $('#tblweight_' + id).prop('value');
    var price = parseFloat(weight) * parseFloat(ttl_PurchaseCost) / parseFloat(ttl_Qty);
    $('#purPrice_' + id).prop('value', price.toFixed(2));
    var percentage = $('#profitPer_' + id).val();
    var sellingPrice = $('#sellPrice_' + id).val();
    var PricePer = ((sellingPrice - price.toFixed(2)) / price.toFixed(2)) * 100;
    $('#profitPer_' + id).val(PricePer.toFixed(2));
});
$('#Itemtbl_Body').on('change', '.tblTtlItemCost', function () {
    id = $(this).closest('tr').attr('id');
    var ttl_Qty = $('#ttlQty_' + id).prop('value');
    var ttl_PurchaseCost = $('#ttlPurchase_' + id).prop('value');
    var weight = $('#tblweight_' + id).prop('value');
    var price = parseFloat(weight) * parseFloat(ttl_PurchaseCost) / parseFloat(ttl_Qty);
    $('#purPrice_' + id).prop('value', price.toFixed(2));
    var percentage = $('#profitPer_' + id).val();
    var sellingPrice = $('#sellPrice_' + id).val();
    var PricePer = ((sellingPrice - price.toFixed(2)) / price.toFixed(2)) * 100;
    $('#profitPer_' + id).val(PricePer.toFixed(2));
});
$('#Itemtbl_Body').on('change', '.tblPurchasePrice', function () {
    id = $(this).closest('tr').attr('id');
    var purchasePrice = $('#purPrice_' + id).prop('value');
    var percentage = $('#profitPer_' + id).val();
    var sellingPrice = $('#sellPrice_' + id).prop('value');
    var PricePer = ((sellingPrice - purchasePrice) / purchasePrice) * 100;
    $('#profitPer_' + id).val(PricePer.toFixed(2));
});
$('#Itemtbl_Body').on('change', '.tblSellingPrice', function () {
    id = $(this).closest('tr').attr('id');
    var sellingPrice = $('#sellPrice_' + id).prop('value');
    var purchasePrice = $('#purPrice_' + id).prop('value');
    var PricePer = ((sellingPrice - purchasePrice) / purchasePrice) * 100;
    $('#profitPer_' + id).val(PricePer.toFixed(2));
});
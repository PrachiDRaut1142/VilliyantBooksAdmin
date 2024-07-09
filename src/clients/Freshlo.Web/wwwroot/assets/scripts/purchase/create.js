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
    if (itemType == "Packed") {
        $('#spnProductMeas').text(+weight + " " + measure + " X Rs. " + sellingPrice + "");
    }
    else { $('#spnProductMeas').text("" + weight + " " + measure + " X Rs.  " + sellingPrice + ""); }
    $('#measure').text("" + measure + "");
    $('#purchasePrice').prop('value', purchasePrice);
    $('#sellingPrice').prop('value', sellingPrice);
    $('#marketPrice').prop('value', marketPrice);
    $('#itemId').text(parameter);
    $('#productName').text(name);
    $('#profitPer').prop('value', profitMargin);
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
                            swal.fire("Item Approved", "", "success");
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
            '</td>' +
            '<td id="measure_' + id + '">' + measure + '</td>' +
            '<td>' +
            '<input type="text" value="' + total_Qty + '" id="ttlQty_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" value="' + totalPurchase + '" id="ttlPurchase_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" value="' + purchasePrice + '" id="purPrice_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" value="' + sellingPrice + '" id="sellPrice_' + id + '"  class="form-control m-input m-input--solid" style="width:150px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" value="' + marketPrice + '" id="marPrice_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
            '<input type="hidden" value="' + profitPer + '" id="profitPer_' + id + '" class="form-control m-input m-input--solid">' +
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

$('#btn_SaveCon').on('click', function () {
    //alert($('#wizadP').text());
    var wizard = $('#wizadP').text();
    var procurement = $('#procuremntId').prop('value');
    var hub = $("#hubId option:selected").text();
    var TotalQuantity = $('#TotalQuantity').prop('value');
    var TotalPrice = $('#TotalPrice').prop('value');
    var TotaItem = $('#TotalItem').prop('value');
    var vendorId = $("#vendorId option:selected").text();
    var percentage = parseFloat((10 / 100) * TotalPrice).toFixed(2);
    var PerAddvalue = (parseFloat(TotalPrice) + parseFloat(percentage)).toFixed(2);
    var PerMinusvalue = (parseFloat(TotalPrice) - parseFloat(percentage)).toFixed(2);
    if (wizard == 2) {
        $('#h_Procument').text(": " + procurement);
        $('#h_Hub').text(": " + hub);
        $('#h_TotalQty').text(": " + TotalQuantity);
        $('#h_PurchaseAmt').text(": ~ " + PerMinusvalue + " / " + PerAddvalue);
        $('#h_TotalItem').text(": " + TotaItem);
        $('#h_vendor').text(": " + vendorId);
    }
});

$('#btnBack').on('click', function () {
    //alert($('#wizadP').text());
});

function remove(id) {
    $('#' + id).remove();
    $('#ProductTblId').dataTable();
    TotalSum();
    swal.fire('success', 'One item removed!', 'success');
}

$('#submitId').on('click', function () {
    TotalSum();
    var procurement = $('#procuremntId').prop('value');
    var hub = $('#hubId').prop('value');
    var TotalQuantity = $('#TotalQuantity').prop('value');
    var TotalPrice = $('#TotalPrice').prop('value');
    var TotaItem = $('#TotalItem').prop('value');
    var vendorId = $("#vendorId").prop('value');
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
        Vendor: vendorId,
        List: MapArr
    };
    var List1 = MapArr
    $.ajax({
        url: '/Purchase/InsertPurchaseOrder',
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

$('#sellingPrice').on('change', function () {
    $('.error-empty-cls').css('display', 'none');
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
$(document).ready(function () {
    $("#AuditCheck_").on('click', function () {
        if ($(this).is(':checked')) {
            $('#AuditQuanity_').removeAttr('disabled');
            $('#Audittextarea_').removeAttr('disabled');
        } else {
            $('#AuditQuanity_').attr('disabled', true);
            $('#Audittextarea_').attr('disabled', true);
        }
        })
    $("#AuditQuanity_").on('change', function (e) {
        var quantity = $('#Auditquantity_').text();
        var price = $('#AuditQuanity_').val();
        var netprice = parseFloat(price-quantity).toFixed(2);
        $('#Audittextarea_').val(netprice);
    })   
});

function btnsavechanges(param) {
    var atLeastOneIsChecked = $('input[name="Check[]"]:checked').length;
    if (atLeastOneIsChecked > 0) {
        var checkboxdata = [];
        var inputElements = document.getElementsByClassName('messageCheckbox');
        for (var i = 0; inputElements[i]; ++i) {
            if (inputElements[i].checked) {
                checkedValue = inputElements[i].value;
                checkboxdata.push(checkedValue);
            }
        }
        for (var k = 0; k < checkboxdata.length;) {
            var Auditlist = new Array();
            $('#tblAudit tr').each(function () {          
                var info = {
                    AssetsId: $(this).closest("tr").prop('id'),
                    Quantity: $('#Auditquantity_' + this.id).text(),
                    AuditQuantity: $('#AuditQuanity_' + this.id).val(),
                    differance: $('#Auditdifferance_' + this.id).val(),
                    Remarks: $('#Audittextarea_' + this.id).val(),    
                }
                Auditlist.push(info);
                if (checkboxdata[k] === info.AssetsId) {
                    $.ajax({
                        url: '/Inventory/CreateAudit',
                        type: 'Post',
                        dataType: 'json',
                        data: info,
                        success: function (data) {
                        }
                    });
                    k++;
                }
            });
        }
        swal({
            title: "Successfully Audited!",
            type: "success",
            confirmButtonText: "Ok!"
        }).then(function (e) {
            if (e.value) {
                window.location.reload();
            }
        });
    }
    else
    {
     swal("Select Checkbox!", "", "warning");
    }
}


function AuitQuanity(param) {
    //var wastagequantity = $('#WastageQuan_' + param).val();
    //var TotalStock = $('#Totalstock_' + param).text();
    //var purchaseprice = $('#Purchaseprice_' + param).text();
    //var Totalwastagequan = $('#TotalWastagequan_' + param).text();
    //var Totalwasprice = $('#TotalWastagePrice_' + param).text();
    //var calucuwastagequan = parseFloat(wastagequantity) + parseFloat(Totalwastagequan);
    //var calculatedstock = parseFloat(TotalStock) - parseFloat(wastagequantity);
    //var wastageprice = parseFloat(wastagequantity) * parseFloat(purchaseprice);
    //var calcuwastagePrice = parseFloat(wastagequantity) * parseFloat(purchaseprice);
    //var totalwastageprice = parseFloat(Totalwasprice) + parseFloat(calcuwastagePrice);
    //var stockPo = parseFloat(calculatedstock) * parseFloat(purchaseprice);
    //$('#CalWastagequan_' + param).text(calucuwastagequan);
    //$('#CalWastagePrice_' + param).text(totalwastageprice);
    //$('#WasPurchase_' + param).text(wastageprice);
    //$('#Wasstock_' + param).text(calculatedstock);
    //$('#stockPo_' + param).text(stockPo);
    var quantity = $('#Auditquantity_' + param).text();
    var price = $('#AuditQuanity_' + param).val();
    var netprice = parseFloat(price - quantity).toFixed(2);
    $('#Auditdifferance_' + param).val(netprice);
}

function checkbox(param) {
    if ($(this).is(':checked')) {
        $('#AuditQuanity_' +param).removeAttr('disabled');
        $('#Audittextarea_' +param).removeAttr('disabled');
    } else {
        $('#AuditQuanity_' +param).attr('disabled', false);
        $('#Audittextarea_' +param).attr('disabled', false);
    }
}

$('#Inventorylogs').on('shown.bs.tab', function () {
    InventoryLogs();
});
function InventoryLogs() {
    $.ajax({
        url: '/Inventory/_InventoryLogs/',
        type: 'Get',
        success: function (result) {
            if (result != "") {
                $('#packed_divId').html(result);
                $('#inventoryLog').addClass('active show');
            }
            else {
                swal.fire("error", "Something went wrong", "error");
            }
        }, error: function (result) { }
    });
}

$('#Inventorylogs').on('click', function () {
    AuditLogs();
});
function AuditLogs() {
    $.ajax({
        url: '/Inventory/_AuditLogs/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#pending_divId').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
        }, error: function (result) {}
    });
}

$("#Inventory").on('change', function (e) {
    var AssetsId = $(this).val();
    var oldquantity = $("#Inventory").find(':selected').attr('data-quantity');
    var quantity = $("#Inventory").find(':selected').attr('data-quantity');
    var price = $("#Inventory").find(':selected').attr('data-price');
    $('#assetId').val(AssetsId);
    $('#lastQuantiity1').text(oldquantity);
    $('#lastQuantiity').val(quantity);
    $('#unitprices').val(price);
});

$("#AssetUnitPrice").on('change', function (e) {
    var quantity = $('#QuantityAd').val();
    var price = $('#AssetUnitPrice').val();
    var netprice = parseFloat(quantity * price).toFixed(2);
    $('#AssetUnitAd').val(netprice);
})

$("#SubmitAdhoc").on('click', function () {
    //var EntryType = $(this)[0].dataset.Id;
    //if (EntryType == 1) {
    //    var Quantity = $('#Quantity').val();
    //    var quantityplus = $('#QuantityAd').val();
    //    var Total = parseFloat(Quantity + quantityplus).toFixed();
    //    $('#QuantityAd').val(Total);
    //}
    //else
    //{

    //    $('#QuantityAd').val(Total);
    //}
    var numError = 0;
    var AssetUnitPrice = $('#AssetUnitPrice').val();
    var QuantityAd = $('#QuantityAd').val();
    if (AssetUnitPrice == '') {
        numError++;
        $('#AssetUnitPriceErr').css('display', 'block');
    }
    if (QuantityAd == '') {
        numError++;
        $('#QuantityAdErr').css('display', 'block');
    }

    if ($('#EntryId').prop('value').trim() == "0") {
        numError++;
        $('#EntryIdErr').css('display', 'block');
    } else {
        $('#EntryIdErr').css('display', 'none');
    }

    if ($('#Inventory').prop('value').trim() == "0") {
        numError++;
        $('#InventoryError').css('display', 'block');
    } else {
        $('#InventoryError').css('display', 'none');
    }

    if (numError == 0) {
        swal.fire({
            title: 'Are you sure?',
            text: 'Create Inventory!',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var ItemId = $('#Inventory').prop('value');
            var AssetsId = $('#Inventory').prop('value');
            var EntryType = $("#EntryId").find(':selected').val();
            var AssetsUnitPrice = $("#AssetUnitPrice").prop('value');
            var AssetUnitAd = $("#AssetUnitAd").prop('value');
            var Quantity = $('#QuantityAd').prop('value');
            var Remarks = $("#RemarksId").prop('value');
            if (result.value) {
                var info = {
                    ItemId: ItemId,
                    AssetsId: AssetsId,
                    AssetsUnitPrice: AssetsUnitPrice,
                    AssetUnitAd: AssetUnitAd,
                    Quantity: Quantity,
                    EntryType: EntryType,
                    Remarks: Remarks,
                }
                if (AssetsId != "")
                    $.ajax({
                        url: '/Inventory/CreateAdhoc/',
                        type: 'POST',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#AdhocCreateid').modal('hide');
                                swal.fire({
                                    text: "Inventory Adhoc create Successfully",
                                    type: 'Success',
                                    confirmButtonText: "OK",
                                }).then(function (e) {
                                    InventoryLogs();
                                })
                            }
                            else {
                                $('#AdhocCreateid').modal('hide');
                                swal.fire({
                                    text: "Soomething Went Wrongs",
                                    type: "error",
                                    confirmButtonText: "OK",
                                }).then(function (e) {
                                    InventoryLogs();
                                })
                            }
                        }
                    });
            }
        })
    }
})

$("#AddHoc").on('click', function () {
    $("#EntryId option[value=0]").prop('selected', true);
    $("#Inventory option[value=0]").prop('selected', true);
    $('#Quantity').val("");
    $('#AssetUnit').val("");
    $('#RemarksId').val("");
    $('#AdhocCreateid').modal('show');
});
//$("#Quantity").keypress(function () {
//    if ($('#AssetName').text() == '') {
//        var quantity = $('#Quantity').val();
//        var netprice = parseFloat(quantity * 20).toFixed(2);
//        $('#unitprices').val(netprice);
//    }
//})
$("#SubmitAdhoc").on('click', function () {
    var EntryType = $(this)[0].dataset.EntryType; SubmitAdhoc
    if (EntryType == 1) {
        var Quantity = $('#Quantity').val();
        var quantityplus = $('#QuantityAd').val();
        var Total = parseFloat(1 + quantityplus).toFixed();
        $('#Quantity').val(Total);
    }
    else {
        var Total = parseFloat(1 - quantityplus).toFixed();
        $('#Quantity').val(Total);
    }
});
$("#pending_divId").on('click', '#auditlist', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Inventory/GetAuditlist?id=' + id,
        type: 'Get',
        success: function (data) {
            $('#AuditItemName').val(data.assetName);
            $('#Quantity').val(data.quantity);
            $('#AuditQuantity').val(data.auditQuantity);
            $('#Auditdate').val(data.createdDate);
            $('#Auditby').val(data.createdBy);
            $('#Remarks').val(data.remarks);
            $('#lastQuantiity123').text(data.auditQuantity - data.quantity);
            $('#AuditLogsid').modal('show');
        }
    });
});

//$('#AssetName').on('keydown', function (e) {
//    if ($('#AssetName').prop('value').length = "") {
//        $('#AssetNameErr').css('display', 'block');
//    }
//    else {
//        $('#AssetNameErr').css('display', 'none');
//    }
//});
//$('#Quantity').on('keydown', function (e) {
//    if ($('#Quantity').prop('value').length = "") {
//        $('#QuantityErr').css('display', 'block');
//    }
//    else {
//        $('#QuantityErr').css('display', 'none');
//    }
//});
//$('#AssetUnit').on('keydown', function (e) {
//    if ($('#AssetUnit').prop('value').length = "") {
//        $('#AssetUnitErr').css('display', 'block');
//    }
//    else {
//        $('#AssetUnitErr').css('display', 'none');
//    }
//});
//$('#AssetLife').on('keydown', function (e) {
//    if ($('#AssetLife').prop('value').length = "") {
//        $('#AssetLifeErr').css('display', 'block');
//    }
//    else {
//        $('#AssetLifeErr').css('display', 'none');
//    }
//});
//$('#Serviceable').on('change', function (e) {
//    if ($('#Serviceable').prop('value').trim() == "0") {
//        $('#ServiceableErr').css('display', 'block');
//    } else {
//        $('#ServiceableErr').css('display', 'none');
//    }
//});
//$('#EntryId').on('change', function (e) {
//    if ($('#EntryId').prop('value').trim() == "0") {
//        $('#EntryIdErr').css('display', 'block');
//    } else {
//        $('#EntryIdErr').css('display', 'none');
//    }
//}); $('#Inventory').on('change', function (e) {
//    if ($('#Inventory').prop('value').trim() == "0") {
//        $('#InventoryError').css('display', 'block');
//    } else {
//        $('#InventoryError').css('display', 'none');
//    }
//});
//$('#AssetUnitPrice').on('keydown', function (e) {
//    if ($('#AssetUnitPrice').prop('value').length = "") {
//        $('#AssetUnitPriceErr').css('display', 'block');
//    }
//    else {
//        $('#AssetUnitPriceErr').css('display', 'none');
//    }
//});
//$('#QuantityAd').on('keydown', function (e) {
//    if ($('#QuantityAd').prop('value').length = "") {
//        $('#QuantityAdErr').css('display', 'block');
//    }
//    else {
//        $('#QuantityAdErr').css('display', 'none');
//    }
//});

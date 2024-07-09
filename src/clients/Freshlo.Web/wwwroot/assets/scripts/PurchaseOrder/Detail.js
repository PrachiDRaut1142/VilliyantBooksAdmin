$('document').ready(function () {
    try {
        var PurchaseIds = $('.PurchaseId').text();
        window.location.href = '/PurchaseOrder/DownloadPDF?Id=' + PurchaseIds;
    } catch (e) {
    }
});

$(function () {
    $('#TransprtChrg,#AgentCom,#OtherEx,#GST').on('change', function () {
        var subPrice = $('#billSubAmount').text();
        var transportChrg = $('#TransprtChrg').prop('value');
        var AgentCom = $('#AgentCom').prop('value');
        var OtherEx = $('#OtherEx').prop('value');
        var Gst = $('#GST').prop('value');
        var totalPay = parseFloat(subPrice) + parseFloat(transportChrg) + parseFloat(AgentCom) + parseFloat(Gst) - parseFloat(OtherEx);
        $('#purchaseAmnt').text(totalPay);
    });
    $('.DetailItemList').on('shown.bs.tab', function () {
        $('#m_portlet_loader').css('display', 'block');
        var Id = $('#Ids').val();
        $.ajax({
            url: '/PurchaseOrder/_AppendPurchaseDetail/?id=' + Id,
            type: 'GET',
            success: function (data) {
                $('#Itemtbl_Body').html(data);
                $('#ProductTblId').DataTable();
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    });
    $('#PurchaseSubmit').on('click', function (e) {
        $('#PurchaseFormId').submit();
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
        var categoryName = test[0].CategoryName;
        var itemstock = test[0].itemstock;
        var Category = test[0].category
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
        $('#categoryName').prop('value', categoryName);
        $('#Category').prop('value', Category);
        $('#itemstock').prop('value', itemstock);
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
        var id = $('#itemId').text();
        var purchasePrice = $('#purchasePrice').prop('value');
        var Product = $('#productName').text();
        var total_Qty = $("#ttl_Qty").prop('value');
        var categoryName = $('#categoryName').prop('value');
        var Category = $('#Category').prop('value');
        var itemstock = $('#itemstock').prop('value');
        var html = "";
        if (id != "") {
            $('#Itemtbl_Body').append('<tr id=' + id + '>' +
                '<td>' +
                '<div class= "kt-checkbox-list">' +
                 '<label class="kt-checkbox">' + 
                '<input class="messageCheckbox" id="messageCheckbox_' + id + '"  value="" type="checkbox" name="Check">' +
                        '<span></span>' +
                          '</label>' +
                  '</div>' +
                  '</td >' +
                '<td id="pluName_' + id + '">' +
                '<a href="#" class="text-link">' + Product + '</a>' +
                '</td>' +
                '<td id="categoryName_' + id + '">' + categoryName + '</td>' +
                '<td>' +
                '<input type="text" value="' + purchasePrice + '" id="purPrice_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + total_Qty + '" id="ttlQty_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + itemstock + '" id="itemstock_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
                '</td>' +
                '<td>' +
                '<a href="#" onclick=remove(\'' + id + '\')><i class="flaticon-delete text-danger" ></i></a>' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + categoryName + '" id="categoryName_' + id + '" class="form-control m-input m-input--solid" style="width:150px;display:none">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + Category + '" id="Category_' + id + '" class="form-control m-input m-input--solid" style="width:150px;display:none">' +
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
        $.makeArray($('#Itemtbl_Body tr[id]').map(function () {
            var Qty = $('#ttlQty_' + this.id).val();
            total_Qty += parseFloat(Qty);
        }));
        var items = $("table[id='ProductTblId'] > tbody > tr").length
        $('#TotalItem').val(items);
        $('#TotalQuantity').val(total_Qty);
    }
    $('#SubmitPurchaseList').on('click', function () {
        TotalSum();
        var procurement = $('#procuremntId').prop('value');
        var hub = $('#hubId').prop('value');
        var TotalQuantity = $('#TotalQuantity').prop('value');
        var TotalPrice = $('#TotalPrice').prop('value');
        var TotaItem = $('#TotalItem').prop('value');
        var purchaseId = $('#PurchaseId').prop('value');
        var vendorId = $("#vendorId").prop('value');
        //List
        var MapArr = [];
        $.each($('#Itemtbl_Body tr[id]').map(function () {
            var Array = {
                ItemId: this.id,
                pluName: $('#pluName_' + this.id).text(),
                categoryName: $('#categoryName_' + this.id).text(),
                Category: $('#Category_' + this.id).val(),
                TotalQuantity: $('#ttlQty_' + this.id).val(),
                purchasePrice: $('#purPrice_' + this.id).val(),
                currentsock: $('#itemstock_' + this.id).val()
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
            url: '/PurchaseOrder/UpdatePurchaseList',
            type: 'POST',
            dataType: 'JSON',
            data: dicData,
            success: function (data) {
                if (data.IsSuccess == true) {
                    location.href = "/PurchaseOrder/Detail/" + data.Data + "";

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

$('#PDFId').on('click', function () {
    var PurchaseIds = $('.PurchaseId').text();
    window.location.href = '/PurchaseOrder/DownloadPDF?Id=' + PurchaseIds;
});

$('.filter').on('click', function () {
    $('#m_portlet_loader').css('display', 'block');
    var tab = $(this)[0].id;
    var MainCategoryId = $.selector_cache("#" + tab + "_MainCategoryId").prop('value');
    var CategoryId = $.selector_cache("#" + tab + "_CategoryId").prop('value');
    var VendorId = $.selector_cache("#" + tab + "_VendorId").prop('value');
    var Id = $('#Ids').val();
    var info = {
        MainCategoryId: MainCategoryId,
        CategoryId: CategoryId,
        VendorId: VendorId,
    }
    $.ajax({
            url: '/PurchaseOrder/_AppendPurchaseDetail/?id=' + Id,
            type: 'GET',
            data: info,
            success: function (data) {
                $('#ProductTblId').DataTable().destroy();
                $('#Itemtbl_Body').html(data);
                $('#m_portlet_loader').css('display', 'none');
            }
        });
});

$('.clear').on('click', function () {
    $('#m_portlet_loader').css('display', 'block');
    var tab = $(this)[0].id;
    $("#" + tab + "_MainCategoryId option[value=0]").prop('selected', true);
    $("#" + tab + "_CategoryId option[value=0]").prop('selected', true);
    $("#" + tab + "_VendorId option[value=0]").prop('selected', true);
    var MainCategoryId = $.selector_cache("#_MainCategoryId ").prop('value');
    var CategoryId = $.selector_cache("#_CategoryId").prop('value');
    var VendorId = $.selector_cache("#" + tab + "_VendorId").prop('value');
    var Id = $('#Ids').val();
    var info = {
        MainCategoryId: MainCategoryId,
        CategoryId: CategoryId,
        VendorId: VendorId,
    }
    $('#m_portlet_loader').css('display', 'block');
    var Id = $('#Ids').val();
    $.ajax({
        url: '/PurchaseOrder/_AppendPurchaseDetail/?id=' + Id,
        type: 'GET',
        success: function (data) {
            $('#Itemtbl_Body').html(data);
            $('#ProductTblId').DataTable();
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});

$('#ProductTblId').on('change', '#selectallid', function (e) {
    var table = $(e.target).closest('table');
    $('td input:checkbox', table).prop('checked', this.checked);
});

var getCheckedall = function () {
    return $('#Itemtbl_Body').find('input[type="checkbox"]')
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
                    url: '/PurchaseOrder/DeletePurchaseList?id=' + id,
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

$('#Transferpo').on('click', function () {
    var id = getCheckedall();
    var totalitems = id.length;
    $('#totalitems').text("" + totalitems);
    var total_Qty = 0;
    for (var i = 0; i < id.length; i++) {
        var Qty = $('#ttlQty_' + id[i]).val();
        total_Qty += parseFloat(Qty);
    }
    $('#TotalQuantitys').val(total_Qty);
});

$('#myModal').on('click', '#CreatNewPo', function () {
    var procurement = $('#procuremntId').prop('value');
    var hub = $('#hubId').prop('value');
    var TotalQuantity = $('#TotalQuantitys').prop('value');
    var TotaItem = $('#totalitems').text();
    var vendorId = $("#vendorIdS").prop('value');
    var MapArr = [];
    var id = getCheckedall();
        for (var i = 0; i < id.length; i++) {
            var Array = {
                ItemId: id[i],
                pluName: $('#pluName_' + id[i]).text(),
                categoryName: $('#categoryName_' + id[i]).text(),
                Category: $('#Category_' + id[i]).val(),
                TotalQuantity: $('#ttlQty_' + id[i]).val(),
                purchasePrice: $('#purPrice_' + id[i]).val(),
                currentsock: $('#itemstock_' + id[i]).val()
            };
            MapArr.push(Array);
        }       
    var dicData = {
        Procurement_Type: procurement,
        Branch: hub,
        TotalQuantity: TotalQuantity,
        Plu_Count: TotaItem,
        Vendor: vendorId,
        List: MapArr
    };
    var List1 = MapArr
    $.ajax({
        url: '/PurchaseOrder/InsertPurchaseOrder',
        type: 'POST',
        dataType: 'JSON',
        data: dicData,
        success: function (data) {
            if (data.IsSuccess == true) {
                window.location.href = "/PurchaseOrder/Detail/" + data.Data + "";
            }
            else {
                swal.fire('Oops!', 'Something went wrong!', 'warning');
            }
        }
    });
});
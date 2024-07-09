!(function () {
    $(document).on('change', '#ApprovalId', function (e) {
        var pluname = $(this).attr("data-pluName");
        var Approval = $(this).val();
        var ItemId = $(this).attr("data-itemId");
        var hubId = $('#hubId').text();
        var hubIdsplit = hubId.split('0');
        var type = hubIdsplit[1];
        var Id = $(this).attr("data-id");
        var updateButton = $('#price_item_' + ItemId);
        $.ajax({
            url: '/ItemMaster/ApprovetheItem?itemId=' + ItemId + '&approval=' + Approval + '&type=' + type + '&Id=' + ItemId,
            type: 'POST',
            dataType: "json",
            success: function (data) {
                if (data.IsSuccess == true) {
                            updateButton.css('display', 'inline-block');
                            updateButton.css('color', 'green');    
                }                
            }
        });
    });
    $('#btnFilterItem').on('click', function () {
        var MainCategory = $('#MainCategory').val();
        var categoryId = $('#Category').val();
        var subcategory = $('#SubCategory').val();
        var supplierid = $('#Supplierid').val();
        var featured = $('#FeaturedId').val();
        var coupen = $('#CouponId').val();
        var itemStatus = $('#StatusId').val();
        var KotPrintedId = $('#KotPrintedId').val();
        var item = {
            MainCategory1 : $('#MainCategory').val(),
            categoryId: $('#Category').val(),
            subcategory: $('#SubCategory').val(),
            supplierid: $('#Supplierid').val(),
            featured1: $('#FeaturedId').val(),
            coupen: $('#CouponId').val(),
            itemStatus: $('#ItemStatus').val(),
            KotPrintedId: $('#KotPrintedId').val()
        };
        ItemMasterList(item)
    });
    $('#divtabrole').on('click','.PendingItem', function () {
        $.ajax({
            url: '/ItemMaster/_ItemList?Status=Pending',
            type: 'Get',
            contentType: "application/json; charset=utf-8",
            traditional: true,
            success: function (data) {
                $('#itemMaster02').html(data);
            }
        });
    });
    ItemMasterList(0);
    function ItemMasterList(item) {
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/ItemMaster/_Manage',
            type: 'Get',
            contentType: "application/json; charset=utf-8",
            traditional: true,
            data: item,
            success: function (data) {
                $('#textTable').DataTable().destroy();
                $('#itemMaster01').html(data);
                $('.dataTable').DataTable();
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    }
    $(document).on('click', '.deleteItemId', function (e) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to Delete!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Delete it!'
        }).then((result) => {
            var status = 'Deleted'
            var itemid = $(this)[0].dataset.id;
            if (result.value) {
                var info = {
                    ItemId: itemid,
                    Approval: status,
                }
                $.ajax({
                    url: '/ItemMaster/DeleteItem',
                    type: 'GET',
                    data: info,
                    success: function (result) {
                        if (result.IsSuccess == true) {
                            swal.fire("success", "Successfully Deleted Item!", "success");
                            window.location.reload();
                        }
                        else
                            swal.fire("error", "Something went wrong!", "error");
                    }
                });
            }
        });
    });
    $.selector_cache('.filtrExcel').on('click', function () {
        var MainCategory = $('#MainCategory').val();
        var categoryId = $('#Category').val();
        var subcategory = $('#SubCategory').val();
        var supplierid = $('#Supplierid').val();
        var featured = $('#FeaturedId').val();
        var coupen = $('#CouponId').val();
        var itemStatus = $('#StatusId').val();
        var KotPrintedId = $('#KotPrintedId').val();
        var item = {
            MainCategory1: $('#MainCategory').val(),
            categoryId: $('#Category').val(),
            subcategory: $('#SubCategory').val(),
            supplierid: $('#Supplierid').val(),
            featured1: $('#FeaturedId').val(),
            coupen: $('#CouponId').val(),
            KotPrintedId: $('#KotPrintedId').val()
        };
    window.location.href = '/ItemMaster/GetFilterExcel?item=' + item;
    });
    $('#btnClearFilter').on('click', function () {
        window.location.reload();
    });
})();

$('#textTable').on('.Feature-Cls','change', function () {
    alert();
});
$('#AddItemFields').on('click', function () {
    $('#ItemFormId').submit();
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
});
//$('#tbdyItemDetail').on('.includeBtn-cls', 'click', function () {
//    alert();
//});
function includebtn(param) {
    $('#ItemId').prop('value', param);
    $('#itemDetail').modal('show');
}
$('#SubmitDetail').on('click', function () {
    $('#ItemDetailId').submit();
});
$('#Purchaseprice').on('change', function () {
    var orderqty = $('#Weight').val();
    var wastePerc = $('#Wastage').val();
    var purchase = $('#Purchaseprice').val();
    var valtotal = $('#ActualCost').val();
    var selling = $('#SellingPrice').val();
    var selpercnt = $('#ProfitPercentage').val();
    var totalprice = purchase * (wastePerc / 100);
    var demtotalprice = parseFloat(totalprice) + parseFloat(purchase);
    var price = demtotalprice.toFixed(2);
    $('#ActualCost').val(price);
    var selingprice = price * selpercnt / 100;
    var valsell = parseFloat(selingprice) + parseFloat(price);
    var temp = valsell.toFixed(2);
    $('#SellingPrice').val(temp);
    var diff = temp - price;
    var data = diff.toFixed(2);
    $('#ProfitPrice').val(data);
});
$('#SellingPrice').on('change', function () {
    if ($('#Purchaseprice').val() === "" || $('#Purchaseprice').val() === null) {
        $('#SpnPurchaseprice').html("This field is required").css('display', 'block');
        return false;
    } else {
        pricefn();
    }
    return true;
});
$('#Wastage').on('change', function () {
    pricefn();
});
function pricefn() {
    var orderqty = $('#Weight').val();
    var wastePerc = $('#Wastage').val();
    var purchase = $('#Purchaseprice').val();
    var valtotal = $('#ActualCost').val();
    var selling = $('#SellingPrice').val();
    var totalprice = purchase * (wastePerc / 100);
    var demtotalprice = parseFloat(totalprice) + parseFloat(purchase);
    var price = demtotalprice.toFixed(2);
    var diff = selling - price;
    $('#ActualCost').val(price);
    var percentage = diff / price * 100;
    var profitMargin = percentage.toFixed(2);
    $('#ProfitPercentage').val(profitMargin);
    var data = diff.toFixed(2);
    $('#ProfitPrice').val(data);
    var selingprice = price * profitMargin / 100;
    var valsell = parseFloat(selingprice) + parseFloat(price);
    var temp = valsell.toFixed(2);
    $('#SellingPrice').val(temp);
}
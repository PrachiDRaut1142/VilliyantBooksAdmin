!(function () {
    $('#btnFilterItem').on('click', function () {
        var MainCategory = $('#MainCategory').val();
        var categoryId = $('#Category').val();
        var subcategory = $('#SubCategory').val();
        var supplierid = $('#Supplierid').val();
        var featured = $('#FeaturedId').val();
        var coupen = $('#CouponId').val();
        var itemStatus = $('#StatusId').val();
        var item = {
            MainCategory1: $('#MainCategory').val(),
            categoryId: $('#Category').val(),
            subcategory: $('#SubCategory').val(),
            supplierid: $('#Supplierid').val(),
            featured1: $('#FeaturedId').val(),
            coupen: $('#CouponId').val(),
            itemStatus: $('#StatusId').val()
        };
        ItemMasterList(item)
    });
    $('#divtabrole').on('click', '.PendingItem', function () {
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
            url: '/ItemMaster/_includeItemList',
            type: 'Get',
            contentType: "application/json; charset=utf-8",
            traditional: true,
            data: item,
            success: function (data) {
                $('.hubItemList').DataTable().destroy();
                $('#includeItemList').html(data);
                $('.m-data-table').DataTable();
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    }
})();

$('#textTable').on('.Feature-Cls', 'change', function () {
    alert();
});
$('#SubmitDetail').on('click', function () {
    var approval = document.getElementById("ApprovedCheckbox").checked;
    var feature = document.getElementById("FeaturedCheckbox").checked;
    var season = document.getElementById("AvailabilityCheckbox").checked;
    var CoupenDisc = document.getElementById("CoupenCheckbox").checked;
    var chefs = document.getElementById("ChefSpecialCheckbox").checked;
    var mainCategory = $('#maincategoryca').val();
    var category = $('#categoryca').val();
    var itemId = $('#item_id').prop('value');
    swal({
        title: "Are you sure ?",
        text: "you want to Include this Item into the Hub",
        showCancelButton: true,
        confirmButtonText: "Yes Include it !",
        confirmButton: true,
    }).then((result) => {
        if (result.value) {
            var dicData = {
                ItemId: itemId,
                MainCategory: mainCategory,
                Category: category,
                Approval: approval,
                featured: feature,
                seasonSale: season,
                CoupenDisc: CoupenDisc,
                Check_Speical: chefs,
            };
            $.ajax({
                url: '/ItemMaster/AddToItem',
                type: 'POST',
                dataType: 'JSON',
                data: dicData,
                success: function (data) {
                    if (data && data.IsSuccess) {
                        swal.fire("success", "Added inot Hub Successfully!", "success").then(okay => {
                            if (okay) {
                                window.location = '/ItemMaster/HubPriceDetails/' + data.Data;
                            }
                            else {
                                alert('errror');
                            }
                        });
                    }
                    else if (data == null) {
                        swal.fire('Error', "Data is Empty", 'Error');
                    }
                    else {
                        swal.fire('Error', 'These was some error try again later', 'error');
                    }
                }
            });
        }
        else {
            swal.fire("Error", "Something Went Wrong Please Try After Sometimes");
        }
    });
});

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

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var target = $(e.target).attr("href");// activated tab
    var name = $(e.target).attr("name");
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/ItemMaster/_' + name,
        type: 'Get',
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (data) {
            //$('#' + name + '').DataTable().destroy();
            $('#' + name + '').html(data);
            $('#m_portlet_loader').css('display', 'none');
            //$('.dataTable' + name + '').DataTable();
            $('.m-data-table').DataTable();
        }
    });
});
// hubItemlist exclude the Item
$('#includeItemList').on('click', '.excludeBtn-cls', function () {
    var thisValue = $(this)[0].dataset.id;
    swal({
        title: "Are you sure ?",
        text: "you want to Exclude this Item into the Hub",
        showCancelButton: true,
        confirmButtonText: "Yes Exclude it !",
        confirmButton: true,
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: '/ItemMaster/DeleteHubItem?itemId=' + thisValue,
                type: 'Get',
                contentType: "application/json; charset=utf-8",
                traditional: true,
                success: function (data) {
                    if (data && data.IsSuccess) {
                        swal.fire("success", "Excluded inot Hub Successfully!", "success").then(okay => {
                            if (okay) {
                                window.location.reload();
                            }
                        });
                    }
                    else if (data == null) {
                        swal.fire('Error', "Data is Empty", 'Error');
                    }
                    else {
                        swal.fire('Error', 'These was some error try again later', 'error');
                    }
                }
            });
        }
        else {
            swal.fire("Error", "Something Went Wrong Please Try After Sometimes");
        }
    });
});

// hubItemlist include the Item 
$(document).ready(function () {
    $('#excludeItemList').on('click', '.includeBtn-cls', function () {
        var itemId = $(this)[0].dataset.id;
        var pluName = $(this)[0].dataset.name;
        var category = $(this)[0].dataset.category;
        var maincategory = $(this)[0].dataset.MainCategory;     
        $('#item_id').prop('value', itemId);
        $('#plname').prop('value', pluName);
        $('#categoryca').prop('value', category);
        $('#maincategoryca').prop('value', maincategory);      
        $('#FeaturedCheckbox').prop('checked', false);
        $('#AvailabilityCheckbox').prop('checked', false);
        $('#ChefSpecialCheckbox').prop('checked', false);
        $('#CoupenCheckbox').prop('checked', false);
        $('#HubPrice').modal('show');
    });

    $('#MainCate').on('change', function () {       
        var maincategoryId = this.value;
        $.ajax({
            url: "/ItemMaster/GetCategorylist?mainCatId=" + maincategoryId,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected disabled>Select Category</option>';
                    $.each(categoryList, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#Category1').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    });
});


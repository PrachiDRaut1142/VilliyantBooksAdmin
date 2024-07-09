!(function ($) {
    $(document).ready(function () {
        var MainCategoryId = $('#MainCategoryId').val();
        var CategoryId = $('#CategoryId').val();
        var ItemType = $('#ItemType').val();
        var ApproavalType = $('#ApproavalType').val();
        var info = {
            MainCategoryId: MainCategoryId,
            CategoryId: CategoryId,
            ItemType: ItemType,
            ApproavalType: ApproavalType,
        }
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
                url: '/Price/_Index/',
                type: 'GET',
                data: info,
                success: function (data) {
                    if (data != null) {
                        $('#tblPricelistId').DataTable().destroy();
                        $('#pricelist').html(data);
                        $('.dataTable').DataTable();
                            $(".seasonSale").prop('checked', true)
                            $('.WasteagePerc').prop('disabled', false);
                            $('.PurchasePrice').prop('disabled', false);
                            $('.SellingProfitPer').prop('disabled', false);
                            $('.ProfitMargin').prop('disabled', false);
                            $('.MarketPrice').prop('disabled', false);
                            $('.SellingPrice').prop('disabled', false);
                            $('#m_portlet_loader').css('display', 'none');                      
                            $('#m_portlet_loader').css('display', 'none');
                    }
                    else
                        swal.fire("error", "Something went wrong", "error")
                }
            });
        });
    //$.selector_cache('#seasonSales').on('change', function () {
    //    var row = $(this).closest('tr');
    //    if ($(this).is(':checked')) {
    //        $(row).find('#SellingPrice').attr("disable", true);
    //    }
    //    else {
    //        $(row).find('#SellingPrice').attr("disable", false);
    //    }
    //         //var value = $('#seasonSales').prop('checked');
    //         //if (value == true) {
    //         //    $(":disabled", $('#SellingPrice').removeAttr("disabled"), true);
    //         //}
    //         //else {
    //         //    $(":disabled", $('#SellingPrice').attr("disabled"), true);
    //         //}
    //     });
    filterFun();
    Seasonsale();
    pricefn();
    SellingProfitPer();
    ProfitMargin();
    SellingPrice();

    $('#pricelistDivId').on('change', '#seasonSales', function () {
        Seasonsale(this);
    });
    function Seasonsale(sale) {
        var target = $(sale).closest('tr');
        var value = target.find('#seasonSales').prop('checked');
        if (value != true) {
            target.find('#WasteagePerc').prop('disabled', true);
            target.find('#PurchasePrice').prop('disabled', true);
            target.find('#SellingProfitPer').prop('disabled', true);
            target.find('#ProfitMargin').prop('disabled', true);
            target.find('#MarketPrice').prop('disabled', true);
            target.find('#SellingPrice').prop('disabled', true);
            target.find('#seasonSalechange').val('N');
        }
        else {
            target.find('#WasteagePerc').prop('disabled', false);
            target.find('#PurchasePrice').prop('disabled', false);
            target.find('#SellingProfitPer').prop('disabled', false);
            target.find('#ProfitMargin').prop('disabled', false);
            target.find('#MarketPrice').prop('disabled', false);
            target.find('#SellingPrice').prop('disabled', false);
            target.find('#seasonSalechange').val('Y');
        }
    }

    $('#pricelistDivId').on('change', '#WasteagePerc,#PurchasePrice', function () {
        pricefn(this);
    });

    function pricefn(current) {
        var target = $(current).closest('tr');
        var WasteagePerc = target.find('#WasteagePerc').val();
        var PurchasePrice = target.find('#PurchasePrice').val();
        var SellingPrice = target.find('#SellingPrice').val();
        var SellingProfitPer = target.find('#SellingProfitPer').val();
        var TotalPrice = target.find('#TotalPrice').val();
        var ProfitMargin = target.find('#ProfitMargin').val();
        var totalprice = PurchasePrice * (WasteagePerc / 100);
        var actualtotalprice = parseFloat(totalprice) + parseFloat(PurchasePrice);
        var price = actualtotalprice.toFixed(2);
        target.find('#TotalPrice').text(price);
        target.find('#TotalPricetxt').val(price);
        var diff = SellingPrice - price;
        var percentage = diff / price * 100;
        var profitpercentMargin = percentage.toFixed(2);
        target.find('#SellingProfitPer').val(profitpercentMargin);
        var data = diff.toFixed(2);
        target.find('#ProfitMargin').val(data);
        //var temp = SellingPrice
        //temp = Math.round(temp);
        //target.find('#SellingPrice').prop('value', temp);
        target.find('#seasonSales').val();
    }

    $('#pricelistDivId').on('change', '#SellingProfitPer', function () {
        SellingProfitPer(this);
    });

    function SellingProfitPer(spr) {
        var target = $(spr).closest('tr');
        var OrderQty = $('#OrderQty').val();
        var WasteagePerc = target.find('#WasteagePerc').val();;
        var PurchasePrice = target.find('#PurchasePrice').val();
        var SellingProfitPer = target.find('#SellingProfitPer').val();
        var TotalPrice = target.find('#TotalPrice').val();
        var ProfitMargin = target.find('#ProfitMargin').val();
        var SellingPrice = target.find('#SellingPrice').val();
        var totalprice = PurchasePrice * (WasteagePerc / 100);
        var actualtotalPrice = parseFloat(totalprice) + parseFloat(PurchasePrice);
        var price = actualtotalPrice.toFixed(2);
        target.find('#TotalPrice').html(price);
        target.find('#TotalPricetxt').val(price);
        var selingprice = price * SellingProfitPer / 100;
        var valsell = parseFloat(selingprice) + parseFloat(price);
        var temp = valsell.toFixed(2);
        temp = Math.round(temp);
        target.find('#SellingPrice').val(temp);
        var diff = temp - price;
        var data = diff.toFixed(2);
        target.find('#ProfitMargin').val(data);
        target.find('#seasonSales').val();
    }

    $('#pricelistDivId').on('change', '#ProfitMargin', function () {
        ProfitMargin(this);
    });

    function ProfitMargin(prm) {
        var target = $(prm).closest('tr');
        var OrderQty = target.find('#OrderQty').val();
        var WasteagePerc = target.find('#WasteagePerc').val();
        var PurchasePrice = target.find('#PurchasePrice').val();
        var SellingProfitPer = target.find('#SellingProfitPer').val();
        var TotalPrice = target.find('#TotalPrice').val();
        var ProfitMargin = target.find('#ProfitMargin').val();
        var SellingPrice = target.find('#SellingPrice').val();
        var totalprice = PurchasePrice * (WasteagePerc / 100);
        var actualtotalPrice = parseFloat(totalprice) + parseFloat(PurchasePrice);
        var price = actualtotalPrice.toFixed(2);
        target.find('#TotalPrice').html(price);
        target.find('#TotalPricetxt').val(price);
        var seling = parseInt(ProfitMargin) + parseInt(price);
        target.find('#SellingPrice').val(seling);
        var percentage = ProfitMargin / price * 100;
        var selingprofitMargin = percentage.toFixed(2);
        target.find('#SellingProfitPer').val(selingprofitMargin);
        target.find('#seasonSales').val();
    }

    $('#pricelistDivId').on('change', '#SellingPrice', function () {
        SellingPrice(this);
    });

    function SellingPrice(sel) {
        var target = $(sel).closest('tr');
        var OrderQty = target.find('#OrderQty').val();
        var WasteagePerc = target.find('#WasteagePerc').val();
        var PurchasePrice = target.find('#PurchasePrice').val();
        var SellingProfitPer = target.find('#SellingProfitPer').val();
        var TotalPrice = target.find('#TotalPrice').val();
        var ProfitMargin = target.find('#ProfitMargin').val();
        var SellingPrice = target.find('#SellingPrice').val();
        var totalprice = PurchasePrice * (WasteagePerc / 100);
        var actualtotalPrice = parseFloat(totalprice) + parseFloat(PurchasePrice);
        var price = actualtotalPrice.toFixed(2);
        target.find('#TotalPrice').text(price);
        target.find('#TotalPricetxt').val(price);
        var diff = SellingPrice - price;
        var percentage = diff / price * 100;
        var profitMargin = percentage.toFixed(2);
        target.find('#SellingProfitPer').val(profitMargin);
        var data = diff.toFixed(2);
        target.find('#ProfitMargin').val(data);
        target.find('#seasonSales').val();
    }

    $('#Filter-Id').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        var ApproavalType = $('#ApproavalType').prop('value');
        if (ApproavalType == 'Y') {
            $(".seasonSale").prop('checked', true)
            $('.WasteagePerc').prop('disabled', false);
            $('.PurchasePrice').prop('disabled', false);
            $('.SellingProfitPer').prop('disabled', false);
            $('.ProfitMargin').prop('disabled', false);
            $('.MarketPrice').prop('disabled', false);
            $('.SellingPrice').prop('disabled', false);
            $('#m_portlet_loader').css('display', 'none');
        }
        else {
            $(".seasonSale").prop('checked', false)
            $('.WasteagePerc').prop('disabled', true);
            $('.PurchasePrice').prop('disabled', true);
            $('.SellingProfitPer').prop('disabled', true);
            $('.ProfitMargin').prop('disabled', true);
            $('.MarketPrice').prop('disabled', true);
            $('.SellingPrice').prop('disabled', true);
            $('#m_portlet_loader').css('display', 'none');
        }
        filterFun();
    });
    function filterFun() {
        var MainCategoryId = $('#MainCategoryId').val();
        var CategoryId = $('#CategoryId').val();
        var ItemType = $('#ItemType').val();
        var ApproavalType = $('#ApproavalType').val();
        var info = {
            MainCategoryId: MainCategoryId,
            CategoryId: CategoryId,
            ItemType: ItemType,
            ApproavalType: ApproavalType,
        }
        $('#m_portlet_loader').css('display', 'block');
        $.ajax(
            {
                url: '/Price/_Index/',
                type: 'GET',
                data: info,
                success: function (data) {
                    if (data != null) {
                        $('#tblPricelistId').DataTable().destroy();
                        $('#pricelist').html(data);
                        $('.dataTable').DataTable();
                        if (ApproavalType == 'Y') {
                            $(".seasonSale").prop('checked', true)
                            $('.WasteagePerc').prop('disabled', false);
                            $('.PurchasePrice').prop('disabled', false);
                            $('.SellingProfitPer').prop('disabled', false);
                            $('.ProfitMargin').prop('disabled', false);
                            $('.MarketPrice').prop('disabled', false);
                            $('.SellingPrice').prop('disabled', false);
                            $('#m_portlet_loader').css('display', 'none');
                        }
                        else {
                            $(".seasonSale").prop('checked', false)
                            $('.WasteagePerc').prop('disabled', true);
                            $('.PurchasePrice').prop('disabled', true);
                            $('.SellingProfitPer').prop('disabled', true);
                            $('.ProfitMargin').prop('disabled', true);
                            $('.MarketPrice').prop('disabled', true);
                            $('.SellingPrice').prop('disabled', true);
                            $('#m_portlet_loader').css('display', 'none');
                        }
                        $('#m_portlet_loader').css('display', 'none');
                    }
                    else
                        swal.fire("error", "Something went wrong", "error")
                }
            });
    }

    $('#Clear-Id').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        $("#MainCategoryId option[value=0]").prop('selected', true);
        $("#CategoryId option[value=0]").prop('selected', true);
        $("#ItemType option[value=0]").prop('selected', true);
        $("#ApproavalType option[value=Y]").prop('selected', true);
        $('#m_portlet_loader').css('display', 'none');
        filterFun();
    });

    $('#AddPricelist').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        $('.WasteagePerc').prop('disabled', false);
        $('.PurchasePrice').prop('disabled', false);
        $('.SellingProfitPer').prop('disabled', false);
        $('.ProfitMargin').prop('disabled', false);
        $('.MarketPrice').prop('disabled', false);
        $('.SellingPrice').prop('disabled', false);
        $('#PriclistFormId').submit();
        $('#m_portlet_loader').css('display', 'none');
    });
    //function SaleToggle() {
    //    if ($(".seasonSale").prop('checked') != true) {
    //        $(":disabled", $('#SellingPrice').attr("disabled"), true);
    //    }
    //    else {
    //        $(":disabled", $('#SellingPrice').removeAttr("disabled"), true);
    //    }
    //}
    //$.selector_cache('#seasonSales').on('click', function (e) {
    //    var value = $('#seasonSales').prop('checked');
    //    if (value == true) {
    //        $(":disabled", $('#SellingPrice').removeAttr("disabled"), true);
    //    }
    //    else {
    //      $(":disabled", $('#SellingPrice').attr("disabled"), true);
    //    }
    //});
})(jQuery);
!(function ($) {
    $(Document).ready(function () {
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
                url: '/Price/_ItemAvaliable/',
                type: 'GET',
                data: info,
                success: function (data) {
                    if (data != null) {
                        $('#AvaliableItemList').DataTable().destroy();
                        $('#ItemAvailableList').html(data);
                        $('.m-data-table').DataTable();
                        $(".seasonSale").prop('checked', true)
                        $('#m_portlet_loader').css('display', 'none');
                    }
                    else
                        swal.fire("error", "Something went wrong", "error")
                }
        });
    });    
    function Seasonsale1(sale) {
        var target = $(sale).closest('tr');
        var value = target.find('#seasonSales1').prop('checked');
        if (value != true) {
            target.find('#seasonSalechange').val('N');
        }
        else {
            target.find('#seasonSalechange').val('Y');
        }
    }

    function Seasonsale2(sale) {
        var target = $(sale).closest('tr');
        var value = target.find('#seasonSales2').prop('checked');
        if (value != true) {
            target.find('#seasonSalechange').val('N');
        }
        else {
            target.find('#seasonSalechange').val('Y');
        }
    }

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
        $.ajax({
                url: '/Price/_ItemAvaliable/',
                type: 'GET',
                data: info,
                success: function (data) {
                    if (data != null) {
                        $('#AvaliableItemList').DataTable().destroy();
                        $('#ItemAvailableList').html(data);
                        $('.m-data-table').DataTable();
                        if (ApproavalType == 'Y') {
                            $(".seasonSale").prop('checked', true)
                            $('#m_portlet_loader').css('display', 'none');
                        }
                        else {
                            $(".seasonSale").prop('checked', false)
                            $('#m_portlet_loader').css('display', 'none');
                        }
                        $('#m_portlet_loader').css('display', 'none');
                    }
                    else
                        swal.fire("error", "Something went wrong", "error")
                }
        });
    }

    $('#ItemAvailableList').on('change', '#seasonSales1', function () {
        Seasonsale1(this);
    });
    $('#ItemAvailableList').on('change', '#seasonSales2', function () {
        Seasonsale2(this);
    });

    $('#Filter-Id').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        var ApproavalType = $('#ApproavalType').prop('value');
        if (ApproavalType == 'Y') {
            $(".seasonSale").prop('checked', true)
            $('#m_portlet_loader').css('display', 'none');
        }
        else {
            $(".seasonSale").prop('checked', false)
            $('#m_portlet_loader').css('display', 'none');
        }
        filterFun();
    });

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
        $('#ItemListFormId').submit();
        $('#m_portlet_loader').css('display', 'none');
    });
})(jQuery);
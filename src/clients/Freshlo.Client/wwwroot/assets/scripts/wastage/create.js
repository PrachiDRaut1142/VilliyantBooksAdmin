!(function ($) {
    // Scroll Top Script
    $(document).ready(function () {
        $(".showdiv").css("display", "none");
        var topOfOthDiv = $(".toptouchdiv").offset().top;
        $(window).scroll(function () {
            if ($(window).scrollTop() > topOfOthDiv) {
                $(".showdiv").css('display', 'block');
            }
            else {
                $(".showdiv").css('display', 'none');
            }
        });
    });
})(jQuery);
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
            var wastageList = new Array();
            $('#tblwastage tr').each(function () {
                var row = $(this);
                var wastagedetail = {
                    ItemId: $(this).closest("tr").prop('id'),
                    TotalStock: $('#Totalstock_' + this.id).text(),
                    Wastagestock: $('#Wasstock_' + this.id).text(),
                    WastageItemPrice: $('#WasPurchase_' + this.id).text(),
                    Measurement: $('#Measure_' + this.id).text(),
                    Wastage_Quan: row.find("td:nth-child(4) input").prop('value'),
                    WastageType: row.find("td:nth-child(5) select").prop('value'),
                    Wastage_Description: $('#WastageDescription_' + this.id).val(),
                    stockId: $('#stockId_' + this.id).text(),
                    stockPo: $('#stockPo_' + this.id).text(),
                    wastageid: $('#wastageId_' + this.id).text(),
                    TotalWastageQuan: $('#CalWastagequan_' + this.id).text(),
                    ItemwastagePrice: $('#CalWastagePrice_' + this.id).text(),

                }
                wastageList.push(wastagedetail);

                if (checkboxdata[k] === wastagedetail.ItemId) {
                    $.ajax({
                        url: '/Wastage/InsertWastage',
                        type: 'Post',
                        dataType: 'json',
                        data: wastagedetail,
                        success: function (data) {
                        }
                    });
                    k++;
                }
            });
        }
        swal({
            title: "Saved Successfully!",
            type: "success",
            confirmButtonText: "Ok!"
        }).then(function (e) {
            if (e.value) {
                    window.location.href = "/Wastage/Create/";
                }
            });
    }
    else {
        swal("Select Checkbox!", "", "warning");
    }

}

function Wastagecal(param) {
    var wastagequantity = $('#WastageQuan_' + param).val();
    var TotalStock = $('#Totalstock_' + param).text();
    var purchaseprice = $('#Purchaseprice_' + param).text();
    var Totalwastagequan = $('#TotalWastagequan_' + param).text();
    var Totalwasprice = $('#TotalWastagePrice_' + param).text();
    var calucuwastagequan = parseFloat(wastagequantity) + parseFloat(Totalwastagequan);
    var calculatedstock = parseFloat(TotalStock) - parseFloat(wastagequantity);
    var wastageprice = parseFloat(wastagequantity) * parseFloat(purchaseprice);
    var calcuwastagePrice = parseFloat(wastagequantity) * parseFloat(purchaseprice);
    var totalwastageprice = parseFloat(Totalwasprice) + parseFloat(calcuwastagePrice);
    var stockPo = parseFloat(calculatedstock) * parseFloat(purchaseprice);
    $('#CalWastagequan_' + param).text(calucuwastagequan);
    $('#CalWastagePrice_' + param).text(totalwastageprice);
    $('#WasPurchase_' + param).text(wastageprice);
    $('#Wasstock_' + param).text(calculatedstock);
    $('#stockPo_' + param).text(stockPo);
}
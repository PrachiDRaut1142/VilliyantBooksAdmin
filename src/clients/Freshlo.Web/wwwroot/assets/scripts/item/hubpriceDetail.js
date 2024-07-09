var sizeArr = [];
var priceArr = [];
var colorArr = [];
var ImageArr = [];
var MapArr = [];
!(function () {
    var optionvale1 = $('#MeasuredIn').prop('value');
    if (optionvale1 == "S") {
        HubSizeListDefault();
    }
    else if (optionvale1 == "W") {
        HubWeightListDefault();
    }
    else {
        HubNovartListDefault();
    }
    $('#Wastage').on('change', function () {
        pricefn();
    });

    //Selling Varience
    $("#btnVariance").on('click', function () {
        var error = 0;
        if ($('#Measurement').val() === null || $('#Measurement').val() === "") {
            $('#SpnVariance').html("Please select measurement").css('display', 'block');
            return false;
        }
        if ($('#SelRange').val() === "" || $('#SelRange').val() === null) {
            $('#SpnVariance').html("Field is empty!").css('display', 'block');
            return false;
        }
        if ($('#SelRange').val() <= 0) {
            $('#SpnVariance').html("Enter positive number").css('display', 'block');
            error += 1;
        }
        if (error === 0) {
            var value = $('#SelRange').val();
            var measure = $('#Measurement').val();
            if ($('#Spn_').text() === "NA") {
                $('#Spn_').html(' ');
            }
            data = $('#Spn_').text();
            data += value + ',';
            $('#Spn_').html(data);
            $('#spndata').val(data);
            $('#divSelTag').append('<span class="Ab-filter spnvariance" data-id=' + value + ' id=' + value + '>' +
                '<span id="spnvalueid" class= "tag_content">' + value + "" + measure + '</span > ' +
                '<a href="#" class="xclose" style="color: #585858; margin-left: 2px;"></a>' +
                '</span>');
            $('#SelRange').val("");
        }
    });

    $('#divSelTag').on('click', '.spnvariance', function () {
        var myVehicleId = $(this).data('id');
        RemoveSpan(myVehicleId);
    });

    var itm = "";
    var dtaa = "";
    function RemoveSpan(parm) {
        var seldata = "";
        $('span[id="' + parm + '"]').remove();
        itm += parm + ',';
        $('#Spnremove_').html(itm);
        dtaa = $('#Spn_').text();
        dtaa = dtaa.split(',');
        for (var i = 0; i < dtaa.length; i++) {
            if (dtaa[i] != parm && dtaa[i] != "") {
                seldata += dtaa[i] + ',';
            }
        }
        $('#Spn_').html(seldata);
        $('#spndata').val(seldata);
        if (seldata === "") {
            $('#Spn_').html(' ');
            $('#Spnremove_').html(' ');
            $('#spndata').val(' ');
        }
        seldata = "";
        data = "";
    }

    $('#ApprovalId').on('change', function () {
        var pluname = $('#pluName').val();
        var Approval = $('#ApprovalId').val();
        var ItemId = $('#ItemId').val();
        $.ajax({
            url: '/ItemMaster/ApprovetheItem?itemId=' + ItemId + '&approval=' + Approval,
            type: 'POST',
            dataType: "json",
            success: function (data) {
                if (data.IsSuccess == true) {
                    swal.fire(pluname + " is set as " + Approval, "", "success");
                } else {
                    swal.fire(pluname + " is set as " + Approval, "", "warning");
                }
                if (Approval == "Approved") {
                    $('#divFeaturedId').css('display', 'block');
                }
                else {
                    $('#divFeaturedId').css('display', 'none');
                }
            }
        });
    });

    if ($('#ApprovalId').val() == 'Approved') {
        $('#divFeaturedId').css('display', 'block');
    }

    var ItemType = $('#ItemSellingType').val();
    if (ItemType === '2') {
        $("#MaxQntOrder").css('display', 'block');
    }
    else {
        $("#MaxQntOrder").css('display', 'none');
    }

    //ItemSellingType
    $("#ItemSellingType").on('change', function () {
        var stockType = $(this).find('option:selected').val();
        if (stockType === '2') {
            $("#MaxQntOrder").css('display', 'block');
        } else {
            $("#MaxQntOrder").css('display', 'none');
        }
    });

    //Images Script
    $('#Useravatar').on('change', function () {
        var tmppath = URL.createObjectURL(event.target.files[0]);
        readURL(this);
    });
    var d = 0;
    function readURL(input) {
        var count = $('#ImagesCount').text();
        if (input.files && input.files[0]) {
            var i;
            for (i = 0; i < input.files.length; ++i) {
                count++;
                var reader = new FileReader();
                reader.onload = function (oFREvent) {
                    $('#appImage').append('<a href="#" id="img" class="PictureImg" data-id=""><img id="galleryid_" src="' + oFREvent.target.result + '" class= "img-thumb-df"/></a>' +
                        '<span hidden id="SpnId_">' + oFREvent.target.result + '</span>');
                }
                reader.readAsDataURL(input.files[i]);
            }
        }
    }

    $('#appImage').on('click', '.PictureImg', function () {
        var Id = $(this).data('id');
        if (Id != "") {
            var galleryImg = $('#SpnId_' + Id).text();
            document.getElementById('myImage').src = galleryImg;
            var icon = "freshlo-img/icon/" + $('#ItemId').val();
            var iconimages = $('#DefaultImg_' + Id).val();
            $('#DefaultImg_1').val('');
            $('#DefaultImg_1').val(iconimages);
        }
        if ($("#galleryid_" + Id).hasClass("gallaryimg")) {
            $('#RemoveImg').css('display', 'block');
        }
        else {
            $('#RemoveImg').css('display', 'none');
        }
    });

    //Images Default
    $('#DefaultImages').on('click', function () {
        var test = $('#SpnId_').text();
        $.ajax({
            url: '/ItemMaster/GetDowload?name=' + $('#DefaultImg_1').val(),
            type: 'POST',
            dataType: "json",
            success: function (data) {
                if (data == "true") {
                    swal("Set as default image!", "", "success");
                    window.location.reload();
                }
                else {
                    swal("Something went wrong!", "It may be default", "error");
                }
            }
        });
    });

    $('#RemoveImg').on('click', function () {
        var keyName = $('#DefaultImg_1').val();
        $.ajax({
            url: '/ItemMaster/DeleteImageSrc?key=' + keyName,
            type: 'GET',
            success: function (data) {
                if (data === true) {
                    //$('#myImage').removeAttr('src');
                    swal("Deleted successfully!", "", "success");
                    window.location.reload(true);
                }
                else {
                    swal("Something went wrong", "", "error");
                }
            }
        });
    });

    //ItemMaster Update
    $(document).on('click', '#btnUpdateItem', function (e) {
        $('#SpnVariance').html(' ');
        var nErrorCount = 0;
        if ($('#KOT_Print').val() == null || $('#KOT_Print').val() == "") {
            $('#SpnkotPrint').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#soldId').val() === "" || $('#soldId').val() === null) {
            $('#Spnsoldid').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#pluName').val() === "" || $('#pluName').val() === null) {
            $('#Spnpluname').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#PluCode').val() === "" || $('#PluCode').val() === null) {
            $('#Spnplucode').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#TagId').val() === "" || $('#TagId').val() === null) {
            $('#SpnTagId').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Category').val() === "" || $('#Category').val() === null) {
            $('#SpnCategory').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#MainCate').val() === "0" || $('#MainCate').val() === null) {
            $('#SpnMainCate').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#ItemType').val() === "" || $('#ItemType').val() === null) {
            $('#SpnItemType').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#SubCategory').val() === "" || $('#SubCategory').val() === null) {
            $('#Spnsubcategory').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Supplier').val() === "" || $('#Supplier').val() === null) {
            $('#Spnsupplier').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Offertype').val() === "" || $('#Offertype').val() === null) {
            $('#Spnoffertype').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#segment').val() === "" || $('#segment').val() === null) {
            $('#Spnsegment').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        //if ($('#Purchaseprice').val() === "" || $('#Purchaseprice').val() === null) {
        //    $('#SpnPurchaseprice').html("This field is required").css('display', 'block');
        //    nErrorCount += 1;
        //}
        //if ($('#Wastage').val() === "" || $('#Wastage').val() === null) {
        //    $('#SpnWastage').html("This field is required").css('display', 'block');
        //    nErrorCount += 1;
        //}
        //if ($('#SellingPrice').val() === "" || $('#SellingPrice').val() === null) {
        //    $('#SpnSellingPrice').html("This field is required").css('display', 'block');
        //    nErrorCount += 1;
        //}
        //if ($('#MarketPrice').val() === "" || $('#MarketPrice').val() === null) {
        //    $('#SpnMarket').html("This field is required").css('display', 'block');
        //    nErrorCount += 1;
        //}

        if ($('#Purchaseprice').val() < 0) {
            $('#SpnPurchaseprice').html("Please enter positive value").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Wastage').val() < 0) {
            $('#SpnWastage').html("Please Enter positive value").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#SellingPrice').val() < 0) {
            $('#SpnSellingPrice').html("Please enter positive value").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#MarketPrice').val() < 0) {
            $('#SpnMarket').html("Please Enter positive value").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#NetWeight').val() === "" || $('#NetWeight').val() === null) {
            $('#NetWeight').val(0.0);

        }
        //if ($('#BrandId').val() === "" || $('#BrandId').val() === null) {
        //    $('#SpnBrandId').html("This field is required").css('display', 'block');
        //    nErrorCount += 1;
        //}
        if ($('#Visibility').val() === "" || $('#Visibility').val() === null) {
            $('#SpnVisibility').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Relatiopship').val() === "2") {
            if ($('#ParentItemId').val() == null) {
                $('#SpnParentItemId').html("This field is required").css('display', 'block');
                nErrorCount += 1;
            }
        }
        if ($('#soldId').val() === '2') {
            //if ($('#StockType').val() === "" || $('#StockType').val() === null) {
            //    $('#SpnStockType').html("This field is required").css('display', 'block');
            //    nErrorCount += 1;
            //}
            if ($('#MQOId').val() === "" || $('#MQOId').val() === null) {
                $('#SpnMQOId').html("This field is required").css('display', 'block');
                nErrorCount += 1;
            }
            if ($('#MQOId').val() <= 0) {
                $('#SpnMQOId').html("Enter natural value").css('display', 'block');
                nErrorCount += 1;
            }
        }
        else {
            if ($('#MQOId').val() === "") {
                $('#MQOId').val('1');
            }
            if ($('#Measurement').val() === "" || $('#Measurement').val() === null) {
                $('#Spnmeasurement').html("This field is required").css('display', 'block');
                nErrorCount += 1;
            }
            if ($('#Weight').val() === "" || $('#Weight').val() === null) {
                $('#SpnWeight').html("This field is required").css('display', 'block');
                nErrorCount += 1;
            }
            if ($('#Weight').val() < 0) {
                $('#SpnWeight').html("Please enter positive value").css('display', 'block');
                nErrorCount += 1;
            }
        }
        if (0 === nErrorCount) {
            $('#Approval').prop('value', $('#ApprovalId').val());
            if ($('#taxId').prop('value') == "No") {
                var igst = $("#IGST option:selected").text();
                var sellingPrice = $('#SellingPrice').val();
                var perValue = (igst / 100) * sellingPrice;
                var actualValue = parseFloat(sellingPrice) - parseFloat(perValue);
                $('#productCost').prop('value', perValue);
            }
            $('#itemMasterformid').submit();
        }
    });
})();

var feature = $('#featuretest').val();
if (feature === "Y") {
    $("#FeaturedCheckbox").prop('checked', true);
}
else {
    $("#FeaturedCheckbox").prop('checked', false);
}

$('#FeaturedCheckbox').on('click', function () {
    var pluname = $('#pluName').val();
    var sVisible = $(this).prop('checked');
    var itemId = 0;
    var Id = $('#Id').val();
    var type = 2;
    $.ajax({
        url: "/ItemMaster/FeaturedUpdate?feature=" + sVisible + "&itemId=" + itemId + '&type=' + type + '&Id=' + Id,
        type: "GET",
        success: function (value) {
            if (value.IsSuccess === true) {
                if (sVisible === true) {
                    swal(pluname + " is set as featured item", "", "success");
                } else {
                    swal(pluname + " is no more as featured item", "", "warning");
                }
            }
        }
    });
});

var coupen = $('#Coupentest').val();
if (coupen == "1") {
    $("#CoupenCheckbox").prop('checked', true);
}
else {
    $("#CoupenCheckbox").prop('checked', false);
}

$('#CoupenCheckbox').on('click', function () {
    var pluname = $('#pluName').val();
    var sVisible = $(this).prop('checked');
    var itemId = 0;
    var Id = $('#Id').val();
    var type = 2;
    $.ajax({
        url: "/ItemMaster/CoupenUpdate?Coupen=" + sVisible + "&itemId=" + itemId + '&type=' + type + '&Id=' + Id,
        type: "GET",
        success: function (value) {
            if (value.IsSuccess === true) {
                if (sVisible === true) {
                    $('#Coupen').prop('value', 1);
                    swal("Coupen set for " + pluname + "", "", "success");
                } else {
                    $('#Coupen').prop('value', 0);
                    swal("Coupen removed for " + pluname + "", "", "warning");
                }
            }
        }
    });
});

var avaliable = $('#avaliblitytest').val();
if (avaliable === "Y") {
    $("#AvailabilityCheckbox").prop('checked', true);
}
else {
    $("#AvailabilityCheckbox").prop('checked', false);
}

$('#ChckSpecialCheckbox').on('click', function () {
    var pluname = $('#pluName').val();
    var sVisible = $(this).prop('checked');
    var itemId = 0;
    var Id = $('#Id').val();
    var type = 2;
    $.ajax({
        url: "/ItemMaster/CheckSpUpadte?checkSp=" + sVisible + "&itemId=" + itemId + '&type=' + type + '&Id=' + Id,
        type: "GET",
        success: function (value) {
            if (value.IsSuccess === true) {
                if (sVisible === true) {
                    swal(pluname + " is set as Checked item", "", "success");
                } else {
                    swal(pluname + " is no more as Checked item", "", "warning");
                }
            }
        }
    });
});

var chefspecail = $('#checksptest').val();
if (chefspecail === "Y") {
    $("#ChckSpecialCheckbox").prop('checked', true);
}
else {
    $("#ChckSpecialCheckbox").prop('checked', false);
}

$('#AvailabilityCheckbox').on('click', function () {
    var pluname = $('#pluName').val();
    var sVisible = $(this).prop('checked');
    var itemId = 0;
    var Id = $('#Id').val();
    var type = 2;
    $.ajax({
        url: "/ItemMaster/SeasonUpdate?Season=" + sVisible + "&itemId=" + itemId + '&type=' + type + '&Id=' + Id,
        type: "GET",
        success: function (value) {
            if (value.IsSuccess === true) {
                if (sVisible === true) {
                    $('#Season').prop('value', "Y");
                    swal(pluname + " is now available", "", "success");
                } else {
                    $('#Season').prop('value', "N");
                    swal(pluname + " is not available", "", "warning");
                }
            }
        }
    });
});

function Removesize(param) {
    var temp = $('#Spn_' + param).text();
    var id = $('#Spnsize_' + param).text();
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/ItmDeleteSize?PriceId=' + id + '&Condition=' + temp,
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                swal.fire("success", "Remove Successfully!", "success").then(okay => {
                    if (okay) {
                        SizeListDefault();                       
                    }                  
                });              
            }
        });
    }
    $('#' + param).empty();
    $.each(sizeArr, function (i) {
        if (sizeArr[i].Size == param) {
            sizeArr.splice(i, 1);
            return false;
        }
    });
    var tbodylenght = $('#Itemtbl_Body tr').length;
    window.location.reload();
}

function RemoveWeight(param) {
    var temp = $('#Spn_' + param).text();
    var id = $('#Spnweight_' + param).text();
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/ItmDeleteWeightQty?PriceId=' + id + '&Condition=' + temp,
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                swal.fire("success", "Remove Successfully!", "success").then(okay => {
                    if (okay) {
                        var varaintName = $('#MeasuredIn').val();
                        if (varaintName == "W") {
                            WeightListDefault();                          
                        }
                        else {
                            NovartListDefault();
                        }
                    }
                });
            }
        });
    }
    $('#' + param).empty();
    $.each(sizeArr, function (I) {
        if (sizeArr[i].Size == param) {
            sizeArr.splice(i, 1);
            return false;
        }
    });
    var tbodylength = $('#ItemtblW_Body tr').length;
    window.location.reload();
}

var optionvale = $('#MeasuredIn').prop('value');
if (optionvale == "S") {
    $('.SizeRow').css('display', 'flex');
}
if (optionvale == "W") {
    $('.WeightRow').css('display', 'flex');
}

$("#MeasuredIn").change(function () {
    var optionvale = $('#MeasuredIn').prop('value');
    if (optionvale != "S" && optionvale != "W") {
        $('.RowSizeandWegitMap').css('display', 'none');
    }
    if (optionvale == "S") {
        $('.RowSizeandWegitMap').css('display', 'flex');
        $('.hasNovaraint').css('display', 'flex');
        $('.sizedetails').css('display', 'flex');
        $('.SizeRow').css('display', 'flex');
        $('.wegihtate').css('display', 'none');
        $('.WeightRow').css('display', 'none');
        $('.VaraintRow').css('display', 'none');
        $('.error-empty-cls').css('display', 'none');
    }
    if (optionvale == "W") {
        $('.RowSizeandWegitMap').css('display', 'flex');
        $('.hasNovaraint').css('display', 'flex');
        $('.sizedetails').css('display', 'none');
        $('.SizeRow').css('display', 'none');
        $('.wegihtate').css('display', 'flex');
        $('.WeightRow').css('display', 'flex');
        $('.VaraintRow').css('display', 'none');
        $('.error-empty-cls').css('display', 'none');
        $('#nonvegconsumption').css('display', 'none');
    }
    if (optionvale == "NOVAR") {
        $('.RowSizeandWegitMap').css('display', 'flex');
        $('.hasNovaraint').css('display', 'none');
        $('.wegihtate').css('display', 'none');
        $('.sizedetails').css('display', 'none');
        $('.SizeRow').css('display', 'none');
        $('.WeightRow').css('display', 'none');
        $('.VaraintRow').css('display', 'flex');
        $('.error-empty-cls').css('display', 'none');
    }
}).change();

$.selector_cache('#SizeColor').on('click', '#AddSize', function (e) {
    var optionvale = $('#MeasuredIn').prop('value');
    var currencySymbol = $('#currencyData').text();
    if (optionvale == "S") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#Size').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnSize').css('display', 'block');
        }
        var lPurchaseprice = $.selector_cache('#Purchaseprice').prop('value').trim();
        if (lPurchaseprice === "") {
            numberOfErrors++;
            $('#spnPurchaseprice').css('display', 'block');
        }
        var lSellingPrice = $.selector_cache('#SellingPrice').prop('value').trim();
        if (lSellingPrice === "") {
            numberOfErrors++;
            $('#spnSellingPrice').css('display', 'block');
        }
        var lMarketPrice = $.selector_cache('#MarketPrice').prop('value').trim();
        if (lMarketPrice === "") {
            numberOfErrors++;
            $('#spnMarketPrice').css('display', 'block');
        }
        if (numberOfErrors == 0) {
            var Size = $('#Size').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#pluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var ItmNetWeight = $('#weightportion').prop('value');
            var Mesaurment = "gm"
            if (Size != "") {
                var Array = {
                    'Size': Size,
                    'ItmNetWeight': ItmNetWeight,
                    'ItmMeasurement': Mesaurment,
                    'Purchaseprice': Purchaseprice,
                    'SellingPrice': SellingPrice,
                    'MarketPrice': MarketPrice,
                    'ProfitMargin': ProfitPercentage,
                    'ProfitPrice': ProfitPrice,
                    'TotalPrice': ActualCost,
                    'ItemId': ItemId,
                    'PluName': PluName,
                    'WastageQty': 0,
                    'WasteagePerc': 0,
                    'Weight': Weight,
                };
                sizeArr.push(Array);
                $('#Size').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                };
                $.ajax({
                    url: '/ItemMaster/HubItemsVariance/',
                    type: 'POST',
                    dataType: 'json',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        swal.fire("success", "Added Successfully!", "success").then(okay => {
                            if (okay) {
                                sizeArr.length = 0;
                                var varaintName = $('#MeasuredIn').val();
                                if (varaintName == "S") {
                                    HubSizeListDefault();
                                }
                                else if (varaintName == "W") {
                                    HubWeightListDefault();
                                }
                                else if (varaintName == "NOVAR") {
                                    HubNovartListDefault();
                                }
                            }
                        });
                    }
                });
            }
            else {
                swal.fire("error", "Something went wrong", "error");
            }
        }
        else {
            swal.fire("error", "Something went wrong", "error");
        }
    }
    if (optionvale == "W") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#Size').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnSize').css('display', 'block');
        }
        var lItmMeasurement = $.selector_cache('#ItmMeasurement').prop('value');
        if (lItmMeasurement === "0") {
            numberOfErrors++;
            $('#SpnItmMeasurement').css('display', 'block');
        }
        var lPurchaseprice = $.selector_cache('#Purchaseprice').prop('value').trim();
        if (lPurchaseprice === "") {
            numberOfErrors++;
            $('#spnPurchaseprice').css('display', 'block');
        }
        var lSellingPrice = $.selector_cache('#SellingPrice').prop('value').trim();
        if (lSellingPrice === "") {
            numberOfErrors++;
            $('#spnSellingPrice').css('display', 'block');
        }
        var lMarketPrice = $.selector_cache('#MarketPrice').prop('value').trim();
        if (lMarketPrice === "") {
            numberOfErrors++;
            $('#spnMarketPrice').css('display', 'block');
        }
        if (numberOfErrors == 0) {
            var Size = $('#Size').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#pluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var ItmNetWeight = 1;
            var Mesaurment = "Unit";
            if (ItmNetWeight != "") {
                var Array = {
                    'Size': Size,
                    'ItmNetWeight': ItmNetWeight,
                    'ItmMeasurement': Mesaurment,
                    'Purchaseprice': Purchaseprice,
                    'SellingPrice': SellingPrice,
                    'MarketPrice': MarketPrice,
                    'ProfitMargin': ProfitPercentage,
                    'ProfitPrice': ProfitPrice,
                    'TotalPrice': ActualCost,
                    'ItemId': ItemId,
                    'Weight': Weight,
                    'PluName': PluName,
                    'WastageQty': 0,
                    'WasteagePerc': 0,
                };
                sizeArr.push(Array);
                $('#Size').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                };
                $.ajax({
                    url: '/ItemMaster/HubItemsVariance/',
                    type: 'POST',
                    dataType: 'json',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        swal.fire("success", "Added Successfully!", "success").then(okay => {
                            if (okay) {
                                sizeArr.length = 0;
                                var varaintName = $('#MeasuredIn').val();
                                if (varaintName == "S") {
                                    HubSizeListDefault();
                                }
                                else if (varaintName == "W") {
                                    HubWeightListDefault();
                                }
                                else if (varaintName == "NOVAR") {
                                    HubNovartListDefault();
                                }
                            }
                        });
                    }
                });
            }
            else {
                swal.fire("error", "Something went wrong", "error");
            }
        }
        else {
            swal.fire("error", "Something went wrong", "error");
        }
    }
    if (optionvale == "NOVAR") {
        var numberOfErrors = 0;
        var tbodyCount = $('#ItemtblNov_Body tr').length;
        var lPurchaseprice = $.selector_cache('#Purchaseprice').prop('value').trim();
        if (lPurchaseprice === "") {
            numberOfErrors++;
            $('#spnPurchaseprice').css('display', 'block');
        }
        var lSellingPrice = $.selector_cache('#SellingPrice').prop('value').trim();
        if (lSellingPrice === "") {
            numberOfErrors++;
            $('#spnSellingPrice').css('display', 'block');
        }
        var lMarketPrice = $.selector_cache('#MarketPrice').prop('value').trim();
        if (lMarketPrice === "") {
            numberOfErrors++;
            $('#spnMarketPrice').css('display', 'block');
        }
        if (numberOfErrors == 0 && tbodyCount < 1) {
            var Size = "NOVAR";
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#pluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var ItmNetWeight = $('#weightportion').prop('value');
            var Mesaurment = "gm";
            if (Mesaurment == "gm" && Size == "NOVAR") {
                var Array = {
                    'Size': Size,
                    'ItmNetWeight': ItmNetWeight,
                    'ItmMeasurement': Mesaurment,
                    'Purchaseprice': Purchaseprice,
                    'SellingPrice': SellingPrice,
                    'MarketPrice': MarketPrice,
                    'ProfitMargin': ProfitPercentage,
                    'ProfitPrice': ProfitPrice,
                    'TotalPrice': ActualCost,
                    'ItemId': ItemId,
                    'Weight': Weight,
                    'PluName': PluName,
                    'WastageQty': 0,
                    'WasteagePerc': 0,
                };
                sizeArr.push(Array);
                $('#Size').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                };
                $.ajax({
                    url: '/ItemMaster/HubItemsVariance/',
                    type: 'POST',
                    dataType: 'json',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        swal.fire("success", "Added Successfully!", "success").then(okay => {
                            if (okay) {
                                sizeArr.length = 0;
                                var varaintName = $('#MeasuredIn').val();
                                if (varaintName == "S") {
                                    HubSizeListDefault();
                                }
                                else if (varaintName == "W") {
                                    HubWeightListDefault();
                                }
                                else if (varaintName == "NOVAR") {
                                    HubNovartListDefault();
                                }
                            }
                        });
                    }
                });
            }
            else {
                swal.fire("error", "something went wrong", "error");
            }
        }
        else {
            swal.fire("error", "You cannot add more than one Item!", "error").then(okay => {
                if (okay) {
                }
            });
            $('#Purchaseprice').val('');
            $('#SellingPrice').val('');
            $('#MarketPrice').val('');
        }
    }
    window.location.reload();
});

$.selector_cache('#hubSubmitVariance').on('click', function (e) {
    var MeasuredIn = $('.MeasuredIn').prop('value');
    var Varianceinfo = {
        "ItemSizeInfo": sizeArr,
        "MeasuredIn": MeasuredIn,
    };
    $.ajax({
        url: '/ItemMaster/HubItemsVariance/',
        type: 'POST',
        dataType: 'json',
        data: { 'variance': Varianceinfo },
        success: function (data) {
            swal.fire("success", "Added Successfully!", "success").then(okay => {
                if (okay) {
                    sizeArr.length = 0;
                    var varaintName = $('#MeasuredIn').val();
                    if (varaintName == "S") {
                        HubSizeListDefault();
                    }
                    else if (varaintName == "W") {
                        HubWeightListDefault();
                    }
                    else if (varaintName == "NOVAR") {
                        HubNovartListDefault();
                    }
                }
            });
        }
    });
});

$.selector_cache('#MoveToBranch').on('click', function (e) {
    var MeasuredIn = $('.MeasuredIn').prop('value');
    var ItemId = $('#ItemId').val();
    var PluName = $('#pluName').val();
    if (MeasuredIn == "S") {
        var arr = $('.mainpriceList').each(function () {
            let Size = $(this).find('.Sizep').val();
            let ItmNetWeight = $(this).find('.ItmNetWeightp').val();
            let Purchaseprice = $(this).find('.Purchasep').val();
            let SellingPrice = $(this).find('.Sellingp').val();
            let MarketPrice = $(this).find('.Marketp').val();
            let ProfitPercentage = $(this).find('.Promarginp').val();
            let ProfitPrice = $(this).find('.Profitpricep').val();
            let ActualCost = $(this).find('.ActualCostp').val();
            //return { Size, Purchaseprice };
             arr = {
                'Size': Size,
                'ItmNetWeight': ItmNetWeight,
                'ItmMeasurement': "Unit",
                'Purchaseprice': Purchaseprice,
                'SellingPrice': SellingPrice,
                'MarketPrice': MarketPrice,
                'ProfitMargin': ProfitPercentage,
                'ProfitPrice': ProfitPrice,
                'TotalPrice': ActualCost,
                'ItemId': ItemId,
                'PluName': PluName,
                'WastageQty': 0,
                'WasteagePerc': 0,
                'Weight': 1,
            };
            sizeArr.push(arr);
        }).get();
    }
    else if (MeasuredIn == "W") {
        var arr = $('.mainpriceList').each(function () {
            let Size = $(this).find('.Sizep').val();
            let ItmNetWeight = $(this).find('.ItmNetWeightp').val();
            let Purchaseprice = $(this).find('.Purchasep').val();
            let SellingPrice = $(this).find('.Sellingp').val();
            let MarketPrice = $(this).find('.Marketp').val();
            let ProfitPercentage = $(this).find('.Promarginp').val();
            let ProfitPrice = $(this).find('.Profitpricep').val();
            let ActualCost = $(this).find('.ActualCostp').val();
            let Mesaurment = $(this).find('.ItmMeasuremntp').val();
            var arr = {
                'Size': Size,
                'ItmNetWeight': ItmNetWeight,
                'ItmMeasurement': Mesaurment,
                'Purchaseprice': Purchaseprice,
                'SellingPrice': SellingPrice,
                'MarketPrice': MarketPrice,
                'ProfitMargin': ProfitPercentage,
                'ProfitPrice': ProfitPrice,
                'TotalPrice': ActualCost,
                'ItemId': ItemId,
                'PluName': PluName,
                'WastageQty': 0,
                'WasteagePerc': 0,
                'Weight': 1,
            };
            sizeArr.push(arr);
        }).get();
    }
    else {
        var arr = $('.mainpriceList').each(function () {
            let Size = "NOVAR"
            let ItmNetWeight = Size;
            let Purchaseprice = $(this).find('.Purchasep').val();
            let SellingPrice = $(this).find('.Sellingp').val();
            let MarketPrice = $(this).find('.Marketp').val();
            let ProfitPercentage = $(this).find('.Promarginp').val();
            let ProfitPrice = $(this).find('.Profitpricep').val();
            let ActualCost = $(this).find('.ActualCostp').val();
            let Mesaurment = "Unit";
            var arr = {
                'Size': Size,
                'ItmNetWeight': ItmNetWeight,
                'ItmMeasurement': Mesaurment,
                'Purchaseprice': Purchaseprice,
                'SellingPrice': SellingPrice,
                'MarketPrice': MarketPrice,
                'ProfitMargin': ProfitPercentage,
                'ProfitPrice': ProfitPrice,
                'TotalPrice': ActualCost,
                'ItemId': ItemId,
                'PluName': PluName,
                'WastageQty': 0,
                'WasteagePerc': 0,
                'Weight': 1,
            };
            sizeArr.push(arr);
        }).get();
    }      
    var Varianceinfo = {
        "ItemSizeInfo": sizeArr,
        "MeasuredIn": MeasuredIn,
    };
    $.ajax({
        url: '/ItemMaster/HubItemsVariance/',
        type: 'POST',
        dataType: 'json',
        data: { 'variance': Varianceinfo },
        success: function (data) {
            swal.fire("success", "Added Successfully!", "success").then(okay => {
                if (okay) {
                    sizeArr.length = 0;
                    var varaintName = $('#MeasuredIn').val();
                    if (varaintName == "S") {
                        HubSizeListDefault();
                        window.location.reload();
                    }
                    else if (varaintName == "W") {
                        HubWeightListDefault();
                    }
                    else if (varaintName == "NOVAR") {
                        HubNovartListDefault();
                    }
                }
            });
        }
    });
});

$.selector_cache('#AddColor').on('click', function (e) {
    var numberOfErrors = 0;
    var lColor = $.selector_cache('#ColorId').prop('value').trim();
    var lImage = $.selector_cache('#ImageId').prop('value').trim();
    if (lColor === "") {
        numberOfErrors++;
        $('#spnColorErr').css('display', 'block');
    }
    if (lImage === "") {
        numberOfErrors++;
        $('#ImageIdErr').css('display', 'block');
    }
    if (numberOfErrors == 0) {
        var color = $('#ColorId').prop('value');
        var dcolor = $('#color_' + color).text();
        var dummyimage = $('#ImageId').attr('src');
        if (dcolor == "") {
            var fileInput = $('#ImageId');
            var ItemColorInfo = new FormData();
            var fileImg = fileInput[0].files[0];
            ItemColorInfo.append('Image', fileImg);
            ItemColorInfo.append('ItemId', $('#ItemId').val());
            ItemColorInfo.append('Color', $('#ColorId').prop('value'));
            $.ajax({
                type: "POST",
                enctype: 'multipart/form-data',
                url: '/ItemMaster/UploadMultiImageAsync/',
                data: ItemColorInfo,
                processData: false,
                contentType: false,
                success: function (data) {
                    if (data == "true") {
                        var Array = {
                            "Color": color,
                        };
                        colorArr.push(Array);
                        $('#ImageId').val('');
                        $('#ColorId').val('');
                        swal.fire("success", "Added Successfully!", "success").then(okay => {
                            if (okay) {
                                Array.length = 0;
                                ColorListDefault();
                            }
                        });
                    }
                }
            });
        }
        else {
            swal.fire("Cannot insert same color!", "", "warning");
        }
    }
});

function Removecolor(param) {
    var temp = $('#Spn_' + param).text();
    var id = $('#Spncolor_' + param).text();
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/ItmDeleteColor?ColorId=' + id + '&Condition=' + temp,
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                swal.fire("success", "Remove Successfully!", "success").then(okay => {
                    if (okay) {
                        ColorListDefault();
                    }
                });
            }
        });
    }
    $('#' + param).remove();
}

$('.ColorMappingInfo').on('click', '#SubmitMapId', function () {
    var MapId = "";
    $.each($("input[name='MapName']:checked").map(function () {
        var mappingId = $(this)[0].attributes[2].nodeValue;
        var id = ($(this).val());
        MapId = mappingId;
        if (mappingId == 0) {
            var Array = {
                colorId: ($(this).val().split(',')[1]),
                priceId: ($(this).val().split(',')[0]),
                ItemId: $('#ItemId').prop('value'),
            };
            MapArr.push(Array);
        }
        else {
            var Array = {
                colorId: ($(this).val().split(',')[1]),
                priceId: ($(this).val().split(',')[0]),
                ItemId: $('#ItemId').prop('value'),
            };
            MapArr.push(Array);
        }
    }));
    var data = {
        "MappingInfo": MapArr,
    };
    if (MapId == "0") {
        $.ajax({
            type: "POST",
            url: '/ItemMaster/MappingVariance/',
            dataType: "JSON",
            data: data,
            success: function (result) {
                if (result == 0) {
                    location.reload();
                }
                else {
                    swal.fire("Something went wrong!", "", "error");
                }
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: '/ItemMaster/EditMappingVariance/',
            dataType: "JSON",
            data: data,
            success: function (result) {
                if (result == 0) {
                    location.reload();
                }
                else {
                    swal.fire("Something went wrong!", "", "error");
                }
            }
        });
    }
});

//AB0069
//Start Edit_update Size using Modal
function EditSize(param) {
    var id = param;   
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/GetSizeInfoDetails?id=' + id ,
            type: 'Get',
            success: function (result) {
                if (result.g != "") {
                    $('.PriceIds').val(result.result.priceId);
                    $('.ItemIds').val(result.result.itemId);
                    $('.size').val(result.result.size);
                    $('.purchaseprice').val(result.result.purchaseprice);
                    $('.sellingPrice').val(result.result.sellingPrice);
                    $('.marketPrice').val(result.result.marketPrice);
                    $('.marketPrice').val(result.result.marketPrice);
                    $('#NewSize').modal('show');
                }
                else {
                    alert('error');
                }
            },
            error: function () {
                alert('Server Error');
            }
        });
    }
}

$.selector_cache('#SubmitSize').on('click', function () {
    let numberOfError = 0;
    if (0 == numberOfError) {
        $.selector_cache('#SizeUpdate').submit();
    }
});
//End Edit_update Size using Modal
$('#sizeColor').on('shown.bs.tab', function () {
    var varaintName = $('#MeasuredIn').val();
    if (varaintName == "S") {
        HubSizeListDefault();
    }
    else if (varaintName == "W") {
        HubWeightListDefault();
    }
    else if (varaintName == "NOVAR") {
        HubNovartListDefault();
    }
    ColorListDefault();
});

function HubSizeListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_HubGetSizeList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#Itemtbl_Body').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function HubWeightListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_HubGetWeightQtyList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#ItemtblW_Body').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#ProductTblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function HubNovartListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_HubGetNoVarQtyList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#ItemtblNov_Body').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#ProductTblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function HubColorListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_HubGetColorList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#ColorTbl').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#ProductTblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

$('#ColorMapping').on('shown.bs.tab', function () {
    ColorSizeMappingDefault();
});

function ColorSizeMappingDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetColorSizeMapping',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('.ColorMappingInfo').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function EditPurchasePrice(param) {
    var id = param;
    var orderqty = $('#Weight').val();
    var wastePerc = $('#Wastage').val();
    var purchase = $('#Purchaseprice_' + id).val();
    var valtotal = $('#ActualCost_' + id).val();
    var selling = $('#SellingPrice_' + id).val();
    var selpercnt = $('#ProfitPercentage_' + id).val();
    var totalprice = purchase * (wastePerc / 100);
    var demtotalprice = parseFloat(totalprice) + parseFloat(purchase);
    var price = demtotalprice.toFixed(2);
    $('#ActualCost_' + id).val(price);
    var selingprice = price * selpercnt / 100;
    var valsell = parseFloat(selingprice) + parseFloat(price);
    var temp = valsell.toFixed(2);
    var diff = temp - price;
    var data = diff.toFixed(2);
    $('#ProfitPrice_' + id).val(data);
    $('#ProfitPercentage_' + id).val(data);
    $('#Purchaseprice_' + id).val(price).toFixed(2);
}

function EditSellingPrice(param){
    var id = param;
    if ($('#Purchaseprice').val() === "" || $('#Purchaseprice').val() === null) {
        $('#SpnPurchaseprice').html("This field is required").css('display', 'block');
        return false;
    } else {
        pricefn(id);
    }
    return true;
}

function EditMarketPrice(param){
    var id = param;
    var wastePerc = $('#Wastage').val();
    var marketprice = $('#MarketPrice_' + id).val();
    var totalprice = marketprice * (wastePerc / 100);
    var demtotalprice = parseFloat(totalprice) + parseFloat(marketprice);
    var price = demtotalprice.toFixed(2);
    $('#MarketPrice_' + id).val(price);
}

function pricefn(id) {
    var orderqty = $('#Weight').val();
    var wastePerc = $('#Wastage').val();
    var purchase = $('#Purchaseprice_' + id).val();
    var valtotal = $('#ActualCost_' + id).val();
    var selling = $('#SellingPrice_' + id).val();
    var totalprice = purchase * (wastePerc / 100);
    var demtotalprice = parseFloat(totalprice) + parseFloat(purchase);
    var price = demtotalprice.toFixed(2);
    var diff = selling - price;
    $('#ActualCost_' + id).val(selling);
    var percentage = diff / price * 100;
    var profitMargin = percentage.toFixed(2);
    $('#ProfitPercentage_' + id).val(profitMargin);
    var data = diff.toFixed(2);
    $('#ProfitPrice_' + id).val(data);
    var selingprice = price * profitMargin / 100;
    var valsell = parseFloat(selingprice) + parseFloat(price);
    var temp = valsell.toFixed(2);
    $('#SellingPrice_' + id).val(temp);
}

function RemovalFromHub(param) {
    var IsHubMap = "No";
    var priceId = param;
    swal({
        title: "Are you sure ?",
        text: "you want to Remove this varaince",
        showCancelButton: true,
        confirmButtonText: "Yes Remove it !",
        confirmButton: true,
    }).then((result) => {
        if (result.value) {
            var dicData = {
                PriceId: priceId,
                IsHubMap :IsHubMap,
            };
            $.ajax({
                url: '/ItemMaster/RemovalExitsVarainttoHub',
                type: 'POST',
                dataType: 'JSON',
                data: dicData,
                success: function (data) {
                    if (data && data.IsSuccess) {
                        swal.fire("success", "Remove Successfully!", "success").then(okay => {
                            if (okay) {
                                window.location.reload();
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
}

$('#SizeColor .addNewPricelsit').on('change', '#Purchaseprice', function () {
    var orderqty = $('#Weight').val();
    var wastePerc = 0;
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
    //$('#SellingPrice').val(temp);
    var diff = temp - price;
    var data = diff.toFixed(2);
    $('#ProfitPrice').val(data);
    $('#ProfitPercentage').val(data);
    $('#Purchaseprice').val(price);
});

$('#SizeColor .addNewPricelsit').on('change', '#SellingPrice', function () {
    if ($('#Purchaseprice').val() === "" || $('#Purchaseprice').val() === null) {
        $('#SpnPurchaseprice').html("This field is required").css('display', 'block');
        return false;
    } else {
        pricefn();
    }
    return true;
});

$('#SizeColor .addNewPricelsit').on('change', '#MarketPrice', function () {
    var wastePerc = $('#Wastage').val();
    var marketprice = $('#MarketPrice').val();
    var totalprice = marketprice * (wastePerc / 100);
    var demtotalprice = parseFloat(totalprice) + parseFloat(marketprice);
    var price = demtotalprice.toFixed(2);
    $('#MarketPrice').val(price);
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
    $('#ActualCost').val(selling);
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

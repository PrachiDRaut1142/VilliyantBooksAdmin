var sizeArr = [];
var priceArr = [];
var colorArr = [];
var ImageArr = [];
var MapArr = [];
!(function () {
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    $(".closeLang").on('click', function (e) {
        $("#languageitemname").val('');
        $("#Descriptionlanguage").val('')
        $("#Itemused").val('');
        $("#Addlanguagetext").val(0);
        $('#spnlanguageitemname').css('display', 'none');
        $('#Spnlanguagetext').css('display', 'none');
    });

    $(".closeVariance").on('click', function (e) {
        closeVariance();
    });
    function closeVariance() {
        $("#VSize").val('');
        $("#Purchaseprice").val('')
        $("#SellingPrice").val('');
        $("#MarketPrice").val('');
        $("#Barcodest").val('');
        $("#ItmMeasurement").val(0);
        $("#spnVSize").css('display', 'none');
        $("#SpnItmMeasurement").css('display', 'none');
        $("#spnPurchaseprice").css('display', 'none');
        $("#spnSellingPrice").css('display', 'none');
        $("#spnMarketPrice").css('display', 'none');
        $("#spnBarcodest").css('display', 'none');
    }
    //$('#ImagePath').on('change', function (e) {
    //    $('#Spnpluname1').css('display', 'none');
    //});
    $('#pluName').on('keypress', function () {
        $('#Spnpluname').css('display', 'none');
    });
    $('#PluCode').on('keypress', function () {
        $('#Spnplucode').css('display', 'none');
        $('#SpnUniqueplucode').css('display', 'none');
    });
    //$('#imagecdnpathvariance').on('keyup', function () {
    //    $('#Spnpluname1').css('display', 'none');
    //});
    $("#VSize").on('keydown', function (e) {
        $("#spnVSize").css('display', 'none');
    });
    $("#ItmMeasurement").on('change', function (e) {
        $("#SpnItmMeasurement").css('display', 'none');
    });
    $("#Purchaseprice").on('keypress', function (e) {
        $("#spnPurchaseprice").css('display', 'none');
    });
    $("#SellingPrice").on('keypress', function (e) {
        $("#spnSellingPrice").css('display', 'none');
    });
    $("#MarketPrice").on('keypress', function (e) {
        $("#spnMarketPrice").css('display', 'none');
    });
    $("#Barcodest").on('keypress', function (e) {
        $("#spnBarcodest").css('display', 'none');
    });
    //$("#ImagePath").on('click', function (e) {
    //    $("#imagecdnpathvariance").val('');
    //});
    //$('#cancel__img').on('click', function () {
    //    $("#item-img-output1").attr("src", '/img/no-image.png');
    //    $("#imagecdnpathvariance").val('');
    //});
    $('.NetWeight').on('change', function (e) {
        var NetWeight = $(this).val();
        var demtotalprice = parseFloat(NetWeight);
        var price = demtotalprice.toFixed(2);
        $('.NetWeight').val(price);
    });
    //$('.price').keypress(function (e) {
    //    var verified = (e.which == 8 || e.which == 110 || e.which == undefined || e.which == 0) ? null : String.fromCharCode(e.which).match(/[^0-9]/);
    //    if (verified) { e.preventDefault(); }
    //});
    if ($('#FoodType').val() == 1 || $('#FoodType').val() == 0 || $('#FoodType').val() == 3) {
        $('#FoodSubType').css('display', 'none');
    }

    if ($('#IsSpecialDay').prop('value') == "Yes") {
        $('#DaysCounter').show();
    }
    else {
        $('#DaysCounter').hide();
    }

    $('#IsSpecialDay').on('change', function () {
        var IsSpecialDay = this.value;
        var avialbleday = $('#AvailableDay').val();
        if (IsSpecialDay == "Yes") {
            var avialbleday = $('#avaldayno').val();
            $('#AvailableDay option[value=' + avialbleday + ']').attr('selected', 'selected');
            $('#DaysCounter').show();
        }
        if (IsSpecialDay == "No") {
            $('#AvailableDay option[value=' + avialbleday + ']').removeAttr('selected', 'selected');
            $('#AvailableDay option[value="7"]').attr('selected', 'selected');
            $('#DaysCounter').hide();
        }
    });

    if ($('#MealTimeType').prop('value') == "Y") {
        $('.Mealtimezone').css('display', 'flex');
    }
    else {
        $('.Mealtimezone').css('display', 'none');
    }

    $('#MealTimeType').on('change', function () {
        var IsMealType = this.value;
        if ("Y" == IsMealType) {
            var dbStartTime = $('#StartTimeDb').val();
            var dbEndTime = $('#EndTimeDb').val();
            $('#StartTime').val(dbStartTime);
            $('#EndTime').val(dbEndTime);
            $('.Mealtimezone').css('display', 'flex');
        }
        else {
            $('.Mealtimezone').css('display', 'none');
        }
    });

    //$('#FoodType').on('change', function () {
    //    var foodTypevalue = this.value;
    //    FoodsubType(foodTypevalue);
    //});

    //var foodType = $('#FoodType').val();
    //var foodSubType = $('#FoodSubTypesData').val();
    //FoodsubType(foodType);
    //function FoodsubType(foodType) {
    //    $.ajax({
    //        url: "/ItemMaster/GetsubfoodList?foodType=" + foodType,
    //        type: "POST",
    //        success: function (foodSubTypeList) {
    //            if (foodSubTypeList.length > 0) {
    //                var html = "";
    //                $.each(foodSubTypeList, function (index, value) {
    //                    if (foodSubType == value.value) {
    //                        html += '<option value="' + value.value + '" selected="selected">' + value.text + '</option>';
    //                    }
    //                    else {
    //                        html += '<option value="' + value.value + '">' + value.text + '</option>';
    //                    }
    //                });
    //                $('#FoodSubType').html(html);
    //            }
    //            else {
    //                //swal.fire('Error!', 'There was some error. Try again later.', 'error');
    //            }
    //        }
    //    });
    //}

    $('#MainCate').on('change', function () {
        var maincategoryId = this.value;
        MCategory(maincategoryId);
    });

    $('#Category').on('change', function () {
        var categoryId = this.value;
        MSubCategory(categoryId);
    });

    var MainCate = $('#MainCate').val();
    var Category = $('#Cate').val();
    var Subcate = $('#SubCate').val();

    MCategory(MainCate);
    MSubCategory(Category);

    function MCategory(MainCate) {
        $.ajax({
            url: "/ItemMaster/GetCategorylist?mainCatId=" + MainCate,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    $.each(categoryList, function (index, value) {
                        if (Category == value.value) {
                            html += '<option value="' + value.value + '" selected="selected">' + value.text + '</option>';
                        }
                        else {
                            html += '<option value="' + value.value + '">' + value.text + '</option>';
                        }
                    });
                    $('#Category').html(html);
                }
                else {
                    //  swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    }

    function MSubCategory(Category) {
        $.ajax({
            url: "/ItemMaster/GetSubCategoryList?CategoryId=" + Category,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    $.each(categoryList, function (index, value) {
                        if (Subcate == value.value) {
                            html += '<option value="' + value.value + '" selected="selected">' + value.text + '</option>';
                        }
                        else {
                            html += '<option value="' + value.value + '">' + value.text + '</option>';
                        }
                    });
                    $('#SubCategory').html(html);
                }
                else {
                    // swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    }

    //Featured Itm
    var test = $('#featuretest').val();
    if (test === "Y") {
        $("#FeaturedCheckbox").prop('checked', true);
    }
    else {
        $("#FeaturedCheckbox").prop('checked', false);
    }
    $('#FeaturedCheckbox').on('click', function () {
        var pluname = $('#pluName').val();
        var sVisible = $(this).prop('checked');
        var itemId = $('#ItemId').val();
        var Id = 0;
        var type = 1;
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

    //Coupen Allowed Itm
    var test = $('#Coupentest').val();
    if (test === "1") {
        $("#CoupenCheckbox").prop('checked', true);
    }
    else {
        $("#CoupenCheckbox").prop('checked', false);
    }

    $('#CoupenCheckbox').on('click', function () {
        var pluname = $('#pluName').val();
        var sVisible = $(this).prop('checked');
        var itemId = $('#ItemId').val();
        var Id = 0;
        var type = 1;
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

    //Checked Itm
    var test1 = $('#checksptest').val();
    if (test1 === "Y") {
        $("#ChckSpecialCheckbox").prop('checked', true);
    }
    else {
        $("#ChckSpecialCheckbox").prop('checked', false);
    }

    $('#ChckSpecialCheckbox').on('click', function () {
        var pluname = $('#pluName').val();
        var sVisible = $(this).prop('checked');
        var itemId = $('#ItemId').val();
        var Id = 0;
        var type = 1;
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

    //Availabity  Itm
    var test1 = $('#avaliblitytest').val();
    if (test1 === "Y") {
        $("#AvailabilityCheckbox").prop('checked', true);
    }
    else {
        $("#AvailabilityCheckbox").prop('checked', false);
    }

    $('#AvailabilityCheckbox').on('click', function () {
        var pluname = $('#pluName').val();
        var sVisible = $(this).prop('checked');
        var itemId = $('#ItemId').val();
        var Id = 0;
        var type = 1;
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

    ///function of price
    $('#SizeColor').on('change', '#Purchaseprice', function () {
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
        var diff = temp - price;
        var data = diff.toFixed(2);
        $('#ProfitPrice').val(data);
        $('#ProfitPercentage').val(data);
        $('#Purchaseprice').val(price);
    });

    $('#Wastage').on('change', function () {
        pricefn();
    });

    $('#SizeColor').on('change', '#SellingPrice', function () {
        if ($('#Purchaseprice').val() === "" || $('#Purchaseprice').val() === null) {
            $('#SpnPurchaseprice').html("This field is required").css('display', 'block');
            return false;
        } else {
            pricefn();
        }
        return true;
    });

    $('#SizeColor').on('change', '#MarketPrice', function () {
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
        var hubId = $('#hubId').text();
        var hubIdsplit = hubId.split('0');
        var ItemIdsplit = ItemId.split('0');
        var type = hubIdsplit[1];
        var Id = ItemIdsplit[1];

        $.ajax({
            url: '/ItemMaster/ApprovetheItem?itemId=' + ItemId + '&approval=' + Approval + '&type=' + type + '&Id=' + Id,
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
        var keyName = $('#DefaultImg_1').val();
        keyName = keyName.split('HurTex/')[1];
        $.ajax({
            url: '/ItemMaster/GetDowload?name=' + keyName,
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
        keyName = keyName.split('HurTex/')[1];
        $.ajax({
            url: '/ItemMaster/DeleteImageSrc?key=' + keyName,
            type: 'POST',
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                if (data === true) {
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
            $('#SpnUniqueplucode').html("Plucode should be unique").css('display', 'none');
            nErrorCount += 1;
        }
        //if ($('#PluCode').val() === "" || $('#PluCode').val() === null) {
        //    $('#Spnplucode').html("This field is required").css('display', 'none');
        //    nErrorCount += 1;
        //}
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
        if ($('#FoodType').val() === "0" || $('#FoodType').val() === null) {
            $('#SpnFoddType').html("This field is required").css('display', 'block');
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
            $('#NetWeight').val('01.00');
        }
        if ($('#Relatiopship').val() === "2") {
            if ($('#ParentItemId').val() == null) {
                $('#SpnParentItemId').html("This field is required").css('display', 'block');
                nErrorCount += 1;
            }
        }
        if ($('#soldId').val() === '2') {
            if ($('#MQOId').val() === "" || $('#MQOId').val() === null) {
                $('#SpnMQOId').html("This field is required").css('display', 'block');
                nErrorCount += 1;
            }
            if ($('#MQOId').val() <= 0) {
                $('#SpnMQOId').html("Enter natural value").css('display', 'block');
                nErrorCount += 1;
            }
        } else {

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

    //Plucode Unique
    $('#PluCode').on('change', function () {
        $.ajax({
            url: "/ItemMaster/CheckUniquePlucode?PluCode=" + this.value,
            type: "POST",
            success: function (result) {
                var plucode = $('#PluCodehidden').val();
                if (result.Data == "" || result.Data == plucode) {
                    $('#SpnUniqueplucode').html("Plucode should be unique").css('display', 'none');
                }
                else {
                    $('#SpnUniqueplucode').html("Plucode should be unique").css('display', 'block');
                    $('#Spnplucode').html("This field is required").css('display', 'none');
                }
            }
        });
    });
    $('#Relatiopship').on('change', function () {
        var value = $(this).val();
        var html = "";
        if (value == 2) {
            $.ajax({
                url: '/Purchase/ItemSelectList',
                dataType: "json",
                success: function (result) {
                    html = '<option selected disabled>Select Item</option >';
                    if (result.Data != "") {
                        $('#ParentItemId').html(html);
                        for (var i = 0; i < result.Data.length; i++) {
                            var Items = result.Data[i];
                            $('#ParentItemId').append('<option value="' + Items.itemId + '">' + Items.pluName + '</option>');
                        }
                    }
                }
            });
        }
        else {
            $('#ParentItemId').html(html);
        }
    });
    $('#Relatiopship').on('change', function () {
        $('#SpnParentItemId').html("This field is required").css('display', 'none');
    });
    //$('.clear-cls').on('change', function () {
    //    $('.error').css('display', 'none');
    //});
    $('#AvailabilityCheckbox').on('click', function () {
        var pluname = $('#pluName').val();
        var sVisible = $(this).prop('checked');
        var itemId = $('#ItemId').val();
        $.ajax({
            url: "/ItemMaster/SeasonUpdate?Season=" + sVisible + "&itemId=" + itemId,
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
    $('#IGST').on('change', function () {
        var value = $(this).val();
        var scst = $(this).find(':selected').attr('data-scst');
        var cgst = $(this).find(':selected').attr('data-cgst');
        $('#SGST').prop('value', scst);
        $('#CGST').prop('value', cgst);
    });

    var taxType = $('#taxType').val();
    var calcuationTaxtype = $('#calcuationTaxtype').val();
    if (taxType == "1" && calcuationTaxtype == "2") {
        $('#TaxDivId').css('display', 'block');
    }
    if (taxType == "2" && calcuationTaxtype == "1") {
        $('#vatTaxDivId').css('display', 'block');
    }

    var VBMeasuredIn = $('#VBMeasuredIn').text();
    var ItemSizecount = $('#ItemSizecount').text();
    if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
        $('#MeasuredIn').prop("disabled", true);
    }
    else {
        $('#MeasuredIn').prop("disabled", false);
    }

    if ($('#pathCheckbox').prop("checked") === true) {
        //var pathCheck = $('#pathCheck').val("1");
        /*alert(pathCheck);*/
        $('.Upload_Image_Path').css('display', 'flex');
        $('.Upload_Image').css('display', 'none');
    }
    else {
        //var pathCheck = $('#pathCheck').val("0");
        /*alert(pathCheck);*/
        $('.Upload_Image').css('display', 'flex');
        $('.Upload_Image_Path').css('display', 'none');
    }
    $('#pathCheckbox').on('click', function () {
        var sVisible = $(this).prop('checked');
        if (sVisible === true) {
            $('.Upload_Image_Path').css('display', 'flex');
            $('.Upload_Image').css('display', 'none');
        } else {
            $('.Upload_Image').css('display', 'flex');
            $('.Upload_Image_Path').css('display', 'none');
        }
    });
    if ($('#MutipleCheckbox').prop("checked") === true) {


        $('.Upload_Image_multiple').css('display', 'flex');
    }
    else {
        //var pathCheck = $('#pathCheck').val("0");
        /*alert(pathCheck);*/
        $('.Upload_Image_multiple').css('display', 'none');

    }
    $('#MutipleCheckbox').on('click', function () {
        var sVisible = $(this).prop('checked');
        if (sVisible === true) {
            $('.Upload_Image_multiple').css('display', 'flex');

        } else {

            $('.Upload_Image_multiple').css('display', 'none');
        }
    });
})();

if ($('#taxId').val() == "Yes") {
    $('#TaxDivId').css('display', 'none');
}
else {
    $('#TaxDivId').css('display', 'block');
}

$('#Barcodest').on('change', function () {
    $.ajax({
        url: "/ItemMaster/CheckUniqueBarcode?Barcode=" + this.value,
        type: "POST",
        success: function (result) {
            if (result.Data != "") {
                swal.fire("error", "Barcode Id Already Exists!", "error").then(okay => {
                    if (okay) {
                        $('#Barcodest').val('');
                    }
                });
            }
        }
    });
});

$('#taxId').on('change', function () {
    var value = $(this).val();
    var dbhsn = $('#dbhsn_code').val();
    var dbgstper = $('#gst_perdb').val();
    var dbsgstper = $('#sgst_perdb').val();
    var dbcgstper = $('#cgst_perdb').val();
    if (value == "No") {
        if (dbhsn != "" && dbhsn != null) {
            $('#HSN_Code').val(dbhsn);
            if (dbgstper != "" && dbgstper != null) {
                $('#IGST').val(dbgstper);
                $('#SGST').val(dbsgstper);
                $('#CGST').val(dbcgstper);
            }
            $('#TaxDivId').css('display', 'block');
        }
    }
    else {
        $('#HSN_Code').val('');
        $('#IGST').val(1);
        $('#SGST').val(0);
        $('#CGST').val(0);
        $('#TaxDivId').css('display', 'none');
    }
});

if ($('#taxId').val() == "No") {
    if ($('#HSN_Code').val() === "" || $('#HSN_Code').val() === null) {
        $('#SpnHSN_Code').html("This field is required").css('display', 'block');
        nErrorCount += 1;
    }
    if ($('#IGST').val() === "" || $('#IGST').val() === null) {
        $('#SpnIGST').html("This field is required").css('display', 'block');
        nErrorCount += 1;
    } if ($('#SGST').val() === "" || $('#SGST').val() === null) {
        $('#SpnSGST').html("This field is required").css('display', 'block');
        nErrorCount += 1;
    }
    if ($('#CGST').val() === "" || $('#CGST').val() === null) {
        $('#SpnCGST').html("This field is required").css('display', 'block');
        nErrorCount += 1;
    }
}


$("#DisplayWithImg").on('click', function () {
    // Check if the checkbox is checked
    if ($(this).prop('checked')) {
        // Checkbox is checked, update the NameImage variable to 'on'
        var NameImage = true;
        $('#DisplayWithImg').val(NameImage)
    } else {
        // Checkbox is unchecked, update the NameImage variable to 'off'
        var NameImage = false;
        $('#DisplayWithImg').val(NameImage);  // Handle accordingly (if needed)
    }
    if (NameImage == true) {
        $('.Upload_Image_Path').css('display', 'block');
        $('.Upload_Image').css('display', 'block');
        $('.upload-variance').css('display', 'block');
    } else {
        $('.Upload_Image_Path').css('display', 'none');
        $('.Upload_Image').css('display', 'none');
        $('.upload-variance').css('display', 'none');
    }

    // You can now use the NameImage variable in other parts of your code or pass it to a function.
    // For example, yourFunction(NameImage);
});
$.selector_cache('#AddSize').on('click', function (e) {
    var optionvale = $('#MeasuredIn').prop('value');

    if (optionvale == "S") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');            
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = 'Size';
            var files1 = $('#ImagePaths').prop("files");
            var files = $('#item-img-output1').prop("files");
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('#Barcodest').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();

                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                   
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
    }
    if (optionvale == "W") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = "Weight";
            var files = $('#ImagePath').prop("files");
            var Barcode = $('#Barcodest').prop('value');
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#Barcodest').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();
                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');
        //}
        if (numberOfErrors == 0) {
            var Size = "NOVAR";
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = $('#weightportion').prop('value');
            var files = $('#ImagePath').prop("files");
            var Mesaurment = "NOVAR";
            if (Size == "NOVAR") {
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                    'WasteagePerc': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#Barcodest').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();
                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "You cannot add more than one Item!", "error").then(okay => {
        //        if (okay) {
        //        }
        //    });
        //    $('#Purchaseprice').val('');
        //    $('#SellingPrice').val('');
        //    $('#MarketPrice').val('');
        //}
    }
    if (optionvale == "Q") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');            
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = 'Q';
            //var files = $('#ImagePath').prop("files");
            var files = $('#item-img-output1').prop("files");
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('#Barcodest').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();

                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
    }
    if (optionvale == "V") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');            
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = 'V';
            //var files = $('#ImagePath').prop("files");
            var files = $('#item-img-output1').prop("files");
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('#Barcodest').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();

                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
    }
    if (optionvale == "D") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');            
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = 'D';
            //var files = $('#ImagePath').prop("files");
            var files = $('#item-img-output1').prop("files");
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('#Barcodest').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();

                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }

                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
    }
    if (optionvale == "L") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');            
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = 'L';
            //var files = $('#ImagePath').prop("files");
            var files = $('#item-img-output1').prop("files");
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('#Barcodest').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = $('.MeasuredIn').prop('value');
                var displayWithImg = $('#DisplayWithImg').val();

                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
    }
    if (optionvale == "Open") {
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');
        //}
        if (numberOfErrors == 0) {
            var Size = "Open";
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = $('#weightportion').prop('value');
            var files = $('#ImagePath').prop("files");
            var Mesaurment = "Open";
            if (Size == "Open") {
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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,
                    'WasteagePerc': 0,
                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#Barcodest').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = 'Open';
                var displayWithImg = $('#DisplayWithImg').val();
                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,
                };

                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "You cannot add more than one Item!", "error").then(okay => {
        //        if (okay) {
        //        }
        //    });
        //    $('#Purchaseprice').val('');
        //    $('#SellingPrice').val('');
        //    $('#MarketPrice').val('');
        //}
    }
    if (optionvale == "C") {
        var numberOfErrors = 0;
        var lSize = $.selector_cache('#VSize').prop('value').trim();
        if (lSize === "") {
            numberOfErrors++;
            $('#spnVSize').css('display', 'block');
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
        var lBarcode = $.selector_cache('#Barcodest').prop('value').trim();
        if (lBarcode === "") {
            numberOfErrors++;
            $('#spnBarcodest').css('display', 'block');
        }
        //if (($('#imagecdnpathvariance').val() == "" || $('#imagecdnpathvariance').val() == null) &&
        //    ($("#item-img-output1").attr('src') == '' || $("#item-img-output1").attr('src') == null || $("#item-img-output1").attr('src') == '/img/no-image.png')) {
        //    numberOfErrors++;
        //    $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');            
        //}
        if (numberOfErrors == 0) {
            var Size = $('#VSize').prop('value');
            var Purchaseprice = $('#Purchaseprice').prop('value');
            var SellingPrice = $('#SellingPrice').prop('value');
            var MarketPrice = $('#MarketPrice').prop('value');
            var ProfitPercentage = $('#ProfitPercentage').prop('value');
            var ProfitPrice = $('#ProfitPrice').prop('value');
            var ActualCost = $('#ActualCost').prop('value');
            var ItemId = $('#ItemId').prop('value');
            var PluName = $('#PluName').prop('value');
            var Weight = $('#Weight').prop('value');
            var imagecdnpathvariance = $('#imagecdnpathvariance').prop('value');
            var Barcode = $('#Barcodest').prop('value');
            var ItmNetWeight = '01.00';
            var Mesaurment = $('#color-picker').prop('value');
        

            var files = $('#item-img-output1').prop("files");

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
                    'Barcode': Barcode,
                    'Weight': Weight,
                    'imagecdnpathvariance': imagecdnpathvariance,
                    'PluName': PluName,
                    'WastageQty': 0,

                };
                sizeArr.push(Array);
                $('#VSize').val('');
                $('#ItmNetWeight').val('');
                $('#Purchaseprice').val('');
                $('#SellingPrice').val('');
                $('#MarketPrice').val('');
                $('#ProfitPercentage').val('');
                $('#ProfitPrice').val('');
                $('#ActualCost').val('');
                $('#Barcodest').val('');
                $("#imagecdnpathvariance").val('');
                $('.error-empty-cls').css('display', 'none');
                var MeasuredIn = 'C';
                var displayWithImg = $('#DisplayWithImg').val();

                var Varianceinfo = {
                    "ItemSizeInfo": sizeArr,
                    "MeasuredIn": MeasuredIn,
                    "DisplayWithImg": displayWithImg,


                };
                formData = new FormData();
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                }
                $.ajax({
                    url: '/ItemMaster/ItemsVariance/',
                    type: 'POST',
                    data: { 'variance': Varianceinfo },
                    success: function (data) {
                        formData.append("variance.PriceId", data.data);
                        formData.append("variance.ImagePath", files);
                        $.ajax({
                            type: 'POST',
                            url: '/ItemMaster/UploadVaraintImages',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                var VBMeasuredIn = $('#VBMeasuredIn').text();
                                var ItemSizecount = $('#ItemSizecount').text();
                                if (VBMeasuredIn == 'S' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'W' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Q' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'V' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'D' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'L' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'NOVAR' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'Open' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else if (VBMeasuredIn == 'C' && ItemSizecount > 0) {
                                    $('#MeasuredIn').prop("disabled", true);
                                }
                                else {
                                    $('#MeasuredIn').prop("disabled", false);
                                }
                                $('#item-img-output1').attr('src', '/img/no-image.png')
                                //$('#item-img-output1').css('width', '0px');
                                //$('#item-img-output1').css('height', '0px');
                                $('#ImagePath').val('');
                                swal.fire("success", "Added Successfully!", "success").then(okay => {
                                    if (okay) {
                                        $('#addProductVari').modal('hide');
                                        sizeArr.length = 0;
                                        $('#MeasuredIn').prop("disabled", true);
                                        var varaintName = $('#MeasuredIn').val();
                                        if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
                                            SizeListDefault();
                                        }
                                        else if (varaintName == "C") {
                                            WeightListDefault();
                                        }
                                        else if (varaintName == "NOVAR" || varaintName == "Open") {
                                            NovartListDefault();
                                        }
                                    }
                                });
                            },
                            error: function (error) {
                                window.location.reload();
                            }
                        });
                    }
                });
            }
        }
        //else {
        //    swal.fire("error", "Something went wrong", "error");
        //}
    }

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
                        //window.location.reload();
                        window.location.href = "/ItemMaster/_GetSizeList";
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
    //if (sizeArr.length == 0) {
    //    $('#SubmitVariance').prop('disabled', true);
    //}
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
                            window.location.href = "/ItemMaster/_GetSizeList";
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
    //if (sizeArr.length == 0) {
    //    $('#SubmitVariance').prop('disabled', true);
    //}
    window.location.reload();
}

var optionvale = $('#MeasuredIn').prop('value');
if (optionvale == "S" || optionvale == "Q" || optionvale == "V" || optionvale == "D" || optionvale == "L" || optionvale == "W") {
    $('.SizeRow').css('display', 'flex');
}
if (optionvale == "C") {
    $('.WeightRow').css('display', 'flex');
}

$("#MeasuredIn").change(function () {
    var foodType = $('#FoodType').val();
    var optionvale = $('#MeasuredIn').prop('value');
    var display = $('#DisplayWithImg').val();
    if (optionvale != "S" && optionvale != "W") {
        $('.RowSizeandWegitMap').css('display', 'none');
    }
    if (optionvale == "S" || optionvale == "Q" || optionvale == "V" || optionvale == "D" || optionvale == "L" || optionvale == "W") {
        $('.RowSizeandWegitMap').css('display', 'block');
        $('.hasNovaraint').css('display', 'flex');
        $('.sizedetails').css('display', 'block');
        $('.SizeRow').css('display', 'flex');
        $('.wegihtate').css('display', 'none');
        $('.wegihtatel').css('display', 'none');
        $('.WeightRow').css('display', 'none');
        $('.VaraintRow').css('display', 'none');
        $('.error-empty-cls').css('display', 'none');
    }
    if (optionvale == "C") {
        $('.RowSizeandWegitMap').css('display', 'block');
        $('.hasNovaraint').css('display', 'flex');
        $('.sizedetails').css('display', 'none');
        $('.SizeRow').css('display', 'none');
        $('.wegihtate').css('display', 'flex');
        $('.wegihtatel').css('display', 'block');
        $('.WeightRow').css('display', 'flex');
        $('.VaraintRow').css('display', 'none');
        $('.error-empty-cls').css('display', 'none');
        $('#nonvegconsumption').css('display', 'none');
    }
    if (optionvale == "NOVAR" || optionvale == "Open") {
        $('.RowSizeandWegitMap').css('display', 'block');
        $('.hasNovaraint').css('display', 'none');
        $('.wegihtate').css('display', 'none');
        $('.wegihtatel').css('display', 'none');
        $('.sizedetails').css('display', 'none');
        $('.SizeRow').css('display', 'none');
        $('.WeightRow').css('display', 'none');
        $('.VaraintRow').css('display', 'flex');
        $('.error-empty-cls').css('display', 'none');
    }
    if (display == true) {
        $('.Upload_Image_Path').css('display', 'block');
        $('.Upload_Image').css('display', 'block');
        $('.upload-variance').css('display', 'block');
    }
    else {
        $('.Upload_Image_Path').css('display', 'none');
        $('.upload-variance').css('display', 'none');
        $('.Upload_Image').css('display', 'none');
    }
}).change();

//$.selector_cache('#SubmitVariance').on('click', function (e) {
//    var MeasuredIn = $('.MeasuredIn').prop('value');
//    var Varianceinfo = {
//        "ItemSizeInfo": sizeArr,
//        "MeasuredIn": MeasuredIn,
//    };
//    $.ajax({
//        url: '/ItemMaster/ItemsVariance/',
//        type: 'POST',
//        dataType: 'json',
//        data: { 'variance': Varianceinfo },
//        success: function (data) {
//            swal.fire("success", "Remove Successfully!", "success").then(okay => {
//                if (okay) {
//                    var varaintName = $('#MeasuredIn').val();
//                    if (varaintName == "S") {
//                        SizeListDefault();
//                    }
//                    else if (varaintName == "W") {
//                        WeightListDefault();
//                    }
//                    else if (varaintName == "NOVAR") {
//                        NovartListDefault();
//                    }
//                }
//            });
//        }
//    });
//});

$(":file").change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        var iconImg = $('#item-img-output1');
        var iconlength = iconImg[0].files.length;
        if (iconlength == 0) {
            reader.onload = iconImage;
        }
        else {
            reader.onload = imageIsLoaded;
        }
        reader.readAsDataURL(this.files[0]);
    }
});

function imageIsLoaded(e) {
    $('#item-img-output1').attr('src', e.target.result);
};

$.selector_cache('#AddColor').on('click', function (e) {
    var numberOfErrors = 0;
    var lColor = $.selector_cache('#ColorId').prop('value').trim();
    var lImage = $.selector_cache('#ImageId').prop('value').trim();
    if (lColor === "") {
        numberOfErrors++;
        $('#spnColorErr').css('display', 'block');
    }
    //if (lImage === "") {
    //    numberOfErrors++;
    //    $('#ImageIdErr').css('display', 'block');
    //}
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

function EditSize(param) {
    var id = param;
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/GetSizeInfoDetails?id=' + id,
            type: 'Get',
            success: function (result) {
                if (result.g != "") {
                    $('.PriceIds').val(result.result.priceId);
                    $('.ItemIds').val(result.result.itemId);
                    $('.size').val(result.result.size);
                    $('.purchaseprice').val(result.result.purchaseprice);
                    $('.sellingPrice').val(result.result.sellingPrice);
                    $('.marketPrice').val(result.result.marketPrice);
                    $('.nonveg').val(result.result.itmNetWeight);
                    $('#ColorCode').val(result.result.measurement);
                    $('#Barcodepricr').val(result.result.barcode);
                    $('#Measurement').val(result.result.colorCode);
                    if (result.result.imagecdnpathvariance == '' || result.result.imagecdnpathvariance == null) {
                        $("#item-img-output").attr("src", 'https://freshlo.oss-ap-south-1.aliyuncs.com/soul%26soul/Product-image/' + result.result.priceId + '.png');
                        // $("#item-img-output").attr("src", 'http://snsadmin.cxengine.net/img/no-image.png');
                    }
                    else {
                        $("#item-img-output").attr("src", result.result.imagecdnpathvariance);
                    }
                    var result1 = result.result.priceId + '.png';
                    //if (result.result.measurement == "Unit") {
                    //    $('#nonvegsize').css('display', 'none');
                    //}
                    //else {
                    //    $('#nonvegsize').css('display', 'block');
                    //}
                    if (result.result.colorCode == "C") {
                        $('#clorcd').css('display', 'flex');
                    }
                    else {
                        $('#clorcd').css('display', 'none');
                    }
                    $('#NewSize').modal('show');
                    //window.location.href = "/ItemMaster/_GetSizeList";
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
    //if (varaintName == "S") {
    //    SizeListDefault();
    //}
    //else if (varaintName == "W") {
    //    WeightListDefault();
    //}
    //else if (varaintName == "NOVAR") {
    //    NovartListDefault();
    //}
    if (varaintName == "S" || varaintName == "Q" || varaintName == "V" || varaintName == "D" || varaintName == "L" || varaintName == "W") {
        SizeListDefault();
    }
    else if (varaintName == "C") {
        WeightListDefault();
    }
    else if (varaintName == "NOVAR" || varaintName == "Open") {
        NovartListDefault();
    }
    ColorListDefault();
});

function SizeListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetSizeList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#Itemtbl_Body').html(data);
            else
                /*swal.fire("error", "Something went wrong", "error");*/
                $('#ProductTblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            /* swal.fire("error", "Something went wrong", "error");*/
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function WeightListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetWeightQtyList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#ItemtblW_Body').html(data);
            else
                /*swal.fire("error", "Something went wrong", "error");*/
                $('#ProductTblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            /*swal.fire("error", "Something went wrong", "error");*/
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function NovartListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetNoVarQtyList',
        data: { id: Id },
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#ItemtblNov_Body').html(data);
            else
                /*swal.fire("error", "Something went wrong", "error");*/
                $('#ProductTblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //    swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

function ColorListDefault() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetColorList',
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

$('#MappedItems').on('shown.bs.tab', function () {
    MappedItemList();
});

function MappedItemList() {
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetMappedItemsList',
        data: { id: Id },
        cache: false,
        type: "GET",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#MappedItemBody').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

//function previewFile(input) {
//    var imagePath = $("#imagecdnpathvariance").val();
//    if (imagePath != '') {
//        $("#item-img-output1").attr("src", imagePath);
//    }
//    else {
//        $("#item-img-output1").attr("src", 'item-img-output');
//    }
//}

function EditItemLanguage(param) {
    var id = param;
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/GetItemLanguageById?id=' + id,
            type: 'Get',
            success: function (result) {
                console.log(result);
                if (result.g != "") {
                    $('#AddlanguageName').val(result.languageName).prop('selected', true);;
                    $('.ItemIdsss').val(result.itemId);
                    $('#Descriptionlanguage').val(result.descriptionlanguage);
                    $('#languageitemname').val(result.itemNameLanguage);
                    $('#Itemused').val(result.itemused);
                    $('#Id').val(result.id);
                    $('#Id1').val(result.id1);
                    $('#addLangMd').modal('show');
                }
                else {
                    swal.fire("error", "Something went wrong", "error");
                }
            }
        });
    }
    else {
        swal.fire("error", "Id not Found", "error");
    }
}
function DeleteItemLanguage(param) {
    param1 = param.split(",");
    var id = param1[0];
    var name = param1[1];
    if (id != "") {
        $.ajax({
            url: '/ItemMaster/DeleteItemLanguageByitemId?id=' + id,
            type: 'Get',
            success: function (result) {
                if (result.g != "") {
                    swal.fire("success", name + " Deleted", "Success");
                }
                else {
                    swal.fire("error", "Something went wrong", "error");
                }
            }
        });
    }
    else {
        swal.fire("error", "Id Not Found", "error");
    }
}



$('#SelectProductType').on('Change', function () {
    var data = $('#SelectProductType').val();
    alert(data);
})

$("#AddMappedItem").on('click', function () {
    var mappedItemId = $('#MappedItem').val();
    var ItemId = $('#ItemId').val(); // Corrected the variable name to ItemId

    $.ajax({
        url: "/ItemMaster/UpdatemappedVarient?mappedItemId=" + mappedItemId + "&itemId=" + ItemId, // Corrected the variable name to ItemId
        type: "GET",
        success: function (value) {
            if (value.IsSuccess === true) {
                MappedItemList();
                if (sVisible === true) {
                    swal(pluname + "is mapped ", "", "success");
                } else {
                    swal(pluname + " is no mapped", "", "warning");
                }
            }
        }
    });
});


//$(document).ready(function () {
//    // Attach click event to the button
//    $("#uploadPdf").click(function () {
//        // Create an input element of type file
//        var input = document.createElement('input');
//        input.type = 'file';

//        // Set the accept attribute to allow only PDF files
//        input.accept = '.pdf';

//        // Trigger a click on the file input
//        input.click();

//        // Handle file selection
//        input.addEventListener('change', function () {
//            // Access the selected file
//            var selectedFile = input.files[0];
//            $("#uploadedPdf").val(selectedFile);
//            alert(selectedFile);
//            // Check if a file is selected
//            if (selectedFile) {
//                // Log the file details (you can customize this part)
//                console.log('Selected PDF file:', selectedFile.name);
//                console.log('File size:', selectedFile.size, 'bytes');
//            }
//        });
//    });
//});

function ProductSpec() {
    var Id = $('#ItemId').val();
    var Id = $('#ItemId').val();
    $.ajax({
        url: '/ItemMaster/_GetProductVarianceSpec',
        data: { id: Id },
        cache: false,
        type: "GET",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#productVarianceSpec').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}


$('#LoadProductSpec').on('click', function () {
    var Id = $('#ItemId').val();
    var productId = $('#ProductSpecCatId').val();
    $.ajax({
        url: '/ItemMaster/_GetProductVarianceSpec',
        data: { id: Id, ProductId: productId },
        cache: false,
        type: "GET",
        dataType: "html",
        success: function (data) {
            if (data != "")
                $('#productVarianceSpec').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});

$(document).ready(function () {
    // Attach click event to the button
    $("#uploadPdf").click(function () {
        // Create an input element of type file
        var input = document.createElement('input');
        input.type = 'file';

        // Set the accept attribute to allow only PDF files
        input.accept = '.pdf';

        // Trigger a click on the file input
        input.click();

        // Handle file selection
        input.addEventListener('change', function () {
            // Access the selected file
            var selectedFile = input.files[0];
            var Id = $('#ItemId').val();
            // Check if a file is selected
            if (selectedFile) {
                // Log the file details (you can customize this part)
                console.log('Selected PDF file:', selectedFile.name);
                console.log('File size:', selectedFile.size, 'bytes');

                // Create a FormData object to store the file data
                var formData = new FormData();
                formData.append('ItemId', Id); // Replace 'your_item_id' with the actual item ID
                formData.append('files', selectedFile);

                // Make an AJAX request to upload the PDF file
                $.ajax({
                    url: '/ItemMaster/UploadPdf',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        // Handle the success response here
                        console.log('File uploaded successfully:', response);
                        uploadedpdf();

                        // Redirect or perform any other action as needed
                    },
                    error: function (xhr, status, error) {
                        // Handle the error here
                        console.error('Error uploading file:', error);
                        // Show an error message or take appropriate action
                    }
                });
            }
        });
    });
});

function uploadedpdf() {

    var Id = $('#ItemId').val();
    var spli = Id.split("0");
    $.ajax({
        url: '/ItemMaster/_Getpdfuploadedview',
        data: { Id: spli[1] },
        cache: false,
        type: "GET",
        success: function (data) {
            // Assuming you want to append the loaded HTML to a specific element
            $('#pdfuploadedview').html(data); // Replace '#targetElement' with the ID or selector of the element where you want to load the partial view
        },
        error: function (xhr, status, error) {
            // Handle errors if any
            console.error(xhr.responseText);
        }
    });
}

var data = [];

$('#productVarianceSpec').on('change', '#descvalue', function () {
    var itemId = $('#ItemId').val();
    var productsubcatId = $(this).attr("data-productId");
    var productcatId = $(this).attr("data-id");
    var DescType = $(this).attr("data-spec");
    var DescValue = $(this).val();

    var existingIndex = data.findIndex(item => item.productsubcatId === productsubcatId);

    if (existingIndex !== -1) {
        // If item with same productsubcatId exists, replace it
        data[existingIndex] = {
            itemId: itemId,
            productsubcatId: productsubcatId,
            productcatId: productcatId,
            DescValue: DescValue,
            DescType: DescType
        };
    } else {
        // Otherwise, add new item
        var itemData = {
            itemId: itemId,
            productsubcatId: productsubcatId,
            productcatId: productcatId,
            DescValue: DescValue,
            DescType: DescType
        };
        data.push(itemData);
    }

    console.log(data); // Optional: to see the data being collected in the browser console
});


$('#savePdf').on('click', function () {
    var productSpe = {
        "productSpec": data,
    };

    $.ajax({
        url: '/ItemMaster/AddProductSpec/',
        type: 'POST',
        data: { 'spec': productSpe },
        success: function (data) {
            data.empty();
            var Id = $('#ItemId').val();
            $.ajax({
                url: '/ItemMaster/_GetProductVarianceSpec',
                data: { id: Id, ProductId: data.data },
                cache: false,
                type: "GET",
                dataType: "html",
                success: function (data) {
                    if (data != "")
                        $('#productVarianceSpec').html(data);
                    else
                        swal.fire("error", "Something went wrong", "error");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal.fire("error", "Something went wrong", "error");
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire("error", "Something went wrong", "error");
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});

$("#productdvariance").click(function () {
    var NameImage = $('#DisplayWithImg').val();
    if (NameImage === 'true') { // Compare with 'true' as string
        $('.Upload_Image_Path').css('display', 'none');
        $('.Upload_Image').css('display', 'flex');
        $('.upload-variance').css('display', 'flex');
        $('.Multiple-Image').css('display', 'flex');
    } else {
        $('.Upload_Image_Path').css('display', 'none');
        $('.Upload_Image').css('display', 'none');
        $('.upload-variance').css('display', 'none');
        $('.Multiple-Image').css('display', 'none');
    }
});




$('#MappedItemBody').on('click', '#deletemapitem', function () {
    var itemId = $('#ItemId').val();
    var mappingItemId = $(this).attr("data-itemId");
    $.ajax({
        url: '/ItemMaster/DeleteMapItem',
        type: 'POST',
        data: { itemId: itemId, mappingItemId: mappingItemId },
        success: function (data) {
            console.log('Item deleted successfully');
            MappedItemList();
        },
        error: function (xhr, status, error) {
            console.error('Error deleting item:', error);
        }
    });
});

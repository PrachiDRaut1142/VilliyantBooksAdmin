$(document).ready(function () {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    $.selector_cache('#CategoryName').on('keyup', function (e) {
        $.selector_cache('#spnCategoryName').css('display', 'none');
    });
    $('#cancel_blah__img').on('click', function () {
        $("#blah").attr("src", '/img/no-image.png');
        $("#imagecdnpath").val('');
    });
    $('.clearItem').on('click', function () {
        window.location.reload();
    });
    $('.catclose').on('click', function () {
        $('#spnCategoryName').css('display', 'none');
        $('#CategoryName').val('');
    });
    $('#pluName').on('keypress', function () {
        $('#Spnpluname').css('display', 'none');
    });
    $('#CategoryName').on('keypress', function () {
        $('#spnCategoryName').css('display', 'none');
    });
    $('#PluCode').on('keypress', function () {
        $('#Spnplucode').css('display', 'none');
    });
    $('#FoodSubType').on('change', function () {
        $('#SpnFoodSubType').css('display', 'none');
    });
    $('#Visibility').on('change', function () {
        $('#SpnVisibility').css('display', 'none');
    });
    $('#UploadImage').on('change', function (e) {
        $('#Spnpluname1').css('display', 'none');
    });
    $('#imagecdnpath').on('keyup', function () {
        $('#Spnpluname1').css('display', 'none');
    });
    $('#Offertype').on('change', function () {
        $('#Spnoffertype').css('display', 'none');
    });    
    //$('#FoodType').on('change', function () {
    //    $('#SpnFoddType').css('display', 'none');
    //    var foodType = this.value;
    //    if (foodType == "2") {
    //        $('#FoodSubType').css('display', 'block');
    //        $('#addcategory').css('display', 'block');
    //    }
    //    else {
    //        $('#FoodSubType').css('display', 'none');
    //        $('#addcategory').css('display', 'none');
    //        //$('#foodtypelabel').css('display', 'none');
    //    }
    //    $.ajax({
    //        url: "/ItemMaster/GetsubfoodList?foodType=" + foodType,
    //        type: "POST",
    //        success: function (subfoodList) {
    //            if (subfoodList.length > 0) {
    //                var html = "";
    //                html = '<option value="0" selected >Select FoodSub Type</option>';
    //                $.each(subfoodList, function (index, value) {
    //                    html += '<option value="' + value.value + '">' + value.text + '</option>';
    //                });
    //                $('#FoodSubType').html(html);
    //            }
    //            else {
    //                //swal.fire('Error!', 'There was some error. Try again later.', 'error');
    //            }
    //        }
    //    });
    //});

    $('#taxId').on('change', function () {
        var value = $(this).val();
        if (value == "No") {
            $('#TaxDivId').css('display', 'block');
        }
        else {
            $('.empty-cls').val('');
            $('#SGST').val(0);
            $('#CGST').val(0);
            $('#TaxDivId').css('display', 'none');
            $('.error-empty-cls').css('display', 'none');
        }
    });

    $('#IGST').on('change', function () {
        var value = $(this).val();
        if (value == "0") {
            $('#SGST').prop('value', 0);
            $('#CGST').prop('value', 0);
        }
        else {
            var scst = $(this).find(':selected').attr('data-scst');
            var cgst = $(this).find(':selected').attr('data-cgst');
            $('#SGST').prop('value', scst);
            $('#CGST').prop('value', cgst);
        }
    });

    $('#MainCate').on('change', function () {
        $('#SpnMainCate').css('display', 'none');
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
                    $('#Category').html(html);
                }
                else {
                    swal.fire('Warning!', 'Please Add Category.', 'warning');
                }
            }
        });
    });
    $('#Supplier').on('change', function () {
        $('#Spnsupplier').css('display', 'none');
    });
    $('#BrandId').on('change', function () {
        $('#SpnBrandId').css('display', 'none');
    });

    $('#Category').on('change', function () {
        var categoryId = this.value;
        $.ajax({
            url: "/ItemMaster/GetSubCategoryList?CategoryId=" + categoryId,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected >Select Category</option>';
                    $.each(categoryList, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#SubCategory').html(html);
                }
                else {
                    //swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    });

    $('#PluCode').on('change', function () {
        $('#Spnplucode').css('display', 'none');
        $.ajax({
            url: "/ItemMaster/CheckUniquePlucode?PluCode=" + this.value,
            type: "POST",
            success: function (result) {
                if (result.Data == "") {
                    $('#SpnUniqueplucode').html("Plucode should be unique").css('display', 'none');
                }
                else {
                    $('#SpnUniqueplucode').html("Plucode should be unique").css('display', 'block');
                }
            }
        });
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

    //ItemMaster Create
    $('#btnSubmitItem').on('click', function () {
        var nErrorCount = 0
        if ($('#FoodType').val() === "0" || $('#FoodType').val() === null) {
            $('#SpnFoddType').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#FoodType').val() === "2" && $('#FoodSubType').val() === null) {
            $('#SpnFoodSubType').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }       
        if ($('#Visibility').val() === "" || $('#Visibility').val() === null) {
            $('#SpnVisibility').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#pluName').val() === "" || $('#pluName').val() === null) {
            $('#Spnpluname').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if (($('#imagecdnpath').val() === "" || $('#imagecdnpath').val() === null) &&
            ($("#blah").attr('src') === '' || $("#blah").attr('src') === null || $("#blah").attr('src') == '/img/no-image.png'))
        {
            $('#Spnpluname1').html("Either Enter Image Path or Upload Image  ").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#PluCode').val() === "" || $('#PluCode').val() === null) {
            $('#Spnplucode').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
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
        if ($('#MainCate').val() === "0" || $('#MainCate').val() === null) {
            $('#SpnMainCate').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Supplier').val() === "0" || $('#Supplier').val() === null) {
            $('#Spnsupplier').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#BrandId').val() === "0" || $('#BrandId').val() === null) {
            $('#SpnBrandId').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#Offertype').val() === "" || $('#Offertype').val() === null) {
            $('#Spnoffertype').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
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
    $('#add_category_md #addnonvegcate').on('click', function () {
        var nErrorCount = 0
        if ($('#CategoryName').val() === "" || $('#CategoryName').val() === null) {
            $('#spnCategoryName').html("This field is required").css('display', 'block');
            nErrorCount += 1;
        }
        var FoodSubType = $('#add_category_md #CategoryName').val();
        if (0 === nErrorCount) {
            var info = {
                FoodSubType: FoodSubType,
            }
            $.ajax({
                url: '/ItemMaster/AddNonVegCategory',
                type: 'POST',
                data: info,
                success: function (result) {
                    if (result != null) {
                        swal.fire('Success', 'Food subtype added successfully.', 'success');
                        $('#add_category_md').modal('hide');
                        loadSubTypeDropDown();
                    }
                    else {
                        swal.fire('error', 'Oops error occurred', 'error');
                    }
                }
            });
        }
    });
    function loadSubTypeDropDown() {
        $.ajax({
            url: "/ItemMaster/GetsubfoodList?foodType=" + 2,
            type: "POST",
            success: function (subfoodList) {
                if (subfoodList.length > 0) {
                    var html = "";
                    html = '<option value="0" selected >Select FoodSub Type</option>';
                    $.each(subfoodList, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#FoodSubType').html(html);
                }
                else {
                    swal.fire('error', 'Oops error occurred', 'error');
                }
            }
        });
    }
    $('#addcategory').on('click', function () {
        $('#add_category_md #CategoryName').val('');
    });
});

function readURL(input) {
    var imagePath = $("#imagecdnpath").val();
    if (imagePath != '') {
        $("#imagecdnpath").val('');
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result).width(150).height(200);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }
    else {
        if (input.files && input.files[0]) {        
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#blah').attr('src', e.target.result).width(150).height(200);
                };
                reader.readAsDataURL(input.files[0]);
        }        
    }
}

jQuery(document).ready(function () {
    ImgUpload();
});

function ImgUpload() {
    var imgWrap = "";
    var imgArray = [];

    $('.upload__inputfile').each(function () {
        $(this).on('change', function (e) {
            imgWrap = $(this).closest('.upload__box').find('.upload__img-wrap');
            var maxLength = $(this).attr('data-max_length');
            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);
            var iterator = 0;
            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    return;
                }
                if (imgArray.length > maxLength) {
                    return false
                } else {
                    var len = 0;
                    for (var i = 0; i < imgArray.length; i++) {
                        if (imgArray[i] !== undefined) {
                            len++;
                        }
                    }
                    if (len > maxLength) {
                        return false;
                    } else {
                        imgArray.push(f);
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var html = "<div class='col-1 upload_multiImage_" + f.name + "' id='upload_multiImage_" + f.name + "'>"
                                + "<div class='upload__img-box'>"
                                + "<div style = 'background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data - file='" + f.name + "' class='img-bg' >"
                                + "<div class='upload__img-close'>"
                                + "</div>"
                                + "</div >"
                                + "<input type='file' id='multiImg' class='multiImg' data-value='" + f.name + "' hidden/>"
                                + "<button type = 'button' class='cancelMultiImage cancel_button btn btn-default' onclick='cancelMultiImg(this);' data-value='" + f.name + "'>"
                                + "<i class='fa fa-times' aria-hidden='true' ></i>"
                                + "</button >"
                                + "</div>"
                                + "</div> "
                               ;
                            imgWrap.append(html);
                            iterator++;
                        }
                        reader.readAsDataURL(f);
                    }
                }
            });
        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}

function previewFile(input) {
    var imagePath = $("#imagecdnpath").val();
    if (imagePath != '') {
        $("#blah").attr("src", imagePath);
    }
    else {
        $("#blah").attr("src", '/img/no-image.png');
    }
}

function cancelMultiImg(input) {
    $('.upload__img-wrap').empty();
    $('#ImagePaths').val('');
}

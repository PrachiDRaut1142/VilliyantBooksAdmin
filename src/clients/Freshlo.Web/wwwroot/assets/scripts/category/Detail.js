$(function () {
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    var req = 'This Field is is required';
    $('#CatSequence').on('keypress', function (e) {
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            $('#SpnCatSequence').css('display', 'none');
            return true;
        }
        else {
            $('#SpnCatSequence').html(req).css('display', 'block');
            e.preventDefault();
            return false;
        }
    });
    $('#CatName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCatName').html(req).css('display', 'block');
            }
        }
        else if ($('#CatName').val() != null || $('#CatName').val() != '') {
            $('#SpnCatName').css('display', 'none');
        }
        else {
            $('#SpnCatName').html(req).css('display', 'block');
        }
    });
    $('#CategoriesName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCategoriesName').html(req).css('display', 'block');
            }
        }
        else if ($('#CategoriesName').val() != null || $('#CategoriesName').val() != '') {
            $('#SpnCategoriesName').css('display', 'none');
        }
        else {
            $('#SpnCategoriesName').html(req).css('display', 'block');
        }
    });
    $('.NameCategories').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnNameCategories').html(req).css('display', 'block');
            }
        }
        else if ($('.NameCategories').val() != null || $('.NameCategories').val() != '') {
            $('#SpnNameCategories').css('display', 'none');
        }
        else {
            $('#SpnNameCategories').html(req).css('display', 'block');
        }
    });
    $('.Squence').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnSquence').html(req).css('display', 'block');
            }
        }
        else if ($('.Squence').val() != null || $('.Squence').val() != '') {
            $('#SpnSquence').css('display', 'none');
        }
        else {
            $('#SpnSquence').html(req).css('display', 'block');
        }
    });
    $('#CategorySequence').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCategorySequence').html(req).css('display', 'block');
            }
        }
        else if ($('#CategorySequence').val() != null || $('#CategorySequence').val() != '') {
            $('#SpnCategorySequence').css('display', 'none');
        }
        else {
            $('#SpnCategorySequence').html(req).css('display', 'block');
        }
    });
    $('.closeCategories').on('click', function () {
        $('#SpnCategoriesName').css('display', 'none');
        $('#SpnCategorySequence').css('display', 'none');
    });
    $('.CloseNewCat').on('click', function () {
        $('#SpnCatName').css('display', 'none');
        $('#SpnCatSequence').css('display', 'none');
    });
    $('.closeUpdateCat').on('click', function () {
        $('#SpnSubCategoryName').css('display', 'none');
        $('#SpnSquence').css('display', 'none');
        $('#SpnNameCategories').css('display', 'none');
    });

    //  MainCateogry Related
    $(document).ready(function () {
        var seasonSale = $('#Visiblitychk').prop('value');
        if (seasonSale == 1) {
            $(".seasonSale").prop('checked', true)
        }
        else {
            $(".seasonSale").prop('checked', false)
        }
    });
    $("#imageFiles").change(function () {
        readURL(this);
    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imgfiles').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $('#UpdateMainCategory').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('#CatName').val() === "" || $('#CatName').val() === null) {
            $('#SpnCatName').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#CatSequence').val() === "" || $('#CatSequence').val() === null) {
            $('#SpnCatSequence').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
            $('#MainCategoryUpdate').submit();
            swal({
                text: "Successfully Update MainCategory",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });

    // Cateogry Related 
    $("#ImagePath").change(function () {
        readURL(this);
    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imgpath').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $('#AddCategories').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('#CategoriesName').val() === "" || $('#CategoriesName').val() === null) {
            $('#SpnCategoriesName').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#CategorySequence').val() === "" || $('#CategorySequence').val() === null) {
            $('#SpnCategorySequence').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
            $('#CatgoreisAdd').submit();
            swal({
                text: "Successfully Create Category",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });
    $('.GetCategoriesDetails').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Category/GetCategoryDetails?id=' + Id,
            type: 'GET',
            success: function (data) {
                var path = 'https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/category-images/';
                $('.Ids').val(data.ItemCategoreisDetails.Id);
                $.selector_cache('#CategoryId').val(data.ItemCategoreisDetails.CategoryId);
                $.selector_cache('#MainCategoryId11').val(data.ItemCategoreisDetails.CategoryId);
                $.selector_cache('#CategoryImg').attr('src', path + data.ItemCategoreisDetails.CategoryId + '.png');
                $('.NameCategories').val(data.ItemCategoreisDetails.CategoriesName);
                $('.Squence').val(data.ItemCategoreisDetails.CategorySequence);
                $(".visible option[value='" + data.ItemCategoreisDetails.CategVisibility + "']").prop('selected', true);
                $('.disc').val(data.ItemCategoreisDetails.CategDescription);
                $('#EditCategory').modal('show');
            }
        });
        $.ajax({
            url: '/Category/_GetItemSubcategorieslist?id=' + Id,
            type: 'GET',
            success: function (data) {
                $('#Subcategorytbl').DataTable().destroy();
                $('#Subcategorydata').html(data);
                $('#EditCategory').modal('show');
            }
        });      
    });
    $('#UpdateItemCategory').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('.NameCategories').val() === "" || $('.NameCategories').val() === null) {
            $('#SpnNameCategories').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('.Squence').val() === "" || $('.Squence').val() === null) {
            $('#SpnSquence').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
            $('#ItemCategoryUpdate').submit();
            swal({
                text: "Successfully Update Category",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });
    $('.m-data-table').on('click', '.deleteCategoryId', function () {
        var Id = $(this)[0].dataset.id;
        Swal.fire({
            title: 'Are you sure?',
            text: 'You want to Delete  Category! Corresponding  Subcategory will be Deleted..!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/Category/DeleteCategory?categoryId=' + Id,
                    type: 'GET',
                    success: function (result) {
                        if (result == true) {
                            swal({
                                text: "Successfully Delete Category",
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
    });
    var req = 'This Field is is required';
    $('#SubCategoryName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnSubCategoryName').html(req).css('display', 'block');
            }
        }
        else if ($('#SubCategoryName').val() != null || $('#SubCategoryName').val() != '') {
            $('#SpnSubCategoryName').css('display', 'none');
        }
        else {
            $('#SpnSubCategoryName').html(req).css('display', 'block');
        }
    });
    // SubCateogry Related
    $(document).on('click', '#SubmitSubCategories', function () {
        var nErrorCount = 0;
        var CategoryId = $('.CategoryId').val();
        if ($('#SubCategoryName').val() === "" || $('#CatName').val() === null) {
            $('#SpnSubCategoryName').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        var SubCategoryName = $('#SubCategoryName').val();
        if (nErrorCount === 0) {
            if (CategoryId != null) {
                Swal.fire({
                    title: 'Are you sure?',
                    text: 'you want to add this item to subCategory !',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, Create it!'
                }).then((result) => {
                    if (result.value) {
                        var dicData = {
                            CategoryId: CategoryId,
                            SubCategoryName: SubCategoryName
                        };
                        $.ajax({
                            url: '/Category/AddSubCategoriesItem/',
                            type: 'POST',
                            dataType: 'JSON',
                            data: dicData,
                            success: function (data) {
                                if (data.IsSuccess == true) {
                                    $.ajax({
                                        url: '/Category/_GetItemSubcategorieslist?id=' + CategoryId,
                                        type: 'GET',
                                        success: function (data) {
                                            $('#Subcategorytbl').DataTable().destroy();
                                            $('#Subcategorydata').html(data);
                                        }
                                    });
                                }
                            }
                        });
                        $('.clearitem').val('');
                        $('.clearitem').text('');
                    }
                });
            }
        }
    });

    $('#Subcategorytbl').on('click', '.deleteSubcategoryId', function () {
        var Id = $(this)[0].dataset.id;
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to delete this Subcategory!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/Category/DeleteSubCategory?subCategoryId=' + Id,
                    type: 'GET',
                    success: function (result) {
                        if (result == true) {
                            swal({
                                text: "Successfully Delete SubCategory",
                                type: "success",
                                confirmButtonText: "OK"
                            }).then(function (e) {
                                location.reload();
                            })
                        }
                        else {
                            swal.fire('Oops!', 'Something went wrong!', 'warning');
                        }
                    }
                });
            }
        });
    });

    $('#mappedMainCatwithHub').on('click', function () {
        var checked = $(this).is(":checked")
        if (checked == true) {
            var mainCategoryId = $(this).val();
            swal({
                title: "Are you sure ?",
                text: "you want to Include this MainCategory into the Hub",
                showCancelButton: true,
                confirmButtonText: "Yes Include it !",
                confirmButton: true,
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: '/Category/AddeIntoHub?MainCatId=' + mainCategoryId,
                        type: 'POST',
                        dataType: 'JSON',
                        success: function (data) {
                            if (data && data.IsSuccess) {
                                swal.fire("success", "Added inot Hub Successfully!", "success").then(okay => {
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
        else {
            var mainCategoryId = $(this).val();
            swal({
                title: "Are you sure ?",
                text: "you want to Exclude this MainCategory into the Hub",
                showCancelButton: true,
                confirmButtonText: "Yes Exclude it !",
                confirmButton: true,
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: '/Category/RemoveIntoHub?MainCatId=' + mainCategoryId,
                        type: 'POST',
                        dataType: 'JSON',
                        success: function (data) {
                            if (data && data.IsSuccess) {
                                swal.fire("success", "Added inot Hub Successfully!", "success").then(okay => {
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
    });
});


//function readURL(input) {
//    if (input.files && input.files[0]) {
//        var reader = new FileReader();
//        reader.onload = function (e) {
//            $('#CategoryImg1')
//                .attr('src', e.target.result) // Set the image source to the base64 encoded URL
//                .width(150) // Adjust width if needed
//                .height(200); // Adjust height if needed
//        };
//        reader.readAsDataURL(input.files[0]); // Read the selected file as a URL
//    }
//}

$(document).ready(function () {
    $(".chips-wrapper .chips").click(function () {
        var Id = $(this).attr("data-id");
        $.ajax({
            url: '/Settings/deleteLanguage?id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Language Deleted Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        addlanguage();
                    })
                }
                else
                swal({
                    text: "Something Went Wrong",
                    type: "error",
                    confirmButtonText: "OK"
                })
            },
            error: function (result) {
                swal({
                    text: "Server Error, Pleas Try Later ",
                    type: "error",
                    confirmButtonText: "OK"
                })
            }
        })
    });

    $('#tableid').on('click','#viewmaincategory',function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Category/GetLanguageMainCategoriesList?Id=' + Id,
            type: 'GET',
            success: function (result) {
                $('#languageMainCateId').html(result);
                $('#mainCategory_translation').modal('show');
                $('#MainCategoryId143').val(Id);
            }, error: function () {
                alert('error');
            }
        });
    });

    $('#tablei').on('click', '#viewcategorylanguage', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Category/GetLanguageSubCategoriesList?Id=' + Id,
            type: 'GET',
            success: function (result) {
                $('#languageSubCateId').html(result);
                $('#category_translation').modal('show');
            }, error: function () {
                alert('error');
            }
        });
    });       
   
    $("#addlanguage").on('click',function () {
        var numError = 0;
        if (numError == 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to Add language',
                showCancelButton: true,
                confirmButtonColor: '#3085D6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Add it!'
            }).then((result) => {
              var  languagename = $('#Addlanguagetext1').val();
                if (result.value) {
                    if (languagename != "")
                        $.ajax({
                            url: '/Settings/Addlanguage/',
                            type: 'GET',
                            data: {
                                languagename: $("#Addlanguagetext1").val(),
                                },
                            success: function (result) {
                                if (result.result != 0) {
                                    $('#AddedLanguage').modal('hide');
                                    swal({
                                        text: "Language Added Successfully",
                                        type: "success",
                                        confirmButtonText: "OK",                                       
                                    }).then(function (e) {
                                        addlanguage();
                                    })
                                }
                                else {
                                    $('#add_language_md').modal('hide');
                                    swal({
                                        text: "Something Went Wrong",
                                        type: "error",
                                        confirmButtonText: "OK",
                                    });
                                }
                            }
                        });
                }
            })
        }                    
    });

    $('#languageitemname').on('keypress', function () {
        $('#spnlanguageitemname').css('display', 'none');
    });
    $('#Addlanguagetext').on('keypress', function () {
        $('#Spnlanguagetext').css('display', 'none');
    });
    $('#AddlanguageName').on('keypress', function () {
        $('#Spnlanguagetext').css('display', 'none');
    });

    //$("#languagesubmit").on('click', function () {
    //    var numError = 0;
    //    if ($('#languageitemname').val() === "" || $('#languageitemname').val() === null) {
    //        $('#spnlanguageitemname').html("This field is required").css('display', 'block');
    //        numError += 1;
    //    }
    //    if ($('#AddlanguageName').val() === "0" || $('#AddlanguageName').val() === null) {
    //        $('#Spnlanguagetext').html("This field is required").css('display', 'block');
    //        numError += 1;
    //    }
    //    if (numError == 0) {
    //        swal.fire({
    //            title: 'Are you sure?',
    //            text: 'Change  language of item!',
    //            showCancelButton: true,
    //            confirmButtonColor: '#3085D6',
    //            cancelButtonColor: '#d33',
    //            confirmButtonText: 'Yes, Change it!'
    //        }).then((result) => {
    //            var ItemNameLanguage = $("#languageitemname").val();
    //            if (result.value) {
    //                if (ItemNameLanguage != "") {
    //                    $('#AddEditItemLang').submit();
    //                }
    //                    //$.ajax({
    //                    //    url: '/Settings/changelanguage/',
    //                    //    type: 'GET',
    //                    //    data: {
    //                    //        ItemNameLanguage: $("#languageitemname").val(),
    //                    //        Descriptionlanguage: $("#Descriptionlanguage").val(),
    //                    //        Itemused: $("#Itemused").val(),
    //                    //        LanguageName: $("#AddlanguageName").val(),
    //                    //        //measuremnetlanguage: $("#Sizelanguage").val(),
    //                    //        ItemId: $("#ItemId").val(),
    //                    //        Id: $("#Id").val()
    //                    //    },
    //                    //    success: function (result) {
    //                    //        if (result.result != 0) {
    //                    //            $('#Language_tab').modal('hide');
    //                    //            swal.fire({
    //                    //                text: "Item Name Changed Successfully",
    //                    //                type: 'Success',
    //                    //                confirmButtonText: "OK",
    //                    //            });
    //                    //            window.location.reload();
    //                    //        }
    //                    //        else {
    //                    //            $('#Language_tab').modal('hide');
    //                    //            swal.fire({
    //                    //                text: "Soomething Went Wrongs",
    //                    //                type: "error",
    //                    //                confirmButtonText: "OK",
    //                    //            })
    //                    //        }
    //                    //    }
    //                    //});
    //            }
    //        })
    //    }
    //});

    //category add
    $("#categorylanguage").on('click', function () {
        var numError = 0;
        if (numError == 0) {
            swal.fire({
                title: 'Are you sure?',
                text: 'Change  language of item!',
                showCancelButton: true,
                confirmButtonColor: '#3085D6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Change it!'
            }).then((result) => {
                var CategoryLanguage = $('#CategoriesNamelanguage').val();
                if (result.value) {
                    if (CategoryLanguage != "")
                        $.ajax({
                            url: '/Category/GetCategoryLanguage/',
                            type: 'GET',
                            data: {
                                CategoryLanguage: $("#CategoriesNamelanguage").val(),
                                CategoryDescription: $("#CategDescriptionlanguage").val(),
                                CategoryId: $("#MainCategoryId11").val(),
                            },
                            success: function (result) {
                                if (result.result != 0) {
                                    $('#ItemCategoryUpdate').modal('hide');
                                    swal.fire({
                                        text: "Item Name Changed Successfully",
                                        type: 'Success',
                                        confirmButtonText: "OK",
                                    });
                                    window.location.reload();
                                }
                                else {
                                    $('#ItemCategoryUpdate').modal('hide');
                                    swal.fire({
                                        text: "Soomething Went Wrongs",
                                        type: "error",
                                        confirmButtonText: "OK",
                                    })
                                }
                            }
                        });
                }
            })
        }
    });
    $('#maincategorylanguagename').on('keyup', function (e) {
        var req = 'This Field is is required';
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#Spnmaincategorylanguagename').html(req).css('display', 'block');
            }
        }
        else if ($('#maincategorylanguagename').val() != null || $('#maincategorylanguagename').val() != '') {
            $('#Spnmaincategorylanguagename').css('display', 'none');
        }
        else {
            $('#Spnmaincategorylanguagename').html(req).css('display', 'block');
        }
    });
    //maincategory added
    //$(document).on('click', '#maincategorylanguage', function () {
    //    //var mainCatId = $(this)[0].dataset.id;
    //    var numError = 0;
    //    var req = 'This Field is is required';
    //    if ($('#maincategorylanguagename').val() === "" || $('#maincategorylanguagename').val() === null) {
    //        $('#Spnmaincategorylanguagename').html(req).css('display', 'block');
    //        numError += 1;
    //    }
    //    if (numError == 0) {
    //        swal.fire({
    //            title: 'Are you sure?',
    //            text: 'Change  language of item!',
    //            showCancelButton: true,
    //            confirmButtonColor: '#3085D6',
    //            cancelButtonColor: '#d33',
    //            confirmButtonText: 'Yes, Change it!'
    //        }).then((result) => {
    //            var MainCategoryLanguage = $('#maincategorylanguagename').val();
    //            if (result.value) {
    //                if (MainCategoryLanguage != "")
    //                    $.ajax({
    //                        url: '/Category/GetLanguageMainCategory/',
    //                        type: 'GET',
    //                        data: {
    //                            MainCategoryLanguage: $("#maincategorylanguagename").val(),
    //                            MainCategoryDescription: $("#maincategoryDescription").val(),
    //                            MainCategoryId: $('#MainCategoryId143').val(),
    //                            LanguageName: $('#Addlanguagetext').val(),
    //                        },
    //                        success: function (result) {
    //                            if (result.result != 0) {
    //                                $('#MainCategoryUpdate').modal('hide');
    //                                swal.fire({
    //                                    text: "Item Name Changed Successfully",
    //                                    type: 'Success',
    //                                    confirmButtonText: "OK",
    //                                })
    //                                location.href = "/Category/Manage/";
    //                            }
    //                            else {
    //                                $('#MainCategoryUpdate').modal('hide');
    //                                swal.fire({
    //                                    text: "Soomething Went Wrongs",
    //                                    type: "error",
    //                                    confirmButtonText: "OK",
    //                                })
    //                                location.href = "/Category/Manage/";
    //                            }
    //                        }
    //                    });
    //            }
    //        })
    //    }
    //});   
   
    $("#LanguageIdlist").on('shown.bs.tab', function () {
        Id = $("#product").val();
        itemnamechange();
    });
    $("#languagesubmit").on('click', function () {
        var numError = 0;
        if ($('#languageitemname').val() === "" || $('#languageitemname').val() === null) {
            $('#spnlanguageitemname').html("This field is required").css('display', 'block');
            numError += 1;
        }
        if ($('#AddlanguageName').val() === "0" || $('#AddlanguageName').val() === null) {
            $('#Spnlanguagetext').html("This field is required").css('display', 'block');
            numError += 1;
        }
        if (numError == 0) {
            swal.fire({
                title: 'Are you sure?',
                text: 'Change  language of item!',
                showCancelButton: true,
                confirmButtonColor: '#3085D6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Change it!'
            }).then((result) => {
                var ItemNameLanguage = $("#languageitemname").val();
                if (result.value) {
                    if (ItemNameLanguage != "") {
                        $('#AddEditItemLang').submit();
                    }
                }
            })
        }
    });
    //maincategory added
    $(document).on('click', '#maincategorylanguage', function () {
        //var mainCatId = $(this)[0].dataset.id;
        var numError = 0;
        var req = 'This Field is is required';
        if ($('#maincategorylanguagename').val() === "" || $('#maincategorylanguagename').val() === null) {
            $('#Spnmaincategorylanguagename').html(req).css('display', 'block');
            numError += 1;
        }
        if (numError == 0) {
            swal.fire({
                title: 'Are you sure?',
                text: 'Change  language of item!',
                showCancelButton: true,
                confirmButtonColor: '#3085D6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Change it!'
            }).then((result) => {
                var MainCategoryLanguage = $('#maincategorylanguagename').val();
                if (result.value) {
                    if (MainCategoryLanguage != "")
                        $('#MainCategoryUpdate').submit();
                }
            })
        }
    });
});

function addlanguage() {
    $.ajax({
        url: '/Settings/languageselect/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#AddedLanguage').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
        }, error: function (result) {
        }
    });
}

function itemnamechange() {
    $.ajax({
        url: '/ItemMaster/Getitemnamechange?Id=' + Id,
        type: 'Get',
        success: function (result) {
            if (result != "")
                $("#itemnamechange").html(result);
            else
                swal.fire("error", "Something went wrong", "error");
        }, error: function (result) {
        }
    });
}

  
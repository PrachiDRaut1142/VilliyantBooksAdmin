$(document).ready(function () {
    //$('#tblPricelistId').DataTable();
    //$('#tblPricelistId').DataTable({
    //    "pageLength": 100,
    //    "ordering": false,
    //    "sorting": true
    //});
    StockList(0);
    $('#MainCategoryId').on('change', function () {
        var maincategoryId = this.value;
        if (maincategoryId != null) {
            $('#Filter-Id').removeAttr('disabled');
        }
        else {
            $('#Filter-Id').attr('disabled');
        }
        $.ajax({
            url: "/ItemMaster/GetCategorylist?mainCatId=" + maincategoryId,
            type: "POST",
            success: function (categoryList) {
                if (categoryList.length > 0) {
                    var html = "";
                    $.each(categoryList, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#CategoryId').html(html);
                }
                else {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            }
        });
    });

    $('#Filter-Id').on('click', function () {
        var MainCategoryId = $('#MainCategoryId').val();
        var CategoryId = $('#CategoryId').val();
        var item = {
            MainCategory: MainCategoryId,
            Category: CategoryId,
            type: 1
        };
        StockList(item)
    });

    function StockList(item) {
        $.ajax({
            url: '/StockManagement/_StockUpdate',
            type: 'Get',
            contentType: "application/json; charset=utf-8",
            traditional: true,
            data: item,
            success: function (data) {
                $('#tblPricelistId').DataTable().destroy();
                $('#StockList').html(data);
                $('#tblPricelistId').DataTable();
            }
        });
    }

    $('#StockList').on('change','#SellingProfitPer', function () {
        var priceId = $(this).attr("data-priceId");
        var itemId = $(this).attr("data-itemId");
        var pluName = $(this).attr("data-pluName");
        var size = $(this).attr("data-size");
        var SellingPrice = this.value;
        var item = {
            ItemId: itemId,
            priceId: priceId,
            SellingPrice: SellingPrice
        };

        var elementToUpdate = $('[data-itemId="' + itemId + '"][data-priceId="' + priceId + '"]');
        var updateButton = $('#price_item_' + priceId);
        var savedButton = $('#Saved_item_' + priceId);

        $.ajax({
            url: '/StockManagement/UpdateSellingPrice',
            type: 'json',
            data: item,
            success: function (data) {
                if (data.IsSuccess == true) {
                    // Update the element with the new stock value
                  //  elementToUpdate.text('Updated Stock: ' + SellingPrice);
                  
                        
                        updateButton.css('display', 'block');
                       // savedButton.css('display', 'block');

                        // Change the text color to green for both buttons
                        updateButton.css('color', 'green');
                       // savedButton.css('color', 'green');

                        // Set a timer to hide the buttons after 3 seconds (3000 milliseconds)
                      // Adjust the time delay as needed (in milliseconds)
                  
                }
                else {
                    swal.fire('Something Went wrong', "", "warning");
                }
            }
        });  
    });

    $('#StockList').on('change', '#StockQty', function () {
        var itemId = $(this).data("itemid"); // Note lowercase "itemid"
        var priceId = $(this).data("priceid"); // Note lowercase "priceid"
        var pluName = $(this).data("pluname");
        var size = $(this).data("size");
        var stock = this.value;
        var item = {
            ItemId: itemId,
            PriceId: priceId, // Note uppercase "PriceId" to match your item object
            StockQty: stock
        };

        var elementToUpdate = $('[data-itemId="' + itemId + '"][data-priceId="' + priceId + '"]');
        var updateButton = $('#update_item_' + priceId);
        var savedButton = $('#Saved_item_' + priceId);

        $.ajax({
            url: '/StockManagement/UpdateStock',
            type: 'json',
            data: item,
            success: function (data) {
                if (data.IsSuccess == true) {
                    // Update the element with the new stock value
                    elementToUpdate.text('Updated Stock: ' + stock);
                    if (stock <= 0) {
                       // elementToUpdate.css('color', 'red');
                        updateButton.css('display', 'block');
                        //savedButton.css('display', 'block');

                        // Change the text color to green for both buttons
                        updateButton.css('color', 'green');
                        //savedButton.css('color', 'green');

                        // Set a timer to hide the buttons after 3 seconds (3000 milliseconds)
                        // Adjust the time delay as needed (in milliseconds)
                    } else {
                       // elementToUpdate.css('color', 'green');
                        updateButton.css('display', 'block');
                       // savedButton.css('display', 'block');

                        // Change the text color to green for both buttons
                        updateButton.css('color', 'green');
                       // savedButton.css('color', 'green');

                        // Set a timer to hide the buttons after 3 seconds (3000 milliseconds)
                      // Adjust the time delay as needed (in milliseconds)
                    }
                    // Show the "update" button for this specific priceId
                  
                } else {
                    swal.fire('Something Went wrong', "", "warning");
                }
            }
        });

        // Hide the "update" button after the AJAX request is initiated
        
    });

    $('#StockList').on('click', '#Isvisiable', function () {
        var itemId = $(this).attr("data-itemId");
        var priceId = $(this).val();
        var checked = $(this).is(":checked");
        if (checked == true) {
            var isviable = "1";
        }
        if (checked == false) {
            var isviable = "0";
        }
        var item = {
            ItemId: itemId,
            priceId: priceId,
            IsVisible: isviable
        };

        var elementToUpdate = $('[data-itemId="' + itemId + '"][data-priceId="' + priceId + '"]');
        // var updateButton = $('#update_item_' + priceId);
        var savedButton = $('#Saved_item_' + priceId);

        $.ajax({
            url: '/StockManagement/UpdateStockVis',
            type: 'json',
            data: item,
            success: function (data) {
                if (data.IsSuccess == true) {
                    // Update the element with the new stock value
                    // elementToUpdate.text('Updated Stock: ' + stock);

                    // elementToUpdate.css('color', 'green');
                    // updateButton.css('display', 'block');
                    savedButton.css('display', 'block');

                    // Change the text color to green for both buttons
                    // updateButton.css('color', 'green');
                    savedButton.css('color', 'green');

                    // Set a timer to hide the buttons after 3 seconds (3000 milliseconds)
                    // Adjust the time delay as needed (in milliseconds)

                }
                else {
                    swal.fire('Something Went wrong', "", "warning");
                }
            }
        });

        // Hide the "update" button after the AJAX request is initiated

    });

    $('#uploadExcel').change(function (e) {
        var fileName = e.target.files[0].name;
        if (fileName != null) {
            $('#uploadimage').css('display', 'none');
            $('#ExcelName').css('display', 'block');
            $('#ExcelName').html(fileName);
        }
        else {
            $('#ExcelName').css('display', 'none');
            $('#uploadimage').css('display', 'block');
        }
    });

    $('#SubmitStockExcel').on('click', function () {
        var fileName = $('#ExcelName').text();
        var UploadedExcel = $('#uploadExcel').val();
        var numberoferrors = 0;
        if (numberoferrors === 0) {
            var formData = new FormData();
            formData.append('FileName', fileName);

            var data = $('#uploadExcel')[0].files[0];
            formData.append('file', data);

            $.ajax({
                url: '/StockManagement/_HeaderModal',
                type: 'post',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response) {
                        $('#Filemodal').html(response);
                        $('#Filemodal').modal('show');
                        $('#upload_excel_md').modal('hide');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                },
                complete: function () {
                    //$('#page-common-loader').css('display', 'none');
                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'none');
                }
            });
        }
    });

    $('#Filemodal').on('click', '.submitExcel', function (e) {
        //('#page-common-loader').css('display', 'block');
        $('#Filemodal').modal('hide');
        $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
        var values = [];
        var values1 = [];
        var numberoferrors = 0;

        $('select[name="HeaderValues"]').each(function (index) {
            values.push($(this).prop('value'));
        });
        var camp = {
            HeaderValues: values,
            FileName: $('#fileName').text(),
        };
        if (numberoferrors == 0) {
            $.ajax({
                url: '/StockManagement/UploadExcel/',
                type: 'POST',
                dataType: 'json',
                data: camp,
                success: function (data) {
                    $('#Filemodal').modal('hide');
                    var SuccessData = data.split(',')[0];
                    var RejectedData = data.split(',')[1];
                    var ListId = data.split(',')[2];

                    if (data.split(',')[0] == "") {
                        var SuccessData = 0;
                    }

                    if (data.split(',')[1] == "") {
                        var RejectedData = 0;
                    }

                    if (data !== "") {
                        if (data === "0,") {
                            swal("Oops", "Your Data is Uploaded in Rejected List", "error");
                        } else {
                            $('#ExcelName').html('');
                            $('#ExcelName').css('display', 'none');
                            $('#uploadimage').css('display', 'block');
                            // Set the confirmButtonText conditionally
                            var confirmButtonText = "Okay"; // Default button text

                            if (RejectedData > 0) {
                                confirmButtonText = "Downlad the error log"; // Change button text if data2 > 2
                            }

                            Swal.fire({
                                title: "File Upload Summary",
                                html: "<h5> Updated: <b>" + SuccessData + "</b> records </h5>"
                                    + "<h5> Rejected: <b>" + RejectedData + "</b> records </h5>",
                                showCancelButton: false,
                                confirmButtonText: confirmButtonText, // Use the conditionally set text
                                //cancelButtonText: "Okay",
                            }).then((result) => {
                                switch (result.value) {

                                    case true:
                                        if (RejectedData > 0) {                                            
                                            DownloadRejectList(ListId);
                                            //window.location.href = '/StockManagement/StockUpdate';
                                        }
                                        else {
                                            window.location.reload();
                                        }
                                        break;

                                    default:
                                        window.location.reload();
                                        break;
                                }
                            });
                        }
                    }
                    else {
                        swal("Oops", "Some error occured please try Again", "error");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $('#Filemodal').modal('hide');
                },
                complete: function () {
                    //$loader.css('display', 'none');
                    //$('#page-common-loader').css('display', 'none');
                    //window.location.reload();
                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'none');
                }
            });
        }
        else {
            //$loader.css('display', 'none');
            //$('#page-common-loader').css('display', 'none');
            $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'none');
        }

        // Hide the "update" button after the AJAX request is initiated

    });

    $('#exportStock').on('click', function () {
        var maincategoryId = $('#MainCategoryId').val();
        var CategoryId = $('#CategoryId').val();
        if (maincategoryId != null || CategoryId != null) {
            var type = 1;
        }
        else {
            var type = 0;
        }
        window.location.href = '/StockManagement/ExportStockExcel?maincategory=' + maincategoryId + '&Category=' + CategoryId + '&type=' + type;
    });

    function DownloadRejectList(ListId) {
        window.location.href = '/StockManagement/ExportRejectedData?ListId=' + ListId;
    };

    $('#tblPricelistId').DataTable({
        "pageLength": 100,
        "ordering": false,
        "sorting": true
    });
});
!(function ($) {
    $(document).ready(function () {
        CallbookinglistView();

    });

    $('#bannerTextMessage').on('click', function () {
        var id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/GetAnnouncementMessage?id=' + id,
            type: 'Get',
            success: function (result) {
                $('#hotel_id').val(result.getBusinessInfo.hotel_id);
                $('#AnnouncemntMessage').val(result.getBusinessInfo.announcemntMessage);
                $('#AnnouncementMessageHeader').modal('show');
            }
        });
    });

    $('#AnnouncementMessageHeader').on('click', '#SubmitMessage', function () {
        var add = {
            hotel_id: $('#hotel_id').val(),
            AnnouncemntMessage: $('#AnnouncemntMessage').val(),
        };
        $.ajax({
            url: '/Sale/EditAnnoucement',
            type: 'POST',
            data: add,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Annoucement Message Updated Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        $('#AnnouncementMessageHeader').modal('hide');
                        location.reload();
                    })
                }
            }
        });
    })

    $(document).ready(function () {
        $('#m_portlet_loader').css('display', 'block');
        $('#m_portlet_loader').css('display', 'none');
    });

    $(document).ready(function () {
        $('#m_portlet_loader').css('display', 'block');
        $('#m_portlet_loader').css('display', 'none');
        $("#announcement_bttn").click(function () {
            $(".announcement-icon").css("display", "none");
            $(".loader-icon").css("display", "inline-block");
            setTimeout(function () {
                $(".announcement-icon").css("display", "inline-block");
                $(".loader-icon").css("display", "none");
            }, 500)
        });
    });

    var $ContactNo = $('#ContactNo'),
        $btnCustomerAdd = $('#btnCustomerAdd'),
        $CustomerDetailId = $('#CustomerDetailId'),
        $btnSearchCustomer = $('#btnSearchCustomer'),
        $GetcustomerNo = $('#GetcustomerNo'),
        $CustomerId = $('#CustomerId'),
        $Name = $('#Name'),
        $EmailId = $('#EmailId'),
        $Ext = $('#Ext'),
        $BuildingName = $('#BuildingName'),
        $RoomNo = $('#RoomNo'),
        $Sector = $('#Sector'),
        $Landmark = $('#Landmark'),
        $Locality = $('#Locality'),
        $AddressType = $('#AddressType'),
        $ZipCode = $('#ZipCode'),
        $City = $('#City'),
        $State = $('#State'),
        $Country = $('#Country'),
        $AddId = $('#AddId'),
        $custId = $('#Id'),
        $Type = $('#Type'),
        $cusdetailId = $('#cusdetailId'),
        $custdetailorgid = $('#custdetailorgid'),
        $customerName = $('#customerName'),
        $customermobileno = $('#customermobileno'),
        $spnaddress = $('#spnaddress'),
        $customerappinstall = $('#customerappinstall'),
        $ContactNo_err = $('#ContactNo_err'),
        $customerId = $('#custId'),
        $customerNumber = $('#customerNumber'),
        $custName = $('#custName'),
        $totalGuest = $('#totalGuest'), CartBodyList
        $perferencType = $('#perferencType'),
        numberOfErrors = 0;
    $('.Orderstatus').on('click', function () {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to close!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, close it!'
        }).then((result) => {
            var SalesId = $(this)[0].dataset.id;
            var OrderdStatus = $(this)[0].dataset.update;
            if (result.value) {
                var info = {
                    SalesId: SalesId,
                    OrderdStatus: OrderdStatus,
                }
                $.ajax({
                    url: '/Sale/CloseOrder',
                    type: 'POST',
                    data: info,
                    success: function (result) {
                        if (result.Data == true && result.IsSuccess == true) {
                            $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                            window.location.reload();
                        }
                        else {
                            swal.fire("error", "Something went wrong!", "error");
                        }
                    }
                });
            }
        });
    });
    //    $('.Orderstatuscancel').on('click', function () {
    //        Swal.fire({
    //            title: 'Are you sure?',
    //            text: 'you want to Cancel!',
    //            showCancelButton: true,
    //            confirmButtonColor: '#3085d6',
    //            cancelButtonColor: '#d33',
    //            confirmButtonText: 'Yes, cancel it!'
    //        }).then((result) => {
    //            var SalesId = $(this)[0].dataset.id;
    //            var OrderdStatus = $(this)[0].dataset.update;
    //            if (result.value) {
    //                var info = {
    //                    SalesId: SalesId,
    //                    OrderdStatus: OrderdStatus,
    //                }
    //                $.ajax({
    //                    url: '/Sale/CloseOrder',
    //                    type: 'POST',
    //                    data: info,
    //                    success: function (result) {
    //                        if (result.Data == true && result.IsSuccess == true) {
    //                            $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
    //                            window.location.reload();
    //                        }
    //                        else {
    //                            swal.fire("error", "Something went wrong!", "error");
    //                        }
    //                    }
    //                });
    //            }
    //        });
    //});
    $('#CancelledOrder').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $('#SalesId').val(SalesId);
        $('#Cancel_Order').modal('show');
    });
    $('#Cancel_Order').on('click', "#CancelReason", function () {
        var SalesId = $('#SalesId').val();
        var OrderdStatus = $(this)[0].dataset.update;
        var CancellationReason = $('#CancellationReason').val();
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to Cancel!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, cancel it!'
        }).then((result) => {
            if (result.value) {
                var info = {
                    SalesId: SalesId,
                    OrderdStatus: OrderdStatus,
                    CancellationReason: CancellationReason,
                }
                $.ajax({
                    url: '/Sale/CloseOrder',
                    type: 'POST',
                    data: info,
                    success: function (result) {
                        if (result.Data == true && result.IsSuccess == true) {
                            $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                            window.location.reload();
                        }
                        else {
                            swal.fire("error", "Something went wrong!", "error");
                        }
                    }
                });
            }
        });
    });
    $('.applyCoupenCode').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#applyCouponCode').modal('show');
                $('#applyCouponCode').modal('show');
            }
        });
    });

    $('.applyCashDiscount').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#applyCashDiscount').modal('show');
            }
        });
    });

    $('.editCustomer').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#changeCustomerMd').modal('show');
            }
        });
    });

    $('.kotPrintListView').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                if (result != "") {
                    $('.salesData').html(result);
                    $('#kotPrintListView').modal('show');
                }
                else {
                    $('.salesData').html(result);
                    $('#kotPrintListView').modal('show');
                }               
            },
            error: function () {
                alert('server error');
            }
        });
    });

    $('.viewItems').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#ItemListView').modal('show');
            }
        });
    });

    $('.kot_log_md').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#kot_log_md').modal('show');
            }
        });
    });

    $('.allTblKot').on('click', function () {
        getAllSalesOrderId();
        var SalesId = getAllSalesOrderId();
        $.ajax({
            url: '/Sale/_SaleAllTblKOT?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#AlltblkotPrintListView').modal('show');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal.fire('OOps!', 'There was no order For KOT Print.', 'Try again later.');
            }
        });
    });

    var getAllSalesOrderId = function () {
        return $('.allSalesId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    $('.ConsolidateTblKot').on('click', function () {
        getAllTblListId();
        var SalesId = getAllTblListId();
        $.ajax({
            url: '/Sale/_SaleConsolidateTblKOT?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#ConsolidatetblkotPrintListView').modal('show');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal.fire('OOps!', 'There was no order For KOT Print.', 'Try again later.');
            }
        });
    });

    var getAllTblListId = function () {
        return $('.ConsolidateSalesId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    $('.paymentStatus').on('click', function () {
        var SalesId = $(this)[0].dataset.id;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateList?Id=' + SalesId,
            type: 'GET',
            success: function (result) {
                $('.salesData').html(result);
                $('#paymentStatus').modal('show');
            }
        });
    });

    $('#applyCouponCode').on('click', '#btnCouponSubmit', function () {
        CoupenCode();
        TotalofTable();
        if ($('#CoupenId').val() != 0) {
            var couponId = $('#CoupenId').prop('value');
            var SalesOrderId = $('#SalesOrderId').text();
            var SalesOrderId = $('#SalesOrderId').text();
            var CustomerId = $('#CustId').text();
            var mainArray = [];
            var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
                //alert(this.id);
                var Array1 = {
                    'SalesId': SalesOrderId,
                    'CustomerId': CustomerId,
                    'Category': $('#Category_' + this.id).val(),
                    'ItemId': $('#ItemId_' + this.id).val(),
                    'PriceId': $('#PriceId_' + this.id).val(),
                    'Measurement': $('#Measurements_' + this.id).val(),
                    'QuantityValue': $('#quantity_' + this.id).val(),
                    'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
                    'TotalPrice': $('#TotalPriceList_' + this.id).val(),
                    'Discount': $('#DiscountListId_' + this.id).val(),
                    'Weight': $('#ItemWeight_' + this.id).val(),
                    'Status': $('#ItemStatus_' + this.id).val()
                };
                mainArray.push(Array1);
            }));
            $.ajax({
                url: '/Sale/ApplyCoupen',
                type: 'POST',
                data: {
                    info: mainArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId
                },
                success: function (data) {
                    if (data.IsSuccess == true) {
                        $('#applyCouponCode').modal();
                    }
                    else
                        swal.fire('Error!', 'Something went wrong.', 'error');
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal.fire('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        }
    });

    $('#paymentStatus').on('click', '#BtnPaymentStatus', function () {
        var PaymentStatus = $('#PaymentStatusId').val();
        var ChangePaymentStatusofProduct = {
            PaymentStatus: $('#PaymentStatusId').val(),
            SalesOrderId: $('#SalesOrderId').text(),
            CustomerId: $('#CustomerIds').val(),
            PaymentMode: $('#PaymentModeId').prop('value')
        };
        $.ajax({
            url: '/Sale/UpdatePaymentStatus',
            type: 'POST',
            data: ChangePaymentStatusofProduct,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Payment Status Change Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        location.reload();
                    })
                }
            }
        });
    })

    $("document").ready(function () {
        var target = $('.categorytabs a[data-toggle="tab"]').attr("href");
        $('#SearchItem').prop('value', "");
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        var maincategory = target.slice(4);
        $.selector_cache('#activeTab').prop('value', target);
        $.ajax({
            type: 'GET',
            url: '/Sale/_ItemListData/',
            data: { 'ItemName': ItemName, 'maincategory': maincategory },
            success: function (data) {
                if (data !== null && data.IsSuccess) {
                    $('#CartBodyList_' + maincategory).html(data.Data);
                } else if (data !== null) {
                    swal('Error!', data.ReturnMessage, 'error');
                } else {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
    });

    $('.maincategory').on('shown.bs.tab', function (e) {
        $('#SearchItem').prop('value', "");
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        var catId = "";
        var catName = "";
        var target = $(e.target).attr("href");
        var maincategory = target.slice(5);
        $.selector_cache('#activeTab').prop('value', target);
        $('.itemtabpane').removeClass('active');
        $('.category').removeClass('active show');
        $.ajax({
            url: "/Sale/CategoryListMainCatWise?MainCatId=" + maincategory,
            type: "GET",
            success: function (itemvList) {
                if (itemvList.length > 0) {
                    catId = itemvList[0].categoryId;
                    catName = itemvList[0].category;
                    var categoryId = '#CT_' + catId;
                    $('.categorytabs a[href="' + categoryId + '"] ').addClass('active show');
                    $('#CT_' + catId).addClass('active show');
                }
                if (catId != "") {
                    $.ajax({
                        type: 'GET',
                        url: '/Sale/_ItemListData/',
                        data: { 'ItemName': ItemName, 'maincategory': catId },
                        success: function (data) {
                            if (data !== null && data.IsSuccess) {
                                $('#CartBodyList_' + catId).html(data.Data);
                            } else if (data !== null) {
                                swal('Error!', data.ReturnMessage, 'error');
                            } else {
                                swal('Error!', 'There was some error. Try again later.', 'error');
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            swal('Error!', 'There was some error. Try again later.', 'error');
                        }
                    });
                }
            },
        });
    });

    $('.category').on('shown.bs.tab', function (e) {
        $('#SearchItem').prop('value', "");
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        var target = $(e.target).attr("href");
        var maincategory = target.slice(4);
        $.ajax({
            type: 'GET',
            url: '/Sale/_ItemListData/',
            data: { 'ItemName': ItemName, 'maincategory': maincategory },
            success: function (data) {
                if (data !== null && data.IsSuccess) {
                    $('#CartBodyList_' + maincategory).html(data.Data);
                } else if (data !== null) {
                    swal('Error!', data.ReturnMessage, 'error');
                } else {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        });
    });

    $(".card").on('click', '#add_Items',function () {
        var TableIds = $(this)[0].dataset.id;
        var orderType = $(this)[0].dataset.type;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateListPanelData?id=' + TableIds + '&Ordertype=' + orderType,
            type: 'GET',
            cache:false,
            success: function (result) {
                $('.tableName').html(result);
                $(".addItems").toggleClass("toggle-panel");
            }
        });
    });

    $(".ordertypes").click(function () {
        var TableIds = $(this)[0].dataset.type;
        $.ajax({
            url: '/Sale/_SaleDetailsTblStateListPanelData?id=' + TableIds,
            type: 'GET',
            success: function (result) {
                $('.tableName').html(result);
                $(".addItems").toggleClass("toggle-panel");
            }
        });
    });

    $("#close_add_item").click(function () {
        $(".addItems").removeClass("toggle-panel");
        $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
        window.location.reload();
    });

    $ContactNo.on('keyup', function () {
        $ContactNo.css('border-color', '');
        $ContactNo_err.html('');
    });

    function validateMobileNumber(inputVal) {
        return !(/^[0-9]{10}$/.test(inputVal));
    }

    $btnCustomerAdd.on('click', function () {
        var numberOfErrors = 0;
        if ($ContactNo.val().trim().length > 1) {
            if (validateMobileNumber($ContactNo.val())) {
                numberOfErrors++;
                $ContactNo_err.html("Please Enter valid Phone Number").css('display', 'block');
            }
        }
        var custDetail = {
            Id: $custId.val(),
            CustomerId: $CustomerId.val(),
            Name: $Name.val(),
            Ext: $Ext.val(),
            ContactNo: $ContactNo.val(),
            EmailId: $EmailId.val(),
            BuildingName: $BuildingName.val(),
            RoomNo: $RoomNo.val(),
            Sector: $Sector.val(),
            Landmark: $Landmark.val(),
            Locality: $Locality.val(),
            AddressType: $AddressType.val(),
            ZipCode: $ZipCode.val(),
            City: $City.val(),
            State: $State.val(),
            Country: $Country.val(),
            Addids: $('#addid').val(),
            Addid: $('#compAddId').val()
        };
        var data = $('#GetcustomerNo').val();
        if (numberOfErrors === 0) {
            swal({
                title: "Are you sure?",
                text: "You want to Create the Customer",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes, send it!"
            }).then(function (e) {
                if (e.value) {
                    $.ajax({
                        url: '/Sale/CreateCustomers',
                        type: 'Post',
                        dataType: 'json',
                        data: custDetail,
                        success: function (data) {
                            if (data.customerId !== null && data.addId !== null) {
                                var custdata = data.customerId.split('_');
                                var custId = custdata[0];
                                $('#create_new_address').modal('hide');
                                $.ajax({
                                    url: '/Sale/_CustMultiAdd',
                                    type: 'Get',
                                    data: { Id: custId },
                                    success: function (data) {
                                        $('#tblcustaddressdetail').html(data);
                                        $GetcustomerNo.val('');
                                        $.ajax({
                                            type: 'GET',
                                            url: '/Sale/GetCustomerData?Type=Default&Id=' + custId,
                                            success: function (result) {
                                                $CustomerId.val(result.getCustomerDetail.customerId);
                                                $Name.val(result.getCustomerDetail.name);
                                                $EmailId.val(result.getCustomerDetail.emailId);
                                                $Ext.val(result.getCustomerDetail.ext);
                                                $ContactNo.val(result.getCustomerDetail.contactNo);
                                                //$BuildingName.val(result.getCustomerDetail.buildingName);
                                                //$RoomNo.val(result.getCustomerDetail.roomNo);  
                                                //$Sector.val(result.getCustomerDetail.sector); 
                                                //$Landmark.val(result.getCustomerDetail.landmark);  
                                                //$Locality.val(result.getCustomerDetail.locality);  
                                                $AddressType.val(result.getCustomerDetail.addressType);
                                                $ZipCode.val(result.getCustomerDetail.zipCode);
                                                $City.val(result.getCustomerDetail.city);
                                                $State.val(result.getCustomerDetail.state);
                                                $Country.val(result.getCustomerDetail.country);
                                                $cusdetailId.text(result.getCustomerDetail.customerId);
                                                $customerName.text(result.getCustomerDetail.name);
                                                $AddId.val(result.getCustomerDetail.addId);
                                                $spnaddress.val(result.getCustomerDetail.computeAddIds);
                                                $custId.val(result.getCustomerDetail.id);
                                                $custdetailorgid.text(result.getCustomerDetail.id);
                                                $Type.val(result.getCustomerDetail.type);
                                                result.getCustomerDetail.contactNo === null ? "NA" : $customermobileno.text(result.getCustomerDetail.contactNo);
                                            }
                                        });
                                    }
                                });
                            }
                            else {
                                $('#create_new_address').modal('show');
                            }
                        }
                    });
                }
            });
        }
    });

    $.selector_cache('#GetcustomerNo').select2({
        containerCssClass: "select2-data-ajax",
        placeholder: 'Select Customer Name/Number',
        ajax: {
            url: '/Sale/GetCustomerContactNo',
            dataType: "json",
            delay: 250,
            data: function (e) {
                return {
                    q: e.term,
                    page: e.page
                };
            },
            processResults: function (data, t) {
                var results = [];
                for (i = 0; i < data.Data.length; i++) {
                    results.push({
                        id: data.Data[i].id,
                        text: 'CI0' + data.Data[i].id + '-' + data.Data[i].contactNo + '-' + data.Data[i].name
                    });
                }
                if (results.length === 0) {
                    swal({
                        title: "Add Custumer?",
                        type: "warning",
                        showCancelButton: !0,
                        confirmButtonText: "Yes,create!"
                    }).then(function (e) {
                        if (e.value) {
                            var regex = /^[a-zA-Z]+$/;
                            if (!t.term.match(regex)) {
                                $ContactNo.val(t.term);
                            }
                            else {
                                $ContactNo.val('');
                            }
                            var regex = /^[0-9]+$/;
                            if (!t.term.match(regex)) {
                                $Name.val(t.term);
                            }
                            else {
                                $Name.val('');
                            }
                            $CustomerId.val('');
                            $EmailId.val('');
                            $Ext.val('');
                            $BuildingName.val('');
                            $RoomNo.val('');
                            $Sector.val('');
                            $Landmark.val('');
                            $Locality.val('');
                            $AddressType.val(0);
                            $ZipCode.val('');
                            $City.val('');
                            $State.val('');
                            $Country.val('');
                            $cusdetailId.text('');
                            $customerName.text('');
                            $AddId.val('');
                            $custId.val('');
                            $Type.val('');
                            $customermobileno.text('');
                            $('#CustomerId').val('');
                            $('#Id').val('');
                            $('#create_new_address').modal('show');
                        }
                    });
                }
                return {
                    results: results,
                    pagination: {
                        more: false
                    }
                };
            },
            cache: !0
        },
        escapeMarkup: function (e) {
            return e;
        },
        minimumInputLength: 1
    });

    $.selector_cache('#GetcustomerNo').on('change', function (e) {
        var test = $(this).select2('data');
        var CustomerId = test[0].id;
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
            success: function (result) {
                if (result.GetCustomerDetail != null) {
                    $CustomerId.val(result.GetCustomerDetail.CustomerId);
                    $Name.val(result.GetCustomerDetail.Name);
                    $EmailId.val(result.GetCustomerDetail.EmailId);
                    $ContactNo.val(result.GetCustomerDetail.ContactNo);                   
                    $ZipCode.val(result.GetCustomerDetail.ZipCode);
                    $City.val(result.GetCustomerDetail.City);
                    $State.val(result.GetCustomerDetail.State);
                    $Country.val(result.GetCustomerDetail.Country);
                    $cusdetailId.text(result.GetCustomerDetail.CustomerId);
                    $custdetailorgid.text(result.GetCustomerDetail.Id);
                    $customerName.text(result.GetCustomerDetail.Name);
                    $AddId.val(result.GetCustomerDetail.AddId);
                    $spnaddress.val(result.GetCustomerDetail.AddId);
                    $custId.val(result.GetCustomerDetail.Id);
                    $Type.val(result.GetCustomerDetail.Type);
                    result.GetCustomerDetail.ZipCode != null ? $('#AddDiv').html(": " + result.GetCustomerDetail.Address1) : $('#AddDiv').html(":      -");
                    result.GetCustomerDetail.ContactNo === null ? "NA" : $customermobileno.text(result.GetCustomerDetail.ContactNo);
                    $('#customerappinstall').text("No");
                    $('#spnappinstall').removeClass("m-badge--success");
                    $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
                    $('#AddDiv').html(": " + result.GetCustomerDetail.Address1);
                    $('#spnaddress').text(result.GetCustomerDetail.ComputeAddIds);
                    $('.custName').text(result.GetCustomerDetail.Name);
                    if (result.GetCustomerDetail.buildingName !== null) {
                        $.ajax({
                            url: '/Sale/CoupenList',
                            type: 'Get',
                            data: { CustomerId: result.GetCustomerDetail.CustomerId },
                            success: function (data) {
                                var select = $('#selectCoupen');
                                select.empty().append($("<option />").val("0").text("Select Coupen").attr("selected", "true"));
                                select.append($("<option />").val("1").text("Apply Cash Discount").attr("selected", "true"));
                                $.each(data, function () {
                                    select.append($("<option />").val(this.value).text(this.text));
                                });
                            }
                        });
                        $.ajax({
                            url: '/Sale/_CustMultiAdd',
                            type: 'Get',
                            data: { Id: CustomerId },
                            success: function (data) {
                                $('#tblcustaddressdetail').html(data);
                            }
                        });
                    }
                    else {
                        $('#tblcustaddressdetail').html(' ');
                    }
                }
                else {
                    swal("Customer Details Is Not Proper!", "", "warning");
                }
            }, error: function () {
                alert('errror');
            }
        });
    });

    $(document).ready(function (e) {
        var test = $('#GetcustomerNo').select2('data');
        var CustomerId = test[0].id;
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
            dataType: "json",
            contentType: "application/json",
            success: function (result) {
                if (result.GetCustomerDetail != null) {
                    $('.custName').text(result.GetCustomerDetail.Name);
                }
                else {
                    swal("Customer Details Is Not Proper!", "", "warning");
                }
            },error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    });

    $(document).on('click', '.btn-number', function (e) {
        e.preventDefault();
        var Id = $(this).attr('data-id');
        fieldName = $(this).attr('data-field');
        type = $(this).attr('data-type');
        var input1 = $("input[name='" + fieldName + "']");
        var input = $("#input-number_" + Id);
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if (type == 'minus') {

                if (currentVal > input.attr('min')) {
                    input.val(currentVal - 1).change();
                    var quantity = $('#input-number_' + Id).val();
                    var price = $('#SpnItemPrice_' + Id).text();
                    var market = $('#MarketPrice_' + Id).text();
                    var netprice = 0.0;
                    if ($('#input-number_' + Id).val() == '') {
                        $('#actualPrice_' + Id).val(parseFloat(price));
                        $('#hdMarket_' + Id).text(parseFloat(market));
                    }
                    else {
                        netprice = parseFloat(quantity * price).toFixed(2);
                        market = parseFloat(quantity * market).toFixed(2);
                        market = Math.round(market);
                        netprice = Math.round(netprice);
                        $('#actualPrice_' + Id).val(netprice);
                        $('#hdMarket_' + Id).text(market);
                        $('#tempSearchAmount').text(parseFloat(netprice));
                    }
                }
                if (parseInt(input.val()) == input.attr('min')) {
                    $(this).attr('disabled', true);
                }

            } else if (type == 'plus') {
                if (currentVal < input.attr('max')) {
                    input.val(currentVal + 1).change();
                    var quantity = $('#input-number_' + Id).val();
                    var price = $('#SpnItemPrice_' + Id).text();
                    var market = $('#MarketPrice_' + Id).text();
                    var netprice = 0.0;
                    if ($('#input-number_' + Id).val() == '') {
                        $('#actualPrice_' + Id).val(parseFloat(price));
                        $('#hdMarket_' + Id).text(parseFloat(market));
                    }
                    else {
                        netprice = parseFloat(quantity * price).toFixed(2);
                        market = parseFloat(quantity * market).toFixed(2);
                        market = Math.round(market);
                        netprice = Math.round(netprice);
                        $('#actualPrice_' + Id).val(netprice);
                        $('#hdMarket_' + Id).text(market);
                        $('#tempSearchAmount').text(parseFloat(netprice));
                    }
                }
                if (parseInt(input.val()) == input.attr('max')) {
                    $(this).attr('disabled', true);
                }
            }
        } else {
            input.val(0);
        }
    });

    $(document).focusin('.input-number', function () {
        $(this).data('oldValue', $(this).val());
    })

    $(document).on('change', '.input-number', function (e) {
        e.preventDefault();
        var Id = $(this).attr('data-id');
        minValue = parseInt($(this).attr('min'));
        maxValue = parseInt($(this).attr('max'));
        valueCurrent = parseInt($(this).val());
        name = $(this).attr('name');
        if (valueCurrent >= minValue) {
            var quantity = $('#input-number_' + Id).val();
            var price = $('#SpnItemPrice_' + Id).text();
            var market = $('#MarketPrice_' + Id).text();
            var netprice = 0.0;
            if ($('#input-number_' + Id).val() == '') {
                $('#actualPrice_' + Id).val(parseFloat(price));
                $('#hdMarket_' + Id).text(parseFloat(market));
            }
            else {
                netprice = parseFloat(quantity * price).toFixed(2);
                market = parseFloat(quantity * market).toFixed(2);
                market = Math.round(market);
                netprice = Math.round(netprice);
                $('#actualPrice_' + Id).val(netprice);
                $('#hdMarket_' + Id).text(market);
                $('#tempSearchAmount').text(parseFloat(netprice));
            }
            $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            alert('Sorry, the minimum value was reached');
            //$(this).val($(this).data('oldValue'));
            $(this).val(0);
            $('#actualPrice_' + Id).val("00.00");
        }
        if (valueCurrent <= maxValue) {
            var quantity = $('#input-number_' + Id).val();
            var price = $('#SpnItemPrice_' + Id).text();
            var market = $('#MarketPrice_' + Id).text();
            var netprice = 0.0;
            if ($('#input-number_' + Id).val() == '') {
                $('#actualPrice_' + Id).val(parseFloat(price));
                $('#hdMarket_' + Id).text(parseFloat(market));
            }
            else {
                netprice = parseFloat(quantity * price).toFixed(2);
                market = parseFloat(quantity * market).toFixed(2);
                market = Math.round(market);
                netprice = Math.round(netprice);
                $('#actualPrice_' + Id).val(netprice);
                $('#hdMarket_' + Id).text(market);
                $('#tempSearchAmount').text(parseFloat(netprice));
            }
            $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            alert('Sorry, the maximum value was reached');
            //$(this).val($(this).data('oldValue'));
            $(this).val(0);
            $('#actualPrice_' + Id).val("00.00");
        }
    });

    // ALL ITEMS VIEW SIDE PANEL
    $("#allItems_view").click(function () {
        $.ajax({
            url: '/Sale/_tablestate/',
            type: 'GET',
            success: function (result) {
                $('#AllitemBody').html(result);
                $(".all-items-panel").addClass("view");
            }
        });
    });

    // ALL ITEMS CLOSE SIDE PANEL
    $("#close_all_items").click(function () {
        $(".all-items-panel").removeClass("view");
    });

    $(document).on("click", ".btn-close", function (e) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to Close Item!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Close it!'
        }).then((result) => {
            var status = 'Close'
            var id = $(this)[0].dataset.id;
            var ItemId = $(this)[0].dataset.approval;
            if (result.value) {
                var info = {
                    SalesOrderId: id,
                    status: status,
                    ItemId: ItemId,
                }
                $.ajax({
                    url: '/Sale/ApprovalKitchenStatusUpdate',
                    type: 'GET',
                    data: info,
                    success: function (result) {
                        if (result.Data == true && result.IsSuccess == true) {
                            swal({
                                text: "Successfully Close Items",
                                type: "success",
                                confirmButtonText: "OK"
                            }).then(function (e) {
                                $.ajax({
                                    url: '/Sale/_tablestate/',
                                    type: 'GET',
                                    success: function (result) {
                                        $('#AllitemBody').html(result);
                                        $(".all-items-panel").addClass("view");
                                    }
                                });
                            })
                        }
                        else
                            swal.fire("error", "Something went wrong!", "error");
                    }
                });
            }
        });
    });

    $('.table-middle').on('change', '#selectallid', function (e) {
        var table = $(e.target).closest('table');
        $('td #chk1', table).prop('checked', this.checked);
    });

    var getChecked = function () {
        const salesId = [];
        const salesListId = [];
        $('#AllitemBody').find('input[type="checkbox"]')
            .filter(':checked')
            .toArray()
            .map(function (x) {
                var a = $(x).attr('data-saleid');
                var b = $(x).attr('data-saleListid');
                salesId.push(a);
                salesListId.push(b);
            });
        var V = { salesId, salesListId };
        return V;
    }

    $(document).on('click', '#AddItemId', function () {
        var id = getChecked();
        var salesId = id.salesId;
        var salesListId = id.salesListId;
        var status = 'Closed'
        if (salesId.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to Close This item !',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Close it!'
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        salesId: salesId,
                        salesListId: salesListId,
                        status: status
                    };
                    $.ajax({
                        url: '/Sale/ApprovalAllKitchenStatusUpdate/',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (result) {
                            if (result.IsSuccess == true) {
                                swal({
                                    text: "Successfully Close Items",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    $.ajax({
                                        url: '/Sale/_tablestate/',
                                        type: 'GET',
                                        success: function (result) {
                                            $('#AllitemBody').html(result);
                                            $(".all-items-panel").addClass("view");
                                        }
                                    });
                                })
                            }
                        }
                    });
                }
            });
        }
    });

    $.selector_cache('#custNumber').on('change', function (e) {
        var contacNumber = $(this).val();
        $.ajax({
            type: 'GET',
            url: '/Sale/GetCustomerDetailsData?contactNo=' + contacNumber,
            success: function (result) {
                if (result != null) {
                    $customerId.val(result.customerId);
                    $custName.val(result.name);
                    $customerNumber.val(result.contactNo);
                }
                else {
                    swal.fire("error", "Customer Details Not Found !", "error");
                }
            }
        });
    });

    $.selector_cache('#custNumber').on('keyup', function (e) {
        $customerId.val('');
        $custName.val('');
        $customerNumber.val('');
    });

    $.selector_cache('#closewatching').on('click', function (e) {
        $('#perferencType').val(0);
        $('#totalGuest').val('');
        $('#custName').val('');
        $('#custNumber').val('');
        $('#custId').val('');
    });

    $('#addWatchMD').on('click', '#BookingSubmit', function () {
        var info = {
            custId: $('#custId').val(),
            custNumber: $('#custNumber').val(),
            custName: $('#custName').val(),
            totalGuest: $('#totalGuest').val(),
            slotTime: $('#slotTime').val(),
            timeType: $('#timeType').prop('value'),
            perferencType: $('#perferencType').prop('value'),
        };
        Swal.fire({
            title: 'Are you sure?',
            text: 'You want to Create!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Create it!'
        }).then((result) => {
            $.ajax({
                url: '/Sale/CreateBooking',
                type: 'POST',
                data: info,
                success: function (data) {
                    if (data.ReturnMessage == "success") {
                        swal("success", "Added Successfully!", "success").then(okay => {
                            if (okay) {
                                $('.custNumber').val(''),
                                    $('.custName').val(''),
                                    $('.totalGuest').val(''),
                                    $('#slotTime').val('')
                                    $('#perferencType').prop('value', 0),
                                    $('#addWatchMD').modal('hide');
                                    CallbookinglistView();
                                    window.location.reload();
                            }
                        });
                    }
                }
            });
        });
    })

    $('#bookingInfo').on('click', '#assingAdavncebooing', function () {
        var id = $(this).attr('data-id');
        var customerId = $(this).attr('data-customerid');
        var bookingIds = $(this).attr('data-bookingid');
        $.ajax({
            url: "/Sale/GetActionTriggerlist?id=" + id,
            type: "Get",
            success: function (getTablelist) {
                if (getTablelist.length > 0) {
                    var html = "";
                    html = '<option value="0" selected >Select Table</option>';
                    $.each(getTablelist, function (index, value) {
                        html += '<option value="' + value.value + '">' + value.text + '</option>';
                    });
                    $('#TriggerId').html(html);
                    $('#customerIds').val(customerId);
                    $('#bookingIds').val(bookingIds);
                    $('#assign_tableMD').modal('show');
                }
            }
        });
    })

    $('#assign_tableMD').on('click', '#Assginetable', function () {
        var dicData = {
            CustomerId: $('#customerIds').val(),
            OrderdStatus: 'Ordered',
            PaymentStatus: 'Pending',
            PaymentMode: 'Cash',
            TotalPrice: 0,
            TotaldisAmt: 0,
            SalesPerson: 'NA',
            TotalQuantity: 0,
            PLU_Count: 0,
            DeliveryCharges: 0,
            ActualDiscAmt: 0,
            Remaining_Amount: 0,
            DiscountedAmount: 0,
            OrderType: "Dine In",
            tableId: $('#TriggerId').val(),
            bookingIds: $('#bookingIds').val(),
        };
        $.ajax({
            url: '/Sale/InserttblBook',
            type: 'POST',
            data: dicData,
            success: function (salesId) {
                if (salesId !== false) {
                    swal("success", "Table allocate to the Customer Successfully!", "success").then(okay => {
                        if (okay) {
                            $('#assign_tableMD').modal('hide');
                            window.location.reload();
                        }
                    });
                }
            }
        });
    })

    $.selector_cache('#serachProduct').on('keyup', $.delay(function (e) {
       var ItemName = $.selector_cache("#serachProduct").prop('value');
        if (ItemName != "") {
            $('#clearinputElement').css('display', 'block');
            $.ajax({
                type: 'GET',
                url: '/Sale/_ItemListData/',
                data: { 'ItemName': ItemName, 'maincategory': "" },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $('.MainCategorytabs').css('display', 'none');
                        $('.fixed-right-height').css("margin-top", "0px");
                        $('#categoryesd').css('display', 'none');
                        $('.pbl').removeClass("col-md-4");
                        $('.pbl').addClass("col-md-6");
                        var categoryId = $('.categorytabs a[data-toggle="tab"]').attr("href").split('_')[1];
                        $(".itemtabpane").removeClass("active show");
                        $(".itemtabpane").first().addClass("active show");
                        $('#CartBodyList_' + categoryId).empty();
                        $('#CartBodyList_' + categoryId).html(data.Data);
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                    } else {
                        swal('Error!', 'There was some error. Try again later.', 'error');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        }
        else {
            getActiveData();
            $('.MainCategorytabs').css('display', 'block');
            $('.fixed-right-height').css("margin-top", "-2.8rem");
            $('#categoryesd').css('display', 'block');
            $('.pbl').removeClass("col-md-6");
            $('.pbl').addClass("col-md-4");
            //$('#CartBodyList22').html("");
            $('#clearinputElement').css('display', 'none');
        }
    }, 500));

    $('#clearinputElement').on('click', function () {
        $("#serachProduct").val("");
        $('.MainCategorytabs').css('display', 'block');
        $('.fixed-right-height').css("margin-top", "-2.8rem");
        $('#categoryesd').css('display', 'block');
        $('.pbl').removeClass("col-md-6");
        $('.pbl').addClass("col-md-4");
        $('#CartBodyList22').html("");
        $('#clearinputElement').css('display', 'none');
        getActiveData();
    });

    $.selector_cache('#customdatetoggle').on('click', function () {
        var data = $(this).is(':checked');
        if (!$('#customdatetoggle').is(':checked')) {
            $('#divddaterange').css('display', 'none');
            $('#divdate').css('display', 'none');
            var d = new Date();
            var data = d.getMonth() + 1 + '-' + d.getDate() + '-' + d.getFullYear();
            $('#TodayDate').val(data);
        }
        else {
            $('#divddaterange').css('display', 'block');
            $('#divdate').css('display', 'none');
            $('#TodayDate').val('');
        }
    });


    $('#SalesListPrint').on('click', '#SalestoPrint', function () {
        var numberofError = 0;
        if ($("#customdatetoggle").prop('checked') == true) {
            if ($('#salesdaterange').val() == "") {
                numberofError++;
                $('#SalesdaterangError').html("Cannot be Empty").css('display', 'block');
            }
        }
        else {
            if ($('#TodayDate').val() == "") {
                numberofError++;
                $('#SalesdateError').html("Cannot be Empty").css('display', 'block');
            }
        }
        if (numberofError == 0) {
            var datetimeprint = $('#TodayDate').val();
            var salesrangedate = $('#salesdaterange').val();
            window.location.href = '/Print/TodaySalesToPrint?Date=' + datetimeprint + '&DateRange=' + salesrangedate;
        }
    });
})(jQuery);

$(document).on('click', '#AddRemark', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    $('#remarksclosed_' + Id).css('display', 'flex');
});

$(document).on('click', '#ClosedRemark', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    $('#remarksclosed_' + Id).css('display', 'none'); 
});

$(document).on('click', '#AddCartId', function (e) {
    e.preventDefault();
    var Id = $(this).attr('data-id');
    var ItemId = $(this).attr('data-Itemid');
    var Size = $(this).attr('data-size');
    fieldName = $(this).attr('data-field');
    var input1 = $("input[name='" + fieldName + "']");
    var input = $("#input-number_" + ItemId);
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        var quantity = $('#input-number_' + ItemId).val();
        var price = $('#SpnItemPrice_' + Id).text();
        var market = $('#MarketPrice_' + Id).text();
        var netprice = 0.0;
        if ($('#input-number_' + Id).val() == '') {
            $('#actualPrice_' + Id).val(parseFloat(price));
            $('#hdMarket_' + Id).text(parseFloat(market));
        }
        else {
            netprice = parseFloat(quantity * price).toFixed(2);
            market = parseFloat(quantity * market).toFixed(2);
            market = market;
            netprice = netprice;
            $('#actualPrice_' + Id).val(netprice);
            $('#hdMarket_' + Id).text(market);
            $('#tempSearchAmount').text(parseFloat(netprice));
        }
    }
    var weightid = $('#priceweightId_' + Id).val();
    var totakCartquant = $('#totalCartQuantity').text();
    var Amount = $('#AmountId').text();
    var amount1 = $('#amount1').val();
    var actualPrice = $('#actualPrice_' + Id).val();
    var quantity = $('#input-number_' + ItemId).val();
    var remark = $('#remarkId_' + ItemId).val();
    var discount = $('#discountId_' + ItemId).val();
    var weight = $('#SpnItempriceWeight_' + Id).text();
    var total = $('#TotalCartItemId').text();
    var pluName = $('#SpnItemName_' + Id).text();
    var itemId = $('#SpnItemId_' + ItemId).text();
    var priceId = $('#SpnItempriceId_' + Id).text();
    var plucode = $('#spnpricePlucode_' + Id).text();
    var coupenDisc = 1;
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    if (!$('#discountId_' + ItemId).prop('value')) {
        discount = 0;
    }
    if (!$('#priceweightId_' + Id).prop('value')) {
        $('#priceweightId_' + Id).prop('value', weight);
    }
    if (!$('#input-number_' + ItemId).prop('value')) {
        quantity = 1;
    }
    if (!$('#remarkId_' + ItemId).prop('value')) {
        remark = "No Remark";
    }
    var AddedDiscount = parseFloat(actualPrice) + parseFloat(discount);
    var dicData = {
        PriceId: Id,
        ItemId: ItemId,
        PluName: $('#SpnItemName_' + Id).text(),
        Category: $('#SpnCategoryId_' + ItemId).text(),
        Measurement: $('#pricemeaure_' + Id).text(),
        Weight: weight,
        SellingPrice: $('#SpnItemPrice_' + Id).text(),
        MarketPrice: $('#hdMarket_' + Id).text(),
        CategoryName: $('#SpnCategory_' + ItemId).text(),
        Quantity: quantity,
        Remark: remark,
        Discount: discount,
        ActualCost: actualPrice,
        ItemType: $('#ItemType_' + ItemId).text(),
        itemstock: $('#TotalItemstock_' + ItemId).text(),
        ItemType: $('#MItemType_' + ItemId).text(),
        stockId: $('#stockId_' + ItemId).text(),
        CreatedBy: $('#CreatedBy').text(),
        Purchaseprice: $('#PurchasePrice_' + Id).text(),
        PluCode: $('#spnPlucode_' + Id).text(),
        CoupenDisc: coupenDisc,
        AddedDiscount: AddedDiscount,
        PId: priceId,
        Id: itemId,
        Size: Size,
    };
    $('#' + dicData.PriceId).remove();
    $('#divId_' + dicData.PriceId).remove();
    $.ajax({
        url: '/Sale/_AddCart',
        type: 'POST',
        data: dicData,
        success: function (data) {
            $('#stockId').text($('#stockId_' + ItemId).text());
            var existing = $('#CartListbody').html();
            var array = [];
            array.push(existing);
            array.push(data);
            $('#CartListbody').html(array);
            $('#Additem').modal('hide');
            SaveHomeDelivery(dicData);
            $.makeArray($('#CartListbody tr[id]').map(function () {
                var exitingquantity = $('#spanquantity_' + this.id).text();
                $('#quantity_' + this.id).val(exitingquantity);
                var exitingremark = $('#spnRemark_' + dicData.ItemId).text();
                $('#remark_' + dicData.ItemId).val(exitingremark);
                var exitingweight = $('#spnNetweight_' + this.id).text();
                $('#Netweight_' + this.id).val(exitingweight);
                var quantity = $('#quantity_' + this.id).val();
                var remark = $('#remark_' + dicData.ItemId).val();
                //var totalCost = $('#actualPrice_' + this.id).val(); // here Changes do it...
                var totalCost = $('#finalcost_' + this.id).val(); // here Changes do it...
                totalSaleCOst += parseFloat(totalCost);
                totalQuantity += parseFloat(quantity);
            }));
            var length = $("#CartListbody").find("tr").length;
            $('#TotalCartItemId').text(length);
            $('#totalCartQuantity').text(totalQuantity);
            $('#AmountId').text(totalSaleCOst);
            $('#Amountspn').text(totalSaleCOst);
            $('#amount1').prop('value', totalSaleCOst);
            $('#TDisAmountId').text(totalSaleCOst);//ToDo
            AddTotalItem();
            CoupenCode();
        }
    });
});

$(document).on('click', '#mymoney', function () {
    SubmitError = 0;
    if ($('#TotalItemId').text() > 0) {
        Submit();
    }
    else {
        if ($('#TotalCartItemId').text() > 0) {
            swal({
                title: "Are you sure you want to create sales order?",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes,create!"
            }).then(function (e) {
                if (e.value == true) {
                    mymoney();
                }
            });
        } else {
            if ($('#CartListbody').text() === "") {
                swal("No item in list to create order", "", "warning");
            }
        }
    }
});

function CoupenCode() {
    var selectcoupen = $('#selectCoupen').val();
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    if (selectcoupen == "1") {
        $('#divDisAmount').css('display', 'none');
        $('#divCashDisAmount').css('display', 'block');
        $('#Cashamount').prop('value', 0);
        $('#Discountamount').prop('value', 0)
    }
    else {
        $('#divDisAmount').css('display', 'block');
        $('#divCashDisAmount').css('display', 'none');
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var coupenId = coupen[1];
        var maxdiscount = coupen[2];
        $('#Discountamount').val(coupendiscount);
        var returnamt2 = $('#amount2').val();
        // var customeramt = $('#AmountId').text();
        $.makeArray($('#tblsaleslist tr[id]').map(function () {
            if ($('#coupenDisc_' + this.id).text() == 1) {
                var ItemCoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithCoupen += parseFloat(ItemCoupenCost);
                var discountpercent = (parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2);
                var discountedPrice = (parseFloat(ItemCoupenCost) - parseFloat(discountpercent)).toFixed(2);
                $('#unitprice_' + this.id).text(discountedPrice);
                $('#discount_' + this.id).text(discountpercent);
                $('#discountId_' + this.id).text(discountpercent);
            }
            else {
                var ItemWithoutoupenCost = $('#AddedDiscount_' + this.id).text();
                var discount = $('#discount_' + this.id).text();
                var discountedPrice = parseFloat(ItemWithoutoupenCost) - parseFloat(discount);
                ItemWithoutCoupen += parseFloat(discountedPrice);
            }
        }));
        $('#ItemWithCoupen').text(ItemWithCoupen);
        $('#ItemWitoutCoupen').text(ItemWithoutCoupen);
        var customeramt = $('#ItemWithCoupen').text();
        $('#coupenIds').val(coupenId);
        var discountpercent = (parseFloat(coupendiscount) * parseFloat(customeramt) / 100).toFixed(2);
        //discountpercent = Math.round(discountpercent);
        discountpercent = parseFloat(discountpercent) > parseFloat(maxdiscount) ? parseFloat(maxdiscount) : parseFloat(discountpercent);
        var acualamtbydiscount = (parseFloat(customeramt) - parseFloat(discountpercent)).toFixed(2);
        //acualamtbydiscount = Math.round(acualamtbydiscount);
        var TotalWithCoupen = parseFloat($('#ItemWitoutCoupen').text()) + parseFloat(acualamtbydiscount);
        //alert(TotalWithCoupen);   
        $('#DiscountTotalamt').val(TotalWithCoupen);
        TotalWithCoupen = Math.round(TotalWithCoupen);
        $('#amount1').val(TotalWithCoupen);
        $('#ActualDiscountAmt').val(discountpercent);
        $('#DisAmountId').text(discountpercent);
        $('#TDisAmountId').text(TotalWithCoupen);
        AddTotalAfterCoupen();
        if (returnamt2 != "") {
            var returnamt = parseFloat(returnamt2) - parseFloat(TotalWithCoupen);
            returnamt = Math.round(returnamt);
            $('#total').text(returnamt);
        }
        if ($('#total').text() > 0) {
            $('#total').removeClass("text-danger");
            $('#total').addClass("text-success");
        }
        else {
            $('#total').removeClass("text-success");
            $('#total').addClass("text-danger");
        }
    }
    //$('#TDisAmountId').text((parseFloat($('#AmountId').text()) + parseFloat($('#DisAmountId').text())).toFixed(2));
}

function AddTotalItem() {
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var DiscountPrice = 0;
    $.makeArray($('#CartListbody tr[id]').map(function () {
        var quantity = $('#quantity_' + this.id).val();
        var remark = $('#remark_' + this.id).val();
        var totalCost = $('#finalcost_' + this.id).val();
        var discount = $('#discount_' + this.id).text();
        totalSaleCOst += parseFloat(totalCost);
        totalQuantity += parseFloat(quantity);
        DiscountPrice += parseFloat(discount);
    }));
    var length = $("#CartListbody").find("tr").length;
    $('#TotalCartItemId').text(length);
    $('#totalCartQuantity').text(totalQuantity);
    var remark = $('#remark_' + this.id).val();
    $('#totalCartremark').text(remark);
    $('#AmountId').text(Math.round(totalSaleCOst));
    $('#Amountspn').text(Math.round(totalSaleCOst));
    $('#amount1').prop('value', Math.round(totalSaleCOst));
    $('#TDisAmountId').text(Math.round(totalSaleCOst));
    $('#DiscountTotalamt').prop('value', totalSaleCOst);
}

function remove(param) {
    var totalItem = $('#TotalCartItemId').text();
    var totalquan = $('#totalCartQuantity').text();
    var cartQuan = $('#quantity_' + param).val();
    var actualprice = $('#unitprice_' + param).text();
    var AmountId = $('#AmountId').text();
    var amount1 = $('#amount1').val();
    var amountspn = $('#TDisAmountId').text();
    var discountAmount = $('#DisAmountId').text();
    var ItemWithCoupen = 0;
    var ItemWithoutCoupen = 0;
    totalItem = parseInt(totalItem) - parseInt(1);
    $('#TotalCartItemId').text(totalItem);
    var minusweigh = parseInt(totalquan) - parseInt(cartQuan);
    $('#totalCartQuantity').text(minusweigh);
    AmountId = parseFloat(AmountId) - parseFloat(actualprice);
    $('#AmountId').text(AmountId);
    $('#TDisAmountId').text(AmountId);
    $('#amount1').val(AmountId);
    $('#' + param).remove();
    $('#divId_' + param).remove();
    AddTotalItem();
    CoupenCode();
}

function SaveHomeDelivery(dicData) {
    $.ajax({
        url: '/Sale/_HomeDelivery',
        type: 'POST',
        data: dicData,
        success: function (data) {
            var array = []
            var existing = $('#HomeDeliveryId').html();
            array.push(existing);
            array.push(data);
            $('#HomeDeliveryId').html(array);
        }
    });
}

function mymoney() {
    if (SubmitError == 0) {
        if (!$('#OrderStatusId').val()) {
            var orderstatus = "Ordered";
        }
        else {
            orderstatus = $('#OrderStatusId').val();
        }
        var paymentStatus = $('#PaymentStatus').val();
        var DeliveryType = "Walking";
        var customerdata = "";
        var Address = "";
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        today = mm + '/' + dd + '/' + yyyy;
        var deliveryDate = today;
        var deliveryCharge = 0;
        var nErrorCount = 0;
        if ($('#spnaddress').text() == "") {
            Address = "NA";
        }
        else {
            Address = $('#spnaddress').text();
        }
        var drpsalesperson = "";
        var paymentmode = $('#PaymentMode').val();
        var deliverytime = "NA";
        var Branch = "HID01";
        var TotalAmount = $('#amount1').val();
        var CashReceived = $('#amount2').val();
        var CashRemaining = $('#total').text();
        var salesId = $('#HomeSalesId').val();
        var TotalCartItem = $('#TotalCartItemId').text();
        var TotalQuantity = $('#totalCartQuantity').text();
        var remark = $('#totalCartremark').text();
        var Amountspn = $('#Amountspn').text();
        var discountper = $('#Discountamount').val();
        var Totaldisamt = $('#DiscountTotalamt').val();
        var ActualDisc = $('#ActualDiscountAmt').val();
        var coupenID = $('#coupenIds').val();
        var DiscountedCash = $('#Cashamount').val();
        if ($('#PaymentMode').val() == 0) {
            nErrorCount++;
            $('#SpnPaymentMode').html("This field is required").css('display', 'block');
        }
        if ($('#PaymentStatus').val() == 0) {
            nErrorCount++;
            $('#SpnPaymentstatus').html("This field is required").css('display', 'block');
        }
        if (nErrorCount === 0) {
            !$('#SearchDiscount').prop('value')
            if ($('#TotalItemId').text() > 0 && salesId == "") {
                swal("Item selected for home delivery but Sale not created!", "", "error");
            }
            else {
                if (0 === nErrorCount) {
                    var totalCount = 0;
                    var unitCost = "";
                    var ItemId = "";
                    var quantity = 0;
                    var weight;
                    var totalQuantity = 0;
                    var totalCost = 0;
                    var category;
                    var stock = "";
                    var measure;
                    var totalSaleCOst = 0.0;
                    var selltype;
                    var salesPerson = "";
                    var discount = 0;
                    var Hub = "NA";
                    var totalUnitCost = 0;
                    var homeDelivery = "";
                    var totalCount = 0;
                    var marketPrice = 0;
                    var totalmarketPrice = 0.0;
                    var savedPrice;
                    var ItemType = "";
                    var stocksid = 0;
                    var purchaseprice = 0;
                    var stockListss = new Array();
                    var stockData = {};
                    var itemDetails = $.makeArray($('#CartListbody tr[id]').map(function () {
                        var data = $('#delivery_' + this.id).text();
                        if ($('#delivery_' + this.id).text() === "no") {
                            try {
                                totalCount++;
                                ItemId = $('#ItemId_' + this.id).text();
                                unitCost = $('#unitprice_' + this.id).text();
                                quantity = $('#quantity_' + this.id).val();
                                remark = $('#remark_' + ItemId).val();
                                weight = $('#Netweight_' + this.id).val();
                                measure = $('#measure_' + this.id).text();
                                category = $('#cateId_' + ItemId).text();
                                totalCost = $('#finalcost_' + this.id).val();
                                stock = $('#Totalitemstock_' + this.id).text();
                                marketPrice = $('#Market_' + this.id).text();
                                purchaseprice = $('#purchaseprice_' + this.id).text();
                                discount = $('#discountId_' + ItemId).text();
                                homeDelivery = $('#delivery_' + this.id).text();
                                ItemType = $('#ItemType_' + ItemId).text();
                                stocksid = $('#stockId_' + ItemId).text();
                                stockData.unitCost = $('#unitprice_' + this.id).text();
                                stockListss.push(stockData);
                                if (ItemType == "Loose") {
                                    totalQuantity += parseFloat(weight);
                                }
                                else {
                                    totalQuantity += parseFloat(quantity);
                                }
                                totalSaleCOst += parseFloat(totalCost);
                                totalUnitCost += parseFloat(unitCost);
                                totalmarketPrice += parseFloat(marketPrice);
                            }
                            catch (e) {}
                            return ItemId + "_" + this.id + "_" + unitCost + "_" + quantity + "_" + remark + "_" + category + "_" + totalCost + "_" + weight + "_" + measure + "_" + stock + "_" + discount + "_" + stocksid + "_" + ItemType + "_" + purchaseprice + "|";
                        }
                    }));
                    var dicData = {
                        CustomerId: $('#cusdetailId').text(),
                        OrderdStatus: orderstatus,
                        SlotId: deliverytime,
                        PaymentStatus: paymentStatus,
                        PaymentMode: paymentmode,
                        AddressId: Address,
                        HubId: Hub,
                        Branch: Branch,
                        TotalPrice: totalSaleCOst,
                        SalesPerson: salesPerson,
                        TotalQuantity: totalQuantity,
                        Remark: remark,
                        PLU_Count: totalCount,
                        MultipleItem: itemDetails,
                        MultipleItem1: itemDetails,
                        DeliveryCharges: deliveryCharge,
                        Multiplestockrecord: itemDetails,
                        TotaldisAmt: Totaldisamt,
                        ActualDiscAmt: ActualDisc,
                        coupenId: coupenID,
                        DeliveryType: DeliveryType,
                        Remaining_Amount: $('#total').text(),
                        DiscountedCash: DiscountedCash,
                        tableId: $('#TableId').text(),
                        OrderType: $('#OrderType').text(),
                    };
                    $('#SavedPriceId').text(Math.round(parseFloat(totalmarketPrice).toFixed(2) - parseFloat(totalSaleCOst).toFixed(2)));
                    if (unitCost !== "" && totalQuantity !== "" && totalCost !== "" && totalSaleCOst !== "") {
                        $.ajax({
                            url: '/Sale/InsertSale',
                            type: 'POST',
                            dataType: 'JSON',
                            data: dicData,
                            success: function (data) {
                                var tempId = null;
                                if (data !== false) {
                                    if (data != null && tempId == null) {
                                        $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                                        location.reload();
                                    }
                                    //if (salesId == "") {
                                    //    setTimeout(function () {
                                    //        window.location.href = "/Print/Index?id1=" + data + "&received=" + CashReceived + "&remaining=" + CashRemaining + "&saved=" + $('#SavedPriceId').text() + "&AmountTotal=" + $('#AmountId').text() + "&quant=" + TotalQuantity + "&item=" + TotalCartItem + '&DiscountPer=' + discountper + '&TotalDiscountAmt=' + Totaldisamt + '&ActualDiscountAmt=' + $('#ActualDiscountAmt').val();

                                    //    }, 2000)
                                    //}
                                    //else {
                                    //    setTimeout(function () {
                                    //        window.location.href = "/Print/Index?id1=" + data + "&id2=" + salesId + "&received=" + CashReceived + "&remaining=" + CashRemaining + "&saved=" + $('#SavedPriceId').text() + "&AmountTotal=" + $('#AmountId').text() + "&quant=" + TotalQuantity + "&item=" + TotalCartItem + '&DiscountPer=' + discountper + '&TotalDiscountAmt=' + Totaldisamt + '&ActualDiscountAmt=' + $('#ActualDiscountAmt').val();

                                    //    }, 2000)
                                    //}
                                }
                                else {
                                    swal('Oops!', 'Something went wrong!', 'warning');
                                }
                            }
                        });
                    }
                }
                else {
                    swal("No Item In Cart!", "", "warning");
                }
            }
        }
    }
}

function Submit() {
    var orderstatus = "Confirmed";
    var paymentStatus = $('#PaymentStatus').val();
    var deliveryDate = "";
    var customerdata = "";
    var drpsalesperson = "";
    var paymentmode = $('#PaymentMode').val();
    var deliverytime = $('#SlotId').val();
    var CashReceived = $('#amount2').val();
    var CashRemaining = $('#total').text();
    var discountper = $('#Discountamount').val();
    var Totaldisamt = $('#DiscountTotalamt').val();
    var ActualDisc = $('#ActualDiscountAmt').val();
    var coupenID = $('#coupenIds').val();
    var tableId = $('#selecttableId').val();
    var customerId = $('#cusdetailId').text();
    var Address = "";
    var Branch = "";
    var DiscountedAmount = $('#Cashamount').val();
    var checkedValues = $('input:checkbox[name=Check]:checked').length;
    if (checkedValues === 0) {
        swal("No item in list to create order", "", "warning");
        SubmitError++;
    }
    if (checkedValues > 0) {
        var deliveryCharge = $('#Deliverycharges').val();
        if ($('#DeliveryDateId').val() === "") {
            $('#SpnDeliverydate').html("This field is required").css('display', 'block');
            SubmitError++;
        }
        if ($('#PaymentStatus').val() === "") {
            $('#SpnPaymentstatus').html("This field is required").css('display', 'block');
            SubmitError++;
        }
        if ($('#SlotId').val() === "") {
            $('#SpnSlotId').html("This field is required").css('display', 'block');
            SubmitError++;
        }
        if ($('#spnaddress').text() === "" || $('#spnaddress').text() === null) {
            swal({
                title: "Address Required",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes,create!"
            }).then(function (e) {
                if (e.value) {
                    $('#create_new_address').modal('show');
                }
                //swal("Address Required", "", "warning");
            });
            SubmitError++;
        }
    }
    if (0 === SubmitError) {
        swal({
            title: "Are you sure you want to home deliver?",
            type: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes!"
        }).then(function (e) {
            if (e.value) {
                var totalCount = 0;
                var unitCost = "";
                var quantity = 0;
                var weight;
                var totalQuantity = 0;
                var totalCost = 0;
                var category;
                var stock = 0;
                var measure;
                var totalSaleCOst = 0.0;
                var selltype;
                var salesPerson = "";
                var marketPrice = 0;
                var totalmarketPrice = 0;
                var discount = 0;
                var Hub = "NA";
                var homeDelivery = "";
                var stocksid = 0;
                var purchaseprice = 0;
                var stockListss = new Array();
                var stockData = {};
                var deliveryTypes = "";
                var delidate = $('#DeliveryDateId').val();
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1;
                var yyyy = today.getFullYear();
                if (mm < 10) mm = '0' + mm;
                today = mm + '/' + dd + '/' + yyyy;
                if (delidate == today) {
                    deliveryTypes = "Express";
                    //$('#Deliverycharges').val(30);
                }
                else {
                    deliveryTypes = "Regular";
                }
                var itemDetails = $.makeArray($('#CartListbody tr[id]').map(function () {
                    if ($('#delivery_' + this.id).text() == "yes") {
                        try {
                            totalCount++;
                            unitCost = $('#unitprice_' + this.id).text();
                            quantity = $('#quantity_' + this.id).val();
                            weight = $('#Netweight_' + this.id).val();
                            measure = $('#measure_' + this.id).text();
                            category = $('#cateId_' + this.id).text();
                            totalCost = $('#unitprice_' + this.id).text();
                            marketPrice = $('#Market_' + this.id).text();
                            stock = $('#Totalitemstock_' + this.id).text();
                            purchaseprice = $('#purchaseprice_' + this.id).text();
                            discount = $('#discountId_' + this.id).text();
                            homeDelivery = $('#delivery_' + this.id).text();
                            totalQuantity += parseFloat(quantity);
                            totalSaleCOst += parseFloat(totalCost);
                            totalmarketPrice += parseFloat(marketPrice);
                            ItemType = $('#ItemType_' + this.id).text();
                            stocksid = $('#stockId_' + this.id).text();
                            stockData.unitCost = $('#unitprice_' + this.id).text();
                            stockListss.push(stockData);
                        } catch (e) {
                            swal('Error', 'Error message', 'error');
                        }
                        return this.id + "_" + unitCost + "_" + quantity + "_" + category + "_" + totalCost + "_" + weight + "_" + measure + "_" + stock + "_" + discount + "_" + stocksid + "_" + ItemType + "_" + purchaseprice + "|";
                    }
                }));
                $('#SavedPriceId').text(Math.round(parseFloat(totalmarketPrice).toFixed(2) - parseFloat(totalSaleCOst).toFixed(2)));
                var CheckedCount = $('input[name="Check"]:checked').length;
                var TotalCount = $('input[name="Check"]').length;
                var actualdisc = "";
                actualdisc = CheckedCount == TotalCount ? $('#ActualDiscountAmt').val() : 0;
                var dicData = {
                    //CustomerId: $('#spncustid').text(),
                    CustomerId: $('#cusdetailId').text(),
                    OrderdStatus: orderstatus,
                    DeliveryDate: $('#DeliveryDateId').val(),
                    SlotId: deliverytime,
                    PaymentStatus: paymentStatus,
                    PaymentMode: paymentmode,
                    AddressId: $('#spnaddress').text(),
                    HubId: Hub,
                    Branch: Branch,
                    TotalPrice: totalSaleCOst,
                    TotaldisAmt: Totaldisamt,
                    SalesPerson: salesPerson,
                    TotalQuantity: totalQuantity,
                    PLU_Count: totalCount,
                    MultipleItem: itemDetails,
                    MulitpleItem1: itemDetails,
                    DeliveryCharges: deliveryCharge,
                    coupenId: coupenID,
                    DeliveryType: deliveryTypes,
                    ActualDiscAmt: actualdisc,
                    Remaining_Amount: $('#total').text(),
                    DiscountedAmount: DiscountedAmount,
                    tableId: tableId
                };
                if (unitCost !== "" && totalQuantity !== "" && totalCost !== "" && totalSaleCOst !== "") {
                    $.ajax({
                        url: '/Sale/InsertSale',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (salesId) {
                            if (salesId !== false) {
                                $('#HomeSalesId').val(salesId);
                                //window.location.href = "/Print/Delivery?id=" + salesId + "&saved=" + $('#SavedPriceId').text();
                                $('#submitId').prop('disabled', true);
                                $('#amount1').val(parseFloat($('#amount1').val()) + parseFloat($('#Deliverycharges').val()));
                                swal('Item added for home delivery', '', '');
                                //if ($('#TotalCartItemId').text() > $('#TotalItemId').text())
                                //    mymoney();
                                //else {
                                //    var homedeliverycount = $('#TotalItemId').text();
                                //    if (homedeliverycount != 0) {
                                //if (stockListss.length == homedeliverycount) {
                                $('#SavedPriceId').text(Math.round(parseFloat(totalmarketPrice).toFixed(2) - parseFloat(totalSaleCOst).toFixed(2)));
                                window.location.href = "/Print/Index?id2=" + salesId + "&received=" + CashReceived + "&remaining=" + CashRemaining + "&saved=" + $('#SavedPriceId').text() +
                                    "&AmountTotal=" + $('#AmountId').text() + "&quant=" + $('#totalCartQuantity').text() + "&item=" + $('#TotalCartItemId').text() + '&DiscountPer=' + discountper + '&TotalDiscountAmt=' + Totaldisamt +
                                    '&ActualDiscountAmt=' + $('#ActualDiscountAmt').val();
                                //}
                                //totalSaleCOst = 0;
                                //totalUnitCost = 0;
                                //totalmarketPrice = 0;
                                //    }
                                //}
                            }
                            else {
                                swal('Oops!', 'Something went wrong!', 'warning');
                            }
                        }
                    });
                } else {
                    swal('Oops!', 'Fields can not be Empty', 'warning');
                }
            }
        });
    }
}

function AddTotalAfterCoupen() {
    var totalSaleCOst = 0;
    var totalQuantity = 0;
    var DiscountPrice = 0;
    $.makeArray($('#CartListbody tr[id]').map(function () {
        var quantity = $('#input-number_' + this.id).val();
        var totalCost = $('#AddedDiscount_' + this.id).text();
        var discount = $('#discount_' + this.id).text();
        totalSaleCOst += parseFloat(totalCost);
        totalQuantity += parseFloat(quantity);
        DiscountPrice += parseFloat(discount);
    }));
    $('#AmountId').text(Math.round(totalSaleCOst));
    //$('#Amountspn').text(totalSaleCOst);
    //$('#amount1').prop('value', totalSaleCOst);
}

function TotalofTable() {
    var totalSaleCOst = 0;
    var totalDiscount = 0;
    var totalCost = 0;
    var totalGST = 0;
    var deliverycharge = $('#billDeliveryCharge').text();
    var discount = $('#billDiscountAmt').text();
    var wallet = $('#WallentAmount').text();
    $.makeArray($('#tblsaleslist tr[id]').map(function () {
        var id = this.id;
        var itemprice = $('#TotalPrice_' + this.id).text();
        var addeddiscount = $('#AddedDiscount_' + this.id).text();
        var discount = $('#discount_' + this.id).text();
        totalDiscount += parseFloat(discount);
        totalCost += parseFloat(addeddiscount);
        totalSaleCOst += parseFloat(itemprice);
        var gstValue = $('#taxValue_' + this.id).text();
        totalGST += parseFloat(gstValue);
    }));
    //totalSaleCOst = Math.round(totalSaleCOst);
    $('#billDiscountAmt').text(parseFloat(totalDiscount).toFixed(2));
    $('#billSubAmount').text((((totalSaleCOst + parseFloat(totalDiscount)).toFixed(2))));
    $('#TotalCartDiscValue').text((totalSaleCOst));
    $('#billCalculationtAmt').text((parseFloat(totalSaleCOst) + parseFloat(deliverycharge)).toFixed(2));
    $('#billFinalAmount').text(Math.round(parseFloat(totalSaleCOst) + parseFloat(deliverycharge) - parseFloat(wallet)) + ".00");
    //$('#GSTValueId').text(totalGST);
}

function ReadyAnnouncment(element) {
    var SalesId = $(element).data('id');
    var OrderdStatus = $(element).data('update');
    var info = {
        SalesId: SalesId,
        OrderdStatus: OrderdStatus,
    }
    $.ajax({
        url: '/Sale/ReadyOrder',
        type: 'POST',
        data: info,
        success: function (result) {
            if (result.Data == true && result.IsSuccess == true) {
                window.location.reload();
            }
            else {
                swal.fire("error", "Something went wrong!", "error");
            }
        }
    });
}

function CallbookinglistView() {
    $.ajax({
        type: 'GET',
        url: '/Sale/_getTblbookinglist/',
        success: function (data) {
            if (data.trim() != "") {
                $('#bookingInfo').html(data);
                $('#bookingInfo').css('display', 'block');
                $('#m_portlet_loader').css('display', 'none');
            }
            else {
                $('#bookingInfo').html(data);
                $('#bookingInfo').css('display', 'none');
                $('#m_portlet_loader').css('display', 'none');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal('Error!', 'There was some error. Try again later.', 'error');
            $('#m_portlet_loader').css('display', 'none');
        }
    });

    //AB0069
    $('#bookingInfo').on('click', '#cancelBooking', function () {
        var info = {
            Id: $(this).attr('data-bid')
            //Id: $('#bId').val()
        };
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to delete!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/Sale/CancelAdvancedBooking',
                    type: 'POST',
                    data: info,
                    success: function (data) {
                        if (data.ReturnMessage == "Success") {
                            swal("success", "Cancelled Successfully!", "success").then(okay => {
                                if (okay) {
                                    window.location.reload();
                                }
                            });
                        }
                        else {
                            swal("error", "Something Went Wrong", "error");
                        }
                    }
                });
            }
        });
    });
}

function getActiveData(){
    var target = $('.categorytabs a[data-toggle="tab"]').attr("href");
    $('#SearchItem').prop('value', "");
    var ItemName = $.selector_cache("#SearchItem").prop('value');
    var maincategory = target.slice(4);
    $.selector_cache('#activeTab').prop('value', target);
    $.ajax({
        type: 'GET',
        url: '/Sale/_ItemListData/',
        data: { 'ItemName': ItemName, 'maincategory': maincategory },
        success: function (data) {
            if (data !== null && data.IsSuccess) {
                $('#CartBodyList_' + maincategory).html(data.Data);
                $('#CartBodyList_' + maincategory).css('display', 'block');
            } else if (data !== null) {
                swal('Error!', data.ReturnMessage, 'error');
            } else {
                swal('Error!', 'There was some error. Try again later.', 'error');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal('Error!', 'There was some error. Try again later.', 'error');
        }
    });
}


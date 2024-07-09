!$(function () {
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
        $custIds = $('#dbChangeCustId'),
        $custdetailorgid = $('#custdetailorgid'),
        $customerName = $('#customerName'),
        $customermobileno = $('#customermobileno'),
        $customerappinstall = $('#customerappinstall'),
        numberOfErrors = 0;
        $ContactNo_err = $('#ContactNo_err');
    /* Apply Customer Added and Editable */
    $(document).on('click', '#btnaddAddress', function () {
        $('#CustomerId').val($('#cusdetailId').text());
        $('#Id').val($('#custdetailorgid').text());
        $BuildingName.val('');
        $RoomNo.val('');
        $Sector.val('');
        $Landmark.val('');
        $Locality.val('');
        $AddressType.val(0);
    });

    $(document).on('keyup', '#ContactNo', function () {
        $ContactNo.css('border-color', '');
        $ContactNo_err.html('');
    });

    function validateMobileNumber(inputVal) {
        return !(/^[0-9]{10}$/.test(inputVal));
    }

    $(document).on('click', '#btnCustomerAdd', function () {
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

    $(document).ready(function () {
        $('#ContactNo').on('change', function (e) {
            $.ajax({
                type: 'GET',
                url: '/Sale/ValidateContactNo?Ext=' + $Ext.val() + '&ContactNo=' + $ContactNo.val(),
                success: function (result) {
                    if (result !== null) {
                        if (result.contactNo === $ContactNo.val()) {
                            $ContactNo.css('border-color', 'red');
                            $ContactNo_err.html("Contact Number Already Exit").css('display', 'block');
                            numberOfErrors++;
                        }
                    }
                },
                error: function (jqXHR, exception) {
                    alert('error');
                }
            });
        });
    });

    $(document).ready(function () {
        $('#GetcustomerNumber').on('change', function (e) {
            var test = $(this).select2('data');
            var CustomerId = test[0].id;
            $.ajax({
                type: 'GET',
                url: '/Sale/GetCustomerData?Type=Default&Id=' + CustomerId,
                success: function (result) {
                    if (result.getCustomerDetail != null) {
                        $CustomerId.val(result.getCustomerDetail.customerId);
                        $Name.val(result.getCustomerDetail.name);
                        $EmailId.val(result.getCustomerDetail.emailId);
                        $ContactNo.val(result.getCustomerDetail.contactNo);                      
                        $ZipCode.val(result.getCustomerDetail.zipCode);
                        $City.val(result.getCustomerDetail.city);
                        $State.val(result.getCustomerDetail.state);
                        $Country.val(result.getCustomerDetail.country);
                        $cusdetailId.text(result.getCustomerDetail.customerId);
                        $custIds.val(result.getCustomerDetail.customerId);
                        $custdetailorgid.text(result.getCustomerDetail.id);
                        $customerName.text(result.getCustomerDetail.name);
                        $AddId.val(result.getCustomerDetail.addId);
                        $spnaddress.val(result.getCustomerDetail.addId);
                        $custId.val(result.getCustomerDetail.id);
                        $Type.val(result.getCustomerDetail.type);
                        result.getCustomerDetail.zipCode != null ? $('#AddDiv').html(": " + result.getCustomerDetail.address1) : $('#AddDiv').html(":      -");
                        result.getCustomerDetail.contactNo === null ? "NA" : $customermobileno.text(result.getCustomerDetail.contactNo);
                        $('#customerappinstall').text("No");
                        $('#spnappinstall').removeClass("m-badge--success");
                        $('#spnappinstall').addClass("m-badge--dot m-badge--dot-small m-badge m-badge--danger");
                        $('#AddDiv').html(": " + result.getCustomerDetail.address1);
                        $('#spnaddress').text(result.getCustomerDetail.computeAddIds);
                        $('#dbChangeCustId').val(result.getCustomerDetail.customerId);
                        if (result.getCustomerDetail.buildingName !== null) {
                            $.ajax({
                                url: '/Sale/CoupenList',
                                type: 'Get',
                                data: { CustomerId: result.getCustomerDetail.customerId },
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
                },
                error: function () {
                    alert('error');
                }
            });
        });
    })

    $(document).ready(function () {
        $('#GetcustomerNumber').select2({
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
                                $('#changeCustomerMd').modal('hide');
                                $('#create_new_address').modal('show');
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
                                $('#ContactNo').val('');
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
    });

    function Discountapply() {
        var returnamt2 = $('#amount1234').val();
        var discountamt = $('#CashDiscount').val();
        var customeramt = $('#amount12').val();
        var deliveryCharge = $('#Deliverycharges').val();
        //var discountpercent = parseFloat(discountamt) - parseFloat(customeramt) / 100;
        discountpercent = Math.round(discountamt);
        var acualamtbydiscount = parseFloat(customeramt) - parseFloat(discountpercent) + parseFloat(deliveryCharge);
        acualamtbydiscount = Math.round(acualamtbydiscount);
        $('#ActualDiscountAmt').val(discountpercent);
        $('#DiscountTotalamt').val(parseFloat(customeramt) - parseFloat(discountpercent));
        $('#DisAmountId').text(discountpercent);
        $('#TDisAmountId').text(acualamtbydiscount);
        $('#amount1').val(acualamtbydiscount);
        if (returnamt2 != "") {
            var returnamt = parseFloat(returnamt2) - parseFloat(acualamtbydiscount);
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
    //$('#applyCouponCode').on('click', function () {
    //    $('#CouponBody').css('display', 'none');
    //    $('#CoupantypeSelect').css('display', 'block');
    //});

    /* Apply Coupen and Cash Discount */
    $(document).on('click', '#Coupantype', function () {
        if ($(this).val() == 2) {
            $('#InternalCoupon').css('display', 'block');
            $('#ExternalCoupon').css('display', 'none');
            $('#CashDiscCoupon').css('display', 'none');
            //$('#applyCouponCode').on('click', '#btnCouponSubmit', function () {
            //    var selectcoupen = $('#CoupenCodeId').val();
            //    var coupen = selectcoupen.split(',');
            //    var coupendiscount = coupen[0];
            //    if (coupendiscount != "1") {
            //        CoupenCode();
            //        TotalofTable();
            //        var couponId = $('#CoupenId').prop('value');
            //        var SalesOrderId = $('#SalesOrderId').text();
            //        var CustomerId = $('#CustId').text();
            //        var mainArray = [];
            //        var mainsubArray = [];
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array1 = {
            //                'SalesId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'Category': $('#Category_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'Measurement': $('#Measurements_' + this.id).val(),
            //                'QuantityValue': $('#quantity_' + this.id).val(),
            //                'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
            //                'TotalPrice': $('#TotalPriceList_' + this.id).val(),
            //                'Discount': $('#DiscountListId_' + this.id).val(),
            //                'Weight': $('#ItemWeight_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val()
            //            };
            //            mainArray.push(Array1);
            //        }));
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array2 = {
            //                'SalesOrderId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'TableId': $('#Table_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'TotalQty': $('#quantity_' + this.id).val(),
            //                'Remark': $('#Remark_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val(),
            //                'KOT_Status': $('#KOT_Status_' + this.id).val()  //16-12-2021
            //            };
            //            mainsubArray.push(Array2);
            //        }));
            //        $.ajax({
            //            url: '/Sale/ApplyCoupen',
            //            type: 'POST',
            //            data: {
            //                info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess == true) {
            //                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
            //                    window.location.reload();
            //                }
            //                else
            //                    swal.fire('Error!', 'Something went wrong.', 'error');
            //            },
            //            error: function (xhr, ajaxOptions, thrownError) {
            //                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            //            }
            //        });
            //    }
            //    else {
            //        CashCoupenCode();
            //        var couponId = -1;
            //        var SalesOrderId = $('#SalesOrderId').text();
            //        var CustomerId = $('#CustId').text();
            //        var discount = $('#CashDiscount').val();
            //        var totalamount = $('#billFinalAmount').text();
            //        var mainArray = [];
            //        var mainsubArray = [];
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array1 = {
            //                'SalesId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'Category': $('#Category_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'Measurement': $('#Measurements_' + this.id).val(),
            //                'QuantityValue': $('#quantity_' + this.id).val(),
            //                'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
            //                'TotalPrice': $('#TotalPriceList_' + this.id).val(),
            //                'Discount': $('#DiscountListId_' + this.id).val(),
            //                'Weight': $('#ItemWeight_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val()
            //            };
            //            mainArray.push(Array1);
            //        }));
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array2 = {
            //                'SalesOrderId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'TableId': $('#Table_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'TotalQty': $('#quantity_' + this.id).val(),
            //                'Remark': $('#Remark_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val(),
            //                'KOT_Status': $('#KOT_Status_' + this.id).val()
            //            };
            //            mainsubArray.push(Array2);
            //        }));
            //        $.ajax({
            //            url: '/Sale/ApplyCashDiscount',
            //            type: 'POST',
            //            data: {
            //                info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId, discount: discount, totalamount: totalamount,
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess == true) {
            //                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
            //                    window.location.reload();
            //                }
            //                else
            //                    swal.fire('Error!', 'Something went wrong.', 'error');
            //            },
            //            error: function (xhr, ajaxOptions, thrownError) {
            //                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            //            }
            //        });
            //    }
            //});
        }
        else if ($(this).val() == 3){
            $('#ExternalCoupon').css('display', 'block');
            $('#InternalCoupon').css('display', 'none');
            $('#CashDiscCoupon').css('display', 'none');
            //$('#applyCouponCode').on('click', '#btnCouponSubmit', function () {
            //    var selectcoupen = $('#CoupenCodeId').val();
            //    var coupen = selectcoupen.split(',');
            //    var coupendiscount = coupen[0];
            //    if (coupendiscount != "1") {
            //        CoupenCode();
            //        TotalofTable();
            //        var couponId = $('#CoupenId').prop('value');
            //        var SalesOrderId = $('#SalesOrderId').text();
            //        var CustomerId = $('#CustId').text();
            //        var mainArray = [];
            //        var mainsubArray = [];
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array1 = {
            //                'SalesId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'Category': $('#Category_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'Measurement': $('#Measurements_' + this.id).val(),
            //                'QuantityValue': $('#quantity_' + this.id).val(),
            //                'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
            //                'TotalPrice': $('#TotalPriceList_' + this.id).val(),
            //                'Discount': $('#DiscountListId_' + this.id).val(),
            //                'Weight': $('#ItemWeight_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val()
            //            };
            //            mainArray.push(Array1);
            //        }));
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array2 = {
            //                'SalesOrderId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'TableId': $('#Table_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'TotalQty': $('#quantity_' + this.id).val(),
            //                'Remark': $('#Remark_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val(),
            //                'KOT_Status': $('#KOT_Status_' + this.id).val()  //16-12-2021
            //            };
            //            mainsubArray.push(Array2);
            //        }));
            //        $.ajax({
            //            url: '/Sale/ApplyCoupen',
            //            type: 'POST',
            //            data: {
            //                info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess == true) {
            //                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
            //                    window.location.reload();
            //                }
            //                else
            //                    swal.fire('Error!', 'Something went wrong.', 'error');
            //            },
            //            error: function (xhr, ajaxOptions, thrownError) {
            //                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            //            }
            //        });
            //    }
            //    else {
            //        CashCoupenCode();
            //        var couponId = -1;
            //        var SalesOrderId = $('#SalesOrderId').text();
            //        var CustomerId = $('#CustId').text();
            //        var discount = $('#CashDiscount').val();
            //        var totalamount = $('#billFinalAmount').text();
            //        var mainArray = [];
            //        var mainsubArray = [];
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array1 = {
            //                'SalesId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'Category': $('#Category_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'Measurement': $('#Measurements_' + this.id).val(),
            //                'QuantityValue': $('#quantity_' + this.id).val(),
            //                'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
            //                'TotalPrice': $('#TotalPriceList_' + this.id).val(),
            //                'Discount': $('#DiscountListId_' + this.id).val(),
            //                'Weight': $('#ItemWeight_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val()
            //            };
            //            mainArray.push(Array1);
            //        }));
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array2 = {
            //                'SalesOrderId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'TableId': $('#Table_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'TotalQty': $('#quantity_' + this.id).val(),
            //                'Remark': $('#Remark_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val(),
            //                'KOT_Status': $('#KOT_Status_' + this.id).val()
            //            };
            //            mainsubArray.push(Array2);
            //        }));
            //        $.ajax({
            //            url: '/Sale/ApplyCashDiscount',
            //            type: 'POST',
            //            data: {
            //                info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId, discount: discount, totalamount: totalamount,
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess == true) {
            //                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
            //                    window.location.reload();
            //                }
            //                else
            //                    swal.fire('Error!', 'Something went wrong.', 'error');
            //            },
            //            error: function (xhr, ajaxOptions, thrownError) {
            //                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            //            }
            //        });
            //    }
            //});         
        }
        else if ($(this).val() == 1) {
            $('#CashDiscCoupon').css('display', 'block');
            $('#InternalCoupon').css('display', 'none');
            $('#ExternalCoupon').css('display', 'none');
            //$('#applyCouponCode').on('click', '#btnCouponSubmit', function () {
            //    var selectcoupen = $('#CoupenCodeId').val();
            //    var coupen = selectcoupen.split(',');
            //    var coupendiscount = coupen[0];
            //    if (coupendiscount != "1") {
            //        CoupenCode();
            //        TotalofTable();
            //        var couponId = $('#CoupenId').prop('value');
            //        var SalesOrderId = $('#SalesOrderId').text();
            //        var CustomerId = $('#CustId').text();
            //        var mainArray = [];
            //        var mainsubArray = [];
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array1 = {
            //                'SalesId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'Category': $('#Category_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'Measurement': $('#Measurements_' + this.id).val(),
            //                'QuantityValue': $('#quantity_' + this.id).val(),
            //                'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
            //                'TotalPrice': $('#TotalPriceList_' + this.id).val(),
            //                'Discount': $('#DiscountListId_' + this.id).val(),
            //                'Weight': $('#ItemWeight_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val()
            //            };
            //            mainArray.push(Array1);
            //        }));
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array2 = {
            //                'SalesOrderId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'TableId': $('#Table_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'TotalQty': $('#quantity_' + this.id).val(),
            //                'Remark': $('#Remark_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val(),
            //                'KOT_Status': $('#KOT_Status_' + this.id).val()  //16-12-2021
            //            };
            //            mainsubArray.push(Array2);
            //        }));
            //        $.ajax({
            //            url: '/Sale/ApplyCoupen',
            //            type: 'POST',
            //            data: {
            //                info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess == true) {
            //                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
            //                    window.location.reload();
            //                }
            //                else
            //                    swal.fire('Error!', 'Something went wrong.', 'error');
            //            },
            //            error: function (xhr, ajaxOptions, thrownError) {
            //                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            //            }
            //        });
            //    }
            //    else {
            //        CashCoupenCode();
            //        var couponId = -1;
            //        var SalesOrderId = $('#SalesOrderId').text();
            //        var CustomerId = $('#CustId').text();
            //        var discount = $('#CashDiscount').val();
            //        var totalamount = $('#billFinalAmount').text();
            //        var mainArray = [];
            //        var mainsubArray = [];
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array1 = {
            //                'SalesId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'Category': $('#Category_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'Measurement': $('#Measurements_' + this.id).val(),
            //                'QuantityValue': $('#quantity_' + this.id).val(),
            //                'PricePerMeas': $('#PercePerItemId_' + this.id).val(),
            //                'TotalPrice': $('#TotalPriceList_' + this.id).val(),
            //                'Discount': $('#DiscountListId_' + this.id).val(),
            //                'Weight': $('#ItemWeight_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val()
            //            };
            //            mainArray.push(Array1);
            //        }));
            //        var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
            //            var Array2 = {
            //                'SalesOrderId': SalesOrderId,
            //                'CustomerId': CustomerId,
            //                'TableId': $('#Table_' + this.id).val(),
            //                'ItemId': $('#ItemId_' + this.id).val(),
            //                'PriceId': $('#PriceId_' + this.id).val(),
            //                'TotalQty': $('#quantity_' + this.id).val(),
            //                'Remark': $('#Remark_' + this.id).val(),
            //                'Status': $('#ItemStatus_' + this.id).val(),
            //                'KOT_Status': $('#KOT_Status_' + this.id).val()
            //            };
            //            mainsubArray.push(Array2);
            //        }));
            //        $.ajax({
            //            url: '/Sale/ApplyCashDiscount',
            //            type: 'POST',
            //            data: {
            //                info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId, discount: discount, totalamount: totalamount,
            //            },
            //            success: function (data) {
            //                if (data.IsSuccess == true) {
            //                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
            //                    window.location.reload();
            //                }
            //                else
            //                    swal.fire('Error!', 'Something went wrong.', 'error');
            //            },
            //            error: function (xhr, ajaxOptions, thrownError) {
            //                swal.fire('Error!', 'There was some error. Try again later.', 'error');
            //            }
            //        });
            //    }
            //});
        }
        else if ($(this).val() == 0) {
            $('#CouponBody').css('display', 'none');
            $('#CoupantypeSelect').css('display', 'block');
            $('#CouponBody').css('display', 'block');
        }
        else {
            $('#CoupantypeSelect').css('display', 'block');
            $('#InternalCoupon').css('display', 'none');
            $('#ExternalCoupon').css('display', 'none');
            $('#CashDiscCoupon').css('display', 'none');
            $('#CouponBody').css('display', 'none');
        }
    });
    $(document).on('change', '#CoupenCodeId', function () {
        if ($(this).val() == 1) {
            $('.Divcashamt').css('display', 'block');
            $('#btnDiscountSubmit').css('display', 'block');
        }
        else {
            $('.Divcashamt').css('display', 'none');
            $('#CashDiscount').val('');
        }
    }); 
    
        $('#applyCouponCode').on('click', '#btnCouponSubmit', function () {
            var selectcoupen = $('#CoupenCodeId').val();
            var coupen = selectcoupen.split(',');
            var coupendiscount = coupen[0];
            if (coupendiscount != "1") {
                CoupenCode();
                TotalofTable();
                var couponId = $('#CoupenId').prop('value');
                var SalesOrderId = $('#SalesOrderId').text();
                var CustomerId = $('#CustId').text();
                var mainArray = [];
                var mainsubArray = [];
                var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
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
                var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
                    var Array2 = {
                        'SalesOrderId': SalesOrderId,
                        'CustomerId': CustomerId,
                        'TableId': $('#Table_' + this.id).val(),
                        'ItemId': $('#ItemId_' + this.id).val(),
                        'PriceId': $('#PriceId_' + this.id).val(),
                        'TotalQty': $('#quantity_' + this.id).val(),
                        'Remark': $('#Remark_' + this.id).val(),
                        'Status': $('#ItemStatus_' + this.id).val(),
                        'KOT_Status': $('#KOT_Status_' + this.id).val()  //16-12-2021
                    };
                    mainsubArray.push(Array2);
                }));
                $.ajax({
                    url: '/Sale/ApplyCoupen',
                    type: 'POST',
                    data: {
                        info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId
                    },
                    success: function (data) {
                        if (data.IsSuccess == true) {
                            $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                            window.location.reload();
                        }
                        else
                            swal.fire('Error!', 'Something went wrong.', 'error');
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal.fire('Error!', 'There was some error. Try again later.', 'error');
                    }
                });
            }
            else {
                CashCoupenCode();
                var couponId = -1;
                var SalesOrderId = $('#SalesOrderId').text();
                var CustomerId = $('#CustId').text();
                var discount = $('#CashDiscount').val();
                var totalamount = $('#billFinalAmount').text();
                var mainArray = [];
                var mainsubArray = [];
                var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
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
                var Array = $.makeArray($('#tblsaleslist tr[id]').map(function () {
                    var Array2 = {
                        'SalesOrderId': SalesOrderId,
                        'CustomerId': CustomerId,
                        'TableId': $('#Table_' + this.id).val(),
                        'ItemId': $('#ItemId_' + this.id).val(),
                        'PriceId': $('#PriceId_' + this.id).val(),
                        'TotalQty': $('#quantity_' + this.id).val(),
                        'Remark': $('#Remark_' + this.id).val(),
                        'Status': $('#ItemStatus_' + this.id).val(),
                        'KOT_Status': $('#KOT_Status_' + this.id).val()
                    };
                    mainsubArray.push(Array2);
                }));
                $.ajax({
                    url: '/Sale/ApplyCashDiscount',
                    type: 'POST',
                    data: {
                        info: mainArray, data: mainsubArray, couponId: couponId, SalesOrderId: SalesOrderId, CustomerId: CustomerId, discount: discount, totalamount: totalamount,
                    },
                    success: function (data) {
                        if (data.IsSuccess == true) {
                            $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                            window.location.reload();
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

    function CoupenCode() {
        var selectcoupen = $('#CoupenCodeId').val();
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var coupenId = coupen[1];
        var maxdiscount = coupen[2];
        $('#Coupendisc').val(coupendiscount);
        var ItemWithCoupen = 0;
        var ItemWithoutCoupen = 0;
        var DeliveryCharge = $('#billDeliveryCharge').text();
        var WallentAmount = $('#WallentAmount').text();
        $.makeArray($('#tblsaleslist tr[id]').map(function () {
            if ($('#coupenDisc_' + this.id).text() == 1) {
                var ItemCoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithCoupen += parseFloat(ItemCoupenCost);
            }
            else {
                var ItemWithoutoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithoutCoupen += parseFloat(ItemWithoutoupenCost);
            }
        }));
        if (selectcoupen != 0) {
            ItemWithCoupen = 0;
            ItemWithoutCoupen = 0;
            $.makeArray($('#tblsaleslist tr[id]').map(function () {
                var netWeight = $('#spnNetweight_' + this.id).text();
                var quantity = $('#spanquantity_' + this.id).text();
                var itemWeight = $('#ItemWeight_' + this.id).val();
                var pricePerMeasure = $('#PercePerItemId_' + this.id).val();
                var totalItem = (parseFloat(netWeight) * parseFloat(quantity) / parseFloat(itemWeight));
                if ($('#coupenDisc_' + this.id).text() == 1) {
                    var ItemCoupenCost = parseFloat(totalItem) * parseFloat(pricePerMeasure);
                    $('#AddedDiscount_' + this.id).text(ItemCoupenCost);
                    var discountpercent = ((parseFloat(coupendiscount) * parseFloat(ItemCoupenCost) / 100).toFixed(2));
                    var discountedPrice = ((parseFloat(ItemCoupenCost) - parseFloat(discountpercent)).toFixed(2));
                    $('#discount_' + this.id).text(discountpercent);
                    $('#DiscountListId_' + this.id).val(discountpercent);
                    $('#TotalPrice_' + this.id).text(discountedPrice);
                    $('#TotalPriceList_' + this.id).val(discountedPrice);
                    ItemWithCoupen += parseFloat(ItemCoupenCost);
                }
                else {
                    var ItemWithoutoupenCost = parseFloat(totalItem) * parseFloat(pricePerMeasure);
                    $('#AddedDiscount_' + this.id).text(ItemWithoutoupenCost);
                    var discount = $('#discount_' + this.id).text();
                    var discountedPrice = (parseFloat(ItemWithoutoupenCost) - parseFloat(discount)).toFixed(2);
                    ItemWithoutCoupen += parseFloat(discountedPrice)
                }
            }));
            $('#ItemWithCoupen').text(ItemWithCoupen);
            $('#ItemWitoutCoupen').text(ItemWithoutCoupen);
            var customeramt = $('#ItemWithCoupen').text();
            var discountpercent = (parseFloat(coupendiscount) * parseFloat(customeramt) / 100).toFixed(2);
            discountpercent = parseFloat(discountpercent) > parseFloat(maxdiscount) ? parseFloat(maxdiscount) : parseFloat(discountpercent);
            var acualamtbydiscount = (parseFloat(customeramt) - parseFloat(discountpercent)).toFixed(2);
            var TotalWithCoupen = (parseFloat($('#ItemWitoutCoupen').text()) + parseFloat(acualamtbydiscount)).toFixed(2);
            var AmountWithoutWallet = parseFloat(TotalWithCoupen) + parseFloat(DeliveryCharge);
            AmountWithoutWallet = parseFloat(AmountWithoutWallet).toFixed(2);
            var TotalAmount = parseFloat(TotalWithCoupen) + parseFloat(DeliveryCharge) - parseFloat(WallentAmount);
            TotalAmount = Math.round(TotalAmount);
            var combineamt = acualamtbydiscount;
            $('#TotalCartDiscValue').text(TotalWithCoupen);
            $('#billSubAmount').text(((parseFloat(TotalWithCoupen)) + parseFloat(discountpercent)).toFixed(2));
            $('#billCalculationtAmt').text(AmountWithoutWallet);
            $('#billDiscountAmt').text(discountpercent);
            $('#billFinalAmount').text(TotalAmount);
            var text = $("#CoupenCodeId option:selected").text();
            var value = $("#CoupenCodeId option:selected").val();
            if (value == 0) $('#CoupenCode').text("");
            else $('#CoupenCode').text(text);
            $('#CoupenId').val(coupenId);
            $('#applyCouponCode').modal('hide');
        }
        if (ItemWithCoupen == 0) {
            swal.fire("warning", "No item in cart are allowed to coupen!", "warning");
            $("#CoupenCodeId option:first").attr('selected', 'selected');
            $('#CoupenCode').text('');
            $('#CoupenId').val(0);
        }
    }

    function CashCoupenCode() {
        var discountamt = $('#CashDiscount').val();
        var selectcoupen = $('#CoupenCodeId').val();
        var coupen = selectcoupen.split(',');
        var coupendiscount = coupen[0];
        var coupenId = coupen[1];
        var maxdiscount = coupen[2];
        $('#Coupendisc').val(coupendiscount);
        var ItemWithCoupen = 0;
        var ItemWithoutCoupen = 0;
        var DeliveryCharge = $('#billDeliveryCharge').text();
        var WallentAmount = $('#WallentAmount').text();
        $.makeArray($('#tblsaleslist tr[id]').map(function () {
            if ($('#coupenDisc_' + this.id).text() == 1) {
                var ItemCoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithCoupen += parseFloat(ItemCoupenCost);
            }
            else {
                var ItemWithoutoupenCost = $('#AddedDiscount_' + this.id).text();
                ItemWithoutCoupen += parseFloat(ItemWithoutoupenCost);
            }
        }));
        if (selectcoupen != 0) {
            ItemWithCoupen = 0;
            ItemWithoutCoupen = 0;
            $.makeArray($('#tblsaleslist tr[id]').map(function () {
                var netWeight = $('#spnNetweight_' + this.id).text();
                var quantity = $('#spanquantity_' + this.id).text();
                var itemWeight = $('#ItemWeight_' + this.id).val();
                var pricePerMeasure = $('#PercePerItemId_' + this.id).val();
                var totalItem = (parseFloat(netWeight) * parseFloat(quantity) / parseFloat(itemWeight));
                if ($('#coupenDisc_' + this.id).text() == 1) {
                    var ItemCoupenCost = parseFloat(totalItem) * parseFloat(pricePerMeasure);
                    $('#AddedDiscount_' + this.id).text(ItemCoupenCost);
                    var discountpercent = 0;
                    var discountedPrice = ((parseFloat(ItemCoupenCost) - parseFloat(discountpercent)).toFixed(2));
                    $('#discount_' + this.id).text(discountpercent);
                    $('#DiscountListId_' + this.id).val(discountpercent);
                    $('#TotalPrice_' + this.id).text(discountedPrice);
                    $('#TotalPriceList_' + this.id).val(discountedPrice);
                    ItemWithCoupen += parseFloat(ItemCoupenCost);
                }
                else {
                    var ItemWithoutoupenCost = parseFloat(totalItem) * parseFloat(pricePerMeasure);
                    $('#AddedDiscount_' + this.id).text(ItemWithoutoupenCost);
                    var discount = $('#discount_' + this.id).text();
                    var discountedPrice = (parseFloat(ItemWithoutoupenCost) - parseFloat(discount)).toFixed(2);
                    ItemWithoutCoupen += parseFloat(discountedPrice)
                }
            }));
            $('#ItemWithCoupen').text(ItemWithCoupen);
            $('#ItemWitoutCoupen').text(ItemWithoutCoupen);
            var customeramt = $('#ItemWithCoupen').text();
            var discountpercent  = Math.round(discountamt);
            var acualamtbydiscount = (parseFloat(customeramt) - parseFloat(discountpercent)).toFixed(2);
            var TotalWithCoupen = (parseFloat($('#ItemWitoutCoupen').text()) + parseFloat(acualamtbydiscount)).toFixed(2);
            var AmountWithoutWallet = parseFloat(TotalWithCoupen) + parseFloat(DeliveryCharge);
            AmountWithoutWallet = parseFloat(AmountWithoutWallet).toFixed(2);
            var TotalAmount = parseFloat(TotalWithCoupen) + parseFloat(DeliveryCharge) - parseFloat(WallentAmount);
            TotalAmount = Math.round(TotalAmount);
            var combineamt = acualamtbydiscount;
            $('#TotalCartDiscValue').text(TotalWithCoupen);
            $('#billSubAmount').text(((parseFloat(TotalWithCoupen)) + parseFloat(discountpercent)).toFixed(2));
            $('#billCalculationtAmt').text(AmountWithoutWallet);
            $('#billDiscountAmt').text(discountpercent);
            $('#billFinalAmount').text(TotalAmount);
            var text = $("#CoupenCodeId option:selected").text();
            var value = $("#CoupenCodeId option:selected").val();
            if (value == 0) $('#CoupenCode').text("");
            else $('#CoupenCode').text(text);
            $('#CoupenId').val(coupenId);
            $('#applyCouponCode').modal('hide');
        }
        if (ItemWithCoupen == 0) {
            swal.fire("warning", "No item in cart are allowed to coupen!", "warning");
            $("#CoupenCodeId option:first").attr('selected', 'selected');
            $('#CoupenCode').text('');
            $('#CoupenId').val(0);
        }
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

    /* Apply Payment Status */
    $("#PaymentStatusId li").click(function () {
        var paymentstatus = $(this).attr('data-value');
        $('#PaymentStatusValue').val(paymentstatus);
        $('PaymentStatusValueSet').val("");
    });

    $("#PaymentModeId li").click(function () {
        var paymentmode = $(this).attr('data-value');
        $('#PaymentModeValue').val(paymentmode);
        $('PaymentModeValueSet').val("");

    });

    $(document).on('click', '#BtnPaymentStatus', function () {
        var PaymentStatusData = "";
        var a = $('#PaymentStatusValue').val();
        var b = $('#PaymentStatusValueSet').val();
        if (a == "") {
            PaymentStatusData = b;
        }
        else {
            PaymentStatusData = a;
        }
        var PaymentModeData = "";
        var c = $('#PaymentModeValue').val();
        var d = $('#PaymentModeValueSet').val();
        if (c == "") {
            PaymentModeData = d;
        }
        else {
            PaymentModeData = c;
        }
        var ChangePaymentStatusofProduct = {
            PaymentStatus: PaymentStatusData,
            SalesOrderId: $('#SalesOrderId').text(),
            CustomerId: $('#CustomerId').val(),
            PaymentMode: PaymentModeData
        };
        $.ajax({
            url: '/Sale/UpdatePaymentStatus',
            type: 'POST',
            data: ChangePaymentStatusofProduct,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    $('#paymentStatus').modal('hide');
                    $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                    window.location.reload();
                }
            }
        });
    })

    /* Apply Customer And Table Editable */
    $('#changeCustomerMd').on('click', '#BtnCustomer', function () {
        var tableId = $('#selecttableId').val();
        var ChangePaymentStatusofProduct = {
            tableId: $('#selecttableId').val(),
            SalesOrderId: $('#SalesOrderId').text(),
            CustomerId: $('#GetcustomerNumber').val(),
        };
        $.ajax({
            url: '/Sale/CustomertblEdit',
            type: 'POST',
            data: ChangePaymentStatusofProduct,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Customer Table Change Successfully",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        $('#changeCustomerMd').modal('hide');
                        $(".page-common-loader").addClass(".page-common-loader.loader-show").css('display', 'flex');
                        window.location.reload();
                    })
                }
            }
        });
    })

    /* Apply KOT Printed */
    $(document).on('click', '#BtnKotUpdate', function () {
        getallSaelsOrerId();
        getallKitchenListId();
        getallSalesListId();
        var id = getallSaelsOrerId();
        var kitchenlistid = getallKitchenListId();
        var SalesListIds = getallSalesListId();
        var salesId = id;
        var salesIds = salesId[0];
        var kitchenlistid = kitchenlistid;
        var SalessListId = SalesListIds;
        var status = 1;
        if (id.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to add this item into KOT Print !',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Add it!'
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        salesId: salesId,
                        kitChenListId: kitchenlistid,
                        salesListId: SalessListId,
                        KOT_Status: status,
                        Status: 'Accepted',
                    };
                    $.ajax({
                        url: '/Sale/ApprovalAllKitchenKOTStatusUpdate/',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (result) {
                            if (result.IsSuccess == true && result.Data != 0) {
                                window.location.href = "/Print/KOTPrint?id1=" + salesIds + "&id2=" + kitchenlistid + "&remaining=" + null + "&saved=" + null + "&AmountTotal=" + null + "&quant=" + null + "&item=" + null + '&DiscountPer=' + null + '&TotalDiscountAmt=' + null + '&ActualDiscountAmt=' + null;
                                
                            }
                            else {
                                window.location.reload(true);
                            }                           
                        }                        
                    });                  
                }
            });
        }
    });

    var getallSaelsOrerId = function () {
        return $('#KotListView').find('.SalesOrderId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getallKitchenListId = function () {
        return $('#KotListView').find('.KitchListId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getallSalesListId = function () {
        return $('#KotListView').find('.SalesListId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    /* Apply All Tbl KOT Printed */
    $('#AlltblkotPrintListView').on('click', '#AlltblBtnKotUpdate', function () {
        getalltblSaelsOrerId();
        getalltblKitchenListId();
        getalltblSalesListId();
        var id = getalltblSaelsOrerId();
        var kitchenlistid = getalltblKitchenListId();
        var SalesListIds = getalltblSalesListId();
        var salesId = id;
        var salesIds = salesId;
        var list = salesIds.filter((x, i, a) => a.indexOf(x) === i);
        var kitchenlistid = kitchenlistid;
        var SalessListId = SalesListIds;
        var status = 1;
        if (id.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to add this item into KOT Print !',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Add it!'
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        salesId: salesId,
                        kitChenListId: kitchenlistid,
                        salesListId: SalessListId,
                        KOT_Status: status,
                        Status: 'Ready',
                    };
                    $.ajax({
                        url: '/Sale/ApprovalAllKitchenKOTStatusUpdate/',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (result) {
                            if (result.IsSuccess == true && result.Data != 0) {
                                window.location.href = "/Print/AllTblKOTPrint?id1=" + list + "&id2=" + kitchenlistid + "&remaining=" + null + "&saved=" + null + "&AmountTotal=" + null + "&quant=" + null + "&item=" + null + '&DiscountPer=' + null + '&TotalDiscountAmt=' + null + '&ActualDiscountAmt=' + null;                                
                            }
                            else {
                                window.location.reload(true);
                            }
                        }
                    });
                }
            });
        }
    });

    var getalltblSaelsOrerId = function () {
        return $('#AlltblkotPrintListView').find('.SalesOrderId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getalltblKitchenListId = function () {
        return $('#AlltblkotPrintListView').find('.KitchListId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getalltblSalesListId = function () {
        return $('#AlltblkotPrintListView').find('.SalesListId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    /* Apply Consolidate Tbl KOT Printed */
    $(document).on('click', '#ConsolidatetblBtnKotUpdate', function () {
        getallConSaelsOrerId();
        getallConKitchenListId();
        getallConSalesListId();
        var id = getallConSaelsOrerId();
        var kitchenlistid = getallConKitchenListId();
        var SalesListIds = getallConSalesListId();
        var salesId = id;
        var salesIds = salesId;
        var kitchenlistid = kitchenlistid;
        var SalessListId = SalesListIds;
        var status = 1;
        if (id.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to add this item into KOT Print !',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Add it!'
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        salesId: salesId,
                        kitChenListId: kitchenlistid,
                        salesListId: SalessListId,
                        KOT_Status: status,
                        Status: 'Ready',
                    };
                    $.ajax({
                        url: '/Sale/ApprovalAllKitchenKOTStatusUpdate/',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (result) {
                            if (result.IsSuccess == true && result.Data != 0) {
                                window.location.href = "/Print/ConsolidateTblKOTPrint?id1=" + salesIds + "&id2=" + kitchenlistid + "&remaining=" + null + "&saved=" + null + "&AmountTotal=" + null + "&quant=" + null + "&item=" + null + '&DiscountPer=' + null + '&TotalDiscountAmt=' + null + '&ActualDiscountAmt=' + null;
                            }
                            else {
                                window.location.reload(true);
                            }
                        }
                    });
                }
            });
        }
    });

    var getallConSaelsOrerId = function () {
        return $('#ConsolidatetblkotPrintListView').find('.SalesOrderId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getallConKitchenListId = function () {
        return $('#ConsolidatetblkotPrintListView').find('.KitchListId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getallConSalesListId = function () {
        return $('#ConsolidatetblkotPrintListView').find('.SalesListId')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }
});

$("#kot_log_md").on("click", "#ReprintLogs", function (e) {
    e.preventDefault();
    var kotId = $(this).attr('data-kotid');
    var salesorderId = $(this).attr('data-SalesId');
    $.ajax({
        url: "/Sale/KotReprint?KOTID=" + kotId + "&salesid=" + salesorderId,
        type: "POST",
        success: function (result) {
            if (result.IsSuccess == true && result.Data != 0) {
                window.location.href = "/Print/KotReprint?kotId=" + kotId + "&salesId=" + salesorderId
            }
        }
    });
});

$('#applyCouponCode').on('change', '#CoupenCodeIdcash', function () {
    var a = $("#CoupenCodeIdcash").val();
    var b = $("#amount12cash").val();
    var c = b - a;
    $('#amount123cash').val(a);
    $("#amount1234cash").val(c);
});
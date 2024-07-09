$(document).ready(function () {
    $('#m_portlet_loader').css('display', 'block');
    $('#m_portlet_loader').css('display', 'none');
});
$(function () {
    /*Create Function Stat */
    $('#CreateZip').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#ZipCreate').submit();
            swal({
                text: "Successfully Create Zipcode",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })            
        }
    });

    $('#CreateSlot').on('click', function () {
        if (0 === nErrorCount) {
            $('#SlotCreate').submit();
        }
    });

    $('#SubmitBrand').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#BrandCreate').submit();
            swal({
                text: "Successfully Create Brand",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });

    $('#SubmitTable').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#TableCreate').submit();
            swal({
                text: "Successfully Create Table",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });

    $('#HubCreate').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#HubFrmCreate').submit();
            swal({
                text: "Successfully Create Hub",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });   
     
    /* Get Details fUNCTION */
    $('.Ziptbl').on('click', '.EditItem', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/GetZipCodeDetails?id=' + Id,
            type: 'GET',
            success: function (data) {
                $('#id').val(data.id);
                $(".Hublist option[value='" + data.hub + "']").prop('selected', true);
                $('#MinValue').val(data.minOrderValue);
                $('#StandCharges').val(data.standradDeleiveryCharges);
                $('#ExpDelCharges').val(data.expressDeleiveryCharges);
                $('.Discriptions').val(data.discription);
                $('.CODAVALs').val(data.codaval);
                $('.ExpressDavals').val(data.expressDaval);
                $('#zipcodes').modal('show');
            }
        });
    });
    $('.EditSlot').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/GetSlotDetails?id=' + Id,
            type: 'GET',
            success: function (data) {
                $('#Id').val(Id);
                $("#SlotType option[value='" + data.slotType + "']").prop('selected', true);
                $('.Timings').val(data.timing);
                $('#Slots').modal('show');
            }
        });
    });

    $('.BrandDetails').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/GetBrandDetails?id=' + Id,
            type: 'GET',
            success: function (data) {
                var path = 'https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/brand/';
                $.selector_cache('.Ids').val(data.getbrandetails.id);
                $.selector_cache('.BrandIds').val(data.getbrandetails.brandId);
                $.selector_cache('#BrandImg').attr('src', path + data.getbrandetails.brandId + '.png');//
                $.selector_cache('.Brandnames').val(data.getbrandetails.brandName);
                $.selector_cache('#Logo').val(data.getbrandetails.imglogo)
                for (var i = 0; i < data.getSupplierlist.length; i++) {
                    $("#Supplierlist option[value='" + data.getSupplierlist[i] + "']").prop('selected', true);
                }
                $('#NewBrand2').modal('show');
            }
        });
    });

    $('.TableDetails').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/GetTableDetails?id=' + Id,
            type: 'GET',
            success: function (data) {
                $.selector_cache('.Ids').val(data.gettableetails.id);
                $.selector_cache('.tableIds').val(data.gettableetails.tableId);
                $.selector_cache('.tableNames').val(data.gettableetails.tableName);
                $.selector_cache('.tableCode').val(data.gettableetails.tableCode);
                $.selector_cache(".branchlist option[value='" + data.gettableetails.branch + "']").prop('selected', true);
                $('#NewTable2').modal('show');
            }
        });
    });

    $('.hubtbl').on('click', '.HubDetails', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/GetHubDetails?id=' + Id,
            type: 'GET',
            success: function (data) {
                $('.id').val(data.id);
                $('#HubId').val(data.hubId);
                $('.HubName').val(data.hubName);
                $('.Area').val(data.area);
                $('.BuildingName').val(data.buildingName);
                $('.RoomNo').val(data.roomNo);
                $('.Landmark').val(data.landmark);
                $('.City').val(data.city);
                $('.State').val(data.state);
                $('.Country').val(data.country);
                $('.Sector').val(data.sector);
                $('#NewHubUpdate').modal('show');
            }
        });
    });

    /* Delete Function Start */
    $('.Ziptbl').on('click', '.deleteId', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/Delete?id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Successfully Delete Zipcode",
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
    });

    $('.deleteSlotId').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/GetSlotDelete?id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Successfully Delete Slot",
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
    });

    $('.deletebrandId').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/DeleteBrand?id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Successfully Delete Brand",
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
    });

    $('.deletetableId').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/DeleteTable?id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Successfully Delete Table",
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
    });

    $('.deleteHubId').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Settings/DeleteHub?id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Successfully Delete Hub",
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
    });

    /* Update Function */
    $('.ZipSubmitBtn').on('click', function () {
        var numError = 0;
        if (numError == 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to Update zip!',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Update it!'
            }).then((result) => {
                var MinValue = $.selector_cache('#MinValue').val();
                var StandCharges = $.selector_cache('#StandCharges').val();
                var ExpDelCharges = $.selector_cache('#ExpDelCharges').val();
                var Description = $.selector_cache('.Discriptions').val();
                var CODAVAL = $.selector_cache('.CODAVALs option:selected').val();
                var ExpressDaval = $.selector_cache('.ExpressDavals option:selected').val();
                var Hublist = $.selector_cache('.Hublist option:selected').val();
                var id = $.selector_cache('#id').val();
                if (result.value) {
                    var info = {
                        id: id,
                        MinOrderValue: MinValue,
                        StandradDeleiveryCharges: StandCharges,
                        ExpressDeleiveryCharges: ExpDelCharges,
                        Discription: Description,
                        CODAVAL: CODAVAL,
                        ExpressDaval: ExpressDaval,
                        Hub: Hublist
                    };
                    if (numError == 0) {
                        $.ajax({
                            url: '/Settings/UpdateZipCodeDetails/',
                            type: 'GET',
                            dataType: 'json',
                            data: info,
                            success: function (data) {
                                $('#zipcodes').modal('hide');
                                swal({
                                    text: "Successfully Updated Hub",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    location.reload();
                                })
                            }
                        });
                    }
                }
            });
        }
    });

    $('.SlotSubmitBtn').on('click', function () {
        var numError = 0;
        if (numError == 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to Update Slot!',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Update it!'
            }).then((result) => {
                var Timing = $.selector_cache('.Timings').val();
                var Id = $.selector_cache('#Id').val();
                var SlotType = $.selector_cache('.SlotTypes option:selected').val();
                if (result.value) {
                    var info = {
                        Id: Id,
                        Timing: Timing,
                        SlotType: SlotType,
                    };
                    if (numError == 0) {
                        $.ajax({
                            url: '/Settings/UpdateSlotList/',
                            type: 'GET',
                            dataType: 'json',
                            data: info,
                            success: function (data) {
                                $('#Slots').modal('hide');
                                swal({
                                    text: "Successfully Updated Slots",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    location.reload();
                                })
                            }
                        });
                    }
                }
            });
        }
    });

    $('#UpdateBrand').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#BrandUpdate').submit();
            swal({
                text: "Successfully Update Brand",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });

    $('#UpdateTable').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#TableUpdate').submit();
            swal({
                text: "Successfully Update Table",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });

    $('#HubUpdate').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#HubFrmUpdate').submit();
            swal({
                text: "Successfully Update Hub",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });

    /* Rest Functionality */
    $.selector_cache(".logse").change(function () {
        readURL(this);
    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imageLogo').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    $('.newbrand').on('click', function () {
        $.selector_cache('.Ids').val('');
        $.selector_cache('#Brandnames').val('');
        $.selector_cache('#ImageLogo').val('')
        $('#Supplierlist option:selected').prop('selected', false);
    });
});
$('#_PasswordSecurity').on('click', 'form button[id="BtnPassSecurity"]', function (e) {
    var passwordconfig = {
        Password_Length: $("#Password_Length").val(),
        Unique_Password_Count: $("#Unique_Password_Count").val(),
        Session_Expiry_Hours: $("#Session_Expiry_Hours").val(),
        Password_Expiry_Day: $("#Password_Expiry_Day").val(),
        Login_Attempt: $("#Login_Attempt").val(),
        Allow_Special_Character: $("#Allow_Special_Character").prop('checked'),
        Check_Capital: $("#Check_Capital").prop('checked'),
        Alpha_Numeric: $("#Alpha_Numeric").prop('checked'),
        Remember_Password: $("#Remember_Password").prop('checked'),
    };
    if (isNaN(passwordconfig.Password_Length) || 6 > passwordconfig.Password_Length || 50 < passwordconfig.Password_Length) {
        swal.fire("Error!", "Value should be between 6 - 50.", "error");
        return;
    }
    if (isNaN(passwordconfig.Unique_Password_Count) || 1 > passwordconfig.Unique_Password_Count || 12 < passwordconfig.Unique_Password_Count) {
        swal.fire("Error!", "Value should be between 1 - 12.", "error");
        return;
    }
    if (isNaN(passwordconfig.Session_Expiry_Hours) || 1 > passwordconfig.Session_Expiry_Hours || 1440 < passwordconfig.Session_Expiry_Hours) {
        swal.fire("Error!", "Value should be between 1 - 1440.", "error");
        return;
    }
    if (isNaN(passwordconfig.Login_Attempt) || 0 > passwordconfig.Login_Attempt || 10 < passwordconfig.Login_Attempt) {
        swal.fire("Error!", "Value should be between 0 - 10.", "error");
        return;
    }
    $.ajax({
        url: '/Settings/_PasswordSecurityPost/',
        type: 'POST',
        data: passwordconfig,
        success: function (result) {
            if (result.IsSuccess && result.ReturnMessage === "Success" && result.Data === true) {
                swal.fire("Success", "Password security details Saved Successfully", "success");
            }
            else {
                swal.fire("Error", "Something went wrong. Please try again later", "error");
            }
        }
    });
});



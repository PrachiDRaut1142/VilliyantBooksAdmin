$(document).ready(function () {
    BusinessInfoDeafult();
    var req = 'This Field is is required';
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    $('#ZipCode').on('keypress', function (e) {
        var len = $('#ZipCode').val();
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);

        if ($(this).val().length >= 5) {
            e.preventDefault();
            return false;
        }
        else if (regex.test(str)) {
            $('#SpnZipCode').css('display', 'none');
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });
    $('#MinOrderValue').on('keypress', function (e) {
        var len = $('#MinOrderValue').val();
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            $('#SpnMinOV').css('display', 'none');
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });
    $('#StandradDeleiveryCharges').on('keypress', function (e) {
        var len = $('#StandradDeleiveryCharges').val();
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            $('#SpnSDC').css('display', 'none');
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });
    $('#ExpressDeleiveryCharges').on('keypress', function (e) {
        var len = $('#ExpressDeleiveryCharges').val();
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            $('#SpnEDC').css('display', 'none');
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });
    $('#CODAVAL').on('change', function () {
        if ($('#CODAVAL').val() > '0' || $('#CODAVAL').val() != '' || $('#CODAVAL').val() != null) {
            $('#SpnCA').css('display', 'none');
        }
        else {
            SubmitZip
        }
    });
    $('#ExpressDaval').on('change', function () {
        if ($('#ExpressDaval').val() > '0' || $('#ExpressDaval').val() != '' || $('#ExpressDaval').val() != null) {
            $('#SpnEDA').css('display', 'none');
        }
        else {
            $('#SpnEDA').html(req).css('display', 'block');
        }
    });
    $('#CountryName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCountryName').html(req).css('display', 'block');
            }
        }
        else if ($('#CountryName').val() != null || $('#CountryName').val() != '') {
            $('#SpnCountryName').css('display', 'none');
        }
        else {
            $('#SpnCountryName').html(req).css('display', 'block');
        }
    });
    $('#ShortCode').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnShortCode').html(req).css('display', 'block');
            }
        }
        else if ($('#ShortCode').val() != null || $('#ShortCode').val() != '') {
            $('#SpnShortCode').css('display', 'none');
        }
        else {
            $('#SpnShortCode').html(req).css('display', 'block');
        }
    });
    $('#symbol').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#Spnsymbol').html(req).css('display', 'block');
            }
        }
        else if ($('#symbol').val() != null || $('#symbol').val() != '') {
            $('#Spnsymbol').css('display', 'none');
        }
        else {
            $('#Spnsymbol').html(req).css('display', 'block');
        }
    });

});
var req = 'This Field is is required';

$('.PaymentGetway').on('shown.bs.tab', function () {
    PaymentsDefault();
});

function PaymentsDefault() {
    $.ajax({
        url: '/Settings/_GetPaymentsgatewayList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#paymentgateway').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            //$('#zipcodelist').DataTable();
        }
    });
}

$('#paymentgateway').on('click', '.PaymentsZDLC', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetpaymentgatewayDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            $('#id').val(result.id);
            $("#ChannelId").val(result.ChannelId);
            $('#SecretKey').val(result.SecretKey);
            $('#PublicKey').val(result.PublicKey);
            $('#paymentsCreate').modal('show');
        }
    });
});

$('.paymentZDLC').on('click', function () {
    $('#id').val('');
    $('#ChannelId').val("");
    $('#SecretKey').val("");
    $('#PublicKey').val("");
    $('#paymentsCreate').modal('show');
});

$('.paymentZDLC').on('click', function () {
    $('#ChannelId').val("");
    $('#SecretKey').val("");
    $('#PublicKey').val("");
    $('#paymentsCreate').modal('show');
});

$('#Submitpayment').on('click', function () {
    var numError = 0;
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create Payments gateway !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var id = $('#id').prop('value');
            var ChannelId = $("#ChannelId").prop('value');
            var SecretKey = $('#SecretKey').prop('value');
            var PublicKey = $("#PublicKey").prop('value');
            if (result.value) {
                var info = {
                    id: id,
                    ChannelId: ChannelId,
                    SecretKey: SecretKey,
                    PublicKey: PublicKey,
                }
                if (id == "")
                    $.ajax({
                        url: '/Settings/Createpaymentsgateway/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#paymentsCreate').modal('hide');
                                swal({
                                    text: "Payments gateway  Create Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    PaymentsDefault();
                                })
                            }
                            else {
                                $('#paymentsCreate').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    PaymentsDefault();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdatepaymentsDetails/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#' + id).remove();
                                $('#paymentsCreate').modal('hide');
                                swal({
                                    text: "Payments Gateway Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    PaymentsDefault();
                                })
                            }
                            else {
                                $('#paymentsCreate').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    PaymentsDefault();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

/* -- ZipCode Start Here ---  */

$('.zipCode').on('shown.bs.tab', function () {
    ZipCodeDefault();
});

function ZipCodeDefault() {
    $.ajax({
        url: '/Settings/_GetzipList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#ZipBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            //$('#zipcodelist').DataTable();
            $('.zipmanage').DataTable();
        }
    });
}

$('#ZipBody').on('click', '.ziptDetailcls', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetZipDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            $('#id').val(result.id);
            // $("#Hub").val(result.hub).prop('selected', true);
            $("#ZipCode").val(result.zipCode);
            $('#MinOrderValue').val(result.minOrderValue);
            $('#StandradDeleiveryCharges').val(result.standradDeleiveryCharges);
            $('#ExpressDeleiveryCharges').val(result.expressDeleiveryCharges);
            $('#Discription').val(result.discription);
            $('#CODAVAL').val(result.codaval).prop('selected', true);;
            $('#ExpressDaval').val(result.expressDaval).prop('selected', true);;
            $('#zipcodesCreate').modal('show');
        }
    });
});

$('.zipMdlCls').on('click', function () {
    $("#ExpressDaval option[value=0]").prop('selected', true);
    $("#CODAVAL option[value=0]").prop('selected', true);
    $('#zipcodesCreate').modal('show');
});
$('.CleaerZip').on('click', function () {
    $('#id').val('');
    $('#ZipCode').val("");
    $('#MinOrderValue').val("");
    $('#StandradDeleiveryCharges').val("");
    $('#ExpressDeleiveryCharges').val("");
    $('#Discription').val("");
    $("#ExpressDaval option[value=0]").prop('selected', true);
    $("#CODAVAL option[value=0]").prop('selected', true);
    $('#zipcodesCreate').modal('hide');
    $('#SpnCA').css('display', 'none');

    $('#SpnZipCode').css('display', 'none');
    $('#SpnMinOV').css('display', 'none');
    $('#SpnSDC').css('display', 'none');
    $('#SpnEDC').css('display', 'none');
    $('#SpnCA').css('display', 'none');
    $('#SpnEDA').css('display', 'none');
});

$('#SubmitZip').on('click', function () {
    var numError = 0;
    //var req = 'This Field is is required';
    //if ($('#ZipCode').val() === "" || $('#ZipCode').val() === null) {
    //    $('#SpnZipCode').html(req).css('display', 'block');
    //    numError += 1;
    //}
    //if ($('#MinOrderValue').val() === "" || $('#MinOrderValue').val() === null) {
    //    $('#SpnMinOV').html(req).css('display', 'block');
    //    numError += 1;
    //}
    //if ($('#StandradDeleiveryCharges').val() === "" || $('#StandradDeleiveryCharges').val() === null) {
    //    $('#SpnSDC').html(req).css('display', 'block');
    //    numError += 1;
    //}
    //if ($('#ExpressDeleiveryCharges').val() === "" || $('#ExpressDeleiveryCharges').val() === null) {
    //    $('#SpnEDC').html(req).css('display', 'block');
    //    numError += 1;
    //}
    //if ($('#CODAVAL').val() === '0' || $('#CODAVAL').val() === '' || $('#CODAVAL').val() === null) {
    //    $('#SpnCA').html(req).css('display', 'block');
    //    numError += 1;
    //}
    //if ($('#ExpressDaval').val() === '0' || $('#ExpressDaval').val() === '' || $('#v').val() === null) {
    //    $('#SpnEDA').html(req).css('display', 'block');
    //    numError += 1;
    //}
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create zipcode !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var id = $('#id').prop('value');
            var zipCode = $("#ZipCode").prop('value');
            var internationalCharge = $("#InternationalCharge").prop('value');
            var country = $("#Country").prop('value');
            var minOrdVale = $('#MinOrderValue').prop('value');
            var standradDeleiveryCharges = $("#StandradDeleiveryCharges").prop('value');
            var expressDeleiveryCharges = $("#ExpressDeleiveryCharges").prop('value');
            var discription = $("#Discription").prop('value');
            var cODAVAL = $("#CODAVAL").prop('value');
            var expressDaval = $("#ExpressDaval").prop('value');
            if (result.value) {
                var info = {
                    id: id,
                    ZipCode: zipCode,
                    InternationalCharge: internationalCharge,
                    Country: country,
                    StandradDeleiveryCharges: standradDeleiveryCharges,
                    ExpressDeleiveryCharges: expressDeleiveryCharges,
                    MinOrderValue: minOrdVale,
                    Discription: discription,
                    CODAVAL: cODAVAL,
                    ExpressDaval: expressDaval
                }
                if (id == "")
                    $.ajax({
                        url: '/Settings/CreateZipCode/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#zipcodesCreate').modal('hide');
                                swal({
                                    text: "ZipCode Create Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    ZipCodeDefault();
                                })
                            }
                            else {
                                $('#zipcodesCreate').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    ZipCodeDefault();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdateZipCodeDetails/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#' + id).remove();
                                $('#zipcodesCreate').modal('hide');
                                swal({
                                    text: "ZipCode Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    ZipCodeDefault();
                                })
                            }
                            else {
                                $('#zipcodesCreate').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    ZipCodeDefault();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

$('#ZipBody').on('click', '.zipDeleteCls', function () {
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
                    ZipCodeDefault();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    ZipCodeDefault();
                })
            }
        }
    });
});
$('#paymentgateway').on('click', '.PaymentsDeleteCls', function () {
    var Id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/paymentgatewayDelete?id=' + Id,
        type: 'Get',
        success: function (result) {
            if (result.IsSuccess == true) {
                swal({
                    text: "Successfully Delete Payment gateway",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    PaymentsDefault();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    PaymentsDefault();
                })
            }
        }
    });
});

/* -- Delivery Slot  Start Here ---  */
$('.deliverySlot').on('shown.bs.tab', function () {
    SlotDefault();
});

function SlotDefault() {
    $.ajax({
        url: '/Settings/_GetSlotList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#SlotBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#SlotList').DataTable();
        }
    });
}

$('#SlotBody').on('click', '.slotDetailcls', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetDeliverySlotDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            $('#Id').val(result.id);
            $('#SlotType').val(result.slotType).prop('selected', true);
            $('#Timing').val(result.timing);
            $('#Slots').modal('show');
        }
    });
});

$('.slotMdlCls').on('click', function () {
    $('#SlotType').val(-1);
    $('#Timing').val("");
    $('#Id').val('');
    $('#Slots').modal('show');
});

$('#SlotSubmitBtn').on('click', function () {
    var numError = 0;
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create Delivery Slot !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var id = $('#Id').prop('value');
            var slotType = $("#SlotType").prop('value');
            var timing = $("#Timing").prop('value');
            if (result.value) {
                var info = {
                    Id: id,
                    SlotType: slotType,
                    Timing: timing,
                }
                if (id == "")
                    $.ajax({
                        url: '/Settings/CreateDeliverySlot/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#Slots').modal('hide');
                                swal({
                                    text: "Delivery Slot Created Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    SlotDefault();
                                })
                            }
                            else {
                                $('#Slots').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    SlotDefault();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdateSlotList/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#' + id).remove();
                                $('#Slots').modal('hide');
                                swal({
                                    text: "Delivery Slot Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    SlotDefault();
                                })
                            }
                            else {
                                $('#Slots').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    SlotDefault();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

$('#SlotBody').on('click', '.SlotDeleteCls', function () {
    var Id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetSlotDelete?id=' + Id,
        type: 'Get',
        success: function (result) {
            if (result.IsSuccess == true) {
                swal({
                    text: "Successfully Delete Delivery Slot",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    SlotDefault();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    SlotDefault();
                })
            }
        }
    });
});

/* -- Offer Type  Start Here ---  */
$('.OfferType').on('shown.bs.tab', function () {
    offerDefault();
});

function offerDefault() {
    $.ajax({
        url: '/Settings/_GetofferList/',
        type: 'Get',
        success: function (result) {
            $('#OfferList').DataTable().destroy();
            if (result != "")
                $('#OfferBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#OfferList').DataTable();
        }
    });
}
$('#OfferBody').on('click', '.offerDetailscls', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetOfferTypeDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            $('#Id').val(result.id);
            $('#Status').val(result.status).prop('selected', true);
            $('#OfferName').val(result.offerName);
            $('#Offers').modal('show');
        }
    });
});
$('.offerCls').on('click', function () {
    $('#Status').val(-1);
    $('#OfferName').val("");
    $('#Id').val('');
    $('#Offers').modal('show');
});
$('#OfferSubmitBtn').on('click', function () {
    var numError = 0;
    var id = $('#Id').prop('value');
    if (numError == 0) {
        if (id == "") {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to create Offer Type !',
                showCancelButton: true,
                confirmButtonColor: '#3085D6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, create it!'
            }).then((result) => {
                var id = $('#Id').prop('value');
                var status = $("#Status").prop('value');
                var OfferName = $("#OfferName").prop('value');
                if (result.value) {
                    var info = {
                        Id: id,
                        Status: status,
                        OfferName: OfferName,
                    }
                    $.ajax({
                        url: '/Settings/CreateOfferType/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#Offers').modal('hide');
                                swal({
                                    text: "Offer Type Created Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    offerDefault();
                                })
                            }
                            else {
                                $('#Offers').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    offerDefault();
                                })
                            }
                        }
                    });

                }
            });
        }
        else {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to Update Offer Type !',
                showCancelButton: true,
                confirmButtonColor: '#3085D6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Update it!'
            }).then((result) => {
                var id = $('#Id').prop('value');
                var status = $("#Status").prop('value');
                var OfferName = $("#OfferName").prop('value');
                if (result.value) {
                    var info = {
                        Id: id,
                        Status: status,
                        OfferName: OfferName,
                    }
                    $.ajax({
                        url: '/Settings/UpdateOfferType/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#' + id).remove();
                                $('#Offers').modal('hide');
                                swal({
                                    text: "Delivery Slot Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    offerDefault();
                                })
                            }
                            else {
                                $('#Offers').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    offerDefault();
                                })
                            }
                        }
                    });
                }
            });
        }
    }
});


/* -- Brand Start Here ---  */
$('.brandInfo').on('shown.bs.tab', function () {
    BrandDeafult();
});
function BrandDeafult() {
    $.ajax({
        url: '/Settings/_GetBrandList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#BrandBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#BrandList').DataTable();
        }
    });
}

$('#BrandBody').on('click', '.brandDetailcls', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetBrandInfoDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            var path = 'https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/brand/';
            $('.BIds').val(result.Data.getbrandetails.id);
            $('.BrandIds').val(result.Data.getbrandetails.brandId);
            $('#BrandImg').attr('src', path + result.Data.getbrandetails.brandId + '.png');
            $('#BrandName').val(result.Data.getbrandetails.brandName);
            //$('#Logo').val(result.Data.getbrandetails.imglogo)
            for (var i = 0; i < result.Data.getSupplierlist.length; i++) {
                $("#Supplierlist option[value='" + result.Data.getSupplierlist[i] + "']").prop('selected', true);
            }
            $('#NewBrand').modal('show');
        }
    });
});

$('.brandMdlCls').on('click', function () {
    var path = 'https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/no-image.png'
    $('.BIds').val('');
    $('#BrandIds').val('');
    $('#BrandName').val('');
    $('#ImageLogo').val('')
    $('#BrandImg').attr('src', path);
    $('#Supplierlist').val('');
    $('#NewBrand').modal('show');
});

$('#SubmitBrand').on('click', function () {
    var numError = 0;
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create Brand !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var Id = $('.BIds').prop('value');
            var BrandName = $("#BrandName").prop('value');
            var Logo = $('input[type=file]').val();
            var a = Logo;
            var Supplierlist = $('#Supplierlist').prop('value');
            if (result.value) {
                var info = {
                    Id: Id,
                    BrandName: BrandName,
                    ImageLogo: Logo,
                    Supplierlist: Supplierlist
                }
                if (Id == "")
                    $.ajax({
                        url: '/Settings/CreateBrand/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result != "") {
                                $('#NewBrand').modal('hide');
                                swal({
                                    text: "Brand Create Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    BrandDeafult();
                                })
                            }
                            else {
                                $('#NewBrand').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    BrandDeafult();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdateBrand/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result != "") {
                                $('#NewBrand').modal('hide');
                                swal({
                                    text: "Brand Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    BrandDeafult();
                                })
                            }
                            else {
                                $('#NewBrand').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    BrandDeafult();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

$('#BrandBody').on('click', '.BrandDeleteCls', function () {
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
                    BrandDeafult();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    BrandDeafult();
                })
            }
        }
    });
});

/* -- Table Start Here -- */
$('.tableInfo').on('shown.bs.tab', function () {
    TableDeafult();
});

function TableDeafult() {
    $.ajax({
        url: '/Settings/_GetTableList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#TableBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#TableList').DataTable();
        }
    });
}

$('.tableMdlCls').on('click', function () {
    $('.TIds').val('');
    $('.tableIds').val('');
    $('#tableName').val('');
    $('#tableCode').val('');
    $('.HubInfo').val().prop('selected', true);
    $('#NewTable').modal('show');
});

$('#TableBody').on('click', '.tableDetailsCls', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetTableInfoDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            $('.TIds').val(result.id);
            $('.tableIds').val(result.tableId);
            $('#tableName').val(result.tableName);
            $('#tableCode').val(result.tableCode);
            $('.HubInfo').val(result.branch).prop('selected', true);
            $('.tblPerfernce').val(result.tblPerfernce).prop('selected', true);
            $('#NewTable').modal('show');
        }
    });
});

$('#TableBody').on('click', '.QrDetailsCls', function () {
    var id = $(this)[0].dataset.id;
    $('#QRtableCode').val(id);
    $('#QrCode').modal('show');
});

$('#QrGenerate').on('click', function () {
    var tableCode = $("#QRtableCode").prop('value');
    var domainName = 'https://www.arabiandastar.com?code=' + tableCode
    if (tableCode != "") {
        $.ajax({
            url: '/Settings/GenerateQRCode?tableCode=' + domainName,
            type: 'GET',
            success: function (result) {
                if (result != "") {
                    $('#QRtableCode').val(tableCode);
                    $('#imgae-qr').attr('src', result);
                    var link = document.createElement("a");
                    document.body.appendChild(link);
                    link.setAttribute("type", "hidden");
                    link.href = result;
                    link.download = tableCode + ".png";
                    link.click();
                    $('#QrCode').modal('show');
                }
                else {
                    $('#QrCode').modal('hide');
                    swal({
                        text: "Something Went Wrong",
                        type: "error",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        TableDeafult();
                    })
                }
            }
        });
    }
    else { }
});

$('#closeQR').on('click', function () {
    $('#imgae-qr').attr('src', '');
    $('#QRtableCode').val('');
    $('#QrCode').modal('hide');
})

$('#SubmitTable').on('click', function () {
    var numError = 0;
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create Table !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var id = $('.TIds').prop('value');
            var tableId = $(".tableIds").prop('value');
            var tableCode = $("#tableCode").prop('value');
            var tableName = $('#tableName').prop('value');
            var hub = $('.HubInfo').prop('value');
            var tblPerfernce = $('.tblPerfernce').prop('value');
            if (result.value) {
                var info = {
                    id: id,
                    tableId: tableId,
                    tableName: tableName,
                    TableCode: tableCode,
                    branch: hub,
                    tblPerfernce: tblPerfernce,
                }
                if (id == "")
                    $.ajax({
                        url: '/Settings/CreateTableCode/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result != "Not Found") {
                                $('#NewTable').modal('hide');
                                swal({
                                    text: "Table Create Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    TableDeafult();
                                })
                            }
                            else {
                                $('#NewTable').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    TableDeafult();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdateTableCode/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#' + id).remove();
                                $('#NewTable').modal('hide');
                                swal({
                                    text: "Table Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    TableDeafult();
                                })
                            }
                            else {
                                $('#NewTable').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    TableDeafult();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

$('#TableBody').on('click', '.deletetableId', function () {
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
                    TableDeafult();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    TableDeafult();
                })
            }
        }
    });
});

/* -- Password Policy Here -- */
$('.PasswordSecurityInfo').on('shown.bs.tab', function () {
    PasswordSecurityDeafult();
});

function PasswordSecurityDeafult() {
    $.ajax({
        url: '/Settings/_GetPasswordSecurityInfo/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('.passwordsecrutiyDetails').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
        }
    });
}

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
        url: '/Settings/_passwordSecurityPost/',
        type: 'POST',
        data: passwordconfig,
        success: function (result) {
            if (result.IsSuccess && result.ReturnMessage === "Success" && result.Data === true) {
                swal({
                    text: "Password security details Saved Successfully",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    PasswordSecurityDeafult();
                });
            }
            else {
                swal({
                    text: "Something went wrong",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    PasswordSecurityDeafult();
                });
            }
        }
    });
});

/* -- Business Info Here -- */
$('.businessInfo').on('shown.bs.tab', function () {
    BusinessInfoDeafult();
});

function BusinessInfoDeafult() {
    $.ajax({
        url: '/Settings/_GetBusinessInfo/',
        type: 'Get',
        success: function (data) {
            if (data != "")
                $('.businessInfodetails').html(data);
            else
                swal.fire("error", "Something went wrong", "error");
        }
    });
}

$('.businessInfodetails').on('click', '#editBusinessInfo', function (e) {
    var businessconfig = {
        hotel_id: $("#hotel_id").val(),
        hotel_name: $("#hotel_name").val(),
        logo_url: $("#logo_url").val(),
        printlogo_url: $("#printlogo_url").val(),
        secondarylogo_url: $("#secondarylogo_url").val(),
        thankhyouMessage: $("#thankhyouMessage").val(),
        splashScreenMessage: $("#splashScreenMessage").val(),
        bussiness_type: $("#bussiness_type").val(),
        currency: $("#currency").val(),
        symbol: $('#currency').find(':selected').attr('data-value'),
        Country: $("#Country").val(),
        first_name: $("#first_name").val(),
        last_name: $("#last_name").val(),
        email: $("#email").val(),
        contact_number: $("#contact_number").val(),
        alternate_contactnumber: $("#alternate_contactnumber").val(),
        website: $("#website").val(),
        business_emailaddress: $("#business_emailaddress").val(),
        TimeId: $("#TimeId").val(),
        legalbusinessName: $("#legalbusinessName").val(),
    };
    $.ajax({
        url: '/Settings/_businessInfoPost/',
        type: 'POST',
        data: businessconfig,
        enctype: 'multipart/form-data',
        success: function (result) {
            if (result.IsSuccess && result.ReturnMessage === "Success" && result.Data === 1) {
                swal({
                    text: "Business Info details Saved Successfully",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    BusinessInfoDeafult();
                });
            }
            else {
                swal({
                    text: "Something went wrong",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    BusinessInfoDeafult();
                });
            }
        }
    });
});

$(".businessInfodetails").on('change', '#UploadImage', function () {
    var hotel_id = $('#hotel_id').val();
    var files = $('#UploadImage').prop("files");
    var url = "/Settings/_businessLogUploader";
    formData = new FormData();
    formData.append("logoUpload", files[0]);
    formData.append("hotel_id", hotel_id)
    jQuery.ajax({
        type: 'POST',
        url: url,
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (repo) {
            if (repo.status == "success") {
                swal({
                    text: "Primary Logo Updated !",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    BusinessInfoDeafult();
                    window.location.reload();
                });
            }
        },
        error: function () {
            alert("Error occurs");
        }
    });
});

$(".businessInfodetails").on('change', '#SecondUploadImage', function () {
    var hotel_id = $('#hotel_id').val();
    var files = $('#SecondUploadImage').prop("files");
    var url = "/Settings/_businessSecondaryLogUploader";
    formData = new FormData();
    formData.append("SecondUploadImage", files[0]);
    formData.append("hotel_id", hotel_id)
    jQuery.ajax({
        type: 'POST',
        url: url,
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (repo) {
            if (repo.status == "success") {
                swal({
                    text: "Secondary Logo Updated !",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    BusinessInfoDeafult();
                    window.location.reload();
                });
            }
        },
        error: function () {
            alert("Error occurs");
        }
    });
});

/* TAX STATUS */
$('.taxaTion').on('shown.bs.tab', function () {
    TaxationDeafult();
});

function TaxationDeafult() {
    $.ajax({
        url: '/Settings/_GetTaxInfo/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('.taxInfodetails').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
        }
    });
}

$(".taxInfodetails").on('change', '#taxType', function () {
    if ($(this).val() == '1') {
        $("#calcuationTaxtype").val('1');
        $("#GSTINNo_view").css("display", "block");
        $("#VATINo_view").css("display", "none");
        $("#OverallBill_ViewGST").css("display", "none");
        $("#OverallBill_ViewVAT").css("display", "none");
    }
    if ($(this).val() == '2') {
        $("#calcuationTaxtype").val('2');
        $("#GSTINNo_view").css("display", "none");
        $("#VATINo_view").css("display", "block");
        $("#OverallBill_ViewVAT").css("display", "block");
        $("#OverallBill_ViewGST").css("display", "none");
    }
});

$(".taxInfodetails").on('change', '#calcuationTaxtype', function () {
    if ($(this).val() == '1') {
        $("#taxType").val('1');
        $("#GSTINNo_view").css("display", "block");
        $("#VATINo_view").css("display", "none");
        $("#OverallBill_ViewGST").css("display", "none");
        $("#OverallBill_ViewVAT").css("display", "none");
    }
    if ($(this).val() == '2') {
        var taxType = $('#taxType').val();
        if (taxType == 1) {
            $("#GSTINNo_view").css("display", "block");
            $("#VATINo_view").css("display", "none");
            $("#OverallBill_ViewGST").css("display", "block");
            $("#OverallBill_ViewVAT").css("display", "none");
        }
        else {
            $("#GSTINNo_view").css("display", "none");
            $("#VATINo_view").css("display", "block");
            $("#OverallBill_ViewVAT").css("display", "block");
            $("#OverallBill_ViewGST").css("display", "none");
        }
    }
});

$('.taxInfodetails').on('click', '#EditTaxationInfo', function (e) {
    var isActive = $("#isActive").prop('value');
    var taxconfig = {
        taxType: $('#taxType').val(),
        taxRegNo: $("#taxRegNo").val(),
        vatRegNo: $("#vatRegNo").val(),
        calcuationTaxtype: $("#calcuationTaxtype").val(),
        taxPercentGST: $("#taxPercentGST").val(),
        taxPercentVAT: $("#taxPercentVAT").val(),
        taxbillType: $("#taxbillType").val(),
        isActive: isActive
    };
    $.ajax({
        url: '/Settings/_GetTaxationUpdate/',
        type: 'POST',
        data: taxconfig,
        success: function (result) {
            if (result.IsSuccess && result.ReturnMessage === "Success" && result.Data === true) {
                swal({
                    text: "Taxation details Saved Successfully",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    TaxationDeafult();
                });
            }
            else {
                swal({
                    text: "Something went wrong",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    TaxationDeafult();
                });
            }
        }
    });
});

// Currency Config Start Here...
$('.currencyInfo').on('shown.bs.tab', function () {
    CurrencyDeafult();
});

function CurrencyDeafult() {
    $.ajax({
        url: '/Settings/_GetCurrencyList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#currencyBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            $('#CurrencyList').DataTable();
        }
    });
}

$('#currencyBody').on('click', '.deletecurrencyId', function () {
    var Id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/DeleteCurrency?id=' + Id,
        type: 'Get',
        success: function (result) {
            if (result.IsSuccess == true) {
                swal({
                    text: "Successfully Delete Currency",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    CurrencyDeafult();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    CurrencyDeafult();
                })
            }
        }
    });
});

$('.currnencyMdlCls').on('click', function () {
    $('.CIds').val('');
    $('.CountryName').val('');
    $('.ShortCode').val('');
    $('.symbol').val('');
    $('#NewCurrency').modal('show');
});

$('#currencyBody').on('click', '.currencyDetailsCls', function () {
    var id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/GetCurrencyInfoDetails?id=' + id,
        type: 'Get',
        success: function (result) {
            $('.CIds').val(result.id);
            $('.CountryName').val(result.countryName);
            $('.ShortCode').val(result.shortCode);
            $('.symbol').val(result.symbol);
            $('#NewCurrency').modal('show');
        }
    });
});
$('.clearcurrency').on('click', function () {
    $('#SpnCountryName').css('display', 'none');
    $('#SpnShortCode').css('display', 'none');
    $('#Spnsymbol').css('display', 'none');
});

$('#Submitcurrency').on('click', function () {
    var numError = 0;
    if ($('#CountryName').val() === "" || $('#CountryName').val() === null) {
        $('#SpnCountryName').html(req).css('display', 'block');
        numError += 1;
    }
    if ($('#ShortCode').val() === "" || $('#ShortCode').val() === null) {
        $('#SpnShortCode').html(req).css('display', 'block');
        numError += 1;
    }
    if ($('#symbol').val() === "" || $('#symbol').val() === null) {
        $('#Spnsymbol').html(req).css('display', 'block');
        numError += 1;
    }
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to Add new Currency !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Add it!'
        }).then((result) => {
            var Id = $('.CIds').prop('value');
            var CountryName = $(".CountryName").prop('value');
            var ShortCode = $(".ShortCode").prop('value');
            var symbol = $('.symbol').prop('value');
            if (result.value) {
                var info = {
                    Id: Id,
                    CountryName: CountryName,
                    ShortCode: ShortCode,
                    symbol: symbol,
                }
                if (Id == "")
                    $.ajax({
                        url: '/Settings/AddCurrency/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result != "Not Found") {
                                $('#NewCurrency').modal('hide');
                                swal({
                                    text: "Currency Successfully Added",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    CurrencyDeafult();
                                })
                            }
                            else {
                                $('#NewCurrency').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    CurrencyDeafult();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/EditCurrency/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#NewCurrency').modal('hide');
                                swal({
                                    text: "Currency Edit Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    CurrencyDeafult();
                                })
                            }
                            else {
                                $('#NewCurrency').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    CurrencyDeafult();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

// Inventory Assets 
$('.assetMdlCls').on('click', function () {
    $('#AssetName').val("");
    $('#AssetUnit').val("");
    $('#AssetLife').val("");
    $('#assetDescription').val("");
    $('#Quantity').val("");
    $("#Serviceable option[value=0]").prop('selected', true);
    $('#AssetCreateBy').val("");
    $('#inventoryAssetCreateid').modal('show');
});

$('.InventoryAsset').on('shown.bs.tab', function () {
    InventoryDefault();
});

function InventoryDefault() {
    $.ajax({
        url: '/Settings/_InventoryAssetList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#inventoryasset').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
        }, error: function (result) { }
    });
}

$('#inventoryasset').on('click', '#DeletedId', function () {
    var Id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/Delete_Inventory_Asset?id=' + Id,
        type: 'Get',
        success: function (result) {
            if (result.IsSuccess == true)
                swal({
                    text: "Inventory Assets Deleted Successfully",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    InventoryDefault();
                })
            else
                swal({
                    text: "Something Went Wrong",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    InventoryDefault();
                })
        },
        error: function (result) {
            swal({
                text: "Server Error, Pleas Try Later ",
                type: "error",
                confirmButtonText: "OK"
            }).then(function (e) {
                InventoryDefault();
            })
        }
    })
});

$('#InventoryAsset').on('click', '.OpenDetailAss', function () {
    $('#AssetName').val("");
    $('#AssetUnit').val("");
    $('#AssetLife').val("");
    $('#Quantity').val("");
    $("#Serviceable option[value=0]").prop('selected', true);
    $('#AssetCreateBy').val("");
    $('#inventoryAssetCreateid').modal('show');
});

$('#inventoryasset').on('click', '.OpenDetailAss', function () {
    var Id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/InventoryDetails?id=' + Id,
        type: 'Get',
        type: 'Get',
        success: function (result) {
            $('#assetsId').val(result.id);
            $('#AssetName').val(result.assetName);
            $('#AssetUnit').val(result.assetsUnitPrice);
            $('#AssetLife').val(result.assetsLifespan);
            $('#Serviceable').val(result.isServicable).prop('selected', true);
            $('#assetDescription').val(result.assetDescriptions);
        },
        error: function () {
            alert('server error');
        }
    });
});

$('#SubmitAsset').on('click', function () {
    var numError = 0;
    var AssetName = $('#AssetName').val();
    var Quantity = $('#Quantity').val();
    var AssetUnit = $('#AssetUnit').val();
    var AssetLife = $('#AssetLife').val();
    if (AssetName == '') {
        numError++;
        $('#AssetNameErr').css('display', 'block');
    }
    if (Quantity == '') {
        numError++;
        $('#QuantityErr').css('display', 'block');
    }
    if (AssetUnit == '') {
        numError++;
        $('#AssetUnitErr').css('display', 'block');
    }
    if (AssetLife == '') {
        numError++;
        $('#AssetLifeErr').css('display', 'block');
    }
    if ($('#Serviceable').prop('value').trim() == "0") {
        numError++;
        $('#ServiceableErr').css('display', 'block');
    } else {
        $('#ServiceableErr').css('display', 'none');
    }
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create Inventory Asset !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var Id = $('#assetsId').prop('value');
            var AssetName = $("#AssetName").prop('value');
            var AssetsUnitPrice = $("#AssetUnit").prop('value');
            var AssetsLifespan = $('#AssetLife').prop('value');
            var Quantity = $('#Quantity').prop('value');
            var IsServicable = $("#Serviceable").prop('value');
            var AssetDescriptions = $("#assetDescription").prop('value');
            if (result.value) {
                var info = {
                    Id: Id,
                    AssetName: AssetName,
                    AssetsUnitPrice: AssetsUnitPrice,
                    AssetsLifespan: AssetsLifespan,
                    Quantity: Quantity,
                    IsServicable: IsServicable,
                    AssetDescriptions: AssetDescriptions,
                }
                if (Id == "")
                    $.ajax({
                        url: '/Settings/CreateInventory/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                $('#inventoryAssetCreateid').modal('hide');
                                swal({
                                    text: "Inventory Asset Create Successfully",
                                    type: "success",
                                    confirmButtonText: "OK",
                                }).then(function (e) {
                                    InventoryDefault();
                                })
                            }
                            else {
                                $('#inventoryAssetCreateid').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK",

                                }).then(function (e) {
                                    InventoryDefault();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdateInventory/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result.result != 0) {
                                //  $('#' + id).remove();
                                $('#inventoryAssetCreateid').modal('hide');
                                swal({
                                    text: "Inventory Assets Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK",

                                }).then(function (e) {
                                    InventoryDefault();
                                })
                            }
                            else {
                                $('#inventoryAssetCreateid').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK",

                                }).then(function (e) {
                                    InventoryDefault();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

$("#AssetName").on('keypress', function (e) {
    if ($(this).val().length == 0) {
        if (e.which == 32) {
            console.log('Space Detected');
            return false;
        }
    }
});

$('#AssetName').keypress(function (e) {
    var regex = new RegExp(/^[a-zA-Z\s]+$/);
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    else {
        e.preventDefault();
        return false;
    }
});

$('#AssetUnit').keypress(function (e) {
    var regex = new RegExp(/^[0-9]+$/);
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    else {
        e.preventDefault();
        return false;
    }
});

$("#AssetUnit").on('keypress', function (e) {
    if ($(this).val().length == 0) {
        if (e.which == 32) {
            console.log('Space Detected');
            return false;
        }
    }
});

$("#AssetUnit").on('keypress', function (e) {
    if ($(this).val().length == 0) {
        if (e.which == 32) {
            console.log('Space Detected');
            return false;
        }
    }
});

$("#AssetLife").on('keypress', function (e) {
    if ($(this).val().length == 0) {
        if (e.which == 32) {
            console.log('Space Detected');
            return false;
        }
    }
});

$("#Quantity").on('change', function () {
    var quantity = $('#Quantity').val();
    var unitprice = $('#unitprices').text();
    var netprice = 0.0;
    if ($('#Quantity').val() == '') {
        $('#unitprices').val(parseFloat(price));
    }
    else {
        netprice = parseFloat(quantity * price).toFixed(2);
        unitprice = parseFloat(quantity * unitprice).toFixed(2);
        $('#unitprices').val(netprice);
    }
});

$("#Quantity").keypress(function () {
    if ($('#AssetName').text() == '') {
        var quantity = $('#Quantity').val();
        var netprice = parseFloat(quantity * 20).toFixed(2);
        $('#unitprices').val(netprice);
    }
})

$('#AssetName').on('keydown', function (e) {
    if ($('#AssetName').prop('value').length = "") {
        $('#AssetNameErr').css('display', 'block');
    }
    else {
        $('#AssetNameErr').css('display', 'none');
    }
});

$('#Quantity').on('keydown', function (e) {
    if ($('#Quantity').prop('value').length = "") {
        $('#QuantityErr').css('display', 'block');
    }
    else {
        $('#QuantityErr').css('display', 'none');
    }
});

$('#AssetUnit').on('keydown', function (e) {
    if ($('#AssetUnit').prop('value').length = "") {
        $('#AssetUnitErr').css('display', 'block');
    }
    else {
        $('#AssetUnitErr').css('display', 'none');
    }
});

$('#AssetLife').on('keydown', function (e) {
    if ($('#AssetLife').prop('value').length = "") {
        $('#AssetLifeErr').css('display', 'block');
    }
    else {
        $('#AssetLifeErr').css('display', 'none');
    }
});

$('#Serviceable').on('change', function (e) {
    if ($('#Serviceable').prop('value').trim() == "0") {
        $('#ServiceableErr').css('display', 'block');
    } else {
        $('#ServiceableErr').css('display', 'none');
    }
});

$('#EntryId').on('change', function (e) {
    if ($('#EntryId').prop('value').trim() == "0") {
        $('#EntryIdErr').css('display', 'block');
    } else {
        $('#EntryIdErr').css('display', 'none');
    }
});

$('#Inventory').on('change', function (e) {
    if ($('#Inventory').prop('value').trim() == "0") {
        $('#InventoryError').css('display', 'block');
    } else {
        $('#InventoryError').css('display', 'none');
    }
});

$('#AssetUnitPrice').on('keydown', function (e) {
    if ($('#AssetUnitPrice').prop('value').length = "") {
        $('#AssetUnitPriceErr').css('display', 'block');
    }
    else {
        $('#AssetUnitPriceErr').css('display', 'none');
    }
});

$('#QuantityAd').on('keydown', function (e) {
    if ($('#QuantityAd').prop('value').length = "") {
        $('#QuantityAdErr').css('display', 'block');
    }
    else {
        $('#QuantityAdErr').css('display', 'none');
    }
});

// recipe management start
$('.RecipeInfo').on('shown.bs.tab', function () {
    RecipeDefault();
});

function RecipeDefault() {
    $.ajax({
        url: '/Settings/_GetRecipeList/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#RecipeBody').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            //$('#zipcodelist').DataTable();
        }
    });
}
$('.ProductType').on('shown.bs.tab', function () {
    ProductMainSpec();
});

function ProductMainSpec() {
    $.ajax({
        url: '/Settings/_GetProductSpecMain/',
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#Productmain').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            //$('#zipcodelist').DataTable();
        }
    });
}

// empty create modal
$('.RecipeMdlCls').on('click', function () {
    $('#R_Id').val('');
    $('#RecipeId').val('');
    $('#Item_name').val("");
    $("#Item_type option[value=0]").prop('selected', true);
    $('#RecipeCreate').modal('show');
});
// create and update Function for recipe
$('#SubmitRecipe').on('click', function () {
    var numError = 0;
    if (numError == 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to create Recipe !',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, create it!'
        }).then((result) => {
            var R_Id = $('#R_Id').prop('value');
            var RecipeId = $('#RecipeId').prop('value');
            var Item_name = $("#Item_name").prop('value');
            var Item_type = $("#Item_type").prop('value');
            if (result.value) {
                var info = {
                    "R_Id": R_Id,
                    "RecipeId": RecipeId,
                    "Item_name": Item_name,
                    "Item_type": Item_type,
                }
                if (R_Id == "")
                    $.ajax({
                        url: '/Settings/CreateRecipe/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result != 0) {
                                $('#RecipeCreate').modal('hide');
                                swal({
                                    text: "Recipe Create Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    RecipeDefault();
                                })
                            }
                            else {
                                $('#RecipeCreate').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    RecipeDefault();
                                })
                            }
                        }
                    });
                else {
                    $.ajax({
                        url: '/Settings/UpdateRecipeDetails/',
                        type: 'GET',
                        data: info,
                        success: function (result) {
                            if (result != 0) {
                                $('#RecipeCreate').modal('hide');
                                swal({
                                    text: "recipe Update Successfully",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    RecipeDefault();
                                })
                            }
                            else {
                                $('#RecipeCreate').modal('hide');
                                swal({
                                    text: "Something Went Wrong",
                                    type: "error",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    RecipeDefault();
                                })
                            }
                        }
                    });
                }
            }
        });
    }
});

//get recipe details by Id
$('#RecipeBody').on('click', '.RecipeDetailcls', function () {
    var R_Id = $(this).attr('data-id')
    $.ajax({
        url: '/Settings/GetRecipeDetails?id=' + R_Id,
        type: 'Get',
        success: function (result) {
            $('#R_Id').val(result.r_Id);
            $('#RecipeId').val(result.recipeId);
            $("#Item_name").val(result.item_name);
            $('#Item_type').val(result.item_type).prop('selected', true);
            $('#RecipeCreate').modal('show');
        }
    });
});
$('#RecipeBody').on('click', '.RecipeDeleteCls', function () {
    var Id = $(this)[0].dataset.id;
    $.ajax({
        url: '/Settings/DeleteRecipe?id=' + Id,
        type: 'Get',
        success: function (result) {
            if (result.IsSuccess == true) {
                swal({
                    text: "Successfully Delete Zipcode",
                    type: "success",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    RecipeDefault();
                })
            }
            else {
                swal({
                    text: "Something went wrong!",
                    type: "error",
                    confirmButtonText: "OK"
                }).then(function (e) {
                    RecipeDefault();
                })
            }
        }
    });
});



$("#ZipCreate").on('change', '#Country', function () {

    var data = $('#Country').val();

    if (data == "saudhi") {
        $('#zip').css('display', 'block');
        $('#min').css('display', 'block');
        $('#internal').css('display', 'none');
        $('#sta').css('display', 'block');
        $('#exp').css('display', 'block');
        $('#des').css('display', 'block');
        $('#cod').css('display', 'block');
        $('#expdel').css('display', 'block');
    }
    else {
        $('#zip').css('display', 'none');
        $('#min').css('display', 'block');
        $('#internal').css('display', 'block');
        $('#sta').css('display', 'none');
        $('#exp').css('display', 'none');
        $('#des').css('display', 'none');
        $('#cod').css('display', 'none');
        $('#expdel').css('display', 'none');
    }


});


$("#Productmain").on('click', ".ProductSpecsCls", function () {
    var data = $(this).attr("data-id");
    var name = $(this).attr("data-name");
    $("#MainProductcatValue").val(name);
    $("#MainProductcatId").val(data);
    $('#product_specification_md').modal('show');
    $.ajax({
        url: '/Settings/_GetProductSpecSucCategory?productId=' + data ,
        type: 'Get',

        success: function (result) {
            if (result != "")
                $('#productSubcategoryList').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            //$('#zipcodelist').DataTable();
        }
    });
});

$("#product_specification_md").on('click', "#open_modal", function () {
    $('#kuch_to_de_md').modal('show');
});

$("#add_product_specification").on("click", function () {
    var name = $("input[name='name']").val();
    var markup = "<tr><td><input type='text'/></td><td class='Add-save'><button type='button' class='btn btn-dark mr-2 Add-member'>Save</button></td></tr>";
    $("#product_table tbody").append(markup);
});

// Event delegation for dynamically created elements
$("#product_table tbody").on("click", ".Add-member", function () {
    // Get the value from the input field of the clicked row
    var ProductSubCatName = $(this).closest("tr").find("input[type='text']").val();
    var ProductCategoryId = $("#MainProductcatId").val(); 
    // Assuming you want to send the value to the server using AJAX // Corrected selector
    var info = {
        ProductSubCatName: ProductSubCatName,
        productCatId: ProductCategoryId,
    };

    $.ajax({
        url: '/Settings/AddProductSubSpecs/',
        type: 'GET',
        data: info,
        success: function (result) {
            getProductSubCat();
           
        }
    });
});


$('#productSubcategoryList').on('click', '.delete-member', function () {
    var prductId = $(this).attr('data-id');

    Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to delete this product specification?',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: '/Settings/DeleteProductSUbSpecs?Id=' + prductId,
                type: 'GET',
                success: function (result) {
                    getProductSubCat();
                    // You might want to show a success message here using Swal.fire
                    Swal.fire({
                        title: 'Deleted!',
                        text: 'Product specification has been deleted.',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    });
                },
                error: function () {
                    // Handle error if the AJAX request fails
                    Swal.fire({
                        title: 'Error!',
                        text: 'Failed to delete product specification.',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                }
            });
        }
    });
});

$('#productSubcategoryList').on('click', '.edit-member', function () {
    var parentRow = $(this).closest('tr'); // Find the closest parent row
    $(this).hide();
    var editCell = parentRow.find(".edit"); // Find the cell containing the value

    editCell.each(function () {
        var content = $(this).text(); // Get the text content of the cell
        $(this).html('<input type="text" class="edit-input" value="' + content + '"/>'); // Replace with an input field
    });

    parentRow.find('.save-member').show(); // Show the save button
});


$('#productSubcategoryList').on('click', '.Add-member', function () {
    //getProductSubCat();
});

$('.clearProductType').on('click',function () {
    $('#producttype').val('');

});



$('#productSubcategoryList').on('click', '.save-member', function () {
    var prductId = $(this).attr('data-id');
    var content;

    // Get the content from each edit-input and replace it with its parent
    $('.edit-input').each(function () {
        content = $(this).val();
        $(this).parent().html(content);
    });

    var info = {
        ProductSubCatName: content,
        ProductSubCatId: prductId,
    };

    // Make AJAX call to update the product subcategory specifications
    $.ajax({
        url: '/Settings/UpdateProductSubSpecs/',
        type: 'GET',
        data: info,
        success: function (result) {
            // After successful update, refresh the product subcategory list
            getProductSubCat();
        }
    });

    // Hide the save button and show the edit button
    $(this).hide();
    $(this).parent().find('.edit-member').show();
});

//$('#productSubcategoryList').on('click','.Add-member', function () {
//    alert("check")
//    getProductSubCat();
//});


//$('#productSubcategoryList').on('click','.save-member',function () {
//    var prductId = $(this).attr('data-id');
//    alert(prductId);
//    $('textarea').each(function () {
//        var content = $(this).val();//.replace(/\n/g,"<br>");
//        $(this).html(content);
//        $(this).contents().unwrap();
//    });
//    $(this).hide();
//    $(this).parent().find('.edit-member').show();
//    getProductSubCat();
//});


function getProductSubCat() {
    var ProductCategoryId = $("#MainProductcatId").val(); 

    $.ajax({
        url: '/Settings/_GetProductSpecSucCategory?productId=' + ProductCategoryId,
        type: 'Get',
        success: function (result) {
            if (result != "")
                $('#productSubcategoryList').html(result);
            else
                swal.fire("error", "Something went wrong", "error");
            //$('#zipcodelist').DataTable();
        }
    });
}


$('.SubmitMainProduct').on('click', function () {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to add a new Product Type?',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Add it!'
    }).then((result) => {
        if (result.value) {
            var ProductCategoryName = $('#producttype').val(); // Corrected selector

            var info = {
                ProductCategoryName: ProductCategoryName,
            };

            $.ajax({
                url: '/Settings/AddProductSpecs/',
                type: 'GET',
                data: info,
                success: function (result) {
                    if (result != "Not Found") {
                        $('#AddProduct').modal('hide');
                        $('#producttype').val('');
                        Swal.fire({
                            text: "Product Successfully Added",
                            type: "success",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            ProductMainSpec();
                        });
                    } else {
                        $('#AddProduct').modal('hide');
                        $('#producttype').val('');
                        Swal.fire({
                            text: "Something Went Wrong",
                            type: "error",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            ProductMainSpec();
                        });
                    }
                },
                error: function () {
                    $('#AddProduct').modal('hide');
                    $('#producttype').val('');
                    Swal.fire({
                        text: "Error Occurred",
                        type: "error",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        ProductMainSpec();
                    });
                }
            });
        }
    });
});

$("#Productmain").on('click', '.deleteProductSpecsId', function () {
    var Id = $(this).attr("data-id");
    Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to delete this product?',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {

            $.ajax({
                url: '/Settings/DeleteProductSpecs?Id='+Id,
                type: 'GET',
                success: function (result) {
                    if (result != "Not Found") {
                        Swal.fire({
                            text: "Product Successfully Delete",
                            type: "success",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            ProductMainSpec();
                        });
                    } else { 
                        Swal.fire({
                            text: "Something Went Wrong",
                            type: "error",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            ProductMainSpec();
                        });
                    }
                },
                error: function () {    
                    Swal.fire({
                        text: "Error Occurred",
                        type: "error",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        ProductMainSpec();
                    });
                }
            });
            
        }
    });
});
$("#EditProductMainCat").on('click', function () {
  
    Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to Update this product name?',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Update'
    }).then((result) => {
        if (result.value) {
            var ProductCategoryName = $("#MainProductcatValue").val();
            var ProductCategoryId = $("#MainProductcatId").val(); 

            var info = {
                ProductCategoryName: ProductCategoryName,
                ProductCategoryId: ProductCategoryId,
            };
            $.ajax({
                url: '/Settings/UpdateProductSpecs/',
                type: 'GET',
                data:info,
                success: function (result) {
                    if (result != "Not Found") {
                        $('#product_specification_md').modal('show');
                        Swal.fire({
                            text: "Product Successfully Updated",
                            type: "success",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            getProductSubCat(ProductCategoryId);
                            ProductMainSpec();
                        });
                    } else { 
                        $('#product_specification_md').modal('show');
                        Swal.fire({
                            text: "Something Went Wrong",
                            type: "error",
                            confirmButtonText: "OK"
                        }).then(function (e) {
                            getProductSubCat(ProductCategoryId);
                            ProductMainSpec();
                        });
                    }
                },
                error: function () {    
                    $('#product_specification_md').modal('show');
                    Swal.fire({
                        text: "Error Occurred",
                        type: "error",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        getProductSubCat(ProductCategoryId);
                        ProductMainSpec();
                    });
                }
            });
            
        }
    });
});




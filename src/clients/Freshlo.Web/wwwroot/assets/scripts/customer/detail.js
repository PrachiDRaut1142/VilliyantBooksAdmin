$(function () {
    var value = $('#CustomerId').prop('value');
    $.ajax({
        type: 'GET',
        url: '/Customer/_salesList?customerId=' + value,
        success: function (data) {
            $('#Orderslist').html(data);
            $.selector_cache('#Saleslist').dataTable();
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});
$(function () {
    var value = $('#CustomerId').prop('value');
    $.ajax({
        type: 'GET',
        url: '/Customer/_addressadd?customerId=' + value,
        success: function (data) {
            $('#Addadress').html(data);
            $.selector_cache('#Addaddress').dataTable();
            $('#m_portlet_loader').css('display', 'none');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal('Error!', 'There was some error. Try again later.', 'error');
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});
$('#addaddress').on('click','#deleteadd', function () {
    var Id = $(this)[0].dataset.id;
    Swal.fire({
        title: 'Are you sure?',
        text: 'you want to delete Address!',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: '/Customer/Delete?id=' + Id,
                type: 'GET',
                success: function (result) {
                    if (result.IsSuccess == true) {
                        swal({
                            text: "Successfully remove  Address",
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



$('#addaddress').on('click', '#editadd', function () {
    var custId = $(this)[0].dataset.id;   
    $.ajax({
        url: "/Customer/GetcustDetail?Type=" + 'Default' + "&custId=" + custId,
        type: "GET",
        success: function (result) {
            if (result.IsSuccess == true) {
                $('#Name').val(result.Data.name);
                $('#EmailId').val(result.Data.emailId);
                $('#ContactNo').val(result.Data.contactNo);
                $('#Sector').val(result.Data.sector);
                $('#Landmark').val(result.Data.landmark);
                $('#Locality').val(result.Data.locality);
                $('#AddressType').val(result.Data.addressType);
                $('#Address2').val(result.Data.address2);
                $('#City').val(result.Data.city);
                $('#State').val(result.Data.state);
                $('#Country').val(result.Data.country);
                $('#Addid').val(result.Data.addId);
                $('#address_details').modal('show');
            }            
        }
    });
});

$('#add_sec_address').on('click', function () {
    var CustomerId = $(this)[0].dataset.id;   
    $('#custId').val(CustomerId);
    $('#add_second_address').modal('show');
});

$('#addresschangebutton').on('click', function () {   
    $('#UpdateUser').submit();
});

$('#create2add').on('click', function () {
    $('#Create2address').submit();
});

//function CustEdit(param) {
//    var paramarray = param.split(',');
//    var custid = paramarray[0];
//    var addid = paramarray[1];
//    $.ajax({
//        url: "/Sale/GetcustDetail?Type=" + 'Default' + "&custId=" + custid,
//        type: "GET",
//        success: function (result) {
//            $CustomerId.val(result.Data.customerId);
//            $Name.val(result.Data.name);
//            $EmailId.val(result.Data.emailId);
//            //$Ext.val(result.getCustomerDetail.ext);
//            $ContactNo.val(result.Data.contactNo);
//            $BuildingName.val(result.Data.buildingName);
//            $RoomNo.val(result.Data.roomNo);
//            $Sector.val(result.Data.sector);
//            $Landmark.val(result.Data.landmark);
//            $Locality.val(result.Data.locality);
//            $AddressType.val(result.Data.addressType);
//            $ZipCode.val(result.Data.zipCode);
//            $City.val(result.Data.city);
//            $State.val(result.Data.state);
//            $Country.val(result.Data.country);
//            $('#addid').val(result.Data.addId);
//            $('#compAddId').val(addid);
//            $('#create_new_address').modal('show');
//        }
//    });
//}

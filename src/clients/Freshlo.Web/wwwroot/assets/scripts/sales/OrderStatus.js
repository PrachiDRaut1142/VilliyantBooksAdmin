$('#OrderStatusId').on('change', function () {
    var orderstatus = this.value;
    var salesId = $('#SalesOrderId1').val();
    var id = $('#sales-Id').val();
    var hub = $('#HubId').val();
    var number = $('#CustomerNo').val();
    var html = "";
    if (orderstatus == "Cancelled") {
        $('#Cancel_Order1').modal('show');
    }
    if (orderstatus == "Shipped") {
        $('#AssignAWBshipped').modal('show');
    }
    if (orderstatus == "Dispatched")
        $('#DeliveryPerson_li').css('display', 'block');

    if (orderstatus == "Out for delivery") {
        // SendOtp(id);
        $.ajax({
            type: 'GET',
            url: '/Sale/GetDeliveryEmployee',
            data: {
                'hubId': hub,
                'orderStatus': orderstatus,
                'salesId': salesId,
                'number': number,
                'id': id
            },
            success: function (data) {
                $('#DeliveryPerson_li').css('display', 'block');
                if (data.IsSuccess == true) {
                    var list = data.Data;
                    $('#DeliveryPersonId').html('');
                    html += '<option value="0" selected disabled>Select Name</option>';
                    for (var i = 0; i < data.Data.length; i++) {
                        html += ('<option value="' + list[i].value + '">' + list[i].text + '</option>');
                    }
                    $('#DeliveryPersonId').html(html);
                    if (orderstatus == "Out for delivery")
                        $('#AssignDeliveryPerson').modal('show');
                    //$('#pendingOrderDiv').html(data);
                    //$.selector_cache('#pendingSales').dataTable();  
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
        $('#DeliveryPerson_li').css('display', 'none');

    }  
});
$('#Cancel_Order1').on('click', "#CancelReason1", function () {
    var SalesId = $('#sales-Id1').val();
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
                        location.reload();
                        $('#Cancel_Order1').modal('hide');
                    }
                    else {
                        swal.fire("error", "Something went wrong!", "error");
                    }
                }
            });
        }
    });
});
$('#AssignDelivery-DrpDwn').on('click', function () {
    var orderstatus = $('#OrderStatusId').val();
    var hub = $('#HubId').val();
    var salesId = $('#SalesOrderId1').val();
    var html = "";
    if (orderstatus == "Out for delivery") {
        // SendOtp(id);
        $.ajax({
            type: 'GET',
            url: '/Sale/GetDeliveryPerson',
            data: {
                'hubId': hub,
                'orderStatus': orderstatus,
                'salesId': salesId,
            },
            success: function (data) {
                if (data.IsSuccess == true) {
                    var list = data.Data;
                    $('#DeliveryPersonId').html('');
                    html += '<option value="0" selected disabled>Select Name</option>';
                    for (var i = 0; i < data.Data.length; i++) {
                        html += ('<option value="' + list[i].value + '">' + list[i].text + '</option>');
                    }
                    $('#DeliveryPersonId').html(html);
                    if (orderstatus == "Out for delivery")
                        $('#AssignDeliveryPerson').modal('show');
                    //$('#pendingOrderDiv').html(data);
                    //$.selector_cache('#pendingSales').dataTable();  
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
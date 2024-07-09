!(function ($) {
    $('.updatepayment').on('click', function () {
        var myPaymentSettledId = $(this).data('id');
        var arr = myPaymentSettledId.split('_');
        var SalesOrderId = arr[0];
        var Id = arr[1];
        var settlementStatus = $('#SettledStatus_' + arr[1]).val();
        var SettlementNotes = $('#Description_' + arr[1]).val();
        var settledfor = arr[3];
        var UpdatePaymentSettlement = {
            SalesOrderId: arr[0],
            Id: arr[1],
            Settlement_Status:$('#SettledStatus_' + arr[1]).val(),
            Description: $('#Description_' + arr[1]).val(),
            PaymentMode: $('#PaymentMode_' + arr[1]).val(),
            PaymentSettled_for: arr[3]
        };
        $.ajax({
            url: '/Sale/UpdatePaymentSettlement',
            type: 'POST',
            data: UpdatePaymentSettlement,
            success: function (data) {
                if (data.ReturnMessage == "Success") {
                    swal({
                        text: "Payment  is Settled",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        location.reload();
                    })
                }
            }
        });
    });
    $('#btnSaveSettlement').on('click', function () {
        var atLeastOneIsChecked = $('input[name="Check[]"]:checked').length;
        if (atLeastOneIsChecked > 0) {
            var checkboxdata = [];
            var Description = [];
            var inputElements = document.getElementsByClassName('messageCheckbox');
            for (var i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    checkedValue = inputElements[i].value;
                    Description.push($('#Description_' + inputElements[i].id).val());
                    checkboxdata.push(checkedValue);
                }
            }
            var Checkboxidsdata = {
                "Checkboxid": checkboxdata,
                "multiDescription": Description
            };
            $.ajax({
                url: '/Sale/UpdatePaymentSettlement/',
                type: 'Post',
                dataType: 'json',
                data: { 'payment': Checkboxidsdata },
                success: function (data) {
                    if (data.ReturnMessage == "Success") {
                        swal({
                            title: "Payment  is Settled",
                            type: "success",
                            confirmButtonText: "Ok!"
                        }).then(function (e) {
                            if (e.value) {
                                location.reload();
                            }
                        });
                    }
                    else {
                        swal("Error", "Something went wrong. Please try again later", "error");
                    }
                }
            });
        }
        else {
            swal("Select Checkbox!", "", "warning");
        }
    });
    $('#selectallid').on('click', function () {
        if ($('input[type=checkbox]').prop('checked') == true) {
            $("input:checkbox").prop('checked', $(this).is(":checked"));
            $('#settledStatus').val('Paid');
        } else {
            $("input:checkbox").prop('checked', false);
            $('#settledStatus').val('Pending');
        }
    });
})(jQuery);
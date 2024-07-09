$(function () {
    $(document).ready(function () {
        $('#m_portlet_loader').css('display', 'block');
        var a = $.selector_cache("#date").prop('value');
        var b = $.selector_cache("#status").prop('value');
        if (a != 0) {
            $.ajax({
                type: 'GET',
                url: '/Customer/_manage/',
                data: {
                    'a': a,
                    'b': b,
                },
                success: function (result) {
                        $.selector_cache('#m_table_1').dataTable().fnDestroy();
                        $.selector_cache('#divTblId').html(result);
                        $.selector_cache('#m_table_1').dataTable();
                        $('#m_portlet_loader').css('display', 'none');
                    }               
            });
        }
        else {
            $('#m_portlet_loader').css('display', 'block');
            $.ajax({
                url: '/Customer/_manage/',
                type: 'POST',
                success: function (result) {
                   $.selector_cache('#m_table_1').dataTable().fnDestroy();
                    $.selector_cache('#divTblId').html(result);
                    $.selector_cache('#m_table_1').dataTable();
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        }
    });
    $('#excelCustomerDetail').on('click', function () {
        window.location.href = '/Customer/GetFilterExcel/';
    });
});

$('#divTblId').on('click', '.walletCls', function () {
    var customerId = $(this)[0].dataset.id;
    $('#CustomerId').prop('value', customerId);
    $('#walletCash').modal('show');
});
$('#SubmitBtn').on('click', function () {
    var CustomerId = $('#CustomerId').prop('value');
    var amount = $('#AmountId').prop('value');
    var description = $('#DescriptionId').prop('value');
    var existingValue = $("#m_table_1 #walletId_" + CustomerId).text();
    var addition = parseFloat(existingValue) + parseFloat(amount);
    var dicData = {
        CustomerId: CustomerId,
        Amount: amount,
        Description: description
    };
    $.ajax({
        url: '/Customer/AddToWallet',
        type: 'POST',
        dataType: 'JSON',
        data: dicData,
        success: function (data) {
            if (data.IsSuccess == true) {
                $("#m_table_1 #walletId_" + CustomerId).text(addition);
                $('#walletCash').modal('hide');
            }
            else {
                swal.fire('Oops!', 'Something went wrong!', 'warning');
            }
        }
    });
});


















 //var divWidth = $("#m_portlet_loader_1").width();
            //var divHeight = $("#m_portlet_loader_1").height();

            //var m_portlet_loader = "<div class='m_portlet__inner_loader' style='width: " + divWidth + "px; height: " + divHeight + "px; '><div class='m_portlet__inner_loader_v' style=''><div class='loader'></div></div></div>";
            //$('#m_portlet_loader_1_app').append(m_portlet_loader);


//$('#m_portlet_loader_1_app').append(m_portlet_loader).hide();


            //var divWidth = $("#m_portlet__body_loader_1").width();
            //var divHeight = $("#m_portlet__body_loader_1").height();
            //var m_portlet__body_loader = "<div class='m_portlet__inner_loader' style='width: " + divWidth + "px; height: " + divHeight + "px; '><div class='m_portlet__inner_loader_v' style=''><div class='loader'></div></div></div>";
            //$('#m_portlet__body_loader_1_app').append(m_portlet__body_loader);
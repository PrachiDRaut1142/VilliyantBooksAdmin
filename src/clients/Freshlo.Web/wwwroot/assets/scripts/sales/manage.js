!(function ($) {
    $(document).ready(function () {
        $('#m_portlet_loader').css('display', 'block');
        var a = $.selector_cache("#date").prop('value');
        var b = $.selector_cache("#status").prop('value');
        var c = $.selector_cache("#hub").prop('value');
        var d = $.selector_cache("#zipcode").prop('value');
        if (a != "") {
            $.ajax({
                type: 'GET',
                url: '/Sale/_salesOrderList/',
                data: {
                    'a': a,
                    'b': b,
                    'c': c,
                    'd': d
                },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $.selector_cache('#sales').dataTable().fnDestroy();
                        $.selector_cache('#saleslist').html(data.Data);
                        $.selector_cache('#sales').dataTable();
                        $('#m_portlet_loader').css('display', 'none');
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                        $('#m_portlet_loader').css('display', 'none');
                    } else {
                        swal('Error!', 'There was some error. Try again later.', 'error');
                        $('#m_portlet_loader').css('display', 'none');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        } 
    });

    $.selector_cache('#m_datetimepicker_6').on('click', function () {
        $.selector_cache("#m_datetimepicker_6").css('border-color', '');
    });
    $.selector_cache('.filter').on('click', function () {
        $('#m_portlet_loader').css('display', 'block');
        var date = $.selector_cache("#m_datetimepicker_6").prop('value');
        var status = $.selector_cache("#StatusId").prop('value');
        var source = $.selector_cache("#Source").prop('value');
        var payment = $.selector_cache("#Payment").prop('value');
        if (date != "") {
            $.ajax({
                type: 'GET',
                url: '/Sale/_salesOrderList/',
                data: {
                    'date': date,
                    'status': status,
                    'source': source,
                    'payment': payment
                },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $.selector_cache('#sales').dataTable().fnDestroy();
                        $.selector_cache('#saleslist').html(data.Data);
                        $.selector_cache('#sales').dataTable();
                        $('#m_portlet_loader').css('display', 'none');
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                        $('#m_portlet_loader').css('display', 'none');
                    } else {
                        swal('Error!', 'There was some error. Try again later.', 'error');
                        $('#m_portlet_loader').css('display', 'none');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        } else {
            $.selector_cache("#m_datetimepicker_6").css('border-color', 'red');
            $('#m_portlet_loader').css('display', 'none');
            swal('Error!', 'Cannot empty the delivery date field.', 'error');
            //setTimeout(function () { $('#m_portlet_loader').css('display', 'none'); }, 500);
        }        
    });
    $.selector_cache('#btnTotalSalePrint').on('click', function () {
        var date = $.selector_cache("#m_datetimepicker_6").prop('value');
        window.location.href ='/Print/TodaySalesToPrint?Date=' + date;

    });
    $.selector_cache('#SalesPrintId').on('click', function () {
        $('#SalestoPrint').css('display', 'block');
        $('#SalestoExcel').css('display', 'none');
        $('#customdatetoggle').prop("checked", false);
        $('#salesdaterange').val('');
        $('#divddaterange').css('display', 'none');
        $('#divdate').css('display', 'block');
        var d = new Date();
        var data = d.getMonth() + 1 + '-' + d.getDate() + '-' + d.getFullYear();
        $('#TodayDate').val(data);
        $('#dateRangePic').modal('show');
    });

    $.selector_cache('#SalesExcelId').on('click', function () {
        $('#SalestoPrint').css('display', 'none');
        $('#SalestoExcel').css('display', 'block');
        $('#customdatetoggle').prop("checked", false);
        $('#salesdaterange').val('');
        $('#divddaterange').css('display', 'none');
        $('#divdate').css('display', 'block');
        var d = new Date();
        var data = d.getMonth() + 1 + '-' + d.getDate() + '-' + d.getFullYear();
        $('#TodayDate').val(data);
        $('#dateRangePic').modal('show');
    });

    $.selector_cache('#customdatetoggle').on('click', function () {
        var data = $(this).is(':checked');
        if (!$(this).is(':checked')) {
            $('#divddaterange').css('display', 'none');
            $('#divdate').css('display', 'block');
            var d = new Date();
            var data = d.getMonth() +1 + '-' + d.getDate() + '-' + d.getFullYear();
            $('#TodayDate').val(data);
        }
        else {
            $('#divddaterange').css('display', 'block');
            $('#divdate').css('display', 'none');
            $('#TodayDate').val('');
        }
    });

    $('#SalestoPrint').on('click', function () {
        var numberofError = 0;
        if (!$(this).is(':checked')) {   
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

    $('#SalestoExcel').on('click', function () {
        var numberofError = 0;
        var checked = $('#customdatetoggle').prop('checked');
        if (checked == false) {
            if ($('#TodayDate').val() == "")
            {
            numberofError++;
                $('#SalesdateError').html("Cannot be Empty").css('display', 'block');
            }
        }    
        else {
            if ($('#salesdaterange').val() == "") {
                numberofError++;
                $('#SalesdaterangError').html("Cannot be Empty").css('display', 'block');
            }
        }
        if (numberofError == 0) {
            $('#SalesdaterangError').html("Cannot be Empty").css('display', 'none');
            $('#SalesdateError').html("Cannot be Empty").css('display', 'none');
            var datetimeprint = $('#TodayDate').val();
            var salesrangedate = $('#salesdaterange').val();
            window.location.href = '/Sale/ExportSalesListtoExcel?Date=' + datetimeprint + '&DateRange=' + salesrangedate;
            $('#dateRangePic').modal('hide');
        }
    });

    $.selector_cache('.filtrExcel').on('click', function () {
        var date = $.selector_cache("#m_datetimepicker_6").prop('value');
        var status = $.selector_cache("#StatusId").prop('value');
        var source = $.selector_cache("#Source").prop('value');
        var payment = $.selector_cache("#Payment").prop('value');
        var a = $.selector_cache('#date').prop('value');
        var b = $.selector_cache('#status').prop('value');
        var startdate = $.selector_cache('#hub').prop('value');
        var enddate = $.selector_cache('#zipcode').prop('value');
        window.location.href = '/Sale/GetFilterExcel?Date=' + date + '&Status=' + status + '&Source=' + source + '&Payment=' + payment + '&a=' + a + '&b=' + b + '&startdate=' + startdate + '&enddate=' + enddate
    }); 
})(jQuery);
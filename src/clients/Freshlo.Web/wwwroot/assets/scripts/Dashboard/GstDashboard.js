$('document').ready(function () {
    try
    {
        var currentDate = new Date();
        var day = currentDate.getDate();
        var month = currentDate.getMonth() + 1;
        var year = currentDate.getFullYear();
        var dateget = year + "/" + month + "/" + day;
        var datefrom = dateget;
        var dateto = dateget;
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Dashboard/_TaxPay?datefrom=' + datefrom + '&dateto=' + dateto,
            type: 'GET',
            success: function (data) {
                $('.GstTbl').DataTable().destroy();
                $('#gstList').html(data);
                $('.GstTbl').DataTable();
            }
        });
        $.ajax({
            url: '/Dashboard/_TaxPayGSTCount?datefrom=' + datefrom + '&dateto=' + dateto,
            type: "POST",
            success: function (result) {
                $('#TodayBasedData').html(result);
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    }
    catch (e)
    {
        alert(e);
    }       
});
$(function () {
    var hour = "07";
    var min = "00";
    $('.datetimerange').daterangepicker({
        "showDropdowns": true,
        "showWeekNumbers": true,
        "timePicker": true,
        "startDate": moment(new Date()).set("hour", hour).set("minute", min),
        "endDate": moment().add(1, 'day').set("hour", hour).set("minute", min),
        ranges: {
            'Today': [moment(new Date()).set("hour", hour).set("minute", min), moment().add(1, 'day').set("hour", hour).set("minute", min)],
            'Tomorrow': [moment().add(1, 'day').set("hour", hour).set("minute", min), moment().add(2, 'day').set("hour", hour).set("minute", min)],
            'Yesterday': [moment().subtract(1, 'days').set("hour", hour).set("minute", min), moment(new Date()).set("hour", hour).set("minute", min)],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
            'This week': [moment().startOf('week'), moment().endOf('week')],
            'Next 7 days': [moment(), moment().add(6, 'days')]
        },
        "locale": {
            "format": 'DD/MM/YYYY hh: mm A',
            "separator": " - ",
            "applyLabel": "Apply",
            "cancelLabel": "Cancel",
            "fromLabel": "From",
            "toLabel": "To",
            "customRangeLabel": "Custom",
            "weekLabel": "W",
            "daysOfWeek": [
                "Su",
                "Mo",
                "Tu",
                "We",
                "Th",
                "Fr",
                "Sa"
            ],
            "monthNames": [
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            ],
            "firstDay": 1
        },
        "linkedCalendars": false,
        "alwaysShowCalendars": false,
        //"startDate": "01/05/2018",
        //"endDate": "31/05/2018"
    }, function (start, end, label) {
        console.log('New date range selected: ' + start.format('YYYY/MM/DD') + ' to ' + end.format('YYYY/MM/DD') + ' (predefined range: ' + label + ')');
    });
});

function filterData() {
    var deliverydate = $('#deliverydate').val();
    var dataArray = deliverydate.split('-');
    var datefrom = dataArray[0];
    var dateto = dataArray[1];
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Dashboard/_TaxPay?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (data) {
            $('.GstTbl').DataTable().destroy();
            $('#gstList').html(data);
            $('.GstTbl').DataTable();
        }
    });
    $.ajax({
        url: '/Dashboard/_TaxPayGSTCount?datefrom=' + datefrom + '&dateto=' + dateto,
        type: "POST",
        success: function (result) {
            $('#TodayBasedData').html(result);
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}
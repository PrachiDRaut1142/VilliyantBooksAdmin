$('document').ready(function () {
    try {
        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate());
        var day = currentDate.getDate();
        var month = currentDate.getMonth() + 1;
        var year = currentDate.getFullYear();
        $('#deliverydate').val(year + "/" + month + "/" + day + " - " + year + "/" + month + "/" + day);
    } catch (e) {}
    window.onload = function () {
        window.print();
    },
        window.onafterprint = function () {
            window.location.href = '/Sale/tablestate/';
        };
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
            "format": 'YYYY/MM/DD hh: mm A',
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

!(function ($) {
    $(document).ready(function () {
        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate());
        var day = currentDate.getDate();
        var month = currentDate.getMonth() + 1;
        var year = currentDate.getFullYear();
        $('#deliverydate').val(year + "/" + month + "/" + day + " - " + year + "/" + month + "/" + day);
        var deliverydate = $('#deliverydate').val();
        var dataArray = deliverydate.split('-');
        var target = dataArray[0];
        var tab = target.replace(new RegExp('/', 'g'), '-');
        var tabc = tab.replace('AM', '');
        var datefrom = tabc.trim();
        var reciver = dataArray[1];
        var rab = reciver.replace(new RegExp('/', 'g'), '-');
        var rabc = rab.replace('AM', '');
        var dateto = rabc.trim();
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Print/_TodaySalesToPrint?datefrom=' + datefrom + '&dateto=' + dateto,
            type: 'GET',
            success: function (result) {
                $('.printTodaysSalesData').html(result);
                window.print();
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    })
})(jQuery);
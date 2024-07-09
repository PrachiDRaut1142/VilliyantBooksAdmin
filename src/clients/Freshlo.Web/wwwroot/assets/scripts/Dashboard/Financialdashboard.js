$('document').ready(function () {
    try {
        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate());
        var day = currentDate.getDate();
        var month = currentDate.getMonth() + 1;
        var year = currentDate.getFullYear();
        $('#deliverydate').val(year + "/" + month + "/" + day + " - " + year + "/" + month + "/" + day);
    } catch (e) {
    }
});
//$('document').ready(function () {
//    try {
//        var currentDate = new Date();
//        currentDate.setDate(currentDate.getDate());
//        var day = currentDate.getDate();
//        var month = currentDate.getMonth() + 1;
//        var year = currentDate.getFullYear();
//        $('#deliverydate').val(year + "/" + month + "/" + day + " - " + year + "/" + month + "/" + day);
//    } catch (e) {
//    }
//});

//$(function () {



//    $('.datetimerange').daterangepicker({
//        "showDropdowns": true,
//        "showWeekNumbers": true,
//        "timePicker": true,
//        "startDate": moment().subtract(30, 'days'),
//        "endDate": moment(new Date()),
//        ranges: {

//            'This Month': [moment().startOf('month'), moment().endOf('month')],
//            'May': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
//            'April': [moment().subtract(2, 'month').startOf('month'), moment().subtract(2, 'month').endOf('month')],
//            'March': [moment().subtract(3, 'month').startOf('month'), moment().subtract(3, 'month').endOf('month')],
//            'February': [moment().subtract(4, 'month').startOf('month'), moment().subtract(4, 'month').endOf('month')],
//            'January': [moment().subtract(5, 'month').startOf('month'), moment().subtract(5, 'month').endOf('month')],


//        },
//        "locale": {
//            "format": 'YYYY/MM/DD hh: mm A',
//            "separator": " - ",
//            "applyLabel": "Apply",
//            "cancelLabel": "Cancel",
//            "fromLabel": "From",
//            "toLabel": "To",
//            "customRangeLabel": "Custom",
//            "weekLabel": "W",
//            "daysOfWeek": [
//                "Su",
//                "Mo",
//                "Tu",
//                "We",
//                "Th",
//                "Fr",
//                "Sa"
//            ],
//            "monthNames": [
//                "January",
//                "February",
//                "March",
//                "April",
//                "May",
//                "June",
//                "July",
//                "August",
//                "September",
//                "October",
//                "November",
//                "December"
//            ],
//            "firstDay": 1
//        },
//        "linkedCalendars": false,
//        "alwaysShowCalendars": false,
//        //"startDate": "01/05/2018",
//        //"endDate": "31/05/2018"
//    }, function (start, end, label) {
//        console.log('New date range selected: ' + start.format('YYYY/MM/DD') + ' to ' + end.format('YYYY/MM/DD') + ' (predefined range: ' + label + ')');
//    });
//});
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

            'Yesterday': [moment().subtract(1, 'days').set("hour", hour).set("minute", min), moment(new Date()).set("hour", hour).set("minute", min)],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],


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

function filterData() {
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
        url: '/Dashboard/_Financial?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            $('#FinancialCount').html(result);
            $('#m_portlet_loader').css('display', 'none');

        }
    });
}

!(function ($) {

    $.selector_cache('#FinancialCount').on('click', '#OrderReceived', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 1;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#PendingOrder', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 2;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#Delivered', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 3;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#Cancelled', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 4;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#PendingPayment', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 5;
        var c = "0";
        var d = "0";
        window.location.href = '/Sale/Manage/?a=' + SelectCountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#CashSaleamt', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 5;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#OnlineSale', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 7;
        var c = "0";
        var d = "0";
        window.location.href = '/Sale/Manage/?a=' + SelectCountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#Card', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 6;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#Pending', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 14;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#Gpay', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 7;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#PhonePay', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 8;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#Paytm', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 9;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#DiscountAmt', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 11;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount').on('click', '#TaxAmount', function (e) {
        $('#m_portlet_loader').css('display', 'block');
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
        var SelecountId = 1;
        var Status = 12;
        var c = datefrom;
        var d = dateto;
        window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });


    $(document).ready(function () {
        filterData();
    })
    $.selector_cache('#FinancialCount').on('click', '#staticBackdrop', function (e) {
        $('#dateranger').modal('show');
    });

    $.selector_cache('#FinancialCount').on('click', '#filtrExcel', function (e) {
        var fincial = $('#assign').val();
        var parts = fincial.split("- ");
        var startdate = parts[0];
        var enddate = parts[1];
        var date = startdate;
        var startdate = startdate;
        var enddate = enddate;
        var status = $.selector_cache("#StatusId").prop('value');
        var source = $.selector_cache("#Source").prop('value');
        var payment = $.selector_cache("#Payment").prop('value');
        var a = 1;
        var b = 1;



        window.location.href = '/Sale/GetFilterExcel?Date=' + date + '&Status=' + status + '&Source=' + source + '&Payment=' + payment + '&a=' + a + '&b=' + b + '&startdate=' + startdate + '&enddate=' + enddate
        $("#dateranger").modal('hide');


    });


})(jQuery);
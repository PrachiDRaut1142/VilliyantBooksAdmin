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
            'Last 3 Days': [moment().subtract(2, 'days'), moment()],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
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
            url: '/Management/_itemdetails?datefrom=' + datefrom + '&dateto=' + dateto,
            type: 'GET',
            success: function (result) {
                if (result != "")
                    $('#itemdetails').html(result);
                $('#m_portlet_loader').css('display', 'none');
                itemdetails(datefrom, dateto);
            }
        });
    };

$(document).ready(function () { 
    $("#itemdetails").on('click', '#vegcount', function () {
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
        let type = 1;
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Management/_itemdetails?datefrom=' + datefrom + '&dateto=' + dateto + '&type='+ type,
            type: 'GET',
            success: function (result) {
                if (result != "")
                    $('#itemdetails').html(result);
                $('#m_portlet_loader').css('display', 'none');
                itemdetails(datefrom, dateto);
            }
        });
    });

    $("#itemdetails").on('click', '#nonvegcount', function () {
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
        let type = 2;
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Management/_itemdetails?datefrom=' + datefrom + '&dateto=' + dateto + '&type=' + type,
            type: 'GET',
            success: function (result) {
                if (result != "")
                    $('#itemdetails').html(result);
                $('#m_portlet_loader').css('display', 'none');
                itemdetails(datefrom, dateto);
            }
        });
    });
    $("#itemdetails").on('click', '#nonvegcount1', function () {
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
        let type = 3;
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Management/_itemdetails?datefrom=' + datefrom + '&dateto=' + dateto + '&type=' + type,
            type: 'GET',
            success: function (result) {
                $('#m_portlet_loader').css('display', 'none');
                if (result != "")
                    $('#itemdetails').html(result);               
                itemdetails(datefrom, dateto);
            }
        });
    });
});

function itemdetails(datefrom, dateto) {
    $.ajax({
        url: '/Management/ItemDetails?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
          /*  $('#itemname').html(result);*/
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}

$(document).ready(function () {
    filterData();
})
$(function () { 
    $(".ranges li[data-range-key='This Month']").addClass('active');
    var hour = "07";
    var min = "00";
    $('.datetimerange1').daterangepicker({
            "showDropdowns": true,
            "showWeekNumbers": true,
            "timePicker": true,
            //"startDate": moment(new Date()).set("hour", hour).set("minute", min),
            //"endDate": moment().add(1, 'day').set("hour", hour).set("minute", min),
        ranges: {
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
        "startDate": moment().startOf('month'),
        "endDate": moment().endOf('month'),
    }, function (start, end, label) {
        console.log('New date range selected: ' + start.format('YYYY/MM/DD') + ' to ' + end.format('YYYY/MM/DD') + ' (predefined range: ' + label + ')');
    });
    $('.datetimerange2').daterangepicker({
        "showDropdowns": true,
        "showWeekNumbers": true,
        "timePicker": true,
        //"startDate": moment(new Date()).set("hour", hour).set("minute", min),
        //"endDate": moment().add(1, 'day').set("hour", hour).set("minute", min),
        ranges: {
            'Today discount': [moment().subtract(1, 'days'), moment()],
            'Last 6 Days': [moment().subtract(5, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
            'Pervious Month': [moment().subtract(2, 'month').startOf('month'), moment().subtract(2, 'month').endOf('month')],
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
        "startDate": moment().startOf('month'),
        "EndDate": moment().endOf('month'),
    }, function (start, end, label) {
        console.log('New date range selected: ' + start.format('YYYY/MM/DD') + ' to ' + end.format('YYYY/MM/DD') + ' (predefined range: ' + label + ')');
    });
    $('.datetimerange3').daterangepicker({
        "showDropdowns": true,
        "showWeekNumbers": true,
        "timePicker": true,
        //"startDate": moment(new Date()).set("hour", hour).set("minute", min),
        //"endDate": moment().add(1, 'day').set("hour", hour).set("minute", min),
        ranges: {
            'This Year': [moment().startOf('year'), moment().endOf('year')],
            'Last Year': [moment().subtract(12, 'month').startOf('month'), moment().subtract('month').endOf('month')],
            'Last 6 Months': [moment().subtract(6, 'month').startOf('month'), moment().subtract('month').endOf('month')],
            'Last 3 Month': [moment().subtract(3, 'month').startOf('month'), moment().subtract('month').endOf('month')],
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
        "startDate": moment().startOf('year'),
        "endDate": moment().endOf('month'),
    }, function (start, end, label) {
        console.log('New date range selected: ' + start.format('YYYY/MM/DD') + ' to ' + end.format('YYYY/MM/DD') + ' (predefined range: ' + label + ')');
    });
});

// Month and Year wise Sales Revnew For Second Graph
function filterMoths() {
    var dateArray = new Array();
    var pendingArray = new Array();
    var cashArray = new Array();
    var upiArray = new Array();
    var cardArray = new Array();
    var deliverydate = $('#deliverydate3').val();
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
        url: '/Management/monthlyCashSummary?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            monthcount(datefrom, dateto);
            document.getElementById("chartContainer2").innerHTML = '&nbsp;';
            document.getElementById("chartContainer2").innerHTML = '<canvas id="sale_chart-2"></canvas>';
            var ctx = document.getElementById("sale_chart-2").getContext("2d");
            if (result != "") {
                for (let li of result.monthcashSummary) {
                    dateArray.push(li.month)
                    cashArray.push(li.totalPrice);
                }
                for (let li of result.monthcardSummary) {
                    cardArray.push(li.totalPrice);
                }
                for (let li of result.monthupiSummary) {
                    upiArray.push(li.totalPrice);
                }
                for (let li of result.monthpendingSummary) {
                    pendingArray.push(li.totalPrice);
                }
                var ctx = document.getElementById("sale_chart-2").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: dateArray,
                        datasets: [{
                            label: 'Pending',
                            backgroundColor: "#f4516c",
                            data: pendingArray,
                        }, {
                            label: 'Card',
                            backgroundColor: "#5867dd",
                            data: cardArray,
                        }, {
                            label: 'UPI',
                            backgroundColor: "#ffb822",
                            data:upiArray,
                        }, {
                            label: 'Cash',
                            backgroundColor: "#00d27a",
                            data:cashArray,
                        }],
                    },
                    options: {
                        tooltips: {
                            displayColors: true,
                            callbacks: {
                                mode: 'x',
                            },
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false,
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                ticks: {
                                    beginAtZero: true,
                                },
                                type: 'linear',
                            }]
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: { position: 'top' },
                    }
                });
            }
        }
    });
}

// Daily Sales Revnew For First Graph 
function filterData() {
    var dateArray = new Array();
    var pendingArray = new Array();
    var cashArray = new Array();
    var upiArray = new Array();
    var cardArray = new Array();
    var deliverydate = $('#deliverydate1').val();
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
        url: '/Management/SaleCashSummary?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            myData(datefrom, dateto);
            document.getElementById("chartContainer").innerHTML = '&nbsp;';
            document.getElementById("chartContainer").innerHTML = '<canvas id="sale_chart"></canvas>';
            var ctx = document.getElementById("sale_chart").getContext("2d");
            if (result != "") {
                for (let li of result.cashSummary) {
                    dateArray.push(li.createdDate)
                    cashArray.push(li.totalPrice);
                }
                for (let li of result.cardSummary) {
                    cardArray.push(li.totalPrice);
                }
                for (let li of result.upiSummary) {
                    upiArray.push(li.totalPrice);
                }
                for (let li of result.pendingSummary) {
                    pendingArray.push(li.totalPrice);
                }
                ctx = document.getElementById("sale_chart").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: dateArray,
                        datasets:
                            [
                                {
                                    label: 'Pending',
                                    backgroundColor: "#f4516c",
                                    data: pendingArray
                                },
                                {
                                    label: 'Cash',
                                    backgroundColor: "#00d27a",
                                    data: cashArray,
                                },
                                {
                                    label: 'UPI',
                                    backgroundColor: "#ffb822",
                                    data: upiArray,
                                },
                                {
                                    label: 'Card',
                                    backgroundColor: "#5867dd",
                                    data: cardArray,
                                }
                            ]
                    },
                    options: {
                        tooltips: {
                            displayColors: true,
                            callbacks: {
                                mode: 'x',
                            },
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false,
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                ticks: {
                                    beginAtZero: true,
                                },
                                type: 'linear',
                            }]
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: { position: 'top' },
                    }
                });
            }
        }
    });    
}

// Discount Sales Revnew For Third Graph
function filterdiscount() {
    var dateArray = new Array();
    var DiscountArray = new Array();
    var deliverydate = $('#deliverydate2').val();
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
        url: '/Management/DiscountSummary1?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            Discount(datefrom, dateto);
            document.getElementById("chartContainer3").innerHTML = '&nbsp;';
            document.getElementById("chartContainer3").innerHTML = '<canvas id="dishcount_chart"></canvas>';
            var ctx = document.getElementById("dishcount_chart").getContext("2d");
            if (result != "") {
                for (let li of result.discountsummary) {
                    dateArray.push(li.createdDate)
                    DiscountArray.push(li.discount);
                }
                var ctx = document.getElementById("dishcount_chart").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: dateArray,
                        datasets: [{
                            label: 'Discount', // Name the series
                            data: DiscountArray,
                            fill: false,
                            borderColor: '#f4516c', // Add custom color border (Line)
                            backgroundColor: '#f4516c', // Add custom color background (Points and Fill)
                            borderWidth: 1 // Specify bar border width
                        }]
                    },
                    options: {
                        responsive: true, // Instruct chart js to respond nicely.
                        maintainAspectRatio: true, // Add to prevent default behaviour of full-width/height
                    }
                });
            }
        }
    });
}

// Daily Sales Overalll Count First Sectrion Of Partial View 
function myData(datefrom,dateto) {
    $.ajax({
        url: '/Management/_SaleSummary?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (response) {
            $('#FinancialCount1').html(response);
            $('#m_portlet_loader').css('display', 'none');
        },
        error: function (er, sw, sq) {
            alert("Error occured in application");
        }
    });
}

// Month and Year Sales Second Section Of Partail View
function monthcount(datefrom, dateto) {
    $.ajax({
        url: '/Management/_SaleMonths?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (response) {
            $('#FinancialCount2').html(response);
            $('#m_portlet_loader').css('display', 'none');
        },
        error: function (er, sw, sq) {
            alert("Error occured in application");
        }
    });
}

// Discount Sales Third Section Of Partail View 
function Discount(datefrom, dateto) {
    $.ajax({
        url: '/Management/_discount?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (response) {
            $('#FinancialCount3').html(response);
            $('#m_portlet_loader').css('display', 'none');           
        },
        error: function (er, sw, sq) {
            alert("Error occured in application");
        }
    });
}

// On Load Page Daily Sales Graph Genrate
$(document).ready(function () {
    var dateArray = new Array();
    var pendingArray = new Array();
    var cashArray = new Array();
    var upiArray = new Array();
    var cardArray = new Array();
    var deliverydate = $('#deliverydate1').val();
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
        url: '/Management/SaleCashSummary?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            myData(datefrom, dateto);
            document.getElementById("chartContainer").innerHTML = '&nbsp;';
            document.getElementById("chartContainer").innerHTML = '<canvas id="sale_chart"></canvas>';
            var ctx = document.getElementById("sale_chart").getContext("2d");
            if (result != "") {
                for (let li of result.cashSummary) {
                    dateArray.push(li.createdDate)
                    cashArray.push(li.totalPrice);
                }
                for (let li of result.cardSummary) {
                    cardArray.push(li.totalPrice);
                }
                for (let li of result.upiSummary) {
                    upiArray.push(li.totalPrice);
                }
                for (let li of result.pendingSummary) {
                    pendingArray.push(li.totalPrice);
                }
                ctx = document.getElementById("sale_chart").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: dateArray,
                        datasets:
                            [
                                {
                                    label: 'Pending',
                                    backgroundColor: "#f4516c",
                                    data: pendingArray
                                },
                                {
                                    label: 'Cash',
                                    backgroundColor: "#00d27a",
                                    data: cashArray,
                                },
                                {
                                    label: 'UPI',
                                    backgroundColor: "#ffb822",
                                    data: upiArray,
                                },
                                {
                                    label: 'Card',
                                    backgroundColor: "#5867dd",
                                    data: cardArray,
                                }
                            ]
                    },
                    options: {
                        tooltips: {
                            displayColors: true,
                            callbacks: {
                                mode: 'x',
                            },
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false,
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                ticks: {
                                    beginAtZero: true,
                                },
                                type: 'linear',
                            }]
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: { position: 'top' },
                    }
                });
            }
        }
    });
});

// On Load Page Month and Sales Graph Genrate
$(document).ready(function () {
    var dateArray = new Array();
    var pendingArray = new Array();
    var cashArray = new Array();
    var upiArray = new Array();
    var cardArray = new Array();
    var deliverydate = $('#deliverydate3').val();
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
        url: '/Management/monthlyCashSummary?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            monthcount(datefrom, dateto);
            document.getElementById("chartContainer2").innerHTML = '&nbsp;';
            document.getElementById("chartContainer2").innerHTML = '<canvas id="sale_chart-2"></canvas>';
            var ctx = document.getElementById("sale_chart-2").getContext("2d");
            if (result != "") {
                for (let li of result.monthcashSummary) {
                    dateArray.push(li.month)
                    cashArray.push(li.totalPrice);
                }
                for (let li of result.monthcardSummary) {
                    cardArray.push(li.totalPrice);
                }
                for (let li of result.monthupiSummary) {
                    upiArray.push(li.totalPrice);
                }
                for (let li of result.monthpendingSummary) {
                    pendingArray.push(li.totalPrice);
                }
                var ctx = document.getElementById("sale_chart-2").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: dateArray,
                        datasets: [{
                            label: 'Pending',
                            backgroundColor: "#f4516c",
                            data: pendingArray,
                        }, {
                            label: 'Card',
                            backgroundColor: "#5867dd",
                            data: cardArray,
                        }, {
                            label: 'UPI',
                            backgroundColor: "#ffb822",
                            data: upiArray,
                        }, {
                            label: 'Cash',
                            backgroundColor: "#00d27a",
                            data: cashArray,
                        }],
                    },
                    options: {
                        tooltips: {
                            displayColors: true,
                            callbacks: {
                                mode: 'x',
                            },
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false,
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                ticks: {
                                    beginAtZero: true,
                                },
                                type: 'linear',
                            }]
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: { position: 'top' },
                    }
                });
            }
        }
    });
});

//On Load Page Discount Graph Genrate
$(document).ready(function () {
    var dateArray = new Array();
    var DiscountArray = new Array();
    var deliverydate = $('#deliverydate2').val();
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
        url: '/Management/DiscountSummary1?datefrom=' + datefrom + '&dateto=' + dateto,
        type: 'GET',
        success: function (result) {
            Discount(datefrom, dateto);
            document.getElementById("chartContainer3").innerHTML = '&nbsp;';
            document.getElementById("chartContainer3").innerHTML = '<canvas id="dishcount_chart"></canvas>';
            var ctx = document.getElementById("dishcount_chart").getContext("2d");
            if (result != "") {
                for (let li of result.discountsummary) {
                    dateArray.push(li.createdDate)
                    DiscountArray.push(li.discount);
                }
                var ctx = document.getElementById("dishcount_chart").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: dateArray,
                        datasets: [{
                            label: 'Discount', // Name the series
                            data: DiscountArray,
                            fill: false,
                            borderColor: '#f4516c', // Add custom color border (Line)
                            backgroundColor: '#f4516c', // Add custom color background (Points and Fill)
                            borderWidth: 1 // Specify bar border width
                        }]
                    },
                    options: {
                        responsive: true, // Instruct chart js to respond nicely.
                        maintainAspectRatio: true, // Add to prevent default behaviour of full-width/height
                    }
                });
            }
        }
    });
});

// Calling All Partail View 
(function ($) {
    $.selector_cache('#FinancialCount1').on('click', '#Overall1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
        var a = 1;
        var b = 3;
    //    window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount1').on('click', '#OrderReceived1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
        var a = 1;
        var b = 3;
       // window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount1').on('click', '#PendingPayment1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 5;
        var c = "0";
        var d = "0";
      //  window.location.href = '/Sale/Manage/?a=' + SelectCountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount1').on('click', '#Card1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
      //  window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount1').on('click', '#upi1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
        var Status = 10;
        var c = datefrom;
        var d = dateto;
     //   window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount2').on('click', '#cash1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
      //  window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount2').on('click', '#upi2', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
       // window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount2').on('click', '#card2', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
      //  window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount2').on('click', '#pending2', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate1').val();
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
       // window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });

    $.selector_cache('#FinancialCount3').on('click', '#DiscountAmt1', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var deliverydate = $('#deliverydate2').val();
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
        var Status = 10;
        var c = datefrom;
        var d = dateto;
      //  window.location.href = '/Sale/Manage/?a=' + SelecountId + '&b=' + Status + '&c=' + c + '&d=' + d;
    });
})(jQuery);
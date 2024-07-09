$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/OrderNotification").build();
    $(function () {
        connection.start().then(function () {
            InvokeNotifyCount();       
        }).catch(function (err) {
            return console.error(err.toString());
        });
    });

    // OrderNotify
    function InvokeNotifyCount() {
        connection.invoke("SendOrderNotification").catch(function (err) {
            return console.error(err.toString());
        });
    }

    connection.on("ReceiveOrderNotification", function (orderCount) {
        if (orderCount > 0) {
            // Toaster Impmenent here...
            randomToast();
            // Initially, animate here
            $(".notification-popup .noti").addClass("animateClass");
            // stop after 2 second 
            setTimeout(function () {
                $(".notification-popup .noti").removeClass("animateClass");
            }, 1200);
            $('span.count').html(orderCount);
        }
        else {                     
            $('span.count').html('0');        
        }
    });

    $('span.noti').click(function (e) {
        e.stopPropagation();
        $('.noti-content').show();
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        //only load notification if not already loaded
        if (count > 0) {
            updateNotification();
        }
        else {
            updateNotification();
            $('span.count', this).html('0');
        }
    })
    $('#ViewAll').click(function (e) {
        var type = 'viewall';
        location.replace("/Sale/PendingOrders");
        updateNotificationcount("",type);
    })

    

    $('html').click(function () {
        $('.noti-content').hide();
    })

    // update notification
    function updateNotification() {
        //var type = 'single';
        $('#notiContent').empty();
        $('#notiContent').append($('<li>Loading...</li>'));
        $.ajax({
            type: 'GET',
            url: '/Sale/GetNotificationOrderdtl',
            success: function (response) {
                $('#notiContent').empty();
                if (response.length == 0) {
                    $('#notiContent').append($('<li>No data available</li>'));
                }
                $.each(response, function (index, value) {
                    $('#notiContent').append($('<li>New Order  : ' + value.CustomerName + ' (' + value.CustomerNo + ') Placed  <a href="#" class="view-link" data-id="' + value.SalesOrderId +'"><span class="badge badge-warning ml-2" >View</span></a></li>'));
                });
                //updateNotificationcount(value.SalesOrderId, type);
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
    $('#notiContent').on('click', 'a.view-link', function (event) {
        event.preventDefault(); // Prevent the default behavior of the anchor tag
        var type = 'single';
        var id = $(this).data('id');
        location.replace('/Sale/Detail?Id='+ id);
        updateNotificationcount(id,type);
    });

    function updateNotificationcount(SalesOrderId , type) {
        $.ajax({
            type: 'POST',
            url: '/Sale/UpdateNotificationCount?SalesOrderId=' + SalesOrderId + '&type=' + type,
            success: function (response) {
                if (response.isSuccess == "true") {
                    $('span.count', this).html(response.Data);
                }
                else {
                    $('span.count', this).html(response.Data);
                }
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
    //function NewOrderToast() {
    //    toastr.options = {
    //          tapToDismiss: false
    //        , timeOut: 0
    //        , extendedTimeOut: 0
    //        , allowHtml: true
    //        , preventDuplicates: true
    //        , preventOpenDuplicates: true
    //        , newestOnTop: true
    //        , closeButton: true
    //        , closeHtml: '<button class="closetoast" style="background-color: grey; padding: 5px;">OK</button>'
    //    }
    //    var $toast = toastr.info("Order added Succesfully");
    //    setTimeout(function () {
    //        $toast.fadeOut(4000);
    //    }, 5000);
    //}
    function randomToast() {
        //toastr.options = {
        //      tapToDismiss: false
        //    , timeOut: 0
        //    , extendedTimeOut: 0
        //    , allowHtml: true
        //    , newestOnTop: true
        //    , preventDuplicates: false
        //    , preventOpenDuplicates: false
        //    , closeButton: true
        //    , closeHtml: '<button class="closetoast" style="background-color: grey; padding: 5px;">OK</button>'
        //}
        var $toast = toastr.info("Order added Succesfully");
        setTimeout(function () {
            $toast.fadeOut(2000);
        }, 2000);
    }
    //$('.closetoast').on('click', function () {
    //    toastr.clear();
    //});
})
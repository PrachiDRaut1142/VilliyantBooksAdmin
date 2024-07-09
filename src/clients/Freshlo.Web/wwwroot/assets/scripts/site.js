$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build();
    connection.start();
    connection.on("LoadKithcenOrderListData", function () {
        LoadKithcenOrderList();
    });
    LoadKithcenOrderList();
    function LoadKithcenOrderList() {
        $('#m_portlet_loader').css('display', 'block');     
        $.ajax({
            url: '/Sale/_PendingkitchenOrderList/?orderStatus=' + 2,
            type: "POST",
            success: function (result) {
                $('._PendingKitchOrderList').html(result);
                $('#pendingKitchenOrderList').css('display', 'block');
                $('#_pendingKitchenOrderList').css('display', 'block');
                $('#allKitchenOrderList').css('display', 'none');
                $('#readyKitchenOrderList').css('display', 'none');
                $('#closedKitchenOrderList').css('display', 'none');
                $('#cancelledOrderList').css('display', 'none');
            }
        });
    }
})
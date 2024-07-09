$(() => {
    var connections = new signalR.HubConnectionBuilder().withUrl("/SignalRserver1").build();
    connections.start();
    connections.on("LoadReadyKithcenOrderListData", function () {
        LoadReadyOrderList();
    });
    LoadReadyOrderList();
    function LoadReadyOrderList() {
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Sale/_readyKitchOrderList/',
            type: "POST",
            success: function (result) {
                $('._ReadyOrderList').html(result);
                $('#readyOrderList').css('display', 'block');
                $('#_readyOrderList').css('display', 'block');
            }
        });
    }
})
!(function ($) {
    $(document).ready(function () {
        $.ajax({
            url: '/Sale/_readyKitchOrderList/',
            type: "POST",
            success: function (result) {
                $('._ReadyOrderList').html(result);
                $('#readyOrderList').css('display', 'block');
                $('#_readyOrderList').css('display', 'block');
            }
        });
    });

    $(document).on("click", ".btn-close", function (e) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to update!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, update it!'
        }).then((result) => {
            var status = 'Close'
            var id = $(this)[0].dataset.id;
            var ItemId = $(this)[0].dataset.approval;
            if (result.value) {
                var info = {
                    SalesOrderId: id,
                    status: status,
                    ItemId: ItemId,
                }
                $.ajax({
                    url: '/Sale/ApprovalKitchenStatusUpdate',
                    type: 'GET',
                    data: info,
                    success: function (result) {
                        if (result.Data == true && result.IsSuccess == true) {
                            swal.fire("success", "Status Updated!", "success");
                            location.reload();
                        }
                        else
                            swal.fire("error", "Something went wrong!", "error");
                    }
                });
            }
        });
    });

    $(".btn-closedAll").click(function () {

        getallIdsC1();
        getallIdsC2();
        var id = getallIdsC1();
        var salelistId = getallIdsC2();
        var salesId = id;
        var salesListId = salelistId;
        var status = 'Closed'
        if (id.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'you want to add item !',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Update it!'
            }).then((result) => {
                if (result.value) {
                    var dicData = {
                        salesId: salesId,
                        salesListId: salesListId,
                        status: status
                    };
                    $.ajax({
                        url: '/Sale/ApprovalAllKitchenStatusUpdate/',
                        type: 'POST',
                        dataType: 'JSON',
                        data: dicData,
                        success: function (result) {
                            if (result.IsSuccess == true) {
                                swal({
                                    text: "Successfully Update All Order",
                                    type: "success",
                                    confirmButtonText: "OK"
                                }).then(function (e) {
                                    location.reload();
                                })
                            }
                        }
                    });
                }
            });
        }
    });

    var getallIdsC1 = function () {
        return $('#aaceptItem').find('.allIds1')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getallIdsC2 = function () {
        return $('#aaceptItem').find('.allIdss2')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }
})(jQuery);
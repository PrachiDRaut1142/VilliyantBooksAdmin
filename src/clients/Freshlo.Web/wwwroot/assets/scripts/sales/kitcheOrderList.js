!(function ($) {
    $(document).ready(function () {
        $('#m_portlet_loader').css('display', 'block');
        $.ajax({
            url: '/Sale/_PendingkitchenOrderList/?orderStatus=' + 2,
            type: "POST",
            success: function (result) {
                $('._PendingKitchOrderList').html(result);
                $('#pendingKitchenOrderList').css('display', 'block');
                $('#_pendingKitchenOrderList').css('display', 'block');
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    });

    $(document).on("click", ".btn-approval", function (e) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to update!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, update it!'
        }).then((result) => {
            var status = 'Accepted'
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

    $(document).on("click", ".btn-ready", function (e) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to update!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, update it!'
        }).then((result) => {
            var status = 'Ready'
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
            else {
                alert('not update..!');
            }
        });
    });

    $(".btn-readyAll").click(function () {
        getallIds1();
        getallIds2();
        var id = getallIds1();
        var salelistId = getallIds2();
        var salesId = id;
        var salesListId = salelistId;
        var status = 'Ready'
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

    var getallIds1 = function () {
        return $('#_pendingKitchOrderList').find('.allIds1')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getallIds2 = function () {
        return $('#_pendingKitchOrderList').find('.allIdss2')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    $(".btn-approvalAll").click(function () {
        getAllId1();
        getAllId2();
        var id = getAllId1();
        var salelistId = getAllId2();
        var salesId = id;
        var salesListId = salelistId;
        var status = 'Accepted'
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

    var getAllId1 = function () {
        return $('#_pendingKitchOrderList').find('.allId1')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }

    var getAllId2 = function () {
        return $('#_pendingKitchOrderList').find('.allId2')
            .toArray()
            .map(function (x) {
                return $(x).attr('id');
            });
    }   
})(jQuery);
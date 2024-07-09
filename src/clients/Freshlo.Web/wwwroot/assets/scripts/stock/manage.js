$(function () {
    $.ajax({
        url: '/Stock/_productList/',
        type: 'POST',
        success: function (result) {
            $('#_stocklist').html(result);
        }
    });
});
$.selector_cache('#SearchItem').on('keyup', $.delay(function () {
    var ItemName = $.selector_cache("#SearchItem").prop('value');
    $.ajax({
        type: 'GET',
        url: '/Stock/_productList/',
        data: { 'ItemName': ItemName },
        success: function (result) {
            $('#_stocklist').html(result);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal('Error!', 'There was some error. Try again later.', 'error');
        }
    });
}, 500));
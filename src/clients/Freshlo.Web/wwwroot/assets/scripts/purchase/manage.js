$(function () {
    $.ajax({
        url: '/Purchase/_manage/',
        type: 'POST',
        success: function (result) {
            $('#purchaseDivId').html(result);
            $('#tblPurchaseId').DataTable();
        }
    });
});
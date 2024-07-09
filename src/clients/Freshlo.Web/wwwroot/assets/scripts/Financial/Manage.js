$(function () {
    var DateRange = $('#dateRangeId').prop('value');
    $.ajax({
        url: '/Financial/_manage/',
        type: 'POST',
        data: {
            'dateRange': DateRange,
        },
        success: function (result) {
            $('#tbl_FinancialId').html(result);
            $('#financial_tblId').DataTable();
        }
    });
});
$('#filterId').on('click', function () {
    var DateRange=$('#dateRangeId').prop('value');
    $.ajax({
        type: 'POST',
        url: '/Financial/_manage',
        data: {
            'dateRange': DateRange,
        },
        success: function (data) {
            $('#tbl_FinancialId').html(data);
            $('#financial_tblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire('Error!', 'There was some error. Try again later.', 'error');
        }
    });
});
$('#clearFilter').on('click', function () {
    $('#dateRangeId').prop('value', '');
    $.ajax({
        type: 'POST',
        url: '/Financial/_manage',
        data: {
            'dateRange': null,
        },
        success: function (data) {
            $('#tbl_FinancialId').html(data);
            $('#financial_tblId').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal.fire('Error!', 'There was some error. Try again later.', 'error');
        }
    });
});
$('#ExportToExcel').on('click', function () {
    var DateRange = $('#dateRangeId').prop('value');
    window.location.href = '/Financial/ExportFinancialListtoExcel?dateRange=' + DateRange;
});
$('#Pdf').on('click', function () {
    var DateRange = $('#dateRangeId').prop('value');
    window.location.href = '/Financial/FinancialPDf?dateRange=' + DateRange;
});

!(function ($) {
    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal.fire('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');

    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal.fire('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }

    $('#SubmitVendor').on('click', function () {
        var nErrorCount = 0;
        if (0 === nErrorCount) {
            $('#UpdtVendor').submit();
        }
    });
})(jQuery);

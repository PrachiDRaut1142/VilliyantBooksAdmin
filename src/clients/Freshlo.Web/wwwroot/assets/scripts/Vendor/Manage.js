$(function () {
    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal.fire('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');
    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal.fire('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }

    $('.deleteId').on('click', function () {
        var Id = $(this)[0].dataset.id;
        $.ajax({
            url: '/Vendor/DeleteVendor?Id=' + Id,
            type: 'Get',
            success: function (result) {
                if (result.IsSuccess == true) {
                    swal({
                        text: "Successfully Delete Vendor",
                        type: "success",
                        confirmButtonText: "OK"
                    }).then(function (e) {
                        location.reload();
                    })
                }
                else {
                    swal.fire('Oops!', 'Something went wrong!', 'warning');
                }
            }
        });
    });
});
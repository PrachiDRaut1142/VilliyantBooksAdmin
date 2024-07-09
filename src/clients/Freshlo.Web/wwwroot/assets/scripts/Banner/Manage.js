$(function () {
    $('.bannermanage').DataTable({
        "pageLength": 10,
        "columnDefs": [
            { "targets": [7, 8], "orderable": false }
        ],
        "lengthMenu": [10, 25, 50, 75, 100]
    });
    if ($.selector_cache('#viewMessage1').prop('value')) {
        swal('Success', $.selector_cache('#viewMessage1').prop('value'), 'success');
    }
    if ($.selector_cache('#errorMessage1').prop('value')) {
        swal('Error', $.selector_cache('#errorMessage1').prop('value'), 'error');
    }
    $('.deleteId').on('click', function () {
        var Id = $(this)[0].dataset.id;
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to delete!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/Banner/Delete?id=' + Id,
                    type: 'GET',
                    success: function (result) {
                        if (result.IsSuccess == true) {
                            swal({
                                text: "Successfully remove to Banner",
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
    });
});
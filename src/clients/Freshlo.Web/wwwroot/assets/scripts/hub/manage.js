$(function () {
    $('.hubmanage').DataTable({
        "pageLength": 10,
        "columnDefs": [
            { "targets": [5, 6], "orderable": false }
        ],
        "lengthMenu": [10, 25, 50, 75, 100]
    });
    if ($('#viewMessage1').prop('value')) {
        Swal.fire('Success', $('#viewMessage1').prop('value'), 'success');
    }
    if ($('#errorMessage1').prop('value')) {
        Swal.fire('Error', $('#errorMessage1').prop('value'), 'error');
    }
    $('.deleteHubId').on('click', function () {
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
                    url: '/Hub/Delete?id=' + Id,
                    type: 'GET',
                    success: function (result) {
                        if (result.IsSuccess == true) {
                            swal({
                                text: "Successfully remove  Hub",
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
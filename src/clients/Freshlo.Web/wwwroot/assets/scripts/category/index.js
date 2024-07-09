$(function () {
    $('.CatManage').DataTable({
        "pageLength": 10,
        "columnDefs": [
            { "targets": [6, 7], "orderable": false }
        ],
        "lengthMenu": [10, 25, 50, 75, 100]
    });
    //Space Validation
    $(".SpaceValidation").on('keypress', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                return false;
            }
        }
    });
    var req = 'This Field is is required';
    $('#CatSequence').on('keypress', function (e) {
        var regex = new RegExp(/^[0-9-,]/);
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            $('#SpnCatSequence').css('display', 'none');
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });
    $('#CatName').on('keyup', function (e) {
        if ($(this).val().length == 0) {
            if (e.which == 32) {
                $('#SpnCatName').html(req).css('display', 'block');
            }
        }
        else if ($('#CatName').val() != null || $('#CatName').val() != '') {
            $('#SpnCatName').css('display', 'none');
        }
        else {
            $('#SpnCatName').html(req).css('display', 'block');
        }
    });
    $('.CatCreateClose').on('click', function () {
        $('#CatName').val('');
        $('#CatSequence').val('');
        $('#CatDescription').val('');
        $('#Visibility').val('1');
        $("#item-img-output").attr("src", '/img/no-image.png');

        $('#SpnCatName').css('display', 'none');
        $('#SpnCatSequence').css('display', 'none');
    });
    $('.closemaincategorylanguage').on('click', function () {
        $('#Spnmaincategorylanguagename').css('display', 'none');
    });

   // MainCategory Related Here...
    $('#SubmitMancategory').on('click', function () {
        var nErrorCount = 0;
        var req = 'This Field is is required';
        if ($('#CatName').val() === "" || $('#CatName').val() === null) {
            $('#SpnCatName').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if ($('#CatSequence').val() === "" || $('#CatSequence').val() === null) {
            $('#SpnCatSequence').html(req).css('display', 'block');
            nErrorCount += 1;
        }
        if (0 === nErrorCount) {
            $('#MainCategoryCreate').submit();
            swal({
                text: "Successfully Create MainCategory",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
    });
    $('.table-borderless').on('click', '.deleteMainCatiD', function () {
        var Id = $(this)[0].dataset.id;
        Swal.fire({
            title: 'Are you sure?',
            text: 'You want to Delete  MainCategory! Corresponding Category & Subcategory will be Deleted..!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/Category/DeleteMainCategory?maincategoryId=' + Id,
                    type: 'GET',
                    success: function (result) {
                        if (result == true) {
                            swal({
                                text: "Successfully Delete MainCategory",
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
            }
        });
    });
});

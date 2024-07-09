$(function () {
    $('#m_portlet_loader').css('display', 'block');
    CallPartialView();
});
$('.filter').on('click', function () {
    var tab = $(this)[0].id;
    var status = $.selector_cache("#" + tab + "_Status").prop('value');
    var zipcode = $.selector_cache("#" + tab + "_zipCode").prop('value');
    $.ajax({
        type: 'GET',
        url: '/Sale/_' + tab + 'Order',
        data: {
            'status': status,
            'zipcode': zipcode
        },
        success: function (data) {
            $('#' + tab + '_divId').html(data);
            $('#' + tab + 'Sales').dataTable();
        }
    });
});
$('#SalesExcelId').on('click', function () {
    var numberofError = 0;
    var activeTab = $.selector_cache('#activeTab').prop('value');
    var status = $.selector_cache("#" + activeTab + "_Status").prop('value');
    var zipcode = $.selector_cache("#" + activeTab + "_zipCode").prop('value');
    if (numberofError == 0) {
        window.location.href = '/Sale/ExportSalesPendingListtoExcel?status=' + status + '&zipcode=' + zipcode + '&activeTab=' + activeTab;
    }
});
$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var target = $(e.target).attr("href");// activated tab
    var name = $(e.target).attr("name");
    $.selector_cache('#activeTab').prop('value', name);
    var zipcode = $.selector_cache("#" + name + "_zipCode").prop('value');
   // $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Sale/_' + name + 'Order',
        type: 'Get',
        data: {
            'zipcode': zipcode
        },
        success: function (result) {
            if (result != "") {
                $('' + target + '_divId').html(result);
                $('#m_portlet_loader').css('display', 'none');
            }
            else {
                swal.fire("error", "Something went wrong", "error");
                $('' + target + '_divId').html(result);
                $('#m_portlet_loader').css('display', 'none');
            }
            $('' + target+'Sales').DataTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal('Error!', 'There was some error. Try again later.', 'error');
            $('' + target + '_divId').html(result);
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});
$('.clear').on('click', function () {
    var tab = $(this)[0].id;
    $("#" + tab + "_Status option[value='']").prop('selected', true);
    $("#" + tab + "_zipCode option[value='']").prop('selected', true);
    var status = $.selector_cache("#_Status").prop('value');
    var zipcode = $.selector_cache("#_zipCode").prop('value');
    $.ajax({
        type: 'GET',
        url: '/Sale/_' + tab + 'Order',
        data: {
            'status': status,
            'zipcode': zipcode
        },
        success: function (data) {
            $('#' + tab + '_divId').html(data);
            $('#' + tab + 'Sales').dataTable();
        }
    });
});
$('#pending_divId').on('change', '#selectallid', function (e) {
    var table = $(e.target).closest('table');
    $('td input:checkbox', table).prop('checked', this.checked);
});
var getChecked = function () {
    return $('#pendingSales').find('input[type="checkbox"]')
        .filter(':checked')
        .toArray()
        .map(function (x) {
            return $(x).attr('id');
        });
}
$(document).on('click', '#updateOrderId', function () {
    var id = getChecked();
    if (id.length > 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'you want to update order(s) as Confirmed!',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Update it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/Sale/UpdateOrder?ids=' + id,
                    type: 'GET',
                    success: function (result) {
                        if (result.IsSuccess == true) {
                            CallPartialView();
                        }
                    }
                });
            }
        });
    }
});
function CallPartialView() {
    $.ajax({
        type: 'GET',
        url: '/Sale/_pendingOrder/',
        success: function (data) {
            $('#pending_divId').html(data);
            $.selector_cache('#pendingSales').dataTable();
            $('#m_portlet_loader').css('display', 'none');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal('Error!', 'There was some error. Try again later.', 'error');
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}
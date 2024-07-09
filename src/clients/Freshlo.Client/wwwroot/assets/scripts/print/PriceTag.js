!(function ($) {
    var table;

    // When document is ready
    $(function () {

        // Initialize #item-list datatable
        table = $.selector_cache('#item-list').DataTable({
            'ajax': '/Print/ItemListTableData',
            'processing': true,
            'deferRender': true,
            'columnDefs': [
                {
                    'targets': [0],
                    'data': 'itemName',
                    'render': function (data, type, row, meta) {
                        return type === 'display' ?
                            '<a href="#" class="text-link">' + data + '</a><span class="font-small-3 display-block">' + row.itemId + '</span>' :
                            data;
                    }
                },
                {
                    'targets': [1],
                    'data': 'quantity',
                    'render': function (data, type, row, meta) {
                        return type === 'display' ?
                            data + ' ' + row.measurement :
                            data;
                    }
                },
                {
                    'targets': [2],
                    'data': 'price',
                    'render': function (data, type, row, meta) {
                        return type === 'display' ?
                            'Rs. ' + parseFloat(data).toFixed(2) :
                            data;
                    }
                },
                {
                    'targets': [3],
                    'orderable': false,
                    'className': 'select-checkbox',
                    'defaultContent': ''
                }
            ],
            'select': {
                style: 'multi',
                selector: 'td:last-child'
            }
        });
    });

    // select all checkbox 'change' event
    $.selector_cache('#select-all').on('change', function (e) {
        if (this.checked)
            table.rows({ page: 'current' }).select();
        else
            table.rows({ page: 'current' }).deselect();
    });

    $.selector_cache('#print-btn').on('click', function (e) {
        var html = '<!DOCTYPE html><html lang="en"><head><meta charset="utf-8" /><title>Freshlo</title><link href="/assets/print/bootstrap.min.css" rel="stylesheet" type="text/css" /><link href="/assets/main/main.css" rel="stylesheet" /></head><body><div class="container-fluid"><div class="row"><div class="col-xs-12"><div class="row" id="print-content">This is sample content.</div></div></div></div></body></html>';
        var printContent = '';
        $.each(table.rows('.selected').data(), function (key, value) {
            printContent += '<div class="col-md-6" style="border-top: 1px solid #ccc;border-right: 1px solid #ccc;border-left: 1px solid #ccc;padding: 20px;text-align:center;"><div class="form-group"><h1 style="font-size: 50px;">' + value.itemName + '</h1><h1 style="font-size: 50px;">' + value.quantity + ' ' + value.measurement + ' x Rs. ' + value.price + '</h1></div></div>';
        });
        var printWindow = window.open('', '', 'width=1280,height=720');
        printWindow.document.write(html);
        printWindow.document.getElementById('print-content').innerHTML = printContent;
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    });
})(jQuery);
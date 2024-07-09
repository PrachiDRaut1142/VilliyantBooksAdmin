$(function () {  
    $(document).ready(function () {
        $('#m_portlet_loader').css('display', 'block');
        var a = $.selector_cache("#date").prop('value');
        var b = $.selector_cache("#status").prop('value');
        if (a != 0) {
            $.ajax({
                type: 'GET',
                url: '/Customer/_customerlist/',
                data: {
                    'a': a,
                    'b': b,
                },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $.selector_cache('#divTblId').html(data.Data);
                        $.selector_cache('#m_table_1').dataTable();
                        $('#m_portlet_loader').css('display', 'none');
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                        $('#m_portlet_loader').css('display', 'none');
                    } else {                        
                        $('#m_portlet_loader').css('display', 'none');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {   
                }
            });
        }
        else {
            $('#m_portlet_loader').css('display', 'block');
            $.ajax({
                url: '/Customer/_customerlist/',
                type: 'POST',
                success: function (data) {
                    //$.selector_cache('#m_table_1').dataTable().fnDestroy();
                    $.selector_cache('#divTblId').html(data.Data);
                    $.selector_cache('#m_table_1').dataTable();
                    $('#m_portlet_loader').css('display', 'none');
                }
            });
        }
    });
});
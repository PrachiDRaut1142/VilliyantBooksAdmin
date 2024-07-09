$(document).ready(function () {
    $('#divCustomerMobileNo').css('display', 'none');
    $('#divReceiption').css('display', 'none');
    $('#TriggerIddivContat').css('display', 'none');
    $('#TriggerIddiv').css('display', 'none');
    $('#Customerlistrgrp').css('display', 'none');
});
var dualListbox = $("#Customernamelist").bootstrapDualListbox({
    nonSelectedListLabel: 'Select the customer Names for notification',
    selectedListLabel: 'Customers selected for notification',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: true,
    moveSelectedLabel: 'Move selected',
    nonSelectedFilter: ''
});
var dualListbox1 = $("#CustomerContactlist").bootstrapDualListbox({
    nonSelectedListLabel: 'Select the customer Contact for notification',
    selectedListLabel: 'Customers Contact  selected for notification',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: true,
    moveSelectedLabel: 'Move selected',
    nonSelectedFilter: ''
});
$(document).on('mouseover', "select", function () {
    $(this).parent().find(".filter").blur();
});
!(function ($) {
    if ($.selector_cache('#errorMessage1').prop('value')) {
        alert($.selector_cache('#errorMessage1').prop('value'));
    }
    $.selector_cache('#submit-Id').on('click', function () {
        $.selector_cache('#notification-FormId').submit();
        var a = $('#NotifyType').prop('value');
        if (a == 0) {
            swal({
                text: "Successfully Send Notification",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }
        if (a == 1) {
            swal({
                text: "Successfully Send SMS",
                type: "success",
                confirmButtonText: "OK"
            }).then(function (e) {
                location.reload();
            })
        }       
    });
    $('#NotifyType').on('change', function () {
        var notifyid = this.value;
        if (notifyid == 1) {
            $('#divtitle').css('display', 'none');
            $('#divReceiption').css('display', 'none');
            $('#divUploadImg').css('display', 'none');
            $('#divsender').css('display', 'block');
            $('#divCustomerMobileNo').css('display', 'none');
            $('#TriggerIddiv').css('display', 'none');
            $('#Customerlistrgrp').css('display', 'none');
            $('#CustomerCntlistgroup').css('display', 'none');
            $("#NotifyTypeOpt").val(-1);
        }
        else {
            $('#divtitle').css('display', 'block');
            $('#divReceiption').css('display', 'none');
            $('#divUploadImg').css('display', 'block');
            $('#divsender').css('display', 'none');
            $('#divCustomerMobileNo').css('display', 'none');
            $('#TriggerIddivContat').css('display', 'none');
            $('#Customerlistrgrp').css('display', 'none');
            $('#CustomerCntlistgroup').css('display', 'none');
            $("#NotifyTypeOpt").val(-1);
        }
    });
    $('#NotifyTypeOpt').on('change', function () {
        var a = $('#NotifyType').prop('value');
        var b = this.value;
        if (a == 0 ) {
            $('#Customerlistrgrp').css('display', 'block');
            $('#CustomerCntlistgroup').css('display', 'none');
        }
        if (a == 1) {
            $('#CustomerCntlistgroup').css('display', 'block');
            $('#Customerlistrgrp').css('display', 'none');
        }
        if (b != 0) {
            $('#Customerlistrgrp').css('display', 'none');
            $('#CustomerCntlistgroup').css('display', 'none');
            $.ajax({
                url: "/Notification/TriggerCustomerList?a=" + a + '&b=' + b,
                type: "Get",
                success: function (getCustomerlist) {
                    if (getCustomerlist.length > 0) {
                        if (a == 0) {
                            $("#Customernamelist").empty();
                            $.each(getCustomerlist, function (index, value) {
                                $("#Customernamelist").append('<option value="' + value.value + '">' + value.text + '</option>');
                            });
                            dualListbox.bootstrapDualListbox('refresh', true);
                            $('#Customerlistrgrp').css('display', 'block');
                            $('#CustomerCntlistgroup').css('display', 'none');
                        }
                        if (a == 1) {
                            $("#CustomerContactlist").empty();
                            $.each(getCustomerlist, function (index, value) {
                                $("#CustomerContactlist").append('<option value="' + value.value + '">' + value.text + '</option>');
                            });
                            dualListbox1.bootstrapDualListbox('refresh', true);
                            $('#CustomerCntlistgroup').css('display', 'block');
                            $('#Customerlistrgrp').css('display', 'none');
                        }
                    }
                }
            });
        }
    });    
})(jQuery);



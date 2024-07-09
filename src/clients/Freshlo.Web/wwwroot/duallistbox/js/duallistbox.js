
//var dualListbox = $('select[name="duallistbox_demo1[]"]').bootstrapDualListbox({
//    nonSelectedListLabel: 'Select the customer Names for notification',
//    selectedListLabel: 'Customers selected for notification',
//    preserveSelectionOnMove: 'moved',
//    moveOnSelect: true,
//    nonSelectedFilter: ''
//});





//$('#NotifyTypeOpt').on('change', function () {
//    var a = $('#NotifyType').prop('value');
//    var b = this.value;
//    $.ajax({
//        url: "/Notification/TriggerCustomerList?a=" + a + '&b=' + b,
//        type: "Get",
//        success: function (getCustomerlist) {
//            if (getCustomerlist.length > 0) {
//                $("#Customernamelist").empty();
//                $.each(getCustomerlist, function (index, value) {
//                    $('#Customernamelist').append('<option value="' + value.value + '">' + value.text + '</option>');
//                });
//                dualListbox.bootstrapDualListbox('refresh',true);

//            }
//        }
//    });
//});


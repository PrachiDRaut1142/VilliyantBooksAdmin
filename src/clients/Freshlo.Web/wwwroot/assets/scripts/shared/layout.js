!(function () {
    //var value_Select = $('#currency_value').children("option:selected").val();
    //var value_amount = $('.value_amount').val();
    //var aed = 18;
    //var totalAmount_rsaed = (value_amount + aed);
    //window.onbeforeunload = function () {
    //    localStorage.setItem(name, $('#currency_value').children("option:selected").val());
    //};
    //window.onload = function () {
    //    var name = localStorage.getItem(name);
    //    if (name !== null) $('#currency_value').val(name);
    //    alert(name);
    //};
    //$('.saleAmount_rsaed').append(totalAmount_rsaed);

    $(document).ready(function () {
        $('.currency').text("Rs");
    });

    $('#currency_value').on('change', function () {
        var currencyvalue = $(this).children("option:selected").val();
        if (currencyvalue == 1) {
            $('.currency').text("Rs");
        }
        else if (currencyvalue == 2) {
            $('.currency').text("AED");
        }
    });
})(jQuery);


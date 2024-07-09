!(function () {
    $('#amount1, #amount2').on('input', function () {
        var qty = parseFloat($.selector_cache('#amount1').val());
        var price = parseFloat($.selector_cache('#amount2').val());
        if ($.selector_cache('#amount2').val()) {
            $('#total').text((qty - price).toFixed(2));
        }
        else {
            $('#total').text(qty.toFixed(2));
        }
    });
})();
//function valueChanged() {
//    if ($('.transaction_value').is(":checked"))
//        $(".transaction").css("display", "none");
//    else
//        $(".transaction_Show").css("display", "block");
//}
$(".transaction_value").click(function () {
    if ($(this).is(":checked")) {
        $(".transaction").css("display", "block");
        $(".transaction_Show").css("display", "none");
        $('#total').css("display", "none");

    } else {
        $(".transaction").css("display", "none");
        $(".transaction_Show").css("display", "block");
        $('#total').css("display", "block");
    }
});
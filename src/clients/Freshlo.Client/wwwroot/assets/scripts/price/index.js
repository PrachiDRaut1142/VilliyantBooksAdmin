!(function ($) {
    // Scroll Top Script
    $(document).ready(function () {
        $(".showdiv").css("display", "none");
        var topOfOthDiv = $(".toptouchdiv").offset().top;
        $(window).scroll(function () {
            if ($(window).scrollTop() > topOfOthDiv) {
                $(".showdiv").css('display', 'block');
            }
            else {
                $(".showdiv").css('display', 'none');
            }
        });
    });
})(jQuery);
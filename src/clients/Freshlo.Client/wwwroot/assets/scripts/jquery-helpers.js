(function ($) {
    $.selector_cache = function (selector, reset) {
        if (!$.selector_cache[selector] || reset) {
            $.selector_cache[selector] = $(selector);
        }
        return $.selector_cache[selector];
    };

    $.delay = function (callback, ms) {
        var timer = 0;
        return function () {
            var context = this, args = arguments;
            clearTimeout(timer);
            timer = setTimeout(function () {
                callback.apply(context, args);
            }, ms || 0);
        };
    };
})(jQuery);
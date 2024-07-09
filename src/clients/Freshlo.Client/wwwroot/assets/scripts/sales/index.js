!(function ($) {
    // Scroll Top Script
    $(document).ready(function () {
        $("#showdiv").css("display", "none");
        var topOfOthDiv = $("#toptouchdiv").offset().top;
        $(window).scroll(function () {
            if ($(window).scrollTop() > topOfOthDiv) {
                $("#showdiv").css('display', 'block');
            }
            else {
                $("#showdiv").css('display', 'none');
            }
        });
    });


    // Search item name item tab -- Abhijit Gadhave create code 
    $.selector_cache('#SearchItem').on('keyup', $.delay(function () {
        var ItemName = $.selector_cache("#SearchItem").prop('value');
        var CatogeryName = $(".tab-pane.active").attr("id");
        //alert(CatogeryName)
        if (CatogeryName == 'allitems') {
            $.ajax({
                type: 'GET',
                url: '/Home/_ItemList/',
                data: {
                    'ItemName': ItemName
                },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $.selector_cache('#ItemDetails').html(data.Data);
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                    } else {
                        swal('Error!', 'There was some error. Try again later.', 'error');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        } else {
            $.ajax({
                type: 'GET',
                url: '/Home/_ItemList1/',
                data: {
                    'ItemName': ItemName,
                    'CatogeryName': CatogeryName
                },
                success: function (data) {
                    if (data !== null && data.IsSuccess) {
                        $.selector_cache('#r_' + CatogeryName).html(data.Data);
                    } else if (data !== null) {
                        swal('Error!', data.ReturnMessage, 'error');
                    } else {
                        swal('Error!', 'There was some error. Try again later.', 'error');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    swal('Error!', 'There was some error. Try again later.', 'error');
                }
            });
        }

    }, 500));
    // End Search item name code 


    //$('.nav-tabs a').on('shown.bs.tab', function (e) {
    //    //var tab = $(this);
    //    //var t = $(tab.attr('href'));
    //    //t.addClass('active');
    //   var ItemName =  $.selector_cache('#SearchItem').val('');        
    //});

})(jQuery);
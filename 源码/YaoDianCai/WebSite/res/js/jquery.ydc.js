(function ($) {
    $.fn.ydc_popup = function (opts) {
        var defaults = {
            masklay: 'div.mask',
            background: '000',
            opacity: 0.4,
            shopBtn: '#ShopBtn',
            shopUrl: '#'
        };
        var options = $.extend(defaults, opts);

        var masklay = $(options.masklay);
        var modal = $(this);
        var winWidth = $(window).width();
        var winHeight = $(window).height();
        var popupWidth = modal.width();
        var popupHeight = modal.height();

        masklay.css({ opacity: options.opacity, backgroundColor: '#' + options.background });
        masklay.css({ position: 'absolute', width: '100%', height: '100%', top: '0', left: '0', 'z-index': '5' });
        masklay.fadeIn(200);
        modal.css({
            "position": "absloute"
        }).animate({
            left: winWidth / 2 - popupWidth / 2,
            top: winHeight / 2 - popupHeight / 2,
            opacity: "show"
        }, "slow");

        modal.find(options.shopBtn).bind('click', function () {
            window.location.href = options.shopUrl;
            return;
        });
    };

    $.fn.ydc_pophide = function (opts) {
        var defaults = {
            masklay: 'div.mask'
        };
        var options = $.extend(defaults, opts);

        var masklay = $(options.masklay);
        var modal = $(this);

        masklay.fadeOut(200);
        modal.animate({
            left: 0,
            top: 0,
            opacity: "hide"
        }, "slow");
    };
})(jQuery);
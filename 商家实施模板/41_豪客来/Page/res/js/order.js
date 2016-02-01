$(document).ready(function () {
    var HaverOrder = JSON.parse(window.localStorage.getItem('HaverOrder')); //菜品的数据
    if (HaverOrder != null && HaverOrder.length > 0) {
        $("#OrderList").html("");
        var orderStr = "";
        for (var i = 0; i < HaverOrder.length; i++) {
            orderStr += '<div class="orderitem">' +
                            '<div class="orderitem_top">' +
                                '<div class="orderitem_top_left">' +
                                    '<div class="orderitem_top_left_ordernum">下单号:' + HaverOrder[i].bid + '</div>' +
                                    '<div class="orderitem_top_left_orderprice">总价格:' + HaverOrder[i].totalPrice + '元</div>' +
                                    '<div class="orderitem_top_left_orderdate">' + HaverOrder[i].createDate + '</div>' +
                                '</div>' +
                                '<div class="orderitem_top_right">' +
                                    '<div class="orderitem_top_right_button"></div>' +
                                '</div>' +
                            '</div>' +
                            '<div class="orderitem_content">';
            for (var j = 0; j < HaverOrder[i].orders.length; j++) {
                orderStr += '<div class="orderitem_content_item">' +
                                '<div class="orderitem_content_itemname">' + HaverOrder[i].orders[j].dishName + '</div>' +
                                '<div class="orderitem_content_itemcount">' + HaverOrder[i].orders[j].dishCount + '份</div>' +
                                '<div class="orderitem_content_itemprice">' + HaverOrder[i].orders[j].dishPrice + '元</div>' +
                            '</div>';
            }
            orderStr +='<div class="orderitem_content_itembottom">' +
                            '<div class="orderitem_content_itembottomfont">合计：</div>' +
                            '<div class="orderitem_content_itembottomcount">' + HaverOrder[i].totalCount + '份</div>' +
                            '<div class="orderitem_content_itembottomprice">' + HaverOrder[i].totalPrice + '元</div>' +
                        '</div>' +
                    '</div>' +
                '</div>';
        }
        $("#OrderList").append(orderStr);
    } else {
        $("#OrderList").html("");
    }
    event_init();
    $('#OrderList').css('max-height', $(window).height() - 130);
});
var event_init = function () {
    $("#header_back").click(function () {
        location.href = "main.html";
    });
    $(".orderitem_top").click(function () {
        if ($(this).parent().find(".orderitem_content").css("display") == "none") {
            $(this).parent().find(".orderitem_content").css("display", "block");
            $(this).find(".orderitem_top_right_button").css("background-image", "url(res/images/orderitem_up.png)");
        } else {
            $(this).parent().find(".orderitem_content").css("display", "none");
            $(this).find(".orderitem_top_right_button").css("background-image", "url(res/images/orderitem_down.png)");
        }
    });
}
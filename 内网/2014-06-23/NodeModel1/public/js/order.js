
$("#YDC_OrderPage").on("pagebeforeshow ", function (event) {
    var HaverOrder = JSON.parse($.cookie('HaverOrder'));
    if (HaverOrder != null && HaverOrder.length > 0) {
        $("#OrderList").html("");
        for (var i = 0,hl = HaverOrder.length; i < hl; i++) {
            var oederlist = $("<div data-role='collapsible' data-iconpos='right' style='margin: 0.5em 0;border-radius: 0.3125em;'></div>"),
                ordermenulist = $("<ul id='OrderMenuList' data-role='listview' data-icon='false'></ul>"),
                ordertitle = new StringBuffer();

            ordertitle.append("<h4>");
            ordertitle.append("<div>");
            ordertitle.append("<span style='width:100%;float: left;font-weight: normal;'>下单号：" + HaverOrder[i].bid + "</span>");
            ordertitle.append("</div>");
            ordertitle.append("<div>");
            ordertitle.append("<span style='float: left; width: 120px;font-weight: normal;'>总价格：" + HaverOrder[i].totalPrice + "元</span>");
            ordertitle.append("<span style='margin-right: 10px;float: right;font-weight: normal;'>" + HaverOrder[i].createDate + "</span>");
            ordertitle.append("</div>");
            ordertitle.append("</h4>");

            for (var j = 0,hol=HaverOrder[i].orders.length; j < hol; j++) {
                var menulist = new StringBuffer();
                menulist.append("<li>");
                menulist.append("<a href=''#' style='background-color:#FFFFFF;font-weight: normal;'>");
                menulist.append("<span class='name'>" + HaverOrder[i].orders[j].dishName + "</span>");
                menulist.append("<span class='count'>" + HaverOrder[i].orders[j].dishCount + "份</span>");
                menulist.append("<span class='price'>" + HaverOrder[i].orders[j].dishPrice + "元</span>");
                menulist.append("</a>");
                menulist.append("</li>");
                ordermenulist.append(menulist.toString());
                if ((j + 1) == HaverOrder[i].orders.length) {
                    var menulisttotal = new StringBuffer();
                    menulisttotal.append("<li>");
                    menulisttotal.append("<a href=''#' style='font-weight: normal;'>");
                    menulisttotal.append("<span class='name'>合计:</span>");
                    menulisttotal.append("<span class='count'>" + HaverOrder[i].totalCount + "份</span>");
                    menulisttotal.append("<span class='price'>" + HaverOrder[i].totalPrice + "元</span>");
                    menulisttotal.append("</a>");
                    menulisttotal.append("</li>");
                    ordermenulist.append(menulisttotal.toString());
                }
            }
            oederlist.append(ordertitle.toString());
            oederlist.append(ordermenulist);
            $("#OrderList").append(oederlist);
        }
        $("#OrderList").trigger("create");
        $("#OrderList").collapsibleset("refresh");
        $("#OrderMenuList").listview("refresh");
    } else {
        $("#OrderList").html("");
    }
});
$("#YDC_OrderPage").ready(function () {
    $("#Order_ShopName").text(name);
});

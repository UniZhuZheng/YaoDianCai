$(document).on("pagebeforehide", "#YDC_TablePage", function () {
    $("#TableName").text(TableNum);
});
$(document).on("pagebeforehide", "#YDC_CartPage", function () {
    $("#CartCount").text(ToTalorderNum);
});
$(document).on("pagebeforehide", "#YDC_DishinfoPage", function () {
    $("#CartCount").text(ToTalorderNum);
});
$("#YDC_MainPage").on("pagebeforeshow", function () {
    document.title = name + ",欢迎您!";
    $("#DishList").empty();
    for (var key in DishInfoMap) {
        var dish = DishInfoMap[key],
            list = new StringBuffer();
        list.append("<li>");
        list.append("<div>");
        list.append("<table class='info'>");
        list.append("<tr>");
        list.append("<td class='left' valign='bottom'>");
        list.append("<a class='dish-link' dishname='" + dish.name + "' href='dishinfo.html'><img src='../ShopInfo/menuimg/" + dish.name + ".jpg' /></a>");
        list.append("</td>");
        list.append("<td class='middle' valign='top'>");
        list.append("<div class='dish-name'>" + dish.name + "</div>");
        list.append("<div class='dish-kind'>" + dish.kind + "</div>");
        list.append("<div class='dish-type'>" + dish.type + "</div>");
        list.append("<div class='dish-count'>" + dish.count + "</div>");
        list.append("</td>");
        list.append("<td class='right' valign='top'>");
        list.append("<div class='dish-price'>");
        list.append("<div class='dish-priceyuan'>元</div>");
        list.append("<div class='dish-pricenum'>" + dish.price + "</div>");
        list.append("</div>");
        if (dish.count <= 0) {
            list.append("<div>");
            list.append("<div class='show'><a class='dishselect-btn' href='#' dishname='" + dish.name + "' data-role='button' data-corners='false' data-inline='true' data-mini='true'><img src='./img/select.png' alt='' /></a></div>");
            list.append("<div class='hide'><a class='dishselected-btn' href='#' dishname='" + dish.name + "' data-role='button' data-corners='false' data-inline='true' data-mini='true'><img src='./img/selected.png' alt='' /></a></div>");
            list.append("</div>");
        } else {
            list.append("<div>");
            list.append("<div class='hide'><a class='dishselect-btn' href='#' dishname='" + dish.name + "' data-role='button' data-corners='false' data-inline='true' data-mini='true'><img src='./img/select.png' alt='' /></a></div>");
            list.append("<div class='show'><a class='dishselected-btn' href='#' dishname='" + dish.name + "' data-role='button' data-corners='false' data-inline='true' data-mini='true'><img src='./img/selected.png' alt='' /></a></div>");
            list.append("</div>");
        }
        list.append("</td></tr></table></div></li>");
        $("#DishList").append(list.toString());
    }

    $(".dishselect-btn").click(function () {
        if (TableNum == "餐桌号") {
            $("#TableSelPopup").popup("open");
            return;
        }
        AddCartDishOne($(this).attr("dishname"));
        $(this).parent().attr("class", "hide");
        $(this).parent().siblings().attr("class", "show");
    });

    $(".dishselected-btn").click(function () {
        if (TableNum == "餐桌号") {
            $("#TableSelPopup").popup("open");
            return;
        }
        DeleteCartDish($(this).attr("dishname"));
        $(this).parent().attr("class", "hide");
        $(this).parent().siblings().attr("class", "show");
    });

    $(".dish-link").click(function () {
        if (TableNum == "餐桌号") {
            $("#TableSelPopup").popup("open");
            return false;
        } else {
            SelDishName = $(this).attr("dishname");
        }
    });
});
$("#YDC_MainPage").ready(function () {
    $("#Main_ShopName").text(name);
    $("#CartCount").text(ToTalorderNum);
    $("#TableName").text(TableNum);
    if (DishTypes.length > 0) {
        $("#TypeSel").empty();
        $("#TypeSel").append('<option value="1">全部</option>');
        for (var i = 0,dtl = DishTypes.length;i < dtl; i++) {
            $("#TypeSel").append('<option value="' + (i + 2) + '">' + DishTypes[i].type + '</option>');
        }
    }
    $("#TableSelPopup").on({
        popupafterclose: function () {
            $.mobile.changePage("/table");
        }
    });
    if (!DisableTableMode) {
        $("#TableBtn").click(function () {
            $.mobile.changePage("/table");
            return false;
        });
    }
    $("#TypeSel").change(function () {
        var type = $("#TypeSel option:selected").text();
        $(".dish-type").each(function () {
            if (type == "全部") {
                $(this).closest("li").css("display", "block");
            } else{
                type == $(this).text() && $(this).closest("li").css("display", "block");
                type != $(this).text() && $(this).closest("li").css("display", "none");
            }
        });
    });
    $("#OptionSel").change(function () {
        if (TableNum == "餐桌号") {
            $("#OptionSel-listbox-popup").bind({
                popupafterclose: function () {
                    $("#TableSelPopup").popup("open");
                    $(this).unbind("popupafterclose");
                    $('#OptionSel').get(0).selectedIndex = 0;
                }
            });
            return;
        }
        var sel = $("#OptionSel option:selected").text();
        sel == "团购券号" && $.mobile.changePage("/tuan");
        sel == "已点菜单" && $.mobile.changePage("/order");
        sel == "呼叫服务" && $.mobile.changePage("/call");
        $('#OptionSel').get(0).selectedIndex = 0;
    });

    setInterval(function () {
        if (TableNum != '餐桌号') {
            RefreshCartDishCount();
        }
    }, 5000);

});
function StringBuffer(){
    this.__strings__ = new Array();
}
StringBuffer.prototype.append = function(str){
    this.__strings__.push(str);
}
StringBuffer.prototype.toString = function(){
    return this.__strings__.join("");
}
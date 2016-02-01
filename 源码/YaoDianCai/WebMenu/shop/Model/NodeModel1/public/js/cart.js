Date.prototype.Format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for(var k=0,ol=o.length;k<ol;k++){
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
var ToTalorderNumTem = 0;
var TotalorderPriceTem = 0;
var shopCarDataTem = null;
var NameType = {};
var loadLayer;
var loadTimeOut;
var isInNet = true;
function loadScript() {
    loadLayer = layer.load("加载中...");
    submitOrder = function () {
        //订单信息存入数据库
        //获取购物车最新信息
        //RefreshCartDishCount();
        var shopCarDataTemStr="";
        if (shopCarDataTem == null) {
            clearTimeout(loadTimeOut);
            layer.alert("购物车为空，不可下单。", 8);
            return;
        }else{
            var len=shopCarDataTem.length;
            for (var i = 0; i < len; i++) {
                if(i<len-1){
                    shopCarDataTemStr+=shopCarDataTem[i].dishNumber+'.'+
                        shopCarDataTem[i].dishName+'.'+
                        shopCarDataTem[i].dishPrice+'.'+
                        shopCarDataTem[i].dishCount+'.';
                }else{
                    shopCarDataTemStr+=shopCarDataTem[i].dishNumber+'.'+
                        shopCarDataTem[i].dishName+'.'+shopCarDataTem[i].dishPrice+'.'+
                        shopCarDataTem[i].dishCount;
                }
            }
        }
        var ShopCar = {
            'bid': BID,
            'totalCount': ToTalorderNumTem,
            'totalPrice': TotalorderPriceTem,
            'tableName': TableNum,
            'memo': '',
            'createDate': '2014-06-10',
            'sid': shopCarDataTemStr
        };
        var ShopCarOrder = {
            'bid': BID,
            'totalCount': ToTalorderNumTem,
            'totalPrice': TotalorderPriceTem,
            'tableName': TableNum,
            'memo': '',
            'createDate': new Date().Format("hh:mm:ss"),
            'orders': shopCarDataTem
        };
        $.getJSON(host+"/jsonpAction/BillAction.ashx?SID="+SID+"&BillJson="+JSON.stringify(ShopCar)+"&option=OP_CreateBill&jsoncallback=?",
            function(data){
                if (!data.ok) {
                    $("#shopcarorderpopup").popup("open");
                    $('.Cart_Confirm_Bac').css('display', 'none');
                    if (data.BID) {
                        BID = data.BID;
                    }
                    return false;
                }
                layer.close(loadLayer);
                alert("下单成功。");

                for (var key in DishInfoMap) {
                    var dish = DishInfoMap[key];
                    dish.count = 0;
                }

                //将以下的单，保存到
                if ($.cookie('HaverOrder') == null || $.cookie('HaverOrder') == "") {
                    var HaverOrder = [ShopCarOrder];
                    var cookie = JSON.stringify(HaverOrder);
                    $.cookie('HaverOrder', cookie, { expires: 1, path: '/' });
                } else {
                    var HaverOrder = JSON.parse($.cookie('HaverOrder'));
                    HaverOrder.push(ShopCarOrder);
                    var cookie = JSON.stringify(HaverOrder);
                    $.cookie('HaverOrder', cookie, { expires: 1, path: '/' });
                }
                ToTalorderNum = 0;
                TotalorderPrice = 0;
                shopCarData = [];
                if (data.BID) {
                    BID = data.BID;
                }
                $('.Cart_Confirm_Bac').css('display', 'none');
                $.mobile.changePage("/main");
            });

    }();

}
$("#YDC_CartPage").on("pagebeforeshow", function () {
    GetCartDishList();
    setInterval(function () {
        if (TableNum != '餐桌号') {
            GetCartDishList();
        }
    }, 5000);
});
function GetCartDishList(){
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+
            "&option=OP_GetCartDishByTable&jsoncallback=?",
        function(data){
            ToTalorderNum = 0;
            TotalorderPrice = 0;
            if (data != null && data != "[]") {
                var dishes = data;
                for (var i = 0,dl=dishes.length; i < dl; i++) {
                    DishInfoMap[dishes[i].dishName].count = dishes[i].dishCount;
                    ToTalorderNum += dishes[i].dishCount;
                    TotalorderPrice += dishes[i].dishCount * dishes[i].dishPrice;
                }
                $("#CartList").empty();
                for (var i = 0,dl=dishes.length;i < dl; i++) {
                    var menuli = new StringBuffer();
                    menuli.append("<li data-icon='false'>");
                    menuli.append("<div dishname='" + dishes[i].dishName + "'>");
                    menuli.append("<div class='delete'></div>");
                    menuli.append("<div class='name'><span>" + dishes[i].dishName + "</span></div>");
                    menuli.append("<div class='minus'></div>");
                    menuli.append("<div class='count'><span>" + dishes[i].dishCount + "</span></div>");
                    menuli.append("<div class='plus'></div>");
                    menuli.append("<div class='price'><span class='unit'>元</span><span class='number'>" + dishes[i].dishPrice + "</span>");
                    menuli.append("</div>");
                    menuli.append("</div>");
                    menuli.append("</li>");
                    $("#CartList").append(menuli.toString()).trigger("create");
                }
                $("#CartList").listview("refresh");
                $("#CartTableName").text(TableNum);
                $("#CartDishTotalCount").text(ToTalorderNum);
                $("#CartDishTotalPrice").text(TotalorderPrice);
                $("#YDC_CartPage .plus").click(function () {
                    var dishName = $(this).parent().attr("dishname"),
                        curcount = $(this).parent().find(".count span").text(),
                        curprice = $(this).parent().find('span.number').text(),
                        n = parseInt(curcount) + 1;
                    AddCartDishOne(dishName);
                    TotalorderPrice += parseFloat(curprice);
                    $(this).parent().find(".count span").text(n);
                    $("#CartDishTotalCount").text(ToTalorderNum);
                    $("#CartDishTotalPrice").text(TotalorderPrice);
                });

                $("#YDC_CartPage .minus").click(function () {
                    var dishName = $(this).parent().attr("dishname"),
                        curcount = $(this).parent().find(".count span").text(),
                        curprice = $(this).parent().find('span.number').text(),
                        n = parseInt(curcount) - 1;
                    if (n == 0) {
                        return true;
                    }
                    DelCartDishOne(dishName);
                    TotalorderPrice -= parseFloat(curprice);
                    $(this).parent().find(".count span").text(n);
                    $("#CartDishTotalCount").text(ToTalorderNum);
                    $("#CartDishTotalPrice").text(TotalorderPrice);
                });

                $("#YDC_CartPage .delete").click(function () {
                    var dishName = $(this).parent().attr("dishname"),
                        curcount = $(this).parent().find(".count span").text(),
                        curprice = $(this).parent().find('span.number').text();
                    DeleteCartDish(dishName);
                    TotalorderPrice -= parseFloat(curprice) * parseInt(curcount);
                    $("#CartDishTotalCount").text(ToTalorderNum);
                    $("#CartDishTotalPrice").text(TotalorderPrice);
                    $(this).closest("li").remove();
                    $("#CartList").listview("refresh");
                });
            } else {
                $("#CartList").empty();
                $("#CartDishTotalCount").text(0);
                $("#CartDishTotalPrice").text(0);
            }
        });
}

function Cart_Confirm() {
    $('.Cart_Confirm_content').empty();
    var cartStr = '';
    for (var i = 0; i < shopCarData.length; i++) {
        cartStr += '<div class="Cart_Confirm_item">' +
                        '<div class="Cart_Confirm_item_name">' + shopCarData[i].dishName + '</div>' +
                        '<div class="Cart_Confirm_item_num">X' + shopCarData[i].dishCount + '</div>' +
                        '<div class="Cart_Confirm_item_price">' + shopCarData[i].dishPrice * shopCarData[i].dishCount + '元</div>' +
                    '</div>';
    }
    $('.Cart_Confirm_content').append(cartStr);
    $('.Cart_Confirm_bottom_table').text(TableNum);
    $('.Cart_Confirm_bottom_num').text('X' + ToTalorderNum);
    $('.Cart_Confirm_bottom_price').text(TotalorderPrice + '元');
    $('.Cart_Confirm_Bac').css('display', 'block');

    shopCarDataTem = null;
    shopCarDataTem = shopCarData;
    ToTalorderNumTem = 0;
    TotalorderPriceTem = 0;
    ToTalorderNumTem = ToTalorderNum;
    TotalorderPriceTem = TotalorderPrice;
}

$("#YDC_CartPage").ready(function () {
    $('.Cart_Confirm_content').css('height', $(window).height() - 168);
    $('.Cart_Confirm_Bac').css('height', $(window).height());
    $('.Cart_Confirm_bottom').css('top', $(window).height() - 100);
    $('.Cart_Confirm_bottom_bottom').css('top', $(window).height() - 70);

    $("#Cart_ShopName").text(name);
    $("#CartSubmit").click(function () {
        RefreshCartDishCount();
        if (shopCarData == null) {
            clearTimeout(loadTimeOut);
            layer.alert("购物车为空，不可下单。", 8);
            return;
        }
        Cart_Confirm();
    });
    $(".Cart_Confirm_bottom_cancle").click(function () {
        $('.Cart_Confirm_Bac').css('display', 'none');
    });
    $(".Cart_Confirm_bottom_submit").click(function () {
        if (ToTalorderNum == 0) {
            $("#CartEmpty").popup("open");
            return;
        }
        loadScript();
    });
});




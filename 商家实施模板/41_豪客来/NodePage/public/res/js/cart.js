var DishInfoMap = JSON.parse(window.localStorage.getItem('Uni_DishInfoMap')); //菜品的数据
var Uni_TableNum = JSON.parse(window.localStorage.getItem('Uni_TableNum'));
var TableNum = Uni_TableNum.TableNum; //桌号
var DisableTableMode = Uni_TableNum.DisableTableMode; //桌号是否可选，false可选，true不可选
var CustomerNum = Uni_TableNum.CustomerNum; //用餐人数
var BID = Uni_TableNum.BID; //用餐单号
var DishTypes = JSON.parse(window.localStorage.getItem('Uni_DishTypes')); //菜品的分类数据
var Uni_TotalOrder = JSON.parse(window.localStorage.getItem('Uni_TotalOrder'));
var ToTalorderNum = Uni_TotalOrder.ToTalorderNum;
var TotalorderPrice = Uni_TotalOrder.TotalorderPrice;

var ToTalorderNumTem = 0;
var TotalorderPriceTem = 0;
var shopCarData = null;
var shopCarDataTem=null;
var NameType = {};
var loadLayer;
var loadTimeOut;
var isInNet = true;
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

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
$(document).ready(function () {
    //初始化购物车
    RefreshCartDishCount();
    index_skin_init();
    event_init();
    setInterval(function () {
        RefreshCartDishCount();
    }, 5000);
});

var index_skin_init = function () {
    $('#shoppingCart_content').css('max-height', $(window).height() - 125);
    $('.Cart_Confirm_content').css('height', $(window).height() - 155);
    $('.Cart_Confirm_Bac').css('height', $(window).height());
    $('.Cart_Confirm_bottom').css('top', $(window).height() - 100);
    $('.Cart_Confirm_bottom_bottom').css('top', $(window).height() - 70);
}

var event_init = function () {
    $("#header_back").click(function () {
        location.href = "/main";
    });
    $("#footer_submit").click(function () {
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
        loadLayer = layer.load("加载中...");
        loadScript();

    });

}
function AddCartDishOne(dishName) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+
            "&DishName="+dishName+"&option=OP_CartAddOne&jsoncallback=?",
        function(data){
            if (!data.ok) {
                layer.alert('添加失败',8);
                return false;
            }
        });

}

function DelCartDishOne(dishName) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+
            "&DishName="+dishName+"&option=OP_CartRemoveOne&jsoncallback=?",
        function(data){
            if (!data.ok) {
                layer.alert('减少失败',8);
                return false;
            }
        });

}

function DeleteCartDish(dishName) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+
            "&DishName="+dishName+"&option=OP_RemoveCart&jsoncallback=?",
        function(data){
            if (!data.ok) {
                layer.alert('删除失败',8);
                return false;
            }
        });

}

function RefreshCartDishCount() {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+
            "&option=OP_GetCartDishByTable&jsoncallback=?",
        function(data){
            ToTalorderNum = 0;
            TotalorderPrice = 0;
            if (data != null && data != "") {
                shopCarData = data;
                NameType = {};
                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                        NameType[nameType] = keyType;

                    }
                }
                for (var i = 0; i < data.length; i++) {
                    DishInfoMap[NameType[data[i].dishName]][data[i].dishName].count = data[i].dishCount;
                    ToTalorderNum += data[i].dishCount;
                    TotalorderPrice += data[i].dishCount * data[i].dishPrice;
                }
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
            } else {
                shopCarData = null;

                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                    }
                }
                ToTalorderNum = 0;
                TotalorderPrice = 0;
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":0,"TotalorderPrice":0}');
            }
            Cart_List();
        });

}
function loadScript() {

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
                    layer.alert('下单失败，请检查后重新下单。');
                    $('.Cart_Confirm_Bac').css('display', 'none');
                    if (data.BID) {
                        BID = data.BID;
                        window.localStorage.setItem('Uni_TableNum', '{"DisableTableMode":' + DisableTableMode + ',"TableNum":"' + TableNum + '","CustomerNum":0,"BID":"' + data.BID + '"}');
                    }
                    return false;
                }
                layer.close(loadLayer);
                alert('下单成功。');
                $('.Cart_Confirm_Bac').css('display', 'none');
                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                    }
                }
                //将以下的单，保存到
                if (window.localStorage.getItem('HaverOrder') == null || window.localStorage.getItem('HaverOrder') == "") {
                    var HaverOrder = [ShopCarOrder];
                    var cookie = JSON.stringify(HaverOrder);
                    window.localStorage.setItem('HaverOrder', cookie);
                } else {
                    var HaverOrder = JSON.parse(window.localStorage.getItem('HaverOrder'));
                    HaverOrder.push(ShopCarOrder);
                    var cookie = JSON.stringify(HaverOrder);
                    window.localStorage.setItem('HaverOrder', cookie);
                }
                ToTalorderNum = 0;
                TotalorderPrice = 0;
                shopCarData = [];
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":0,"TotalorderPrice":0}');
                if (data.BID) {
                    BID = data.BID;
                    window.localStorage.setItem('Uni_TableNum', '{"DisableTableMode":' + DisableTableMode + ',"TableNum":"' + TableNum + '","CustomerNum":0,"BID":"' + data.BID + '"}');
                }
                location.href = "/main";
            });


    }();

}
function Cart_List() {
    $('.menu_list').empty();
    var cartStr = '';
    for (var i = 0; i < shopCarData.length; i++) {
        cartStr += '<li>' +
                    '<div class="menu_delete"></div>' +
                    '<div class="menu_name">' + shopCarData[i].dishName + '</div>' +
                    '<div class="menu_name_en"></div>' +
                    '<div style="clear: both;margin-left: 45px;padding-top: 0px;">' +
                        '<div class="menu_minus" ' + (shopCarData[i].dishCount > 1 ? 'style="background-image: url(res/images/minus_click.png);"' : '') + '></div>' +
                        '<div class="menu_count">' + shopCarData[i].dishCount + '</div>' +
                        '<div class="menu_plus"></div>' +
                        '<div class="menu_price">' + shopCarData[i].dishPrice + '元</div>' +
                    '</div>' +
                '</li>'
    }
    $('.menu_list').append(cartStr);
    $('#table_num').text(TableNum);
    $('#total_count').text('X' + ToTalorderNum);
    $('#total_price').text(TotalorderPrice + '元');

    $(".menu_delete").click(function () {
        var dishName = $(this).parent().find('.menu_name').text();
        var dishPrice = $(this).parent().find('.menu_price').text();
        var dishCount = $(this).parent().find('.menu_count').text();

        DeleteCartDish(dishName);

        ToTalorderNum -= parseInt(dishCount);
        TotalorderPrice -= parseInt(dishCount) * parseFloat(dishPrice);

        $('#total_count').text('X' + ToTalorderNum);
        $('#total_price').text(TotalorderPrice + '元');
        $(this).parent().remove();

        DishInfoMap[NameType[dishName]][dishName].count = 0;
        window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
        window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
    });
    $(".menu_minus").click(function () {
        var dishName = $(this).parent().parent().find('.menu_name').text();
        var dishPrice = $(this).parent().find('.menu_price').text();
        var dishCount = $(this).parent().find('.menu_count').text();
        if (parseInt(dishCount) > 1) {
            DelCartDishOne(dishName);

            ToTalorderNum -= 1;
            TotalorderPrice -= parseFloat(dishPrice);

            $('#total_count').text('X' + ToTalorderNum);
            $('#total_price').text(TotalorderPrice + '元');
            $(this).parent().find('.menu_count').text(parseInt(dishCount) - 1);

            DishInfoMap[NameType[dishName]][dishName].count = parseInt(dishCount) - 1;
            window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
            window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
            if (parseInt(dishCount) - 1 == 1) {
                $(this).css('background-image', 'url("res/images/minus_normal.png")');
            }
        }
    });
    $(".menu_plus").click(function () {
        var dishName = $(this).parent().parent().find('.menu_name').text();
        var dishPrice = $(this).parent().find('.menu_price').text();
        var dishCount = $(this).parent().find('.menu_count').text();

        AddCartDishOne(dishName);

        ToTalorderNum += 1;
        TotalorderPrice += parseFloat(dishPrice);

        $('#total_count').text('X' + ToTalorderNum);
        $('#total_price').text(TotalorderPrice + '元');
        $(this).parent().find('.menu_count').text(parseInt(dishCount) + 1);
        
        DishInfoMap[NameType[dishName]][dishName].count = parseInt(dishCount) + 1;
        window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
        window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
        $(this).parent().find('.menu_minus').css('background-image', 'url("res/images/minus_click.png")');
    });

}
function Cart_Confirm() {
    $('.Cart_Confirm_content').empty();
    var cartStr = '';
    for (var i = 0; i < shopCarData.length; i++) {
        cartStr += '<div class="Cart_Confirm_item">'+
                        '<div class="Cart_Confirm_item_name">' + shopCarData[i].dishName + '</div>' +
                        '<div class="Cart_Confirm_item_num">X' + shopCarData[i].dishCount + '</div>' +
                        '<div class="Cart_Confirm_item_price">' + shopCarData[i].dishPrice * shopCarData[i].dishCount + '元</div>' +
                        '<div class="Cart_Confirm_item_name_en"></div>' +
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
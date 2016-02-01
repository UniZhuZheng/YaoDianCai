//json字符串转换为json对象
function strToJson(str) {
    var json = (new Function("return " + str))();
    return json;
}

if ($.cookie('userInfo')==null) {
    window.location.href = "/Login.html";
}

var userInfo = strToJson($.cookie('userInfo'));
//回到商家列表
function right_tab_shop() {
    window.location.href = "/ShopList.html";
}
//回到商家下单列表
function right_tab_menu() {
    window.location.href = "/BillShopList.html";
}
function right_tab_auth() {
    window.location.href = "/WifiGWShopList.html";
}

function right_tab_tuan() {
    window.location.href = "/TuanShopList.html";
}
function right_tab_dish() {
    window.location.href = "/ShopsDishCountList.html";
}
function right_tab_chat() {
    window.location.href = "/WebChatList.html";
}
function right_tab_tab() {
    window.location.href = "/ShopTabList.html";
}
function user_exit() {
    if (confirm("您确定退出程序吗？")) {
        $.cookie("userInfo", '', { expires: -1, path: '/' });
        $.cookie("shopSID", '', { expires: -1, path: '/' });
        window.location.href = "/Login.html";
    }
}

var request =
{
    QueryString: function (val) {
        var uri = window.location.search;
        var re = new RegExp("" + val + "=([^&?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
    }
}

function DateTimeConvertYMD(datetime) {
    if (typeof datetime != 'string') {
        return "";
    }

    var ss = (datetime.split(' ')[0]).split('/');
    if (!ss[0] || !ss[1] || !ss[2]) {
        return "";
    }

    return ss[0] + '/' + ss[1] + '/' + ss[2];
}
function DateTimeConvertHMS(datetime) {
    if (typeof datetime != 'string') {
        return "";
    }

    var ss = (datetime.split(' ')[1]).split(':');
    if (!ss[0] || !ss[1] || !ss[2]) {
        return "";
    }

    return ss[0] + ':' + ss[1] + ':' + ss[2];
}
function DateTimeConvertMDHM(datetime) {
    if (typeof datetime != 'string') {
        return "";
    }


    var yy = (datetime.split(' ')[0]).split('/');
    if (!yy[0] || !yy[1] || !yy[2]) {
        return "";
    }

    var ss = (datetime.split(' ')[1]).split(':');
    if (!ss[0] || !ss[1] || !ss[2]) {
        return "";
    }

    return yy[1] + '/' + yy[2] + ' ' + ss[0] + ':' + ss[1];
}
function ShopInfoTotalRecord() {
    $.ajax({
        type: 'post',
        url: '/action/ShopAction.ashx',
        data: { option: 'OP_ShopInfoTotalRecordCount' },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                $('.tab_font_shop').text(msg.shopCount);
                $('.tab_font_menu').text(msg.billCount);
                $('.tab_font_tuan').text(msg.tuanCount);
                $('.tab_font_auth').text(msg.authCount);
                $('.tab_font_chat').text(msg.chatCount);
                $('.tab_font_tab').text(msg.labelCount);
            }
        }
    });
}
$('document').ready(function () {
    ShopInfoTotalRecord();
});
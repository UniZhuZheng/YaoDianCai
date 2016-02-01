var request =
{
    QueryString: function (val) {
        var uri = window.location.search;
        var re = new RegExp("" + val + "=([^&?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
    }
}
var SID = request.QueryString("SID");
var account = request.QueryString("account");
var name = request.QueryString("name");
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
function user_exit() {
    if (confirm("您确定退出吗？")) {
        window.location.href = "http://www.yaodiancai.com";
    }
}

//回到商家列表
function right_tab_shop() {
    window.location.href = "main.html?SID=" + SID + "&account=" + account + "&name=" + name;
}
//回到商家下单列表
function right_tab_menu() {
    window.location.href = "ShopBillRecord.html?SID=" + SID + "&account=" + account + "&name=" +name;
}
function right_tab_auth() {
    window.location.href = "ShopWifiGWRecord.html?SID=" + SID + "&account=" + account + "&name=" + name;
}

function right_tab_tuan() {
    window.location.href = "ShopTuanRecord.html?SID=" + SID + "&account=" + account + "&name=" + name;
}
function right_tab_dish() {
    window.location.href = "ShopDishList.html?SID=" + SID + "&account=" + account + "&name=" + name;
}
function right_tab_tab() {
    window.location.href = "ShopTabList.html?SID=" + SID + "&account=" + account + "&name=" + name;
}
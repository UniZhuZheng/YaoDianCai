//json字符串转换为json对象
function strToJson(str) {
    var json = (new Function("return " + str))();
    return json;
}
//if ($.cookie('userInfo') == null) {
//    window.location.href = "login.html";
//}

var userInfo = strToJson($.cookie('userInfo'));

function user_exit() {
    if (confirm("您确定退出程序吗？")) {
        $.cookie("userInfo", '', { expires: -1, path: '/' });
        $.cookie("shopSID", '', { expires: -1, path: '/' });
        window.location.href = "login.html";
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


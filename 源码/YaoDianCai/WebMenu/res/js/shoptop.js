function user_exit() {
    if (confirm("您确定关闭当前页吗？")) {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
}

var request = {
    QueryString: function (val) {
        var uri = window.location.search;
        var re = new RegExp("" + val + "=([^&?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
    }
}


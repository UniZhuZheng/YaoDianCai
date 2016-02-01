$("#YDC_CallPage").ready(function () {
    $("#Call_ShopName").text(name);
    $("#YDC_CallPage .Content_div_call").click(function () {
        var loadLayer = layer.load("加载中...");
        $.ajax({
            type: 'post',
            url: "/action/CallAction.ashx",
            data: { "tableName": TableNum, "SID": SID, "option": "OP_AddCall" },
            dataType: 'json',
            success: function (data) {
                if (data.ok) {
                    alert("呼叫服务成功。");
                    $.mobile.changePage("main.html", { transition: "flip" });
                }
                layer.close(loadLayer);
            }
        });
    });
});

$("#YDC_CallPage").ready(function () {
    $("#Call_ShopName").text(name);
    $("#YDC_CallPage .Content_div_call").click(function () {
        var loadLayer = layer.load("加载中...");
        $.getJSON(host+"/jsonpAction/CallAction.ashx?SID="+SID+"&tableName="+TableNum+
                "&option=OP_AddCall&jsoncallback=?",
            function(data){
                if (data.ok) {
                    alert("呼叫服务成功。");
                    $.mobile.changePage("/main", { transition: "flip" });
                }
                layer.close(loadLayer);
            });

    });
});

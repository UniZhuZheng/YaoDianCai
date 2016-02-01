var Uni_TableNum = JSON.parse(window.localStorage.getItem('Uni_TableNum'));
var TableNum = Uni_TableNum.TableNum; //桌号
var DisableTableMode = Uni_TableNum.DisableTableMode; //桌号是否可选，false可选，true不可选
var CustomerNum = Uni_TableNum.CustomerNum; //用餐人数
$(document).ready(function () {
    $("#flippage").click(function () {
        location.href = "/flippage";
    });
    $("#Content .Content_div_call").click(function () {
        var loadLayer = layer.load("加载中...");
        $.getJSON(host+"/jsonpAction/CallAction.ashx?SID="+SID+"&tableName="+TableNum+"&option=OP_AddCall&jsoncallback=?",
            function(data){
                layer.close(loadLayer);
                if (data.ok) {
                    alert("呼叫服务成功。");
                    location.href = "/flippage";
                } else {
                    alert("呼叫服务失败。");
                }
            });
    });
});
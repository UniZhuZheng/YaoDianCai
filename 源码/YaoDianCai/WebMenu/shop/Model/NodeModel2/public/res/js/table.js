var DishInfoMap = JSON.parse(window.localStorage.getItem('Uni_DishInfoMap')); //菜品的数据
var Uni_TableNum = JSON.parse(window.localStorage.getItem('Uni_TableNum'));
var TableNum = Uni_TableNum.TableNum; //桌号
var DisableTableMode = Uni_TableNum.DisableTableMode; //桌号是否可选，false可选，true不可选
var CustomerNum = Uni_TableNum.CustomerNum; //用餐人数
var BID = Uni_TableNum.BID; //用餐单号

var ToTalorderNum = 0;
var TotalorderPrice = 0;
$(document).ready(function () {
    data_init();
    $("#footer_confirmDiv").click(function () {
    /*
        var CustomerNumInput = $.trim($("#table_inputText").val());
        if (CustomerNumInput == '') {
            alert('请输入本餐桌的人数。');
            return;
        }
        if (isNaN(CustomerNumInput)) {
            alert('人数只能是数字。');
            return;
        }*/
        if (TableNum == "餐桌号") {
            layer.alert('请选择您的餐桌号。',12);
            return;
        }
        RefreshCartDishCount();
    });
});
function data_init() {
    $("#table_inputText").val(CustomerNum);
    $("#menu_list").empty();
    var TableInfoData = JSON.parse(window.localStorage.getItem('Uni_TableInfoData'));
    var tableStr = '';
    for (var i = 0; i < TableInfoData.length;i++ ) {
        tableStr+='<div class="menu_list_item">'+
                    '<a class="menu_list_tableNum ' +
                        ((TableNum != "餐桌号" && TableNum == TableInfoData[i].tablename) ? "table_number_click" : "table_number_normal") + '">' + 
                        TableInfoData[i].tablename + '</a>' +
                '</div>';
    }
    $("#menu_list").append(tableStr);
    skin_init();
    event_init();
}
var skin_init = function () {
    $('#menu_list').css('height', $(window).height() - 120);
}
var event_init = function () {
    $(".menu_list_tableNum").click(function () {
        $(".menu_list_tableNum").removeClass("table_number_click");
        $(".menu_list_tableNum").addClass("table_number_normal");
        $(this).addClass("table_number_click");
        $(this).removeClass("table_number_normal");
        TableNum = $(this).text();

        RefreshCartDishCount();
    });
}
function RefreshCartDishCount() {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+"&option=OP_GetCartDishByTable&jsoncallback=?",
        function(data){
            ToTalorderNum = 0;
            TotalorderPrice = 0;
            if (data != null && data != "") {
                var NameType = {};
                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                        NameType[nameType] = keyType;
                    }
                }
                for (var i = 0; i < data.length; i++) {
                    var dishType = NameType[data[i].dishName];
                    DishInfoMap[dishType][data[i].dishName].count = data[i].dishCount;
                    ToTalorderNum += data[i].dishCount;
                    TotalorderPrice += data[i].dishCount * data[i].dishPrice;
                }
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
            } else {
                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                    }
                }
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":0,"TotalorderPrice":0}');

            }
            $.getJSON(host+"/jsonpAction/TableAction.ashx?SID="+SID+"&tableName="+TableNum+"&option=OP_GetBIDByTable&jsoncallback=?",
                function(data){
                    if (data.ok) {
                        window.localStorage.setItem('Uni_TableNum', '{"DisableTableMode":' + DisableTableMode + ',"TableNum":"' + TableNum + '","CustomerNum":0,"BID":"' + data.BID + '"}');
                    } else {
                        window.localStorage.setItem('Uni_TableNum', '{"DisableTableMode":' + DisableTableMode + ',"TableNum":"' + TableNum + '","CustomerNum":0,"BID":""}');
                    }
                    location.href = "/main";
            });
    });
}
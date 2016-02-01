
function Initialize() {
    step_callback = function (curstep, finalstep) {
        if (curstep >= finalstep) {
            var TableNum = "餐桌号";
            var DisableTableMode = false;
            var BID = "";
            var tablename = (function (val) {
                var uri = window.location.search;
                var re = new RegExp("" + val + "=([^&?]*)", "ig");
                return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
            })("tableName");

            if (tablename == null || tablename == "") {
                DisableTableMode = false;
            } else {
                var TableNumTem = decodeURI(tablename);
                var Uni_TableInfoData = JSON.parse(window.localStorage.getItem('Uni_TableInfoData'));
                for (var i = 0; i < Uni_TableInfoData.length; i++) {
                    if (TableNumTem == Uni_TableInfoData[i].tablename) {
                        TableNum = TableNumTem;
                        BID = Uni_TableInfoData[i].BID
                        DisableTableMode = true;
                        break;
                    }
                }
            }
            window.localStorage.setItem('Uni_TableNum', '{"DisableTableMode":' + DisableTableMode + ',"TableNum":"' + TableNum + '","CustomerNum":0,"BID":"'+BID+'"}');
            window.location.href = "main.html";
        }
    };

    (function (finalstep) {
        var curstep = 0;
        window.localStorage.removeItem('Uni_DishInfoMap');
        window.localStorage.removeItem('Uni_DishTypes');
        window.localStorage.removeItem('Uni_TableInfoData');
        window.localStorage.removeItem('Uni_TotalOrder');
        window.localStorage.removeItem('Uni_TableNum');
        window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":0,"TotalorderPrice":0}');
        window.localStorage.setItem('Uni_TableNum', '{"DisableTableMode":false,"TableNum":"餐桌号","CustomerNum":0}');
        $.ajax({
            type: 'post',
            url: "/action/DishAction.ashx",
            data: { "option": "OP_GetAllDishes", "SID": SID },
            success: function (data) {
                curstep++;
                if (data != null) {
                    var DishInfoData = JSON.parse(data);
                    var DishInfoMap = {};
                    for (var i = 0; i < DishInfoData.length; i++) {
                        var type = DishInfoData[i].type;
                        if (!DishInfoMap[type]) {
                            DishInfoMap[type] = {};
                        }
                        DishInfoMap[type][DishInfoData[i].name] = DishInfoData[i];
                    }
                    window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                }
                step_callback(curstep, finalstep);
            }
        });

        $.ajax({
            type: 'post',
            url: "/action/DishAction.ashx",
            data: { "option": "OP_ListDishType", "SID": SID },
            success: function (data) {
                curstep++;
                if (data != null) {
                    window.localStorage.setItem('Uni_DishTypes', data);
                }
                step_callback(curstep, finalstep);
            }
        });

        $.ajax({
            type: 'post',
            url: "/action/TableAction.ashx",
            data: { "option": "OP_ListAllTables", "SID": SID },
            success: function (data) {
                curstep++;
                if (data != null) {
                    window.localStorage.setItem('Uni_TableInfoData', data);
                }
                step_callback(curstep, finalstep);
            }
        });
    })(3);
};
/*
iPhone 4.3.2 系统：
Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5
iPone 5.1 系统：
Mozilla/5.0 (iPhone; U; CPU iPhone OS 5_1 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5
iPone 5.1.1 系统：
Mozilla/5.0 (iPhone; U; CPU iPhone OS 5_1_1 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5
*/
function gt_ios4() {
    // 判断是否 iPhone 或者 iPod
    if ((navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i))) {
        // 判断系统版本号是否大于 4
        return Boolean(navigator.userAgent.match(/OS [5-9]_\d[_\d]* like Mac OS X/i));
    } else {
        return false;
    }
}
$(document).ready(function () {
    if (window.localStorage) {
        Initialize();
        $('.body_div').css('height', $(window).height());
        $('#tomain').click(function () {
            window.location.href = "main.html";
        });
    } else {
        if (gt_ios4()) {
            $.ajax({
                type: 'post',
                url: 'action/HostAction.ashx',
                data: { SID: SID},
                dataType: 'json',
                success: function (msg) {
                    if (msg.ok) {
                        data = msg.domain;
                        $('#tomain').attr('href', 'http://' + data + '/shop/' + SID + '/Html/Page/index.html');
                    }
                }
            });
            
        } else {
            alert("您的浏览器为内置浏览器，不兼容本项目。请换其他浏览器重新登录。");
        }
    }
});
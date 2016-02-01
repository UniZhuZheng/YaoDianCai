//---------------------------------------------------缓存数据------------------------------------------------------------
var SelDishName,
    SelDishMarkCount,
    DishInfoData = [],
    DishInfoMap = {},
    TableInfoData = [],
    DishTypes = [],
    ToTalorderNum = 0,
    TableNum = "餐桌号",
    BID = "",
    TotalorderPrice = 0,
    shopCarData = [],
    DisableTableMode = false;

function Initialize()
{
    $.mobile.loading('show');
    step_callback = function (curstep, finalstep) {
        if (curstep >= finalstep) {
            var tablename = (function (val) {
                var uri = window.location.search,
                    re = new RegExp("" + val + "=([^&?]*)", "ig");
                return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
            })("tableName");
            if (tablename == null || tablename == "") {
                DisableTableMode = false;
            } else {
                TableNum = decodeURI(tablename);
                for (var i = 0; i < TableInfoData.length; i++) {
                    if (TableInfoData[i].tablename == TableNum) {
                        BID = TableInfoData[i].BID;
                        break;
                    }
                    
                }
                DisableTableMode = true;
            }
            $.mobile.loading('hide');
            $.mobile.changePage("main.html");
        }
    };

    (function (finalstep) {
        var curstep = 0;
        $.ajax({
            type: 'post',
            url: "/action/DishAction.ashx",
            data: { "option": "OP_GetAllDishes", "SID": SID },
            success: function (data) {
                curstep++;
                if (data != null) {
                    DishInfoData = JSON.parse(data);
                    for (var i = 0,didl=DishInfoData.length; i < didl; i++ ) {
                        DishInfoMap[DishInfoData[i].name] = DishInfoData[i];
                    }
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
                    DishTypes = JSON.parse(data);
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
                    TableInfoData = JSON.parse(data);
                }
                step_callback(curstep, finalstep);
            }
        });
    })(3);
};
function UpdateCartDish(dishName, dishCount) {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "DishName": dishName, DishCount: dishCount, "SID": SID, "option": "OP_UpdateCartDishCount" },
        dataType: 'json',
        success: function (data) {
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        }
    });
    RefreshCartDishCount();
}
function AddCartDishOne(dishName) {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "DishName": dishName, "SID": SID, "option": "OP_CartAddOne" },
        dataType: 'json',
        success: function (data) {
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        }
    });
    RefreshCartDishCount();
}

function DelCartDishOne(dishName) {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "DishName": dishName, "SID": SID, "option": "OP_CartRemoveOne" },
        dataType: 'json',
        success: function (data) {
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        }
    });
    RefreshCartDishCount();
}
function DeleteCartDish(dishName) {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "DishName": dishName, "SID": SID, "option": "OP_RemoveCart" },
        dataType: 'json',
        success: function (data) {
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        }
    });
    RefreshCartDishCount();
}
function RefreshCartDishCount() {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "SID": SID, "option": "OP_GetCartDishByTable" },
        dataType: 'json',
        success: function (data) {
            ToTalorderNum = 0;
            if (data != null && data != "") {
                shopCarData = data;
                for (var key in DishInfoMap) {
                    var dish = DishInfoMap[key];
                    dish.count = 0;
                }
                for (var i = 0,max = data.length; i < max; i++) {
                    DishInfoMap[data[i].dishName].count = data[i].dishCount;
                    ToTalorderNum += data[i].dishCount;
                }
                $("#CartCount").text(ToTalorderNum);
            } else {
                for (var key in DishInfoMap) {
                    var dish = DishInfoMap[key];
                    dish.count = 0;
                }
            }
        }
    });
}
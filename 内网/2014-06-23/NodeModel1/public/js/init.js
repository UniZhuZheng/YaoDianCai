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
            $.mobile.changePage("/main");
        }
    };

    (function (finalstep) {
        var curstep = 0;
        $.getJSON(host+"/jsonpAction/DishAction.ashx?SID="+SID+"&option=OP_GetAllDishes&jsoncallback=?",
            function(data){
                curstep++;
                if (data != null) {
                    DishInfoData = data;
                    for (var i = 0,didl=DishInfoData.length; i < didl; i++ ) {
                        DishInfoMap[DishInfoData[i].name] = DishInfoData[i];
                    }
                }
                step_callback(curstep, finalstep);
            });
        $.getJSON(host+"/jsonpAction/DishAction.ashx?SID="+SID+"&option=OP_ListDishType&jsoncallback=?",
            function(data){
                curstep++;
                if (data != null) {
                    DishTypes = data;
                }
                step_callback(curstep, finalstep);
            });
        $.getJSON(host+"/jsonpAction/TableAction.ashx?SID="+SID+"&option=OP_ListAllTables&jsoncallback=?",
            function(data){
                curstep++;
                if (data != null) {
                    TableInfoData = data;
                }
                step_callback(curstep, finalstep);

            });
    })(3);
};
function UpdateCartDish(dishName, dishCount) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+"&DishName="+dishName+
            "&DishCount="+dishCount+"&option=OP_UpdateCartDishCount&jsoncallback=?",
        function(data){
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        });
    RefreshCartDishCount();
}
function AddCartDishOne(dishName) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+"&DishName="+dishName+
            "&option=OP_CartAddOne&jsoncallback=?",
        function(data){
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        });
    RefreshCartDishCount();
}

function DelCartDishOne(dishName) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+"&DishName="+dishName+
            "&option=OP_CartRemoveOne&jsoncallback=?",
        function(data){
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        });
    RefreshCartDishCount();
}
function DeleteCartDish(dishName) {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+"&DishName="+dishName+
            "&option=OP_RemoveCart&jsoncallback=?",
        function(data){
            if (!data.ok) {
                $("#currentshopcarpopup").popup("open");
                return false;
            }
        });
    RefreshCartDishCount();
}
function RefreshCartDishCount() {
    $.getJSON(host+"/jsonpAction/CartAction.ashx?SID="+SID+"&TableName="+TableNum+
            "&option=OP_GetCartDishByTable&jsoncallback=?",
        function(data){
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
        });
}
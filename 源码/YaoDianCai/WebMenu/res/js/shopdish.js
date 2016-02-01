var SID = request.QueryString("SID");
var maxResult = 20;
var tableUpdateCount = 0;
var stateSelectName = "";

var layerDiv;

function ShopDisdOptionHide() {
    layer.close(layerDiv);
}

/*---------------------------------菜品新增----------------------------------------*/
function MenuAddShow(div_id) {
    layerDiv = $.layer({
        shade: [0.3, '#000', true],
        shadeClose: true,
        type: 1,
        move: ['.xubox_title', false],
        area: ['349px', 'auto'],
        title: '菜品新增',
        border: [10, 0.5, '#000', true],
        page: { dom: '#' + div_id },
        close: function (index) {
            layer.close(index);
        }
    });
}

function MenuAdd() {
    var number = $.trim($("input[name='number']").val());
    var name = $.trim($("input[name='name']").val());
    if (name == "") {
        layer.alert('菜品名不可为空。', 12);
        return;
    }
    var property = $.trim($("input[name='property']").val());
    var type = $.trim($("input[name='type']").val());
    var price = $.trim($("input[name='price']").val());
    if (price == "") {
        layer.alert('菜品价格不可为空。', 12);
        return;
    }
    if (isNaN(price)) {
        layer.alert("菜品价格必须为数字",12);
        return;
    }
    var content = $.trim($("#content").val());
    $.ajax({
        type: 'post',
        url: '/action/DishAction.ashx',
        data: { option: 'OP_CreateOneDish', SID: SID, number: number, name: name, property: property, type: type, price: price, content: content },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                refreshMenuList();
                ShopDisdOptionHide();
            } else {
                layer.alert("菜单信息添加失败。",12);
            }
            $("input[name='number']").val("");
            $("input[name='name']").val("");
            $("input[name='property']").val("");
            $("input[name='type']").val("");
            $("input[name='price']").val("");
            $("#content").val("");
        },
        error: function () {
            layer.alert("网络连接出错，请重试。",12);
        }
    });
}
/*-----------------------------------桌号修改--------------------------------------------*/
function TableUpdateShow(div_id) {
    layerDiv = $.layer({
        shade: [0.3, '#000', true],
        shadeClose: true,
        type: 1,
        move: ['.xubox_title', false],
        area: ['690px', 'auto'],
        title: '桌号信息',
        border: [10, 0.5, '#000', true],
        page: { dom: '#' + div_id },
        close: function (index) {
            layer.close(index);
        }
    });

    $(".table_update_content").empty();
    tableUpdateCount = 0;
    $.ajax({
        type: 'post',
        url: '/action/TableAction.ashx',
        data: { option: 'OP_ListAllTables', SID: SID },
        dataType: 'json',
        success: function (data) {
            if (data != null) {
                $("#TableCount").html(data.length);
                for (var i = 0; i < data.length; i++) {
                    tableUpdateCount = i;
                    $(".table_update_content").append('<div class="table_update_item table_update_item_' + i +
                        '" onclick="TableDelete(\'table_update_item_' + i + '\',\'' + data[i].tablename + '\');">' +
                        '<div class="table_update_img"></div>' +
                        '<div class="table_update_font">' + data[i].tablename + '</div>' +
                        '</div>');
                }
            }
        },
        error: function () {
            layer.alert("网络连接出错，请重试。",12);
        }
    });

}

function TableUpdatehide() {
    layer.close(layerDiv);
}

function TableAdd() {
    var tableName = $.trim($("#tableNumber").val());
    if (tableName=="") {
        layer.alert("餐桌桌号不可为空。",12);
        return;
    }
    $.ajax({
        type: 'post',
        url: '/action/TableAction.ashx',
        data: { option: 'OP_CreateTable', SID: SID, tableName: tableName },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                tableUpdateCount++;
                $("#TableCount").html(tableUpdateCount);
                $(".table_update_content").append('<div class="table_update_item table_update_item_' + tableUpdateCount +
                    '" onclick="TableDelete(\'table_update_item_' + tableUpdateCount + '\',\'' + tableName + '\');">' +
                    '<div class="table_update_img"></div>' +
                    '<div class="table_update_font">' + tableName + '</div>' +
                    '</div>');
            } else {
                layer.alert("餐桌桌号已存在或添加失败。",12);
            }
            $("#tableNumber").val("");
        },
        error: function () {
            layer.alert("网络连接出错，请重试。",12);
        }
    });
}
function TableDelete(item, tableName) {
    $.layer({
        shade: [0], //不显示遮罩
        area: ['auto', 'auto'],
        dialog: {
            msg: '您确定要删除选中的菜品吗？',
            btns: 2,
            type: 4,
            btn: ['确定', '取消'],
            yes: function (index) {
                $.ajax({
                    type: 'post',
                    url: '/action/TableAction.ashx',
                    data: { option: 'OP_RemoveTable', SID: SID, tableName: tableName },
                    dataType: 'json',
                    success: function (msg) {
                        if (msg.ok) {
                            $("#TableCount").html(parseInt($("#TableCount").html()) - 1);
                            $("." + item).remove();
                        } else {
                            layer.alert("餐桌桌号删除失败。",12);
                        }
                    },
                    error: function () {
                        layer.alert("网络连接出错，请重试。",12);
                    }
                });
                layer.close(index);
            },
            no: function (index) {
                layer.close(index);
            }
        }
    });
}
/*---------------------------------模板修改-----------------------------------------*/
function TemplateShow(div_id) {
    layerDiv = $.layer({
        shade: [0.3, '#000', true],
        shadeClose: true,
        type: 1,
        move: ['.xubox_title', false],
        area: ['690px', 'auto'],
        title: '模板切换',
        border: [10, 0.5, '#000', true],
        page: { dom: '#' + div_id },
        close: function (index) {
            layer.close(index);
        }
    });
}

/*------------------------------菜品状态-------------------------------------*/

function MenuStateShow(div_id, name) {
    layerDiv=$.layer({
        shade: [0.3, '#000', true],
        shadeClose:true,
        type: 1,
        move: ['.xubox_title', false],
        area:['245px', 'auto'],
        title:'菜品状态修改',
        border:[10 , 0.5 , '#000', true],
        page:{ dom: '#' + div_id },
        close:function (index) {
            layer.close(index);
        }
    });
    stateSelectName = name;
}
function MenuStateSelect(state) {
    if (stateSelectName == null || stateSelectName=="") {
        layer.alert("菜品名获取失败，请重新操作。",12);
        return;
    }
    $.ajax({
        type: 'post',
        url: '/action/DishAction.ashx',
        data: { option: 'OP_ChangeDishState', SID: SID, name: stateSelectName, state: state },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                refreshMenuList();
                ShopDisdOptionHide();
                stateSelectName = "";
            } else {
                layer.alert("菜品状态修改失败。",12);
            }
        }
    });
}

/*------------------------------------菜品修改------------------------------------------------------*/
function MenuUpdateShow(div_id) {
    layerDiv = $.layer({
        shade: [0.3, '#000', true],
        shadeClose: true,
        type: 1,
        move: ['.xubox_title', false],
        area: ['349px', 'auto'],
        title: '菜品信息修改',
        border: [10, 0.5, '#000', true],
        page: { dom: '#' + div_id },
        close: function (index) {
            layer.close(index);
        }
    });
}
function GetMenuInfo(name) {
    $.ajax({
        type: 'post',
        url: '/action/DishAction.ashx',
        data: { option: 'OP_GetDishByName', SID: SID, name: name },
        dataType: 'json',
        success: function (msg) {
            if (msg != null) {
                $("input[name='numberUpdate']").val(msg.number);
                $("input[name='nameUpdate']").val(msg.name);
                $("input[name='propertyUpdate']").val(msg.property);
                $("input[name='typeUpdate']").val(msg.type);
                $("input[name='priceUpdate']").val(msg.price);
                
                $("#updateContent").val(msg.menucontent);

                MenuUpdateShow('Menu_Update');
            } else {
                layer.alert("菜品信息获取失败。", 12);
            }
        }
    });
}
function MenuUpdate() {
    var name = $.trim($("input[name='nameUpdate']").val());
    if (name == "") {
        layer.alert("菜品名不可为空。",12);
        return;
    }
    var number = $.trim($("input[name='numberUpdate']").val());
    var property = $.trim($("input[name='propertyUpdate']").val());
    var type = $.trim($("input[name='typeUpdate']").val());
    var price = $.trim($("input[name='priceUpdate']").val());
    if (price == "") {
        layer.alert("菜品价格不可为空。",12);
        return;
    }
    if (isNaN(price)) {
        layer.alert("菜品价格必须为数字",12);
        return;
    }
    var content = $.trim($("#updateContent").val());
    alert(content);
    $.ajax({
        type: 'post',
        url: '/action/DishAction.ashx',
        data: { option: 'OP_UpdateOneDish', SID: SID, number: number, name: name, property: property, type: type, price: price, content: content },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                refreshMenuList();
                ShopDisdOptionHide();
                
            } else {
                layer.alert("菜单信息修改失败。",12);
            }
            $("input[name='numberUpdate']").val("");
            $("input[name='nameUpdate']").val("");
            $("input[name='propertyUpdate']").val("");
            $("input[name='typeUpdate']").val("");
            $("input[name='priceUpdate']").val("");
            $("#updateContent").val("");
        },
        error: function () {
            layer.alert("网络连接出错，请重试。",12);
        }
    });
}
/*--------------------------------------菜品图片修改--------------------------------------------------*/
function MenuImgShow(div_id, name) {
    layerDiv = $.layer({
        shade: [0.3, '#000', true],
        shadeClose: true,
        type: 1,
        move: ['.xubox_title', false],
        area: ['340px', 'auto'],
        title: '菜品图片修改',
        border: [10, 0.5, '#000', true],
        page: { dom: '#' + div_id },
        close: function (index) {
            layer.close(index);
        }
    });
    stateSelectName = name;
}

function MenuImgSubmit() {
    if (stateSelectName == null || stateSelectName == "") {
        layer.alert("菜品名获取失败，请重新操作。",12);
        return;
    }
    $("#menuimgform").ajaxForm({
        url: "/action/DishAction.ashx?option=OP_ImgUpload&SID=" + SID + "&name=" + encodeURI(stateSelectName),
        beforeSubmit: function () {
            var _file = document.getElementById('menuimgfile');
            if (_file.value == "") {
                layer.alert("请选择文件！", 12);
                _file.focus();
                return false;
            }
            return true;
        },
        success: function (data) {
            var msg = JSON.parse(data);
            if (msg.ok) {
                alert(msg.uploadMsg);
                refreshMenuList();
            }
            else {
                alert(msg.uploadMsg);
            }
            stateSelectName = "";
            ShopDisdOptionHide();
            $("#menuimgfile").val("");
        },
        error: function (ex) {
            layer.alert('服务器正忙，请稍后提交...' + ex.status, 12);
        }
    });
}
/*------------------------------菜品删除-------------------------------------------*/
function MenuDelete(name) {
    $.layer({
        shade: [0], //不显示遮罩
        area: ['auto', 'auto'],
        dialog: {
            msg: '您确定要删除选中的菜品吗？',
            btns: 2,
            type: 4,
            btn: ['确定', '取消'],
            yes: function (index) {
                $.ajax({
                    type: 'post',
                    url: '/action/DishAction.ashx',
                    data: { option: 'OP_RemoveDishByName', SID: SID, name: name },
                    dataType: 'json',
                    success: function (msg) {
                        if (msg.ok) {
                            refreshMenuList();
                        } else {
                            layer.alert("菜品删除失败。",12);
                        }
                    }
                });
                layer.close(index);
            },
            no: function (index) {
                layer.close(index);
            }
        }
    });
}

function appendList(data) {
    var html = "";
    for (var i = 0; i < data.length; i++) {
        html += '<div class="shopsdishlist_td">' +
                    '<div class="shopsdishlist_td_1">' + data[i].number + '</div>' +
                    '<div class="shopsdishlist_td_2">' + data[i].name + '</div>' +
                    '<div class="shopsdishlist_td_3">' + data[i].type + '</div>' +
                    '<div class="shopsdishlist_td_4">' + data[i].property + '</div>' +
                    '<div class="shopsdishlist_td_5">' + data[i].price + '</div>' +
                    '<div class="shopsdishlist_td_6" onclick="MenuStateShow(\'Menu_State\',\'' + data[i].name + '\')"><image src="' + data[i].stateImg + '" width="68" height="32"></image></div>' +
                    '<div class="shopsdishlist_td_7" onclick="MenuImgShow(\'MenuImg_Update\',\'' + data[i].name + '\')"><image src="' + data[i].imgCode + '" height="50"></image></div>' +
                    '<div class="shopsdishlist_td_8"><div class="shopsdish_update" onclick="GetMenuInfo(\'' + data[i].name + 
                        '\');"></div><div class="shopsdish_delete" onclick="MenuDelete(\'' + data[i].name + '\');"></div></div>' +
                '</div>';
    }
    $(".shopsdishlist_center").append(html);
}

function refreshMenuList() {
    var pageselectCallback = function (page_index) {
        $(".shopsdishlist_td").remove();
        $.ajax({
            type: 'post',
            url: '/action/DishAction.ashx',
            data: { option: 'OP_ListLimitDishes', SID: SID, firstResult: page_index * maxResult, maxResult: maxResult },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    appendList(data);
                } else {
                    $(".shopsdishlist_td").remove();
                }
            }
        });
        return true;
    }

    $.ajax({
        type: 'post',
        url: '/action/DishAction.ashx',
        data: { option: 'OP_CountLimitDishes', SID: SID },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                $("#MenuCount").html(msg.count);
                $("#Pagination").pagination(msg.count, {
                    num_edge_entries: 1,
                    items_per_page: maxResult,
                    prev_text: "&nbsp;",
                    next_text: "&nbsp;",
                    ellipse_text: "&nbsp;",
                    callback: pageselectCallback
                });
            }
        }
    });
}
function refreshSearchMenuList(search) {
    var pageselectCallback = function (page_index) {
        $(".shopsdishlist_td").remove();
        $.ajax({
            type: 'post',
            url: '/action/DishAction.ashx',
            data: { option: 'OP_ListLimitDishesSearch', searchKey: search, SID: SID, firstResult: page_index * maxResult, maxResult: maxResult },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    appendList(data);
                } else {
                    $(".shopsdishlist_td").remove();
                }
            }
        });
        return true;
    }

    $.ajax({
        type: 'post',
        url: '/action/DishAction.ashx',
        data: { option: 'OP_CountLimitDishesSearch', searchKey: search, SID: SID },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                $("#MenuCount").html(msg.count);
                $("#Pagination").pagination(msg.count, {
                    num_edge_entries: 1,
                    items_per_page: maxResult,
                    prev_text: "&nbsp;",
                    next_text: "&nbsp;",
                    ellipse_text: "&nbsp;",
                    callback: pageselectCallback
                });
            }
        }
    });

}
function search() {
    var search = $.trim($('.navigation_center input[name=search]').val());
    if (search == "") {
        refreshMenuList();
    } else {
        refreshSearchMenuList(search);
    }
    $('.navigation_center input[name=search]').val('');
}

$('document').ready(function () {
    refreshMenuList();
});
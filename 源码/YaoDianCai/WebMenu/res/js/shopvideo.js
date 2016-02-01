var SID = request.QueryString("SID");
var maxResult = 20;
var tableUpdateCount = 0;
var stateSelectName = "";

var layerDiv;

function ShopDisdOptionHide() {
    layer.close(layerDiv);
}
function DateTimeConvertYMD(datetime) {
    if (typeof datetime != 'string') {
        return "";
    }

    var ss = (datetime.split(' ')[0]).split('/');
    if (!ss[0] || !ss[1] || !ss[2]) {
        return "";
    }

    return ss[0] + '/' + ss[1] + '/' + ss[2];
}
function DateTimeConvertHMS(datetime) {
    if (typeof datetime != 'string') {
        return "";
    }

    var ss = (datetime.split(' ')[1]).split(':');
    if (!ss[0] || !ss[1] || !ss[2]) {
        return "";
    }

    return ss[0] + ':' + ss[1] + ':' + ss[2];
}
/*---------------------------------菜品新增----------------------------------------*/
function MenuAddShow(div_id) {
    layerDiv = $.layer({
        shade: [0.3, '#000', true],
        shadeClose: true,
        type: 1,
        move: ['.xubox_title', false],
        area: ['349px', 'auto'],
        title: '视频新增',
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
        layer.alert('视频名不可为空。', 12);
        return;
    }
    var tab = $.trim($("input[name='tab']").val());
    var content = $.trim($("#content").val());
   
    $.ajax({
        type: 'post',
        url: '/action/VideoAction.ashx',
        data: { option: 'OP_Insert', SID: SID, number: number, name: name, tab: tab, content: content },
        dataType: 'json',
        success: function (msg) {
 
            if (msg.ok) {
                refreshMenuList();
                ShopDisdOptionHide();
            } else {
                layer.alert(msg.msg, 12);
            }
            $("input[name='number']").val("");
            $("input[name='name']").val("");
            $("input[name='tab']").val("");
            $("#content").val("");
        },
        error: function () {
            layer.alert("网络连接出错，请重试。", 12);
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
        title: '视频信息修改',
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
        url: '/action/VideoAction.ashx',
        data: { option: 'OP_QueryName', SID: SID, name: name },
        dataType: 'json',
        success: function (msg) {
            if (msg != null) {
                $("input[name='numberUpdate']").val(msg.number);
                $("input[name='nameUpdate']").val(msg.name);
                $("input[name='tabUpdate']").val(msg.tab);

                $("#updateContent").val(msg.content);

                MenuUpdateShow('Menu_Update');
            } else {
                layer.alert("视频信息获取失败。", 12);
            }
        }
    });
}
function MenuUpdate() {
    var name = $.trim($("input[name='nameUpdate']").val());
    if (name == "") {
        layer.alert("视频名不可为空。", 12);
        return;
    }
    var number = $.trim($("input[name='numberUpdate']").val());
    var tab = $.trim($("input[name='tabUpdate']").val());
    
    var content = $.trim($("#updateContent").val());

    $.ajax({
        type: 'post',
        url: '/action/VideoAction.ashx',
        data: { option: 'OP_UpdateInfo', SID: SID, number: number, name: name, tab: tab, content: content },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                refreshMenuList();
                ShopDisdOptionHide();

            } else {
                layer.alert("视频信息修改失败。", 12);
            }
            $("input[name='numberUpdate']").val("");
            $("input[name='nameUpdate']").val("");
            $("input[name='tabUpdate']").val("");
            $("#updateContent").val("");
        },
        error: function () {
            layer.alert("网络连接出错，请重试。", 12);
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
        title: '视频文件上传',
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
        layer.alert("视频名获取失败，请重新操作。", 12);
        return;
    }
    $("#menuimgform").ajaxForm({
        url: "/action/VideoAction.ashx?option=OP_Upload&SID=" + SID + "&name=" +encodeURI( stateSelectName),
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
            msg: '您确定要删除选中的视频信息吗？',
            btns: 2,
            type: 4,
            btn: ['确定', '取消'],
            yes: function (index) {
                $.ajax({
                    type: 'post',
                    url: '/action/VideoAction.ashx',
                    data: { option: 'OP_Delete', SID: SID, name: name },
                    dataType: 'json',
                    success: function (msg) {
                        if (msg.ok) {
                            refreshMenuList();
                        } else {
                            layer.alert("视频信息删除失败。", 12);
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
function SortUpDown(name1,sort1,name2,sort2) {
    $.ajax({
        type: 'post',
        url: '/action/VideoAction.ashx',
        data: { option: 'OP_Update', SID: SID, name1: name1, sort1: sort1, name2: name2, sort2: sort2 },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
                refreshMenuList();
            } else {
                layer.alert("排序失败。", 12);
            }
        }
    });
}
function appendList(data) {
    var html = "";
    for (var i = 0; i < data.length; i++) {
        if (data.length == 1) {
            html += '<div class="shopsdishlist_td">' +
                    '<div class="shopsdishlist_td_1">' + data[i].number + '</div>' +
                    '<div class="shopsdishlist_td_2">' + data[i].name + '</div>' +
                    '<div class="shopsdishlist_td_3">' + data[i].tab + '</div>' +
                    '<div class="shopsdishlist_td_4">' + DateTimeConvertYMD(data[i].createDate) + '</div>' +
                    '<div class="shopsdishlist_td_5">' + data[i].count + '</div>' +
                    '<div class="shopsdishlist_td_6"><div class="option-up"></div><div class="option-down"></div></div>' +
                    '<div class="shopsdishlist_td_7"><div class="option-file-upload" onclick="MenuImgShow(\'MenuImg_Update\',\'' + data[i].name + '\')"></div></div>' +
                    '<div class="shopsdishlist_td_8"><div class="shopsdish_update" onclick="GetMenuInfo(\'' + data[i].name +
                        '\');"></div><div class="shopsdish_delete" onclick="MenuDelete(\'' + data[i].name + '\');"></div></div>' +
                '</div>';
        } else {
        html += '<div class="shopsdishlist_td">' +
                    '<div class="shopsdishlist_td_1">' + data[i].number + '</div>' +
                    '<div class="shopsdishlist_td_2">' + data[i].name + '</div>' +
                    '<div class="shopsdishlist_td_3">' + data[i].tab + '</div>' +
                    '<div class="shopsdishlist_td_4">' + DateTimeConvertYMD(data[i].createDate) + '</div>' +
                    '<div class="shopsdishlist_td_5">' + data[i].count + '</div>';
        if (i == 0) {
            html += '<div class="shopsdishlist_td_6"><div class="option-up"></div>' +
                    '<div class="option-down" onclick="SortUpDown(\'' + data[i].name + '\',' + data[i + 1].sort + ',\'' + data[i + 1].name + '\',' + data[i].sort + ');" ></div></div>';
        } else if (i == data.length - 1) {
            html += '<div class="shopsdishlist_td_6"><div class="option-up" onclick="SortUpDown(\'' + data[i - 1].name +
                            '\',' + data[i].sort + ',\'' + data[i].name + '\',' + data[i - 1].sort + ');"></div><div class="option-down"></div></div>';
        } else {
            html += '<div class="shopsdishlist_td_6"><div class="option-up" onclick="SortUpDown(\'' + data[i - 1].name +
                            '\',' + data[i].sort + ',\'' + data[i].name + '\',' + data[i - 1].sort + ');"></div>' +
                            '<div class="option-down" onclick="SortUpDown(\'' + data[i].name +
                            '\',' + data[i + 1].sort + ',\'' + data[i + 1].name + '\',' + data[i].sort + ');"></div></div>';
        }
        html += '<div class="shopsdishlist_td_7"><div class="option-file-upload" onclick="MenuImgShow(\'MenuImg_Update\',\'' + data[i].name + '\')"></div></div>' +
                    '<div class="shopsdishlist_td_8"><div class="shopsdish_update" onclick="GetMenuInfo(\'' + data[i].name +
                        '\');"></div><div class="shopsdish_delete" onclick="MenuDelete(\'' + data[i].name + '\');"></div></div>' +
                '</div>';
        }
    }
    $(".shopsdishlist_center").append(html);
}

function refreshMenuList() {
    $(".shopsdishlist_td").remove();
    $.ajax({
        type: 'post',
        url: '/action/VideoAction.ashx',
        data: { option: 'OP_Query', SID: SID },
        dataType: 'json',
        success: function (data) {
            if (data.ok) {
                appendList(data.lists);
            } else {
                $(".shopsdishlist_td").remove();
            }
        }
    });
}

$('document').ready(function () {
    refreshMenuList();
});
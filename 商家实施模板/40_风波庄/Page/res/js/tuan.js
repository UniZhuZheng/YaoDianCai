var TableNum = JSON.parse(window.localStorage.getItem('Uni_TableNum')).TableNum; //桌号
var TuanInfo = JSON.parse(window.localStorage.getItem('Uni_TuanInfo'));
$(document).ready(function () {
    if (TuanInfo) {
        $("#TuanRecordList").empty();
        var tuanStr = '';
        for (var i = 0; i < TuanInfo.length; i++) {
            tuanStr += '<div class="record_list_item">' +
                    '<span class="item_name">' + TuanInfo[i].website + '</span>' +
                    '<span class="item_num">' + TuanInfo[i].number + '</span>' +
                '</div>';
        }
        $("#TuanRecordList").append(tuanStr);
    }
    
    $("#footer_confirm").click(function () {
        var owner = $.trim($('#TuanOwner').val());
        var phone = $.trim($('#TuanPhone').val());
        var website = $.trim($('#TuanWebsite').val());
        var number = $.trim($('#TuanNumber').val());

        if (number == null || number == "") {
            layer.alert('请输入您的团购号。');
            return;
        }

        var loadLayer = layer.load("加载中...");

        
        var nowdate = new Date();
        var tuaninfo = {
            "owner": owner,
            "phone": phone,
            "website": website,
            "number": number,
            "createDate": nowdate.getHours() + ":" + nowdate.getMinutes() + ":" + nowdate.getSeconds(),
            "tableName": TableNum
        };

        $.ajax({
            type: 'post',
            url: "/action/TuanAction.ashx",
            data: { "TuanJson": JSON.stringify(tuaninfo), "SID": SID, "option": "OP_CreateTuan" },
            dataType: 'json',
            success: function (data) {
                if (data.ok) {
                    layer.alert("团购提交成功。",12);
                    $('#TuanNumber').val("");
                    var s = '<div class="record_list_item">' +
                            '<span class="item_name">' + website + '</span>' +
                            '<span class="item_num">' + number + '</span>' +
                        '</div>';
                    $("#TuanRecordList").append(s);
                    if (TuanInfo) {
                        TuanInfo = [{ 'website': website, 'number': number}];
                    } else {
                        TuanInfo.push({ 'website': website, 'number': number });
                    }
                    window.localStorage.setItem('Uni_TuanInfo', JSON.stringify(TuanInfo));
                }
                layer.close(loadLayer);
            }
        });
    });

    skin_init();
    event_init();
});

var skin_init = function () {
    $('#tuan_content').css('height', $(window).height() - 120)
}

var event_init = function () {
    $("#flippage").click(function () {
        location.href = "flippage.html";
    });
}
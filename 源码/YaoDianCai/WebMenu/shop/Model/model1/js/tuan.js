$(document).ready(function () {
    $('#Tuan_ShopName').text(name);
    $("#TuanSubmit").click(function () {
        var owner = $.trim($('#TuanOwner').val()),
            phone = $.trim($('#TuanPhone').val()),
            website = $.trim($('#TuanWebsite').val()),
            number = $.trim($('#TuanNumber').val());
        if (number == null || number == "") {
            $("#TuanNumberPopup").popup("open");
            return;
        }
        var loadLayer = layer.load("加载中..."),
        s = "<span style='float:left;display:block;margin-left:25px; width:8em'>" + website + "</span><span>" + number + "</span><br />";
        $("#TuanHistoryList").append(s);
        var nowdate = new Date(),
            tuaninfo = {
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
                    alert("团购提交成功。");
                    $('#TuanNumber').val("");
                }
                layer.close(loadLayer);
            }
        });
    });
});
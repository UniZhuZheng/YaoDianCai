$("#YDC_TablePage").on("pagebeforeshow", function (event) {
    if (TableInfoData.length > 0) {
        $("#TableList").empty();
        var selected = $("#TableNameSelected").text();
        var blocks = ["<div class='ui-block-a'>", "<div class='ui-block-b'>", "<div class='ui-block-c'>"];
        for (var i = 0,tidl = TableInfoData.length; i < tidl; i++) {
            var tablebtn = new StringBuffer();
            if (TableInfoData[i].tablename == selected) {
                tablebtn.append(blocks[i % 3]);
                tablebtn.append("<a href='main.html' class='selected' data-role='button' data-inline='true' data-corners='false' data-direction='reverse'>");
                tablebtn.append(TableInfoData[i].tablename );
                tablebtn.append("</a></div>");

            } else {
                tablebtn.append(blocks[i % 3]);
                tablebtn.append("<a href='main.html' data-role='button' data-inline='true' data-corners='false' data-direction='reverse'>");
                tablebtn.append(TableInfoData[i].tablename);
                tablebtn.append("</a></div>");
            }
            $("#TableList").append(tablebtn.toString()).trigger('create');
        }
    }
    $("#TableList a").each(function () {
        $(this).click(function () {
            $("#TableNameSelected").text($(this).text());
            TableNum = $(this).text();
            for (var i = 0; i < TableInfoData.length; i++) {
                if (TableInfoData[i].tablename == TableNum) {
                    BID = TableInfoData[i].BID;
                    break;
                }
            }
            RefreshCartDishCount();
        });
    });
});
$("#YDC_TablePage").ready(function () {
    $("#Table_ShopName").text(name);
    $("#TableBackBtn").click(function () {
        if (TableNum == "餐桌号") {
            $("#TablePopup").popup("open");
            return false;
        }
    });
});
var SID = request.QueryString("SID");
var maxResult = 20;
var tableUpdateCount = 0;
var stateSelectName = "";

var layerDiv;

function ShopDisdOptionHide() {
    layer.close(layerDiv);
}
function appendList(data) {
    var html = "";
    for (var i = 0; i < data.length; i++) {
        html += '<div class="shopsdishlist_td">' +
                    '<div class="shopsdishlist_td_1">' + data[i].tableName + '</div>' +
                    '<div class="shopsdishlist_td_2">' + data[i].content + '</div>' +
                    '<div class="shopsdishlist_td_3">' + data[i].createDate + '</div>' +
                '</div>';
    }
    $(".shopsdishlist_center").append(html);
}

function refreshMenuList() {
    var pageselectCallback = function (page_index) {
        $(".shopsdishlist_td").remove();
        $.ajax({
            type: 'post',
            url: '/action/CallAction.ashx',
            data: { option: 'OP_Query', SID: SID, firstResult: page_index * maxResult, maxResult: maxResult },
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
        url: '/action/CallAction.ashx',
        data: { option: 'OP_QueryCount', SID: SID },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
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

function refreshDateMenuList(option, startDate) {
    var pageselectCallback = function (page_index) {
        $(".shopsdishlist_td").remove();
        $.ajax({
            type: 'post',
            url: '/action/CallAction.ashx',
            data: { option: option, SID: SID, startDate: startDate, firstResult: page_index * maxResult, maxResult: maxResult },
            dataType: 'json',
            success: function (msg) {
                if (msg.ok) {
                    appendList(msg.lists);
                } else {
                    $(".shopsdishlist_td").remove();
                }
            }
        });
        return true;
    }

    $.ajax({
        type: 'post',
        url: '/action/CallAction.ashx',
        data: { option: option + 'Count', SID: SID, startDate: startDate },
        dataType: 'json',
        success: function (msg) {
            if (msg.ok) {
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

function cancleSearch() {
    refreshMenuList();
    $(".inputDate").val("全部");
}
$('document').ready(function () {
    refreshMenuList();
    $(".inputDate").val("全部");

    $('.inputDate').DatePicker({
        format: 'Y-m-d',
        date: $('#inputDate').val(),
        current: $('#inputDate').val(),
        starts: 1,
        position: 'right',
        onBeforeShow: function () {
            if ($.trim($('#inputDate').val()) == "全部") {
                var myDate = new Date();
                $('#inputDate').DatePickerSetDate(myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate(), true);
                return;
            }
            $('#inputDate').DatePickerSetDate($('#inputDate').val(), true);
        },
        onChange: function (formated, dates) {
            $('#inputDate').val(formated);
            $('#inputDate').DatePickerHide();
            var startDate = formated;
            refreshDateMenuList('OP_QueryLimitDay', startDate);
        },
        onMonthsChange: function (yearDate, monthsDate) {
            $('#inputDate').val(yearDate + '-' + monthsDate);
            $('#inputDate').DatePickerHide();
            var startDate = yearDate + '-' + monthsDate + '-01';
            refreshDateMenuList('OP_QueryLimitMonth', startDate);
        }
    });
});
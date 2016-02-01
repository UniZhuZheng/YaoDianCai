var Uni_TableNum = JSON.parse(window.localStorage.getItem('Uni_TableNum'));
var TableNum = Uni_TableNum.TableNum; //桌号
var DisableTableMode = Uni_TableNum.DisableTableMode; //桌号是否可选，false可选，true不可选
var CustomerNum = Uni_TableNum.CustomerNum; //用餐人数
$(document).ready(function () {
    $('#Content_tab').css('height', $(window).height() - 100);
    $('.Content_tab_content').css('height', $(window).height() - 180);

    $('.textarea').val("");
    $("#header_back").click(function () {
        location.href = "main.html";
    });
    $('.content_tab_button').click(function () {
        $('#Content_tab').css('display', 'none');
        $('#Content_Evaluate').css('display', 'block');
        $('.content_tab_button').css('display', 'none');
    });
    $('.Content_Evaluate_cancle').click(function () {
        $('#Content_tab').css('display', 'block');
        $('#Content_Evaluate').css('display', 'none');
        $('.content_tab_button').css('display', 'block');
    });
    $('.textarea').keyup(function () {
        var len = $.trim($('.textarea').val()).length;

        if (len <= 150) {
            $('.Content_Evaluate_num').text((150 - len) + '字');
            $('.Content_Evaluate_num').css('color', '#000000');
            $('.textarea').css('border', '1px solid #000000');
        } else {
            $('.Content_Evaluate_num').css('color', '#ff4444');
            $('.textarea').css('border', '1px solid #FF4444');
            $('.Content_Evaluate_num').text('0字');
        }
    });
    $('.Content_Evaluate_submit').click(function () {
        var text = $.trim($('.textarea').val());
        var len = text.length;
        if(len<=0){
            alert('请先输入评价的内容。');
            return;
        }
        if (len > 150) {
            alert('您输入的字数太多，请检查后输入。');
            return;
        }
        var loadLayer = layer.load("加载中...");
        $.ajax({
            type: 'post',
            url: "/action/ShopCommentAction.ashx",
            data: { "SID": SID,"comment":text, "option": "OP_ShopCommentAdd" },
            dataType: 'json',
            success: function (data) {
                layer.close(loadLayer);
                if (data.ok) {
                    alert("商家评价提交成功。");
                    $('.textarea').val("");
                    $('#Content_tab').css('display', 'block');
                    $('#Content_Evaluate').css('display', 'none');
                    $('.content_tab_button').css('display', 'block');
                } else {
                    alert("商家评价提交失败。");
                }
            }
        });
    });
    var loadLayer = layer.load("加载中...");
    $.ajax({
        type: 'post',
        url: "/action/ShopLabelAction.ashx",
        data: { "SID": SID, "option": "OP_ListAllLabels" },
        dataType: 'json',
        success: function (data) {
            layer.close(loadLayer);
            if (data.ok) {
                $(".Content_tab_content").empty();
                if (data.lists.length > 0) {
                    appendTab(data.lists);
                } else {
                    alert("商家暂无印象标签。");
                }
            } else {
                alert("商家印象标签获取失败。");
            }
        }
    });
});

function appendTab(lists) {
    var str = "";
    var tabCount = $.cookie('tabCount');
    
    if (tabCount == null || tabCount == 'null') {
        tabCount = {};
        for (var i = 0; i < lists.length; i++) {
            var j = i % 5 + 1;
            str += '<div class="Content_tab_item Content_tab_margin_' + j + ' ">' +
                    '<div class="Content_tab_item_font Content_tab_item_font_' + j + '">' + lists[i].name + '</div>' +
                    '<div class="Content_tab_item_num">' + lists[i].count + '</div>' +
                '</div>';
            tabCount[lists[i].name] = false;
        }
    } else {
        tabCount = JSON.parse(tabCount);
        for (var i = 0; i < lists.length; i++) {
            var j = i % 5 + 1;
            str += '<div class="Content_tab_item Content_tab_margin_' + j + ' ' + 
                        ((tabCount[lists[i].name]==true) ? 'Content_tab_item_active' : '') + '">' +
                    '<div class="Content_tab_item_font Content_tab_item_font_' + j + '">' + lists[i].name + '</div>' +
                    '<div class="Content_tab_item_num">' + lists[i].count + '</div>' +
                '</div>';
        }
    }
    
    $(".Content_tab_content").append(str);
    $.cookie('tabCount', JSON.stringify(tabCount), { expires: 1, path: '/' });

    $(".Content_tab_item").click(function () {
        var it = $(this);
        var tabCount = JSON.parse($.cookie('tabCount'));
        var name = it.find('.Content_tab_item_font').text();
        var count = parseInt(it.find('.Content_tab_item_num').text());
        if (tabCount[name]) {
            count = count - 1;
        } else {
            count = count + 1;
        }

        $.ajax({
            type: 'post',
            url: "/action/ShopLabelAction.ashx",
            data: { "SID": SID, "option": "OP_LabelsUpdateCount", "name": name, "count": count },
            dataType: 'json',
            success: function (data) {
                if (data.ok) {
                    it.find('.Content_tab_item_num').text(count);
                    if (tabCount[name]) {
                        it.removeClass('Content_tab_item_active');
                        tabCount[name] = false;
                    } else {
                        it.addClass('Content_tab_item_active');
                        tabCount[name] = true;
                    }
                    $.cookie('tabCount', JSON.stringify(tabCount), { expires: 1, path: '/' });
                } else {
                    alert("商家印象标签新增失败。");
                }
            }
        });
    });
}
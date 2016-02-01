$(document).ready(function () {
    $(".videolistdiv").empty();
    $.getJSON(host+"/jsonpAction/VideoAction.ashx?SID="+SID+
            "&option=OP_Query&jsoncallback=?",
        function(data){
            if (data.ok) {
                data_init(data.lists);
            }
        });

    $('#flippage').click(function () {
        window.location.href = "/flippage";
    });
});
function data_init(data) {
    var videoStr = '';
    for (var i = 0; i < data.length; i++) {
        videoStr+='<div class="videoitem">'+
            '<div class="videoimg"><video src="ShopInfo/video/' + data[i].fileName +
            '" width="100%" height="100%" preload="auto" controls="controls" poster="res/images/videoimg.png"></video></div>' +
            '<div class="videonamecount">' +
            '<div class="videoname">'+(i+1)+'.' + data[i].name + '</div>' +
            '<div class="videofont" style="display:none;">点播次数：</div><div class="videocount" style="display:none;">' + data[i].count + '</div>' +
            '</div>' +
            '<div class="videocontent">' + data[i].content + '</div>' +
            '</div>';
    }
    $(".videolistdiv").append(videoStr);
    skin_init();
}
var skin_init = function () {
    $('.videolistdiv').css('height', $(window).height() - 80);
}

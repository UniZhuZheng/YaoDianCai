var dataMusic;
var Media;
var currCount = 0;
$(document).ready(function () {
    $(".videolistdiv").empty();
    $.getJSON(host+"/jsonpAction/MusicAction.ashx?SID="+SID+
            "&option=OP_Query&jsoncallback=?",
        function(data){
            if (data.ok) {
                data_init(data.lists);
            }
        });

    $('#flippage').click(function () {
        window.location.href = "flippage.html";
    });
});
function data_init(data) {
    var videoStr = '';
    dataMusic = data;
    for (var i = 0; i < data.length; i++) {
        videoStr += '<div class="videoitem" name="'+i+'">' +
            '<div class="videonamecount">' +
            '<div class="videoname">' + (i + 1) + '.' + data[i].name + '</div>' +
            '<div class="videopop">Pop</div><div class="videofont">点播次数：</div><div class="videocount videocount'+i+'" >' + data[i].count + '</div>' +
            '</div>' +
            '</div>';
    }
    $(".videolistdiv").append(videoStr);
    skin_init();
}
var skin_init = function () {
    $('.videolistdiv').css('height', $(window).height() - 180);
    $('.videoitem').click(function () {
        var me = this;
        var i = $(this).attr('name');
        var count = parseInt($(this).find('.videocount').text());
        currCount = i;
        musicplay(count);

    });
}
function musicplay(count) {
    if (Media) {
        Media.pause();
    }
    Media = new Audio("ShopInfo/music/" + dataMusic[currCount].fileName);
    mediaEvent(count);
    Media.load();
    Media.play();
    $('.musicplayname').text(dataMusic[currCount].name);
}
function musicplaypre() {
    if (currCount>0) {
        currCount--;
        var count = parseInt($('.videocount' + currCount).text());
        musicplay(count);
    }
}
function musicplayplay() {
    if (currCount==0) {
        Media = new Audio("ShopInfo/music/" + dataMusic[currCount].fileName);
        mediaEvent(0);
        $('.musicplayname').text(dataMusic[currCount].name);
    }
    Media.play();
}
function musicplaystop() {
    Media.pause();
}
function musicplaynext() {
    if (currCount < dataMusic.length-1) {
        currCount++;
        var count = parseInt($('.videocount' + currCount).text());
        musicplay(count);
    }
}

function mediaEvent(count) {
    Media.addEventListener("play", function () {
        $.getJSON(host+"/jsonpAction/MusicAction.ashx?SID="+SID+"&name="+dataMusic[currCount].name+"&count="+(count+1)+
                "&option=OP_UpdateCount&jsoncallback=?",
            function(data){
                if (data.ok) {
                    $('.videocount' + currCount).text(count + 1);
                }
            });
    });
    Media.addEventListener("ended", function () {
        currCount++;
        var count = parseInt($('.videocount' + currCount).text());
        musicplay(count);
    });
}

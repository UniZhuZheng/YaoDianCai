//var DishInfoMap = JSON.parse($.cookie('Uni_DishInfoMap')); //菜品的数据
var DishInfoMap = JSON.parse(window.localStorage.getItem('Uni_DishInfoMap')); //菜品的数据
var dishName =decodeURI ((function (val) {
    var uri = window.location.search;
    var re = new RegExp("" + val + "=([^&?]*)", "ig");
    return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
})("dishName"));
var dishType = decodeURI((function (val) {
    var uri = window.location.search;
    var re = new RegExp("" + val + "=([^&?]*)", "ig");
    return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
})("dishType"));
$(document).ready(function () {
    $('#detailInfo_img').attr('src', '../ShopInfo/menuimg/' + dishName + '.jpg');
    $('#detailInfo_name').text(dishName);
    $('#description_text').text(DishInfoMap[dishType][dishName].content);
    skin_init();
    event_init();
});

var skin_init = function () {
    $('#menuInfo_content').css('height', $(window).height() - 100);
}

var event_init = function () {
    $("#header_back").click(function () {
        location.href = "/main";
    });
}
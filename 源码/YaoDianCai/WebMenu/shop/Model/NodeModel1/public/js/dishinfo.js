$("#YDC_DishinfoPage").on("pagebeforeshow", function () {
    document.title = SelDishName;
    var dish = DishInfoMap[SelDishName];
    SelDishMarkCount = dish.count;
    $("#DishinfoImg").attr("src", "../ShopInfo/menuimg/" + dish.name + ".jpg");
    $("#DishinfoNumber").text(dish.number);
    $("#DishinfoName").text(dish.name);
    $("#DishinfoKind").text(dish.kind);
    $("#DishinfoType").text(dish.type);
    $("#DishinfoContent").text(dish.content);
    $("#DishinfoPrice").text(dish.price);
    $("#DishinfoCount span").text(SelDishMarkCount).css("color", "#eee");
});

$("#YDC_DishinfoPage").ready(function () {
    $("#Dishinfo_ShopName").text(name);
    $("#DishinfoSubmit").click(function () {
        var cur = parseInt($("#DishinfoCount span").text());
        (cur != SelDishMarkCount) && UpdateCartDish($("#DishinfoName").text(), cur);
        $.mobile.changePage("/main");
    });
    $("#DishinfoMinus").click(function () {
        var cur = parseInt($("#DishinfoCount span").text());
        if (cur == 0) return;
        $("#DishinfoCount span").text(--cur);
        cur == SelDishMarkCount ?
            $("#DishinfoCount span").css("color", "#eee"):
            $("#DishinfoCount span").css("color", "#c00");
    });
    $("#DishinfoPlus").click(function () {
        var cur = parseInt($("#DishinfoCount span").text());
        $("#DishinfoCount span").text(++cur);
        cur == SelDishMarkCount ?
            $("#DishinfoCount span").css("color", "#eee"):
            $("#DishinfoCount span").css("color", "#c00");
    });
});

var DishInfoMap = JSON.parse(window.localStorage.getItem('Uni_DishInfoMap')); //菜品的数据
var Uni_TableNum = JSON.parse(window.localStorage.getItem('Uni_TableNum'));
var TableNum = Uni_TableNum.TableNum; //桌号
var DisableTableMode = Uni_TableNum.DisableTableMode; //桌号是否可选，false可选，true不可选
var CustomerNum = Uni_TableNum.CustomerNum; //用餐人数
var DishTypes = JSON.parse(window.localStorage.getItem('Uni_DishTypes')); //菜品的分类数据
var Uni_TotalOrder = JSON.parse(window.localStorage.getItem('Uni_TotalOrder'));
var ToTalorderNum = Uni_TotalOrder.ToTalorderNum;
var TotalorderPrice = Uni_TotalOrder.TotalorderPrice;

function UpdateCartDish(dishName, dishCount) {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "DishName": dishName, "DishCount": dishCount, "SID": SID, "option": "OP_UpdateCartDishCount" },
        dataType: 'json',
        success: function (data) {
            if (!data.ok) {
                layer.alert('添加失败。',8);
                return false;
            }
        }
    });
}

function DishUpdate() {
    $(".YDC_products_select_subtract").click(function () {
        if (TableNum == '餐桌号') {
            alert("请先选择桌号。");
            location.href = "table.html";
            return;
        }
        var dishName = $(this).attr('name');
        var dishType = $(this).attr('type');
        var curcount = $(this).parent().find('.YDC_products_select_num').text();
        var curprice = $(this).parent().prev().find('.price').text();
        if (parseInt(curcount) > 0) {
            UpdateCartDish(dishName, parseInt(curcount) - 1);

            DishInfoMap[dishType][dishName].count = parseInt(curcount) - 1;
            if (DishInfoMap[dishType][dishName].count == 0) {
                $(this).css('background-image', 'url(res/images/minus_normal.png)');
            }

            ToTalorderNum -= 1;
            TotalorderPrice -= parseFloat(curprice);

            $(this).parent().find('.YDC_products_select_num').text(parseInt(curcount) - 1);
            
            $('.price_total_left_number').text(TotalorderPrice + '元');
            window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
            window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
        }

    });
    $(".YDC_products_select_add").click(function () {
        if (TableNum == '餐桌号') {
            alert("请先选择桌号。");
            location.href = "table.html";
            return;
        }
        var dishName = $(this).attr('name');
        var dishType = $(this).attr('type');
        var curcount = $(this).parent().find('.YDC_products_select_num').text();
        var curprice = $(this).parent().prev().find('.price').text();

        UpdateCartDish(dishName, parseInt(curcount) + 1);
        DishInfoMap[dishType][dishName].count = parseInt(curcount) + 1;
        if (DishInfoMap[dishType][dishName].count > 0) {
            $(this).parent().find('.YDC_products_select_subtract').css('background-image', 'url(res/images/minus_click.png)');
        }
        ToTalorderNum += 1;
        TotalorderPrice += parseFloat(curprice);

        $(this).parent().find('.YDC_products_select_num').text(parseInt(curcount) + 1)
        //$('#menu_select_number').text("X" + ToTalorderNum);
        $('.price_total_left_number').text(TotalorderPrice + '元');
        window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
        window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
    });
}

function event_init() {
    //左边列表点击
    $(".menu_type_item").click(function () {
        $(".menu_type_item").removeClass("left_frame_item_click");
        $(this).addClass("left_frame_item_click");
        $("#YDC_products").empty();
        dishType = $(this).find(".type_name").text();
        if (DishInfoMap) {
            var temdishType= (dishType == "推荐") ? "特殊二" : dishType;
            var DishInfoData = DishInfoMap[temdishType];
            var dishStr = '';
            for (var key in DishInfoData) {
                var dish = DishInfoData[key];
                dishStr += '<div class="YDC_products_item">' +
//						'<div class="img" name="' + dish.name + '"><img src="../ShopInfo/menuimg/' + dish.name + '.jpg"></div>' +
						'<div class="YDC_products_info">' +
							'<div class="name">' + dish.name + '</div>' +
                            '<div class="name_en">' + DishInfoEn.DishName[dish.name] + '</div>' +

						'</div>' +
                        '<div class="YDC_products_price">' +
                            '<div class="price_font" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>RMB. </div>' +
                            '<div class="price" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>' + dish.price + '</div>' +
                            '<div class="price_yuan" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>元</div>' +
                        '</div>'+
						'<div class="YDC_products_select" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>' +
						    '<div name="' + dish.name + '" type="' + dishType + '" class="YDC_products_select_subtract subtract_normal" ' +
                                (dish.count > 0 ? 'style="background-image:url(res/images/minus_click.png);"' : '') + '></div>' +
							'<div class="YDC_products_select_num">' + dish.count + '</div>' +
							'<div name="' + dish.name + '" type="' + dishType + '" class="YDC_products_select_add add_normal"></div>' +
						'</div>' +
					'</div>';
            }
            $("#YDC_products").append(dishStr);
            if (dishType == "推荐") {
                $(".YDC_products_item .img").click(function () {
                    var dishName = $(this).attr('name');
                    location.href = "dishinfo.html?dishName=" + dishName + "&dishType=特殊二";
                });
            }
            DishUpdate();
        }

    });
    DishUpdate();
}
function body_init() {
    $("#popup").hide();
    $('#YDC_header .YDC_person_mumber').css('width', $(window).width() - 100);
    $('#left_menu').css('height', $(window).height() - 150);
    $('.popup_bac').css('height', $(window).height());
    $('#right_contents').css({
        height: $(window).height() - 90,
        width: $(window).width() - 100
    });
    $('.popup_bac').click(function () {
        $('.popup_bac').css('display', 'none');
    });
    $("#YDC_more").click(function () {
        if ($("#popup").is(":hidden")) {
            $("#popup").show();
            $('.popup_bac').css('display', 'block');
        } else {
            $("#popup").hide();
        }
    });
    if (!DisableTableMode) {
        $("#desk_select").click(function () {
            window.localStorage.setItem('ToPage', "main.html");
                location.href = "table.html";
        });
    }
    

    $("#YDC_footer_shopping_cart").click(function () {
        if (ToTalorderNum <= 0) {
            layer.alert('您尚未点菜，请先点餐。',12);
            return;
        }
        location.href = "cart.html";
    });

    $("#popup_tuan").click(function () {
        if (TableNum == '餐桌号') {
            alert("请先选择桌号。");
            location.href = "table.html";
            return;
        }
        location.href = "tuan.html";
    });
    $("#popup_menu_record").click(function () {
        if (TableNum == '餐桌号') {
            alert("请先选择桌号。");
            location.href = "table.html";
            return;
        }
        location.href = "order.html";
    });
    $("#popup_call").click(function () {
        if (TableNum == '餐桌号') {
            alert("请先选择桌号。");
            location.href = "table.html";
            return;
        }
        location.href = "call.html";
    });
}

function data_init() {
    //获取桌号信息
    $("#table_number").text(TableNum);
    $("#person_number").text(CustomerNum + '人');
    //获取菜品分类数据
    $("#left_menu").empty();
    $("#YDC_products").empty();
    var dishType;
    if (DishTypes) {
        for (var i = 0; i < DishTypes.length; i++) {
            if (DishTypes[i].type.indexOf('特殊一') == -1) {
                dishType = DishTypes[i].type;
                break;
            }
        }
        var typeStr = '';
        for (var i = 0; i < DishTypes.length; i++) {
            if (DishTypes[i].type.indexOf('特殊一') == -1) {
                typeStr +=
                        '<div class="menu_type_item ' +
                            ((DishTypes[i].type == dishType) ? 'left_frame_item_click' : '') +
                        '">' +
                            '<div class="type_name">' +
                                (DishTypes[i].type == '特殊二' ? '推荐' : DishTypes[i].type) +
                            '</div>' +
                            '<div class="type_name_en">' +
                                DishInfoEn.DishType[DishTypes[i].type] +
                            '</div>' +
                        '</div>';
            } 
        }
        $("#left_menu").append(typeStr);
    }
    
    

    //获取菜品信息
    if (DishInfoMap) {
        var DishInfoData = DishInfoMap[dishType];
        var dishStr = '';
        for (var key in DishInfoData) {
            var dish = DishInfoData[key];
            dishStr += '<div class="YDC_products_item">' +
//						'<div class="img" name="' + dish.name + '"><img src="../ShopInfo/menuimg/' + dish.name + '.jpg"></div>' +
						'<div class="YDC_products_info">' +
							'<div class="name">' + dish.name + '</div>' +
                            '<div class="name_en">' + DishInfoEn.DishName[dish.name] + '</div>' +
						'</div>' +
                        '<div class="YDC_products_price">' +
                            '<div class="price_font" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>RMB. </div>' +
                            '<div class="price" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>' + dish.price + '</div>' +
                            '<div class="price_yuan" ' + (dishType == "推荐" ? 'style="display:none;"' : '') + '>元</div>' +
                        '</div>'+
						'<div class="YDC_products_select" ' + (dishType == "特殊二" ? 'style="display:none;"' : '') + '>' +
						    '<div name="' + dish.name + '" type="' + dishType + '" class="YDC_products_select_subtract subtract_normal" ' +
                                (dish.count > 0 ? 'style="background-image:url(res/images/minus_click.png);"' : '') + '></div>' +
							'<div class="YDC_products_select_num">' + dish.count + '</div>' +
							'<div name="' + dish.name + '" type="' + dishType + '" class="YDC_products_select_add add_normal"></div>' +
						'</div>' +
					'</div>';
        }
        $("#YDC_products").append(dishStr);
        if (dishType == "特殊二") {
            $(".YDC_products_item .img").click(function () {
                var dishName = $(this).attr('name');
                location.href = "dishinfo.html?dishName=" + dishName + "&dishType=" + dishType;
            });
        }
    }
    //获取点单总数
    //$('#menu_select_number').text("X" + ToTalorderNum);
    $('.price_total_left_number').text(TotalorderPrice + '元');
    
    event_init();
}
function RefreshCartDishCount() {
    $.ajax({
        type: 'post',
        async: false,
        url: "/action/CartAction.ashx",
        data: { "TableName": TableNum, "SID": SID, "option": "OP_GetCartDishByTable" },
        dataType: 'json',
        success: function (data) {
            ToTalorderNum = 0;
            TotalorderPrice = 0;
            if (data != null && data != "") {
                shopCarData = data;
                NameType = {};
                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                        NameType[nameType] = keyType;
                    }
                }
                for (var i = 0; i < data.length; i++) {
                    DishInfoMap[NameType[data[i].dishName]][data[i].dishName].count = data[i].dishCount;
                    ToTalorderNum += data[i].dishCount;
                    TotalorderPrice += data[i].dishCount * data[i].dishPrice;
                }
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":' + ToTalorderNum + ',"TotalorderPrice":' + TotalorderPrice + '}');
            } else {

                for (var keyType in DishInfoMap) {
                    for (var nameType in DishInfoMap[keyType]) {
                        var dish = DishInfoMap[keyType][nameType];
                        dish.count = 0;
                    }
                }
                ToTalorderNum = 0;
                TotalorderPrice = 0;
                window.localStorage.setItem('Uni_DishInfoMap', JSON.stringify(DishInfoMap));
                window.localStorage.setItem('Uni_TotalOrder', '{"ToTalorderNum":0,"TotalorderPrice":0}');
            }
            //$('#menu_select_number').text("X" + ToTalorderNum);
            $('.price_total_left_number').text(TotalorderPrice + '元');
        }
    });
}
//获取桌号
$(document).ready(function () {
    //显示数据
    data_init();
    body_init();

    setInterval(function () {
        if (TableNum != '餐桌号') {
            RefreshCartDishCount();
        }
    }, 5000);

    $('#flippage').click(function () {
        window.location.href = "flippage.html";
    });
});
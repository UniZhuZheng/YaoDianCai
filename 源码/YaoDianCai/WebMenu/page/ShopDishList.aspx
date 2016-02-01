<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopDishList.aspx.cs" Inherits="Uni.WebMenu.Page.ShopDishList" %>

<!DOCTYPE html>
<html>
<head>
    <title>商家菜品界面</title>
    <link href="/res/css/Shops-option.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/res/js/jquery.min.js"></script>
    <script type="text/javascript" src="/res/js/layer/layer.min.js"></script>
    <script type="text/javascript" src="/res/js/jquery.form.js"></script>
    <script type="text/javascript" src="/res/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/res/js/jquery.pagination.js"></script>
    <script type="text/javascript" src="/res/js/shoptop.js"></script>
    <script type="text/javascript" src="/res/js/shopdish.js"></script>
    
</head>
<body>
    <div class="body_panel">
        <div class="logo_top">
            <div class="logo_top_left"><img src="/res/images/top-logo.png" width="100%" height="100%" alt="" /></div>
            <div class="logo_top_right">
                <div><span>admin</span></div>
                <div onclick="user_exit();" style="cursor:pointer;"><img src="/res/images/arrows-bottom.png" width="100%" height="100%" alt="" /></div>
            </div>
        </div>
        <div class="navigation">
            <div class="navigation_left">
                <div class="navigation_left_head"></div>
                <div class="navigation_left_middle" style="width:90px;"><span>商家菜品列表</span></div>
                <div class="navigation_left_end"></div>
            </div>
            <div class="navigation_center">
                <input name="search" />
                <div class="navigation_center_search" onclick="search();"></div>
            </div>
        </div>

        <div class="shopsdishlist">
            <div class="shopsdishlist_top">
                <div class="dish_top_left">
                    <div class="dish_top_left_div">
                        <div>商家名：</div>
                        <div class="div_font" ID="ShopName" runat="server"></div>
                    </div>
                    <div class="dish_top_left_div">
                        <div>菜品数：</div>
                        <div class="div_font" id="MenuCount">25</div>
                    </div>
                </div>
                <div class="dish_top_center">
                    <div class="dish_top_left_div">
                        <div>餐桌数：</div>
                        <div class="div_font TableCount_div" ID="TableCount" runat="server">10</div>
                    </div>
                    <div class="dish_top_left_div">
                        <div>模&nbsp;&nbsp;&nbsp;板：</div>
                        <div class="div_font">默认</div>
                    </div>
                </div>
                <div class="dish_top_right">
                    <div class="dish_top_right_div dish_top_right_menu" onclick="MenuAddShow('Menu_Add');"></div>
                    <div class="dish_top_right_div dish_top_right_tem" onclick="TemplateShow('Template_Div');"></div>
                    <div class="dish_top_right_div dish_top_right_table" onclick="TableUpdateShow('Table_Update');"></div>
                </div>
            </div>
            <div class="shopsdishlist_center">
                <div class="shopsdishlist_th">
                    <div class="shopsdishlist_th_1">菜品编号</div>
                    <div class="shopsdishlist_th_2">菜品名称</div>
                    <div class="shopsdishlist_th_3">菜品分类</div>
                    <div class="shopsdishlist_th_4">菜品口味</div>
                    <div class="shopsdishlist_th_5">价格（元）</div>
                    <div class="shopsdishlist_th_6">菜品状态</div>
                    <div class="shopsdishlist_th_7">菜品图片</div>
                </div>
            </div>
        </div>
        <div class="pagination">
            <div id="Pagination">
        </div>
        </div>
    </div>
   <!--更改菜品的状态-->
   <div id="Menu_State" class="menu_state">
        <div class="menu_state_select">
            <div class="menu_state_select_xsz" onclick="MenuStateSelect(0)"></div>
            <div class="menu_state_select_wxs" onclick="MenuStateSelect(1)"></div>
            <div class="menu_state_select_wsj" onclick="MenuStateSelect(2)"></div>
        </div>
   </div>


    <!--  添加菜品  -->
    <div id="Menu_Add" class="menu_add" >
        <div class="menu_add_row">
            <div class="menu_add_font">菜品名称：</div>
            <div><input name="name" type="text" runat="server" id="name"/></div>
        </div>
        <div class="menu_add_row">
            <div class="menu_add_font">菜品编号：</div>
            <div><input name="number" type="text" runat="server" id="number"/></div>
        </div>
        
        <div class="menu_add_row">
            <div class="menu_add_font">菜品分类：</div>
            <div><input name="type" type="text" runat="server" id="type"/></div>
        </div>
        <div class="menu_add_row">
            <div class="menu_add_font">菜品口味：</div>
            <div><input name="property" type="text" runat="server" id="property"/></div>
        </div>
        <div class="menu_add_row">
            <div class="menu_add_font">菜品价格：</div>
            <div><input name="price" type="text" runat="server" id="price"/></div>
        </div>
        <div class="menu_add_row menu_add_area">
            <div class="menu_add_font">菜品介绍：</div>
            <div>
                <textarea rows="3" cols="20" id="content">
                </textarea>

            </div>
        </div>
        <div class="menu_add_row" style="float:right;">
            <div class="menu_add_cancel"  onclick="ShopDisdOptionHide();"></div>
            <div class="menu_add_confirm" onclick="MenuAdd();"></div>
        </div>
    </div>

    <!--  修改信息菜品  -->
    <div id="Menu_Update" class="menu_update" >
        <div class="menu_update_row">
            <div class="menu_update_font">菜品名称：</div>
            <div><input name="nameUpdate" type="text" style="color:#AAAAAA;" readonly/></div>
        </div>
        <div class="menu_update_row">
            <div class="menu_update_font">菜品编号：</div>
            <div><input name="numberUpdate" type="text" /></div>
        </div>
        
        <div class="menu_update_row">
            <div class="menu_update_font">菜品分类：</div>
            <div><input name="typeUpdate" type="text" /></div>
        </div>
        <div class="menu_update_row">
            <div class="menu_update_font">菜品口味：</div>
            <div><input name="propertyUpdate" type="text" /></div>
        </div>
        <div class="menu_update_row">
            <div class="menu_update_font">菜品价格：</div>
            <div><input name="priceUpdate" type="text" /></div>
        </div>
        <div class="menu_update_row menu_update_area">
            <div class="menu_update_font">菜品介绍：</div>
            <div>
                <textarea rows="3" cols="20" id="updateContent">
                </textarea>

            </div>
        </div>
        <div class="menu_update_row" style="float:right;">
            <div class="menu_update_cancel"  onclick="ShopDisdOptionHide();"></div>
            <div class="menu_update_confirm" onclick="MenuUpdate();"></div>
        </div>
    </div>

    <!--桌号更改-->
    <div id="Table_Update" class="table_update">
        <div class="table_update_top">
            <div class="table_add_button" onclick="TableAdd();"></div>
            <div class="table_add_input"><input type="text" id="tableNumber"/></div>
            <div class="table_add_font">请输入新桌号名称：</div>
        </div>
        <div class="table_update_content">
        </div>
        <div class="table_update_bottom">
            <div class="table_update_cancle" onclick="ShopDisdOptionHide();"></div>
        </div>
    </div>
    <!--面板更改-->
    <div id="Template_Div" class="template_div">
        <div class="template_div_content">
            <div class="template_div_select">
                <div class="template_div_select_top">
                    <div class="template_div_select_top_img template_div_select_top_img_select"></div>
                    <div class="template_div_select_top_font">默认模板一</div>
                </div>
                <div class="template_div_select_img"></div>
            </div>
        </div>
        <div class="template_div_bottom">
            <div class="template_div_cancle" onclick="ShopDisdOptionHide();"></div>
            <div class="template_div_confirm"></div>
        </div>
    </div>
    
    <!--菜品图片修改-->
    <div id="MenuImg_Update" class="menuimg_update">
        <div class="menuimg_update_top">
            <form id="menuimgform" method="post" autocomplete="off" enctype="multipart/form-data">
                <input id="menuimgfile" name="postfile" type="file" />
                <input type="submit" value="上传保存" onclick="MenuImgSubmit();" style="width:80px;"/>
        </div>
        <div class="menuimg_update_bottom">
            <div class="menuimg_update_cancle" onclick="ShopDisdOptionHide();"></div>
        </div>
    </div>
</body>
</html>

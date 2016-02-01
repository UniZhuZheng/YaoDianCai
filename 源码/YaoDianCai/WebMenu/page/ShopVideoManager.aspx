<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopVideoManager.aspx.cs" Inherits="Uni.WebMenu.page.ShopVideoManager" %>

<!DOCTYPE html>
<html>
<head>
    <title>商家视频界面</title>
    <link href="/res/css/shopvideo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/res/js/jquery.min.js"></script>
    <script type="text/javascript" src="/res/js/layer/layer.min.js"></script>
    <script type="text/javascript" src="/res/js/jquery.form.js"></script>
    <script type="text/javascript" src="/res/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/res/js/jquery.pagination.js"></script>
    <script type="text/javascript" src="/res/js/shoptop.js"></script>
    <script type="text/javascript" src="/res/js/shopvideo.js"></script>
    
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
                <div class="navigation_left_middle" style="width:90px;"><span>商家视频列表</span></div>
                <div class="navigation_left_end"></div>
            </div>
            <div class="navigation_center">
                <div class="option_add" onclick="MenuAddShow('Menu_Add');"></div>
            </div>
        </div>

        <div class="shopsdishlist">
            <div class="shopsdishlist_center">
                <div class="shopsdishlist_th">
                    <div class="shopsdishlist_th_1">编号</div>
                    <div class="shopsdishlist_th_2">视频名</div>
                    <div class="shopsdishlist_th_3">标签</div>
                    <div class="shopsdishlist_th_4">上传时间</div>
                    <div class="shopsdishlist_th_5">点播次数</div>
                    <div class="shopsdishlist_th_6">排序</div>
                    <div class="shopsdishlist_th_7">视频上传</div>
                </div>
            </div>
        </div>
    </div>
    <!--  添加菜品  -->
    <div id="Menu_Add" class="menu_add" >
        <div class="menu_add_row">
            <div class="menu_add_font">视频名称：</div>
            <div><input name="name" type="text" runat="server" id="name"/></div>
        </div>
        <div class="menu_add_row">
            <div class="menu_add_font">视频编号：</div>
            <div><input name="number" type="text" runat="server" id="number"/></div>
        </div>
        
        <div class="menu_add_row">
            <div class="menu_add_font">视频标签：</div>
            <div><input name="tab" type="text" runat="server" id="tab"/></div>
        </div>
        <div class="menu_add_row menu_add_area">
            <div class="menu_add_font">视频摘要：</div>
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
            <div class="menu_update_font">视频名称：</div>
            <div><input name="nameUpdate" type="text" style="color:#AAAAAA;" readonly/></div>
        </div>
        <div class="menu_update_row">
            <div class="menu_update_font">视频编号：</div>
            <div><input name="numberUpdate" type="text" /></div>
        </div>
        
        <div class="menu_update_row">
            <div class="menu_update_font">视频标签：</div>
            <div><input name="tabUpdate" type="text" /></div>
        </div>
        <div class="menu_update_row menu_update_area">
            <div class="menu_update_font">视频摘要：</div>
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

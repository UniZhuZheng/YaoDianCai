<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopCallManager.aspx.cs" Inherits="Uni.WebMenu.page.ShopCallManager" %>

<!DOCTYPE html>
<html>
<head>
    <title>呼叫界面</title>
    <link href="../res/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link href="/res/css/shopcall.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/res/js/jquery.min.js"></script>
    <script src="../res/js/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript" src="/res/js/layer/layer.min.js"></script>
    <script type="text/javascript" src="/res/js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" src="/res/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/res/js/jquery.pagination.js"></script>
    <script type="text/javascript" src="/res/js/datepicker.js" ></script>
    <script type="text/javascript" src="/res/js/shoptop.js"></script>
    <script type="text/javascript" src="/res/js/shopcall.js"></script>
    
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
                <div class="navigation_left_middle" style="width:90px;"><span>呼叫列表</span></div>
                <div class="navigation_left_end"></div>
            </div>
            <div class="navigation_center">
                <input name="search" class="inputDate" id='inputDate' readonly />
                <div class="navigation_center_search_cancle" onclick="cancleSearch();"></div>
            </div>
        </div>

        <div class="shopsdishlist">
            <div class="shopsdishlist_center">
                <div class="shopsdishlist_th">
                    <div class="shopsdishlist_th_1">桌号</div>
                    <div class="shopsdishlist_th_2">内容</div>
                    <div class="shopsdishlist_th_3">时间</div>
                </div>
            </div>
        </div>
        <div class="pagination">
            <div id="Pagination">
            </div>
        </div>
    </div>
    
</body>
</html>

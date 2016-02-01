<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopFileUpload.aspx.cs" Inherits="Uni.WebMenu.Page.ShopFileUpload" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>商家上传界面</title> 
    <link   type="text/css" rel="stylesheet" href="/res/css/Shops-option.css" />
    <script type="text/javascript" src="/res/js/jquery.min.js"></script>
    <script type="text/javascript" src="/res/js/jquery.form.js"></script>
    <script type="text/javascript" src="/res/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/res/js/shoptop.js"></script>
</head>

<body>
    <form id="form" runat="server">
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
                <div class="navigation_left_middle" style="width:90px;"><span>商家菜品操作</span></div>
                <div class="navigation_left_end"></div>
            </div>
        </div>

        <div class="shopsfileupload">
            <div class="shopsfileupload_top">
                <div class="fileupload_top_font">请先导入菜单数据</div>
            </div>

            <div class="fileupload_upload">
                <div class="fileupload_upload_title">
                    <div class="fileupload_upload_num"></div>
                    <div class="fileupload_upload_font">导入文件</div>
                </div>
                <div class="fileupload_upload_input">
                    <asp:FileUpload  ID="filepath" runat="server" style="width:300px;"/>
                    <asp:Button ID="btnupfile"  runat="server" Text="上传" onclick="btnupfile_Click" />
                </div>
            </div>

            <div class="shops_template">
                <div class="shops_template_font">
                    <div class="template_font_img"></div>
                    <div class="template_font_font">模板选择</div>
                </div>
                <div class="shops_template_content">
                    <div class="template_select" onclick="templateSelect_Click">
                        <div class="template_select_top">
                            <div class="template_select_top_img template_select_top_img_select"></div>
                            <div class="template_select_top_font">默认模板一</div>
                        </div>
                        <div class="template_select_img"></div>
                    </div>
                    <!--
                    <div class="template_select">
                        <div class="template_select_top">
                            <div class="template_select_top_img"></div>
                            <div class="template_select_top_font">默认模板二</div>
                        </div>
                        <div class="template_select_img"></div>
                    </div>

                    <div class="template_select">
                        <div class="template_select_top">
                            <div class="template_select_top_img"></div>
                            <div class="template_select_top_font">默认模板三</div>
                        </div>
                        <div class="template_select_img"></div>
                    </div>

                    <div class="template_select">
                        <div class="template_select_top">
                            <div class="template_select_top_img"></div>
                            <div class="template_select_top_font">默认模板四</div>
                        </div>
                        <div class="template_select_img"></div>
                    </div>-->
                </div>
            </div>

            <div class="fileupload_upload_info">
                <div class="upload_info_name">
                    <div ID="uploadInfoNameImg" runat="server" class="upload_info_name_img"></div>
                    <div class="upload_info_name_font">商家名称：</div>
                    <div ID="uploadInfoNameInfo" runat="server" class="upload_info_name_info" style="display:block;"></div>
                    <div ID="uploadInfoNameStatus" runat="server" class="upload_info_name_status">商家点名获取失败</div>
                </div>

                <div class="upload_info_table">
                    <div ID="uploadInfoTableImg" runat="server" class="upload_info_table_img"></div>
                    <div class="upload_info_table_font">餐桌解析：</div>
                    <div ID="uploadInfoTableInfo" runat="server" class="upload_info_table_info">商家餐桌解析成功</div>
                    <div ID="uploadInfoTableStatus" runat="server" class="upload_info_table_status">商家餐桌解析失败</div>
                </div>
                <div class="upload_info_dish">
                    <div ID="uploadInfoDishImg" runat="server" class="upload_info_dish_img"></div>
                    <div class="upload_info_dish_font">菜品解析：</div>
                    <div ID="uploadInfoDishInfo" runat="server" class="upload_info_dish_info">商家菜品解析成功</div>
                    <div ID="uploadInfoDishStatus" runat="server" class="upload_info_dish_status">商家菜品解析失败</div>
                </div>
                <div class="upload_info_pic">
                    <div ID="uploadInfoPicImg" runat="server" class="upload_info_pic_img"></div>
                    <div class="upload_info_pic_font">图片解析：</div>
                    <div ID="uploadInfoPicInfo" runat="server" class="upload_info_pic_info">商家菜品图片解析成功</div>
                    <div ID="uploadInfoPicStatus" runat="server" class="upload_info_pic_status">商家菜品图片解析失败</div>
                </div>
            </div>

            

            <div class="shopsfileupload_bottom">
                <div class="upload_bottom_button">
                    <div ><asp:Button class="bottom_button_save" ID="btnCreatMenu" runat="server" onclick="btnCreatMenu_Click" /></div>
                    <div ><asp:Button class="bottom_button_cancle" ID="btnCalcle"  runat="server" onclick="btnClose_Click" /></div>
                </div>
            </div>
        </div>

        <div class="option">
            <div class="option_context">
            </div>
        </div>
    </div>
    
    </form>
</body>
</html>

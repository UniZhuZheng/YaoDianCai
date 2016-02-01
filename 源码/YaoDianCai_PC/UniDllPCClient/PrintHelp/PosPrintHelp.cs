using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniDllPCClient.EntityHelp;

namespace UniDllPCClient.PrintHelp
{
    public class PosPrintHelp
    {
        private const int POS_OPEN_NETPORT = 0x15;//打开网络接口
        private const int POS_SUCCESS = 1001; // 函数执行成功
        private const int POS_FONT_TYPE_STANDARD = 0x00; // 标准 ASCII
        private const int POS_PRINT_MODE_STANDARD = 0x00;// 标准模式（行模式）
        private const int POS_FONT_STYLE_NORMAL = 0x00;// 正常
        private const int POS_FONT_TYPE_CHINESE = 0x03; // 标准 “宋体”
        private const int POS_FONT_STYLE_THICK_UNDERLINE = 0x100; // 2点粗的下划线
        private const int POS_FONT_STYLE_THIN_UNDERLINE = 0x80; // 1点粗的下划线
        private const int POS_CUT_MODE_FULL = 0x00; // 全切
        private const string PrintLine = "------------------------------";
        public static string IPAddress = "192.168.123.200";

        public static bool OpenPrint()
        {
            bool flag = true;
            IntPtr open = PosPrintMethods.POS_Open(IPAddress, 0, 0, 0, 0, POS_OPEN_NETPORT);
            if (open.ToInt32() == -1)
            {
                flag = false;
            }
            return flag;
        }

        public static bool OpenPrint(string IP)
        {
            bool flag = true;
            IntPtr open = PosPrintMethods.POS_Open(IP, 0, 0, 0, 0, POS_OPEN_NETPORT);
            if (open.ToInt32() == -1)
            {
                flag = false;
            }
            return flag;
        }

        // "               要点菜               "  
        // "桌号:餐桌号                         " 
        // "时间: 2014/12/12 00:00:00           "
        // "编号      菜品名    数量   金额     "  
        // "------------------------------------"  
        // "  1      宫保鸡丁    X1    30元     "  
        // "  2        莲藕      X1    30元     "  
        // "  3       叫化鸡     X1    30元     " 
        // "------------------------------------" 
        // "合计                 X3    90元     "  
        // "------------------------------------"  
        // "                 要点菜无限点菜系统 " 
        public static bool PrintBillEntity(BillEntity billentity, string shopname)
        {
            int nRet = PosPrintMethods.POS_SetMotionUnit(180, 180);
            if (POS_SUCCESS != nRet)
            {
                return false;
            }
            PosPrintMethods.POS_SetMode(POS_PRINT_MODE_STANDARD);
            PosPrintMethods.POS_SetRightSpacing(0);
            PosPrintMethods.POS_SetLineSpacing(150);
            PosPrintMethods.POS_S_TextOut(shopname, 150, 1, 2, POS_FONT_TYPE_STANDARD, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_SetLineSpacing(35);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("餐桌号:" + billentity.TableName, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("时间:" + billentity.CreateDate, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("编号 菜品名     数量   金额", 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            for (int i = 0; i < billentity.Orders.Count; i++)
            {
                StringBuilder menu = new StringBuilder();
                menu.Append((i + 1).ToString());
                menu.Append(" ");
                menu.Append(billentity.Orders[i].DishName);
                menu.Append("     ");
                menu.Append("X" + billentity.Orders[i].DishCount);
                menu.Append("      ");
                menu.Append(billentity.Orders[i].DishPrice);
                PosPrintMethods.POS_S_TextOut(menu.ToString(), 25, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
            }
            PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            StringBuilder total = new StringBuilder();
            total.Append("合计:");
            total.Append("           ");
            total.Append("X" + billentity.TotalCount);
            total.Append("    ");
            total.Append(billentity.TotalPrice);
            PosPrintMethods.POS_S_TextOut(total.ToString(), 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("要点菜无限菜单系统", 80, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_SetLineSpacing(24);

            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            // 切纸
            PosPrintMethods.POS_CutPaper(POS_CUT_MODE_FULL, 0);
            return true;
        }

        // "               要点菜               "
        // "团购单                              " 
        // "------------------------------------"
        // "桌号: 餐桌号                        "
        // "时间: 2014/12/12 00:00:00           "
        // "团购商家:大众点评                   "
        // "团购商家:12345678900                "
        // "团购商家:0000000000                 "
        // "------------------------------------"
        // "         要点菜无限点菜系统         "
        public static bool PrintTuanEntity(List<TuanEntity> tuanentityinfolist, string shopname)
        {
            int nRet = PosPrintMethods.POS_SetMotionUnit(180, 180);
            if (POS_SUCCESS != nRet)
            {
                return false;
            }
            PosPrintMethods.POS_SetMode(POS_PRINT_MODE_STANDARD);
            PosPrintMethods.POS_SetRightSpacing(0);
            PosPrintMethods.POS_SetLineSpacing(150);
            PosPrintMethods.POS_S_TextOut(shopname, 150, 1, 2, POS_FONT_TYPE_STANDARD, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_SetLineSpacing(35);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("团购单", 150, 1, 1, POS_FONT_TYPE_STANDARD, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            for (int i = 0; i < tuanentityinfolist.Count; i++)
            {
                PosPrintMethods.POS_S_TextOut("餐桌号:" + tuanentityinfolist[i].TableName, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
                PosPrintMethods.POS_S_TextOut("时间:" + tuanentityinfolist[i].CreateDate, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
                PosPrintMethods.POS_S_TextOut("团购商家:" + tuanentityinfolist[i].Website, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
                PosPrintMethods.POS_S_TextOut("客户电话:" + tuanentityinfolist[i].Phone, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
                PosPrintMethods.POS_S_TextOut("团购编号:" + tuanentityinfolist[i].Number, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
                PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
                PosPrintMethods.POS_FeedLine();
            }

            PosPrintMethods.POS_S_TextOut("要点菜无限菜单系统", 80, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_SetLineSpacing(24);

            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            // 切纸
            PosPrintMethods.POS_CutPaper(POS_CUT_MODE_FULL, 0);
            return true;
        }

        public static bool PrintTuanEntity(TuanEntity tuanentityinfo, string shopname)
        {
            int nRet = PosPrintMethods.POS_SetMotionUnit(180, 180);
            if (POS_SUCCESS != nRet)
            {
                return false;
            }
            PosPrintMethods.POS_SetMode(POS_PRINT_MODE_STANDARD);
            PosPrintMethods.POS_SetRightSpacing(0);
            PosPrintMethods.POS_SetLineSpacing(150);
            PosPrintMethods.POS_S_TextOut(shopname, 150, 1, 2, POS_FONT_TYPE_STANDARD, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_SetLineSpacing(35);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("团购单", 150, 1, 1, POS_FONT_TYPE_STANDARD, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();

            PosPrintMethods.POS_S_TextOut("餐桌号:" + tuanentityinfo.TableName, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("时间:" + tuanentityinfo.CreateDate, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("团购商家:" + tuanentityinfo.Website, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("客户电话:" + tuanentityinfo.Phone, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut("团购编号:" + tuanentityinfo.Number, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_S_TextOut(PrintLine, 15, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            
            PosPrintMethods.POS_S_TextOut("要点菜无限菜单系统", 80, 1, 1, POS_FONT_TYPE_CHINESE, POS_FONT_STYLE_NORMAL);
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_SetLineSpacing(24);

            PosPrintMethods.POS_FeedLine();
            PosPrintMethods.POS_FeedLine();
            // 切纸
            PosPrintMethods.POS_CutPaper(POS_CUT_MODE_FULL, 0);
            return true;
        }
    }
}

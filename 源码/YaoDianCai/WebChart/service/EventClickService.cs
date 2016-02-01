using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniDaoChart;
using UniDaoChart.Entity;
using UniDllChart.Service;

namespace WebChart.service
{
    public class EventClickService
    {
        /*
         * MainAction.processRequest
         * */
        public static string GetShopList() {
            List<ShopEntity> lists=WebChatDao.GetShopList();
            if (lists.Count > 0)
            {
                string str = "";
                for (int i = 0; i < lists.Count;i++ )
                {
                    str += "商家名：" + lists[i].Name + "\n商家地址：" + lists[i].Address;
                    str += "\n--------------------------------\n";
                }
                return str;
            }
            else {
                return "暂无商家信息。";
            }
        }
        /*
         * MainAction.processRequest
         * */
        public static string GetInfoByDay(string openId)
        {
            string SID = WebChatService.IsExits(openId);
            string respContent;
            if ("".Equals(SID) || SID == null)
            {
                respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
            }
            else
            {
                DateTime startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime endDate = startDate.AddDays(1);
                int[] counts = WebChatService.GetCountByDate(SID, startDate, endDate);
                respContent = "当日下单数：" + counts[0] + "\n" +
                    "当日团购数：" + counts[1] + "\n" +
                    "当日认证数：" + counts[2] + "\n";
            }
            return respContent;
        }
        /*
         * MainAction.processRequest
         * */
        public static string GetInfoByMonth(string openId) {
            string SID = WebChatService.IsExits(openId);
            string respContent;
            if ("".Equals(SID) || SID == null)
            {
                respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
            }
            else
            {
                DateTime startDate = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01"));
                DateTime endDate = startDate.AddMonths(1);
                int[] counts = WebChatService.GetCountByDate(SID, startDate, endDate);
                respContent = "当月下单数：" + counts[0] + "\n" +
                    "当月团购数：" + counts[1] + "\n" +
                    "当月认证数：" + counts[2] + "\n";
            }
            return respContent;
        }
        /*
         * MainAction.processRequest
         * */
        public static string GetInfoByAll(string openId) {
            string SID = WebChatService.IsExits(openId);
            string respContent;
            if ("".Equals(SID) || SID == null)
            {
                respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
            }
            else
            {
                DateTime startDate = Convert.ToDateTime("2014-06-03");
                DateTime endDate = DateTime.Now;
                int[] counts = WebChatService.GetCountByDate(SID, startDate, endDate);
                respContent = "全部下单数：" + counts[0] + "\n" +
                    "全部团购数：" + counts[1] + "\n" +
                    "全部认证数：" + counts[2] + "\n";
            }
            return respContent;
        }
        /*
         * MainAction.processRequest
         * */
        public static string GetOrderInfo(string openId)
        {
            string SID = WebChatService.IsExits(openId);
            string respContent;
            if ("".Equals(SID) || SID == null)
            {
                respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
            }
            else
            {
                DateTime startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime endDate = startDate.AddDays(1);
                List<BillEntity> lists = WebChatDao.GetBillList(SID, startDate, endDate);
                if (lists.Count > 0)
                {
                    string str = "";
                    for (int i = 0; i < lists.Count; i++)
                    {
                        str += "桌号：" + lists[i].TableName + "\n下单号：" +
                            lists[i].BID + "\n总金额：" + lists[i].Price + "\n菜品数量：" +
                            lists[i].DishCount + "\n下单时间：" + lists[i].CreateDate;
                        str += "\n--------------------------------\n";
                    }
                    respContent = str;
                }
                else
                {
                    respContent = "暂无下单信息。";
                }
            }
            return respContent;
        }
        /*
         * MainAction.processRequest
         * */
        public static string BandShop(string openId)
        {
            string SID = WebChatService.IsExits(openId);
            string respContent;
            if ("".Equals(SID) || SID == null)
            {
                respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
            }
            else
            {
                ShopEntity se = WebChatDao.Query(openId);
                if (se == null)
                {
                    respContent = "商家信息获取失败。";
                }
                else {
                    respContent = "你绑定的商家为：\n"+
                        "商家名："+se.Name+"\n"+
                        "商家账号："+se.Account+"\n"+
                        "商家地址："+se.Address;
                }
            }
            return respContent;
        }
    }
}
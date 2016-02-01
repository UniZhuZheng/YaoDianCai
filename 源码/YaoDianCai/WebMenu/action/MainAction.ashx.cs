using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.action
{
    /// <summary>
    /// CollectAction 的摘要说明
    /// </summary>
    public class MainAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_CurrentDay": 
                    content = CurrentDay(context);
                    break;

                case "OP_CurrentMonthOrder": 
                    content = CurrentMonthOrder(context);
                    break;

                case "OP_CurrentMonthTuan": 
                    content = CurrentMonthTuan(context);
                    break;
                case "OP_CurrentMonthAuth": 
                    content = CurrentMonthAuth(context);
                    break;
                case "OP_CurrentYear": 
                    content = CurrentYear(context);
                    break;
                case "OP_GetBillSales":
                    content = GetBillSales(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string GetBillSales(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            DateTime curDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime startDate = curDate.AddDays(-1);
            DateTime endDate = curDate.AddDays(1);
            string str = BillService.GetBillSales(sid, startDate, endDate);
            return str;
        }

        private string CurrentDay(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            DateTime startDate =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime endDate = startDate.AddDays(1);
            //获取今日下单数
            int numOrder = BillService.GetBillListCountByDate(sid, startDate, endDate);
            //获取今日团购数
            int numTuan = TuanService.GetBillListCountByDate(sid, startDate, endDate);
            //获取今日认证数
            int numAuth = WifiGWService.getAuthCount(sid, startDate, endDate);
            return "{\"ok\":true,\"order\":"+numOrder+",\"tuan\":"+numTuan+",\"auth\":"+numAuth+"}";
        }

        private string CurrentMonthOrder(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            DateTime startDate = Convert.ToDateTime((DateTime.Now.Year+"-"+DateTime.Now.Month+"-01"));
            DateTime endDate = startDate.AddMonths(1);
            string str = BillService.GetCurrentMonthOrder(sid, startDate, endDate);
            return str;
        }

        private string CurrentMonthTuan(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            DateTime startDate = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01"));
            DateTime endDate = startDate.AddMonths(1);
            string str = TuanService.GetCurrentMonthTuan(sid, startDate, endDate);
            return str;
        }

        private string CurrentMonthAuth(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            DateTime startDate = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01"));
            DateTime endDate = startDate.AddMonths(1);
            string str = WifiGWService.GetCurrentMonthAuth(sid, startDate, endDate);
            return str;
        }

        private string CurrentYear(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime((DateTime.Now.Year + "-01-01"));
            DateTime endDate = startDate.AddYears(1);
            //获取今日下单数
            string Order = BillService.GetCurrentYearOrder(sid, startDate, endDate);
            //获取今日团购数
            string Tuan = TuanService.GetCurrentYearTuan(sid, startDate, endDate);
            //获取今日认证数
            string Auth = WifiGWService.GetCurrentYearAuth(sid, startDate, endDate);
            return "{\"ok\":true,\"orderlists\":" + Order + ",\"tuanlists\":" + Tuan + ",\"authlists\":" + Auth + "}";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
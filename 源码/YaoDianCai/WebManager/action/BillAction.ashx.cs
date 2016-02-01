using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.WebManager.Action
{
    public class BillAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ShopBillInfoLimit":
                    content = ShopBillInfoLimit(context);
                    break;

                case "OP_ShopBillInfoLimit_Count":
                    content = ShopBillInfoLimit_Count(context);
                    break;

                case "OP_ShopBillInfoSearchLimit":
                    content = ShopBillInfoSearchLimit(context);
                    break;

                case "OP_ShopBillInfoSearchLimit_Count":
                    content = ShopBillInfoSearchLimit_Count(context);
                    break;

                case "OP_BillInfoByShopLimit":
                    content = BillInfoByShopLimit(context);
                    break;

                case "OP_BillInfoByShopLimit_Count":
                    content = BillInfoByShopLimit_Count(context);
                    break;

                case "OP_BillInfoByDayLimit":
                    content = BillInfoByDayLimit(context);
                    break;

                case "OP_BillInfoByDayLimit_Count":
                    content = BillInfoByDayLimit_Count(context);
                    break;

                case "OP_BillInfoByMonthLimit":
                    content = BillInfoByMonthLimit(context);
                    break;

                case "OP_BillInfoByMonthLimit_Count":
                    content = BillInfoByMonthLimit_Count(context);
                    break;

                case "OP_ShowDishRecord":
                    content = ShowDishRecord(context);
                    break;
            }
            
            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }


        private string ShopBillInfoLimit(HttpContext context)
        {
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopEntity> list = BillService.GetShop(firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"area\":\"" + list[i].HostId + "\",";
                str += "\"hostName\":\"" + list[i].HostName + "\",";
                str += "\"count\":\"" + list[i].BillCount + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }

        private string ShopBillInfoLimit_Count(HttpContext context)
        {
            int num = BillService.GetShopCount();
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopBillInfoSearchLimit(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopEntity> list = BillService.GetShopSearch(key, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"area\":\"" + list[i].HostId + "\",";
                str += "\"hostName\":\"" + list[i].HostName + "\",";
                str += "\"count\":\"" + list[i].BillCount + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }

        private string ShopBillInfoSearchLimit_Count(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];

            int num = BillService.GetSearchCount(key);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string BillInfoByShopLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<BillEntity> list = BillService.GetBillList(sid, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"tableName\":\"" + list[i].TableName + "\",";
                str += "\"BID\":\"" + list[i].BID + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\",";
                str += "\"count\":\"" + list[i].DishCount + "\",";
                str += "\"price\":\"" + list[i].Price + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }

        private string BillInfoByShopLimit_Count(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int num = BillService.GetBillListCount(sid);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string BillInfoByDayLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);

            List<BillEntity> list = BillService.GetBillListByDate(sid, startDate, endDate, firstResult, maxResult);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"tableName\":\"" + list[i].TableName + "\",";
                str += "\"BID\":\"" + list[i].BID + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\",";
                str += "\"count\":\"" + list[i].DishCount + "\",";
                str += "\"price\":\"" + list[i].Price + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }

        private string BillInfoByDayLimit_Count(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);
            int num = BillService.GetBillListCountByDate(sid, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string BillInfoByMonthLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);

            List<BillEntity> list = BillService.GetBillListByDate(sid, startDate, endDate, firstResult, maxResult);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"tableName\":\"" + list[i].TableName + "\",";
                str += "\"BID\":\"" + list[i].BID + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\",";
                str += "\"count\":\"" + list[i].DishCount + "\",";
                str += "\"price\":\"" + list[i].Price + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }

        private string BillInfoByMonthLimit_Count(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);
            int num = BillService.GetBillListCountByDate(sid, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShowDishRecord(HttpContext context)
        {
            string bid = context.Request.Params["BID"];
            List<DishRecordEntity> list = DishRecordService.GetDishRecord(bid);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"id\":\"" + list[i].Id + "\",";
                str += "\"dishNum\":\"" + list[i].Number + "\",";
                str += "\"dishName\":\"" + list[i].Name + "\",";
                str += "\"dishPrice\":\"" + list[i].Price + "\",";
                str += "\"dishCount\":\"" + list[i].Count + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }
    }
}
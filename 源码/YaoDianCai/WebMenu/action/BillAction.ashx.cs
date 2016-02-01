using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;
using Uni.YDC.Dao.Menu.Entity;

namespace Uni.WebMenu.Action
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
                case "OP_CreateBill":
                    content = CreateBill(context);
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

        private string CreateBill(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string json = context.Request["BillJson"];
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            string returnStr = BillService.AddBill(sid, json, cartStatus);
            if (string.IsNullOrEmpty(returnStr))
            {
                return "{\"ok\":false}";
            }
            else {
                if (returnStr.Split('-')[0].Equals("false"))
                {
                    return "{\"ok\":false,\"BID\":\"" + returnStr.Split('-')[1] + "\"}";
                }
                else {
                    return "{\"ok\":true,\"BID\":\"" + returnStr.Split('-')[1] + "\"}";
                }
            }

            
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
                str += "\"createDate\":\"" + list[i].CreateDate + "\",";
                str += "\"count\":\"" + list[i].TotalCount + "\",";
                str += "\"price\":\"" + list[i].TotalPrice + "\"";
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
                str += "\"createDate\":\"" + list[i].CreateDate + "\",";
                str += "\"count\":\"" + list[i].TotalCount + "\",";
                str += "\"price\":\"" + list[i].TotalPrice + "\"";
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
                str += "\"createDate\":\"" + list[i].CreateDate + "\",";
                str += "\"count\":\"" + list[i].TotalCount + "\",";
                str += "\"price\":\"" + list[i].TotalPrice + "\"";
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
            string sid = context.Request.Params["SID"];
            List<OrderEntity> list = OrderService.GetOrderDish(sid,bid);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"id\":\"" + list[i].DishNumber + "\",";
                str += "\"dishNum\":\"" + list[i].DishNumber + "\",";
                str += "\"dishName\":\"" + list[i].DishName + "\",";
                str += "\"dishPrice\":\"" + list[i].DishPrice + "\",";
                str += "\"dishCount\":\"" + list[i].DishCount + "\"";
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
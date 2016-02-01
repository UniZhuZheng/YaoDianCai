using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.WebManager.Action
{
    public class TuanAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ShopTuanInfoLimit":
                    content = ShopTuanInfoLimit(context);
                    break;

                case "OP_ShopTuanInfoLimit_Count":
                    content = ShopTuanInfoLimit_Count(context);
                    break;

                case "OP_ShopTuanInfoSearchLimit":
                    content = ShopTuanInfoSearchLimit(context);
                    break;

                case "OP_ShopTuanInfoSearchLimit_Count":
                    content = ShopTuanInfoSearchLimit_Count(context);
                    break;

                case "OP_TuanInfoByShopLimit":
                    content = TuanInfoByShopLimit(context);
                    break;

                case "OP_TuanInfoByShopLimit_Count":
                    content = TuanInfoByShopLimit_Count(context);
                    break;

                case "OP_TuanInfoByDayLimit":
                    content = TuanInfoByDayLimit(context);
                    break;

                case "OP_TuanInfoByDayLimit_Count":
                    content = TuanInfoByDayLimit_Count(context);
                    break;

                case "OP_TuanInfoByMonthLimit":
                    content = TuanInfoByMonthLimit(context);
                    break;

                case "OP_TuanInfoByMonthLimit_Count":
                    content = TuanInfoByMonthLimit_Count(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }


        private string ShopTuanInfoLimit(HttpContext context)
        {
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopEntity> list = TuanService.GetShop(firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"hostName\":\"" + list[i].HostName + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"groupSubCount\":\"" + list[i].TuanCount + "\"";
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

        private string ShopTuanInfoLimit_Count(HttpContext context)
        {
            int num = TuanService.GetShopCount();
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopTuanInfoSearchLimit(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopEntity> list = TuanService.GetShopSearch(key, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"hostName\":\"" + list[i].HostName + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"groupSubCount\":\"" + list[i].TuanCount + "\"";
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

        private string ShopTuanInfoSearchLimit_Count(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];

            int num = TuanService.GetSearchCount(key);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string TuanInfoByShopLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<TuanEntity> list = TuanService.GetTuanList(sid, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"number\":\"" + list[i].Number + "\",";
                str += "\"webSite\":\"" + list[i].Website + "\",";
                str += "\"owner\":\"" + list[i].Owner + "\",";
                str += "\"phone\":\"" + list[i].Phone + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].ShopName + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\"";
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

        private string TuanInfoByShopLimit_Count(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int num = TuanService.GetTuanListCount(sid);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string TuanInfoByDayLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);

            List<TuanEntity> list = TuanService.GetTuanListByDate(sid, startDate, endDate, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"number\":\"" + list[i].Number + "\",";
                str += "\"webSite\":\"" + list[i].Website + "\",";
                str += "\"owner\":\"" + list[i].Owner + "\",";
                str += "\"phone\":\"" + list[i].Phone + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].ShopName + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\"";
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

        private string TuanInfoByDayLimit_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);
            int num = TuanService.GetBillListCountByDate(SID, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string TuanInfoByMonthLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);

            List<TuanEntity> list = TuanService.GetTuanListByDate(sid, startDate, endDate, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"number\":\"" + list[i].Number + "\",";
                str += "\"webSite\":\"" + list[i].Website + "\",";
                str += "\"owner\":\"" + list[i].Owner + "\",";
                str += "\"phone\":\"" + list[i].Phone + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].ShopName + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\"";
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


        private string TuanInfoByMonthLimit_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);
            int num = TuanService.GetBillListCountByDate(SID, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }
    }
}
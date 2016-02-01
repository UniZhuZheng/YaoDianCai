using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.WebManager.Action
{
    public class WifiAuthAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_WifiAuthByGWLimit":
                    content = WifiAuthByGWLimit(context);
                    break;

                case "OP_WifiAuthByGWLimit_Count":
                    content = WifiAuthByGWLimit_Count(context);
                    break;

                case "OP_WifiAuthByDayLimit":
                    content = WifiAuthByDayLimit(context);
                    break;

                case "OP_WifiAuthByDayLimit_Count":
                    content = WifiAuthByDayLimit_Count(context);
                    break;

                case "OP_WifiAuthByMonthLimit":
                    content = WifiAuthByMonthLimit(context);
                    break;

                case "OP_WifiAuthByMonthLimit_Count":
                    content = WifiAuthByMonthLimit_Count(context);
                    break;

                case "OP_WifiAuthDistinction_Count":
                    content = WifiAuthDistinction_Count(context);
                    break;
                case "OP_WifiAuthDistinction_List":
                    content = WifiAuthDistinction_List(context);
                    break;
                case "OP_WifiAuthDistinctionSearch_Count":
                    content = WifiAuthDistinctionSearch_Count(context);
                    break;
                case "OP_WifiAuthDistinctionSearch_List":
                    content = WifiAuthDistinctionSearch_List(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string WifiAuthDistinctionSearch_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            string searchKey = context.Request.Params["searchKey"];
            int num = WifiAuthService.GetDistinctionCount(SID, searchKey);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WifiAuthDistinctionSearch_List(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            string searchKey = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            List<object[]> lists = WifiAuthService.GetDistinctionList(SID,searchKey, firstResult, maxResult);
            int size = lists.Count;
            if (size <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < size; i++)
            {
                object[] obj = lists[i];
                str += "{";
                str += "\"MAC\":\"" + obj[0] + "\",";
                str += "\"MACCount\":\"" + obj[1] + "\"";
                if (i == (size - 1))
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

        private string WifiAuthDistinction_List(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            List<object[]> lists = WifiAuthService.GetDistinctionList(SID, firstResult, maxResult);
            int size = lists.Count;
            if (size <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < size; i++)
            {
                object[] obj = lists[i];
                str += "{";
                str += "\"MAC\":\"" + obj[0] + "\",";
                str += "\"MACCount\":\"" + obj[1] + "\"";
                if (i == (size - 1))
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

        private string WifiAuthDistinction_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            int num = WifiAuthService.GetDistinctionCount(SID);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        
        private string WifiAuthByGWLimit(HttpContext context)
        {
            string gwId = context.Request.Params["gwId"];

            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            List<WifiAuthEntity> list = WifiAuthService.GetWifiAuth(gwId, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"gwId\":\"" + list[i].GwId + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"token\":\"" + list[i].Token + "\",";
                str += "\"mac\":\"" + list[i].Mac + "\",";
                str += "\"userIp\":\"" + list[i].UserIp + "\",";
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

        private string WifiAuthByGWLimit_Count(HttpContext context)
        {
            string gwId = context.Request.Params["gwId"];
            int num = WifiAuthService.GetCount(gwId);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WifiAuthByDayLimit(HttpContext context)
        {
            string gwId = context.Request.Params["gwId"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);

            List<WifiAuthEntity> list = WifiAuthService.GetWifiAuthByDate(gwId, startDate, endDate, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"gwId\":\"" + list[i].GwId + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"token\":\"" + list[i].Token + "\",";
                str += "\"mac\":\"" + list[i].Mac + "\",";
                str += "\"userIp\":\"" + list[i].UserIp + "\",";
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

        private string WifiAuthByDayLimit_Count(HttpContext context)
        {
            string gwId = context.Request.Params["gwId"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);

            int num = WifiAuthService.GetCountByDate(gwId, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WifiAuthByMonthLimit(HttpContext context)
        {
            string gwId = context.Request.Params["gwId"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);

            List<WifiAuthEntity> list = WifiAuthService.GetWifiAuthByDate(gwId, startDate, endDate, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"gwId\":\"" + list[i].GwId + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"token\":\"" + list[i].Token + "\",";
                str += "\"mac\":\"" + list[i].Mac + "\",";
                str += "\"userIp\":\"" + list[i].UserIp + "\",";
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

        private string WifiAuthByMonthLimit_Count(HttpContext context)
        {
            string gwId = context.Request.Params["gwId"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);

            int num = WifiAuthService.GetCountByDate(gwId, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }
    }
}
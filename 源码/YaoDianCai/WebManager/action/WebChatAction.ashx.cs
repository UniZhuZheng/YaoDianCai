using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.WebManager.action
{
    /// <summary>
    /// WebChatAction 的摘要说明
    /// </summary>
    public class WebChatAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ShopWebChatInfoLimit":
                    content = WebChatInfoLimit(context);
                    break;

                case "OP_ShopWebChatInfoLimit_Count":
                    content = WebChatInfoLimit_Count(context);
                    break;

                case "OP_ShopWebChatInfoSearchLimit":
                    content = WebChatInfoSearchLimit(context);
                    break;

                case "OP_ShopWebChatInfoSearchLimit_Count":
                    content = WebChatInfoSearchLimit_Count(context);
                    break;

                case "OP_WebChatInfoByShopLimit":
                    content = WebChatInfoByShopLimit(context);
                    break;

                case "OP_WebChatInfoByShopLimit_Count":
                    content = WebChatInfoByShopLimit_Count(context);
                    break;

                case "OP_WebChatInfoByDayLimit":
                    content = WebChatInfoByDayLimit(context);
                    break;

                case "OP_WebChatInfoByDayLimit_Count":
                    content = WebChatInfoByDayLimit_Count(context);
                    break;

                case "OP_WebChatInfoByMonthLimit":
                    content = WebChatInfoByMonthLimit(context);
                    break;

                case "OP_WebChatInfoByMonthLimit_Count":
                    content = WebChatInfoByMonthLimit_Count(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string WebChatInfoByShopLimit(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            List<WebChatEntity> list = WebChatService.GetWebChatList(SID);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"openId\":\"" + list[i].OpenId + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"state\":" + list[i].State + ",";
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

        private string WebChatInfoByShopLimit_Count(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int num = WebChatService.GetWebChatListCount(sid);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WebChatInfoByDayLimit(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);
            List<WebChatEntity> list = WebChatService.GetWebChatList(SID, startDate, endDate);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"openId\":\"" + list[i].OpenId + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"state\":" + list[i].State + ",";
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

        private string WebChatInfoByDayLimit_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);
            int num = WebChatService.GetWebChatListCount(SID, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WebChatInfoByMonthLimit(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);
            List<WebChatEntity> list = WebChatService.GetWebChatList(SID, startDate, endDate);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"openId\":\"" + list[i].OpenId + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"state\":" + list[i].State + ",";
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

        private string WebChatInfoByMonthLimit_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);
            int num = WebChatService.GetWebChatListCount(SID, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WebChatInfoSearchLimit_Count(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];

            int num = WebChatService.GetSearchCount(key);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WebChatInfoSearchLimit(HttpContext context)
        {
            string search = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<object[]> list = WebChatService.GetWebChatList(search,firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"name\":\"" + list[i][0] + "\",";
                str += "\"SID\":\"" + list[i][1] + "\",";
                str += "\"hostName\":\"" + list[i][2] + "\",";
                str += "\"webchatCount\":\"" + list[i][3] + "\"";
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

        private string WebChatInfoLimit(HttpContext context)
        {
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<object[]> list = WebChatService.GetWebChatList(firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"name\":\"" + list[i][0] + "\",";
                str += "\"SID\":\"" + list[i][1] + "\",";
                str += "\"hostName\":\"" + list[i][2] + "\",";
                str += "\"webchatCount\":\"" + list[i][3] + "\"";
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

        private string WebChatInfoLimit_Count(HttpContext context)
        {
            int num = WebChatService.GetShopCount();
            return "{\"ok\":true,\"count\":" + num + "}";
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
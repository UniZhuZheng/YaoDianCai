using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Site.Service;
using Uni.YDC.Dao.Site.Entity;
namespace Uni.WebSite.action
{
    /// <summary>
    /// WifiGWAction 的摘要说明
    /// </summary>
    public class WifiGWAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_GetGWBySID":
                    content = GetWifiGWBySID(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string GetWifiGWBySID(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            List<WifiGWEntity> lists = WifiGWService.GetWifiGWBySID(SID);
            if (lists.Count > 0)
            {
                string str = "{\"ok\":ok,lists:[";
                for (int i = 0; i < lists.Count;i++ )
                {
                    WifiGWEntity we = lists[i];
                    if (i == (lists.Count - 1))
                    {
                        str += "{";
                        str += "\"SID\":\"" + we.SID + "\",";
                        str += "\"GwId\":\"" + we.GwId + "\",";
                        str += "\"Address\":\"" + we.Address + "\",";
                        str += "\"Port\":\"" + we.Port + "\"";
                        str += "}";

                    }
                    else {
                        str += "{";
                        str += "\"SID\":\"" + we.SID + "\",";
                        str += "\"GwId\":\"" + we.GwId + "\",";
                        str += "\"Address\":\"" + we.Address + "\",";
                        str += "\"Port\":\"" + we.Port + "\"";
                        str += "},";
                    }
                    
                }
                str+="]}";
                return str;
            }
            else {
                return "{\"ok\":false}";
            }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.action
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
            string str=WifiGWService.GetWifiGWBySID(SID);
            return str;
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
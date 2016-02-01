using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.action
{
    /// <summary>
    /// HostAction 的摘要说明
    /// </summary>
    public class HostAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string content = "{\"ok\":false}";
            string sid = context.Request.Params["SID"];
            if (!string.IsNullOrEmpty(sid) && sid.Length > 4)
            {
                string domain = WifiGWService.GetHostDomain(sid.Substring(0, 4));
                if (!string.IsNullOrEmpty(domain))
                {
                    content = "{\"ok\":true,\"domain\":\"" + domain + "\"}";
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);


            //string content = "{\"ok\":false}";
            //string sid = context.Request.Params["SID"];
            //string jsoncallback = context.Request.Params["jsoncallback"];
            //if (!string.IsNullOrEmpty(sid) && sid.Length > 4)
            //{
            //    string domain = WifiGWService.GetHostDomain(sid.Substring(0, 4));
            //    if (!string.IsNullOrEmpty(domain))
            //    {
            //        content = "{\"ok\":true,\"domain\":\"" + domain + "\"}";
            //    }
            //}

            //context.Response.ContentType = "text/plain";
            //context.Response.Write(jsoncallback + "(" + content + ")");
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
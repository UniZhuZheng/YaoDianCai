using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Site.Service;


namespace Uni.WebSite.WifiAuth
{

    public class login : IHttpHandler
    {
        public static string Template_Success = "<!DOCTYPE html><html><head><script type='text/javascript'>window.location.href = '{0}';</script></head><body></body></html>";
        public static string Template_Error   = "<!DOCTYPE html><html><head></head><body>你访问的页面不存在</body></html>";

        public bool IsReusable { get { return false; } }


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string gwId   = context.Request.Params["gw_id"];
            string gwAddr = context.Request.Params["gw_address"];
            string gwPort = context.Request.Params["gw_port"];
            string host   = context.Request.Params["REMOTE_HOST"];
            if (string.IsNullOrEmpty(gwId) || string.IsNullOrEmpty(gwAddr) || string.IsNullOrEmpty(gwPort) || string.IsNullOrEmpty(host))
            {
                context.Response.Write(Template_Error);
                return;
            }

            //判断网关编号是否存在
            if (WifiGWService.AuthWifiGW(gwId, gwAddr, gwPort, host))
            {
                string url = string.Format("http://{0}:{1}/wifidog/auth?token={2}-{3}", gwAddr, gwPort, gwId, DateTime.Now.ToString("yyyyMMdd"));
                context.Response.Write(string.Format(Template_Success, url));
            }
            else
            {
                context.Response.Write(Template_Error);
            }
        }
    }
}
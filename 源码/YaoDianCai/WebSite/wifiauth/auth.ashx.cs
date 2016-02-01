using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Site.Service;
using System.IO;


namespace Uni.WebSite.WifiAuth
{
    public class auth : IHttpHandler
    {
        public bool IsReusable { get { return false; } 
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string token = context.Request.Params["token"];
            string ip    = context.Request.Params["ip"];
            string mac   =HttpUtility.UrlDecode( context.Request.Params["mac"]).ToUpper();
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(mac))
            {
                context.Response.Write("Auth: 0");
                return;
            }

            int incoming = 0;
            if (!string.IsNullOrEmpty(context.Request.Params["incoming"]))
            {
                incoming = Convert.ToInt32(context.Request.Params["incoming"]);
            }

            int outcoming = 0;
            if (!string.IsNullOrEmpty(context.Request.Params["outgoing"]))
            {
                outcoming = Convert.ToInt32(context.Request.Params["outgoing"]);
            }


            string[] ss = token.Split('-');
            if (WifiGWService.IsGWIdExists(ss[0], mac) && ss[1].Equals(DateTime.Now.ToString("yyyyMMdd")))
            {
                WifiAuthService.RefreshAuthRecord(ss[0], token, mac, ip, incoming, outcoming);
                context.Response.Write("Auth: 1");
            }
            else
            {
                context.Response.Write("Auth: 0");
            }
        }
    }
}
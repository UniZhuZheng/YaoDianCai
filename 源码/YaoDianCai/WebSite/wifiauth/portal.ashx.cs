using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Site.Service;
using Uni.YDC.Dao.Site.Entity;


namespace Uni.WebSite.WifiAuth
{
    public class portal : IHttpHandler
    {
        private static  string Template = "<!DOCTYPE html><html><head>" +
            "<script type='text/javascript'>" +
                "var date = new Date();" +
                "date.setTime(date.getTime() + (5* 60 * 60 * 1000));" +
                "document.cookie = \"wifiauth={{'gwId':'{0}', 'gwAddress':'{1}', 'gwPort':'{2}', 'gwHost':'{3}','SID':'{4}'}};expires=\" + date.toGMTString() +\";path=/;\";" +
                "window.location.href = '{5}';" +
            "</script></head><body></body></html>";
        //domain=.yaodiancai.com;
        private static string AuthError_Url = "/autherror.html";

        public bool IsReusable { get { return true; } }


        public void ProcessRequest(HttpContext context)
        {
            string gwId = context.Request.Params["gw_id"];
            if (string.IsNullOrEmpty(gwId))
            {
                context.Response.Redirect(AuthError_Url);
                return;
            }

            string[] gws = WifiGWService.GetAuthAddress(gwId);
            if (gws == null)
            {
                context.Response.Redirect(AuthError_Url);
                return;
            }

            string sid = gwId.Substring(3, 10);
            InnerNetEntity inn = InnerNetService.QueryBySID(sid);
            string url;
            if (inn != null)
            {
                url = "http://" + inn.IP + ":" + inn.Port + "/index";
            }
            else {
                string domain = WifiGWService.GetHostDomain(gwId.Substring(3, 4));
                if (string.IsNullOrEmpty(domain))
                {
                    context.Response.Redirect(AuthError_Url);
                    return;
                }
                url = "http://" + domain + "/shop/" + sid + "/Html/Page/index.html";
            }
            context.Response.ContentType = "text/html";
            context.Response.Write(string.Format(Template, gwId, gws[0], gws[1], gws[2], sid, url));
        }
    }
}

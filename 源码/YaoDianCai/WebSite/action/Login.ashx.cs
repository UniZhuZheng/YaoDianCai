using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Site.Service;
using Uni.YDC.Dao.Site.Entity;

namespace Uni.WebSite.action
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string account = context.Request.Params["account"];
            string password = context.Request.Params["password"];
            string content = "{\"ok\":false}";

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(content);
                return;
            }

            ShopEntity shopInfo = LoginService.ShopLogin(account, password);
            if (shopInfo!=null)
            {
                content = "{\"ok\":true,\"SID\":\"" + shopInfo.SID + "\",\"name\":\"" + shopInfo.Name + "\",\"domain\":\""+shopInfo.HostDomain+"\"}";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
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
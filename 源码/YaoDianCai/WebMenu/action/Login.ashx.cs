using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.action
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

            string shopInfo = ShopInfoService.ShopLogin(account, password);
            if (!string.IsNullOrEmpty(shopInfo))
            {
                content = shopInfo;
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
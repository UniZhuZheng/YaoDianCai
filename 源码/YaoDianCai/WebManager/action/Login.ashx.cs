using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using System.Web.Security;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.WebManager.Action
{

    public class Login : IHttpHandler
    {
        public bool IsReusable { get { return false; } }


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

            //int mgId = ManagementService.Login(account, password);
            //if (mgId > 0)
            //{
            //    content = "{\"ok\":true,\"id\":" + mgId + "}";
            //}
            ManagementsEntity me=ManagementService.Login(account);
            if (password.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(me.Password, "MD5").ToLower()))
            {
                content = "{\"ok\":true,\"id\":" + me.Id + "}";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }
    }
}
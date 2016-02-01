using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.action
{
    /// <summary>
    /// ShopCommentAction 的摘要说明
    /// </summary>
    public class ShopCommentAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ShopCommentAdd":
                    content = ShopCommentAdd(context);
                    break;
                
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string ShopCommentAdd(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string comment = context.Request.Params["comment"];
            if (ShopCommentService.ShopCommentAdd(sid, comment))
            {
                return "{\"ok\":true}";
            }
            else
            {
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
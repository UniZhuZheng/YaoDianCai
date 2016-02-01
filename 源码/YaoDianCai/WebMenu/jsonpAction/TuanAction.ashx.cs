using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// TuanAction 的摘要说明
    /// </summary>
    public class TuanAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_CreateTuan":
                    content = CreateTuan(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback + "(" + content + ")");
        }

        private string CreateTuan(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string json = context.Request["TuanJson"];
            if (!TuanService.AddTuan(sid, json))
            {
                return "{\"ok\":false}";
            }
            return "{\"ok\":true}";
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
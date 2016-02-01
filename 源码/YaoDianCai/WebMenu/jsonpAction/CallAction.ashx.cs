using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// CallAction 的摘要说明
    /// </summary>
    public class CallAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_AddCall":
                    content = AddCall(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback + "(" + content + ")");
        }
        private string AddCall(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["TableName"];
            if (!CallService.Insert(sid, tableName))
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
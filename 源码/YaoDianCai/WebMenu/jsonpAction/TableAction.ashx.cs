using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// TableAction 的摘要说明
    /// </summary>
    public class TableAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ListAllTables": //"table_select"
                    content = ListAllTables(context);
                    break;
                case "OP_GetBIDByTable": //"table_delete"
                    content = GetBIDByTable(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback + "(" + content + ")");
        }

        private string GetBIDByTable(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["tableName"];
            string BID = TableService.GetBIDByTable(sid, tableName);
            return "{\"ok\":true,\"BID\":\"" + BID + "\"}";
        }

        private string ListAllTables(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            return TableService.GetAllTableJson(sid);
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
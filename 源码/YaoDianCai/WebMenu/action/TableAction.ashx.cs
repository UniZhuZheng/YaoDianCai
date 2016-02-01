using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;


namespace Uni.WebMenu.Action
{
    public class TableAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ListAllTables": //"table_select"
                    content = ListAllTables(context);
                    break;

                case "OP_CreateTable": //"table_add"
                    content = CreateTable(context);
                    break;
                    
                case "OP_RemoveTable": //"table_delete"
                    content = RemoveTable(context);
                    break;
                case "OP_CountTable": //"table_delete"
                    content = CountTable(context);
                    break;
                case "OP_GetBIDByTable": //"table_delete"
                    content = GetBIDByTable(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string GetBIDByTable(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["tableName"];
            string BID = TableService.GetBIDByTable(sid, tableName);
            return "{\"ok\":true,\"BID\":\"" + BID + "\"}";
        }

        private string CountTable(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int num = TableService.GetTableCount(sid);
            return "{\"ok\":true,\"count\":"+num+"}";
        }

        private string ListAllTables(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            return TableService.GetAllTableJson(sid);
        }

        private string CreateTable(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request["tableName"];

            if (!TableService.AddTable(sid, tableName))
            {
                return "{\"ok\":false}";
            }
            return "{\"ok\":true}";
        }

        private string RemoveTable(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request["tableName"];

            TableService.DelTable(sid, tableName);
            return "{\"ok\":true}";
        }
    }
}
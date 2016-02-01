using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;
using System.Web.Script.Serialization;
using Uni.YDC.Dao.Menu.Entity;

namespace Uni.WebMenu.action
{
    /// <summary>
    /// CallAction 的摘要说明
    /// </summary>
    public class CallAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_AddCall":
                    content = AddCall(context);
                    break;
                case "OP_Query":
                    content = Query(context);
                    break;
                case "OP_QueryCount":
                    content = QueryCount(context);
                    break;
                case "OP_QueryLimitDay":
                    content = QueryLimitDay(context);
                    break;
                case "OP_QueryLimitDayCount":
                    content = QueryLimitDayCount(context);
                    break;
                case "OP_QueryLimitMonth":
                    content = QueryLimitMonth(context);
                    break;
                case "OP_QueryLimitMonthCount":
                    content = QueryLimitMonthCount(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string QueryLimitMonthCount(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);
            int num = CallService.QueryCount(SID, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string QueryLimitMonth(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddMonths(1);

            List<CallEntity> list = CallService.Query(SID, startDate, endDate, firstResult, maxResult);
            var q = from t in list
                    select new
                    {
                        content = t.Content,
                        type = t.Type,
                        state = t.State,
                        createDate = t.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        tableName = t.TableName,
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        private string QueryLimitDay(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);

            List<CallEntity> list = CallService.Query(SID, startDate, endDate, firstResult, maxResult);
            var q = from t in list
                    select new
                    {
                        content = t.Content,
                        type = t.Type,
                        state = t.State,
                        createDate = t.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        tableName = t.TableName,
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        private string QueryLimitDayCount(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            DateTime startDate = Convert.ToDateTime(context.Request.Params["startDate"]);
            DateTime endDate = startDate.AddDays(1);
            int num = CallService.QueryCount(SID, startDate, endDate);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string QueryCount(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            int num = CallService.QueryCount(sid);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string Query(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<CallEntity> list = CallService.Query(sid, firstResult, maxResult);
            var q = from t in list
                    select new
                    {
                        content = t.Content,
                        type = t.Type,
                        state = t.State,
                        createDate = t.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        tableName = t.TableName,
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
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
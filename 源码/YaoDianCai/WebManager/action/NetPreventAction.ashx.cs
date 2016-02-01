using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.WebManager.action
{
    /// <summary>
    /// NetPreventAction 的摘要说明
    /// </summary>
    public class NetPreventAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_GetNetPreventList":
                    content = GetNetPreventList(context);
                    break;
                case "OP_NetPreventAdd":
                    content = NetPreventAdd(context);
                    break;
                case "OP_NetPreventDelete":
                    content = NetPreventDelete(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string NetPreventDelete(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            string MAC = context.Request.Params["MAC"];
            if (string.IsNullOrEmpty(MAC))
            {
                return "{\"ok\":false}";
            }

            if (!NetPreventService.Delete(SID, MAC))
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true}";
        }

        private string NetPreventAdd(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            string MAC = context.Request.Params["MAC"];
            if (string.IsNullOrEmpty(MAC))
            {
                return "{\"ok\":false}";
            }

            if (!NetPreventService.Insert(SID, MAC))
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true}";
        }

        private string GetNetPreventList(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            List<NetPreventEntity> list= NetPreventService.GetNetPreventList(sid);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"MAC\":\"" + list[i].MAC + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
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
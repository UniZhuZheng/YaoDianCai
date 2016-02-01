using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.WebManager.Action
{
    public class HostAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_HostSelection":
                    content = HostSelection(context);
                    break;

                case "OP_HostList":
                    content = HostList(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string HostList(HttpContext context)
        {
            List<HostEntity> lists = HostService.GetAllHosts();

            if (lists.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < lists.Count; i++)
            {
                str += "{";
                str += "\"hostId\":\"" + lists[i].HostId + "\",";
                str += "\"name\":\"" + lists[i].Name + "\",";
                str += "\"domain\":\"" + lists[i].Domain + "\"";
                if (i == (lists.Count - 1))
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

        private string HostSelection(HttpContext context)
        {
            List<HostEntity> lists = HostService.GetAllHosts();

            if (lists.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < lists.Count; i++)
            {
                str += "{";
                str += "\"hostId\":\"" + lists[i].HostId + "\",";
                str += "\"name\":\"" + lists[i].Name + "\"";
                if (i == (lists.Count - 1))
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
    }
}
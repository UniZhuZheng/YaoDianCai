using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// VideoAction 的摘要说明
    /// </summary>
    public class VideoAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_Query":
                    content = Query(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback + "(" + content + ")");
        }

        private string Query(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            List<VideoEntity> list = VideoService.Query(sid);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"number\":\"" + list[i].Number + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"tab\":\"" + list[i].Tab + "\",";
                str += "\"content\":\"" + list[i].Content + "\",";
                str += "\"count\":\"" + list[i].Count + "\",";
                str += "\"sort\":\"" + list[i].Sort + "\",";
                str += "\"fileName\":\"" + list[i].FileName + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate + "\"";
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
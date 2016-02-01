using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.action
{
    /// <summary>
    /// ShopLabelAction 的摘要说明
    /// </summary>
    public class ShopLabelAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ListAllLabels": 
                    content = ListAllLabels(context);
                    break;
                case "OP_LabelsUpdateCount":
                    content = LabelsUpdateCount(context);
                    break;
                case "OP_ShopLabelCountList":
                    content = ShopLabelCountList(context);
                    break;
                case "OP_ShopLabelComment_Count":
                    content = ShopLabelComment_Count(context);
                    break;
                case "OP_ShopLabelComment_List":
                    content = ShopLabelComment_List(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string ShopLabelComment_List(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopCommentEntity> list = ShopLabelService.ShopLabelComment_List(SID, firstResult, maxResult);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"comment\":\"" + list[i].Comment + "\",";
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

        private string ShopLabelComment_Count(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            int num = ShopLabelService.ShopLabelComment_Count(SID);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopLabelCountList(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            List<ShopLabelEntity> list = ShopLabelService.ShopLabelCountList(SID);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"count\":\"" + list[i].Count + "\",";
                str += "\"lastDate\":\"" + Convert.ToDateTime(list[i].CreateDate).ToString("yyyy/MM/dd HH:mm:ss") + "\"";
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

        private string LabelsUpdateCount(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            int count =Convert.ToInt32( context.Request.Params["count"]);

            if (ShopLabelService.LabelsUpdateCount(sid, name, count))
            {
                return "{\"ok\":true}";
            }
            else {
                return "{\"ok\":false}";
            }
            
        }

        private string ListAllLabels(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            List<ShopLabelEntity> list = ShopLabelService.GetShopLabelList(sid);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"type\":\"" + list[i].Type + "\",";
                str += "\"Status\":\"" + list[i].Status + "\",";
                str += "\"count\":\"" + list[i].Count + "\",";
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
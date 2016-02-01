using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Dao.Manager.Entity;
using Uni.YDC.Web.Manager.Service;

namespace Uni.WebManager.action
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
                case "OP_ShopLabelList":
                    content = ShopLabelList(context);
                    break;
                case "OP_ShopLabelAdd":
                    content = ShopLabelAdd(context);
                    break;
                case "OP_ShopLabelDelete":
                    content = ShopLabelDelete(context);
                    break;
                case "OP_ShopLabelExit":
                    content = ShopLabelExit(context);
                    break;
                case "OP_ShopLabelUpdate":
                    content = ShopLabelUpdate(context);
                    break;
                case "OP_ShopLabelLimit_Count":
                    content = ShopLabelLimit_Count(context);
                    break;
                case "OP_ShopLabelLimit":
                    content = ShopLabelLimit(context);
                    break;
                case "OP_ShopLabelSearchLimit_Count":
                    content = ShopLabelSearchLimit_Count(context);
                    break;
                case "OP_ShopLabelSearchLimit":
                    content = ShopLabelSearchLimit(context);
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
                str += "\"id\":\"" + list[i].Id + "\",";
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
            List<object[]> list = ShopLabelService.ShopLabelCountList(SID);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"id\":\"" + list[i][0] + "\",";
                str += "\"SID\":\"" + list[i][1] + "\",";
                str += "\"name\":\"" + list[i][2] + "\",";
                str += "\"count\":\"" + list[i][3] + "\",";
                str += "\"lastDate\":\"" +Convert.ToDateTime(list[i][4]).ToString("yyyy/MM/dd HH:mm:ss") + "\"";
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

        private string ShopLabelSearchLimit(HttpContext context)
        {
            string search = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<object[]> list = ShopLabelService.GetShopTabList_1(search,firstResult, maxResult);
            List<object[]> list_2 = ShopLabelService.GetShopTabList_2(search, firstResult, maxResult);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"name\":\"" + list[i][0] + "\",";
                str += "\"SID\":\"" + list[i][1] + "\",";
                str += "\"tabCount\":\"" + list[i][2] + "\",";
                str += "\"comCount\":\"" + list_2[i][2] + "\"";
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

        private string ShopLabelSearchLimit_Count(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];
            int num = ShopLabelService.GetShopCount(key);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopLabelLimit(HttpContext context)
        {
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<object[]> list = ShopLabelService.GetShopTabList_1(firstResult, maxResult);
            List<object[]> list_2 = ShopLabelService.GetShopTabList_2(firstResult, maxResult);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"name\":\"" + list[i][0] + "\",";
                str += "\"SID\":\"" + list[i][1] + "\",";
                str += "\"tabCount\":\"" + list[i][2] + "\",";
                str += "\"comCount\":\"" + list_2[i][2] + "\"";
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

        private string ShopLabelLimit_Count(HttpContext context)
        {
            int num = ShopLabelService.GetShopCount();
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopLabelUpdate(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request.Params["id"]);
            string name = context.Request.Params["name"];
            string SID = context.Request.Params["SID"];
            string oldName = context.Request.Params["oldName"];
            if (ShopLabelService.ShopLabelUpdate(id, name, SID, oldName))
            {
                return "{\"ok\":true}";
            }
            else
            {
                return "{\"ok\":false}";
            }
        }

        private string ShopLabelExit(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            if (ShopLabelService.ShopLabelExit(SID, name))
            {
                return "{\"ok\":true}";
            }
            else
            {
                return "{\"ok\":false}";
            }
        }

        private string ShopLabelDelete(HttpContext context)
        {
            int id =Convert.ToInt32( context.Request.Params["id"]);
            string name = context.Request.Params["name"];
            string SID = context.Request.Params["SID"];
            if (ShopLabelService.ShopLabelDelete(id,SID, name))
            {
                return "{\"ok\":true}";
            }
            else
            {
                return "{\"ok\":false}";
            }
        }

        private string ShopLabelAdd(HttpContext context)
        {
            string SID = context.Request.Params["SID"];
            string name = context.Request.Params["shopTab"];
            ShopLabelEntity se = new ShopLabelEntity();
            se.SID = SID;
            se.Name = name;
            se.Type = 1;
            se.Status = 1;
            se.CreateDate = DateTime.Now;
            if (ShopLabelService.ShopLabelAdd(se))
            {
                return "{\"ok\":true}";
            }
            else {
                return "{\"ok\":false}";
            }
        }

        private string ShopLabelList(HttpContext context)
        {
            string SID = context.Request.Params["SID"];

            List<ShopLabelEntity> list = ShopLabelService.ShopLabelList(SID);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"id\":\"" + list[i].Id + "\",";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"type\":\"" + list[i].Type + "\",";
                str += "\"status\":\"" + list[i].Status + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\"";
                
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
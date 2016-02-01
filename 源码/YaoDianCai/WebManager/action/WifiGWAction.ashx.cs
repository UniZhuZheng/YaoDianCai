using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.WebManager.Action
{
    public class WifiGWAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ShopWifiGWLimit":
                    content = ShopWifiGWLimit(context);
                    break;

                case "OP_ShopWifiGWLimit_Count":
                    content = ShopWifiGWLimit_Count(context);
                    break;

                case "OP_ShopWifiGWSearchLimit":
                    content = ShopWifiGWSearchLimit(context);
                    break;

                case "OP_ShopWifiGWSearchLimit_Count":
                    content = ShopWifiGWSearchLimit_Count(context);
                    break;

                case "OP_WifiGWByShopLimit":
                    content = WifiGWByShopLimit(context);
                    break;

                case "OP_WifiGWByShopLimit_Count":
                    content = WifiGWByShopLimit_Count(context);
                    break;

                case "OP_WifiGWNewOne":
                    content = WifiGWNewOne(context);
                    break;

                case "OP_WifiGWIdByShop":
                    content = WifiGWIdByShop(context);
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }


        private string ShopWifiGWLimit(HttpContext context)
        {
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopWifiGWEntity> list = WifiGWService.GetShopWifiGW(firstResult, maxResult);

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

                if (list[i].WifiGWEntity.Length <= 0)
                {
                    str += "\"gwLists\":[]}";
                    return str;
                }
                str += "\"gwLists\":[";
                for (int j = 0; j < list[i].WifiGWEntity.Length; j++)
                {
                    str += "{";
                    str += "\"gwId\":\"" + list[i].WifiGWEntity[j].GwId + "\",";
                    str += "\"gwName\":\"" + list[i].WifiGWEntity[j].Name + "\",";
                    str += "\"gwAddress\":\"" + list[i].WifiGWEntity[j].Address + "\",";
                    str += "\"gwPort\":\"" + list[i].WifiGWEntity[j].Port + "\",";
                    str += "\"createDate\":\"" + list[i].WifiGWEntity[j].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\",";
                    str += "\"count\":\"" + list[i].WifiGWEntity[j].AuthCount + "\"";
                    if (j == (list[i].WifiGWEntity.Length - 1))
                    {
                        str += "}";
                    }
                    else
                    {
                        str += "},";
                    }
                }
                str += "]}";
                if (i < (list.Count - 1))
                {
                    str += ",";
                }
            }
            str += "]}";
            return str;
        }

        private string ShopWifiGWLimit_Count(HttpContext context)
        {
            int num = WifiGWService.GetWifiGWCount();
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopWifiGWSearchLimit(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<ShopWifiGWEntity> list = WifiGWService.GetShopWifiGWSearch(key, firstResult, maxResult);

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

                if (list[i].WifiGWEntity.Length <= 0)
                {
                    str += "\"gwLists\":[]}";
                    return str;
                }
                str += "\"gwLists\":[";
                for (int j = 0; j < list[i].WifiGWEntity.Length; j++)
                {
                    str += "{";
                    str += "\"gwId\":\"" + list[i].WifiGWEntity[j].GwId + "\",";
                    str += "\"gwName\":\"" + list[i].WifiGWEntity[j].Name + "\",";
                    str += "\"gwAddress\":\"" + list[i].WifiGWEntity[j].Address + "\",";
                    str += "\"gwPort\":\"" + list[i].WifiGWEntity[j].Port + "\",";
                    str += "\"createDate\":\"" + list[i].WifiGWEntity[j].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\",";
                    str += "\"count\":\"" + list[i].WifiGWEntity[j].AuthCount + "\"";
                    if (j == (list[i].WifiGWEntity.Length - 1))
                    {
                        str += "}";
                    }
                    else
                    {
                        str += "},";
                    }
                }
                str += "]}";
                if (i < (list.Count - 1))
                {
                    str += ",";
                }
            }
            str += "]}";
            return str;
        }

        private string ShopWifiGWSearchLimit_Count(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];

            int num = WifiGWService.GetSearchCount(key);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string WifiGWByShopLimit(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            List<WifiGWEntity> list = WifiGWService.GetWifiGWList(sid, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"gwId\":\"" + list[i].GwId + "\",";
                str += "\"gwName\":\"" + list[i].Name + "\",";
                str += "\"gwAddress\":\"" + list[i].Address + "\",";
                str += "\"gwPort\":\"" + list[i].Port + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString("yyyy/MM/dd HH:mm:ss") + "\",";
                str += "\"count\":\"" + list[i].AuthCount + "\"";
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

        private string WifiGWByShopLimit_Count(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int num = WifiGWService.GetWifiGWListCount(sid);
            return "{\"ok\":true,\"count\":" + num + "}";
        }


        private string WifiGWNewOne(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string gwId = WifiGWService.NewOne(sid);
            if (string.IsNullOrEmpty(gwId))
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true,\"gwId\":\"" + gwId + "\"}";
        }

        private string WifiGWIdByShop(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            List<WifiGWEntity> list = WifiGWService.GetAllWifiGWByShop(sid);
            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"gwId\":\"" + list[i].GwId + "\",";
                str += "\"gwName\":\"" + list[i].Name + "\"";

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

    }
}
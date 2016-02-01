using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.Core.Common.Utils;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.WebManager.Action
{
    public class ShopAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ShopNameExists":
                    content = ShopNameExists(context);
                    break;

                case "OP_ShopAccountExists":
                    content = ShopAccountExists(context);
                    break;

                case "OP_ShopNew":
                    content = ShopNew(context);
                    break;

                case "OP_ShopBaseInfo":
                    content = ShopBaseInfo(context);
                    break;

                case "OP_ShopBaseInfoUpdate":
                    content = ShopBaseInfoUpdate(context);
                    break;

                case "OP_ShopPasswordUpdate":
                    content = ShopPasswordUpdate(context);
                    break;

                case "OP_ShopListAllLimit":
                    content = ShopListAllLimit(context);
                    break;

                case "OP_ShopListAllCount":
                    content = ShopListAllCount(context);
                    break;

                case "OP_ShopListSearchLikeLimit":
                    content = ShopListSearchLikeLimit(context);
                    break;

                case "OP_ShopListSearchLikeCount":
                    content = ShopListSearchLikeCount(context);
                    break;

                case "OP_ShopInfoTotalRecordCount":
                    content = OP_ShopInfoTotalRecordCount(context);
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string OP_ShopInfoTotalRecordCount(HttpContext context)
        {
            return ShopService.GetShopInfoTotalRecordCount();
        }

        private string ShopNameExists(HttpContext context)
        {
            string name = context.Request.Params["name"];
            if (string.IsNullOrEmpty(name))
            {
                return "{\"ok\":false}";
            }

            if (!ShopService.IsExistShopName(name))
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true}";
        }

        private string ShopAccountExists(HttpContext context)
        {
            string account = context.Request.Params["account"];
            if (string.IsNullOrEmpty(account))
            {
                return "{\"ok\":false}";
            }

            if (!ShopService.IsExistShopAccount(account))
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true}";
        }

        private string ShopNew(HttpContext context)
        {
            string name = context.Request.Params["name"];
            string account = context.Request.Params["account"];
            string password = context.Request.Params["password"];
            string hostId = context.Request.Params["hostId"];
            string phone = context.Request.Params["phone"];
            string email = context.Request.Params["email"];
            string contact = context.Request.Params["contact"];
            string address = context.Request.Params["address"];
            int createBy = Convert.ToInt32(context.Request.Params["createBy"]);
            int wifiCount = Convert.ToInt32(context.Request.Params["wifiCount"]);

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hostId))
            {
                return "{\"ok\":false}";
            }

            HttpContext.Current.Application.Lock();
            ShopEntity shop = ShopService.CreateShop(account, password, name, hostId, phone, email, contact, address, wifiCount);
            HttpContext.Current.Application.UnLock();

            if (shop == null)
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true,\"SID\":\"" + shop.SID + "\"}";
        }

        private string ShopBaseInfo(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            ShopEntity entity =  ShopService.GetShopBaseInfo(sid);

            string str = "{\"ok\":true,\"data\":";
                str += "{";
                str += "\"SID\":\"" + entity.SID + "\",";
                str += "\"account\":\"" + entity.Account + "\",";
                str += "\"name\":\"" + entity.Name + "\",";
                str += "\"area\":\"" + entity.HostId + "\",";
                str += "\"hostName\":\"" + entity.HostName + "\",";
                str += "\"phone\":\"" + entity.Phone + "\",";
                str += "\"email\":\"" + entity.Email + "\",";
                str += "\"contact\":\"" + entity.Contact + "\",";
                str += "\"address\":\"" + entity.Address + "\"";
                str += "}";
            str += "}";
            return str;
        }


        private string ShopPasswordUpdate(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string password = context.Request.Params["password"];
            ShopService.SetNewPassword(sid, password);

            return "{\"ok\":true}";
        }

        private string ShopListAllLimit(HttpContext context)
        {
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            List<ShopEntity> list = ShopService.GetAllShopLimit(firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"account\":\"" + list[i].Account + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"area\":\"" + list[i].HostId + "\",";
                str += "\"hostName\":\"" + list[i].HostName + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString() + "\",";
                str += "\"groupSubCount\":\"" + list[i].TuanCount + "\",";
                str += "\"dishMarket\":\"" + list[i].DishOnSellCount + "\",";
                str += "\"tableNumber\":\"" + list[i].TableCount + "\",";
                str += "\"menuTem\":" + list[i].TplId + ",";
                str += "\"gwCount\":\"" + list[i].WifiGWCount + "\"";
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

        private string ShopListAllCount(HttpContext context)
        {
            int num = ShopService.GetAllShopCount();
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopListSearchLikeLimit(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            List<ShopEntity> list = ShopService.GetSearchLikeLimit(key, firstResult, maxResult);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"SID\":\"" + list[i].SID + "\",";
                str += "\"account\":\"" + list[i].Account + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"area\":\"" + list[i].HostId + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate.ToString() + "\",";
                str += "\"groupSubCount\":\"" + list[i].TuanCount + "\",";
                str += "\"dishMarket\":\"" + list[i].BillCount + "\",";
                str += "\"tableNumber\":\"" + list[i].TableCount + "\",";
                str += "\"menuTem\":\"" + list[i].TplId + "\",";
                str += "\"gwCount\":\"" + list[i].WifiGWCount + "\"";
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

        private string ShopListSearchLikeCount(HttpContext context)
        {
            string key = context.Request.Params["searchKey"];
            int num = ShopService.GetSearchLikeCount(key);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

       
        
        private string ShopBaseInfoUpdate(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string phone = context.Request.Params["phone"];
            string email = context.Request.Params["email"];
            string contact = context.Request.Params["contact"];
            string address = context.Request.Params["address"];

            bool ret = ShopService.SetShopBaseInfo(sid, phone, email, contact, address);
            if (!ret)
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true}";
        }
    }
}
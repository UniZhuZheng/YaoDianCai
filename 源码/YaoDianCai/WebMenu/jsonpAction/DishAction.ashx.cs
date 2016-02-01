using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// DishAction 的摘要说明
    /// </summary>
    public class DishAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";
            switch (option)
            {
                case "OP_GetAllDishes": //menu_select_client
                    content = GetAllDishes(context);
                    break;
                case "OP_ListDishType":
                    content = ListDishType(context);
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback+"("+content+")");
        }

        private string ListDishType(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            return DishService.GetAllTypeJson(sid);
        }

        private string GetAllDishes(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            return DishService.GetAllDishesJson(sid);
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
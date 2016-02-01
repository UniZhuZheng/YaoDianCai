using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// CartAction 的摘要说明
    /// </summary>
    public class CartAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_GetCartDishByTable": //"2"
                    content = GetCartDishByTable(context);
                    break;
                case "OP_UpdateCartDishCount": //"1"
                    content = UpdateCartDishCount(context);
                    break;
                case "OP_CartAddOne":
                    content = CartAddOne(context);
                    break;
                case "OP_CartRemoveOne": //"4"
                    content = CartRemoveOne(context);
                    break;
                case "OP_RemoveCart": //"3"
                    content = RemoveCart(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback + "(" + content + ")");
        }

        private string UpdateCartDishCount(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["TableName"];
            string dishName = context.Request.Params["DishName"];
            int dishCount = Convert.ToInt32(context.Request.Params["DishCount"]);
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            CartService.UpdateDishCount(sid, tableName, dishName, dishCount, cartStatus);
            return "{\"ok\":true}";
        }

        private string GetCartDishByTable(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["TableName"];
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            return CartService.GetCartJson(sid, tableName, cartStatus);
        }
        private string CartAddOne(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["TableName"];
            string dishName = context.Request.Params["DishName"];
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            CartService.AddCartOne(sid, tableName, dishName, cartStatus);

            return "{\"ok\":true}";
        }
        private string CartRemoveOne(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["TableName"];
            string dishName = context.Request.Params["DishName"];
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            CartService.RemoveCartOne(sid, tableName, dishName, cartStatus);

            return "{\"ok\":true}";
        }
        private string RemoveCart(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string tableName = context.Request.Params["TableName"];
            string dishName = context.Request.Params["DishName"];
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            CartService.RemoveCart(sid, tableName, dishName, cartStatus);

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
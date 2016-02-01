using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Menu.Service;
using System.Web.Script.Serialization;
using Uni.YDC.Dao.Menu.Entity;

namespace Uni.WebMenu.jsonpAction
{
    /// <summary>
    /// BillAction 的摘要说明
    /// </summary>
    public class BillAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string jsoncallback = context.Request.Params["jsoncallback"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_CreateBill":
                    content = CreateBill(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(jsoncallback + "(" + content + ")");
        }
        private string CreateBill(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string json = context.Request["BillJson"];
            int cartStatus = 0;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BillEntity bill = serializer.Deserialize<BillEntity>(json);
            if (bill == null)
            {
                return "{\"ok\":false}";
            }
            string[] str = bill.SID.Split('.');
            List<OrderEntity> list = new List<OrderEntity>();
            for (int i = 0; i < str.Length; i = i + 4)
            {
                OrderEntity oe = new OrderEntity();
                oe.DishNumber = str[i];
                oe.DishName = str[i + 1];
                oe.DishPrice = str[i + 2];
                oe.DishCount = Convert.ToInt32(str[i + 3]);
                list.Add(oe);
            }
            bill.Orders = list;
            bill.SID = "";
            json = serializer.Serialize(bill);
            string returnStr = BillService.AddBill(sid, json, cartStatus);
     
            if (string.IsNullOrEmpty(returnStr))
            {
                return "{\"ok\":false}";
            }
            else
            {
                if (returnStr.Split('-')[0].Equals("false"))
                {
                    return "{\"ok\":false,\"BID\":\"" + returnStr.Split('-')[1] + "\"}";
                }
                else
                {
                    return "{\"ok\":true,\"BID\":\"" + returnStr.Split('-')[1] + "\"}";
                }
            }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Menu;
using Uni.YDC.Dao.Menu.Entity;


namespace Uni.YDC.Web.Menu.Service
{
    public class OrderService
    {
        /*
         * BillAction.ShowDishRecord
         * */
        public static List<OrderEntity> GetOrderDish(string sid, string bid)
        {
            return new OrderDao(sid).queryAll(bid);
        }
        /*
         * ClientAndroidService.UpdateOrderDishStatus
         * */
        public static string GetOrderDish(string sid, string bid, string dishNumber, string dishName, float dishPrice, int dishCount)
        {
            bool webSign = false;
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[5];
                args[0] = bid;
                args[1] = dishNumber;
                args[2] = dishName;
                args[3] = dishPrice.ToString();
                args[4] = dishCount.ToString();
                webSign=Convert.ToBoolean(WSHelper.InvokeWebService(url, "UpdateOrderDishStatus", args));
            }
            catch (Exception)
            {
                return "{\"ok\":false}";
            }
            if (webSign && new OrderDao(sid).updateStatus(bid, dishNumber, dishName, dishPrice, dishCount))
            {
                return "{\"ok\":true}";
            }
            else {
                return "{\"ok\":false}";
            }
        }
    }
}

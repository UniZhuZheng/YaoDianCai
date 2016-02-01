using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;

namespace Uni.YDC.Web.Menu.Service
{
    public class CartService
    {
        public static bool AddCart(string sid, string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            CartEntity cart = serializer.Deserialize<CartEntity>(json);
            if (cart == null)
            {
                return false;
            }

            new CartDao(sid).insert(cart);
            return true;
        }
        /*
         * CartAction.CartAddOne
         * */
        public static void AddCartOne(string sid, string tableName, string dishName,int cartStatus)
        {
            new CartDao(sid).insert(tableName, dishName,cartStatus);
        }
        /*
         * CartAction.UpdateCartDishCount
         * */
        public static void UpdateDishCount(string sid, string tableName, string dishName, int dishCount, int cartStatus)
        {
            int count = new CartDao(sid).count(tableName, dishName, cartStatus);

            if (dishCount <= 0 && count > 0)
            {
                new CartDao(sid).delete(tableName, dishName, cartStatus);
            }
            else if (dishCount > count)
            {
                for (int i = 0; i < (dishCount - count); i++)
                {
                    new CartDao(sid).insert(tableName, dishName, cartStatus);
                }
            }
            else if (dishCount < count)
            {
                for (int i = 0; i < (count - dishCount); i++)
                {
                    new CartDao(sid).deleteOne(tableName, dishName, cartStatus);
                }
            }
        }
        /*
         * CartAction.GetCartDishByTable
         * */
        public static string GetCartJson(string sid, string tablename, int cartStatus)
        {
            List<CartEntity> list = new CartDao(sid).query(tablename, cartStatus);
            var p =
                from c in list
                select new
                {
                    dishNumber = c.DishNumber,
                    dishName = c.DishName,
                    dishPrice = c.DishPrice,
                    dishCount = c.DishCount
                };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(p);
        }
        /*
         * CartAction.RemoveCart
         * */
        public static void RemoveCart(string sid, string tableName, string dishName, int cartStatus)
        {
            new CartDao(sid).delete(tableName, dishName, cartStatus);
        }
        /*
         * CartAction.CartRemoveOne
         * */
        public static void RemoveCartOne(string sid, string tableName, string dishName, int cartStatus)
        {
            new CartDao(sid).deleteOne(tableName, dishName, cartStatus);
        }

        /*
         * ClientAndroidService.GetCartTableForShopOrder
         * */
        public static string GetCartTableForShopOrder(string sid)
        {
            List<TableEntity> list = new CartDao(sid).GetCartTableForShopOrder(sid);
            var q = from t in list
                    select new
                    {
                        tablename = t.Name
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * ClientAndroidService.DeleteCartInfoForShopOrder
         * */
        internal static string DeleteCartInfoForShopOrder(string sid, string tableName)
        {
            if (new CartDao(sid).DeleteCartInfoForShopOrder(tableName))
            {
                return "[{\"ok\":true}]";
            }
            else
            {
                return "[{\"ok\":false}]";
            }
        }
    }
}

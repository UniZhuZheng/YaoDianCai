using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Uni.YDC.Dao.Menu;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Web.Menu.Service;

namespace Uni.YDC.Web.Menu
{
    public class ClientAndroidService
    {
        /*
         * ClientAndroidRemote.IntervalInfo
         * */
        public static string IntervalInfo(string sid) 
        {
            string billjson = RefreshBill(sid);
            string tuanjson = RefreshTuan(sid);
            return "{ \"BillInfo\":" + billjson + ",\"TuanInfo\":" + tuanjson + " }";
        }
        /*
         * ClientAndroidService.IntervalInfo
         * */
        private static string RefreshBill(string sid)
        {
            List<BillEntity> list = BillService.GetAllNewBill(sid);
            var q = from o in list
                    select new
                    {
                        BID = o.BID,
                        totalCount = o.TotalCount,
                        totalPrice = o.TotalPrice,
                        tableName = o.TableName,
                        createDate = o.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        orders = from cm in OrderService.GetOrderDish(sid, o.BID)
                                    select new
                                        {
                                            dishName = cm.DishName,
                                            dishNumber = cm.DishNumber,
                                            dishPrice = cm.DishPrice,
                                            dishCount = cm.DishCount,
                                            dishStatus=cm.DishStatus
                                        }
                    };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * ClientAndroidService.IntervalInfo
         * */
        private static string RefreshTuan(string sid)
        {
            List<TuanEntity> list = TuanService.GetAllNewTuan(sid);
            var q = from gb in list
                    select new
                    {
                        number = gb.Number,
                        phone = gb.Phone,
                        website = gb.Website,
                        createDate = gb.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        tableName = gb.TableName,
                    };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * ClientAndroidRemote.GetOldBillInfo
         * */
        public static string GetOldBillInfo(string sid, string time)
        {
            List<BillEntity> list = BillService.GetAllBillsByTime(sid, time);
            var q = from o in list
                    select new
                    {
                        BID = o.BID,
                        totalCount = o.TotalCount,
                        totalPrice = o.TotalPrice,
                        tableName = o.TableName,
                        createDate = o.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        orders = from cm in OrderService.GetOrderDish(sid, o.BID)
                                    where cm.BID == o.BID
                                    select new
                                    {
                                        dishName = cm.DishName,
                                        dishNumber = cm.DishNumber,
                                        dishPrice = cm.DishPrice,
                                        dishCount = cm.DishCount,
                                        dishStatus = cm.DishStatus
                                    }
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * ClientAndroidRemote.ToOldBill
         * */
        public static string ToOldBill(string sid, string bid)
        {
            BillService.SetDishState(sid, bid, 1);

            return "[{\"ok\":true}]";
        }

        public static string GetAllDishInfo(string sid)
        {
            List<string> list = new DishDao(sid).queryAllType();
            var q = from p in list
                select new
                {
                    type = p,
                    childmenu = from m in new DishDao(sid).queryAll() where m.Type == p
                    select new
                    {
                        number = m.Number,
                        name = m.Name,
                        price = m.Price,
                        state = m.State
                    }
                };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
  
        public static string UpdateDishInfo(string sid, string dishName, string dishPrice, string dishState)
        {
            new DishDao(sid).update(dishName, dishPrice, Convert.ToInt32(dishState));
            return "[{\"ok\":true}]";
        }

        public static string GetDishInfo(string sid, string dishName)
        {
            DishEntity dish = new DishDao(sid).queryInfo(dishName);
            var q = new
                {
                    imgcode = dish.Imgcode,
                    type = dish.Type,
                    property = dish.Property,
                    content = dish.Content
                };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * ClientAndroidRemote.GetOldTuanInfo
         * */
        public static string GetOldTuanInfo(string sid, string time)
        {
            List<TuanEntity> list = new TuanDao(sid).queryAll(time);
            var q = from t in list
                select new
                {
                    number = t.Number,
                    phone = t.Phone,
                    website = t.Website,
                    createDate = t.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    tableName = t.TableName,
                };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        /*
         * ClientAndroidRemote.ToOldTuan
         * */
        public static string ToOldTuan(string sid, string tuanNumber)
        {
            new TuanDao(sid).updateState(tuanNumber, 1);
            return "[{\"ok\":true}]";
        }
        /*
         * ClientAndroidRemote.UpdateOrderDishStatus
         * */
        public static string UpdateOrderDishStatus(string sid,string bid, string dishNumber, string dishName, float dishPrice, int dishCount)
        {
            return OrderService.GetOrderDish(sid,bid, dishNumber, dishName, dishPrice, dishCount);
        }

        public static string GetDishsForShopOrder(string sid)
        {
            throw new NotImplementedException();
        }

        public static string CreateBillForShopOrder(string sid, string json)
        {
            int cartStatus = 1;//cartStatus 0表示顾客新增的菜品，1表示服务员新增菜品
            if (BillService.AddBillPhone(sid, json, cartStatus))
            {
                return "[{\"ok\":true}]";
            }
            else {
                return "[{\"ok\":false}]";
            }
        }
        /*
         * ClientAndroidRemote.GetTableForShopOrder
         * */
        public static string GetTableForShopOrder(string sid)
        {
            return TableService.GetAllTableJson(sid);
        }
        /*
         * ClientAndroidRemote.GetCartTableForShopOrder
         * */
        public static string GetCartTableForShopOrder(string sid)
        {
            return CartService.GetCartTableForShopOrder(sid);
        }
        /*
         * ClientAndroidRemote.DeleteCartInfoForShopOrder
         * */
        public static string DeleteCartInfoForShopOrder(string sid, string tableName)
        {
            return CartService.DeleteCartInfoForShopOrder(sid, tableName);
        }
        /*
         * ClientAndroidRemote.GetCallList
         * */
        public static string GetCallList(string sid)
        {
            List<CallEntity> list = CallService.GetCallList(sid);
            var q = from t in list
                    select new
                    {
                        content = t.Content,
                        type = t.Type,
                        state = t.State,
                        createDate = t.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        tableName = t.TableName,
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * ClientAndroidRemote.UpdateCallState
         * */
        public static string UpdateCallState(string sid, string tableName)
        {
            if (CallService.UpdateCallState(sid, tableName))
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

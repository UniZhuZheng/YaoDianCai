using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;
using System.Web.Script.Serialization;


namespace Uni.YDC.Web.Manager.Service
{
    public class BillService
    {
        public static void AddBill(string sid, string bid, string tableName)
        {
            BillDao.Insert(sid, bid, tableName);
        }
        /*
         * WebMenuRemote.AddBillRecord
         * */
        public static void AddBill(string sid, string bid, string tableName, string memo, string dishRecord)
        {
            BillDao.Insert(sid, bid, tableName, memo);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<DishRecordEntity> dre = serializer.Deserialize<List<DishRecordEntity>>(dishRecord);
            DishRecordService.Insert(bid, dre);
        }
        /*
         * BillAction.BillInfoByShopLimit
         * */
        public static List<BillEntity> GetBillList(string sid, int firstResult, int maxResult)
        {
            return BillDao.QueryAll(sid, firstResult, maxResult);
        }
        /*
         * BillAction.BillInfoByDayLimit
         * BillAction.BillInfoByMonthLimit
         * */
        public static List<BillEntity> GetBillListByDate(string sid, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            return BillDao.QueryAll(sid, startDate, endDate, firstResult, maxResult);
        }
        /*
         * BillAction.BillInfoByShopLimit_Count
         * */
        public static int GetBillListCount(string sid)
        {
            return BillDao.Count(sid);
        }
        /*
         * BillAction.BillInfoByDayLimit_Count
         * BillAction.BillInfoByMonthLimit_Count
         * */
        public static int GetBillListCountByDate(string sid, DateTime startDate, DateTime endDate)
        {
            return BillDao.Count(sid, startDate, endDate);
        }
        /*
         * BillAction.ShopBillInfoLimit
         * */
        public static List<ShopEntity> GetShop(int firstResult, int maxResult)
        {
            return ShopDao.QueryAll(firstResult, maxResult);
        }
        /*
         * BillAction.ShopBillInfoLimit_Count
         * */
        public static int GetShopCount()
        {
            return ShopDao.Count();
        }
        /*
         * BillAction.ShopBillInfoSearchLimit
         * */
        public static List<ShopEntity> GetShopSearch(string search, int firstResult, int maxResult)
        {
            return ShopDao.QuerySearchAll(search, firstResult, maxResult);
        }
        /*
         * BillAction.ShopBillInfoSearchLimit_Count
         * */
        public static int GetSearchCount(string search)
        {
            return ShopDao.CountSearch(search);
        }

        
    }
}

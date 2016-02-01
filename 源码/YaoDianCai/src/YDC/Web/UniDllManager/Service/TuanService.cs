using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class TuanService
    {
        /*
         * TuanAction.ShopTuanInfoLimit
         * */
        public static List<ShopEntity> GetShop(int firstResult, int maxResult)
        {
            return ShopDao.QueryAll(firstResult, maxResult);
        }
        /*
         * TuanAction.ShopTuanInfoLimit_Count
         * */
        public static int GetShopCount()
        {
            return ShopDao.Count();
        }
        /*
         * TuanAction.ShopTuanInfoSearchLimit
         * */
        public static List<ShopEntity> GetShopSearch(string search, int firstResult, int maxResult)
        {
            return ShopDao.QuerySearchAll(search, firstResult, maxResult);
        }
        /*
         * TuanAction.ShopTuanInfoSearchLimit_Count
         * */

        public static int GetSearchCount(string search)
        {
            return ShopDao.CountSearch(search);
        }
        /*
         * TuanAction.TuanInfoByShopLimit
         * */
        public static List<TuanEntity> GetTuanList(string sid, int firstResult, int maxResult)
        {
            return TuanDao.QueryAll(sid, firstResult, maxResult);
        }
        /*
         * TuanAction.TuanInfoByShopLimit_Count
         * */
        public static int GetTuanListCount(string sid)
        {
            return TuanDao.Count(sid);
        }
        /*
         * TuanAction.TuanInfoByDayLimit
         * TuanAction.TuanInfoByMonthLimit
         * */
        public static List<TuanEntity> GetTuanListByDate(string sid, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            return TuanDao.QueryAll(sid, startDate, endDate, firstResult, maxResult);
        }
        /*
         * TuanAction.TuanInfoByDayLimit_Count
         * TuanAction.TuanInfoByMonthLimit_Count
         * */
        public static int GetBillListCountByDate(string sid, DateTime startDate, DateTime endDate)
        {
            return TuanDao.Count(sid, startDate, endDate);
        }
        /*
         * WebMenuRemote.AddTuanRecord
         * */
        public static void SetTuanRecord(string sid, string shopName, string number, string website, string owner, string phone)
        {
            TuanDao.Insert(sid, shopName, number, website, owner, phone);
        }
    }
}

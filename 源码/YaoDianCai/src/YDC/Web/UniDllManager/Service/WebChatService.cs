using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class WebChatService
    {
        /*
         * WebChatAction.WebChatInfoLimit
         * */
        public static List<object[]> GetWebChatList(int firstResult, int maxResult)
        {
            return WebChatDao.GetWebChatList(firstResult, maxResult);
        }
        /*
         * WebChatAction.WebChatInfoSearchLimit
         * */
        public static List<object[]> GetWebChatList(string search,int firstResult, int maxResult)
        {
            return WebChatDao.GetWebChatList(search,firstResult, maxResult);
        }
        /*
         * WebChatAction.WebChatInfoLimit_Count
         * */
        public static int GetShopCount()
        {
            return ShopDao.Count();
        }
        /*
         * WebChatAction.WebChatInfoSearchLimit_Count
         * */
        public static int GetSearchCount(string search)
        {
            return ShopDao.CountSearch(search);
        }
        /*
         * WebChatAction.WebChatInfoByShopLimit_Count
         * */
        public static int GetWebChatListCount(string sid)
        {
            return WebChatDao.Count(sid);
        }
        /*
         * WebChatAction.WebChatInfoByDayLimit_Count
         * WebChatAction.WebChatInfoByMonthLimit_Count
         * */
        public static int GetWebChatListCount(string SID, DateTime startDate, DateTime endDate)
        {
            return WebChatDao.Count(SID,startDate,endDate);
        }
        /*
         * WebChatAction.WebChatInfoByShopLimit
         * */
        public static List<WebChatEntity> GetWebChatList(string SID)
        {
            return WebChatDao.Query(SID);
        }
        /*
         * WebChatAction.WebChatInfoByDayLimit
         * WebChatAction.WebChatInfoByMonthLimit
         * */
        public static List<WebChatEntity> GetWebChatList(string SID, DateTime startDate, DateTime endDate)
        {
            return WebChatDao.Query(SID,startDate,endDate);
        }
    }
}

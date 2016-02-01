using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniDaoChart;

namespace UniDllChart.Service
{
    public class WebChatService
    {
        /*
         * MessageTextService.ContentDeal
         * */
        public static string Insert(string openId, string account, string password) {
            return WebChatDao.Insert(openId,account,password);
        }
        /*
         * MessageTextService.ContentDeal
         * */
        public static string Update(string openId, string account, string password)
        {
            return WebChatDao.Update(openId, account, password);
        }
        /*
         * MessageTextService.ContentDeal
         * */
        public static string IsExits(string openId) {
            return WebChatDao.IsExits(openId);
        }
        /*
         * MessageTextService.ContentDeal
         * */
        public static int[] GetCountByDate(string SID, DateTime startDate, DateTime endDate) {
            return WebChatDao.GetCountByDate(SID, startDate, endDate);
        }

        public static void Insert(string openId)
        {
            WebChatDao.Insert(openId);
        }

    }
}

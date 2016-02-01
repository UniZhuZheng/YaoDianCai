using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu;
using Uni.YDC.Dao.Menu.Entity;

namespace Uni.YDC.Web.Menu.Service
{
    public class CallService
    {
        /*
         * CallAction.AddCall
         * */
        public static bool Insert(string SID,string tableName) {
            return new CallDao(SID).Insert(tableName);
        }
        /*
         * ClientAndroidService.GetCallList
         * */
        public static List<CallEntity> GetCallList(string SID)
        {
            return new CallDao(SID).QueryAll();
        }
        /*
         * ClientAndroidService.UpdateCallState
         * */
        public static bool UpdateCallState(string SID, string tableName)
        {
            return new CallDao(SID).UpdateCallState(tableName);
        }

        public static int QueryCount(string sid)
        {
            return new CallDao(sid).QueryCount();
        }

        public static List<CallEntity> Query(string sid, int firstResult, int maxResult)
        {
            return new CallDao(sid).Query(firstResult, maxResult);
        }

        public static int QueryCount(string SID, DateTime startDate, DateTime endDate)
        {
            return new CallDao(SID).QueryCount(startDate, endDate);
        }

        public static List<CallEntity> Query(string SID, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            return new CallDao(SID).Query(startDate, endDate, firstResult, maxResult);
        }
    }
}

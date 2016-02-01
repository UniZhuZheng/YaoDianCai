using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class WifiAuthService
    {
        /*
         * WifiAuthAction.WifiAuthByGWLimit_Count
         * */
        public static int GetCount(string gwId)
        {
            return WifiAuthDao.Count(gwId);
        }
        /*
         * WifiAuthAction.WifiAuthByGWLimit
         * */
        public static List<WifiAuthEntity> GetWifiAuth(string gwId, int firstResult, int maxResult)
        {
            return WifiAuthDao.QueryAll(gwId, firstResult, maxResult);
        }
        /*
         * WifiAuthAction.WifiAuthByDayLimit
         * WifiAuthAction.WifiAuthByMonthLimit
         * */
        public static List<WifiAuthEntity> GetWifiAuthByDate(string gwId, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            return WifiAuthDao.QueryAll(gwId, startDate, endDate, firstResult, maxResult);
        }
        /*
         * WifiAuthAction.WifiAuthByDayLimit_Count
         * WifiAuthAction.WifiAuthByMonthLimit_Count
         * */
        public static int GetCountByDate(string gwId, DateTime startDate, DateTime endDate)
        {
            return WifiAuthDao.Count(gwId, startDate, endDate);
        }
        public static int GetCountByDate(DateTime startDate, DateTime endDate,string SID)
        {
            return WifiAuthDao.Count(startDate, endDate,SID);
        }
        
        private static string ToString(List<object[]> lists)
        {
            int size = lists.Count;
            if (size <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < size; i++)
            {
                object[] obj = lists[i];
                str += "{";
                str += "\"gwId\":\"" + obj[0] + "\",";
                str += "\"SID\":\"" + obj[1] + "\",";
                str += "\"token\":\"" + obj[2] + "\",";
                str += "\"mac\":\"" + obj[3] + "\",";
                str += "\"userIp\":\"" + obj[4] + "\",";
                str += "\"createDate\":\"" + obj[5] + "\"";
                if (i == (size - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }
        /*
         * WebSiteRemote.RefreshAuthRecord
         * */
        public static void RefreshAuthRecord(string sid, string gwId, string token, string mac, string ip, int incoming, int outcoming)
        {
            if (WifiAuthDao.Exists(sid, token, mac))
            {
                WifiAuthDao.Update(sid, gwId, token, mac, ip, incoming, outcoming);
            }
            else
            {
                WifiAuthDao.Insert(sid, gwId, token, mac, ip, incoming, outcoming);
            }
        }
        /*
         * WifiAuthAction.WifiAuthDistinction_Count
         * */
        public static int GetDistinctionCount(string SID)
        {
            return WifiAuthDao.GetDistinctionCount(SID);
        }
        /*
         * WifiAuthAction.WifiAuthDistinction_List
         * */
        public static List<object[]> GetDistinctionList(string SID, int firstResult, int maxResult)
        {
            return WifiAuthDao.GetDistinctionList(SID, firstResult, maxResult);
        }
        /*
         * WifiAuthAction.WifiAuthDistinctionSearch_Count
         * */
        public static int GetDistinctionCount(string SID, string searchKey)
        {
            return WifiAuthDao.GetDistinctionCount(SID, searchKey);
        }
        /*
         * WifiAuthAction.WifiAuthDistinctionSearch_List
         * */
        public static List<object[]> GetDistinctionList(string SID, string searchKey, int firstResult, int maxResult)
        {
            return WifiAuthDao.GetDistinctionList(SID,searchKey, firstResult, maxResult);
        }
    }
}

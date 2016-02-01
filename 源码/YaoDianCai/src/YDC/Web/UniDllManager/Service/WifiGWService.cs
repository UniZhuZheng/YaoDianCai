using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class WifiGWService
    {
        /*
         * WifiGWAction.ShopWifiGWLimit
         * */
        public static List<ShopWifiGWEntity> GetShopWifiGW(int firstResult, int maxResult)
        {
            List<ShopWifiGWEntity> result = new List<ShopWifiGWEntity>();

            List<ShopEntity> list = ShopDao.QueryAll(firstResult, maxResult);
            if (list.Count <= 0)
            {
                return result;
            }

            for (int i = 0; i < list.Count; i++)
            {
                ShopEntity shop = list[i];
                result.Add(new ShopWifiGWEntity
                {
                    SID = shop.SID,
                    Name = shop.Name,
                    WifiGWEntity = WifiGWDao.QueryAll(shop.SID).ToArray()
                });
            }

            return result;
        }
        /*
         * WifiGWAction.ShopWifiGWSearchLimit
         * */
        public static List<ShopWifiGWEntity> GetShopWifiGWSearch(string search, int firstResult, int maxResult)
        {
            List<ShopWifiGWEntity> result = new List<ShopWifiGWEntity>();

            List<ShopEntity> list = ShopDao.QuerySearchAll(search, firstResult, maxResult);
            if (list.Count <= 0)
            {
                return result;
            }

            for (int i = 0; i < list.Count; i++)
            {
                ShopEntity shop = list[i];
                result.Add(new ShopWifiGWEntity
                {
                    SID = shop.SID,
                    Name = shop.Name,
                    WifiGWEntity = WifiGWDao.QueryAll(shop.SID).ToArray()
                });
            }

            return result;
        }

        /*
         * WifiGWAction.ShopWifiGWLimit_Count
         * */
        public static int GetWifiGWCount()
        {
            return ShopDao.Count();
        }
        /*
         * WifiGWAction.ShopWifiGWSearchLimit_Count
         * */
        public static int GetSearchCount(string search)
        {
            return ShopDao.CountSearch(search);
        }
        /*
         * WifiGWAction.WifiGWByShopLimit
         * */
        public static List<WifiGWEntity> GetWifiGWList(string sid, int firstResult, int maxResult)
        {
            return WifiGWDao.QueryAll(sid, firstResult, maxResult);
        }
        /*
         * WifiGWAction.WifiGWIdByShop
         * WebMenuRemote.GetWifiGWBySID
         * */
        public static List<WifiGWEntity> GetAllWifiGWByShop(string sid)
        {
            return WifiGWDao.QueryAll(sid);
        }
        /*
         * WifiGWAction.WifiGWByShopLimit_Count
         * */
        public static int GetWifiGWListCount(string sid)
        {
            return WifiGWDao.Count(sid);
        }


        public static int SelectCount()
        {
            return ShopDao.Count();
        }
        /*
         * WifiGWAction.WifiGWNewOne
         * */
        public static string NewOne(string sid)
        {
            return WifiGWDao.Insert(sid);
        }
        /*
         * WebSiteRemote.GetWifiGWBySID
         * */
        public static List<WifiGWEntity> GetWifiGWBySID(string SID) {
            return WifiGWDao.GetWifiGWBySID(SID);
        }
        /*
         * WebSiteRemote.IsGWIdExists
         * */
        public static bool IsGWIdExists(string gwId,string mac)
        {
            string SID = gwId.Substring(3, 10);
            if (NetPreventService.IsExitMac(SID, mac))
            {
                return false;
            }
            else {
                return WifiGWDao.Exists(gwId);
            }
            
        }
        /*
         * WebSiteRemote.AuthWifiGW
         * */
        public static string AuthWifiGW(string gwId, string gwAddr, string gwPort, string host, string type)
        {
            string addr = gwAddr + ":" + gwPort + ":" + host;
            if (type.Equals("0"))
            {
                WifiGWEntity gw = WifiGWDao.Query(gwId);
                if (gw == null)
                {
                    return "false";
                }

                if (!string.Format("{0}:{1}:{2}", gw.Address, gw.Port, gw.RemoteHost).Equals(addr))
                {
                    return WifiGWDao.Update(gwId, gwAddr, gwPort, host);
                }
                return "false";
            }
            else {
                return WifiGWDao.Update(gwId, gwAddr, gwPort, host);
                
            }
        }
        /*
         * WifiGWService.GetAuthAddress
         * */
        public static WifiGWEntity GetAuthAddress(string gwId)
        {
            return WifiGWDao.Query(gwId);
        }
        private static string ToString_gwPart(ShopEntity shop, List<object[]> lists)
        {
            string str = "{";
            str += "\"SID\":\"" + shop.SID + "\",";
            str += "\"name\":\"" + shop.Name + "\",";

            int size = lists.Count;
            if (size <= 0)
            {
                str += "\"gwLists\":[]}";
                return str;
            }
            str += "\"gwLists\":[";
            for (int i = 0; i < size; i++)
            {
                object[] obj = lists[i];
                str += "{";
                str += "\"gwId\":\"" + obj[0] + "\",";
                str += "\"gwName\":\"" + obj[1] + "\",";
                str += "\"gwAddress\":\"" + obj[2] + "\",";
                str += "\"gwPort\":\"" + obj[3] + "\",";
                str += "\"createDate\":\"" + obj[5] + "\",";
                str += "\"count\":\"" + obj[6] + "\"";
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

        private static string ToString_gwPart(object[] obj1, List<object[]> lists)
        {
            string str = "{";
            str += "\"SID\":\"" + obj1[0] + "\",";
            str += "\"name\":\"" + obj1[2] + "\",";

            int size = lists.Count;
            if (size <= 0)
            {
                str += "\"gwLists\":[]}";
                return str;
            }
            str += "\"gwLists\":[";
            for (int i = 0; i < size; i++)
            {
                object[] obj = lists[i];
                str += "{";
                str += "\"gwId\":\"" + obj[0] + "\",";
                str += "\"gwName\":\"" + obj[1] + "\",";
                str += "\"gwAddress\":\"" + obj[2] + "\",";
                str += "\"gwPort\":\"" + obj[3] + "\",";
                str += "\"createDate\":\"" + obj[5] + "\",";
                str += "\"count\":\"" + obj[6] + "\"";
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

        private static string ToString_limit(List<object[]> lists)
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
                str += "\"gwName\":\"" + obj[1] + "\",";
                str += "\"SID\":\"" + obj[2] + "\",";
                str += "\"name\":\"" + obj[3] + "\",";
                str += "\"createDate\":\"" + obj[4] + "\",";
                str += "\"count\":\"" + obj[5] + "\"";
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
        private static string ToString(List<object[]> lists)
        {
            
            int size=lists.Count;
            if(size<=0){
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < size;i++ )
            {
                object[] obj=lists[i];
                str += "{";
                str += "\"gwId\":\"" + obj[0] + "\",";
                str += "\"gwName\":\"" + obj[1] + "\",";
                str += "\"gwAddress\":\"" + obj[2] + "\",";
                str += "\"gwPort\":\"" + obj[3] + "\",";
                str += "\"SID\":\"" + obj[4] + "\",";
                str += "\"createDate\":\"" + obj[5] + "\",";
                str += "\"count\":\"" + obj[6] + "\"";
                if (i == (size - 1))
                {
                    str += "}";
                }
                else {
                    str += "},";
                }
            }
            str+="]}";
            return str;
        }

        private static string ToGwidString(List<object[]> lists)
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
                str += "\"gwName\":\"" + obj[1] + "\"";
            
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

        
    }
}

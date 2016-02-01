using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Site;
using Uni.YDC.Dao.Site.Entity;
using Uni.Core.Common.Utils;
using System.Web.Script.Serialization;

namespace Uni.YDC.Web.Site.Service
{
    public class WifiGWService
    {
        private static Dictionary<string, string> CachedGWAddress = new Dictionary<string, string>();
        /*
         * auth.ProcessRequest
         * */
        public static bool IsGWIdExists(string gwId, string mac)
        {
            //string obj = "";
            //try
            //{
            //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
            //    string[] args = new string[2];
            //    args[0] = gwId;
            //    args[1] = mac;
            //    obj = WSHelper.InvokeWebService(url, "IsGWIdExists", args).ToString();
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            //return obj.Equals("true") ? true : false;
            string SID = gwId.Substring(3, 10);
            if (NetPreventService.IsExitMac(SID, mac))
            {
                return false;
            }
            else
            {
                return WifiGWDao.Exists(gwId);
            }

        }
        /*
         * login.ProcessRequest
         * */
        public static bool AuthWifiGW(string gwId, string gwAddr, string gwPort, string host)
        {
            string addr = gwAddr + ":" + gwPort + ":" + host;

            lock (CachedGWAddress)
            {
                if (!CachedGWAddress.ContainsKey(gwId))
                {
                    //string obj = "";
                    //try
                    //{
                    //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
                    //    string[] args = new string[5];
                    //    args[0] = gwId;
                    //    args[1] = gwAddr;
                    //    args[2] = gwPort;
                    //    args[3] = host;
                    //    args[4] = "0";
                    //    obj = WSHelper.InvokeWebService(url, "AuthWifiGW", args).ToString();
                    //}
                    //catch (Exception)
                    //{
                    //    return false;
                    //}

                    WifiGWEntity gw = WifiGWDao.Query(gwId);
                    if (gw == null)
                    {
                        return false;
                    }

                    if (!string.Format("{0}:{1}:{2}", gw.Address, gw.Port, gw.RemoteHost).Equals(addr))
                    {
                        WifiGWDao.Update(gwId, gwAddr, gwPort, host);
                    }

                    CachedGWAddress.Add(gwId, addr);
                    return true ;
                }
                else
                {
                    if (!CachedGWAddress[gwId].Equals(addr))
                    {
                        //string obj = "";
                        //try
                        //{
                        //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
                        //    string[] args = new string[5];
                        //    args[0] = gwId;
                        //    args[1] = gwAddr;
                        //    args[2] = gwPort;
                        //    args[3] = host;
                        //    args[4] = "1";
                        //    obj = WSHelper.InvokeWebService(url, "AuthWifiGW", args).ToString();
                        //}
                        //catch (Exception)
                        //{
                        //    return false;
                        //}
                        WifiGWDao.Update(gwId, gwAddr, gwPort, host);
                        CachedGWAddress[gwId] = addr;
                    }

                    return true;
                }
            }
        }
        /*
         * portal.ProcessRequest
         * */
        public static string[] GetAuthAddress(string gwId)
        {
            if (CachedGWAddress.ContainsKey(gwId))
            {
                return CachedGWAddress[gwId].Split(':');
            }

            //string obj = "";
            //try
            //{
            //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
            //    string[] args = new string[1];
            //    args[0] = gwId;
            //    obj = WSHelper.InvokeWebService(url, "GetAuthAddress", args).ToString();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //WifiGWEntity gw = serializer.Deserialize<WifiGWEntity>(obj);

            WifiGWEntity gw = WifiGWDao.Query(gwId);
            if (gw == null)
            {
                return null;
            }

            return new string[] {gw.Address, gw.Port, gw.RemoteHost};
        }
        /*
         * HostAction.ProcessRequest
         * ShopDishAction.ProcessRequest
         * */
        public static string GetHostDomain(string hostId)
        {
            //string obj = "";
            //try
            //{
            //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
            //    string[] args = new string[1];
            //    args[0] = hostId;
            //    obj = WSHelper.InvokeWebService(url, "GetHostDomain", args).ToString();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //return obj;

            HostEntity host = HostDao.Query(hostId);
            if (host == null)
            {
                return null;
            }

            return host.Domain;
        }
        /*
         * WifiGWAction.GetWifiGWBySID
         * */
        public static List<WifiGWEntity> GetWifiGWBySID(string SID)
        {
            //string obj = "";
            //try
            //{
            //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
            //    string[] args = new string[1];
            //    args[0] = SID;
            //    obj = WSHelper.InvokeWebService(url, "GetWifiGWBySID", args).ToString();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //List<WifiGWEntity> se = serializer.Deserialize<List<WifiGWEntity>>(obj);
            //return se;

            return WifiGWDao.GetWifiGWBySID(SID);
        }
    }
}

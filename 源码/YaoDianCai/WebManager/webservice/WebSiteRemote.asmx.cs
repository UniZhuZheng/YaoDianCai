using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Uni.YDC.Dao.Manager.Entity;
using Uni.YDC.Web.Manager.Service;
using System.Web.Script.Serialization;

namespace Uni.WebManager.webservice
{
    [WebService(Namespace = "http://admin.yaodiancai.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebSiteRemote : System.Web.Services.WebService
    {
        /*
         * WebSite.LoginService.ShopLogin
         * */
        [WebMethod(Description = "商家登陆验证")]
        public string ShopLogin(string account, string password)
        {
            ShopEntity shop = ShopService.ShopLogin(account, password);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(shop);
        }

        /*
         * WebSite.WifiGWService.GetHostDomain
         * */
        [WebMethod(Description = "获取服务器的网址")]
        public string GetHostDomain(string hostId)
        {
            return HostService.GetHostDomain(hostId);
        }

        /*
         * WebSite.WifiGWService.GetWifiGWBySID
         * */
        [WebMethod(Description = "商家登陆验证")]
        public string GetWifiGWBySID(string SID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(WifiGWService.GetWifiGWBySID(SID));
        }

        /*
         * WebSite.WifiGWService.GetWifiGWBySID
         * */
        [WebMethod(Description = "用户无线认证")]
        public string IsGWIdExists(string gwId,string mac)
        {
            return WifiGWService.IsGWIdExists(gwId,mac)?"true":"false";
        }
        /*
         * WebSite.WifiAuthService.RefreshAuthRecord
         * */
        [WebMethod(Description = "用户无线认证保存")]
        public void RefreshAuthRecord(string sid, string gwId, string token, string mac, string ip, string incoming, string outcoming)
        {
            WifiAuthService.RefreshAuthRecord(sid,gwId, token, mac, ip,Convert.ToInt32(incoming),Convert.ToInt32(outcoming));
            
        }
        /*
         * WebSite.WifiGWService.AuthWifiGW
         * */
        [WebMethod(Description = "用户无线认证保存")]
        public string AuthWifiGW(string gwId, string gwAddr, string gwPort, string host, string type)
        {
            //type=0时表示第一次添加，type=1时表示第N次添加
            return WifiGWService.AuthWifiGW(gwId, gwAddr, gwPort, host, type);
        }

        /*
         * WebSite.WifiGWService.GetAuthAddress
         * */
        [WebMethod(Description = "获取商家无线认证地址")]
        public string GetAuthAddress(string gwId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(WifiGWService.GetAuthAddress(gwId));
        }

        /*
         * WebSite.
         * */
        [WebMethod(Description = "新增屏蔽用户")]
        public string NetPreventInsert(string SID, string MAC)
        {
            return NetPreventService.Insert(SID, MAC) ? "true" : "false";
        }
    }
}

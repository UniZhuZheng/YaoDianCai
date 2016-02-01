using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;
using Uni.YDC.Web.Manager;


namespace Uni.WebManager.WebService
{
    [WebService(Namespace = "http://admin.yaodiancai.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ClientCommonRemote : System.Web.Services.WebService
    {
        [WebMethod(Description = "商家登录验证")]
        public string ShopLogin(string account, string password)
        {
            ShopEntity shop = ShopService.ShopLogin(account, password);
            if (shop == null)
            {
                return "[{\"ok\":false}]";
            }

            string str = "[{\"ok\":true,";
            str += "\"SID\":\"" + shop.SID + "\",";
            str += "\"account\":\"" + shop.Account + "\",";
            str += "\"name\":\"" + shop.Name + "\",";
            str += "\"hostName\":\"" + shop.HostName + "\",";
            str += "\"domain\":\"" + HostService.GetHostDomain(shop.HostId) + "\",";
            str += "\"phone\":\"" + shop.Phone + "\",";
            str += "\"email\":\"" + shop.Email + "\",";
            str += "\"address\":\"" + shop.Address + "\",";
            str += "\"contact\":\"" + shop.Contact + "\"";
            str += "}]";

            return str;
        }

        [WebMethod(Description = "商家内网修改ip验证")]
        public string InnerNetLogin(string account, string password, string IP, string port)
        {
            return InnerNetService.Insert(account,password,IP,port);
        }
        [WebMethod(Description = "商家内网状态修改")]
        public bool InnerNetUpdateStatus(string SID, string status)
        {
            return InnerNetService.Update(SID, Convert.ToInt32(status));
        }
    }
}

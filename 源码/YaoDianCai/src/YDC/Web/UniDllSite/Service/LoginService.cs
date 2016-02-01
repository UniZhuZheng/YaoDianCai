using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Site.Entity;
using Uni.YDC.Dao.Site;
using System.Web.Script.Serialization;

namespace Uni.YDC.Web.Site.Service
{

    public class LoginService
    {
        /*
         * Login.ProcessRequest
         * */
        public static ShopEntity ShopLogin(string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            //string obj = "";
            //try
            //{
            //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
            //    string[] args = new string[2];
            //    args[0] = account;
            //    args[1] = password;
            //    obj = WSHelper.InvokeWebService(url, "ShopLogin", args).ToString();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //ShopEntity se = serializer.Deserialize<ShopEntity>(obj);
            //return se;

            return ShopDao.Query(account, password);
        }
    }
}

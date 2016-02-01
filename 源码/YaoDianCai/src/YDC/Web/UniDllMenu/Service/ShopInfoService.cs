using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class ShopInfoService
    {
        /*
         * Login.ProcessRequest
         * */
        public static string ShopLogin(string account,string password){
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return "";
            }

            string obj = "";
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[2];
                args[0] = account;
                args[1] = password;
                obj = WSHelper.InvokeWebService(url, "ShopLogin", args).ToString();
            }
            catch (Exception)
            {
                return "";
            }
            return obj;
        }
        /*
         * ShopInfoService.Page_Load
         * */
        public static ShopInfoEntity GetShopInfo(string sid)
        {
            return new ShopInfoDao(sid).Query();
        }
        /*
         * WebManagerRemote.UpdateShopInfo
         * */
        public static void SetShopInfo(string sid, string phone, string email, string contact, string address)
        {
            new ShopInfoDao(sid).Update(phone, email, contact, address);
        }
    }
}

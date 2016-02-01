using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class ShopService
    {
        /*
         * ClientCommonRemote.ShopLogin
         * WebSiteRemote.ShopLogin
         * InnerNetService.Insert
         * */
        public static ShopEntity ShopLogin(string name, string password)
        {
            return ShopDao.Query(name, password);
        }
        /*
         * ShopAction.ShopNew
         * */
        public static ShopEntity CreateShop(string account, string password, string name, string hostId, string phone, string email, string contact, string address, int wifiCount)
        {
            string sid = ShopDao.NewShopId(hostId);
            if (string.IsNullOrEmpty(sid))
            {
                return null;
            }

            bool ret = false;
            try
            {
                // Todo : WebServiceInterface
                string domain = HostService.GetHostDomain(hostId);
                string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
                string[] args = new string[8];
                args[0] = sid;
                args[1] = account;
                args[2] = name;
                args[3] = hostId;
                args[4] = phone;
                args[5] = email;
                args[6] = address;
                args[7] = contact;
                object obj = WSHelper.InvokeWebService(url, "CreateShop", args);
                ret = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return null;
            }

            if (!ret)
            {
                return null;
            }
            ShopDao.Insert(sid, account, password, name, hostId, phone, email, contact, address, wifiCount);

            return new ShopEntity
            {
                SID = sid,
                Name = name,
                Account = account,
                HostId = hostId,
                Phone = phone,
                Email = email,
                Contact = contact,
                Address = address
            };
        }
        /*
         * ShopAction.ShopBaseInfoUpdate
         * */
        public static bool SetShopBaseInfo(string sid, string phone, string email, string contact, string address)
        {
            bool ret = false;
            try
            {
                // Todo : WebServiceInterface
                string domain = HostService.GetHostDomain(sid.Substring(0, 4));
                string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
                string[] args = new string[5];
                args[0] = sid;
                args[1] = phone;
                args[2] = email;
                args[3] = contact;
                args[4] = address;
                object result = WSHelper.InvokeWebService(url, "UpdateShopInfo", args);
                ret = Convert.ToBoolean(result);
            }
            catch (Exception)
            {
                return false;
            }

            if (!ret)
            {
                return false;
            }

            ShopDao.Update(sid, phone, email, contact, address);
            return true;
        }
        /*
         * ShopAction.ShopNameExists
         * */
        public static bool IsExistShopName(string name)
        {
            if (ShopDao.CountByName(name) > 0)
            {
                return true;
            }

            return false;
        }
        /*
         * ShopAction.ShopAccountExists
         * */
        public static bool IsExistShopAccount(string account)
        {
            if (ShopDao.CountByAccount(account) > 0)
            {
                return true;
            }

            return false;
        }
        /*
         * ShopAction.ShopPasswordUpdate
         * */
        public static void SetNewPassword(string sid, string password)
        {
            //bool ret = false;
            //try
            //{
            //    // Todo : WebServiceInterface
            //    string domain = HostService.GetHostDomain(sid.Substring(0, 4));
            //    string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
            //    string[] args = new string[2];
            //    args[0] = sid;
            //    args[1] = password;
            //    WSHelper.InvokeWebService(url, "UpdateShopPwd", args);
            //}
            //catch (Exception)
            //{
            //    return;
            //}

            ShopDao.UpdatePassword(sid, password);
        }
        /*
         * ShopAction.ShopBaseInfo
         * ShopLabelService.ShopLabelAdd
         * 
         * */

        public static ShopEntity GetShopBaseInfo(string sid)
        {
            return ShopDao.Query(sid);
        }
        /*
         * ShopAction.ShopListAllLimit
         * */
        public static List<ShopEntity> GetAllShopLimit(int firstResult, int maxResult)
        {
            return ShopDao.QueryAll(firstResult, maxResult);
        }
        /*
         * ShopAction.ShopListAllCount
         * */
        public static int GetAllShopCount()
        {
            return ShopDao.Count();
        }
        /*
         * ShopAction.ShopListSearchLikeLimit
         * */
        public static List<ShopEntity> GetSearchLikeLimit(string key, int firstResult, int maxResult)
        {
            return ShopDao.QuerySearchAll(key, firstResult, maxResult);
        }
        /*
         * ShopAction.ShopListSearchLikeCount
         * */
        public static int GetSearchLikeCount(string search)
        {
            return ShopDao.CountSearch(search);
        }

        /*
         * ShopAction.OP_ShopInfoTotalRecordCount
         * */
        public static string GetShopInfoTotalRecordCount()
        {
            return ShopDao.GetTotalRecordCount();
        }
        /*
         * WebMenuRemote.UpdateDishInfo
         * */
        public static void UpdateShopBaseInfo(string SID, int dishOnSellCount, int dishOffSellCount, int dishDisableCount, int dishCount)
        {
            ShopDao.UpdateShopBaseInfo(SID, dishOnSellCount, dishOffSellCount, dishDisableCount, dishCount);
        }
        /*
         * WebMenuRemote.UpdateTableInfo
         * */
        public static void UpdateShopBaseInfo(string SID, int tableCount)
        {
            ShopDao.UpdateShopBaseInfo(SID, tableCount);
        }
    }
}

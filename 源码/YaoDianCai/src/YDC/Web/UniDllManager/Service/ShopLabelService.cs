using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager.Entity;
using Uni.YDC.Dao.Manager;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Manager.Service
{
    public class ShopLabelService
    {
        /*
         * ShopLabelAction.ShopLabelList
         * */
        public static List<ShopLabelEntity> ShopLabelList(string SID)
        {
            return ShopLabelDao.Query(SID);
        }
        /*
         * ShopLabelAction.ShopLabelAdd
         * */
        public static bool ShopLabelAdd(ShopLabelEntity se)
        {
            bool ret = false;
            try
            {
                string domain = ShopService.GetShopBaseInfo(se.SID).HostDomain;
                string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
                string[] args = new string[4];
                args[0] = se.SID;
                args[1] = se.Name;
                args[2] = se.Type.ToString();
                args[3] = se.Status.ToString();
                object obj = WSHelper.InvokeWebService(url, "ShopLabelInsert", args);
                ret = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;
            }
            if (!ret)
            {
                return false;
            }
            return ShopLabelDao.Insert(se);
        }
        /*
         * ShopLabelAction.ShopLabelDelete
         * */
        public static bool ShopLabelDelete(int id,string SID, string name)
        {
            bool ret = false;
            try
            {
                string domain = ShopService.GetShopBaseInfo(SID).HostDomain;
                string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
                string[] args = new string[2];
                args[0] = SID;
                args[1] = name;
                object obj = WSHelper.InvokeWebService(url, "ShopLabelDelete", args);
                ret = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;
            }
            if (!ret)
            {
                return false;
            }
            return ShopLabelDao.Delete(id);
        }
        /*
         * ShopLabelAction.ShopLabelExit
         * */
        public static bool ShopLabelExit(string SID, string name)
        {
            return ShopLabelDao.IsExit(SID, name);
        }
        /*
         * ShopLabelAction.ShopLabelUpdate
         * */
        public static bool ShopLabelUpdate(int id, string name, string SID, string oldName)
        {
            bool ret = false;
            try
            {
                string domain = ShopService.GetShopBaseInfo(SID).HostDomain;
                string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
                string[] args = new string[3];
                args[0] = SID;
                args[1] = name;
                args[2] = oldName;
                object obj = WSHelper.InvokeWebService(url, "ShopLabelUpdate", args);
                ret = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;
            }
            if (!ret)
            {
                return false;
            }
            return ShopLabelDao.Update(id, name);
        }
        /*
         * ShopLabelAction.ShopLabelLimit_Count
         * */
        public static int GetShopCount()
        {
            return ShopDao.Count();
        }
        /*
         * ShopLabelAction.ShopLabelLimit
         * */
        public static List<object[]> GetShopTabList_1(int firstResult, int maxResult)
        {
            return ShopLabelDao.GetShopTabList_1(firstResult,maxResult);
        }/*
         * ShopLabelAction.ShopLabelLimit
         * */
        public static List<object[]> GetShopTabList_2(int firstResult, int maxResult)
        {
            return ShopLabelDao.GetShopTabList_2(firstResult, maxResult);
        }
        /*
         * ShopLabelAction.ShopLabelSearchLimit_Count
         * */
        public static int GetShopCount(string search)
        {
            return ShopDao.CountSearch(search);
        }
        /*
         * ShopLabelAction.ShopLabelSearchLimit
         * */
        public static List<object[]> GetShopTabList_1(string search, int firstResult, int maxResult)
        {
            return ShopLabelDao.GetShopTabList_1(search,firstResult, maxResult);
        }
        /*
         * ShopLabelAction.ShopLabelSearchLimit
         * */
        public static List<object[]> GetShopTabList_2(string search, int firstResult, int maxResult)
        {
            return ShopLabelDao.GetShopTabList_2(search, firstResult, maxResult);
        }
        /*
         * ShopLabelAction.ShopLabelCountList
         * */
        public static List<object[]> ShopLabelCountList(string SID)
        {
            return ShopLabelDao.ShopLabelCountList(SID);
        }
        /*
         * ShopLabelAction.ShopLabelComment_Count
         * */
        public static int ShopLabelComment_Count(string SID)
        {
            return ShopLabelDao.ShopLabelComment_Count(SID);
        }
        /*
         * ShopLabelAction.ShopLabelComment_List
         * */
        public static List<ShopCommentEntity> ShopLabelComment_List(string SID, int firstResult, int maxResult)
        {
            return ShopLabelDao.ShopLabelComment_List(SID,firstResult,maxResult);
        }
        /*
         * WebMenuRemote.LabelsUpdateCount
         * */
        public static bool LabelsUpdateCount(string SID, string name, int count)
        {
            return ShopLabelDao.LabelsUpdateCount(SID, name, count);
        }
    }
}

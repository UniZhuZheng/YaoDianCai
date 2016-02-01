using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class ShopLabelService
    {
        /*
         * ShopLabelAction.GetShopLabelList
         * */
        public static List<ShopLabelEntity> GetShopLabelList(string SID) {
            return new ShopLabelDao(SID).Query();
        }
        /*
         * ShopLabelAction.LabelsUpdateCount
         * */
        public static bool LabelsUpdateCount(string SID, string name, int count)
        {
            bool ret = false;
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[3];
                args[0] = SID;
                args[1] = name;
                args[2] =Convert.ToString(count);
                object obj = WSHelper.InvokeWebService(url, "LabelsUpdateCount", args);
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

            return new ShopLabelDao(SID).Update(name,count);
        }
        /*
         * WebManagerRemote.ShopLabelInsert
         * */
        public static bool ShopLabelInsert(ShopLabelEntity u)
        {
            return new ShopLabelDao(u.SID).Insert(u);
        }
        /*
         * WebManagerRemote.ShopLabelUpdate
         * */
        public static bool ShopLabelUpdate(string sid, string oldName, string newName)
        {
            return new ShopLabelDao(sid).Update(oldName, newName);
        }
        /*
         * WebManagerRemote.ShopLabelDelete
         * */
        public static bool ShopLabelDelete(string sid, string name)
        {
            return new ShopLabelDao(sid).Delete(name);
        }

        public static List<ShopCommentEntity> ShopLabelComment_List(string SID, int firstResult, int maxResult)
        {
            return new ShopCommentDao(SID).ShopLabelComment_List(firstResult, maxResult);
        }

        public static int ShopLabelComment_Count(string SID)
        {
            return new ShopCommentDao(SID).ShopLabelComment_Count();
        }

        public static List<ShopLabelEntity> ShopLabelCountList(string SID)
        {
            return new ShopLabelDao(SID).Query();
        }
    }
}

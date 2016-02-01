using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class ShopCommentService
    {
        /*
         * ShopCommentAction.ShopCommentAdd
         * */
        public static bool ShopCommentAdd(string SID, string comment)
        {
            bool ret = false;
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[2];
                args[0] = SID;
                args[1] = comment;
                object obj =  WSHelper.InvokeWebService(url, "ShopCommentAdd", args);
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
            return new ShopCommentDao(SID).Insert(comment);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;

namespace Uni.YDC.Web.Manager.Service
{
    public class ShopCommentService
    {
        /*
         * WebMenuRemote.ShopCommentAdd
         * */
        public static bool ShopCommentAdd(string SID, string comment)
        {
            return ShopCommentDao.Insert(SID,comment);
        }
    }
}

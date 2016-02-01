using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Site.Entity;
using Uni.YDC.Dao.Site;

namespace Uni.YDC.Web.Site.Service
{
    /*
     * ShopDishAction.ProcessRequest
     * portal.ProcessRequest
     * */
    public class InnerNetService
    {
        public static InnerNetEntity QueryBySID(string SID) {
            return InnerNetDao.QueryBySID(SID);
        }
    }
}

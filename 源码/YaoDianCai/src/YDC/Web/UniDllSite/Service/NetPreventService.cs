using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Site;

namespace Uni.YDC.Web.Site.Service
{
    public class NetPreventService
    {
        /*
         * WifiGWService.IsGWIdExists
         * */
        public static bool IsExitMac(string SID, string MAC)
        {
            return NetPreventDao.IsExitMac(SID, MAC);
        }
    }
}

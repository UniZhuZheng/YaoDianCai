using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class NetPreventService
    {
        /*
         * WifiGWService.IsGWIdExists
         * */
        public static bool IsExitMac(string SID, string MAC)
        {
            return NetPreventDao.IsExitMac(SID,MAC);
        }
        /*
         * WebSiteRemote.NetPreventInsert
         * NetPreventAction.NetPreventAdd
         * */
        public static bool Insert(string SID, string MAC)
        {
            return NetPreventDao.Insert(SID, MAC);
        }
        /*
         * NetPreventAction.GetNetPreventList
         * */
        public static List<NetPreventEntity> GetNetPreventList(string SID)
        {
            return NetPreventDao.GetNetPreventList(SID);
        }

        /*
         * NetPreventAction.NetPreventDelete
         * */
        public static bool Delete(string SID, string MAC)
        {
            return NetPreventDao.Delete(SID, MAC);
        }
    }
}

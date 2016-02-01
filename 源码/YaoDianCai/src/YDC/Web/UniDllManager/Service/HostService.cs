using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class HostService
    {
        /*
         * HostAction.HostList
         * HostAction.HostSelection
         * */
        public static List<HostEntity> GetAllHosts()
        {
            return HostDao.QueryAll();
        }
        /*
         * ShopService.SetShopBaseInfo
         * ShopService.SetNewPassword
         * WebSiteRemote.GetHostDomain
         * */
        public static string GetHostDomain(string hostId)
        {
            HostEntity host =  HostDao.Query(hostId);
            if (host == null)
            {
                return "";
            }

            return host.Domain;
        }
    }
}

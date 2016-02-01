using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class ManagementService
    {
        /*
         * Login.ProcessRequest
         * */
        public static int Login(string account, string password)
        {
            return ManagementDao.Login(account, password);
        }

        public static ManagementsEntity Login(string account)
        {
            return ManagementDao.Login(account);
        }
    }
}

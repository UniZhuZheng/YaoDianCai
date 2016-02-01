using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Site
{
    public class NetPreventDao
    {
        /*
         * NetPreventService.IsExitMac
         * */
        public static bool IsExitMac(string SID, string MAC)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("netprevent").SelectCount("id").Where("SID", SID).Where("MAC", MAC);
                return dbManager.ExecuteScalar<int>(q) > 0 ? true : false;
            }
        }
    }
}

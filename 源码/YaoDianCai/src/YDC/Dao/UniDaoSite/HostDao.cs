using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using Uni.YDC.Dao.Site.Entity;

namespace Uni.YDC.Dao.Site
{
    public class HostDao
    {
        /*
         * WifiGWService.GetHostDomain
         * */
        public static HostEntity Query(string hostId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlQuery q = new SqlQuery("hosts").Select("id", "hostId", "name", "domain", "ip", "createDate").Where("hostId", hostId);
                    return dbManager.ExecuteList(q).ConvertAll(r => ToHostEntity(r)).First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private static HostEntity ToHostEntity(object[] r)
        {
            HostEntity h = new HostEntity
            {
                Id = Convert.ToInt32(r[0]),
                HostId = (string)r[1],
                Name = (string)r[2],
                Domain = (string)r[3],
                Ip = (string)r[4],
                CreateDate = Convert.ToDateTime(r[5])
            };
            return h;
        }
    }
}

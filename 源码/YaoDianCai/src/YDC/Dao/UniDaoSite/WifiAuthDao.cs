using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using Uni.YDC.Dao.Site.Entity;


namespace Uni.YDC.Dao.Site
{
    public class WifiAuthDao
    {
        /*
         * WifiAuthService.RefreshAuthRecord
         * */
        public static void Insert(string sid, string gwId, string token, string mac, string ip, int incoming, int outcoming)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert sql = new SqlInsert("wifiauth")
                    .InColumnValue("SID", sid)
                    .InColumnValue("gwId", gwId)
                    .InColumnValue("token", token)
                    .InColumnValue("mac", mac)
                    .InColumnValue("userIp", ip)
                    .InColumnValue("incoming", incoming)
                    .InColumnValue("outcoming", outcoming)
                    .InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(sql);

                SqlQuery q = new SqlQuery("shops").Select("wifiAuthCount").Where("SID", sid);
                int count = dbManager.ExecuteScalar<int>(q) + 1;

                SqlUpdate u = new SqlUpdate("shops").Set("wifiAuthCount", count).Where("SID", sid);
                dbManager.ExecuteNonQuery(u);

                tx.Commit();
            }
        }
        /*
         * WifiAuthService.RefreshAuthRecord
         * */
        public static void Update(string sid, string gwId, string token, string mac, string ip, int incoming, int outcoming)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("wifiauth").Set("incoming", incoming).Set("outcoming", outcoming).Set("userIp", ip).Where("SID", sid).Where("gwId", gwId).Where("token", token).Where("mac", mac);
                dbManager.ExecuteNonQuery(sql);
            }
        }
        /*
         * WifiAuthService.RefreshAuthRecord
         * */
        public static bool Exists(string sid, string token, string mac)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("wifiauth").SelectCount().Where("SID", sid).Where("token", token).Where("mac", mac);
                return dbManager.ExecuteScalar<int>(sql) > 0;
            }
        }
    }
}

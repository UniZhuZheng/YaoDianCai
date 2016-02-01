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
    public class WifiGWDao
    {
        /*
         * WifiGWService.IsGWIdExists
         * */
        public static bool Exists(string gwId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("wifigw").SelectCount().Where("gwId", gwId);
                return dbManager.ExecuteScalar<int>(sql) > 0;
            }
        }
        /*
         * WifiGWService.AuthWifiGW
         * WifiGWService.GetAuthAddress
         * */
        public static WifiGWEntity Query(string gwId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlQuery sql = new SqlQuery("wifigw").Select("id", "SID", "gwId", "gwName", "gwAddress", "gwPort", "remoteHost", "createDate").Where("gwId", gwId);
                    return dbManager.ExecuteList(sql).ConvertAll(r => ToWifiGWEntity(r)).First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        /*
         * WifiGWService.AuthWifiGW
         * */
        public static void Update(string gwId, string gwAddress, string gwPort, string remoteHost)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("wifigw").Set("gwAddress", gwAddress).Set("gwPort", gwPort).Set("remoteHost", remoteHost).Where("gwId", gwId);
                dbManager.ExecuteNonQuery(sql);
            }
        }
        /*
         * WifiGWService.GetWifiGWBySID
         * */
        public static List<WifiGWEntity> GetWifiGWBySID(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlQuery sql = new SqlQuery("wifigw").Select("id", "SID", "gwId", "gwName", "gwAddress", "gwPort", "remoteHost", "createDate").Where("SID", SID);
                    return dbManager.ExecuteList(sql).ConvertAll(r => ToWifiGWEntity(r));
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        private static WifiGWEntity ToWifiGWEntity(object[] r)
        {
            WifiGWEntity u = new WifiGWEntity
            {
                Id = Convert.ToInt32(r[0]),
                SID = (string)r[1],
                GwId = (string)r[2],
                Name = (string)r[3],
                Address = (string)r[4],
                Port = (string)r[5],
                RemoteHost = (string)r[6],
                CreateDate = Convert.ToDateTime(r[7])
            };
            return u;
        }

        
    }
}

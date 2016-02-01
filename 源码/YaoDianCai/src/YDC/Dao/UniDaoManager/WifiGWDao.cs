using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using System.Data;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.YDC.Dao.Manager
{
    public class WifiGWDao
    {
        /*
         * WifiGWService.NewOne
         * */
        public static string Insert(string sid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlQuery q = new SqlQuery("wifigw").SelectMax("gwCount").Where("SID", sid);
                int num = dbManager.ExecuteScalar<int>(q);
                num += 101;
                if (num >= 200)
                {
                    // Todo : 无线认证路由超出最大值，做相应处理
                    return null;
                }

                string gwId = "YDC" + sid + num.ToString().Substring(1, 2);

                SqlInsert i = new SqlInsert("wifigw").
                    InColumnValue("gwId", gwId).
                    InColumnValue("SID", sid).
                    InColumnValue("gwCount", num - 100).
                    InColumnValue("gwName", "默认").
                    InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(i);

                q = new SqlQuery("shops").Select("wifiGWCount").Where("SID", sid);
                int count = dbManager.ExecuteScalar<int>(q) + 1;

                SqlUpdate u = new SqlUpdate("shops").Set("wifiGWCount", count).Where("SID", sid);
                dbManager.ExecuteNonQuery(u);

                tx.Commit();
                return gwId;
            }
        }
        /*
         * WifiGWService.GetShopWifiGW
         * WifiGWService.GetShopWifiGWSearch
         * WifiGWService.GetAllWifiGWByShop
         * */
        public static List<WifiGWEntity> QueryAll(string sid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifigw wg").
                    Select("wg.id", "wg.SID", "wg.gwId", "wg.gwName", "wg.gwAddress", "wg.gwPort", "wg.createDate").SelectCount("wa.id").
                    LeftOuterJoin("wifiauth wa", Exp.EqColumns("wg.gwId", "wa.gwId")).
                    Where("wg.SID", sid).
                    GroupBy("wg.id", "wg.SID", "wg.gwId", "wg.gwName", "wg.gwAddress", "wg.gwPort", "wg.createDate");
                
                return dbManager.ExecuteList(q).ConvertAll(r => ToWifiGWEntity(r));
            }
        }
        /*
         * WifiGWService.GetWifiGWList
         * */
        public static List<WifiGWEntity> QueryAll(string sid, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifigw wg").
                    Select("wg.id", "wg.SID", "wg.gwId", "wg.gwName", "wg.gwAddress", "wg.gwPort", "wg.createDate").SelectCount("wa.id").
                    LeftOuterJoin("wifiauth wa", Exp.EqColumns("wg.gwId", "wa.gwId")).
                    Where("wg.SID", sid).
                    GroupBy("wg.id", "wg.SID", "wg.gwId", "wg.gwName", "wg.gwAddress", "wg.gwPort", "wg.createDate").
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("wifigw").Select("id").Where("SID", sid).SetMaxResults(firstResult);
                    q.Where(!new InExp("wg.id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToWifiGWEntity(r));
            }
        }
        /*
         * WifiGWService.GetWifiGWListCount
         * 
         * */
        public static int Count(string sid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifigw").SelectCount().Where("SID", sid);
                return dbManager.ExecuteScalar<int>(q);
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
                    return dbManager.ExecuteList(sql).ConvertAll(r => ToWifiGWEntity1(r));
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
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
         * */

        public static WifiGWEntity Query(string gwId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlQuery sql = new SqlQuery("wifigw").Select("id", "SID", "gwId", "gwName", "gwAddress", "gwPort", "remoteHost", "createDate").Where("gwId", gwId);
                    return dbManager.ExecuteList(sql).ConvertAll(r => ToWifiGWEntity1(r)).First();
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
        public static string Update(string gwId, string gwAddr, string gwPort, string host)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("wifigw").Set("gwAddress", gwAddr).Set("gwPort", gwPort).Set("remoteHost", host).Where("gwId", gwId);
                dbManager.ExecuteNonQuery(sql);

                return "true";
            }
        }
        private static WifiGWEntity ToWifiGWEntity1(object[] r)
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
                CreateDate = Convert.ToDateTime(r[6]),
                AuthCount = Convert.ToInt32(r[7])
            };
            return u;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.YDC.Dao.Manager
{
    public class WifiAuthDao
    {
        //public static void Insert(string gwId, string token, string mac, string ip, int incoming, int outcoming)
        //{
        //    string sid = gwId.Substring(3, 10);

        //    using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
        //    using (IDbTransaction tx = dbManager.BeginTransaction())
        //    {
        //        SqlInsert sql = new SqlInsert("wifiauth")
        //            .InColumnValue("SID", sid)
        //            .InColumnValue("gwId", gwId)
        //            .InColumnValue("token", token)
        //            .InColumnValue("mac", mac)
        //            .InColumnValue("userIp", ip)
        //            .InColumnValue("incoming", incoming)
        //            .InColumnValue("outcoming", outcoming)
        //            .InColumnValue("createDate", DateTime.Now);
        //        dbManager.ExecuteNonQuery(sql);

        //        SqlQuery q = new SqlQuery("shops").Select("wifiAuthCount").Where("SID", sid);
        //        int count = dbManager.ExecuteScalar<int>(q) + 1;

        //        SqlUpdate u = new SqlUpdate("shops").Set("wifiAuthCount", count).Where("SID", sid);
        //        dbManager.ExecuteNonQuery(u);

        //        tx.Commit();
        //    }
        //}
        /*
         * 
         * WifiAuthService.GetWifiAuth
         * */
        public static List<WifiAuthEntity> QueryAll(string gwId, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifiauth").
                    Select("id", "SID", "gwId", "token", "mac", "userIp", "createDate").
                    Where("gwId", gwId).
                    OrderBy("createDate",false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("wifiauth").Select("id").Where("gwId", gwId).OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToWifiAuthEntity(r));
            }
        }
        /*
         * WifiAuthService.GetWifiAuthByDate
         * */
        public static List<WifiAuthEntity> QueryAll(string gwId, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifiauth").
                    Select("id", "SID", "gwId", "token", "mac", "userIp", "createDate").
                    Where("gwId", gwId).
                    Where(new BetweenExp("createDate", startDate, endDate)).
                    OrderBy("createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("wifiauth").Select("id").Where("gwId", gwId).
                        Where(new BetweenExp("createDate", startDate, endDate)).OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToWifiAuthEntity(r));
            }
        }
        /*
         * WifiAuthService.GetCount
         * */
        public static int Count(string gwId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifiauth").SelectCount().Where("gwId", gwId);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * WifiAuthService.GetCountByDate
         * */
        public static int Count(string gwId, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifiauth").SelectCount().Where("gwId", gwId).Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        public static int Count(DateTime startDate, DateTime endDate,string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("wifiauth").SelectCount().Where("SID", SID).Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }

        public static bool Exists(string token, string mac)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("wifiauth").SelectCount().Where("token", token).Where("mac", mac);
                return dbManager.ExecuteScalar<int>(sql) > 0;
            }
        }

        private static WifiAuthEntity ToWifiAuthEntity(object[] r)
        {
            WifiAuthEntity u = new WifiAuthEntity
            {
                Id = Convert.ToInt32(r[0]),
                SID = (string)r[1],
                GwId = (string)r[2],
                Token = (string)r[3],
                Mac = (string)r[4],
                UserIp = (string)r[5],
                CreateDate = Convert.ToDateTime(r[6])
            };
            return u;
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
        /*
         * WifiAuthService.RefreshAuthRecord
         * */
        public static void Update(string sid, string gwId, string token, string mac, string ip, int incoming, int outcoming)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("wifiauth").
                    Set("incoming", incoming).
                    Set("outcoming", outcoming).
                    Set("userIp", ip).
                    Where("SID", sid).Where("gwId", gwId).Where("token", token).Where("mac", mac);
                dbManager.ExecuteNonQuery(sql);
            }
        }
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
         * WifiAuthService.GetDistinctionCount
         * */
        public static int GetDistinctionCount(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery qChild = new SqlQuery("netprevent").Select("MAC").Where("SID", SID);
                
                SqlQuery q = new SqlQuery("wifiauth").SelectCount("Distinct mac")
                    .Where("SID", SID).Where(!new InExp("mac", qChild));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * WifiAuthService.GetDistinctionList
         * */
        public static List<object[]> GetDistinctionList(string SID, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery qChild = new SqlQuery("netprevent").Select("MAC").Where("SID", SID);
                SqlQuery q = new SqlQuery("wifiauth").Select("mac").SelectCount("mac").
                    Where("SID", SID).Where(!new InExp("mac", qChild)).
                    GroupBy("mac");
                return dbManager.ExecuteList(q);
            }
        }
        /*
         * WifiAuthService.GetDistinctionCount
         * */
        public static int GetDistinctionCount(string SID, string searchKey)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery qChild = new SqlQuery("netprevent").Select("MAC").Where("SID", SID);

                SqlQuery q = new SqlQuery("wifiauth").SelectCount("Distinct mac")
                    .Where("SID", SID).Where(!new InExp("mac", qChild)).Where(Exp.Like("mac", searchKey, SqlLike.AnyWhere));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * WifiAuthService.GetDistinctionList
         * */
        public static List<object[]> GetDistinctionList(string SID, string searchKey, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery qChild = new SqlQuery("netprevent").Select("MAC").Where("SID", SID);
                SqlQuery q = new SqlQuery("wifiauth").Select("mac").SelectCount("mac").
                    Where("SID", SID).Where(!new InExp("mac", qChild)).Where(Exp.Like("mac", searchKey, SqlLike.AnyWhere)).
                    GroupBy("mac");
                return dbManager.ExecuteList(q);
            }
        }
    }
}

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
    public class TuanDao
    {
        /*
         * TuanService.SetTuanRecord
         * */
        public static void Insert(string sid, string shopName, string number, string website, string owner, string phone)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert i = new SqlInsert("tuanrecords").
                    InColumnValue("SID", sid).
                    InColumnValue("shopName", shopName).
                    InColumnValue("number", number).
                    InColumnValue("website", website).
                    InColumnValue("owner", owner).
                    InColumnValue("phone", phone).
                    InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(i);

                SqlQuery q = new SqlQuery("shops").Select("tuanCount").Where("SID", sid);
                int count = dbManager.ExecuteScalar<int>(q) + 1;

                SqlUpdate u = new SqlUpdate("shops").Set("tuanCount", count).Where("SID", sid);
                dbManager.ExecuteNonQuery(u);

                tx.Commit();
            }
        }
        /*
         * TuanService.GetTuanList
         * */
        public static List<TuanEntity> QueryAll(string sid, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("tuanrecords").
                    Select("id", "SID", "shopName", "number", "website", "owner", "phone", "createDate").
                    Where("SID", sid).
                    OrderBy("createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult>0)
                {
                    SqlQuery qChild = new SqlQuery("tuanrecords").Select("id").Where("SID", sid).OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                List<object[]> list = dbManager.ExecuteList(q);
                return list.ConvertAll(r => ToTuanEntity(r));
            }
        }
        /*
         * TuanService.GetTuanListByDate
         * */
        public static List<TuanEntity> QueryAll(string sid, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("tuanrecords").
                    Select("id", "SID", "shopName", "number", "webSite", "owner", "phone", "createDate").
                    Where("SID", sid).Where(new BetweenExp("createDate", startDate, endDate)).
                    OrderBy("createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("tuanrecords").Select("id").
                        Where("SID", sid).Where(new BetweenExp("createDate", startDate, endDate)).
                        OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToTuanEntity(r));
            }
        }
        /*
         * TuanService.GetTuanListCount
         * */
        public static int Count(string sid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("tuanrecords").SelectCount("id").Where("SID", sid);
                return dbManager.ExecuteScalar<int>(sql);
            }
        }
        /*
         * TuanService.GetBillListCountByDate
         * */
        public static int Count(string sid, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("tuanrecords").SelectCount("id").Where("SID", sid).Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(sql);
            }
        }

        private static TuanEntity ToTuanEntity(object[] r)
        {
            TuanEntity u = new TuanEntity
            {
                Id = Convert.ToInt32(r[0]),
                SID = (string)r[1],
                ShopName = (string)r[2],
                Number = (string)r[3],
                Website = (string)r[4],
                Owner = (string)r[5],
                Phone = (string)r[6],
                CreateDate = Convert.ToDateTime(r[7])
            };
            return u;
        }
    }
}

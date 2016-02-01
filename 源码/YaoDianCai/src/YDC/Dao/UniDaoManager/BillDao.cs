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
    public class BillDao
    {
        public static void Insert(string sid, string bid, string tableName)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert i = new SqlInsert("billrecords").
                    InColumnValue("SID", sid).
                    InColumnValue("BID", bid).
                    InColumnValue("tableName", tableName).
                    InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(i);

                SqlQuery q = new SqlQuery("shops").Select("billCount").Where("SID", sid);
                int count = dbManager.ExecuteScalar<int>(q) + 1;

                SqlUpdate u = new SqlUpdate("shops").Set("billCount", count).Where("SID", sid);
                dbManager.ExecuteNonQuery(u);

                tx.Commit();
            }
        }
        /*
         * BillService.AddBill
         * */
        public static void Insert(string sid, string bid, string tableName,string memo)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert i = new SqlInsert("billrecords").
                    InColumnValue("SID", sid).
                    InColumnValue("BID", bid).
                    InColumnValue("tableName", tableName).
                    InColumnValue("memo", memo).
                    InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(i);

                SqlQuery q = new SqlQuery("shops").Select("billCount").Where("SID", sid);
                int count = dbManager.ExecuteScalar<int>(q) + 1;

                SqlUpdate u = new SqlUpdate("shops").Set("billCount", count).Where("SID", sid);
                dbManager.ExecuteNonQuery(u);

                tx.Commit();
            }
        }
        /*
         * BillService.GetBillList
         * */
        public static List<BillEntity> QueryAll(string sid, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("billrecords bil").
                    Select("bil.BID", "bil.SID", "bil.tableName", "bil.createDate").SelectSum("dr.dishCount").SelectSum("dr.dishPrice*dr.dishCount").
                    RightOuterJoin("dishrecords dr", Exp.EqColumns("dr.BID", "bil.BID")).
                    Where("bil.SID", sid).
                    Where("dr.dishStatus", 0).
                    GroupBy("bil.id", "bil.BID", "bil.SID", "bil.tableName", "bil.createDate").
                    OrderBy("bil.createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("billrecords").Select("id").Where("SID", sid).OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("bil.id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToBillEntity(r));
            }
        }
        /*
         * BillService.GetBillListByDate
         * */
        public static List<BillEntity> QueryAll(string sid, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("billrecords bil").
                    Select("bil.BID", "bil.SID", "bil.tableName", "bil.createDate").SelectSum("dr.dishCount").SelectSum("dr.dishPrice*dr.dishCount").
                    RightOuterJoin("dishrecords dr", Exp.EqColumns("dr.BID", "bil.BID")).
                    Where("bil.SID", sid).
                    Where("dr.dishStatus", 0).
                    Where(new BetweenExp("bil.createDate", startDate, endDate)).
                    GroupBy("bil.id", "bil.BID", "bil.SID", "bil.tableName", "bil.createDate").
                    OrderBy("bil.createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("billrecords").Select("id").Where("SID", sid).
                        Where(new BetweenExp("createDate", startDate, endDate)).OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("bil.id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToBillEntity(r));
            }
        }
        /*
         * BillService.GetBillListCount
         * */
        public static int Count(string sid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("billrecords").SelectCount("id").Where("SID", sid);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * BillService.GetBillListCountByDate
         * */
        public static int Count(string sid, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("billrecords").SelectCount("id").Where("SID", sid).Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }

        private static BillEntity ToBillEntity(object[] r)
        {
            BillEntity u = new BillEntity
            {
                BID = (string)r[0],
                SID = (string)r[1],
                TableName = (string)r[2],
                CreateDate = Convert.ToDateTime(r[3]),
                DishCount = Convert.ToInt32(r[4]),
                Price = Convert.ToSingle(r[5])
            };
            return u;
        }
    }
}

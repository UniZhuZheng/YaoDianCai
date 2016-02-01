using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.YDC.Dao.Manager
{
    public class DishRecordDao
    {
        /*
         * DishRecordService.Insert
         * */
        public static bool Insert(string BID, string dishNum, string dishName, float dishPrice, int dishCount)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert sql = new SqlInsert("dishrecords").
                    InColumnValue("BID", BID).
                    InColumnValue("dishNum", dishNum).
                    InColumnValue("dishName", dishName).
                    InColumnValue("dishPrice", dishPrice.ToString("G0")).
                    InColumnValue("dishCount", dishCount).
                    InColumnValue("dishStatus", 0).
                    InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(sql);

                //SqlQuery q = new SqlQuery("shops").Select("dishSalesCount").Where("SID", sid);
                //int count = dbManager.ExecuteScalar<int>(q) + 1;

                //SqlUpdate u = new SqlUpdate("shops").Set("dishSalesCount", count).Where("SID", sid);
                //dbManager.ExecuteNonQuery(u);

                tx.Commit();
                return true;
            }
        }
        public static bool Insert(string BID, List<DishRecordEntity> lists)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert sql = new SqlInsert("dishrecords").InColumns("BID", "dishNum", "dishName", "dishPrice", "dishCount", "dishStatus", "createDate");
                foreach (DishRecordEntity dre in lists)
                {
                    sql.Values(BID, dre.Number, dre.Name, dre.Price.ToString("G0"), dre.Count, 0, DateTime.Now);
                }
                dbManager.ExecuteNonQuery(sql);

                //SqlQuery q = new SqlQuery("shops").Select("dishSalesCount").Where("SID", sid);
                //int count = dbManager.ExecuteScalar<int>(q) + 1;

                //SqlUpdate u = new SqlUpdate("shops").Set("dishSalesCount", count).Where("SID", sid);
                //dbManager.ExecuteNonQuery(u);

                tx.Commit();
                return true;
            }
        }
        /*
         * DishRecordService.GetDishRecord
         * */
        public static List<DishRecordEntity> QueryAll(string bid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("dishrecords").Select("id", "dishNum", "dishName", "dishPrice", "dishCount").Where("BID", bid).Where("dishStatus", 0).OrderBy("id", false);
                return dbManager.ExecuteList(q).ConvertAll(r => ToDishRecordEntity(r));
            }
        }
        /*
         * DishRecordService.updateOrderDishStatus
         * */
        public static bool updateOrderDishStatus(string BID, string dishNumber, string dishName, float dishPrice, int dishCount)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    if (dishCount == 1)
                    {
                        SqlUpdate u = new SqlUpdate("dishrecords").Set("dishStatus", 1).Where("BID", BID).Where("dishName", dishName);
                        dbManager.ExecuteNonQuery(u);
                    }
                    else
                    {
                        SqlUpdate u = new SqlUpdate("dishrecords").Set("dishCount", dishCount - 1).Where("BID", BID).Where("dishName", dishName);
                        dbManager.ExecuteNonQuery(u);
                        SqlInsert i = new SqlInsert("dishrecords").
                            InColumns("BID", "dishNum", "dishName", "dishPrice", "dishCount", "dishStatus", "createDate").
                            Values(BID, dishNumber, dishName, dishPrice, 1, 1,DateTime.Now);
                        dbManager.ExecuteNonQuery(i);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        private static DishRecordEntity ToDishRecordEntity(object[] r)
        {
            DishRecordEntity d = new DishRecordEntity
            {
                Id = Convert.ToInt32(r[0]),
                Number = (string)r[1],
                Name = (string)r[2],
                Price = Convert.ToSingle(r[3]),
                Count = Convert.ToInt32(r[4])
            };
            return d;
        }

        
    }
}

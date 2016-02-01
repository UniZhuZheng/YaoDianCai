using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database.Sql.Expressions;


namespace Uni.YDC.Dao.Menu
{
    public class BillDao
    {
        private readonly string SID;

        public BillDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         * BillService.AddBill
         * */
        public string insert(BillEntity bill, int cartStatus)
        {
            using (DbManager dbManager = new DbManager(SID))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert sql = new SqlInsert("mbill")
                        .InColumnValue("BID", bill.BID)
                        .InColumnValue("tableName", bill.TableName)
                        .InColumnValue("totalCount", bill.TotalCount)
                        .InColumnValue("totalPrice", bill.TotalPrice)
                        .InColumnValue("state", 0)
                        .InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(sql);

                sql = new SqlInsert("morder").InColumns("BID", "dishNumber", "dishName", "dishPrice", "dishCount", "dishStatus");
                for (int i = 0; i < bill.Orders.Count; i++)
                {
                    sql.Values(bill.BID, bill.Orders[i].DishNumber, bill.Orders[i].DishName, bill.Orders[i].DishPrice, bill.Orders[i].DishCount,0);
                }
                dbManager.ExecuteNonQuery(sql);

                SqlDelete d = new SqlDelete("mcart").Where("tableName", bill.TableName).Where("dishStatus", cartStatus);
                dbManager.ExecuteNonQuery(d);
                string BID = SID + DateTime.Now.ToString("yyyyMMddhhmmss");
                SqlUpdate q = new SqlUpdate("mtable").Set("BID", BID).Where("name", bill.TableName);
                dbManager.ExecuteNonQuery(q);
                tx.Commit();
                return BID;
            }
        }
        /*
         * BillService.AddBillPhone
         * */
        public void insertPhone(BillEntity bill, int cartStatus)
        {
            using (DbManager dbManager = new DbManager(SID))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert sql = new SqlInsert("mbill")
                        .InColumnValue("BID", bill.BID)
                        .InColumnValue("tableName", bill.TableName)
                        .InColumnValue("totalCount", bill.TotalCount)
                        .InColumnValue("totalPrice", bill.TotalPrice)
                        .InColumnValue("state", 0)
                        .InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(sql);

                sql = new SqlInsert("morder").InColumns("BID", "dishNumber", "dishName", "dishPrice", "dishCount", "dishStatus");
                for (int i = 0; i < bill.Orders.Count; i++)
                {
                    sql.Values(bill.BID, bill.Orders[i].DishNumber, bill.Orders[i].DishName, bill.Orders[i].DishPrice, bill.Orders[i].DishCount, 0);
                }
                dbManager.ExecuteNonQuery(sql);

                SqlDelete d = new SqlDelete("mcart").Where("tableName", bill.TableName).Where("dishStatus", cartStatus);
                dbManager.ExecuteNonQuery(d);
                tx.Commit();
            }
        }
        /*
         * BillService.GetAllNewBill
         * */
        public List<BillEntity> queryAllNew()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery ordersql = new SqlQuery("mbill").Select("BID", "tableName", "totalCount", "totalPrice", "state", "createDate").Where("state", 0).Where(!Exp.Eq("totalCount", 0));
                return dbManager.ExecuteList(ordersql).ConvertAll(o =>
                    {
                        BillEntity bill = new BillEntity
                        {
                            BID =(string) o[0].ToString(),
                            TableName = (string)o[1].ToString(),
                            TotalCount = Convert.ToInt32(o[2]),
                            TotalPrice = (string)o[3],
                            State = Convert.ToInt32(o[4]),
                            CreateDate = Convert.ToDateTime(o[5])
                        };
                        return bill;
                    });
            }
        }
        /*
         * BillService.GetAllBillsByTime
         * */
        public List<BillEntity> queryAll(string time)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                DateTime times = Convert.ToDateTime(time);

                SqlQuery ordersql = new SqlQuery("mbill").Select("BID", "tableName", "totalCount", "totalPrice", "state", "createDate").
                    Where(new BetweenExp("createDate", times, times.AddDays(1))).Where("state", 1).Where(!Exp.Eq("totalCount",0)).OrderBy("createDate", false);
                return dbManager.ExecuteList(ordersql).ConvertAll(o =>
                    {
                        BillEntity bill = new BillEntity()
                        {
                            BID = (string)o[0].ToString(),
                            TableName = (string)o[1].ToString(),
                            TotalCount = Convert.ToInt32(o[2]),
                            TotalPrice = (string)o[3],
                            State = Convert.ToInt32(o[4]),
                            CreateDate = Convert.ToDateTime(o[5])
                        };
                        return bill;
                    });
            }
        }
        /*
         * BillService.SetDishState
         * */
        public void updateState(string bid, int state)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u = new SqlUpdate("mbill").Set("state", state).Where("BID", bid);
                dbManager.ExecuteNonQuery(u);
            }
        }

        /**
         * BillService.GetBillList
         * */

        public  List<BillEntity> QueryAll(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mbill").
                    Select("BID","tableName", "totalCount", "totalPrice","createDate").
                    Where(!Exp.Eq("totalCount",0)).
                    OrderBy("createDate", false).
                    SetFirstResult(firstResult).
                    SetMaxResults(maxResult);
                
                return dbManager.ExecuteList(q).ConvertAll(r => ToBillEntity(r));
            }
        }
        /*
         * BillService.GetBillListByDate
         * */
        public  List<BillEntity> QueryAll(DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mbill").
                    Select("BID", "tableName", "totalCount", "totalPrice", "createDate").
                    Where(!Exp.Eq("totalCount", 0)).
                    Where(new BetweenExp("createDate", startDate, endDate)).
                    OrderBy("createDate", false).
                    SetFirstResult(firstResult).
                    SetMaxResults(maxResult);
              
                return dbManager.ExecuteList(q).ConvertAll(r => ToBillEntity(r));
            }
        }
        /*
         * BillService.GetBillListCount
         * */
        public int Count()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mbill").SelectCount();
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * BillService.GetBillListCountByDate
         * */
        public  int Count(DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mbill").SelectCount().Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        public int GetBillSales(DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mbill").SelectSum("totalPrice").Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        private BillEntity ToBillEntity(object[] r)
        {
            BillEntity u = new BillEntity
            {
                BID = (string)r[0],
                SID = SID,
                TableName = (string)r[1],
                TotalCount = Convert.ToInt32(r[2]),
                TotalPrice = (string)r[3],
                CreateDate = Convert.ToDateTime(r[4])
            };
            return u;
        }

        
    }
}

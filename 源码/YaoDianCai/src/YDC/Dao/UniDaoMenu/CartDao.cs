using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Menu
{
    public class CartDao
    {
        private readonly string SID;
        public CartDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }

        public void insert(CartEntity cart)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mcart")
                    .InColumnValue("tableName", cart.TableName)
                    .InColumnValue("dishNumber", cart.DishNumber)
                    .InColumnValue("dishName", cart.DishName)
                    .InColumnValue("dishPrice", cart.DishPrice)
                    .InColumnValue("dishCount", cart.DishCount)
                    .InColumnValue("createDate", cart.CreateDate);
                dbManager.ExecuteNonQuery(i);
            }
        }
        /*
         * CartService.UpdateDishCount
         * CartService.AddCartOne
         * */
        public void insert(string tableName, string dishName,int cartStatus)
        {
            DishEntity dish = new DishDao(SID).query(dishName);
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mcart")
                    .InColumnValue("tableName", tableName)
                    .InColumnValue("dishNumber", dish.Number)
                    .InColumnValue("dishName", dishName)
                    .InColumnValue("dishPrice", dish.Price)
                    .InColumnValue("dishCount", 1)
                    .InColumnValue("dishStatus", cartStatus)
                    .InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(i);
            }
        }
        public void update(string tableName, string dishName, int count)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u = new SqlUpdate("mcart").Set("dishCount", count).Where("dishName", dishName).Where("tableName", tableName);
                dbManager.ExecuteNonQuery(u);
            }
        }
        /*
         * CartService.GetCartJson
         * */
        public List<CartEntity> query(string tableName, int cartStatus)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery cmsql = new SqlQuery("mcart").
                    Select("tableName", "dishNumber", "dishName", "dishPrice").
                    SelectCount("dishCount").
                    Where("tableName", tableName).Where("dishStatus", cartStatus).
                    GroupBy("tableName", "dishNumber", "dishName", "dishPrice");
                return dbManager.ExecuteList(cmsql).ConvertAll(sc =>
                    {
                        CartEntity cart = new CartEntity()
                        {
                            TableName = (string)sc[0],
                            DishNumber = (string)sc[1],
                            DishName = (string)sc[2],
                            DishPrice = (string)sc[3],
                            DishCount = Convert.ToInt32(sc[4])
                        };
                        return cart;
                    });
            }
        }
        /*
         * CartService.UpdateDishCount
         * */
        public int count(string tableName, string dishName, int cartStatus)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mcart").SelectCount().Where("dishName", dishName).Where("tableName", tableName).Where("dishStatus", cartStatus);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * CartService.UpdateDishCount
         * CartService.RemoveCart
         * */
        public void delete(string tableName, string dishName, int cartStatus)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete d = new SqlDelete("mcart").Where("dishName", dishName).Where("tableName", tableName).Where("dishStatus", cartStatus);
                dbManager.ExecuteNonQuery(d);
            }
        }
        /*
         * CartAction.UpdateCartDishCount
         * CartService.RemoveCartOne
         * */
        public void deleteOne(string tableName, string dishName, int cartStatus)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery s = new SqlQuery("mcart").Select("rowid").Where("dishName", dishName).Where("tableName", tableName).Where("dishStatus", cartStatus).SetFirstResult(0).SetMaxResults(1);
                SqlDelete d = new SqlDelete("mcart").Where(new EqColumnsExp("rowid", s));
                dbManager.ExecuteNonQuery(d);
            }
        }
        /*
         * CartService.GetCartTableForShopOrder
         * */
        public List<TableEntity> GetCartTableForShopOrder(string sid)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery msql = new SqlQuery("mcart").Select("tableName").Distinct();
                return dbManager.ExecuteList(msql).ConvertAll(m =>
                {
                    TableEntity table = new TableEntity();
                    table.Name = m[0].ToString();
                    return table;
                });
            }
        }
        /*
         * ClientAndroidService.DeleteCartInfoForShopOrder
         * */
        public bool DeleteCartInfoForShopOrder(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete d = new SqlDelete("mcart").Where("tableName", tableName);
                return dbManager.ExecuteNonQuery(d)>=0?true:false;
            }
        }
    }
}

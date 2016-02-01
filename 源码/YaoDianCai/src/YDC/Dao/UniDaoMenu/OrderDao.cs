using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database;
using System.Data;
using Uni.Core.Database.Sql;
using Uni.Core.Common.Utils;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Menu
{
    public class OrderDao
    {
        private readonly string SID;


        public OrderDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         *  OrderService.GetOrderDish
         * */
        public List<OrderEntity> queryAll(string bid)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("morder").Select("BID", "tableName", "dishNumber", "dishName", "dishPrice", "dishCount", "dishStatus").
                    Where("BID", bid).Where("dishStatus", 0);
                return dbManager.ExecuteList(q).ConvertAll(o =>
                    {
                        OrderEntity order = new OrderEntity
                        {
                            BID =(string) o[0],
                            TableName = (string)o[1],
                            DishNumber = (string)o[2],
                            DishName = (string)o[3],
                            DishPrice = (string)o[4],
                            DishCount = Convert.ToInt32(o[5]),
                            DishStatus = Convert.ToInt32(o[6])
                        };
                        return order;
                    });
            }
        }
        /*
         * OrderService.GetOrderDish
         * */
        public bool updateStatus(string bid,string dishNumber, string dishName,float dishPrice, int dishCount)
        {
            using (DbManager dbManager = new DbManager(SID)) {
                try {
                    if (dishCount == 1)
                    {
                        SqlUpdate u = new SqlUpdate("morder").Set("dishStatus", 1).Where("BID", bid).Where("dishName", dishName);
                        dbManager.ExecuteNonQuery(u);

                    }
                    else
                    {
                        SqlUpdate u = new SqlUpdate("morder").Set("dishCount", (dishCount - 1)).Where("BID", bid).Where("dishName", dishName);
                        dbManager.ExecuteNonQuery(u);
                        SqlInsert i = new SqlInsert("morder").
                            InColumns("BID", "dishNumber", "dishName", "dishPrice", "dishCount", "dishStatus").
                            Values(bid, dishNumber, dishName, dishPrice, 1, 1);
                        dbManager.ExecuteNonQuery(i);
                    }
                    SqlQuery qPrice = new SqlQuery("morder").SelectSum("dishPrice*dishCount").Where("BID", bid).Where("dishStatus", 0);
                    float totalPrice = dbManager.ExecuteScalar<float>(qPrice);
                    SqlQuery qCount = new SqlQuery("morder").SelectSum("dishCount").Where("BID", bid).Where("dishStatus", 0);
                    int totalCount = dbManager.ExecuteScalar<int>(qCount);
                    SqlUpdate um = new SqlUpdate("mbill").Set("totalPrice", totalPrice.ToString("G0")).Set("totalCount", totalCount).Where("BID", bid);
                    dbManager.ExecuteNonQuery(um);
                    return true;
                }catch(Exception e){
                    return false;
                }
            }
        }
    }
}

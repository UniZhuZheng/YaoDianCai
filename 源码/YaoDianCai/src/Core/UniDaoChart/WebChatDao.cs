using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using UniDaoChart.Entity;

namespace UniDaoChart
{
    public class WebChatDao
    {
        /*
         * EventClickService.GetShopList
         * */
        //获取商家的基本信息
        public static List<ShopEntity> GetShopList() {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    List<ShopEntity> u = null;
                    SqlQuery sql = new SqlQuery("shops s").
                        Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                        LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId"));
                    u = dbManager.ExecuteList(sql).ConvertAll(r => ToShopEntity(r));
                    return u;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public static List<BillEntity> GetBillList(string sid, DateTime startDate, DateTime endDate)
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
                    OrderBy("bil.createDate", false);
                return dbManager.ExecuteList(q).ConvertAll(r => ToBillEntity(r));
            }
        }
        /*
         * WebChatService.BandShop
         * */
        public static ShopEntity Query(string openId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlQuery q = new SqlQuery("shops s").
                        Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                        LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                        LeftOuterJoin("webchat wc", Exp.EqColumns("wc.SID", "s.SID")).
                        Where("wc.openId", openId);
                    return dbManager.ExecuteList(q).ConvertAll(r => ToShopEntity(r)).First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /*
         * WebChatService.Insert
         * */
        //用户登录验证
        public static string Insert(string openId, string account, string password)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId)) {
                try
                {
                    ShopEntity u = null;
                    SqlQuery sql = new SqlQuery("shops s").
                        Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                        LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                        Where("s.account", account).Where("s.password", password);
                    u=dbManager.ExecuteList(sql).ConvertAll(r => ToShopEntity(r)).First();
                    if (u==null)
                    {
                        return "";
                    }
                    SqlInsert i = new SqlInsert("webchat").
                        InColumnValue("openId", openId).
                        InColumnValue("SID", u.SID).
                        InColumnValue("state", 0).
                        InColumnValue("createDate", DateTime.Now);
                    return dbManager.ExecuteNonQuery(i) > 0 ? u.SID : "";
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public static void Insert(string openId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlInsert i = new SqlInsert("webchat").
                        InColumnValue("openId", openId).
                        InColumnValue("state", 0).
                        InColumnValue("createDate", DateTime.Now);
                    dbManager.ExecuteNonQuery(i);
                }
                catch (Exception)
                {
                    
                }
            }
        }

        /*
         * WebChatService.Update
         * */
        //用户登录更改
        public static string Update(string openId, string account, string password)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    ShopEntity u = null;
                    SqlQuery sql = new SqlQuery("shops s").
                        Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                        LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                        Where("s.account", account).Where("s.password", password);
                    u = dbManager.ExecuteList(sql).ConvertAll(r => ToShopEntity(r)).First();
                    if (u == null)
                    {
                        return "";
                    }
                    SqlUpdate i = new SqlUpdate("webchat").
                        Set("SID", u.SID).
                        Set("state", 0).
                        Set("createDate", DateTime.Now).
                        Where("openId", openId);
                    return dbManager.ExecuteNonQuery(i) > 0 ? u.SID : "";
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        /*
         * WebChatService.IsExits
         * */
        //判断用户是否注册过
        public static string IsExits(string openId)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId)) {
                try {
                    SqlQuery i = new SqlQuery("webchat").SelectCount("SID").Where("openId", openId);
                    return dbManager.ExecuteScalar<string>(i);
                }
                catch (Exception) {
                    return "";
                }
            }
        }

        //删除openId
        public static bool Delete(string openId) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlDelete i = new SqlDelete("webchat").Where("openId", openId);
                    return dbManager.ExecuteNonQuery(i)>0?true:false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        /*
         * WebChatService.GetCountByDate
         * */
        //查询信息
        public static int[] GetCountByDate(string SID, DateTime startDate, DateTime endDate) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                int[] counts=new int[3];
                SqlQuery qBill = new SqlQuery("billrecords").SelectCount("id").Where("SID", SID).Where(new BetweenExp("createDate", startDate, endDate));
                int numOrder = dbManager.ExecuteScalar<int>(qBill);
                counts[0] = numOrder;
                SqlQuery qTuan = new SqlQuery("tuanrecords").SelectCount("id").Where("SID", SID).Where(new BetweenExp("createDate", startDate, endDate));
                int numTuan = dbManager.ExecuteScalar<int>(qTuan);
                counts[1] = numTuan;
                SqlQuery qWifi = new SqlQuery("wifiauth").SelectCount("id").Where("SID", SID).Where(new BetweenExp("createDate", startDate, endDate));
                int numAuth = dbManager.ExecuteScalar<int>(qWifi);
                counts[2] = numAuth;
                return counts;
            }
        }

        private static ShopEntity ToShopEntity(object[] r)
        {
            ShopEntity u = new ShopEntity
            {
                Id = Convert.ToInt32(r[0]),
                SID = (string)r[1],
                Name = (string)r[2],
                Account = (string)r[3],
                HostId = (string)r[4],
                Phone = (string)r[5],
                Email = (string)r[6],
                Address = (string)r[7],
                Contact = (string)r[8],
                DishOnSellCount = Convert.ToInt32(r[9]),
                DishOffSellCount = Convert.ToInt32(r[10]),
                DishDisableCount = Convert.ToInt32(r[11]),
                DishSalesCount = Convert.ToInt32(r[12]),
                DishCount = Convert.ToInt32(r[13]),
                BillCount = Convert.ToInt32(r[14]),
                TuanCount = Convert.ToInt32(r[15]),
                TableCount = Convert.ToInt32(r[16]),
                WifiGWCount = Convert.ToInt32(r[17]),
                WifiAuthCount = Convert.ToInt32(r[18]),
                TplId = Convert.ToInt32(r[19]),
                CreateDate = Convert.ToDateTime(r[20]),
                HostName = (string)r[21],
                HostDomain = (string)r[22]
            };
            return u;
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

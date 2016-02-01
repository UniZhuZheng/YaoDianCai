using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Dao.Manager
{
    public class ShopDao
    {
        public static string NewShopId(string hostId)
        {
            string sid = null;
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops").SelectMax("id").Where("hostId", hostId);
                int num = dbManager.ExecuteScalar<int>(q);
                num += 1000001;
                if (num >= 2000000)
                {
                    // ToDo : 超出最大商家数，提示运营商
                    return null;
                }

                sid = hostId + num.ToString().Substring(1, 6);
            }

            return sid;
        }
        /*
         * ShopService.GetShopBaseInfo
         * */
        public static ShopEntity Query(string sid)
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
                        Where("s.SID", sid);
                    return dbManager.ExecuteList(q).ConvertAll(r => ToShopEntity(r)).First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        /*
         * ShopService.ShopLogin
         * */
        public static ShopEntity Query(string account, string password)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                try
                {
                    SqlQuery sql = new SqlQuery("shops s").
                        Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                        LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                        Where("s.account", account).Where("s.password", password);
                    return dbManager.ExecuteList(sql).ConvertAll(r => ToShopEntity(r)).First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        /*
         * BillService.GetShop
         * ShopService.GetAllShopLimit
         * TuanService.GetShop
         * WifiGWService.GetShopWifiGW
         * */
        public static List<ShopEntity> QueryAll(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                    LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                    OrderBy("s.id", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shops").Select("id").OrderBy("id", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("s.id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopEntity(r));
            }
        }
        /*
         * BillService.GetShopSearch
         * ShopService.GetSearchLikeLimit
         * TuanService.GetShopSearch
         * WifiGWService.GetShopWifiGWSearch
         * */
        public static List<ShopEntity> QuerySearchAll(string search, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.id", "s.SID", "s.name", "s.account", "s.hostId", "s.phone", "s.email", "s.address", "s.contact",
                        "s.dishOnSellCount", "s.dishOffSellCount", "s.dishDisableCount", "s.dishSalesCount", "s.dishCount",
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name", "h.domain").
                    LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                    Where(Exp.Like("s.name", search, SqlLike.AnyWhere)).
                    OrderBy("s.id", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shops").Select("id").Where(Exp.Like("s.name", search, SqlLike.AnyWhere)).OrderBy("id", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("s.id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopEntity(r));
            }
        }
        /*
         * ShopService.CreateShop
         * */
        public static void Insert(string sid, string account, string password, string name, string hostId, string phone, string email, string contact, string address, int wifiCount)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlInsert sql = new SqlInsert("shops").
                    InColumnValue("SID", sid).
                    InColumnValue("name", name).
                    InColumnValue("account", account).
                    InColumnValue("password", password).
                    InColumnValue("hostId", hostId).
                    InColumnValue("phone", phone).
                    InColumnValue("email", email).
                    InColumnValue("contact", contact).
                    InColumnValue("address", address).
                    InColumnValue("dishOnSellCount", 0).
                    InColumnValue("dishOffSellCount", 0).
                    InColumnValue("dishDisableCount", 0).
                    InColumnValue("dishSalesCount", 0).
                    InColumnValue("dishCount", 0).
                    InColumnValue("tuanCount", 0).
                    InColumnValue("billCount", 0).
                    InColumnValue("tableCount", 0).
                    InColumnValue("wifiGWCount", wifiCount).
                    InColumnValue("wifiAuthCount", 0).
                    InColumnValue("tplId", 1).
                    InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(sql);

                SqlDelete d = new SqlDelete("wifigw").Where("SID", sid);
                dbManager.ExecuteScalar<int>(d);

                if (wifiCount > 0)
                {
                    SqlInsert si = new SqlInsert("wifigw").InColumns("SID", "gwId", "gwName", "gwAddress", "gwPort", "remoteHost", "gwCount", "createDate");
                    for (int i = 1; i <= wifiCount; i++)
                    {
                        string n = i > 9 ? i.ToString() : "0" + i.ToString();
                        string gwId = "YDC" + sid + n;
                        si.Values(sid, gwId, "Default", "", "", "", i, DateTime.Now);
                    }
                    dbManager.ExecuteNonQuery(si);
                }

                tx.Commit();
            }
        }
        /*
         * ShopService.SetShopBaseInfo
         * */
        public static void Update(string sid, string phone, string email, string address, string contact)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate q = new SqlUpdate("shops").Set("phone", phone).Set("email", email).Set("address", address).Set("contact", contact).Where("SID", sid);
                dbManager.ExecuteNonQuery(q);
            }
        }
        /*
         * ShopService.SetNewPassword
         * */
        public static void UpdatePassword(string sid, string password)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("shops").Set("password", password).Where("SID", sid);
                dbManager.ExecuteNonQuery(sql);
            }
        }
        /*
         * BillService.GetShopCount
         * ShopService.GetAllShopCount
         * TuanService.GetShopCount
         * WifiGWService.GetWifiGWCount
         * WebChatService.GetShopCount
         * ShopLabelService.GetShopCount
         * */
        public static int Count()
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("shops").SelectCount();
                return dbManager.ExecuteScalar<int>(sql);
            }
        }
        /*
         * BillService.GetSearchCount
         * ShopService.GetSearchLikeCount
         * TuanService.GetSearchCount
         * WifiGWService.GetSearchCount
         * WebChatService.GetSearchCount
         * ShopLabelService.GetShopCount
         * */
        public static int CountSearch(string search)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("shops").SelectCount().Where(Exp.Like("name", search, SqlLike.AnyWhere));
                return dbManager.ExecuteScalar<int>(sql);
            }
        }
        /*
         * ShopService.IsExistShopName
         * */
        public static int CountByName(string name)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops").SelectCount().Where("name", name);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * ShopService.IsExistShopAccount
         * */
        public static int CountByAccount(string account)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops").SelectCount().Where("account", account);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * ShopService.GetShopInfoTotalRecordCount
         * */
        public static string GetTotalRecordCount()
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery ssql = new SqlQuery("shops").SelectCount();
                int sNum= dbManager.ExecuteScalar<int>(ssql);
                SqlQuery bsql = new SqlQuery("billrecords").SelectCount();
                int bNum = dbManager.ExecuteScalar<int>(bsql);
                SqlQuery tsql = new SqlQuery("tuanrecords").SelectCount();
                int tNum = dbManager.ExecuteScalar<int>(tsql);
                SqlQuery wsql = new SqlQuery("wifiauth").SelectCount();
                int wNum = dbManager.ExecuteScalar<int>(wsql);
                SqlQuery csql = new SqlQuery("webchat").SelectCount();
                int cNum = dbManager.ExecuteScalar<int>(csql);
                SqlQuery slql = new SqlQuery("shoplabel").SelectCount();
                int lNum = dbManager.ExecuteScalar<int>(slql);
                return "{\"ok\":true,\"shopCount\":" + sNum + ",\"billCount\":" +
                    bNum + ",\"tuanCount\":" + tNum + ",\"authCount\":" + wNum + ",\"chatCount\":" + cNum + ",\"labelCount\":" + lNum + "}";
            }
        }
        /*
         * ShopService.UpdateShopBaseInfo
         * */
        public static void UpdateShopBaseInfo(string SID, int dishOnSellCount, int dishOffSellCount, int dishDisableCount, int dishCount)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("shops")
                    .Set("dishOnSellCount", dishOnSellCount)
                    .Set("dishOffSellCount", dishOffSellCount)
                    .Set("dishDisableCount", dishDisableCount)
                    .Set("dishCount", dishCount)
                    .Where("SID", SID);
                dbManager.ExecuteNonQuery(sql);
            }
        }
        /*
         * ShopService.UpdateShopBaseInfo
         * */
        public static void UpdateShopBaseInfo(string SID, int tableCount)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate sql = new SqlUpdate("shops")
                    .Set("tableCount", tableCount)
                    .Where("SID", SID);
                dbManager.ExecuteNonQuery(sql);
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
    }
}

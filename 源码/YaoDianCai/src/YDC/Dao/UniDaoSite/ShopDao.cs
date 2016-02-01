using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Site.Entity;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Site
{
    public class ShopDao
    {
        /*
         * LoginService.ShopLogin
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
                        "s.billCount", "s.tuanCount", "s.tableCount", "s.wifiGWCount", "s.wifiAuthCount", "s.tplId", "s.createDate", "h.name","h.domain").
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

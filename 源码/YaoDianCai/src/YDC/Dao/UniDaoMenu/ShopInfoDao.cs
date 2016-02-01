using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu.Entity;

namespace Uni.YDC.Dao.Menu
{
    public class ShopInfoDao
    {
        private readonly string SID;
        public ShopInfoDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         *  ShopInfoService.GetShopInfo
         *  TuanService.AddTuan
         * */
        public ShopInfoEntity Query()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery msql = new SqlQuery("mshopinfo").Select("SID", "account", "name", "hostId", "phone", "email", "address", "contact", "createDate").Where("SID", SID);
                return dbManager.ExecuteList(msql).ConvertAll(m =>
                                {
                                    ShopInfoEntity si = new ShopInfoEntity();
                                    si.SID =(string) m[0];
                                    si.Account = (string)m[1];
                                    si.Name = (string)m[2];
                                    si.HostId = (string)m[3];
                                    si.Phone = (string)m[4];
                                    si.Email = (string)m[5];
                                    si.Address = (string)m[6];
                                    si.Contact = (string)m[7];
                                    si.CreateDate = Convert.ToDateTime(m[8]);
                                    return si;
                                }).FirstOrDefault();
            }
        }
        /*
         * ShopService.CreateShop
         * */
        public void InsertOnly(ShopInfoEntity si)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete d = new SqlDelete("mshopinfo");
                dbManager.ExecuteNonQuery(d);

                SqlInsert i = new SqlInsert("mshopinfo")
                    .InColumnValue("SID", si.SID.ToString())
                    .InColumnValue("account", si.Account)
                    .InColumnValue("name", si.Name)
                    .InColumnValue("hostId", si.HostId.ToString())
                    .InColumnValue("phone", si.Phone)
                    .InColumnValue("email", si.Email)
                    .InColumnValue("address", si.Address)
                    .InColumnValue("contact", si.Contact)
                    .InColumnValue("createDate", si.CreateDate);
                dbManager.ExecuteNonQuery(i);
            }
        }
        /*
         * ShopInfoService.SetShopInfo
         * */
        public void Update(string phone, string email, string contact, string address)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate sql = new SqlUpdate("mshopinfo").Set("phone", phone).Set("email", email).Set("address", address).Set("contact", contact).Where("SID", SID);
                dbManager.ExecuteNonQuery(sql);
            }
        }
    }
}

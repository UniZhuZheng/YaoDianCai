using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Menu
{
    public class ShopLabelDao
    {
        private readonly string SID;
        public ShopLabelDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        public bool Insert(ShopLabelEntity sl) {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mshoplabel")
                    .InColumnValue("SID", sl.SID)
                    .InColumnValue("name", sl.Name)
                    .InColumnValue("type", sl.Type)
                    .InColumnValue("status", sl.Status)
                    .InColumnValue("count", sl.Count)
                    .InColumnValue("createDate", sl.CreateDate);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }

        public bool Delete(string name)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete i = new SqlDelete("mshoplabel").Where("name", name);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }

        public bool Update(string oldName, string newName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate i = new SqlUpdate("mshoplabel").Set("name", newName).Where("name", oldName);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }
        /*
         * ShopLabelService.LabelsUpdateCount
         * */
        public bool Update(string name, int count)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate i = new SqlUpdate("mshoplabel").Set("count", count).Where("name", name);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }
        /*
         * ShopLabelService.GetShopLabelList
         * */
        public List<ShopLabelEntity> Query()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mshoplabel").
                    Select("SID", "name", "type", "status", "count", "createDate");
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopLabelEntity(r));
            }
        }

        private ShopLabelEntity ToShopLabelEntity(object[] r)
        {
            ShopLabelEntity u = new ShopLabelEntity
            {
                SID = (string)r[0],
                Name = (string)r[1],
                Type = Convert.ToInt32(r[2]),
                Status = Convert.ToInt32(r[3]),
                Count = Convert.ToInt32(r[4]),
                CreateDate = Convert.ToDateTime(r[5])
            };
            return u;
        }


        
    }
}

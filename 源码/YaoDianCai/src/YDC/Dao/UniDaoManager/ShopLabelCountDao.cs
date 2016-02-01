using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Manager
{
    public class ShopLabelCountDao
    {
        public static bool AddCount(string SID,int labelId) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery s = new SqlQuery("shoplabelcount")
                    .Select("count")
                    .Where("SID", SID)
                    .Where("labelId", labelId);
                int count = dbManager.ExecuteScalar<int>(s);
                if (count > 0)
                {
                    SqlInsert i = new SqlInsert("shoplabelcount").
                        InColumnValue("SID", SID).
                        InColumnValue("labelId", labelId).
                        InColumnValue("count", 1).
                        InColumnValue("lastDate", DateTime.Now);
                    return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
                }
                else {
                    SqlUpdate u = new SqlUpdate("shoplabelcount").
                        Set("count", count+1).
                        Set("lastDate", DateTime.Now).
                        Where("SID", SID).
                        Where("labelId", labelId);
                    return dbManager.ExecuteNonQuery(u) > 0 ? true : false;
                }
            }
        }
    }
}

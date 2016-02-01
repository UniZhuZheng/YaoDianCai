using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Dao.Manager
{
    public class NetPreventDao
    {
        /*
         * NetPreventService.IsExitMac
         * */
        public static bool IsExitMac(string SID,string MAC) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("netprevent").SelectCount("id").Where("SID", SID).Where("MAC", MAC);
                return dbManager.ExecuteScalar<int>(q) > 0 ? true : false ;
            }
        }
        /*
         * NetPreventService.Insert
         * */
        public static bool Insert(string SID, string MAC)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlInsert q = new SqlInsert("netprevent").InColumnValue("SID", SID).InColumnValue("MAC", MAC);
                return dbManager.ExecuteNonQuery(q) > 0 ? true : false;
            }
        }
        /*
         * NetPreventService.GetNetPreventList
         * */
        public static List<NetPreventEntity> GetNetPreventList(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("netprevent").Select("SID","MAC").Where("SID", SID);
                return dbManager.ExecuteList(q).ConvertAll(r => ToNetPreventEntity(r));
            }
        }
        /*
         * NetPreventService.Delete
         * */
        public static bool Delete(string SID, string MAC)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlDelete q = new SqlDelete("netprevent").Where("SID", SID).Where("MAC", MAC);
                return dbManager.ExecuteNonQuery(q) > 0 ? true : false;
            }
        }
        private static NetPreventEntity ToNetPreventEntity(object[] r)
        {
            NetPreventEntity u = new NetPreventEntity
            {
                SID = (string)r[0],
                MAC = (string)r[1]
            };
            return u;
        }

        
    }
}

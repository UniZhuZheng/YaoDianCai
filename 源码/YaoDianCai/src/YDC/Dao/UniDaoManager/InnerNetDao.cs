using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Manager
{
    public class InnerNetDao
    {
        /*
         * InnerNetService.Insert
         * */
        public static bool Insert(string SID,string IP,string port) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId)) {
                SqlQuery q = new SqlQuery("innernet").Select("id").Where("SID",SID);
                if (dbManager.ExecuteScalar<int>(q) > 0)
                {
                    SqlUpdate u = new SqlUpdate("innernet").
                        Set("IP", IP).
                        Set("port", port).
                        Set("status", 1).
                        Set("createDate", DateTime.Now).
                        Where("SID", SID);
                    return dbManager.ExecuteNonQuery(u) > 0 ? true : false;
                }
                else {
                    SqlInsert i = new SqlInsert("innernet").
                        InColumnValue("SID", SID).
                        InColumnValue("IP", IP).
                        InColumnValue("port", port).
                        InColumnValue("status", 1).
                        InColumnValue("createDate", DateTime.Now);
                    return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
                }
            }
        }
        /*
         * InnerNetService.Update
         * */
        public static bool Update(string SID,int status)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId)) {
                SqlUpdate u = new SqlUpdate("innernet").
                        Set("status", status).
                        Where("SID", SID);
                return dbManager.ExecuteNonQuery(u) > 0 ? true : false;
            }
        }
    }
}

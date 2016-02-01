using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Site.Entity;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Site
{
    public class InnerNetDao
    {
        /*
         * InnerNetService.QueryBySID
         * */
        public static InnerNetEntity QueryBySID(string SID) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId)) {
                SqlQuery q = new SqlQuery("innernet").
                    Select("id", "SID", "IP", "port", "status", "createDate").
                    Where("SID", SID).
                    Where("status",1);
                return dbManager.ExecuteList(q).ConvertAll(r => ToInnerNetEntity(r)).FirstOrDefault();
            }
        }

        private static InnerNetEntity ToInnerNetEntity(object[] r)
        {
            InnerNetEntity u=new InnerNetEntity(){
                Id =Convert.ToInt32( r[0]),
                SID = (string)r[1],
                IP = (string)r[2],
                Port = (string)r[3],
                Status = Convert.ToInt32(r[4]),
                CreateDate = Convert.ToDateTime(r[5])
            };
            return u;
        }
    }
}

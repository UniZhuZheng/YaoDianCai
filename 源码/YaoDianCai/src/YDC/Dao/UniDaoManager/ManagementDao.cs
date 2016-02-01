using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Dao.Manager
{
    public class ManagementDao
    {
        /*
         * ManagementService.Login
         * */
        public static int Login(string account, string password)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("managements").Select("id").Where("account", account).Where("password", password);
                return dbManager.ExecuteScalar<int>(sql);
            }
        }

        public static ManagementsEntity Login(string account)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery sql = new SqlQuery("managements").Select("id", "account", "password", "name", "createDate").Where("account", account);
                return dbManager.ExecuteList(sql).ConvertAll(r => ToManagementsEntity(r)).First();
            }
        }

        private static ManagementsEntity ToManagementsEntity(object[] r)
        {
            ManagementsEntity u = new ManagementsEntity
            {
                Id = Convert.ToInt32(r[0]),
                Account = (string)r[1],
                Password = (string)r[2],
                Name = (string)r[3],
                CreateDate = Convert.ToDateTime(r[4])
            };
            return u;
        }
    }
}

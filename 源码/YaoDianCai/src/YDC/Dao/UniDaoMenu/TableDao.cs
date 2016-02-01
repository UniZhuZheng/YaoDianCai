using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Menu.Entity;
using System.Data;

namespace Uni.YDC.Dao.Menu
{
    public class TableDao
    {
        private readonly string SID;

        public TableDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         * TableService.GetAllTableJson
         * */
        public List<TableEntity> queryAll()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery msql = new SqlQuery("mtable").Select("name", "BID");
                return dbManager.ExecuteList(msql).ConvertAll(m =>
                    {
                        TableEntity table = new TableEntity();
                        table.Name =(string) m[0];
                        table.BID =(string) m[1];
                        return table;
                    });
            }
        }

        public void insert(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mtable").InColumnValue("name", tableName).
                    InColumnValue("BID", SID + "" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                dbManager.ExecuteNonQuery(i);
            }
        }
        /*
         * TableService.RefreshAll
         * */
        public void insertAll(List<TableEntity> list)
        {
            using (DbManager dbManager = new DbManager(SID))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlDelete d = new SqlDelete("mtable");
                dbManager.ExecuteNonQuery(d);

                for (int i = 0; i < list.Count; i++)
                {
                    SqlInsert q = new SqlInsert("mtable").InColumnValue("name", list[i].Name).InColumnValue("BID", list[i].BID);
                    dbManager.ExecuteNonQuery(q);
                }

                tx.Commit();
            }
        }

        public void delete(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete sql = new SqlDelete("mtable").Where("name", tableName);
                dbManager.ExecuteNonQuery(sql);
            }
        }

        public void deleteAll()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete d = new SqlDelete("mtable");
                dbManager.ExecuteNonQuery(d);
            }
        }
      
        public int count()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery sql = new SqlQuery("mtable").SelectCount();
                return dbManager.ExecuteScalar<int>(sql);
            }
        }

        public int count(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mtable").SelectCount().Where("name", tableName);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * TableService.GetBIDByTable
         * */
        public string GetBIDByTable(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mtable").Select("BID").Where("name", tableName);
                return dbManager.ExecuteScalar<string>(q);
            }
        }
    }
}

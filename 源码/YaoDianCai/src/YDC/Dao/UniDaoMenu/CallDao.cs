using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Menu
{
    public class CallDao
    {
        private readonly string SID;
        public CallDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         * CallService.Insert
         * */
        public bool Insert(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mcall")
                    .InColumnValue("content", "")
                    .InColumnValue("type", 1)
                    .InColumnValue("state", 0)
                    .InColumnValue("tableName", tableName)
                    .InColumnValue("createDate",DateTime.Now);
                return dbManager.ExecuteNonQuery(i)>0?true:false;
            }
        }
        /*
         * CallService.GetCallList
         * */
        public List<CallEntity> QueryAll()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery i = new SqlQuery("mcall").Select("content","type","state","tableName","createDate").Where("state",0);
                return dbManager.ExecuteList(i).ConvertAll(o => ToCallEntity(o));
            }
        }
        /*
         * CallService.GetCallList
         * */
        public bool UpdateCallState(string tableName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate i = new SqlUpdate("mcall").Set("state", 1).Where("tableName", tableName);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }

        public int QueryCount()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery i = new SqlQuery("mcall").SelectCount();
                return dbManager.ExecuteScalar<int>(i);
            }
        }

        public List<CallEntity> Query(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery i = new SqlQuery("mcall").
                    Select("content", "type", "state", "tableName", "createDate").
                    SetFirstResult(firstResult).
                    SetMaxResults(maxResult).OrderBy("createDate",false);
                return dbManager.ExecuteList(i).ConvertAll(o => ToCallEntity(o));
            }
        }
        public int QueryCount(DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery i = new SqlQuery("mcall").SelectCount().
                    Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(i);
            }
        }

        public List<CallEntity> Query(DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery i = new SqlQuery("mcall").
                    Select("content", "type", "state", "tableName", "createDate").
                    Where(new BetweenExp("createDate", startDate, endDate)).
                    SetFirstResult(firstResult).
                    SetMaxResults(maxResult).OrderBy("createDate", false);
                return dbManager.ExecuteList(i).ConvertAll(o => ToCallEntity(o));
            }
        }

        private CallEntity ToCallEntity(object[] o)
        {
            CallEntity ce = new CallEntity
            {
                Content = (string)o[0],
                Type = Convert.ToInt32(o[1]),
                State = Convert.ToInt32(o[2]),
                TableName = (string)o[3],
                CreateDate = Convert.ToDateTime(o[4])
            };
            return ce;
        }

        
    }
}

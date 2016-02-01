using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database;
using System.Data;
using Uni.Core.Database.Sql;
using Uni.Core.Common.Utils;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Menu
{
    public class TuanDao
    {
        private readonly string SID;

        public TuanDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         * TuanService.AddTuan
         * */
        public void insert(TuanEntity tuan)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mtuan")
                    .InColumnValue("tableName", tuan.TableName)
                    .InColumnValue("number", tuan.Number)
                    .InColumnValue("website", tuan.Website)
                    .InColumnValue("owner", tuan.Owner)
                    .InColumnValue("phone", tuan.Phone)
                    .InColumnValue("createDate", DateTime.Now)
                    .InColumnValue("state", 0);
                dbManager.ExecuteNonQuery(i);              
            }
        }
        public void insert(List<TuanEntity> lists)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mtuan").InColumns("tableName","number","website","owner","phone","createDate","state");
                foreach(TuanEntity tuan in lists){
                    i.Values(tuan.TableName,tuan.Number,tuan.Website,tuan.Owner,tuan.Phone,DateTime.Now,0);
                }
                dbManager.ExecuteNonQuery(i);
            }
        }
        /*
         * TuanService.GetAllNewTuan
         * */
        public List<TuanEntity> queryAllNew()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mtuan").Select("tablename", "website", "number", "owner", "phone", "state", "createDate").Where("state", 0);
                return dbManager.ExecuteList(q).ConvertAll(o =>
                    {
                        TuanEntity tuan = new TuanEntity 
                        {
                            TableName = (string)o[0],
                            Website = (string)o[1],
                            Number = (string)o[2],
                            Owner = (string)o[3],
                            Phone = (string)o[4],
                            State = Convert.ToInt32(o[5]),
                            CreateDate = Convert.ToDateTime(o[6])
                        };
                        return tuan;
                    });
            }
        }
        /*
         * ClientAndroidService.GetOldTuanInfo
         * */
        public List<TuanEntity> queryAll(string time)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                DateTime times = Convert.ToDateTime(time);

                SqlQuery q = new SqlQuery("mtuan").Select("tablename", "website", "number", "owner", "phone", "state", "createDate")
                    .Where(new BetweenExp("createDate", times, times.AddDays(1))).Where("state", 1).OrderBy("createDate", false);
                return dbManager.ExecuteList(q).ConvertAll(o =>
                    {
                        TuanEntity tuan = new TuanEntity
                        {
                            TableName = (string)o[0],
                            Website = (string)o[1],
                            Number = (string)o[2],
                            Owner = (string)o[3],
                            Phone = (string)o[4],
                            State = Convert.ToInt32(o[5]),
                            CreateDate = Convert.ToDateTime(o[6])
                        };
                        return tuan;
                    });
            }
        }
        /*
         * ClientAndroidService.ToOldTuan
         * */
        public void updateState(string number, int state)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate msql = new SqlUpdate("mtuan").Set("state", state).Where("number", number);
                dbManager.ExecuteNonQuery(msql);
            }
        }

        public List<TuanEntity> queryAllNew(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID)) {
                SqlQuery q = new SqlQuery("mtuan").Select("tablename", "website", "number", "owner", "phone", "state", "createDate").OrderBy("createDate", false)
                    .SetFirstResult(firstResult).SetMaxResults(maxResult);
                return dbManager.ExecuteList(q).ConvertAll(o =>
                {
                    TuanEntity tuan = new TuanEntity
                    {
                        TableName = (string)o[0],
                        Website = (string)o[1],
                        Number = (string)o[2],
                        Owner = (string)o[3],
                        Phone = (string)o[4],
                        State = Convert.ToInt32(o[5]),
                        CreateDate = Convert.ToDateTime(o[6])
                    };
                    return tuan;
                });
            }
        }

        public List<TuanEntity> queryAllNew(DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mtuan").Select("tablename", "website", "number", "owner", "phone", "state", "createDate").OrderBy("createDate", false)
                    .Where(new BetweenExp("createDate", startDate, endDate))
                    .SetFirstResult(firstResult).SetMaxResults(maxResult);
                return dbManager.ExecuteList(q).ConvertAll(o =>
                {
                    TuanEntity tuan = new TuanEntity
                    {
                        TableName = (string)o[0],
                        Website = (string)o[1],
                        Number = (string)o[2],
                        Owner = (string)o[3],
                        Phone = (string)o[4],
                        State = Convert.ToInt32(o[5]),
                        CreateDate = Convert.ToDateTime(o[6])
                    };
                    return tuan;
                });
            }
        }

        public int queryCount()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mtuan").SelectCount();
                return dbManager.ExecuteScalar<int>(q);
            }
        }

        public int queryCount(DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mtuan").SelectCount().Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
    }
}

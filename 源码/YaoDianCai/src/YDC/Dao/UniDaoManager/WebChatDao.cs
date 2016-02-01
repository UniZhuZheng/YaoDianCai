using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Dao.Manager
{
    public class WebChatDao
    {
        /*
         * WebChatService.GetWebChatList
         * */
        public static List<object[]> GetWebChatList(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.name", "s.SID", "h.name").SelectCount("wc.SID").
                    LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                    LeftOuterJoin("webchat wc", Exp.EqColumns("wc.SID", "s.SID")).
                    OrderBy("s.id", false).GroupBy("s.id", "s.name", "s.SID", "h.name").
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shops").Select("id").OrderBy("id", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("s.id", qChild));
                }
                return dbManager.ExecuteList(q);
            }
        }
        /*
         * WebChatService.GetWebChatList
         * */
        public static List<object[]> GetWebChatList(string search, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.name", "s.SID", "h.name").SelectCount("wc.SID").
                    LeftOuterJoin("hosts h", Exp.EqColumns("h.hostId", "s.hostId")).
                    LeftOuterJoin("webchat wc", Exp.EqColumns("wc.SID", "s.SID")).
                    OrderBy("s.id", false).GroupBy("s.id", "s.name", "s.SID", "h.name").
                    Where(Exp.Like("s.name", search, SqlLike.AnyWhere)).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shops").Select("id").OrderBy("id", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("s.id", qChild));
                }
                return dbManager.ExecuteList(q);
            }
        }
        /*
         * WebChatService.GetWebChatListCount
         * */
        public static int Count(string sid)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId)) {
                SqlQuery q = new SqlQuery("webchat").SelectCount("id").Where("SID", sid);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * WebChatService.GetWebChatListCount
         * */
        public static int Count(string SID, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("webchat").SelectCount("id").Where("SID", SID).
                    Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * WebChatService.GetWebChatList
         * */
        public static List<WebChatEntity> Query(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("webchat").Select("id", "openId", "SID", "state", "createDate").Where("SID", SID);
                return dbManager.ExecuteList(q).ConvertAll(r => ToWebChatEntity(r));
            }
        }
        /*
         * WebChatService.GetWebChatList
         * */
        public static List<WebChatEntity> Query(string SID, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("webchat").Select("id", "openId", "SID", "state", "createDate").
                    Where("SID", SID).Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteList(q).ConvertAll(r => ToWebChatEntity(r));
            }
        }
        private static WebChatEntity ToWebChatEntity(object[] r)
        {
            WebChatEntity u = new WebChatEntity
            {
                Id = Convert.ToInt32(r[0]),
                OpenId = (string)r[1],
                SID = (string)r[2],
                State = Convert.ToInt32(r[3]),
                CreateDate = Convert.ToDateTime(r[4])
            };
            return u;
        }
        
    }
}

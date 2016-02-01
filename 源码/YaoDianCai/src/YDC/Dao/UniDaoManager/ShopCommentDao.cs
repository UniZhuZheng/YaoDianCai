using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Manager.Entity;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Manager
{
    public class ShopCommentDao
    {
        public static List<ShopCommentEntity> Query(string SID,int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shopcomment").
                    Select("id", "SID", "comment", "createDate").
                    Where("SID", SID).
                    OrderBy("createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shopcomment").Select("id").Where("SID", SID).
                        OrderBy("createDate", false).
                        SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopCommentEntity(r));
            }
        }

        public static List<ShopCommentEntity> Query(string SID, int firstResult, int maxResult, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shopcomment").
                    Select("id", "SID", "comment", "createDate").
                    Where("SID", SID).
                    Where(new BetweenExp("createDate", startDate, endDate)).
                    OrderBy("createDate", false).
                    SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shopcomment").Select("id").Where("SID", SID).
                        Where(new BetweenExp("createDate", startDate, endDate)).
                        OrderBy("createDate", false).
                        SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopCommentEntity(r));
            }
        }

        public static int Count(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shopcomment").
                    SelectCount("id").
                    Where("SID",SID);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        public static int Count(string SID, DateTime startDate, DateTime endDate)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shopcomment").
                    SelectCount("id").
                    Where("SID", SID).
                    Where(new BetweenExp("createDate", startDate, endDate));
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * ShopCommentService.ShopCommentAdd
         * */
        public static bool Insert(string SID,string comment) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlInsert i = new SqlInsert("shopcomment")
                    .InColumnValue("SID", SID)
                    .InColumnValue("comment", comment)
                    .InColumnValue("createDate", DateTime.Now);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }

        private static ShopCommentEntity ToShopCommentEntity(object[] r)
        {
            ShopCommentEntity u = new ShopCommentEntity
            {
                Id = Convert.ToInt32(r[0]),
                SID = (string)r[1],
                Comment = (string)r[2],
                CreateDate = Convert.ToDateTime(r[3])
            };
            return u;
        }
    }
}

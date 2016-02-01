using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager.Entity;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.Core.Database.Sql.Expressions;

namespace Uni.YDC.Dao.Manager
{
    public class ShopLabelDao
    {
        /*
         * ShopLabelService.ShopLabelList
         * */
        public static List<ShopLabelEntity> Query(string SID) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shoplabel").
                    Select("id", "SID", "name", "type", "status", "createDate").
                    Where("SID", SID);
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopLabelEntity(r));
            }
        }

        /*
         * ShopLabelService.ShopLabelExit
         * */
        public static bool IsExit(string SID, string name)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shoplabel").
                    Select("id").
                    Where("SID", SID).Where("name", name);
                return dbManager.ExecuteScalar<int>(q)>0?true:false;
            }
        }

        /*
         * ShopLabelService.ShopLabelAdd
         * */
        public static bool Insert(ShopLabelEntity sl) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlInsert i = new SqlInsert("shoplabel")
                    .InColumnValue("SID", sl.SID)
                    .InColumnValue("name", sl.Name)
                    .InColumnValue("type", sl.Type)
                    .InColumnValue("status", sl.Status)
                    .InColumnValue("createDate", sl.CreateDate);
                return dbManager.ExecuteNonQuery(i)>0?true:false;
            }
        }
        /*
         * ShopLabelService.ShopLabelDelete
         * */
        public static bool Delete(int id) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlDelete i = new SqlDelete("shoplabel").Where("id", id);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }
        /*
         * ShopLabelService.ShopLabelUpdate
         * */
        public static bool Update(int id,string name) {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlUpdate i = new SqlUpdate("shoplabel").Set("name",name).Where("id", id);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }
        /*
         * ShopLabelService.GetShopTabList
         * */
        public static List<object[]> GetShopTabList_1(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.name", "s.SID").SelectCount("sl.SID").
                    LeftOuterJoin("shoplabel sl", Exp.EqColumns("sl.SID", "s.SID")).
                    OrderBy("s.id", false).
                    GroupBy("s.id", "s.name", "s.SID").
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
         * ShopLabelService.GetShopTabList
         * */
        public static List<object[]> GetShopTabList_1(string search, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.name", "s.SID").SelectCount("sl.SID").
                    LeftOuterJoin("shoplabel sl", Exp.EqColumns("sl.SID", "s.SID")).
                    Where(Exp.Like("s.name", search, SqlLike.AnyWhere)).
                    OrderBy("s.id", false).
                    GroupBy("s.id", "s.name", "s.SID").
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
         * ShopLabelService.GetShopTabList
         * */
        public static List<object[]> GetShopTabList_2(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.name", "s.SID").SelectCount("sc.SID").
                    LeftOuterJoin("shopcomment sc", Exp.EqColumns("sc.SID", "s.SID")).
                    OrderBy("s.id", false).
                    GroupBy("s.id", "s.name", "s.SID").
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
         * ShopLabelService.GetShopTabList
         * */
        public static List<object[]> GetShopTabList_2(string search, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shops s").
                    Select("s.name", "s.SID").SelectCount("sc.SID").
                    LeftOuterJoin("shopcomment sc", Exp.EqColumns("sc.SID", "s.SID")).
                    Where(Exp.Like("s.name", search, SqlLike.AnyWhere)).
                    OrderBy("s.id", false).
                    GroupBy("s.id", "s.name", "s.SID").
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
         * ShopLabelService.ShopLabelCountList
         * */
        public static List<object[]> ShopLabelCountList(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shoplabelcount slc").
                    Select("slc.id", "slc.SID", "sl.name", "slc.count", "slc.lastDate").
                    LeftOuterJoin("shoplabel sl", Exp.EqColumns("sl.id", "slc.labelId")).
                    Where("slc.SID", SID);
                
                return dbManager.ExecuteList(q);
            }
        }
        /*
         * ShopLabelService.ShopLabelComment_Count
         * */

        public static int ShopLabelComment_Count(string SID)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shopcomment").
                    SelectCount("id").
                    Where("SID", SID);
                return dbManager.ExecuteScalar<int>(q);
            }
        }
        /*
         * ShopLabelService.ShopLabelComment_List
         * */
        public static List<ShopCommentEntity> ShopLabelComment_List(string SID, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery q = new SqlQuery("shopcomment").
                        Select("id", "SID","comment","createDate").
                        Where("SID",SID).
                        OrderBy("createDate", false).
                        SetMaxResults(maxResult);
                if (firstResult > 0)
                {
                    SqlQuery qChild = new SqlQuery("shopcomment").Select("id").OrderBy("createDate", false).SetMaxResults(firstResult);
                    q.Where(!new InExp("id", qChild));
                }
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopCommentEntity(r));
            }
        }
        /*
         * ShopLabelService.LabelsUpdateCount
         * */
        public static bool LabelsUpdateCount(string SID, string name, int count)
        {
            using (DbManager dbManager = new DbManager(DBManager.DatabaseId))
            {
                SqlQuery ql = new SqlQuery("shoplabel").
                    Select("id").
                    Where("SID", SID).
                    Where("name", name);
                int labelId= dbManager.ExecuteScalar<int>(ql);


                SqlQuery s = new SqlQuery("shoplabelcount")
                    .SelectCount("id")
                    .Where("SID", SID)
                    .Where("labelId", labelId);
                int co = dbManager.ExecuteScalar<int>(s);
                if (co <= 0)
                {
                    SqlInsert i = new SqlInsert("shoplabelcount").
                        InColumnValue("SID", SID).
                        InColumnValue("labelId", labelId).
                        InColumnValue("count", 1).
                        InColumnValue("lastDate", DateTime.Now);
                    return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
                }
                else
                {
                    SqlUpdate u = new SqlUpdate("shoplabelcount").
                        Set("count", count).
                        Set("lastDate", DateTime.Now).
                        Where("SID", SID).
                        Where("labelId", labelId);
                    return dbManager.ExecuteNonQuery(u) > 0 ? true : false;
                }
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
        
        private static ShopLabelEntity ToShopLabelEntity(object[] r)
        {
            ShopLabelEntity u = new ShopLabelEntity
            {
                Id = Convert.ToInt32(r[0]),
                SID = (string)r[1],
                Name = (string)r[2],
                Type = Convert.ToInt32(r[3]),
                Status = Convert.ToInt32(r[4]),
                CreateDate = Convert.ToDateTime(r[5])
            };
            return u;
        }

        
    }
}

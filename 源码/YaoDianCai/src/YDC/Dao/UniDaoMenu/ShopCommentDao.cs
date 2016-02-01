using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Menu.Entity;

namespace Uni.YDC.Dao.Menu
{
    public class ShopCommentDao
    {
        private readonly string SID;
        public ShopCommentDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         * ShopCommentService.ShopCommentAdd
         * */
        public bool Insert(string comment) {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mshopcomment")
                    .InColumnValue("SID", SID)
                    .InColumnValue("comment", comment)
                    .InColumnValue("createDate", DateTime.Now);
                return dbManager.ExecuteNonQuery(i) > 0 ? true : false;
            }
        }
        public int ShopLabelComment_Count()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mshopcomment").
                    SelectCount();
                return dbManager.ExecuteScalar<int>(q);
            }
            
        }

        public List<ShopCommentEntity> ShopLabelComment_List(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mshopcomment").
                        Select( "SID", "comment", "createDate").
                        SetFirstResult(firstResult).
                        SetMaxResults(maxResult).
                        OrderBy("createDate", false);
                return dbManager.ExecuteList(q).ConvertAll(r => ToShopCommentEntity(r));
            }
        }
        private static ShopCommentEntity ToShopCommentEntity(object[] r)
        {
            ShopCommentEntity u = new ShopCommentEntity
            {
                SID = (string)r[0],
                Comment = (string)r[1],
                CreateDate = Convert.ToDateTime(r[2])
            };
            return u;
        }
    }
}

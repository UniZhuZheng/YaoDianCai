using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Menu
{
    public class VideoDao
    {
        private readonly string SID;

        public VideoDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }
        /*
         * VideoService.Insert
         * */
        public bool Insert(VideoEntity u){
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mvideo")
                    .InColumnValue("number", u.Number)
                    .InColumnValue("name", u.Name)
                    .InColumnValue("content", u.Content)
                    .InColumnValue("tab", u.Tab)
                    .InColumnValue("count", u.Count)
                    .InColumnValue("sort", u.Sort)
                    .InColumnValue("fileName", u.FileName)
                    .InColumnValue("createDate", DateTime.Now);
                return dbManager.ExecuteNonQuery(i)>0?true:false;
            }
        }
        public bool Update(string name, string fileName)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u1 = new SqlUpdate("mvideo").
                    Set("fileName", fileName).
                    Where("name", name);
                return dbManager.ExecuteNonQuery(u1) > 0 ? true : false;
            }
        }
        public bool Update(string name, int count)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u1 = new SqlUpdate("mvideo").
                    Set("count", count).
                    Where("name", name);
                return dbManager.ExecuteNonQuery(u1) > 0 ? true : false;
            }
        }
        public bool Update(string name,string number,string content,string tab) {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u1 = new SqlUpdate("mvideo").
                    Set("number", number).
                    Set("content", content).
                    Set("tab", tab).
                    Where("name", name);
                return dbManager.ExecuteNonQuery(u1) > 0 ? true : false;
            }
        }

        public bool Update(string name1,int sort1,string name2,int sort2) {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u1 = new SqlUpdate("mvideo").Set("sort", sort1).Where("name",name1);
                bool b1 = dbManager.ExecuteNonQuery(u1) > 0 ? true : false;
                SqlUpdate u2 = new SqlUpdate("mvideo").Set("sort", sort2).Where("name", name2);
                bool b2 = dbManager.ExecuteNonQuery(u2) > 0 ? true : false;

                return b1 && b2;
            }
        }
        public bool Delete(string name)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete q = new SqlDelete("mvideo").Where("name", name);
                return dbManager.ExecuteNonQuery(q) > 0 ? true : false;
            }
        }
        public int Count() {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mvideo").SelectMax("sort");
                return dbManager.ExecuteScalar<int>(q);
            }
        }

        public VideoEntity QueryByName(string name) {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mvideo").
                    Select("number", "name","tab", "content","fileName", "count", "sort", "createDate").
                    Where("name",name).
                    OrderBy("sort", false);
                return dbManager.ExecuteList(q).ConvertAll(r => ToVideoEntity(r)).FirstOrDefault();
            }
        }

        public List<VideoEntity> Query() {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mvideo").
                    Select("number", "name", "tab", "content", "fileName", "count", "sort", "createDate").
                    OrderBy("sort",false);
                return dbManager.ExecuteList(q).ConvertAll(r => ToVideoEntity(r));
            }
        }

        private VideoEntity ToVideoEntity(object[] r)
        {
            VideoEntity u = new VideoEntity
            {
                Number = (string)r[0],
                Name = (string)r[1],
                Tab=(string)r[2],
                Content = (string)r[3],
                FileName = (string)r[4],
                Count = Convert.ToInt32(r[5]),
                Sort = Convert.ToInt32(r[6]),
                CreateDate = Convert.ToDateTime(r[7])
            };
            return u;
        }
    }
}

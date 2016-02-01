using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Uni.Core.Database;
using Uni.Core.Database.Sql;
using Uni.YDC.Dao.Menu.Entity;
using Uni.Core.Common.Utils;
using Uni.Core.Database.Sql.Expressions;


namespace Uni.YDC.Dao.Menu
{
    public class DishDao
    {
        private readonly string SID;

        public DishDao(string sid)
        {
            this.SID = sid;
            if (!ShopDBFactory.IsRegistered(SID))
            {
                ShopDBFactory.Register(SID);
            }
        }


        public void insert(DishEntity dish)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlInsert i = new SqlInsert("mdish")
                    .InColumnValue("number", dish.Number)
                    .InColumnValue("name", dish.Name)
                    .InColumnValue("price", dish.Price)
                    .InColumnValue("type", dish.Type)
                    .InColumnValue("property", dish.Property)
                    .InColumnValue("content", dish.Content)
                    .InColumnValue("state", dish.State)
                    .InColumnValue("createDate", DateTime.Now);
                dbManager.ExecuteNonQuery(i);
            }
        }

        public void insertAll(List<DishEntity> list)
        {
            using (DbManager dbManager = new DbManager(SID))
            using (IDbTransaction tx = dbManager.BeginTransaction())
            {
                SqlDelete d = new SqlDelete("mdish");
                dbManager.ExecuteNonQuery(d);

                for (int i = 0; i < list.Count; i++)
                {
                    SqlInsert q = new SqlInsert("mdish")
                        .InColumnValue("number", list[i].Number)
                        .InColumnValue("name", list[i].Name)
                        .InColumnValue("price", list[i].Price)
                        .InColumnValue("type", list[i].Type)
                        .InColumnValue("property", list[i].Property)
                        .InColumnValue("content", list[i].Content)
                        .InColumnValue("state", list[i].State)
                        .InColumnValue("createDate", DateTime.Now);
                    dbManager.ExecuteNonQuery(q);
                }

                tx.Commit();
            }
        }

        public DishEntity query(string name)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mdish").Select("number", "name", "price", "type", "property", "content", "state", "createDate").Where("name", name);
                return dbManager.ExecuteList(q).ConvertAll(m =>
                {
                    DishEntity dish = new DishEntity()
                    {
                        Number =(string) m[0],
                        Name = (string)m[1],
                        Price = m[2].ToString(),
                        Type = (string)m[3],
                        Property = (string)m[4],
                        Content = (string)m[5],
                        State = Convert.ToInt32(m[6]),
                        CreateDate = Convert.ToDateTime(m[7])
                    };
                    return dish;
                }).FirstOrDefault();
            }
        }

        public DishEntity queryInfo(string name)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mdish").Select("number", "name", "price", "type", "property", "content", "state", "createDate").Where("name", name);
                DishEntity d =  dbManager.ExecuteList(q).ConvertAll(m =>
                {
                    DishEntity dish = new DishEntity()
                    {
                        Number = (string)m[0],
                        Name = (string)m[1],
                        Price = m[2].ToString(),
                        Type = (string)m[3],
                        Property = (string)m[4],
                        Content = (string)m[5],
                        State = Convert.ToInt32(m[6]),
                        CreateDate = Convert.ToDateTime(m[7])
                    };
                    return dish;
                }).FirstOrDefault();

                if (ImageCode.ImageToByte(SID, name) == null)
                {
                    d.Imgcode = "";
                }
                else {
                    d.Imgcode = Convert.ToBase64String(ImageCode.ImageToByte(SID, name));
                }
                
                return d;
            }
        }
        /*
         * DishService.GetAllDishesJson
         * */
        public List<DishEntity> queryAll()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mdish").Select("number", "name", "price", "type", "property", "content", "state", "createDate");
                return dbManager.ExecuteList(q).ConvertAll(m =>
                {
                    DishEntity dish = new DishEntity();
                    dish.Number = (string)m[0];
                    dish.Name = (string)m[1];
                    dish.Price = m[2].ToString();
                    dish.Type = (string)m[3];
                    dish.Property = (string)m[4];
                    dish.Content = (string)m[5];
                    switch (m[6].ToString())
                    {
                        case "0":
                            dish.StateImg = "/res/images/menu-option-xsz.png";
                            break;
                        case "1":
                            dish.StateImg = "/res/images/menu-option-wxs.png";
                            break;
                        case "2":
                            dish.StateImg = "/res/images/menu-option-wsj.png";
                            break;
                        default:
                            dish.StateImg = "/res/images/menu-option-xsz.png";
                            break;
                    }
                    dish.State = Convert.ToInt32(m[6]);
                    dish.CreateDate = Convert.ToDateTime(m[7]);
                    dish.Imgcode = "/shop/" + SID + "/Html/ShopInfo/menuimg/" + (string)m[1] + ".jpg";
                    return dish;
                });
            }
        }
        /*
         * DishService.Select
         * */
        public List<DishEntity> queryAll(int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mdish").Select("number", "name", "price", "type", "property", "content", "state", "createDate")
                    .SetFirstResult(firstResult).SetMaxResults(maxResult);
                return dbManager.ExecuteList(q).ConvertAll(m =>
                    {
                        DishEntity dish = new DishEntity();
                        dish.Number = (string)m[0];
                        dish.Name = (string)m[1];
                        dish.Price = m[2].ToString();
                        dish.Type = (string)m[3];
                        dish.Property = (string)m[4];
                        dish.Content = (string)m[5];
                        switch (m[6].ToString())
                        {
                            case "0":
                                dish.StateImg = "/res/images/menu-option-xsz.png";
                                break;
                            case "1":
                                dish.StateImg = "/res/images/menu-option-wxs.png";
                                break;
                            case "2":
                                dish.StateImg = "/res/images/menu-option-wsj.png";
                                break;
                            default:
                                dish.StateImg = "/res/images/menu-option-xsz.png";
                                break;
                        }
                        dish.State = Convert.ToInt32(m[6]);
                        dish.CreateDate = Convert.ToDateTime(m[7]);
                        dish.Imgcode = "/shop/" + SID + "/Html/ShopInfo/menuimg/" + (string)m[1] + ".jpg";
                        return dish;
                    });
            }
        }
        /*
         * DishService.Select
         * */
        public List<DishEntity> queryAll(string searchKey,int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mdish").Select("number", "name", "price", "type", "property", "content", "state", "createDate")
                    .SetFirstResult(firstResult).SetMaxResults(maxResult).Where(Exp.Like("name", searchKey, SqlLike.AnyWhere));
                return dbManager.ExecuteList(q).ConvertAll(m =>
                {
                    DishEntity dish = new DishEntity();
                    dish.Number = (string)m[0];
                    dish.Name = (string)m[1];
                    dish.Price = m[2].ToString();
                    dish.Type = (string)m[3];
                    dish.Property = (string)m[4];
                    dish.Content = (string)m[5];
                    switch (m[6].ToString())
                    {
                        case "0":
                            dish.StateImg = "/res/images/menu-option-xsz.png";
                            break;
                        case "1":
                            dish.StateImg = "/res/images/menu-option-wxs.png";
                            break;
                        case "2":
                            dish.StateImg = "/res/images/menu-option-wsj.png";
                            break;
                        default:
                            dish.StateImg = "/res/images/menu-option-xsz.png";
                            break;
                    }
                    dish.State = Convert.ToInt32(m[6]);
                    dish.CreateDate = Convert.ToDateTime(m[7]);
                    dish.Imgcode = "/shop/" + SID + "/Html/ShopInfo/menuimg/" + (string)m[1] + ".jpg";
                    return dish;
                });
            }
        }
        public void update(DishEntity menu)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u = new SqlUpdate("mdish")
                    .Set("number", menu.Number)
                    .Set("price", menu.Price)
                    .Set("type", menu.Type)
                    .Set("property", menu.Property)
                    .Set("content", menu.Content)
                    .Where("name", menu.Name);
                dbManager.ExecuteNonQuery(u);
            }
        }

        public void update(string name, int state)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate sql = new SqlUpdate("mdish").Set("state", state).Where("name", name);
                dbManager.ExecuteNonQuery(sql);
            }
        }

        public void update(string name, string price, int state)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlUpdate u = new SqlUpdate("mdish").Set("price", price).Set("state", state).Where("name", name);
                dbManager.ExecuteNonQuery(u);
            }
        }
        /*
         * DishService.GetDishCount
         * */
        public int count()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery sql = new SqlQuery("mdish").SelectCount();
                return dbManager.ExecuteScalar<int>(sql);
            }
        }
        /*
         * DishService.GetDishCount
         * */
        public int countSearch(string searchKey)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery sql = new SqlQuery("mdish").SelectCount().Where(Exp.Like("name", searchKey, SqlLike.AnyWhere));
                return dbManager.ExecuteScalar<int>(sql);
            }
        }
        public int count(string name)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery sql = new SqlQuery("mdish").SelectCount().Where("name", name);
                return dbManager.ExecuteScalar<int>(sql);
            }
        }

        public void delete()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete sql = new SqlDelete("mdish");
                dbManager.ExecuteNonQuery(sql);
            }
        }

        public void delete(string name)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlDelete sql = new SqlDelete("mdish").Where("name", name);
                dbManager.ExecuteNonQuery(sql);
            }
        }
        /*
         * DishService.GetAllTypeJson
         * */
        public List<string> queryAllType()
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery psql = new SqlQuery("mdish").Select("type").Where(!new EqExp("state", 2)).Distinct();
                return dbManager.ExecuteList(psql).ConvertAll(p =>
                    {
                        return p[0].ToString();
                    });
            }
        }

        public int count(int state)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery sql = new SqlQuery("mdish").SelectCount().Where(!new EqExp("state",state));
                return dbManager.ExecuteScalar<int>(sql);
            }
        }

        public List<DishEntity> queryAll(int state, int firstResult, int maxResult)
        {
            using (DbManager dbManager = new DbManager(SID))
            {
                SqlQuery q = new SqlQuery("mdish").Select("number", "name", "price", "type", "property", "content", "state", "createDate").Where(!new EqExp("state", state))
                    .SetFirstResult(firstResult).SetMaxResults(maxResult);
                return dbManager.ExecuteList(q).ConvertAll(m =>
                {
                    DishEntity dish = new DishEntity();
                    dish.Number = (string)m[0];
                    dish.Name = (string)m[1];
                    dish.Price = m[2].ToString();
                    dish.Type = (string)m[3];
                    dish.Property = (string)m[4];
                    dish.Content = (string)m[5];
                    switch (m[6].ToString())
                    {
                        case "0":
                            dish.StateImg = "/res/images/menu-option-xsz.png";
                            break;
                        case "1":
                            dish.StateImg = "/res/images/menu-option-wxs.png";
                            break;
                        case "2":
                            dish.StateImg = "/res/images/menu-option-wsj.png";
                            break;
                        default:
                            dish.StateImg = "/res/images/menu-option-xsz.png";
                            break;
                    }
                    dish.State = Convert.ToInt32(m[6]);
                    dish.CreateDate = Convert.ToDateTime(m[7]);
                    dish.Imgcode = "/shop/" + SID + "/Html/ShopInfo/menuimg/" + (string)m[1] + ".jpg";
                    return dish;
                });
            }
        }
    }
}

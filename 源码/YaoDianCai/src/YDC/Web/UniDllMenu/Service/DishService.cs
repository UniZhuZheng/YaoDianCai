using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;
using System.Web.Script.Serialization;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class DishService
    {
        public static bool RefreshAll(string sid, List<DishEntity> list)
        {
            if (list == null)
            {
                return false;
            }

            try
            {
                int dishOnSellCount=0;
                int dishOffSellCount=0;
                int dishDisableCount=0;
                int dishCount=list.Count;
                foreach(DishEntity de in list){
                    if(de.State==1){
                        dishOffSellCount++;
                    }
                    else if (de.State == 2)
                    {
                        dishDisableCount++;
                    }
                    else {
                        dishOnSellCount++;
                    }
                }

                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[5];
                args[0] = sid;
                args[1] = Convert.ToString(dishOnSellCount);
                args[2] = Convert.ToString(dishOffSellCount);
                args[3] = Convert.ToString(dishDisableCount);
                args[4] = Convert.ToString(dishCount);

                WSHelper.InvokeWebService(url, "UpdateDishInfo", args);
            }
            catch (Exception)
            {
                return false;
            }
            new DishDao(sid).insertAll(list);
            return true;
        }
        /*
         * DishAction.GetAllDishes
         * */
        public static string GetAllDishesJson(string SID)
        {
            List<DishEntity> list = new DishDao(SID).queryAll();
            var q = from m in list
                    where m.State == 0
                    select new
                    {
                        number = m.Number,             
                        name = m.Name,
                        kind = m.Property,
                        price = m.Price,
                        type = m.Type,
                        content = m.Content,
                        state = m.State,
                        createDate = m.CreateDate.ToString("yyyy-MM-dd"),
                        count = 0
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        public static List<Uni.YDC.Dao.Menu.Entity.DishEntity> GetMenuList(string SID)
        {
            try
            {
                return new DishDao(SID).queryAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /*
         * ShopDishList.Page_Load
         * DishAction.CountLimitDishes
         * */
        public static int GetDishCount(string sid)
        {
            return new DishDao(sid).count();
        }
        /*
         * DishAction.CountLimitDishesSearch
         * */
        public static int GetDishCount(string sid, string searchKey)
        {
            return new DishDao(sid).countSearch(searchKey);
        }
        public static bool AddNewDish(string sid, DishEntity dish)
        {
            if (new DishDao(sid).count(dish.Name) > 0)
            {
                return false;
            }

            new DishDao(sid).insert(dish);
            return true;
        }

        public static void Delete(string sid, string name)
        {
            new DishDao(sid).delete(name);
        }

        public static void Delete(string sid)
        {
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[5];
                args[0] = sid;
                args[1] = "0";
                args[2] = "0";
                args[3] = "0";
                args[4] = "0";

                WSHelper.InvokeWebService(url, "UpdateDishInfo", args);
            }
            catch (Exception)
            {
            }
            new DishDao(sid).delete();
        }
        /*
         * DishAction.ListLimitDishes
         * */
        public static string Select(string sid, int firstResult, int maxResult)
        {
            List<DishEntity> list = new DishDao(sid).queryAll(firstResult, maxResult);

            var q = from m in list
                    select new
                        {
                            number = m.Number,
                            name = m.Name,
                            price = m.Price,
                            type = m.Type,
                            property = m.Property,
                            state = m.State,
                            stateImg = m.StateImg,
                            imgCode = m.Imgcode,
                            menucontent = m.Content,
                            createDate = m.CreateDate.ToString("yyyy-MM-dd")
                        };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        /*
         * DishAction.ListLimitDishesSearch
         * */
        public static string Select(string sid, int firstResult, int maxResult, string searchKey)
        {
            List<DishEntity> list = new DishDao(sid).queryAll(searchKey,firstResult, maxResult);

            var q = from m in list
                    select new
                    {
                        number = m.Number,
                        name = m.Name,
                        price = m.Price,
                        type = m.Type,
                        property = m.Property,
                        state = m.State,
                        stateImg = m.StateImg,
                        imgCode = m.Imgcode,
                        menucontent = m.Content,
                        createDate = m.CreateDate.ToString("yyyy-MM-dd")
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }
        public static void StateChange(string sid, string name, int state)
        {
            new DishDao(sid).update(name, state);
        }

        public static string GetDishJson(string sid, string name)
        {
            DishEntity dish = new DishDao(sid).query(name);

            var q = new
                    {
                        number = dish.Number,
                        name = dish.Name,
                        price = dish.Price,
                        type = dish.Type,
                        property = dish.Property,
                        state = dish.State,
                        stateImg = dish.StateImg,
                        imgCode = dish.Imgcode,
                        menucontent = dish.Content,
                        createDate = dish.CreateDate.ToString("yyyy-MM-dd")
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        public static void Update(string SID, Dao.Menu.Entity.DishEntity menu)
        {
            new DishDao(SID).update(menu);
        }
        /*
         * DishAction.ListDishType
         * */

        public static string GetAllTypeJson(string sid)
        {
            List<string> list = new DishDao(sid).queryAllType();
            var q = from p in list
                    select new
                        {
                            type = p
                        };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }


        public static string Select(string sid, int state, int firstResult, int maxResult)
        {
            List<DishEntity> list = new DishDao(sid).queryAll(state,firstResult, maxResult);
            var q = from m in list
                    select new
                    {
                        number = m.Number,
                        name = m.Name,
                        price = m.Price,
                        type = m.Type,
                        property = m.Property,
                        state = m.State,
                        stateImg = m.StateImg,
                        imgCode = m.Imgcode,
                        menucontent = m.Content,
                        createDate = m.CreateDate.ToString("yyyy-MM-dd")
                    };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        public static int GetDishCount(string sid, int state)
        {
            return new DishDao(sid).count(state);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class BillService
    {
        /*
         * BillAction.CreateBill
         * 
         * */
        public static string AddBill(string sid, string json, int cartStatus)
        {
            if ("".Equals(json)||json==null)
            {
                return "";
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BillEntity bill = serializer.Deserialize<BillEntity>(json);
            if (bill == null)
            {
                return "";
            }
            //判断菜单桌号对应的下单号是否正确
            string BID = TableService.GetBIDByTable(sid, bill.TableName);
            if (!bill.BID.Equals(BID))
            {
                return "false-"+BID;   
            }
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[5];
                args[0] = sid;
                args[1] = bill.BID;
                args[2] = bill.TableName;
                args[3] = bill.Memo;
                List<DishRecordEntity> list = new List<DishRecordEntity>();
                for (int i = 0; i < bill.Orders.Count; i++)
                {
                    DishRecordEntity dre = new DishRecordEntity();
                    dre.Number = bill.Orders[i].DishNumber;
                    dre.Name = bill.Orders[i].DishName;
                    dre.Price = Convert.ToSingle(bill.Orders[i].DishPrice);
                    dre.Count = bill.Orders[i].DishCount;
                    list.Add(dre);
                }
                var p =
                from c in list
                select new
                {
                    number = c.Number,
                    name = c.Name,
                    price = c.Price,
                    count = c.Count
                };
                JavaScriptSerializer jss = new JavaScriptSerializer();
                args[4] = jss.Serialize(p);

                WSHelper.InvokeWebService(url, "AddBillRecord", args);

            }
            catch (Exception)
            {
                return "";
            }

            BID=new BillDao(sid).insert(bill, cartStatus);
            return "true-"+BID;
        }
        /*
         * ClientAndroidService.CreateBillForShopOrder
         * */
        public static bool AddBillPhone(string sid, string json, int cartStatus)
        {
            if ("".Equals(json) || json == null)
            {
                return false;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BillEntity bill = serializer.Deserialize<BillEntity>(json);
            if (bill == null)
            {
                return false;
            }
            
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[5];
                args[0] = sid;
                args[1] = bill.BID;
                args[2] = bill.TableName;
                args[3] = bill.Memo;
                List<DishRecordEntity> list = new List<DishRecordEntity>();
                for (int i = 0; i < bill.Orders.Count; i++)
                {
                    DishRecordEntity dre = new DishRecordEntity();
                    dre.Number = bill.Orders[i].DishNumber;
                    dre.Name = bill.Orders[i].DishName;
                    dre.Price = Convert.ToSingle(bill.Orders[i].DishPrice);
                    dre.Count = bill.Orders[i].DishCount;
                    list.Add(dre);
                }
                var p =
                from c in list
                select new
                {
                    number = c.Number,
                    name = c.Name,
                    price = c.Price,
                    count = c.Count
                };
                JavaScriptSerializer jss = new JavaScriptSerializer();
                args[4] = jss.Serialize(p);

                WSHelper.InvokeWebService(url, "AddBillRecord", args);

            }
            catch (Exception)
            {
                return false;
            }

            new BillDao(sid).insertPhone(bill, cartStatus);
            return true;
        }
        /*
         * ClientAndroidService.RefreshBill
         * */
        public static List<BillEntity> GetAllNewBill(string sid)
        {
            return new BillDao(sid).queryAllNew();
        }
        /*
         * ClientAndroidService.GetOldBillInfo
         * */
        public static List<BillEntity> GetAllBillsByTime(string sid, string time)
        {
            return new BillDao(sid).queryAll(time);
        }
        /*
         * ClientAndroidService.ToOldBill
         * */
        public static void SetDishState(string sid, string bid, int state)
        {
            new BillDao(sid).updateState(bid, state);
        }

        /*
         * BillAction.BillInfoByShopLimit
         * */

        public static List<BillEntity> GetBillList(string sid, int firstResult, int maxResult)
        {
            return new BillDao(sid).QueryAll(firstResult, maxResult);
        }
        /*
         * BillAction.BillInfoByDayLimit
         * BillAction.BillInfoByMonthLimit
         * */
        public static List<BillEntity> GetBillListByDate(string sid, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            return new BillDao(sid).QueryAll(startDate, endDate, firstResult, maxResult);
        }
        /*
         * BillAction.BillInfoByShopLimit_Count
         * */
        public static int GetBillListCount(string sid)
        {
            return new BillDao(sid).Count();
        }
        /*
         *BillAction. BillInfoByDayLimit_Count
         *BillAction.BillInfoByMonthLimit_Count
         * */
        public static int GetBillListCountByDate(string sid, DateTime startDate, DateTime endDate)
        {
            return new BillDao(sid).Count(startDate, endDate);
        }

        public static string GetCurrentMonthOrder(string sid, DateTime startDate, DateTime endDate)
        {
            string str="{\"ok\":true,\"lists\":[";
            DateTime curDate = DateTime.Now;
            for (int i = 1; startDate < endDate && startDate < curDate; i++)
            {
                if (startDate.AddDays(1) == endDate || startDate.AddDays(1)>curDate)
                {
                    str +="{\"count\":"+ new BillDao(sid).Count(startDate, startDate.AddDays(1)) + "}";
                }
                else {
                    str += "{\"count\":" + new BillDao(sid).Count(startDate, startDate.AddDays(1)) + "},";
                }
                startDate=startDate.AddDays(1);
            }
            str += "]}";
            return str;
        }

        public static string GetCurrentYearOrder(string sid, DateTime startDate, DateTime endDate)
        {
            string str = "[";
            DateTime curDate = DateTime.Now;
            for (int i = 1; startDate < endDate && startDate < curDate; i++)
            {
                if (startDate.AddMonths(1) == endDate || startDate.AddMonths(1) > curDate)
                {
                    str += "{\"count\":" + new BillDao(sid).Count(startDate, startDate.AddMonths(1)) + "}";
                }
                else
                {
                    str += "{\"count\":" + new BillDao(sid).Count(startDate, startDate.AddMonths(1)) + "},";
                }
                startDate = startDate.AddMonths(1);
            }
            str += "]";
            return str;
        }

        public static string GetBillSales(string sid, DateTime startDate, DateTime endDate)
        {
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 1; startDate < endDate; i++)
            {
                if (startDate.AddDays(1) == endDate)
                {
                    str += "{\"sales\":" + new BillDao(sid).GetBillSales(startDate, startDate.AddDays(1)) + "}";
                }
                else
                {
                    str += "{\"sales\":" + new BillDao(sid).GetBillSales(startDate, startDate.AddDays(1)) + "},";
                }
                startDate = startDate.AddDays(1);
            }
            str += "]}";
            return str;
        }

        
    }
}

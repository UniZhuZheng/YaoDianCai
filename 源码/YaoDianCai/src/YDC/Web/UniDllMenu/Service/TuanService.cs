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
    public class TuanService
    {
        /*
         * TuanAction.CreateTuan
         * */
        public static bool AddTuan(string sid, string json)
        {
            if ("".Equals(json)||json==null)
            {
                return false;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            TuanEntity tuan = serializer.Deserialize<TuanEntity>(json);
            if (tuan == null)
            {
                return false;
            }
            try
            {
                ShopInfoEntity si = new ShopInfoDao(sid).Query();
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[6];
                args[0] = sid;
                args[1] = si.Name;
                args[2] = tuan.Number;
                args[3] = tuan.Website;
                args[4] = tuan.Owner;
                args[5] = tuan.Phone;
                WSHelper.InvokeWebService(url, "AddTuanRecord", args);
                
            }
            catch (Exception)
            {
                return false;
            }

            new TuanDao(sid).insert(tuan);

            return true;
        }
        /*
         * ClientAndroidService.RefreshTuan
         * */
        public static List<TuanEntity> GetAllNewTuan(string sid)
        {
            return new TuanDao(sid).queryAllNew();
        }

        public static List<TuanEntity> GetTuanList(string sid, int firstResult, int maxResult)
        {
            return new TuanDao(sid).queryAllNew(firstResult, maxResult);
        }

        public static int GetTuanListCount(string sid)
        {
            return new TuanDao(sid).queryCount();
        }

        public static List<TuanEntity> GetTuanListByDate(string sid, DateTime startDate, DateTime endDate, int firstResult, int maxResult)
        {
            return new TuanDao(sid).queryAllNew(startDate, endDate,firstResult, maxResult);
        }

        public static int GetBillListCountByDate(string sid, DateTime startDate, DateTime endDate)
        {
            return new TuanDao(sid).queryCount(startDate, endDate);
        }

        public static string GetCurrentMonthTuan(string sid, DateTime startDate, DateTime endDate)
        {
            string str = "{\"ok\":true,\"lists\":[";
            DateTime curDate = DateTime.Now;
            for (int i = 1; startDate < endDate && startDate < curDate; i++)
            {
                if (startDate.AddDays(1) == endDate || startDate.AddDays(1) > curDate)
                {
                    str += "{\"count\":" + new TuanDao(sid).queryCount(startDate, startDate.AddDays(1)) + "}";
                }
                else
                {
                    str += "{\"count\":" + new TuanDao(sid).queryCount(startDate, startDate.AddDays(1)) + "},";
                }
                startDate = startDate.AddDays(1);
            }
            str += "]}";
            return str;
        }

        public static string GetCurrentYearTuan(string sid, DateTime startDate, DateTime endDate)
        {
            string str = "[";
            DateTime curDate = DateTime.Now;
            for (int i = 1; startDate < endDate && startDate < curDate; i++)
            {
                if (startDate.AddMonths(1) == endDate || startDate.AddMonths(1) > curDate)
                {
                    str += "{\"count\":" + new TuanDao(sid).queryCount(startDate, startDate.AddMonths(1)) + "}";
                }
                else
                {
                    str += "{\"count\":" + new TuanDao(sid).queryCount(startDate, startDate.AddMonths(1)) + "},";
                }
                startDate = startDate.AddMonths(1);
            }
            str += "]";
            return str;
        }
    }
}

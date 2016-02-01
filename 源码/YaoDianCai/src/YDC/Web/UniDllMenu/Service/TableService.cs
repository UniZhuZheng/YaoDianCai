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
    public class TableService
    {
        /*
         * ShopFileUpload.ParseTableData
         * */
        public static bool RefreshAll(string sid, List<TableEntity> list)
        {
            if (list == null)
            {
                return false;
            }

            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[2];
                args[0] = sid;
                args[1] =Convert.ToString(list.Count);
                WSHelper.InvokeWebService(url, "UpdateTableInfo", args);

            }
            catch (Exception)
            {
                return false;
            }

            new TableDao(sid).insertAll(list);

            return true;
        }
        /*
         * TableAction.ListAllTables
         * ClientAndroidService.GetTableForShopOrder
         * */
        public static string GetAllTableJson(string sid)
        {
            List<TableEntity> list = new TableDao(sid).queryAll();
            var q = from t in list
                    select new
                        {
                            tablename = t.Name,
                            BID = t.BID
                        };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(q);
        }

        public static int GetTableCount(string sid)
        {
            return new TableDao(sid).count();
        }

        public static bool AddTable(string sid, string tableName)
        {
            TableDao dao = new TableDao(sid);
            if (dao.count(tableName) > 0)
            {
                return false;
            }

            dao.insert(tableName);
            return true;
        }
        
        public static void DelTable(string sid, string tableName)
        {
            new TableDao(sid).delete(tableName);
        }
        /*
         * TableAction.GetBIDByTable
         * BillService.AddBill
         * */
        public static string GetBIDByTable(string sid, string tableName)
        {
            return new TableDao(sid).GetBIDByTable(tableName);
        }
        public static void DelAllTables(string sid)
        {
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[2];
                args[0] = sid;
                args[1] = "0";
                WSHelper.InvokeWebService(url, "UpdateTableInfo", args);

            }
            catch (Exception)
            {
            }
            new TableDao(sid).deleteAll();
        }
        
    }
}

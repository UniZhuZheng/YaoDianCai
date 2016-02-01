using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Uni.YDC.Web.Manager.Service;
using Uni.YDC.Dao.Manager.Entity;


namespace Uni.WebManager.WebService
{
    [WebService(Namespace = "http://admin.yaodiancai.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebMenuRemote : System.Web.Services.WebService
    {
        [WebMethod(Description = "更新商家的菜单信息")]
        public bool UpdateDishInfo(string SID, string dishOnSellCount, string dishOffSellCount, string dishDisableCount, string dishCount)
        {
            ShopService.UpdateShopBaseInfo(SID, Convert.ToInt32(dishOnSellCount), Convert.ToInt32(dishOffSellCount), Convert.ToInt32(dishDisableCount), Convert.ToInt32(dishCount));
            return true;
        }
        [WebMethod(Description = "更新商家的桌号信息")]
        public bool UpdateTableInfo(string SID, string tableCount)
        {
            ShopService.UpdateShopBaseInfo(SID, Convert.ToInt32(tableCount));
            return true;
        }
      

        [WebMethod(Description = "添加商家下单的菜品信息")]
        public bool AddDishRecord(string bid, string dishNum, string dishName, string dishPrice, string dishCount) 
        {
            DishRecordService.Insert(bid, dishNum, dishName,Convert.ToSingle(dishPrice), Convert.ToInt32(dishCount));
            return true;
        }
        /*
         * WebMenu.BillService.AddBill
         * */
        [WebMethod(Description = "添加商家下单记录")]
        public bool AddBillRecord(string sid, string bid, string tableName,string memo,string dishRecord)
        {
            //BillService.AddBill(sid, bid, tableName);
            BillService.AddBill(sid, bid, tableName,memo, dishRecord);
            return true;
        }
        /*
         * WebMenu.TuanService.AddTuan
         * */
        [WebMethod(Description = "更新商家的团购信息")]
        public bool AddTuanRecord(string sid, string shopName, string number, string website, string owner, string phone)
        {
            TuanService.SetTuanRecord(sid, shopName, number, website, owner, phone);
            return true;
        }
        /*
         * WebMenu.WifiGWService.GetWifiGWBySID
         * */
        [WebMethod(Description = "获取商家的网关号")]
        public string GetWifiGWBySID(string sid)
        {
            List<WifiGWEntity> lists= WifiGWService.GetAllWifiGWByShop(sid);
            if (lists.Count > 0)
            {
                string str = "{\"ok\":true,\"lists\":[";
                for (int i = 0; i < lists.Count; i++)
                {
                    WifiGWEntity we = lists[i];
                    if (i == (lists.Count - 1))
                    {
                        str += "{";
                        str += "\"SID\":\"" + we.SID + "\",";
                        str += "\"GwId\":\"" + we.GwId + "\",";
                        str += "\"Address\":\"" + we.Address + "\",";
                        str += "\"Port\":\"" + we.Port + "\",";
                        str += "\"token\":\"" + (we.GwId + "-" + DateTime.Now.ToString("yyyyMMdd")) + "\"";
                        str += "}";
                    }
                    else {
                        str += "{";
                        str += "\"SID\":\"" + we.SID + "\",";
                        str += "\"GwId\":\"" + we.GwId + "\",";
                        str += "\"Address\":\"" + we.Address + "\",";
                        str += "\"Port\":\"" + we.Port + "\",";
                        str += "\"token\":\"" + (we.GwId + "-" + DateTime.Now.ToString("yyyyMMdd")) + "\"";
                        str += "},";
                    }
                    
                }
                str += "]}";
                return str;
            }
            else
            {
                return "{\"ok\":false}";
            }
        }
        [WebMethod(Description = "获取商家的网关号认证次数")]
        public string getAuthCount(string sid, string startDate, string endDate)
        {
            int num = WifiAuthService.GetCountByDate(Convert.ToDateTime(startDate),Convert.ToDateTime( endDate),sid);
            return num.ToString();
        }
        [WebMethod(Description = "获取商家的网关号每月认证次数")]
        public string GetCurrentMonthAuth(string sid, string startDateStr, string endDateStr)
        {
            DateTime startDate = Convert.ToDateTime(startDateStr);
            DateTime endDate = Convert.ToDateTime(endDateStr);
            string str = "{\"ok\":true,\"lists\":[";
            DateTime curDate = DateTime.Now;
            for (int i = 1; startDate < endDate && startDate < curDate; i++)
            {
                if (startDate.AddDays(1) == endDate || startDate.AddDays(1) > curDate)
                {
                    str += "{\"count\":" + WifiAuthService.GetCountByDate(startDate, startDate.AddDays(1),sid) + "}";
                }
                else
                {
                    str += "{\"count\":" + WifiAuthService.GetCountByDate(startDate, startDate.AddDays(1),sid) + "},";
                }
                startDate = startDate.AddDays(1);
            }
            str += "]}";
            return str;
        }
        [WebMethod(Description = "获取商家的网关号年认证次数")]
        public string GetCurrentYearAuth(string sid, string startDateStr, string endDateStr)
        {
            DateTime startDate = Convert.ToDateTime(startDateStr);
            DateTime endDate = Convert.ToDateTime(endDateStr);
            string str = "[";
            DateTime curDate = DateTime.Now;
            for (int i = 1; startDate < endDate && startDate < curDate; i++)
            {
                if (startDate.AddMonths(1) == endDate || startDate.AddMonths(1) > curDate)
                {
                    str += "{\"count\":" + WifiAuthService.GetCountByDate(startDate, startDate.AddMonths(1), sid) + "}";
                }
                else
                {
                    str += "{\"count\":" + WifiAuthService.GetCountByDate(startDate, startDate.AddMonths(1), sid) + "},";
                }
                startDate = startDate.AddMonths(1);
            }
            str += "]";
            return str;
        }
        /*
         * WebMenu.ShopInfoService.ShopLogin
         * */
        [WebMethod(Description = "商家登录验证")]
        public string ShopLogin(string account, string password)
        {
            ShopEntity shop = ShopService.ShopLogin(account, password);
            if (shop == null)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,";
            str += "\"SID\":\"" + shop.SID + "\",";
            str += "\"name\":\"" + shop.Name + "\"";
            str += "}";

            return str;
        }
        /*
         * WebMenu.OrderService.GetOrderDish
         * */
        [WebMethod(Description = "修改已点菜品的状态")]
        public bool UpdateOrderDishStatus(string bid, string dishNumber, string dishName, string dishPrice, string dishCount)
        {
            return DishRecordService.updateOrderDishStatus( bid, dishNumber, dishName, Convert.ToSingle(dishPrice), Convert.ToInt32(dishCount));
        }
        /*
         * WebMenu.WifiGWService.GetHostDomain
         * */
        [WebMethod(Description = "获取服务器的网址")]
        public string GetHostDomain(string hostId)
        {
            return HostService.GetHostDomain(hostId);
        }

        /*
         * WebMenu.ShopCommentService.ShopCommentAdd
         * */
        [WebMethod(Description = "添加商家点评描述")]
        public bool ShopCommentAdd(string SID, string comment)
        {
            return ShopCommentService.ShopCommentAdd(SID, comment);
        }

        /*
         * WebMenu.ShopLabelService.LabelsUpdateCount
         * */
        [WebMethod(Description = "修改商家标签点击数量")]
        public bool LabelsUpdateCount(string SID, string name,string count)
        {
            return ShopLabelService.LabelsUpdateCount(SID, name, Convert.ToInt32(count));
        }
    }
}

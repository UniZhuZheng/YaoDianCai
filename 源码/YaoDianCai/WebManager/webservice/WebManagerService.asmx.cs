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
    public class WebManagerService : System.Web.Services.WebService
    {
        [WebMethod(Description = "更新商家的菜单信息")]
        public bool updateDishInfo(string SID, int dishAmount, int dishMarket, int dishStop, int dishUnder)
        {
            //return ShopsInfoService.updateDishInfo(SID, dishAmount, dishMarket, dishStop, dishUnder);
            return true;
        }

        [WebMethod(Description = "更新商家的团购信息")]
        public bool updateGroupAuth(string number, string website, string owner, string phone, string sid, string shopName)
        {
            TuanService.SetTuanRecord(sid, shopName, number, website, owner, phone);
            return true;
        }

        [WebMethod(Description = "添加商家下单记录")]
        public bool addBills(string sid, string bid, string tableName)
        {
            BillService.AddBill(sid, bid, tableName);
            return true;
        }

        [WebMethod(Description = "添加商家下单的菜品信息")]
        public bool addDishRecords(string BID, string dishNum, string dishName, string dishPrice,string dishCount) 
        {
            DishRecordService.Insert(BID, dishNum, dishName, Convert.ToInt32(dishPrice), Convert.ToInt32(dishCount));
            return true;
        }

        [WebMethod(Description = "商家登录验证")]
        public string ShopsLogin(string account, string password)
        {
            ShopEntity shop = ShopService.ShopLogin(account, password);
            if (shop == null)
            {
                return "[{\"ok\":false}]";
            }

            string str = "[{\"ok\":true,";
            str += "\"SID\":\"" + shop.SID + "\",";
            str += "\"account\":\"" + shop.Account + "\",";
            str += "\"name\":\"" + shop.Name + "\",";
            str += "\"area\":\"" + shop.HostId + "\",";
            str += "\"phone\":\"" + shop.Phone + "\",";
            str += "\"email\":\"" + shop.Email + "\",";
            str += "\"address\":\"" + shop.Address + "\",";
            str += "\"contact\":\"" + shop.Contact + "\"";
            str += "}]";

            return str;
        }
    }
}

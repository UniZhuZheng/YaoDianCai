using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Uni.YDC.Web.Menu;


namespace Uni.WebMenu.WebService
{
    [WebService(Namespace = "http://sz.yaodiancai.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ClientAndroidRemote : System.Web.Services.WebService
    {
        [WebMethod(Description = "定时获取新点单信息和团购信息")]
        public string IntervalInfo(string sid)
        {
            return ClientAndroidService.IntervalInfo(sid);
        }

        [WebMethod(Description = "历史点单信息")]
        public string GetOldBillInfo(string sid, string time)
        {
            return ClientAndroidService.GetOldBillInfo(sid, time);
        }

        [WebMethod(Description = "修改新订单为历史点单")]
        public string ToOldBill(string sid, string bid)
        {
            return ClientAndroidService.ToOldBill(sid, bid);
        }

        [WebMethod(Description = "历史团购信息信息")]
        public string GetOldTuanInfo(string SID, string time)
        {
            return ClientAndroidService.GetOldTuanInfo(SID, time);
        }

        [WebMethod(Description = "修改新团购为历史团购")]
        public string ToOldTuan(string sid, string tuanNumber)
        {
            return ClientAndroidService.ToOldTuan(sid, tuanNumber);
        }

        [WebMethod(Description = "获取修改菜品信息")]
        public string GetAllDishInfo(string sid)
        {
            return ClientAndroidService.GetAllDishInfo(sid);
        }

        [WebMethod(Description = "修改菜品信息")]
        public string UpdateDishInfo(string sid, string dishName, string dishPrice, string dishState)
        {
            return ClientAndroidService.UpdateDishInfo(sid, dishName, dishPrice, dishState);
        }

        [WebMethod(Description = "菜品详细信息")]
        public string GetDishInfo(string sid, string dishName)
        {
            return ClientAndroidService.GetDishInfo(sid, dishName);
        }
        /*
         * 手机端删菜
         * */
        [WebMethod(Description = "修改已点菜品的状态")]
        public string UpdateOrderDishStatus(string sid,string bid, string dishNumber, string dishName, string dishPrice, string dishCount)
        {
            return ClientAndroidService.UpdateOrderDishStatus(sid, bid, dishNumber,  dishName,Convert.ToSingle(dishPrice), Convert.ToInt32(dishCount));
        }

        /*
         * 服务员手机客户端下单
         * */
        [WebMethod(Description = "为商家点单下单")]
        public string CreateBillForShopOrder(string sid, string json)
        {
            return ClientAndroidService.CreateBillForShopOrder(sid,json);
        }
        /*
         * 服务员手机客户端点菜
         * */
        [WebMethod(Description = "为商家点单获取桌号信息")]
        public string GetTableForShopOrder(string sid)
        {
            return ClientAndroidService.GetTableForShopOrder(sid);
        }
        /*
         * 服务员手机客户端获取购物车的桌号
         * */
        [WebMethod(Description = "为商家点单获取购物车中的桌号")]
        public string GetCartTableForShopOrder(string sid)
        {
            return ClientAndroidService.GetCartTableForShopOrder(sid);
        }
        /*
         * 服务员手机客户端清台
         * */
        [WebMethod(Description = "为商家点单清台")]
        public string DeleteCartInfoForShopOrder(string sid,string tableName)
        {
            return ClientAndroidService.DeleteCartInfoForShopOrder(sid,tableName);
        }

        /*
         * 
         * */
        [WebMethod(Description = "获取商家的呼叫信息")]
        public string GetCallList(string sid)
        {
            return ClientAndroidService.GetCallList(sid);
        }
        /*
         * 
         * */
        [WebMethod(Description = "修改商家的呼叫信息状态")]
        public string UpdateCallState(string sid, string tableName)
        {
            return ClientAndroidService.UpdateCallState(sid,tableName);
        }
    }
}

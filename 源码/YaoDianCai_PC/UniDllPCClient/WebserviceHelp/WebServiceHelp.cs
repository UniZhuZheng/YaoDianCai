using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using UniDllPCClient.EntityHelp;

namespace UniDllPCClient.WebserviceHelp
{
    public class WebServiceHelp
    {
        /// <summary>
        /// 商家登录信息
        /// </summary>
        /// <param name="sname"></param>
        /// <param name="spassword"></param>
        /// <returns></returns>
        public static ShopInfoEntity ShopLogin(string account, string password)
        {
            string webmethod = "ShopLogin";

            string parameter = HttpUtility.UrlEncode("account") + "=" + HttpUtility.UrlEncode(account)
                                  + "&" +
                                  HttpUtility.UrlEncode("password") + "=" + HttpUtility.UrlEncode(password);

            string shopentity = WebServiceVisit.WebServiceLogin(webmethod, parameter);

            JavaScriptSerializer webserialize = new JavaScriptSerializer();
            return webserialize.Deserialize<ShopInfoEntity>(shopentity.Substring(1, shopentity.Length - 2));
        }

        /// <summary>
        /// 定时获取新点单信息和团购信息
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public static IntervalInfoList IntervalInfo(string sid)
        {
            string webmethod = "IntervalInfo";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid);
            string newinfo = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            JavaScriptSerializer webserialize = new JavaScriptSerializer();
            return webserialize.Deserialize<IntervalInfoList>(newinfo);
        }
        /// <summary>
        /// 历史点单信息
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static BillEntityList GetOldBillInfo(string sid, string time)
        {
            string webmethod = "GetOldBillInfo ";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid)
                                  + "&" +
                               HttpUtility.UrlEncode("time") + "=" + HttpUtility.UrlEncode(time);

            string billentity = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            string billentitylist = "{ billentitylist:" + billentity + "}";
            JavaScriptSerializer webserialize = new JavaScriptSerializer();
            return webserialize.Deserialize<BillEntityList>(billentitylist);
        }
        /// <summary>
        /// 修改新订单为历史点单
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public static bool ToOldBill(string sid, string bid)
        {
            string webmethod = "ToOldBill ";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid)
                                  + "&" +
                                  HttpUtility.UrlEncode("bid") + "=" + HttpUtility.UrlEncode(bid);

            string tooldbill = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            if (tooldbill.IndexOf("true") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 历史团购信息信息
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TuanEntityList GetOldTuanInfo(string SID, string time)
        {
            string webmethod = "GetOldTuanInfo ";
            string parameter = HttpUtility.UrlEncode("SID") + "=" + HttpUtility.UrlEncode(SID)
                                  + "&" +
                                  HttpUtility.UrlEncode("time") + "=" + HttpUtility.UrlEncode(time);

            string tuanentity = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            string tuanentitylist = "{ tuanentitylist:" + tuanentity + "}";
            JavaScriptSerializer webserialize = new JavaScriptSerializer();
            return webserialize.Deserialize<TuanEntityList>(tuanentitylist);
        }
        /// <summary>
        /// 修改新团购为历史团购
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="tuanNumber"></param>
        /// <returns></returns>
        public static bool ToOldTuan(string sid, string tuanNumber)
        {
            string webmethod = "ToOldTuan ";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid)
                                  + "&" +
                                  HttpUtility.UrlEncode("tuanNumber") + "=" + HttpUtility.UrlEncode(tuanNumber);

            string tooldtuan = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            if (tooldtuan.IndexOf("true") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取修改菜品信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static PDishEntityList GetAllDishInfo(string sid)
        {
            string webmethod = "GetAllDishInfo ";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid);
            string orderentitylist = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            string porderentitylist = "{ dishentitylist:" + orderentitylist + "}";
            JavaScriptSerializer webserialize = new JavaScriptSerializer();
            return webserialize.Deserialize<PDishEntityList>(porderentitylist);
        }
        /// <summary>
        /// 修改菜品信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="dishName"></param>
        /// <param name="dishPrice"></param>
        /// <param name="dishState"></param>
        /// <returns></returns>
        public static bool UpdateDishInfo(string sid, string dishName, string dishPrice, string dishState)
        {
            string webmethod = "UpdateDishInfo ";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid)
                                + "&" +
                                HttpUtility.UrlEncode("dishName") + "=" + HttpUtility.UrlEncode(dishName)
                                + "&" +
                                HttpUtility.UrlEncode("dishPrice") + "=" + HttpUtility.UrlEncode(dishPrice)
                                + "&" +
                                HttpUtility.UrlEncode("dishState") + "=" + HttpUtility.UrlEncode(dishState);

            string updatedishinfo = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            if (updatedishinfo.IndexOf("true") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 菜品详细信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="dishName"></param>
        /// <returns></returns>
        public static DishEntity GetDishInfo(string sid, string dishName)
        {
            string webmethod = "GetDishInfo ";
            string parameter = HttpUtility.UrlEncode("sid") + "=" + HttpUtility.UrlEncode(sid)
                                  + "&" +
                                  HttpUtility.UrlEncode("dishName") + "=" + HttpUtility.UrlEncode(dishName);
            string orderentity = WebServiceVisit.WebServiceMenu(webmethod, parameter);
            JavaScriptSerializer webserialize = new JavaScriptSerializer();
            return webserialize.Deserialize<DishEntity>(orderentity);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Uni.YDC.Web.Menu;

namespace Uni.WebMenu.webservice
{
    /// <summary>
    /// ClientPCRemote 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://sz.yaodiancai.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ClientPCRemote : System.Web.Services.WebService
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
    }
}

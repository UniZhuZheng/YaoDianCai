using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Web.Site.Service;
using System.IO;
using System.Text;
using Uni.YDC.Dao.Site.Entity;

namespace Uni.WebSite.action
{
    /// <summary>
    /// ShopDishAction 的摘要说明
    /// </summary>
    public class ShopDishAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            string SID = context.Request.Params["SID"];
            string tableName = context.Request.Params["tableName"];
            if (string.IsNullOrEmpty(SID))
            {
                HttpContext.Current.Server.Transfer("/autherror.html");
                return;
            }
            //判断商家是否开通了内网服务
            InnerNetEntity inn = InnerNetService.QueryBySID(SID);
            if (inn != null)
            {
                if (!string.IsNullOrEmpty(tableName))
                {
                    HttpContext.Current.Response.Redirect("http://" + inn.IP+":"+ inn.Port+ "/index?tableName=" + tableName);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("http://" + inn.IP + ":" + inn.Port + "/index");
                }
            }
            else {
                string domain = WifiGWService.GetHostDomain(SID.Substring(0, 4));
                if (!string.IsNullOrEmpty(tableName))
                {
                    HttpContext.Current.Response.Redirect("http://" + domain + "/shop/" + SID + "/Html/Page/index.html?tableName=" + tableName);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("http://" + domain + "/shop/" + SID + "/Html/Page/index.html");
                }
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
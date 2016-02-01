using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using Uni.Core.Database;
using Uni.Core.Database.Sql;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections;
using Uni.YDC.Dao.Menu;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Web.Menu.Service;
using Uni.YDC.Web.Menu;


namespace Uni.WebMenu.Page
{
    public partial class ShopDishList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Cookies["userInfo"] == null)
            //{
            //    Server.Transfer("RequestError.aspx");
            //    return;
            //}

            string sid = HttpContext.Current.Request.Params["SID"];
            if (string.IsNullOrEmpty(sid))
            {
                Server.Transfer("RequestError.aspx");
                return;
            }

            string spath = Path.Combine(Global.ShopRootPath, sid);
            if (!Directory.Exists(spath))
            {
                Server.Transfer("RequestError.aspx");
                return;
            }

            int n = DishService.GetDishCount(sid);
            if (n <= 0)
            {
                Server.Transfer("ShopFileUpload.aspx?SID=" + sid);
                return;
            }

            ShopInfoEntity si = ShopInfoService.GetShopInfo(sid);
            this.ShopName.InnerText = si.Name;

            int tableCount = TableService.GetTableCount(sid);
            this.TableCount.InnerText =Convert.ToString(tableCount);
        }
     
    }
}
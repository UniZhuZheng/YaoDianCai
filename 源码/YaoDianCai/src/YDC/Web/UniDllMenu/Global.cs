using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace Uni.YDC.Web.Menu
{
    public static class Global
    {
        public static string ShopRootPath;
        public static string DefualtModel;
        public static string DefualtNodeModel;
        public static string YDCManagerDomain;

        static Global()
        {
            YDCManagerDomain = "admin.yaodiancai.com";
            ShopRootPath = HttpContext.Current.Server.MapPath("/shop/");
            DefualtModel = "model1";
            DefualtNodeModel = "NodeModel1";
        }
    }
}

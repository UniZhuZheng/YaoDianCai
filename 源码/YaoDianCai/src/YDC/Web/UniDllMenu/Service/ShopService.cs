using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;
using Uni.Core.Common.Utils;


namespace Uni.YDC.Web.Menu.Service
{
    public class ShopService
    {
        /*
         * WebManagerRemote.CreateShop
         * */
        public static bool CreateShop(string sid, ShopInfoEntity si)
        {
            string spath = Path.Combine(Global.ShopRootPath, sid);

            if (Directory.Exists(spath))
            {
                return false;
            }

            try
            {
                Directory.CreateDirectory(spath);
            }
            catch (Exception)
            {
                return false;
            }
            
            try
            {
                string dbpath = Path.Combine(spath, "DB");
                Directory.CreateDirectory(dbpath);

                string zippath = Path.Combine(spath, "Zip");
                Directory.CreateDirectory(zippath);

                string htmlpath = Path.Combine(spath, "Html");
                Directory.CreateDirectory(htmlpath);

                string pagepath = Path.Combine(htmlpath, "Page");
                Directory.CreateDirectory(pagepath);

                string shopinfopath = Path.Combine(htmlpath, "ShopInfo");
                Directory.CreateDirectory(shopinfopath);

                string nodepagepath = Path.Combine(htmlpath, "NodePage");
                Directory.CreateDirectory(nodepagepath);

                CreateJSContants(shopinfopath, si);

                string fileToUpZip = Path.Combine(Global.ShopRootPath, "Model", Global.DefualtModel + ".zip");
                string zipedFolder = Path.Combine(Path.Combine(Global.ShopRootPath, sid), "Html","Page");
                ZipHelper.UnZip(fileToUpZip, zipedFolder);

                //内网服务模板

                string nodefileToUpZip = Path.Combine(Global.ShopRootPath, "Model", Global.DefualtNodeModel + ".zip");
                string nodezipedFolder = Path.Combine(htmlpath, "NodePage");
                ZipHelper.UnZip(nodefileToUpZip, nodezipedFolder);

                string modeshopinfopath = Path.Combine(htmlpath, "NodePage", "public", "ShopInfo");
                CreateJSContants(modeshopinfopath, si);


                ShopDBFactory.Create(Path.Combine(dbpath, sid + ".sqlite"), sid);

                new ShopInfoDao(sid).InsertOnly(si);
            }
            catch (Exception)
            {
                try
                {
                    Directory.Delete(spath, true);
                }
                catch (Exception)
                {
                    return false;
                }

                return false;
            }

            return true;
        }

        private static void CreateJSContants(string path, ShopInfoEntity si)
        {
            string file = Path.Combine(path, "contants.js");
            string content = "var SID='" + si.SID+"';";
            content += "var name='" + si.Name + "';";
            if (!File.Exists(file))
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Create, FileAccess.Write)))
                {
                    sw.WriteLine(content);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Open, FileAccess.Write)))
                {
                    sw.WriteLine(content);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class InnerNetService
    {
        /*
         * WebManagerRemote.InnerNetFileUpdate
         * */
        public static bool InnerNetFileUpdate(string sid, string name, string domain, string ip, string port) {
            try {
                string spath = Path.Combine(Global.ShopRootPath, sid);
                string htmlpath = Path.Combine(spath, "Html");
                string modeshopinfopath = Path.Combine(htmlpath, "NodePage", "public", "ShopInfo");

                string file = Path.Combine(modeshopinfopath, "contants.js");
                string content = "var SID='" + sid + "';";
                content += "var name='" + name + "';";
                content += "var host='http://" + domain + "';";
                content += "var localIp='http://" + ip + ":" + port + "';";

                if (!File.Exists(file))
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Create, FileAccess.Write)))
                    {
                        sw.WriteLine(content);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Open, FileAccess.Write)))
                    {
                        
                        sw.WriteLine(content);
                        sw.Close();
                    }
                }
                string readpathfile = Path.Combine(Global.ShopRootPath, "Model", "NodeModel2", "Server.js");
                string writepathfile = Path.Combine(htmlpath, "NodePage", "Server.js");
                if (File.Exists(readpathfile))
                {
                    using (StreamReader sr = new StreamReader(new FileStream(readpathfile, FileMode.Open, FileAccess.Read)))
                    {
                        string str = sr.ReadToEnd();
                        str = str.Replace("{","{{");
                        str = str.Replace("}", "}}");
                        str = str.Replace("@0@", "{0}");
                        str = string.Format(str, port);
                        StreamWriter sw = new StreamWriter(new FileStream(writepathfile, FileMode.Open, FileAccess.Write));
                        sw.WriteLine(str);

                        sw.Close();
                        sr.Close();
                    }
                }
                else
                {
                    return false;
                }
                //将重写的文件进行压缩
                ZipHelper.CompressDirectory(Path.Combine(htmlpath, "NodePage"), Path.Combine(htmlpath, "NodePage.zip"), 6, 1024);

                return true;
            }catch(Exception e){
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager.Entity;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Manager;

namespace Uni.YDC.Web.Manager.Service
{
    public class InnerNetService
    {
        /*
         * ClientCommonRemote.InnerNetLogin
         * */
        public static string Insert(string account, string password, string IP, string port)
        {
            //验证用户是否存在
            ShopEntity se = ShopService.ShopLogin(account, password);
            if (se != null)
            {
                string SID = se.SID;
                string name = se.Name;
                string domain = se.HostDomain;
                //用户存在，远程调用，修改内网的文件
                bool ret = false;
                try
                {
                    string url = "http://" + domain + "/webservice/WebManagerRemote.asmx";
                    string[] args = new string[5];
                    args[0] = SID;
                    args[1] = name;
                    args[2] = domain;
                    args[3] = IP;
                    args[4] = port;
                    object obj = WSHelper.InvokeWebService(url, "InnerNetFileUpdate", args);
                    ret = Convert.ToBoolean(obj);
                }
                catch (Exception)
                {
                    return "[{\"ok\":false}]";
                }

                if (!ret)
                {
                    return "[{\"ok\":false}]";
                }
                //新增到数据库
                if (InnerNetDao.Insert(SID, IP, port))
                {
                    return "[{\"ok\":true,\"domain\":\"http://" + domain + "/shop/" + SID + "/Html\",\"fileName\":\"NodePage.zip\"}]";
                }
                else
                {
                    return "[{\"ok\":false}]";
                }
            }
            else
            {
                return "[{\"ok\":false}]";
            }
        }
        /*
         * ClientCommonRemote.InnerNetUpdateStatus
         * */
        public static bool Update(string SID, int status)
        {
            return InnerNetDao.Update(SID, status);
        }
    }
}

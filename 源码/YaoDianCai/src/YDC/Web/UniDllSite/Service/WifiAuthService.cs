using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Site;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Site.Service
{
    public class WifiAuthService
    {
        /*
         * auth.ProcessRequest
         * */
        public static void RefreshAuthRecord(string gwId, string token, string mac, string ip, int incoming, int outcoming)
        {
            string sid = gwId.Substring(3, 10);
            //try
            //{
            //    string url = "http://" + Global.YDCManagerDomain + "/webservice/WebSiteRemote.asmx";
            //    string[] args = new string[7];
            //    args[0] = sid;
            //    args[1] = gwId;
            //    args[2] = token;
            //    args[3] = mac;
            //    args[4] = ip;
            //    args[5] = incoming.ToString();
            //    args[6] = outcoming.ToString();
            //    WSHelper.InvokeWebService(url, "RefreshAuthRecord", args);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}


            if (WifiAuthDao.Exists(sid, token, mac))
            {
                WifiAuthDao.Update(sid, gwId, token, mac, ip, incoming, outcoming);
            }
            else
            {
                WifiAuthDao.Insert(sid, gwId, token, mac, ip, incoming, outcoming);
            }
        }
    }
}

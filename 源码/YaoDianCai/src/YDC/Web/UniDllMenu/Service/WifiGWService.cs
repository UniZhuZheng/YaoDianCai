using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.Core.Common.Utils;

namespace Uni.YDC.Web.Menu.Service
{
    public class WifiGWService
    {
        /*
         * WifiGWAction.GetWifiGWBySID
         * */
        public static string GetWifiGWBySID(string SID)
        {
            if ("".Equals(SID) || SID == null)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":false}";
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[1];
                args[0] = SID;
                str = WSHelper.InvokeWebService(url, "GetWifiGWBySID", args).ToString();

            }
            catch (Exception)
            {
                return "{\"ok\":false}";
            }

            return str;
        }

        //获取认证数量

        public static int getAuthCount(string SID, DateTime startDate, DateTime endDate)
        {
            if ("".Equals(SID) || SID == null)
            {
                return 0;
            }
            int str = 0;
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[3];
                args[0] = SID;
                args[1] = startDate.ToString("yyyy-MM-dd");
                args[2] = endDate.ToString("yyyy-MM-dd");
                str = Convert.ToInt32(WSHelper.InvokeWebService(url, "getAuthCount", args).ToString());

            }
            catch (Exception)
            {
                return 0;
            }

            return str;
        }

        public static string GetCurrentMonthAuth(string sid, DateTime startDate, DateTime endDate)
        {
            if ("".Equals(sid) || sid == null)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":false}";
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[3];
                args[0] = sid;
                args[1] = startDate.ToString("yyyy-MM-dd");
                args[2] = endDate.ToString("yyyy-MM-dd");
                str = WSHelper.InvokeWebService(url, "GetCurrentMonthAuth", args).ToString();

            }
            catch (Exception)
            {
                return "{\"ok\":false}";
            }

            return str;
        }

        public static string GetCurrentYearAuth(string sid, DateTime startDate, DateTime endDate)
        {
            if ("".Equals(sid) || sid == null)
            {
                return "[]";
            }
            string str = "[]";
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[3];
                args[0] = sid;
                args[1] = startDate.ToString("yyyy-MM-dd");
                args[2] = endDate.ToString("yyyy-MM-dd");
                str = WSHelper.InvokeWebService(url, "GetCurrentYearAuth", args).ToString();

            }
            catch (Exception)
            {
                return "[]";
            }

            return str;
        }
        /*
         * HostAction.ProcessRequest
         * */
        public static string GetHostDomain(string hostId)
        {
            string obj = "";
            try
            {
                string url = "http://" + Global.YDCManagerDomain + "/webservice/WebMenuRemote.asmx";
                string[] args = new string[1];
                args[0] = hostId;
                obj = WSHelper.InvokeWebService(url, "GetHostDomain", args).ToString();
            }
            catch (Exception)
            {
                return null;
            }
            return obj;
        }
    }
}

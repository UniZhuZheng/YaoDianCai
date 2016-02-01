using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace UniDllChart.Service
{
    public class MenuDefinedService
    {
        public static string postUrlStr = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
        public static string postUrl;
        public static string menuInfo = "{"+
                                             "\"button\":["+
                                             "{"+	
                                                  "\"type\":\"click\","+
                                                  "\"name\":\"商家列表\","+
                                                  "\"key\":\"Shop_List\""+
                                              "},"+
                                              "{"+
                                                   "\"name\":\"数据查询\","+
                                                   "\"sub_button\":["+
                                                        "{"+
                                                           "\"type\":\"click\","+
                                                           "\"name\":\"当日汇总\","+
                                                           "\"key\":\"TODAY_INFO\""+
                                                        "},"+
                                                        "{"+
                                                           "\"type\":\"click\","+
                                                           "\"name\":\"当月汇总\","+
                                                           "\"key\":\"MONTH_INFO\""+
                                                        "},"+
                                                        "{"+
                                                           "\"type\":\"click\","+
                                                           "\"name\":\"全部汇总\","+
                                                           "\"key\":\"ALL_INFO\""+
                                                        "},"+
                                                        "{"+
                                                           "\"type\":\"click\","+
                                                           "\"name\":\"当日下单明细\","+
                                                           "\"key\":\"ORDER_INFO\""+
                                                        "}"+
                                                    "]"+
                                               "},"+
                                              "{"+
                                                   "\"type\":\"click\","+
                                                   "\"name\":\"商家绑定\","+
                                                   "\"key\":\"Shop_Band\""+
                                              "}"+
                                              "]"+
                                         "}";
        //新建自定义菜单
        static MenuDefinedService() {
            postUrl = string.Format(postUrlStr, GetAccessToken());
        }
        public static string createMenu() {
            string returnValue = string.Empty;
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(menuInfo);
                Uri uri = new Uri(postUrl);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = byteData.Length;
                //定义Stream信息
                Stream stream = webReq.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Close();
                //获取返回信息
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                returnValue = streamReader.ReadToEnd();
                //关闭信息
                streamReader.Close();
                response.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                return returnValue;
            }
            return returnValue;
        }
        protected static string GetAccessToken()
        {
            string accessToken = string.Empty;
            string getUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            //getUrl = string.Format(getUrl, "你申请的appid", "你申请的secret");
            getUrl = string.Format(getUrl, "wx37214d47902449a2", "7e6a260935a5a14e2ff52093356eb5cc");
            Uri uri = new Uri(getUrl);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
            webReq.Method = "POST";

            //获取返回信息
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string returnJason = streamReader.ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(returnJason);
            object value;
            if (json.TryGetValue("access_token", out value))
            {
                accessToken = value.ToString();
            }
            return accessToken;
        }

    }
    
}

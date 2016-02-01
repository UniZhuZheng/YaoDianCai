using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace UniDllPCClient.WebserviceHelp
{
    public class WebServiceVisit
    {
        private static string domain;

        public static string Domain
        {
            get { return WebServiceVisit.domain; }
            set { WebServiceVisit.domain = value; }
        }

        public static string WebServiceMenu(string webmethod, string parameter)
        {
            string URL = "http://" + domain +"/webservice/ClientAndroidRemote.asmx/" + webmethod;
            //创建一个HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //Post请求方式
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //设置参数，并进行URL编码
            byte[] payload;
            //将URL编码后的字符串转化为字节
            payload = System.Text.Encoding.UTF8.GetBytes(parameter);
            //设置请求的ContentLength
            request.ContentLength = payload.Length;
            //发送请求，获得请求流
            Stream writer = request.GetRequestStream();
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            //关闭请求流
            writer.Close();
            //获得响应流
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            //转化为XML，自己进行处理
            XmlTextReader Reader = new XmlTextReader(s);
            Reader.MoveToContent();
            return Reader.ReadInnerXml();
        }

        public static string WebServiceLogin(string webmethod, string parameter)
        {
            //string URL = "http://192.168.0.105:8081/webservice/ClientCommonRemote.asmx/" + webmethod;
            string URL = "http://admin.yaodiancai.com/webservice/ClientCommonRemote.asmx/" + webmethod;
            //创建一个HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //Post请求方式
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //设置参数，并进行URL编码
            byte[] payload;
            //将URL编码后的字符串转化为字节
            payload = System.Text.Encoding.UTF8.GetBytes(parameter);
            //设置请求的ContentLength
            request.ContentLength = payload.Length;
            //发送请求，获得请求流
            Stream writer = request.GetRequestStream();
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            //关闭请求流
            writer.Close();

            //获得响应流
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            //转化为XML，自己进行处理
            XmlTextReader Reader = new XmlTextReader(s);
            Reader.MoveToContent();
            return Reader.ReadInnerXml();
        }
    }
}

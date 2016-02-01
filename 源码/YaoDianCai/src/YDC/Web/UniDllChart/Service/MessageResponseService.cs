using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using UniDaoChart.EntityResponse;

namespace UniDllChart.Service
{
    public class MessageResponseService
    {
        /** 
	     * 返回消息类型：文本 
	     */
        public static string RESP_MESSAGE_TYPE_TEXT = "text";

        /** 
         * 返回消息类型：音乐 
         */
        public static string RESP_MESSAGE_TYPE_MUSIC = "music";

        /** 
         * 返回消息类型：图文 
         */
        public static string RESP_MESSAGE_TYPE_NEWS = "news";

        /** 
         * 请求消息类型：文本 
         */
        public static string REQ_MESSAGE_TYPE_TEXT = "text";

        /** 
         * 请求消息类型：图片 
         */
        public static string REQ_MESSAGE_TYPE_IMAGE = "image";

        /** 
         * 请求消息类型：链接 
         */
        public static string REQ_MESSAGE_TYPE_LINK = "link";

        /** 
         * 请求消息类型：地理位置 
         */
        public static string REQ_MESSAGE_TYPE_LOCATION = "location";

        /** 
         * 请求消息类型：音频 
         */
        public static string REQ_MESSAGE_TYPE_VOICE = "voice";

        /** 
         * 请求消息类型：推送 
         */
        public static string REQ_MESSAGE_TYPE_EVENT = "event";

        /** 
         * 事件类型：subscribe(订阅) 
         */
        public static string EVENT_TYPE_SUBSCRIBE = "subscribe";

        /** 
         * 事件类型：unsubscribe(取消订阅) 
         */
        public static string EVENT_TYPE_UNSUBSCRIBE = "unsubscribe";

        /** 
         * 事件类型：CLICK(自定义菜单点击事件) 
         */
        public static string EVENT_TYPE_CLICK = "CLICK";

        /*
         * 解析微信发来的请求（XML）
         * */
        public static Dictionary<string, string> parseXml(Stream s)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //封装请求类  
            XmlDocument doc = new XmlDocument();
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            doc.LoadXml(Encoding.UTF8.GetString(b));
            XmlElement rootElement = doc.DocumentElement;
            XmlNodeList xnlist = rootElement.ChildNodes;

            foreach (XmlNode xn in xnlist)
            {
                dic.Add(xn.Name, xn.InnerText);
            }
            return dic;
        }

        /*
         * 文本消息对象转换成xml
         * */
        public static string textMessageToXml(TextMessage textMessage)
        {

            return "<xml>" +
                         "<ToUserName><![CDATA[" + textMessage.ToUserName + "]]></ToUserName>" +
                         "<FromUserName><![CDATA[" + textMessage.FromUserName + "]]></FromUserName>" +
                         "<CreateTime>" + textMessage.CreateTime + "</CreateTime>" +
                         "<MsgType><![CDATA[" + textMessage.MsgType + "]]></MsgType>" +
                         "<Content><![CDATA[" + textMessage.Content + "]]></Content>" +
                     "</xml>";
        }

        /*
         * 图片消息对象转换成xml
         * */
        public static string imageMessageToXml(ImageMessage imageMessage)
        {

            return "<xml>" +
                         "<ToUserName><![CDATA[" + imageMessage.ToUserName + "]]></ToUserName>" +
                         "<FromUserName><![CDATA[" + imageMessage.FromUserName + "]]></FromUserName>" +
                         "<CreateTime>" + imageMessage.CreateTime + "</CreateTime>" +
                         "<MsgType><![CDATA[" + imageMessage.MsgType + "]]></MsgType>" +
                         "<Image>" +
                            "<MediaId><![CDATA[" + imageMessage.Image.MediaId + "]]></MediaId>" +
                        "</Image>" +
                     "</xml>";
        }

        /*
         * 语音消息对象转换成xml
         * */
        public static string voiceMessageToXml(VoiceMessage voiceMessage)
        {

            return "<xml>" +
                         "<ToUserName><![CDATA[" + voiceMessage.ToUserName + "]]></ToUserName>" +
                         "<FromUserName><![CDATA[" + voiceMessage.FromUserName + "]]></FromUserName>" +
                         "<CreateTime>" + voiceMessage.CreateTime + "</CreateTime>" +
                         "<MsgType><![CDATA[" + voiceMessage.MsgType + "]]></MsgType>" +
                         "<Voice>" +
                            "<MediaId><![CDATA[" + voiceMessage.Voice.MediaId + "]]></MediaId>" +
                        "</Voice>" +
                     "</xml>";
        }

        /*
         * 视频消息对象转换成xml
         * */
        public static string videoMessageToXml(VideoMessage videoMessage)
        {

            return "<xml>" +
                         "<ToUserName><![CDATA[" + videoMessage.ToUserName + "]]></ToUserName>" +
                         "<FromUserName><![CDATA[" + videoMessage.FromUserName + "]]></FromUserName>" +
                         "<CreateTime>" + videoMessage.CreateTime + "</CreateTime>" +
                         "<MsgType><![CDATA[" + videoMessage.MsgType + "]]></MsgType>" +
                         "<Video>" +
                            "<MediaId><![CDATA[" + videoMessage.Video.MediaId + "]]></MediaId>" +
                            "<Title><![CDATA[" + videoMessage.Video.MediaId + "]]></Title>" +
                            "<Description><![CDATA[" + videoMessage.Video.MediaId + "]]></Description>" +
                        "</Video>" +
                     "</xml>";
        }

        /*
         * 音乐消息对象转换成xml
         * */
        public static string musicMessageToXml(MusicMessage musicMessage)
        {
            return "<xml>" +
                        "<ToUserName><![CDATA[" + musicMessage.ToUserName + "]]></ToUserName>" +
                        "<FromUserName><![CDATA[" + musicMessage.FromUserName + "]]></FromUserName>" +
                        "<CreateTime>" + musicMessage.CreateTime + "</CreateTime>" +
                        "<MsgType><![CDATA[" + musicMessage.MsgType + "]]></MsgType>" +
                        "<Music>" +
                            "<Title><![CDATA[" + musicMessage.Music.Title + "]]></Title>" +
                            "<Description><![CDATA[" + musicMessage.Music.Description + "]]></Description>" +
                            "<MusicUrl><![CDATA[" + musicMessage.Music.MusicUrl + "]]></MusicUrl>" +
                            "<HQMusicUrl><![CDATA[" + musicMessage.Music.HQMusicUrl + "]]></HQMusicUrl>" +
                            "<ThumbMediaId><![CDATA[" + musicMessage.Music.ThumbMediaId + "]]></ThumbMediaId>" +
                        "</Music>" +
                    "</xml>";
        }
        /*
         * 图文消息对象转换成xml
         * */
        public static string newsMessageToXml(NewsMessage newsMessage)
        {
            string str = "<xml>" +
                        "<ToUserName><![CDATA[" + newsMessage.ToUserName + "]]></ToUserName>" +
                        "<FromUserName><![CDATA[" + newsMessage.FromUserName + "]]></FromUserName>" +
                        "<CreateTime>" + newsMessage.CreateTime + "</CreateTime>" +
                        "<MsgType><![CDATA[" + newsMessage.MsgType + "]]></MsgType>" +
                        "<ArticleCount>" + newsMessage.ArticleCount + "</ArticleCount>" +
                        "<Articles>";
            foreach (ArticleEntity art in newsMessage.Articles)
            {
                str += "<item>" +
                        "<Title><![CDATA[" + art.Title + "]]></Title>" +
                        "<Description><![CDATA[" + art.Description + "]]></Description>" +
                        "<PicUrl><![CDATA[" + art.PicUrl + "]]></PicUrl>" +
                        "<Url><![CDATA[" + art.Url + "]]></Url>" +
                    "</item>";
            }
            str += "</Articles>" + "</xml>";
            return str;
        }
    }
}

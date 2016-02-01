using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniDllChart.Service;
using UniDaoChart.EntityResponse;
using System.Text.RegularExpressions;
using WebChart.service;

namespace WebChart.action
{
    /// <summary>
    /// MainAction 的摘要说明
    /// </summary>
    public class MainAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string returnStr = "";
            if (HttpContext.Current.Request.HttpMethod.ToLower() == "post")
            {
                returnStr = processRequest(context);
            }
            else
            {
                //用户第一次请求，对请求进行校验
                returnStr = Auth(context);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(returnStr);
        }

        private string Auth(HttpContext context)
        {
             //微信加密签名   
            string signature = context.Request.QueryString["signature"];
             //时间戳   
            string timestamp = context.Request.QueryString["timestamp"];
             //随机数   
            string nonce = context.Request.QueryString["nonce"];
             //随机字符串   
            string echostr = context.Request.QueryString["echostr"];
             //通过检验signature对请求进行校验，若校验成功则原样返回echostr，表示接入成功，否则接入失败
            if (AuthService.checkSignature(signature, timestamp, nonce))
            {
                return echostr;
            }
            return "";

        }

        private string processRequest(HttpContext context)
        {
            string respMessage = null;
            try {
                // 默认返回的文本消息内容
                string respContent = "请求处理异常，请稍候尝试！";
                // xml请求解析
                Dictionary<string, string> dic = MessageResponseService.parseXml(context.Request.InputStream);
                // 发送方帐号（open_id）
                string fromUserName = dic["FromUserName"];
                // 公众帐号   
                string toUserName = dic["ToUserName"];
                // 消息类型   
                string msgType = dic["MsgType"];

                // 文本消息   
                if (msgType.Equals(MessageResponseService.REQ_MESSAGE_TYPE_TEXT))
                {
                    string reqContent = dic["Content"];
                    respContent = MessageTextService.ContentDeal(fromUserName,reqContent);
                }
                // 图片消息   
                else if (msgType.Equals(MessageResponseService.REQ_MESSAGE_TYPE_IMAGE))
                {
                    respContent = "您发送的是图片消息！";
                }
                // 地理位置消息   
                else if (msgType.Equals(MessageResponseService.REQ_MESSAGE_TYPE_LOCATION))
                {
                    respContent = "您发送的是地理位置消息！";
                }
                // 链接消息   
                else if (msgType.Equals(MessageResponseService.REQ_MESSAGE_TYPE_LINK))
                {
                    respContent = "您发送的是链接消息！";
                }
                // 音频消息   
                else if (msgType.Equals(MessageResponseService.REQ_MESSAGE_TYPE_VOICE))
                {
                    respContent = "您发送的是音频消息！";
                }

                // 事件推送   
                else if (msgType.Equals(MessageResponseService.REQ_MESSAGE_TYPE_EVENT))
                {
                    // 事件类型   
                    string eventType = dic["Event"];
                    // 订阅   
                    if (eventType.Equals(MessageResponseService.EVENT_TYPE_SUBSCRIBE))
                    {
                        respContent = "谢谢您的关注！";
                        WebChatService.Insert(fromUserName);
                        return "";
                    }
                    // 取消订阅   
                    else if (eventType.Equals(MessageResponseService.EVENT_TYPE_UNSUBSCRIBE))
                    {
                        // TODO 取消订阅后用户再收不到公众号发送的消息，因此不需要回复消息   
                    }
                    // 自定义菜单点击事件   
                    else if (eventType.Equals(MessageResponseService.EVENT_TYPE_CLICK))
                    {
                        // TODO 自定义菜单权没有开放，暂不处理该类消息 
                        string eventKey= dic["EventKey"];
                        if (eventKey.Equals("Shop_List"))
                        {
                            respContent = EventClickService.GetShopList();
                        }
                        else if (eventKey.Equals("TODAY_INFO"))
                        {
                            respContent = EventClickService.GetInfoByDay(fromUserName);
                        }
                        else if (eventKey.Equals("MONTH_INFO"))
                        {
                            respContent = EventClickService.GetInfoByMonth(fromUserName);
                        }
                        else if (eventKey.Equals("ALL_INFO"))
                        {
                            respContent = EventClickService.GetInfoByAll(fromUserName);
                        }
                        else if (eventKey.Equals("ORDER_INFO"))
                        {
                            respContent = EventClickService.GetOrderInfo(fromUserName);
                        }
                        else if (eventKey.Equals("Shop_Band"))
                        {
                            respContent = EventClickService.BandShop(fromUserName);
                        }
                    }
                }

                // 回复文本消息   
                TextMessage textMessage = new TextMessage();
                textMessage.ToUserName = fromUserName;
                textMessage.FromUserName = toUserName;

                DateTime d1 = DateTime.Now;
                DateTime d2 = new DateTime(1970, 1, 1);
                double d = d1.Subtract(d2).TotalMilliseconds;
                textMessage.CreateTime = Convert.ToInt64(d1.Subtract(d2).TotalMilliseconds) / 1000;
                textMessage.MsgType = MessageResponseService.RESP_MESSAGE_TYPE_TEXT;
                //textMessage.FuncFlag = 0;
                textMessage.Content = respContent;
                respMessage = MessageResponseService.textMessageToXml(textMessage);
            }
            catch (Exception e)
            {
                throw e;
            }
            return respMessage;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
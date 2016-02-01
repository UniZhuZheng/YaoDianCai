using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityRequest
{
    /*
     * 消息基类（普通用户 -> 公众帐号）
     * */
    public class BaseMessage
    {
        // 开发者微信号   
        private string toUserName;
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }
        // 发送方帐号（一个OpenID）   
        private string fromUserName;
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }
        // 消息创建时间 （整型）   
        private Int64 createTime;
        public Int64 CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        // 消息类型（text/image/location/link）   
        private string msgType;
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        // 消息id，64位整型   
        private Int64 msgId;
        public Int64 MsgId
        {
            get { return msgId; }
            set { msgId = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
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
        // 位0x0001被标志时，星标刚收到的消息   
        private int funcFlag;
        public int FuncFlag
        {
            get { return funcFlag; }
            set { funcFlag = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityRequest
{
    /*
     * 请求消息之文本消息
     * 
     * */
    public class TextMessage : BaseMessage
    {
        // 消息内容
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}

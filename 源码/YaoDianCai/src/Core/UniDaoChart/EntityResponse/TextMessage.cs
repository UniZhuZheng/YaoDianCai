using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
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

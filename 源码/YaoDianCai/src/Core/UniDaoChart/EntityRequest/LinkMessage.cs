using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityRequest
{
    /*
     * 请求消息之链接消息
     * */
    public class LinkMessage : BaseMessage
    {
        // 消息标题   
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        // 消息描述   
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        // 消息链接   
        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}

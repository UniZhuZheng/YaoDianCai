using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityRequest
{
    /*
     * 请求消息之图片消息
     * */
    public class ImageMessage : BaseMessage
    {
        // 图片链接
        private string picUrl;
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }
    }
}

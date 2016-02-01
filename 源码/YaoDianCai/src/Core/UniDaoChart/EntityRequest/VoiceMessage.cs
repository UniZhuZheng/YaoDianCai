using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityRequest
{
    /*
     * 请求消息之语音消息
     * */
    public class VoiceMessage : BaseMessage
    {
        // 媒体ID   
        private string mediaId;
        public string MediaId
        {
            get { return mediaId; }
            set { mediaId = value; }
        }
        // 语音格式   
        private string format;
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

    }
}

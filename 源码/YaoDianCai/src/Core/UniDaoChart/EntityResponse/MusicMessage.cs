using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    /*
     * 响应消息之音乐消息
     * */
    public class MusicMessage : BaseMessage
    {
        // 音乐
        private MusicEntity music;
        public MusicEntity Music
        {
            get { return music; }
            set { music = value; }
        }
    }
}

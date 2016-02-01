using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    public class MusicEntity
    {
        // 音乐名称   
	    private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
	    // 音乐描述   
	    private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
	    // 音乐链接   
	    private string musicUrl;
        public string MusicUrl
        {
            get { return musicUrl; }
            set { musicUrl = value; }
        }
	    // 高质量音乐链接，WIFI环境优先使用该链接播放音乐   
	    private string hqMusicUrl;
        public string HQMusicUrl
        {
            get { return hqMusicUrl; }
            set { hqMusicUrl = value; }
        }
        //缩略图的媒体id，通过上传多媒体文件，得到的id 
        private string thumbMediaId;
        public string ThumbMediaId {
            get { return thumbMediaId; }
            set { thumbMediaId = value; }
        }

    }
}

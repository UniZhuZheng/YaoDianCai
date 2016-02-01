using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    public class VideoMessage : BaseMessage
    {
        private VideoEntity video;
        public VideoEntity Video
        {
            get { return video; }
            set { video = value; }
        }
    }
}

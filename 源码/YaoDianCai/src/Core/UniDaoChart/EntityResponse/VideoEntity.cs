using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    public class VideoEntity
    {
        private string mediaId;
        public string MediaId
        {
            get { return mediaId; }
            set { mediaId = value; }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}

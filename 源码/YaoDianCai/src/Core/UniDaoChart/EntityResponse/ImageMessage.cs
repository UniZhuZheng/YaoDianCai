using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    public class ImageMessage : BaseMessage
    {
        private ImageEntity image;
        public ImageEntity Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}

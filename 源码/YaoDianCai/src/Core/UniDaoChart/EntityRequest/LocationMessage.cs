using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityRequest
{
    /*
     * 请求消息之地理位置消息
     * */
    public class LocationMessage : BaseMessage
    {
        // 地理位置维度   
        private string location_X;
        public string Location_X
        {
            get { return location_X; }
            set { location_X = value; }
        }
        // 地理位置经度   
        private string location_Y;
        public string Location_Y
        {
            get { return location_Y; }
            set { location_Y = value; }
        }
        // 地图缩放大小   
        private string scale;
        public string Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        // 地理位置信息   
        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

    }
}

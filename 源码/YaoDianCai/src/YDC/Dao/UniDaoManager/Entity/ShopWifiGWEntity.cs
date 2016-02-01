using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class ShopWifiGWEntity
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string sid;
        public string SID
        {
            get { return sid; }
            set { sid = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private WifiGWEntity[] wifiGWEntity;
        public WifiGWEntity[] WifiGWEntity
        {
            get { return wifiGWEntity; }
            set { wifiGWEntity = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class NetPreventEntity
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
        private string mac;
        public string MAC
        {
            get { return mac; }
            set { mac = value; }
        }
    }
}

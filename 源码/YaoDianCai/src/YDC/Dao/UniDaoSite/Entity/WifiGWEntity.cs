using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Site.Entity
{
    public class WifiGWEntity
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

        private string gwId;
        public string GwId
        {
            get { return gwId; }
            set { gwId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string port;
        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        private string remoteHost;
        public string RemoteHost
        {
            get { return remoteHost; }
            set { remoteHost = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

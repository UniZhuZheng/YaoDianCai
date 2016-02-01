using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Site.Entity
{
    public class HostEntity
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string hostId;
        public string HostId
        {
            get { return hostId; }
            set { hostId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string domain;
        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        private string ip;
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

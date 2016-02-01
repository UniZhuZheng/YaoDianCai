using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class WifiAuthEntity
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

        private string token;
        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        private string mac;
        public string Mac
        {
            get { return mac; }
            set { mac = value; }
        }

        private string userIp;
        public string UserIp
        {
            get { return userIp; }
            set { userIp = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

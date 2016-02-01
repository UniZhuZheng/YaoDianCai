using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class InnerNetEntity
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
        private string ip;
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
        private string port;
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

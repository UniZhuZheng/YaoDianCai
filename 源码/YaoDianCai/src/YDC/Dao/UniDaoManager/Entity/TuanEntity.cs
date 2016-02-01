using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class TuanEntity
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

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private string website;
        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        private string owner;
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string shopName;
        public string ShopName
        {
            get { return shopName; }
            set { shopName = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

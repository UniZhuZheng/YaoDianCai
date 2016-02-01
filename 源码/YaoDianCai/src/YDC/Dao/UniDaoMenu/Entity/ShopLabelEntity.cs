using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Menu.Entity
{
    public class ShopLabelEntity
    {
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

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

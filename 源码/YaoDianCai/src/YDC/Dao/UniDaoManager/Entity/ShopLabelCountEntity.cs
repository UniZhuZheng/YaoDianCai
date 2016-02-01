using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class ShopLabelCountEntity
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

        private int labelId;
        public int LabelId
        {
            get { return labelId; }
            set { labelId = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private DateTime lastDate;
        public DateTime LastDate
        {
            get { return lastDate; }
            set { lastDate = value; }
        }
    }
}

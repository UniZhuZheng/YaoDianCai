using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    [Serializable]
    public class BillEntity
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string bid;
        public string BID
        {
            get { return bid; }
            set { bid = value; }
        }

        private string sid;
        public string SID
        {
            get { return sid; }
            set { sid = value; }
        }

        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private int dishCount;
        public int DishCount
        {
            get { return dishCount; }
            set { dishCount = value; }
        }

        private float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        private string memo;
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
}

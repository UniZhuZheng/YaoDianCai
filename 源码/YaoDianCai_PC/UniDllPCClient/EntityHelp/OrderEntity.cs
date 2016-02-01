using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDllPCClient.EntityHelp
{
    public class OrderEntity
    {
        private string bid;
        public string BID
        {
            get { return bid; }
            set { bid = value; }
        }

        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dishNumber;
        public string DishNumber
        {
            get { return dishNumber; }
            set { dishNumber = value; }
        }

        private string dishName;
        public string DishName
        {
            get { return dishName; }
            set { dishName = value; }
        }

        private string dishPrice;
        public string DishPrice
        {
            get { return dishPrice; }
            set { dishPrice = value; }
        }

        private int dishCount;
        public int DishCount
        {
            get { return dishCount; }
            set { dishCount = value; }
        }

        private string createDate;
        public string CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

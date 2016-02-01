using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Menu.Entity
{
    public class BillEntity
    {
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

        private int totalCount;
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }

        private string totalPrice;
        public string TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        private List<OrderEntity> orders;
        public List<OrderEntity> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        private string memo;
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
}

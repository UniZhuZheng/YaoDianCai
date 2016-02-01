using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class DishRecordEntity
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        //private string bid;
        //public string BID
        //{
        //    get { return bid; }
        //    set { bid = value; }
        //}
        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}

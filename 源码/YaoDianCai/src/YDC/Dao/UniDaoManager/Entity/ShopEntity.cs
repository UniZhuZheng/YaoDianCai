using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class ShopEntity
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

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        private string hostId;
        public string HostId
        {
            get { return hostId; }
            set { hostId = value; }
        }
        private string hostName;
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string contact;
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        private int dishOnSellCount;
        public int DishOnSellCount
        {
            get { return dishOnSellCount; }
            set { dishOnSellCount = value; }
        }

        private int dishOffSellCount;
        public int DishOffSellCount
        {
            get { return dishOffSellCount; }
            set { dishOffSellCount = value; }
        }

        private int dishDisableCount;
        public int DishDisableCount
        {
            get { return dishDisableCount; }
            set { dishDisableCount = value; }
        }

        private int dishSalesCount;
        public int DishSalesCount
        {
            get { return dishSalesCount; }
            set { dishSalesCount = value; }
        }

        private int dishCount;
        public int DishCount
        {
            get { return dishCount; }
            set { dishCount = value; }
        }

        private int billCount;
        public int BillCount
        {
            get { return billCount; }
            set { billCount = value; }
        }

        private int tuanCount;
        public int TuanCount
        {
            get { return tuanCount; }
            set { tuanCount = value; }
        }

        private int tableCount;
        public int TableCount
        {
            get { return tableCount; }
            set { tableCount = value; }
        }

        private int wifiGWCount;
        public int WifiGWCount
        {
            get { return wifiGWCount; }
            set { wifiGWCount = value; }
        }

        private int wifiAuthCount;
        public int WifiAuthCount
        {
            get { return wifiAuthCount; }
            set { wifiAuthCount = value; }
        }

        private int tplId;
        public int TplId
        {
            get { return tplId; }
            set { tplId = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        private string hostDomain;
        public string HostDomain
        {
            get { return hostDomain; }
            set { hostDomain = value; }
        }
    }
}


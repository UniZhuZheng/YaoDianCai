using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDllPCClient.EntityHelp
{
    public class ShopInfoEntity
    {
        private bool ok;

        public bool Ok
        {
            get { return ok; }
            set { ok = value; }
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



        private string domain;
        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; }
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

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

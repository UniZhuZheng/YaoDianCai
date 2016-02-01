using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDllPCClient.EntityHelp
{
    /// <summary>
    /// 团购信息
    /// </summary>
    public class TuanEntity
    {
        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
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

        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }

    public class TuanEntityList
    {
        private List<TuanEntity> tuanentitylist;
        public List<TuanEntity> Tuanentitylist
        {
            get { return tuanentitylist; }
            set { tuanentitylist = value; }
        }
    }
}

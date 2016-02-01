using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Menu.Entity
{
    public class CallEntity
    {
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
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
    }
}

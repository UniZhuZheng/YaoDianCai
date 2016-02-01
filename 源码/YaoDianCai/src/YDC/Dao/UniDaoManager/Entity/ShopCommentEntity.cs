using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Manager.Entity
{
    public class ShopCommentEntity
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

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}

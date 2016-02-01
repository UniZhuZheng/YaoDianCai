using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Menu.Entity
{
    public class TableEntity
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string bid;
        public string BID
        {
            get { return bid; }
            set { bid = value; }
        }
    }
}

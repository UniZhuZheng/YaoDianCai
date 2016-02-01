using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Menu.Entity
{
    public class TuanListEntity
    {
        private List<TuanEntity> tuanList;
        public List<TuanEntity> TuanList
        {
            get { return tuanList; }
            set { tuanList = value; }
        }
    }
}

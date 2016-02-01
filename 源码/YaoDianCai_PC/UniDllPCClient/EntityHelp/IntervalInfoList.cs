using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDllPCClient.EntityHelp
{
    public class IntervalInfoList
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        private List<BillEntity> billInfo;
        public List<BillEntity> BillInfo
        {
            get { return billInfo; }
            set { billInfo = value; }
        }

        /// <summary>
        /// 团购信息
        /// </summary>
        private List<TuanEntity> tuanInfo;
        public List<TuanEntity> TuanInfo
        {
            get { return tuanInfo; }
            set { tuanInfo = value; }
        }

    }
}

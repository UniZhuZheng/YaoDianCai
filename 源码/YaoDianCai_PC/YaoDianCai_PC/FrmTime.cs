using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace YaoDianCai_PC
{
    public partial class FrmTime : CCSkinMain
    {
        public delegate void PickTime(string time);

        public FrmTime()
        {
            InitializeComponent();
        }

        public event PickTime PickSelectTime;
        private void btnSure_Click(object sender, EventArgs e)
        {
            PickSelectTime(mcdTime.SelectionStart.ToString("yyyy年MM月dd日"));
            this.Close();
        }

        private void mcdTime_MouseDown(object sender, MouseEventArgs e)
        {
            //MonthCalendar.HitTestInfo hitInfo = mcdTime.HitTest(e.Location);
            //if (hitInfo.HitArea == MonthCalendar.HitArea.Date)//当选择了日期后
            //{
            //    this.labTime.Text = mcdTime.SelectionStart.ToString("yyyy年MM月dd日");
            //}
        }

    }
}

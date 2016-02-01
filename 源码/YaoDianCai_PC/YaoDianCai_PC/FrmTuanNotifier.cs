using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using UniDllPCClient.EntityHelp;
using CCWin.Win32;
using CCWin.Win32.Const;

namespace YaoDianCai_PC
{
    public partial class FrmTuanNotifier : CCSkinMain
    {
        private string _ipaddress;
        //订单信息
        private TuanEntity _tuanentity;
        //商家信息
        private ShopInfoEntity _shopinfofntity;

        public delegate void CloseTuanFrmNotifier(string number);

        public FrmTuanNotifier()
        {
            InitializeComponent();
        }

        public FrmTuanNotifier(ShopInfoEntity shopinfofntity, TuanEntity tuanentity, string ipaddress)
            : this()
        {
            this._ipaddress = ipaddress;
            this._tuanentity = tuanentity;
            this._shopinfofntity = shopinfofntity;
        }

        public event CloseTuanFrmNotifier closetuanfrmnotifier;
        private void FrmTuanNotifier_FormClosing(object sender, FormClosingEventArgs e)
        {
            closetuanfrmnotifier(_tuanentity.Number);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FrmTuanEntity newbillentity = new FrmTuanEntity(_shopinfofntity, _tuanentity, _ipaddress, true,true);
            newbillentity.ShowDialog();
            this.Close();
        }


        private void FrmTuanNotifier_Load(object sender, EventArgs e)
        {
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.PointToScreen(p);
            this.Location = p;
            NativeMethods.AnimateWindow(this.Handle, 130, AW.AW_SLIDE + AW.AW_VER_NEGATIVE);//开始窗体动画
            this.labRemMsg.Text = _tuanentity.Number;
        }
    }
}

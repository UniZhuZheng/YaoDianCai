using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.Win32;
using CCWin.Win32.Const;
using UniDllPCClient.EntityHelp;

namespace YaoDianCai_PC
{
    public partial class FrmBillNotifier : CCSkinMain
    {
        private string _ipaddress;
        //订单信息
        private BillEntity _billentity;
        //商家信息
        private ShopInfoEntity _shopinfofntity;
        
        public delegate void CloseFrmNotifier(string bid);

        public FrmBillNotifier()
        {
            InitializeComponent();
        }
        public FrmBillNotifier(ShopInfoEntity shopinfofntity, BillEntity billentity, string ipaddress)
            : this()
        {
            this._ipaddress = ipaddress;
            this._billentity = billentity;
            this._shopinfofntity = shopinfofntity;
        }

        private void FrmNotifier_Load(object sender, EventArgs e)
        {
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.PointToScreen(p);
            this.Location = p;
            NativeMethods.AnimateWindow(this.Handle, 130, AW.AW_SLIDE + AW.AW_VER_NEGATIVE);//开始窗体动画
            this.labRemMsg.Text = _billentity.BID;
        }

        public event CloseFrmNotifier closefrmnotifier;
        private void FrmNotifier_FormClosing(object sender, FormClosingEventArgs e)
        {
            closefrmnotifier(_billentity.BID);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FrmBillEntityPrint newbillentity = new FrmBillEntityPrint(_shopinfofntity, _billentity, _ipaddress,true,true);
            newbillentity.ShowDialog();
            this.Close();
        }

    }
}

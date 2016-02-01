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
using UniDllPCClient.PrintHelp;
using UniDllPCClient.WebserviceHelp;

namespace YaoDianCai_PC
{
    public partial class FrmTuanEntity : CCSkinMain
    {
        private bool _noflag;
        //
        private bool _btnManage;
        //打印机ip地址
        private string _ipaddress;
        //商家信息
        private ShopInfoEntity _shopinfofntity;
        private TuanEntity _tuanentityinfo;
        public delegate void RemoveItem(TuanEntity tuanentity);
        public FrmTuanEntity()
        {
            InitializeComponent();
        }
        public FrmTuanEntity(ShopInfoEntity shopinfofntity, TuanEntity tuanentityinfo, string ipaddress, bool noflag, bool btnManage)
            : this()
        {
            this._noflag = noflag;
            this._btnManage = btnManage;
            this._ipaddress = ipaddress;
            this._shopinfofntity = shopinfofntity;
            this._tuanentityinfo = tuanentityinfo;
        }
        private void FrmTuanEntity_Load(object sender, EventArgs e)
        {
            this.labTableName.Text = _tuanentityinfo.TableName;
            this.labTime.Text = _tuanentityinfo.CreateDate.ToString("yyyy年MM月dd日 时hh分mm秒ss");
            this.labWebSite.Text = _tuanentityinfo.Website;
            this.labPhone.Text = _tuanentityinfo.Phone;
            this.labNumber.Text = _tuanentityinfo.Number;
            if (!_btnManage)
            {
                this.btnManage.Visible = false;
                this.btnClose.Visible = true;
            }
        }
        public event RemoveItem removetuanitem;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool openprint = false;
            if (!string.IsNullOrEmpty(_ipaddress))
            {
                openprint = PosPrintHelp.OpenPrint(_ipaddress);
            }
            else
            {
                openprint = PosPrintHelp.OpenPrint();
            }

            if (openprint)
            {
                PosPrintHelp.PrintTuanEntity(_tuanentityinfo, _shopinfofntity.Name);
                bool flag = WebServiceHelp.ToOldTuan(_shopinfofntity.SID, _tuanentityinfo.Number);
                if (flag)
                {
                    MessageBox.Show("打印成功！");
                    if (!_noflag)
                    {
                        //删除打印过的订单
                        removetuanitem(_tuanentityinfo);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("打印失败！");
                }
            }
            else
            {
                MessageBox.Show("打印机链接失败！" + _ipaddress);
            }
        }
        private void btnManage_Click(object sender, EventArgs e)
        {
            bool flag = WebServiceHelp.ToOldTuan(_shopinfofntity.SID, _tuanentityinfo.Number);
            if (flag)
            {
                MessageBox.Show( _tuanentityinfo.Number + "团购处理！");
                if (!_noflag)
                {
                    //删除打印过的订单
                    removetuanitem(_tuanentityinfo);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(_tuanentityinfo.Number + "团购处理岁处理失败！");
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class FrmBillEntityPrint : CCSkinMain
    {
        private bool _noflag;
        //处理按钮显示
        private bool _btnManage;
        //打印机ip
        private string _ipaddress;
        //订单信息
        private BillEntity _billentity;     
        //商家信息
        private ShopInfoEntity _shopinfofntity;
        
        public delegate void RemoveItem(string bid);

        public FrmBillEntityPrint()
        {
            InitializeComponent();
        }
        public FrmBillEntityPrint(ShopInfoEntity shopinfofntity, BillEntity billentity, string ipaddress, bool noflag, bool btnManage)
            : this()
        {
            this._noflag = noflag;
            this._btnManage = btnManage;
            this._ipaddress = ipaddress;
            this._billentity = billentity;
            this._shopinfofntity = shopinfofntity;
        }

        private void FrmBillEntityNew_Load(object sender, EventArgs e)
        {
            //桌号
            this.labTableName.Text = _billentity.TableName;
            //时间
            this.labTime.Text = _billentity.CreateDate.ToString("yyyy年MM月dd日hh时mm分");
            //总份数
            this.labTotalOrderNum.Text = _billentity.TotalCount.ToString() + " 份";
            //总价
            this.labTotalPrice.Text = _billentity.TotalPrice + " 元";
            if (!_btnManage)
            {
                this.btnManage.Visible = false;
                this.btnClose.Visible = true;
            }

            this.lstMenuInfo.Columns.Add("空格", 0, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("子菜编号", 110, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("子菜名称", 150, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("子菜价格", 100, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("子菜份数", 80, HorizontalAlignment.Center);

            for (int i = 0; i < _billentity.Orders.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = " ";
                item.SubItems.Add(_billentity.Orders[i].DishNumber);
                item.SubItems.Add(_billentity.Orders[i].DishName);
                item.SubItems.Add(_billentity.Orders[i].DishCount.ToString() + " 份");
                int totalprice = Convert.ToInt32(_billentity.Orders[i].DishPrice) * (_billentity.Orders[i].DishCount);
                item.SubItems.Add(totalprice + " 元");
                this.lstMenuInfo.Items.Add(item);
            }
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            bool printflag = WebServiceHelp.ToOldBill(_shopinfofntity.SID, _billentity.BID);
            if (printflag)
            {
                MessageBox.Show(_billentity.BID + "处理成功");
                //删除打印过的订单
                removebillitem(_billentity.BID);
                this.Close();
            }
            else
            {
                MessageBox.Show(_billentity.BID + "处理失败");
            }
        }

        public event RemoveItem removebillitem;
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

            //链接打印机
            if (openprint)
            {
                PosPrintHelp.PrintBillEntity(_billentity, _shopinfofntity.Name);

                bool printflag = WebServiceHelp.ToOldBill(_shopinfofntity.SID, _billentity.BID);
                if (printflag)
                {
                    MessageBox.Show(_billentity.BID + "打印成功");
                    if (_noflag)
                    {
                        //删除打印过的订单
                        removebillitem(_billentity.BID);
                        this.Close();
                    }
                  
                }
                else
                {
                    MessageBox.Show(_billentity.BID + "打印失败");
                }
            }
            else
            {
                MessageBox.Show("打印机链接失败！");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

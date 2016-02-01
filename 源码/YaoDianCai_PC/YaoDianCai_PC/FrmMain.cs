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
using System.Threading;
using UniDllPCClient.SystemHelp;
using UniDllPCClient.WebserviceHelp;

namespace YaoDianCai_PC
{
    public partial class FrmMain : CCSkinMain
    {
        //商家信息
        private ShopInfoEntity _shopinfofntity;
        //定时获取新信息
        private IntervalInfoList _intervalinfolist;
        //小窗口提示
        private Dictionary<string, FrmBillNotifier> Billfrmnotifier = new Dictionary<string, FrmBillNotifier>();
        private Dictionary<string, FrmTuanNotifier> Tuanfrmnotifier = new Dictionary<string, FrmTuanNotifier>();
        private string Ipaddress = string.Empty;

        public FrmMain()
        {
            InitializeComponent();
        }
        public FrmMain(ShopInfoEntity shopinfofntity)
            : this()
        {
            this._shopinfofntity = shopinfofntity;
            FromHelp.eventSend += new SendHandler(ReceiveHandler);
        }
        //监听事件
        void ReceiveHandler(object sender, object msg)
        {
            Type t = msg.GetType();
            if (t.IsEnum)
            {
                eControl e = (eControl)msg;
                switch (e)
                {
                    case eControl.Load_IntervalInfo:
                        LoadIntervalInfo(sender as IntervalInfoList);
                        break;
                }
            }
        }
        //窗体加载
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.lblName.Text = _shopinfofntity.Name;
            this.labTime.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            //新订单列表
            this.lstNewOrder.Columns.Add("订单号", 0, HorizontalAlignment.Center);
            this.lstNewOrder.Columns.Add("桌号", 100, HorizontalAlignment.Center);
            this.lstNewOrder.Columns.Add("总数量", 50, HorizontalAlignment.Center);
            this.lstNewOrder.Columns.Add("总价", 70, HorizontalAlignment.Center);
            this.lstNewOrder.Columns.Add("时间", 180, HorizontalAlignment.Center);
            //历史点单
            this.lstOldOrder.Columns.Add("订单号", 0, HorizontalAlignment.Center);
            this.lstOldOrder.Columns.Add("桌号", 100, HorizontalAlignment.Center);
            this.lstOldOrder.Columns.Add("总数量", 50, HorizontalAlignment.Center);
            this.lstOldOrder.Columns.Add("总价", 70, HorizontalAlignment.Center);
            this.lstOldOrder.Columns.Add("时间", 180, HorizontalAlignment.Center);
            //新团购列表
            this.lstNewGroupBuy.Columns.Add("团购号", 0, HorizontalAlignment.Center);
            this.lstNewGroupBuy.Columns.Add("桌号", 120, HorizontalAlignment.Center);
            this.lstNewGroupBuy.Columns.Add("团购来源", 100, HorizontalAlignment.Center);
            this.lstNewGroupBuy.Columns.Add("团购时间", 180, HorizontalAlignment.Center);
            //历史团购
            this.lstOldGroupBuy.Columns.Add("团购号", 0, HorizontalAlignment.Center);
            this.lstOldGroupBuy.Columns.Add("桌号", 120, HorizontalAlignment.Center);
            this.lstOldGroupBuy.Columns.Add("团购来源", 100, HorizontalAlignment.Center);
            this.lstOldGroupBuy.Columns.Add("团购时间", 180, HorizontalAlignment.Center);

            //启动线程定时获取新数据
            Thread thread = new Thread(() => GetIntervalInfoList());
            thread.Start();
        }
        //定时获取数据
        private void GetIntervalInfoList()
        {
            while (true)
            {
                _intervalinfolist = WebServiceHelp.IntervalInfo(_shopinfofntity.SID);
                if (_intervalinfolist.BillInfo.Count > 0 || _intervalinfolist.TuanInfo.Count > 0)
                {
                    FromHelp.SendMessage(_intervalinfolist, eControl.Load_IntervalInfo);
                }
                Thread.Sleep(10000);
            }
        }
        //跟新列表数据代理
        delegate void LoadIntervalInfoEventHandler(IntervalInfoList intervalinfolist);
        private void LoadIntervalInfo(IntervalInfoList intervalinfolist)
        {
            if (this.InvokeRequired)
            {
                LoadIntervalInfoEventHandler lni = new LoadIntervalInfoEventHandler(LoadIntervalInfo);
                this.Invoke(lni, new object[] { intervalinfolist });
            }
            else
            {
                this.LoadBillInfo();
                this.LoadTuanInfo();
            }
        }


        #region 新订单
        private void LoadBillInfo()
        {
            if (this.Visible)
            {
                this.lstNewOrder.Items.Clear();
                for (int i = 0; i < _intervalinfolist.BillInfo.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = _intervalinfolist.BillInfo[i].BID;
                    item.SubItems.Add(_intervalinfolist.BillInfo[i].TableName);
                    item.SubItems.Add(_intervalinfolist.BillInfo[i].TotalCount.ToString() + "份");
                    item.SubItems.Add(_intervalinfolist.BillInfo[i].TotalPrice + "元");
                    item.SubItems.Add(_intervalinfolist.BillInfo[i].CreateDate.ToString("yyyy年MM月dd日"));
                    this.lstNewOrder.Items.Add(item);
                }
                if (this.lstNewOrder.Items.Count > 0)
                {
                    this.ptbNeworderMsg.Visible = true;
                }
            }
            else
            {
                for (int i = 0; i < _intervalinfolist.BillInfo.Count; i++)
                {
                    FrmBillNotifier frmnotifiter = new FrmBillNotifier(_shopinfofntity, _intervalinfolist.BillInfo[i], Ipaddress);
                    if (!Billfrmnotifier.ContainsKey(_intervalinfolist.BillInfo[i].BID))
                    {
                        Billfrmnotifier.Add(_intervalinfolist.BillInfo[i].BID, frmnotifiter);
                        frmnotifiter.closefrmnotifier += new FrmBillNotifier.CloseFrmNotifier(CloseBillFrm);
                        frmnotifiter.Show();
                    }
                }
            }
        }
        private void CloseBillFrm(object obid)
        {
            if (Billfrmnotifier.ContainsKey(obid.ToString()))
            {
                Billfrmnotifier.Remove(obid.ToString());
            }
            if (Billfrmnotifier.Keys.Count == 0)
            {
                this.ptbNeworderMsg.Visible = false;
            }
        }
        private void lstNewOrder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstNewOrder.SelectedItems.Count > 0 && this.lstNewOrder.SelectedItems != null)
            {
                string bid = this.lstNewOrder.SelectedItems[0].Text;
                for (int i = 0; i < _intervalinfolist.BillInfo.Count; i++)
                {
                    if (_intervalinfolist.BillInfo[i].BID == bid)
                    {
                        FrmBillEntityPrint newbillentity = new FrmBillEntityPrint(_shopinfofntity, _intervalinfolist.BillInfo[i], Ipaddress,false,true);
                        newbillentity.removebillitem += new FrmBillEntityPrint.RemoveItem(RemoveBillItems);
                        newbillentity.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要打印的订单！");
            }             
        }
        private void RemoveBillItems(object obid)
        {
            foreach (ListViewItem item in this.lstNewOrder.Items)
            {
                if (item.SubItems[0].Text == obid.ToString())
                {
                    item.Remove();
                }
            }
            this.lstNewOrder.Refresh();

            if (this.lstNewOrder.Items.Count == 0)
            {
                this.ptbNeworderMsg.Visible = false;
            }
        }
        #endregion

        #region 历史订单
        private BillEntityList _billentityklist;
        private void BillEntityOldData()
        {
            string time = Convert.ToDateTime(this.labTime.Text).ToString("yyyy-MM-dd");
            _billentityklist = WebServiceHelp.GetOldBillInfo(_shopinfofntity.SID, time);
            if (_billentityklist != null || _billentityklist.Billentitylist.Count > 0)
            {
                this.lstOldOrder.Items.Clear();
                for (int i = 0; i < _billentityklist.Billentitylist.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = _billentityklist.Billentitylist[i].BID;
                    item.SubItems.Add(_billentityklist.Billentitylist[i].TableName);
                    item.SubItems.Add(_billentityklist.Billentitylist[i].TotalCount.ToString() + "份");
                    item.SubItems.Add(_billentityklist.Billentitylist[i].TotalPrice + "元");
                    item.SubItems.Add(_billentityklist.Billentitylist[i].CreateDate.ToString("yyyy年MM月dd日"));
                    this.lstOldOrder.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("数据请求失败！");
                return;
            }
        }
        private void lstOldOrder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstOldOrder.SelectedItems.Count > 0 && this.lstOldOrder.SelectedItems != null)
            {
                BillEntity billentity = new BillEntity();
                string bid = this.lstOldOrder.SelectedItems[0].Text;
                for (int i = 0; i < _billentityklist.Billentitylist.Count; i++)
                {
                    if (_billentityklist.Billentitylist[i].BID == bid)
                    {
                        billentity = _billentityklist.Billentitylist[i];
                    }
                }
                FrmBillEntityPrint newbillentity = new FrmBillEntityPrint(_shopinfofntity, billentity, Ipaddress, false, false);
                newbillentity.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择要历史订单！");
            }       
        }
        #endregion

        #region 新团购
        public void LoadTuanInfo()
        {
            if (this.Visible)
            {
                this.lstNewGroupBuy.Items.Clear();
                for (int i = 0; i < _intervalinfolist.TuanInfo.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = _intervalinfolist.TuanInfo[i].Number;
                    item.SubItems.Add(_intervalinfolist.TuanInfo[i].TableName);
                    item.SubItems.Add(_intervalinfolist.TuanInfo[i].Website);
                    item.SubItems.Add(_intervalinfolist.TuanInfo[i].CreateDate.ToString("yyyy年MM月dd日"));
                    this.lstNewGroupBuy.Items.Add(item);
                }
                if (this.lstNewGroupBuy.Items.Count > 0)
                {
                    this.ptbNewGroupBuyMsg.Visible = true;
                }
            }
            else
            {
                for (int i = 0; i < _intervalinfolist.TuanInfo.Count; i++)
                {
                    FrmTuanNotifier frmnotifiter = new FrmTuanNotifier(_shopinfofntity, _intervalinfolist.TuanInfo[i], Ipaddress);
                    if (!Billfrmnotifier.ContainsKey(_intervalinfolist.BillInfo[i].BID))
                    {
                        Tuanfrmnotifier.Add(_intervalinfolist.TuanInfo[i].Number, frmnotifiter);
                        frmnotifiter.closetuanfrmnotifier += new FrmTuanNotifier.CloseTuanFrmNotifier(CloseTuanFrm);
                        frmnotifiter.Show();
                    }
                }
            }
        }
        private void CloseTuanFrm(object onum)
        {
            if (Tuanfrmnotifier.ContainsKey(onum.ToString()))
            {
                Tuanfrmnotifier.Remove(onum.ToString());
            }
            if (Tuanfrmnotifier.Keys.Count == 0)
            {
                this.ptbNewGroupBuyMsg.Visible = false;
            }
        }
        private void lstNewGroupBuy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstNewGroupBuy.SelectedItems.Count > 0 && this.lstNewGroupBuy.SelectedItems != null)
            {
                TuanEntity tuanentity = new TuanEntity();
                string number = this.lstNewGroupBuy.SelectedItems[0].SubItems[0].Text;
                for (int i = 0; i < _intervalinfolist.TuanInfo.Count; i++)
                {
                    if (_intervalinfolist.TuanInfo[i].Number == number)
                    {
                       tuanentity = _intervalinfolist.TuanInfo[i];
                    }
                }
                FrmTuanEntity groupbuyprint = new FrmTuanEntity(_shopinfofntity, tuanentity, Ipaddress, false,true);
                groupbuyprint.removetuanitem += new FrmTuanEntity.RemoveItem(RemoveTuanItems);
                groupbuyprint.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择要打印的团购单！");
            }
        }
        private void RemoveTuanItems(object gblist)
        {
            TuanEntity removegblist = (TuanEntity)gblist;

            foreach (ListViewItem item in this.lstNewGroupBuy.Items)
            {
                if (item.SubItems[0].Text == removegblist.Number && item.SubItems[1].Text == removegblist.TableName)
                {
                    item.Remove();
                }
            }
            this.lstNewGroupBuy.Refresh();

            if (this.lstNewGroupBuy.Items.Count == 0)
            {
                this.ptbNewGroupBuyMsg.Visible = false;
            }
        }
        #endregion

        #region 历史团购
        private TuanEntityList _tuanentitylist;
        private void TuanEntityOldData()
        {
            string time = Convert.ToDateTime(this.labTime.Text).ToString("yyyy-MM-dd");
            _tuanentitylist = WebServiceHelp.GetOldTuanInfo(_shopinfofntity.SID, time);
            if (_tuanentitylist != null || _tuanentitylist.Tuanentitylist.Count > 0)
            {
                this.lstOldGroupBuy.Items.Clear();
                for (int i = 0; i <  _tuanentitylist.Tuanentitylist.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = _tuanentitylist.Tuanentitylist[i].Number;
                    item.SubItems.Add( _tuanentitylist.Tuanentitylist[i].TableName);
                    item.SubItems.Add( _tuanentitylist.Tuanentitylist[i].Website);
                    item.SubItems.Add( _tuanentitylist.Tuanentitylist[i].CreateDate.ToString("yyyy年MM月dd日"));
                    this.lstOldGroupBuy.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("数据请求失败！");
                return;
            }
        }
        private void lstOldGroupBuy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstOldGroupBuy.SelectedItems.Count > 0 && this.lstOldGroupBuy.SelectedItems != null)
            {
                TuanEntity tuanentity = new TuanEntity();
                string number = this.lstOldGroupBuy.SelectedItems[0].SubItems[0].Text;
                for (int i = 0; i < _tuanentitylist.Tuanentitylist.Count; i++)
                {
                    if (_tuanentitylist.Tuanentitylist[i].Number == number)
                    {
                        tuanentity = _tuanentitylist.Tuanentitylist[i];
                    }
                }
                FrmTuanEntity groupbuyprint = new FrmTuanEntity(_shopinfofntity, tuanentity, Ipaddress, false, false);
                groupbuyprint.removetuanitem += new FrmTuanEntity.RemoveItem(RemoveTuanItems);
                groupbuyprint.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择要查看的团购单！");
            }
        }

        #endregion

        #region 小图标按钮
        //时间选择
        private void toolbtnTime_Click(object sender, EventArgs e)
        {
            FrmTime time = new FrmTime();
            time.PickSelectTime += new FrmTime.PickTime(PickSelectTime);
            time.ShowDialog();
        }
        private void PickSelectTime(object time)
        {
            this.labTime.Text = time.ToString();
            if (this.tabManTab.SelectedIndex == 1)
            {
                this.BillEntityOldData();
            }
            if (this.tabManTab.SelectedIndex == 3)
            {
                this.TuanEntityOldData();
            }
        }

        //菜单管理
        private void toolbtnMenuManger_Click(object sender, EventArgs e)
        {
            FrmPassway _frmpassway = new FrmPassway();
            _frmpassway.passwaypassword += new FrmPassway.PassWayPassWord(OpenMenuManager);
            _frmpassway.ShowDialog();
        }
        public void OpenMenuManager(object opws)
        {
            if (opws.ToString() == _shopinfofntity.Password)
            {
                FrmDishEntityManger menumanager = new FrmDishEntityManger(_shopinfofntity);
                menumanager.ShowDialog();
            }
            else
            {
                MessageBox.Show("商家编号错误！");
            }
        }
        //打印设置
        private void toolbtnPrintSet_Click(object sender, EventArgs e)
        {
            FrmPrintSet printset = new FrmPrintSet();
            printset.setnewipaddress += new FrmPrintSet.SetIpAddress(NewIpAddress);
            printset.ShowDialog();
        }
        private void NewIpAddress(object ipaddress)
        {
            this.Ipaddress = ipaddress.ToString();
        }
        #endregion

        private void tabManTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.tabManTab.SelectedIndex;
            for (int i = 0; i < this.tabManTab.TabCount; i++)
            {
                if (i == index)
                {
                    this.tabManTab.TabPages[i].ForeColor = Color.FromArgb(255, 187, 51);
                }
                else
                {
                    this.tabManTab.TabPages[i].ForeColor = Color.Black;
                }
            }
            switch (index)
            {
                //历史点单
                case 1:
                    this.palSelect.Location = new Point(100, 110);
                    this.panTime.Visible = true;
                    this.BillEntityOldData();
                    break;
                //团购信息
                case 2:
                    this.palSelect.Location = new Point(200, 110);
                    this.panTime.Visible = false;
                    this.LoadTuanInfo();
                    break;
                //历史团购
                case 3:
                    this.palSelect.Location = new Point(300, 110);
                    this.panTime.Visible = true;
                    this.TuanEntityOldData();
                    break;
                //新点单信息
                default:
                    this.palSelect.Location = new Point(2, 110);
                    this.panTime.Visible = false;
                    this.LoadBillInfo();
                    break;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

      
   
    }
}

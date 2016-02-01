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
using UniDllPCClient.WebserviceHelp;

namespace YaoDianCai_PC
{
    public partial class FrmDishEntityManger : CCSkinMain
    {
        //商家信息
        private ShopInfoEntity _shopinfofntity;
        private PDishEntityList _pdishentitylist;

        public FrmDishEntityManger()
        {
            InitializeComponent();
        }

        public FrmDishEntityManger(ShopInfoEntity shopinfofntity)
            : this()
        {
            this._shopinfofntity = shopinfofntity;
        }

        private void FrmDishEntityManger_Load(object sender, EventArgs e)
        {
            this.lstMenuInfo.Columns.Add("菜品编号", 145, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("菜品名称", 155, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("菜品分类", 120, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("菜品价格", 90, HorizontalAlignment.Center);
            this.lstMenuInfo.Columns.Add("菜品状态", 95, HorizontalAlignment.Center);
            this.cmbProperty.Items.Add("全部");
            _pdishentitylist = WebServiceHelp.GetAllDishInfo(_shopinfofntity.SID);
            for (int i = 0; i < _pdishentitylist.Dishentitylist.Count; i++)
            {
                this.cmbProperty.Items.Add(_pdishentitylist.Dishentitylist[i].Type);
            }
            this.cmbProperty.SelectedIndex = 0;
        }

        private void ListViewData(string type)
        {
            List<DishEntityList> listviewdata = new List<DishEntityList>();
            this.lstMenuInfo.Items.Clear();
            for (int i = 0; i < _pdishentitylist.Dishentitylist.Count; i++)
            {
                if (_pdishentitylist.Dishentitylist[i].Type == type)
                {
                    listviewdata.Add(_pdishentitylist.Dishentitylist[i]);
                }
            }
            this.ListViewData(listviewdata);
        }
        private void ListViewData(List<DishEntityList> listdata)
        {
            this.lstMenuInfo.Items.Clear();
            for (int i = 0; i < listdata.Count; i++)
            {
                for (int j = 0; j < listdata[i].Childmenu.Count; j++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = listdata[i].Childmenu[j].Number;
                    item.SubItems.Add(listdata[i].Childmenu[j].Name);
                    item.SubItems.Add(listdata[i].Type);
                    item.SubItems.Add(listdata[i].Childmenu[j].Price + "元");
                    switch (listdata[i].Childmenu[j].State)
                    {
                        // 0销售 1停售 2下架
                        case 0:
                            item.SubItems.Add("销售");
                            break;
                        case 1:
                            item.SubItems.Add("停售");
                            break;
                        case 2:
                            item.SubItems.Add("下架");
                            break;
                        default:
                            break;
                    }
                    this.lstMenuInfo.Items.Add(item);
                }
            }
        }
        private void ListViewData(string type, string condition)
        {
            List<DishEntityList> listviewdata = new List<DishEntityList>();
            this.lstMenuInfo.Items.Clear();
            for (int i = 0; i < _pdishentitylist.Dishentitylist.Count; i++)
            {
                if (_pdishentitylist.Dishentitylist[i].Type == type)
                {
                    for (int j = 0; j < _pdishentitylist.Dishentitylist[i].Childmenu.Count; j++)
                    {
                        bool nameflag = _pdishentitylist.Dishentitylist[i].Childmenu[j].Name.IndexOf(condition) > -1;
                        bool numflag = _pdishentitylist.Dishentitylist[i].Childmenu[j].Number.IndexOf(condition) > -1;
                        if (nameflag || numflag)
                        {
                            listviewdata.Add(_pdishentitylist.Dishentitylist[i]);
                        }
                    }
                }
            }
            this.ListViewData(listviewdata);
        }

        private void cmbProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string property = this.cmbProperty.Text;
            if (property == "全部")
            {
                this.ListViewData(_pdishentitylist.Dishentitylist);
            }
            else
            {
                this.ListViewData(property);
            }
        }

        private void lstMenuInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string menuname = this.lstMenuInfo.SelectedItems[0].SubItems[1].Text;
            for (int i = 0; i < _pdishentitylist.Dishentitylist.Count; i++)
            {
                for (int j = 0; j < _pdishentitylist.Dishentitylist[i].Childmenu.Count; j++)
                {
                    if (_pdishentitylist.Dishentitylist[i].Childmenu[j].Name == menuname)
                    {
                        FrmDishEntityInfo menuinfo = new FrmDishEntityInfo(_shopinfofntity.SID, _pdishentitylist.Dishentitylist[i].Childmenu[j]);
                        menuinfo.updatedishinfo += new FrmDishEntityInfo.UpdateDishinfo(UpdateDishEntityInfo);
                        menuinfo.ShowDialog();
                    }
                }
            }  
        }
        //菜单修改
        private void tsbEditMenu_Click(object sender, EventArgs e)
        {
            if (this.lstMenuInfo.SelectedItems.Count > 0)
            {
                string menuname = this.lstMenuInfo.SelectedItems[0].SubItems[1].Text;

                for (int i = 0; i < _pdishentitylist.Dishentitylist.Count; i++)
                {
                    for (int j = 0; j < _pdishentitylist.Dishentitylist[i].Childmenu.Count; j++)
                    {
                        if (_pdishentitylist.Dishentitylist[i].Childmenu[j].Name == menuname)
                        {
                            FrmDishEntityInfo menuinfo = new FrmDishEntityInfo(_shopinfofntity.SID, _pdishentitylist.Dishentitylist[i].Childmenu[j]);
                            menuinfo.updatedishinfo += new FrmDishEntityInfo.UpdateDishinfo(UpdateDishEntityInfo);
                            menuinfo.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的菜！");
            }
        }

        public void UpdateDishEntityInfo(object odishentity)
        {
            DishEntity dishentity = (DishEntity)odishentity;
            foreach (ListViewItem item in this.lstMenuInfo.Items)
            {
                if (item.SubItems[1].Text == dishentity.Name)
                {
                    item.SubItems[3].Text = dishentity.Price + "元";
                    switch (dishentity.State)
                    {
                        // 0销售 1停售 2下架
                        case 0:
                            item.SubItems[4].Text = "销售";
                            break;
                        case 1:
                            item.SubItems[4].Text = "停售";
                            break;
                        case 2:
                            item.SubItems[4].Text = "下架";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void FrmDishEntityManger_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

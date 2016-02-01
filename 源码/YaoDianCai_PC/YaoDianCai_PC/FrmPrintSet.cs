using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using UniDllPCClient.PrintHelp;
using UniDllPCClient.EntityHelp;
using System.Threading;

namespace YaoDianCai_PC
{
    public partial class FrmPrintSet : CCSkinMain
    {
        private string _shopname;
        public delegate void SetIpAddress(string ipaddress);

        public FrmPrintSet()
        {
            InitializeComponent();
        }
        public event SetIpAddress setnewipaddress;
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            string defauleop = this.txtDefaultIP.Text;
            string newip = this.txtNewIP.Text;
            if (!string.IsNullOrEmpty(newip))
            {
                setnewipaddress(newip);
            }
            else
            {
                setnewipaddress(defauleop);
            }
            this.Close();
        }
        private void btnTestPrint_Click(object sender, EventArgs e)
        {
            BillEntity billentity = new BillEntity();
            billentity.TableName = "餐桌号";
            billentity.CreateDate = DateTime.Now;
            billentity.Orders[0].DishName = "宫保鸡丁";
            billentity.Orders[0].DishCount = 99;
            billentity.Orders[0].DishPrice = "99元";
            billentity.Orders[1].DishName = "宫保鸡丁";
            billentity.Orders[1].DishCount = 99;
            billentity.Orders[1].DishPrice = "99元";
            billentity.Orders[2].DishName = "宫保鸡丁";
            billentity.Orders[2].DishCount = 99;
            billentity.Orders[2].DishPrice = "99元";
            billentity.TotalCount = 99;
            billentity.TotalPrice = "999元";
            PosPrintHelp.PrintBillEntity(billentity,"要点菜");
            TuanEntity tuanentity = new TuanEntity();
            tuanentity.TableName = "餐桌号";
            tuanentity.CreateDate = DateTime.Now;
            tuanentity.Website = "大众点评";
            tuanentity.Phone = "12345678900";
            tuanentity.Number = "0000000000";
            PosPrintHelp.PrintTuanEntity(tuanentity, "要点菜");
        }
        private void FrmPrintSet_Load(object sender, EventArgs e)
        {
            this.txtDefaultIP.Text = "192.168.0.112";
            //this.txtNewIP.Text = "192.168.0.112";
            string ordercontent = "";
            ordercontent += "               要点菜               " + "\r\n";
            ordercontent += "桌号:餐桌号                         " + "\r\n";
            ordercontent += "时间: 2014/12/12 12:12:12           " + "\r\n";
            ordercontent += "编号      菜品名    数量   金额     " + "\r\n";
            ordercontent += "------------------------------------" + "\r\n";
            ordercontent += "  1      宫保鸡丁    X99   99元     " + "\r\n" + "\r\n";
            ordercontent += "  2        莲藕      X99   99元     " + "\r\n" + "\r\n";
            ordercontent += "  3       叫化鸡     X99   99元     " + "\r\n";
            ordercontent += "------------------------------------" + "\r\n";
            ordercontent += "合计                 X99  999元     " + "\r\n";
            ordercontent += "------------------------------------" + "\r\n";
            ordercontent += "         要点菜无限点菜系统         " + "\r\n";
            this.txtTestOContent.Text = ordercontent;

            string groupbuycontent = "";
            groupbuycontent += "               要点菜               " + "\r\n";
            groupbuycontent += "团购单                              " + "\r\n";
            groupbuycontent += "------------------------------------" + "\r\n";
            groupbuycontent += "桌号: 餐桌号                        " + "\r\n" + "\r\n";
            groupbuycontent += "时间: 2014/12/12 00:00:00           " + "\r\n" + "\r\n";
            groupbuycontent += "团购商家:大众点评                   " + "\r\n" + "\r\n";  
            groupbuycontent += "客户电话:12345678900                " + "\r\n" + "\r\n"; 
            groupbuycontent += "团购编号:0000000000                 " + "\r\n" ; 
            groupbuycontent += "------------------------------------" + "\r\n";
            groupbuycontent += "         要点菜无限点菜系统         " + "\r\n";
            this.txtTestGBContent.Text = groupbuycontent;
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            string IPaddress = this.txtDefaultIP.Text;
            if (!string.IsNullOrEmpty(IPaddress.ToString()))
            {
                Connect(IPaddress);
            }
            else
            {
                MessageBox.Show("当前IP不能为空！");
            }
        }

        private void FrmPrintSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            string newip = this.txtNewIP.Text;
            if (!string.IsNullOrEmpty(newip))
            {
                setnewipaddress(newip);
            }
        }

        private void btnSetNewIP_Click(object sender, EventArgs e)
        {
            string newip = this.txtNewIP.Text;
            if (!string.IsNullOrEmpty(newip))
            {
                this.txtDefaultIP.Text = newip;
                //setnewipaddress(newip);

                Thread thread = new Thread(() => Connect(newip));
                thread.Start();
            }
        }

        public void Connect(string ipaddress)
        {
            if (PosPrintHelp.OpenPrint(ipaddress))
            {
                MessageBox.Show(ipaddress + " 链接成功！");
            }
            else
            {
                MessageBox.Show(ipaddress + " 链接失败！");
            }
        }
    }
}

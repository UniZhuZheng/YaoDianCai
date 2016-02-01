using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniDllPCClient.EntityHelp;
using CCWin;
using UniDllPCClient.WebserviceHelp;
using System.IO;

namespace YaoDianCai_PC
{
    public partial class FrmDishEntityInfo : CCSkinMain
    {
        private string _SID;
        private DishEntity _dishinfo;
        private DishEntity _alldishinfo;
        public delegate void UpdateDishinfo(DishEntity dishentity);

        public FrmDishEntityInfo()
        {
            InitializeComponent();
        }

        public FrmDishEntityInfo(string SID, DishEntity alldishinfo)
            : this()
        {
            this._SID = SID;
            this._alldishinfo = alldishinfo;
        }

        private void FrmDishEntityInfo_Load(object sender, EventArgs e)
        {
            _dishinfo = WebServiceHelp.GetDishInfo(_SID, _alldishinfo.Name);
            this.labMenuNum.Text = _alldishinfo.Number;
            this.labMenuName.Text = _alldishinfo.Name;
            this.labMenuProperty.Text = _dishinfo.Property;
            this.labMenuType.Text = _dishinfo.Type;
            this.labMenuPrice.Text = _alldishinfo.Price + "元";
            this.txtContent.Text = _dishinfo.Content;
            switch (_dishinfo.State)
            {
                // 0销售 1停售 2下架
                case 0:
                    this.labState.Text = "销售";
                    break;
                case 1:
                    this.labState.Text = "停售";
                    break;
                case 2:
                    this.labState.Text = "下架";
                    break;
                default:
                    break;
            }           
            this.picMenuImg.Image = ByteToImage(Convert.FromBase64String(_dishinfo.Imgcode));
        }

        public Bitmap ByteToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            Bitmap partImg = new Bitmap(this.picMenuImg.Width, this.picMenuImg.Height);
            Graphics graphics = Graphics.FromImage(partImg);
            graphics.DrawImage(image, 0, 0, this.picMenuImg.Width, this.picMenuImg.Height);
            return partImg;
        }

        public event UpdateDishinfo updatedishinfo;
        private void btnClose_Click(object sender, EventArgs e)
        {
            // 0销售 1停售 2下架
            _alldishinfo.Property = _dishinfo.Property;
            _alldishinfo.Type = _dishinfo.Type;
            _alldishinfo.Imgcode = _dishinfo.Imgcode;
            updatedishinfo(_alldishinfo);
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FrmDishEntityInfoEdit frmdishedit = new FrmDishEntityInfoEdit(_alldishinfo);
            frmdishedit.editdishinfo += new FrmDishEntityInfoEdit.EditDishinfo(EditDishInfo);
            frmdishedit.ShowDialog();
        }
        
        private void EditDishInfo(object alldishinfo)
        {
            DishEntity dishentity = (DishEntity)alldishinfo;
            bool flag = WebServiceHelp.UpdateDishInfo(_SID, dishentity.Name, dishentity.Price, dishentity.State + "");
            if (flag)
            {
                this.labMenuPrice.Text = dishentity.Price + "元";
                switch (dishentity.State)
                {
                    // 0销售 1停售 2下架
                    case 0:
                        this.labState.Text = "销售";
                        break;
                    case 1:
                        this.labState.Text = "停售";
                        break;
                    case 2:
                        this.labState.Text = "下架";
                        break;
                    default:
                        break;
                }           
            }
            else
            {
                MessageBox.Show("修改数据失败！");
            }
        }
    }
}

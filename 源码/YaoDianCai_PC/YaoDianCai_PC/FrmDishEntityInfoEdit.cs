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

namespace YaoDianCai_PC
{
    public partial class FrmDishEntityInfoEdit : CCSkinMain
    {
        private DishEntity _dishentity;
        public delegate void EditDishinfo(DishEntity dishentity);

        public FrmDishEntityInfoEdit()
        {
            InitializeComponent();
        }
        public FrmDishEntityInfoEdit(DishEntity dishentity):this()
        {
            this._dishentity = dishentity;
        }
        private void FrmDishEntityInfoEdit_Load(object sender, EventArgs e)
        {
            this.txtPrice.Text = _dishentity.Price;
            if (_dishentity.State==0)
            {
                this.rbtnState0.Checked = true;
            }
            if (_dishentity.State == 1)
            {
                this.rbtnState1.Checked = true;
            }
        }

        public event EditDishinfo editdishinfo;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.rbtnState0.Checked)
            {
                _dishentity.State = 0;
            }
            if (this.rbtnState1.Checked)
            {
                _dishentity.State = 1;
            }
            _dishentity.Price = this.txtPrice.Text;
            editdishinfo(_dishentity);
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

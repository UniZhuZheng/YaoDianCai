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
    public partial class FrmPassway : CCSkinMain
    {
        public delegate void PassWayPassWord(string password);

        public FrmPassway()
        {
            InitializeComponent();
        }

        public event PassWayPassWord passwaypassword;
        private void btnSure_Click(object sender, EventArgs e)
        {
            string password = this.txtPassword.Text;
            if (!string.IsNullOrEmpty(password))
            {
                passwaypassword(password);
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入商家密码！");
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string password = this.txtPassword.Text;
                if (!string.IsNullOrEmpty(password))
                {
                    passwaypassword(password);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请输入商家密码！");
                }
            }
        }
    }
}

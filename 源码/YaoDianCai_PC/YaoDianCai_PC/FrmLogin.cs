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
using UniDllPCClient.SystemHelp;
using System.Threading;
using UniDllPCClient.WebserviceHelp;

namespace YaoDianCai_PC
{
    public partial class FrmLogin : CCSkinMain
    {
        //主窗体
        private FrmMain _frmmain;
        //商家信息
        private ShopInfoEntity _shopinfofntity;


        public FrmLogin()
        {
            InitializeComponent();
            this.txtName.SkinTxt.Text ="kmd";
            this.txtPwd.SkinTxt.Text = "123456";
            FromHelp.eventSend += new SendHandler(ReceiveHandler);
        }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        void ReceiveHandler(object sender, object msg)
        {
            Type t = msg.GetType();
            if (t.IsEnum)
            {
                eControl e = (eControl)msg;
                switch (e)
                {
                    case eControl.Show_Main:
                        ShowFrmMain(sender as ShopInfoEntity);
                        break;
                    case eControl.Show_Tip:
                        ShowError();
                        break;
                }
            }
        }

        #region 登录

        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.txtName.SkinTxt.Text.Length == 0 || this.txtPwd.SkinTxt.Text.Length == 0)
            {
                MessageBox.Show("用户名密码不能为空！");
                return;
            }

            this.imgLoadding.Visible = this.labLogin.Visible = this.txtName.SkinTxt.ReadOnly = this.txtPwd.SkinTxt.ReadOnly = true;
            this.btnLogin.Enabled = false;
            string sname = this.txtName.SkinTxt.Text.ToString();
            string spassword = this.txtPwd.SkinTxt.Text.ToString();
            Thread thread = new Thread(() => Login(sname, spassword));
            thread.Start();
        }
        private void Login(string name, string password)
        {
            try
            {
                _shopinfofntity = WebServiceHelp.ShopLogin(name, password);
                WebServiceVisit.Domain = _shopinfofntity.Domain;
                if (_shopinfofntity.Ok)
                {
                    _shopinfofntity.Password = password;
                    FromHelp.SendMessage(_shopinfofntity, eControl.Show_Main);
                }
                else
                {
                    FromHelp.SendMessage(_shopinfofntity, eControl.Show_Tip);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        delegate void ShowFrmMainEventHandler(ShopInfoEntity shopinfo);
        private void ShowFrmMain(ShopInfoEntity shopinfo)
        {
            if (this.InvokeRequired)
            {
                ShowFrmMainEventHandler sfm = new ShowFrmMainEventHandler(ShowFrmMain);
                this.Invoke(sfm, new object[] { shopinfo });
            }
            else
            {
                this.Hide();
                _frmmain = new FrmMain(_shopinfofntity);
                _frmmain.Show();
            }
        }
        delegate void ShowErrorEventHandler();
        private void ShowError()
        {
            if (this.InvokeRequired)
            {
                ShowErrorEventHandler se = new ShowErrorEventHandler(ShowError);
                this.Invoke(se);
            }
            else
            {
                this.imgLoadding.Visible = this.labLogin.Visible = this.txtName.SkinTxt.ReadOnly = this.txtPwd.SkinTxt.ReadOnly = false;
                this.btnLogin.Enabled = true;
                this.labError.Visible = true;
            }
        }

        #endregion

        #region 托盘图标
        //显示窗体
        private void tsmiMainWindow_Click(object sender, EventArgs e)
        {
            if (_frmmain != null)
            {
                _frmmain.Show();
            }
            else
            {
                this.Show();
            }
        }
        //退出
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }
        //双击图标
        private void noIcTaskIcon_DoubleClick(object sender, EventArgs e)
        {
            if (_frmmain != null)
            {
                _frmmain.Show();
            }
            else
            {
                this.Show();
            }
        }
        #endregion

        private void txtName_SkinTxt_KeyDown(object sender, KeyEventArgs e)
        {
            this.labError.Visible = false;
        }
        private void txtPwd_SkinTxt_KeyDown(object sender, KeyEventArgs e)
        {
            this.labError.Visible = false;
        }
        private void txtPwd_SkinTxt_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtName.SkinTxt.Text.Length == 0 || this.txtPwd.SkinTxt.Text.Length == 0)
                {
                    MessageBox.Show("用户名密码不能为空！");
                    return;
                }

                this.imgLoadding.Visible = this.labLogin.Visible = this.txtName.SkinTxt.ReadOnly = this.txtPwd.SkinTxt.ReadOnly = true;
                this.btnLogin.Enabled = false;
                string sname = this.txtName.SkinTxt.Text.ToString();
                string spassword = this.txtPwd.SkinTxt.Text.ToString();
                Thread thread = new Thread(() => Login(sname, spassword));
                thread.Start();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}

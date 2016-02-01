namespace YaoDianCai_PC
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.labLogin = new System.Windows.Forms.Label();
            this.imgLoadding = new System.Windows.Forms.PictureBox();
            this.labError = new System.Windows.Forms.Label();
            this.skinPictureBox1 = new CCWin.SkinControl.SkinPictureBox();
            this.btnLogin = new CCWin.SkinControl.SkinButton();
            this.txtPwd = new CCWin.SkinControl.SkinTextBox();
            this.txtName = new CCWin.SkinControl.SkinTextBox();
            this.scmsRightClick = new CCWin.SkinControl.SkinContextMenuStrip();
            this.tsmiMainWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.noIcTaskIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgLoadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).BeginInit();
            this.txtPwd.SuspendLayout();
            this.txtName.SuspendLayout();
            this.scmsRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // labLogin
            // 
            this.labLogin.AutoSize = true;
            this.labLogin.BackColor = System.Drawing.Color.Transparent;
            this.labLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLogin.ForeColor = System.Drawing.Color.Black;
            this.labLogin.Location = new System.Drawing.Point(145, 366);
            this.labLogin.Name = "labLogin";
            this.labLogin.Size = new System.Drawing.Size(109, 22);
            this.labLogin.TabIndex = 51;
            this.labLogin.Text = "正在登录 ......";
            this.labLogin.Visible = false;
            // 
            // imgLoadding
            // 
            this.imgLoadding.Image = ((System.Drawing.Image)(resources.GetObject("imgLoadding.Image")));
            this.imgLoadding.Location = new System.Drawing.Point(20, 356);
            this.imgLoadding.Margin = new System.Windows.Forms.Padding(0);
            this.imgLoadding.Name = "imgLoadding";
            this.imgLoadding.Size = new System.Drawing.Size(360, 2);
            this.imgLoadding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLoadding.TabIndex = 50;
            this.imgLoadding.TabStop = false;
            this.imgLoadding.Visible = false;
            // 
            // labError
            // 
            this.labError.AutoSize = true;
            this.labError.BackColor = System.Drawing.Color.Transparent;
            this.labError.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labError.ForeColor = System.Drawing.Color.Red;
            this.labError.Location = new System.Drawing.Point(126, 366);
            this.labError.Name = "labError";
            this.labError.Size = new System.Drawing.Size(154, 22);
            this.labError.TabIndex = 49;
            this.labError.Text = "账户名或密码错误！";
            this.labError.Visible = false;
            // 
            // skinPictureBox1
            // 
            this.skinPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinPictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinPictureBox1.Image = global::YaoDianCai_PC.Properties.Resources.login_logo;
            this.skinPictureBox1.Location = new System.Drawing.Point(39, 105);
            this.skinPictureBox1.Name = "skinPictureBox1";
            this.skinPictureBox1.Size = new System.Drawing.Size(323, 52);
            this.skinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.skinPictureBox1.TabIndex = 48;
            this.skinPictureBox1.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogin.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnLogin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnLogin.DownBack = ((System.Drawing.Image)(resources.GetObject("btnLogin.DownBack")));
            this.btnLogin.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnLogin.Location = new System.Drawing.Point(84, 550);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnLogin.MouseBack")));
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnLogin.NormlBack")));
            this.btnLogin.Palace = true;
            this.btnLogin.Size = new System.Drawing.Size(235, 45);
            this.btnLogin.TabIndex = 47;
            this.btnLogin.Text = "登       陆";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.Transparent;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPwd.Icon = null;
            this.txtPwd.IconIsButton = true;
            this.txtPwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPwd.Location = new System.Drawing.Point(84, 317);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(0);
            this.txtPwd.MinimumSize = new System.Drawing.Size(0, 28);
            this.txtPwd.MouseBack = null;
            this.txtPwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.NormlBack = null;
            this.txtPwd.Padding = new System.Windows.Forms.Padding(5, 5, 28, 5);
            this.txtPwd.Size = new System.Drawing.Size(235, 28);
            // 
            // txtPwd.BaseText
            // 
            this.txtPwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtPwd.SkinTxt.Name = "BaseText";
            this.txtPwd.SkinTxt.PasswordChar = '●';
            this.txtPwd.SkinTxt.Size = new System.Drawing.Size(202, 18);
            this.txtPwd.SkinTxt.TabIndex = 0;
            this.txtPwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPwd.SkinTxt.WaterText = "密码";
            this.txtPwd.SkinTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_SkinTxt_KeyDown_1);
            this.txtPwd.TabIndex = 46;
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_SkinTxt_KeyDown);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Transparent;
            this.txtName.Icon = null;
            this.txtName.IconIsButton = false;
            this.txtName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtName.Location = new System.Drawing.Point(84, 280);
            this.txtName.Margin = new System.Windows.Forms.Padding(0);
            this.txtName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtName.MouseBack = null;
            this.txtName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtName.Name = "txtName";
            this.txtName.NormlBack = null;
            this.txtName.Padding = new System.Windows.Forms.Padding(5, 5, 28, 5);
            this.txtName.Size = new System.Drawing.Size(235, 28);
            // 
            // txtName.BaseText
            // 
            this.txtName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtName.SkinTxt.Name = "BaseText";
            this.txtName.SkinTxt.Size = new System.Drawing.Size(202, 18);
            this.txtName.SkinTxt.TabIndex = 0;
            this.txtName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtName.SkinTxt.WaterText = "用户名";
            this.txtName.TabIndex = 45;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_SkinTxt_KeyDown);
            // 
            // scmsRightClick
            // 
            this.scmsRightClick.Arrow = System.Drawing.Color.Black;
            this.scmsRightClick.Back = System.Drawing.Color.White;
            this.scmsRightClick.BackRadius = 4;
            this.scmsRightClick.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.scmsRightClick.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.scmsRightClick.Fore = System.Drawing.Color.Black;
            this.scmsRightClick.HoverFore = System.Drawing.Color.White;
            this.scmsRightClick.ItemAnamorphosis = true;
            this.scmsRightClick.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.scmsRightClick.ItemBorderShow = true;
            this.scmsRightClick.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.scmsRightClick.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.scmsRightClick.ItemRadius = 4;
            this.scmsRightClick.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.scmsRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMainWindow,
            this.tsmiExit});
            this.scmsRightClick.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.scmsRightClick.Name = "skinContextMenuStrip1";
            this.scmsRightClick.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.scmsRightClick.Size = new System.Drawing.Size(125, 48);
            this.scmsRightClick.SkinAllColor = true;
            this.scmsRightClick.TitleAnamorphosis = true;
            this.scmsRightClick.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.scmsRightClick.TitleRadius = 4;
            this.scmsRightClick.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // tsmiMainWindow
            // 
            this.tsmiMainWindow.Name = "tsmiMainWindow";
            this.tsmiMainWindow.Size = new System.Drawing.Size(124, 22);
            this.tsmiMainWindow.Text = "显示面板";
            this.tsmiMainWindow.Click += new System.EventHandler(this.tsmiMainWindow_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(124, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // noIcTaskIcon
            // 
            this.noIcTaskIcon.ContextMenuStrip = this.scmsRightClick;
            this.noIcTaskIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("noIcTaskIcon.Icon")));
            this.noIcTaskIcon.Visible = true;
            this.noIcTaskIcon.DoubleClick += new System.EventHandler(this.noIcTaskIcon_DoubleClick);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.login_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackPalace = global::YaoDianCai_PC.Properties.Resources.BackPalace;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(400, 700);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(1, -1);
            this.Controls.Add(this.labLogin);
            this.Controls.Add(this.imgLoadding);
            this.Controls.Add(this.labError);
            this.Controls.Add(this.skinPictureBox1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MiniDownBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_normal;
            this.MiniSize = new System.Drawing.Size(28, 20);
            this.Name = "FrmLogin";
            this.ShowBorder = false;
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogin_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.imgLoadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).EndInit();
            this.txtPwd.ResumeLayout(false);
            this.txtPwd.PerformLayout();
            this.txtName.ResumeLayout(false);
            this.txtName.PerformLayout();
            this.scmsRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labLogin;
        private System.Windows.Forms.PictureBox imgLoadding;
        private System.Windows.Forms.Label labError;
        private CCWin.SkinControl.SkinPictureBox skinPictureBox1;
        private CCWin.SkinControl.SkinButton btnLogin;
        private CCWin.SkinControl.SkinTextBox txtPwd;
        private CCWin.SkinControl.SkinTextBox txtName;
        private CCWin.SkinControl.SkinContextMenuStrip scmsRightClick;
        private System.Windows.Forms.ToolStripMenuItem tsmiMainWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.NotifyIcon noIcTaskIcon;
    }
}
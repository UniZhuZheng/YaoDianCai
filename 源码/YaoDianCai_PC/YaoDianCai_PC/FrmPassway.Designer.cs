namespace YaoDianCai_PC
{
    partial class FrmPassway
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPassway));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSure = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(36, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 61;
            this.label1.Text = "请输入商家密码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txtPassword.Location = new System.Drawing.Point(39, 85);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(196, 27);
            this.txtPassword.TabIndex = 59;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnSure
            // 
            this.btnSure.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSure.BackColor = System.Drawing.Color.Transparent;
            this.btnSure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSure.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnSure.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnSure.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSure.DownBack = ((System.Drawing.Image)(resources.GetObject("btnSure.DownBack")));
            this.btnSure.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnSure.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnSure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnSure.Location = new System.Drawing.Point(171, 136);
            this.btnSure.Margin = new System.Windows.Forms.Padding(0);
            this.btnSure.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnSure.MouseBack")));
            this.btnSure.Name = "btnSure";
            this.btnSure.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnSure.NormlBack")));
            this.btnSure.Palace = true;
            this.btnSure.Size = new System.Drawing.Size(98, 35);
            this.btnSure.TabIndex = 153;
            this.btnSure.Text = "确   定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // FrmPassway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.menu_bg;
            this.BackPalace = global::YaoDianCai_PC.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(1, -1);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPassway";
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "菜单管理";
            this.TitleOffset = new System.Drawing.Point(5, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private CCWin.SkinControl.SkinButton btnSure;
    }
}
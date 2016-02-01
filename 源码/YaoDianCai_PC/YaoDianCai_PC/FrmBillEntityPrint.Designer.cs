namespace YaoDianCai_PC
{
    partial class FrmBillEntityPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBillEntityPrint));
            this.lstMenuInfo = new YDCControl.YDCListViews();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTime = new System.Windows.Forms.Label();
            this.labTableName = new System.Windows.Forms.Label();
            this.labTotalPrice = new System.Windows.Forms.Label();
            this.labTotalOrderNum = new System.Windows.Forms.Label();
            this.btnPrint = new CCWin.SkinControl.SkinButton();
            this.btnManage = new CCWin.SkinControl.SkinButton();
            this.btnClose = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // lstMenuInfo
            // 
            this.lstMenuInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstMenuInfo.BorderColor = System.Drawing.Color.Transparent;
            this.lstMenuInfo.DrawBackColor = false;
            this.lstMenuInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lstMenuInfo.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstMenuInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstMenuInfo.HideSelection = false;
            this.lstMenuInfo.Location = new System.Drawing.Point(24, 86);
            this.lstMenuInfo.Name = "lstMenuInfo";
            this.lstMenuInfo.OwnerDraw = true;
            this.lstMenuInfo.RowBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstMenuInfo.RowBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstMenuInfo.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstMenuInfo.Size = new System.Drawing.Size(480, 262);
            this.lstMenuInfo.TabIndex = 87;
            this.lstMenuInfo.UseCompatibleStateImageBehavior = false;
            this.lstMenuInfo.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(32, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 86;
            this.label1.Text = "总计：";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(25, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 2);
            this.panel2.TabIndex = 85;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(25, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 2);
            this.panel1.TabIndex = 84;
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.BackColor = System.Drawing.Color.Transparent;
            this.labTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.labTime.Location = new System.Drawing.Point(330, 55);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(175, 20);
            this.labTime.TabIndex = 83;
            this.labTime.Text = "2012年01月01日12时12分";
            // 
            // labTableName
            // 
            this.labTableName.AutoSize = true;
            this.labTableName.BackColor = System.Drawing.Color.Transparent;
            this.labTableName.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTableName.Location = new System.Drawing.Point(31, 44);
            this.labTableName.Name = "labTableName";
            this.labTableName.Size = new System.Drawing.Size(62, 31);
            this.labTableName.TabIndex = 82;
            this.labTableName.Text = "桌号";
            // 
            // labTotalPrice
            // 
            this.labTotalPrice.AutoSize = true;
            this.labTotalPrice.BackColor = System.Drawing.Color.Transparent;
            this.labTotalPrice.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.labTotalPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.labTotalPrice.Location = new System.Drawing.Point(402, 358);
            this.labTotalPrice.Name = "labTotalPrice";
            this.labTotalPrice.Size = new System.Drawing.Size(68, 27);
            this.labTotalPrice.TabIndex = 78;
            this.labTotalPrice.Text = "400元";
            // 
            // labTotalOrderNum
            // 
            this.labTotalOrderNum.AutoSize = true;
            this.labTotalOrderNum.BackColor = System.Drawing.Color.Transparent;
            this.labTotalOrderNum.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.labTotalOrderNum.Location = new System.Drawing.Point(324, 360);
            this.labTotalOrderNum.Name = "labTotalOrderNum";
            this.labTotalOrderNum.Size = new System.Drawing.Size(50, 24);
            this.labTotalOrderNum.TabIndex = 79;
            this.labTotalOrderNum.Text = "20份";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrint.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnPrint.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnPrint.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnPrint.DownBack = ((System.Drawing.Image)(resources.GetObject("btnPrint.DownBack")));
            this.btnPrint.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnPrint.Location = new System.Drawing.Point(407, 402);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnPrint.MouseBack")));
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnPrint.NormlBack")));
            this.btnPrint.Palace = true;
            this.btnPrint.Size = new System.Drawing.Size(98, 35);
            this.btnPrint.TabIndex = 88;
            this.btnPrint.Text = "打   印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnManage
            // 
            this.btnManage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnManage.BackColor = System.Drawing.Color.Transparent;
            this.btnManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnManage.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnManage.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnManage.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnManage.DownBack = ((System.Drawing.Image)(resources.GetObject("btnManage.DownBack")));
            this.btnManage.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnManage.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnManage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnManage.Location = new System.Drawing.Point(297, 402);
            this.btnManage.Margin = new System.Windows.Forms.Padding(0);
            this.btnManage.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnManage.MouseBack")));
            this.btnManage.Name = "btnManage";
            this.btnManage.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnManage.NormlBack")));
            this.btnManage.Palace = true;
            this.btnManage.Size = new System.Drawing.Size(98, 35);
            this.btnManage.TabIndex = 89;
            this.btnManage.Text = "处   理";
            this.btnManage.UseVisualStyleBackColor = false;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnClose.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnClose.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClose.DownBack = ((System.Drawing.Image)(resources.GetObject("btnClose.DownBack")));
            this.btnClose.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnClose.Location = new System.Drawing.Point(297, 402);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnClose.MouseBack")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnClose.NormlBack")));
            this.btnClose.Palace = true;
            this.btnClose.Size = new System.Drawing.Size(98, 35);
            this.btnClose.TabIndex = 90;
            this.btnClose.Text = "关   闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmBillEntityPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.menu_bg;
            this.BorderPalace = global::YaoDianCai_PC.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(528, 462);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(0, -1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lstMenuInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labTime);
            this.Controls.Add(this.labTableName);
            this.Controls.Add(this.labTotalPrice);
            this.Controls.Add(this.labTotalOrderNum);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(528, 462);
            this.MaxSize = new System.Drawing.Size(28, 20);
            this.MiniDownBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_highlight;
            this.MinimumSize = new System.Drawing.Size(528, 462);
            this.MiniNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_normal;
            this.MiniSize = new System.Drawing.Size(28, 20);
            this.Name = "FrmBillEntityPrint";
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "要点菜商家管理平台";
            this.TitleOffset = new System.Drawing.Point(5, 0);
            this.Load += new System.EventHandler(this.FrmBillEntityNew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private YDCControl.YDCListViews lstMenuInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label labTableName;
        private System.Windows.Forms.Label labTotalPrice;
        private System.Windows.Forms.Label labTotalOrderNum;
        private CCWin.SkinControl.SkinButton btnPrint;
        private CCWin.SkinControl.SkinButton btnManage;
        private CCWin.SkinControl.SkinButton btnClose;
    }
}
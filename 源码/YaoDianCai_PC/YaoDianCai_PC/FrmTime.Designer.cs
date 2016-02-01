namespace YaoDianCai_PC
{
    partial class FrmTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTime));
            this.mcdTime = new System.Windows.Forms.MonthCalendar();
            this.btnSure = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // mcdTime
            // 
            this.mcdTime.Location = new System.Drawing.Point(35, 56);
            this.mcdTime.Name = "mcdTime";
            this.mcdTime.TabIndex = 60;
            this.mcdTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mcdTime_MouseDown);
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
            this.btnSure.Location = new System.Drawing.Point(158, 245);
            this.btnSure.Margin = new System.Windows.Forms.Padding(0);
            this.btnSure.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnSure.MouseBack")));
            this.btnSure.Name = "btnSure";
            this.btnSure.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnSure.NormlBack")));
            this.btnSure.Palace = true;
            this.btnSure.Size = new System.Drawing.Size(97, 35);
            this.btnSure.TabIndex = 79;
            this.btnSure.Text = "确   定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // FrmTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.menu_bg;
            this.BackPalace = global::YaoDianCai_PC.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(290, 300);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(1, -1);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.mcdTime);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(290, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(290, 300);
            this.Name = "FrmTime";
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查询时间";
            this.TitleOffset = new System.Drawing.Point(5, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mcdTime;
        private CCWin.SkinControl.SkinButton btnSure;

    }
}
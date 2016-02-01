namespace YaoDianCai_PC
{
    partial class FrmTuanNotifier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTuanNotifier));
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.labRemMsg = new CCWin.SkinControl.SkinLabel();
            this.btnPrint = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel1.Location = new System.Drawing.Point(32, 50);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(74, 21);
            this.skinLabel1.TabIndex = 52;
            this.skinLabel1.Text = "新团购：";
            // 
            // labRemMsg
            // 
            this.labRemMsg.AutoSize = true;
            this.labRemMsg.BackColor = System.Drawing.Color.Transparent;
            this.labRemMsg.BorderColor = System.Drawing.Color.White;
            this.labRemMsg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labRemMsg.Location = new System.Drawing.Point(32, 86);
            this.labRemMsg.Name = "labRemMsg";
            this.labRemMsg.Size = new System.Drawing.Size(91, 21);
            this.labRemMsg.TabIndex = 53;
            this.labRemMsg.Text = "123456789";
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
            this.btnPrint.Location = new System.Drawing.Point(181, 144);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnPrint.MouseBack")));
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnPrint.NormlBack")));
            this.btnPrint.Palace = true;
            this.btnPrint.Size = new System.Drawing.Size(98, 35);
            this.btnPrint.TabIndex = 79;
            this.btnPrint.Text = "打   印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmTuanNotifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.menu_bg;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(1, -1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.labRemMsg);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FrmTuanNotifier";
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "新团购消息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTuanNotifier_FormClosing);
            this.Load += new System.EventHandler(this.FrmTuanNotifier_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel labRemMsg;
        private CCWin.SkinControl.SkinButton btnPrint;
    }
}
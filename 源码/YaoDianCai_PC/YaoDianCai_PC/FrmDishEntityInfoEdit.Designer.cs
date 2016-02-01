namespace YaoDianCai_PC
{
    partial class FrmDishEntityInfoEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDishEntityInfoEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnState1 = new System.Windows.Forms.RadioButton();
            this.rbtnState0 = new System.Windows.Forms.RadioButton();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnSave = new CCWin.SkinControl.SkinButton();
            this.btnCancle = new CCWin.SkinControl.SkinButton();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label1.Location = new System.Drawing.Point(31, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "价格";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label2.Location = new System.Drawing.Point(31, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "状态";
            // 
            // rbtnState1
            // 
            this.rbtnState1.AutoSize = true;
            this.rbtnState1.BackColor = System.Drawing.Color.Transparent;
            this.rbtnState1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnState1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.rbtnState1.Location = new System.Drawing.Point(173, 50);
            this.rbtnState1.Name = "rbtnState1";
            this.rbtnState1.Size = new System.Drawing.Size(50, 21);
            this.rbtnState1.TabIndex = 147;
            this.rbtnState1.TabStop = true;
            this.rbtnState1.Text = "停售";
            this.rbtnState1.UseVisualStyleBackColor = false;
            // 
            // rbtnState0
            // 
            this.rbtnState0.AutoSize = true;
            this.rbtnState0.BackColor = System.Drawing.Color.Transparent;
            this.rbtnState0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnState0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.rbtnState0.Location = new System.Drawing.Point(103, 50);
            this.rbtnState0.Name = "rbtnState0";
            this.rbtnState0.Size = new System.Drawing.Size(50, 21);
            this.rbtnState0.TabIndex = 146;
            this.rbtnState0.TabStop = true;
            this.rbtnState0.Text = "销售";
            this.rbtnState0.UseVisualStyleBackColor = false;
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtPrice.Location = new System.Drawing.Point(103, 93);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(84, 29);
            this.txtPrice.TabIndex = 148;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnSave.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnSave.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSave.DownBack = ((System.Drawing.Image)(resources.GetObject("btnSave.DownBack")));
            this.btnSave.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnSave.Location = new System.Drawing.Point(35, 151);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnSave.MouseBack")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnSave.NormlBack")));
            this.btnSave.Palace = true;
            this.btnSave.Size = new System.Drawing.Size(98, 35);
            this.btnSave.TabIndex = 151;
            this.btnSave.Text = "保   存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancle.BackColor = System.Drawing.Color.Transparent;
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancle.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnCancle.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnCancle.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnCancle.DownBack = ((System.Drawing.Image)(resources.GetObject("btnCancle.DownBack")));
            this.btnCancle.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCancle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.btnCancle.Location = new System.Drawing.Point(153, 151);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancle.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnCancle.MouseBack")));
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnCancle.NormlBack")));
            this.btnCancle.Palace = true;
            this.btnCancle.Size = new System.Drawing.Size(98, 35);
            this.btnCancle.TabIndex = 152;
            this.btnCancle.Text = "取   消";
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label3.Location = new System.Drawing.Point(189, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 21);
            this.label3.TabIndex = 153;
            this.label3.Text = "元";
            // 
            // FrmDishEntityInfoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.menu_bg;
            this.ClientSize = new System.Drawing.Size(285, 213);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(1, -1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.rbtnState1);
            this.Controls.Add(this.rbtnState0);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(285, 213);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(285, 213);
            this.Name = "FrmDishEntityInfoEdit";
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmDishEntityInfoEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnState1;
        private System.Windows.Forms.RadioButton rbtnState0;
        private System.Windows.Forms.TextBox txtPrice;
        private CCWin.SkinControl.SkinButton btnSave;
        private CCWin.SkinControl.SkinButton btnCancle;
        private System.Windows.Forms.Label label3;
    }
}
namespace YaoDianCai_PC
{
    partial class FrmMain
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
            CCWin.SkinControl.Animation animation13 = new CCWin.SkinControl.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tabManTab = new CCWin.SkinControl.SkinTabControl();
            this.tabNewOrderInfo = new System.Windows.Forms.TabPage();
            this.imgListViewHtight = new System.Windows.Forms.ImageList(this.components);
            this.tabOldOrderInfo = new System.Windows.Forms.TabPage();
            this.tabGroupBuyInfo = new System.Windows.Forms.TabPage();
            this.tabOldGroupBuyInfo = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblName = new CCWin.SkinControl.SkinLabel();
            this.sktsysbtn = new CCWin.SkinControl.SkinToolStrip();
            this.toolbtnMenuManger = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrintSet = new System.Windows.Forms.ToolStripButton();
            this.panTime = new System.Windows.Forms.Panel();
            this.labTime = new System.Windows.Forms.Label();
            this.skttime = new CCWin.SkinControl.SkinToolStrip();
            this.toolbtnTime = new System.Windows.Forms.ToolStripButton();
            this.ptbNeworderMsg = new System.Windows.Forms.PictureBox();
            this.ptbNewGroupBuyMsg = new System.Windows.Forms.PictureBox();
            this.timNoIc = new System.Windows.Forms.Timer(this.components);
            this.lstNewOrder = new YDCControl.YDCListViews();
            this.lstOldOrder = new YDCControl.YDCListViews();
            this.lstNewGroupBuy = new YDCControl.YDCListViews();
            this.lstOldGroupBuy = new YDCControl.YDCListViews();
            this.palSelect = new System.Windows.Forms.Panel();
            this.tabManTab.SuspendLayout();
            this.tabNewOrderInfo.SuspendLayout();
            this.tabOldOrderInfo.SuspendLayout();
            this.tabGroupBuyInfo.SuspendLayout();
            this.tabOldGroupBuyInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.sktsysbtn.SuspendLayout();
            this.panTime.SuspendLayout();
            this.skttime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbNeworderMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbNewGroupBuyMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // tabManTab
            // 
            this.tabManTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            animation13.AnimateOnlyDifferences = false;
            animation13.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation13.BlindCoeff")));
            animation13.LeafCoeff = 0F;
            animation13.MaxTime = 1F;
            animation13.MinTime = 0F;
            animation13.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation13.MosaicCoeff")));
            animation13.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation13.MosaicShift")));
            animation13.MosaicSize = 0;
            animation13.Padding = new System.Windows.Forms.Padding(0);
            animation13.RotateCoeff = 0F;
            animation13.RotateLimit = 0F;
            animation13.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation13.ScaleCoeff")));
            animation13.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation13.SlideCoeff")));
            animation13.TimeCoeff = 2F;
            animation13.TransparencyCoeff = 0F;
            this.tabManTab.Animation = animation13;
            this.tabManTab.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabManTab.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabManTab.Controls.Add(this.tabNewOrderInfo);
            this.tabManTab.Controls.Add(this.tabOldOrderInfo);
            this.tabManTab.Controls.Add(this.tabGroupBuyInfo);
            this.tabManTab.Controls.Add(this.tabOldGroupBuyInfo);
            this.tabManTab.ItemSize = new System.Drawing.Size(98, 36);
            this.tabManTab.ItemStretch = true;
            this.tabManTab.Location = new System.Drawing.Point(1, 78);
            this.tabManTab.Name = "tabManTab";
            this.tabManTab.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabManTab.PageArrowDown")));
            this.tabManTab.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabManTab.PageArrowHover")));
            this.tabManTab.PageArrowVisble = false;
            this.tabManTab.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabManTab.PageCloseHover")));
            this.tabManTab.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabManTab.PageCloseNormal")));
            this.tabManTab.PageDown = ((System.Drawing.Image)(resources.GetObject("tabManTab.PageDown")));
            this.tabManTab.PageHover = ((System.Drawing.Image)(resources.GetObject("tabManTab.PageHover")));
            this.tabManTab.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.tabManTab.PageNorml = null;
            this.tabManTab.SelectedIndex = 0;
            this.tabManTab.Size = new System.Drawing.Size(398, 590);
            this.tabManTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabManTab.TabIndex = 131;
            this.tabManTab.SelectedIndexChanged += new System.EventHandler(this.tabManTab_SelectedIndexChanged);
            // 
            // tabNewOrderInfo
            // 
            this.tabNewOrderInfo.BackColor = System.Drawing.Color.Gray;
            this.tabNewOrderInfo.Controls.Add(this.lstNewOrder);
            this.tabNewOrderInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabNewOrderInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.tabNewOrderInfo.Location = new System.Drawing.Point(0, 36);
            this.tabNewOrderInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tabNewOrderInfo.Name = "tabNewOrderInfo";
            this.tabNewOrderInfo.Size = new System.Drawing.Size(398, 554);
            this.tabNewOrderInfo.TabIndex = 1;
            this.tabNewOrderInfo.Text = "新点单";
            // 
            // imgListViewHtight
            // 
            this.imgListViewHtight.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgListViewHtight.ImageSize = new System.Drawing.Size(16, 45);
            this.imgListViewHtight.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabOldOrderInfo
            // 
            this.tabOldOrderInfo.Controls.Add(this.lstOldOrder);
            this.tabOldOrderInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabOldOrderInfo.Location = new System.Drawing.Point(0, 36);
            this.tabOldOrderInfo.Name = "tabOldOrderInfo";
            this.tabOldOrderInfo.Size = new System.Drawing.Size(398, 554);
            this.tabOldOrderInfo.TabIndex = 2;
            this.tabOldOrderInfo.Text = "历史点单";
            this.tabOldOrderInfo.UseVisualStyleBackColor = true;
            // 
            // tabGroupBuyInfo
            // 
            this.tabGroupBuyInfo.Controls.Add(this.lstNewGroupBuy);
            this.tabGroupBuyInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabGroupBuyInfo.Location = new System.Drawing.Point(0, 36);
            this.tabGroupBuyInfo.Name = "tabGroupBuyInfo";
            this.tabGroupBuyInfo.Size = new System.Drawing.Size(398, 554);
            this.tabGroupBuyInfo.TabIndex = 3;
            this.tabGroupBuyInfo.Text = "团购";
            this.tabGroupBuyInfo.UseVisualStyleBackColor = true;
            // 
            // tabOldGroupBuyInfo
            // 
            this.tabOldGroupBuyInfo.Controls.Add(this.lstOldGroupBuy);
            this.tabOldGroupBuyInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabOldGroupBuyInfo.Location = new System.Drawing.Point(0, 36);
            this.tabOldGroupBuyInfo.Name = "tabOldGroupBuyInfo";
            this.tabOldGroupBuyInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabOldGroupBuyInfo.Size = new System.Drawing.Size(398, 554);
            this.tabOldGroupBuyInfo.TabIndex = 4;
            this.tabOldGroupBuyInfo.Text = "历史团购";
            this.tabOldGroupBuyInfo.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::YaoDianCai_PC.Properties.Resources.商家;
            this.pictureBox1.Location = new System.Drawing.Point(35, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 22);
            this.pictureBox1.TabIndex = 133;
            this.pictureBox1.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.BorderColor = System.Drawing.Color.White;
            this.lblName.BorderSize = 4;
            this.lblName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(213)))), ((int)(((byte)(45)))));
            this.lblName.Location = new System.Drawing.Point(70, 36);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(107, 31);
            this.lblName.TabIndex = 132;
            this.lblName.Text = "XXX餐馆";
            // 
            // sktsysbtn
            // 
            this.sktsysbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sktsysbtn.Arrow = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sktsysbtn.AutoSize = false;
            this.sktsysbtn.Back = System.Drawing.Color.White;
            this.sktsysbtn.BackColor = System.Drawing.Color.Transparent;
            this.sktsysbtn.BackRadius = 4;
            this.sktsysbtn.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.sktsysbtn.Base = System.Drawing.Color.Transparent;
            this.sktsysbtn.BaseFore = System.Drawing.Color.Black;
            this.sktsysbtn.BaseForeAnamorphosis = true;
            this.sktsysbtn.BaseForeAnamorphosisBorder = 4;
            this.sktsysbtn.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.sktsysbtn.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.sktsysbtn.BaseHoverFore = System.Drawing.Color.Black;
            this.sktsysbtn.BaseItemAnamorphosis = true;
            this.sktsysbtn.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.sktsysbtn.BaseItemBorderShow = true;
            this.sktsysbtn.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("sktsysbtn.BaseItemDown")));
            this.sktsysbtn.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sktsysbtn.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("sktsysbtn.BaseItemMouse")));
            this.sktsysbtn.BaseItemPressed = System.Drawing.Color.Transparent;
            this.sktsysbtn.BaseItemRadius = 2;
            this.sktsysbtn.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.sktsysbtn.BaseItemSplitter = System.Drawing.Color.Transparent;
            this.sktsysbtn.Dock = System.Windows.Forms.DockStyle.None;
            this.sktsysbtn.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.sktsysbtn.Fore = System.Drawing.Color.Black;
            this.sktsysbtn.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.sktsysbtn.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.sktsysbtn.HoverFore = System.Drawing.Color.White;
            this.sktsysbtn.ItemAnamorphosis = false;
            this.sktsysbtn.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.sktsysbtn.ItemBorderShow = false;
            this.sktsysbtn.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.sktsysbtn.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.sktsysbtn.ItemRadius = 3;
            this.sktsysbtn.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.sktsysbtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtnMenuManger,
            this.toolbtnPrintSet});
            this.sktsysbtn.Location = new System.Drawing.Point(327, 673);
            this.sktsysbtn.Name = "sktsysbtn";
            this.sktsysbtn.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.sktsysbtn.Size = new System.Drawing.Size(92, 24);
            this.sktsysbtn.SkinAllColor = true;
            this.sktsysbtn.TabIndex = 135;
            this.sktsysbtn.Text = "skinToolStrip2";
            this.sktsysbtn.TitleAnamorphosis = false;
            this.sktsysbtn.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.sktsysbtn.TitleRadius = 4;
            this.sktsysbtn.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolbtnMenuManger
            // 
            this.toolbtnMenuManger.AutoSize = false;
            this.toolbtnMenuManger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnMenuManger.Image = global::YaoDianCai_PC.Properties.Resources.menu_manage;
            this.toolbtnMenuManger.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolbtnMenuManger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnMenuManger.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.toolbtnMenuManger.Name = "toolbtnMenuManger";
            this.toolbtnMenuManger.Size = new System.Drawing.Size(24, 24);
            this.toolbtnMenuManger.Text = "菜单管理";
            this.toolbtnMenuManger.ToolTipText = "菜单管理";
            this.toolbtnMenuManger.Click += new System.EventHandler(this.toolbtnMenuManger_Click);
            // 
            // toolbtnPrintSet
            // 
            this.toolbtnPrintSet.AutoSize = false;
            this.toolbtnPrintSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPrintSet.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnPrintSet.Image")));
            this.toolbtnPrintSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolbtnPrintSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPrintSet.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.toolbtnPrintSet.Name = "toolbtnPrintSet";
            this.toolbtnPrintSet.Size = new System.Drawing.Size(24, 24);
            this.toolbtnPrintSet.Text = "打印设置";
            this.toolbtnPrintSet.ToolTipText = "打印设置";
            this.toolbtnPrintSet.Click += new System.EventHandler(this.toolbtnPrintSet_Click);
            // 
            // panTime
            // 
            this.panTime.BackColor = System.Drawing.Color.Transparent;
            this.panTime.Controls.Add(this.labTime);
            this.panTime.Controls.Add(this.skttime);
            this.panTime.Location = new System.Drawing.Point(5, 672);
            this.panTime.Name = "panTime";
            this.panTime.Size = new System.Drawing.Size(154, 24);
            this.panTime.TabIndex = 131;
            this.panTime.Visible = false;
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.BackColor = System.Drawing.Color.Transparent;
            this.labTime.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labTime.Location = new System.Drawing.Point(4, 4);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(100, 17);
            this.labTime.TabIndex = 128;
            this.labTime.Text = "2012年12月12日";
            // 
            // skttime
            // 
            this.skttime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.skttime.Arrow = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.skttime.AutoSize = false;
            this.skttime.Back = System.Drawing.Color.White;
            this.skttime.BackColor = System.Drawing.Color.Transparent;
            this.skttime.BackRadius = 4;
            this.skttime.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skttime.Base = System.Drawing.Color.Transparent;
            this.skttime.BaseFore = System.Drawing.Color.Black;
            this.skttime.BaseForeAnamorphosis = true;
            this.skttime.BaseForeAnamorphosisBorder = 4;
            this.skttime.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skttime.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skttime.BaseHoverFore = System.Drawing.Color.Black;
            this.skttime.BaseItemAnamorphosis = true;
            this.skttime.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.skttime.BaseItemBorderShow = true;
            this.skttime.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skttime.BaseItemDown")));
            this.skttime.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.skttime.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skttime.BaseItemMouse")));
            this.skttime.BaseItemPressed = System.Drawing.Color.Transparent;
            this.skttime.BaseItemRadius = 2;
            this.skttime.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skttime.BaseItemSplitter = System.Drawing.Color.Transparent;
            this.skttime.Dock = System.Windows.Forms.DockStyle.None;
            this.skttime.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skttime.Fore = System.Drawing.Color.Black;
            this.skttime.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skttime.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.skttime.HoverFore = System.Drawing.Color.White;
            this.skttime.ItemAnamorphosis = false;
            this.skttime.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skttime.ItemBorderShow = false;
            this.skttime.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skttime.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skttime.ItemRadius = 3;
            this.skttime.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.skttime.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtnTime});
            this.skttime.Location = new System.Drawing.Point(105, 1);
            this.skttime.Name = "skttime";
            this.skttime.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skttime.Size = new System.Drawing.Size(30, 24);
            this.skttime.SkinAllColor = true;
            this.skttime.TabIndex = 129;
            this.skttime.Text = "skinToolStrip2";
            this.skttime.TitleAnamorphosis = false;
            this.skttime.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skttime.TitleRadius = 4;
            this.skttime.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolbtnTime
            // 
            this.toolbtnTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnTime.Image = global::YaoDianCai_PC.Properties.Resources.time;
            this.toolbtnTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnTime.Name = "toolbtnTime";
            this.toolbtnTime.Size = new System.Drawing.Size(23, 21);
            this.toolbtnTime.Text = "选择查询时间";
            this.toolbtnTime.Click += new System.EventHandler(this.toolbtnTime_Click);
            // 
            // ptbNeworderMsg
            // 
            this.ptbNeworderMsg.BackColor = System.Drawing.Color.Transparent;
            this.ptbNeworderMsg.Image = global::YaoDianCai_PC.Properties.Resources.dian1;
            this.ptbNeworderMsg.Location = new System.Drawing.Point(76, 93);
            this.ptbNeworderMsg.Name = "ptbNeworderMsg";
            this.ptbNeworderMsg.Size = new System.Drawing.Size(16, 16);
            this.ptbNeworderMsg.TabIndex = 136;
            this.ptbNeworderMsg.TabStop = false;
            this.ptbNeworderMsg.Visible = false;
            // 
            // ptbNewGroupBuyMsg
            // 
            this.ptbNewGroupBuyMsg.BackColor = System.Drawing.Color.Transparent;
            this.ptbNewGroupBuyMsg.Image = global::YaoDianCai_PC.Properties.Resources.dian1;
            this.ptbNewGroupBuyMsg.Location = new System.Drawing.Point(268, 92);
            this.ptbNewGroupBuyMsg.Name = "ptbNewGroupBuyMsg";
            this.ptbNewGroupBuyMsg.Size = new System.Drawing.Size(16, 16);
            this.ptbNewGroupBuyMsg.TabIndex = 127;
            this.ptbNewGroupBuyMsg.TabStop = false;
            this.ptbNewGroupBuyMsg.Visible = false;
            // 
            // timNoIc
            // 
            this.timNoIc.Interval = 500;
            // 
            // lstNewOrder
            // 
            this.lstNewOrder.BorderColor = System.Drawing.Color.Transparent;
            this.lstNewOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstNewOrder.DrawBackColor = true;
            this.lstNewOrder.FullRowSelect = true;
            this.lstNewOrder.HeadColor = System.Drawing.Color.Transparent;
            this.lstNewOrder.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstNewOrder.Location = new System.Drawing.Point(-1, -3);
            this.lstNewOrder.Name = "lstNewOrder";
            this.lstNewOrder.OwnerDraw = true;
            this.lstNewOrder.RowBackColor2 = System.Drawing.Color.White;
            this.lstNewOrder.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.lstNewOrder.Size = new System.Drawing.Size(402, 557);
            this.lstNewOrder.StateImageList = this.imgListViewHtight;
            this.lstNewOrder.TabIndex = 1;
            this.lstNewOrder.UseCompatibleStateImageBehavior = false;
            this.lstNewOrder.View = System.Windows.Forms.View.Details;
            this.lstNewOrder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstNewOrder_MouseDoubleClick);
            // 
            // lstOldOrder
            // 
            this.lstOldOrder.BorderColor = System.Drawing.Color.Transparent;
            this.lstOldOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstOldOrder.DrawBackColor = true;
            this.lstOldOrder.FullRowSelect = true;
            this.lstOldOrder.HeadColor = System.Drawing.Color.Transparent;
            this.lstOldOrder.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstOldOrder.Location = new System.Drawing.Point(-1, -3);
            this.lstOldOrder.Name = "lstOldOrder";
            this.lstOldOrder.OwnerDraw = true;
            this.lstOldOrder.RowBackColor2 = System.Drawing.Color.White;
            this.lstOldOrder.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.lstOldOrder.Size = new System.Drawing.Size(402, 557);
            this.lstOldOrder.StateImageList = this.imgListViewHtight;
            this.lstOldOrder.TabIndex = 2;
            this.lstOldOrder.UseCompatibleStateImageBehavior = false;
            this.lstOldOrder.View = System.Windows.Forms.View.Details;
            this.lstOldOrder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstOldOrder_MouseDoubleClick);
            // 
            // lstNewGroupBuy
            // 
            this.lstNewGroupBuy.BorderColor = System.Drawing.Color.Transparent;
            this.lstNewGroupBuy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstNewGroupBuy.DrawBackColor = true;
            this.lstNewGroupBuy.FullRowSelect = true;
            this.lstNewGroupBuy.HeadColor = System.Drawing.Color.Transparent;
            this.lstNewGroupBuy.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstNewGroupBuy.Location = new System.Drawing.Point(-1, -3);
            this.lstNewGroupBuy.Name = "lstNewGroupBuy";
            this.lstNewGroupBuy.OwnerDraw = true;
            this.lstNewGroupBuy.RowBackColor2 = System.Drawing.Color.White;
            this.lstNewGroupBuy.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.lstNewGroupBuy.Size = new System.Drawing.Size(402, 557);
            this.lstNewGroupBuy.StateImageList = this.imgListViewHtight;
            this.lstNewGroupBuy.TabIndex = 2;
            this.lstNewGroupBuy.UseCompatibleStateImageBehavior = false;
            this.lstNewGroupBuy.View = System.Windows.Forms.View.Details;
            this.lstNewGroupBuy.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstNewGroupBuy_MouseDoubleClick);
            // 
            // lstOldGroupBuy
            // 
            this.lstOldGroupBuy.BorderColor = System.Drawing.Color.Transparent;
            this.lstOldGroupBuy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstOldGroupBuy.DrawBackColor = true;
            this.lstOldGroupBuy.FullRowSelect = true;
            this.lstOldGroupBuy.HeadColor = System.Drawing.Color.Transparent;
            this.lstOldGroupBuy.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstOldGroupBuy.Location = new System.Drawing.Point(-1, -3);
            this.lstOldGroupBuy.Name = "lstOldGroupBuy";
            this.lstOldGroupBuy.OwnerDraw = true;
            this.lstOldGroupBuy.RowBackColor2 = System.Drawing.Color.White;
            this.lstOldGroupBuy.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.lstOldGroupBuy.Size = new System.Drawing.Size(402, 557);
            this.lstOldGroupBuy.StateImageList = this.imgListViewHtight;
            this.lstOldGroupBuy.TabIndex = 3;
            this.lstOldGroupBuy.UseCompatibleStateImageBehavior = false;
            this.lstOldGroupBuy.View = System.Windows.Forms.View.Details;
            this.lstOldGroupBuy.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstOldGroupBuy_MouseDoubleClick);
            // 
            // palSelect
            // 
            this.palSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.palSelect.Location = new System.Drawing.Point(2, 110);
            this.palSelect.Name = "palSelect";
            this.palSelect.Size = new System.Drawing.Size(98, 3);
            this.palSelect.TabIndex = 137;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::YaoDianCai_PC.Properties.Resources.main_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackPalace = global::YaoDianCai_PC.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(400, 700);
            this.CloseBoxSize = new System.Drawing.Size(39, 20);
            this.CloseDownBack = global::YaoDianCai_PC.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_close_disable;
            this.ControlBoxOffset = new System.Drawing.Point(1, -1);
            this.Controls.Add(this.palSelect);
            this.Controls.Add(this.ptbNewGroupBuyMsg);
            this.Controls.Add(this.ptbNeworderMsg);
            this.Controls.Add(this.panTime);
            this.Controls.Add(this.sktsysbtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tabManTab);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 700);
            this.MiniDownBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_highlight;
            this.MinimumSize = new System.Drawing.Size(400, 700);
            this.MiniNormlBack = global::YaoDianCai_PC.Properties.Resources.btn_mini_normal;
            this.MiniSize = new System.Drawing.Size(28, 20);
            this.Name = "FrmMain";
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "要点菜商家管理平台";
            this.TitleOffset = new System.Drawing.Point(5, 0);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabManTab.ResumeLayout(false);
            this.tabNewOrderInfo.ResumeLayout(false);
            this.tabOldOrderInfo.ResumeLayout(false);
            this.tabGroupBuyInfo.ResumeLayout(false);
            this.tabOldGroupBuyInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.sktsysbtn.ResumeLayout(false);
            this.sktsysbtn.PerformLayout();
            this.panTime.ResumeLayout(false);
            this.panTime.PerformLayout();
            this.skttime.ResumeLayout(false);
            this.skttime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbNeworderMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbNewGroupBuyMsg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl tabManTab;
        private System.Windows.Forms.TabPage tabNewOrderInfo;
        private System.Windows.Forms.TabPage tabOldOrderInfo;
        private System.Windows.Forms.TabPage tabGroupBuyInfo;
        private System.Windows.Forms.TabPage tabOldGroupBuyInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CCWin.SkinControl.SkinLabel lblName;
        private CCWin.SkinControl.SkinToolStrip sktsysbtn;
        private System.Windows.Forms.ToolStripButton toolbtnMenuManger;
        private System.Windows.Forms.ToolStripButton toolbtnPrintSet;
        private System.Windows.Forms.Panel panTime;
        private System.Windows.Forms.Label labTime;
        private CCWin.SkinControl.SkinToolStrip skttime;
        private System.Windows.Forms.ToolStripButton toolbtnTime;
        private System.Windows.Forms.PictureBox ptbNeworderMsg;
        private System.Windows.Forms.PictureBox ptbNewGroupBuyMsg;
        private YDCControl.YDCListViews lstNewOrder;
        private YDCControl.YDCListViews lstNewGroupBuy;
        private YDCControl.YDCListViews lstOldOrder;
        private YDCControl.YDCListViews lstOldGroupBuy;
        private System.Windows.Forms.Timer timNoIc;
        private System.Windows.Forms.ImageList imgListViewHtight;
        private System.Windows.Forms.Panel palSelect;
    }
}
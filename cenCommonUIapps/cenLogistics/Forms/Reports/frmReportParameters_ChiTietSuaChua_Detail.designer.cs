namespace cenCommonUIapps.cenLogistics.Forms
{
    partial class frmReportParameters_ChiTietSuaChua_Detail
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            this.cmdSave = new Infragistics.Win.Misc.UltraButton();
            this.cmdCancel = new Infragistics.Win.Misc.UltraButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtTenDanhMucNhanSu = new cenControls.saTextBox();
            this.txtMaDanhMucNhanSu = new cenControls.saTextBox();
            this.txtDenNgay = new cenControls.saDateTimeBox();
            this.txtTuNgay = new cenControls.saDateTimeBox();
            this.saLabel3 = new cenControls.saLabel();
            this.saLabel2 = new cenControls.saLabel();
            this.saLabel1 = new cenControls.saLabel();
            this.saLabel4 = new cenControls.saLabel();
            this.txtMaDanhMucBoPhanContainer = new cenControls.saTextBox();
            this.txtTenDanhMucBoPhanContainer = new cenControls.saTextBox();
            this.txtMaDanhMucMaSuaChuaContainer = new cenControls.saTextBox();
            this.txtTenDanhMucMaSuaChuaContainer = new cenControls.saTextBox();
            this.saLabel5 = new cenControls.saLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDanhMucNhanSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDanhMucNhanSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDanhMucBoPhanContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDanhMucBoPhanContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDanhMucMaSuaChuaContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDanhMucMaSuaChuaContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            appearance1.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.cmdSave.Appearance = appearance1;
            this.cmdSave.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
            this.cmdSave.Location = new System.Drawing.Point(445, 168);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Tag = "cmdOK";
            this.cmdSave.Text = "Đồng ý";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            appearance2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            appearance2.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.cmdCancel.Appearance = appearance2;
            this.cmdCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
            this.cmdCancel.CausesValidation = false;
            this.cmdCancel.Location = new System.Drawing.Point(526, 168);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Tag = "cmdClose";
            this.cmdCancel.Text = "&Hủy";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.None;
            this.ultraGroupBox1.Controls.Add(this.ultraGroupBox2);
            this.ultraGroupBox1.Controls.Add(this.cmdCancel);
            this.ultraGroupBox1.Controls.Add(this.cmdSave);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(604, 194);
            this.ultraGroupBox1.TabIndex = 6;
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGroupBox2.Appearance = appearance3;
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.Rectangular3D;
            this.ultraGroupBox2.Controls.Add(this.txtTenDanhMucMaSuaChuaContainer);
            this.ultraGroupBox2.Controls.Add(this.txtTenDanhMucBoPhanContainer);
            this.ultraGroupBox2.Controls.Add(this.txtTenDanhMucNhanSu);
            this.ultraGroupBox2.Controls.Add(this.txtMaDanhMucMaSuaChuaContainer);
            this.ultraGroupBox2.Controls.Add(this.txtMaDanhMucBoPhanContainer);
            this.ultraGroupBox2.Controls.Add(this.txtMaDanhMucNhanSu);
            this.ultraGroupBox2.Controls.Add(this.txtDenNgay);
            this.ultraGroupBox2.Controls.Add(this.saLabel5);
            this.ultraGroupBox2.Controls.Add(this.saLabel4);
            this.ultraGroupBox2.Controls.Add(this.txtTuNgay);
            this.ultraGroupBox2.Controls.Add(this.saLabel3);
            this.ultraGroupBox2.Controls.Add(this.saLabel2);
            this.ultraGroupBox2.Controls.Add(this.saLabel1);
            this.ultraGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(598, 165);
            this.ultraGroupBox2.TabIndex = 0;
            this.ultraGroupBox2.UseAppStyling = false;
            // 
            // txtTenDanhMucNhanSu
            // 
            appearance6.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTenDanhMucNhanSu.Appearance = appearance6;
            this.txtTenDanhMucNhanSu.AutoSize = false;
            this.txtTenDanhMucNhanSu.Enabled = false;
            this.txtTenDanhMucNhanSu.Location = new System.Drawing.Point(234, 70);
            this.txtTenDanhMucNhanSu.Name = "txtTenDanhMucNhanSu";
            this.txtTenDanhMucNhanSu.Size = new System.Drawing.Size(355, 21);
            this.txtTenDanhMucNhanSu.TabIndex = 3;
            // 
            // txtMaDanhMucNhanSu
            // 
            appearance9.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtMaDanhMucNhanSu.Appearance = appearance9;
            this.txtMaDanhMucNhanSu.AutoSize = false;
            this.txtMaDanhMucNhanSu.Location = new System.Drawing.Point(81, 70);
            this.txtMaDanhMucNhanSu.Name = "txtMaDanhMucNhanSu";
            this.txtMaDanhMucNhanSu.Size = new System.Drawing.Size(147, 21);
            this.txtMaDanhMucNhanSu.TabIndex = 2;
            // 
            // txtDenNgay
            // 
            appearance10.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtDenNgay.Appearance = appearance10;
            this.txtDenNgay.AutoFillDate = Infragistics.Win.UltraWinMaskedEdit.AutoFillDate.MonthAndYear;
            this.txtDenNgay.AutoFillTime = Infragistics.Win.UltraWinMaskedEdit.AutoFillTime.CurrentTime;
            this.txtDenNgay.AutoSize = false;
            this.txtDenNgay.Location = new System.Drawing.Point(81, 43);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(147, 21);
            this.txtDenNgay.TabIndex = 1;
            this.txtDenNgay.Value = null;
            // 
            // txtTuNgay
            // 
            appearance13.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTuNgay.Appearance = appearance13;
            this.txtTuNgay.AutoFillDate = Infragistics.Win.UltraWinMaskedEdit.AutoFillDate.MonthAndYear;
            this.txtTuNgay.AutoFillTime = Infragistics.Win.UltraWinMaskedEdit.AutoFillTime.CurrentTime;
            this.txtTuNgay.AutoSize = false;
            this.txtTuNgay.Location = new System.Drawing.Point(81, 16);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(147, 21);
            this.txtTuNgay.TabIndex = 0;
            this.txtTuNgay.Value = null;
            // 
            // saLabel3
            // 
            appearance14.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel3.Appearance = appearance14;
            this.saLabel3.AutoSize = true;
            this.saLabel3.Location = new System.Drawing.Point(6, 70);
            this.saLabel3.Name = "saLabel3";
            this.saLabel3.Size = new System.Drawing.Size(50, 14);
            this.saLabel3.TabIndex = 0;
            this.saLabel3.Text = "Hàng tàu";
            // 
            // saLabel2
            // 
            appearance15.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel2.Appearance = appearance15;
            this.saLabel2.AutoSize = true;
            this.saLabel2.Location = new System.Drawing.Point(6, 43);
            this.saLabel2.Name = "saLabel2";
            this.saLabel2.Size = new System.Drawing.Size(53, 14);
            this.saLabel2.TabIndex = 0;
            this.saLabel2.Text = "Đến ngày";
            // 
            // saLabel1
            // 
            appearance16.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel1.Appearance = appearance16;
            this.saLabel1.AutoSize = true;
            this.saLabel1.Location = new System.Drawing.Point(6, 16);
            this.saLabel1.Name = "saLabel1";
            this.saLabel1.Size = new System.Drawing.Size(45, 14);
            this.saLabel1.TabIndex = 0;
            this.saLabel1.Text = "Từ ngày";
            // 
            // saLabel4
            // 
            appearance12.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel4.Appearance = appearance12;
            this.saLabel4.AutoSize = true;
            this.saLabel4.Location = new System.Drawing.Point(6, 97);
            this.saLabel4.Name = "saLabel4";
            this.saLabel4.Size = new System.Drawing.Size(64, 14);
            this.saLabel4.TabIndex = 0;
            this.saLabel4.Text = "Mã bộ phận";
            // 
            // txtMaDanhMucBoPhanContainer
            // 
            appearance8.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtMaDanhMucBoPhanContainer.Appearance = appearance8;
            this.txtMaDanhMucBoPhanContainer.AutoSize = false;
            this.txtMaDanhMucBoPhanContainer.Location = new System.Drawing.Point(81, 97);
            this.txtMaDanhMucBoPhanContainer.Name = "txtMaDanhMucBoPhanContainer";
            this.txtMaDanhMucBoPhanContainer.Size = new System.Drawing.Size(147, 21);
            this.txtMaDanhMucBoPhanContainer.TabIndex = 4;
            // 
            // txtTenDanhMucBoPhanContainer
            // 
            appearance5.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTenDanhMucBoPhanContainer.Appearance = appearance5;
            this.txtTenDanhMucBoPhanContainer.AutoSize = false;
            this.txtTenDanhMucBoPhanContainer.Enabled = false;
            this.txtTenDanhMucBoPhanContainer.Location = new System.Drawing.Point(234, 97);
            this.txtTenDanhMucBoPhanContainer.Name = "txtTenDanhMucBoPhanContainer";
            this.txtTenDanhMucBoPhanContainer.Size = new System.Drawing.Size(355, 21);
            this.txtTenDanhMucBoPhanContainer.TabIndex = 5;
            // 
            // txtMaDanhMucMaSuaChuaContainer
            // 
            appearance7.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtMaDanhMucMaSuaChuaContainer.Appearance = appearance7;
            this.txtMaDanhMucMaSuaChuaContainer.AutoSize = false;
            this.txtMaDanhMucMaSuaChuaContainer.Location = new System.Drawing.Point(81, 124);
            this.txtMaDanhMucMaSuaChuaContainer.Name = "txtMaDanhMucMaSuaChuaContainer";
            this.txtMaDanhMucMaSuaChuaContainer.Size = new System.Drawing.Size(147, 21);
            this.txtMaDanhMucMaSuaChuaContainer.TabIndex = 6;
            // 
            // txtTenDanhMucMaSuaChuaContainer
            // 
            appearance4.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTenDanhMucMaSuaChuaContainer.Appearance = appearance4;
            this.txtTenDanhMucMaSuaChuaContainer.AutoSize = false;
            this.txtTenDanhMucMaSuaChuaContainer.Enabled = false;
            this.txtTenDanhMucMaSuaChuaContainer.Location = new System.Drawing.Point(234, 124);
            this.txtTenDanhMucMaSuaChuaContainer.Name = "txtTenDanhMucMaSuaChuaContainer";
            this.txtTenDanhMucMaSuaChuaContainer.Size = new System.Drawing.Size(355, 21);
            this.txtTenDanhMucMaSuaChuaContainer.TabIndex = 7;
            // 
            // saLabel5
            // 
            appearance11.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel5.Appearance = appearance11;
            this.saLabel5.AutoSize = true;
            this.saLabel5.Location = new System.Drawing.Point(6, 124);
            this.saLabel5.Name = "saLabel5";
            this.saLabel5.Size = new System.Drawing.Size(69, 14);
            this.saLabel5.TabIndex = 0;
            this.saLabel5.Text = "Mã sửa chữa";
            // 
            // frmReportParameters_ChiTietSuaChua_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 194);
            this.Controls.Add(this.ultraGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportParameters_ChiTietSuaChua_Detail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.frmReportParameters_ChiTietBaoGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDanhMucNhanSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDanhMucNhanSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDanhMucBoPhanContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDanhMucBoPhanContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDanhMucMaSuaChuaContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDanhMucMaSuaChuaContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Infragistics.Win.Misc.UltraButton cmdSave;
        public Infragistics.Win.Misc.UltraButton cmdCancel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private cenControls.saTextBox txtTenDanhMucNhanSu;
        private cenControls.saTextBox txtMaDanhMucNhanSu;
        private cenControls.saDateTimeBox txtDenNgay;
        private cenControls.saDateTimeBox txtTuNgay;
        private cenControls.saLabel saLabel3;
        private cenControls.saLabel saLabel2;
        private cenControls.saLabel saLabel1;
        private cenControls.saTextBox txtTenDanhMucMaSuaChuaContainer;
        private cenControls.saTextBox txtTenDanhMucBoPhanContainer;
        private cenControls.saTextBox txtMaDanhMucMaSuaChuaContainer;
        private cenControls.saTextBox txtMaDanhMucBoPhanContainer;
        private cenControls.saLabel saLabel5;
        private cenControls.saLabel saLabel4;
    }
}
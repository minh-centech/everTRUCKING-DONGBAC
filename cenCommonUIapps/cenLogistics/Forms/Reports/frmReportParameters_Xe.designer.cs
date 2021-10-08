namespace cenCommonUIapps.cenLogistics.Forms
{
    partial class frmReportParameters_Xe
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
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.cmdSave = new Infragistics.Win.Misc.UltraButton();
            this.cmdCancel = new Infragistics.Win.Misc.UltraButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtDenNgay = new cenControls.saDateTimeBox();
            this.txtTuNgay = new cenControls.saDateTimeBox();
            this.saLabel3 = new cenControls.saLabel();
            this.saLabel2 = new cenControls.saLabel();
            this.saLabel1 = new cenControls.saLabel();
            this.cboIDDanhMucXe = new cenControls.saComboDanhMuc();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDDanhMucXe)).BeginInit();
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
            this.cmdSave.Location = new System.Drawing.Point(445, 114);
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
            this.cmdCancel.Location = new System.Drawing.Point(526, 114);
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
            this.ultraGroupBox1.Size = new System.Drawing.Size(604, 140);
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
            this.ultraGroupBox2.Controls.Add(this.cboIDDanhMucXe);
            this.ultraGroupBox2.Controls.Add(this.txtDenNgay);
            this.ultraGroupBox2.Controls.Add(this.txtTuNgay);
            this.ultraGroupBox2.Controls.Add(this.saLabel3);
            this.ultraGroupBox2.Controls.Add(this.saLabel2);
            this.ultraGroupBox2.Controls.Add(this.saLabel1);
            this.ultraGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(598, 111);
            this.ultraGroupBox2.TabIndex = 0;
            this.ultraGroupBox2.UseAppStyling = false;
            // 
            // txtDenNgay
            // 
            appearance5.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtDenNgay.Appearance = appearance5;
            this.txtDenNgay.AutoFillDate = Infragistics.Win.UltraWinMaskedEdit.AutoFillDate.MonthAndYear;
            this.txtDenNgay.AutoFillTime = Infragistics.Win.UltraWinMaskedEdit.AutoFillTime.CurrentTime;
            this.txtDenNgay.AutoSize = false;
            this.txtDenNgay.Location = new System.Drawing.Point(77, 43);
            this.txtDenNgay.MaskInput = "dd/mm/yyyy";
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(147, 21);
            this.txtDenNgay.TabIndex = 1;
            this.txtDenNgay.Value = null;
            // 
            // txtTuNgay
            // 
            appearance6.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTuNgay.Appearance = appearance6;
            this.txtTuNgay.AutoFillDate = Infragistics.Win.UltraWinMaskedEdit.AutoFillDate.MonthAndYear;
            this.txtTuNgay.AutoFillTime = Infragistics.Win.UltraWinMaskedEdit.AutoFillTime.CurrentTime;
            this.txtTuNgay.AutoSize = false;
            this.txtTuNgay.Location = new System.Drawing.Point(77, 16);
            this.txtTuNgay.MaskInput = "dd/mm/yyyy";
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(147, 21);
            this.txtTuNgay.TabIndex = 0;
            this.txtTuNgay.Value = null;
            // 
            // saLabel3
            // 
            appearance7.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel3.Appearance = appearance7;
            this.saLabel3.AutoSize = true;
            this.saLabel3.Location = new System.Drawing.Point(6, 70);
            this.saLabel3.Name = "saLabel3";
            this.saLabel3.Size = new System.Drawing.Size(18, 14);
            this.saLabel3.TabIndex = 0;
            this.saLabel3.Text = "Xe";
            // 
            // saLabel2
            // 
            appearance8.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel2.Appearance = appearance8;
            this.saLabel2.AutoSize = true;
            this.saLabel2.Location = new System.Drawing.Point(6, 43);
            this.saLabel2.Name = "saLabel2";
            this.saLabel2.Size = new System.Drawing.Size(53, 14);
            this.saLabel2.TabIndex = 0;
            this.saLabel2.Text = "Đến ngày";
            // 
            // saLabel1
            // 
            appearance9.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel1.Appearance = appearance9;
            this.saLabel1.AutoSize = true;
            this.saLabel1.Location = new System.Drawing.Point(6, 16);
            this.saLabel1.Name = "saLabel1";
            this.saLabel1.Size = new System.Drawing.Size(45, 14);
            this.saLabel1.TabIndex = 0;
            this.saLabel1.Text = "Từ ngày";
            // 
            // cboIDDanhMucXe
            // 
            appearance4.ForeColorDisabled = System.Drawing.Color.Black;
            this.cboIDDanhMucXe.Appearance = appearance4;
            this.cboIDDanhMucXe.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            this.cboIDDanhMucXe.AutoSize = false;
            this.cboIDDanhMucXe.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cboIDDanhMucXe.Location = new System.Drawing.Point(77, 70);
            this.cboIDDanhMucXe.Name = "cboIDDanhMucXe";
            this.cboIDDanhMucXe.Size = new System.Drawing.Size(515, 21);
            this.cboIDDanhMucXe.TabIndex = 4;
            // 
            // frmReportParameters_Xe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 140);
            this.Controls.Add(this.ultraGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportParameters_Xe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.frmReportParameters_ChiTietBaoGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDDanhMucXe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Infragistics.Win.Misc.UltraButton cmdSave;
        public Infragistics.Win.Misc.UltraButton cmdCancel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private cenControls.saDateTimeBox txtDenNgay;
        private cenControls.saDateTimeBox txtTuNgay;
        private cenControls.saLabel saLabel3;
        private cenControls.saLabel saLabel2;
        private cenControls.saLabel saLabel1;
        private cenControls.saComboDanhMuc cboIDDanhMucXe;
    }
}
﻿namespace cenCommonUIapps.DatabaseCore.Forms
{
    partial class frmDanhMucLoaiDoiTuongUpdate
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
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.saLabel1 = new cenControls.saLabel();
            this.txtMa = new cenControls.saTextBox();
            this.saLabel2 = new cenControls.saLabel();
            this.txtTen = new cenControls.saTextBox();
            this.saLabel3 = new cenControls.saLabel();
            this.txtTenBangDuLieu = new cenControls.saTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupForm)).BeginInit();
            this.groupForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupEditor)).BeginInit();
            this.groupEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBangDuLieu)).BeginInit();
            this.SuspendLayout();
            // 
            // groupForm
            // 
            this.groupForm.Size = new System.Drawing.Size(580, 232);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(399, 206);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(491, 206);
            // 
            // cmdSaveNew
            // 
            this.cmdSaveNew.Location = new System.Drawing.Point(267, 206);
            // 
            // groupEditor
            // 
            this.groupEditor.Controls.Add(this.txtTen);
            this.groupEditor.Controls.Add(this.txtTenBangDuLieu);
            this.groupEditor.Controls.Add(this.txtMa);
            this.groupEditor.Controls.Add(this.saLabel3);
            this.groupEditor.Controls.Add(this.saLabel2);
            this.groupEditor.Controls.Add(this.saLabel1);
            this.groupEditor.Size = new System.Drawing.Size(574, 201);
            // 
            // saLabel1
            // 
            appearance6.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel1.Appearance = appearance6;
            this.saLabel1.AutoSize = true;
            this.saLabel1.Location = new System.Drawing.Point(6, 6);
            this.saLabel1.Name = "saLabel1";
            this.saLabel1.Size = new System.Drawing.Size(20, 14);
            this.saLabel1.TabIndex = 0;
            this.saLabel1.Text = "Mã";
            // 
            // txtMa
            // 
            appearance3.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtMa.Appearance = appearance3;
            this.txtMa.AutoSize = false;
            this.txtMa.Location = new System.Drawing.Point(100, 7);
            this.txtMa.MaxLength = 128;
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(239, 21);
            this.txtMa.TabIndex = 0;
            // 
            // saLabel2
            // 
            appearance5.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel2.Appearance = appearance5;
            this.saLabel2.AutoSize = true;
            this.saLabel2.Location = new System.Drawing.Point(6, 34);
            this.saLabel2.Name = "saLabel2";
            this.saLabel2.Size = new System.Drawing.Size(24, 14);
            this.saLabel2.TabIndex = 0;
            this.saLabel2.Text = "Tên";
            // 
            // txtTen
            // 
            appearance1.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTen.Appearance = appearance1;
            this.txtTen.AutoSize = false;
            this.txtTen.Location = new System.Drawing.Point(100, 34);
            this.txtTen.MaxLength = 255;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(466, 21);
            this.txtTen.TabIndex = 1;
            // 
            // saLabel3
            // 
            appearance4.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel3.Appearance = appearance4;
            this.saLabel3.AutoSize = true;
            this.saLabel3.Location = new System.Drawing.Point(6, 60);
            this.saLabel3.Name = "saLabel3";
            this.saLabel3.Size = new System.Drawing.Size(88, 14);
            this.saLabel3.TabIndex = 0;
            this.saLabel3.Text = "Tên bảng dữ liệu";
            // 
            // txtTenBangDuLieu
            // 
            appearance2.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtTenBangDuLieu.Appearance = appearance2;
            this.txtTenBangDuLieu.AutoSize = false;
            this.txtTenBangDuLieu.Location = new System.Drawing.Point(100, 61);
            this.txtTenBangDuLieu.MaxLength = 128;
            this.txtTenBangDuLieu.Name = "txtTenBangDuLieu";
            this.txtTenBangDuLieu.Size = new System.Drawing.Size(239, 21);
            this.txtTenBangDuLieu.TabIndex = 2;
            // 
            // frmDanhMucLoaiDoiTuongUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 229);
            this.Name = "frmDanhMucLoaiDoiTuongUpdate";
            this.Text = "frmDanhMucDonViUpdate";
            this.Load += new System.EventHandler(this.frmDanhMucLoaiDoiTuongUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupForm)).EndInit();
            this.groupForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupEditor)).EndInit();
            this.groupEditor.ResumeLayout(false);
            this.groupEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBangDuLieu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private cenControls.saTextBox txtTen;
        private cenControls.saTextBox txtMa;
        private cenControls.saLabel saLabel2;
        private cenControls.saLabel saLabel1;
        private cenControls.saTextBox txtTenBangDuLieu;
        private cenControls.saLabel saLabel3;
    }
}
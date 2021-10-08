namespace cenCommonUIapps.cenLogistics.Forms
{
    partial class frmDanhMucDinhMucChiPhiXeUpdate
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
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.saLabel4 = new cenControls.saLabel();
            this.saLabel19 = new cenControls.saLabel();
            this.txtGhiChu = new cenControls.saTextBox();
            this.cboIDDanhMucXe = new cenControls.saComboDanhMuc();
            this.saLabel1 = new cenControls.saLabel();
            ((System.ComponentModel.ISupportInitialize)(this.groupForm)).BeginInit();
            this.groupForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupEditor)).BeginInit();
            this.groupEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDDanhMucXe)).BeginInit();
            this.SuspendLayout();
            // 
            // groupForm
            // 
            this.groupForm.Size = new System.Drawing.Size(688, 99);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(507, 73);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(599, 73);
            // 
            // cmdSaveNew
            // 
            this.cmdSaveNew.Location = new System.Drawing.Point(375, 73);
            // 
            // groupEditor
            // 
            this.groupEditor.Controls.Add(this.cboIDDanhMucXe);
            this.groupEditor.Controls.Add(this.txtGhiChu);
            this.groupEditor.Controls.Add(this.saLabel1);
            this.groupEditor.Controls.Add(this.saLabel19);
            this.groupEditor.Controls.Add(this.saLabel4);
            this.groupEditor.Size = new System.Drawing.Size(682, 70);
            // 
            // saLabel4
            // 
            appearance5.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel4.Appearance = appearance5;
            this.saLabel4.AutoSize = true;
            this.saLabel4.Location = new System.Drawing.Point(6, 60);
            this.saLabel4.Name = "saLabel4";
            this.saLabel4.Size = new System.Drawing.Size(0, 0);
            this.saLabel4.TabIndex = 0;
            // 
            // saLabel19
            // 
            appearance4.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel19.Appearance = appearance4;
            this.saLabel19.AutoSize = true;
            this.saLabel19.Location = new System.Drawing.Point(9, 9);
            this.saLabel19.Name = "saLabel19";
            this.saLabel19.Size = new System.Drawing.Size(33, 14);
            this.saLabel19.TabIndex = 0;
            this.saLabel19.Text = "Số xe";
            // 
            // txtGhiChu
            // 
            appearance2.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtGhiChu.Appearance = appearance2;
            this.txtGhiChu.AutoSize = false;
            this.txtGhiChu.Location = new System.Drawing.Point(75, 37);
            this.txtGhiChu.MaxLength = 0;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(599, 21);
            this.txtGhiChu.TabIndex = 1;
            // 
            // cboIDDanhMucXe
            // 
            appearance1.ForeColorDisabled = System.Drawing.Color.Black;
            this.cboIDDanhMucXe.Appearance = appearance1;
            this.cboIDDanhMucXe.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            this.cboIDDanhMucXe.AutoSize = false;
            this.cboIDDanhMucXe.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cboIDDanhMucXe.Location = new System.Drawing.Point(75, 10);
            this.cboIDDanhMucXe.Name = "cboIDDanhMucXe";
            this.cboIDDanhMucXe.Size = new System.Drawing.Size(284, 21);
            this.cboIDDanhMucXe.TabIndex = 0;
            // 
            // saLabel1
            // 
            appearance3.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.saLabel1.Appearance = appearance3;
            this.saLabel1.AutoSize = true;
            this.saLabel1.Location = new System.Drawing.Point(9, 37);
            this.saLabel1.Name = "saLabel1";
            this.saLabel1.Size = new System.Drawing.Size(43, 14);
            this.saLabel1.TabIndex = 0;
            this.saLabel1.Text = "Ghi chú";
            // 
            // frmDanhMucDinhMucChiPhiXeUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 99);
            this.Name = "frmDanhMucDinhMucChiPhiXeUpdate";
            this.Text = "frmDanhMucDonViUpdate";
            this.Load += new System.EventHandler(this.frmDanhMucChungTuUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupForm)).EndInit();
            this.groupForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupEditor)).EndInit();
            this.groupEditor.ResumeLayout(false);
            this.groupEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDDanhMucXe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private cenControls.saLabel saLabel4;
        private cenControls.saTextBox txtGhiChu;
        private cenControls.saLabel saLabel19;
        private cenControls.saComboDanhMuc cboIDDanhMucXe;
        private cenControls.saLabel saLabel1;
    }
}
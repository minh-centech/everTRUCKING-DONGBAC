namespace cenBase.Forms
{
    partial class frmChungTufilter
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
            this.cmdSave = new Infragistics.Win.Misc.UltraButton();
            this.cmdCancel = new Infragistics.Win.Misc.UltraButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.groupEditors = new Infragistics.Win.Misc.UltraGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupEditors)).BeginInit();
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
            this.cmdSave.Location = new System.Drawing.Point(585, 324);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(83, 23);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Tag = "cmdfind";
            this.cmdSave.Text = "Đồng ý (F6)";
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
            this.cmdCancel.Location = new System.Drawing.Point(674, 324);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(81, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Tag = "cmdcancel";
            this.cmdCancel.Text = "Đóng (ESC)";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.None;
            this.ultraGroupBox1.Controls.Add(this.groupEditors);
            this.ultraGroupBox1.Controls.Add(this.cmdSave);
            this.ultraGroupBox1.Controls.Add(this.cmdCancel);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(758, 350);
            this.ultraGroupBox1.TabIndex = 6;
            // 
            // groupEditors
            // 
            this.groupEditors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.groupEditors.Appearance = appearance3;
            this.groupEditors.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.Rectangular3D;
            this.groupEditors.Location = new System.Drawing.Point(3, 3);
            this.groupEditors.Name = "groupEditors";
            this.groupEditors.Size = new System.Drawing.Size(752, 320);
            this.groupEditors.TabIndex = 0;
            this.groupEditors.UseAppStyling = false;
            // 
            // frmChungTufilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 350);
            this.ControlBox = false;
            this.Controls.Add(this.ultraGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChungTufilter";
            this.Text = "Tìm chứng từ";
            this.Load += new System.EventHandler(this.frm_donvi_capnhat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmfilterParameters_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupEditors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Infragistics.Win.Misc.UltraButton cmdSave;
        public Infragistics.Win.Misc.UltraButton cmdCancel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox groupEditors;
    }
}
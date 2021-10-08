namespace cenCommonUIbase.Forms.BaoCao
{ 
    partial class frmReportViewer_BaoCaoChiTietXeRaVao
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinEditors.EditorButton editorButton1 = new Infragistics.Win.UltraWinEditors.EditorButton("cmdfilter");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Toolbar1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btExcel");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btTaiLai");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btTaiLai");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btExcel");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ugBaoCao = new cenControls.saNonUpdateGrid();
            this.txtfilterBox = new cenControls.saTextBox();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this._frmReportViewer_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._frmReportViewer_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmReportViewer_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmReportViewer_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtfilterBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ugBaoCao);
            this.ultraTabPageControl1.Controls.Add(this.txtfilterBox);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(681, 329);
            // 
            // ugBaoCao
            // 
            this.ugBaoCao.DisplayLayout.LoadStyle = Infragistics.Win.UltraWinGrid.LoadStyle.LoadOnDemand;
            this.ugBaoCao.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ugBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugBaoCao.Location = new System.Drawing.Point(0, 24);
            this.ugBaoCao.Name = "ugBaoCao";
            this.ugBaoCao.Size = new System.Drawing.Size(681, 305);
            this.ugBaoCao.TabIndex = 0;
            this.ugBaoCao.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ugBaoCao_InitializeRow);
            // 
            // txtfilterBox
            // 
            appearance1.ForeColorDisabled = System.Drawing.Color.Black;
            this.txtfilterBox.Appearance = appearance1;
            this.txtfilterBox.AutoSize = false;
            editorButton1.Key = "cmdfilter";
            editorButton1.Text = "";
            this.txtfilterBox.ButtonsLeft.Add(editorButton1);
            this.txtfilterBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtfilterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfilterBox.Location = new System.Drawing.Point(0, 0);
            this.txtfilterBox.Name = "txtfilterBox";
            this.txtfilterBox.NullText = "F12: Lọc";
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.Color.Silver;
            this.txtfilterBox.NullTextAppearance = appearance2;
            this.txtfilterBox.Size = new System.Drawing.Size(681, 24);
            this.txtfilterBox.TabIndex = 16;
            this.txtfilterBox.ValueChanged += new System.EventHandler(this.filterBoxValueChanged);
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 25);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(685, 355);
            this.ultraTabControl1.TabIndex = 0;
            ultraTab2.Key = "grid";
            ultraTab2.TabPage = this.ultraTabPageControl1;
            ultraTab2.Text = "Grid";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(681, 329);
            // 
            // _frmReportViewer_Toolbars_Dock_Area_Left
            // 
            this._frmReportViewer_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmReportViewer_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control;
            this._frmReportViewer_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._frmReportViewer_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmReportViewer_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 25);
            this._frmReportViewer_Toolbars_Dock_Area_Left.Name = "_frmReportViewer_Toolbars_Dock_Area_Left";
            this._frmReportViewer_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 355);
            this._frmReportViewer_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this;
            this.ultraToolbarsManager1.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ultraToolbarsManager1.MdiMergeable = false;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            buttonTool1.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool2,
            buttonTool1});
            ultraToolbar1.Text = "Toolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            buttonTool8.SharedPropsInternal.AppearancesSmall.Appearance = appearance3;
            buttonTool8.SharedPropsInternal.Caption = "Tải lại";
            buttonTool8.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool9.SharedPropsInternal.AppearancesSmall.Appearance = appearance4;
            buttonTool9.SharedPropsInternal.Caption = "Excel";
            buttonTool9.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool8,
            buttonTool9});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            // 
            // _frmReportViewer_Toolbars_Dock_Area_Right
            // 
            this._frmReportViewer_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmReportViewer_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control;
            this._frmReportViewer_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._frmReportViewer_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmReportViewer_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(685, 25);
            this._frmReportViewer_Toolbars_Dock_Area_Right.Name = "_frmReportViewer_Toolbars_Dock_Area_Right";
            this._frmReportViewer_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 355);
            this._frmReportViewer_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmReportViewer_Toolbars_Dock_Area_Top
            // 
            this._frmReportViewer_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmReportViewer_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control;
            this._frmReportViewer_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._frmReportViewer_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmReportViewer_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frmReportViewer_Toolbars_Dock_Area_Top.Name = "_frmReportViewer_Toolbars_Dock_Area_Top";
            this._frmReportViewer_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(685, 25);
            this._frmReportViewer_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmReportViewer_Toolbars_Dock_Area_Bottom
            // 
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control;
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 380);
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.Name = "_frmReportViewer_Toolbars_Dock_Area_Bottom";
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(685, 0);
            this._frmReportViewer_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // frmReportViewer_BaoCaoChiTietXeRaVao
            // 
            this.ClientSize = new System.Drawing.Size(685, 380);
            this.Controls.Add(this.ultraTabControl1);
            this.Controls.Add(this._frmReportViewer_Toolbars_Dock_Area_Left);
            this.Controls.Add(this._frmReportViewer_Toolbars_Dock_Area_Right);
            this.Controls.Add(this._frmReportViewer_Toolbars_Dock_Area_Bottom);
            this.Controls.Add(this._frmReportViewer_Toolbars_Dock_Area_Top);
            this.KeyPreview = true;
            this.Name = "frmReportViewer_BaoCaoChiTietXeRaVao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmReportViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportViewer_KeyDown);
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtfilterBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmReportViewer_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmReportViewer_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmReportViewer_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmReportViewer_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        public cenControls.saTextBox txtfilterBox;
        public cenControls.saNonUpdateGrid ugBaoCao;
    }
}
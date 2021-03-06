using cenControls;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenBase.BaseForms
{
    public partial class frmBaseGridUpdate : Form
    {
        public DataTable dt;
        public String TableName;
        public Boolean Saved = false, GridValidation = true;
        public Int32 RowID = 0;
        BindingSource bs;
        public frmBaseGridUpdate()
        {
            InitializeComponent();
            saUpdateGrid.ConfirmDelete = false;
        }
        private void frmQuanLy_Load(object sender, EventArgs e)
        {
            saUpdateGrid.AfterCellUpdate += new CellEventHandler(ugChiTiet_AfterCellUpdate);
            saUpdateGrid.AfterRowInsert += new RowEventHandler(ugChiTiet_AfterRowInsert);

            bs = new BindingSource
            {
                DataSource = dt
            };

            saUpdateGrid.DataSource = bs;
            //saUpdateGrid.DisplayLayout.Bands[0].Columns["ID"].Hidden = false;
            saUpdateGrid.SetEditableState(false);
            if (dt.Rows.Count == 0)
            {
                bs.AddNew();
                saUpdateGrid.Rows[0].Activate();
                saUpdateGrid.ActiveRow.Cells["ID"].Value = RowID + dt.Rows.Count + 1;
            }
        }
        private void UltraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString().ToUpper())
            {
                case "BTOK":
                    bs.EndEdit();
                    UpdateData();
                    if (!Saved) return;
                    this.Close();
                    break;
                case "BTXOADONG":
                    if (saUpdateGrid.ActiveRow == null) return;
                    int i = saUpdateGrid.ActiveRow.Index;
                    saUpdateGrid.ActiveRow.Delete();
                    if (i > 0) i -= 1;
                    if (i <= saUpdateGrid.Rows.Count - 1)
                    {
                        saUpdateGrid.Focus();
                        saUpdateGrid.Rows[i].Activate();
                        saUpdateGrid.PerformAction(UltraGridAction.EnterEditMode);
                    }
                    break;
                case "BTTHEMDONG":
                    GridValidation = false;
                    bs.AddNew();
                    saUpdateGrid.Focus();
                    saUpdateGrid.DisplayLayout.Rows[saUpdateGrid.DisplayLayout.Rows.Count - 1].Activate();
                    saUpdateGrid.ActiveRow.Cells["ID"].Value = RowID + cenBase.Classes.ChungTu.MaxTempID(dt);
                    foreach (UltraGridColumn ugCol in saUpdateGrid.DisplayLayout.Bands[0].Columns)
                    {
                        if (!ugCol.Hidden)
                        {
                            saUpdateGrid.ActiveCell = saUpdateGrid.ActiveRow.Cells[ugCol.Key];
                            saUpdateGrid.ActiveCell.Activate();
                            break;
                        }
                    }
                    GridValidation = true;
                    break;
            }
        }

        protected virtual void List()
        {
        }
        protected virtual void UpdateData()
        {
        }

        private void frmBaseGridUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                DialogResult dlg = cenCommon.cenCommon.QuestionMessage("Bạn có muốn lưu lại dữ liệu trước khi thoát?", 1);
                switch (dlg)
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        dt.RejectChanges();
                        bs.EndEdit();
                        break;
                    case System.Windows.Forms.DialogResult.Yes:
                        bs.EndEdit();
                        UpdateData();
                        if (!Saved) e.Cancel = true;
                        break;
                }
            }
        }

        private void ugChiTiet_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell == null) return;
            if (GridValidation)
                gridColumnDataProcess((saUpdateGrid)sender, e.Cell, out GridValidation, true);
        }
        private void ugChiTiet_AfterRowInsert(object sender, RowEventArgs e)
        {
            e.Row.Cells["ID"].Value = RowID + cenBase.Classes.ChungTu.MaxTempID(dt);
        }
        //Valid các ô nhập dữ liệu trên lưới
        protected virtual void gridColumnDataProcess(UltraGrid ug, UltraGridCell uCell, out Boolean GridValidation, Boolean ShowLookUp)
        {
            GridValidation = true;
        }

        private void frmBaseGridUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}

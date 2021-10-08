﻿using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctDonHangValidDebitNote : Form
    {
        public object IDDanhMucChungTu = null;
        public DataTable dataTable = null;
        public bool OK = false;
        public object validValue = null, value = null;
        const string validColumn = "DebitNote";
        const string valueColumn = "ID";
        bool Escape = false;
        public BindingSource bsDanhMuc;
        public List<DataRow> dataRows;
        ctDonHangBUS bus;
        public frm_ctDonHangValidDebitNote()
        {
            InitializeComponent();
        }
        private void FormClose(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void FormLoad(object sender, EventArgs e)
        {
            LoadDanhMuc(false);
            txtFilterBox.Value = validValue;
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Visible = false;
        }
        /// <summary>
        /// Nạp danh mục
        /// </summary>
        private void LoadDanhMuc(bool Refresh)
        {
            bool Xem = false, Them = false, Sua = false, Xoa = false;
            if (!cenCommon.cenCommon.IsNull(IDDanhMucChungTu))
                DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out Xem, out Them, out Sua, out Xoa);
            if (!cenCommon.cenCommon.IsNull(IDDanhMucChungTu) && !cenCommon.GlobalVariables.isAdmin && !Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xem dữ liệu danh mục này!");
                bus = new ctDonHangBUS();
                dataTable = bus.ValidDebitNote(null, null);
                //dataTable.TableName = DanhMucXe.tableName;
            }
            if (Refresh)
            {
                bus = new ctDonHangBUS();
                dataTable = bus.ValidDebitNote(IDDanhMucChungTu, null);
                //dataTable.TableName = DanhMucXe.tableName;
            }
            bsDanhMuc = new BindingSource
            {
                DataSource = dataTable
            };
            ug.FixedColumnsList = "[DebitNote][So]";
            ug.DataSource = bsDanhMuc;
            ug.DisplayLayout.Override.FilterUIType = FilterUIType.Default; //Biểu tượng lọc đặt trên tiêu đề cột
            if (txtFilterBox.Value != null)
            {
                ug.DisplayLayout.Bands[0].Columns[validColumn].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
                ug.DisplayLayout.Bands[0].ColumnFilters[validColumn].FilterConditions.Clear();
                ug.DisplayLayout.Bands[0].ColumnFilters[validColumn].FilterConditions.Add(FilterComparisionOperator.Contains, txtFilterBox.Text);
            }
            if (ug.Rows.GetFilteredInNonGroupByRows().Count() > 0)
            {
                ug.ActiveRow = ug.Rows.GetFilteredInNonGroupByRows()[0];
                ug.PerformAction(UltraGridAction.ActivateCell);
            }
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// Refresh danh mục
        /// </summary>
        protected void RefreshDanhMuc()
        {
            LoadDanhMuc(true);
        }
        /// <summary>
        /// Bắt phím chức năng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Escape):
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }
        /// <summary>
        /// Đóng form
        /// </summary>
        protected void CloseForm()
        {
            if (Escape || bsDanhMuc.Current == null)
            {
                value = null;
                return;
            }
            value = ((DataRowView)bsDanhMuc.Current).Row[valueColumn];
            dataRows = new List<DataRow>();
            dataRows.Add(((DataRowView)bsDanhMuc.Current).Row);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Return | keyData == Keys.Escape)
            {
                Escape = (keyData == Keys.Escape);
                this.DialogResult = DialogResult.OK;
                return true;
            }
            else
                return base.ProcessDialogKey(keyData);
        }
        private void Ug_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmDanhMucXeValid_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseForm();
        }

        private void filterBoxValueChanged(object sender, EventArgs e)
        {
            ug.DisplayLayout.Bands[0].Columns[validColumn].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ug.DisplayLayout.Bands[0].ColumnFilters[validColumn].FilterConditions.Clear();
            ug.DisplayLayout.Bands[0].ColumnFilters[validColumn].FilterConditions.Add(FilterComparisionOperator.Contains, txtFilterBox.Text);
            Text = ug.Rows.GetFilteredInNonGroupByRows().Count().ToString();
            if (ug.Rows.GetFilteredInNonGroupByRows().Count() > 0)
            {
                ug.Rows.GetFilteredInNonGroupByRows()[0].Selected = true;
                ug.Rows.GetFilteredInNonGroupByRows()[0].Activated = true;
            }
        }
        private void UltraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString().ToUpper())
            {
                case "BTTHEM":
                    InsertDanhMuc();
                    break;
                case "BTSUA":
                    UpdateDanhMuc();
                    break;
                case "BTTAILAI":
                    LoadDanhMuc(true);
                    break;
            }
        }
        private void InsertDanhMuc()
        {
        }
        private void UpdateDanhMuc()
        {
        }
    }
}

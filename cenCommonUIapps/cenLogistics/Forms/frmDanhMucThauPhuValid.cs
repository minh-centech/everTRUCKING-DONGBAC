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
    public partial class frmDanhMucThauPhuValid : Form
    {
        public object IDDanhMucLoaiDoiTuong = null;
        public DataTable dataTable = null;
        public bool OK = false;
        public object validValue = null, value = null;
        const string validColumn = "Ma";
        const string valueColumn = "ID";
        public bool Them = true, Sua = true, Xoa = false;
        bool Escape = false;
        public BindingSource bsDanhMuc;
        public List<DataRow> dataRows;
        DanhMucThauPhuBUS bus;
        public frmDanhMucThauPhuValid()
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
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = Them;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Visible = Sua;
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Visible = Xoa;
        }
        /// <summary>
        /// Nạp danh mục
        /// </summary>
        private void LoadDanhMuc(bool Refresh)
        {
            bool Xem = false, Them = false, Sua = false, Xoa = false;
            if (!cenCommon.cenCommon.IsNull(IDDanhMucLoaiDoiTuong))
                DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out Xem, out Them, out Sua, out Xoa);
            if (!cenCommon.cenCommon.IsNull(IDDanhMucLoaiDoiTuong) && !cenCommon.GlobalVariables.isAdmin && !Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xem dữ liệu danh mục này!");
                bus = new DanhMucThauPhuBUS();
                dataTable = bus.List(null, -1, null, null);
                //dataTable.TableName = DanhMucXe.tableName;
            }
            if (Refresh)
            {
                bus = new DanhMucThauPhuBUS();
                dataTable = bus.List(null, -1, null, null);
                //dataTable.TableName = DanhMucXe.tableName;
            }
            bsDanhMuc = new BindingSource
            {
                DataSource = dataTable
            };
            ug.FixedColumnsList = "[Ma][Ten]";
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
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm mới dữ liệu danh mục này!");
                return;
            }
            frmDanhMucThauPhuUpdate frmUpdate = new frmDanhMucThauPhuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                dataTable = dataTable
            };
            frmUpdate.ShowDialog();
            if (ug.Rows.GetFilteredInNonGroupByRows().Count() > 0)
            {
                ug.Rows.GetFilteredInNonGroupByRows()[0].Selected = true;
                ug.Rows.GetFilteredInNonGroupByRows()[0].Activated = true;
            }
            frmUpdate.Dispose();
        }
        private void UpdateDanhMuc()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Sua)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền sửa dữ liệu danh mục này!");
                return;
            }
            if (bsDanhMuc == null || bsDanhMuc.Current == null) return;
            //frmDanhMucXeUpdate frmUpdate = new frmDanhMucXeUpdate
            //{
            //    CapNhat = 2,
            //    dataTable = dataTable,
            //    dataRow = ((DataRowView)bsDanhMuc.Current).Row,
            //    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong
            //};
            //frmUpdate.ShowDialog();
        }
    }
}

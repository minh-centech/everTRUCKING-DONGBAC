﻿using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
namespace cenReportController
{
    public partial class frmDataValid : Form
    {
        public Func<DataTable> validProcedure = null;
        public Func<DataRow> insertProcedure = null;

        public string validColumn = "";
        
        public bool Them = true, Sua = true, Xoa = false, Escape = false;
        
        public DataRow dataRow;

        public static DataTable dtValid;

        public object validValue;

        BindingSource bsData;
        
        public frmDataValid()
        {
            InitializeComponent();
        }
        private void FormClose(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormLoad(object sender, EventArgs e)
        {
            LoadDanhMuc();
            txtFilterBox.Value = validValue;
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = Them;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Visible = Sua;
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Visible = Xoa;
        }
        /// <summary>
        /// Nạp danh mục
        /// </summary>
        private void LoadDanhMuc()
        {
            //
            dtValid = validProcedure();
            //
            ug.FixedColumnsList = "[" + validColumn  +  "]";
            bsData = new BindingSource() { DataSource = dtValid };
            ug.DataSource = bsData;
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
            LoadDanhMuc();
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
                    Close();
                    break;
            }
        }
        /// <summary>
        /// Đóng form
        /// </summary>
        protected void CloseForm()
        {
            if (Escape || bsData.Current == null)
            {
                dataRow = null;
                return;
            }
            dataRow = ((DataRowView)bsData.Current).Row;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Return | keyData == Keys.Escape)
            {
                Escape = (keyData == Keys.Escape);
                Close();
                return true;
            }
            else
                return base.ProcessDialogKey(keyData);
        }
        private void Ug_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            Close();
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
                    LoadDanhMuc();
                    break;
            }
        }
        private void InsertDanhMuc()
        {
            if (insertProcedure != null)
            {
                dataRow = insertProcedure();
                if (dataRow != null) dtValid.ImportRow(dataRow); else LoadDanhMuc();
            }
        }
        //Chỉ dùng khi insert trong form valid
        public static void InsertToList(DataTable dtUpdate)
        {
            dtValid.Merge(dtUpdate);
        }
        private void UpdateDanhMuc()
        {
            //Nếu có trong DTO thì gọi form tương ứng, nếu ko thì gọi customs
        }
    }
}

﻿using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace cenBase.Forms
{
    public partial class frmChungTuList : Form
    {
        public List<SqlParameter> ThamSoListChungTu = new List<SqlParameter>();

        public Boolean List2Print = false;//List để in nhiều chứng từ
        public Boolean UpdateData = true; //Có update ngược lại form chứng từ hay không

        public Int32 SoLuongChungTu = 0;
        public Form frmMDIParent = null;

        public String IDDanhMucChungTu = "";

        public DataSet dsChungTu = null;

        public Boolean CheckChungTu = false;
        public Boolean OK = false;
        public String IDChungTu = "";
        public DataRow drChungTu = null;

        BindingSource bsChungTu;
        BindingSource bsChungTuChiTiet;

        public frmChungTuList()
        {
            InitializeComponent();
        }
        private void frmBaseListChungTu_Load(object sender, EventArgs e)
        {
            LoadChungTu();
        }
        /// <summary>
        /// Load danh sách chứng từ
        /// </summary>
        public void LoadChungTu()
        {
            //Xoá cột checkcolumn nếu tồn tại
            if (dsChungTu.Tables["dtChungTu"].Columns.Contains("CheckColumn"))
                dsChungTu.Tables["dtChungTu"].Columns.Remove("CheckColumn");
            //Xoá cột checkcolumn nếu tồn tại
            if (dsChungTu.Tables["dtChungTuChiTiet"].Columns.Contains("CheckColumn"))
                dsChungTu.Tables["dtChungTuChiTiet"].Columns.Remove("CheckColumn");
            //Nếu có check chứng từ thì add checkcolumn vào DataSet
            if (CheckChungTu || List2Print)
            {
                DataColumn dcCheck = null;
                dcCheck = new DataColumn("CheckColumn", typeof(Boolean));
                dcCheck.DefaultValue = false;
                dsChungTu.Tables["dtChungTu"].Columns.Add(dcCheck);
                dsChungTu.Tables["dtChungTu"].Columns["CheckColumn"].SetOrdinal(0);
                ugChungTu.AddCheckBoxColumn = CheckChungTu || List2Print;
                if (!List2Print) //Nếu List để in thì không check chứng từ chi tiết
                {
                    dcCheck = new DataColumn("CheckColumn", typeof(Boolean));
                    dcCheck.DefaultValue = false;
                    dsChungTu.Tables["dtChungTuChiTiet"].Columns.Add(dcCheck);
                    dsChungTu.Tables["dtChungTuChiTiet"].Columns["CheckColumn"].SetOrdinal(0);
                    ugChungTuChiTiet.AddCheckBoxColumn = CheckChungTu;
                }
            }

            bsChungTu = new BindingSource();
            bsChungTu.DataSource = dsChungTu;
            bsChungTu.DataMember = "dtChungTu";

            bsChungTuChiTiet = new BindingSource();
            bsChungTuChiTiet.DataSource = bsChungTu;
            bsChungTuChiTiet.DataMember = "rltChungTuChiTiet";


            ugChungTu.DataSource = bsChungTu; ;
            ugChungTuChiTiet.DataSource = bsChungTuChiTiet;

            //ugChungTu.DisplayLayout.Bands[0].Columns["ID"].Hidden = false;

        }
        /// <summary>
        /// Chọn chứng từ
        /// </summary>
        private void ChonChungTu()
        {
            //Xoá cột checkcolumn nếu tồn tại
            if (dsChungTu.Tables["dtChungTu"].Columns.Contains("CheckColumn"))
                dsChungTu.Tables["dtChungTu"].Columns.Remove("CheckColumn");
            //Xoá cột checkcolumn nếu tồn tại
            if (dsChungTu.Tables["dtChungTuChiTiet"].Columns.Contains("CheckColumn"))
                dsChungTu.Tables["dtChungTuChiTiet"].Columns.Remove("CheckColumn");
        }
        /// <summary>
        /// Đóng form, chọn chứng từ nếu có
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Chọn chứng từ khi double-click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ugChungTu_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            OK = true;
            Close();
        }
        /// <summary>
        /// Chọn chứng từ khi bấm Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBaseListChungTu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Close();
            }
        }
        private void frmBaseListChungTu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ugChungTu.ActiveRow != null)
            {
                IDChungTu = ugChungTu.ActiveRow.Cells["ID"].Value.ToString();
                if (bsChungTu.Current != null)
                {
                    drChungTu = ((DataRowView)bsChungTu.Current).Row;
                }
                ChonChungTu();
            }
        }
        /// <summary>
        /// Check, uncheck Detail grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ugChungTu_AfterCellUpdate(object sender, CellEventArgs e)
        {
            //Check, Uncheck Detail Grid
            if (e.Cell.Column.Key == "CheckColumn" && ugChungTuChiTiet.Rows.Count > 0)
            {
                if (Convert.ToBoolean(e.Cell.Value) == true)
                    e.Cell.Appearance.ForeColor = Color.Black;
                if (!List2Print)
                {
                    foreach (UltraGridRow ugr in ugChungTuChiTiet.Rows)
                    {
                        ugr.Cells["CheckColumn"].Value = e.Cell.Value;
                        ugChungTuChiTiet.UpdateData();
                    }
                }
            }
        }
        /// <summary>
        /// Kiểm tra giá trị của checkbox khi user click vào checkcolumn header để tự động check/uncheck grid chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ugChungTu_MouseUp(object sender, MouseEventArgs e)
        {
            if (ugChungTu.Rows.Count > 0)
            {
                UltraGrid grid = (UltraGrid)sender;
                Infragistics.Win.UIElement element = grid.DisplayLayout.UIElement.LastElementEntered;
                if (element != null)
                {
                    Infragistics.Win.UltraWinGrid.ColumnHeader header = element.GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader)) as Infragistics.Win.UltraWinGrid.ColumnHeader;
                    if (header != null && header.Column.Key.ToUpper() == "CHECKCOLUMN")
                    {
                        String strState = header.Column.GetHeaderCheckedState(grid.Rows).ToString().ToUpper();
                        if (strState != "INTERMEDIATE")
                        {
                            Boolean bChecked = (strState == "CHECKED");
                            if (!List2Print)
                            {
                                foreach (DataRow drChungTuChiTiet in dsChungTu.Tables["dtChungTuChiTiet"].Rows)
                                {
                                    drChungTuChiTiet["CheckColumn"] = bChecked;
                                }
                                dsChungTu.Tables["dtChungTuChiTiet"].AcceptChanges();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// In nhiều chứng từ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataRow[] drChungTuInS = dsChungTu.Tables["dtChungTu"].Select("CheckColumn=True");
            if (drChungTuInS != null && drChungTuInS.Length > 0)
            {
                //clsBaseChungTu.InNhieuChungTu(IDDanhMucChungTu, IDLoaiManHinh, TenChungTu, drChungTuInS, dsChungTu, this.frmMDIParent, true);
                Close();
            }
            else
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn chưa chọn chứng từ để in!");
        }

        private void UltraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToUpper())
            {
                case "BTCHON":
                    OK = true;
                    Close();
                    break;
                case "BTXOA":
                    //XoaChungTu();
                    break;
                case "BTTIMLAI":
                    filterChungTu();
                    break;
            }
        }
        public void filterChungTu()
        {
            //frmChungTufilter frmfilterParameters = new frmChungTufilter();
            //frmfilterParameters.IDDanhMucChungTu = IDDanhMucChungTu;
            //frmfilterParameters.ShowDialog();
            //if (frmfilterParameters.OK)
            //{
            //    DataTable dtThamSo = frmfilterParameters.dtThamSo;
            //    frmfilterParameters.Dispose();
            //    ChungTu ChungTu = new ChungTu(TenBang, TenBangChiTiet);
            //    dsChungTu = new DataSet();
            //    dsChungTu = ChungTu.Select(dtThamSo);
            //    ChungTu.Dispose();
            //    if (dsChungTu != null && dsChungTu.Tables["dtChungTu"].Rows.Count > 0)
            //    {
            //        LoadChungTu();
            //    }
            //    else
            //    {
            //        cenCommon.cenCommon.ErrorMessageOkOnly("Không tìm thấy chứng từ nào");
            //    }
            //}
        }
    }
}

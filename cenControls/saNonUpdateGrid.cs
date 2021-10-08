﻿using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace cenControls
{
    public partial class saNonUpdateGrid : UltraGrid
    {
        public Int16 CustomizeColumns = 0; //Có tùy biến lại cột hay không.
        public Boolean isReport = false; //Có phải grid báo cáo hay không

        public Boolean AddCheckBoxColumn = false;
        public Boolean AddSummaryRow = false;

        public String HiddenColumnsList = "";
        public String FixedColumnsList = "";
        public String SummaryColumnsList = "";

        public String LoaiDanhMuc = "";
        public Boolean RowCount = true;

        public saNonUpdateGrid()
        {
            InitializeComponent();
            CustomizeColumns = 0;
            DisplayLayout.TabNavigation = TabNavigation.NextControl;
            DisplayLayout.LoadStyle = LoadStyle.LoadOnDemand;
        }
        protected override void OnInitializeLayout(InitializeLayoutEventArgs e)
        {
            if (CustomizeColumns == 0)
            {
                CustomizeColumns = 1;
                //Vì Layout của Grid được tự động lưu lại sau khi khởi tạo lần đầu tiên,
                //ên không cần thiết phải tùy biến lại sau mỗi lần đổi datasource để tăng tốc độ hiển thị
                //Chỉ khởi tạo lại khi DataSource mới khác cấu trúc với DataSource cũ.
                //Khởi tạo lại Layout
                this.DisplayLayout.Reset();
                this.Text = "";

                this.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;

                this.UseAppStyling = true;
                this.UseFlatMode = Infragistics.Win.DefaultableBoolean.Default;
                this.UseOsThemes = Infragistics.Win.DefaultableBoolean.Default;
                this.RowUpdateCancelAction = RowUpdateCancelAction.CancelUpdate;
                this.DisplayLayout.UseFixedHeaders = true;
                this.DisplayLayout.Override.WrapHeaderText = DefaultableBoolean.True;
                //
                base.DisplayLayout.RowSelectorImages.ActiveRowImage = null;
                base.DisplayLayout.RowSelectorImages.DataChangedImage = null;
                base.DisplayLayout.RowSelectorImages.AddNewRowImage = null;
                base.DisplayLayout.RowSelectorImages.ActiveAndDataChangedImage = null;
                base.DisplayLayout.RowSelectorImages.ActiveAndAddNewRowImage = null;
                //this.DisplayLayout.Override.ActiveRowAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True; //Đặt chữ đậm cho dòng hiện tại
                this.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Black; //Đặt chữ màu đen cho dòng hiện tại
                this.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn; //Không tự động căn chỉnh độ rộng cột
                this.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid; //Loại đường biên
                this.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true; //Hiển thị cả những dòng trống
                //this.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.True; //WrapText
                this.DisplayLayout.Override.AllowColMoving = AllowColMoving.NotAllowed;
                this.DisplayLayout.Override.AllowColSwapping = AllowColSwapping.NotAllowed;
                this.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow; //Biểu tượng lọc đặt trên tiêu đề cột
                this.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
                this.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Default; //(!isReport) ? Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle : Infragistics.Win.UltraWinGrid.HeaderClickAction.Default; //Sắp xếp khi click tiêu đề cột
                this.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed; //Tự do thay đổi kích thước dòng
                this.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single; //Không cho phép chọn nhiều dồng
                if (AddSummaryRow)
                {
                    this.DisplayLayout.Override.SummaryFooterAppearance.BackColor = System.Drawing.Color.LightSteelBlue; //Màu nền cho dòng tổng cộng
                    this.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False; //Hiển thị caption của dòng tổng cộng
                    this.DisplayLayout.Override.SummaryValueAppearance.BackColor = System.Drawing.Color.LightSteelBlue; //Màu nền của dòng tổng cộng
                    this.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True; //Màu nền của dòng tổng cộng
                    this.DisplayLayout.Override.SummaryValueAppearance.FontData.SizeInPoints = 9; //Màu nền của dòng tổng cộng
                }
                //this.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True; //WrapText header
                this.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand; //Chế độ xem bảng đơn
                //this.DisplayLayout.Override.RowAppearance.BorderColor = Color.LightGray;
                //Customize các thuộc tính giao diện cột khi DataSource được thiết lập;
                if (e.Layout.Bands.Count > 0)
                {
                    UltraGridBand saBand = e.Layout.Bands[0]; //Chỉ lấy band 0 vì grid được đặt ở chế độ view single band
                    saBand.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.Yes; //Cho phép add dòng mới bằng code
                    saBand.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False; //Không cho phép xóa dòng
                    saBand.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                    saBand.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
                    //saBand.Override.RowSizing = RowSizing.AutoFree;
                    saBand.Summaries.Clear();
                    //saBand.Override.RowAlternateAppearance.BorderColor = System.Drawing.Color.LightGray; //Tô màu đường viền
                    //saBand.Override.RowAppearance.BorderColor = System.Drawing.Color.LightGray; //Tô màu đường viền
                    //Row selector
                    this.DisplayLayout.Bands[0].Override.ActiveCellAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    this.DisplayLayout.Bands[0].Header.Appearance.BorderColor = Color.LightGray;
                    this.DisplayLayout.Bands[0].Header.Appearance.BackColor = Color.LightGray;
                    this.DisplayLayout.Bands[0].Header.Appearance.BorderColor = Color.LightGray;
                    this.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    this.DisplayLayout.Override.RowSelectorAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                    this.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
                    this.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
                    this.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;

                    //Định dạng các cột
                    foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugcol in saBand.Columns)
                    {
                        String ColumnKey = ugcol.Key.ToUpper(); //(ugcol.Key.IndexOf('_') >= 0) ? ugcol.Key.Substring(ugcol.Header.Caption.IndexOf('_') + 1).ToUpper() : ugcol.Key.ToUpper();
                        ugcol.CellActivation = Activation.NoEdit;
                        ugcol.CellClickAction = CellClickAction.CellSelect;
                        ugcol.CellAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                        ugcol.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                        ugcol.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.StartsWith;
                        ugcol.CellAppearance.BorderColor = Color.LightGray;
                        ugcol.Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
                        //Không hiển thị một số cột đặc biệt: Mật khẩu, ID, CreateDate, EditDate
                        if ((ColumnKey.StartsWith("ID") && ColumnKey != "IDDANHMUCTACNGHIEPCONTAINERKHONGOAIQUAN")
                            || ColumnKey.StartsWith("MATKHAU")
                            || ColumnKey == "PHUONGPHAPTINH"
                            || ColumnKey == "PHAMVIAPDUNG"
                            || ColumnKey == "ROWID"
                            || ColumnKey == "RCOLOR"
                            || ColumnKey == "RBOLD")
                        {
                            ugcol.Hidden = true;
                        }
                        //Không hiển thị các cột trong HiddenColumnsList
                        if (HiddenColumnsList.ToUpper().IndexOf("[" + ColumnKey + "]") >= 0)
                        {
                            ugcol.Hidden = true;
                        }
                        ugcol.Header.Caption = cenCommon.cenCommon.TraTuDien(ColumnKey);
                        if (AddSummaryRow && (ColumnKey.StartsWith("SOTIEN")))
                        {
                            saBand.Summaries.Add(ugcol.Key.ToString(), Infragistics.Win.UltraWinGrid.SummaryType.Sum, saBand.Columns[ugcol.Key]);
                        }
                        if (FixedColumnsList.ToUpper().IndexOf("[" + ColumnKey + "]") >= 0)
                        {
                            ugcol.Header.Fixed = true;
                        }
                        cenCommon.cenCommon.SetGridColumnWidth(ugcol);
                        cenCommon.cenCommon.SetGridColumnMask(ugcol);
                    }
                    if (AddSummaryRow)
                    {
                        saBand.SummaryFooterCaption = ""; //Xóa caption mặc định dòng tổng cộng
                        //if (RowCount && saBand.Columns.Count > 0)
                        //{
                        //    saBand.Summaries.Add("Count", Infragistics.Win.UltraWinGrid.SummaryType.Count, saBand.Columns[CountPos]); //Hiển thị số lượng dòng trong lưới
                        //}
                        //Định dạng dòng tổng cộng
                        foreach (Infragistics.Win.UltraWinGrid.SummarySettings ugsum in saBand.Summaries)
                        {
                            if (ugsum.SummaryType != Infragistics.Win.UltraWinGrid.SummaryType.Count) ugsum.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                            if (ugsum.SummaryType == SummaryType.Count)
                                ugsum.DisplayFormat = "Count: {0:##,###;(##,###);-;-}";
                            if (cenCommon.GlobalVariables.TenCotSoLuong.ToUpper().IndexOf("(" + ugsum.Key.ToUpper() + ")") >= 0)
                                ugsum.DisplayFormat = "{0:" + cenCommon.GlobalVariables.FormatSoLuong + ";(" + cenCommon.GlobalVariables.FormatSoLuong + ");-;-}";
                            if (cenCommon.GlobalVariables.TenCotTrongLuong.ToUpper().IndexOf("(" + ugsum.Key.ToUpper() + ")") >= 0)
                                ugsum.DisplayFormat = "{0:" + cenCommon.GlobalVariables.FormatTrongLuong + ";(" + cenCommon.GlobalVariables.FormatTrongLuong + ");-;-}";
                            if (cenCommon.GlobalVariables.TenCotKhoiLuong.ToUpper().IndexOf("(" + ugsum.Key.ToUpper() + ")") >= 0)
                                ugsum.DisplayFormat = "{0:" + cenCommon.GlobalVariables.FormatKhoiLuong + ";(" + cenCommon.GlobalVariables.FormatKhoiLuong + ");-;-}";
                            if (ugsum.Key.ToUpper().StartsWith("SOTIEN"))
                                ugsum.DisplayFormat = "{0:" + cenCommon.GlobalVariables.FormatTien + ";(" + cenCommon.GlobalVariables.FormatTien + ");-;-}";
                            ugsum.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
                            ugsum.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                            ugsum.SummaryPosition = Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn;
                            if (ugsum.SummaryType != Infragistics.Win.UltraWinGrid.SummaryType.Count) ugsum.SummaryPositionColumn = ugsum.SourceColumn;
                            if (ugsum.SummaryType != Infragistics.Win.UltraWinGrid.SummaryType.Count) ugsum.SummaryType = Infragistics.Win.UltraWinGrid.SummaryType.Sum;
                        }
                    }
                    //Tạo 1 cột check box dùng để select
                    if (AddCheckBoxColumn && saBand.Columns.Count > 0)
                    {
                        if (!saBand.Columns.Exists("CheckColumn"))
                        {
                            saBand.Columns.Insert(saBand.Columns.Count, "CheckColumn");
                            saBand.Columns["CheckColumn"].Header.VisiblePosition = 0;
                        }
                        saBand.Columns["CheckColumn"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
                        saBand.Columns["CheckColumn"].Header.CheckBoxSynchronization = HeaderCheckBoxSynchronization.RowsCollection;
                        saBand.Columns["CheckColumn"].Header.CheckBoxAlignment = HeaderCheckBoxAlignment.Center;
                        saBand.Columns["CheckColumn"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                        saBand.Columns["CheckColumn"].DefaultCellValue = false;
                        saBand.Columns["CheckColumn"].CellActivation = Activation.AllowEdit;
                        saBand.Columns["CheckColumn"].CellClickAction = CellClickAction.Edit;
                        saBand.Columns["CheckColumn"].Header.Caption = "";
                        saBand.Columns["CheckColumn"].Width = 50;
                    }

                }
            }
        }
        protected override void OnBeforeRowsDeleted(BeforeRowsDeletedEventArgs e)
        {
            e.DisplayPromptMsg = false;
            base.OnBeforeRowsDeleted(e);
        }
        /// <summary>
        /// Tìm dòng trong lưới
        /// </summary>
        /// <param name="ColumnKey">Key của cột cần tìm</param>
        /// <param name="Value">Giá trị dạng chuỗi của cột cần tìm</param>
        public Boolean FindRow(string ColumnKey, string Value)
        {
            Boolean Found = false;
            //Kiểm tra cấu trúc, tên cột, số dòng
            if (this.DisplayLayout.Bands.Count > 0 && this.DisplayLayout.Bands[0].Columns.Exists(ColumnKey) && this.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in this.Rows)
                {
                    String FindValue = ugr.Cells[ColumnKey].Value.ToString();
                    if (FindValue == Value)
                    {
                        //Nếu tìm thấy
                        Found = true;
                        if (ActiveRow != null) this.ActiveRow.Selected = false;
                        this.ActiveRow = ugr;
                        this.ActiveRow.Activate();
                        this.ActiveRow.Selected = true;
                        break;
                    }
                }
            }
            return Found;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (AddCheckBoxColumn && DisplayLayout.Bands[0].Columns.Exists("CheckColumn") && ActiveRow != null && e.KeyCode == Keys.Space)
            //if (DisplayLayout.Bands[0].Columns.Exists("CheckColumn") && ActiveRow != null && e.KeyCode == Keys.Space)
            {
                if (ActiveRow.Cells["CheckColumn"].Value == DBNull.Value)
                    ActiveRow.Cells["CheckColumn"].Value = true;
                else
                    ActiveRow.Cells["CheckColumn"].Value = !Convert.ToBoolean(ActiveRow.Cells["CheckColumn"].Value);
                UpdateData();
            }
            base.OnKeyDown(e);
        }

        protected override void OnClickCell(ClickCellEventArgs e)
        {
            if (AddCheckBoxColumn && e.Cell != null && e.Cell.Column.Key == "CheckColumn")
            //if (e.Cell != null && e.Cell.Column.Key == "CheckColumn")
            {
                if (e.Cell.Value == DBNull.Value)
                    e.Cell.Value = true;
                else
                    e.Cell.Value = !(Convert.ToBoolean(e.Cell.Value));
                UpdateData();
            }
            base.OnClickCell(e);
        }
    }
}

﻿
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Windows.Forms;
namespace cenControls
{
    public partial class saUpdateGrid : UltraGrid
    {
        public int CustomizeColumns = 0; //Có tùy biến lại cột hay không.
        public Boolean IsModified = false;
        public Boolean ConfirmDelete = true;
        //public Boolean EnterByKey = false;
        public String LastUpdatedCellKey = "";

        public String ReadOnlyColumnsList = "";
        public String HiddenColumnsList = "";
        public String FixedColumnsList = "";
        public String SummaryColumnsList = "";

        public Boolean ReadOnly = false;


        public saUpdateGrid()
        {
            InitializeComponent();
            this.ExitEditModeOnLeave = true;
            //this.KeyActionMappings.Add(new GridKeyActionMapping(Keys.Enter,UltraGridAction.NextCellByTab,0,0,Infragistics.Win.SpecialKeys.AltShift,0));
        }
        protected override void OnInitializeLayout(InitializeLayoutEventArgs e)
        {
            if (CustomizeColumns == 0)
            {
                //Vì Layout của Grid được tự động lưu lại sau khi khởi tạo lần đầu tiên,
                //ên không cần thiết phải tùy biến lại sau mỗi lần đổi datasource để tăng tốc độ hiển thị
                //Chỉ khởi tạo lại khi DataSource mới khác cấu trúc với DataSource cũ.
                //Khởi tạo lại Layout
                this.DisplayLayout.Reset();
                this.Text = "";
                this.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell;

                this.DisplayLayout.UseFixedHeaders = true;

                this.DisplayLayout.Override.WrapHeaderText = DefaultableBoolean.True;

                this.UpdateMode = UpdateMode.OnUpdate;

                this.UseAppStyling = true;
                this.UseFlatMode = Infragistics.Win.DefaultableBoolean.Default;
                this.UseOsThemes = Infragistics.Win.DefaultableBoolean.Default;

                base.DisplayLayout.RowSelectorImages.ActiveRowImage = null;
                base.DisplayLayout.RowSelectorImages.DataChangedImage = null;
                base.DisplayLayout.RowSelectorImages.AddNewRowImage = null;
                base.DisplayLayout.RowSelectorImages.ActiveAndDataChangedImage = null;
                base.DisplayLayout.RowSelectorImages.ActiveAndAddNewRowImage = null;

                //this.DisplayLayout.Override.ActiveRowAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True; //Đặt chữ đậm cho dòng hiện tại
                //this.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Black; //Đặt chữ màu đen cho dòng hiện tại
                this.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn; //Tự động căn chỉnh độ rộng cột
                this.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid; //Loại đường biên
                this.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true; //Hiển thị cả những dòng trống
                //this.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.True; //WrapText
                //Kiểm tra xem có được phép lọc trên lưới cập nhật hay không?
                this.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow; //Biểu tượng lọc đặt trên tiêu đề cột
                //Thay đổi định dạng ActiveCell
                this.DisplayLayout.Override.AllowColMoving = AllowColMoving.NotAllowed;
                this.DisplayLayout.Override.AllowColSwapping = AllowColSwapping.NotAllowed;
                //this.DisplayLayout.Override.ActiveCellAppearance.ForeColor = Color.Black;
                this.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Default; //Sắp xếp khi click tiêu đề cột
                this.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed; //Tự do thay đổi kích thước dòng
                this.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single; //Không cho phép chọn nhiều dồng
                this.DisplayLayout.Override.SummaryFooterAppearance.BackColor = System.Drawing.Color.LightSteelBlue; //Màu nền cho dòng tổng cộng
                this.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False; //Hiển thị caption của dòng tổng cộng
                this.DisplayLayout.Override.SummaryValueAppearance.BackColor = System.Drawing.Color.LightSteelBlue; //Màu nền của dòng tổng cộng
                this.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True; //Màu nền của dòng tổng cộng
                this.DisplayLayout.Override.SummaryValueAppearance.FontData.SizeInPoints = 9; //Màu nền của dòng tổng cộng
                //this.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True; //WrapText header
                this.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand; //Chế độ xem bảng đơn
                //Customize các thuộc tính giao diện cột khi DataSource được thiết lập;
                if (e.Layout.Bands.Count > 0)
                {
                    UltraGridBand saBand = e.Layout.Bands[0]; //Chỉ lấy band 0 vì grid được đặt ở chế độ view single band

                    saBand.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;//Khi click cell thì select text và cho phép sửa
                                                                                                                      //saBand.Override.RowAlternateAppearance.BorderColor = System.Drawing.Color.LightGray; //Tô màu đường viền
                                                                                                                      //saBand.Override.RowAppearance.BorderColor = System.Drawing.Color.LightGray; //Tô màu đường viền
                                                                                                                      //Row selector

                    this.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    this.DisplayLayout.Override.RowSelectorAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                    this.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
                    this.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
                    this.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;

                    //Định dạng các cột
                    foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugcol in saBand.Columns)
                    {
                        String ColumnKey = ugcol.Key.ToUpper(); //(ugcol.Key.IndexOf('_') >= 0) ? ugcol.Key.Substring(ugcol.Header.Caption.IndexOf('_') + 1).ToUpper() : ColumnKey;
                        ugcol.CellAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                        ugcol.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                        ugcol.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.StartsWith;
                        ugcol.Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Left;
                        ugcol.Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
                        //Không hiển thị một số cột đặc biệt: Mật khẩu, ID, CreateDate, EditDate
                        if ((ColumnKey.StartsWith("ID") && ColumnKey != "IDDANHMUCTACNGHIEPCONTAINERKHONGOAIQUAN") || ColumnKey.StartsWith("MATKHAU"))
                        {
                            ugcol.Hidden = true;
                        }
                        //Không hiển thị các cột trong HiddenColumnsList
                        if (HiddenColumnsList.ToUpper().IndexOf("[" + ColumnKey + "]") >= 0)
                        {
                            ugcol.Hidden = true;
                        }
                        //Lấy chuỗi tương ứng với mã ngôn ngữ để hiển thị
                        ugcol.Header.Caption = cenCommon.cenCommon.TraTuDien(ugcol.Key);
                        if (ugcol.Key.Contains("$$"))
                            ugcol.Header.Caption = cenCommon.cenCommon.TraTuDien(ugcol.Key.Substring(ugcol.Key.IndexOf("$$") + 2));
                        //Nếu là cột chứa số tiền hoặc số lượng thì thêm tổng cộng
                        if (ColumnKey.StartsWith("SOTIEN"))
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

                    saBand.SummaryFooterCaption = ""; //Xóa caption mặc định dòng tổng cộng
                    //saBand.Summaries.Add(Infragistics.Win.UltraWinGrid.SummaryType.Count, saBand.Columns[CountPos]); //Hiển thị số lượng dòng trong lưới
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
                        if (cenCommon.GlobalVariables.TenCotTien.ToUpper().IndexOf("(" + ugsum.Key.ToUpper() + ")") >= 0)
                            ugsum.DisplayFormat = "{0:" + cenCommon.GlobalVariables.FormatTien + ";(" + cenCommon.GlobalVariables.FormatTien + ");-;-}";
                        if (ugsum.Key.ToUpper().StartsWith("SOTIEN"))
                            ugsum.DisplayFormat = "{0:" + cenCommon.GlobalVariables.FormatTien + ";(" + cenCommon.GlobalVariables.FormatTien + ");-;-}";
                        ugsum.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
                        ugsum.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                        ugsum.SummaryPosition = Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn;
                        if (ugsum.SummaryType != Infragistics.Win.UltraWinGrid.SummaryType.Count) ugsum.SummaryPositionColumn = ugsum.SourceColumn;
                        if (ugsum.SummaryType != Infragistics.Win.UltraWinGrid.SummaryType.Count) ugsum.SummaryType = Infragistics.Win.UltraWinGrid.SummaryType.Sum;
                    }

                }
            }
        }
        /// <summary>
        /// Thay đổi trạng thái readonly của lưới
        /// </summary>
        /// <param name="ReadOnly"></param>
        public void SetEditableState(Boolean bReadOnly)
        {
            ReadOnly = bReadOnly;
            PerformAction(UltraGridAction.ExitEditMode);
            if (ActiveCell != null)
                PerformAction(UltraGridAction.DeactivateCell);
            UltraGridBand saBand = this.DisplayLayout.Bands[0];

            saBand.Override.AllowAddNew = (!ReadOnly) ? Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom : Infragistics.Win.UltraWinGrid.AllowAddNew.Yes; //Cho phép add dòng mới bằng code
            saBand.Override.AllowDelete = (!ReadOnly) ? Infragistics.Win.DefaultableBoolean.True : Infragistics.Win.DefaultableBoolean.False; //Cho phép xóa dòng
            saBand.Override.AllowUpdate = (!ReadOnly) ? Infragistics.Win.DefaultableBoolean.True : Infragistics.Win.DefaultableBoolean.False; //Cho phép sửa dòng

            saBand.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            saBand.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            saBand.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;

            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugcol in saBand.Columns)
            {
                if (!ugcol.Hidden && ReadOnlyColumnsList.ToUpper().IndexOf("[" + ugcol.Key.ToUpper() + "]") < 0)
                {
                    ugcol.TabStop = true;
                    ugcol.CellActivation = (!ReadOnly) ? Activation.AllowEdit : Activation.NoEdit;
                    ugcol.CellClickAction = (!ReadOnly) ? CellClickAction.EditAndSelectText : CellClickAction.CellSelect;
                }
                else
                {
                    ugcol.TabStop = false;
                    ugcol.CellActivation = Activation.NoEdit;
                    ugcol.CellClickAction = CellClickAction.CellSelect;
                }
            }
        }
        protected override void OnBeforeRowsDeleted(BeforeRowsDeletedEventArgs e)
        {
            e.DisplayPromptMsg = false;
            if (ReadOnly)
            {
                e.Cancel = true;
            }
            else
            {
                if (!ConfirmDelete) { e.Cancel = false; return; }
                DialogResult dlg = MessageBox.Show("Bạn đã chọn 1 dòng để xóa, Bạn có chắc chắn không?", "Chú ý!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.No) e.Cancel = true;
            }
        }
        protected override void OnCellChange(CellEventArgs e)
        {
            IsModified = true;
            base.OnCellChange(e);
        }
        protected override void OnAfterRowsDeleted()
        {
            IsModified = true;
            base.OnAfterRowsDeleted();
        }
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    if ((keyData == Keys.Return | keyData == Keys.Tab) && keyData != Keys.Shift)
        //    {
        //        return base.ProcessDialogKey(Keys.Tab);
        //    }
        //    else
        //        return base.ProcessDialogKey(keyData);
        //}
        public UltraGridColumn FirstVisibleColumn()
        {
            UltraGridColumn fvc = null;
            foreach (UltraGridColumn ugc in this.DisplayLayout.Bands[0].Columns)
            {
                if (ugc.Hidden == false)
                {
                    fvc = ugc;
                    break;
                }
            }
            return fvc;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            UltraGridCell activeCell = this?.ActiveCell;

            // if there is an active cell, its not in edit mode and can enter edit mode
            if (null != activeCell && false == activeCell.IsInEditMode && activeCell.CanEnterEditMode)
            {
                // if the character is not a control character
                if (char.IsControl(e.KeyChar) == false)
                {
                    // try to put cell into edit mode
                    PerformAction(UltraGridAction.EnterEditMode);

                    // if this cell is still active and it is in edit mode...
                    if (ActiveCell == activeCell && activeCell.IsInEditMode)
                    {
                        // get its editor
                        EmbeddableEditorBase editor = this.ActiveCell.EditorResolved;
                        // if the editor supports selectable text
                        if (editor.SupportsSelectableText)
                        {
                            // select all the text so it can be replaced
                            editor.SelectionStart = 0;
                            editor.SelectionLength = editor.TextLength;

                            if (editor is EditorWithMask)
                            {
                                // just clear the selected text and let the grid
                                // forward the keypress to the editor
                                editor.SelectedText = string.Empty;
                                SendKeys.Send(e.KeyChar.ToString());
                            }
                            else
                            {
                                // then replace the selected text with the character
                                editor.SelectedText = new string(e.KeyChar, 1);
                                // mark the event as handled so the grid doesn't process it
                                e.Handled = true;
                            }
                        }
                    }
                }
            }
            else
                base.OnKeyPress(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (!e.Shift && !e.Control && !e.Alt)
                        SendKeys.Send("{TAB}");
                    break;
                //case Keys.F9:
                //    DisplayLayout.Bands[0].AddNew();
                //    PerformAction(UltraGridAction.EnterEditMode);
                //    break;
                //case Keys.F11:
                //    if (ActiveRow != null)
                //        ActiveRow.Delete();
                //    if (Rows.Count == 0)
                //    {
                //        DisplayLayout.Bands[0].AddNew();
                //    }
                //    PerformAction(UltraGridAction.EnterEditMode);
                //    break;
                case Keys.Up:
                    PerformAction(UltraGridAction.ExitEditMode, false, false);
                    PerformAction(UltraGridAction.AboveCell, false, false);
                    e.Handled = true;
                    PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case Keys.Down:
                    PerformAction(UltraGridAction.ExitEditMode, false, false);
                    PerformAction(UltraGridAction.BelowCell, false, false);
                    e.Handled = true;
                    PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
        protected override void OnEnter(EventArgs e)
        {
            if (ActiveRow != null)
                ActiveRow.Activate();
            else if (Rows.Count > 0) Rows[0].Activate();
            if (ActiveCell != null)
                ActiveCell.Activate();
            else if (ActiveRow != null) ActiveRow.Cells[FirstVisibleColumn().Key].Activate();
            base.OnEnter(e);
        }
    }
}

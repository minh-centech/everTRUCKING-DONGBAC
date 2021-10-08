using cenControls;
using cenDTO.DatabaseCore;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucBaoCaoUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        cenDTO.DatabaseCore.DanhMucBaoCao obj = null;
        public object DefaultValue;
        public frmDanhMucBaoCaoUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucBaoCao";
        }
        protected override void SaveData(bool AddNew)
        {
            if (Save())
            {
                if (!AddNew)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    CapNhat = 1;
                    //Xóa text box
                    txtMa.Value = null;
                    txtTen.Value = null;
                    txtTenStore.Value = null;
                    cboKhoGiay.Value = 1;
                    cboKieuIn.Value = 1;
                    txtChucDanhKi.Value = null;
                    txtDienGiaiKi.Value = null;
                    txtTenNguoiKi.Value = null;
                    txtMaDanhMucBaoCaoThamChieu.ID = null;
                    txtMaDanhMucBaoCaoThamChieu.Value = null;
                    txtTenDanhMucBaoCaoThamChieu.Value = null;
                    txtMaDanhMucBaoCaoCopyCot.ID = null;
                    txtMaDanhMucBaoCaoCopyCot.Value = null;
                    txtTenDanhMucBaoCaoCopyCot.Value = null;
                    txtFileExcelMau.Value = null;
                    txtSheetExcelMau.Value = null;
                    txtSoDongBatDau.Value = null;
                    txtMaDanhMucNhomBaoCao.ID = null;
                    txtMaDanhMucNhomBaoCao.Value = null;
                    txtTenDanhMucNhomBaoCao.Value = null;
                    txtTenDanhMucBaoCaoCopyCot.Enabled = true;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucBaoCao
                {
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    ReportProcedureName = txtTenStore.Value,
                    FixedColumnList = txtFixedColumn.Value,
                    KhoGiay = cboKhoGiay.Value,
                    HuongIn = cboKieuIn.Value,
                    ChucDanhKy = txtChucDanhKi.Value,
                    DienGiaiKy = txtDienGiaiKi.Value,
                    TenNguoiKy = txtTenNguoiKi.Value,
                    ThamChieuChungTu = chkThamChieuChungTu.Checked,
                    IDDanhMucBaoCaoThamChieu = txtMaDanhMucBaoCaoThamChieu.ID,
                    MaDanhMucBaoCaoThamChieu = txtMaDanhMucBaoCaoThamChieu.Value,
                    TenDanhMucBaoCaoThamChieu = txtTenDanhMucBaoCaoThamChieu.Value,
                    FileExcelMau = txtFileExcelMau.Value,
                    SheetExcelMau = txtSheetExcelMau.Value,
                    SoDongBatDau = txtSoDongBatDau.Value,
                    IDDanhMucNhomBaoCao = txtMaDanhMucNhomBaoCao.ID,
                    MaDanhMucNhomBaoCao = txtMaDanhMucNhomBaoCao.Value,
                    TenDanhMucNhomBaoCao = txtTenDanhMucNhomBaoCao.Value,
                    IDDanhMucBaoCaoCopyCot = txtMaDanhMucBaoCaoCopyCot.ID,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucBaoCao
                {
                    ID = dataRow["ID"],
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    ReportProcedureName = txtTenStore.Value,
                    FixedColumnList = txtFixedColumn.Value,
                    KhoGiay = cboKhoGiay.Value,
                    HuongIn = cboKieuIn.Value,
                    ChucDanhKy = txtChucDanhKi.Value,
                    DienGiaiKy = txtDienGiaiKi.Value,
                    TenNguoiKy = txtTenNguoiKi.Value,
                    ThamChieuChungTu = chkThamChieuChungTu.Checked,
                    IDDanhMucBaoCaoThamChieu = txtMaDanhMucBaoCaoThamChieu.ID,
                    MaDanhMucBaoCaoThamChieu = txtMaDanhMucBaoCaoThamChieu.Value,
                    TenDanhMucBaoCaoThamChieu = txtTenDanhMucBaoCaoThamChieu.Value,
                    FileExcelMau = txtFileExcelMau.Value,
                    SheetExcelMau = txtSheetExcelMau.Value,
                    SoDongBatDau = txtSoDongBatDau.Value,
                    IDDanhMucNhomBaoCao = txtMaDanhMucNhomBaoCao.ID,
                    MaDanhMucNhomBaoCao = txtMaDanhMucNhomBaoCao.Value,
                    TenDanhMucNhomBaoCao = txtTenDanhMucNhomBaoCao.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucBaoCaoBUS _BUS = new cenBUS.DatabaseCore.DanhMucBaoCaoBUS();
            bool OK = (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
            if (OK && obj != null && Int64.TryParse(obj.ID.ToString(), out Int64 _ID) && _ID > 0)
            {
                if (dataTable != null)
                {
                    if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
                    {
                        DataRow dr = dataTable.NewRow();
                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            if (dataTable.Columns.Contains(prop.Name))
                                dr[prop.Name] = !cenCommon.cenCommon.IsNull(prop.GetValue(obj, null)) ? prop.GetValue(obj, null) : DBNull.Value;
                        }
                        dataTable.Rows.Add(dr);
                        dataTable.AcceptChanges();
                    }
                    else
                    {
                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            if (dataTable.Columns.Contains(prop.Name))
                                dataRow[prop.Name] = !cenCommon.cenCommon.IsNull(prop.GetValue(obj, null)) ? prop.GetValue(obj, null) : DBNull.Value;
                        }
                    }
                }
                ID = _ID;
                return true;
            }
            else
            {
                ID = null;
                return false;
            }
        }

        private void frmDanhMucBaoCaoUpdate_Load(object sender, EventArgs e)
        {
            //Load danh mục nhóm báo cáo
            cenBUS.DatabaseCore.DanhMucNhomBaoCaoBUS bus = new cenBUS.DatabaseCore.DanhMucNhomBaoCaoBUS();
            DataTable dtNhomBaoCao = bus.List(null);
            txtMaDanhMucNhomBaoCao.dtValid = dtNhomBaoCao;
            saTextBox[] txtNhomBaoCaoMoRong = new saTextBox[1];
            txtNhomBaoCaoMoRong[0] = txtTenDanhMucNhomBaoCao;
            txtTenDanhMucNhomBaoCao.Enabled = false;
            txtMaDanhMucNhomBaoCao.txtMoRong = txtNhomBaoCaoMoRong;
            txtMaDanhMucNhomBaoCao.ReturnedColumnsList = "Ten";
            txtMaDanhMucNhomBaoCao.IsValid = true;
            txtMaDanhMucNhomBaoCao.procedureName = DanhMucNhomBaoCao.listProcedureName;
            txtMaDanhMucNhomBaoCao.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //Load danh mục báo cáo tham chiếu
            cenBUS.DatabaseCore.DanhMucBaoCaoBUS bus1 = new cenBUS.DatabaseCore.DanhMucBaoCaoBUS();
            DataSet dsBaoCao = bus1.List(null);
            txtMaDanhMucBaoCaoThamChieu.dtValid = dsBaoCao.Tables[DanhMucBaoCao.tableName];
            saTextBox[] txtBaoCaoThamChieuMoRong = new saTextBox[1];
            txtBaoCaoThamChieuMoRong[0] = txtTenDanhMucBaoCaoThamChieu;
            txtTenDanhMucBaoCaoThamChieu.Enabled = false;
            txtMaDanhMucBaoCaoThamChieu.txtMoRong = txtBaoCaoThamChieuMoRong;
            txtMaDanhMucBaoCaoThamChieu.ReturnedColumnsList = "Ten";
            txtMaDanhMucBaoCaoThamChieu.IsValid = true;
            txtMaDanhMucBaoCaoThamChieu.procedureName = DanhMucBaoCao.listProcedureName;
            txtMaDanhMucBaoCaoThamChieu.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //Load danh mục báo cáo tham chiếu
            txtMaDanhMucBaoCaoCopyCot.dtValid = dsBaoCao.Tables[DanhMucBaoCao.tableName];
            saTextBox[] txtBaoCaoCopyCotMoRong = new saTextBox[1];
            txtBaoCaoCopyCotMoRong[0] = txtTenDanhMucBaoCaoCopyCot;
            txtTenDanhMucBaoCaoCopyCot.Enabled = false;
            txtMaDanhMucBaoCaoCopyCot.txtMoRong = txtBaoCaoCopyCotMoRong;
            txtMaDanhMucBaoCaoCopyCot.ReturnedColumnsList = "Ten";
            txtMaDanhMucBaoCaoCopyCot.IsValid = true;
            txtMaDanhMucBaoCaoCopyCot.procedureName = DanhMucBaoCao.listProcedureName;
            txtMaDanhMucBaoCaoCopyCot.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //Load danh mục khổ giấy và kiểu in
            cboKhoGiay.Items.Add(1, "A4");
            cboKhoGiay.Items.Add(2, "A3");
            cboKieuIn.Items.Add(1, "Dọc");
            cboKieuIn.Items.Add(2, "Ngang");
            //
            txtMaDanhMucBaoCaoCopyCot.Enabled = (CapNhat != 2);
            //
            txtMa.Value = DefaultValue;
            txtTen.Value = DefaultValue;
            if (CapNhat >= 2)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                txtTenStore.Value = dataRow["ReportProcedureName"];
                txtFixedColumn.Value = dataRow["FixedColumnList"];
                cboKhoGiay.Value = dataRow["KhoGiay"];
                cboKieuIn.Value = dataRow["HuongIn"];
                txtChucDanhKi.Value = dataRow["ChucDanhKy"];
                txtDienGiaiKi.Value = dataRow["DienGiaiKy"];
                txtTenNguoiKi.Value = dataRow["TenNguoiKy"];
                chkThamChieuChungTu.Checked = bool.Parse(dataRow["ThamChieuChungTu"].ToString());
                txtMaDanhMucBaoCaoThamChieu.Value = dataRow["MaDanhMucBaoCaoThamChieu"];
                txtMaDanhMucBaoCaoThamChieu.ID = dataRow["IDDanhMucBaoCaoThamChieu"];
                txtTenDanhMucBaoCaoThamChieu.Value = dataRow["TenDanhMucBaoCaoThamChieu"];
                txtFileExcelMau.Value = dataRow["FileExcelMau"];
                txtSheetExcelMau.Value = dataRow["SheetExcelMau"];
                txtSoDongBatDau.Value = dataRow["SoDongBatDau"];
                txtMaDanhMucNhomBaoCao.Value = dataRow["MaDanhMucNhomBaoCao"];
                txtMaDanhMucNhomBaoCao.ID = dataRow["IDDanhMucNhomBaoCao"];
                txtTenDanhMucNhomBaoCao.Value = dataRow["TenDanhMucNhomBaoCao"];
            }
        }
    }
}

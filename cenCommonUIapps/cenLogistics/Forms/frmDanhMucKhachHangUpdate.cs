using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmDanhMucKhachHangUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        DanhMucKhachHang obj = null;
        public frmDanhMucKhachHangUpdate()
        {
            InitializeComponent();
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
                    CapNhat = cenCommon.ThaoTacDuLieu.Them;
                    //Xóa text box
                    txtMa.Value = null;
                    txtTen.Value = null;
                    cboIDDanhMucNhanSu.Value = null;
                    txtDiaChi.Value = null;
                    txtSoDienThoai.Value = null;
                    txtSoFax.Value = null;
                    txtEmail.Value = null;
                    txtMaSoThue.Value = null;
                    txtTenNganHang.Value = null;
                    txtSoTaiKhoan.Value = null;
                    txtNguoiDaiDien.Value = null;
                    txtNguoiGiaoNhan.Value = null;
                    txtSoDienThoaiGiaoNhan.Value = null;
                    txtGhiChu.Value = null;
                }
            }
        }
        private bool Save()
        {
            //
            if (cenCommon.cenCommon.IsNull(txtMa.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu mã khách hàng!"); txtMa.Focus(); return false; }
            if (cenCommon.cenCommon.IsNull(txtTen.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu tên khách hàng!"); txtMa.Focus(); return false; }
            //if (cenCommon.cenCommon.IsNull(cboIDDanhMucNhanSu.Value) || !cboIDDanhMucNhanSu.IsItemInList()) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu mã sale!"); cboIDDanhMucNhanSu.Focus(); return false; }

            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.DanhMucKhachHang
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    //
                    IDDanhMucNhanSu = cboIDDanhMucNhanSu.Value,
                    TenDanhMucNhanSu = cboIDDanhMucNhanSu.Text,
                    DiaChi = txtDiaChi.Value,
                    SoDienThoai = txtSoDienThoai.Value,
                    SoFax = txtSoFax.Value,
                    Email = txtEmail.Value,
                    MaSoThue = txtMaSoThue.Value,
                    TenNganHang = txtTenNganHang.Value,
                    SoTaiKhoan = txtSoTaiKhoan.Value,
                    NguoiDaiDien = txtNguoiDaiDien.Value,
                    NguoiGiaoNhan = txtNguoiGiaoNhan.Value,
                    SoDienThoaiGiaoNhan = txtSoDienThoaiGiaoNhan.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.DanhMucKhachHang
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    //
                    IDDanhMucNhanSu = cboIDDanhMucNhanSu.Value,
                    TenDanhMucNhanSu = cboIDDanhMucNhanSu.Text,
                    DiaChi = txtDiaChi.Value,
                    SoDienThoai = txtSoDienThoai.Value,
                    SoFax = txtSoFax.Value,
                    Email = txtEmail.Value,
                    MaSoThue = txtMaSoThue.Value,
                    TenNganHang = txtTenNganHang.Value,
                    SoTaiKhoan = txtSoTaiKhoan.Value,
                    NguoiDaiDien = txtNguoiDaiDien.Value,
                    NguoiGiaoNhan = txtNguoiGiaoNhan.Value,
                    SoDienThoaiGiaoNhan = txtSoDienThoaiGiaoNhan.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.DanhMucKhachHangBUS _BUS = new cenBUS.cenLogistics.DanhMucKhachHangBUS();
            bool OK = (CapNhat == 1 || CapNhat == 3) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
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
                ID = obj.ID;
                return true;
            }
            else
            {
                ID = null;
                return false;
            }
        }
        private void frmDanhMucChungTuUpdate_Load(object sender, EventArgs e)
        {
            //DanhMucNhanSu
            DanhMucNhanSuBUS DanhMucNhanSuBUS = new DanhMucNhanSuBUS();
            DataTable dtNhanSu = DanhMucNhanSuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu)), null);
            cboIDDanhMucNhanSu.DataSource = dtNhanSu;
            cboIDDanhMucNhanSu.ValueMember = "ID";
            cboIDDanhMucNhanSu.DisplayMember = "Ten";
            if (CapNhat >= cenCommon.ThaoTacDuLieu.Sua)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                cboIDDanhMucNhanSu.Value = dataRow["IDDanhMucNhanSu"];
                txtDiaChi.Value = dataRow["DiaChi"];
                txtSoDienThoai.Value = dataRow["SoDienThoai"];
                txtSoFax.Value = dataRow["SoFax"];
                txtEmail.Value = dataRow["Email"];
                txtMaSoThue.Value = dataRow["MaSoThue"];
                txtTenNganHang.Value = dataRow["TenNganHang"];
                txtSoTaiKhoan.Value = dataRow["SoTaiKhoan"];
                txtNguoiDaiDien.Value = dataRow["NguoiDaiDien"];
                txtNguoiGiaoNhan.Value = dataRow["NguoiGiaoNhan"];
                txtSoDienThoaiGiaoNhan.Value = dataRow["SoDienThoaiGiaoNhan"];
                txtGhiChu.Value = dataRow["GhiChu"];
            }
        }
    }
}

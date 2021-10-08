using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmDanhMucThauPhuUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        DanhMucThauPhu obj = null;
        public frmDanhMucThauPhuUpdate()
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
                    txtDiaChi.Value = null;
                    txtSoDienThoai.Value = null;
                    txtMaSoThueCMND.Value = null;
                    cboIDDanhMucNhomHangVanChuyen.Value = null;
                    txtKyHieuKeToan.Value = null;
                    txtGhiChu.Value = null;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {
            //
            if (cenCommon.cenCommon.IsNull(txtMa.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu mã thầu phụ!"); txtMa.Focus(); return false; }
            if (cenCommon.cenCommon.IsNull(txtTen.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu tên thầu phụ!"); txtMa.Focus(); return false; }
            //if (cenCommon.cenCommon.IsNull(cboIDDanhMucNhomHangVanChuyen.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu nhóm khách hàng!"); cboIDDanhMucNhomHangVanChuyen.Focus(); return false; }

            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.DanhMucThauPhu
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    //
                    DiaChi = txtDiaChi.Value,
                    SoDienThoai = txtSoDienThoai.Value,
                    MaSoThueCMND = txtMaSoThueCMND.Value,
                    IDDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Value,
                    KyHieuKeToan = txtKyHieuKeToan.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.DanhMucThauPhu
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    //
                    DiaChi = txtDiaChi.Value,
                    SoDienThoai = txtSoDienThoai.Value,
                    MaSoThueCMND = txtMaSoThueCMND.Value,
                    IDDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Value,
                    KyHieuKeToan = txtKyHieuKeToan.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.DanhMucThauPhuBUS _BUS = new cenBUS.cenLogistics.DanhMucThauPhuBUS();
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
            //DanhMucNhomHangVanChuyen
            DanhMucDoiTuongBUS DanhMucNhomHangVanChuyenBUS = new DanhMucDoiTuongBUS();
            DataTable dtNhomHangVanChuyen = DanhMucNhomHangVanChuyenBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen)), null);
            cboIDDanhMucNhomHangVanChuyen.DataSource = dtNhomHangVanChuyen;
            cboIDDanhMucNhomHangVanChuyen.ValueMember = "ID";
            cboIDDanhMucNhomHangVanChuyen.DisplayMember = "Ten";

            if (CapNhat >= cenCommon.ThaoTacDuLieu.Sua)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                txtDiaChi.Value = dataRow["DiaChi"];
                txtSoDienThoai.Value = dataRow["SoDienThoai"];
                txtMaSoThueCMND.Value = dataRow["MaSoThueCMND"];
                cboIDDanhMucNhomHangVanChuyen.Value = dataRow["IDDanhMucNhomHangVanChuyen"];
                txtKyHieuKeToan.Value = dataRow["KyHieuKeToan"];
                txtGhiChu.Value = dataRow["GhiChu"];
            }
        }
    }
}

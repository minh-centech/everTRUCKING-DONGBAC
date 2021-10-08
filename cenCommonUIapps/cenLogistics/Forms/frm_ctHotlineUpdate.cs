﻿using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctHotlineUpdate : cenBase.BaseForms.frmBaseChungTuSingleUpdate
    {
        public object IDctDonHang = null;
        ctHotline obj = null;
        public frm_ctHotlineUpdate()
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
                    txtSo.Value = null;
                    txtNgayLap.Value = DateTime.Now;
                    if (cboIDDanhMucTrangThaiChungTu.Items.Count > 0) cboIDDanhMucTrangThaiChungTu.SelectedItem = cboIDDanhMucTrangThaiChungTu.Items[0];
                    //
                    txtSoDonHang.Value = null;
                    txtNgayDongTraHang.Value = null;
                    txtGioDongTraHang.Value = null;
                    txtDebitNote.Value = null;
                    txtBillBooking.Value = null;
                    txtTenDanhMucKhachHang.Value = null;
                    txtTenDanhMucTuyenVanTai.Value = null;
                    cboLoaiHang.Value = null;
                    txtTrongLuong.Value = null;
                    txtSoTienCuoc.Value = null;
                    txtNoiDongHang.Value = null;
                    txtNoiLayCont.Value = null;
                    txtNoiHaCont.Value = null;
                    txtSoContainer.Value = null;
                    txtTenDanhMucChuXe.Value = null;
                    txtBienSo.Value = null;
                    txtTenDanhMucTaiXe.Value = null;
                    txtNgayThucHien.Value = null;
                    txtGioThucHien.Value = null;
                    txtDienThoai.Value = null;
                    txtThongTinThuTuc.Value = null;
                    txtGhiChuHotline.Value = null;
                    txtTinhTrangHotline.Value = null;
                    cboTrangThaiHotline.Value = null;
                }
            }
        }
        private bool Save()
        {
            if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(IDctDonHang))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt doanh thu, không sửa được thông tin hotline!"); return false; }
            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.ctHotline
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    //
                    IDChungTu = IDctDonHang,
                    NgayGioThucHien = new DateTime(txtNgayThucHien.DateTime.Year, txtNgayThucHien.DateTime.Month, txtNgayThucHien.DateTime.Day, txtGioThucHien.DateTime.Hour, txtGioThucHien.DateTime.Minute, 0),
                    NgayThucHien = txtNgayThucHien.Value,
                    GioThucHien = txtGioThucHien.DateTime.Hour.ToString("0#") + ":" + txtGioThucHien.DateTime.Minute.ToString("0#"),
                    DienThoai = txtDienThoai.Value,
                    ThongTinThuTuc = txtThongTinThuTuc.Value,
                    GhiChuHotline = txtGhiChuHotline.Value,
                    TinhTrangHotline = txtTinhTrangHotline.Value,
                    TrangThaiHotline = cboTrangThaiHotline.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.ctHotline
                {
                    ID = dataRow["IDctHotline"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    //
                    IDChungTu = IDctDonHang,
                    NgayGioThucHien = new DateTime(txtNgayThucHien.DateTime.Year, txtNgayThucHien.DateTime.Month, txtNgayThucHien.DateTime.Day, txtGioThucHien.DateTime.Hour, txtGioThucHien.DateTime.Minute, 0),
                    NgayThucHien = txtNgayThucHien.Value,
                    GioThucHien = txtGioThucHien.DateTime.Hour.ToString("0#") + ":" + txtGioThucHien.DateTime.Minute.ToString("0#"),
                    DienThoai = txtDienThoai.Value,
                    ThongTinThuTuc = txtThongTinThuTuc.Value,
                    GhiChuHotline = txtGhiChuHotline.Value,
                    TinhTrangHotline = txtTinhTrangHotline.Value,
                    TrangThaiHotline = cboTrangThaiHotline.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.ctHotlineBUS _BUS = new cenBUS.cenLogistics.ctHotlineBUS();
            bool OK = (CapNhat == 1 || CapNhat == 3) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
            if (OK && obj != null && Int64.TryParse(obj.ID.ToString(), out Int64 _ID) && _ID > 0)
            {
                if (dataTable != null)
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        if (prop.Name != "So" && prop.Name != "ID" && dataRow.Table.Columns.Contains(prop.Name))
                            dataRow[prop.Name] = !cenCommon.cenCommon.IsNull(prop.GetValue(obj, null)) ? prop.GetValue(obj, null) : DBNull.Value;
                    }
                }
                ID = obj.ID;
                dataRow["IDctHotline"] = obj.ID;
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
            txtGioDongTraHang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtGioThucHien.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            //DanhMucTrangThaiChungTu
            DanhMucChungTuTrangThaiBUS danhMucChungTuTrangThaiBUS = new DanhMucChungTuTrangThaiBUS();
            DataTable dtTrangThai = danhMucChungTuTrangThaiBUS.List(null, IDDanhMucChungTu);
            cboIDDanhMucTrangThaiChungTu.DataSource = dtTrangThai;
            cboIDDanhMucTrangThaiChungTu.ValueMember = "ID";
            cboIDDanhMucTrangThaiChungTu.DisplayMember = "Ten";
            if (cboIDDanhMucTrangThaiChungTu.Items.Count > 0) cboIDDanhMucTrangThaiChungTu.SelectedItem = cboIDDanhMucTrangThaiChungTu.Items[0];
            //
            cboTrangThaiHotline.Items.Add(cenCommon.GlobalVariables.ctHotline_DangDiDongTra, cenCommon.GlobalVariables.ctHotline_DienGiaiDangDiDongTra);
            cboTrangThaiHotline.Items.Add(cenCommon.GlobalVariables.ctHotline_DangChoDongTra, cenCommon.GlobalVariables.ctHotline_DienGiaiDangChoDongTra);
            cboTrangThaiHotline.Items.Add(cenCommon.GlobalVariables.ctHotline_DaXong, cenCommon.GlobalVariables.ctHotline_DienGiaiDaXong);
            //Loại hàng
            cboLoaiHang.Items.Add(1, "Nhập");
            cboLoaiHang.Items.Add(2, "Xuất");
            cboLoaiHang.Items.Add(3, "Nội địa");
            //
            ctHotlineBUS ctHotlineBUS = new ctHotlineBUS();
            dataRow = ctHotlineBUS.List(IDDanhMucChungTu, IDctDonHang).Rows[0];
            //
            txtSo.Value = dataRow["So"];
            txtNgayLap.Value = dataRow["NgayLap"];
            cboIDDanhMucTrangThaiChungTu.Value = dataRow["IDDanhMucChungTuTrangThai"];
            txtSoDonHang.Value = dataRow["So"];
            txtNgayDongTraHang.Value = dataRow["NgayDongTraHang"];
            txtGioDongTraHang.Value = dataRow["GioDongTraHang"];
            txtGioDongTraHang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtDebitNote.Value = dataRow["DebitNote"];
            txtBillBooking.Value = dataRow["BillBooking"];
            txtTenDanhMucKhachHang.Value = dataRow["TenDanhMucKhachHang"];
            txtTenDanhMucTuyenVanTai.Value = dataRow["TenDanhMucTuyenVanTai"];
            cboLoaiHang.Value = dataRow["LoaiHang"];
            txtTrongLuong.Value = dataRow["TrongLuong"];
            txtSoTienCuoc.Value = dataRow["SoTienCuoc"];
            txtNoiLayCont.Value = dataRow["TenDanhMucDiaDiemLayContHang"];
            txtNoiHaCont.Value = dataRow["TenDanhMucDiaDiemTraContHang"];
            txtSoContainer.Value = dataRow["SoContainer"];
            txtTenDanhMucChuXe.Value = dataRow["TenDanhMucThauPhu"];
            txtBienSo.Value = dataRow["BienSo"];
            txtTenDanhMucTaiXe.Value = dataRow["TenDanhMucTaiXe"];
            //txtTenDanhMucChuXeNgoai.Value = dataRow["TenDanhMucChuXeNgoai"];
            //txtBienSoXeNgoai.Value = dataRow["BienSoXeNgoai"];
            //txtTaiXeXeNgoai.Value = dataRow["TaiXeXeNgoai"];
            //
            txtNgayThucHien.MaskInput = cenCommon.GlobalVariables.MaskInputDate;
            txtNgayThucHien.Value = dataRow["NgayGioThucHien"];
            txtGioThucHien.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtGioThucHien.Value = dataRow["GioThucHien"];

            txtDienThoai.Value = dataRow["DienThoai"];
            txtThongTinThuTuc.Value = dataRow["ThongTinThuTuc"];
            txtGhiChuHotline.Value = dataRow["GhiChuHotline"];
            txtTinhTrangHotline.Value = dataRow["TinhTrangHotline"];
            cboTrangThaiHotline.Value = dataRow["TrangThaiHotline"];

            if (CapNhat == cenCommon.ThaoTacDuLieu.Xem)
            {
                cmdSave.Enabled = false;
                cmdSaveNew.Enabled = false;
            }
        }
    }
}

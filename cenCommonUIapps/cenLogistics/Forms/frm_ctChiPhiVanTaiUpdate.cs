﻿using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctChiPhiVanTaiUpdate : cenBase.BaseForms.frmBaseChungTuSingleUpdate
    {
        public object IDctDonHang = null;
        ctChiPhiVanTai obj = null;
        public frm_ctChiPhiVanTaiUpdate()
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
                    txtTenDanhMucKhachHang.Value = null;
                    txtBienSo.Value = null;
                    txtDebitNote.Value = null;
                    txtBillBooking.Value = null;
                    txtTenDanhMucNhomHangVanChuyen.Value = null;
                    txtTenDanhMucHangHoa.Value = null;
                    txtTenDanhMucTuyenVanTai.Value = null;
                    txtSoLuongNhienLieu.Value = null;
                    txtSoTienVeCauDuong.Value = null;
                    txtSoTienLuatAnCa.Value = null;
                    txtSoTienKetHopVeCauDuongLuatAnCa.Value = null;
                    txtSoTienLuuCaKhac.Value = null;
                    txtSoTienLuatDuongCam.Value = null;
                    txtSoTienTongLuuCaKhacLuatDuongCam.Value = null;
                    txtSoTienLuongChuyen.Value = null;
                    txtSoTienLuongChuNhat.Value = null;
                    txtSoTienCuocThueXeNgoai.Value = null;
                    cboIDDanhMucCanBoTamUng.Value = null;
                    txtHanThanhToan.Value = null;
                    txtGhiChu.Value = null;
                }
            }
        }
        private bool Save()
        {
            //if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotChiPhiVanTaiGuiKeToan(dataRow["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt chi phí, không xóa được"); return false; }
            //if (cenCommon.cenCommon.IsNull(txtHanThanhToan.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu thời hạn thanh toán"); txtHanThanhToan.Focus(); return false; }
            obj = new cenDTO.cenLogistics.ctChiPhiVanTai
            {
                ID = dataRow["IDctChiPhiVanTai"],
                IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                IDDanhMucChungTu = IDDanhMucChungTu,
                //
                IDChungTu = IDctDonHang,
                SoLuongNhienLieu = txtSoLuongNhienLieu.Value,
                SoTienVeCauDuong = txtSoTienVeCauDuong.Value,
                SoTienLuatAnCa = txtSoTienLuatAnCa.Value,
                SoTienKetHopVeCauDuongLuatAnCa = txtSoTienKetHopVeCauDuongLuatAnCa.Value,
                SoTienLuuCaKhac = txtSoTienLuuCaKhac.Value,
                SoTienLuatDuongCam = txtSoTienLuatDuongCam.Value,
                SoTienTongLuuCaKhacLuatDuongCam = txtSoTienTongLuuCaKhacLuatDuongCam.Value,
                SoTienLuongChuyen = txtSoTienLuongChuyen.Value,
                SoTienLuongChuNhat = txtSoTienLuongChuNhat.Value,
                SoTienCuocThueXeNgoai = txtSoTienCuocThueXeNgoai.Value,
                IDDanhMucCanBoTamUng = cboIDDanhMucCanBoTamUng.Value,
                ThoiHanThanhToan = txtHanThanhToan.Value,
                //
                GhiChu = txtGhiChu.Value,
                IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                CreateDate = null,
                EditDate = null
            };
            cenBUS.cenLogistics.ctChiPhiVanTaiBUS _BUS = new cenBUS.cenLogistics.ctChiPhiVanTaiBUS();
            bool OK = (cenCommon.cenCommon.IsNull(obj.ID)) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
            if (OK && obj != null && Int64.TryParse(obj.ID.ToString(), out Int64 _ID) && _ID > 0)
            {
                if (dataTable != null)
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        if ((prop.Name.StartsWith("SoLuong") || prop.Name.StartsWith("SoTien") || prop.Name.StartsWith("IDDanhMucCanBoTamUng") || prop.Name.StartsWith("ThoiHanThanhToan") || prop.Name.StartsWith("GhiChu")) && prop.Name != "ID" && dataTable.Columns.Contains(prop.Name))
                            dataRow[prop.Name] = !cenCommon.cenCommon.IsNull(prop.GetValue(obj, null)) ? prop.GetValue(obj, null) : DBNull.Value;
                    }
                }
                ID = obj.ID;
                dataRow["IDctChiPhiVanTai"] = obj.ID;
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
            //DanhMucTrangThaiChungTu
            DanhMucChungTuTrangThaiBUS danhMucChungTuTrangThaiBUS = new DanhMucChungTuTrangThaiBUS();
            DataTable dtTrangThai = danhMucChungTuTrangThaiBUS.List(null, IDDanhMucChungTu);
            cboIDDanhMucTrangThaiChungTu.DataSource = dtTrangThai;
            cboIDDanhMucTrangThaiChungTu.ValueMember = "ID";
            cboIDDanhMucTrangThaiChungTu.DisplayMember = "Ten";
            if (cboIDDanhMucTrangThaiChungTu.Items.Count > 0) cboIDDanhMucTrangThaiChungTu.SelectedItem = cboIDDanhMucTrangThaiChungTu.Items[0];
            //Sale
            DanhMucNhanSuBUS DanhMucNhanSuBUS = new DanhMucNhanSuBUS();
            DataTable dtSale = DanhMucNhanSuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu)), null);
            cboIDDanhMucCanBoTamUng.dtValid = dtSale;
            cboIDDanhMucCanBoTamUng.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu));
            cboIDDanhMucCanBoTamUng.procedureName = DanhMucNhanSu.listProcedureName;
            cboIDDanhMucCanBoTamUng.DataSource = dtSale;
            cboIDDanhMucCanBoTamUng.ValueMember = "ID";
            cboIDDanhMucCanBoTamUng.DisplayMember = "Ten";
            this.cboIDDanhMucCanBoTamUng.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //
            ctChiPhiVanTaiBUS ctChiPhiVanTaiBUS = new ctChiPhiVanTaiBUS();
            dataRow = ctChiPhiVanTaiBUS.List(IDDanhMucChungTu, IDctDonHang).Rows[0];
            //
            ID = dataRow["IDctChiPhiVanTai"];
            txtSo.Value = dataRow["So"];
            txtNgayLap.Value = dataRow["NgayLap"];
            cboIDDanhMucTrangThaiChungTu.Value = dataRow["IDDanhMucChungTuTrangThai"];
            txtSoDonHang.Value = dataRow["So"];
            txtNgayDongTraHang.Value = dataRow["NgayDongTraHang"];
            txtTenDanhMucKhachHang.Value = dataRow["TenDanhMucKhachHang"];
            txtBienSo.Value = dataRow["BienSo"];
            txtDebitNote.Value = dataRow["DebitNote"];
            txtBillBooking.Value = dataRow["BillBooking"];
            txtTenDanhMucNhomHangVanChuyen.Value = dataRow["TenDanhMucNhomHangVanChuyen"];
            txtTenDanhMucHangHoa.Value = dataRow["TenDanhMucHangHoa"];
            txtTenDanhMucTuyenVanTai.Value = dataRow["TenDanhMucTuyenVanTai"];
            txtSoLuongNhienLieu.Value = dataRow["SoLuongNhienLieu"];
            txtSoTienVeCauDuong.Value = dataRow["SoTienVeCauDuong"];
            txtSoTienLuatAnCa.Value = dataRow["SoTienLuatAnCa"];
            txtSoTienKetHopVeCauDuongLuatAnCa.Value = dataRow["SoTienKetHopVeCauDuongLuatAnCa"];
            txtSoTienLuuCaKhac.Value = dataRow["SoTienLuuCaKhac"];
            txtSoTienLuatDuongCam.Value = dataRow["SoTienLuatDuongCam"];
            txtSoTienTongLuuCaKhacLuatDuongCam.Value = dataRow["SoTienTongLuuCaKhacLuatDuongCam"];
            txtSoTienLuongChuyen.Value = dataRow["SoTienLuongChuyen"];
            txtSoTienLuongChuNhat.Value = dataRow["SoTienLuongChuNhat"];
            txtSoTienCuocThueXeNgoai.Value = dataRow["SoTienCuocThueXeNgoai"];
            cboIDDanhMucCanBoTamUng.Value = dataRow["IDDanhMucCanBoTamUng"];
            txtHanThanhToan.Value = dataRow["ThoiHanThanhToan"];
            txtGhiChu.Value = dataRow["GhiChu"];

            if (CapNhat == cenCommon.ThaoTacDuLieu.Xem)
            {
                cmdSave.Enabled = false;
                cmdSaveNew.Enabled = false;
                cmdLayDinhMucChiPhi.Enabled = false;
            }

        }

        private void txtSoTienLuuCaKhac_ValueChanged(object sender, EventArgs e)
        {
            float SoTienLuuCaKhac = float.Parse(!cenCommon.cenCommon.IsNull(txtSoTienLuuCaKhac.Value) ? txtSoTienLuuCaKhac.Value.ToString() : "0");
            float SoTienLuatDuongCam = float.Parse(!cenCommon.cenCommon.IsNull(txtSoTienLuatDuongCam.Value) ? txtSoTienLuatDuongCam.Value.ToString() : "0");
            txtSoTienTongLuuCaKhacLuatDuongCam.Value = SoTienLuuCaKhac + SoTienLuatDuongCam;
        }

        private void cmdLayDinhMucChiPhi_Click(object sender, EventArgs e)
        {
            object SoLuongNhienLieu = null, SoTienVeCauDuong = null, SoTienLuatAnCa = null, SoTienKetHopVeCauDuongLuatAnCa = null, SoTienLuuCaKhac = null, SoTienLuatDuongCam = null, SoTienTongLuuCaKhacLuatDuongCam = null, SoTienLuongChuyen = null;
            DanhMucDinhMucChiPhiBUS.GetSoTien(dataRow["ID"], dataRow["IDDanhMucTuyenVanTai"], dataRow["IDDanhMucXe"], dataRow["TrangThaiDonHang"],SoLuongNhienLieu, SoTienVeCauDuong, SoTienLuatAnCa, SoTienKetHopVeCauDuongLuatAnCa, SoTienLuuCaKhac, SoTienLuatDuongCam, SoTienTongLuuCaKhacLuatDuongCam, SoTienLuongChuyen);
            txtSoLuongNhienLieu.Value = SoLuongNhienLieu;
            txtSoTienVeCauDuong.Value = SoTienVeCauDuong;
            txtSoTienLuatAnCa.Value = SoTienLuatAnCa;
            txtSoTienKetHopVeCauDuongLuatAnCa.Value = SoTienKetHopVeCauDuongLuatAnCa;
            txtSoTienLuuCaKhac.Value = SoTienLuuCaKhac;
            txtSoTienLuatDuongCam.Value = SoTienLuatDuongCam;
            txtSoTienTongLuuCaKhacLuatDuongCam.Value = SoTienTongLuuCaKhacLuatDuongCam;
            txtSoTienLuongChuyen.Value = SoTienLuongChuyen;
        }
    }
}

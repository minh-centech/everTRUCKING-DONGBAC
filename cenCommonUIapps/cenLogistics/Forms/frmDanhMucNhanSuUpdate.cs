﻿using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenControls;
using cenDTO.cenLogistics;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmDanhMucNhanSuUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        DanhMucNhanSu obj = null;
        public frmDanhMucNhanSuUpdate()
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
                    txtMaDanhMucPhongBan.Value = null;
                    txtMaDanhMucPhongBan.ID = null;
                    txtTenDanhMucPhongBan.Value = null;
                    txtNguyenQuan.Value = null;
                    txtDiaChiThuongTru.Value = null;
                    txtNgaySinh.Value = null;
                    txtSoCMND.Value = null;
                    txtNgayCap.Value = null;
                    txtNoiCap.Value = null;
                    txtSoDienThoai.Value = null;
                    txtEmail.Value = null;
                    txtMaSoThue.Value = null;
                    txtSoTaiKhoan.Value = null;
                    txtTrinhDo.Value = null;
                    txtNgayVaoLam.Value = null;
                    txtMaDanhMucTinhTrangLamViec.Value = null;
                    txtMaDanhMucTinhTrangLamViec.ID = null;
                    txtTenDanhMucTinhTrangLamViec.Value = null;
                    txtGhiChu.Value = null;
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.DanhMucNhanSu
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    //
                    IDDanhMucPhongBan = txtMaDanhMucPhongBan.ID,
                    MaDanhMucPhongBan = txtMaDanhMucPhongBan.Value,
                    TenDanhMucPhongBan = txtTenDanhMucPhongBan.Value,
                    NguyenQuan = txtNguyenQuan.Value,
                    DiaChiThuongTru = txtDiaChiThuongTru.Value,
                    NgaySinh = txtNgaySinh.Value,
                    SoCMND = txtSoCMND.Value,
                    NgayCap = txtNgayCap.Value,
                    NoiCap = txtNoiCap.Value,
                    SoDienThoai = txtSoDienThoai.Value,
                    MaSoThue = txtMaSoThue.Value,
                    SoTaiKhoan = txtSoTaiKhoan.Value,
                    Email = txtEmail.Value,
                    TrinhDo = txtTrinhDo.Value,
                    NgayVaoLam = txtNgayVaoLam.Value,
                    IDDanhMucTinhTrangLamViec = txtMaDanhMucTinhTrangLamViec.ID,
                    MaDanhMucTinhTrangLamViec = txtMaDanhMucTinhTrangLamViec.Value,
                    TenDanhMucTinhTrangLamViec = txtTenDanhMucTinhTrangLamViec.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.DanhMucNhanSu
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    //
                    IDDanhMucPhongBan = txtMaDanhMucPhongBan.ID,
                    MaDanhMucPhongBan = txtMaDanhMucPhongBan.Value,
                    TenDanhMucPhongBan = txtTenDanhMucPhongBan.Value,
                    NguyenQuan = txtNguyenQuan.Value,
                    DiaChiThuongTru = txtDiaChiThuongTru.Value,
                    NgaySinh = txtNgaySinh.Value,
                    SoCMND = txtSoCMND.Value,
                    NgayCap = txtNgayCap.Value,
                    NoiCap = txtNoiCap.Value,
                    SoDienThoai = txtSoDienThoai.Value,
                    MaSoThue = txtMaSoThue.Value,
                    SoTaiKhoan = txtSoTaiKhoan.Value,
                    Email = txtEmail.Value,
                    TrinhDo = txtTrinhDo.Value,
                    NgayVaoLam = txtNgayVaoLam.Value,
                    IDDanhMucTinhTrangLamViec = txtMaDanhMucTinhTrangLamViec.ID,
                    MaDanhMucTinhTrangLamViec = txtMaDanhMucTinhTrangLamViec.Value,
                    TenDanhMucTinhTrangLamViec = txtTenDanhMucTinhTrangLamViec.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.DanhMucNhanSuBUS _BUS = new cenBUS.cenLogistics.DanhMucNhanSuBUS();
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
            //DanhMucPhongBan
            DanhMucDoiTuongBUS BUS = new DanhMucDoiTuongBUS();
            DataTable dtPhongBan = BUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongPhongBan)), null);
            txtMaDanhMucPhongBan.IsValid = true;
            txtMaDanhMucPhongBan.dtValid = dtPhongBan;
            txtMaDanhMucPhongBan.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongPhongBan));
            saTextBox[] txtMaDanhMucPhongBanExt = new saTextBox[1];
            txtMaDanhMucPhongBanExt[0] = txtTenDanhMucPhongBan;
            txtMaDanhMucPhongBan.txtMoRong = txtMaDanhMucPhongBanExt;
            txtMaDanhMucPhongBan.ValidColumnName = "Ma";
            txtMaDanhMucPhongBan.ReturnedColumnsList = "Ten";
            txtMaDanhMucPhongBan.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //DanhMucTinhTrangLamViec
            BUS = new DanhMucDoiTuongBUS();
            DataTable dtTinhTrangLamViec = BUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTinhTrangLamViec)), null);
            txtMaDanhMucTinhTrangLamViec.IsValid = true;
            txtMaDanhMucTinhTrangLamViec.dtValid = dtTinhTrangLamViec;
            txtMaDanhMucTinhTrangLamViec.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTinhTrangLamViec));
            saTextBox[] txtMaDanhMucTinhTrangLamViecExt = new saTextBox[1];
            txtMaDanhMucTinhTrangLamViecExt[0] = txtTenDanhMucTinhTrangLamViec;
            txtMaDanhMucTinhTrangLamViec.txtMoRong = txtMaDanhMucTinhTrangLamViecExt;
            txtMaDanhMucTinhTrangLamViec.ValidColumnName = "Ma";
            txtMaDanhMucTinhTrangLamViec.ReturnedColumnsList = "Ten";
            txtMaDanhMucTinhTrangLamViec.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);

            if (CapNhat >= cenCommon.ThaoTacDuLieu.Sua)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                txtMaDanhMucPhongBan.Value = dataRow["MaDanhMucPhongBan"];
                txtMaDanhMucPhongBan.ID = dataRow["IDDanhMucPhongBan"];
                txtTenDanhMucPhongBan.Value = dataRow["TenDanhMucPhongBan"];
                txtNguyenQuan.Value = dataRow["NguyenQuan"];
                txtDiaChiThuongTru.Value = dataRow["DiaChiThuongTru"];
                txtNgaySinh.Value = dataRow["NgaySinh"];
                txtSoCMND.Value = dataRow["SoCMND"];
                txtNgayCap.Value = dataRow["NgayCap"];
                txtNoiCap.Value = dataRow["NoiCap"];
                txtSoDienThoai.Value = dataRow["SoDienThoai"];
                txtEmail.Value = dataRow["Email"];
                txtMaSoThue.Value = dataRow["MaSoThue"];
                txtSoTaiKhoan.Value = dataRow["SoTaiKhoan"];
                txtTrinhDo.Value = dataRow["TrinhDo"];
                txtNgayVaoLam.Value = dataRow["NgayVaoLam"];
                txtMaDanhMucTinhTrangLamViec.Value = dataRow["MaDanhMucTinhTrangLamViec"];
                txtMaDanhMucTinhTrangLamViec.ID = dataRow["IDDanhMucTinhTrangLamViec"];
                txtTenDanhMucTinhTrangLamViec.Value = dataRow["TenDanhMucTinhTrangLamViec"];
                txtGhiChu.Value = dataRow["GhiChu"];
            }
        }
    }
}

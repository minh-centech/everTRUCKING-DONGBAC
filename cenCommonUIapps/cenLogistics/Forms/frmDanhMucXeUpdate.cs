using cenBUS.cenLogistics;
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
    public partial class frmDanhMucXeUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        DanhMucXe obj = null;
        public frmDanhMucXeUpdate()
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
                    txtBienSo.Value = null;
                    txtMaDanhMucNhomXe.Value = null;
                    txtMaDanhMucNhomXe.ID = null;
                    txtTenDanhMucNhomXe.Value = null;
                    txtMaDanhMucNhienLieu.Value = null;
                    txtMaDanhMucNhienLieu.ID = null;
                    txtTenDanhMucNhienLieu.Value = null;
                    cboIDDanhMucThauPhu.Value = null;
                    cboIDDanhMucNhomHangVanChuyen.Value = null;
                    txtGhiChu.Value = null;
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.DanhMucXe
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    BienSo = txtBienSo.Value,
                    IDDanhMucNhomXe = txtMaDanhMucNhomXe.ID,
                    MaDanhMucNhomXe = txtMaDanhMucNhomXe.Value,
                    TenDanhMucNhomXe = txtTenDanhMucNhomXe.Value,
                    IDDanhMucNhienLieu = txtMaDanhMucNhienLieu.ID,
                    MaDanhMucNhienLieu = txtMaDanhMucNhienLieu.Value,
                    TenDanhMucNhienLieu = txtTenDanhMucNhienLieu.Value,
                    IDDanhMucThauPhu = cboIDDanhMucThauPhu.Value,
                    TenDanhMucThauPhu = txtTenDanhMucThauPhu.Text,
                    IDDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Value,
                    TenDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Text,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.DanhMucXe
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    BienSo = txtBienSo.Value,
                    IDDanhMucNhomXe = txtMaDanhMucNhomXe.ID,
                    MaDanhMucNhomXe = txtMaDanhMucNhomXe.Value,
                    TenDanhMucNhomXe = txtTenDanhMucNhomXe.Value,
                    IDDanhMucNhienLieu = txtMaDanhMucNhienLieu.ID,
                    MaDanhMucNhienLieu = txtMaDanhMucNhienLieu.Value,
                    TenDanhMucNhienLieu = txtTenDanhMucNhienLieu.Value,
                    IDDanhMucThauPhu = cboIDDanhMucThauPhu.Value,
                    TenDanhMucThauPhu = txtTenDanhMucThauPhu.Text,
                    IDDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Value,
                    TenDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Text,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.DanhMucXeBUS _BUS = new cenBUS.cenLogistics.DanhMucXeBUS();
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
            //DanhMucNhomXe
            DanhMucDoiTuongBUS BUS = new DanhMucDoiTuongBUS();
            DataTable dtNhomXe = BUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomXe)), null);
            txtMaDanhMucNhomXe.IsValid = true;
            txtMaDanhMucNhomXe.dtValid = dtNhomXe;
            txtMaDanhMucNhomXe.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomXe));
            saTextBox[] txtMaDanhMucNhomXeExt = new saTextBox[1];
            txtMaDanhMucNhomXeExt[0] = txtTenDanhMucNhomXe;
            txtMaDanhMucNhomXe.txtMoRong = txtMaDanhMucNhomXeExt;
            txtMaDanhMucNhomXe.ValidColumnName = "Ma";
            txtMaDanhMucNhomXe.ReturnedColumnsList = "Ten";
            txtMaDanhMucNhomXe.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //DanhMucNhienLieu
            BUS = new DanhMucDoiTuongBUS();
            DataTable dtNhienLieu = BUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhienLieu)), null);
            txtMaDanhMucNhienLieu.IsValid = true;
            txtMaDanhMucNhienLieu.dtValid = dtNhienLieu;
            txtMaDanhMucNhienLieu.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhienLieu));
            saTextBox[] txtMaDanhMucNhienLieuExt = new saTextBox[1];
            txtMaDanhMucNhienLieuExt[0] = txtTenDanhMucNhienLieu;
            txtMaDanhMucNhienLieu.txtMoRong = txtMaDanhMucNhienLieuExt;
            txtMaDanhMucNhienLieu.ValidColumnName = "Ma";
            txtMaDanhMucNhienLieu.ReturnedColumnsList = "Ten";
            txtMaDanhMucNhienLieu.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //DanhMucThauPhu
            DanhMucThauPhuBUS DanhMucThauPhuBUS = new DanhMucThauPhuBUS();
            DataTable dtThauPhu = DanhMucThauPhuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu)), null, null);
            cboIDDanhMucThauPhu.DataSource = dtThauPhu;
            cboIDDanhMucThauPhu.ValueMember = "ID";
            cboIDDanhMucThauPhu.DisplayMember = "Ma";
            //DanhMucNhomHangVanChuyen
            DanhMucDoiTuongBUS DanhMucNhomHangVanChuyenBUS = new DanhMucDoiTuongBUS();
            DataTable dtNhomHangVanChuyen = DanhMucNhomHangVanChuyenBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen)), null);
            cboIDDanhMucNhomHangVanChuyen.DataSource = dtNhomHangVanChuyen;
            cboIDDanhMucNhomHangVanChuyen.ValueMember = "ID";
            cboIDDanhMucNhomHangVanChuyen.DisplayMember = "Ten";


            if (CapNhat >= cenCommon.ThaoTacDuLieu.Sua)
            {
                txtBienSo.Value = dataRow["BienSo"];
                txtMaDanhMucNhomXe.Value = dataRow["MaDanhMucNhomXe"];
                txtMaDanhMucNhomXe.ID = dataRow["IDDanhMucNhomXe"];
                txtTenDanhMucNhomXe.Value = dataRow["TenDanhMucNhomXe"];
                txtMaDanhMucNhienLieu.Value = dataRow["MaDanhMucNhienLieu"];
                txtMaDanhMucNhienLieu.ID = dataRow["IDDanhMucNhienLieu"];
                txtTenDanhMucNhienLieu.Value = dataRow["TenDanhMucNhienLieu"];
                cboIDDanhMucThauPhu.Value = dataRow["IDDanhMucThauPhu"];
                cboIDDanhMucNhomHangVanChuyen.Value = dataRow["IDDanhMucNhomHangVanChuyen"];
                txtGhiChu.Value = dataRow["GhiChu"];
            }
        }

        private void cboIDDanhMucThauPhu_ValueChanged(object sender, EventArgs e)
        {
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucThauPhu.Value) || !cboIDDanhMucThauPhu.IsItemInList()) return;
            DanhMucThauPhuBUS DanhMucThauPhuBUS = new DanhMucThauPhuBUS();
            DataTable dt = DanhMucThauPhuBUS.List(cboIDDanhMucThauPhu.Value, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu)), null, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtTenDanhMucThauPhu.Value = dt.Rows[0]["Ten"];
            }
        }
    }
}

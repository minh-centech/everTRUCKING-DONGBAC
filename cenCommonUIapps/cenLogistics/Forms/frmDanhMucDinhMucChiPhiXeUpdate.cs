using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenControls;
using cenDTO.cenLogistics;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmDanhMucDinhMucChiPhiXeUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public object IDDanhMucDinhMucChiPhi = null;
        DanhMucDinhMucChiPhiXe obj = null;
        public frmDanhMucDinhMucChiPhiXeUpdate()
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
                    cboIDDanhMucXe.Value = null;
                    txtGhiChu.Value = null;
                }
            }
        }
        private bool Save()
        {
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucXe.Value) || !cboIDDanhMucXe.IsItemInList()) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu thông tin số xe!"); cboIDDanhMucXe.Focus(); return false; }
            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.DanhMucDinhMucChiPhiXe
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDinhMucChiPhiXe)),
                    //
                    IDDanhMucDinhMucChiPhi = IDDanhMucDinhMucChiPhi,
                    IDDanhMucXe = cboIDDanhMucXe.Value,
                    BienSo = cboIDDanhMucXe.Text,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.DanhMucDinhMucChiPhiXe
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                    //
                    IDDanhMucDinhMucChiPhi = IDDanhMucDinhMucChiPhi,
                    IDDanhMucXe = cboIDDanhMucXe.Value,
                    BienSo = cboIDDanhMucXe.Text,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.DanhMucDinhMucChiPhiXeBUS _BUS = new cenBUS.cenLogistics.DanhMucDinhMucChiPhiXeBUS();
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
            //DanhMucXe
            DanhMucXeBUS DanhMucXeBUS = new DanhMucXeBUS();
            DataTable dtXe = DanhMucXeBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongXe)), null, null, null);
            cboIDDanhMucXe.DataSource = dtXe;
            cboIDDanhMucXe.ValueMember = "ID";
            cboIDDanhMucXe.DisplayMember = "BienSo";
            //
            if (CapNhat >= cenCommon.ThaoTacDuLieu.Sua)
            {
                cboIDDanhMucXe.Value = dataRow["IDDanhMucXe"];
                txtGhiChu.Value = dataRow["GhiChu"];
            }
        }
    }
}

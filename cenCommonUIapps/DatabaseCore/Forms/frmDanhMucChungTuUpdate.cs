using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucChungTuUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        cenDTO.DatabaseCore.DanhMucChungTu obj = null;
        public frmDanhMucChungTuUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucDonVi";
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
                    txtKiHieu.Value = null;
                    cboLoaiManHinh.Value = null;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTu
                {
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    KiHieu = txtKiHieu.Value,
                    LoaiManHinh = cboLoaiManHinh.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTu
                {
                    ID = dataRow["ID"],
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    KiHieu = txtKiHieu.Value,
                    LoaiManHinh = cboLoaiManHinh.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucChungTuBUS _BUS = new cenBUS.DatabaseCore.DanhMucChungTuBUS();
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
                ID = _ID;
                return true;
            }
            else
            {
                ID = null;
                return false;
            }
        }



        private void frmDanhMucDonViUpdate_Load(object sender, EventArgs e)
        {
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDHopDongVanChuyen, cenCommon.LoaiManHinh.NameHopDongVanChuyen);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDonHang, cenCommon.LoaiManHinh.NameDonHang);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDThanhToanTamUng, cenCommon.LoaiManHinh.NameThanhToanTamUng);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDChotDoanhThuGuiKeToan, cenCommon.LoaiManHinh.NameChotDoanhThuGuiKeToan);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDTrangThaiXe, cenCommon.LoaiManHinh.NameTrangThaiXe);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDQuanLyDau, cenCommon.LoaiManHinh.NameQuanLyDau);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDPhieuDoNhienLieu, cenCommon.LoaiManHinh.NamePhieuDoNhienLieu);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDSuaChua, cenCommon.LoaiManHinh.NameSuaChua);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDChiPhiVanTai, cenCommon.LoaiManHinh.NameChiPhiVanTai);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDChotChiPhiVanTaiGuiKeToan, cenCommon.LoaiManHinh.NameChotChiPhiVanTaiGuiKeToan);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDChiPhiVanTaiBoSung, cenCommon.LoaiManHinh.NameChiPhiVanTaiBoSung);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDChotChiPhiVanTaiBoSungGuiKeToan, cenCommon.LoaiManHinh.NameChotChiPhiVanTaiBoSungGuiKeToan);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDTamUng, cenCommon.LoaiManHinh.NameTamUng);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDieuHanh, cenCommon.LoaiManHinh.NameDieuHanh);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDHotline, cenCommon.LoaiManHinh.NameHotline);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDInGiayVanTai, cenCommon.LoaiManHinh.NameInGiayVanTai);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDKeHoachVanTai, cenCommon.LoaiManHinh.NameKeHoachVanTai);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetTamUngHoanUngKinhDoanh, cenCommon.LoaiManHinh.NameDuyetTamUngHoanUngKinhDoanh);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetDeNghiThanhToanKinhDoanh, cenCommon.LoaiManHinh.NameDuyetDeNghiThanhToanKinhDoanh);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetDoanhThuKinhDoanh, cenCommon.LoaiManHinh.NameDuyetDoanhThuKinhDoanh);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetTamUngChuyenDieuVan, cenCommon.LoaiManHinh.NameDuyetTamUngChuyenDieuVan);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetDeNghiThanhToanChuyenDieuVan, cenCommon.LoaiManHinh.NameDuyetDeNghiThanhToanChuyenDieuVan);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetChiPhiDieuVan, cenCommon.LoaiManHinh.NameDuyetChiPhiDieuVan);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetChiPhiDieuVanBoSung, cenCommon.LoaiManHinh.NameDuyetChiPhiDieuVanBoSung);
            cboLoaiManHinh.Items.Add(cenCommon.LoaiManHinh.IDDuyetTamUngDieuVan, cenCommon.LoaiManHinh.NameDuyetTamUngDieuVan);
            if (CapNhat >= 2)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                txtKiHieu.Value = dataRow["KiHieu"];
                cboLoaiManHinh.Value = dataRow["LoaiManHinh"];
            }
        }
    }
}

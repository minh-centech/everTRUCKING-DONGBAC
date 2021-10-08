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
    public partial class frm_ctHopDongVanChuyenUpdate : cenBase.BaseForms.frmBaseChungTuSingleUpdate
    {
        object FileContent;
        ctHopDongVanChuyen obj = null;
        public frm_ctHopDongVanChuyenUpdate()
        {
            InitializeComponent();
            this.groupEditor.Width = 1004;
            this.groupEditor.Height = 562;
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
                    txtSoHopDong.Value = null;
                    txtNgayHopDong.Value = DateTime.Now;
                    txtNgayCoHieuLuc.Value = null;
                    txtNgayHetHieuLuc.Value = null;
                    txtMaDanhMucKhachHang.Value = null;
                    txtMaDanhMucKhachHang.ID = null;
                    txtTenDanhMucKhachHang.Value = null;
                    txtSoTien.Value = null;
                    txtNoiDung.Value = null;
                    cboTrangThaiThucHien.Value = 0;
                    txtFileName.Value = null;
                    FileContent = null;
                    txtGhiChu.Value = null;
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.ctHopDongVanChuyen
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    IDDanhMucChungTuTrangThai = cboIDDanhMucTrangThaiChungTu.Value,
                    So = txtSo.Value,
                    NgayLap = txtNgayLap.Value,
                    SoHopDong = txtSoHopDong.Value,
                    NgayHopDong = txtNgayHopDong.Value,
                    NgayCoHieuLuc = txtNgayCoHieuLuc.Value,
                    NgayHetHieuLuc = txtNgayHetHieuLuc.Value,
                    IDDanhMucKhachHang = txtMaDanhMucKhachHang.ID,
                    MaDanhMucKhachHang = txtMaDanhMucKhachHang.Value,
                    TenDanhMucKhachHang = txtTenDanhMucKhachHang.Value,
                    NoiDung = txtNoiDung.Value,
                    SoTien = txtSoTien.Value,
                    TrangThaiHopDong = cboTrangThaiThucHien.Value,
                    FileName = txtFileName.Value,
                    FileContent = FileContent,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.ctHopDongVanChuyen
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    IDDanhMucChungTuTrangThai = cboIDDanhMucTrangThaiChungTu.Value,
                    So = txtSo.Value,
                    NgayLap = txtNgayLap.Value,
                    SoHopDong = txtSoHopDong.Value,
                    NgayHopDong = txtNgayHopDong.Value,
                    NgayCoHieuLuc = txtNgayCoHieuLuc.Value,
                    NgayHetHieuLuc = txtNgayHetHieuLuc.Value,
                    IDDanhMucKhachHang = txtMaDanhMucKhachHang.ID,
                    MaDanhMucKhachHang = txtMaDanhMucKhachHang.Value,
                    TenDanhMucKhachHang = txtTenDanhMucKhachHang.Value,
                    NoiDung = txtNoiDung.Value,
                    SoTien = txtSoTien.Value,
                    TrangThaiHopDong = cboTrangThaiThucHien.Value,
                    FileName = txtFileName.Value,
                    FileContent = !cenCommon.cenCommon.IsNull(FileContent) ? FileContent : null,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.ctHopDongVanChuyenBUS _BUS = new cenBUS.cenLogistics.ctHopDongVanChuyenBUS();
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
                //dataRow["ID"] = obj.ID;
                //dataRow["So"] = obj.So;
                //dataRow["IDDanhMucDonVi"] = obj.IDDanhMucDonVi;
                //dataRow["IDDanhMucChungTu"] = obj.IDDanhMucChungTu;
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
            //DanhMucTrangThaiChungTu
            DanhMucChungTuTrangThaiBUS danhMucChungTuTrangThaiBUS = new DanhMucChungTuTrangThaiBUS();
            DataTable dtTrangThai = danhMucChungTuTrangThaiBUS.List(null, IDDanhMucChungTu);
            cboIDDanhMucTrangThaiChungTu.DataSource = dtTrangThai;
            cboIDDanhMucTrangThaiChungTu.ValueMember = "ID";
            cboIDDanhMucTrangThaiChungTu.DisplayMember = "Ten";
            //DanhMucKhachHang
            DanhMucKhachHangBUS BUS = new DanhMucKhachHangBUS();
            DataTable dtKhachHang = BUS.Valid(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang)), null);
            txtMaDanhMucKhachHang.IsValid = true;
            txtMaDanhMucKhachHang.dtValid = dtKhachHang;
            txtMaDanhMucKhachHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang));
            saTextBox[] txtMaDanhMucKhachHangExt = new saTextBox[1];
            txtMaDanhMucKhachHangExt[0] = txtTenDanhMucKhachHang;
            txtMaDanhMucKhachHang.txtMoRong = txtMaDanhMucKhachHangExt;
            txtMaDanhMucKhachHang.ValidColumnName = "Ma";
            txtMaDanhMucKhachHang.ReturnedColumnsList = "Ten";
            txtMaDanhMucKhachHang.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //TrangThaiThucHienHopDong
            cboTrangThaiThucHien.Items.Add(0, "Chưa thực hiện");
            cboTrangThaiThucHien.Items.Add(1, "Đang thực hiện");
            cboTrangThaiThucHien.Items.Add(2, "Đã thực hiện");
            //
            if (cboIDDanhMucTrangThaiChungTu.Items.Count > 0) cboIDDanhMucTrangThaiChungTu.SelectedItem = cboIDDanhMucTrangThaiChungTu.Items[0];
            txtNgayLap.Value = DateTime.Now;
            txtNgayHopDong.Value = DateTime.Now;
            cboTrangThaiThucHien.Value = 0;
            txtFileName.Value = null;
            FileContent = null;
            if (CapNhat >= cenCommon.ThaoTacDuLieu.Sua)
            {

                txtSo.Value = dataRow["So"];
                txtNgayLap.Value = dataRow["NgayLap"];
                cboIDDanhMucTrangThaiChungTu.Value = dataRow["IDDanhMucChungTuTrangThai"];
                txtSoHopDong.Value = dataRow["SoHopDong"];
                txtNgayHopDong.Value = dataRow["NgayHopDong"];
                txtNgayCoHieuLuc.Value = dataRow["NgayCoHieuLuc"];
                txtNgayHetHieuLuc.Value = dataRow["NgayHetHieuLuc"];
                txtMaDanhMucKhachHang.Value = dataRow["MaDanhMucKhachHang"];
                txtMaDanhMucKhachHang.ID = dataRow["IDDanhMucKhachHang"];
                txtTenDanhMucKhachHang.Value = dataRow["TenDanhMucKhachHang"];
                txtNoiDung.Value = dataRow["NoiDung"];
                txtSoTien.Value = dataRow["SoTien"];
                cboTrangThaiThucHien.Value = dataRow["TrangThaiHopDong"];
                txtFileName.Value = dataRow["FileName"];
                txtGhiChu.Value = dataRow["GhiChu"];
                //Load hình ảnh
                ctHopDongVanChuyenBUS ctHopDongVanChuyenBUS = new ctHopDongVanChuyenBUS();
                DataTable dtFileContent = ctHopDongVanChuyenBUS.ListFileContent(dataRow["ID"]);
                if (dtFileContent.Rows.Count > 0)
                {
                    FileContent = dtFileContent.Rows[0]["FileContent"];
                }
            }
        }

        private void txtFileName_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            switch (e.Button.Key.ToUpper())
            {
                case "BTTAIFILE":
                    OpenFileDialog openFileDlg = new OpenFileDialog
                    {
                        Filter = "All files (*.*)|*.*",
                        RestoreDirectory = true
                    };
                    if (openFileDlg.ShowDialog() == DialogResult.OK)
                    {
                        txtFileName.Value = System.IO.Path.GetFileName(openFileDlg.FileName);
                        FileContent = System.IO.File.ReadAllBytes(openFileDlg.FileName);
                        openFileDlg.Dispose();
                    }
                    break;
                case "BTXEMFILE":
                    if (cenCommon.cenCommon.IsNull(FileContent)) { cenCommon.cenCommon.ErrorMessageOkOnly("Không có nội dung file cần xem"); return; }
                    string FileName = cenCommon.cenCommon.OpenSaveFileDialog("Xem file hợp đồng", cenCommon.GlobalVariables.TempDir + txtFileName.Value, "All files (*.*)|*.*");
                    if (FileName != "")
                    {
                        System.IO.File.WriteAllBytes(FileName, (byte[])FileContent);
                        System.Diagnostics.Process.Start(FileName);
                    }
                    break;
                case "BTXOAFILE":
                    txtFileName.Value = null;
                    FileContent = null;
                    break;
            }
        }
    }
}

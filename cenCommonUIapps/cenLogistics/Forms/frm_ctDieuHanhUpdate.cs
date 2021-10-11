using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctDieuHanhUpdate : cenBase.BaseForms.frmBaseChungTuSingleUpdate
    {
        public object IDctDonHang = null;
        ctDieuHanh obj = null;
        DataTable dtXe, dtTaiXe, dtDonHangKetHop;
        public frm_ctDieuHanhUpdate()
        {
            InitializeComponent();
        }
        protected override void SaveData(bool AddNew)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private bool Save()
        {
            if (!cenCommon.cenCommon.IsNull(ctDieuHanhBUS.ListChiTietDonHangChinh(dataRow["ID"])) && int.Parse(ctDieuHanhBUS.ListChiTietDonHangChinh(dataRow["ID"]).ToString()) > 0 && cboTrangThaiDonHang.Value.ToString() != "5")
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Đây là đơn hàng nhánh, không được phép khai báo đơn hàng kẹp/kết hợp");
                return false;
            }
            //if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(IDctDonHang))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt doanh thu, không sửa được thông tin điều hành!"); return false; }
            //if (cenCommon.cenCommon.IsNull(cboIDDanhMucThauPhu.Value) || !cboIDDanhMucThauPhu.IsItemInList()) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu mã chủ xe!"); cboIDDanhMucThauPhu.Focus(); return false; }
            if (!cenCommon.cenCommon.IsNull(cboIDDanhMucXe.Value) && !cboIDDanhMucXe.IsItemInList()) { cenCommon.cenCommon.ErrorMessageOkOnly("Số xe chưa được khai báo!"); cboIDDanhMucXe.Focus(); return false; }
            //if (cenCommon.cenCommon.IsNull(cboIDDanhMucTaiXe.Value) || !cboIDDanhMucTaiXe.IsItemInList()) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu tên tài xế!"); cboIDDanhMucTaiXe.Focus(); return false; }

            if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
            {
                obj = new cenDTO.cenLogistics.ctDieuHanh
                {
                    ID = dataRow["IDctDieuHanh"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    //
                    IDChungTu = IDctDonHang,
                    IDDanhMucThauPhu = cboIDDanhMucThauPhu.Value,
                    TenDanhMucThauPhu = txtTenDanhMucThauPhu.Value,
                    IDDanhMucXe = cboIDDanhMucXe.Value,
                    BienSo = cboIDDanhMucXe.Text,
                    BienSoXeNgoai = txtBienSoXeNgoai.Value,
                    IDDanhMucTaiXe = cboIDDanhMucTaiXe.Value,
                    TenDanhMucTaiXe = cboIDDanhMucTaiXe.Text,
                    TrangThaiDonHang = cboTrangThaiDonHang.Value,
                    SoDonHangKetHop = txtSoDonHangKetHop.Value,
                    DienThoai = txtDienThoai.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungCreate = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.cenLogistics.ctDieuHanh
                {
                    ID = dataRow["IDctDieuHanh"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    //
                    IDChungTu = IDctDonHang,
                    IDDanhMucThauPhu = cboIDDanhMucThauPhu.Value,
                    TenDanhMucThauPhu = txtTenDanhMucThauPhu.Value,
                    IDDanhMucXe = cboIDDanhMucXe.Value,
                    BienSo = cboIDDanhMucXe.Text,
                    BienSoXeNgoai = txtBienSoXeNgoai.Value,
                    IDDanhMucTaiXe = cboIDDanhMucTaiXe.Value,
                    TenDanhMucTaiXe = cboIDDanhMucTaiXe.Text,
                    TrangThaiDonHang = cboTrangThaiDonHang.Value,
                    SoDonHangKetHop = txtSoDonHangKetHop.Value,
                    DienThoai = txtDienThoai.Value,
                    GhiChu = txtGhiChu.Value,
                    //
                    IDDanhMucNguoiSuDungEdit = cenCommon.GlobalVariables.IDDanhMucNguoiSuDung,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.cenLogistics.ctDieuHanhBUS _BUS = new cenBUS.cenLogistics.ctDieuHanhBUS();
            bool OK = (cenCommon.cenCommon.IsNull(dataRow["IDctDieuHanh"])) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
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
                dataRow["IDctDieuHanh"] = obj.ID;
                //Lưu danh sách đơn hàng kết hợp
                ctDieuHanhChiTietDonHangKetHopBUS ctDieuHanhChiTietDonHangKetHopBUS = new ctDieuHanhChiTietDonHangKetHopBUS();
                foreach (DataRow drDonHangKetHop in dtDonHangKetHop.Rows)
                {
                    if (drDonHangKetHop.RowState == DataRowState.Deleted)
                    {
                        if (!ctDieuHanhChiTietDonHangKetHopBUS.Delete(new ctDieuHanhChiTietDonHangKetHop() { ID = drDonHangKetHop["ID", DataRowVersion.Original] })) return false;
                    }
                    else
                    {
                        ctDieuHanhChiTietDonHangKetHop obj = new ctDieuHanhChiTietDonHangKetHop()
                        {
                            ID = drDonHangKetHop["ID"],
                            IDDanhMucChungTu = IDDanhMucChungTu,
                            IDChungTu = dataRow["ID"],
                            IDChungTuChiTiet = dataRow["IDctDieuHanh"],
                            IDctDonHang = drDonHangKetHop["IDctDonHang"],
                            GhiChu = drDonHangKetHop["GhiChu"]
                        };
                        if (drDonHangKetHop.RowState == DataRowState.Modified)
                        {
                            if (!ctDieuHanhChiTietDonHangKetHopBUS.Update(ref obj)) return false;
                        }
                        if (drDonHangKetHop.RowState == DataRowState.Added)
                        {
                            if (!ctDieuHanhChiTietDonHangKetHopBUS.Insert(ref obj)) return false;
                        }
                    }
                }
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
            ctDieuHanhBUS ctDieuHanhBUS = new ctDieuHanhBUS();
            dataRow = ctDieuHanhBUS.List2(IDDanhMucChungTu, IDctDonHang).Rows[0];

            ctDieuHanhChiTietDonHangKetHopBUS ctDieuHanhChiTietDonHangKetHopBUS = new ctDieuHanhChiTietDonHangKetHopBUS();
            dtDonHangKetHop = ctDieuHanhChiTietDonHangKetHopBUS.List(IDDanhMucChungTu, IDctDonHang);

            //DanhMucTrangThaiChungTu
            DanhMucChungTuTrangThaiBUS danhMucChungTuTrangThaiBUS = new DanhMucChungTuTrangThaiBUS();
            DataTable dtTrangThai = danhMucChungTuTrangThaiBUS.List(null, IDDanhMucChungTu);
            cboIDDanhMucTrangThaiChungTu.DataSource = dtTrangThai;
            cboIDDanhMucTrangThaiChungTu.ValueMember = "ID";
            cboIDDanhMucTrangThaiChungTu.DisplayMember = "Ten";
            if (cboIDDanhMucTrangThaiChungTu.Items.Count > 0) cboIDDanhMucTrangThaiChungTu.SelectedItem = cboIDDanhMucTrangThaiChungTu.Items[0];
            //Chủ xe
            DanhMucThauPhuBUS DanhMucThauPhuBUS = new DanhMucThauPhuBUS();
            DataTable dtChuXe = DanhMucThauPhuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu)), dataRow["IDDanhMucNhomHangVanChuyen"], null);
            cboIDDanhMucThauPhu.dtValid = dtChuXe;
            cboIDDanhMucThauPhu.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu));
            cboIDDanhMucThauPhu.procedureName = DanhMucThauPhu.listProcedureName;
            cboIDDanhMucThauPhu.DataSource = dtChuXe;
            cboIDDanhMucThauPhu.ValueMember = "ID";
            cboIDDanhMucThauPhu.DisplayMember = "Ma";
            cboIDDanhMucThauPhu.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);

            cboTrangThaiDonHang.Items.Add("1", "Đơn");
            cboTrangThaiDonHang.Items.Add("2", "Kẹp");
            cboTrangThaiDonHang.Items.Add("3", "1h-1v/1v-1h");
            cboTrangThaiDonHang.Items.Add("4", "Kết hợp");
            cboTrangThaiDonHang.Items.Add("5", "Nhánh");
            //Xe
            DanhMucXeBUS DanhMucXeBUS = new DanhMucXeBUS();
            dtXe = DanhMucXeBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongXe)), null, null, null);
            cboIDDanhMucXe.dtValid = dtXe;
            cboIDDanhMucXe.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongXe));
            cboIDDanhMucXe.procedureName = DanhMucXe.listProcedureName;
            cboIDDanhMucXe.DataSource = dtXe;
            cboIDDanhMucXe.ValueMember = "ID";
            cboIDDanhMucXe.DisplayMember = "BienSo";
            cboIDDanhMucXe.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //Tài xế
            DanhMucTaiXeBUS DanhMucTaiXeBUS = new DanhMucTaiXeBUS();
            dtTaiXe = DanhMucTaiXeBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTaiXe)), null, null);
            cboIDDanhMucTaiXe.dtValid = dtTaiXe;
            cboIDDanhMucTaiXe.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTaiXe));
            cboIDDanhMucTaiXe.procedureName = DanhMucTaiXe.listProcedureName;
            cboIDDanhMucTaiXe.DataSource = dtTaiXe;
            cboIDDanhMucTaiXe.ValueMember = "ID";
            cboIDDanhMucTaiXe.DisplayMember = "Ten";
            cboIDDanhMucTaiXe.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //

            txtGioDongHang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtGioTraHang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtGioCatMang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            //
            txtSo.Value = dataRow["So"];
            txtNgayLap.Value = dataRow["NgayLap"];
            cboIDDanhMucTrangThaiChungTu.Value = dataRow["IDDanhMucChungTuTrangThai"];
            txtMaDanhMucKhachHang.Value = dataRow["MaDanhMucKhachHang"];
            txtTenDanhMucKhachHang.Value = dataRow["TenDanhMucKhachHang"];
            txtSoTienCuoc.Value = dataRow["SoTienCuoc"];
            txtDebitNote.Value = dataRow["DebitNote"];
            txtTenLoaiHang.Value = dataRow["TenLoaiHang"];
            txtBillBooking.Value = dataRow["BillBooking"];
            txtMaDanhMucNhomHangVanChuyen.Value = dataRow["MaDanhMucNhomHangVanChuyen"];
            txtMaDanhMucHangHoa.Value = dataRow["MaDanhMucHangHoa"];
            txtKhoiLuong.Value = dataRow["KhoiLuong"];
            txtSoContainer.Value = dataRow["SoContainer"];
            txtTenDanhMucDiaDiemLayContHang.Value = dataRow["TenDanhMucDiaDiemLayCont"];
            txtTenDanhMucDiaDiemTraContHang.Value = dataRow["TenDanhMucDiaDiemTraCont"];
            txtNgayDongHang.Value = dataRow["NgayDongHang"];
            txtGioDongHang.Value = dataRow["GioDongHang"];
            txtNgayTraHang.Value = dataRow["NgayTraHang"];
            txtGioTraHang.Value = dataRow["GioTraHang"];
            txtTenDanhMucDiaDiemLayHang.Value = dataRow["TenDanhMucDiaDiemLayHang"];
            txtTenDanhMucDiaDiemTraHang.Value = dataRow["TenDanhMucDiaDiemTraHang"];
            txtTenDanhMucTuyenVanTai.Value = dataRow["TenDanhMucTuyenVanTai"];
            txtNgayCatMang.Value = dataRow["NgayCatMang"];
            txtGioCatMang.Value = dataRow["GioCatMang"];
            txtNguoiGiaoNhan.Value = dataRow["NguoiGiaoNhan"];
            txtSoDienThoaiGiaoNhan.Value = dataRow["SoDienThoaiGiaoNhan"];
            txtGhiChuCongViec.Value = dataRow["GhiChuCongViec"];
            //
            cboIDDanhMucThauPhu.Value = dataRow["IDDanhMucThauPhu"];
            txtTenDanhMucThauPhu.Value = dataRow["TenDanhMucThauPhu"];
            txtMaSoThueCMND.Value = dataRow["MaSoThueCMNDThauPhu"];
            cboIDDanhMucXe.Value = dataRow["IDDanhMucXe"];
            txtBienSoXeNgoai.Value = dataRow["BienSoXeNgoai"];
            cboIDDanhMucTaiXe.Value = dataRow["IDDanhMucTaiXe"];
            txtDienThoai.Value = dataRow["DienThoai"];
            txtSoCMND.Value = dataRow["SoCMND"];
            cboTrangThaiDonHang.Value = dataRow["TrangThaiDonHang"];
            if (cenCommon.cenCommon.IsNull(cboTrangThaiDonHang.Value)) cboTrangThaiDonHang.Value = "1";
            foreach (DataRow drDonHangKetHop in dtDonHangKetHop.Rows)
            {
                txtSoDonHangKetHop.Value += drDonHangKetHop["SoDonHang"] + ";";
            }
            txtGhiChu.Value = dataRow["GhiChu"];
            if (CapNhat == cenCommon.ThaoTacDuLieu.Xem)
            {
                cmdSave.Enabled = false;
                cmdSaveNew.Enabled = false;
            }
        }
        private void cboIDDanhMucTaiXe_ValueChanged(object sender, EventArgs e)
        {
            txtDienThoai.Value = null;
            txtSoCMND.Value = null;
            //
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucTaiXe.Value) || !cboIDDanhMucTaiXe.IsItemInList()) return;
            DanhMucTaiXeBUS DanhMucTaiXeBUS = new DanhMucTaiXeBUS();
            DataTable dt = DanhMucTaiXeBUS.List(cboIDDanhMucTaiXe.Value, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTaiXe)), null, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtDienThoai.Value = dt.Rows[0]["SoDienThoai"];
                txtSoCMND.Value = dt.Rows[0]["SoCMND"];
            }
        }
        private void txtSoDonHangKetHop_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            OpenChiTietDonHangKetHop();
        }
        private void txtSoDonHangKetHop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                OpenChiTietDonHangKetHop();
            }
        }
        private void cboIDDanhMucThauPhu_ValueChanged(object sender, EventArgs e)
        {
            //
            txtTenDanhMucThauPhu.Value = null;
            txtMaSoThueCMND.Value = null;
            cboIDDanhMucXe.Value = null;
            cboIDDanhMucTaiXe.Value = null;
            cboIDDanhMucXe.DataSource = null;
            cboIDDanhMucTaiXe.DataSource = null;
            //
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucThauPhu.Value) || !cboIDDanhMucThauPhu.IsItemInList()) return;
            DanhMucThauPhuBUS DanhMucThauPhuBUS = new DanhMucThauPhuBUS();
            DataTable dt = DanhMucThauPhuBUS.List(cboIDDanhMucThauPhu.Value, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu)), null, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtTenDanhMucThauPhu.Value = dt.Rows[0]["Ten"];
                txtMaSoThueCMND.Value = dt.Rows[0]["MaSoThueCMND"];
            }
            //Xe
            DanhMucXeBUS DanhMucXeBUS = new DanhMucXeBUS();
            dtXe = DanhMucXeBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongXe)), cboIDDanhMucThauPhu.Value, dataRow["IDDanhMucNhomHangVanChuyen"], null);
            cboIDDanhMucXe.dtValid = dtXe;
            cboIDDanhMucXe.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongXe));
            cboIDDanhMucXe.procedureName = DanhMucXe.listProcedureName;
            cboIDDanhMucXe.DataSource = dtXe;
            cboIDDanhMucXe.ValueMember = "ID";
            cboIDDanhMucXe.DisplayMember = "BienSo";
            //Tài xế
            DanhMucTaiXeBUS DanhMucTaiXeBUS = new DanhMucTaiXeBUS();
            dtTaiXe = DanhMucTaiXeBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTaiXe)), cboIDDanhMucThauPhu.Value, null);
            cboIDDanhMucTaiXe.dtValid = dtTaiXe;
            cboIDDanhMucTaiXe.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTaiXe));
            cboIDDanhMucTaiXe.procedureName = DanhMucTaiXe.listProcedureName;
            cboIDDanhMucTaiXe.DataSource = dtTaiXe;
            cboIDDanhMucTaiXe.ValueMember = "ID";
            cboIDDanhMucTaiXe.DisplayMember = "Ten";
        }
        private void OpenChiTietDonHangKetHop()
        {
            if (!cenCommon.cenCommon.IsNull(ctDieuHanhBUS.ListChiTietDonHangChinh(dataRow["ID"])) && int.Parse(ctDieuHanhBUS.ListChiTietDonHangChinh(dataRow["ID"]).ToString()) > 0)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Đây là đơn hàng nhánh, không được phép khai báo đơn hàng kẹp/kết hợp");
                return;
            }    
            //Update số đơn hàng kết hợp của đơn hàng này
            frm_ctDieuHanhChiTietDonHangKetHopUpdate frmUpdate = new frm_ctDieuHanhChiTietDonHangKetHopUpdate()
            {
                IDDanhMucChungTu = IDDanhMucChungTu,
                IDChungTu = dataRow["ID"],
                IDChungTuChiTiet = dataRow["IDctDieuHanh"],
                dt = dtDonHangKetHop.Copy(),
            };
            frmUpdate.ShowDialog();
            if (frmUpdate.Saved)
            {
                txtSoDonHangKetHop.Value = null;
                dtDonHangKetHop = frmUpdate.dt;
                foreach (DataRow drDonHangKetHop in dtDonHangKetHop.Rows)
                {
                    if (drDonHangKetHop.RowState != DataRowState.Deleted)
                        txtSoDonHangKetHop.Value += drDonHangKetHop["SoDonHang"] + ";";
                }
            }
            frmUpdate.Dispose();
        }

    }
}

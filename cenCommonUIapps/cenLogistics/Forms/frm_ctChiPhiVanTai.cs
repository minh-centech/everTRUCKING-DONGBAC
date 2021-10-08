using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctChiPhiVanTai : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        public frm_ctChiPhiVanTai()
        {
            InitializeComponent();
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Caption = "Cập nhật chi phí (CTRL+E)";
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Caption = "Xóa chi phí (CTRL+D)";
            txtDenNgay.DateTime = DateTime.Now;
            txtTuNgay.DateTime = DateTime.Now;
        }
        protected override void List()
        {
            //NhomHangVanChuyen
            DanhMucDoiTuongBUS NhomHangVanChuyenBUS = new DanhMucDoiTuongBUS();
            DataTable dtNhomHangVanChuyen = NhomHangVanChuyenBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen)), null);
            cboIDDanhMucNhomHangVanChuyen.dtValid = dtNhomHangVanChuyen;
            cboIDDanhMucNhomHangVanChuyen.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen));
            cboIDDanhMucNhomHangVanChuyen.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucNhomHangVanChuyen.DataSource = dtNhomHangVanChuyen;
            cboIDDanhMucNhomHangVanChuyen.ValueMember = "ID";
            cboIDDanhMucNhomHangVanChuyen.DisplayMember = "Ten";
            //
            ctChiPhiVanTaiBUS bus = new ctChiPhiVanTaiBUS();
            dtData = bus.ListDisplay(IDDanhMucChungTu, txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucNhomHangVanChuyen.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.FixedColumnsList = "[So][NgayDongTraHang][DebitNote][BillBooking]";
            ug.DataSource = bsDanhMuc;
        }

        protected override void InsertDanhMuc()
        {
        }


        protected override void UpdateDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Sua)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền sửa dữ liệu chứng từ này!");
                return;
            }
            if (bsDanhMuc.Current == null) return;

            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;

            frm_ctChiPhiVanTaiUpdate frmUpdate = new frm_ctChiPhiVanTaiUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameChiPhiVanTai,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData,
                IDctDonHang = dr["ID"]
            };
            frmUpdate.ShowDialog();
            dr["IDctChiPhiVanTai"] = frmUpdate.ID;
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            if (bsDanhMuc.Current == null) return;
            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;
            if (cenCommon.cenCommon.IsNull(dr["IDctChiPhiVanTai"])) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotChiPhiVanTaiGuiKeToan(dr["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt chi phí, không xóa được"); return; }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctChiPhiVanTaiBUS BUS = new ctChiPhiVanTaiBUS();
            if (BUS.Delete(new ctChiPhiVanTai() { ID = dr["IDctChiPhiVanTai"] }))
            {
                dr["IDctChiPhiVanTai"] = DBNull.Value;
                dtData.AcceptChanges();
            }
        }
        protected override void In()
        {
        }

        private void ug_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucChuXe"].Value) && e.Row.Cells["MaDanhMucChuXe"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Đơn")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(206, 231, 255);
            }
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucChuXe"].Value) && e.Row.Cells["MaDanhMucChuXe"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(131, 192, 255);
            }
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucChuXe"].Value) && !e.Row.Cells["MaDanhMucChuXe"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(202, 237, 97);
            }
        }
    }
}

using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctHotline : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        public frm_ctHotline()
        {
            InitializeComponent();
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Caption = "Cập nhật (CTRL+E)";
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Caption = "Xóa (CTRL+D)";
            txtTuNgay.Value = DateTime.Now;
            txtDenNgay.Value = DateTime.Now;
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
            ctHotlineBUS bus = new ctHotlineBUS();
            dtData = bus.ListDisplay(IDDanhMucChungTu, null, txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucNhomHangVanChuyen.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.FixedColumnsList = "[So][DebitNote]";
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

            frm_ctHotlineUpdate frmUpdate = new frm_ctHotlineUpdate
            {
                CapNhat = (cenCommon.cenCommon.IsNull(dr["IDctHotline"])) ? cenCommon.ThaoTacDuLieu.Them : cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameHotline,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData,
                dataRow = dr,
                IDctDonHang = dr["ID"]
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.ID))
            {
                ctHotlineBUS BUS = new ctHotlineBUS();
                DataRow drChungTuUpdate = BUS.ListDisplay(IDDanhMucChungTu, dr["ID"], txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucNhomHangVanChuyen.Value).Rows[0];
                dr.ItemArray = drChungTuUpdate.ItemArray;
            }
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            if (bsDanhMuc.Current == null) return;
            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;
            if (cenCommon.cenCommon.IsNull(dr["IDctHotline"])) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(dr["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt doanh thu, không xóa thông tin hotline!"); return; }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctHotlineBUS BUS = new ctHotlineBUS();
            if (BUS.Delete(new ctHotline() { ID = dr["IDctHotline"] }))
            {
                dr["DienThoai"] = DBNull.Value;
                dr["ThongTinThuTuc"] = DBNull.Value;
                dr["GhiChuHotline"] = DBNull.Value;
                dr["TinhTrangHotline"] = DBNull.Value;
                dr["TrangThaiHotline"] = DBNull.Value;
                dtData.AcceptChanges();
            }
        }
        protected override void In()
        {
        }
    }
}

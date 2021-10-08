using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenControls;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctInGiayVanTai : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        saComboDanhMuc cboLoaiDonChuyen = null;
        public frm_ctInGiayVanTai()
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
            DanhMucKhachHangBUS KhachHangBUS = new DanhMucKhachHangBUS();
            DataTable dtKhachHang = KhachHangBUS.Valid(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang)), null);
            cboIDDanhMucChuXe.dtValid = dtKhachHang;
            cboIDDanhMucChuXe.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang));
            cboIDDanhMucChuXe.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucChuXe.DataSource = dtKhachHang;
            cboIDDanhMucChuXe.ValueMember = "ID";
            cboIDDanhMucChuXe.DisplayMember = "Ten";

            ////Loại hàng
            //cboLoaiHang = new saComboDanhMuc();
            //cboLoaiHang.Items.Add(1, "Nhập");
            //cboLoaiHang.Items.Add(2, "Xuất");
            //cboLoaiHang.Items.Add(3, "Nội địa");
            //
            cboLoaiDonChuyen = new saComboDanhMuc();
            cboLoaiDonChuyen.Items.Add(1, "Đơn chuyển, hàng nhẹ");
            cboLoaiDonChuyen.Items.Add(2, "Đơn chuyển, hàng nặng");
            //
            ctInGiayVanTaiBUS bus = new ctInGiayVanTaiBUS();
            dtData = bus.List(null, IDDanhMucChungTu, txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucChuXe.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.HiddenColumnsList = "[NgayLap]";
            ug.FixedColumnsList = "[So][NgayDongTraHang][DebitNote][BillBooking]";
            ug.DataSource = bsDanhMuc;
            ug.DisplayLayout.Bands[0].Columns["LoaiDonChuyen"].EditorComponent = cboLoaiDonChuyen;
            ug.DisplayLayout.Bands[0].Columns["LoaiDonChuyen"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
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

            frm_ctInGiayVanTaiUpdate frmUpdate = new frm_ctInGiayVanTaiUpdate
            {
                CapNhat = (cenCommon.cenCommon.IsNull(dr["IDctInGiayVanTai"])) ? cenCommon.ThaoTacDuLieu.Them : cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameInGiayVanTai,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData,
                dataRow = dr,
                IDctDonHang = dr["ID"]
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            if (bsDanhMuc.Current == null) return;
            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;
            if (cenCommon.cenCommon.IsNull(dr["IDctInGiayVanTai"])) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctInGiayVanTaiBUS BUS = new ctInGiayVanTaiBUS();
            if (BUS.Delete(new ctInGiayVanTai() { ID = dr["IDctInGiayVanTai"] }))
            {
                dr["LoaiDonChuyen"] = DBNull.Value;
                dr["SoKmVoHangNhe"] = DBNull.Value;
                dr["SoKmHangNhe"] = DBNull.Value;
                dr["SoKmVoHangNang"] = DBNull.Value;
                dr["SoKmHangNang"] = DBNull.Value;
                dr["SoLuongNhienLieuDinhMuc"] = DBNull.Value;
                dr["SoLuongNhienLieuCapThem"] = DBNull.Value;
                dr["SoLuongNhienLieu"] = DBNull.Value;
                dr["LyDoCapThem"] = DBNull.Value;
                dr["SoTienVeCauDuong"] = DBNull.Value;
                dr["SoTienLuatAnCa"] = DBNull.Value;
                dr["SoTienTamUng"] = DBNull.Value;
                dr["GhiChu"] = DBNull.Value;
                dtData.AcceptChanges();
            }
        }

        private void ug_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //if (!cenCommon.cenCommon.IsNull(e.Row.Cells["IDctChotInGiayVanTaiGuiKeToan"].Value))
            //{
            //    e.Row.Appearance.ForeColor = Color.DarkBlue;
            //}
        }

        private void ug_AfterRowActivate(object sender, EventArgs e)
        {
            //if (!cenCommon.cenCommon.IsNull(ug.ActiveRow.Cells["IDctChotInGiayVanTaiGuiKeToan"].Value) && !ug.ActiveRow.IsFilterRow)
            //{
            //    ug.DisplayLayout.Bands[0].Override.ActiveRowAppearance.ForeColor = Color.DarkBlue;
            //}
            //else
            //{
            //    ug.DisplayLayout.Bands[0].Override.ActiveRowAppearance.ForeColor = Color.Black;
            //}
        }

        protected override void In()
        {
        }
    }
}

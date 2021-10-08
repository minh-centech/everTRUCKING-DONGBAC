using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctDieuHanh : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        public frm_ctDieuHanh()
        {
            InitializeComponent();
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Caption = "Cập nhật (CTRL+E)";
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Caption = "Xóa (CTRL+D)";
            txtTuNgay.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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
            ctDieuHanhBUS bus = new ctDieuHanhBUS();
            dtData = bus.ListDisplay(IDDanhMucChungTu, null, txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucNhomHangVanChuyen.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.HiddenColumnsList = "[LoaiHang][NgayLap]";
            ug.FixedColumnsList = "[So][DebitNote]";
            ug.DataSource = bsDanhMuc;
            //
            ctDonHangBUS ctDonHangBUS = new ctDonHangBUS();
            DataTable dtTotalNhomHangVanChuyen = ctDonHangBUS.ListNhomHangVanChuyen(IDDanhMucChungTu, txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucNhomHangVanChuyen.Value);
            BindingSource bsTotal = new BindingSource();
            bsTotal.DataSource = dtTotalNhomHangVanChuyen;

            txtTotal.MaskInput = "";
            txtCont.MaskInput = "";
            txtTruck.MaskInput = "";

            txtTotal.SetDataBinding(bsTotal, "Total", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCont.SetDataBinding(bsTotal, "Cont", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTruck.SetDataBinding(bsTotal, "Truck", true, DataSourceUpdateMode.OnPropertyChanged);
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

            frm_ctDieuHanhUpdate frmUpdate = new frm_ctDieuHanhUpdate
            {
                CapNhat = (cenCommon.cenCommon.IsNull(dr["IDctDieuHanh"])) ? cenCommon.ThaoTacDuLieu.Them : cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameDieuHanh,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData,
                dataRow = dr,
                IDctDonHang = dr["ID"]
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.ID))
            {
                ctDieuHanhBUS BUS = new ctDieuHanhBUS();
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
            if (cenCommon.cenCommon.IsNull(dr["IDctDieuHanh"])) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(dr["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt doanh thu, không xóa thông tin điều hành!"); return; }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctDieuHanhBUS BUS = new ctDieuHanhBUS();
            if (BUS.Delete(new ctDieuHanh() { ID = dr["IDctDieuHanh"] }))
            {
                DataRow drChungTuUpdate = BUS.ListDisplay(IDDanhMucChungTu, dr["ID"], txtTuNgay.Value, txtDenNgay.Value, cboIDDanhMucNhomHangVanChuyen.Value).Rows[0];
                dr.ItemArray = drChungTuUpdate.ItemArray;
                dtData.AcceptChanges();
            }
        }
        protected override void In()
        {
        }

        private void ug_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucThauPhu"].Value) && e.Row.Cells["MaDanhMucThauPhu"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Đơn")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(206, 231, 255);
            }
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucThauPhu"].Value) && e.Row.Cells["MaDanhMucThauPhu"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(131, 192, 255);
            }
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucThauPhu"].Value) && !e.Row.Cells["MaDanhMucThauPhu"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(202, 237, 97);
            }
        }
    }
}

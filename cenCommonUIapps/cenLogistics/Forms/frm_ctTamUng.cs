using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctTamUng : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        public frm_ctTamUng()
        {
            InitializeComponent();
            txtTuNgay.DateTime = DateTime.Now;
            txtDenNgay.Value = DateTime.Now;
        }
        protected override void List()
        {
            ctDonHangBUS bus = new ctDonHangBUS();
            dtData = bus.ListDisplay(DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)), null, txtTuNgay.Value, txtDenNgay.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.HiddenColumnsList = "[NgayDongTraHang]";
            ug.DataSource = bsDanhMuc;
            //
            DataTable dtNhomHangVanChuyen = bus.ListNhomHangVanChuyen(IDDanhMucChungTu, txtTuNgay.Value, txtDenNgay.Value, null);
            BindingSource bsTotal = new BindingSource();
            bsTotal.DataSource = dtNhomHangVanChuyen;

            txtTotal.MaskInput = "-nnnn";
            txtCont.MaskInput = "-nnnn";
            txtTruck.MaskInput = "-nnnn";

            txtTotal.SetDataBinding(bsTotal, "Total", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCont.SetDataBinding(bsTotal, "Cont", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTruck.SetDataBinding(bsTotal, "Truck", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        protected override void InsertDanhMuc()
        {
            return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm dữ liệu chứng từ này!");
                return;
            }
            frm_ctTamUngUpdate frmUpdate = new frm_ctTamUngUpdate
            {
                UpdateMode = cenCommon.ThaoTacDuLieu.Them,
                Text = cenCommon.LoaiManHinh.NameDonHang,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.IDChungTu))
            {
                ctDonHangBUS bus = new ctDonHangBUS();
                DataTable dtChungTuInsert = bus.ListDisplay(IDDanhMucChungTu, frmUpdate.IDChungTu, null, null);
                dtData.Merge(dtChungTuInsert);
            }
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            if (ug.ActiveRow == null || !ug.ActiveRow.IsDataRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Sua)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền sửa dữ liệu chứng từ này!");
                return;
            }
            if (bsDanhMuc.Current == null) return;

            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;

            //if (cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(dr["ID"])))
            //{

                frm_ctTamUngUpdate frmUpdate = new frm_ctTamUngUpdate
                {
                    UpdateMode = cenCommon.ThaoTacDuLieu.Sua,
                    Text = cenCommon.LoaiManHinh.NameTamUng,
                    IDChungTu = dr["ID"],
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    LoaiManHinh = LoaiManHinh
                };
                frmUpdate.ShowDialog();
                if (!cenCommon.cenCommon.IsNull(frmUpdate.IDChungTu))
                {
                    ctDonHangBUS bus = new ctDonHangBUS();
                    DataTable dtChungTuUpdate = bus.ListDisplay(IDDanhMucChungTu, frmUpdate.IDChungTu, null, null);
                    dr.ItemArray = dtChungTuUpdate.Rows[0].ItemArray;
                }
                frmUpdate.Dispose();
            //}
            //else
            //    ViewDanhMuc();
        }
        protected override void DeleteDanhMuc()
        {
            return;
            if (ug.ActiveRow == null || !ug.ActiveRow.IsDataRow) return;
            if (bsDanhMuc.Current == null) return;
            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            //if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(dr["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt doanh thu, không xóa được!"); return; }
            //if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotChiPhiVanTaiGuiKeToan(dr["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt chi phí, không xóa được!"); return; }
            //if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotChiPhiVanTaiBoSungGuiKeToan(dr["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt chi phí bổ sung, không xóa được!"); return; }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctDonHangBUS BUS = new ctDonHangBUS();
            ctDonHang obj = new ctDonHang()
            {
                ID = dr["ID"]
            };
            if (BUS.Delete(obj))
            {
                if (ug.ActiveRow == null) return;
                int i = ug.ActiveRow.Index;
                bsDanhMuc.RemoveCurrent();
                if (i > 0) i += 1;
                if (i <= ug.Rows.Count - 1)
                {
                    ug.Focus();
                    ug.Rows[i].Activate();
                }
            }
        }
        protected override void In()
        {
        }
        protected override void ViewDanhMuc()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xem dữ liệu chứng từ này!");
                return;
            }
            if (bsDanhMuc.Current == null) return;

            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;

            frm_ctTamUngUpdate frmUpdate = new frm_ctTamUngUpdate
            {
                UpdateMode = cenCommon.ThaoTacDuLieu.Xem,
                Text = cenCommon.LoaiManHinh.NameDonHang,
                IDChungTu = dr["ID"],
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.IDChungTu))
            {
                ctDonHangBUS bus = new ctDonHangBUS();
                DataTable dtChungTuUpdate = bus.ListDisplay(IDDanhMucChungTu, frmUpdate.IDChungTu, null, null);
                dr.ItemArray = dtChungTuUpdate.Rows[0].ItemArray;
            }
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm dữ liệu chứng từ này!");
                return;
            }
            if (bsDanhMuc.Current == null) return;

            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;

            frm_ctTamUngUpdate frmUpdate = new frm_ctTamUngUpdate
            {
                UpdateMode = cenCommon.ThaoTacDuLieu.Copy,
                Text = cenCommon.LoaiManHinh.NameDonHang,
                IDChungTu = dr["ID"],
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.IDChungTu))
            {
                ctDonHangBUS bus = new ctDonHangBUS();
                DataTable dtChungTuInsert = bus.ListDisplay(IDDanhMucChungTu, frmUpdate.IDChungTu, null, null);
                dtData.Merge(dtChungTuInsert);
            }
            frmUpdate.Dispose();
        }

        private void ug_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            //if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucThauPhu"].Value) && e.Row.Cells["MaDanhMucThauPhu"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Đơn")
            //{
            //    e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(206, 231, 255);
            //}
            //if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucThauPhu"].Value) && e.Row.Cells["MaDanhMucThauPhu"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            //{
            //    e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(131, 192, 255);
            //}
            //if (!cenCommon.cenCommon.IsNull(e.Row.Cells["MaDanhMucThauPhu"].Value) && !e.Row.Cells["MaDanhMucThauPhu"].Value.ToString().ToUpper().StartsWith("PLJ") && e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            //{
            //    e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(202, 237, 97);
            //}
            if (!cenCommon.cenCommon.IsNull(e.Row.Cells["Huy"].Value) && bool.Parse(e.Row.Cells["Huy"].Value.ToString()))
            {
                e.Row.Appearance.ForeColor = Color.Red;
            }
        }
        private void ug_AfterRowActivate(object sender, EventArgs e)
        {
            if (ug.ActiveRow.IsDataRow && !cenCommon.cenCommon.IsNull(ug.ActiveRow.Cells["Huy"].Value) && bool.Parse(ug.ActiveRow.Cells["Huy"].Value.ToString()))
            {
                ug.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Red;
            }
            else
            {
                ug.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Black;
            }
        }
    }
}

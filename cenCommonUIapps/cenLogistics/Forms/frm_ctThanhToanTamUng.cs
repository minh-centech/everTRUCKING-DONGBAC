using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctThanhToanTamUng : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        public frm_ctThanhToanTamUng()
        {
            InitializeComponent();
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btThem"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btCopy"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["btSua"].SharedProps.Caption = "Cập nhật thanh toán (CTRL+E)";
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Enabled = false;
            UltraToolbarsManager1.Tools["btXoa"].SharedProps.Visible = false;
            txtTuNgay.Value = DateTime.Now;
            txtDenNgay.Value = DateTime.Now;
        }
        protected override void List()
        {
            ctThanhToanTamUngBUS bus = new ctThanhToanTamUngBUS();
            dtData = bus.ListDisplay(IDDanhMucChungTu, null, null, txtTuNgay.Value, txtDenNgay.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.FixedColumnsList = "[NgayDongTraHang][So][DebitNote][BillBooking]";
            ug.DataSource = bsDanhMuc;
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

            frm_ctThanhToanTamUngUpdate frmUpdate = new frm_ctThanhToanTamUngUpdate
            {
                UpdateMode = cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameThanhToanTamUng,
                IDChungTu = dr["ID"],
                IDChungTuChiTiet = dr["IDctDonHangChiTietTamUng"],
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.IDChungTu))
            {
                ctThanhToanTamUngBUS bus = new ctThanhToanTamUngBUS();
                DataTable dtChungTuUpdate = bus.ListDisplay(IDDanhMucChungTu, frmUpdate.IDChungTu, frmUpdate.IDChungTuChiTiet, null, null);
                dr.ItemArray = dtChungTuUpdate.Rows[0].ItemArray;
            }
            frmUpdate.Dispose();
        }

        private void ug_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            if (e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Nhánh")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(206, 231, 255);
            }
            if (e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kẹp")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(131, 192, 255);
            }
            if (e.Row.Cells["TrangThaiDonHang"].Value.ToString() == "Kết hợp")
            {
                e.Row.Appearance.BackColor = System.Drawing.Color.FromArgb(202, 237, 97);
            }
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

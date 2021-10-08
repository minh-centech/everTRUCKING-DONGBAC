using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenControls;
using cenDTO.cenLogistics;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctHopDongVanChuyen : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        saComboDanhMuc cboTrangThaiHopDong;
        public frm_ctHopDongVanChuyen()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            DanhMucThamSoNguoiSuDungBUS.UpdateGiaTri(cenCommon.ThamSoNguoiSuDung.ctHopDongVanChuyen_TuNgay, txtTuNgay.Value.ToString());
            DanhMucThamSoNguoiSuDungBUS.UpdateGiaTri(cenCommon.ThamSoNguoiSuDung.ctHopDongVanChuyen_DenNgay, txtDenNgay.Value.ToString());
            //
            ctHopDongVanChuyenBUS bus = new ctHopDongVanChuyenBUS();
            dtData = bus.List(DBNull.Value, IDDanhMucChungTu, txtTuNgay.Value, txtDenNgay.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.HiddenColumnsList = "[So][NgayLap][SoHopDong][NgayHopDong][TrangThaiHopDong]";
            ug.FixedColumnsList = "[SoHopDong][NgayHopDong][TrangThaiHopDong]";
            ug.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand, AutoResizeColumnWidthOptions.IncludeCells);
            ug.DataSource = bsDanhMuc;
            //TrangThaiThucHienHopDong
            cboTrangThaiHopDong = new saComboDanhMuc();
            cboTrangThaiHopDong.Items.Add(0, "Chưa thực hiện");
            cboTrangThaiHopDong.Items.Add(1, "Đang thực hiện");
            cboTrangThaiHopDong.Items.Add(2, "Hoàn thành");
            cboTrangThaiHopDong.Items.Add(3, "Hủy");
            ug.DisplayLayout.Bands[0].Columns["TrangThaiHopDong"].EditorComponent = cboTrangThaiHopDong;
            ug.DisplayLayout.Bands[0].Columns["TrangThaiHopDong"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
        }

        protected override void InsertDanhMuc()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm dữ liệu chứng từ này!");
                return;
            }
            frm_ctHopDongVanChuyenUpdate frmUpdate = new frm_ctHopDongVanChuyenUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                Text = cenCommon.LoaiManHinh.NameHopDongVanChuyen,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
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

            frm_ctHopDongVanChuyenUpdate frmUpdate = new frm_ctHopDongVanChuyenUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameHopDongVanChuyen,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData,
                dataRow = dr,
                ID = dr["ID"]
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            if (bsDanhMuc.Current == null) return;
            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;

            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctHopDongVanChuyenBUS BUS = new ctHopDongVanChuyenBUS();
            if (BUS.Delete(new ctHopDongVanChuyen() { ID = dr["ID"] }))
            {
                if (ug.ActiveRow == null) return;
                int i = ug.ActiveRow.Index;
                bsDanhMuc.RemoveCurrent();
                if (i > 0) i -= 1;
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
    }
}

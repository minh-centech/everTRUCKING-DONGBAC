using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctPhieuDoNhienLieu : cenBase.BaseForms.frmBaseChungTuSingleList
    {
        public frm_ctPhieuDoNhienLieu()
        {
            InitializeComponent();
            txtTuNgay.Value = DateTime.Now;
            txtDenNgay.Value = DateTime.Now;
        }
        protected override void List()
        {
            //
            ctPhieuDoNhienLieuBUS bus = new ctPhieuDoNhienLieuBUS();
            dtData = bus.ListDisplay(IDDanhMucChungTu, null, txtTuNgay.Value, txtDenNgay.Value);
            bsDanhMuc = new BindingSource();
            bsDanhMuc.DataSource = dtData;
            ug.FixedColumnsList = "[So][NgayLap][NgayDoNhienLieu]";
            ug.DataSource = bsDanhMuc;
        }

        protected override void InsertDanhMuc()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm dữ liệu chứng từ này!");
                return;
            }

            frm_ctPhieuDoNhienLieuUpdate frmUpdate = new frm_ctPhieuDoNhienLieuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                Text = cenCommon.LoaiManHinh.NamePhieuDoNhienLieu,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.ID))
            {
                ctPhieuDoNhienLieuBUS BUS = new ctPhieuDoNhienLieuBUS();
                DataTable dtChungTuUpdate = BUS.ListDisplay(IDDanhMucChungTu, frmUpdate.ID, txtTuNgay.Value, txtDenNgay.Value);
                dtData.Merge(dtChungTuUpdate);
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

            frm_ctPhieuDoNhienLieuUpdate frmUpdate = new frm_ctPhieuDoNhienLieuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = cenCommon.LoaiManHinh.NameDieuHanh,
                IDDanhMucChungTu = IDDanhMucChungTu,
                LoaiManHinh = LoaiManHinh,
                dataTable = dtData,
                dataRow = dr,
                ID = dr["ID"],
                IDctDonHang = dr["IDChungTu"]
            };
            frmUpdate.ShowDialog();
            if (!cenCommon.cenCommon.IsNull(frmUpdate.ID))
            {
                ctPhieuDoNhienLieuBUS BUS = new ctPhieuDoNhienLieuBUS();
                DataRow drChungTuUpdate = BUS.ListDisplay(IDDanhMucChungTu, frmUpdate.ID, txtTuNgay.Value, txtDenNgay.Value).Rows[0];
                dr.ItemArray = drChungTuUpdate.ItemArray;
            }
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            if (ug.ActiveRow == null || !ug.ActiveRow.IsDataRow) return;
            if (bsDanhMuc.Current == null) return;
            DataRow dr = ((DataRowView)bsDanhMuc.Current).Row;
            if (cenCommon.cenCommon.IsNull(dr["ID"])) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu chứng từ này!");
                return;
            }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            ctPhieuDoNhienLieuBUS BUS = new ctPhieuDoNhienLieuBUS();
            if (BUS.Delete(new ctPhieuDoNhienLieu() { ID = dr["ID"] }))
            {
                bsDanhMuc.RemoveCurrent();
            }
        }
        protected override void In()
        {
            if (ug.ActiveRow == null || !ug.ActiveRow.IsDataRow) return;
            object IDChungTu = ug.ActiveRow.Cells["ID"].Value;
            if (!cenCommon.cenCommon.IsNull(IDChungTu))
                cenBase.Classes.ChungTu.inChungTu(IDDanhMucChungTu.ToString(), IDChungTu.ToString(), ctPhieuDoNhienLieu.tableName, null, this.MdiParent, false, cenCommon.GlobalVariables.reportPath + @"ctPhieuDoNhienLieu.rpt", ctPhieuDoNhienLieu.listProcedureName, "", 1, LoaiManHinh, 1);
        }
    }
}

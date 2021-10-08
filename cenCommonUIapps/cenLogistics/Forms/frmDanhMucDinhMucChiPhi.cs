using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmDanhMucDinhMucChiPhi : cenBase.BaseForms.frmBaseDanhMucChiTiet
    {
        DanhMucDinhMucChiPhiBUS BUS = new DanhMucDinhMucChiPhiBUS();
        public frmDanhMucDinhMucChiPhi()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xem dữ liệu danh mục này!");
                return;
            }
            BUS = new DanhMucDinhMucChiPhiBUS();
            dsData = BUS.List(DBNull.Value, IDDanhMucLoaiDoiTuong, null);
            bsDanhMuc = new BindingSource
            {
                DataSource = dsData,
                DataMember = DanhMucDinhMucChiPhi.tableName
            };
            ug.FixedColumnsList = "[Ma][Ten]";
            ug.HiddenColumnsList = "[MaDanhMucNhomHangVanChuyen][MaDanhMucHangHoa][MaDanhMucKhachHang][MaDanhMucChiPhiKinhDoanh]";
            ug.DataSource = bsDanhMuc;
            ug.DisplayLayout.Bands[0].Columns["NgayApDung"].MaskInput = cenCommon.GlobalVariables.MaskInputDateTime;

            bsDanhMucChiTiet = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = cenCommon.GlobalVariables.prefix_DataRelation + DanhMucDinhMucChiPhiXe.tableName
            };
            ugChiTiet.DataSource = bsDanhMucChiTiet;
        }
        protected override void InsertDanhMuc()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm mới dữ liệu danh mục này!");
                return;
            }
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucDinhMucChiPhiUpdate frmUpdate = new frmDanhMucDinhMucChiPhiUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dsData.Tables[DanhMucDinhMucChiPhi.tableName],
                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                TenDanhMucLoaiDoiTuong = TenDanhMucLoaiDoiTuong
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm mới dữ liệu danh mục này!");
                return;
            }
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucDinhMucChiPhiUpdate frmUpdate = new frmDanhMucDinhMucChiPhiUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                dataTable = dsData.Tables[DanhMucDinhMucChiPhi.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row,
                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                TenDanhMucLoaiDoiTuong = TenDanhMucLoaiDoiTuong
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Sua)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền sửa dữ liệu danh mục này!");
                return;
            }
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucDinhMucChiPhiUpdate frmUpdate = new frmDanhMucDinhMucChiPhiUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                dataTable = dsData.Tables[DanhMucDinhMucChiPhi.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row,
                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                TenDanhMucLoaiDoiTuong = TenDanhMucLoaiDoiTuong
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            if (ug.ActiveRow == null || ug.ActiveRow.IsFilterRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu danh mục này!");
                return;
            }
            base.DeleteDanhMuc();
            if (!bContinue) return;
            DataRow dataRow = ((DataRowView)bsDanhMuc.Current).Row;
            DanhMucDinhMucChiPhi obj = new DanhMucDinhMucChiPhi()
            {
                ID = dataRow["ID"]
            };
            if (BUS.Delete(obj))
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
        protected override void InsertDanhMucChiTiet()
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDinhMucChiPhiXe)), out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Them)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền thêm mới dữ liệu danh mục này!");
                return;
            }
            base.InsertDanhMucChiTiet();
            if (!bContinue) return;
            DataRow drDinhMucChiPhi = ((DataRowView)bsDanhMuc.Current).Row;
            frmDanhMucDinhMucChiPhiXeUpdate frmUpdate = new frmDanhMucDinhMucChiPhiXeUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dsData.Tables[DanhMucDinhMucChiPhiXe.tableName],
                IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDinhMucChiPhiXe)),
                TenDanhMucLoaiDoiTuong = TenDanhMucLoaiDoiTuong,
                IDDanhMucDinhMucChiPhi = drDinhMucChiPhi["ID"],
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMucChiTiet()
        {
            if (ugChiTiet.ActiveRow == null || ugChiTiet.ActiveRow.IsFilterRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDinhMucChiPhiXe)), out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Sua)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền sửa dữ liệu danh mục này!");
                return;
            }
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            DataRow drDinhMucChiPhi = ((DataRowView)bsDanhMuc.Current).Row;
            frmDanhMucDinhMucChiPhiXeUpdate frmUpdate = new frmDanhMucDinhMucChiPhiXeUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                dataTable = dsData.Tables[DanhMucDinhMucChiPhiXe.tableName],
                dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
                IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDinhMucChiPhiXe)),
                TenDanhMucLoaiDoiTuong = TenDanhMucLoaiDoiTuong,
                IDDanhMucDinhMucChiPhi = drDinhMucChiPhi["ID"]
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMucChiTiet()
        {
            if (ugChiTiet.ActiveRow == null || ugChiTiet.ActiveRow.IsFilterRow) return;
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDinhMucChiPhiXe)), out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xoa)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa dữ liệu danh mục này!");
                return;
            }
            base.DeleteDanhMucChiTiet();
            if (!bContinue) return;
            DataRow dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row;
            DanhMucDinhMucChiPhiXe obj = new DanhMucDinhMucChiPhiXe()
            {
                ID = dataRow["ID"]
            };
            DanhMucDinhMucChiPhiXeBUS BUSXe = new DanhMucDinhMucChiPhiXeBUS();
            if (BUSXe.Delete(obj))
            {
                if (ugChiTiet.ActiveRow == null) return;
                int i = ugChiTiet.ActiveRow.Index;
                bsDanhMucChiTiet.RemoveCurrent();
                if (i > 0) i -= 1;
                if (i <= ugChiTiet.Rows.Count - 1)
                {
                    ugChiTiet.Focus();
                    ugChiTiet.Rows[i].Activate();
                }
            }
        }
    }
}

using cenBUS.DatabaseCore;
using cenCommon;
using cenDTO.DatabaseCore;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucPhanQuyen : cenBase.BaseForms.frmBaseDanhMucChiTiet
    {
        BindingSource bsLoaiDoiTuong = null, bsChungTu = null, bsBaoCao = null;
        DanhMucPhanQuyenBUS BUS = new DanhMucPhanQuyenBUS();
        DanhMucPhanQuyenDonViBUS BUSDonVi = new DanhMucPhanQuyenDonViBUS();
        DanhMucPhanQuyenLoaiDoiTuongBUS BUSLoaiDoiTuong = new DanhMucPhanQuyenLoaiDoiTuongBUS();
        DanhMucPhanQuyenChungTuBUS BUSChungTu = new DanhMucPhanQuyenChungTuBUS();
        DanhMucPhanQuyenBaoCaoBUS BUSBaoCao = new DanhMucPhanQuyenBaoCaoBUS();
        public frmDanhMucPhanQuyen()
        {
            InitializeComponent();
        }
        protected override void List()
        {

            dsData = BUS.List(null);

            bsDanhMuc = new BindingSource
            {
                DataSource = dsData,
                DataMember = DanhMucPhanQuyen.tableName
            };

            bsDanhMucChiTiet = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucPhanQuyenDonVi.tableName
            };

            bsLoaiDoiTuong = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucPhanQuyenLoaiDoiTuong.tableName
            };

            bsChungTu = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucPhanQuyenChungTu.tableName
            };

            bsBaoCao = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucPhanQuyenBaoCao.tableName
            };

            ug.DataSource = bsDanhMuc;
            ugChiTiet.DataSource = bsDanhMucChiTiet;
            ugLoaiDoiTuong.DataSource = bsLoaiDoiTuong;
            ugChungTu.DataSource = bsChungTu;
            ugBaoCao.DataSource = bsBaoCao;

            tabChiTiet.Tabs["tabChiTiet"].Text = cenCommon.cenCommon.TraTuDien(DanhMucPhanQuyenDonVi.tableName);
            tabChiTiet.Tabs["tabLoaiDoiTuong"].Text = cenCommon.cenCommon.TraTuDien(DanhMucPhanQuyenLoaiDoiTuong.tableName);
            tabChiTiet.Tabs["tabChungTu"].Text = cenCommon.cenCommon.TraTuDien(DanhMucPhanQuyenChungTu.tableName);
            tabChiTiet.Tabs["tabBaoCao"].Text = cenCommon.cenCommon.TraTuDien(DanhMucPhanQuyenBaoCao.tableName);
        }

        protected override void InsertDanhMuc()
        {
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucPhanQuyenUpdate frmUpdate = new frmDanhMucPhanQuyenUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dsData.Tables[DanhMucPhanQuyen.tableName],
                TenDanhMucLoaiDoiTuong = "Thêm mới Danh mục phân quyền",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucPhanQuyenUpdate frmUpdate = new frmDanhMucPhanQuyenUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                TenDanhMucLoaiDoiTuong = "Sao chép Danh mục phân quyền",
                dataTable = dsData.Tables[DanhMucPhanQuyen.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucPhanQuyenUpdate frmUpdate = new frmDanhMucPhanQuyenUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền",
                dataTable = dsData.Tables[DanhMucPhanQuyen.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            base.DeleteDanhMuc();
            if (!bContinue) return;
            DataRow dataRow = ((DataRowView)bsDanhMuc.Current).Row;
            DanhMucPhanQuyen obj = new DanhMucPhanQuyen()
            {
                ID = dataRow["ID"]
            };
            int i = ug.ActiveRow.Index;
            if (BUS.Delete(obj))
            {
                bsDanhMuc.RemoveCurrent();
                if (i > 0) i -= 1;
                if (i <= ug.Rows.Count - 1)
                {
                    ug.Focus();
                    ug.Rows[i].Activate();
                }
            }
        }

        protected override void UpdateDanhMucChiTiet()
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;

            switch (tabChiTiet.SelectedTab.Key.ToUpper())
            {
                case "TABCHITIET":
                    frmDanhMucPhanQuyenDonViUpdate frmDanhMucPhanQuyenDonViUpdate = new frmDanhMucPhanQuyenDonViUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền đơn vị",
                        IDDanhMucPhanQuyen = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucPhanQuyen"],
                        dataTable = dsData.Tables[DanhMucPhanQuyenDonVi.tableName],
                        dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
                    };
                    frmDanhMucPhanQuyenDonViUpdate.ShowDialog();
                    frmDanhMucPhanQuyenDonViUpdate.Dispose();
                    break;
                case "TABLOAIDOITUONG":
                    frmDanhMucPhanQuyenLoaiDoiTuongUpdate frmDanhMucPhanQuyenLoaiDoiTuongUpdate = new frmDanhMucPhanQuyenLoaiDoiTuongUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền loại đối tượng",
                        IDDanhMucPhanQuyen = ((DataRowView)bsLoaiDoiTuong.Current).Row["IDDanhMucPhanQuyen"],
                        dataTable = dsData.Tables[DanhMucPhanQuyenLoaiDoiTuong.tableName],
                        dataRow = ((DataRowView)bsLoaiDoiTuong.Current).Row,
                    };
                    frmDanhMucPhanQuyenLoaiDoiTuongUpdate.ShowDialog();
                    frmDanhMucPhanQuyenLoaiDoiTuongUpdate.Dispose();
                    break;
                case "TABCHUNGTU":
                    frmDanhMucPhanQuyenChungTuUpdate frmDanhMucPhanQuyenChungTuUpdate = new frmDanhMucPhanQuyenChungTuUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền chứng từ",
                        IDDanhMucPhanQuyen = ((DataRowView)bsChungTu.Current).Row["IDDanhMucPhanQuyen"],
                        dataTable = dsData.Tables[DanhMucPhanQuyenChungTu.tableName],
                        dataRow = ((DataRowView)bsChungTu.Current).Row,
                    };
                    frmDanhMucPhanQuyenChungTuUpdate.ShowDialog();
                    frmDanhMucPhanQuyenChungTuUpdate.Dispose();
                    break;
                case "TABBAOCAO":
                    frmDanhMucPhanQuyenBaoCaoUpdate frmDanhMucPhanQuyenBaoCaoUpdate = new frmDanhMucPhanQuyenBaoCaoUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền báo cáo",
                        IDDanhMucPhanQuyen = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucPhanQuyen"],
                        dataTable = dsData.Tables[DanhMucPhanQuyenBaoCao.tableName],
                        dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
                    };
                    frmDanhMucPhanQuyenBaoCaoUpdate.ShowDialog();
                    frmDanhMucPhanQuyenBaoCaoUpdate.Dispose();
                    break;
            }
        }

        private void ugChiTiet_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucPhanQuyenDonViUpdate frmDanhMucPhanQuyenDonViUpdate = new frmDanhMucPhanQuyenDonViUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền đơn vị",
                IDDanhMucPhanQuyen = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucPhanQuyen"],
                dataTable = dsData.Tables[DanhMucPhanQuyenDonVi.tableName],
                dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
            };
            frmDanhMucPhanQuyenDonViUpdate.ShowDialog();
            frmDanhMucPhanQuyenDonViUpdate.Dispose();
        }

        private void ugBaoCao_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            bContinue = (bsBaoCao != null && bsBaoCao.Current != null && ugBaoCao != null && ugBaoCao.ActiveRow != null);
            if (!bContinue) return;
            frmDanhMucPhanQuyenBaoCaoUpdate frmDanhMucPhanQuyenBaoCaoUpdate = new frmDanhMucPhanQuyenBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền báo cáo",
                IDDanhMucPhanQuyen = ((DataRowView)bsBaoCao.Current).Row["IDDanhMucPhanQuyen"],
                dataTable = dsData.Tables[DanhMucPhanQuyenBaoCao.tableName],
                dataRow = ((DataRowView)bsBaoCao.Current).Row,
            };
            frmDanhMucPhanQuyenBaoCaoUpdate.ShowDialog();
            frmDanhMucPhanQuyenBaoCaoUpdate.Dispose();
        }

        private void ugChungTuQuyTrinh_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucPhanQuyenChungTuUpdate frmDanhMucPhanQuyenChungTuUpdate = new frmDanhMucPhanQuyenChungTuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền chứng từ",
                IDDanhMucPhanQuyen = ((DataRowView)bsChungTu.Current).Row["IDDanhMucPhanQuyen"],
                dataTable = dsData.Tables[DanhMucPhanQuyenChungTu.tableName],
                dataRow = ((DataRowView)bsChungTu.Current).Row,
            };
            frmDanhMucPhanQuyenChungTuUpdate.ShowDialog();
            frmDanhMucPhanQuyenChungTuUpdate.Dispose();
        }
        private void ugChungTuIn_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucPhanQuyenLoaiDoiTuongUpdate frmDanhMucPhanQuyenLoaiDoiTuongUpdate = new frmDanhMucPhanQuyenLoaiDoiTuongUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục phân quyền loại đối tượng",
                IDDanhMucPhanQuyen = ((DataRowView)bsLoaiDoiTuong.Current).Row["IDDanhMucPhanQuyen"],
                dataTable = dsData.Tables[DanhMucPhanQuyenLoaiDoiTuong.tableName],
                dataRow = ((DataRowView)bsLoaiDoiTuong.Current).Row,
            };
            frmDanhMucPhanQuyenLoaiDoiTuongUpdate.ShowDialog();
            frmDanhMucPhanQuyenLoaiDoiTuongUpdate.Dispose();
        }
    }
}

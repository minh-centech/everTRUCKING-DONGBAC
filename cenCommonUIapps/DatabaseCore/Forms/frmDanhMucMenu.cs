using cenBUS.DatabaseCore;
using cenCommon;
using cenDTO.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucMenu : cenBase.BaseForms.frmBaseDanhMucChiTiet
    {
        BindingSource bsChungTu = null, bsBaoCao = null;
        DanhMucMenuBUS BUS = new DanhMucMenuBUS();
        DanhMucMenuLoaiDoiTuongBUS BUSLoaiDoiTuong = new DanhMucMenuLoaiDoiTuongBUS();
        DanhMucMenuChungTuBUS BUSChungTu = new DanhMucMenuChungTuBUS();
        DanhMucMenuBaoCaoBUS BUSBaoCao = new DanhMucMenuBaoCaoBUS();
        public frmDanhMucMenu()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            BUS = new DanhMucMenuBUS();
            dsData = BUS.List(null);

            bsDanhMuc = new BindingSource
            {
                DataSource = dsData,
                DataMember = DanhMucMenu.tableName
            };

            bsDanhMucChiTiet = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucMenuLoaiDoiTuong.tableName
            };

            bsChungTu = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucMenuChungTu.tableName
            };

            bsBaoCao = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucMenuBaoCao.tableName
            };

            ug.DataSource = bsDanhMuc;
            ugChiTiet.DataSource = bsDanhMucChiTiet;
            ugChungTu.DataSource = bsChungTu;
            ugBaoCao.DataSource = bsBaoCao;

            tabChiTiet.Tabs["tabChiTiet"].Text = cenCommon.cenCommon.TraTuDien(DanhMucMenuLoaiDoiTuong.tableName);
            tabChiTiet.Tabs["tabChungTu"].Text = cenCommon.cenCommon.TraTuDien(DanhMucMenuChungTu.tableName);
            tabChiTiet.Tabs["tabBaoCao"].Text = cenCommon.cenCommon.TraTuDien(DanhMucMenuBaoCao.tableName);
        }

        protected override void InsertDanhMuc()
        {
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucMenuUpdate frmUpdate = new frmDanhMucMenuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dsData.Tables[DanhMucMenu.tableName],
                Text = "Thêm mới danh mục menu",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucMenuUpdate frmUpdate = new frmDanhMucMenuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                Text = "Sao chép danh mục menu",
                dataTable = dsData.Tables[DanhMucMenu.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucMenuUpdate frmUpdate = new frmDanhMucMenuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục menu",
                dataTable = dsData.Tables[DanhMucMenu.tableName],
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
            DanhMucMenu obj = new DanhMucMenu()
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

        protected override void InsertDanhMucChiTiet()
        {
            base.InsertDanhMucChiTiet();
            if (!bContinue) return;
            switch (tabChiTiet.SelectedTab.Key.ToUpper())
            {
                case "TABCHITIET":
                    frmDanhMucMenuLoaiDoiTuongUpdate frmDanhMucMenuLoaiDoiTuongUpdate = new frmDanhMucMenuLoaiDoiTuongUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Them,
                        Text = "Thêm mới danh mục menu loại đối tượng",
                        dataTable = dsData.Tables[DanhMucMenuLoaiDoiTuong.tableName],
                        IDDanhMucMenu = ((DataRowView)bsDanhMuc.Current).Row["ID"],
                    };
                    frmDanhMucMenuLoaiDoiTuongUpdate.ShowDialog();
                    frmDanhMucMenuLoaiDoiTuongUpdate.Dispose();
                    break;
                case "TABCHUNGTU":
                    frmDanhMucMenuChungTuUpdate frmDanhMucMenuChungTuUpdate = new frmDanhMucMenuChungTuUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Them,
                        Text = "Thêm mới danh mục menu loại chứng từ",
                        dataTable = dsData.Tables[DanhMucMenuChungTu.tableName],
                        IDDanhMucMenu = ((DataRowView)bsDanhMuc.Current).Row["ID"],
                    };
                    frmDanhMucMenuChungTuUpdate.ShowDialog();
                    frmDanhMucMenuChungTuUpdate.Dispose();
                    break;
                case "TABBAOCAO":
                    frmDanhMucMenuBaoCaoUpdate frmDanhMucMenuBaoCaoUpdate = new frmDanhMucMenuBaoCaoUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Them,
                        Text = "Thêm mới danh mục menu báo cáo",
                        dataTable = dsData.Tables[DanhMucMenuBaoCao.tableName],
                        IDDanhMucMenu = ((DataRowView)bsDanhMuc.Current).Row["ID"],
                    };
                    frmDanhMucMenuBaoCaoUpdate.ShowDialog();
                    frmDanhMucMenuBaoCaoUpdate.Dispose();
                    break;
            }
        }


        protected override void UpdateDanhMucChiTiet()
        {
            //base.UpdateDanhMucChiTiet();
            //if (!bContinue) return;
            switch (tabChiTiet.SelectedTab.Key.ToUpper())
            {
                case "TABCHITIET":
                    frmDanhMucMenuLoaiDoiTuongUpdate frmDanhMucMenuLoaiDoiTuongUpdate = new frmDanhMucMenuLoaiDoiTuongUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        Text = "Chỉnh sửa danh mục menu loại đối tượng",
                        IDDanhMucMenu = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucMenu"],
                        dataTable = dsData.Tables[DanhMucMenuLoaiDoiTuong.tableName],
                        dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
                    };
                    frmDanhMucMenuLoaiDoiTuongUpdate.ShowDialog();
                    frmDanhMucMenuLoaiDoiTuongUpdate.Dispose();
                    break;
                case "TABCHUNGTU":
                    frmDanhMucMenuChungTuUpdate frmDanhMucMenuChungTuUpdate = new frmDanhMucMenuChungTuUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        Text = "Chỉnh sửa danh mục menu chứng từ",
                        IDDanhMucMenu = ((DataRowView)bsChungTu.Current).Row["IDDanhMucMenu"],
                        dataTable = dsData.Tables[DanhMucMenuChungTu.tableName],
                        dataRow = ((DataRowView)bsChungTu.Current).Row,
                    };
                    frmDanhMucMenuChungTuUpdate.ShowDialog();
                    frmDanhMucMenuChungTuUpdate.Dispose();
                    break;
                case "TABBAOCAO":
                    frmDanhMucMenuBaoCaoUpdate frmDanhMucMenuBaoCaoUpdate = new frmDanhMucMenuBaoCaoUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        Text = "Chỉnh sửa danh mục menu loại báo cáo",
                        IDDanhMucMenu = ((DataRowView)bsBaoCao.Current).Row["IDDanhMucMenu"],
                        dataTable = dsData.Tables[DanhMucMenuBaoCao.tableName],
                        dataRow = ((DataRowView)bsBaoCao.Current).Row,
                    };
                    frmDanhMucMenuBaoCaoUpdate.ShowDialog();
                    frmDanhMucMenuBaoCaoUpdate.Dispose();
                    break;
            }
        }

        protected override void DeleteDanhMucChiTiet()
        {
            DataRow dataRow;
            Object obj;
            int i;
            switch (tabChiTiet.SelectedTab.Key.ToUpper())
            {
                case "TABCHITIET":
                    if (cenCommon.cenCommon.IsNull(bsDanhMucChiTiet.Current) || ugChiTiet.ActiveRow == null || cenCommon.cenCommon.QuestionMessage("Bạn có chắc chắn muốn xóa mục dữ liệu này?", 0) == DialogResult.No) return;
                    dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row;
                    obj = new DanhMucMenuLoaiDoiTuong()
                    {
                        ID = dataRow["ID"]
                    };
                    i = ugChiTiet.ActiveRow.Index;
                    if (BUSLoaiDoiTuong.Delete((DanhMucMenuLoaiDoiTuong)obj))
                    {
                        bsDanhMucChiTiet.RemoveCurrent();
                        if (i > 0) i -= 1;
                        if (i <= ugChiTiet.Rows.Count - 1)
                        {
                            ugChiTiet.Focus();
                            ugChiTiet.Rows[i].Activate();
                        }
                    }
                    break;
                case "TABCHUNGTU":
                    if (cenCommon.cenCommon.IsNull(bsChungTu.Current) || ugChungTu.ActiveRow == null || cenCommon.cenCommon.QuestionMessage("Bạn có chắc chắn muốn xóa mục dữ liệu này?", 0) == DialogResult.No) return;
                    dataRow = ((DataRowView)bsChungTu.Current).Row;
                    obj = new DanhMucMenuChungTu()
                    {
                        ID = dataRow["ID"]
                    };
                    i = ugChungTu.ActiveRow.Index;
                    if (BUSChungTu.Delete((DanhMucMenuChungTu)obj))
                    {
                        bsChungTu.RemoveCurrent();
                        if (i > 0) i -= 1;
                        if (i <= ugChungTu.Rows.Count - 1)
                        {
                            ugChungTu.Focus();
                            ugChungTu.Rows[i].Activate();
                        }
                    }
                    break;
                case "TABBAOCAO":
                    if (cenCommon.cenCommon.IsNull(bsBaoCao.Current) || ugBaoCao.ActiveRow == null || cenCommon.cenCommon.QuestionMessage("Bạn có chắc chắn muốn xóa mục dữ liệu này?", 0) == DialogResult.No) return;
                    dataRow = ((DataRowView)bsBaoCao.Current).Row;
                    obj = new DanhMucMenuBaoCao()
                    {
                        ID = dataRow["ID"]
                    };
                    i = ugBaoCao.ActiveRow.Index;
                    if (BUSBaoCao.Delete((DanhMucMenuBaoCao)obj))
                    {
                        bsBaoCao.RemoveCurrent();
                        if (i > 0) i -= 1;
                        if (i <= ugBaoCao.Rows.Count - 1)
                        {
                            ugBaoCao.Focus();
                            ugBaoCao.Rows[i].Activate();
                        }
                    }
                    break;
            }
        }

        private void ugBaoCao_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucMenuBaoCaoUpdate frmDanhMucMenuBaoCaoUpdate = new frmDanhMucMenuBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục menu báo cáo",
                IDDanhMucMenu = ((DataRowView)bsBaoCao.Current).Row["IDDanhMucMenu"],
                dataTable = dsData.Tables[DanhMucMenuBaoCao.tableName],
                dataRow = ((DataRowView)bsBaoCao.Current).Row,
            };
            frmDanhMucMenuBaoCaoUpdate.ShowDialog();
            frmDanhMucMenuBaoCaoUpdate.Dispose();
        }

        private void ugChungTu_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucMenuChungTuUpdate frmDanhMucMenuChungTuUpdate = new frmDanhMucMenuChungTuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục menu chứng tù",
                IDDanhMucMenu = ((DataRowView)bsChungTu.Current).Row["IDDanhMucMenu"],
                dataTable = dsData.Tables[DanhMucMenuChungTu.tableName],
                dataRow = ((DataRowView)bsChungTu.Current).Row,
            };
            frmDanhMucMenuChungTuUpdate.ShowDialog();
            frmDanhMucMenuChungTuUpdate.Dispose();
        }
    }
}

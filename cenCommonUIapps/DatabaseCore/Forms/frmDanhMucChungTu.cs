using cenBUS.DatabaseCore;
using cenCommon;
using cenDTO.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucChungTu : cenBase.BaseForms.frmBaseDanhMucChiTiet
    {
        BindingSource bsChungTuIn = null, bsChungTuQuyTrinh = null;
        DanhMucChungTuBUS BUS = new DanhMucChungTuBUS();
        DanhMucChungTuTrangThaiBUS BUSTrangThai = new DanhMucChungTuTrangThaiBUS();
        DanhMucChungTuInBUS BUSIn = new DanhMucChungTuInBUS();
        DanhMucChungTuQuyTrinhBUS BUSQuyTrinh = new DanhMucChungTuQuyTrinhBUS();
        public frmDanhMucChungTu()
        {
            InitializeComponent();
        }
        protected override void List()
        {

            dsData = BUS.List(null);

            bsDanhMuc = new BindingSource
            {
                DataSource = dsData,
                DataMember = DanhMucChungTu.tableName
            };

            bsDanhMucChiTiet = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucChungTuTrangThai.tableName
            };

            bsChungTuIn = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucChungTuIn.tableName
            };

            bsChungTuQuyTrinh = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucChungTuQuyTrinh.tableName
            };

            ug.DataSource = bsDanhMuc;
            ugChiTiet.DataSource = bsDanhMucChiTiet;
            ugChungTuIn.DataSource = bsChungTuIn;
            ugChungTuQuyTrinh.DataSource = bsChungTuQuyTrinh;

            tabChiTiet.Tabs["tabChiTiet"].Text = cenCommon.cenCommon.TraTuDien(DanhMucChungTuTrangThai.tableName);
            tabChiTiet.Tabs["tabChungTuIn"].Text = cenCommon.cenCommon.TraTuDien(DanhMucChungTuIn.tableName);
            tabChiTiet.Tabs["tabChungTuQuyTrinh"].Text = cenCommon.cenCommon.TraTuDien(DanhMucChungTuQuyTrinh.tableName);

            //ug.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            //ugChiTiet.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            //ugChungTuIn.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            //ugChungTuQuyTrinh.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        protected override void InsertDanhMuc()
        {
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucChungTuUpdate frmUpdate = new frmDanhMucChungTuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                Text = "Thêm mới danh mục chứng từ",
                dataTable = dsData.Tables[DanhMucChungTu.tableName],
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucChungTuUpdate frmUpdate = new frmDanhMucChungTuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                Text = "Sao chép danh mục chứng từ",
                dataTable = dsData.Tables[DanhMucChungTu.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucChungTuUpdate frmUpdate = new frmDanhMucChungTuUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục chứng từ",
                dataTable = dsData.Tables[DanhMucChungTu.tableName],
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
            DanhMucChungTu obj = new DanhMucChungTu()
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
                    frmDanhMucChungTuTrangThaiUpdate frmDanhMucChungTuTrangThaiUpdate = new frmDanhMucChungTuTrangThaiUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Them,
                        Text = "Thêm mới danh mục chứng từ trạng thái",
                        dataTable = dsData.Tables[DanhMucChungTuTrangThai.tableName],
                        IDDanhMucChungTu = ((DataRowView)bsDanhMuc.Current).Row["ID"],
                    };
                    frmDanhMucChungTuTrangThaiUpdate.ShowDialog();
                    frmDanhMucChungTuTrangThaiUpdate.Dispose();
                    break;
                case "TABCHUNGTUIN":
                    frmDanhMucChungTuInUpdate frmDanhMucChungTuInUpdate = new frmDanhMucChungTuInUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Them,
                        Text = "Thêm mới danh mục chứng từ in",
                        dataTable = dsData.Tables[DanhMucChungTuIn.tableName],
                        IDDanhMucChungTu = ((DataRowView)bsDanhMuc.Current).Row["ID"],
                    };
                    frmDanhMucChungTuInUpdate.ShowDialog();
                    frmDanhMucChungTuInUpdate.Dispose();
                    break;
                case "TABCHUNGTUQUYTRINH":
                    frmDanhMucChungTuQuyTrinhUpdate frmDanhMucChungTuQuyTrinhUpdate = new frmDanhMucChungTuQuyTrinhUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Them,
                        Text = "Thêm mới danh mục chứng từ quy trình",
                        dataTable = dsData.Tables[DanhMucChungTuQuyTrinh.tableName],
                        IDDanhMucChungTu = ((DataRowView)bsDanhMuc.Current).Row["ID"],
                    };
                    frmDanhMucChungTuQuyTrinhUpdate.ShowDialog();
                    frmDanhMucChungTuQuyTrinhUpdate.Dispose();
                    break;
            }
        }
        protected override void UpdateDanhMucChiTiet()
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;

            switch (tabChiTiet.SelectedTab.Key.ToUpper())
            {
                case "TABCHITIET":
                    frmDanhMucChungTuTrangThaiUpdate frmDanhMucChungTuTrangThaiUpdate = new frmDanhMucChungTuTrangThaiUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        Text = "Chỉnh sửa danh mục chứng từ trạng thái",
                        IDDanhMucChungTu = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucChungTu"],
                        dataTable = dsData.Tables[DanhMucChungTuTrangThai.tableName],
                        dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
                    };
                    frmDanhMucChungTuTrangThaiUpdate.ShowDialog();
                    frmDanhMucChungTuTrangThaiUpdate.Dispose();
                    break;
                case "TABCHUNGTUIN":
                    frmDanhMucChungTuInUpdate frmDanhMucChungTuInUpdate = new frmDanhMucChungTuInUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        Text = "Chỉnh sửa danh mục chứng từ in",
                        IDDanhMucChungTu = ((DataRowView)bsChungTuIn.Current).Row["IDDanhMucChungTu"],
                        dataTable = dsData.Tables[DanhMucChungTuIn.tableName],
                        dataRow = ((DataRowView)bsChungTuIn.Current).Row
                    };
                    frmDanhMucChungTuInUpdate.ShowDialog();
                    frmDanhMucChungTuInUpdate.Dispose();
                    break;
                case "TABCHUNGTUQUYTRINH":
                    frmDanhMucChungTuQuyTrinhUpdate frmDanhMucChungTuQuyTrinhUpdate = new frmDanhMucChungTuQuyTrinhUpdate
                    {
                        CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                        Text = "Chỉnh sửa danh mục chứng từ quy trình",
                        IDDanhMucChungTu = ((DataRowView)bsChungTuQuyTrinh.Current).Row["IDDanhMucChungTu"],
                        dataTable = dsData.Tables[DanhMucChungTuQuyTrinh.tableName],
                        dataRow = ((DataRowView)bsChungTuQuyTrinh.Current).Row,
                    };
                    frmDanhMucChungTuQuyTrinhUpdate.ShowDialog();
                    frmDanhMucChungTuQuyTrinhUpdate.Dispose();
                    break;
            }
        }

        private void ugChiTiet_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucChungTuTrangThaiUpdate frmDanhMucChungTuTrangThaiUpdate = new frmDanhMucChungTuTrangThaiUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục chứng từ trạng thái",
                IDDanhMucChungTu = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucChungTu"],
                dataTable = dsData.Tables[DanhMucChungTuTrangThai.tableName],
                dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
            };
            frmDanhMucChungTuTrangThaiUpdate.ShowDialog();
            frmDanhMucChungTuTrangThaiUpdate.Dispose();
        }
        private void ugChungTuQuyTrinh_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucChungTuQuyTrinhUpdate frmDanhMucChungTuQuyTrinhUpdate = new frmDanhMucChungTuQuyTrinhUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục chứng từ quy trình",
                IDDanhMucChungTu = ((DataRowView)bsChungTuQuyTrinh.Current).Row["IDDanhMucChungTu"],
                dataTable = dsData.Tables[DanhMucChungTuQuyTrinh.tableName],
                dataRow = ((DataRowView)bsChungTuQuyTrinh.Current).Row
            };
            frmDanhMucChungTuQuyTrinhUpdate.ShowDialog();
            frmDanhMucChungTuQuyTrinhUpdate.Dispose();
        }
        private void ugChungTuIn_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucChungTuInUpdate frmDanhMucChungTuInUpdate = new frmDanhMucChungTuInUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục chứng từ in",
                IDDanhMucChungTu = ((DataRowView)bsChungTuIn.Current).Row["IDDanhMucChungTu"],
                dataTable = dsData.Tables[DanhMucChungTuIn.tableName],
                dataRow = ((DataRowView)bsChungTuIn.Current).Row,
            };
            frmDanhMucChungTuInUpdate.ShowDialog();
            frmDanhMucChungTuInUpdate.Dispose();
        }

        protected override void DeleteDanhMucChiTiet()
        {
            base.DeleteDanhMucChiTiet();
            if (!bContinue) return;
            DataRow dataRow;
            Object obj;
            int i;
            switch (tabChiTiet.SelectedTab.Key.ToUpper())
            {
                case "TABCHITIET":
                    dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row;
                    obj = new DanhMucChungTuTrangThai()
                    {
                        ID = dataRow["ID"]
                    };
                    i = ugChiTiet.ActiveRow.Index;
                    if (BUSTrangThai.Delete((DanhMucChungTuTrangThai)obj))
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
                case "TABCHUNGTUIN":
                    dataRow = ((DataRowView)bsChungTuIn.Current).Row;
                    obj = new DanhMucChungTuIn()
                    {
                        ID = dataRow["ID"]
                    };
                    i = ugChungTuIn.ActiveRow.Index;
                    if (BUSIn.Delete((DanhMucChungTuIn)obj))
                    {
                        bsChungTuIn.RemoveCurrent();
                        if (i > 0) i -= 1;
                        if (i <= ugChungTuIn.Rows.Count - 1)
                        {
                            ugChungTuIn.Focus();
                            ugChungTuIn.Rows[i].Activate();
                        }
                    }
                    break;
                case "TABCHUNGTUQUYTRINH":
                    dataRow = ((DataRowView)bsChungTuQuyTrinh.Current).Row;
                    obj = new DanhMucChungTuQuyTrinh()
                    {
                        ID = dataRow["ID"]
                    };
                    i = ugChungTuQuyTrinh.ActiveRow.Index;
                    if (BUSQuyTrinh.Delete((DanhMucChungTuQuyTrinh)obj))
                    {
                        bsChungTuQuyTrinh.RemoveCurrent();
                        if (i > 0) i -= 1;
                        if (i <= ugChungTuQuyTrinh.Rows.Count - 1)
                        {
                            ugChungTuQuyTrinh.Focus();
                            ugChungTuQuyTrinh.Rows[i].Activate();
                        }
                    }
                    break;
            }
        }
    }
}

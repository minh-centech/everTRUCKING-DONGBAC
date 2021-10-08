using cenBUS.DatabaseCore;
using cenCommon;
using cenDTO.DatabaseCore;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucBaoCao : cenBase.BaseForms.frmBaseDanhMucChiTiet
    {
        DanhMucBaoCaoBUS BUS = new DanhMucBaoCaoBUS();
        DanhMucBaoCaoCotBUS BUSChiTiet = new DanhMucBaoCaoCotBUS();
        public frmDanhMucBaoCao()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            BUS = new DanhMucBaoCaoBUS();
            dsData = BUS.List(null);

            bsDanhMuc = new BindingSource
            {
                DataSource = dsData,
                DataMember = DanhMucBaoCao.tableName
            };

            bsDanhMucChiTiet = new BindingSource
            {
                DataSource = bsDanhMuc,
                DataMember = GlobalVariables.prefix_DataRelation + DanhMucBaoCaoCot.tableName
            };
            ug.DataSource = bsDanhMuc;
            ugChiTiet.DataSource = bsDanhMucChiTiet;
            tabChiTiet.Tabs["tabChiTiet"].Text = cenCommon.cenCommon.TraTuDien(DanhMucBaoCaoCot.tableName);
        }

        protected override void InsertDanhMuc()
        {
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucBaoCaoUpdate frmUpdate = new frmDanhMucBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dsData.Tables[DanhMucBaoCao.tableName],
                Text = "Thêm mới danh mục báo cáo",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();

        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucBaoCaoUpdate frmUpdate = new frmDanhMucBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                Text = "Thêm mới danh mục báo cáo",
                dataTable = dsData.Tables[DanhMucBaoCao.tableName],
                dataRow = ((DataRowView)bsDanhMuc.Current).Row
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucBaoCaoUpdate frmUpdate = new frmDanhMucBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục báo cáo",
                dataTable = dsData.Tables[DanhMucBaoCao.tableName],
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
            DanhMucBaoCao obj = new DanhMucBaoCao()
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
            frmDanhMucBaoCaoCotUpdate frmUpdate = new frmDanhMucBaoCaoCotUpdate
            {
                CapNhat = 1,
                Text = "Thêm mới danh mục cột báo cáo",
                dataTable = dsData.Tables[DanhMucBaoCaoCot.tableName],
                IDDanhMucBaoCao = ((DataRowView)bsDanhMuc.Current).Row["ID"],
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMucChiTiet()
        {
            base.UpdateDanhMucChiTiet();
            if (!bContinue) return;
            frmDanhMucBaoCaoCotUpdate frmUpdate = new frmDanhMucBaoCaoCotUpdate
            {
                CapNhat = 2,
                Text = "Chỉnh sửa danh mục cột báo cáo",
                IDDanhMucBaoCao = ((DataRowView)bsDanhMucChiTiet.Current).Row["IDDanhMucBaoCao"],
                dataTable = dsData.Tables[DanhMucBaoCaoCot.tableName],
                dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row,
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMucChiTiet()
        {
            base.DeleteDanhMucChiTiet();
            if (!bContinue) return;
            DataRow dataRow = ((DataRowView)bsDanhMucChiTiet.Current).Row;
            DanhMucBaoCaoCot obj = new DanhMucBaoCaoCot()
            {
                ID = dataRow["ID"]
            };
            int i = ugChiTiet.ActiveRow.Index;
            if (BUSChiTiet.Delete(obj))
            {
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

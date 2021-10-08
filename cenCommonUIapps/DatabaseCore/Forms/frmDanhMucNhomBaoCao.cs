using cenBUS.DatabaseCore;
using cenDTO.DatabaseCore;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucNhomBaoCao : cenBase.BaseForms.frmBaseDanhMuc
    {
        DanhMucNhomBaoCaoBUS BUS = new DanhMucNhomBaoCaoBUS();
        public frmDanhMucNhomBaoCao()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            dtData = BUS.List(null);
            tableName = DanhMucNhomBaoCao.tableName;
            dtData.TableName = tableName;

            bsDanhMuc = new BindingSource
            {
                DataSource = dtData
            };
            ug.DataSource = bsDanhMuc;
        }
        protected override void InsertDanhMuc()
        {
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucNhomBaoCaoUpdate frmUpdate = new frmDanhMucNhomBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dtData,
                TenDanhMucLoaiDoiTuong = "Danh mục nhóm báo cáo",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucNhomBaoCaoUpdate frmUpdate = new frmDanhMucNhomBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                TenDanhMucLoaiDoiTuong = "Danh mục nhóm báo cáo",
                dataTable = dtData,
                dataRow = ((DataRowView)bsDanhMuc.Current).Row
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucNhomBaoCaoUpdate frmUpdate = new frmDanhMucNhomBaoCaoUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Danh mục nhóm báo cáo",
                dataTable = dtData,
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
            DanhMucNhomBaoCao obj = new DanhMucNhomBaoCao()
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
    }
}

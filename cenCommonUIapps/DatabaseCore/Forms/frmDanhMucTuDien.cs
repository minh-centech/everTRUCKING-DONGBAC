using cenBUS.DatabaseCore;
using cenDTO.DatabaseCore;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucTuDien : cenBase.BaseForms.frmBaseDanhMuc
    {
        DanhMucTuDienBUS BUS = new DanhMucTuDienBUS();
        public frmDanhMucTuDien()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            dtData = BUS.List(null);
            tableName = DanhMucTuDien.tableName;
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
            frmDanhMucTuDienUpdate frmUpdate = new frmDanhMucTuDienUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dtData,
                Text = "Thêm mới danh mục từ điển",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucTuDienUpdate frmUpdate = new frmDanhMucTuDienUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                Text = "Sao chép danh mục từ điển",
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
            frmDanhMucTuDienUpdate frmUpdate = new frmDanhMucTuDienUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                Text = "Chỉnh sửa danh mục từ điển",
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
            DanhMucTuDien obj = new DanhMucTuDien()
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

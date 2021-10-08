using cenBUS.DatabaseCore;
using cenDTO.DatabaseCore;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucLoaiDoiTuong : cenBase.BaseForms.frmBaseDanhMuc
    {
        DanhMucLoaiDoiTuongBUS BUS = new DanhMucLoaiDoiTuongBUS();
        public frmDanhMucLoaiDoiTuong()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            dtData = BUS.List(null);
            tableName = DanhMucLoaiDoiTuong.tableName;
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
            frmDanhMucLoaiDoiTuongUpdate frmUpdate = new frmDanhMucLoaiDoiTuongUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dtData,
                Text = "Thêm mới danh mục loại đối tượng",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucLoaiDoiTuongUpdate frmUpdate = new frmDanhMucLoaiDoiTuongUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                dataTable = dtData,
                dataRow = ((DataRowView)bsDanhMuc.Current).Row,
                Text = "Sao chép danh mục loại đối tượng",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void UpdateDanhMuc()
        {
            base.UpdateDanhMuc();
            if (!bContinue) return;
            frmDanhMucLoaiDoiTuongUpdate frmUpdate = new frmDanhMucLoaiDoiTuongUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                dataTable = dtData,
                dataRow = ((DataRowView)bsDanhMuc.Current).Row,
                Text = "Sửa đổi danh mục loại đối tượng",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void DeleteDanhMuc()
        {
            base.DeleteDanhMuc();
            if (!bContinue) return;
            DataRow dataRow = ((DataRowView)bsDanhMuc.Current).Row;
            DanhMucLoaiDoiTuong obj = new DanhMucLoaiDoiTuong()
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

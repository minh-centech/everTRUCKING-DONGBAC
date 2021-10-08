using cenBUS.DatabaseCore;
using cenDTO.DatabaseCore;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucNguoiSuDung : cenBase.BaseForms.frmBaseDanhMuc
    {
        DanhMucNguoiSuDungBUS BUS = new DanhMucNguoiSuDungBUS();
        public frmDanhMucNguoiSuDung()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            dtData = BUS.List(null);
            tableName = DanhMucNguoiSuDung.tableName;
            dtData.TableName = tableName;

            bsDanhMuc = new BindingSource
            {
                DataSource = dtData
            };
            ug.DataSource = bsDanhMuc;
            ug.DisplayLayout.Bands[0].Columns["Password"].Hidden = true;
        }
        protected override void InsertDanhMuc()
        {
            base.InsertDanhMuc();
            if (!bContinue) return;
            frmDanhMucNguoiSuDungUpdate frmUpdate = new frmDanhMucNguoiSuDungUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Them,
                dataTable = dtData,
                TenDanhMucLoaiDoiTuong = "Thêm mới Danh mục người sử dụng",
            };
            frmUpdate.ShowDialog();
            frmUpdate.Dispose();
        }
        protected override void CopyDanhMuc()
        {
            base.CopyDanhMuc();
            if (!bContinue) return;
            frmDanhMucNguoiSuDungUpdate frmUpdate = new frmDanhMucNguoiSuDungUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Copy,
                TenDanhMucLoaiDoiTuong = "Sao chép Danh mục người sử dụng",
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
            frmDanhMucNguoiSuDungUpdate frmUpdate = new frmDanhMucNguoiSuDungUpdate
            {
                CapNhat = cenCommon.ThaoTacDuLieu.Sua,
                TenDanhMucLoaiDoiTuong = "Chỉnh sửa Danh mục người sử dụng",
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
            DanhMucNguoiSuDung obj = new DanhMucNguoiSuDung()
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

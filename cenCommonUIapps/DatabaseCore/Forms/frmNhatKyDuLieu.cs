using cenBUS.DatabaseCore;
using cenDTO.DatabaseCore;
using Infragistics.Win.UltraWinGrid;
using System.Windows.Forms;
namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmNhatKyDuLieu : cenBase.BaseForms.frmBaseDanhMuc
    {
        public frmNhatKyDuLieu()
        {
            InitializeComponent();
            UltraToolbarsManager1.Tools["BTTHEM"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["BTSUA"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["BTXOA"].SharedProps.Visible = false;
            UltraToolbarsManager1.Tools["BTCOPY"].SharedProps.Visible = false;
        }
        protected override void List()
        {

            NhatKyDuLieuBUS _BUS = new NhatKyDuLieuBUS();
            dtData = _BUS.List();
            tableName = NhatKyDuLieu.tableName;
            dtData.TableName = tableName;
            bsDanhMuc = new BindingSource
            {
                DataSource = dtData
            };
            ug.HiddenColumnsList = "[AutoID][AutoIDChungTu][AutoIDChungTuChiTiet]";
            ug.FixedColumnsList = "[Ngay][Gio][MaDanhMucNguoiSuDung][TenDanhMucNguoiSuDung]";
            ug.DataSource = bsDanhMuc;
            ug.DisplayLayout.Bands[0].PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand);
        }
        protected override void InsertDanhMuc()
        {
        }
        protected override void CopyDanhMuc()
        {
        }
        protected override void UpdateDanhMuc()
        {
        }
        protected override void DeleteDanhMuc()
        {
        }
    }
}

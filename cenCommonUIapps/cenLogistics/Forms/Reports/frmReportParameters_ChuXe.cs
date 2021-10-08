using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmReportParameters_ChuXe : Form
    {
        public object dTuNgay = null, dDenNgay = null, IDDanhMucChuXe = null;
        public string ChuoiThamSoHienThiGrid = "";
        //Thông tin trả về
        public Boolean OK = false;//Ok hay Cancel

        public frmReportParameters_ChuXe()
        {
            InitializeComponent();
            txtTuNgay.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtDenNgay.DateTime = txtTuNgay.DateTime.AddMonths(1).AddDays(-1);
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {

            OK = true;
            if (cenCommon.cenCommon.IsNull(txtTuNgay.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu ngày bắt đầu!"); txtTuNgay.Focus(); return; }
            if (cenCommon.cenCommon.IsNull(txtDenNgay.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu ngày kết thúc!"); txtDenNgay.Focus(); return; }
            dTuNgay = txtTuNgay.Value;
            dDenNgay = txtDenNgay.Value;
            IDDanhMucChuXe = txtMaDanhMucThauPhu.ID;
            ChuoiThamSoHienThiGrid = "Từ ngày: " + txtTuNgay.DateTime.Day.ToString("0#") + "/" + txtTuNgay.DateTime.Month.ToString("0#") + "/" + txtTuNgay.DateTime.Year.ToString("000#") + " - Đến ngày: " + txtDenNgay.DateTime.Day.ToString("0#") + "/" + txtDenNgay.DateTime.Month.ToString("0#") + "/" + txtDenNgay.DateTime.Year.ToString("000#") + ((!cenCommon.cenCommon.IsNull(IDDanhMucChuXe)) ? " - Chủ xe: " + txtTenDanhMucThauPhu.Text : "");
            Close();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        /// <summary>
        /// Ngừng
        /// </summary>
        private void Cancel()
        {
            OK = false;
            Close();
        }

        private void frmReportParameters_ChiTietBaoGia_Load(object sender, EventArgs e)
        {
            DanhMucThauPhuBUS DanhMucThauPhuBUS = new DanhMucThauPhuBUS();
            DataTable dtThauPhu = DanhMucThauPhuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu)), null, null);
            txtMaDanhMucThauPhu.IsValid = true;
            txtMaDanhMucThauPhu.dtValid = dtThauPhu;
            txtMaDanhMucThauPhu.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongThauPhu));
            saTextBox[] txtMaDanhMucThauPhuExt = new saTextBox[1];
            txtMaDanhMucThauPhuExt[0] = txtTenDanhMucThauPhu;
            txtMaDanhMucThauPhu.txtMoRong = txtMaDanhMucThauPhuExt;
            txtMaDanhMucThauPhu.ValidColumnName = "Ma";
            txtMaDanhMucThauPhu.ReturnedColumnsList = "Ten";
            txtMaDanhMucThauPhu.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //
        }
    }
}

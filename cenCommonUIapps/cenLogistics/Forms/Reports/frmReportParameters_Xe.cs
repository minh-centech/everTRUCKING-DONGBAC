using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmReportParameters_Xe : Form
    {
        public object dTuNgay = null, dDenNgay = null, IDDanhMucXe = null;
        public string ChuoiThamSoHienThiGrid = "";
        //Thông tin trả về
        public Boolean OK = false;//Ok hay Cancel

        public frmReportParameters_Xe()
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
            IDDanhMucXe = cboIDDanhMucXe.Value;
            ChuoiThamSoHienThiGrid = "Từ ngày: " + txtTuNgay.DateTime.Day.ToString("0#") + "/" + txtTuNgay.DateTime.Month.ToString("0#") + "/" + txtTuNgay.DateTime.Year.ToString("000#") + " - Đến ngày: " + txtDenNgay.DateTime.Day.ToString("0#") + "/" + txtDenNgay.DateTime.Month.ToString("0#") + "/" + txtDenNgay.DateTime.Year.ToString("000#") + ((!cenCommon.cenCommon.IsNull(IDDanhMucXe)) ? " - Xe: " + cboIDDanhMucXe.Text : "");
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
            DanhMucXeBUS DanhMucXeBUS = new DanhMucXeBUS();
            DataTable dtXe = DanhMucXeBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongXe)), null, null, null);
            cboIDDanhMucXe.DataSource = dtXe;
            cboIDDanhMucXe.ValueMember = "ID";
            cboIDDanhMucXe.DisplayMember = "BienSo";
        }
    }
}

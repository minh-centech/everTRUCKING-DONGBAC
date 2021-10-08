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
    public partial class frmReportParameters_KhachHang_Sale : Form
    {
        public object dTuNgay = null, dDenNgay = null, IDDanhMucKhachHang = null, IDDanhMucSale = null;
        public string ChuoiThamSoHienThiGrid = "";
        //Thông tin trả về
        public Boolean OK = false;//Ok hay Cancel

        public frmReportParameters_KhachHang_Sale()
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
            IDDanhMucKhachHang = txtMaDanhMucKhachHang.ID;
            IDDanhMucSale = txtMaDanhMucNhanSu.ID;
            ChuoiThamSoHienThiGrid = "Từ ngày: " + txtTuNgay.DateTime.Day.ToString("0#") + "/" + txtTuNgay.DateTime.Month.ToString("0#") + "/" + txtTuNgay.DateTime.Year.ToString("000#") + " - Đến ngày: " + txtDenNgay.DateTime.Day.ToString("0#") + "/" + txtDenNgay.DateTime.Month.ToString("0#") + "/" + txtDenNgay.DateTime.Year.ToString("000#") + ((!cenCommon.cenCommon.IsNull(IDDanhMucKhachHang)) ? " - Khách hàng: " + txtTenDanhMucKhachHang.Text : "") + ((!cenCommon.cenCommon.IsNull(IDDanhMucSale)) ? " - Sale: " + txtTenDanhMucNhanSu.Text : "");
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
            DanhMucKhachHangBUS DanhMucKhachHangBUS = new DanhMucKhachHangBUS();
            DataTable dtKhachHang = DanhMucKhachHangBUS.Valid(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang)), null);
            txtMaDanhMucKhachHang.IsValid = true;
            txtMaDanhMucKhachHang.dtValid = dtKhachHang;
            txtMaDanhMucKhachHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang));
            saTextBox[] txtMaDanhMucKhachHangExt = new saTextBox[1];
            txtMaDanhMucKhachHangExt[0] = txtTenDanhMucKhachHang;
            txtMaDanhMucKhachHang.txtMoRong = txtMaDanhMucKhachHangExt;
            txtMaDanhMucKhachHang.ValidColumnName = "Ma";
            txtMaDanhMucKhachHang.ReturnedColumnsList = "Ten";
            txtMaDanhMucKhachHang.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            DanhMucNhanSuBUS DanhMucNhanSuBUS = new DanhMucNhanSuBUS();
            DataTable dtNhanSu = DanhMucNhanSuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu)), null);
            txtMaDanhMucNhanSu.IsValid = true;
            txtMaDanhMucNhanSu.dtValid = dtNhanSu;
            txtMaDanhMucNhanSu.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu));
            saTextBox[] txtMaDanhMucNhanSuExt = new saTextBox[1];
            txtMaDanhMucNhanSuExt[0] = txtTenDanhMucNhanSu;
            txtMaDanhMucNhanSu.txtMoRong = txtMaDanhMucNhanSuExt;
            txtMaDanhMucNhanSu.ValidColumnName = "Ma";
            txtMaDanhMucNhanSu.ReturnedColumnsList = "Ten";
            txtMaDanhMucNhanSu.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //
            txtTuNgay.MaskInput = cenCommon.GlobalVariables.MaskInputDate;
            txtDenNgay.MaskInput = cenCommon.GlobalVariables.MaskInputDate;

        }
    }
}

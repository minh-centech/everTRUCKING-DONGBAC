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
    public partial class frmReportParameters_NhomHang_Sale : Form
    {
        public object dTuNgay = null, dDenNgay = null, IDDanhMucNhomHangVanChuyen = null, IDDanhMucSale = null;
        public string ChuoiThamSoHienThiGrid = "";
        //Thông tin trả về
        public Boolean OK = false;//Ok hay Cancel

        public frmReportParameters_NhomHang_Sale()
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
            IDDanhMucNhomHangVanChuyen = txtMaDanhMucNhomHang.ID;
            IDDanhMucSale = txtMaDanhMucNhanSu.ID;
            ChuoiThamSoHienThiGrid = "Từ ngày: " + txtTuNgay.DateTime.Day.ToString("0#") + "/" + txtTuNgay.DateTime.Month.ToString("0#") + "/" + txtTuNgay.DateTime.Year.ToString("000#") + " - Đến ngày: " + txtDenNgay.DateTime.Day.ToString("0#") + "/" + txtDenNgay.DateTime.Month.ToString("0#") + "/" + txtDenNgay.DateTime.Year.ToString("000#") + ((!cenCommon.cenCommon.IsNull(IDDanhMucNhomHangVanChuyen)) ? " - Nhóm hàng: " + txtTenDanhMucNhomHang.Text : "") + ((!cenCommon.cenCommon.IsNull(IDDanhMucSale)) ? " - Sale: " + txtTenDanhMucNhanSu.Text : "");
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
            DanhMucDoiTuongBUS DanhMucNhomHangVanChuyenBUS = new DanhMucDoiTuongBUS();
            DataTable dtNhomHangVanChuyen = DanhMucNhomHangVanChuyenBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen)), null);
            txtMaDanhMucNhomHang.IsValid = true;
            txtMaDanhMucNhomHang.dtValid = dtNhomHangVanChuyen;
            txtMaDanhMucNhomHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen));
            saTextBox[] txtMaDanhMucNhomHangVanChuyenExt = new saTextBox[1];
            txtMaDanhMucNhomHangVanChuyenExt[0] = txtTenDanhMucNhomHang;
            txtMaDanhMucNhomHang.txtMoRong = txtMaDanhMucNhomHangVanChuyenExt;
            txtMaDanhMucNhomHang.ValidColumnName = "Ma";
            txtMaDanhMucNhomHang.ReturnedColumnsList = "Ten";
            txtMaDanhMucNhomHang.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
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

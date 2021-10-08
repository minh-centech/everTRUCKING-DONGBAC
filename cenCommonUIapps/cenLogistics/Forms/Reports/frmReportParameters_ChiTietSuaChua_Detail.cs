using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.Misc;
using cenControls;
using cenCommon;
using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frmReportParameters_ChiTietSuaChua_Detail : Form
    {
        public object dTuNgay = null, dDenNgay = null, IDDanhMucNhanSu = null, IDDanhMucBoPhanContainer = null, IDDanhMucMaSuaChuaContainer = null;
        DataTable dtHangTau, dtBoPhanContainer, dtMaSuaChuaContainer;
        //Thông tin trả về
        public Boolean OK = false;//Ok hay Cancel

        public frmReportParameters_ChiTietSuaChua_Detail()
        {
            InitializeComponent();
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {

            OK = true;
            dTuNgay = txtTuNgay.Value;
            dDenNgay = txtDenNgay.Value;
            IDDanhMucNhanSu = txtMaDanhMucNhanSu.ID;
            IDDanhMucBoPhanContainer = txtMaDanhMucBoPhanContainer.ID;
            IDDanhMucMaSuaChuaContainer = txtMaDanhMucMaSuaChuaContainer.ID;
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

            //DanhMucNhanSu
            DanhMucNhanSuBUS DanhMucNhanSuBUS = new DanhMucNhanSuBUS();
            dtHangTau = DanhMucNhanSuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(null)), null);
            txtMaDanhMucNhanSu.IsValid = true;
            txtMaDanhMucNhanSu.dtValid = dtHangTau;
            txtMaDanhMucNhanSu.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(null));
            saTextBox[] txtMaDanhMucNhanSuExt = new saTextBox[1];
            txtMaDanhMucNhanSuExt[0] = txtTenDanhMucNhanSu;
            txtMaDanhMucNhanSu.txtMoRong = txtMaDanhMucNhanSuExt;
            txtMaDanhMucNhanSu.ValidColumnName = "Ma";
            txtMaDanhMucNhanSu.ReturnedColumnsList = "Ten";
            txtMaDanhMucNhanSu.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //DanhMucBoPhanContainer
            DanhMucDoiTuongBUS DanhMucBoPhanContainerBUS = new DanhMucDoiTuongBUS();
            dtBoPhanContainer = DanhMucBoPhanContainerBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(null)), null);
            txtMaDanhMucBoPhanContainer.IsValid = true;
            txtMaDanhMucBoPhanContainer.dtValid = dtBoPhanContainer;
            txtMaDanhMucBoPhanContainer.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(null));
            saTextBox[] txtMaDanhMucBoPhanContainerExt = new saTextBox[1];
            txtMaDanhMucBoPhanContainerExt[0] = txtTenDanhMucBoPhanContainer;
            txtMaDanhMucBoPhanContainer.txtMoRong = txtMaDanhMucBoPhanContainerExt;
            txtMaDanhMucBoPhanContainer.ValidColumnName = "Ma";
            txtMaDanhMucBoPhanContainer.ReturnedColumnsList = "Ten";
            txtMaDanhMucBoPhanContainer.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //DanhMucMaSuaChuaContainer
            DanhMucDoiTuongBUS DanhMucMaSuaChuaContainerBUS = new DanhMucDoiTuongBUS();
            dtMaSuaChuaContainer = DanhMucMaSuaChuaContainerBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(null)), null);
            txtMaDanhMucMaSuaChuaContainer.IsValid = true;
            txtMaDanhMucMaSuaChuaContainer.dtValid = dtMaSuaChuaContainer;
            txtMaDanhMucMaSuaChuaContainer.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(null));
            saTextBox[] txtMaDanhMucMaSuaChuaContainerExt = new saTextBox[1];
            txtMaDanhMucMaSuaChuaContainerExt[0] = txtTenDanhMucMaSuaChuaContainer;
            txtMaDanhMucMaSuaChuaContainer.txtMoRong = txtMaDanhMucMaSuaChuaContainerExt;
            txtMaDanhMucMaSuaChuaContainer.ValidColumnName = "Ma";
            txtMaDanhMucMaSuaChuaContainer.ReturnedColumnsList = "Ten";
            txtMaDanhMucMaSuaChuaContainer.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);

            txtTuNgay.MaskInput = cenCommon.GlobalVariables.MaskInputDate;
            txtDenNgay.MaskInput = cenCommon.GlobalVariables.MaskInputDate;

        }
    }
}

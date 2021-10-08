using cenBUS.DatabaseCore;
using cenCommonUIapps.cenLogistics.Forms;
using cenCommonUIapps.DatabaseCore.Forms;
using cenControls;
using cenDAO;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace cenCommonUIapps
{
    public class cenCommonUIapps
    {
        public static void runDanhMuc(string formCaption, object IDDanhMucLoaiDoiTuong, Form mdiParent)
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenLoaiDoiTuong(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucLoaiDoiTuong, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền truy cập danh mục này");
                return;
            }
            string TenBangDuLieu = DanhMucLoaiDoiTuongBUS.GetTenBangDuLieu(IDDanhMucLoaiDoiTuong);
            switch (TenBangDuLieu)
            {
                case DanhMucDinhMucChiPhi.tableName:
                    frmDanhMucDinhMucChiPhi frmDanhMucDinhMucChiPhi = new frmDanhMucDinhMucChiPhi()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucDinhMucChiPhi.Show();
                    break;
                case DanhMucHangHoa.tableName:
                    frmDanhMucHangHoa frmDanhMucHangHoa = new frmDanhMucHangHoa()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucHangHoa.Show();
                    break;
                case DanhMucGiaNhienLieu.tableName:
                    frmDanhMucGiaNhienLieu frmDanhMucGiaNhienLieu = new frmDanhMucGiaNhienLieu()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucGiaNhienLieu.Show();
                    break;
                case DanhMucTuyenVanTai.tableName:
                    frmDanhMucTuyenVanTai frmDanhMucTuyenVanTai = new frmDanhMucTuyenVanTai()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucTuyenVanTai.Show();
                    break;
                case DanhMucTaiXe.tableName:
                    frmDanhMucTaiXe frmDanhMucTaiXe = new frmDanhMucTaiXe()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucTaiXe.Show();
                    break;
                case DanhMucXe.tableName:
                    frmDanhMucXe frmDanhMucXe = new frmDanhMucXe()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucXe.Show();
                    break;
                case DanhMucNhanSu.tableName:
                    frmDanhMucNhanSu frmDanhMucNhanSu = new frmDanhMucNhanSu()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucNhanSu.Show();
                    break;
                case DanhMucKhachHang.tableName:
                    frmDanhMucKhachHang frmDanhMucKhachHang = new frmDanhMucKhachHang()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucKhachHang.Show();
                    break;
                case DanhMucThauPhu.tableName:
                    frmDanhMucThauPhu frmDanhMucThauPhu = new frmDanhMucThauPhu()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucThauPhu.Show();
                    break;
                case DanhMucDoiTuong.tableName:
                    frmDanhMucDoiTuong frmDanhMucDoiTuong = new frmDanhMucDoiTuong()
                    {
                        Text = formCaption,
                        IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                        TenDanhMucLoaiDoiTuong = formCaption,
                        MdiParent = mdiParent,
                    };
                    frmDanhMucDoiTuong.Show();
                    break;
            }

        }
        public static void runChungTu(string formCaption, object LoaiManHinh, object IDDanhMucChungTu, Form mdiParent)
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenChungTu(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucChungTu, out bool Xem, out bool Them, out bool Sua, out bool Xoa);
            if (!cenCommon.GlobalVariables.isAdmin && !Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền truy cập danh mục này");
                return;
            }
            switch (LoaiManHinh)
            {
                case cenCommon.LoaiManHinh.IDHopDongVanChuyen:
                    frm_ctHopDongVanChuyen frm_ctHopDongVanChuyen = new frm_ctHopDongVanChuyen()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = IDDanhMucChungTu,
                        MdiParent = mdiParent
                    };
                    frm_ctHopDongVanChuyen.txtTuNgay.Value = DanhMucThamSoNguoiSuDungBUS.GetGiaTri(cenCommon.ThamSoNguoiSuDung.ctHopDongVanChuyen_TuNgay);
                    frm_ctHopDongVanChuyen.txtDenNgay.Value = DanhMucThamSoNguoiSuDungBUS.GetGiaTri(cenCommon.ThamSoNguoiSuDung.ctHopDongVanChuyen_DenNgay);
                    frm_ctHopDongVanChuyen.Show();
                    break;
                case cenCommon.LoaiManHinh.IDDonHang:
                    frm_ctDonHang frm_ctDonHang = new frm_ctDonHang()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = IDDanhMucChungTu,
                        MdiParent = mdiParent,

                    };
                    frm_ctDonHang.Show();
                    break;
                case cenCommon.LoaiManHinh.IDTamUng:
                    frm_ctTamUng frm_ctTamUng = new frm_ctTamUng()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent,
                    };
                    frm_ctTamUng.Show();
                    break;
                case cenCommon.LoaiManHinh.IDThanhToanTamUng:
                    frm_ctThanhToanTamUng frm_ctThanhToanTamUng = new frm_ctThanhToanTamUng()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctThanhToanTamUng.Show();
                    break;
                case cenCommon.LoaiManHinh.IDChotDoanhThuGuiKeToan:
                    frm_ctChotDoanhThuGuiKeToan frm_ctChotDoanhThuGuiKeToan = new frm_ctChotDoanhThuGuiKeToan()
                    {
                        Text = formCaption,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctChotDoanhThuGuiKeToan.Show();
                    break;
                case cenCommon.LoaiManHinh.IDChiPhiVanTai:
                    frm_ctChiPhiVanTai frm_ctChiPhiVanTai = new frm_ctChiPhiVanTai()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctChiPhiVanTai.Show();
                    break;
                case cenCommon.LoaiManHinh.IDChotChiPhiVanTaiGuiKeToan:
                    frm_ctChotChiPhiVanTaiGuiKeToan frm_ctChotChiPhiVanTaiGuiKeToan = new frm_ctChotChiPhiVanTaiGuiKeToan()
                    {
                        Text = formCaption,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctChotChiPhiVanTaiGuiKeToan.Show();
                    break;
                case cenCommon.LoaiManHinh.IDChiPhiVanTaiBoSung:
                    frm_ctChiPhiVanTaiBoSung frm_ctChiPhiVanTaiBoSung = new frm_ctChiPhiVanTaiBoSung()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctChiPhiVanTaiBoSung.Show();
                    break;
                case cenCommon.LoaiManHinh.IDChotChiPhiVanTaiBoSungGuiKeToan:
                    frm_ctChotChiPhiVanTaiBoSungGuiKeToan frm_ctChotChiPhiVanTaiBoSungGuiKeToan = new frm_ctChotChiPhiVanTaiBoSungGuiKeToan()
                    {
                        Text = formCaption,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctChotChiPhiVanTaiBoSungGuiKeToan.Show();
                    break;
                case cenCommon.LoaiManHinh.IDSuaChua:
                    frm_ctSuaChua frm_ctSuaChua = new frm_ctSuaChua()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = IDDanhMucChungTu,
                        MdiParent = mdiParent
                    };
                    frm_ctSuaChua.Show();
                    break;
                case cenCommon.LoaiManHinh.IDDieuHanh:
                    frm_ctDieuHanh frm_ctDieuHanh = new frm_ctDieuHanh()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctDieuHanh.Show();
                    break;
                case cenCommon.LoaiManHinh.IDHotline:
                    frm_ctHotline frm_ctHotline = new frm_ctHotline()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctHotline.Show();
                    break;
                case cenCommon.LoaiManHinh.IDInGiayVanTai:
                    frm_ctInGiayVanTai frm_ctInGiayVanTai = new frm_ctInGiayVanTai()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = DanhMucChungTuBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoChungTuDonHang)),
                        MdiParent = mdiParent
                    };
                    frm_ctInGiayVanTai.Show();
                    break;
                case cenCommon.LoaiManHinh.IDKeHoachVanTai:
                    frm_ctKeHoachVanTai frm_ctKeHoachVanTai = new frm_ctKeHoachVanTai()
                    {
                        Text = formCaption,
                    };
                    frm_ctKeHoachVanTai.Show();
                    break;
                case cenCommon.LoaiManHinh.IDPhieuDoNhienLieu:
                    frm_ctPhieuDoNhienLieu frm_ctPhieuDoNhienLieu = new frm_ctPhieuDoNhienLieu()
                    {
                        Text = formCaption,
                        LoaiManHinh = LoaiManHinh,
                        IDDanhMucChungTu = IDDanhMucChungTu,
                        MdiParent = mdiParent
                    };
                    frm_ctPhieuDoNhienLieu.Show();
                    break;
            }

        }
        public static void runBaoCao(String IDDanhMucBaoCao, Form MDIParent)
        {
            DanhMucPhanQuyenBUS.GetPhanQuyenBaoCao(cenCommon.GlobalVariables.IDDanhMucPhanQuyen, IDDanhMucBaoCao, out bool Xem);
            if (!Xem)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xem báo cáo này!");
                return;
            }
            DanhMucBaoCaoBUS BUS = new DanhMucBaoCaoBUS();
            string MaDanhMucBaoCao = BUS.GetMaByID(IDDanhMucBaoCao);
            //Lấy tên procedure
            ConnectionDAO connectionDAO = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", IDDanhMucBaoCao);
            DataSet dsBaoCao = connectionDAO.dsList(sqlParameters, "List_DanhMucBaoCao");
            sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@IDDanhMucBaoCao", IDDanhMucBaoCao);
            DataTable dtBaoCaoCot = connectionDAO.tableList(sqlParameters, "List_DanhMucBaoCaoCot", "DanhMucBaoCaoCot");

            cenReportController.Report.runReport(dsBaoCao.Tables[0].Rows[0]["ReportProcedureName"].ToString(), dsBaoCao.Tables[0].Rows[0]["Ten"].ToString(), MDIParent);
            return;

            switch (MaDanhMucBaoCao)
            {
                case cenCommon.LoaiBaoCao.BC_DOANHTHU_KD:
                    //
                    frmReportParameters_KhachHang_Sale frmReportParameters_KhachHang_Sale = new frmReportParameters_KhachHang_Sale();
                    frmReportParameters_KhachHang_Sale.ShowDialog();
                    if (frmReportParameters_KhachHang_Sale.OK)
                    {
                        DataTable dt = Reports.rep_BC_DOANHTHU_KD(frmReportParameters_KhachHang_Sale.dTuNgay, frmReportParameters_KhachHang_Sale.dDenNgay, frmReportParameters_KhachHang_Sale.IDDanhMucKhachHang, frmReportParameters_KhachHang_Sale.IDDanhMucSale);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_KhachHang_Sale.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_KhachHang_Sale.dDenNgay;
                        frmReportViewer.IDDanhMucKhachHang = frmReportParameters_KhachHang_Sale.IDDanhMucKhachHang;
                        frmReportViewer.IDDanhMucSale = frmReportParameters_KhachHang_Sale.IDDanhMucSale;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_KhachHang_Sale.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_DOANHTHU_KD_CNKH:
                    frmReportParameters_KhachHang_Sale frmReportParameters_KhachHang_Sale1 = new frmReportParameters_KhachHang_Sale();
                    frmReportParameters_KhachHang_Sale1.ShowDialog();
                    if (frmReportParameters_KhachHang_Sale1.OK)
                    {
                        DataTable dt = Reports.rep_BC_DOANHTHU_KD_CNKH(frmReportParameters_KhachHang_Sale1.dTuNgay, frmReportParameters_KhachHang_Sale1.dDenNgay, frmReportParameters_KhachHang_Sale1.IDDanhMucKhachHang, frmReportParameters_KhachHang_Sale1.IDDanhMucSale);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_KhachHang_Sale1.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_KhachHang_Sale1.dDenNgay;
                        frmReportViewer.IDDanhMucKhachHang = frmReportParameters_KhachHang_Sale1.IDDanhMucKhachHang;
                        frmReportViewer.IDDanhMucSale = frmReportParameters_KhachHang_Sale1.IDDanhMucSale;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_KhachHang_Sale1.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_CHI_PHI_VAN_TAI:
                    frmReportParameters_NhomHang_Sale frmReportParameters_NhomHang_Sale = new frmReportParameters_NhomHang_Sale();
                    frmReportParameters_NhomHang_Sale.ShowDialog();
                    if (frmReportParameters_NhomHang_Sale.OK)
                    {
                        DataTable dt = Reports.rep_BC_CHI_PHI_VAN_TAI(frmReportParameters_NhomHang_Sale.dTuNgay, frmReportParameters_NhomHang_Sale.dDenNgay, frmReportParameters_NhomHang_Sale.IDDanhMucNhomHangVanChuyen, frmReportParameters_NhomHang_Sale.IDDanhMucSale);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_NhomHang_Sale.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_NhomHang_Sale.dDenNgay;
                        frmReportViewer.IDDanhMucNhomHangVanChuyen = frmReportParameters_NhomHang_Sale.IDDanhMucNhomHangVanChuyen;
                        frmReportViewer.IDDanhMucSale = frmReportParameters_NhomHang_Sale.IDDanhMucSale;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_NhomHang_Sale.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_CHI_PHI_VAN_TAI_BO_SUNG:
                    frmReportParameters_NhomHang_Sale frmReportParameters_NhomHang_Sale1 = new frmReportParameters_NhomHang_Sale();
                    frmReportParameters_NhomHang_Sale1.ShowDialog();
                    if (frmReportParameters_NhomHang_Sale1.OK)
                    {
                        DataTable dt = Reports.rep_BC_CHI_PHI_VAN_TAI_BO_SUNG(frmReportParameters_NhomHang_Sale1.dTuNgay, frmReportParameters_NhomHang_Sale1.dDenNgay, frmReportParameters_NhomHang_Sale1.IDDanhMucNhomHangVanChuyen, frmReportParameters_NhomHang_Sale1.IDDanhMucSale);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_NhomHang_Sale1.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_NhomHang_Sale1.dDenNgay;
                        frmReportViewer.IDDanhMucNhomHangVanChuyen = frmReportParameters_NhomHang_Sale1.IDDanhMucNhomHangVanChuyen;
                        frmReportViewer.IDDanhMucSale = frmReportParameters_NhomHang_Sale1.IDDanhMucSale;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_NhomHang_Sale1.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_DOANHTHU_KT:
                    frmReportParameters_KhachHang_Sale frmReportParameters_KhachHang_Sale2 = new frmReportParameters_KhachHang_Sale();
                    frmReportParameters_KhachHang_Sale2.ShowDialog();
                    if (frmReportParameters_KhachHang_Sale2.OK)
                    {
                        DataTable dt = Reports.rep_BC_DOANHTHU_KT(frmReportParameters_KhachHang_Sale2.dTuNgay, frmReportParameters_KhachHang_Sale2.dDenNgay, frmReportParameters_KhachHang_Sale2.IDDanhMucKhachHang, frmReportParameters_KhachHang_Sale2.IDDanhMucSale);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_KhachHang_Sale2.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_KhachHang_Sale2.dDenNgay;
                        frmReportViewer.IDDanhMucKhachHang = frmReportParameters_KhachHang_Sale2.IDDanhMucKhachHang;
                        frmReportViewer.IDDanhMucSale = frmReportParameters_KhachHang_Sale2.IDDanhMucSale;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_KhachHang_Sale2.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_LOINHUAN_KD:
                    frmReportParameters_KhachHang_Sale frmReportParameters_KhachHang_Sale3 = new frmReportParameters_KhachHang_Sale();
                    frmReportParameters_KhachHang_Sale3.ShowDialog();
                    if (frmReportParameters_KhachHang_Sale3.OK)
                    {
                        DataSet ds = Reports.rep_BC_LOINHUAN_KD(frmReportParameters_KhachHang_Sale3.dTuNgay, frmReportParameters_KhachHang_Sale3.dDenNgay, frmReportParameters_KhachHang_Sale3.IDDanhMucKhachHang, frmReportParameters_KhachHang_Sale3.IDDanhMucSale);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = ds.Tables[0];
                        frmReportViewer.dsData = ds;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_KhachHang_Sale3.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_KhachHang_Sale3.dDenNgay;
                        frmReportViewer.IDDanhMucKhachHang = frmReportParameters_KhachHang_Sale3.IDDanhMucKhachHang;
                        frmReportViewer.IDDanhMucSale = frmReportParameters_KhachHang_Sale3.IDDanhMucSale;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_KhachHang_Sale3.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_SUACHUA:
                    frmReportParameters_Xe frmReportParameters_Xe = new frmReportParameters_Xe();
                    frmReportParameters_Xe.ShowDialog();
                    if (frmReportParameters_Xe.OK)
                    {
                        DataTable dt = Reports.rep_BC_SUACHUA(frmReportParameters_Xe.dTuNgay, frmReportParameters_Xe.dDenNgay, frmReportParameters_Xe.IDDanhMucXe);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_Xe.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_Xe.dDenNgay;
                        frmReportViewer.IDDanhMucXe = frmReportParameters_Xe.IDDanhMucXe;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_Xe.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_TU_QT:
                    frmReportParameters_KhachHang frmReportParameters_KhachHang = new frmReportParameters_KhachHang();
                    frmReportParameters_KhachHang.ShowDialog();
                    if (frmReportParameters_KhachHang.OK)
                    {
                        DataTable dt = Reports.rep_BC_TU_QT(frmReportParameters_KhachHang.dTuNgay, frmReportParameters_KhachHang.dDenNgay, frmReportParameters_KhachHang.IDDanhMucKhachHang);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_KhachHang.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_KhachHang.dDenNgay;
                        frmReportViewer.IDDanhMucKhachHang = frmReportParameters_KhachHang.IDDanhMucKhachHang;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_KhachHang.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
                case cenCommon.LoaiBaoCao.BC_TU_TIENDUONG:
                    frmReportParameters_ChuXe frmReportParameters_ChuXe = new frmReportParameters_ChuXe();
                    frmReportParameters_ChuXe.ShowDialog();
                    if (frmReportParameters_ChuXe.OK)
                    {
                        DataTable dt = Reports.rep_BC_TU_TIENDUONG(frmReportParameters_ChuXe.dTuNgay, frmReportParameters_ChuXe.dDenNgay, frmReportParameters_ChuXe.IDDanhMucChuXe);
                        frmReportViewer frmReportViewer = new frmReportViewer();
                        frmReportViewer.MaDanhMucBaoCao = MaDanhMucBaoCao;
                        frmReportViewer.MDIParent = MDIParent;
                        frmReportViewer.dtData = dt;
                        frmReportViewer.dtCauTrucCot = dtBaoCaoCot;
                        frmReportViewer.TenFileExcel = dsBaoCao.Tables[0].Rows[0]["FileExcelMau"].ToString();
                        frmReportViewer.TenSheetExcel = dsBaoCao.Tables[0].Rows[0]["SheetExcelMau"].ToString();
                        frmReportViewer.SoDongBatDau = (cenCommon.cenCommon.IsNull(dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"])) ? 1 : (int)dsBaoCao.Tables[0].Rows[0]["SoDongBatDau"];
                        frmReportViewer.Text = MaDanhMucBaoCao;
                        frmReportViewer.dTuNgay = frmReportParameters_ChuXe.dTuNgay;
                        frmReportViewer.dDenNgay = frmReportParameters_ChuXe.dDenNgay;
                        frmReportViewer.IDDanhMucChuXe = frmReportParameters_ChuXe.IDDanhMucChuXe;
                        frmReportViewer.ChuoiThamSoHienThiGrid = frmReportParameters_ChuXe.ChuoiThamSoHienThiGrid;
                        frmReportViewer.Show();
                    }
                    break;
            }
        }
        public class validDanhMuc
        {
            public static void txtBox_Validating(object sender, CancelEventArgs e)
            {
                saTextBox txtID = (saTextBox)sender;
                txtID.Text = txtID.Text.Trim();
                if (txtID.dtValid != null) //Nếu có loại danh mục thì mới valid
                {
                    //Xoá textbox chứa thông tin trả về nếu ko có dữ liệu
                    if (txtID.Text == "")
                    {
                        txtID.ID = null;
                        //Tìm textbox chứa tên
                        if (txtID.txtMoRong != null && txtID.txtMoRong.Count() > 0)
                        {
                            foreach (saTextBox txtMoRong in txtID.txtMoRong)
                            {
                                txtMoRong.Text = "";
                            }
                        }
                    }

                    if ((txtID.IsModified || txtID.ID == null || txtID.ID.ToString() == "") && txtID.IsValid)
                    {
                        if (!txtID.IsNullable | txtID.Text != "")
                        {
                            String ID = "",
                                    Ma = "";

                            String[] ThongTinMoRong = txtID.ReturnedColumnsList.Split(';');

                            Int32 iThongTinMoRong = 0;
                            if (txtID.txtMoRong != null && iThongTinMoRong < ThongTinMoRong.Count()) iThongTinMoRong = ThongTinMoRong.Count();

                            List<DataRow> drDoiTuong = ValidDoiTuong(txtID.dtValid, txtID.Text.Trim(), txtID.IDDanhMucLoaiDoiTuong, txtID.IsMultiSelect, txtID.showLookUp, txtID.ValidColumnName, txtID.ValueColumnName, txtID.procedureName);

                            if (drDoiTuong == null || drDoiTuong.Count == 0)
                            {
                                txtID.ID = null;
                                if (txtID.txtMoRong != null && txtID.txtMoRong.Count() > 0)
                                {
                                    foreach (saTextBox txtMoRong in txtID.txtMoRong)
                                    {
                                        txtMoRong.Text = "";
                                    }
                                }
                                return;
                            }

                            if (txtID.txtMoRong != null && txtID.txtMoRong.Count() > 0)
                            {
                                foreach (saTextBox txtMoRong in txtID.txtMoRong)
                                {
                                    txtMoRong.Text = "";
                                }
                            }
                            foreach (DataRow iDoiTuong in drDoiTuong)
                            {
                                Ma = Ma + iDoiTuong[txtID.ValidColumnName].ToString() + ";";
                                ID = ID + iDoiTuong[txtID.ValueColumnName].ToString() + ";";
                                if (txtID.txtMoRong != null)
                                {
                                    for (Int32 i = 0; i < iThongTinMoRong; i++)
                                    {
                                        if (txtID.dtValid.Columns.Contains(ThongTinMoRong[i]) && iDoiTuong[ThongTinMoRong[i]] != DBNull.Value)
                                        {
                                            txtID.txtMoRong[i].Text = txtID.txtMoRong[i].Text + iDoiTuong[ThongTinMoRong[i]].ToString() + ";";
                                        }
                                    }
                                }
                            }
                            txtID.Text = Ma.Substring(0, Ma.Length - 1);
                            txtID.ID = ID.Substring(0, ID.Length - 1);
                            if (txtID.txtMoRong != null)
                            {
                                for (Int32 i = 0; i < iThongTinMoRong; i++)
                                {
                                    if (txtID.txtMoRong[i].Text.Length > 1)
                                        txtID.txtMoRong[i].Text = txtID.txtMoRong[i].Text.Substring(0, txtID.txtMoRong[i].Text.Length - 1);
                                }
                            }
                        }
                    }
                }
            }
            public static List<DataRow> ValidDoiTuong(DataTable dtValid, object FindValue, object IDDanhMucLoaiDoiTuong, Boolean MultiSelect, bool ShowLookUp, string ValidColumnName, string ValueColumnName, string procedureName)
            {
                //Danh sách dòng dữ liệu trả về
                List<DataRow> listDoiTuong = new List<DataRow>();
                //Có cần phải mở form look-up hay không?
                Boolean isShowLookUp = false;
                //Valid có trả về dòng dữ liệu hay không
                ValidDataRow(listDoiTuong, dtValid, FindValue.ToString(), ValidColumnName, out bool isExist, out bool isOK, ref isShowLookUp);
                if (ShowLookUp && listDoiTuong.Count != 1) //Nếu user yêu cầu mở form look-up và cần phải mở
                {
                    //Gọi form lookup tương ứng theo store
                    switch (procedureName)
                    {
                        case DanhMucXe.listProcedureName:
                            frmDanhMucXeValid frmDanhMucXeValid = new frmDanhMucXeValid
                            {
                                dataTable = dtValid,
                                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                                validValue = FindValue
                            };
                            frmDanhMucXeValid.ShowDialog();
                            if (frmDanhMucXeValid.dataRows != null)
                            {
                                listDoiTuong = frmDanhMucXeValid.dataRows;
                            }
                            frmDanhMucXeValid.Dispose();
                            break;
                        case DanhMucTuyenVanTai.listProcedureName:
                            frmDanhMucTuyenVanTaiValid frmDanhMucTuyenVanTaiValid = new frmDanhMucTuyenVanTaiValid
                            {
                                dataTable = dtValid,
                                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                                validValue = FindValue
                            };
                            frmDanhMucTuyenVanTaiValid.ShowDialog();
                            if (frmDanhMucTuyenVanTaiValid.dataRows != null)
                            {
                                listDoiTuong = frmDanhMucTuyenVanTaiValid.dataRows;
                            }
                            frmDanhMucTuyenVanTaiValid.Dispose();
                            break;
                        case DanhMucHangHoa.listProcedureName:
                            frmDanhMucHangHoaValid frmDanhMucHangHoaValid = new frmDanhMucHangHoaValid
                            {
                                dataTable = dtValid,
                                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                                validValue = FindValue
                            };
                            frmDanhMucHangHoaValid.ShowDialog();
                            if (frmDanhMucHangHoaValid.dataRows != null)
                            {
                                listDoiTuong = frmDanhMucHangHoaValid.dataRows;
                            }
                            frmDanhMucHangHoaValid.Dispose();
                            break;
                        case DanhMucKhachHang.listProcedureName:
                            frmDanhMucKhachHangValid frmDanhMucKhachHangValid = new frmDanhMucKhachHangValid
                            {
                                dataTable = dtValid,
                                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                                validValue = FindValue
                            };
                            frmDanhMucKhachHangValid.ShowDialog();
                            if (frmDanhMucKhachHangValid.dataRows != null)
                            {
                                listDoiTuong = frmDanhMucKhachHangValid.dataRows;
                            }
                            frmDanhMucKhachHangValid.Dispose();
                            break;
                        case DanhMucNhanSu.listProcedureName:
                            frmDanhMucNhanSuValid frmDanhMucNhanSuValid = new frmDanhMucNhanSuValid
                            {
                                dataTable = dtValid,
                                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                                validValue = FindValue
                            };
                            frmDanhMucNhanSuValid.ShowDialog();
                            if (frmDanhMucNhanSuValid.dataRows != null)
                            {
                                listDoiTuong = frmDanhMucNhanSuValid.dataRows;
                            }
                            frmDanhMucNhanSuValid.Dispose();
                            break;
                        default:
                            DatabaseCore.Forms.frmDanhMucDoiTuongValid frmLookUp = new DatabaseCore.Forms.frmDanhMucDoiTuongValid
                            {
                                dataTable = dtValid,
                                IDDanhMucLoaiDoiTuong = IDDanhMucLoaiDoiTuong,
                                validValue = FindValue
                            };
                            frmLookUp.ShowDialog();
                            if (frmLookUp.dataRows != null)
                            {
                                listDoiTuong = frmLookUp.dataRows;
                            }
                            frmLookUp.Dispose();
                            break;
                    }


                }
                return listDoiTuong;
            }
            public static void ValidDataRow(List<DataRow> listDoiTuong, DataTable dtDoiTuong, String Value, String ColumnName, out Boolean isExist, out Boolean isOK, ref Boolean isShowLookUp)
            {
                isExist = false;
                isOK = false;
                String sSelect = ColumnName + "=\'" + Value + "\'";
                DataRow[] drs = dtDoiTuong.Select(sSelect);
                //Nếu không có dòng dữ liệu trả về
                if (drs == null || drs.Length == 0)
                {
                    isShowLookUp = true;
                    listDoiTuong = null;
                    return;
                }
                //Nếu chỉ có 1 dòng dữ liệu trả về
                if (dtDoiTuong.Rows.Count == 1)
                {
                    isExist = true;
                    //Nếu có thì chỉ add vào danh sách đúng dòng này
                    listDoiTuong.Add(drs[0]);
                    return;
                }
                //Nếu có hơn 1 dòng dữ liệu trả về
                if (dtDoiTuong.Rows.Count > 1)
                {
                    isExist = true;
                    //Kiểm tra xem có mã nào chính xác bằng mã này hay không?
                    isOK = (dtDoiTuong.Select(sSelect).Length > 0);
                    //Nếu không có dòng chứa mã này
                    if (!isOK)
                    {
                        //Nếu không thì add toàn bộ dòng trả về
                        //foreach (DataRow drDoiTuong in dtDoiTuong.Rows)
                        //    listDoiTuong.Add(drDoiTuong);
                        //Show Look-up
                        isShowLookUp = true;
                        listDoiTuong = null;
                        return;
                    }
                    if (isOK)
                    {
                        //Nếu có thì chỉ add vào danh sách đúng dòng này
                        listDoiTuong.Add(dtDoiTuong.Select(sSelect)[0]);
                        return;
                    }
                }
            }
            public static void cboDanhMuc_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.F5)
                {
                    saComboDanhMuc cboDanhMuc = (saComboDanhMuc)sender;
                    //Gọi form lookup tương ứng theo store
                    switch (cboDanhMuc.procedureName)
                    {
                        case DanhMucXe.listProcedureName:
                            frmDanhMucXeValid frmDanhMucXeValid = new frmDanhMucXeValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucXeValid.ShowDialog();
                            if (frmDanhMucXeValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucXeValid.value;
                            }
                            frmDanhMucXeValid.Dispose();
                            break;
                        case DanhMucTuyenVanTai.listProcedureName:
                            frmDanhMucTuyenVanTaiValid frmDanhMucTuyenVanTaiValid = new frmDanhMucTuyenVanTaiValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucTuyenVanTaiValid.ShowDialog();
                            if (frmDanhMucTuyenVanTaiValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucTuyenVanTaiValid.value;
                            }
                            frmDanhMucTuyenVanTaiValid.Dispose();
                            break;
                        case DanhMucHangHoa.listProcedureName:
                            frmDanhMucHangHoaValid frmDanhMucHangHoaValid = new frmDanhMucHangHoaValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucHangHoaValid.ShowDialog();
                            if (frmDanhMucHangHoaValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucHangHoaValid.value;
                            }
                            frmDanhMucHangHoaValid.Dispose();
                            break;
                        case DanhMucKhachHang.listProcedureName:
                            frmDanhMucKhachHangValid frmDanhMucKhachHangValid = new frmDanhMucKhachHangValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucKhachHangValid.ShowDialog();
                            if (frmDanhMucKhachHangValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucKhachHangValid.value;
                            }
                            frmDanhMucKhachHangValid.Dispose();
                            break;
                        case DanhMucThauPhu.listProcedureName:
                            frmDanhMucThauPhuValid frmDanhMucThauPhuValid = new frmDanhMucThauPhuValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucThauPhuValid.ShowDialog();
                            if (frmDanhMucThauPhuValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucThauPhuValid.value;
                            }
                            frmDanhMucThauPhuValid.Dispose();
                            break;
                        case DanhMucTaiXe.listProcedureName:
                            frmDanhMucTaiXeValid frmDanhMucTaiXeValid = new frmDanhMucTaiXeValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucTaiXeValid.ShowDialog();
                            if (frmDanhMucTaiXeValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucTaiXeValid.value;
                            }
                            frmDanhMucTaiXeValid.Dispose();
                            break;
                        case DanhMucNhanSu.listProcedureName:
                            frmDanhMucNhanSuValid frmDanhMucNhanSuValid = new frmDanhMucNhanSuValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmDanhMucNhanSuValid.ShowDialog();
                            if (frmDanhMucNhanSuValid.value != null)
                            {
                                cboDanhMuc.Value = frmDanhMucNhanSuValid.value;
                            }
                            frmDanhMucNhanSuValid.Dispose();
                            break;
                        default:
                            DatabaseCore.Forms.frmDanhMucDoiTuongValid frmLookUp = new DatabaseCore.Forms.frmDanhMucDoiTuongValid
                            {
                                dataTable = cboDanhMuc.dtValid,
                                IDDanhMucLoaiDoiTuong = cboDanhMuc.IDDanhMucLoaiDoiTuong,
                                validValue = cboDanhMuc.Text
                            };
                            frmLookUp.ShowDialog();
                            if (frmLookUp.value != null)
                            {
                                cboDanhMuc.Value = frmLookUp.value;
                            }
                            frmLookUp.Dispose();
                            break;
                    }
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }
        }
    }
}

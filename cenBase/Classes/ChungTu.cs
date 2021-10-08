﻿using cenBase.Forms;
using cenDAO;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.ReportAppServer.DataSetConversion;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cenBase.Classes
{
    public class ChungTu
    {
        public Form MdiParentForm = null;

        public static int MaxTempID(DataTable dt)
        {
            int MaxTempID = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && int.TryParse(dr["ID"].ToString(), out int tempID))
                    MaxTempID = Math.Min(MaxTempID, tempID);
            }
            return MaxTempID - 1;
        }
        public static void CheckTrangThaiChungTu(String IDDanhMucTrangThaiChungTu, out Boolean suaChungTu, out Boolean XoaChungTu)
        {
            suaChungTu = false;
            XoaChungTu = false;
            List<SqlParameter> sqlParamList = new List<SqlParameter>
            {
                new SqlParameter("@IDDanhMucTrangThaiChungTu", IDDanhMucTrangThaiChungTu),
                new SqlParameter("@suaChungTu", null),
                new SqlParameter("@XoaChungTu", null)
            };
            commonDAO.ExecNonQueryStoredProcedure("Check_TrangThaiChungTu", sqlParamList);
            suaChungTu = Convert.ToBoolean(sqlParamList[1].Value);
            XoaChungTu = Convert.ToBoolean(sqlParamList[2].Value);
        }
        public static void gridColumnDataProcess(String LoaiManHinh, UltraGrid ug, UltraGridCell uCell, out Boolean GridValidation, Boolean ShowLookUp, Boolean ValidDanhMucPhi = false, Object TinhTheoCont = null, Object PhamViApDung = null, Object ContSize = null, Object IDDanhMucDaiLy = null)
        {
            Decimal TienHang = 0;
            Decimal TienThue = 0;
            Decimal SoTien = 0;


            GridValidation = true;
            String TenCotTen = "";
            String TenCotID = "";
            List<DataRow> listDoiTuong = null;
            if (uCell != null)
            {
                UltraGridRow uRow = uCell.Row;
                Object cValue = uCell.Value;
                switch (uCell.Column.Key.ToUpper())
                {
                    case "SOLUONG":
                        GridValidation = false;
                        if (uRow.Band.Columns.Exists("DonGia"))
                            TienHang = Decimal.Round(cenCommon.cenCommon.Convert2Decimal(uRow.Cells["SoLuong"].Value) * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["DonGia"].Value), cenCommon.GlobalVariables.LamTronDonGia);
                        if (uRow.Band.Columns.Exists("ThueSuat"))
                            TienThue = Decimal.Round(TienHang * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["ThueSuat"].Value) / 100, cenCommon.GlobalVariables.LamTronSoTien);
                        SoTien = Decimal.Round(TienHang + TienThue, cenCommon.GlobalVariables.LamTronSoTien);
                        if (uRow.Band.Columns.Exists("TienHang"))
                            uRow.Cells["TienHang"].Value = TienHang;
                        if (uRow.Band.Columns.Exists("TienThue"))
                            uRow.Cells["TienThue"].Value = TienThue;
                        if (uRow.Band.Columns.Exists("ThanhTien"))
                            uRow.Cells["ThanhTien"].Value = SoTien;
                        if (uRow.Band.Columns.Exists("SoTien"))
                            uRow.Cells["SoTien"].Value = SoTien;
                        GridValidation = true;
                        break;
                    case "DONGIA":
                        GridValidation = false;
                        if (uRow.Band.Columns.Exists("SoLuong"))
                            TienHang = Decimal.Round(cenCommon.cenCommon.Convert2Decimal(uRow.Cells["SoLuong"].Value) * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["DonGia"].Value), cenCommon.GlobalVariables.LamTronDonGia);
                        if (uRow.Band.Columns.Exists("ThueSuat"))
                            TienThue = Decimal.Round(TienHang * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["ThueSuat"].Value) / 100, cenCommon.GlobalVariables.LamTronSoTien);
                        SoTien = Decimal.Round(TienHang + TienThue, cenCommon.GlobalVariables.LamTronSoTien);
                        if (uRow.Band.Columns.Exists("TienHang"))
                            uRow.Cells["TienHang"].Value = TienHang;
                        if (uRow.Band.Columns.Exists("TienThue"))
                            uRow.Cells["TienThue"].Value = TienThue;
                        if (uRow.Band.Columns.Exists("ThanhTien"))
                            uRow.Cells["ThanhTien"].Value = SoTien;
                        if (uRow.Band.Columns.Exists("SoTien"))
                            uRow.Cells["SoTien"].Value = SoTien;
                        GridValidation = true;
                        break;

                    case "TIENHANG":
                        GridValidation = false;
                        if (uRow.Band.Columns.Exists("SoLuong"))
                            TienHang = Decimal.Round(cenCommon.cenCommon.Convert2Decimal(uRow.Cells["SoLuong"].Value) * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["DonGia"].Value), cenCommon.GlobalVariables.LamTronDonGia);
                        if (uRow.Band.Columns.Exists("ThueSuat"))
                            TienThue = Decimal.Round(TienHang * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["ThueSuat"].Value) / 100, cenCommon.GlobalVariables.LamTronSoTien);
                        SoTien = Decimal.Round(TienHang + TienThue, cenCommon.GlobalVariables.LamTronSoTien);
                        if (uRow.Band.Columns.Exists("TienHang"))
                            uRow.Cells["TienHang"].Value = TienHang;
                        if (uRow.Band.Columns.Exists("TienThue"))
                            uRow.Cells["TienThue"].Value = TienThue;
                        if (uRow.Band.Columns.Exists("ThanhTien"))
                            uRow.Cells["ThanhTien"].Value = SoTien;
                        GridValidation = true;
                        break;
                    case "THUESUAT":
                        GridValidation = false;
                        if (uRow.Band.Columns.Exists("SoLuong"))
                            TienHang = Decimal.Round(cenCommon.cenCommon.Convert2Decimal(uRow.Cells["SoLuong"].Value) * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["DonGia"].Value), cenCommon.GlobalVariables.LamTronDonGia);
                        if (uRow.Band.Columns.Exists("ThueSuat"))
                            TienThue = Decimal.Round(TienHang * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["ThueSuat"].Value) / 100, cenCommon.GlobalVariables.LamTronSoTien);
                        SoTien = Decimal.Round(TienHang + TienThue, cenCommon.GlobalVariables.LamTronSoTien);
                        if (uRow.Band.Columns.Exists("TienHang"))
                            uRow.Cells["TienHang"].Value = TienHang;
                        if (uRow.Band.Columns.Exists("TienThue"))
                            uRow.Cells["TienThue"].Value = TienThue;
                        if (uRow.Band.Columns.Exists("ThanhTien"))
                            uRow.Cells["ThanhTien"].Value = SoTien;
                        GridValidation = true;
                        break;
                    case "TIENTHUE":
                        GridValidation = false;
                        if (uRow.Band.Columns.Exists("SoLuong"))
                            TienHang = Decimal.Round(cenCommon.cenCommon.Convert2Decimal(uRow.Cells["SoLuong"].Value) * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["DonGia"].Value), cenCommon.GlobalVariables.LamTronDonGia);
                        if (uRow.Band.Columns.Exists("ThueSuat"))
                            TienThue = Decimal.Round(TienHang * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["ThueSuat"].Value) / 100, cenCommon.GlobalVariables.LamTronSoTien);
                        SoTien = Decimal.Round(TienHang + TienThue, cenCommon.GlobalVariables.LamTronSoTien);
                        if (uRow.Band.Columns.Exists("TienHang"))
                            uRow.Cells["TienHang"].Value = TienHang;
                        if (uRow.Band.Columns.Exists("TienThue"))
                            uRow.Cells["TienThue"].Value = TienThue;
                        if (uRow.Band.Columns.Exists("ThanhTien"))
                            uRow.Cells["ThanhTien"].Value = SoTien;
                        GridValidation = true;
                        break;
                    case "THANHTIEN":
                        GridValidation = false;
                        //if (uRow.Band.Columns.Exists("SoLuong"))
                        //    TienHang = Decimal.Round(cenCommon.cenCommon.Convert2Decimal(uRow.Cells["SoLuong"].Value) * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["DonGia"].Value), cenCommon.GlobalVariables.LamTronDonGia);
                        //if (uRow.Band.Columns.Exists("ThueSuat"))
                        //    TienThue = Decimal.Round(TienHang * cenCommon.cenCommon.Convert2Decimal(uRow.Cells["ThueSuat"].Value) / 100, cenCommon.GlobalVariables.LamTronSoTien);
                        //SoTien = Decimal.Round(TienHang + TienThue, cenCommon.GlobalVariables.LamTronSoTien);
                        //if (uRow.Band.Columns.Exists("TienHang"))
                        //    uRow.Cells["TienHang"].Value = TienHang;
                        //if (uRow.Band.Columns.Exists("TienThue"))
                        //    uRow.Cells["TienThue"].Value = TienThue;
                        //if (uRow.Band.Columns.Exists("ThanhTien"))
                        //    uRow.Cells["ThanhTien"].Value = SoTien;
                        //GridValidation = true;
                        break;
                    default:
                        //Mã đối tượng
                        GridValidation = true;
                        if (uCell.Column.Key.ToString().StartsWith("MaDanhMuc")) //Valid danh mục đối tượng
                        {
                            String LoaiDanhMuc = uCell.Column.Key.Substring(2);
                            if (LoaiDanhMuc == "DanhMucChuHang" || LoaiDanhMuc == "DanhMucChuVo" || LoaiDanhMuc == "DanhMucHangTau1" || LoaiDanhMuc == "DanhMucHangTau2" || LoaiDanhMuc == "DanhMucDoiTuongThanhToan") LoaiDanhMuc = "DanhMucHangTau";
                            if (LoaiDanhMuc == "DanhMucKhungGioCamDien1" || LoaiDanhMuc == "DanhMucKhungGioCamDien2") LoaiDanhMuc = "DanhMucKhungGioCamDien";
                            String IDDanhMucLoaiDoiTuong = "";
                            //DanhMucBUS _DanhMucBUS = new DanhMucBUS();
                            //IDDanhMucLoaiDoiTuong = _DanhMucBUS.GetIDLoaiDoiTuong(LoaiDanhMuc);
                            //if (IDDanhMucLoaiDoiTuong != "") LoaiDanhMuc = _DanhMucBUS.GetTenBangDuLieu(IDDanhMucLoaiDoiTuong);
                            //TenCotID = "ID" + uCell.Column.Key.Substring(2);
                            //TenCotTen = "Ten" + uCell.Column.Key.Substring(2);
                            ////Nếu không có mã thì đặt ID và Ten về trống
                            //if (cValue == DBNull.Value || cValue.ToString() == "")
                            //{
                            //    GridValidation = false;
                            //    if (uRow.Band.Columns.Exists(TenCotID)) uRow.Cells[TenCotID].Value = "";
                            //    if (uRow.Band.Columns.Exists(TenCotTen)) uRow.Cells[TenCotTen].Value = "";
                            //    GridValidation = true;
                            //    return;
                            //}
                            //DataTable dtValid = new DataTable
                            //{
                            //    TableName = LoaiDanhMuc
                            //};
                            //listDoiTuong = cenBase.Classes.DanhMuc.ValidDoiTuong(dtValid, cValue.ToString(), LoaiDanhMuc, false, IDDanhMucLoaiDoiTuong, LoaiManHinh, ShowLookUp);
                            ////Nếu không có dòng dữ liệu trả về
                            //if (listDoiTuong == null || listDoiTuong.Count == 0)
                            //{
                            //    GridValidation = false;
                            //    uCell.Value = DBNull.Value;
                            //    if (uRow.Band.Columns.Exists(TenCotID)) uRow.Cells[TenCotID].Value = DBNull.Value;
                            //    if (uRow.Band.Columns.Exists(TenCotTen)) uRow.Cells[TenCotTen].Value = DBNull.Value;
                            //    GridValidation = true;
                            //    return;
                            //}
                            ////Nếu có dòng dữ liệu trả về
                            //if (listDoiTuong.Count > 0)
                            //{
                            //    //Chỉ lấy dòng dữ liệu đầu tiên
                            //    DataRow drDoiTuong = listDoiTuong[0];
                            //    if (drDoiTuong != null)
                            //    {
                            //        GridValidation = false;
                            //        if (drDoiTuong.Table.Columns.Contains("Ma"))
                            //        {
                            //            uCell.Value = drDoiTuong["Ma"].ToString();
                            //            //if (LoaiDanhMuc != "DanhMucViTriKho")
                            //            //    ug.DisplayLayout.Bands[0].Columns[uCell.Column.Key].DefaultCellValue = uCell.Value;
                            //        }
                            //        if (drDoiTuong.Table.Columns.Contains("ID") && uRow.Band.Columns.Exists(TenCotID))
                            //        {
                            //            uRow.Cells[TenCotID].Value = drDoiTuong["ID"].ToString();
                            //            //if (LoaiDanhMuc != "DanhMucViTriKho")
                            //            //    ug.DisplayLayout.Bands[0].Columns[TenCotID].DefaultCellValue = uRow.Cells[TenCotID].Value;
                            //        }
                            //        if (drDoiTuong.Table.Columns.Contains("Ten") && uRow.Band.Columns.Exists(TenCotTen))
                            //        {
                            //            uRow.Cells[TenCotTen].Value = drDoiTuong["Ten"].ToString();
                            //            //if (LoaiDanhMuc != "DanhMucViTriKho")
                            //            //    ug.DisplayLayout.Bands[0].Columns[TenCotTen].DefaultCellValue = uRow.Cells[TenCotTen].Value;
                            //        }

                            //        if (uCell.Column.Key.Substring(2) == "DanhMucLoaiHangHoa")
                            //        {
                            //            uRow.Cells["IDDanhMucDonViTinh"].Value = drDoiTuong["IDDanhMucDonViTinh"];
                            //            uRow.Cells["MaDanhMucDonViTinh"].Value = drDoiTuong["MaDanhMucDonViTinh"];
                            //        }

                            //        //if (uCell.Column.Key.Substring(2) == "DanhMucDichVuContainer")
                            //        //{
                            //        //    uRow.Cells["DonViTinh"].Value = drDoiTuong["DonViTinh"];
                            //        //    uRow.Cells["DonGia"].Value = drDoiTuong["DonGia"];
                            //        //}


                            //        //if (uCell.Column.Key.Substring(2) == "DanhMucPhi")
                            //        //{
                            //        //    uRow.Cells["DienGiai"].Value = drDoiTuong["DienGiaiTrenHoaDon"];
                            //        //    uRow.Cells["DonGia"].Value = drDoiTuong["DonGia"];
                            //        //    uRow.Cells["ThueSuat"].Value = drDoiTuong["ThueSuat"];
                            //        //}
                            //        GridValidation = true;
                            //        return;
                            //    }
                            //}
                        }
                        break;
                }
            }
        }
        public static void filterChungTu(String IDDanhMucChungTu, Int16 DetailLevel, String TableName, String TableNameDetail, String TableNameDetail2, String FormCaption, out object IDChungTu, out System.Data.DataSet dsChungTu)
        {
            IDChungTu = "";
            dsChungTu = null;
            //ChungTuBUS _BUS = new ChungTuBUS();
            //DataTable dtThamSo = _BUS.GetfilterParameter(TableName);
            //if (dtThamSo == null) { cenCommon.cenCommon.ErrorMessageOkOnly("Lỗi khi tìm kiểm chứng từ!"); return; }
            //frmChungTufilter frmfilterParameters = new frmChungTufilter
            //{
            //    Text = FormCaption,
            //    IDDanhMucChungTu = IDDanhMucChungTu,
            //    TableName = TableName,
            //    dtThamSo = dtThamSo
            //};
            //frmfilterParameters.ShowDialog();
            //if (frmfilterParameters.OK)
            //{
            //    dtThamSo = frmfilterParameters.dtThamSo;
            //    frmfilterParameters.Dispose();
            //    ChungTuDAO _DAO = new ChungTuDAO();
            //    dsChungTu = _DAO.Load(dtThamSo, IDDanhMucChungTu, DetailLevel, TableName, TableNameDetail, TableNameDetail2);
            //}
        }
        public static void inChungTu(String IDDanhMucChungTu, String IDChungTu, String TableName, String TableNameDetail, Form MDIParent, Boolean MultiplePrint, String FileReport, String spName, String TenMayIn, Int16 SoLien, Object LoaiManHinh, Int16 KieuIn)
        {
            //"_rpt_" + drChungTu["FileMauIn"].ToString() + "ID:" + drChungTu["ID"].ToString() + "PRINTER:" + drChungTu["TenMayIn"].ToString() + "SOLIEN:" + drChungTu["SoLien"].ToString() + "SPNAME:" + drChungTu["spName"].ToString()
            ConnectionDAO _DAO = new ConnectionDAO();
            SqlParameter[] sqlParameter;

            sqlParameter = new SqlParameter[3];
            sqlParameter[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameter[1] = new SqlParameter("@IDDanhMucChungTu", IDDanhMucChungTu);
            sqlParameter[2] = new SqlParameter("@ID", IDChungTu);

            System.Data.DataSet dsChungTu = _DAO.dsList(sqlParameter, spName);
            if (dsChungTu == null || dsChungTu.Tables[0].Rows.Count == 0) { cenCommon.cenCommon.ErrorMessageOkOnly("Không lấy được dữ liệu chứng từ!"); return; }

            String rptFile;
            Boolean OK = false;
            rptFile = "";

            System.Data.DataSet dsChungTuChiTiet;
            string XMLDataFile;

            if (dsChungTu.Tables.Count == 1)
                OK = PrintProcess_Parameter(LoaiManHinh.ToString(), FileReport, dsChungTu.Tables[0].Rows[0], dsChungTu.Tables[0].Copy(), IDChungTu.ToString(), out rptFile, out XMLDataFile, out dsChungTuChiTiet, false, String.Empty);
            else
                OK = PrintProcess_Parameter(LoaiManHinh.ToString(), FileReport, dsChungTu.Tables[0].Rows[0], dsChungTu.Tables[1].Copy(), IDChungTu.ToString(), out rptFile, out XMLDataFile, out dsChungTuChiTiet, false, String.Empty);
            if (OK)
            {
                cenBase.Forms.frmChungTuPrintPreview frmChungTuPrintPreview = new frmChungTuPrintPreview
                {
                    FileReport = rptFile,
                    MdiParent = MDIParent,
                    XMLFilePath = XMLDataFile,
                    dsData = dsChungTuChiTiet,
                    TenMayIn = TenMayIn,
                    SoLien = SoLien
                };
                frmChungTuPrintPreview.Show();
            }
        }
        public static Boolean PrintProcess_Parameter(string LoaiManHinh, String FileReport, DataRow drChungTu, DataTable dtChungTuDetail, String IDChungTu, out String GeneratedReportFile, out String XMLDataFile, out System.Data.DataSet dsChungTuChiTiet, Boolean PrintPreview, String PrinterName)
        {
            Boolean OK = false;
            //Tên file report được sinh ra
            XMLDataFile = "";
            dsChungTuChiTiet = null;
            GeneratedReportFile = cenCommon.GlobalVariables.TempDir + "\\" + Guid.NewGuid().ToString() + ".rpt";//cenCommon.GlobalVariables.TempDir + Guid.NewGuid().ToString() + ".rpt";
            String FormattedValue = ""; //Chuỗi giá trị sau khi định dạng
            String NumberFormatString = "";

            //Bắt đầu xử lý in chứng từ
            if (drChungTu != null)
            {
                String ChungTuDetailDataXMLPath = "";
                //Mở file Report
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rpt.Load(FileReport, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
                //Lưu vào file tạm
                rpt.SaveAs(GeneratedReportFile);
                rpt.Close();
                //Mở file tạm để ghi thông tin
                rpt.Load(GeneratedReportFile, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
                //
                CrystalDecisions.ReportAppServer.ClientDoc.ISCDReportClientDocument rptClientDoc = rpt.ReportClientDocument;
                CrystalDecisions.ReportAppServer.Controllers.ReportDefController2 reportDefController = rptClientDoc.ReportDefController;
                CrystalDecisions.ReportAppServer.Controllers.ReportObjectController reportObjectController = rptClientDoc.ReportDefController.ReportObjectController;
                ReportObjects reportObjects = reportObjectController.GetReportObjectsByKind(CrReportObjectKindEnum.crReportObjectKindText);
                //Gán dữ liệu cho Header Chứng từ
                foreach (FormulaField formulaObject in rptClientDoc.DataDefController.DataDefinition.FormulaFields)
                {
                    if (formulaObject.Name.ToUpper().StartsWith("F"))
                    {
                        String DataFieldName = formulaObject.Name.Substring(1).ToUpper();
                        switch (DataFieldName)
                        {
                            case "KTT":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.TenKtt + "\"";
                                break;
                            case "THUQUY":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.TenThuQuy + "\"";
                                break;
                            case "GIAMDOC":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.TenGiamDoc + "\"";
                                break;
                            case "TENDONVI":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.TenDonVi.ToUpper() + "\"";
                                break;
                            case "DIACHIDONVI":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.DiaChi + "\"";
                                break;
                            case "QUYETDINHSO":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.MauBaoCaoNoiDung + "\"";
                                break;
                            case "NGAY":
                                switch (LoaiManHinh)
                                {
                                    case cenCommon.LoaiManHinh.IDDonHang:
                                    case cenCommon.LoaiManHinh.IDTamUng:
                                    case cenCommon.LoaiManHinh.IDThanhToanTamUng:
                                        if (drChungTu.Table.Columns.Contains("NgayVanChuyen"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayVanChuyen"]).Day + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayTamUng"]).Day + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayThanhToanTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayThanhToanTamUng"]).Day + "\"";
                                        break;
                                    default:
                                        rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayLap"]).Day + "\"";
                                        break;
                                }
                                break;
                            case "THANG":
                                switch (LoaiManHinh)
                                {
                                    case cenCommon.LoaiManHinh.IDDonHang:
                                    case cenCommon.LoaiManHinh.IDTamUng:
                                    case cenCommon.LoaiManHinh.IDThanhToanTamUng:
                                        if (drChungTu.Table.Columns.Contains("NgayVanChuyen"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayVanChuyen"]).Month + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayTamUng"]).Month + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayThanhToanTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayThanhToanTamUng"]).Month + "\"";
                                        break;
                                    default:
                                        rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayLap"]).Month + "\"";
                                        break;
                                }
                                break;
                            case "NAM4":
                                switch (LoaiManHinh)
                                {
                                    case cenCommon.LoaiManHinh.IDDonHang:
                                    case cenCommon.LoaiManHinh.IDTamUng:
                                    case cenCommon.LoaiManHinh.IDThanhToanTamUng:
                                        if (drChungTu.Table.Columns.Contains("NgayVanChuyen"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayVanChuyen"]).Year + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayTamUng"]).Year + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayThanhToanTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayThanhToanTamUng"]).Year + "\"";
                                        break;
                                    default:
                                        rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayLap"]).Year + "\"";
                                        break;
                                }
                                break;
                            case "NAM2":
                                switch (LoaiManHinh)
                                {
                                    case cenCommon.LoaiManHinh.IDDonHang:
                                        if (drChungTu.Table.Columns.Contains("NgayVanChuyen"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayVanChuyen"]).Year.ToString("0#") + "\"";
                                        if (drChungTu.Table.Columns.Contains("NgayTamUng"))
                                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayTamUng"]).Year.ToString("0#") + "\"";
                                        break;
                                    default:
                                        rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDateTime(drChungTu["NgayLap"]).Year.ToString("0#") + "\"";
                                        break;
                                }
                                break;
                            case "TenDanhMucHangTauHoaDon":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + drChungTu["TenDanhMucHangTauHoaDon"].ToString().ToUpper() + "\"";
                                break;
                            case "TENDanhMucHangTau":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + drChungTu["TenDanhMucHangTau"].ToString().ToUpper() + "\"";
                                break;
                            case "TENNGUOISUDUNG":
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.GlobalVariables.TenDanhMucNguoiSuDung + "\"";
                                break;
                            case "TIENBANGCHU":
                                if (drChungTu.Table.Columns.Contains("SoTienTamUng") && drChungTu["SoTienTamUng"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(drChungTu["SoTienTamUng"].ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("SoTien") && drChungTu["SoTien"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(drChungTu["SoTien"].ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("TongTien") && drChungTu["TongTien"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(drChungTu["TongTien"].ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("ThanhTien") && drChungTu["ThanhTien"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(drChungTu["ThanhTien"].ToString()) + " đồng." + "\"";
                                break;
                            case "SOTIENBANGCHU":
                                if (drChungTu.Table.Columns.Contains("SoTienHoanTamUng") && drChungTu["SoTienHoanTamUng"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(Math.Round(Double.Parse(drChungTu["SoTienHoanTamUng"].ToString()), 0).ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("SoTienTamUng") && drChungTu["SoTienTamUng"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(Math.Round(Double.Parse(drChungTu["SoTienTamUng"].ToString()), 0).ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("SoTien") && drChungTu["SoTien"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(Math.Round(Double.Parse(drChungTu["SoTien"].ToString()), 0).ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("TongSoTienThucTe") && drChungTu["TongSoTienThucTe"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(Math.Round(Double.Parse(drChungTu["TongSoTienThucTe"].ToString()), 0).ToString()) + " đồng." + "\"";
                                if (drChungTu.Table.Columns.Contains("TongSoTienThanhToan") && drChungTu["TongSoTienThanhToan"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(Math.Round(Double.Parse(drChungTu["TongSoTienThanhToan"].ToString()), 0).ToString()) + " đồng." + "\"";
                                //if (drChungTu.Table.Columns.Contains("TongCongNt") && drChungTu["TongCongNt"] != DBNull.Value)
                                //    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + clsDataAccessLayer.DocTienVIE(Convert.ToInt32(drChungTu["TongCongNt"])) + "\"";
                                break;
                            case "SOLUONGBANGCHU":
                                if (drChungTu.Table.Columns.Contains("SoLuong") && drChungTu["SoLuong"] != DBNull.Value)
                                    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + cenCommon.cenCommon.SoTienBangChu(drChungTu["SoLuong"].ToString()) + "\"";
                                //if (drChungTu.Table.Columns.Contains("TongCongNt") && drChungTu["TongCongNt"] != DBNull.Value)
                                //    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + clsDataAccessLayer.DocTienVIE(Convert.ToInt32(drChungTu["TongCongNt"])) + "\"";
                                break;
                            //case "NGAYHACHTOAN":
                            //    if (drChungTu.Table.Columns.Contains(DataFieldName) && drChungTu[DataFieldName] != DBNull.Value)
                            //        rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + "Ngày " + Convert.ToDateTime(drChungTu[DataFieldName]).Day.ToString() + " tháng " + Convert.ToDateTime(drChungTu[DataFieldName]).Month.ToString() + " năm " + Convert.ToDateTime(drChungTu[DataFieldName]).Year.ToString() + "\"";
                            //    break;
                            //case "TIENNT":
                            //case "TONGCONGNT":
                            //    if (drChungTu.Table.Columns.Contains(DataFieldName) && drChungTu[DataFieldName] != DBNull.Value)
                            //        rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + Convert.ToDecimal(drChungTu[DataFieldName]).ToString("n2", cenCommon.GlobalVariables.ci) + "\"";
                            //    break;
                            case "TIEN":
                            case "SOTIEN":
                            case "SOTIENTAMUNG":
                            case "SOTIENHAIQUAN":
                            case "SOTIENNANGHA":
                            case "SOTIENCUOCVO":
                            case "SOTIENCHICUOCVO":
                            case "SOTIENCHITHUTUC":
                            case "SOTIENVATCHITHUTUC":
                            case "SOTIENCHIKHAC":
                            case "SOTIENHOANTAMUNG":
                            case "TONGSOTIENTAMUNG":
                            case "TONGSOTIENCHITHUTUC":
                            case "TONGSOTIENCHICUOCVO":
                            case "TONGSOTIENCHITHUTUCTOTAL":
                            case "TONGSOTIENCHITRAHOCOHOADON":
                            case "TONGSOTIENCHI":
                            case "TONGSOTIENCHENHLECH":
                            case "TONGSOTIENTHANHTOAN":
                                if (!drChungTu.Table.Columns.Contains(DataFieldName) || drChungTu[DataFieldName] == DBNull.Value) break;
                                NumberFormatString = "n" + cenCommon.GlobalVariables.LamTronSoTien;
                                FormattedValue = Convert.ToDecimal(drChungTu[DataFieldName]).ToString(NumberFormatString, cenCommon.GlobalVariables.ci);
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + FormattedValue + "\"";
                                break;
                            //case "TIEUDE":
                            //    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + TieuDe.ToUpper() + "\"";
                            //    break;
                            default:

                                //Lấy giá trị từ DataSet
                                if (!drChungTu.Table.Columns.Contains(DataFieldName) || drChungTu[DataFieldName] == DBNull.Value) break;
                                FormattedValue = drChungTu[DataFieldName].ToString();
                                //else //Nếu không thì tìm trong bảng Chứng từ tham số giá trị
                                //{
                                //    if (dtChungTuThamSoGiaTri != null)
                                //    {
                                //        foreach (DataRow drChungTuThamSoGiaTri in dtChungTuThamSoGiaTri.Rows)
                                //        {
                                //            if (drChungTuThamSoGiaTri["TenBien"].ToString().ToUpper() == formulaObject.Name.ToUpper())
                                //            {
                                //                FormattedValue = "\"" + drChungTuThamSoGiaTri["GiaTriThamSo"].ToString() + "\"";
                                //                break;
                                //            }
                                //        }
                                //    }
                                //}
                                //Định dạng dữ liệu
                                //if (cenCommon.GlobalVariables.TenCotNgayThang.ToUpper().Contains("(" + DataFieldName.ToUpper() + ")")) //Định dạng ngày tháng
                                //    FormattedValue = Convert.ToDateTime(drChungTu[DataFieldName]).ToString(cenCommon.GlobalVariables.FormatNgayThangNam);
                                //if (cenCommon.GlobalVariables.TenCotThoiGian.ToUpper().Contains("(" + DataFieldName.ToUpper() + ")")) //Định dạng ngày tháng
                                //    FormattedValue = Convert.ToDateTime(drChungTu[DataFieldName]).ToString(cenCommon.GlobalVariables.FormatThoiGian);
                                if (DataFieldName.ToUpper().StartsWith("NGAY") || DataFieldName.ToUpper().StartsWith("THOIHAN")) //Định dạng ngày tháng
                                    FormattedValue = Convert.ToDateTime(drChungTu[DataFieldName]).ToString(cenCommon.GlobalVariables.FormatNgayThangNam);
                                if (DataFieldName.ToUpper() == "SOLUONG" || DataFieldName.ToUpper() == "TRONGLUONG" || DataFieldName.ToUpper() == "KHOILUONG" || DataFieldName.ToUpper() == "SOLUONGHANG" || DataFieldName.ToUpper() == "TRONGLUONGHANG" || DataFieldName.ToUpper() == "KHOILUONGHANG") //Định dạng số lượng
                                {
                                    NumberFormatString = "n3";
                                    FormattedValue = Convert.ToDecimal(drChungTu[DataFieldName]).ToString(NumberFormatString, cenCommon.GlobalVariables.ci);
                                }
                                //if (cenCommon.GlobalVariables.sMaThamSoTenCotGiaTienNgoaiTe.Contains(DataFieldName)) //Định dạng giá tiền ngoại tệ
                                //{
                                //    NumberFormatString = "n" + cenCommon.GlobalVariables.LamTronGiaTienNgoaiTe;
                                //    FormattedValue = Convert.ToDecimal(drChungTu[DataFieldName]).ToString(NumberFormatString, cenCommon.GlobalVariables.ci);
                                //}
                                //if (cenCommon.GlobalVariables.sMaThamSoTenCotGiaTienHachToan.Contains(DataFieldName)) //Định dạng giá tiền hạch toán
                                //{
                                //    NumberFormatString = "n" + cenCommon.GlobalVariables.LamTronGiaTienHachToan;
                                //    FormattedValue = Convert.ToDecimal(drChungTu[DataFieldName]).ToString(NumberFormatString, cenCommon.GlobalVariables.ci);
                                //}
                                //if (cenCommon.GlobalVariables.sMaThamSoTenCotTienNgoaiTe.Contains(DataFieldName)) //Định dạng tiền ngoại tệ
                                //{
                                //    NumberFormatString = "n" + cenCommon.GlobalVariables.LamTronTienNgoaiTe;
                                //    FormattedValue = Convert.ToDecimal(drChungTu[DataFieldName]).ToString(NumberFormatString, cenCommon.GlobalVariables.ci);
                                //}

                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "\"" + FormattedValue + "\"";
                                break;
                        }
                    }
                }
                //Xử lý in chi tiết chứng từ, chỉ dùng cho những chứng từ có phần chi tiết
                if (dtChungTuDetail != null && dtChungTuDetail.Rows.Count > 0)
                {
                    //Lưu chi tiết chứng từ ra XML
                    ChungTuDetailDataXMLPath = cenCommon.GlobalVariables.TempDir + "\\" + Guid.NewGuid().ToString() + ".xml";
                    dtChungTuDetail.TableName = "ChungTuChiTiet";
                    System.Data.DataSet dsChungTuDetail = new System.Data.DataSet();
                    dsChungTuDetail.Tables.Add(dtChungTuDetail);
                    dsChungTuDetail.WriteXml(ChungTuDetailDataXMLPath);
                    //Gán DataSource cho file report
                    AddDataSourceUsingSchemaFile(rpt.ReportClientDocument, ChungTuDetailDataXMLPath, "ChungTuChiTiet", dsChungTuDetail, false);
                    //Gán dataField cho các formula
                    foreach (FormulaField formulaObject in rptClientDoc.DataDefController.DataDefinition.FormulaFields)
                    {
                        if (formulaObject.Name.ToUpper().StartsWith("DT") && dtChungTuDetail.Columns.Contains(formulaObject.Name.Substring(2)))
                        {
                            String DataType = dtChungTuDetail.Columns[formulaObject.Name.Substring(2)].DataType.ToString().ToUpper();
                            String ColumnName = formulaObject.Name.Substring(2);
                            String DataFieldName = "{" + dtChungTuDetail.TableName + "." + ColumnName + "}";
                            rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = DataFieldName;
                            ////Định dạng dữ liệu
                            //if (cenCommon.GlobalVariables.sMaThamSoTenCotNgayThang.Contains(ColumnName.ToUpper())) //Định dạng ngày tháng
                            //    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "ToText(" + DataFieldName + "), \"" + cenCommon.GlobalVariables.FormatNgayThang + "\")";

                            if (ColumnName.ToUpper() == "SOLUONG") //Định dạng số lượng
                            {
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + "0" + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            }

                            if (ColumnName.ToUpper() == "TRONGLUONG" || ColumnName.ToUpper() == "KHOILUONG") //Định dạng số lượng
                            {
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + "3" + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            }
                            //if (ColumnName.ToUpper()=="DONGIA") //Định dạng giá tiền ngoại tệ
                            //{
                            //    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + cenCommon.GlobalVariables.LamTronGiaTienNgoaiTe + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            //}
                            if (ColumnName.ToUpper() == "DONGIA" || ColumnName.ToUpper() == "DONGIAHIENTHI") //Định dạng giá tiền hạch toán
                            {
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + "2" + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            }
                            //if (cenCommon.GlobalVariables.sMaThamSoTenCotTienNgoaiTe.Contains(ColumnName.ToUpper())) //Định dạng tiền ngoại tệ
                            //{
                            //    rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + cenCommon.GlobalVariables.LamTronTienNgoaiTe + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            //}
                            if (ColumnName.ToUpper() == "STT" || ColumnName.ToUpper() == "TIEN" || ColumnName.ToUpper() == "TIENHANG" || ColumnName.ToUpper() == "TIENHANGHIENTHI") //Định dạng tiền hạch toán
                            {
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + "0" + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            }
                            if (ColumnName.ToUpper().StartsWith("SOTIEN")) //Định dạng tiền hạch toán
                            {
                                rpt.DataDefinition.FormulaFields[formulaObject.Name].Text = "IIF(" + DataFieldName + "=0,\"-\",ToText(" + DataFieldName + ", " + "0" + ", \"" + cenCommon.GlobalVariables.DigitSymbol + "\", \"" + cenCommon.GlobalVariables.DecimalSymbol + "\"))";
                            }
                        }
                    }
                    dsChungTuChiTiet = new System.Data.DataSet();
                    dsChungTuChiTiet = dsChungTuDetail;
                    XMLDataFile = ChungTuDetailDataXMLPath;
                }
                rptClientDoc.Save();
                if (PrintPreview)
                {
                    rpt.PrintOptions.PrinterName = PrinterName;
                    rpt.PrintToPrinter(1, false, 1, 1);
                }
                rpt.Close();
                rpt.Dispose();
                OK = true;
            }
            return OK;
        }
        public static void AddDataSourceUsingSchemaFile(
        CrystalDecisions.ReportAppServer.ClientDoc.ISCDReportClientDocument rcDoc,		// report client document 
        string schema_file_name,		// xml schema file location 
        string table_name,				// table to be added 
        System.Data.DataSet data, Boolean Added)        // dataset 
        {
            PropertyBag crLogonInfo;            // logon info 
            PropertyBag crAttributes;           // logon attributes 
            CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo crConnectionInfo;  // connection info 
            CrystalDecisions.ReportAppServer.DataDefModel.Table crTable;
            // database table 
            // create logon property 
            crLogonInfo = new PropertyBag();
            crLogonInfo["XML File Path"] = schema_file_name;
            // create logon attributes 
            crAttributes = new PropertyBag();
            crAttributes["Database DLL"] = "crdb_adoplus.dll";
            crAttributes["QE_DatabaseType"] = "ADO.NET (XML)";
            crAttributes["QE_ServerDescription"] = "NewDataSet";
            crAttributes["QE_SQLDB"] = true;
            crAttributes["QE_LogonProperties"] = crLogonInfo;
            // create connection info 
            crConnectionInfo = new CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo
            {
                Kind = CrConnectionInfoKindEnum.crConnectionInfoKindCRQE,
                Attributes = crAttributes
            };
            // create a table 
            crTable = new CrystalDecisions.ReportAppServer.DataDefModel.Table
            {
                ConnectionInfo = crConnectionInfo,
                Name = table_name,
                Alias = table_name
            };
            // add a table 
            if (!Added)
                rcDoc.DatabaseController.AddTable(crTable, null);
            // pass dataset 
            rcDoc.DatabaseController.SetDataSource(DataSetConverter.Convert(data), table_name, table_name);
        }
    }
}

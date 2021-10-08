﻿using cenDAO;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

//Class chứa các function & method liên quan tới việc xử lý báo cáo
namespace cenReportController
{
    public class Report
    {
        public static void runReport(string reportProcedureName, string TenDanhMucBaoCao, Form MDIParent)
        {
            //Lấy parameter
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add(new DataColumn("TenThamSo", typeof(string))); //Tên tham số trong procedure
            dtParameters.Columns.Add(new DataColumn("DienGiaiThamSo", typeof(string))); //Tên tham số tiếng Việt
            dtParameters.Columns.Add(new DataColumn("GiaTri", typeof(string))); //Giá trị tham số do người dùng nhập
            dtParameters.Columns.Add(new DataColumn("GiaTriThamSo", typeof(string))); //Giá trị tham số truyền vào procedure
            dtParameters.Columns.Add(new DataColumn("KieuDuLieu", typeof(string))); //Kiểu dữ liệu
            dtParameters.Columns.Add(new DataColumn("GhiChu", typeof(string)));
            SqlParameterCollection sqlParameterCollection;
            using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = reportProcedureName,
                    CommandType = CommandType.StoredProcedure
                };
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                sqlParameterCollection = sqlCommand.Parameters;
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    if (sqlParameter.ParameterName != "@RETURN_VALUE" && sqlParameter.ParameterName != "@IDDanhMucDonVi")
                    {
                        DataRow drParameter = dtParameters.NewRow();
                        drParameter["TenThamSo"] = sqlParameter.ParameterName;
                        drParameter["DienGiaiThamSo"] = cenCommon.cenCommon.TraTuDien(sqlParameter.ParameterName.Substring(1));
                        drParameter["KieuDuLieu"] = sqlParameter.SqlDbType.ToString().ToUpper();
                        if (sqlParameter.SqlDbType.ToString().ToUpper() == "DATE")
                        {
                            if (drParameter["TenThamSo"].ToString().StartsWith("@TuNgay"))
                            {
                                DateTime TuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                drParameter["GiaTri"] = TuNgay.Day.ToString("0#") + "/" + TuNgay.Month.ToString("0#") + "/" + TuNgay.Year;
                            }
                            else if (drParameter["TenThamSo"].ToString().StartsWith("@DenNgay"))
                            {
                                DateTime DenNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                                drParameter["GiaTri"] = DenNgay.Day.ToString("0#") + "/" + DenNgay.Month.ToString("0#") + "/" + DenNgay.Year;
                            }
                            else
                            {
                                DateTime Ngay = DateTime.Now;
                                drParameter["GiaTri"] = Ngay.Day.ToString("0#") + "/" + Ngay.Month.ToString("0#") + "/" + Ngay.Year;
                            }
                            drParameter["GiaTriThamSo"] = drParameter["GiaTri"];
                            drParameter["GhiChu"] = "Nhập theo định dạng DD/MM/YYYY";

                        }
                        dtParameters.Rows.Add(drParameter);
                    }
                }
            }
            Boolean OK = true;
            DataTable dtData;
            string ChuoiThamSoHienThi = "";
            if (dtParameters.Rows.Count > 0)
            {
                frmReportParameters frmReportParameters = new frmReportParameters()
                {
                    Text = TenDanhMucBaoCao,
                    dtParameters = dtParameters
                };
                frmReportParameters.ShowDialog();
                OK = frmReportParameters.OK;
                if (OK)
                {
                    dtParameters = frmReportParameters.dtParameters;
                    ChuoiThamSoHienThi = frmReportParameters.ChuoiThamSoHienThiGrid;
                }
                frmReportParameters.Dispose();
            }
            if (!OK) return;

            SqlParameter[] sqlParameters = new SqlParameter[dtParameters.Rows.Count + 1];
            sqlParameters[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            if (dtParameters.Rows.Count > 0)
            {
                for (int i = 0; i <= dtParameters.Rows.Count - 1; i++)
                {
                    sqlParameters[i + 1] = new SqlParameter(dtParameters.Rows[i]["TenThamSo"].ToString(), dtParameters.Rows[i]["GiaTriThamSo"]);
                }
            }
            cenDAO.ConnectionDAO connectionDAO = new cenDAO.ConnectionDAO();
            dtData = connectionDAO.tableList(sqlParameters, reportProcedureName, reportProcedureName);
            frmReportViewer frmReportViewer = new frmReportViewer();
            frmReportViewer.Text = TenDanhMucBaoCao;
            frmReportViewer.dtData = dtData;
            frmReportViewer.dtParameters = dtParameters;
            frmReportViewer.reportProcedureName = reportProcedureName;
            frmReportViewer.ChuoiThamSoHienThiGrid = ChuoiThamSoHienThi;
            frmReportViewer.MDIParent = MDIParent;
            frmReportViewer.Show();
        }
    }
}

using cenControls;
using cenDAO;
using cenReportController;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace cenCommonUIbase
{
    public static class cenCommonUIbase
    {
        public static void SetDisplayLanguage(Control ctl)
        {

            foreach (Control iCtl in ctl.Controls)
            {
                if (iCtl is saLabel | iCtl is saCheckBox | iCtl is Infragistics.Win.Misc.UltraLabel)
                    iCtl.Text = cenCommon.cenCommon.TraTuDien(iCtl.Text);
                if (iCtl is saDateTimeBox)
                    ((saDateTimeBox)iCtl).FormatProvider = cenCommon.GlobalVariables.ci;
                if (iCtl is saNumericBox)
                {
                    ((saNumericBox)iCtl).FormatProvider = cenCommon.GlobalVariables.ci;
                    saNumericBox numBox = (saNumericBox)iCtl;
                    //Định dạng các cột số lượng
                    if (cenCommon.GlobalVariables.TenCotSoLuong.ToUpper().IndexOf("(" + numBox.Name.ToUpper().Substring(3) + ")") >= 0)
                    {
                        numBox.MaskInput = cenCommon.GlobalVariables.DinhDangNhapSoLuong;
                    }
                    //Định dạng các cột trọng lượng
                    if (cenCommon.GlobalVariables.TenCotTrongLuong.ToUpper().IndexOf("(" + numBox.Name.ToUpper().Substring(3) + ")") >= 0)
                    {
                        numBox.MaskInput = cenCommon.GlobalVariables.DinhDangNhapTrongLuong;
                    }
                    //Định dạng các cột khối lượng
                    if (cenCommon.GlobalVariables.TenCotKhoiLuong.ToUpper().IndexOf("(" + numBox.Name.ToUpper().Substring(3) + ")") >= 0)
                    {
                        numBox.MaskInput = cenCommon.GlobalVariables.DinhDangNhapKhoiLuong;
                    }
                    //Định dạng các cột đơn giá
                    if (cenCommon.GlobalVariables.TenCotGia.ToUpper().IndexOf("(" + numBox.Name.ToUpper().Substring(3) + ")") >= 0)
                    {
                        numBox.MaskInput = cenCommon.GlobalVariables.DinhDangNhapGia;
                    }
                    //Định dạng các cột số tiền
                    if (cenCommon.GlobalVariables.TenCotTien.ToUpper().IndexOf("(" + numBox.Name.ToUpper().Substring(3) + ")") >= 0)
                    {
                        numBox.MaskInput = cenCommon.GlobalVariables.DinhDangNhapTien;
                    }
                }
                if (iCtl is Infragistics.Win.UltraWinTabControl.UltraTabControl)
                    foreach (Infragistics.Win.UltraWinTabControl.UltraTab ut in ((Infragistics.Win.UltraWinTabControl.UltraTabControl)iCtl).Tabs)
                    {
                        ut.Text = cenCommon.cenCommon.TraTuDien(ut.Text);
                    }
                if (iCtl.Controls.Count > 0)
                    SetDisplayLanguage(iCtl);
            }
        }
        public static Form CheckFormOpened(String frmName)
        {
            Form frmOpened = null;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name.ToUpper() == frmName.ToUpper())
                {
                    frmOpened = frm;
                    break;
                }
            }
            return frmOpened;
        }
    }
}

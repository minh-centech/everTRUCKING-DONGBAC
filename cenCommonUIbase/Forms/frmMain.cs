using cenBase.Forms;
using cenBUS.DatabaseCore;
using cenCommonUIapps.DatabaseCore.Forms;
using cenDTO.DatabaseCore;
using Infragistics.Win.UltraWinToolbars;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace cenCommonUIbase.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Load file giao diện từ app.config
            if (File.Exists(Application.StartupPath + "\\Office2007Blue.isl"))
            {
                Infragistics.Win.AppStyling.StyleManager.Load(Application.StartupPath + "\\Office2007Blue.isl");
            }
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
            if (!frmLogin.OK)
            {
                Application.Exit();
                return;
            }
            cenCommon.GlobalVariables.Logged = true;
            if (cenCommon.GlobalVariables.Logged)
            {
                this.Text = "Phần mềm quản lý everTrucking - " + cenCommon.GlobalVariables.TenDonVi + "-Bản quyền: Công ty cổ phần Everlink";
                //Tạo folder output nếu chưa có
                if (!Directory.Exists(cenCommon.GlobalVariables.OutputDir))
                    Directory.CreateDirectory(cenCommon.GlobalVariables.OutputDir);
                //Disable menu quản trị nếu không phải người dùng quản trị
                PopupMenuTool mnuHeThong = (PopupMenuTool)this.ultraToolbarsManager1.Toolbars[0].Tools["mnuHeThong"];
                mnuHeThong.Tools["_sys_DanhMucQuanTri"].SharedProps.Enabled = cenCommon.GlobalVariables.isAdmin;
                //Nạp danh mục từ điển
                cenCommon.GlobalVariables.dtTuDien = new DataTable();
                DanhMucTuDienBUS TuDienBUS = new DanhMucTuDienBUS();
                cenCommon.GlobalVariables.dtTuDien = TuDienBUS.List(null);
                LoadMenu();
                //InitParameters();
                frmDesktop frmDesktop = new frmDesktop
                {
                    MdiParent = this
                };
                frmDesktop.Show();
                //Hiển thị trạng thái
                this.ultraStatusBar1.Panels["sttTenDuLieu"].Text = "Dữ liệu: " + cenCommon.GlobalVariables.TenDuLieu;
                this.ultraStatusBar1.Panels["sttTenDonVi"].Text = "Đơn vị: " + cenCommon.GlobalVariables.TenDonVi;
                this.ultraStatusBar1.Panels["sttTenDanhMucNguoiSuDung"].Text = "User: " + cenCommon.GlobalVariables.MaDanhMucNguoiSuDung;
            }
        }
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            String LoaiChucNang = e.Tool.Key.ToUpper(), //e.Tool.Key.Substring(0, 5).ToUpper(),
                    TenChucNang = e.Tool.Key.Substring(5),
                    FormCaption = e.Tool.SharedProps.Caption;

            switch (LoaiChucNang)
            {
                case "_SYS_DANHMUCDONVI": //Các chức năng quản trị, hệ thống
                    frmDanhMucDonVi frmDanhMucDonVi = new frmDanhMucDonVi
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucDonVi.Show();
                    break;
                case "_SYS_DANHMUCCHUNGTU": //Các chức năng quản trị, hệ thống
                    frmDanhMucChungTu frmDanhMucChungTu = new frmDanhMucChungTu
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucChungTu.Show();
                    break;
                case "_SYS_DANHMUCLOAIDOITUONG": //Các chức năng quản trị, hệ thống
                    frmDanhMucLoaiDoiTuong frmDanhMucLoaiDoiTuong = new frmDanhMucLoaiDoiTuong
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucLoaiDoiTuong.Show();
                    break;
                case "_SYS_DANHMUCNHOMBAOCAO": //Các chức năng quản trị, hệ thống
                    frmDanhMucNhomBaoCao frmDanhMucNhomBaoCao = new frmDanhMucNhomBaoCao
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucNhomBaoCao.Show();
                    break;
                case "_SYS_DANHMUCBAOCAO": //Các chức năng quản trị, hệ thống
                    frmDanhMucBaoCao frmDanhMucBaoCao = new frmDanhMucBaoCao
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucBaoCao.Show();
                    break;
                case "_SYS_DANHMUCPHANQUYEN": //Các chức năng quản trị, hệ thống
                    frmDanhMucPhanQuyen frmDanhMucPhanQuyen = new frmDanhMucPhanQuyen
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucPhanQuyen.Show();
                    break;
                case "_SYS_DANHMUCMENU": //Các chức năng quản trị, hệ thống
                    frmDanhMucMenu frmDanhMucMenu = new frmDanhMucMenu
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucMenu.Show();
                    break;
                case "_SYS_DANHMUCTUDIEN": //Các chức năng quản trị, hệ thống
                    frmDanhMucTuDien frmDanhMucTuDien = new frmDanhMucTuDien
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucTuDien.Show();
                    break;
                case "_SYS_DANHMUCTHAMSOHETHONG": //Các chức năng quản trị, hệ thống
                    frmDanhMucThamSoHeThong frmDanhMucThamSoHeThong = new frmDanhMucThamSoHeThong
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucThamSoHeThong.Show();
                    break;
                case "_SYS_DANHMUCNGUOISUDUNG": //Các chức năng quản trị, hệ thống
                    frmDanhMucNguoiSuDung frmDanhMucNguoiSuDung = new frmDanhMucNguoiSuDung
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucNguoiSuDung.Show();
                    break;
                case "_SYS_DANHMUCTHAMSONGUOISUDUNG": //Các chức năng quản trị, hệ thống
                    frmDanhMucThamSoNguoiSuDung frmDanhMucThamSoNguoiSuDung = new frmDanhMucThamSoNguoiSuDung
                    {
                        Text = cenCommon.cenCommon.TraTuDien(TenChucNang),
                        MdiParent = this
                    };
                    frmDanhMucThamSoNguoiSuDung.Show();
                    break;
                case "_SYS_THAYDOIMATKHAU": //Các chức năng quản trị, hệ thống
                    frmChangePassword frmChangePassword = new frmChangePassword();
                    frmChangePassword.ShowDialog();
                    break;
                case "_SYS_NHATKYDULIEU": //Các chức năng quản trị, hệ thống
                    frmNhatKyDuLieu frmNhatKyDuLieu = new frmNhatKyDuLieu()
                    {
                        Text = "Nhật ký dữ liệu",
                        MdiParent = this
                    };
                    frmNhatKyDuLieu.Show();
                    break;
                case "_SYS_DANGXUAT": //Đăng xuất
                    Application.Exit();
                    Application.Restart();
                    break;
                case "_SYS_THOAT": //Các chức năng quản trị, hệ thống
                    Application.Exit();
                    break;
                default:
                    if (LoaiChucNang.StartsWith("_DDM_"))
                    {
                        String IDDanhMucLoaiDoiTuong = TenChucNang.Substring(0, TenChucNang.IndexOf("ID:"));
                        cenCommonUIapps.cenCommonUIapps.runDanhMuc(FormCaption, IDDanhMucLoaiDoiTuong, this);
                    }
                    else if (LoaiChucNang.StartsWith("_DCT_"))
                    {
                        String IDDanhMucChungTu = TenChucNang.Substring(0, TenChucNang.IndexOf("ID:"));
                        String MaDanhMucChungTu = TenChucNang.Substring(TenChucNang.IndexOf("MA:") + "MA:".Length, TenChucNang.IndexOf("LOAIMANHINH:") - TenChucNang.IndexOf("MA:") - "MA:".Length);
                        String LoaiManHinh = TenChucNang.Substring(TenChucNang.IndexOf("LOAIMANHINH:") + "LOAIMANHINH:".Length, TenChucNang.Length - TenChucNang.IndexOf("LOAIMANHINH:") - "LOAIMANHINH:".Length);
                        cenCommonUIapps.cenCommonUIapps.runChungTu(FormCaption, LoaiManHinh, IDDanhMucChungTu, this);
                    }
                    else if (LoaiChucNang.StartsWith("_DBC_"))
                    {
                        String IDDanhMucBaoCao = TenChucNang.Substring(0, TenChucNang.IndexOf("ID:"));
                        cenCommonUIapps.cenCommonUIapps.runBaoCao(IDDanhMucBaoCao, this);
                    }
                    break;
                    //case "MNUBAOCAOCHITIETXERAVAO":
                    //    frmReportParameters_BaoCaoChiTietXeRaVao frmReportParameters_BaoCaoChiTietXeRaVao = new frmReportParameters_BaoCaoChiTietXeRaVao();
                    //    //frmReportParameters_BaoCaoChiTietXeRaVao.MdiParent = this;
                    //    frmReportParameters_BaoCaoChiTietXeRaVao.ShowDialog();
                    //    if (frmReportParameters_BaoCaoChiTietXeRaVao.OK)
                    //    {
                    //        frmReportViewer_BaoCaoChiTietXeRaVao frm = new frmReportViewer_BaoCaoChiTietXeRaVao
                    //        {
                    //            MdiParent = this,
                    //            Bai = frmReportParameters_BaoCaoChiTietXeRaVao.cboBai.Value.ToString(),
                    //            TuNgay = frmReportParameters_BaoCaoChiTietXeRaVao.txtTuNgay.DateTime.Year.ToString() + "-" + frmReportParameters_BaoCaoChiTietXeRaVao.txtTuNgay.DateTime.Month.ToString() + "-" + frmReportParameters_BaoCaoChiTietXeRaVao.txtTuNgay.DateTime.Day.ToString(),
                    //            DenNgay = frmReportParameters_BaoCaoChiTietXeRaVao.txtDenNgay.DateTime.Year.ToString() + "-" + frmReportParameters_BaoCaoChiTietXeRaVao.txtDenNgay.DateTime.Month.ToString() + "-" + frmReportParameters_BaoCaoChiTietXeRaVao.txtDenNgay.DateTime.Day.ToString()
                    //        };
                    //        frm.Show();
                    //    }
                    //    break;
            }
        }
        public void LoadMenu()
        {
            Boolean CoChungTu = false;

            DanhMucMenuBUS menuBUS = new DanhMucMenuBUS();
            DataSet dsMenu = menuBUS.List(null);

            if (dsMenu == null) return;

            //Load menu các phân hệ
            foreach (DataRow drMenu in dsMenu.Tables[DanhMucMenu.tableName].Rows)
            {
                Int32 OrderNo = 1;
                CoChungTu = false;
                PopupMenuTool pmtPhanHe = new PopupMenuTool("_div_" + drMenu["ID"].ToString());
                pmtPhanHe.SharedProps.Caption = drMenu["Ten"].ToString();
                pmtPhanHe.SharedProps.DisplayStyle = ToolDisplayStyle.ImageAndText;
                //pmtPhanHe.SharedProps.AppearancesSmall.Appearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drMenu["Anh"]);
                //pmtPhanHe.SharedProps.AppearancesLarge.Appearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drMenu["Anh"]);
                this.ultraToolbarsManager1.Tools.Add(pmtPhanHe);
                this.ultraToolbarsManager1.Toolbars[0].Tools.AddTool(pmtPhanHe.Key);
                OrderNo += 1;
                //Add các button là chứng từ
                foreach (DataRow drChungTu in dsMenu.Tables[DanhMucMenuChungTu.tableName].Rows)
                {
                    if (drChungTu["IDDanhMucMenu"].ToString() == drMenu["ID"].ToString())
                    {
                        ButtonTool btChungTu = new ButtonTool("_dct_" + drChungTu["IDDanhMucChungTu"].ToString() + "ID:" + drChungTu["ID"].ToString() + "MA:" + drChungTu["MaDanhMucChungTu"].ToString() + "LOAIMANHINH:" + drChungTu["LoaiManHinh"].ToString());
                        //ButtonTool btChungTu = new ButtonTool("_dct_" + drChungTu["ID"].ToString() + "_IDDanhMuc" + drChungTu["IDDanhMucChungTu"].ToString());
                        //if (drChungTu["Anh"] != DBNull.Value)
                        //    btChungTu.SharedProps.AppearancesSmall.Appearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drChungTu["Anh"]);
                        btChungTu.SharedProps.Caption = drChungTu["NoiDungHienThi"].ToString();
                        this.ultraToolbarsManager1.Tools.Add(btChungTu);
                        pmtPhanHe.Tools.AddTool(btChungTu.Key);
                        CoChungTu = true;
                    }
                }
                //Add các button là danh mục
                Boolean CoDanhMuc = false;
                foreach (DataRow drDanhMuc in dsMenu.Tables[DanhMucMenuLoaiDoiTuong.tableName].Rows)
                {
                    if (drDanhMuc["IDDanhMucMenu"].ToString() == drMenu["ID"].ToString())
                    {
                        ButtonTool btDanhMuc = new ButtonTool("_ddm_" + drDanhMuc["IDDanhMucLoaiDoiTuong"].ToString() + "ID:" + drDanhMuc["ID"].ToString());
                        //if (drDanhMuc["Anh"] != DBNull.Value)
                        //    btDanhMuc.SharedProps.AppearancesSmall.Appearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drDanhMuc["Anh"]);
                        btDanhMuc.SharedProps.Caption = drDanhMuc["NoiDungHienThi"].ToString();
                        this.ultraToolbarsManager1.Tools.Add(btDanhMuc);
                        pmtPhanHe.Tools.AddTool(btDanhMuc.Key);
                        if (CoChungTu)
                        {
                            pmtPhanHe.Tools[btDanhMuc.Key].InstanceProps.IsFirstInGroup = true;
                            CoChungTu = false;
                        }
                        CoDanhMuc = true;
                    }
                }
                //Add các button là báo cáo
                foreach (DataRow drBaoCao in dsMenu.Tables[DanhMucMenuBaoCao.tableName].Rows)
                {

                    if (drBaoCao["IDDanhMucMenu"].ToString() == drMenu["ID"].ToString())
                    {
                        if (drBaoCao["IDDanhMucNhomBaoCao"] != DBNull.Value && !pmtPhanHe.Tools.Exists("_ref_" + drBaoCao["IDDanhMucNhomBaoCao"].ToString() + "_" + drMenu["ID"].ToString()))
                        {
                            //Nếu nhóm báo cáo khác null thì add group tương ứng với nhóm
                            PopupMenuTool pmtBaoCao = new PopupMenuTool("_ref_" + drBaoCao["IDDanhMucNhomBaoCao"].ToString() + "_" + drMenu["ID"].ToString());
                            pmtBaoCao.SharedProps.Caption = drBaoCao["TenDanhMucNhomBaoCao"].ToString();
                            this.ultraToolbarsManager1.Tools.Add(pmtBaoCao);
                            pmtPhanHe.Tools.AddTool(pmtBaoCao.Key);
                            if (CoDanhMuc || CoChungTu)
                            {
                                pmtPhanHe.Tools[pmtBaoCao.Key].InstanceProps.IsFirstInGroup = true;
                                CoDanhMuc = false;
                                CoChungTu = false;
                            }
                        }
                        ButtonTool btBaoCao = new ButtonTool("_dbc_" + drBaoCao["IDDanhMucBaoCao"].ToString() + "ID:" + drBaoCao["ID"].ToString());
                        btBaoCao.SharedProps.Caption = drBaoCao["TenDanhMucBaoCao"].ToString();
                        this.ultraToolbarsManager1.Tools.Add(btBaoCao);
                        PopupMenuTool ppBaoCao = (PopupMenuTool)pmtPhanHe.Tools["_ref_" + drBaoCao["IDDanhMucNhomBaoCao"].ToString() + "_" + drMenu["ID"].ToString()];
                        ppBaoCao.Tools.AddTool(btBaoCao.Key);
                    }
                }
            }
        }
        /// <summary>
        /// Load giá trị tham số hệ thống khi chạy chương trình
        /// </summary>
        public static void InitParameters()
        {
            //DanhMucBUS _DanhMucBUS = new DanhMucBUS();
            //cenCommon.GlobalVariables.TenCotNgayThang = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotNgayThang);
            //cenCommon.GlobalVariables.TenCotThoiGian = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotThoiGian);
            //cenCommon.GlobalVariables.TenCotSoLuong = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotSoLuong);
            //cenCommon.GlobalVariables.TenCotKhoiLuong = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotKhoiLuong);
            //cenCommon.GlobalVariables.TenCotTrongLuong = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotTrongLuong);
            //cenCommon.GlobalVariables.TenCotTien = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotSoTien);
            //cenCommon.GlobalVariables.TenCotGia = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotDonGia);
            //cenCommon.GlobalVariables.TenCotMaSo = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotMaSo);
            //cenCommon.GlobalVariables.TenCotDienGiai = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotDienGiai);
            //cenCommon.GlobalVariables.TenCotDropdown = _DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoTenCotDropdown);

            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotNgayThangGrid), out cenCommon.GlobalVariables.DoRongCotNgayThangGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotSoLuongGrid), out cenCommon.GlobalVariables.DoRongCotSoLuongGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotKhoiLuongGrid), out cenCommon.GlobalVariables.DoRongCotKhoiLuongGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotTrongLuongGrid), out cenCommon.GlobalVariables.DoRongCotTrongLuongGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotSoTienGrid), out cenCommon.GlobalVariables.DoRongCotTienGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotDonGiaGrid), out cenCommon.GlobalVariables.DoRongCotGiaGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotMaSoGrid), out cenCommon.GlobalVariables.DoRongCotMaSoGrid);
            //Int16.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotDienGiaiGrid), out cenCommon.GlobalVariables.DoRongCotDienGiaiGrid);

            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotNgayThangReport), out cenCommon.GlobalVariables.DoRongCotNgayThangReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotSoLuongReport), out cenCommon.GlobalVariables.DoRongCotSoLuongReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotKhoiLuongReport), out cenCommon.GlobalVariables.DoRongCotKhoiLuongReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotTrongLuongReport), out cenCommon.GlobalVariables.DoRongCotTrongLuongReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotSoTienReport), out cenCommon.GlobalVariables.DoRongCotTienReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotDonGiaReport), out cenCommon.GlobalVariables.DoRongCotGiaReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotMaSoReport), out cenCommon.GlobalVariables.DoRongCotMaSoReport);
            //Double.TryParse(_DanhMucBUS.GetGiaTriThamSoHeThong(cenCommon.ThamSoHeThong.MaThamSoDoRongCotDienGiaiReport), out cenCommon.GlobalVariables.DoRongCotDienGiaiReport);
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cenCommon.GlobalVariables.Logged)
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name.ToUpper() != "FRMDESKTOP" && frm.Name.ToUpper() != "FRMMAIN")
                    {
                        cenCommon.cenCommon.ErrorMessageOkOnly("Bạn cần đóng hết cửa sổ đang mở trước khi thoát!");
                        cenCommon.GlobalVariables.CanLogout = false;
                        e.Cancel = true;
                        break;
                    }
                }
                //if (!e.Cancel && cenCommon.GlobalVariables._MITECOAuto == "1")
                //{
                //    cenCommon.GlobalVariables.CanLogout = (cenCommon.cenCommon.QuestionMessage("Phần mềm sẽ không gửi được dữ liệu container lên HQGS nếu bạn đóng?\nBạn có chắc chắn không?", 0) == DialogResult.Yes);
                //    if (!cenCommon.GlobalVariables.CanLogout) e.Cancel = true;
                //}
                if (cenCommon.GlobalVariables.CanLogout)
                {
                    cenCommon.GlobalVariables.CanLogout = (cenCommon.cenCommon.QuestionMessage("Bạn chắc chắn muốn thoát?", 0) == DialogResult.Yes);
                    e.Cancel = !cenCommon.GlobalVariables.CanLogout;
                }
            }
        }

        private void Logout()
        {
            this.Close();
            if (cenCommon.GlobalVariables.CanLogout)
            {
                Application.Restart();
                Application.ExitThread();
            }
        }

        private void AutoInOutContainerTimer_Tick(object sender, EventArgs e)
        {
            //AutoInOutContainerTimer.Enabled = false;
            //Boolean Send = false;
            //String strGio, strPhut, strGioPhut;
            //strGio = DateTime.Now.Hour.ToString();
            //strPhut = DateTime.Now.Minute.ToString();
            //strGioPhut = ((strGio.Length < 2) ? "0" + strGio : strGio) + ":" + ((strPhut.Length < 2) ? "0" + strPhut : strPhut);
            //foreach (String strTimer in cenCommon.GlobalVariables._MITECOTimer)
            //{
            //    if (Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " " + strGioPhut) <= Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " " + strTimer))
            //    {
            //        Send = true;
            //        break;
            //    }
            //}
            //if (Send)
            //{
            //    cenCommon.cenCommon.InfoMessage("Hệ thống đang thực hiện gửi dữ liệu tự động tới HQGS, xin hãy đợi đến khi kết thúc!");
            //    cenCommonUIapps.Class.AutoContainerCY.AutoContainerInOut();
            //    cenCommon.cenCommon.InfoMessage("Hệ thống đã thực hiện gửi dữ liệu tự động tới HQGS!");
            //}
            //AutoInOutContainerTimer.Enabled = true;
        }


    }
}

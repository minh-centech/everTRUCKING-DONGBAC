using cenBUS.DatabaseCore;
using cenDTO.DatabaseCore;
using Infragistics.Win.UltraWinExplorerBar;
using System;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIbase.Forms
{
    public partial class frmDesktop : Form
    {
        public frmDesktop()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            uBarMenu.Groups.Clear();
            uBarBaoCao.Groups.Clear();
            //Load Menu, DanhMuc, ChungTu, BaoCao
            //Load danh sách phân hệ
            DanhMucMenuBUS menuBUS = new DanhMucMenuBUS();
            DataSet dsMenu = menuBUS.List(null);
            foreach (DataRow drMenu in dsMenu.Tables[DanhMucMenu.tableName].Rows)
            {
                UltraExplorerBarGroup uGroup = new UltraExplorerBarGroup
                {
                    Key = "mnu" + drMenu["ID"].ToString(),
                    Text = drMenu["Ten"].ToString()
                };
                //if (drMenu["Anh"] != DBNull.Value)
                //{
                //    uGroup.Settings.AppearancesSmall.HeaderAppearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drMenu["Anh"]);
                //    uGroup.Settings.AppearancesSmall.NavigationPaneHeaderAppearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drMenu["Anh"]);
                //}
                uBarMenu.Groups.Add(uGroup);
            }
            //Load chứng từ và danh mục tương ứng với các phân hệ
            foreach (UltraExplorerBarGroup uGroup in uBarMenu.Groups)
            {
                Boolean CoChungTu = false;
                //Load chứng từ
                foreach (DataRow drChungTu in dsMenu.Tables[DanhMucMenuChungTu.tableName].Rows)
                {
                    if (drChungTu["IDDanhMucMenu"].ToString() == uGroup.Key.Substring(3))
                    {
                        UltraExplorerBarItem uItem = new UltraExplorerBarItem();
                        uItem.Settings.Style = ItemStyle.Button;
                        uItem.Key = "_dct_" + drChungTu["IDDanhMucChungTu"].ToString() + "ID:" + drChungTu["ID"].ToString() + "MA:" + drChungTu["MaDanhMucChungTu"].ToString() + "LOAIMANHINH:" + drChungTu["LoaiManHinh"].ToString();
                        uItem.Text = drChungTu["NoiDungHienThi"].ToString();
                        //if (drChungTu["Anh"] != DBNull.Value)
                        //{
                        //    uItem.Settings.AppearancesSmall.Appearance.Image = cenCommon.GlobalVariables.ImageFromByte((Byte[])drChungTu["Anh"]);
                        //}
                        //else
                        //    uItem.Settings.AppearancesSmall.Appearance.Image = null;
                        uGroup.Items.Add(uItem);
                        CoChungTu = true;
                    }
                }
                //Nếu có chứng từ thì thêm 1 đường kẻ ngang
                if (CoChungTu)
                {
                    UltraExplorerBarItem uItem = new UltraExplorerBarItem();
                    uItem.Settings.Style = ItemStyle.Separator;
                    uItem.Key = "br" + uGroup.Key;
                    uGroup.Items.Add(uItem);
                }
                //Load danh mục
                Boolean CoDanhMuc = false;
                foreach (DataRow drDanhMuc in dsMenu.Tables[DanhMucMenuLoaiDoiTuong.tableName].Rows)
                {
                    if (drDanhMuc["IDDanhMucMenu"].ToString() == uGroup.Key.Substring(3))
                    {
                        //uGroup.Items.Add("ct" + drDanhMuc["ID"].ToString(), drDanhMuc["Ten"].ToString());

                        UltraExplorerBarItem uItem = new UltraExplorerBarItem();
                        uItem.Settings.Style = ItemStyle.Button;
                        uItem.Key = "_ddm_" + drDanhMuc["IDDanhMucLoaiDoiTuong"].ToString() + "ID:" + drDanhMuc["ID"].ToString();
                        uItem.Text = drDanhMuc["NoiDungHienThi"].ToString();
                        uGroup.Items.Add(uItem);

                        CoDanhMuc = true;
                    }
                }
                //Nếu không có danh mục thì bỏ đường kẻ ngang
                if (!CoDanhMuc & uGroup.Items.Exists("br" + uGroup.Key))
                {
                    UltraExplorerBarItem uItem = uGroup.Items["br" + uGroup.Key];
                    uGroup.Items.Remove(uItem);
                }
            }
            //Load báo cáo
            //Load các group trong báo cáo trước
            foreach (DataRow drBaoCao in dsMenu.Tables[DanhMucMenuBaoCao.tableName].Rows)
            {
                if (!uBarBaoCao.Groups.Exists("mnuGroup" + drBaoCao["IDDanhMucNhomBaoCao"].ToString()))
                {
                    UltraExplorerBarGroup uGroup = new UltraExplorerBarGroup
                    {
                        Key = "mnuGroup" + drBaoCao["IDDanhMucNhomBaoCao"].ToString(),
                        Text = drBaoCao["TenDanhMucNhomBaoCao"].ToString()
                    };
                    uBarBaoCao.Groups.Add(uGroup);
                }
            }
            //Load báo cáo vào từng group
            foreach (DataRow drBaoCao in dsMenu.Tables[DanhMucMenuBaoCao.tableName].Rows)
            {
                UltraExplorerBarItem uItem = new UltraExplorerBarItem();
                String GroupKey = "mnuGroup" + drBaoCao["IDDanhMucNhomBaoCao"].ToString();
                uItem.Settings.Style = ItemStyle.Button;
                uItem.Key = "_dbc_" + drBaoCao["IDDanhMucBaoCao"].ToString() + "ID:" + drBaoCao["ID"].ToString() + "menu:" + drBaoCao["IDDanhMucMenu"].ToString();
                uItem.Text = drBaoCao["NoiDungHienThi"].ToString();
                uBarBaoCao.Groups[GroupKey].Items.Add(uItem);
            }
            //Chọn phân hệ đầu tiên
            if (uBarMenu.Groups.Count > 0)
            {
                uBarMenu.Groups[0].Selected = true;
                ShowBaoCao(uBarMenu.Groups[0].Key.Substring(3), uBarMenu.Groups[0].Text);
            }
        }
        private void frmDesktop_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void uBarMenu_GroupClick(object sender, Infragistics.Win.UltraWinExplorerBar.GroupEventArgs e)
        {
            //Hiển thị danh sách báo cáo tương ứng với phân hệ này
            if (e != null) ShowBaoCao(e.Group.Key.Substring(3), e.Group.Text);
        }
        private void ShowBaoCao(String menuID, String menuText)
        {
            if (menuID != null)
            {
                if (uBarBaoCao.Groups.Exists("mnuGroupNull"))
                    uBarBaoCao.Groups["mnuGroupNull"].Text = menuText;
                //Ẩn hiện những mục báo cáo không thuộc phân hệ này
                foreach (UltraExplorerBarGroup uGroup in uBarBaoCao.Groups)
                {
                    uGroup.Visible = true;
                    Boolean HasVisibleChildren = false;
                    foreach (UltraExplorerBarItem uItem in uGroup.Items)
                    {
                        if (!uItem.Key.Contains("menu:" + menuID))
                            uItem.Visible = false;
                        else
                        {
                            uItem.Visible = true;
                            HasVisibleChildren = true;
                        }
                    }
                    //Nếu group báo cáo này không có báo cáo nào thì ẩn đi
                    if (!HasVisibleChildren) uGroup.Visible = false;
                }
                foreach (UltraExplorerBarGroup uGroup in uBarBaoCao.Groups)
                {
                    if (uGroup.Visible)
                    {
                        uGroup.Selected = true;
                        break;
                    }
                }
            }
        }

        private void uBarMenu_ItemClick(object sender, ItemEventArgs e)
        {
            String LoaiChucNang = e.Item.Key.Substring(0, 5).ToUpper();
            String TenChucNang = e.Item.Key.Substring(5);
            switch (LoaiChucNang)
            {
                case "_DDM_": //Các chức năng danh mục
                    //Lấy loại danh mục (tên bảng dữ liệu) theo IDLoaiDoiTuong
                    String IDDanhMucLoaiDoiTuong = TenChucNang.Substring(0, TenChucNang.IndexOf("ID:"));
                    cenCommonUIapps.cenCommonUIapps.runDanhMuc(e.Item.Text, IDDanhMucLoaiDoiTuong, this.MdiParent);
                    break;
                case "_DCT_": //các chức năng chứng từ
                    String IDDanhMucChungTu = TenChucNang.Substring(0, TenChucNang.IndexOf("ID:"));
                    String MaDanhMucChungTu = TenChucNang.Substring(TenChucNang.IndexOf("MA:") + "MA:".Length, TenChucNang.IndexOf("LOAIMANHINH:") - TenChucNang.IndexOf("MA:") - "MA:".Length);
                    String LoaiManHinh = TenChucNang.Substring(TenChucNang.IndexOf("LOAIMANHINH:") + "LOAIMANHINH:".Length, TenChucNang.Length - TenChucNang.IndexOf("LOAIMANHINH:") - "LOAIMANHINH:".Length);
                    cenCommonUIapps.cenCommonUIapps.runChungTu(e.Item.Text, LoaiManHinh, IDDanhMucChungTu, this.MdiParent);
                    break;
                case "_DBC_": //các chức năng báo cáo
                    String IDDanhMucBaoCao = TenChucNang.Substring(0, TenChucNang.IndexOf("ID:"));
                    cenCommonUIapps.cenCommonUIapps.runBaoCao(IDDanhMucBaoCao, this.MdiParent);
                    break;
            }
        }
    }
}

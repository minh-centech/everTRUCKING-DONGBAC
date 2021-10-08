using cenBase.BaseForms;
using cenBUS.cenLogistics;
using cenBUS.DatabaseCore;
using cenControls;
using cenDTO.cenLogistics;
using cenDTO.DatabaseCore;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.cenLogistics.Forms
{
    public partial class frm_ctTamUngUpdate : frmBaseChungTuMasterDetail
    {
        public Form mdiParent;
        DataTable dtTrangThai, dtSale, dtKhachHang, dtNhomHangVanChuyen, dtHangHoa, dtHangTau, dtDiaDiemGiaoNhan, dtDiaDiemLayTraHang;
        ctDonHang obj;
        ctDonHangBUS bus;
        public frm_ctTamUngUpdate()
        {
            InitializeComponent();
        }
        #region Methods
        protected override void loadValidDataSet()
        {
            //Load danh mục chứng từ trạng thái
            DanhMucChungTuTrangThaiBUS danhMucChungTuTrangThaiBUS = new DanhMucChungTuTrangThaiBUS();
            dtTrangThai = danhMucChungTuTrangThaiBUS.List(null, IDDanhMucChungTu);
            cboIDDanhMucTrangThaiChungTu.DataSource = dtTrangThai;
            cboIDDanhMucTrangThaiChungTu.ValueMember = "ID";
            cboIDDanhMucTrangThaiChungTu.DisplayMember = "Ten";
            //Sale
            DanhMucNhanSuBUS DanhMucNhanSuBUS = new DanhMucNhanSuBUS();
            dtSale = DanhMucNhanSuBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu)), null);
            cboIDDanhMucSale.dtValid = dtSale;
            cboIDDanhMucSale.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu));
            cboIDDanhMucSale.procedureName = DanhMucNhanSu.listProcedureName;
            cboIDDanhMucSale.DataSource = dtSale;
            cboIDDanhMucSale.ValueMember = "ID";
            cboIDDanhMucSale.DisplayMember = "Ten";
            this.cboIDDanhMucSale.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //DanhMucKhachHang
            DanhMucKhachHangBUS KhachHangBUS = new DanhMucKhachHangBUS();
            dtKhachHang = KhachHangBUS.Valid((dsChungTu.Tables[ctDonHang.tableName].Rows.Count > 0) ? dsChungTu.Tables[ctDonHang.tableName].Rows[0]["IDDanhMucKhachHang"] : null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang)), null);
            txtMaDanhMucKhachHang.IsValid = true;
            txtMaDanhMucKhachHang.dtValid = dtKhachHang;
            txtMaDanhMucKhachHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongKhachHang));
            saTextBox[] txtMaDanhMucKhachHangExt = new saTextBox[3];
            txtMaDanhMucKhachHangExt[0] = txtTenDanhMucKhachHang;
            txtMaDanhMucKhachHangExt[1] = txtIDDanhMucNhanSu;
            txtMaDanhMucKhachHangExt[2] = txtMaSoThue;
            txtMaDanhMucKhachHang.txtMoRong = txtMaDanhMucKhachHangExt;
            txtMaDanhMucKhachHang.ValidColumnName = "Ma";
            txtMaDanhMucKhachHang.ReturnedColumnsList = "Ten;IDDanhMucNhanSu;MaSoThue";
            txtMaDanhMucKhachHang.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //LoaiHang
            cboLoaiHang.Items.Add("1", "Nhập");
            cboLoaiHang.Items.Add("2", "Xuất");
            cboLoaiHang.Items.Add("3", "Nội địa");
            //Nhóm hàng vận chuyển
            DanhMucDoiTuongBUS DanhMucNhomHangVanChuyenBUS = new DanhMucDoiTuongBUS();
            dtNhomHangVanChuyen = DanhMucNhomHangVanChuyenBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen)), null);
            cboIDDanhMucNhomHangVanChuyen.dtValid = dtNhomHangVanChuyen;
            cboIDDanhMucNhomHangVanChuyen.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhomHangVanChuyen));
            cboIDDanhMucNhomHangVanChuyen.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucNhomHangVanChuyen.DataSource = dtNhomHangVanChuyen;
            cboIDDanhMucNhomHangVanChuyen.ValueMember = "ID";
            cboIDDanhMucNhomHangVanChuyen.DisplayMember = "Ten";
            this.cboIDDanhMucNhomHangVanChuyen.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //Hãng Tàu
            DanhMucDoiTuongBUS DanhMucHangTauBUS = new DanhMucDoiTuongBUS();
            dtHangTau = DanhMucHangTauBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongHangTau)), null);
            cboIDDanhMucHangTau.dtValid = dtHangTau;
            cboIDDanhMucHangTau.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongHangTau));
            cboIDDanhMucHangTau.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucHangTau.DataSource = dtHangTau;
            cboIDDanhMucHangTau.ValueMember = "ID";
            cboIDDanhMucHangTau.DisplayMember = "Ten";
            this.cboIDDanhMucHangTau.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //Hàng hóa
            DanhMucHangHoaBUS DanhMucHangHoaBUS = new DanhMucHangHoaBUS();
            dtHangHoa = DanhMucHangHoaBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongHangHoa)), null, null);
            cboIDDanhMucHangHoa.dtValid = dtHangHoa;
            cboIDDanhMucHangHoa.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongHangHoa));
            cboIDDanhMucHangHoa.procedureName = DanhMucHangHoa.listProcedureName;
            cboIDDanhMucHangHoa.DataSource = dtHangHoa;
            cboIDDanhMucHangHoa.ValueMember = "ID";
            cboIDDanhMucHangHoa.DisplayMember = "Ten";
            this.cboIDDanhMucHangHoa.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //Địa điểm lấy/trả cont
            DanhMucDoiTuongBUS DanhMucDiaDiemGiaoNhanBUS = new DanhMucDoiTuongBUS();
            dtDiaDiemGiaoNhan = DanhMucDiaDiemGiaoNhanBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDiaDiemLayTraCont)), null);
            cboIDDanhMucDiaDiemLayContHang.dtValid = dtDiaDiemGiaoNhan;
            cboIDDanhMucDiaDiemLayContHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDiaDiemLayTraCont));
            cboIDDanhMucDiaDiemLayContHang.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucDiaDiemLayContHang.DataSource = dtDiaDiemGiaoNhan;
            cboIDDanhMucDiaDiemLayContHang.ValueMember = "ID";
            cboIDDanhMucDiaDiemLayContHang.DisplayMember = "Ten";
            this.cboIDDanhMucDiaDiemLayContHang.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            cboIDDanhMucDiaDiemTraContHang.dtValid = dtDiaDiemGiaoNhan;
            cboIDDanhMucDiaDiemTraContHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDiaDiemLayTraCont));
            cboIDDanhMucDiaDiemTraContHang.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucDiaDiemTraContHang.DataSource = dtDiaDiemGiaoNhan;
            cboIDDanhMucDiaDiemTraContHang.ValueMember = "ID";
            cboIDDanhMucDiaDiemTraContHang.DisplayMember = "Ten";
            this.cboIDDanhMucDiaDiemTraContHang.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //Địa điểm lấy/trả Hang
            DanhMucDoiTuongBUS DanhMucDiaDiemLayTraHangBUS = new DanhMucDoiTuongBUS();
            dtDiaDiemLayTraHang = DanhMucDiaDiemLayTraHangBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDiaDiemLayTraHang)), null);
            cboIDDanhMucDiaDiemLayHang.dtValid = dtDiaDiemLayTraHang;
            cboIDDanhMucDiaDiemLayHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDiaDiemLayTraHang));
            cboIDDanhMucDiaDiemLayHang.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucDiaDiemLayHang.DataSource = dtDiaDiemLayTraHang;
            cboIDDanhMucDiaDiemLayHang.ValueMember = "ID";
            cboIDDanhMucDiaDiemLayHang.DisplayMember = "Ten";
            this.cboIDDanhMucDiaDiemLayHang.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            cboIDDanhMucDiaDiemTraHang.dtValid = dtDiaDiemLayTraHang;
            cboIDDanhMucDiaDiemTraHang.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongDiaDiemLayTraHang));
            cboIDDanhMucDiaDiemTraHang.procedureName = DanhMucDoiTuong.listProcedureName;
            cboIDDanhMucDiaDiemTraHang.DataSource = dtDiaDiemLayTraHang;
            cboIDDanhMucDiaDiemTraHang.ValueMember = "ID";
            cboIDDanhMucDiaDiemTraHang.DisplayMember = "Ten";
            this.cboIDDanhMucDiaDiemTraHang.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
            //Tuyến vận tải
            DanhMucTuyenVanTaiBUS DanhMucTuyenVanTaiBUS = new DanhMucTuyenVanTaiBUS();
            DataTable dtTuyenVanTai = DanhMucTuyenVanTaiBUS.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTuyenVanTai)), null);
            cboIDDanhMucTuyenVanTai.dtValid = dtTuyenVanTai;
            cboIDDanhMucTuyenVanTai.IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongTuyenVanTai));
            cboIDDanhMucTuyenVanTai.procedureName = DanhMucTuyenVanTai.listProcedureName;
            cboIDDanhMucTuyenVanTai.DataSource = dtTuyenVanTai;
            cboIDDanhMucTuyenVanTai.ValueMember = "ID";
            cboIDDanhMucTuyenVanTai.DisplayMember = "Ten";
            this.cboIDDanhMucTuyenVanTai.KeyDown += new System.Windows.Forms.KeyEventHandler(cenCommonUIapps.validDanhMuc.cboDanhMuc_KeyDown);
        }
        protected override void loadData()
        {
            bus = new ctDonHangBUS();
            dsChungTu = bus.List((UpdateMode == cenCommon.ThaoTacDuLieu.Them) ? DBNull.Value : IDDanhMucChungTu, (UpdateMode == cenCommon.ThaoTacDuLieu.Them) ? DBNull.Value : IDChungTu);
            bsChungTu = new BindingSource();
            bsChungTu.DataSource = dsChungTu;
            bsChungTu.DataMember = ctDonHang.tableName;
            bsChungTuChiTiet = new BindingSource();
            bsChungTuChiTiet.DataSource = bsChungTu;
            bsChungTuChiTiet.DataMember = cenCommon.GlobalVariables.prefix_DataRelation + ctDonHang.tableNameChiTiet;
            ugChiTiet.HiddenColumnsList = "[LanTamUng]";
            ugChiTiet.ReadOnlyColumnsList = "[TenDanhMucCanBoTamUng][TenDanhMucHangTau][MaDanhMucNguoiSuDungCreate][CreateDate][MaDanhMucNguoiSuDungEdit][EditDate][MaDanhMucNguoiSuDungDelete][DeleteDate]";
            ugChiTiet.DataSource = bsChungTuChiTiet;
            ugChiTiet.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.Default; //Biểu tượng lọc đặt trên tiêu đề cột
            tabChiTiet.Tabs["ChiTiet"].Text = cenCommon.cenCommon.TraTuDien(ctDonHang.tableNameChiTiet) + "-CTRL + INSERT: Thêm dòng; CTRL + DELETE: Xóa dòng";
            if (UpdateMode == cenCommon.ThaoTacDuLieu.Copy)
            {
                dsChungTu.Tables[ctDonHang.tableName].Rows[0]["So"] = DBNull.Value;
            }
        }
        protected override void themChungTu()
        {
            bsChungTu.AddNew();

            ugChiTiet.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
            ugChiTiet.DisplayLayout.Bands[0].Columns["MaDanhMucCanBoTamUng"].CellClickAction = CellClickAction.EditAndSelectText;
            ugChiTiet.DisplayLayout.Bands[0].Columns["MaDanhMucCanBoTamUng"].CellActivation = Activation.AllowEdit;

            txtNgayLap.Value = cenCommon.cenCommon.NgayHachToan();
            txtNgayCatMang.Value = cenCommon.cenCommon.NgayHachToan();
            txtSoLuong.Value = 1;

            if (cboIDDanhMucTrangThaiChungTu.Items.Count > 0)
                cboIDDanhMucTrangThaiChungTu.SelectedItem = cboIDDanhMucTrangThaiChungTu.Items[0];

            UpdateMode = cenCommon.ThaoTacDuLieu.Them;
            //enableControl();

            bsChungTuChiTiet.AddNew();
        }
        protected override void suaChungTu()
        {
            base.suaChungTu();
            //
            UpdateMode = cenCommon.ThaoTacDuLieu.Sua;
            //enableControl();
            ugChiTiet.SetEditableState(false);
            //
            txtNgayDongHang.Focus();
            txtMaDanhMucKhachHang.ID = dsChungTu.Tables[ctDonHang.tableName].Rows[0]["IDDanhMucKhachHang"];
        }
        protected override void saveCustomsData()
        {
            if (UpdateMode == 0) return;
            base.saveCustomsData();
        }
        protected override void setDataBindings()
        {
            base.setDataBindings();
            txtNgayLap.MaskInput = cenCommon.GlobalVariables.MaskInputDate;
            //
            txtSo.SetDataBinding(bsChungTu, "So", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNgayLap.SetDataBinding(bsChungTu, "NgayLap", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucTrangThaiChungTu.DataBindings.Clear();
            cboIDDanhMucTrangThaiChungTu.DataBindings.Add("Value", bsChungTu, "IDDanhMucChungTuTrangThai", false, DataSourceUpdateMode.OnPropertyChanged);
            //

        }
        protected override void setCustomsDataBindings()
        {
            cboIDDanhMucSale.DataBindings.Clear();
            cboIDDanhMucSale.DataBindings.Add("Value", bsChungTu, "IDDanhMucSale", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMaDanhMucKhachHang.SetDataBinding(bsChungTu, "MaDanhMucKhachHang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtTenDanhMucKhachHang.SetDataBinding(bsChungTu, "TenDanhMucKhachHang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMaSoThue.SetDataBinding(bsChungTu, "MaSoThue", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDebitNote.SetDataBinding(bsChungTu, "DebitNote", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBillBooking.SetDataBinding(bsChungTu, "BillBooking", false, DataSourceUpdateMode.OnPropertyChanged);
            cboLoaiHang.DataBindings.Clear();
            cboLoaiHang.DataBindings.Add("Value", bsChungTu, "LoaiHang", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucNhomHangVanChuyen.DataBindings.Clear();
            cboIDDanhMucNhomHangVanChuyen.DataBindings.Add("Value", bsChungTu, "IDDanhMucNhomHangVanChuyen", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucHangHoa.DataBindings.Clear();
            cboIDDanhMucHangHoa.DataBindings.Add("Value", bsChungTu, "IDDanhMucHangHoa", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucHangTau.DataBindings.Clear();
            cboIDDanhMucHangTau.DataBindings.Add("Value", bsChungTu, "IDDanhMucHangTau", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoLuong.SetDataBinding(bsChungTu, "SoLuong", false, DataSourceUpdateMode.OnPropertyChanged);
            txtTrongLuong.SetDataBinding(bsChungTu, "KhoiLuong", false, DataSourceUpdateMode.OnPropertyChanged);
            txtKhoiLuong.SetDataBinding(bsChungTu, "TheTich", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoContainer.SetDataBinding(bsChungTu, "SoContainer", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucDiaDiemLayContHang.DataBindings.Clear();
            cboIDDanhMucDiaDiemLayContHang.DataBindings.Add("Value", bsChungTu, "IDDanhMucDiaDiemLayCont", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucDiaDiemTraContHang.DataBindings.Clear();
            cboIDDanhMucDiaDiemTraContHang.DataBindings.Add("Value", bsChungTu, "IDDanhMucDiaDiemTraCont", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNgayDongHang.SetDataBinding(bsChungTu, "NgayDongHang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtGioDongHang.SetDataBinding(bsChungTu, "GioDongHang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNgayTraHang.SetDataBinding(bsChungTu, "NgayTraHang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtGioTraHang.SetDataBinding(bsChungTu, "GioTraHang", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucDiaDiemLayHang.DataBindings.Clear();
            cboIDDanhMucDiaDiemLayHang.DataBindings.Add("Value", bsChungTu, "IDDanhMucDiaDiemLayHang", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucDiaDiemTraHang.DataBindings.Clear();
            cboIDDanhMucDiaDiemTraHang.DataBindings.Add("Value", bsChungTu, "IDDanhMucDiaDiemTraHang", false, DataSourceUpdateMode.OnPropertyChanged);
            cboIDDanhMucTuyenVanTai.DataBindings.Clear();
            cboIDDanhMucTuyenVanTai.DataBindings.Add("Value", bsChungTu, "IDDanhMucTuyenVanTai", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNgayCatMang.SetDataBinding(bsChungTu, "NgayCatMang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNgayCatMang.SetDataBinding(bsChungTu, "GioCatMang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNguoiGiaoNhan.SetDataBinding(bsChungTu, "NguoiGiaoNhan", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoDienThoaiGiaoNhan.SetDataBinding(bsChungTu, "SoDienThoaiGiaoNhan", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoTienCuoc.SetDataBinding(bsChungTu, "SoTienCuoc", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoTienThuNang.SetDataBinding(bsChungTu, "SoTienThuNang", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoTienThuHa.SetDataBinding(bsChungTu, "SoTienThuHa", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoTienThuKhac.SetDataBinding(bsChungTu, "SoTienThuKhac", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNoiDungThuKhac.SetDataBinding(bsChungTu, "NoiDungThuKhac", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSoTienGiamTru.SetDataBinding(bsChungTu, "SoTienGiamTru", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNoiDungGiamTru.SetDataBinding(bsChungTu, "NoiDungGiamTru", false, DataSourceUpdateMode.OnPropertyChanged);
            txtThoiHanThanhToan.SetDataBinding(bsChungTu, "ThoiHanThanhToan", false, DataSourceUpdateMode.OnPropertyChanged);
            txtGhiChu.SetDataBinding(bsChungTu, "GhiChu", false, DataSourceUpdateMode.OnPropertyChanged);
        }
        protected override void gridColumnDataProcess(UltraGrid ug, UltraGridCell uCell, out bool GridValidation, bool ShowLookUp)
        {
            GridValidation = true;
            if (uCell.Row.IsFilterRow) return;
            String columnKey = uCell.Column.Key.ToUpper();
            if (columnKey == "MADANHMUCCANBOTAMUNG")
            {
                GridValidation = false;
                //Valid đơn vị tính
                DanhMucDoiTuongBUS bus = new DanhMucDoiTuongBUS();
                DataTable dt = bus.List(null, DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu)), uCell.Value);
                if (dt == null) { GridValidation = true; return; }
                if (dt.Rows.Count == 1)
                {
                    uCell.Row.Cells["IDDanhMucCanBoTamUng"].Value = dt.Rows[0]["ID"].ToString();
                    uCell.Row.Cells["MaDanhMucCanBoTamUng"].Value = dt.Rows[0]["Ma"].ToString();
                    uCell.Row.Cells["TenDanhMucCanBoTamUng"].Value = dt.Rows[0]["Ten"].ToString();
                    GridValidation = true;
                    return;
                }
                else
                {
                    //Show valid form
                    frmDanhMucNhanSuValid frmDanhMucNhanSuValid = new frmDanhMucNhanSuValid()
                    {
                        IDDanhMucLoaiDoiTuong = DanhMucLoaiDoiTuongBUS.GetID(DanhMucThamSoHeThongBUS.GetGiaTri(cenCommon.ThamSoHeThong.MaThamSoLoaiDoiTuongNhanSu)),
                        validValue = uCell.Row.Cells["MaDanhMucCanBoTamUng"].Value,
                        dataTable = dt
                    };
                    frmDanhMucNhanSuValid.ShowDialog();
                    if (frmDanhMucNhanSuValid.dataRows == null || frmDanhMucNhanSuValid.dataRows.Count == 0) { GridValidation = true; return; }
                    if (frmDanhMucNhanSuValid.dataRows.Count > 0)
                    {
                        uCell.Row.Cells["IDDanhMucCanBoTamUng"].Value = frmDanhMucNhanSuValid.dataRows[0]["ID"].ToString();
                        uCell.Row.Cells["MaDanhMucCanBoTamUng"].Value = frmDanhMucNhanSuValid.dataRows[0]["Ma"].ToString();
                        uCell.Row.Cells["TenDanhMucCanBoTamUng"].Value = frmDanhMucNhanSuValid.dataRows[0]["Ten"].ToString();
                    }
                }
                GridValidation = true;
                return;
            }
            //else if (columnKey == "SOTIENCUOCVO" || columnKey == "SOTIENHAIQUAN" || columnKey == "SOTIENNANGHA" || columnKey == "SOTIENCHIKHAC")
            //{
            //    float SoTienCuocVo = 0, SoTienHaiQuan = 0, SoTienNangHa = 0, SoTienChiKhac = 0;
            //    foreach (UltraGridRow row in ugChiTiet.Rows)
            //    {
            //        SoTienCuocVo += float.Parse((!cenCommon.cenCommon.IsNull(row.Cells["SoTienCuocVo"].Value)) ? row.Cells["SoTienCuocVo"].Value.ToString() : "0");
            //        SoTienHaiQuan += float.Parse((!cenCommon.cenCommon.IsNull(row.Cells["SoTienHaiQuan"].Value)) ? row.Cells["SoTienHaiQuan"].Value.ToString() : "0");
            //        SoTienNangHa += float.Parse((!cenCommon.cenCommon.IsNull(row.Cells["SoTienNangHa"].Value)) ? row.Cells["SoTienNangHa"].Value.ToString() : "0");
            //        SoTienChiKhac += float.Parse((!cenCommon.cenCommon.IsNull(row.Cells["SoTienChiKhac"].Value)) ? row.Cells["SoTienChiKhac"].Value.ToString() : "0");
            //    }
            //    txtTongSoTienCuocVo.Value = SoTienCuocVo;
            //    txtTongSoTienHaiQuan.Value = SoTienHaiQuan;
            //    txtTongSoTienNangHa.Value = SoTienNangHa;
            //    txtTongSoTienChiKhac.Value = SoTienChiKhac;
            //}
            else
            {
                cenBase.Classes.ChungTu.gridColumnDataProcess(LoaiManHinh.ToString(), ug, uCell, out GridValidation, ShowLookUp);
            }
        }
        protected override void filter()
        {
            //DataSet dsChungTuOld = dsChungTu.Copy();

            //cenBase.Classes.ChungTu.filterChungTu(IDDanhMucChungTu.ToString(), DetailLevel, TableName, TableNameDetail, TableNameDetail2, this.Text, out IDChungTu, out dsChungTu);
            //if (dsChungTu == null || dsChungTu.Tables[TableName].Rows.Count == 0) { dsChungTu = dsChungTuOld.Copy(); dsChungTuOld.Dispose(); }
            //setDataBindings();
            //UpdateMode = 0;
            //enableControl();
        }

        private void ugChiTiet_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
        {
            if (!cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow.Cells["IDDanhMucNguoiSuDungCreate"].Value) && ugChiTiet.ActiveRow.Cells["IDDanhMucNguoiSuDungCreate"].Value.ToString() != cenCommon.GlobalVariables.IDDanhMucNguoiSuDung.ToString())
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền sửa tạm ứng của người khác!");
                e.Cancel = true;
                return;
            }
        }

        protected override void luuChungTu(bool Exit)
        {
            if (UpdateMode == cenCommon.ThaoTacDuLieu.Xem) return;

            IDChungTu = null;
            ugChiTiet.UpdateData();
            //Valid dữ liệu
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucSale.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu tên sale!"); cboIDDanhMucSale.Focus(); return; }
            if (cenCommon.cenCommon.IsNull(txtMaDanhMucKhachHang.ID)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu mã khách hàng!"); txtMaDanhMucKhachHang.Focus(); return; }
            if (cenCommon.cenCommon.IsNull(txtDebitNote.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu số DebitNote!"); txtDebitNote.Focus(); return; }
            if (cenCommon.cenCommon.IsNull(cboLoaiHang.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu loại hàng!"); cboLoaiHang.Focus(); return; }
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucNhomHangVanChuyen.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu nhóm hàng!"); cboIDDanhMucNhomHangVanChuyen.Focus(); return; }

            if (cboLoaiHang.Value.ToString() == "1" && cenCommon.cenCommon.IsNull(txtNgayTraHang.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu ngày trả hàng!"); txtNgayTraHang.Focus(); return; }
            if (cboLoaiHang.Value.ToString() == "2" && cenCommon.cenCommon.IsNull(txtNgayDongHang.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu ngày đóng hàng!"); txtNgayDongHang.Focus(); return; }
            if (cboLoaiHang.Value.ToString() == "3")
            {
                if (cenCommon.cenCommon.IsNull(txtNgayDongHang.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu ngày đóng hàng!"); txtNgayDongHang.Focus(); return; }
                if (cenCommon.cenCommon.IsNull(txtNgayTraHang.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu ngày trả hàng!"); txtNgayDongHang.Focus(); return; }
            }
            if (cenCommon.cenCommon.IsNull(cboIDDanhMucTuyenVanTai.Value)) { cenCommon.cenCommon.ErrorMessageOkOnly("Thiếu tuyến vận tải!"); cboIDDanhMucTuyenVanTai.Focus(); return; }

            foreach (UltraGridRow row in ugChiTiet.Rows)
            {
                if (!cenCommon.cenCommon.IsNull(row.Cells["NgayTamUng"].Value))
                {
                    if (cenCommon.cenCommon.IsNull(row.Cells["IDDanhMucCanBoTamUng"].Value))
                    {
                        cenCommon.cenCommon.ErrorMessageOkOnly("Mã cán bộ tạm ứng ở dòng thứ " + (row.Index + 1).ToString() + " không hợp lệ!");
                        ugChiTiet.Rows[row.Index].Activate();
                        ugChiTiet.Rows[row.Index].Cells["MaDanhMucCanBoTamUng"].Activate();
                        ugChiTiet.PerformAction(UltraGridAction.EnterEditMode);
                        return;
                    }
                    if (cenCommon.cenCommon.IsNull(row.Cells["ThoiHanThanhToan"].Value))
                    {
                        cenCommon.cenCommon.ErrorMessageOkOnly("Thời hạn thanh toán ở dòng thứ " + (row.Index + 1).ToString() + " không hợp lệ!");
                        ugChiTiet.Rows[row.Index].Activate();
                        ugChiTiet.Rows[row.Index].Cells["ThoiHanThanhToan"].Activate();
                        ugChiTiet.PerformAction(UltraGridAction.EnterEditMode);
                        return;
                    }
                }
            }
            //Lưu chứng từ
            bsChungTuChiTiet.EndEdit();
            bsChungTu.EndEdit();

            DataRow dataRow = ((DataRowView)bsChungTu.Current).Row;
            //if (!cenCommon.cenCommon.IsNull(ctDonHangBUS.GetIDctChotDoanhThuGuiKeToan(dataRow["ID"]))) { cenCommon.cenCommon.ErrorMessageOkOnly("Đơn hàng đã chốt doanh thu, không sửa được!"); return; }
            obj = new ctDonHang()
            {
                ID = (UpdateMode == cenCommon.ThaoTacDuLieu.Sua) ? dataRow["ID"] : null,
                IDDanhMucDonVi = (UpdateMode == cenCommon.ThaoTacDuLieu.Sua) ? dataRow["IDDanhMucDonVi"] : cenCommon.GlobalVariables.IDDonVi,
                IDDanhMucChungTu = (UpdateMode == cenCommon.ThaoTacDuLieu.Sua) ? dataRow["IDDanhMucChungTu"] : IDDanhMucChungTu,
                IDDanhMucChungTuTrangThai = cboIDDanhMucTrangThaiChungTu.Value,
                So = txtSo.Value,
                NgayLap = txtNgayLap.DateTime,
                //
                IDDanhMucSale = cboIDDanhMucSale.Value,
                IDDanhMucKhachHang = txtMaDanhMucKhachHang.ID,
                MaDanhMucKhachHang = txtMaDanhMucKhachHang.Value,
                TenDanhMucKhachHang = txtTenDanhMucKhachHang.Value,
                DebitNote = txtDebitNote.Value,
                BillBooking = txtBillBooking.Value,
                LoaiHang = cboLoaiHang.Value,
                IDDanhMucNhomHangVanChuyen = cboIDDanhMucNhomHangVanChuyen.Value,
                IDDanhMucHangHoa = cboIDDanhMucHangHoa.Value,
                IDDanhMucHangTau = cboIDDanhMucHangTau.Value,
                SoLuong = txtSoLuong.Value,
                KhoiLuong = txtTrongLuong.Value,
                TheTich = txtKhoiLuong.Value,
                SoContainer = txtSoContainer.Value,
                IDDanhMucDiaDiemLayCont = cboIDDanhMucDiaDiemLayContHang.Value,
                IDDanhMucDiaDiemTraCont = cboIDDanhMucDiaDiemTraContHang.Value,
                NgayDongHang = txtNgayDongHang.Value,
                GioDongHang = txtGioDongHang.DateTime.Hour.ToString("0#") + ":" + txtGioDongHang.DateTime.Minute.ToString("0#"),
                NgayTraHang = txtNgayTraHang.Value,
                GioTraHang = txtGioTraHang.DateTime.Hour.ToString("0#") + ":" + txtGioTraHang.DateTime.Minute.ToString("0#"),
                IDDanhMucDiaDiemLayHang = cboIDDanhMucDiaDiemLayHang.Value,
                IDDanhMucDiaDiemTraHang = cboIDDanhMucDiaDiemTraHang.Value,
                IDDanhMucTuyenVanTai = cboIDDanhMucTuyenVanTai.Value,
                NgayCatMang = txtNgayCatMang.Value,
                GioCatMang = txtGioCatMang.DateTime.Hour.ToString("0#") + ":" + txtGioCatMang.DateTime.Minute.ToString("0#"),
                NguoiGiaoNhan = txtNguoiGiaoNhan.Value,
                SoDienThoaiGiaoNhan = txtSoDienThoaiGiaoNhan.Value,
                SoTienCuoc = txtSoTienCuoc.Value,
                SoTienThuNang = txtSoTienThuNang.Value,
                SoTienThuHa = txtSoTienThuHa.Value,
                SoTienThuKhac = txtSoTienThuKhac.Value,
                NoiDungThuKhac = txtNoiDungThuKhac.Value,
                SoTienGiamTru = txtSoTienGiamTru.Value,
                NoiDungGiamTru = txtNoiDungGiamTru.Value,
                ThoiHanThanhToan = txtThoiHanThanhToan.Value,
                GhiChu = txtGhiChu.Value,
                //
                IDDanhMucNguoiSuDungCreate = (UpdateMode == cenCommon.ThaoTacDuLieu.Them) ? cenCommon.GlobalVariables.IDDanhMucNguoiSuDung : (dataRow["IDDanhMucNguoiSuDungCreate"] ?? dataRow["IDDanhMucNguoiSuDungCreate"]),
                MaDanhMucNguoiSuDungCreate = (UpdateMode == cenCommon.ThaoTacDuLieu.Them) ? cenCommon.GlobalVariables.MaDanhMucNguoiSuDung.ToString() : (dataRow["MaDanhMucNguoiSuDungCreate"].ToString() ?? dataRow["MaDanhMucNguoiSuDungCreate"].ToString()),
                IDDanhMucNguoiSuDungEdit = (UpdateMode == cenCommon.ThaoTacDuLieu.Sua) ? cenCommon.GlobalVariables.IDDanhMucNguoiSuDung : ((dataRow != null) ? dataRow["IDDanhMucNguoiSuDungEdit"] : null),
                MaDanhMucNguoiSuDungEdit = (UpdateMode == cenCommon.ThaoTacDuLieu.Sua) ? cenCommon.GlobalVariables.MaDanhMucNguoiSuDung.ToString() : ((dataRow != null) ? dataRow["MaDanhMucNguoiSuDungEdit"].ToString() : null),
                CreateDate = null,
                EditDate = null
            };
            foreach (DataRow drChiTiet in dsChungTu.Tables[ctDonHang.tableNameChiTiet].Rows)
            {

                if (drChiTiet.RowState != DataRowState.Deleted)
                {
                    if (!cenCommon.cenCommon.IsNull(drChiTiet["IDDanhMucCanBoTamUng"]))
                    {
                        obj.listChiTiet.Add(new ctDonHang.ctDonHangChiTietTamUng()
                        {
                            ID = (UpdateMode == cenCommon.ThaoTacDuLieu.Sua) ? drChiTiet["ID"] : null,
                            IDChungTu = dataRow["ID"],
                            //
                            NgayTamUng = drChiTiet["NgayTamUng"],
                            SoTienPhiHaTang = drChiTiet["SoTienPhiHaTang"],
                            SoTienLocalCharge = drChiTiet["SoTienLocalCharge"],
                            SoTienLuuBai = drChiTiet["SoTienLuuBai"],
                            SoTienNangHa = drChiTiet["SoTienNangHa"],
                            SoTienCuocVo = drChiTiet["SoTienCuocVo"],
                            SoTienHaiQuan = drChiTiet["SoTienHaiQuan"],
                            SoTienLamHang = drChiTiet["SoTienLamHang"],
                            SoTienChonVo = drChiTiet["SoTienChonVo"],
                            SoTienChiKhac = drChiTiet["SoTienChiKhac"],
                            IDDanhMucCanBoTamUng = drChiTiet["IDDanhMucCanBoTamUng"],
                            ThoiHanThanhToan = drChiTiet["ThoiHanThanhToan"],
                            //
                            GhiChu = drChiTiet["GhiChu"],
                            DataRowState = drChiTiet.RowState
                        }
                        );
                    }
                }
                else
                {
                    if (!cenCommon.cenCommon.IsNull(drChiTiet["IDDanhMucCanBoTamUng", DataRowVersion.Original]))
                    {
                        obj.listChiTiet.Add(new ctDonHang.ctDonHangChiTietTamUng()
                        {
                            ID = drChiTiet["ID", DataRowVersion.Original],
                            IDChungTu = dataRow["ID"],
                            //
                            NgayTamUng = drChiTiet["NgayTamUng", DataRowVersion.Original],
                            SoTienPhiHaTang = drChiTiet["SoTienPhiHaTang", DataRowVersion.Original],
                            SoTienLocalCharge = drChiTiet["SoTienLocalCharge", DataRowVersion.Original],
                            SoTienLuuBai = drChiTiet["SoTienLuuBai", DataRowVersion.Original],
                            SoTienNangHa = drChiTiet["SoTienNangHa", DataRowVersion.Original],
                            SoTienCuocVo = drChiTiet["SoTienCuocVo", DataRowVersion.Original],
                            SoTienHaiQuan = drChiTiet["SoTienHaiQuan", DataRowVersion.Original],
                            SoTienLamHang = drChiTiet["SoTienLamHang", DataRowVersion.Original],
                            SoTienChonVo = drChiTiet["SoTienChonVo", DataRowVersion.Original],
                            SoTienChiKhac = drChiTiet["SoTienChiKhac", DataRowVersion.Original],
                            IDDanhMucCanBoTamUng = drChiTiet["IDDanhMucCanBoTamUng", DataRowVersion.Original],
                            ThoiHanThanhToan = drChiTiet["ThoiHanThanhToan", DataRowVersion.Original],
                            //
                            GhiChu = drChiTiet["GhiChu", DataRowVersion.Original],
                            DataRowState = drChiTiet.RowState
                        }
                        );
                    }
                }

            }
            bus = new ctDonHangBUS();
            Saved = (UpdateMode == cenCommon.ThaoTacDuLieu.Them || UpdateMode == cenCommon.ThaoTacDuLieu.Copy) ? bus.Insert(ref obj) : bus.Update(ref obj, false);
            if (Saved && obj != null && Int64.TryParse(obj.ID.ToString(), out Int64 _ID) && _ID > 0)
            {

                int i = -1;
                if (!cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow))
                {
                    i = ugChiTiet.ActiveRow.Index;
                }

                IDChungTu = obj.ID;

                bus = new ctDonHangBUS();
                dsChungTu = bus.List(IDDanhMucChungTu, IDChungTu);
                bsChungTu.DataSource = dsChungTu;
                bsChungTu.DataMember = ctDonHang.tableName;
                bsChungTuChiTiet.DataSource = bsChungTu;
                bsChungTuChiTiet.DataMember = cenCommon.GlobalVariables.prefix_DataRelation + ctDonHang.tableNameChiTiet;

                ugChiTiet.ReadOnlyColumnsList = "[TenDanhMucCanBoTamUng][TenDanhMucHangTau][LanTamUng]";
                ugChiTiet.DataSource = bsChungTuChiTiet;


                UpdateMode = cenCommon.ThaoTacDuLieu.Xem;

                if (i >= 0 && ugChiTiet.Rows.Count >= i + 1)
                {
                    ugChiTiet.Rows[i].Activate();
                    ugChiTiet.Rows[i].Selected = true;
                }

                if (Exit)
                {
                    UpdateMode = cenCommon.ThaoTacDuLieu.Xem;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    UpdateMode = cenCommon.ThaoTacDuLieu.Sua;
                    enableControl();
                }
            }
        }
        protected override void inChungTu()
        {
            //if (bsChungTu.Current == null) return;
            //if (UpdateMode != 0) luuChungTu();
            //DataRow dataRow = ((DataRowView)bsChungTu.Current).Row;
            //if (!ctDonHangBUS.GetSuaXoa(dataRow["ID"]))
            //{
            //    cenCommon.cenCommon.ErrorMessageOkOnly("Không in được chứng từ đã bị hủy!");
            //    return;
            //}
            //String IDChungTu = dataRow["ID"].ToString();
            //String IDDanhMucChungTu = dataRow["IDDanhMucChungTu"].ToString();
            //cenBase.Classes.ChungTu.inChungTu(IDDanhMucChungTu, IDChungTu, ctDonHang.tableName, ctDonHang.tableNameChiTiet, this.MdiParent, false, cenCommon.GlobalVariables.reportPath + @"\" + ctDonHang.reportFileName, ctDonHang.listProcedureName, TenMayIn, 2, LoaiManHinh, 0);
        }
        protected override void themChungTuChiTiet()
        {
            GridValidation = false;
            bsChungTuChiTiet.AddNew();
            ugChiTiet.Focus();
            ugChiTiet.DisplayLayout.Rows[ugChiTiet.DisplayLayout.Rows.Count - 1].Activate();
            if (cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow.Cells["ID"].Value))
            {
                ugChiTiet.ActiveRow.Cells["ID"].Value = cenBase.Classes.ChungTu.MaxTempID(dsChungTu.Tables[ctDonHang.tableNameChiTiet]);
            }
            ugChiTiet.ActiveCell = ugChiTiet.ActiveRow.Cells["MaDanhMucCanBoTamUng"];
            ugChiTiet.ActiveCell.Activate();
            ugChiTiet.PerformAction(UltraGridAction.EnterEditMode);
            GridValidation = true;
        }
        protected override void xoaChungTuChiTiet()
        {
            if (ugChiTiet.ActiveRow == null) return;
            if (!cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow.Cells["IDDanhMucNguoiSuDungCreate"].Value) && ugChiTiet.ActiveRow.Cells["IDDanhMucNguoiSuDungCreate"].Value.ToString() != cenCommon.GlobalVariables.IDDanhMucNguoiSuDung.ToString())
            {
                cenCommon.cenCommon.ErrorMessageOkOnly("Bạn không có quyền xóa tạm ứng của người khác!");
                return;
            }
            int i = ugChiTiet.ActiveRow.Index;
            ugChiTiet.ActiveRow.Delete();
            if (i > 0) i -= 1;
            if (i <= ugChiTiet.Rows.Count - 1)
            {
                ugChiTiet.Focus();
                ugChiTiet.Rows[i].Activate();
                ugChiTiet.Rows[i].Cells["MaDanhMucCanBoTamUng"].Activate();
                ugChiTiet.PerformAction(UltraGridAction.EnterEditMode);
            }
        }
        #endregion Method
        #region FormEvents
        private void frm_ctDonHang_Load(object sender, EventArgs e)
        {
            txtGioDongHang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtGioTraHang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            txtGioCatMang.MaskInput = cenCommon.GlobalVariables.MaskInputTime;
            //
            loadData();
            loadValidDataSet();
            //
            setDataBindings();
            setCustomsDataBindings();
            //
            if (UpdateMode == cenCommon.ThaoTacDuLieu.Them)
                themChungTu();
            if (UpdateMode == cenCommon.ThaoTacDuLieu.Sua)
                suaChungTu();
            if (UpdateMode == cenCommon.ThaoTacDuLieu.Xem)
            {
                enableControl();
                toolBar.Toolbars[0].Tools["btSua"].SharedProps.Enabled = true;
            }
            if (UpdateMode == cenCommon.ThaoTacDuLieu.Copy)
            {
                ugChiTiet.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
                ugChiTiet.DisplayLayout.Bands[0].Columns["MaDanhMucCanBoTamUng"].CellClickAction = CellClickAction.EditAndSelectText;
                ugChiTiet.DisplayLayout.Bands[0].Columns["MaDanhMucCanBoTamUng"].CellActivation = Activation.AllowEdit;
                dsChungTu.Tables[ctDonHang.tableNameChiTiet].Rows.Clear();
                dsChungTu.AcceptChanges();
                bsChungTuChiTiet.AddNew();
                txtNgayDongHang.Focus();
                txtMaDanhMucKhachHang.ID = dsChungTu.Tables[ctDonHang.tableName].Rows[0]["IDDanhMucKhachHang"];
            }
        }
        //In phiếu
        private void toolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "ctYeuCauVanChuyen":
                    if (UpdateMode != cenCommon.ThaoTacDuLieu.Xem) luuChungTu(false);
                    if (!cenCommon.cenCommon.IsNull(IDChungTu))
                        cenBase.Classes.ChungTu.inChungTu(IDDanhMucChungTu.ToString(), IDChungTu.ToString(), ctDonHang.tableName, ctDonHang.tableNameChiTiet, this.MdiParent, false, cenCommon.GlobalVariables.reportPath + @"ctYeuCauVanChuyen.rpt", ctDonHang.listProcedureName, "", 1, LoaiManHinh, 1);
                    break;
                case "ctGiayDeNghiTamUng":
                    if (cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow)) return;
                    if (UpdateMode != cenCommon.ThaoTacDuLieu.Xem) luuChungTu(false);
                    if (!cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow.Cells["ID"].Value))
                        cenBase.Classes.ChungTu.inChungTu(IDDanhMucChungTu.ToString(), ugChiTiet.ActiveRow.Cells["ID"].Value.ToString(), ctDonHang.tableName, "", this.MdiParent, false, cenCommon.GlobalVariables.reportPath + @"ctGiayDeNghiTamUng.rpt", ctDonHang.listIDChiTietProcedureName, "", 1, LoaiManHinh, 1);
                    break;
                case "ctGiayDeNghiTamUngCK":
                    if (cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow)) return;
                    if (UpdateMode != cenCommon.ThaoTacDuLieu.Xem) luuChungTu(false);
                    if (!cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow.Cells["ID"].Value))
                        cenBase.Classes.ChungTu.inChungTu(IDDanhMucChungTu.ToString(), ugChiTiet.ActiveRow.Cells["ID"].Value.ToString(), ctDonHang.tableName, "", this.MdiParent, false, cenCommon.GlobalVariables.reportPath + @"ctGiayDeNghiTamUngCK.rpt", ctDonHang.listIDChiTietProcedureName, "", 1, LoaiManHinh, 1);
                    break;
                case "ctGiayBienNhan":
                    if (cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow)) return;
                    if (UpdateMode != cenCommon.ThaoTacDuLieu.Xem) luuChungTu(false);
                    if (!cenCommon.cenCommon.IsNull(ugChiTiet.ActiveRow.Cells["ID"].Value))
                        cenBase.Classes.ChungTu.inChungTu(IDDanhMucChungTu.ToString(), ugChiTiet.ActiveRow.Cells["ID"].Value.ToString(), ctDonHang.tableName, "", this.MdiParent, false, cenCommon.GlobalVariables.reportPath + @"ctGiayBienNhan.rpt", ctDonHang.listIDChiTietProcedureName, "", 1, LoaiManHinh, 1);
                    break;
                default:
                    //In phiếu yêu cầu vận chuyển
                    break;
            }
        }
        #endregion FormEvents
    }



}

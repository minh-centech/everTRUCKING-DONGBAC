namespace cenDTO.cenLogistics
{
    public class DanhMucDinhMucChiPhi : BaseDTO
    {
        public object IDDanhMucDonVi { get; set; }
        public object IDDanhMucLoaiDoiTuong { get; set; }
        public object NgayApDung { get; set; }
        public object IDDanhMucTuyenVanTai { get; set; }
        public object MaDanhMucTuyenVanTai { get; set; }
        public object TenDanhMucTuyenVanTai { get; set; }
        public object LoaiTacNghiep { get; set; }
        public object SoLuongNhienLieu { get; set; }
        public object SoTienVeCauDuong { get; set; }
        public object SoTienLuatAnCa { get; set; }
        public object SoTienKetHopVeCauDuongLuatAnCa { get; set; }
        public object SoTienLuuCaKhac { get; set; }
        public object SoTienLuatDuongCam { get; set; }
        public object SoTienTongLuuCaKhacLuatDuongCam { get; set; }
        public object SoTienLuongChuyen { get; set; }
        public object SoTramLuat { get; set; }
        public object SoTramVe { get; set; }
        public object GhiChu { get; set; }
        public object IDDanhMucNguoiSuDungCreate { get; set; }
        public object IDDanhMucNguoiSuDungEdit { get; set; }

        public const string tableName = "DanhMucDinhMucChiPhi";
        public const string listProcedureName = "List_" + tableName;
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;
        public const string getSoTienProcedureName = "Get_" + tableName + "_SoTien";
        public DanhMucDinhMucChiPhi()
        {
            ID = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
            NgayApDung = null;
            IDDanhMucTuyenVanTai = null;
            MaDanhMucTuyenVanTai = null;
            TenDanhMucTuyenVanTai = null;
            SoLuongNhienLieu = null;
            SoTienVeCauDuong = null;
            SoTienLuatAnCa = null;
            SoTienKetHopVeCauDuongLuatAnCa = null;
            SoTienLuuCaKhac = null;
            SoTienLuatDuongCam = null;
            SoTienTongLuuCaKhacLuatDuongCam = null;
            SoTienLuongChuyen = null;
            SoTramLuat = null;
            SoTramVe = null;
            GhiChu = null;
            IDDanhMucNguoiSuDungCreate = null;
            IDDanhMucNguoiSuDungEdit = null;
            CreateDate = null;
            EditDate = null;
        }
    }
    public class DanhMucDinhMucChiPhiXe : BaseDTO
    {
        public object IDDanhMucDonVi { get; set; }
        public object IDDanhMucLoaiDoiTuong { get; set; }
        public object IDDanhMucDinhMucChiPhi { get; set; }
        public object IDDanhMucXe { get; set; }
        public object BienSo { get; set; }
        public object GhiChu { get; set; }
        public object IDDanhMucNguoiSuDungCreate { get; set; }
        public object IDDanhMucNguoiSuDungEdit { get; set; }

        public const string tableName = "DanhMucDinhMucChiPhiXe";
        public const string listProcedureName = "List_" + tableName;
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;

        public DanhMucDinhMucChiPhiXe()
        {
            ID = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
            IDDanhMucDinhMucChiPhi = null;
            IDDanhMucXe = null;
            GhiChu = null;
            IDDanhMucNguoiSuDungCreate = null;
            IDDanhMucNguoiSuDungEdit = null;
            CreateDate = null;
            EditDate = null;
        }
    }
}

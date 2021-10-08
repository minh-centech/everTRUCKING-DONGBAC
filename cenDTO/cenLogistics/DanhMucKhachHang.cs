namespace cenDTO.cenLogistics
{
    public class DanhMucKhachHang : BaseDTO
    {
        public object IDDanhMucDonVi { get; set; }
        public object IDDanhMucLoaiDoiTuong { get; set; }
        public object Ma { get; set; }
        public object Ten { get; set; }
        public object IDDanhMucNhanSu { get; set; }
        public object TenDanhMucNhanSu { get; set; }
        public object DiaChi { get; set; }
        public object SoDienThoai { get; set; }
        public object SoFax { get; set; }
        public object Email { get; set; }
        public object MaSoThue { get; set; }
        public object TenNganHang { get; set; }
        public object SoTaiKhoan { get; set; }
        public object NguoiDaiDien { get; set; }
        public object NguoiGiaoNhan { get; set; }
        public object SoDienThoaiGiaoNhan { get; set; }
        public object GhiChu { get; set; }
        public object IDDanhMucNguoiSuDungCreate { get; set; }
        public object IDDanhMucNguoiSuDungEdit { get; set; }
        public const string tableName = "DanhMucKhachHang";
        public const string listProcedureName = "List_" + tableName;
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;
        public const string validProcedureName = "List_" + tableName + "_Valid";
        public DanhMucKhachHang()
        {
            ID = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
            Ma = null;
            Ten = null;
            IDDanhMucNhanSu = null;
            TenDanhMucNhanSu = null;
            DiaChi = null;
            SoDienThoai = null;
            SoFax = null;
            Email = null;
            MaSoThue = null;
            TenNganHang = null;
            SoTaiKhoan = null;
            NguoiDaiDien = null;
            NguoiGiaoNhan = null;
            SoDienThoaiGiaoNhan = null;
            GhiChu = null;
            IDDanhMucNguoiSuDungCreate = null;
            IDDanhMucNguoiSuDungEdit = null;
            CreateDate = null;
            EditDate = null;
        }
    }
}

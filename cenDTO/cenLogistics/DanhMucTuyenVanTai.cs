namespace cenDTO.cenLogistics
{
    public class DanhMucTuyenVanTai : BaseDTO
    {
        public object IDDanhMucDonVi { get; set; }
        public object IDDanhMucLoaiDoiTuong { get; set; }
        public object Ma { get; set; }
        public object Ten { get; set; }
        public object DiemDau { get; set; }
        public object IDDanhMucTinhThanhDau { get; set; }
        public object MaDanhMucTinhThanhDau { get; set; }
        public object TenDanhMucTinhThanhDau { get; set; }
        public object DiemCuoi { get; set; }
        public object IDDanhMucTinhThanhCuoi { get; set; }
        public object MaDanhMucTinhThanhCuoi { get; set; }
        public object TenDanhMucTinhThanhCuoi { get; set; }
        public object CuLy { get; set; }
        public object GhiChu { get; set; }
        public object IDDanhMucNguoiSuDungCreate { get; set; }
        public object IDDanhMucNguoiSuDungEdit { get; set; }

        public const string tableName = "DanhMucTuyenVanTai";
        public const string listProcedureName = "List_" + tableName;
        public const string validProcedureName = "List_" + tableName + "_byID";
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;

        public DanhMucTuyenVanTai()
        {
            ID = null;
            IDDanhMucDonVi = null;
            IDDanhMucLoaiDoiTuong = null;
            Ma = null;
            Ten = null;
            DiemDau = null;
            IDDanhMucTinhThanhDau = null;
            MaDanhMucTinhThanhDau = null;
            TenDanhMucTinhThanhDau = null;
            DiemCuoi = null;
            IDDanhMucTinhThanhCuoi = null;
            MaDanhMucTinhThanhCuoi = null;
            TenDanhMucTinhThanhCuoi = null;
            CuLy = null;
            GhiChu = null;
            IDDanhMucNguoiSuDungCreate = null;
            IDDanhMucNguoiSuDungEdit = null;
            CreateDate = null;
            EditDate = null;
        }
    }
}

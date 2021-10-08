using System.Data;
namespace cenDTO.cenLogistics
{
    public class ctThanhToanTamUng : ctDTO
    {
        public object IDChungTu { get; set; }
        public object IDChungTuChiTiet { get; set; }
        public object LoaiThanhToanTamUng { get; set; }
        public object IDDanhMucCanBoThanhToanTamUng { get; set; }
        public object MaDanhMucCanBoThanhToanTamUng { get; set; }
        public object TenDanhMucCanBoThanhToanTamUng { get; set; }
        public object NgayThanhToanTamUng { get; set; }
        public object SoTienHoanTamUng { get; set; }
        public object SoTienPhiHaTang { get; set; }
        public object SoTienLocalCharge { get; set; }
        public object SoTienLuuBai { get; set; }
        public object SoTienNangHa { get; set; }
        public object SoTienCuocVo { get; set; }
        public object SoTienHaiQuan { get; set; }
        public object SoTienLamHang { get; set; }
        public object SoTienChonVo { get; set; }
        public object SoTienChiKhac { get; set; }
        public object SoHoaDon { get; set; }
        public DataRowState DataRowState { get; set; }


        public const string tableName = "ctThanhToanTamUng";
        public const string listProcedureName = "List_" + tableName;
        public const string listDisplayProcedureName = "List_" + tableName + "_Display";
        public const string listDeNghiThanhToanHoanUngProcedureName = "List_" + tableName + "_DeNghiThanhToanHoanUng";
        public const string listDeNghiThanhToanProcedureName = "List_" + tableName + "_DeNghiThanhToan";
        public const string listDeNghiThanhToanGuiKhachHangProcedureName = "List_" + tableName + "_DeNghiThanhToanGuiKhachHang";
        public const string insertProcedureName = "Insert_" + tableName;
        public const string updateProcedureName = "Update_" + tableName;
        public const string deleteProcedureName = "Delete_" + tableName;

        //
        public ctThanhToanTamUng()
        {
            ID = null;
            IDDanhMucDonVi = null;
            IDDanhMucChungTu = null;
            IDDanhMucChungTuTrangThai = null;
            So = null;
            NgayLap = null;
            //
            IDChungTu = null;
            IDChungTuChiTiet = null;
            LoaiThanhToanTamUng = null;
            IDDanhMucCanBoThanhToanTamUng = null;
            MaDanhMucCanBoThanhToanTamUng = null;
            TenDanhMucCanBoThanhToanTamUng = null;
            NgayThanhToanTamUng = null;
            SoTienHoanTamUng = null;
            SoTienPhiHaTang = null;
            SoTienLocalCharge = null;
            SoTienLuuBai = null;
            SoTienNangHa = null;
            SoTienCuocVo = null;
            SoTienHaiQuan = null;
            SoTienLamHang = null;
            SoTienChonVo = null;
            SoTienChiKhac = null;
            SoHoaDon = null;
            DataRowState = DataRowState.Unchanged;
            //
            IDDanhMucNguoiSuDungCreate = null;
            MaDanhMucNguoiSuDungCreate = null;
            TenDanhMucNguoiSuDungCreate = null;
            CreateDate = null;
            IDDanhMucNguoiSuDungEdit = null;
            MaDanhMucNguoiSuDungEdit = null;
            TenDanhMucNguoiSuDungEdit = null;
            EditDate = null;
        }
    }
}

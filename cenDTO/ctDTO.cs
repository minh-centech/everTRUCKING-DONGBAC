namespace cenDTO
{
    public class ctDTO
    {
        //Properties

        public object ID { set; get; }
        public object IDDanhMucDonVi { set; get; }
        public object IDDanhMucChungTu { set; get; }
        public object IDDanhMucChungTuTrangThai { get; set; }

        public object So { get; set; }
        public object NgayLap { get; set; }

        public object GhiChu { get; set; }
        public object IDDanhMucNguoiSuDungCreate { set; get; }
        public object MaDanhMucNguoiSuDungCreate { set; get; }
        public object TenDanhMucNguoiSuDungCreate { set; get; }
        public object CreateDate;
        public object IDDanhMucNguoiSuDungEdit { set; get; }
        public object MaDanhMucNguoiSuDungEdit { set; get; }
        public object TenDanhMucNguoiSuDungEdit { set; get; }
        public object EditDate;
        public object IDDanhMucNguoiSuDungDelete { set; get; }
        public object MaDanhMucNguoiSuDungDelete { set; get; }
        public object TenDanhMucNguoiSuDungDelete { set; get; }
        public object DeleteDate;
    }
}

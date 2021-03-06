---------------
alter procedure List_ctKeHoachVanTai
	@TuNgay						date = null,
	@DenNgay					date = null
as
begin
	set nocount on;
	--
	if @TuNgay is null set @TuNgay = '2020-01-01';
	if @DenNgay is null set @DenNgay = '2030-01-01';
	--
	create table #ChungTu
	(
		ID								bigint,
		DebitNote						nvarchar(128),
		IDDanhMucNhomHangVanChuyen		bigint,
		Tuyen							nvarchar(255),
		HangHoa							nvarchar(128),
		SoXe							nvarchar(128),
		ChuXe							nvarchar(128),
		TenDanhMucDiaDiemLayContHang	nvarchar(255),
		TenDanhMucDiaDiemTraContHang	nvarchar(255),
		NgayDongTraHang					datetime,
		GioDongTraHang					nvarchar(5),
		GioThucHien						nvarchar(5),
		TenDanhMucHangTau				nvarchar(255),
		TrangThaiDonHang				nvarchar(255),
		GhiChu							nvarchar(max),
		IDDanhMucNguoiSuDungCreate		bigint,
		MaDanhMucNguoiSuDungCreate		nvarchar(128),
		CreateDate						datetime,
		IDDanhMucNguoiSuDungEdit		bigint,
		MaDanhMucNguoiSuDungEdit		nvarchar(128),
		EditDate						datetime
	);
	create table #Total
	(
		Total	int,
		Cont	int,
		Truck	int
	);
	--
	insert into #ChungTu
	select 
		a.ID,
		a.DebitNote,
		a.IDDanhMucNhomHangVanChuyen,
		TuyenVanTai.Ten,
		HangHoa.Ma,
		Xe.BienSo,
		ThauPhu.Ma,
		DiaDiemLayContHang.Ten,
		DiaDiemTraContHang.Ten,
		isnull(a.NgayDongHang, a.NgayTraHang) NgayDongTraHang,
		isnull(a.GioDongHang, a.GioTraHang) GioDongTraHang,
		ctHotline.GioThucHien,
		HangTau.Ten,
		case when ctDieuHanh.TrangThaiDonHang = 1 or ctDieuHanh.TrangThaiDonHang is null then N'Đơn' when ctDieuHanh.TrangThaiDonHang = 2 then N'Kẹp' when ctDieuHanh.TrangThaiDonHang = 3 then N'1h-1v/1v-1h' when ctDieuHanh.TrangThaiDonHang = 4 then N'Nhánh' else N'Kết hợp' end TrangThaiDonHang,
		a.GhiChu,
		a.IDDanhMucNguoiSuDungCreate,
		UserCreate.Ma MaDanhMucNguoiSuDungCreate,
		a.CreateDate,
		a.IDDanhMucNguoiSuDungEdit,
		UserEdit.Ma MaDanhMucNguoiSuDungEdit,
		a.EditDate
	from ctDonHang a
		left join ctHotline on a.ID = ctHotline.IDChungTu
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucHangHoa HangHoa on a.IDDanhMucHangHoa = HangHoa.ID
		left join DanhMucThauPhu ThauPhu on ctDieuHanh.IDDanhMucThauPhu = ThauPhu.ID
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucDoITuong NhomHangVanChuyen on a.IDDanhMucNhomHangVanChuyen = NhomHangVanChuyen.ID
		left join DanhMucDoiTuong HangTau on a.IDDanhMucHangTau = HangTau.ID
		left join DanhMucDiaDiemGiaoNhan DiaDiemTraContHang on a.IDDanhMucDiaDiemTraContHang = DiaDiemTraContHang.ID
		left join DanhMucDiaDiemGiaoNhan DiaDiemLayContHang on a.IDDanhMucDiaDiemLayContHang = DiaDiemLayContHang.ID
		left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
		left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where a.Huy is null and cast(isnull(a.NgayDongHang, a.NgayTraHang) as date) >= @TuNgay and cast(isnull(a.NgayDongHang, a.NgayTraHang) as date) <= @DenNgay order by NhomHangVanChuyen.Ma, a.DebitNote, isnull(a.NgayDongHang, a.NgayTraHang);
	--
	insert into #Total
	(
		Total,
		Cont,
		Truck
	)
	values
	(
		(select Count(ID) from #ChungTu),
		(select Count(ID) from #ChungTu where IDDanhMucNhomHangVanChuyen = 2007),
		(select Count(ID) from #ChungTu where IDDanhMucNhomHangVanChuyen <> 2007)
	);
	--
	select * from #ChungTu;
	select * from #Total;
	--
	drop table #ChungTu;
	drop table #Total;
end;
go

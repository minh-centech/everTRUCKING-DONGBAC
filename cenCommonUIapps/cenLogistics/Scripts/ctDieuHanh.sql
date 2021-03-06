/*
create table ctDieuHanh
(
	ID								bigint not null,
	IDDanhMucDonVi					bigint not null,
	IDDanhMucChungTu				bigint not null,
	--
	IDChungTu						bigint not null, --ID đơn hàng
	IDDanhMucThauPhu				bigint,
	IDDanhMucXe						bigint,
	BienSoXeNgoai					nvarchar(128),
	IDDanhMucTaiXe					bigint,
	DienThoai						nvarchar(128),
	TrangThaiDonHang				tinyint, --1: đơn, 2 kẹp, 3: 1h-1v/1v-1h, 4: kết hợp
	SoDonHangKetHop					nvarchar(255),
	GhiChu							nvarchar(255),
	--
	IDDanhMucNguoiSuDungCreate		bigint	not null,
	CreateDate						datetime not null,
	IDDanhMucNguoiSuDungEdit		bigint,
	EditDate						datetime,
	constraint PK_ctDieuHanh primary key (ID),
	constraint ctDieuHanh_AutoID foreign key (ID) references AutoID(ID),
	constraint DonVi_ctDieuHanh foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint DanhMucChungTu_ctDieuHanh foreign key (IDDanhMucChungTu) references DanhMucChungTu(ID),
	constraint ChungTu_ctDieuHanh foreign key (IDChungTu) references ctDonHang(ID),
	constraint unq_IDChungTu_ctDieuHanh unique(IDChungTu),
	--
	constraint DanhMucThauPhu_ctDieuHanh foreign key (IDDanhMucThauPhu) references DanhMucThauPhu(ID),
	constraint DanhMucXe_ctDieuHanh foreign key (IDDanhMucXe) references DanhMucXe(ID),
	constraint DanhMucTaiXe_ctDieuHanh foreign key (IDDanhMucTaiXe) references DanhMucTaiXe(ID),
	--
	constraint UserCreate_ctDieuHanh foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint UserEdit_ctDieuHanh foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
*/
---------------
alter procedure List_ctDieuHanh_Display
	@IDDanhMucDonVi				bigint,
	@IDDanhMucChungTu			bigint,
	@ID							bigint = null,
	@TuNgay						date = null,
	@DenNgay					date = null,
	@IDDanhMucNhomHangVanChuyen	bigint = null
as
begin
	set nocount on;
	--
	if @TuNgay is null set @TuNgay = '2020-01-01';
	if @DenNgay is null set @DenNgay = '2030-01-01';
	select 
		a.ID,
		ctDieuHanh.ID IDctDieuHanh,
		a.So,
		a.NgayLap,
		a.IDDanhMucChungTuTrangThai,
		case when a.LoaiHang = 1 then a.NgayTraHang else a.NgayDongHang end NgayDongTraHang,
		case when a.LoaiHang = 1 then a.GioTraHang else a.GioDongHang end GioDongTraHang,
		a.DebitNote,
		KhachHang.Ma MaDanhMucKhachHang,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		a.GhiChu GhiChuCongViec,
		convert(nvarchar(10), a.NgayCatMang, 103) NgayCatMang,
		a.GioCatMang,
		a.LoaiHang,
		case when a.LoaiHang = 1 then N'Nhập' when a.LoaiHang = 2 then N'Xuất' else N'Nội địa' end TenLoaiHang,
		HangHoa.Ma MaDanhMucHangHoa,
		case when ctDieuHanh.TrangThaiDonHang = 1 or ctDieuHanh.TrangThaiDonHang is null then N'Đơn' when ctDieuHanh.TrangThaiDonHang = 2 then N'Kẹp' when ctDieuHanh.TrangThaiDonHang = 3 then N'1h-1v/1v-1h' when ctDieuHanh.TrangThaiDonHang = 4 then N'Kết hợp' else N'Nhánh' end TrangThaiDonHang,
		isnull(stuff((select ';' + ctDonHangKetHop.So from ctDieuHanhChiTietDonHangKetHop ctKetHop left join ctDonHang ctDonHangKetHop on ctKetHop.IDctDonHang = ctDonHangKetHop.ID where ctKetHop.IDChungTu = a.ID for xml path('')), 1, 1, ''), stuff((select ';' + ctDonHangKetHop.So from ctDieuHanhChiTietDonHangKetHop ctKetHop left join ctDonHang ctDonHangKetHop on ctKetHop.IDChungTu = ctDonHangKetHop.ID where ctKetHop.IDctDonHang = a.ID for xml path('')), 1, 1, '')) DanhSachDonHangKetHop,
		a.KhoiLuong,
		a.TheTich,
		ThauPhu.ID IDDanhMucThauPhu,
		ThauPhu.Ma MaDanhMucThauPhu,
		Xe.ID IDDanhMucXe,
		isnull(Xe.BienSo, ctDieuHanh.BienSoXeNgoai) BienSo,
		TaiXe.ID IDDanhMucTaiXe,
		TaiXe.Ten TenDanhMucTaiXe,
		ctDieuHanh.GhiChu GhiChuDieuHanh,
		ctDieuHanh.DienThoai,
		--ctHotline.TinhTrangHotline, --TinhTrangHotline,
		--ctHotline.GhiChuHotline, --GhiChuHotline,
		a.SoContainer,
		DiaDiemLayCont.Ten TenDanhMucDiaDiemLayCont,
		DiaDiemTraCont.Ten TenDanhMucDiaDiemTraCont,
		a.SoTienCuoc,
		ctDieuHanh.IDDanhMucNguoiSuDungCreate,
		ctDieuHanh.CreateDate,
		ctDieuHanh.IDDanhMucNguoiSuDungEdit,
		ctDieuHanh.EditDate
	from ctDonHang a
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		--left join ctHotline on a.ID = ctHotline.IDChungTu
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucThauPhu ThauPhu on ctDieuHanh.IDDanhMucThauPhu = ThauPhu.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join DanhMucHangHoa HangHoa on a.IDDanhMucHangHoa = HangHoa.ID
		left join DanhMucDoiTuong NhomHangVanChuyen on a.IDDanhMucNhomHangVanChuyen = NhomHangVanChuyen.ID
		left join DanhMucDoiTuong DiaDiemLayCont on a.IDDanhMucDiaDiemLayCont = DiaDiemLayCont.ID
		left join DanhMucDoiTuong DiaDiemTraCont on a.IDDanhMucDiaDiemTraCont = DiaDiemTraCont.ID
		left join DanhMucNguoiSuDung UserCreate on ctDieuHanh.IDDanhMucNguoiSuDungCreate = UserCreate.ID
		left join DanhMucNguoiSuDung UserEdit on ctDieuHanh.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu and a.Huy is null
		and case when @ID is not null then a.ID else -1 end = isnull(@ID, -1)
		and case when @IDDanhMucNhomHangVanChuyen is not null then a.IDDanhMucNhomHangVanChuyen else -1 end = isnull(@IDDanhMucNhomHangVanChuyen, -1)
		and a.Huy is null
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay order by NhomHangVanChuyen.Ma, a.DebitNote, isnull(a.NgayDongHang, a.NgayTraHang);
end;
go
---------------
alter procedure List_ctDieuHanh
	@IDDanhMucDonVi		bigint,
	@IDDanhMucChungTu	bigint,
	@ID					bigint
as
begin
	set nocount on;
	--
	select 
		a.ID,
		ctDieuHanh.ID IDctDieuHanh,
		a.So,
		a.NgayLap,
		a.IDDanhMucChungTuTrangThai,
		isnull(a.NgayDongHang, a.NgayTraHang) NgayDongTraHang,
		isnull(a.GioDongHang, a.GioTraHang) GioDongTraHang,
		a.DebitNote,
		KhachHang.Ma MaDanhMucKhachHang,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		convert(nvarchar(10), a.NgayCatMang, 103) NgayCatMang,
		a.GioCatMang,
		a.LoaiHang,
		case when a.LoaiHang = 1 then N'Nhập' when a.LoaiHang = 2 then N'Xuất' else N'Nội địa' end TenLoaiHang,
		HangHoa.Ma MaDanhMucHangHoa,
		a.KhoiLuong,
		--ctHotline.TinhTrangHotline, --TinhTrangHotline,
		--ctHotline.GhiChuHotline, --GhiChuHotline,
		ThauPhu.ID IDDanhMucThauPhu,
		ThauPhu.Ma MaDanhMucThauPhu,
		Xe.ID IDDanhMucXe,
		Xe.BienSo,
		ctDieuHanh.BienSoXeNgoai,
		TaiXe.ID IDDanhMucTaiXe,
		TaiXe.Ten TenDanhMucTaiXe,
		ctDieuHanh.GhiChu GhiChuDieuHanh,
		ctDieuHanh.DienThoai,
		a.GhiChu GhiChuCongViec,
		a.SoContainer,
		DiaDiemLayCont.Ten TenDanhMucDiaDiemLayCont,
		DiaDiemTraCont.Ten TenDanhMucDiaDiemTraCont,
		a.SoTienCuoc
	from ctDonHang a
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		--left join ctHotline on a.ID = ctHotline.IDChungTu
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucThauPhu ThauPhu on ctDieuHanh.IDDanhMucThauPhu = ThauPhu.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join DanhMucHangHoa HangHoa on a.IDDanhMucHangHoa = HangHoa.ID
		left join DanhMucDoiTuong NhomHangVanChuyen on a.IDDanhMucNhomHangVanChuyen = NhomHangVanChuyen.ID
		left join DanhMucDoiTuong DiaDiemLayCont on a.IDDanhMucDiaDiemLayCont = DiaDiemLayCont.ID
		left join DanhMucDoiTuong DiaDiemTraCont on a.IDDanhMucDiaDiemTraCont = DiaDiemTraCont.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu and a.Huy is null and a.ID = @ID;
	--
	select * from #ChungTu;
	--
	drop table #ChungTu;
end;
go
---------------
alter procedure List_ctDieuHanh2
	@IDDanhMucDonVi		bigint,
	@IDDanhMucChungTu	bigint,
	@ID					bigint
as
begin
	set nocount on;
	--
	select 
		a.ID,
		ctDieuHanh.ID IDctDieuHanh,
		a.So,
		a.NgayLap,
		a.IDDanhMucChungTuTrangThai,
		KhachHang.Ma MaDanhMucKhachHang,
		KhachHang.Ten TenDanhMucKhachHang,
		a.SoTienCuoc,
		a.DebitNote,
		case when a.LoaiHang = 1 then N'Nhập' when a.LoaiHang = 2 then N'Xuất' else N'Nội địa' end TenLoaiHang,
		a.BillBooking,
		a.IDDanhMucNhomHangVanChuyen,
		NhomHangVanChuyen.Ma MaDanhMucNhomHangVanChuyen,
		HangHoa.Ma MaDanhMucHangHoa,
		a.KhoiLuong,
		a.SoContainer,
		DiaDiemLayCont.Ten TenDanhMucDiaDiemLayCont,
		DiaDiemTraCont.Ten TenDanhMucDiaDiemTraCont,
		a.NgayDongHang,
		a.GioDongHang GioDongHang,
		a.NgayTraHang,
		a.GioTraHang,
		DiaDiemLayHang.Ten TenDanhMucDiaDiemLayHang,
		DiaDiemTraHang.Ten TenDanhMucDiaDiemTraHang,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		NgayCatMang,
		a.GioCatMang,
		a.NguoiGiaoNhan,
		a.SoDienThoaiGiaoNhan,
		a.GhiChu GhiChuCongViec,
		ThauPhu.ID IDDanhMucThauPhu,
		ThauPhu.Ma MaDanhMucThauPhu,
		ThauPhu.Ten TenDanhMucThauPhu,
		ThauPhu.MaSoThueCMND MaSoThueCMNDThauPhu,
		Xe.ID IDDanhMucXe,
		ctDieuHanh.BienSoXeNgoai,
		TaiXe.ID IDDanhMucTaiXe,
		TaiXe.Ten TenDanhMucTaiXe,
		ctDieuHanh.DienThoai,
		TaiXe.SoCMND,
		ctDieuHanh.TrangThaiDonHang,
		ctDieuHanh.SoDonHangKetHop,
		ctDieuHanh.GhiChu
	from ctDonHang a
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		--left join ctHotline on a.ID = ctHotline.IDChungTu
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucDoiTuong NhomHangVanChuyen on a.IDDanhMucNhomHangVanChuyen = NhomHangVanChuyen.ID
		left join DanhMucHangHoa HangHoa on a.IDDanhMucHangHoa = HangHoa.ID
		left join DanhMucDoiTuong DiaDiemLayCont on a.IDDanhMucDiaDiemLayCont = DiaDiemLayCont.ID
		left join DanhMucDoiTuong DiaDiemTraCont on a.IDDanhMucDiaDiemTraCont = DiaDiemTraCont.ID

		left join DanhMucDoiTuong DiaDiemLayHang on a.IDDanhMucDiaDiemLayHang = DiaDiemLayHang.ID
		left join DanhMucDoiTuong DiaDiemTraHang on a.IDDanhMucDiaDiemTraHang = DiaDiemTraHang.ID

		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucThauPhu ThauPhu on ctDieuHanh.IDDanhMucThauPhu = ThauPhu.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu and a.Huy is null and a.ID = @ID;
	--
	select * from #ChungTu;
	--
	drop table #ChungTu;
end;
go
------------------
alter procedure Insert_ctDieuHanh
(
	@ID								bigint = null output,
	@IDDanhMucDonVi					bigint,
	@IDDanhMucChungTu				bigint,
	--
	@IDChungTu						bigint, --ID đơn hàng
	@IDDanhMucThauPhu				bigint = null,
	@IDDanhMucXe					bigint = null,
	@BienSoXeNgoai					nvarchar(128) = null,
	@IDDanhMucTaiXe					bigint = null,
	@DienThoai						nvarchar(128) = null,
	@TrangThaiDonHang				tinyint = null,
	@SoDonHangKetHop				nvarchar(255) = null,
	@GhiChu							nvarchar(255) = null,
	--
	@IDDanhMucNguoiSuDungCreate		bigint,
	@CreateDate						datetime = null output
)
as
begin
	set nocount on;
	declare @Err int;
	select @CreateDate = GETDATE();
	begin tran
	begin try
	
	--
	exec Insert_AutoID @ID out, @TenBangDuLieu = N'ctDieuHanh';
	insert	into ctDieuHanh
	(
		ID,
		IDDanhMucDonVi,
		IDDanhMucChungTu,
		--
		IDChungTu,
		IDDanhMucThauPhu,
		IDDanhMucXe,
		BienSoXeNgoai,
		IDDanhMucTaiXe,
		DienThoai,
		TrangThaiDonHang,
		SoDonHangKetHop,
		GhiChu,
		--
		IDDanhMucNguoiSuDungCreate,
		CreateDate
	)
	values
	(
		@ID,
		@IDDanhMucDonVi,
		@IDDanhMucChungTu,
		--
		@IDChungTu,
		@IDDanhMucThauPhu,
		@IDDanhMucXe,
		@BienSoXeNgoai,
		@IDDanhMucTaiXe,
		@DienThoai,
		@TrangThaiDonHang,
		@SoDonHangKetHop,
		@GhiChu,
		--
		@IDDanhMucNguoiSuDungCreate,
		@CreateDate
	)
	--cập nhật vào bảng chi phí vận tải, chỉ cập nhật nếu chưa có
	declare @IDctChiPhiVanTai bigint = null, @IDDanhMucTuyenVanTai bigint = null, @countKetHop int;
	select @IDDanhMucTuyenVanTai = IDDanhMucTuyenVanTai from ctDonHang where ID = @IDChungTu;
	select @IDctChiPhiVanTai = ID from ctChiPhiVanTai where IDChungTu = @IDChungTu;
	--kiểm tra xem đơn có nằm trong danh sách kết hợp hay không
	select @countKetHop = count(ID) from ctDieuHanhChiTietDonHangKetHop where IDctDonHang = @IDChungTu;
	if @countKetHop = 0 and @IDctChiPhiVanTai is null and @IDDanhMucTuyenVanTai is not null and @IDDanhMucXe is not null
	begin
		--lấy các loại tiền theo định mức
		declare @SoLuongNhienLieu					float = null,
				@SoTienVeCauDuong					float = null,
				@SoTienLuatAnCa						float = null,
				@SoTienKetHopVeCauDuongLuatAnCa		float = null,
				@SoTienLuuCaKhac					float = null,
				@SoTienLuatDuongCam					float = null,
				@SoTienTongLuuCaKhacLuatDuongCam	float = null,
				@SoTienLuongChuyen					float = null;
		select	@SoLuongNhienLieu = T.SoLuongNhienLieu,
				@SoTienVeCauDuong = T.SoTienVeCauDuong,
				@SoTienLuatAnCa = T.SoTienLuatAnCa,
				@SoTienKetHopVeCauDuongLuatAnCa = T.SoTienKetHopVeCauDuongLuatAnCa,
				@SoTienLuuCaKhac = T.SoTienLuuCaKhac,
				@SoTienLuatDuongCam = T.SoTienLuatDuongCam,
				@SoTienTongLuuCaKhacLuatDuongCam = T.SoTienTongLuuCaKhacLuatDuongCam,
				@SoTienLuongChuyen = T.SoTienLuongChuyen
		from 
		(
			select top 1 
				a.SoLuongNhienLieu,
				a.SoTienVeCauDuong,
				a.SoTienLuatAnCa,
				a.SoTienKetHopVeCauDuongLuatAnCa,
				a.SoTienLuuCaKhac,
				a.SoTienLuatDuongCam,
				a.SoTienTongLuuCaKhacLuatDuongCam,
				a.SoTienLuongChuyen
			from DanhMucDinhMucChiPhi a left join DanhMucDinhMucChiPhiXe b on a.ID = b.IDDanhMucDinhMucChiPhi
			where a.IDDanhMucTuyenVanTai = @IDDanhMucTuyenVanTai and a.LoaiTacNghiep = @TrangThaiDonHang and b.IDDanhMucXe = @IDDanhMucXe
		) T;
		exec Insert_ctChiPhiVanTai
			@ID									= null,
			@IDDanhMucDonVi						= @IDDanhMucDonVi,
			@IDDanhMucChungTu					= @IDDanhMucChungTu,
			--
			@IDChungTu							= @IDChungTu,
			@SoLuongNhienLieu					= @SoLuongNhienLieu,
			@SoTienVeCauDuong					= @SoTienVeCauDuong,
			@SoTienLuatAnCa						= @SoTienLuatAnCa,
			@SoTienKetHopVeCauDuongLuatAnCa		= @SoTienKetHopVeCauDuongLuatAnCa,
			@SoTienLuuCaKhac					= @SoTienLuuCaKhac,
			@SoTienLuatDuongCam					= @SoTienLuatDuongCam,
			@SoTienTongLuuCaKhacLuatDuongCam	= @SoTienTongLuuCaKhacLuatDuongCam,
			@SoTienLuongChuyen					= @SoTienLuongChuyen,
			@SoTienLuongChuNhat					= null,
			@SoTienCuocThueXeNgoai				= null,
			@IDDanhMucCanBoTamUng				= null,
			@ThoiHanThanhToan					= null,
			@GhiChu								= null,
			--
			@IDDanhMucNguoiSuDungCreate			= @IDDanhMucNguoiSuDungCreate,
			@CreateDate							= null;
	end;
	commit tran
	end try
	begin catch
		rollback
		declare @ErrMsg nvarchar(max)
		select @ErrMsg = error_message()
		raiserror(@ErrMsg, 16, 1)
	end catch
	set @Err = @@Error
	return @Err
end
go
--
alter procedure Update_ctDieuHanh
(
	@ID							bigint,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucChungTu			bigint,
	--
	@IDChungTu					bigint, --ID đơn hàng
	@IDDanhMucThauPhu			bigint = null,
	@IDDanhMucXe				bigint = null,
	@BienSoXeNgoai				nvarchar(128) = null,
	@IDDanhMucTaiXe				bigint = null,
	@DienThoai					nvarchar(128) = null,
	@TrangThaiDonHang			tinyint = null,
	@SoDonHangKetHop			nvarchar(255) = null,
	@GhiChu						nvarchar(255) = null,
	--
	@IDDanhMucNguoiSuDungEdit	bigint,
	@EditDate					datetime = null output
)
as
begin
	set nocount on;
	declare @Err int;
	select @EditDate = GETDATE();

	declare @countID bigint = null;
	
	--exec Check_ForeignKey N'ctChiPhiVanTai', N'IDChungTu', @IDChungTu, N'Đơn hàng này đã có phát sinh chi phí vận tải, không sửa được!', @countID out;
	--if (@countID > 0)
	--	return;

	--exec Check_ForeignKey N'ctChiPhiVanTaiBoSung', N'IDChungTu', @IDChungTu, N'Đơn hàng này đã có phát sinh chi phí vận tải bổ sung, không sửa được!', @countID out;
	--if (@countID > 0)
	--	return;

	begin tran
	begin try
	update ctDieuHanh
		set
			IDChungTu = @IDChungTu,
			IDDanhMucThauPhu = @IDDanhMucThauPhu,
			IDDanhMucXe = @IDDanhMucXe,
			BienSoXeNgoai = @BienSoXeNgoai,
			IDDanhMucTaiXe = @IDDanhMucTaiXe,
			DienThoai = @DienThoai,
			TrangThaiDonHang = @TrangThaiDonHang,
			SoDonHangKetHop = @SoDonHangKetHop,
			GhiChu = @GhiChu,
			--
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @EditDate
		where ID = @ID;
	--cập nhật vào bảng chi phí vận tải, chỉ cập nhật nếu chưa có
	declare @IDctChiPhiVanTai bigint = null, @IDDanhMucTuyenVanTai bigint = null, @countKetHop int;
	select @IDDanhMucTuyenVanTai = IDDanhMucTuyenVanTai from ctDonHang where ID = @IDChungTu;
	select @IDctChiPhiVanTai = ID from ctChiPhiVanTai where IDChungTu = @IDChungTu;
	--kiểm tra xem đơn có nằm trong danh sách kết hợp hay không
	select @countKetHop = count(ID) from ctDieuHanhChiTietDonHangKetHop where IDctDonHang = @IDChungTu;
	--xóa chi phí cũ cập nhật
	if @countKetHop = 0 and @IDctChiPhiVanTai is null and @IDDanhMucTuyenVanTai is not null and @IDDanhMucXe is not null
	begin
		--lấy các loại tiền theo định mức
		declare @SoLuongNhienLieu					float = null,
				@SoTienVeCauDuong					float = null,
				@SoTienLuatAnCa						float = null,
				@SoTienKetHopVeCauDuongLuatAnCa		float = null,
				@SoTienLuuCaKhac					float = null,
				@SoTienLuatDuongCam					float = null,
				@SoTienTongLuuCaKhacLuatDuongCam	float = null,
				@SoTienLuongChuyen					float = null;
		select	@SoLuongNhienLieu = T.SoLuongNhienLieu,
				@SoTienVeCauDuong = T.SoTienVeCauDuong,
				@SoTienLuatAnCa = T.SoTienLuatAnCa,
				@SoTienKetHopVeCauDuongLuatAnCa = T.SoTienKetHopVeCauDuongLuatAnCa,
				@SoTienLuuCaKhac = T.SoTienLuuCaKhac,
				@SoTienLuatDuongCam = T.SoTienLuatDuongCam,
				@SoTienTongLuuCaKhacLuatDuongCam = T.SoTienTongLuuCaKhacLuatDuongCam,
				@SoTienLuongChuyen = T.SoTienLuongChuyen
		from 
		(
			select top 1 
				a.SoLuongNhienLieu,
				a.SoTienVeCauDuong,
				a.SoTienLuatAnCa,
				a.SoTienKetHopVeCauDuongLuatAnCa,
				a.SoTienLuuCaKhac,
				a.SoTienLuatDuongCam,
				a.SoTienTongLuuCaKhacLuatDuongCam,
				a.SoTienLuongChuyen
			from DanhMucDinhMucChiPhi a left join DanhMucDinhMucChiPhiXe b on a.ID = b.IDDanhMucDinhMucChiPhi
			where a.IDDanhMucTuyenVanTai = @IDDanhMucTuyenVanTai and a.LoaiTacNghiep = @TrangThaiDonHang and b.IDDanhMucXe = @IDDanhMucXe
		) T;
		exec Insert_ctChiPhiVanTai
			@ID									= null,
			@IDDanhMucDonVi						= @IDDanhMucDonVi,
			@IDDanhMucChungTu					= @IDDanhMucChungTu,
			--
			@IDChungTu							= @IDChungTu,
			@SoLuongNhienLieu					= @SoLuongNhienLieu,
			@SoTienVeCauDuong					= @SoTienVeCauDuong,
			@SoTienLuatAnCa						= @SoTienLuatAnCa,
			@SoTienKetHopVeCauDuongLuatAnCa		= @SoTienKetHopVeCauDuongLuatAnCa,
			@SoTienLuuCaKhac					= @SoTienLuuCaKhac,
			@SoTienLuatDuongCam					= @SoTienLuatDuongCam,
			@SoTienTongLuuCaKhacLuatDuongCam	= @SoTienTongLuuCaKhacLuatDuongCam,
			@SoTienLuongChuyen					= @SoTienLuongChuyen,
			@SoTienLuongChuNhat					= null,
			@SoTienCuocThueXeNgoai				= null,
			@IDDanhMucCanBoTamUng				= null,
			@ThoiHanThanhToan					= null,
			@GhiChu								= null,
			--
			@IDDanhMucNguoiSuDungCreate			= @IDDanhMucNguoiSuDungEdit,
			@CreateDate							= null;
	end;
	commit tran
	end try
	begin catch
		rollback
		declare @ErrMsg nvarchar(max)
		select @ErrMsg = error_message()
		raiserror(@ErrMsg, 16, 1)
	end catch
	set @Err = @@Error
	return @Err
end
go
--
alter procedure Delete_ctDieuHanh
(
	@ID	bigint
)
as
begin
	set nocount on;
	declare @Err int;
	declare @NgayCapNhat datetime;
	select @NgayCapNhat = GETDATE()
	begin tran
	begin try

	declare @countID bigint = null, @IDChungTu bigint = null;
	select @IDChungTu = IDChungTu from ctDieuHanh where ID = @ID;
	exec Check_ForeignKey N'ctChiPhiVanTai', N'IDChungTu', @IDChungTu, N'Đơn hàng này đã có phát sinh chi phí vận tải, không xóa được!', @countID out;
	if (@countID > 0)
		return;
	exec Check_ForeignKey N'ctChiPhiVanTaiBoSung', N'IDChungTu', @IDChungTu, N'Đơn hàng này đã có phát sinh chi phí vận tải bổ sung, không xóa được!', @countID out;
	if (@countID > 0)
		return;

	declare @IDChiTiet bigint;
	--
	declare curCT cursor for select ID from ctDieuHanhChiTietDonHangKetHop where IDChungTuChiTiet = @ID;
	open curCT;
	fetch next from curCT into @IDChiTiet
	while @@FETCH_STATUS = 0
	begin
		delete from ctDieuHanhChiTietDonHangKetHop where ID = @IDChiTiet;
		delete from AutoID where ID = @IDChiTiet;
		fetch next from curCT into @IDChiTiet;
	end;
	deallocate curCT;

	--
	delete from ctDieuHanh where ID = @ID;
	delete from AutoID where ID = @ID;
	commit tran
	end try
	begin catch
		rollback
		declare @ErrMsg nvarchar(max)
		select @ErrMsg = error_message()
		raiserror(@ErrMsg, 16, 1)
	end catch
	set @Err = @@Error
	return @Err
end
go
------------------
alter procedure [dbo].[List_ctDieuHanh_ChiTietDonHangChinh]
	@IDDanhMucDonVi		bigint,
	@ID					bigint,
	@countDonHangChinh	int = 0 out
as
begin
	set nocount on;
	select @countDonHangChinh = count(ID) from ctDieuHanhChiTietDonHangKetHop where IDDanhMucDonVi = @IDDanhMucDonVi and IDctDonHang = @ID;
end;
------------------


/*
create table ctInGiayVanTai
(
	ID								bigint not null,
	IDDanhMucDonVi					bigint not null,
	IDDanhMucChungTu				bigint not null,
	--
	IDChungTu						bigint not null, --ID đơn hàng
	LoaiDonChuyen					tinyint not null, --0: Không chuyển, 1: Đơn chuyển, hàng nhẹ, 2: Đơn chuyển, hàng nặng
	SoKmVoHangNhe					float,
	SoKmHangNhe						float,
	SoKmVoHangNang					float,
	SoKmHangNang					float,
	SoLuongNhienLieuDinhMuc			float,
	SoLuongNhienLieuCapThem			float,
	LyDoCapThem						nvarchar(255),
	SoTienVeCauDuong				float,
	SoTienLuatAnCa					float,
	GhiChu							nvarchar(512),
	--
	IDDanhMucNguoiSuDungCreate		bigint	not null,
	CreateDate						datetime not null,
	IDDanhMucNguoiSuDungEdit		bigint,
	EditDate						datetime,
	constraint PK_ctInGiayVanTai primary key (ID),
	constraint ctInGiayVanTai_AutoID foreign key (ID) references AutoID(ID),
	constraint DonVi_ctInGiayVanTai foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint DanhMucChungTu_ctInGiayVanTai foreign key (IDDanhMucChungTu) references DanhMucChungTu(ID),
	constraint ChungTu_ctInGiayVanTai foreign key (IDChungTu) references ctDonHang(ID),
	--
	constraint UserCreate_ctInGiayVanTai foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint UserEdit_ctInGiayVanTai foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
*/
---------------
alter procedure List_ctInGiayVanTai
	@IDDanhMucDonVi		bigint,
	@IDDanhMucChungTu	bigint,
	@ID					bigint = null,
	@TuNgay				date = null,
	@DenNgay			date = null,
	@IDDanhMucChuXe		bigint = null
as
begin
	set nocount on;
	--
	if @TuNgay is null set @TuNgay = '2020-01-01';
	if @DenNgay is null set @DenNgay = '2030-01-01';
	--
	create table #ChungTu
	(
		ID							bigint,
		IDDanhMucDonVi				bigint,
		IDDanhMucChungTu			bigint,
		IDDanhMucChungTuTrangThai	bigint,
		IDctInGiayVanTai			bigint,
		So							nvarchar(35),
		NgayLap						datetime,
		--
		LoaiDonChuyen				tinyint,
		NgayDongTraHang				date,
		GioNhanHang					nvarchar(5),
		NgayThucHien				date,
		GioThucHien					nvarchar(5),
		DebitNote					nvarchar(128),
		BillBooking					nvarchar(128),
		IDDanhMucKhachHang			bigint,
		TenDanhMucKhachHang			nvarchar(255),
		DiaChiDanhMucKhachHang		nvarchar(512),
		BienSo						nvarchar(128),
		TenDanhMucTaiXe				nvarchar(255),
		DienThoai					nvarchar(128),
		TenDanhMucHangHoa			nvarchar(255),
		SoContainer					nvarchar(128),
		TrongLuong					float,
		TenDanhMucChuXe				nvarchar(255),
		TenDanhMucTuyenVanTai		nvarchar(255),
		CuLy						float,
		TinhTrangHotline			nvarchar(255),
		SoKmVoHangNhe				float,
		SoKmHangNhe					float,
		SoKmVoHangNang				float,
		SoKmHangNang				float,
		SoLuongNhienLieuDinhMuc		float,
		SoLuongNhienLieuCapThem		float,
		SoLuongNhienLieu			float,
		LyDoCapThem					nvarchar(max),
		SoTienVeCauDuong			float,
		SoTienLuatAnCa				float,
		SoTienTamUng				float,
		GhiChu						nvarchar(max),
		--
		IDDanhMucNguoiSuDungCreate	bigint,
		MaDanhMucNguoiSuDungCreate	nvarchar(128),
		CreateDate					datetime,
		IDDanhMucNguoiSuDungEdit	bigint,
		MaDanhMucNguoiSuDungEdit	nvarchar(128),
		EditDate					datetime
	);
	--
	insert into #ChungTu
	select 
		a.ID,
		a.IDDanhMucDonVi,
		a.IDDanhMucChungTu,
		a.IDDanhMucChungTuTrangThai,
		ctInGiayVanTai.ID,
		a.So,
		a.NgayLap,
		--
		ctInGiayVanTai.LoaiDonChuyen,
		isnull(a.NgayDongHang, a.NgayTraHang) NgayDongTraHang,
		isnull(a.GioDongHang, a.GioTraHang) GioDongTraHang,
		ctHotline.NgayThucHien,
		ctHotline.GioThucHien,
		a.DebitNote,
		a.BillBooking,
		a.IDDanhMucKhachHang,
		KhachHang.Ten TenDanhMucKhachHang,
		KhachHang.DiaChi DiaChiDanhMucKhachHang,
		Xe.BienSo,
		TaiXe.Ten,
		ctDieuHanh.DienThoai,
		HangHoa.Ten TenDanhMucHangHoa,
		a.SoContainer,
		a.KhoiLuong,
		ChuXe.Ten TenDanhMucChuXe,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		TuyenVanTai.CuLy,
		ctHotline.TinhTrangHotline,
		ctInGiayVanTai.SoKmVoHangNhe,
		ctInGiayVanTai.SoKmHangNhe,
		ctInGiayVanTai.SoKmVoHangNang,
		ctInGiayVanTai.SoKmHangNang,
		ctInGiayVanTai.SoLuongNhienLieuDinhMuc,
		ctInGiayVanTai.SoLuongNhienLieuCapThem,
		isnull(ctInGiayVanTai.SoLuongNhienLieuDinhMuc, 0) + isnull(ctInGiayVanTai.SoLuongNhienLieuCapThem, 0) SoLuongNhienLieu,
		ctInGiayVanTai.LyDoCapThem,
		ctInGiayVanTai.SoTienVeCauDuong,
		ctInGiayVanTai.SoTienLuatAnCa,
		isnull(ctInGiayVanTai.SoTienVeCauDuong, 0) + isnull(ctInGiayVanTai.SoTienLuatAnCa, 0) SoTienTamUng,
		ctInGiayVanTai.GhiChu,
		--
		a.IDDanhMucNguoiSuDungCreate,
		UserCreate.Ma MaDanhMucNguoiSuDungCreate,
		a.CreateDate,
		a.IDDanhMucNguoiSuDungEdit,
		UserEdit.Ma MaDanhMucNguoiSuDungEdit,
		a.EditDate
	from ctDonHang a
		left join ctInGiayVanTai on a.ID = ctInGiayVanTai.IDChungTu
		left join ctHotline on a.ID = ctHotline.IDChungTu
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucHangHoa HangHoa on a.IDDanhMucHangHoa = HangHoa.ID
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join DanhMucDoiTuong NhomHangVanChuyen on a.IDDanhMucNhomHangVanChuyen = NhomHangVanChuyen.ID
		left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
		left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu
		and case when @IDDanhMucChuXe is not null then ctDieuHanh.IDDanhMucChuXe else -1 end = isnull(@IDDanhMucChuXe, -1)
		and case when @ID is not null then a.ID else -1 end = isnull(@ID, -1)
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay order by NhomHangVanChuyen.Ma, a.DebitNote, isnull(a.NgayDongHang, a.NgayTraHang);
	--update số lượng nhiên liệu, tiền vé cầu đường, tiền luật, ăn ca từ chi phí vận tải, chi phí vận tải bổ sung
	--update #ChungTu set
	--	SoLuongNhienLieuDinhMuc = ctChiPhiVanTai.SoLuongNhienLieu,
	--	SoLuongNhienLieu = ctChiPhiVanTai.SoLuongNhienLieu,
	--	SoTienVeCauDuong = ctChiPhiVanTai.SoTienVeCauDuong,
	--	SoTienLuatAnCa = ctChiPhiVanTai.SoTienLuatAnCa,
	--	SoTienTamUng = isnull(ctChiPhiVanTai.SoTienVeCauDuong, 0) + isnull(ctChiPhiVanTai.SoTienLuatAnCa, 0)
	--from #ChungTu inner join ctChiPhiVanTai on #ChungTu.ID = ctChiPhiVanTai.IDChungTu;
	--update #ChungTu set
	--	SoLuongNhienLieuCapThem = isnull(#ChungTu.SoLuongNhienLieuCapThem, 0) + isnull(ctChiPhiVanTaiBoSung.SoLuongNhienLieu, 0),
	--	SoLuongNhienLieu = isnull(#ChungTu.SoLuongNhienLieu, 0) + isnull(ctChiPhiVanTaiBoSung.SoLuongNhienLieu, 0),
	--	SoTienVeCauDuong = isnull(#ChungTu.SoTienVeCauDuong, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienVeCauDuong, 0),
	--	SoTienLuatAnCa = isnull(#ChungTu.SoTienLuatAnCa, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienLuatAnCa, 0),
	--	SoTienTamUng = isnull(#ChungTu.SoTienTamUng, 0) +  isnull(ctChiPhiVanTaiBoSung.SoTienVeCauDuong, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienLuatAnCa, 0)
	--from #ChungTu inner join ctChiPhiVanTaiBoSung on #ChungTu.ID = ctChiPhiVanTaiBoSung.IDChungTu;
	--
	select * from #ChungTu;
	--
	drop table #ChungTu;
end;
go
------------------
alter procedure Insert_ctInGiayVanTai
(
	@ID								bigint = null output,
	@IDDanhMucDonVi					bigint,
	@IDDanhMucChungTu				bigint,
	--
	@IDChungTu						bigint, --ID đơn hàng
	@LoaiDonChuyen					tinyint, --0: Không chuyển, 1: Đơn chuyển, hàng nhẹ, 2: Đơn chuyển, hàng nặng
	@SoKmVoHangNhe					float = null,
	@SoKmHangNhe					float = null,
	@SoKmVoHangNang					float = null,
	@SoKmHangNang					float = null,
	@SoLuongNhienLieuDinhMuc		float = null,
	@SoLuongNhienLieuCapThem		float = null,
	@LyDoCapThem					nvarchar(255) = null,
	@SoTienVeCauDuong				float = null,
	@SoTienLuatAnCa					float = null,
	@GhiChu							nvarchar(512) = null,
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
	exec Insert_AutoID @ID out, @TenBangDuLieu = N'ctInGiayVanTai';
	insert	into ctInGiayVanTai
	(
		ID,
		IDDanhMucDonVi,
		IDDanhMucChungTu,
		--
		IDChungTu,
		LoaiDonChuyen,
		SoKmVoHangNhe,
		SoKmHangNhe,
		SoKmVoHangNang,
		SoKmHangNang,
		SoLuongNhienLieuDinhMuc,
		SoLuongNhienLieuCapThem,
		LyDoCapThem,
		SoTienVeCauDuong,
		SoTienLuatAnCa,
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
		@LoaiDonChuyen,
		@SoKmVoHangNhe,
		@SoKmHangNhe,
		@SoKmVoHangNang,
		@SoKmHangNang,
		@SoLuongNhienLieuDinhMuc,
		@SoLuongNhienLieuCapThem,
		@LyDoCapThem,
		@SoTienVeCauDuong,
		@SoTienLuatAnCa,
		@GhiChu,
		--
		@IDDanhMucNguoiSuDungCreate,
		@CreateDate
	)
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
alter procedure Update_ctInGiayVanTai
(
	@ID							bigint,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucChungTu			bigint,
	--
	@IDChungTu					bigint, --ID đơn hàng
	@LoaiDonChuyen				tinyint, --0: Không chuyển, 1: Đơn chuyển, hàng nhẹ, 2: Đơn chuyển, hàng nặng
	@SoKmVoHangNhe				float = null,
	@SoKmHangNhe				float = null,
	@SoKmVoHangNang				float = null,
	@SoKmHangNang				float = null,
	@SoLuongNhienLieuDinhMuc	float = null,
	@SoLuongNhienLieuCapThem	float = null,
	@LyDoCapThem				nvarchar(255) = null,
	@SoTienVeCauDuong			float = null,
	@SoTienLuatAnCa				float = null,
	@GhiChu						nvarchar(512) = null,
	--
	@IDDanhMucNguoiSuDungEdit	bigint,
	@EditDate					datetime = null output
)
as
begin
	set nocount on;
	declare @Err int;
	select @EditDate = GETDATE()
	begin tran
	begin try
	update ctInGiayVanTai
		set
			IDChungTu = @IDChungTu,
			LoaiDonChuyen = @LoaiDonChuyen,
			SoKmVoHangNhe = @SoKmVoHangNhe,
			SoKmHangNhe = @SoKmHangNhe,
			SoKmVoHangNang = @SoKmVoHangNang,
			SoKmHangNang = @SoKmHangNang,
			SoLuongNhienLieuDinhMuc = @SoLuongNhienLieuDinhMuc,
			SoLuongNhienLieuCapThem = @SoLuongNhienLieuCapThem,
			LyDoCapThem = @LyDoCapThem,
			SoTienVeCauDuong = @SoTienVeCauDuong,
			SoTienLuatAnCa = @SoTienLuatAnCa,
			GhiChu = @GhiChu,
			--
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @EditDate
		where ID = @ID;
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
alter procedure Delete_ctInGiayVanTai
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
	--
	delete from ctInGiayVanTai where ID = @ID;
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

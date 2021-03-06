/*
create table ctHopDongVanChuyen
(
	ID								bigint not null,
	IDDanhMucDonVi					bigint not null,
	IDDanhMucChungTu				bigint not null,
	IDDanhMucChungTuTrangThai		bigint not null,
	So								nvarchar(35) not null,
	NgayLap							datetime not null,
	--
	SoHopDong						nvarchar(128) not null,
	NgayHopDong						date not null,
	NgayCoHieuLuc					date not null,
	NgayHetHieuLuc					date not null,
	IDDanhMucKhachHang				bigint not null,
	NoiDung							nvarchar(512),
	SoTien							float,
	TrangThaiHopDong				tinyint not null, --0: chưa thực hiện, 1: đang thực hiện, 2: hoàn thành
	FileName						nvarchar(128),
	FileContent						varbinary(max),
	--
	GhiChu							nvarchar(max),
	IDDanhMucNguoiSuDungCreate		bigint	not null,
	CreateDate						datetime not null,
	IDDanhMucNguoiSuDungEdit		bigint,
	EditDate						datetime,
	constraint PK_ctHopDongVanChuyen primary key (ID),
	constraint ctHopDongVanChuyen_AutoID foreign key (ID) references AutoID(ID),
	constraint DonVi_ctHopDongVanChuyen foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint DanhMucChungTu_ctHopDongVanChuyen foreign key (IDDanhMucChungTu) references DanhMucChungTu(ID),
	constraint DanhMucChungTuTrangThai_ctHopDongVanChuyen foreign key (IDDanhMucChungTuTrangThai) references DanhMucChungTuTrangThai(ID),
	constraint So_ctHopDongVanChuyen unique (So),
	--
	constraint DanhMucKhachHang_ctHopDongVanChuyen foreign key (IDDanhMucKhachHang) references DanhMucKhachHang(ID),
	--
	constraint UserCreate_ctHopDongVanChuyen foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint UserEdit_ctHopDongVanChuyen foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
*/
---------------
alter procedure List_ctHopDongVanChuyen
	@IDDanhMucDonVi		bigint,
	@IDDanhMucChungTu	bigint,
	@ID					bigint = null,
	@TuNgay				datetime = null,
	@DenNgay			datetime = null
as
begin
	set nocount on;
	--
	if @TuNgay is null set @TuNgay = '2020-01-01';
	if @DenNgay is null set @DenNgay = '2030-01-01';
	select 
		a.ID,
		a.IDDanhMucDonVi,
		a.IDDanhMucChungTu,
		a.IDDanhMucChungTuTrangThai,
		a.So,
		a.NgayLap,
		--
		a.SoHopDong,
		a.NgayHopDong,
		a.NgayCoHieuLuc,
		a.NgayHetHieuLuc,
		a.IDDanhMucKhachHang,
		KhachHang.Ma MaDanhMucKhachHang,
		KhachHang.Ten TenDanhMucKhachHang,
		a.NoiDung,
		a.SoTien,
		a.TrangThaiHopDong,
		a.FileName,
		a.GhiChu,
		--
		a.IDDanhMucNguoiSuDungCreate,
		UserCreate.Ma MaDanhMucNguoiSuDungCreate,
		a.CreateDate,
		a.IDDanhMucNguoiSuDungEdit,
		UserEdit.Ma MaDanhMucNguoiSuDungEdit,
		a.EditDate
	from ctHopDongVanChuyen a
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
		left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu 
		and cast(a.NgayHopDong as date) >= @TuNgay and cast(a.NgayHopDong as date) <= @DenNgay
		and case when @ID is not null then a.ID else 0 end = isnull(@ID, 0);
end;
go
------------------
alter procedure List_ctHopDongVanChuyen_FileContent
	@ID bigint
as
begin
	select	a.FileContent from ctHopDongVanChuyen a where a.ID = @ID;
end;
go
------------------
alter procedure Insert_ctHopDongVanChuyen
(
	@ID								bigint = null output,
	@IDDanhMucDonVi					bigint,
	@IDDanhMucChungTu				bigint,
	@IDDanhMucChungTuTrangThai		bigint,
	@So								nvarchar(35) = null output,
	@NgayLap						datetime,
	--
	@SoHopDong						nvarchar(128),
	@NgayHopDong					date,
	@NgayCoHieuLuc					date,
	@NgayHetHieuLuc					date,
	@IDDanhMucKhachHang				bigint,
	@NoiDung						nvarchar(512),
	@SoTien							float,
	@TrangThaiHopDong				tinyint,
	@FileName						nvarchar(128) = null,
	@FileContent					varbinary(max) = null,
	--
	@GhiChu							nvarchar(255) = null,
	@IDDanhMucNguoiSuDungCreate		bigint,
	@CreateDate						datetime = null output
)
as
begin
	set nocount on;
	declare @Err int;
	set @CreateDate = GETDATE()
	begin tran
	begin try
	--Đánh số chứng từ
	exec Insert_AutoID @ID out, @TenBangDuLieu = N'ctHopDongVanChuyen';
	declare @KyHieu nvarchar(20), @ctCount int, @ThuTu nvarchar(5);
	select @KyHieu = KiHieu from DanhMucChungTu where ID = @IDDanhMucChungTu;
	select @ctCount = ISNULL(MAX(CAST(RIGHT(SO, 5) AS INT)), 0) + 1 from ctHopDongVanChuyen; -- where cast(NgayLap as date) = cast(@NgayLap as date);
	set @ThuTu = RIGHT('00000'+ISNULL(cast(@ctCount as nvarchar(5)),''),5);
	--set @So = @KyHieu + CONVERT(VARCHAR(8), @NgayLap, 112) + '-' + @ThuTu;
	set @So = @KyHieu + @ThuTu;
	--
	insert	into ctHopDongVanChuyen
	(
		ID,
		IDDanhMucDonVi,
		IDDanhMucChungTu,
		IDDanhMucChungTuTrangThai,
		So,
		NgayLap,
		--
		SoHopDong,
		NgayHopDong,
		NgayCoHieuLuc,
		NgayHetHieuLuc,
		IDDanhMucKhachHang,
		NoiDung,
		SoTien,
		TrangThaiHopDong,
		FileName,
		FileContent,
		--
		GhiChu,
		IDDanhMucNguoiSuDungCreate,
		CreateDate
	)
	values
	(
		@ID,
		@IDDanhMucDonVi,
		@IDDanhMucChungTu,
		@IDDanhMucChungTuTrangThai,
		@So,
		@NgayLap,
		--
		@SoHopDong,
		@NgayHopDong,
		@NgayCoHieuLuc,
		@NgayHetHieuLuc,
		@IDDanhMucKhachHang,
		@NoiDung,
		@SoTien,
		@TrangThaiHopDong,
		@FileName,
		@FileContent,
		--
		@GhiChu,
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
------------------
alter procedure Update_ctHopDongVanChuyen
(
	@ID								bigint,
	@IDDanhMucDonVi					bigint,
	@IDDanhMucChungTu				bigint,
	@IDDanhMucChungTuTrangThai		bigint,
	--
	@SoHopDong						nvarchar(128),
	@NgayHopDong					date,
	@NgayCoHieuLuc					date,
	@NgayHetHieuLuc					date,
	@IDDanhMucKhachHang				bigint,
	@NoiDung						nvarchar(512),
	@SoTien							float,
	@TrangThaiHopDong				tinyint,
	@FileName						nvarchar(128) = null,
	@FileContent					varbinary(max) = null,
	--
	@GhiChu							nvarchar(255) = null,
	@IDDanhMucNguoiSuDungEdit		bigint,
	@EditDate						datetime = null output
)
as
begin
	set nocount on;
	declare @Err int;
	select @EditDate = GETDATE()
	begin tran
	begin try
	update ctHopDongVanChuyen
		set
			IDDanhMucChungTuTrangThai = @IDDanhMucChungTuTrangThai,
			--
			SoHopDong = @SoHopDong,
			NgayHopDong = @NgayHopDong,
			NgayCoHieuLuc = @NgayCoHieuLuc,
			NgayHetHieuLuc = @NgayHetHieuLuc,
			IDDanhMucKhachHang = @IDDanhMucKhachHang,
			NoiDung = @NoiDung,
			SoTien = @SoTien,
			TrangThaiHopDong = @TrangThaiHopDong,
			FileName = @FileName,
			FileContent = @FileContent,
			--
			GhiChu = @GhiChu,
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
------------------
alter procedure Delete_ctHopDongVanChuyen
(
	@ID	bigint
)
as
begin
	set nocount on;
	declare @Err int;
	begin tran
	begin try
	--
	delete from ctHopDongVanChuyen where ID = @ID;
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
---------------
alter procedure List_ctHopDongVanChuyen_ValidSoHopDong
	@SoHopDong nvarchar(128) = null
as
begin
	set nocount on;
	--
	if @SoHopDong is null set @SoHopDong = '%' else set @SoHopDong = '%' + @SoHopDong + '%';
	select 
		a.ID,
		a.IDDanhMucDonVi,
		a.IDDanhMucChungTu,
		a.IDDanhMucChungTuTrangThai,
		a.So,
		a.NgayLap,
		--
		a.SoHopDong,
		a.NgayHopDong,
		a.NgayCoHieuLuc,
		a.NgayHetHieuLuc,
		a.IDDanhMucKhachHang,
		KhachHang.Ma MaDanhMucKhachHang,
		KhachHang.Ten TenDanhMucKhachHang,
		a.NoiDung,
		a.SoTien,
		a.TrangThaiHopDong,
		a.FileName,
		a.GhiChu,
		--
		a.IDDanhMucNguoiSuDungCreate,
		UserCreate.Ma MaDanhMucNguoiSuDungCreate,
		a.CreateDate,
		a.IDDanhMucNguoiSuDungEdit,
		UserEdit.Ma MaDanhMucNguoiSuDungEdit,
		a.EditDate
	from ctHopDongVanChuyen a
		left join DanhMucKhahcHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
		left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where SoHopDong like @SoHopDong;
end;
go
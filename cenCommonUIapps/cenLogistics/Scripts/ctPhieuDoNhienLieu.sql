/*
create table ctPhieuDoNhienLieu
(
	ID								bigint not null,
	IDDanhMucDonVi					bigint not null,
	IDDanhMucChungTu				bigint not null,
	IDDanhMucChungTuTrangThai		bigint not null,
	IDChungTu						bigint not null, --ID đơn hàng
	So								nvarchar(35) not null,
	NgayLap							datetime not null,
	--
	IDDanhMucDonViCungCapNhienLieu	bigint not null,
	NgayDoNhienLieu					datetime not null,
	SoLuongNhienLieu				float,
	DonGia							float,
	--
	GhiChu							nvarchar(max),
	IDDanhMucNguoiSuDungCreate		bigint	not null,
	CreateDate						datetime not null,
	IDDanhMucNguoiSuDungEdit		bigint,
	EditDate						datetime,
	constraint PK_ctPhieuDoNhienLieu primary key (ID),
	constraint ctPhieuDoNhienLieu_AutoID foreign key (ID) references AutoID(ID),
	constraint DonVi_ctPhieuDoNhienLieu foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint DanhMucChungTu_ctPhieuDoNhienLieu foreign key (IDDanhMucChungTu) references DanhMucChungTu(ID),
	constraint ChungTu_ctPhieuDoNhienLieu foreign key (IDChungTu) references ctDonHang(ID),
	constraint So_ctPhieuDoNhienLieu unique (So),
	--
	constraint DanhMucDonViCungCapNhienLieu_ctPhieuDoNhienLieu foreign key (IDDanhMucDonViCungCapNhienLieu) references DanhMucDoiTuong(ID),
	--
	constraint UserCreate_ctPhieuDoNhienLieu foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint UserEdit_ctPhieuDoNhienLieu foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
*/
---------------
alter procedure List_ctPhieuDoNhienLieu_Display
	@IDDanhMucDonVi		bigint,
	@IDDanhMucChungTu	bigint,
	@TuNgay				date = null,
	@DenNgay			date = null,
	@ID					bigint = null
as
begin
	set nocount on;
	--
	if @TuNgay is null set @TuNgay = '2020-01-01';
	if @DenNgay is null set @DenNgay = '2030-01-01';
	--
	select 
		a.ID,
		a.IDDanhMucDonVi,
		a.IDDanhMucChungTu,
		a.IDDanhMucChungTuTrangThai,
		a.IDChungTu,
		a.So,
		a.NgayLap,
		a.NgayDoNhienLieu,
		ctDonHang.So SoDonHang,
		ctDonHang.DebitNote,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		NhomHangVanChuyen.Ma MaDanhMucNhomHangVanChuyen,
		ChuXe.Ten TenDanhMucChuXe,
		Xe.BienSo,
		TaiXe.Ten TenDanhMucTaiXe,
		a.IDDanhMucDonViCungCapNhienLieu, DVCCNL.Ma MaDanhMucDonViCungCapNhienLieu, DVCCNL.Ten TenDanhMucDonViCungCapNhienLieu,
		a.SoLuongNhienLieu,
		a.DonGia,
		ROUND(a.SoLuongNhienLieu, 2) * ROUND(a.DonGia, 0) ThanhTien,
		ctDonHang.GhiChu GhiChuCS,
		a.GhiChu
	from ctPhieuDoNhienLieu a 
		left join ctDonHang on a.IDChungTu = ctDonHang.ID
		left join ctDieuHanh on a.IDChungTu = ctDieuHanh.IDChungTu
		left join DanhMucTuyenVanTai TuyenVanTai on ctDonHang.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucDoiTuong NhomHangVanChuyen on ctDonHang.IDDanhMucNhomHangVanChuyen = NhomHangVanChuyen.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join DanhMucDoiTuong DVCCNL on a.IDDanhMucDonViCungCapNhienLieu = DVCCNL.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu
		and cast(a.NgayDoNhienLieu as date) >= @TuNgay and cast(a.NgayDoNhienLieu as date) <= @DenNgay
		and case when @ID is not null then a.ID else -1 end = isnull(@ID, -1)
	order by a.NgayDoNhienLieu;
end;
go
---------------
alter procedure List_ctPhieuDoNhienLieu
	@IDDanhMucDonVi		bigint,
	@IDDanhMucChungTu	bigint,
	@ID					bigint

as
begin
	set nocount on;
	--
	select 
		a.ID,
		a.IDDanhMucDonVi,
		a.IDDanhMucChungTu,
		a.IDDanhMucChungTuTrangThai,
		a.IDChungTu,
		a.So,
		a.NgayLap,
		a.NgayDoNhienLieu,
		ctDonHang.So SoDonHang,
		ctDonHang.DebitNote,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		ChuXe.Ten TenDanhMucChuXe,
		Xe.BienSo,
		TaiXe.Ten TenDanhMucTaiXe,
		a.IDDanhMucDonViCungCapNhienLieu, DVCCNL.Ma MaDanhMucDonViCungCapNhienLieu, DVCCNL.Ten TenDanhMucDonViCungCapNhienLieu,
		a.SoLuongNhienLieu,
		a.DonGia,
		ROUND(a.SoLuongNhienLieu, 2) * ROUND(a.DonGia, 0) ThanhTien,
		ctDonHang.GhiChu GhiChuCS,
		a.GhiChu
	from ctPhieuDoNhienLieu a 
		left join ctDonHang on a.IDChungTu = ctDonHang.ID
		left join ctDieuHanh on ctDonHang.ID = ctDieuHanh.IDChungTu
		left join DanhMucTuyenVanTai TuyenVanTai on ctDonHang.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join DanhMucDoiTuong DVCCNL on a.IDDanhMucDonViCungCapNhienLieu = DVCCNL.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucChungTu = @IDDanhMucChungTu and a.ID = @ID;
end;
go
------------------
alter procedure Insert_ctPhieuDoNhienLieu
(
	@ID								bigint = null output,
	@IDDanhMucDonVi					bigint,
	@IDDanhMucChungTu				bigint,
	@IDDanhMucChungTuTrangThai		bigint,
	@IDChungTu						bigint,
	--
	@So								nvarchar(35) = null output,
	@NgayLap						datetime,
	@IDDanhMucDonViCungCapNhienLieu	bigint,
	@NgayDoNhienLieu				datetime,
	@SoLuongNhienLieu				float = null,
	@DonGia							float = null,
	@GhiChu							nvarchar(max) = null,
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
	--Đánh số chứng từ
	exec Insert_AutoID @ID out, @TenBangDuLieu = N'ctPhieuDoNhienLieu';
	declare @KyHieu nvarchar(20), @ctCount int, @ThuTu nvarchar(5);
	select @KyHieu = KiHieu from DanhMucChungTu where ID = @IDDanhMucChungTu;
	select @ctCount = ISNULL(MAX(CAST(RIGHT(SO, 5) AS INT)), 0) + 1 from ctPhieuDoNhienLieu; -- where cast(NgayLap as date) = cast(@NgayLap as date);
	set @ThuTu = RIGHT('00000'+ISNULL(cast(@ctCount as nvarchar(5)),''),5);
	--set @So = @KyHieu + CONVERT(VARCHAR(8), @NgayLap, 112) + '-' + @ThuTu;
	set @So = @KyHieu + @ThuTu;
	--
	exec Insert_AutoID @ID out, @TenBangDuLieu = N'ctPhieuDoNhienLieu';
	insert	into ctPhieuDoNhienLieu
	(
		ID,
		IDDanhMucDonVi,
		IDDanhMucChungTu,
		IDDanhMucChungTuTrangThai,
		IDChungTu,
		--
		So,
		NgayLap,
		IDDanhMucDonViCungCapNhienLieu,
		NgayDoNhienLieu,
		SoLuongNhienLieu,
		DonGia,
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
		@IDDanhMucChungTuTrangThai,
		@IDChungTu,
		--
		@So,
		@NgayLap,
		@IDDanhMucDonViCungCapNhienLieu,
		@NgayDoNhienLieu,
		@SoLuongNhienLieu,
		@DonGia,
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
alter procedure Update_ctPhieuDoNhienLieu
(
	@ID								bigint,
	@IDDanhMucDonVi					bigint,
	@IDDanhMucChungTu				bigint,
	@IDDanhMucChungTuTrangThai		bigint,
	@IDChungTu						bigint,
	--
	@So								nvarchar(35) = null,
	@NgayLap						datetime = null,
	@IDDanhMucDonViCungCapNhienLieu	bigint,
	@NgayDoNhienLieu				datetime,
	@SoLuongNhienLieu				float = null,
	@DonGia							float = null,
	@GhiChu							nvarchar(255) = null,
	--
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
	update ctPhieuDoNhienLieu
		set
			IDDanhMucChungTuTrangThai = @IDDanhMucChungTuTrangThai,
			IDChungTu = @IDChungTu,
			IDDanhMucDonViCungCapNhienLieu = @IDDanhMucDonViCungCapNhienLieu,
			NgayDoNhienLieu = @NgayDoNhienLieu,
			SoLuongNhienLieu = @SoLuongNhienLieu,
			DonGia = @DonGia,
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
--
alter procedure Delete_ctPhieuDoNhienLieu
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
	delete from ctPhieuDoNhienLieu where ID = @ID;
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

/*
drop table DanhMucTuyenVanTai
create table DanhMucTuyenVanTai
(
	ID							bigint			not null,
	IDDanhMucDonVi				bigint			not null,
	IDDanhMucLoaiDoiTuong		bigint			not null,
	--
	Ma							nvarchar(128)	not null,
	Ten							nvarchar(255)	not null,
	DiemDau						nvarchar(255),
	IDDanhMucTinhThanhDau		bigint,
	DiemCuoi					nvarchar(255),
	IDDanhMucTinhThanhCuoi		bigint,
	CuLy						float,
	GhiChu						nvarchar(512),
	--
	IDDanhMucNguoiSuDungCreate	bigint			not null,
	CreateDate					datetime		not null,
	IDDanhMucNguoiSuDungEdit	bigint,
	EditDate					datetime,
	constraint	PK_DanhMucTuyenVanTai primary key (ID),
	constraint	DanhMucTuyenVanTai_DanhMucDoiTuong foreign key (ID) references DanhMucDoiTuong(ID),
	constraint	DanhMucDonVi_DanhMucTuyenVanTai foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint	DanhMucLoaiDoiTuong_DanhMucTuyenVanTai foreign key (IDDanhMucLoaiDoiTuong) references DanhMucLoaiDoiTuong(ID),
	constraint	Ma_DanhMucTuyenVanTai unique(Ma),
	--
	constraint	DanhMucTinhThanhDau_DanhMucTuyenVanTai foreign key (IDDanhMucTinhThanhDau) references DanhMucDoiTuong(ID),
	constraint	DanhMucTinhThanhCuoi_DanhMucTuyenVanTai foreign key (IDDanhMucTinhThanhCuoi) references DanhMucDoiTuong(ID),
	--
	constraint	DanhMucNguoiSuDungCreate_DanhMucTuyenVanTai foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint	DanhMucNguoiSuDungEdit_DanhMucTuyenVanTai foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
*/
create procedure List_DanhMucTuyenVanTai
	@ID bigint = null,
	@IDDanhMucDonVi bigint,
	@IDDanhMucLoaiDoiTuong bigint,
	@SearchStr nvarchar(255) = null
as
begin
	set nocount on;
	if @SearchStr is null set @SearchStr = '%' else set @SearchStr = '%' + @SearchStr + '%';
	select	a.ID, 
			a.IDDanhMucDonVi, 
			a.IDDanhMucLoaiDoiTuong, 
			--
			a.Ma,
			a.Ten,
			a.DiemDau,
			a.IDDanhMucTinhThanhDau,
			TinhThanhDau.Ma MaDanhMucTinhThanhDau,
			TinhThanhDau.Ten TenDanhMucTinhThanhDau,
			a.DiemCuoi,
			a.IDDanhMucTinhThanhCuoi,
			TinhThanhCuoi.Ma MaDanhMucTinhThanhCuoi,
			TinhThanhCuoi.Ten TenDanhMucTinhThanhCuoi,
			a.CuLy,
			a.GhiChu,
			--
			a.IDDanhMucNguoiSuDungCreate, UserCreate.Ma MaDanhMucNguoiSuDungCreate, 
			a.CreateDate, 
			a.IDDanhMucNguoiSuDungEdit, UserEdit.Ma MaDanhMucNguoiSuDungEdit, 
			a.EditDate 
	from DanhMucTuyenVanTai a 
			left join DanhMucDoiTuong TinhThanhDau on a.IDDanhMucTinhThanhDau = TinhThanhDau.ID
			left join DanhMucDoiTuong TinhThanhCuoi on a.IDDanhMucTinhThanhCuoi = TinhThanhCuoi.ID
			left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
			left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where 
		a.IDDanhMucDonVi = @IDDanhMucDonVi 
		and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong 
		and case when @ID is not null then a.ID else 0 end = ISNULL(@ID, 0) 
		and (a.Ma like @SearchStr or a.Ten like @SearchStr)
	order by a.Ma;
end
go
--
create procedure Insert_DanhMucTuyenVanTai
	@ID							bigint out,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucLoaiDoiTuong		bigint,
	@Ma							nvarchar(128),
	@Ten						nvarchar(255),
	@DiemDau					nvarchar(255) = null,
	@IDDanhMucTinhThanhDau		bigint = null,
	@DiemCuoi					nvarchar(255) = null,
	@IDDanhMucTinhThanhCuoi		bigint = null,
	@CuLy						float = null,
	@GhiChu						nvarchar(512) = null,
	@IDDanhMucNguoiSuDungCreate	bigint,
	@CreateDate					datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		exec Insert_DanhMucDoiTuong @ID out, @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong, @Ma, @Ten, @IDDanhMucNguoiSuDungCreate, @CreateDate out;
		insert DanhMucTuyenVanTai 
		(	
			ID, 
			IDDanhMucDonVi, 
			IDDanhMucLoaiDoiTuong, 
			Ma,
			Ten,
			DiemDau,
			IDDanhMucTinhThanhDau,
			DiemCuoi,
			IDDanhMucTinhThanhCuoi,
			CuLy,
			GhiChu,
			IDDanhMucNguoiSuDungCreate, 
			CreateDate
		) 
		values 
		(	
			@ID, 
			@IDDanhMucDonVi, 
			@IDDanhMucLoaiDoiTuong, 
			@Ma, 
			@Ten, 
			@DiemDau,
			@IDDanhMucTinhThanhDau,
			@DiemCuoi,
			@IDDanhMucTinhThanhCuoi,
			@CuLy,
			@GhiChu,
			@IDDanhMucNguoiSuDungCreate, 
			@CreateDate
		);
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE()
		raiserror(@ErrMsg, 16, 1)
	end catch
end
go
--
--
create procedure Update_DanhMucTuyenVanTai
	@ID							bigint,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucLoaiDoiTuong		bigint,
	@Ma							nvarchar(128),
	@Ten						nvarchar(255),
	@DiemDau					nvarchar(255) = null,
	@IDDanhMucTinhThanhDau		bigint = null,
	@DiemCuoi					nvarchar(255) = null,
	@IDDanhMucTinhThanhCuoi		bigint = null,
	@CuLy						float = null,
	@GhiChu						nvarchar(512) = null,
	@IDDanhMucNguoiSuDungEdit	bigint,
	@EditDate					datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		exec Update_DanhMucDoiTuong @ID, @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong, @Ma, @Ten, @IDDanhMucNguoiSuDungEdit, @EditDate out
		update DanhMucTuyenVanTai set
			Ma = @Ma,
			Ten = @Ten,
			DiemDau = @DiemDau,
			IDDanhMucTinhThanhDau = @IDDanhMucTinhThanhDau,
			DiemCuoi = @DiemCuoi,
			IDDanhMucTinhThanhCuoi = IDDanhMucTinhThanhCuoi,
			CuLy = @CuLy,
			GhiChu = @GhiChu,
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @EditDate
		where ID = @ID;
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE()
		raiserror(@ErrMsg, 16, 1)
	end catch
end
go
--
--
create procedure Delete_DanhMucTuyenVanTai
	@ID			bigint
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		delete DanhMucTuyenVanTai	where ID = @ID;
		exec Delete_DanhMucDoiTuong @ID;
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE()
		raiserror(@ErrMsg, 16, 1)
	end catch
end
go

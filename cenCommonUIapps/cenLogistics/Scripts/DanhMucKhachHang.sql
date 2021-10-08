/*
drop table DanhMucKhachHang
--luôn luôn mặc định khách hàng là Fn, nếu có F2, F1 thì khai báo F2, F1
create table DanhMucKhachHang
(
	ID							bigint			not null,
	IDDanhMucDonVi				bigint			not null,
	IDDanhMucLoaiDoiTuong		bigint			not null,
	Ma							nvarchar(128)	not null,
	Ten							nvarchar(255)	not null,
	--
	IDDanhMucNhanSu				bigint,
	DiaChi						nvarchar(512),
	SoDienThoai					nvarchar(128),
	SoFax						nvarchar(128),
	Email						nvarchar(255),
	MaSoThue					nvarchar(20),
	TenNganHang					nvarchar(255),
	SoTaiKhoan					nvarchar(255),
	NguoiDaiDien				nvarchar(255),
	NguoiGiaoNhan				nvarchar(255),
	SoDienThoaiGiaoNhan			nvarchar(255),
	GhiChu						nvarchar(512),
	--
	IDDanhMucNguoiSuDungCreate	bigint			not null,
	CreateDate					datetime		not null,
	IDDanhMucNguoiSuDungEdit	bigint,
	EditDate					datetime,
	constraint	PK_DanhMucKhachHang primary key (ID),
	constraint	DanhMucKhachHang_DanhMucDoiTuong foreign key (ID) references DanhMucDoiTuong(ID),
	constraint	DanhMucDonVi_DanhMucKhachHang foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint	DanhMucLoaiDoiTuong_DanhMucKhachHang foreign key (IDDanhMucLoaiDoiTuong) references DanhMucLoaiDoiTuong(ID),
	constraint	Ma_DanhMucKhachHang unique(Ma),
	--
	constraint	DanhMucNhanSu_DanhMucKhachHang foreign key (IDDanhMucNhanSu) references DanhMucNhanSu(ID),
	--
	constraint	DanhMucNguoiSuDungCreate_DanhMucKhachHang foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint	DanhMucNguoiSuDungEdit_DanhMucKhachHang foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
create index idx_KhachHang_Ma on DanhMucKhachHang (Ma);
create index idx_KhachHang_Ten on DanhMucKhachHang (Ten);
*/
alter procedure List_DanhMucKhachHang
	@ID bigint = null,
	@IDDanhMucDonVi bigint,
	@IDDanhMucLoaiDoiTuong bigint = null,
	@SearchStr nvarchar(255) = null
as
begin
	set nocount on;
	if @SearchStr is null set @SearchStr = '%' else set @SearchStr = '%' + @SearchStr + '%';
	--
	select	a.ID, 
			a.IDDanhMucDonVi, 
			a.IDDanhMucLoaiDoiTuong, 
			--
			a.Ma,
			a.Ten,
			a.IDDanhMucNhanSu,
			NhanSu.Ten TenDanhMucNhanSu,
			a.DiaChi,
			a.SoDienThoai,
			a.SoFax,
			a.Email,
			a.MaSoThue,
			a.TenNganHang,
			a.SoTaiKhoan,
			a.NguoiDaiDien,
			a.NguoiGiaoNhan,
			a.SoDienThoaiGiaoNhan,
			a.GhiChu,
			--
			a.IDDanhMucNguoiSuDungCreate, UserCreate.Ma MaDanhMucNguoiSuDungCreate, 
			a.CreateDate, 
			a.IDDanhMucNguoiSuDungEdit, UserEdit.Ma MaDanhMucNguoiSuDungEdit, 
			a.EditDate 
		from DanhMucKhachHang a 
			left join DanhMucNhanSu NhanSu on a.IDDanhMucNhanSu = NhanSu.ID
			left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
			left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where 
		a.IDDanhMucDonVi = @IDDanhMucDonVi
		and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong 
		and case when @ID is not null then a.ID else -1 end = ISNULL(@ID, -1) 
		and (a.Ma like @SearchStr or a.Ten like @SearchStr)
	order by a.Ma;
end
go
--
alter procedure Insert_DanhMucKhachHang
	@ID							bigint out,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucLoaiDoiTuong		bigint,
	@Ma							nvarchar(128),
	@Ten						nvarchar(255),
	@IDDanhMucNhanSu			bigint = null,
	@DiaChi						nvarchar(512) = null,
	@SoDienThoai				nvarchar(128) = null,
	@SoFax						nvarchar(128) = null,
	@Email						nvarchar(255) = null,
	@MaSoThue					nvarchar(20) = null,
	@TenNganHang				nvarchar(255) = null,
	@SoTaiKhoan					nvarchar(255) = null,
	@NguoiDaiDien				nvarchar(255) = null,
	@NguoiGiaoNhan				nvarchar(255) = null,
	@SoDienThoaiGiaoNhan		nvarchar(255) = null,
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
		insert DanhMucKhachHang 
		(	
			ID, 
			IDDanhMucDonVi, 
			IDDanhMucLoaiDoiTuong, 
			Ma,
			Ten,
			IDDanhMucNhanSu,
			DiaChi,
			SoDienThoai,
			SoFax,
			Email,
			MaSoThue,
			TenNganHang,
			SoTaiKhoan,
			NguoiDaiDien,
			NguoiGiaoNhan,
			SoDienThoaiGiaoNhan,
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
			@IDDanhMucNhanSu,
			@DiaChi,
			@SoDienThoai,
			@SoFax,
			@Email,
			@MaSoThue,
			@TenNganHang,
			@SoTaiKhoan,
			@NguoiDaiDien,
			@NguoiGiaoNhan,
			@SoDienThoaiGiaoNhan,
			@GhiChu,
			@IDDanhMucNguoiSuDungCreate, 
			@CreateDate
		);
	commit tran
	end try
	begin catch
		rollback tran;
		select @ErrMsg = ERROR_MESSAGE()
		raiserror(@ErrMsg, 16, 1)
	end catch
end
go
--
alter procedure Update_DanhMucKhachHang
	@ID							bigint,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucLoaiDoiTuong		bigint,
	@Ma							nvarchar(128),
	@Ten						nvarchar(255),
	@IDDanhMucNhanSu			bigint = null,
	@DiaChi						nvarchar(512) = null,
	@SoDienThoai				nvarchar(128) = null,
	@SoFax						nvarchar(128) = null,
	@Email						nvarchar(255) = null,
	@MaSoThue					nvarchar(20) = null,
	@TenNganHang				nvarchar(255) = null,
	@SoTaiKhoan					nvarchar(255) = null,
	@NguoiDaiDien				nvarchar(255) = null,
	@NguoiGiaoNhan				nvarchar(255) = null,
	@SoDienThoaiGiaoNhan		nvarchar(255) = null,
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
		update DanhMucKhachHang set
			Ma = @Ma,
			Ten = @Ten,
			IDDanhMucNhanSu = @IDDanhMucNhanSu,
			DiaChi = @DiaChi,
			SoDienThoai = @SoDienThoai,
			SoFax = @SoFax,
			Email = @Email,
			MaSoThue = @MaSoThue,
			TenNganHang = @TenNganHang,
			SoTaiKhoan = @SoTaiKhoan,
			NguoiDaiDien = @NguoiDaiDien,
			NguoiGiaoNhan = @NguoiGiaoNhan,
			SoDienThoaiGiaoNhan = @SoDienThoaiGiaoNhan,
			GhiChu = @GhiChu,
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @EditDate
		where ID = @ID;
	commit tran
	end try
	begin catch
		rollback tran;
		select @ErrMsg = ERROR_MESSAGE()
		raiserror(@ErrMsg, 16, 1)
	end catch
end
go
--
--
alter procedure Delete_DanhMucKhachHang
	@ID			bigint
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		delete DanhMucKhachHang	where ID = @ID;
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
alter procedure List_DanhMucKhachHang_Valid
	@ID bigint = null,
	@IDDanhMucDonVi bigint,
	@IDDanhMucLoaiDoiTuong bigint = null,
	@SearchStr nvarchar(255) = null
as
begin
	set nocount on;
	if @SearchStr is null set @SearchStr = '%' else set @SearchStr = '%' + @SearchStr + '%';
	--
	select	a.ID, 
			a.IDDanhMucDonVi, 
			a.IDDanhMucLoaiDoiTuong, 
			--
			a.Ma,
			a.Ten,
			a.IDDanhMucNhanSu,
			NhanSu.Ten TenDanhMucNhanSu,
			a.DiaChi,
			a.SoDienThoai,
			a.SoFax,
			a.Email,
			a.MaSoThue,
			a.TenNganHang,
			a.SoTaiKhoan,
			a.NguoiDaiDien,
			a.NguoiGiaoNhan,
			a.SoDienThoaiGiaoNhan,
			a.GhiChu,
			--
			a.IDDanhMucNguoiSuDungCreate, UserCreate.Ma MaDanhMucNguoiSuDungCreate, 
			a.CreateDate, 
			a.IDDanhMucNguoiSuDungEdit, UserEdit.Ma MaDanhMucNguoiSuDungEdit, 
			a.EditDate 
		from DanhMucKhachHang a 
			left join DanhMucNhanSu NhanSu on a.IDDanhMucNhanSu = NhanSu.ID
			left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
			left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where 
		a.IDDanhMucDonVi = @IDDanhMucDonVi
		and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong 
		and case when @ID is not null then a.ID else -1 end = ISNULL(@ID, -1) 
		and (a.Ma like @SearchStr or a.Ten like @SearchStr)
	order by a.Ma;
end
go

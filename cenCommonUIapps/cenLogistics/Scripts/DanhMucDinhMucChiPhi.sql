/*
create table DanhMucDinhMucChiPhi
(
	ID									bigint			not null,
	IDDanhMucDonVi						bigint			not null,
	IDDanhMucLoaiDoiTuong				bigint			not null,
	--
	NgayApDung							date			not null,
	IDDanhMucTuyenVanTai				bigint			not null,
	LoaiTacNghiep						tinyint			not null, --1:Đơn, 2: Kẹp, 3: 1h-1v/1v-1h, 4: Kết hợp
	SoLuongNhienLieu					float,
	SoTienVeCauDuong					float,
	SoTienLuatAnCa						float,
	SoTienKetHopVeCauDuongLuatAnCa		float,
	SoTienLuuCaKhac						float, 
	SoTienLuatDuongCam					float,
	SoTienTongLuuCaKhacLuatDuongCam		float,
	SoTienLuongChuyen					float,
	SoTramLuat							smallint,
	SoTramVe							smallint,
	GhiChu								nvarchar(512),
	--
	IDDanhMucNguoiSuDungCreate			bigint			not null,
	CreateDate							datetime		not null,
	IDDanhMucNguoiSuDungEdit			bigint,
	EditDate							datetime,
	constraint	PK_DanhMucDinhMucChiPhi primary key (ID),
	constraint	DanhMucDinhMucChiPhi_DanhMucDoiTuong foreign key (ID) references DanhMucDoiTuong(ID),
	constraint	DanhMucDonVi_DanhMucDinhMucChiPhi foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint	DanhMucLoaiDoiTuong_DanhMucDinhMucChiPhi foreign key (IDDanhMucLoaiDoiTuong) references DanhMucLoaiDoiTuong(ID),
	--
	constraint	DanhMucTuyenVanTai_DanhMucDinhMucChiPhi foreign key (IDDanhMucTuyenVanTai) references DanhMucTuyenVanTai(ID),
	--
	constraint	DanhMucNguoiSuDungCreate_DanhMucDinhMucChiPhi foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint	DanhMucNguoiSuDungEdit_DanhMucDinhMucChiPhi foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
create table DanhMucDinhMucChiPhiXe
(
	ID									bigint	not null,
	IDDanhMucDonVi						bigint	not null,
	IDDanhMucLoaiDoiTuong				bigint	not null,
	--
	IDDanhMucDinhMucChiPhi				bigint	not null,
	IDDanhMucXe							bigint	not null,
	GhiChu								nvarchar(512),
	--
	IDDanhMucNguoiSuDungCreate			bigint			not null,
	CreateDate							datetime		not null,
	IDDanhMucNguoiSuDungEdit			bigint,
	EditDate							datetime,
	constraint	PK_DanhMucDinhMucChiPhiXe primary key (ID),
	constraint	DanhMucDinhMucChiPhiXe_DanhMucDoiTuong foreign key (ID) references DanhMucDoiTuong(ID),
	constraint	DanhMucDonVi_DanhMucDinhMucChiPhiXe foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint	DanhMucLoaiDoiTuong_DanhMucDinhMucChiPhiXe foreign key (IDDanhMucLoaiDoiTuong) references DanhMucLoaiDoiTuong(ID),
	--
	constraint	DanhMucDinhMucChiPhi_DanhMucDinhMucChiPhiXe foreign key (IDDanhMucDinhMucChiPhi) references DanhMucDinhMucChiPhi(ID),
	constraint	DanhMucXe_DanhMucDinhMucChiPhiXe foreign key (IDDanhMucXe) references DanhMucXe(ID),
	--
	constraint	DanhMucNguoiSuDungCreate_DanhMucDinhMucChiPhiXe foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint	DanhMucNguoiSuDungEdit_DanhMucDinhMucChiPhiXe foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
*/
alter procedure List_DanhMucDinhMucChiPhi
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
			a.NgayApDung,
			a.IDDanhMucTuyenVanTai,
			TuyenVanTai.Ma MaDanhMucTuyenVanTai,
			TuyenVanTai.Ten TenDanhMucTuyenVanTai,
			a.LoaiTacNghiep,
			case when a.LoaiTacNghiep = 1 then N'Đơn' when a.LoaiTacNghiep = 2 then N'Kẹp' when a.LoaiTacNghiep = 3 then N'1h-1v/1v-1h' else N'Kết hợp' end TenLoaiTacNghiep,
			a.SoLuongNhienLieu,
			a.SoTienVeCauDuong,
			a.SoTienLuatAnCa,
			a.SoTienKetHopVeCauDuongLuatAnCa,
			a.SoTienLuuCaKhac, 
			a.SoTienLuatDuongCam,
			a.SoTienTongLuuCaKhacLuatDuongCam,
			a.SoTienLuongChuyen,
			a.SoTramLuat,
			a.SoTramVe,
			a.GhiChu,
			--
			a.IDDanhMucNguoiSuDungCreate, UserCreate.Ma MaDanhMucNguoiSuDungCreate, 
			a.CreateDate, 
			a.IDDanhMucNguoiSuDungEdit, UserEdit.Ma MaDanhMucNguoiSuDungEdit, 
			a.EditDate
		into #DanhMucDinhMucChiPhi
		from DanhMucDinhMucChiPhi a 
			left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
			left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
			left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where 
		a.IDDanhMucDonVi = @IDDanhMucDonVi 
		and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong 
		and case when @ID is not null then a.ID else 0 end = ISNULL(@ID, 0) 
	order by a.NgayApDung;
	select
		a.ID,
		a.IDDanhMucDonVi,
		a.IDDanhMucLoaiDoiTuong,
		--
		a.IDDanhMucDinhMucChiPhi,
		a.IDDanhMucXe,
		DanhMucXe.BienSo,
		a.GhiChu,
		--
		a.IDDanhMucNguoiSuDungCreate, UserCreate.Ma MaDanhMucNguoiSuDungCreate, 
		a.CreateDate, 
		a.IDDanhMucNguoiSuDungEdit, UserEdit.Ma MaDanhMucNguoiSuDungEdit, 
		a.EditDate
	into #DanhMucDinhMucChiPhiXe
	from DanhMucDinhMucChiPhiXe a 
		left join DanhMucXe on a.IDDanhMucXe = DanhMucXe.ID
		left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
			left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where a.IDDanhMucDinhMucChiPhi in (select ID from #DanhMucDinhMucChiPhi);
	--
	select * from #DanhMucDinhMucChiPhi;
	select * from #DanhMucDinhMucChiPhiXe;
	--
	drop table #DanhMucDinhMucChiPhi;
	drop table #DanhMucDinhMucChiPhiXe;
end
go
--
alter procedure Insert_DanhMucDinhMucChiPhi
	@ID									bigint out,
	@IDDanhMucDonVi						bigint,
	@IDDanhMucLoaiDoiTuong				bigint,
	@NgayApDung							date,
	@IDDanhMucTuyenVanTai				bigint,
	@LoaiTacNghiep						tinyint, --1:Đơn, 2: Kẹp, 3: 1h-1v/1v-1h, 4: kết hợp
	@SoLuongNhienLieu					float = null,
	@SoTienVeCauDuong					float = null,
	@SoTienLuatAnCa						float = null,
	@SoTienKetHopVeCauDuongLuatAnCa		float = null,
	@SoTienLuuCaKhac					float = null,
	@SoTienLuatDuongCam					float = null,
	@SoTienTongLuuCaKhacLuatDuongCam	float = null,
	@SoTienLuongChuyen					float = null,
	@SoTramLuat							smallint = null,
	@SoTramVe							smallint = null,
	@GhiChu								nvarchar(512) = null,
	@IDDanhMucNguoiSuDungCreate			bigint,
	@CreateDate							datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		exec Insert_DanhMucDoiTuong @ID out, @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong, null, null, @IDDanhMucNguoiSuDungCreate, @CreateDate out;
		insert DanhMucDinhMucChiPhi 
		(	
			ID, 
			IDDanhMucDonVi, 
			IDDanhMucLoaiDoiTuong, 
			NgayApDung,
			IDDanhMucTuyenVanTai,
			LoaiTacNghiep,
			SoLuongNhienLieu,
			SoTienVeCauDuong,
			SoTienLuatAnCa,
			SoTienKetHopVeCauDuongLuatAnCa,
			SoTienLuuCaKhac,
			SoTienLuatDuongCam,
			SoTienTongLuuCaKhacLuatDuongCam,
			SoTienLuongChuyen,
			SoTramLuat,
			SoTramVe,
			GhiChu,
			IDDanhMucNguoiSuDungCreate, 
			CreateDate
		) 
		values 
		(	
			@ID, 
			@IDDanhMucDonVi, 
			@IDDanhMucLoaiDoiTuong, 
			@NgayApDung,
			@IDDanhMucTuyenVanTai,
			@LoaiTacNghiep,
			@SoLuongNhienLieu,
			@SoTienVeCauDuong,
			@SoTienLuatAnCa,
			@SoTienKetHopVeCauDuongLuatAnCa,
			@SoTienLuuCaKhac,
			@SoTienLuatDuongCam,
			@SoTienTongLuuCaKhacLuatDuongCam,
			@SoTienLuongChuyen,
			@SoTramLuat,
			@SoTramVe,
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
alter procedure Update_DanhMucDinhMucChiPhi
	@ID									bigint,
	@IDDanhMucDonVi						bigint,
	@IDDanhMucLoaiDoiTuong				bigint,
	@NgayApDung							date,
	@IDDanhMucTuyenVanTai				bigint,
	@LoaiTacNghiep						tinyint, --1:Đơn, 2: Kẹp, 3: 1h-1v/1v-1h
	@SoLuongNhienLieu					float = null,
	@SoTienVeCauDuong					float = null,
	@SoTienLuatAnCa						float = null,
	@SoTienKetHopVeCauDuongLuatAnCa		float = null,
	@SoTienLuuCaKhac					float = null,
	@SoTienLuatDuongCam					float = null,
	@SoTienTongLuuCaKhacLuatDuongCam	float = null,
	@SoTienLuongChuyen					float = null,
	@SoTramLuat							smallint = null,
	@SoTramVe							smallint = null,
	@GhiChu								nvarchar(512) = null,
	@IDDanhMucNguoiSuDungEdit			bigint,
	@EditDate							datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		exec Update_DanhMucDoiTuong @ID, @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong, null, null, @IDDanhMucNguoiSuDungEdit, @EditDate out
		update DanhMucDinhMucChiPhi set
			NgayApDung = @NgayApDung,
			IDDanhMucTuyenVanTai = @IDDanhMucTuyenVanTai,
			LoaiTacNghiep = @LoaiTacNghiep,
			SoLuongNhienLieu = @SoLuongNhienLieu,
			SoTienVeCauDuong = @SoTienVeCauDuong,
			SoTienLuatAnCa = @SoTienLuatAnCa,
			SoTienKetHopVeCauDuongLuatAnCa = @SoTienKetHopVeCauDuongLuatAnCa,
			SoTienLuuCaKhac = @SoTienLuuCaKhac,
			SoTienLuatDuongCam = @SoTienLuatDuongCam,
			SoTienTongLuuCaKhacLuatDuongCam = @SoTienTongLuuCaKhacLuatDuongCam,
			SoTienLuongChuyen = @SoTienLuongChuyen,
			SoTramLuat = @SoTramLuat,
			SoTramVe = @SoTramVe,
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
alter procedure Delete_DanhMucDinhMucChiPhi
	@ID			bigint
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		delete DanhMucDinhMucChiPhi	where ID = @ID;
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
---
alter procedure Insert_DanhMucDinhMucChiPhiXe
	@ID									bigint out,
	@IDDanhMucDonVi						bigint,
	@IDDanhMucLoaiDoiTuong				bigint,
	@IDDanhMucDinhMucChiPhi				bigint,
	@IDDanhMucXe						bigint,
	@GhiChu								nvarchar(512) = null,
	@IDDanhMucNguoiSuDungCreate			bigint,
	@CreateDate							datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		exec Insert_DanhMucDoiTuong @ID out, @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong, null, null, @IDDanhMucNguoiSuDungCreate, @CreateDate out;
		insert DanhMucDinhMucChiPhiXe 
		(	
			ID, 
			IDDanhMucDonVi, 
			IDDanhMucLoaiDoiTuong, 
			IDDanhMucDinhMucChiPhi,
			IDDanhMucXe,
			GhiChu,
			IDDanhMucNguoiSuDungCreate, 
			CreateDate
		) 
		values 
		(	
			@ID, 
			@IDDanhMucDonVi, 
			@IDDanhMucLoaiDoiTuong, 
			@IDDanhMucDinhMucChiPhi,
			@IDDanhMucXe,
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
alter procedure Update_DanhMucDinhMucChiPhiXe
	@ID									bigint,
	@IDDanhMucDonVi						bigint,
	@IDDanhMucLoaiDoiTuong				bigint,
	@IDDanhMucDinhMucChiPhi				bigint,
	@IDDanhMucXe						bigint,
	@GhiChu								nvarchar(512) = null,
	@IDDanhMucNguoiSuDungEdit			bigint,
	@EditDate							datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		exec Update_DanhMucDoiTuong @ID, @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong, null, null, @IDDanhMucNguoiSuDungEdit, @EditDate out
		update DanhMucDinhMucChiPhiXe set
			IDDanhMucDinhMucChiPhi = @IDDanhMucDinhMucChiPhi,
			IDDanhMucXe = @IDDanhMucXe,
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
alter procedure Delete_DanhMucDinhMucChiPhiXe
	@ID			bigint
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		delete DanhMucDinhMucChiPhiXe where ID = @ID;
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
---
alter procedure Get_DanhMucDinhMucChiPhi_SoTien
	@IDChungTu							bigint,
	@IDDanhMucTuyenVanTai				bigint,
	@IDDanhMucXe						bigint,
	@LoaiTacNghiep						tinyint, --1:Đơn, 2: Kẹp, 3: 1h-1v/1v-1h, 4: Kết hợp
	@SoLuongNhienLieu					float = null out,
	@SoTienVeCauDuong					float = null out,
	@SoTienLuatAnCa						float = null out,
	@SoTienKetHopVeCauDuongLuatAnCa		float = null out,
	@SoTienLuuCaKhac					float = null out,
	@SoTienLuatDuongCam					float = null out,
	@SoTienTongLuuCaKhacLuatDuongCam	float = null out,
	@SoTienLuongChuyen					float = null out
as
begin
	set nocount on;
	--kiểm tra xem đơn có nằm trong danh sách kết hợp hay không
	declare @countKetHop int;
	select @countKetHop = count(ID) from ctDieuHanhChiTietDonHangKetHop where IDctDonHang = @IDChungTu;

	if (@countKetHop > 0)
	begin
		raiserror(N'Đơn hàng này là đơn hàng nhánh, không được cập nhật chi phí từ định mức!', 16, 1);
		return;
	end
	else
		begin
		if @countKetHop = 0 and @IDDanhMucTuyenVanTai is not null and @IDDanhMucXe is not null
		begin
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
				where a.IDDanhMucTuyenVanTai = @IDDanhMucTuyenVanTai and a.LoaiTacNghiep = @LoaiTacNghiep and b.IDDanhMucXe = @IDDanhMucXe
			) T;
		end;
	end;
end
go
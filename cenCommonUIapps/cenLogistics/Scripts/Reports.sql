alter procedure [dbo].[rep_BC_CHI_PHI_VAN_TAI]
		@IDDanhMucDonVi bigint,
		@TuNgay date,
		@DenNgay date,
		@IDDanhMucKhachHang bigint = null,
		@IDDanhMucThauPhu bigint = null,
		@IDDanhMucTuyenVanTai bigint = null,
		@IDDanhMucXe bigint = null,
		@IDDanhMucSale bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2020-12-31',
--		@IDDanhMucNhomHangVanChuyen bigint = null,
--		@IDDanhMucSale bigint = null;
begin
	set nocount on;
	create table #Report
	(
		Stt								bigint identity(1, 1),
		IDctDonHang						bigint,
		DebitNote						nvarchar(128),
		MaDanhMucTuyenVanTai			nvarchar(128),
		TenDanhMucTuyenVanTai			nvarchar(255),
		TenDanhMucTaiXe					nvarchar(255),
		MaDanhMucChuXe					nvarchar(128),
		BienSo_ChuXeNgoai				nvarchar(128),
		NgayLenhDieuXe					nvarchar(10),
		SoLuongNhienLieu				float,
		SoTienVeCauDuong				float,
		SoTienLuatAnCa					float,
		SoTienKetHopVeCauDuongLuatAnCa	float,
		SoTienLuuCaKhac					float, 
		SoTienLuatDuongCam				float,
		SoTienTongLuuCaKhacLuatDuongCam	float,
		SoTienLuongChuyen				float,
		SoTienLuongChuNhat				float,
		SoTienLuongTong					float,
		SoTienCuocThueXeNgoai			float,
		SoTienLuuCaThueXeNgoai			float,
		SoTienTongThueXeNgoai			float,
		SoTienTamUng					float,
		SoTienChenhLech					float,
		GhiChu							nvarchar(max),
		SoDonHang						nvarchar(128)
	);
	insert into #Report
	(
		IDctDonHang,
		DebitNote,
		MaDanhMucTuyenVanTai,
		TenDanhMucTuyenVanTai,
		TenDanhMucTaiXe,
		MaDanhMucChuXe,
		BienSo_ChuXeNgoai,
		NgayLenhDieuXe,
		SoLuongNhienLieu,
		SoTienVeCauDuong,
		SoTienLuatAnCa,
		SoTienKetHopVeCauDuongLuatAnCa,
		SoTienLuuCaKhac,
		SoTienLuatDuongCam,
		SoTienTongLuuCaKhacLuatDuongCam,
		SoTienLuongChuyen,
		SoTienLuongChuNhat,
		SoTienLuongTong,
		SoTienCuocThueXeNgoai,
		SoTienLuuCaThueXeNgoai,
		SoTienTongThueXeNgoai,
		SoTienTamUng,
		SoTienChenhLech,
		GhiChu,
		SoDonHang
	)
	select
		a.ID IDctDonHang,
		a.DebitNote DebiNote,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		TaiXe.Ten,
		ChuXe.KyHieuKeToan MaDanhMucChuXe,
		Xe.BienSo,
		convert(nvarchar(10), isnull(a.NgayDongHang, a.NgayTraHang), 103) NgayLenhDieuXe,
		ctChiPhiVanTai.SoLuongNhienLieu,
		ctChiPhiVanTai.SoTienVeCauDuong,
		ctChiPhiVanTai.SoTienLuatAnCa,
		ctChiPhiVanTai.SoTienKetHopVeCauDuongLuatAnCa,
		ctChiPhiVanTai.SoTienLuuCaKhac,
		ctChiPhiVanTai.SoTienLuatDuongCam,
		isnull(ctChiPhiVanTai.SoTienVeCauDuong, 0) + isnull(ctChiPhiVanTai.SoTienLuatAnCa, 0) + isnull(ctChiPhiVanTai.SoTienKetHopVeCauDuongLuatAnCa, 0) + isnull(ctChiPhiVanTai.SoTienLuuCaKhac, 0) + isnull(ctChiPhiVanTai.SoTienLuatDuongCam, 0),
		ctChiPhiVanTai.SoTienLuongChuyen,
		ctChiPhiVanTai.SoTienLuongChuNhat,
		ISNULL(ctChiPhiVanTai.SoTienLuongChuyen, 0) + ISNULL(ctChiPhiVanTai.SoTienLuongChuNhat, 0) SoTienLuongTong,
		ctChiPhiVanTai.SoTienCuocThueXeNgoai,
		null SoTienLuuCaThueXeNgoai,
		ctChiPhiVanTai.SoTienCuocThueXeNgoai SoTienTongThueXeNgoai,
		null SoTienTamUng,
		null SoTienChenhLech,
		ctChiPhiVanTai.GhiChu,
		a.So
	from ctDonHang a
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join ctChiPhiVanTai on a.ID = ctChiPhiVanTai.IDChungTu
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.Huy is null
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay
		and case when @IDDanhMucKhachHang is not null then a.IDDanhMucKhachHang else -1 end = isnull(@IDDanhMucKhachHang, -1)
		and case when @IDDanhMucThauPhu is not null then ctDieuHanh.IDDanhMucThauPhu else -1 end = isnull(@IDDanhMucThauPhu, -1)
		and case when @IDDanhMucTuyenVanTai is not null then a.IDDanhMucTuyenVanTai else -1 end = isnull(@IDDanhMucTuyenVanTai, -1)
		and case when @IDDanhMucXe is not null then ctDieuHanh.IDDanhMucXe else -1 end = isnull(@IDDanhMucXe, -1)
		and case when @IDDanhMucSale is not null then a.IDDanhMucSale else -1 end = isnull(@IDDanhMucSale, -1)
	order by isnull(a.NgayDongHang, a.NgayTraHang);
	--update số tiền đã tạm ứng, số tiền chênh lệch
	update #Report set SoTienTamUng = T.SoTienTamUng
		from #Report left join (select IDChungTu, SUM(isnull(SoTienCuocVo, 0) + isnull(SoTienHaiQuan, 0) + isnull(SoTienNangHa, 0) + isnull(SoTienChiKhac, 0)) SoTienTamUng from ctDonHangChiTietTamUng group by IDChungTu) T
		on #Report.IDctDonHang = T.IDChungTu;
	update #Report set SoTienChenhLech = ISNULL(SoTienVeCauDuong, 0) + ISNULL(SoTienLuatAnCa, 0) + ISNULL(SoTienVeCauDuong, 0) + ISNULL(SoTienKetHopVeCauDuongLuatAnCa, 0) 
										+ ISNULL(SoTienLuuCaKhac, 0) + ISNULL(SoTienLuatDuongCam, 0) - ISNULL(SoTienTamUng, 0);
	--
	insert into #Report
	(
		DebitNote,
		SoLuongNhienLieu,
		SoTienVeCauDuong,
		SoTienLuatAnCa,
		SoTienKetHopVeCauDuongLuatAnCa,
		SoTienLuuCaKhac,
		SoTienLuatDuongCam,
		SoTienTongLuuCaKhacLuatDuongCam,
		SoTienLuongChuyen,
		SoTienLuongChuNhat,
		SoTienLuongTong,
		SoTienCuocThueXeNgoai,
		SoTienTongThueXeNgoai,
		SoTienTamUng,
		SoTienChenhLech
	)
	select
		N'TỔNG CỘNG',
		sum(SoLuongNhienLieu),
		sum(SoTienVeCauDuong),
		sum(SoTienLuatAnCa),
		sum(SoTienKetHopVeCauDuongLuatAnCa),
		sum(SoTienLuuCaKhac),
		sum(SoTienLuatDuongCam),
		sum(SoTienTongLuuCaKhacLuatDuongCam),
		sum(SoTienLuongChuyen),
		sum(SoTienLuongChuNhat),
		sum(SoTienLuongTong),
		sum(SoTienCuocThueXeNgoai),
		sum(SoTienTongThueXeNgoai),
		sum(SoTienTamUng),
		sum(SoTienChenhLech)
	from #Report;
	--
	select * from #Report order by Stt;
	--
	drop table #Report;
end;
go
-------------------
create procedure [dbo].[rep_BC_CHI_PHI_VAN_TAI_BO_SUNG]
		@IDDanhMucDonVi bigint,
		@TuNgay date,
		@DenNgay date,
		@IDDanhMucKhachHang bigint = null,
		@IDDanhMucThauPhu bigint = null,
		@IDDanhMucTuyenVanTai bigint = null,
		@IDDanhMucXe bigint = null,
		@IDDanhMucSale bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2020-12-31',
--		@IDDanhMucNhomHangVanChuyen bigint = null,
--		@IDDanhMucSale bigint = null;
begin
	set nocount on;
	create table #Report
	(
		Stt								bigint identity(1, 1),
		IDctDonHang						bigint,
		DebitNote						nvarchar(128),
		MaDanhMucTuyenVanTai			nvarchar(128),
		TenDanhMucTuyenVanTai			nvarchar(255),
		TenDanhMucTaiXe					nvarchar(255),
		MaDanhMucChuXe					nvarchar(128),
		BienSo_ChuXeNgoai				nvarchar(128),
		NgayLenhDieuXe					nvarchar(10),
		SoLuongNhienLieu				float,
		SoTienVeCauDuong				float,
		SoTienLuatAnCa					float,
		SoTienKetHopVeCauDuongLuatAnCa	float,
		SoTienLuuCaKhac					float, 
		SoTienLuatDuongCam				float,
		SoTienTongLuuCaKhacLuatDuongCam	float,
		SoTienLuongChuyen				float,
		SoTienLuongChuNhat				float,
		SoTienLuongTong					float,
		SoTienCuocThueXeNgoai			float,
		SoTienLuuCaThueXeNgoai			float,
		SoTienTongThueXeNgoai			float,
		GhiChu							nvarchar(max),
		SoDonHang						nvarchar(128),
		NgayBoSung						nvarchar(10)
	);
	insert into #Report
	(
		IDctDonHang,
		DebitNote,
		MaDanhMucTuyenVanTai,
		TenDanhMucTuyenVanTai,
		TenDanhMucTaiXe,
		MaDanhMucChuXe,
		BienSo_ChuXeNgoai,
		NgayLenhDieuXe,
		SoLuongNhienLieu,
		SoTienVeCauDuong,
		SoTienLuatAnCa,
		SoTienKetHopVeCauDuongLuatAnCa,
		SoTienLuuCaKhac,
		SoTienLuatDuongCam,
		SoTienTongLuuCaKhacLuatDuongCam,
		SoTienLuongChuyen,
		SoTienLuongChuNhat,
		SoTienLuongTong,
		SoTienCuocThueXeNgoai,
		SoTienLuuCaThueXeNgoai,
		SoTienTongThueXeNgoai,
		GhiChu,
		SoDonHang,
		NgayBoSung
	)
	select
		a.ID IDctDonHang,
		a.DebitNote DebiNote,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		case when ctDieuHanh.IDDanhMucThauPhu not in (9307, 10614) then ChuXe.Ten else TaiXe.Ten end,
		ChuXe.KyHieuKeToan MaDanhMucChuXe,
		case when ctDieuHanh.IDDanhMucThauPhu not in (9307, 10614) then ChuXe.Ma else Xe.BienSo end,
		convert(nvarchar(10), isnull(a.NgayDongHang, a.NgayTraHang), 103) NgayLenhDieuXe,
		ctChiPhiVanTaiBoSung.SoLuongNhienLieu,
		ctChiPhiVanTaiBoSung.SoTienVeCauDuong,
		ctChiPhiVanTaiBoSung.SoTienLuatAnCa,
		ctChiPhiVanTaiBoSung.SoTienKetHopVeCauDuongLuatAnCa,
		ctChiPhiVanTaiBoSung.SoTienLuuCaKhac,
		ctChiPhiVanTaiBoSung.SoTienLuatDuongCam,
		isnull(ctChiPhiVanTaiBoSung.SoTienVeCauDuong, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienLuatAnCa, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienKetHopVeCauDuongLuatAnCa, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienLuuCaKhac, 0) + isnull(ctChiPhiVanTaiBoSung.SoTienLuatDuongCam, 0),
		ctChiPhiVanTaiBoSung.SoTienLuongChuyen,
		ctChiPhiVanTaiBoSung.SoTienLuongChuNhat,
		ISNULL(ctChiPhiVanTaiBoSung.SoTienLuongChuyen, 0) + ISNULL(ctChiPhiVanTaiBoSung.SoTienLuongChuNhat, 0) SoTienLuongTong,
		ctChiPhiVanTaiBoSung.SoTienCuocThueXeNgoai,
		null SoTienLuuCaThueXeNgoai,
		ctChiPhiVanTaiBoSung.SoTienCuocThueXeNgoai SoTienTongThueXeNgoai,
		ctChiPhiVanTaiBoSung.GhiChu,
		a.So,
		convert(nvarchar(10), ctChiPhiVanTaiBoSung.NgayBoSung, 103)
	from ctDonHang a
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join ctChiPhiVanTaiBoSung on a.ID = ctChiPhiVanTaiBoSung.IDChungTu
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and ctChiPhiVanTaiBoSung.ID is not null and a.Huy is null
		and ctChiPhiVanTaiBoSung.NgayBoSung >= @TuNgay and ctChiPhiVanTaiBoSung.NgayBoSung <= @DenNgay
		and case when @IDDanhMucKhachHang is not null then a.IDDanhMucKhachHang else -1 end = isnull(@IDDanhMucKhachHang, -1)
		and case when @IDDanhMucThauPhu is not null then ctDieuHanh.IDDanhMucThauPhu else -1 end = isnull(@IDDanhMucThauPhu, -1)
		and case when @IDDanhMucTuyenVanTai is not null then a.IDDanhMucTuyenVanTai else -1 end = isnull(@IDDanhMucTuyenVanTai, -1)
		and case when @IDDanhMucXe is not null then ctDieuHanh.IDDanhMucXe else -1 end = isnull(@IDDanhMucXe, -1)
		and case when @IDDanhMucSale is not null then a.IDDanhMucSale else -1 end = isnull(@IDDanhMucSale, -1)
	order by isnull(a.NgayDongHang, a.NgayTraHang);
	--
	insert into #Report
	(
		DebitNote,
		SoLuongNhienLieu,
		SoTienVeCauDuong,
		SoTienLuatAnCa,
		SoTienKetHopVeCauDuongLuatAnCa,
		SoTienLuuCaKhac,
		SoTienLuatDuongCam,
		SoTienTongLuuCaKhacLuatDuongCam,
		SoTienLuongChuyen,
		SoTienLuongChuNhat,
		SoTienLuongTong,
		SoTienCuocThueXeNgoai,
		SoTienTongThueXeNgoai
	)
	select
		N'TỔNG CỘNG',
		sum(SoLuongNhienLieu),
		sum(SoTienVeCauDuong),
		sum(SoTienLuatAnCa),
		sum(SoTienKetHopVeCauDuongLuatAnCa),
		sum(SoTienLuuCaKhac),
		sum(SoTienLuatDuongCam),
		sum(SoTienTongLuuCaKhacLuatDuongCam),
		sum(SoTienLuongChuyen),
		sum(SoTienLuongChuNhat),
		sum(SoTienLuongTong),
		sum(SoTienCuocThueXeNgoai),
		sum(SoTienTongThueXeNgoai)
	from #Report;
	--
	select * from #Report order by Stt;
	--
	drop table #Report;
end;
go
-------------------
alter procedure [dbo].[rep_BC_DOANHTHU]
	@IDDanhMucDonVi bigint,
	@TuNgay date,
	@DenNgay date,
	@IDDanhMucKhachHang bigint = null,
	@IDDanhMucThauPhu bigint = null,
	@IDDanhMucTuyenVanTai bigint = null,
	@IDDanhMucXe bigint = null,
	@IDDanhMucSale bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2020-12-31',
--		@IDDanhMucKhachHang bigint = null,
--		@IDDanhMucSale bigint = null;
begin
	set nocount on;
	create table #Report
	(
		Stt							bigint identity(1, 1),
		DebitNote					nvarchar(128),
		MaDanhMucKhachHang			nvarchar(128),
		MaDanhMucTuyenVanTai		nvarchar(128),
		TenDanhMucTuyenVanTai		nvarchar(255),
		MaDanhMucChuXe				nvarchar(128),
		BienSo						nvarchar(128),
		NgayDongTraHang				nvarchar(10),
		SoTienCuoc					float,
		SoTienThuNang				float,
		SoTienThuHa					float,
		SoTienThuKhac				float,
		NoiDungThuKhac				nvarchar(256),
		SoTienGiamTru				float,
		NoiDungGiamTru				nvarchar(256),
		SoTienDoanhThu				float,
		GhiChu						nvarchar(max)
	);
	insert into #Report
	select
		a.DebitNote,
		KhachHang.Ma MaDanhMucKhachHang,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		ChuXe.Ma MaDanhMucChuXe,
		Xe.BienSo,
		convert(nvarchar(10), isnull(a.NgayDongHang, a.NgayTraHang), 103),
		a.SoTienCuoc,
		a.SoTienThuNang,
		a.SoTienThuHa,
		a.SoTienThuKhac,
		a.NoiDungThuKhac,
		a.SoTienGiamTru,
		a.NoiDungGiamTru,
		isnull(a.SoTienCuoc, 0) + isnull(a.SoTienThuNang, 0) + isnull(a.SoTienThuHa, 0) +isnull(a.SoTienThuKhac, 0) - isnull(a.SoTienGiamTru, 0) SoTienDoanhThu,
		a.GhiChu
	from ctDonHang a
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.Huy is null
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay
		and case when @IDDanhMucKhachHang is not null then a.IDDanhMucKhachHang else -1 end = isnull(@IDDanhMucKhachHang, -1)
		and case when @IDDanhMucThauPhu is not null then ctDieuHanh.IDDanhMucThauPhu else -1 end = isnull(@IDDanhMucThauPhu, -1)
		and case when @IDDanhMucTuyenVanTai is not null then a.IDDanhMucTuyenVanTai else -1 end = isnull(@IDDanhMucTuyenVanTai, -1)
		and case when @IDDanhMucXe is not null then ctDieuHanh.IDDanhMucXe else -1 end = isnull(@IDDanhMucXe, -1)
		and case when @IDDanhMucSale is not null then a.IDDanhMucSale else -1 end = isnull(@IDDanhMucSale, -1)
	order by isnull(a.NgayDongHang, a.NgayTraHang);
	--
	insert into #Report
	(
		DebitNote,
		SoTienCuoc,
		SoTienThuNang,
		SoTienThuHa,
		SoTienThuKhac,
		SoTienGiamTru,
		SoTienDoanhThu
	)
	select
		N'TỔNG CỘNG',
		sum(SoTienCuoc),
		sum(SoTienThuNang),
		sum(SoTienThuHa),
		sum(SoTienThuKhac),
		sum(SoTienGiamTru),
		sum(SoTienDoanhThu)
	from #Report;
	--
	select * from #Report;
	--
	drop table #Report;
end;
go
-------------------
ALTER procedure [dbo].[rep_BC_DOANHTHU_KD_CNKH]
	@IDDanhMucDonVi bigint,
	@TuNgay date,
	@DenNgay date,
	@IDDanhMucKhachHang bigint = null,
	@IDDanhMucSale bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2020-12-31',
--		@IDDanhMucKhachHang bigint = null,
--		@IDDanhMucSale bigint = null;
begin
	set nocount on;
	create table #Report
	(
		Stt							bigint identity(1, 1),
		MaDanhMucKhachHang			nvarchar(128),
		TenDanhMucKhachHang			nvarchar(255),
		SoTienCuoc					float,
		SoTienThuNang				float,
		SoTienThuHa					float,
		SoTienThuKhac				float,
		SoTienGiamTru				float,
		SoTienDoanhThu				float,
	);
	insert into #Report
	select
		KhachHang.Ma MaDanhMucKhachHang,
		KhachHang.Ten TenDanhMucKhachHang,
		sum(a.SoTienCuoc),
		sum(a.SoTienThuNang),
		sum(a.SoTienThuHa),
		sum(a.SoTienThuKhac),
		sum(a.SoTienGiamTru),
		sum(isnull(a.SoTienCuoc, 0)) + sum(isnull(a.SoTienThuNang, 0)) + sum(isnull(a.SoTienThuHa, 0)) + sum(isnull(a.SoTienThuKhac, 0)) - sum(isnull(a.SoTienGiamTru, 0)) SoTienDoanhThu
	from ctDonHang a
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.Huy is null
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay
		and case when @IDDanhMucKhachHang is not null then a.IDDanhMucKhachHang else -1 end = isnull(@IDDanhMucKhachHang, -1)
		and case when @IDDanhMucSale is not null then a.IDDanhMucSale else -1 end = isnull(@IDDanhMucSale, -1)
	group by KhachHang.Ma, KhachHang.Ten
	order by KhachHang.Ma;
	--
	insert into #Report
	(
		TenDanhMucKhachHang,
		SoTienCuoc,
		SoTienThuNang,
		SoTienThuHa,
		SoTienThuKhac,
		SoTienGiamTru,
		SoTienDoanhThu
	)
	select
		N'TỔNG CỘNG',
		sum(SoTienCuoc),
		sum(SoTienThuNang),
		sum(SoTienThuHa),
		sum(SoTienThuKhac),
		sum(SoTienGiamTru),
		sum(SoTienDoanhThu)
	from #Report;
	--
	select * from #Report;
	--
	drop table #Report;
end;
go
-------------------
alter procedure [dbo].[rep_BC_LOINHUAN_KD]
		@IDDanhMucDonVi bigint,
		@TuNgay date,
		@DenNgay date,
		@IDDanhMucKhachHang bigint = null,
		@IDDanhMucThauPhu bigint = null,
		@IDDanhMucTuyenVanTai bigint = null,
		@IDDanhMucXe bigint = null,
		@IDDanhMucSale bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2021-12-31',
--		@IDDanhMucKhachHang bigint = null,
--		@IDDanhMucSale bigint = null;
begin
	set nocount on;
	create table #Report --CHI_TIET
	(
		Stt								bigint identity(1, 1),
		IDctDonHang						bigint,
		SoDonHang						nvarchar(128),
		DebitNote						nvarchar(128),
		MaDanhMucKhachHang				nvarchar(128),
		MaDanhMucTuyenVanTai			nvarchar(128),
		TenDanhMucTuyenVanTai			nvarchar(255),
		MaDanhMucChuXe					nvarchar(128),
		BienSo_ChuXeNgoai				nvarchar(128),
		TenDanhMucTaiXe					nvarchar(255),
		NgayDongTraHang					nvarchar(10),
		SoTienCuoc						float,
		SoTienThuNang					float,
		SoTienThuHa						float,
		SoTienThuKhac					float,
		SoTienGiamTru					float,
		SoLuongNhienLieu				float,
		SoTienNhienLieu					float,
		SoTienVeCauDuong				float,
		SoTienLuatAnCa					float,
		SoTienKetHopVeCauDuongLuatAnCa	float,
		SoTienLuuCaKhac					float, 
		SoTienLuatDuongCam				float,
		SoTienTongChiPhiChuyenDi		float,
		SoTienLuongChuyen				float,
		SoTienLuongChuNhat				float,
		SoTienLuongTong					float,
		SoTienCuocThueXeNgoai			float,
		SoTienLuuCaThueXeNgoai			float,
		SoTienTongThueXeNgoai			float,
		SoTienThuTucHaiQuan				float,
		SoTienDoanhThuTong				float,
		SoTienChiPhiTong				float,
		SoTienLoiNhuan					float,
		GhiChu							nvarchar(max)
	);
	insert into #Report
	(
		IDctDonHang,
		SoDonHang,
		DebitNote,
		MaDanhMucKhachHang,
		MaDanhMucTuyenVanTai,
		TenDanhMucTuyenVanTai,
		MaDanhMucChuXe,
		BienSo_ChuXeNgoai,
		TenDanhMucTaiXe,
		NgayDongTraHang,
		SoTienCuoc,
		SoTienThuNang,
		SoTienThuHa,
		SoTienThuKhac,
		SoTienGiamTru,
		SoTienDoanhThuTong,
		GhiChu
	)
	select
		a.ID IDctDonHang,
		a.So,
		a.DebitNote DebiNote,
		KhachHang.Ma MaDanhMucKhachHang,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		ChuXe.KyHieuKeToan,
		Xe.BienSo,
		TaiXe.Ten,
		CONVERT(nvarchar(10), isnull(a.NgayDongHang, a.NgayTraHang), 103),
		a.SoTienCuoc,
		a.SoTienThuNang,
		a.SoTienThuHa,
		a.SoTienThuKhac,
		a.SoTienGiamTru,
		isnull(SoTienCuoc,  0) + isnull(SoTienThuNang,  0) + isnull(SoTienThuHa,  0) + isnull(SoTienThuKhac,  0) - isnull(SoTienGiamTru,  0),
		a.GhiChu
	from ctDonHang a
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucTaiXe TaiXe on ctDieuHanh.IDDanhMucTaiXe = TaiXe.ID
		left join DanhMucNhanSu Sale on a.IDDanhMucSale = Sale.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.Huy is null
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay
		and case when @IDDanhMucKhachHang is not null then a.IDDanhMucKhachHang else -1 end = isnull(@IDDanhMucKhachHang, -1)
		and case when @IDDanhMucThauPhu is not null then ctDieuHanh.IDDanhMucThauPhu else -1 end = isnull(@IDDanhMucThauPhu, -1)
		and case when @IDDanhMucTuyenVanTai is not null then a.IDDanhMucTuyenVanTai else -1 end = isnull(@IDDanhMucTuyenVanTai, -1)
		and case when @IDDanhMucXe is not null then ctDieuHanh.IDDanhMucXe else -1 end = isnull(@IDDanhMucXe, -1)
		and case when @IDDanhMucSale is not null then a.IDDanhMucSale else -1 end = isnull(@IDDanhMucSale, -1)
	order by isnull(a.NgayDongHang, a.NgayTraHang), a.DebitNote;
	--update chi phí vận tải
	update #Report set
		SoLuongNhienLieu = ISNULL(T.SoLuongNhienLieu, 0), 
		SoTienVeCauDuong = ISNULL(T.SoTienVeCauDuong, 0),
		SoTienLuatAnCa = ISNULL(T.SoTienLuatAnCa, 0),
		SoTienKetHopVeCauDuongLuatAnCa = ISNULL(T.SoTienKetHopVeCauDuongLuatAnCa, 0),
		SoTienLuuCaKhac = ISNULL(T.SoTienLuuCaKhac, 0),
		SoTienLuatDuongCam = ISNULL(T.SoTienLuatDuongCam, 0),
		SoTienTongChiPhiChuyenDi = ISNULL(T.SoTienVeCauDuong, 0) + ISNULL(T.SoTienLuatAnCa, 0) + ISNULL(T.SoTienKetHopVeCauDuongLuatAnCa, 0) + ISNULL(T.SoTienLuuCaKhac, 0) + ISNULL(T.SoTienLuatDuongCam, 0),
		SoTienLuongChuyen = ISNULL(T.SoTienLuongChuyen, 0),
		SoTienLuongChuNhat = ISNULL(T.SoTienLuongChuNhat, 0),
		SoTienLuongTong = ISNULL(T.SoTienLuongChuyen, 0) + ISNULL(T.SoTienLuongChuNhat, 0),
		SoTienCuocThueXeNgoai = ISNULL(T.SoTienCuocThueXeNgoai, 0),
		SoTienTongThueXeNgoai = ISNULL(T.SoTienCuocThueXeNgoai, 0)
	from #Report inner join ctChiPhiVanTai T on #Report.IDctDonHang = T.IDChungTu;
	--
	update #Report set
		SoLuongNhienLieu = ISNULL(#Report.SoLuongNhienLieu, 0) + ISNULL(T.SoLuongNhienLieu, 0), 
		SoTienVeCauDuong = ISNULL(#Report.SoTienVeCauDuong, 0) + ISNULL(T.SoTienVeCauDuong, 0),
		SoTienLuatAnCa = ISNULL(#Report.SoTienLuatAnCa, 0) + ISNULL(T.SoTienLuatAnCa, 0),
		SoTienKetHopVeCauDuongLuatAnCa = ISNULL(#Report.SoTienKetHopVeCauDuongLuatAnCa, 0) + ISNULL(T.SoTienKetHopVeCauDuongLuatAnCa, 0),
		SoTienLuuCaKhac = ISNULL(#Report.SoTienLuuCaKhac, 0) + ISNULL(T.SoTienLuuCaKhac, 0),
		SoTienLuatDuongCam = ISNULL(#Report.SoTienLuatDuongCam, 0) + ISNULL(T.SoTienLuatDuongCam, 0),
		SoTienTongChiPhiChuyenDi = ISNULL(#Report.SoTienTongChiPhiChuyenDi, 0) + ISNULL(T.SoTienVeCauDuong, 0) + ISNULL(T.SoTienLuatAnCa, 0) + ISNULL(T.SoTienKetHopVeCauDuongLuatAnCa, 0) + ISNULL(T.SoTienLuuCaKhac, 0) + ISNULL(T.SoTienLuatDuongCam, 0),
		SoTienLuongChuyen = ISNULL(#Report.SoTienLuongChuyen, 0) + ISNULL(T.SoTienLuongChuyen, 0),
		SoTienLuongChuNhat = ISNULL(#Report.SoTienLuongChuNhat, 0) + ISNULL(T.SoTienLuongChuNhat, 0),
		SoTienLuongTong = ISNULL(#Report.SoTienLuongTong, 0) + ISNULL(T.SoTienLuongChuyen, 0) + ISNULL(T.SoTienLuongChuNhat, 0),
		SoTienCuocThueXeNgoai = ISNULL(#Report.SoTienCuocThueXeNgoai, 0) + ISNULL(T.SoTienCuocThueXeNgoai, 0),
		SoTienTongThueXeNgoai = ISNULL(#Report.SoTienTongThueXeNgoai, 0) + ISNULL(T.SoTienCuocThueXeNgoai, 0)
	from #Report inner join 
	(select IDChungTu,	SUM(ISNULL(SoLuongNhienLieu, 0)) SoLuongNhienLieu, SUM(ISNULL(SoTienVeCauDuong, 0)) SoTienVeCauDuong, SUM(ISNULL(SoTienLuatAnCa, 0)) SoTienLuatAnCa, SUM(ISNULL(SoTienKetHopVeCauDuongLuatAnCa, 0)) SoTienKetHopVeCauDuongLuatAnCa, 
						SUM(ISNULL(SoTienLuuCaKhac, 0)) SoTienLuuCaKhac, SUM(ISNULL(SoTienLuatDuongCam, 0)) SoTienLuatDuongCam, SUM(ISNULL(SoTienLuongChuyen, 0)) SoTienLuongChuyen, SUM(ISNULL(SoTienLuongChuNhat, 0)) SoTienLuongChuNhat, 
						SUM(ISNULL(SoTienCuocThueXeNgoai , 0)) SoTienCuocThueXeNgoai  from ctChiPhiVanTaiBoSung group by IDChungTu) T 
	on #Report.IDctDonHang = T.IDChungTu;
	--update số tiền nhiên liệu
	declare @IDctDonHang bigint, @Ngay datetime, @GiaNhienLieu float;
	declare curReport cursor for select IDctDonHang from #Report;
	open curReport;
	while @@fetch_status = 0
	begin
		select @Ngay = isnull(isnull(NgayDongHang, NgayTraHang), getdate()) from ctDonHang where ID = @IDctDonHang;
		select @Ngay = max(NgayGioApDung) from DanhMucGiaNhienLieu where NgayGioApDung <= @Ngay;
		set @GiaNhienLieu = (select top 1 DonGiaTruocThue from DanhMucGiaNhienLieu where NgayGioApDung = @Ngay);
		update #Report set SoTienNhienLieu = SoLuongNhienLieu * isnull(@GiaNhienLieu, 0) where IDctDonHang = @IDctDonHang;
		fetch next from curReport into @IDctDonHang;
	end;
	deallocate curReport;
	--update chi phí thủ tục hải quan
	--update tổng chi phí
	update #Report set SoTienChiPhiTong = ISNULL(SoTienNhienLieu, 0) + ISNULL(SoTienVeCauDuong, 0) + ISNULL(SoTienLuatAnCa, 0)
										+ ISNULL(SoTienKetHopVeCauDuongLuatAnCa, 0) + ISNULL(SoTienLuuCaKhac, 0) + ISNULL(SoTienLuatDuongCam, 0)	
										+ ISNULL(SoTienLuongChuyen, 0) + ISNULL(SoTienLuongChuNhat, 0) + ISNULL(SoTienCuocThueXeNgoai, 0)  + ISNULL(SoTienThuTucHaiQuan, 0);
	update #Report set SoTienLoiNhuan = ISNULL(SoTienDoanhThuTong, 0) - ISNULL(SoTienChiPhiTong, 0);
	--
	insert into #Report
	(
		DebitNote,
		SoTienCuoc,
		SoTienThuNang,
		SoTienThuHa,
		SoTienThuKhac,
		SoTienGiamTru,
		SoLuongNhienLieu,
		SoTienNhienLieu,
		SoTienVeCauDuong,
		SoTienLuatAnCa,
		SoTienKetHopVeCauDuongLuatAnCa,
		SoTienLuuCaKhac,
		SoTienLuatDuongCam,
		SoTienTongChiPhiChuyenDi,
		SoTienLuongChuyen,
		SoTienLuongChuNhat,
		SoTienLuongTong,
		SoTienCuocThueXeNgoai,
		SoTienLuuCaThueXeNgoai,
		SoTienTongThueXeNgoai,
		SoTienThuTucHaiQuan,
		SoTienDoanhThuTong,
		SoTienChiPhiTong,
		SoTienLoiNhuan
	)
	select
		N'TỔNG CỘNG',
		sum(SoTienCuoc),
		sum(SoTienThuNang),
		sum(SoTienThuHa),
		sum(SoTienThuKhac),
		sum(SoTienGiamTru),
		sum(SoLuongNhienLieu),
		sum(SoTienNhienLieu),
		sum(SoTienVeCauDuong),
		sum(SoTienLuatAnCa),
		sum(SoTienKetHopVeCauDuongLuatAnCa),
		sum(SoTienLuuCaKhac),
		sum(SoTienLuatDuongCam),
		sum(SoTienTongChiPhiChuyenDi),
		sum(SoTienLuongChuyen),
		sum(SoTienLuongChuNhat),
		sum(SoTienLuongTong),
		sum(SoTienCuocThueXeNgoai),
		sum(SoTienLuuCaThueXeNgoai),
		sum(SoTienTongThueXeNgoai),
		sum(SoTienThuTucHaiQuan),
		sum(SoTienDoanhThuTong),
		sum(SoTienChiPhiTong),
		sum(SoTienLoiNhuan)
	from #Report;
	--SHEET CHI_TIET
	select * from #Report;
	--
	drop table #Report;
end;
go
-------------------
create procedure [dbo].[rep_BC_TU_QT]
	@IDDanhMucDonVi bigint = null,
	@TuNgay date = null,
	@DenNgay date = null,
	@IDDanhMucCanBoTamUng bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2030-12-31',
--		@IDDanhMucKhachHang bigint = null;
begin
	set nocount on;
	create table #Report
	(
		Stt							bigint identity(1, 1),
		IDctDonHangChiTietTamUng	bigint,
		NgayTamUng					nvarchar(10),
		MaDanhMucKhachHang			nvarchar(128),
		DebitNote					nvarchar(128),
		MaDanhMucHangHoa			nvarchar(128),
		TrongLuong					float,
		SoTienTamUng				float,
		SoTienPhiHaTang				float,
		SoTienLocalCharge			float,
		SoTienLuuBai				float,
		SoTienNangHa				float,
		SoTienCuocVo				float,
		SoTienHaiQuan				float,
		SoTienLamHang				float,
		SoTienChonVo				float,
		SoTienChiKhac				float,
		SoTienHoanTamUng			float,
		NgayThanhToanTamUng			nvarchar(10),
		SoTienQuyetToan				float,
		SoTienTon					float,
		MaDanhMucCanBoTamUng		nvarchar(128),
		GhiChu						nvarchar(max),
	)
	--
	insert into #Report
	(
		IDctDonHangChiTietTamUng,
		NgayTamUng,
		MaDanhMucKhachHang,
		DebitNote,
		MaDanhMucHangHoa,
		TrongLuong,
		SoTienTamUng,
		MaDanhMucCanBoTamUng,
		GhiChu
	)
	select
		b.ID,
		convert(nvarchar(10), b.NgayTamUng, 103),
		KhachHang.Ma MaDanhMucKhachHang,
		a.DebitNote,
		HangHoa.Ma MaDanhMucHangHoa,
		a.KhoiLuong,
		isnull(b.SoTienPhiHaTang, 0) + isnull(b.SoTienLocalCharge, 0) + isnull(b.SoTienLuuBai, 0) + isnull(b.SoTienNangHa, 0)  + isnull(b.SoTienCuocVo, 0) + isnull(b.SoTienHaiQuan, 0) + isnull(b.SoTienLamHang, 0) + isnull(b.SoTienChonVo, 0) + isnull(b.SoTienChiKhac, 0) SoTienTamUng,
		CanBoTamUng.Ma MaDanhMucCanBoTamUng,
		a.GhiChu
	from ctDonHang a inner join ctDonHangChiTietTamUng b on a.ID = b.IDChungTu
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join DanhMucHangHoa HangHoa on a.IDDanhMucHangHoa = HangHoa.ID
		left join DanhMucNhanSu CanBoTamUng on b.IDDanhMucCanBoTamUng = CanBoTamUng.ID
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.Huy is null
	and case when @IDDanhMucCanBoTamUng is not null then b.IDDanhMucCanBoTamUng else -1 end = ISNULL(@IDDanhMucCanBoTamUng, -1)
	and b.NgayTamUng >= @TuNgay and b.NgayTamUng <= @DenNgay;
	--
	update #Report set	SoTienPhiHaTang = T.SoTienPhiHaTang,
						SoTienLocalCharge = T.SoTienLocalCharge,
						SoTienLuuBai = T.SoTienLuuBai,
						SoTienNangHa = T.SoTienNangHa,
						SoTienCuocVo = T.SoTienCuocVo,
						SoTienHaiQuan = T.SoTienHaiQuan,
						SoTienLamHang = T.SoTienLamHang,
						SoTienChonVo = T.SoTienChonVo,
						SoTienHoanTamUng = T.SoTienHoanTamUng,
						SoTienQuyetToan = ISNULL(T.SoTienPhiHaTang, 0) + ISNULL(T.SoTienLocalCharge, 0)  + ISNULL(T.SoTienLuuBai, 0) + ISNULL(T.SoTienNangHa, 0) + ISNULL(T.SoTienCuocVo, 0) + ISNULL(T.SoTienHaiQuan, 0) + ISNULL(T.SoTienLamHang, 0) + ISNULL(T.SoTienChonVo, 0) + ISNULL(T.SoTienChiKhac, 0)
	from #Report inner join (select IDChungTuChiTiet, SUM(isnull(SoTienPhiHaTang, 0)) SoTienPhiHaTang, SUM(isnull(SoTienLocalCharge, 0)) SoTienLocalCharge, SUM(isnull(SoTienLuuBai, 0)) SoTienLuuBai, SUM(isnull(SoTienNangHa, 0)) SoTienNangHa, SUM(isnull(SoTienCuocVo, 0)) SoTienCuocVo, SUM(isnull(SoTienHaiQuan, 0)) SoTienHaiQuan, SUM(isnull(SoTienLamHang, 0)) SoTienLamHang, SUM(isnull(SoTienChonVo, 0)) SoTienChonVo, SUM(isnull(SoTienChiKhac, 0)) SoTienChiKhac, SUM(isnull(SoTienHoanTamUng, 0)) SoTienHoanTamUng from ctThanhToanTamUng group by IDChungTuChiTiet) T
	on #Report.IDctDonHangChiTietTamUng = T.IDChungTuChiTiet;
	--
	update #Report set	NgayThanhToanTamUng = (select CONVERT(nvarchar(10), max(ctThanhToanTamUng.NgayThanhToanTamUng), 103) from ctThanhToanTamUng where ctThanhToanTamUng.IDChungTuChiTiet = #Report.IDctDonHangChiTietTamUng);
	--
	update #Report set	SoTienTon = isnull(SoTienTamUng, 0) - isnull(SoTienHoanTamUng, 0) - isnull(SoTienQuyetToan, 0);
	--
	insert into #Report
	(
		MaDanhMucKhachHang,
		SoTienTamUng,
		SoTienPhiHaTang,
		SoTienLocalCharge,
		SoTienLuuBai,
		SoTienNangHa,
		SoTienCuocVo,
		SoTienHaiQuan,
		SoTienLamHang,
		SoTienChonVo,
		SoTienChiKhac,
		SoTienHoanTamUng,
		SoTienQuyetToan,
		SoTienTon
	)
	select
		N'Tổng cộng',
		sum(SoTienTamUng),
		sum(SoTienPhiHaTang),
		sum(SoTienLocalCharge),
		sum(SoTienLuuBai),
		sum(SoTienNangHa),
		sum(SoTienCuocVo),
		sum(SoTienHaiQuan),
		sum(SoTienLamHang),
		sum(SoTienChonVo),
		sum(SoTienChiKhac),
		sum(SoTienHoanTamUng),
		sum(SoTienQuyetToan),
		sum(SoTienTon)
	from #Report;
	--
	select * from #Report;
	--
	drop table #Report;
end;
go
-------------------
ALTER procedure [dbo].[rep_BC_TU_TIENDUONG]
		@IDDanhMucDonVi bigint,
		@TuNgay date,
		@DenNgay date,
		@IDDanhMucChuXe bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2021-12-31',
--		@IDDanhMucChuXe bigint = null
begin
	set nocount on;
	create table #Report
	(
		Stt						bigint identity(1, 1),
		IDctDonHang				bigint,
		DebitNote				nvarchar(128),
		MaDanhMucKhachHang		nvarchar(128),
		MaDanhMucTuyenVanTai	nvarchar(128),
		TenDanhMucTuyenVanTai	nvarchar(255),
		MaDanhMucChuXe			nvarchar(128),
		BienSo_ChuXeNgoai		nvarchar(128),
		SoLuongNhienLieu		float,
		SoTienTamUng			float,
		GhiChu					nvarchar(max),
		SoDonHang				nvarchar(128),
		NgayDongTraHang			nvarchar(10)
	);
	insert into #Report
	(
		IDctDonHang,
		DebitNote,
		MaDanhMucKhachHang,
		MaDanhMucTuyenVanTai,
		TenDanhMucTuyenVanTai,
		MaDanhMucChuXe,
		BienSo_ChuXeNgoai,
		GhiChu,
		SoDonHang,
		NgayDongTraHang
	)
	select
		a.ID IDctDonHang,
		a.DebitNote DebiNote,
		KhachHang.Ma MaDanhMucKhachHang,
		TuyenVanTai.Ma MaDanhMucTuyenVanTai,
		TuyenVanTai.Ten TenDanhMucTuyenVanTai,
		ChuXe.KyHieuKeToan MaDanhMucChuXe,
		case when ctDieuHanh.IDDanhMucThauPhu not in (9307, 10614) then ChuXe.Ma else Xe.BienSo end,
		a.GhiChu,
		a.So,
		CONVERT(nvarchar(10), isnull(a.NgayDongHang, a.NgayTraHang), 103)
	from ctDonHang a
		left join DanhMucTuyenVanTai TuyenVanTai on a.IDDanhMucTuyenVanTai = TuyenVanTai.ID
		left join DanhMucKhachHang KhachHang on a.IDDanhMucKhachHang = KhachHang.ID
		left join ctDieuHanh on a.ID = ctDieuHanh.IDChungTu
		left join DanhMucThauPhu ChuXe on ctDieuHanh.IDDanhMucThauPhu = ChuXe.ID
		left join DanhMucXe Xe on ctDieuHanh.IDDanhMucXe = Xe.ID
		left join DanhMucNhanSu Sale on a.IDDanhMucSale = Sale.ID
		--left join DanhMucDoiTuong [NhaCungCapTrich1%] on Sale.Ma = [NhaCungCapTrich1%].Ma
	where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.Huy is null
		and isnull(a.NgayDongHang, a.NgayTraHang) >= @TuNgay and isnull(a.NgayDongHang, a.NgayTraHang) <= @DenNgay
		and case when @IDDanhMucChuXe is not null then ctDieuHanh.IDDanhMucThauPhu else -1 end = isnull(@IDDanhMucChuXe, -1)
	order by isnull(a.NgayDongHang, a.NgayTraHang);
	--
	update #Report set SoLuongNhienLieu = ctChiPhiVanTai.SoLuongNhienLieu
	from #Report inner join ctChiPhiVanTai on #Report.IDctDonHang = ctChiPhiVanTai.IDChungTu;
	update #Report set SoLuongNhienLieu = ISNULL(#Report.SoLuongNhienLieu, 0) +  ctChiPhiVanTaiBoSung.SoLuongNhienLieu
	from #Report inner join ctChiPhiVanTaiBoSung on #Report.IDctDonHang = ctChiPhiVanTaiBoSung.IDChungTu;
	--
	update #Report set SoTienTamUng =	ISNULL(SoTienVeCauDuong, 0) + ISNULL(SoTienLuatAnCa, 0) + ISNULL(SoTienKetHopVeCauDuongLuatAnCa, 0) +
										ISNULL(SoTienLuuCaKhac, 0) + ISNULL(SoTienLuatDuongCam, 0) --ISNULL(SoTienLuongChuyen, 0) + ISNULL(SoTienLuongChuNhat, 0) + 
										--ISNULL(SoTienCuocThueXeNgoai, 0) + ISNULL(SoTienVeCauDuong, 0)
	from #Report inner join ctChiPhiVanTai T on #Report.IDctDonHang = T.IDChungTu;
	update #Report set SoTienTamUng =	ISNULL(SoTienTamUng, 0) + ISNULL(SoTienVeCauDuong, 0) + ISNULL(SoTienLuatAnCa, 0) + ISNULL(SoTienKetHopVeCauDuongLuatAnCa, 0) +
										ISNULL(SoTienLuuCaKhac, 0) + ISNULL(SoTienLuatDuongCam, 0) --+ --ISNULL(SoTienLuongChuyen, 0) + ISNULL(SoTienLuongChuNhat, 0) + 
										--ISNULL(SoTienCuocThueXeNgoai, 0) + ISNULL(SoTienVeCauDuong, 0)
	from #Report inner join ctChiPhiVanTaiBoSung T on #Report.IDctDonHang = T.IDChungTu;
	--
	insert into #Report
	(
		
		DebitNote,
		SoLuongNhienLieu,
		SoTienTamUng
	)
	select
		N'Tổng cộng',
		SUM(SoLuongNhienLieu),
		SUM(SoTienTamUng)
	from #Report;
	--
	select * from #Report;
	--
	drop table #Report;
end;
go
-------------------
ALTER procedure [dbo].[rep_BC_SUACHUA]
	@IDDanhMucDonVi bigint = null,
	@TuNgay date = null,
	@DenNgay date = null,
	@IDDanhMucXe bigint = null
as
--declare	@IDDanhMucDonVi bigint = 1,
--		@TuNgay date = '2020-01-01',
--		@DenNgay date = '2030-12-31',
--		@IDDanhMucXe bigint = null;
begin
	set nocount on;
	create table #Report
	(
		Stt					bigint identity(1, 1),
		NguoiSuaChua		nvarchar(255),
		IDDanhMucXe			bigint,
		BienSo				nvarchar(128),
		NoiSuaChua			nvarchar(512),
		NgaySuaChua			nvarchar(10),
		NoiDungSuaChua		nvarchar(max),
		DonViTinh			nvarchar(128),
		SoLuong				float,
		DonGiaVatTu			float,
		DonGiaNhanCong		float,
		SoTienVatTu			float, 
		SoTienNhanCong		float,
		SoTien				float,
		GhiChu				nvarchar(max),
		ID					tinyint, --1: báo cáo chung, 2: báo cáo theo xe, 3: báo cáo theo nơi sửa chữa
	)
	--
	insert into #Report
	(
		NguoiSuaChua,
		IDDanhMucXe,
		BienSo,
		NoiSuaChua,
		NgaySuaChua,
		NoiDungSuaChua,
		DonViTinh,
		SoLuong,
		DonGiaVatTu,
		DonGiaNhanCong,
		SoTienVatTu,
		SoTienNhanCong,
		SoTien,
		GhiChu,
		ID
	)
	select
		ctSuaChua.NguoiSuaChua,
		ctSuaChua.IDDanhMucXe,
		DanhMucXe.BienSo,
		ctSuaChua.NoiSuaChua,
		convert(nvarchar(10), ctSuaChua.NgaySuaChua, 103),
		ctSuaChua.NoiDungSuaChua,
		ctSuaChua.DonViTinh,
		ctSuaChua.SoLuong,
		ctSuaChua.DonGiaVatTu,
		ctSuaChua.DonGiaNhanCong,
		ctSuaChua.SoTienVatTu,
		ctSuaChua.SoTienNhanCong,
		ctSuaChua.SoTien,
		ctSuaChua.GhiChu,
		1
	from ctSuaChua left join DanhMucXe on ctSuaChua.IDDanhMucXe = DanhMucXe.ID
	where ctSuaChua.IDDanhMucDonVi = @IDDanhMucDonVi 
	and case when @IDDanhMucXe is not null then ctSuaChua.IDDanhMucXe else -1 end = ISNULL(@IDDanhMucXe, -1)
	and cast(ctSuaChua.NgaySuaChua as date) >= @TuNgay and cast(ctSuaChua.NgaySuaChua as date) <= @DenNgay
	order by ctSuaChua.NgaySuaChua;
	--
	insert into #Report
	(
		NguoiSuaChua,
		SoTienVatTu,
		SoTienNhanCong,
		SoTien
	)
	select
		N'TỔNG CỘNG',
		sum(SoTienVatTu),
		sum(SoTienNhanCong),
		sum(SoTien)
	from #Report;
	--chèn 1 dòng trắng
	insert into #Report
	(
		NguoiSuaChua
	)
	values
	(		
		null
	);
	--chèn tổng sửa chữa theo xe
	insert into #Report
	(
		NguoiSuaChua,
		SoTienVatTu,
		SoTienNhanCong,
		SoTien,
		ID
	)
	select
		DanhMucXe.BienSo,
		sum(ctSuaChua.SoTienVatTu),
		sum(ctSuaChua.SoTienNhanCong),
		sum(ctSuaChua.SoTien),
		2
	from ctSuaChua left join DanhMucXe on ctSuaChua.IDDanhMucXe = DanhMucXe.ID
	where ctSuaChua.IDDanhMucDonVi = @IDDanhMucDonVi 
	and case when @IDDanhMucXe is not null then ctSuaChua.IDDanhMucXe else -1 end = ISNULL(@IDDanhMucXe, -1)
	and cast(ctSuaChua.NgaySuaChua as date) >= @TuNgay and cast(ctSuaChua.NgaySuaChua as date) <= @DenNgay
	group by DanhMucXe.BienSo;
	insert into #Report
	(
		NguoiSuaChua,
		SoTienVatTu,
		SoTienNhanCong,
		SoTien,
		ID
	)
	select
		N'TỔNG CỘNG',
		sum(#Report.SoTienVatTu),
		sum(#Report.SoTienNhanCong),
		sum(#Report.SoTien),
		2
	from #Report where ID is not null and ID = 2;
	----chèn 1 dòng trắng
	insert into #Report
	(
		NguoiSuaChua
	)
	values
	(		
		null
	);
	--chèn tổng sửa chữa nơi sửa chữa
	insert into #Report
	(
		NguoiSuaChua,
		SoTienVatTu,
		SoTienNhanCong,
		SoTien,
		ID
	)
	select
		ctSuaChua.NoiSuaChua,
		sum(ctSuaChua.SoTienVatTu),
		sum(ctSuaChua.SoTienNhanCong),
		sum(ctSuaChua.SoTien),
		3
	from ctSuaChua left join DanhMucXe on ctSuaChua.IDDanhMucXe = DanhMucXe.ID
	where ctSuaChua.IDDanhMucDonVi = @IDDanhMucDonVi 
	and case when @IDDanhMucXe is not null then ctSuaChua.IDDanhMucXe else -1 end = ISNULL(@IDDanhMucXe, -1)
	and cast(ctSuaChua.NgaySuaChua as date) >= @TuNgay and cast(ctSuaChua.NgaySuaChua as date) <= @DenNgay
	group by ctSuaChua.NoiSuaChua;
	insert into #Report
	(
		NguoiSuaChua,
		SoTienVatTu,
		SoTienNhanCong,
		SoTien,
		ID
	)
	select
		N'TỔNG CỘNG',
		sum(#Report.SoTienVatTu),
		sum(#Report.SoTienNhanCong),
		sum(#Report.SoTien),
		3
	from #Report where  ID is not null and ID = 3;
	--
	select * from #Report order by Stt;
	--
	drop table #Report;
end;

-------------------
-------------------
-------------------
-------------------
-------------------
-------------------
-------------------

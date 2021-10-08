using cenDAO.cenLogistics;
using cenDTO.cenLogistics;
using System;
using System.Data;
namespace cenBUS.cenLogistics
{
    public class DanhMucDinhMucChiPhiBUS
    {
        public DanhMucDinhMucChiPhiBUS()
        {
        }

        public DataSet List(object ID, object IDDanhMucLoaiDoiTuong, object SearchStr)
        {
            try
            {
                DanhMucDinhMucChiPhiDAO dao = new DanhMucDinhMucChiPhiDAO();
                return dao.List(ID, IDDanhMucLoaiDoiTuong, SearchStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Insert(ref DanhMucDinhMucChiPhi obj)
        {
            try
            {
                DanhMucDinhMucChiPhiDAO dao = new DanhMucDinhMucChiPhiDAO();
                return dao.Insert(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(ref DanhMucDinhMucChiPhi obj)
        {
            try
            {
                DanhMucDinhMucChiPhiDAO dao = new DanhMucDinhMucChiPhiDAO();
                return dao.Update(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(DanhMucDinhMucChiPhi obj)
        {
            try
            {
                DanhMucDinhMucChiPhiDAO dao = new DanhMucDinhMucChiPhiDAO();
                return dao.Delete(obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void GetSoTien(object IDChungTu, object IDDanhMucTuyenVanTai, object IDDanhMucXe, object LoaiTacNghiep, out object SoLuongNhienLieu, out object SoTienVeCauDuong, out object SoTienLuatAnCa, out object SoTienKetHopVeCauDuongLuatAnCa, out object SoTienLuuCaKhac, out object SoTienLuatDuongCam, out object SoTienTongLuuCaKhacLuatDuongCam, out object SoTienLuongChuyen)
        {
            SoLuongNhienLieu = null;
            SoTienVeCauDuong = null;
            SoTienLuatAnCa = null;
            SoTienKetHopVeCauDuongLuatAnCa = null;
            SoTienLuuCaKhac = null;
            SoTienLuatDuongCam = null;
            SoTienTongLuuCaKhacLuatDuongCam = null;
            SoTienLuongChuyen = null;
            DanhMucDinhMucChiPhiDAO dao = new DanhMucDinhMucChiPhiDAO();
            dao.GetSoTien(IDChungTu, IDDanhMucTuyenVanTai, IDDanhMucXe, LoaiTacNghiep, out SoLuongNhienLieu, out SoTienVeCauDuong, out SoTienLuatAnCa, out SoTienKetHopVeCauDuongLuatAnCa, out SoTienLuuCaKhac, out SoTienLuatDuongCam, out SoTienTongLuuCaKhacLuatDuongCam, out SoTienLuongChuyen);
        }
    }
    public class DanhMucDinhMucChiPhiXeBUS
    {
        public DanhMucDinhMucChiPhiXeBUS()
        {
        }
        public bool Insert(ref DanhMucDinhMucChiPhiXe obj)
        {
            try
            {
                DanhMucDinhMucChiPhiXeDAO dao = new DanhMucDinhMucChiPhiXeDAO();
                return dao.Insert(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(ref DanhMucDinhMucChiPhiXe obj)
        {
            try
            {
                DanhMucDinhMucChiPhiXeDAO dao = new DanhMucDinhMucChiPhiXeDAO();
                return dao.Update(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(DanhMucDinhMucChiPhiXe obj)
        {
            try
            {
                DanhMucDinhMucChiPhiXeDAO dao = new DanhMucDinhMucChiPhiXeDAO();
                return dao.Delete(obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

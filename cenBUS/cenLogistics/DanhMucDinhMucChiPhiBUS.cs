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
        public static void GetSoTien(object IDChungTu, object IDDanhMucTuyenVanTai, object IDDanhMucXe, object LoaiTacNghiep, object SoLuongNhienLieu, object SoTienVeCauDuong, object SoTienLuatAnCa, object SoTienKetHopVeCauDuongLuatAnCa, object SoTienLuuCaKhac, object SoTienLuatDuongCam, object SoTienTongLuuCaKhacLuatDuongCam, object SoTienLuongChuyen)
        {
            DanhMucDinhMucChiPhiDAO dao = new DanhMucDinhMucChiPhiDAO();
            dao.GetSoTien(IDChungTu, IDDanhMucTuyenVanTai, IDDanhMucXe, LoaiTacNghiep, SoLuongNhienLieu, SoTienVeCauDuong, SoTienLuatAnCa, SoTienKetHopVeCauDuongLuatAnCa, SoTienLuuCaKhac, SoTienLuatDuongCam, SoTienTongLuuCaKhacLuatDuongCam, SoTienLuongChuyen);
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

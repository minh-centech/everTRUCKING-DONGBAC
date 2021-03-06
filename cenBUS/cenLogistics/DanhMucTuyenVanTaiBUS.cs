using cenDAO.cenLogistics;
using cenDTO.cenLogistics;
using System;
using System.Data;
namespace cenBUS.cenLogistics
{
    public class DanhMucTuyenVanTaiBUS
    {
        public DanhMucTuyenVanTaiBUS()
        {
        }

        public DataTable List(object ID, object IDDanhMucLoaiDoiTuong, object SearchStr)
        {
            try
            {
                DanhMucTuyenVanTaiDAO dao = new DanhMucTuyenVanTaiDAO();
                return dao.List(ID, IDDanhMucLoaiDoiTuong, SearchStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Insert(ref DanhMucTuyenVanTai obj)
        {
            try
            {
                DanhMucTuyenVanTaiDAO dao = new DanhMucTuyenVanTaiDAO();
                return dao.Insert(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(ref DanhMucTuyenVanTai obj)
        {
            try
            {
                DanhMucTuyenVanTaiDAO dao = new DanhMucTuyenVanTaiDAO();
                return dao.Update(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(DanhMucTuyenVanTai obj)
        {
            try
            {
                DanhMucTuyenVanTaiDAO dao = new DanhMucTuyenVanTaiDAO();
                return dao.Delete(obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable Valid(object ID, object IDDanhMucLoaiDoiTuong)
        {
            try
            {
                DanhMucTuyenVanTaiDAO dao = new DanhMucTuyenVanTaiDAO();
                return dao.Valid(ID, IDDanhMucLoaiDoiTuong);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using cenDAO.DatabaseCore;
using cenDTO.DatabaseCore;
using System;
using System.Data;
namespace cenBUS.DatabaseCore
{
    public class DanhMucDoiTuongBUS
    {
        public DanhMucDoiTuongBUS()
        {
        }

        public DataTable List(object ID, object IDDanhMucLoaiDoiTuong, object SearchStr)
        {
            try
            {
                DanhMucDoiTuongDAO dao = new DanhMucDoiTuongDAO();
                return dao.List(ID, IDDanhMucLoaiDoiTuong, SearchStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Insert(ref DanhMucDoiTuong obj)
        {
            try
            {
                DanhMucDoiTuongDAO dao = new DanhMucDoiTuongDAO();
                return dao.Insert(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(ref DanhMucDoiTuong obj)
        {
            try
            {
                DanhMucDoiTuongDAO dao = new DanhMucDoiTuongDAO();
                return dao.Update(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(DanhMucDoiTuong obj)
        {
            try
            {
                DanhMucDoiTuongDAO dao = new DanhMucDoiTuongDAO();
                return dao.Delete(obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static object GetID(object IDDanhMucLoaiDoiTuong, object Ma)
        {
            try
            {
                DanhMucDoiTuongDAO dao = new DanhMucDoiTuongDAO();
                return dao.GetID(IDDanhMucLoaiDoiTuong, Ma);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

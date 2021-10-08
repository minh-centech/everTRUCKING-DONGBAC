using cenDAO.DatabaseCore;
using cenDTO.DatabaseCore;
using System;
using System.Data;
namespace cenBUS.DatabaseCore
{
    public class DanhMucNguoiSuDungBUS
    {
        public DanhMucNguoiSuDungBUS()
        {
        }

        public DataTable List(object ID)
        {
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.List(ID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable ListValidMa(object Ma)
        {
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.ListValidMa(Ma);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Insert(ref DanhMucNguoiSuDung obj)
        {
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.Insert(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(ref DanhMucNguoiSuDung obj)
        {
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.Update(ref obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(DanhMucNguoiSuDung obj)
        {
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.Delete(obj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static object GetID(object Ma, object Password, out object IDDanhMucPhanQuyen, out bool isAdmin)
        {
            IDDanhMucPhanQuyen = null;
            isAdmin = false;
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.GetID(Ma, Password, out IDDanhMucPhanQuyen, out isAdmin);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool UpdatePassword(object ID, object Password)
        {
            try
            {
                DanhMucNguoiSuDungDAO dao = new DanhMucNguoiSuDungDAO();
                return dao.UpdatePassword(ID, Password);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

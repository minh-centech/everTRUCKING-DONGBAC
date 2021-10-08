using cenDAO.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Data.SqlClient;
namespace cenDAO.cenLogistics
{
    public class DanhMucNhanSuDAO
    {
        public DataTable List(object ID, object IDDanhMucLoaiDoiTuong, object SearchStr)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ID", ID);
            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", IDDanhMucLoaiDoiTuong);
            sqlParameters[3] = new SqlParameter("@SearchStr", SearchStr);
            DataTable dt = dao.tableList(sqlParameters, DanhMucNhanSu.listProcedureName, DanhMucNhanSu.tableName);
            return dt;
        }
        public bool Insert(ref DanhMucNhanSu obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucNhanSu.insertProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[22];
                            sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = sizeof(Int64)
                            };
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", obj.IDDanhMucLoaiDoiTuong);
                            sqlParameters[3] = new SqlParameter("@Ma", obj.Ma);
                            sqlParameters[4] = new SqlParameter("@Ten", obj.Ten);
                            sqlParameters[5] = new SqlParameter("@IDDanhMucPhongBan", obj.IDDanhMucPhongBan);
                            sqlParameters[6] = new SqlParameter("@NguyenQuan", obj.NguyenQuan);
                            sqlParameters[7] = new SqlParameter("@DiaChiThuongTru", obj.DiaChiThuongTru);
                            sqlParameters[8] = new SqlParameter("@NgaySinh", obj.NgaySinh);
                            sqlParameters[9] = new SqlParameter("@SoCMND", obj.SoCMND);
                            sqlParameters[10] = new SqlParameter("@NgayCap", obj.NgayCap);
                            sqlParameters[11] = new SqlParameter("@NoiCap", obj.NoiCap);
                            sqlParameters[12] = new SqlParameter("@SoDienThoai", obj.SoDienThoai);
                            sqlParameters[13] = new SqlParameter("@MaSoThue", obj.MaSoThue);
                            sqlParameters[14] = new SqlParameter("@SoTaiKhoan", obj.SoTaiKhoan);
                            sqlParameters[15] = new SqlParameter("@Email", obj.Email);
                            sqlParameters[16] = new SqlParameter("@TrinhDo", obj.TrinhDo);
                            sqlParameters[17] = new SqlParameter("@NgayVaoLam", obj.NgayVaoLam);
                            sqlParameters[18] = new SqlParameter("@IDDanhMucTinhTrangLamViec", obj.IDDanhMucTinhTrangLamViec);
                            sqlParameters[19] = new SqlParameter("@GhiChu", obj.GhiChu);
                            sqlParameters[20] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", obj.IDDanhMucNguoiSuDungCreate);
                            sqlParameters[21] = new SqlParameter("@CreateDate", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(DanhMucNhanSu.tableName), sqlConnection, sqlTransaction))
                            {
                                sqlTransaction.Commit();
                                return true;
                            }
                            else
                            {
                                sqlTransaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly(ex.Message);
                return false;
            }
        }
        public bool Update(ref DanhMucNhanSu obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucNhanSu.updateProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[22];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", obj.IDDanhMucLoaiDoiTuong);
                            sqlParameters[3] = new SqlParameter("@Ma", obj.Ma);
                            sqlParameters[4] = new SqlParameter("@Ten", obj.Ten);
                            sqlParameters[5] = new SqlParameter("@IDDanhMucPhongBan", obj.IDDanhMucPhongBan);
                            sqlParameters[6] = new SqlParameter("@NguyenQuan", obj.NguyenQuan);
                            sqlParameters[7] = new SqlParameter("@DiaChiThuongTru", obj.DiaChiThuongTru);
                            sqlParameters[8] = new SqlParameter("@NgaySinh", obj.NgaySinh);
                            sqlParameters[9] = new SqlParameter("@SoCMND", obj.SoCMND);
                            sqlParameters[10] = new SqlParameter("@NgayCap", obj.NgayCap);
                            sqlParameters[11] = new SqlParameter("@NoiCap", obj.NoiCap);
                            sqlParameters[12] = new SqlParameter("@SoDienThoai", obj.SoDienThoai);
                            sqlParameters[13] = new SqlParameter("@MaSoThue", obj.MaSoThue);
                            sqlParameters[14] = new SqlParameter("@SoTaiKhoan", obj.SoTaiKhoan);
                            sqlParameters[15] = new SqlParameter("@Email", obj.Email);
                            sqlParameters[16] = new SqlParameter("@TrinhDo", obj.TrinhDo);
                            sqlParameters[17] = new SqlParameter("@NgayVaoLam", obj.NgayVaoLam);
                            sqlParameters[18] = new SqlParameter("@IDDanhMucTinhTrangLamViec", obj.IDDanhMucTinhTrangLamViec);
                            sqlParameters[19] = new SqlParameter("@GhiChu", obj.GhiChu);
                            sqlParameters[20] = new SqlParameter("@IDDanhMucNguoiSuDungEdit", obj.IDDanhMucNguoiSuDungEdit);
                            sqlParameters[21] = new SqlParameter("@EditDate", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Sua, cenCommon.cenCommon.TraTuDien(DanhMucNhanSu.tableName), sqlConnection, sqlTransaction))
                            {
                                sqlTransaction.Commit();
                                return true;
                            }
                            else
                            {
                                sqlTransaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly(ex.Message);
                return false;
            }
        }
        public bool Delete(DanhMucNhanSu obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucNhanSu.deleteProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[1];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Xoa, cenCommon.cenCommon.TraTuDien(DanhMucNhanSu.tableName), sqlConnection, sqlTransaction))
                            {
                                sqlTransaction.Commit();
                                return true;
                            }
                            else
                            {
                                sqlTransaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly(ex.Message);
                return false;
            }
        }
    }
}

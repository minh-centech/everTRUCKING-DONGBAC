using cenDAO.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Data.SqlClient;
namespace cenDAO.cenLogistics
{
    public class DanhMucDinhMucChiPhiDAO
    {
        public DataSet List(object ID, object IDDanhMucLoaiDoiTuong, object SearchStr)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ID", ID);
            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", IDDanhMucLoaiDoiTuong);
            sqlParameters[3] = new SqlParameter("@SearchStr", SearchStr);
            DataSet ds = dao.dsList(sqlParameters, DanhMucDinhMucChiPhi.listProcedureName);
            if (ds != null)
            {
                ds.Tables[0].TableName = DanhMucDinhMucChiPhi.tableName;
                ds.Tables[1].TableName = DanhMucDinhMucChiPhiXe.tableName;
                ds.Relations.Add(cenCommon.GlobalVariables.prefix_DataRelation + DanhMucDinhMucChiPhiXe.tableName, ds.Tables[DanhMucDinhMucChiPhi.tableName].Columns["ID"], ds.Tables[DanhMucDinhMucChiPhiXe.tableName].Columns["IDDanhMucDinhMucChiPhi"]);
            }    
            return ds;
        }
        public bool Insert(ref DanhMucDinhMucChiPhi obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhi.insertProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[19];
                            sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = sizeof(Int64)
                            };
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", obj.IDDanhMucLoaiDoiTuong);
                            sqlParameters[3] = new SqlParameter("@NgayApDung", obj.NgayApDung);
                            sqlParameters[4] = new SqlParameter("@IDDanhMucTuyenVanTai", obj.IDDanhMucTuyenVanTai);
                            sqlParameters[5] = new SqlParameter("@LoaiTacNghiep", obj.LoaiTacNghiep);
                            sqlParameters[6] = new SqlParameter("@SoLuongNhienLieu", obj.SoLuongNhienLieu);
                            sqlParameters[7] = new SqlParameter("@SoTienVeCauDuong", obj.SoTienVeCauDuong);
                            sqlParameters[8] = new SqlParameter("@SoTienLuatAnCa", obj.SoTienLuatAnCa);
                            sqlParameters[9] = new SqlParameter("@SoTienKetHopVeCauDuongLuatAnCa", obj.SoTienKetHopVeCauDuongLuatAnCa);
                            sqlParameters[10] = new SqlParameter("@SoTienLuuCaKhac", obj.SoTienLuuCaKhac);
                            sqlParameters[11] = new SqlParameter("@SoTienLuatDuongCam", obj.SoTienLuatDuongCam);
                            sqlParameters[12] = new SqlParameter("@SoTienTongLuuCaKhacLuatDuongCam", obj.SoTienTongLuuCaKhacLuatDuongCam);
                            sqlParameters[13] = new SqlParameter("@SoTienLuongChuyen", obj.SoTienLuongChuyen);
                            sqlParameters[14] = new SqlParameter("@SoTramLuat", obj.SoTramLuat);
                            sqlParameters[15] = new SqlParameter("@SoTramVe", obj.SoTramVe);
                            sqlParameters[16] = new SqlParameter("@GhiChu", obj.GhiChu);
                            sqlParameters[17] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", obj.IDDanhMucNguoiSuDungCreate);
                            sqlParameters[18] = new SqlParameter("@CreateDate", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(DanhMucDinhMucChiPhi.tableName), sqlConnection, sqlTransaction))
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
        public bool Update(ref DanhMucDinhMucChiPhi obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhi.updateProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[19];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", obj.IDDanhMucLoaiDoiTuong);
                            sqlParameters[3] = new SqlParameter("@NgayApDung", obj.NgayApDung);
                            sqlParameters[4] = new SqlParameter("@IDDanhMucTuyenVanTai", obj.IDDanhMucTuyenVanTai);
                            sqlParameters[5] = new SqlParameter("@LoaiTacNghiep", obj.LoaiTacNghiep);
                            sqlParameters[6] = new SqlParameter("@SoLuongNhienLieu", obj.SoLuongNhienLieu);
                            sqlParameters[7] = new SqlParameter("@SoTienVeCauDuong", obj.SoTienVeCauDuong);
                            sqlParameters[8] = new SqlParameter("@SoTienLuatAnCa", obj.SoTienLuatAnCa);
                            sqlParameters[9] = new SqlParameter("@SoTienKetHopVeCauDuongLuatAnCa", obj.SoTienKetHopVeCauDuongLuatAnCa);
                            sqlParameters[10] = new SqlParameter("@SoTienLuuCaKhac", obj.SoTienLuuCaKhac);
                            sqlParameters[11] = new SqlParameter("@SoTienLuatDuongCam", obj.SoTienLuatDuongCam);
                            sqlParameters[12] = new SqlParameter("@SoTienTongLuuCaKhacLuatDuongCam", obj.SoTienTongLuuCaKhacLuatDuongCam);
                            sqlParameters[13] = new SqlParameter("@SoTienLuongChuyen", obj.SoTienLuongChuyen);
                            sqlParameters[14] = new SqlParameter("@SoTramLuat", obj.SoTramLuat);
                            sqlParameters[15] = new SqlParameter("@SoTramVe", obj.SoTramVe);
                            sqlParameters[16] = new SqlParameter("@GhiChu", obj.GhiChu);
                            sqlParameters[17] = new SqlParameter("@IDDanhMucNguoiSuDungEdit", obj.IDDanhMucNguoiSuDungEdit);
                            sqlParameters[18] = new SqlParameter("@EditDate", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Sua, cenCommon.cenCommon.TraTuDien(DanhMucDinhMucChiPhi.tableName), sqlConnection, sqlTransaction))
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
        public bool Delete(DanhMucDinhMucChiPhi obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhi.deleteProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[1];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Xoa, cenCommon.cenCommon.TraTuDien(DanhMucDinhMucChiPhi.tableName), sqlConnection, sqlTransaction))
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

        public void GetSoTien(object IDChungTu, object IDDanhMucTuyenVanTai, object IDDanhMucXe, object LoaiTacNghiep, out object SoLuongNhienLieu, out object SoTienVeCauDuong, out object SoTienLuatAnCa, out object SoTienKetHopVeCauDuongLuatAnCa, out object SoTienLuuCaKhac, out object SoTienLuatDuongCam, out object SoTienTongLuuCaKhacLuatDuongCam, out object SoTienLuongChuyen)
        {
            SoLuongNhienLieu = null;
            SoTienVeCauDuong = null;
            SoTienLuatAnCa = null;
            SoTienKetHopVeCauDuongLuatAnCa = null;
            SoTienLuuCaKhac = null;
            SoTienLuatDuongCam = null;
            SoTienTongLuuCaKhacLuatDuongCam = null;
            SoTienLuongChuyen = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhi.getSoTienProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[12];
                            sqlParameters[0] = new SqlParameter("@IDChungTu", IDChungTu);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucTuyenVanTai", IDDanhMucTuyenVanTai);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucXe", IDDanhMucXe);
                            sqlParameters[3] = new SqlParameter("@LoaiTacNghiep", LoaiTacNghiep);
                            sqlParameters[4] = new SqlParameter("@SoLuongNhienLieu", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[5] = new SqlParameter("@SoTienVeCauDuong", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[6] = new SqlParameter("@SoTienLuatAnCa", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[7] = new SqlParameter("@SoTienKetHopVeCauDuongLuatAnCa", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[8] = new SqlParameter("@SoTienLuuCaKhac", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[9] = new SqlParameter("@SoTienLuatDuongCam", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[10] = new SqlParameter("@SoTienTongLuuCaKhacLuatDuongCam", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlParameters[11] = new SqlParameter("@SoTienLuongChuyen", DBNull.Value)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            sqlCommand.ExecuteNonQuery();
                            SoLuongNhienLieu = sqlParameters[4].Value;
                            SoTienVeCauDuong = sqlParameters[5].Value;
                            SoTienLuatAnCa = sqlParameters[6].Value;
                            SoTienKetHopVeCauDuongLuatAnCa = sqlParameters[7].Value;
                            SoTienLuuCaKhac = sqlParameters[8].Value;
                            SoTienLuatDuongCam = sqlParameters[9].Value;
                            SoTienTongLuuCaKhacLuatDuongCam = sqlParameters[10].Value;
                            SoTienLuongChuyen = sqlParameters[11].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cenCommon.cenCommon.ErrorMessageOkOnly(ex.Message);
            }
        }
    }
    public class DanhMucDinhMucChiPhiXeDAO
    {
        public bool Insert(ref DanhMucDinhMucChiPhiXe obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhiXe.insertProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[8];
                            sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = sizeof(Int64)
                            };
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", obj.IDDanhMucLoaiDoiTuong);
                            sqlParameters[3] = new SqlParameter("@IDDanhMucDinhMucChiPhi", obj.IDDanhMucDinhMucChiPhi);
                            sqlParameters[4] = new SqlParameter("@IDDanhMucXe", obj.IDDanhMucXe);
                            sqlParameters[5] = new SqlParameter("@GhiChu", obj.GhiChu);
                            sqlParameters[6] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", obj.IDDanhMucNguoiSuDungCreate);
                            sqlParameters[7] = new SqlParameter("@CreateDate", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(DanhMucDinhMucChiPhiXe.tableName), sqlConnection, sqlTransaction))
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
        public bool Update(ref DanhMucDinhMucChiPhiXe obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhiXe.updateProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[8];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucLoaiDoiTuong", obj.IDDanhMucLoaiDoiTuong);
                            sqlParameters[3] = new SqlParameter("@IDDanhMucDinhMucChiPhi", obj.IDDanhMucDinhMucChiPhi);
                            sqlParameters[4] = new SqlParameter("@IDDanhMucXe", obj.IDDanhMucXe);
                            sqlParameters[5] = new SqlParameter("@GhiChu", obj.GhiChu);
                            sqlParameters[6] = new SqlParameter("@IDDanhMucNguoiSuDungEdit", obj.IDDanhMucNguoiSuDungEdit);
                            sqlParameters[7] = new SqlParameter("@EditDate", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = 8
                            };
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Sua, cenCommon.cenCommon.TraTuDien(DanhMucDinhMucChiPhiXe.tableName), sqlConnection, sqlTransaction))
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
        public bool Delete(DanhMucDinhMucChiPhiXe obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(DanhMucDinhMucChiPhiXe.deleteProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[1];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Xoa, cenCommon.cenCommon.TraTuDien(DanhMucDinhMucChiPhiXe.tableName), sqlConnection, sqlTransaction))
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

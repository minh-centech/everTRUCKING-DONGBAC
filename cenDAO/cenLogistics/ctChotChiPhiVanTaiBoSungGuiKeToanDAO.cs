using cenDAO.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace cenDAO.cenLogistics
{
    public class ctChotChiPhiVanTaiBoSungGuiKeToanDAO
    {
        public DataSet List(object IDDanhMucChungTu, object TuNgay, object DenNgay, object IDDanhMucNhomHangVanChuyen, object IDDanhMucSale)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[1] = new SqlParameter("@IDDanhMucChungTu", IDDanhMucChungTu);
            sqlParameters[2] = new SqlParameter("@TuNgay", TuNgay);
            sqlParameters[3] = new SqlParameter("@DenNgay", DenNgay);
            sqlParameters[4] = new SqlParameter("@IDDanhMucNhomHangVanChuyen", IDDanhMucNhomHangVanChuyen);
            sqlParameters[5] = new SqlParameter("@IDDanhMucSale", IDDanhMucSale);
            DataSet ds = dao.dsList(sqlParameters, ctChotChiPhiVanTaiBoSungGuiKeToan.listProcedureName);
            if (ds != null)
            {
                ds.Tables[0].TableName = ctDonHang.tableName;
                ds.Tables[1].TableName = ctChotChiPhiVanTaiBoSungGuiKeToan.tableName;
            }
            return ds;
        }
        public bool Insert(List<ctChotChiPhiVanTaiBoSungGuiKeToan> objList)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(ctChotChiPhiVanTaiBoSungGuiKeToan.insertProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            foreach (ctChotChiPhiVanTaiBoSungGuiKeToan obj in objList)
                            {
                                SqlParameter[] sqlParameters = new SqlParameter[6];
                                sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                                {
                                    Direction = ParameterDirection.Output,
                                    Size = sizeof(Int64)
                                };
                                sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
                                sqlParameters[2] = new SqlParameter("@IDDanhMucChungTu", obj.IDDanhMucChungTu);
                                sqlParameters[3] = new SqlParameter("@IDChungTu", obj.IDChungTu);
                                sqlParameters[4] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", obj.IDDanhMucNguoiSuDungCreate);
                                sqlParameters[5] = new SqlParameter("@CreateDate", DBNull.Value)
                                {
                                    Direction = ParameterDirection.Output,
                                    Size = 8
                                };
                                sqlCommand.Parameters.Clear();
                                sqlCommand.Parameters.AddRange(sqlParameters);
                                int rowAffected = sqlCommand.ExecuteNonQuery();
                                obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                                obj.CreateDate = DateTime.Parse(sqlParameters[sqlParameters.Length - 1].Value.ToString());
                                if (!NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, obj.ID, null, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(ctChotChiPhiVanTaiBoSungGuiKeToan.tableName), sqlConnection, sqlTransaction))
                                {
                                    sqlTransaction.Rollback();
                                    return false;
                                }
                            }
                            sqlTransaction.Commit();
                            return true;
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
        public bool Delete(List<ctChotChiPhiVanTaiBoSungGuiKeToan> objList)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(ctChotChiPhiVanTaiBoSungGuiKeToan.deleteProcedureName, sqlConnection, sqlTransaction))
                        {
                            foreach (ctChotChiPhiVanTaiBoSungGuiKeToan obj in objList)
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                SqlParameter[] sqlParameters = new SqlParameter[1];
                                sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                                sqlCommand.Parameters.Clear();
                                sqlCommand.Parameters.AddRange(sqlParameters);
                                int rowAffected = sqlCommand.ExecuteNonQuery();
                                if (!NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, null, null, cenCommon.ThaoTacDuLieu.Xoa, cenCommon.cenCommon.TraTuDien(ctChotChiPhiVanTaiBoSungGuiKeToan.tableName), sqlConnection, sqlTransaction))
                                {
                                    sqlTransaction.Rollback();
                                    return false;
                                }
                            }
                            sqlTransaction.Commit();
                            return true;
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

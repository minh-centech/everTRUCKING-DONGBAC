using cenDAO.DatabaseCore;
using cenDTO.cenLogistics;
using System;
using System.Data;
using System.Data.SqlClient;
namespace cenDAO.cenLogistics
{
    public class ctDonHangDAO
    {
        public DataSet List(object IDDanhMucChungTu, object ID)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[1] = new SqlParameter("@IDDanhMucChungTu", IDDanhMucChungTu);
            sqlParameters[2] = new SqlParameter("@ID", ID);
            DataSet ds = dao.dsList(sqlParameters, ctDonHang.listProcedureName);
            if (ds != null)
            {
                ds.Tables[0].TableName = ctDonHang.tableName;
                ds.Tables[1].TableName = ctDonHang.tableNameChiTiet;
                ds.Relations.Add(cenCommon.GlobalVariables.prefix_DataRelation + ctDonHang.tableNameChiTiet, ds.Tables[ctDonHang.tableName].Columns["ID"], ds.Tables[ctDonHang.tableNameChiTiet].Columns["IDChungTu"]);

            }
            return ds;
        }
        public DataTable ListDisplay(object IDDanhMucChungTu, object ID, object TuNgay, object DenNgay)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[1] = new SqlParameter("@IDDanhMucChungTu", IDDanhMucChungTu);
            sqlParameters[2] = new SqlParameter("@ID", ID);
            sqlParameters[3] = new SqlParameter("@TuNgay", TuNgay);
            sqlParameters[4] = new SqlParameter("@DenNgay", DenNgay);
            DataTable dt = dao.tableList(sqlParameters, ctDonHang.listDisplayProcedureName, ctDonHang.tableName);
            return dt;
        }
        public DataTable ListNhomHangVanChuyen(object IDDanhMucChungTu, object TuNgay, object DenNgay, object IDDanhMucNhomHangVanChuyen)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[1] = new SqlParameter("@IDDanhMucChungTu", IDDanhMucChungTu);
            sqlParameters[2] = new SqlParameter("@TuNgay", TuNgay);
            sqlParameters[3] = new SqlParameter("@DenNgay", DenNgay);
            sqlParameters[4] = new SqlParameter("@IDDanhMucNhomHangVanChuyen", IDDanhMucNhomHangVanChuyen);
            DataTable dt = dao.tableList(sqlParameters, ctDonHang.listNhomHangVanChuyenProcedureName, ctDonHang.tableName);
            return dt;
        }
        public DataTable ValidDebitNote(object IDDanhMucChungTu, object DebitNote)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
            sqlParameters[1] = new SqlParameter("@IDDanhMucChungTu", IDDanhMucChungTu);
            sqlParameters[2] = new SqlParameter("@DebitNote", DebitNote);
            DataTable dt = dao.tableList(sqlParameters, ctDonHang.validDebitNoteProcedureName, ctDonHang.tableName);
            return dt;
        }
        public bool Insert(ref ctDonHang obj)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
                command = new SqlCommand(ctDonHang.insertProcedureName, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParameters = new SqlParameter[41];
                sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                {
                    Direction = ParameterDirection.Output,
                    Size = sizeof(Int64)
                };
                sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
                sqlParameters[2] = new SqlParameter("@IDDanhMucChungTu", obj.IDDanhMucChungTu);
                sqlParameters[3] = new SqlParameter("@IDDanhMucChungTuTrangThai", obj.IDDanhMucChungTuTrangThai);
                sqlParameters[4] = new SqlParameter("@So", obj.So)
                {
                    Direction = ParameterDirection.Output,
                    Size = 35
                };
                sqlParameters[5] = new SqlParameter("@NgayLap", obj.NgayLap);
                //
                sqlParameters[6] = new SqlParameter("@IDDanhMucSale", obj.IDDanhMucSale);
                sqlParameters[7] = new SqlParameter("@IDDanhMucKhachHang", obj.IDDanhMucKhachHang);
                sqlParameters[8] = new SqlParameter("@DebitNote", obj.DebitNote);
                sqlParameters[9] = new SqlParameter("@BillBooking", obj.BillBooking);
                sqlParameters[10] = new SqlParameter("@LoaiHang", obj.LoaiHang);
                sqlParameters[11] = new SqlParameter("@IDDanhMucNhomHangVanChuyen", obj.IDDanhMucNhomHangVanChuyen);
                sqlParameters[12] = new SqlParameter("@IDDanhMucHangHoa", obj.IDDanhMucHangHoa);
                sqlParameters[13] = new SqlParameter("@IDDanhMucHangTau", obj.IDDanhMucHangTau);
                sqlParameters[14] = new SqlParameter("@SoLuong", obj.SoLuong);
                sqlParameters[15] = new SqlParameter("@KhoiLuong", obj.KhoiLuong);
                sqlParameters[16] = new SqlParameter("@TheTich", obj.TheTich);
                sqlParameters[17] = new SqlParameter("@SoContainer", obj.SoContainer);
                sqlParameters[18] = new SqlParameter("@IDDanhMucDiaDiemLayCont", obj.IDDanhMucDiaDiemLayCont);
                sqlParameters[19] = new SqlParameter("@IDDanhMucDiaDiemTraCont", obj.IDDanhMucDiaDiemTraCont);
                sqlParameters[20] = new SqlParameter("@NgayDongHang", obj.NgayDongHang);
                sqlParameters[21] = new SqlParameter("@GioDongHang", obj.GioDongHang);
                sqlParameters[22] = new SqlParameter("@NgayTraHang", obj.NgayTraHang);
                sqlParameters[23] = new SqlParameter("@GioTraHang", obj.GioTraHang);
                sqlParameters[24] = new SqlParameter("@IDDanhMucDiaDiemLayHang", obj.IDDanhMucDiaDiemLayHang);
                sqlParameters[25] = new SqlParameter("@IDDanhMucDiaDiemTraHang", obj.IDDanhMucDiaDiemTraHang);
                sqlParameters[26] = new SqlParameter("@IDDanhMucTuyenVanTai", obj.IDDanhMucTuyenVanTai);
                sqlParameters[27] = new SqlParameter("@NgayCatMang", obj.NgayCatMang);
                sqlParameters[28] = new SqlParameter("@GioCatMang", obj.GioCatMang);
                sqlParameters[29] = new SqlParameter("@NguoiGiaoNhan", obj.NguoiGiaoNhan);
                sqlParameters[30] = new SqlParameter("@SoDienThoaiGiaoNhan", obj.SoDienThoaiGiaoNhan);
                sqlParameters[31] = new SqlParameter("@SoTienCuoc", obj.SoTienCuoc);
                sqlParameters[32] = new SqlParameter("@SoTienThuNang", obj.SoTienThuNang);
                sqlParameters[33] = new SqlParameter("@SoTienThuHa", obj.SoTienThuHa);
                sqlParameters[34] = new SqlParameter("@SoTienThuKhac", obj.SoTienThuKhac);
                sqlParameters[35] = new SqlParameter("@NoiDungThuKhac", obj.NoiDungThuKhac);
                sqlParameters[36] = new SqlParameter("@SoTienGiamTru", obj.SoTienGiamTru);
                sqlParameters[37] = new SqlParameter("@NoiDungGiamTru", obj.NoiDungGiamTru);
                sqlParameters[38] = new SqlParameter("@ThoiHanThanhToan", obj.ThoiHanThanhToan);
                //
                sqlParameters[39] = new SqlParameter("@GhiChu", obj.GhiChu);
                sqlParameters[40] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", cenCommon.GlobalVariables.IDDanhMucNguoiSuDung);
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameters);
                int rowAffected = command.ExecuteNonQuery();
                obj.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                obj.So = sqlParameters[4].Value.ToString();
                if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, obj.ID, null, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), connection, transaction))
                {
                    foreach (ctDonHang.ctDonHangChiTietTamUng ctChiTiet in obj.listChiTiet)
                    {
                        command = new SqlCommand(ctDonHang.insertChiTietProcedureName, connection, transaction);
                        command.CommandType = CommandType.StoredProcedure;
                        sqlParameters = new SqlParameter[18];
                        sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                        {
                            Direction = ParameterDirection.Output,
                            Size = sizeof(Int64)
                        };
                        sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                        sqlParameters[2] = new SqlParameter("@IDDanhMucChungTu", obj.IDDanhMucChungTu);
                        sqlParameters[3] = new SqlParameter("@IDChungTu", obj.ID);
                        //
                        sqlParameters[4] = new SqlParameter("@NgayTamUng", ctChiTiet.NgayTamUng);
                        sqlParameters[5] = new SqlParameter("@SoTienPhiHaTang", ctChiTiet.SoTienPhiHaTang);
                        sqlParameters[6] = new SqlParameter("@SoTienLocalCharge", ctChiTiet.SoTienLocalCharge);
                        sqlParameters[7] = new SqlParameter("@SoTienLuuBai", ctChiTiet.SoTienLuuBai);
                        sqlParameters[8] = new SqlParameter("@SoTienNangHa", ctChiTiet.SoTienNangHa);
                        sqlParameters[9] = new SqlParameter("@SoTienCuocVo", ctChiTiet.SoTienCuocVo);
                        sqlParameters[10] = new SqlParameter("@SoTienHaiQuan", ctChiTiet.SoTienHaiQuan);
                        sqlParameters[11] = new SqlParameter("@SoTienLamHang", ctChiTiet.SoTienLamHang);
                        sqlParameters[12] = new SqlParameter("@SoTienChonVo", ctChiTiet.SoTienChonVo);
                        sqlParameters[13] = new SqlParameter("@SoTienChiKhac", ctChiTiet.SoTienChiKhac);
                        sqlParameters[14] = new SqlParameter("@IDDanhMucCanBoTamUng", ctChiTiet.IDDanhMucCanBoTamUng);
                        sqlParameters[15] = new SqlParameter("@ThoiHanThanhToan", ctChiTiet.ThoiHanThanhToan);
                        sqlParameters[16] = new SqlParameter("@GhiChu", ctChiTiet.GhiChu);
                        sqlParameters[17] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", cenCommon.GlobalVariables.IDDanhMucNguoiSuDung);
                        command.Parameters.Clear();
                        command.Parameters.AddRange(sqlParameters);
                        command.ExecuteNonQuery();
                        ctChiTiet.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                        if (!NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(ctChiTiet), ctChiTiet.ID, obj.ID, ctChiTiet.ID, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), connection, transaction))
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }

            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                cenCommon.cenCommon.ErrorMessageOkOnly(ex.Message);
                return false;
            }
            finally
            {
                command.Dispose();
                transaction.Dispose();
                connection.Close();
                connection.Dispose();
            }
        }
        public bool Update(ref ctDonHang obj, bool UpdateChungTu)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlCommand command = null;
            SqlParameter[] sqlParameters = null;
            try
            {
                connection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString);
                connection.Open();
                transaction = connection.BeginTransaction();

                if (UpdateChungTu)
                {
                    command = new SqlCommand(ctDonHang.updateProcedureName, connection, transaction);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlParameters = new SqlParameter[41];
                    sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                    sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", cenCommon.GlobalVariables.IDDonVi);
                    sqlParameters[2] = new SqlParameter("@IDDanhMucChungTu", obj.IDDanhMucChungTu);
                    sqlParameters[3] = new SqlParameter("@IDDanhMucChungTuTrangThai", obj.IDDanhMucChungTuTrangThai);
                    sqlParameters[4] = new SqlParameter("@So", obj.So);
                    sqlParameters[5] = new SqlParameter("@NgayLap", obj.NgayLap);
                    //
                    sqlParameters[6] = new SqlParameter("@IDDanhMucSale", obj.IDDanhMucSale);
                    sqlParameters[7] = new SqlParameter("@IDDanhMucKhachHang", obj.IDDanhMucKhachHang);
                    sqlParameters[8] = new SqlParameter("@DebitNote", obj.DebitNote);
                    sqlParameters[9] = new SqlParameter("@BillBooking", obj.BillBooking);
                    sqlParameters[10] = new SqlParameter("@LoaiHang", obj.LoaiHang);
                    sqlParameters[11] = new SqlParameter("@IDDanhMucNhomHangVanChuyen", obj.IDDanhMucNhomHangVanChuyen);
                    sqlParameters[12] = new SqlParameter("@IDDanhMucHangHoa", obj.IDDanhMucHangHoa);
                    sqlParameters[13] = new SqlParameter("@IDDanhMucHangTau", obj.IDDanhMucHangTau);
                    sqlParameters[14] = new SqlParameter("@SoLuong", obj.SoLuong);
                    sqlParameters[15] = new SqlParameter("@KhoiLuong", obj.KhoiLuong);
                    sqlParameters[16] = new SqlParameter("@TheTich", obj.TheTich);
                    sqlParameters[17] = new SqlParameter("@SoContainer", obj.SoContainer);
                    sqlParameters[18] = new SqlParameter("@IDDanhMucDiaDiemLayCont", obj.IDDanhMucDiaDiemLayCont);
                    sqlParameters[19] = new SqlParameter("@IDDanhMucDiaDiemTraCont", obj.IDDanhMucDiaDiemTraCont);
                    sqlParameters[20] = new SqlParameter("@NgayDongHang", obj.NgayDongHang);
                    sqlParameters[21] = new SqlParameter("@GioDongHang", obj.GioDongHang);
                    sqlParameters[22] = new SqlParameter("@NgayTraHang", obj.NgayTraHang);
                    sqlParameters[23] = new SqlParameter("@GioTraHang", obj.GioTraHang);
                    sqlParameters[24] = new SqlParameter("@IDDanhMucDiaDiemLayHang", obj.IDDanhMucDiaDiemLayHang);
                    sqlParameters[25] = new SqlParameter("@IDDanhMucDiaDiemTraHang", obj.IDDanhMucDiaDiemTraHang);
                    sqlParameters[26] = new SqlParameter("@IDDanhMucTuyenVanTai", obj.IDDanhMucTuyenVanTai);
                    sqlParameters[27] = new SqlParameter("@NgayCatMang", obj.NgayCatMang);
                    sqlParameters[28] = new SqlParameter("@GioCatMang", obj.GioCatMang);
                    sqlParameters[29] = new SqlParameter("@NguoiGiaoNhan", obj.NguoiGiaoNhan);
                    sqlParameters[30] = new SqlParameter("@SoDienThoaiGiaoNhan", obj.SoDienThoaiGiaoNhan);
                    sqlParameters[31] = new SqlParameter("@SoTienCuoc", obj.SoTienCuoc);
                    sqlParameters[32] = new SqlParameter("@SoTienThuNang", obj.SoTienThuNang);
                    sqlParameters[33] = new SqlParameter("@SoTienThuHa", obj.SoTienThuHa);
                    sqlParameters[34] = new SqlParameter("@SoTienThuKhac", obj.SoTienThuKhac);
                    sqlParameters[35] = new SqlParameter("@NoiDungThuKhac", obj.NoiDungThuKhac);
                    sqlParameters[36] = new SqlParameter("@SoTienGiamTru", obj.SoTienGiamTru);
                    sqlParameters[37] = new SqlParameter("@NoiDungGiamTru", obj.NoiDungGiamTru);
                    sqlParameters[38] = new SqlParameter("@ThoiHanThanhToan", obj.ThoiHanThanhToan);
                    //
                    sqlParameters[39] = new SqlParameter("@GhiChu", obj.GhiChu);
                    sqlParameters[40] = new SqlParameter("@IDDanhMucNguoiSuDungEdit", obj.IDDanhMucNguoiSuDungEdit);
                    command.Parameters.Clear();
                    command.Parameters.AddRange(sqlParameters);
                    int rowAffected = command.ExecuteNonQuery();
                }

                if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, obj.ID, null, cenCommon.ThaoTacDuLieu.Sua, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), connection, transaction))
                {
                    foreach (ctDonHang.ctDonHangChiTietTamUng ctChiTiet in obj.listChiTiet)
                    {
                        if (ctChiTiet.DataRowState == DataRowState.Added)
                        {
                            command = new SqlCommand(ctDonHang.insertChiTietProcedureName, connection, transaction);
                            command.CommandType = CommandType.StoredProcedure;
                            sqlParameters = new SqlParameter[18];
                            sqlParameters[0] = new SqlParameter("@ID", DBNull.Value)
                            {
                                Direction = ParameterDirection.Output,
                                Size = sizeof(Int64)
                            };
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucChungTu", obj.IDDanhMucChungTu);
                            sqlParameters[3] = new SqlParameter("@IDChungTu", ctChiTiet.IDChungTu);
                            //
                            sqlParameters[4] = new SqlParameter("@NgayTamUng", ctChiTiet.NgayTamUng);
                            sqlParameters[5] = new SqlParameter("@SoTienPhiHaTang", ctChiTiet.SoTienPhiHaTang);
                            sqlParameters[6] = new SqlParameter("@SoTienLocalCharge", ctChiTiet.SoTienLocalCharge);
                            sqlParameters[7] = new SqlParameter("@SoTienLuuBai", ctChiTiet.SoTienLuuBai);
                            sqlParameters[8] = new SqlParameter("@SoTienNangHa", ctChiTiet.SoTienNangHa);
                            sqlParameters[9] = new SqlParameter("@SoTienCuocVo", ctChiTiet.SoTienCuocVo);
                            sqlParameters[10] = new SqlParameter("@SoTienHaiQuan", ctChiTiet.SoTienHaiQuan);
                            sqlParameters[11] = new SqlParameter("@SoTienLamHang", ctChiTiet.SoTienLamHang);
                            sqlParameters[12] = new SqlParameter("@SoTienChonVo", ctChiTiet.SoTienChonVo);
                            sqlParameters[13] = new SqlParameter("@SoTienChiKhac", ctChiTiet.SoTienChiKhac);
                            sqlParameters[14] = new SqlParameter("@IDDanhMucCanBoTamUng", ctChiTiet.IDDanhMucCanBoTamUng);
                            sqlParameters[15] = new SqlParameter("@ThoiHanThanhToan", ctChiTiet.ThoiHanThanhToan);
                            sqlParameters[16] = new SqlParameter("@GhiChu", ctChiTiet.GhiChu);
                            sqlParameters[17] = new SqlParameter("@IDDanhMucNguoiSuDungCreate", cenCommon.GlobalVariables.IDDanhMucNguoiSuDung);
                            command.Parameters.Clear();
                            command.Parameters.AddRange(sqlParameters);
                            command.ExecuteNonQuery();
                            ctChiTiet.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            if (!NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(ctChiTiet), ctChiTiet.ID, obj.ID, ctChiTiet.ID, cenCommon.ThaoTacDuLieu.Them, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), connection, transaction))
                            {
                                transaction.Rollback();
                                return false;
                            }

                        }
                        if (ctChiTiet.DataRowState == DataRowState.Modified)
                        {
                            command = new SqlCommand(ctDonHang.updateChiTietProcedureName, connection, transaction);
                            command.CommandType = CommandType.StoredProcedure;
                            sqlParameters = new SqlParameter[18];
                            sqlParameters[0] = new SqlParameter("@ID", ctChiTiet.ID);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucDonVi", obj.IDDanhMucDonVi);
                            sqlParameters[2] = new SqlParameter("@IDDanhMucChungTu", obj.IDDanhMucChungTu);
                            sqlParameters[3] = new SqlParameter("@IDChungTu", ctChiTiet.IDChungTu);
                            //
                            sqlParameters[4] = new SqlParameter("@NgayTamUng", ctChiTiet.NgayTamUng);
                            sqlParameters[4] = new SqlParameter("@NgayTamUng", ctChiTiet.NgayTamUng);
                            sqlParameters[5] = new SqlParameter("@SoTienPhiHaTang", ctChiTiet.SoTienPhiHaTang);
                            sqlParameters[6] = new SqlParameter("@SoTienLocalCharge", ctChiTiet.SoTienLocalCharge);
                            sqlParameters[7] = new SqlParameter("@SoTienLuuBai", ctChiTiet.SoTienLuuBai);
                            sqlParameters[8] = new SqlParameter("@SoTienNangHa", ctChiTiet.SoTienNangHa);
                            sqlParameters[9] = new SqlParameter("@SoTienCuocVo", ctChiTiet.SoTienCuocVo);
                            sqlParameters[10] = new SqlParameter("@SoTienHaiQuan", ctChiTiet.SoTienHaiQuan);
                            sqlParameters[11] = new SqlParameter("@SoTienLamHang", ctChiTiet.SoTienLamHang);
                            sqlParameters[12] = new SqlParameter("@SoTienChonVo", ctChiTiet.SoTienChonVo);
                            sqlParameters[13] = new SqlParameter("@SoTienChiKhac", ctChiTiet.SoTienChiKhac);
                            sqlParameters[14] = new SqlParameter("@IDDanhMucCanBoTamUng", ctChiTiet.IDDanhMucCanBoTamUng);
                            sqlParameters[15] = new SqlParameter("@ThoiHanThanhToan", ctChiTiet.ThoiHanThanhToan);
                            sqlParameters[16] = new SqlParameter("@GhiChu", ctChiTiet.GhiChu);
                            sqlParameters[17] = new SqlParameter("@IDDanhMucNguoiSuDungEdit", cenCommon.GlobalVariables.IDDanhMucNguoiSuDung);
                            command.Parameters.Clear();
                            command.Parameters.AddRange(sqlParameters);
                            command.ExecuteNonQuery();
                            ctChiTiet.ID = Int64.Parse(sqlParameters[0].Value.ToString());
                            if (!NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(ctChiTiet), ctChiTiet.ID, obj.ID, ctChiTiet.ID, cenCommon.ThaoTacDuLieu.Sua, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), connection, transaction))
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        if (ctChiTiet.DataRowState == DataRowState.Deleted)
                        {
                            command = new SqlCommand(ctDonHang.deleteChiTietProcedureName, connection, transaction);
                            command.CommandType = CommandType.StoredProcedure;
                            sqlParameters = new SqlParameter[2];
                            sqlParameters[0] = new SqlParameter("@ID", ctChiTiet.ID);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucNguoiSuDungDelete", cenCommon.GlobalVariables.IDDanhMucNguoiSuDung);
                            command.Parameters.Clear();
                            command.Parameters.AddRange(sqlParameters);
                            command.ExecuteNonQuery();
                            if (!NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(ctChiTiet), ctChiTiet.ID, obj.ID, ctChiTiet.ID, cenCommon.ThaoTacDuLieu.Xoa, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), connection, transaction))
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    //}
                    transaction.Commit();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                cenCommon.cenCommon.ErrorMessageOkOnly(ex.Message);
                return false;
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                transaction.Dispose();
                connection.Close();
                connection.Dispose();
            }
        }
        public bool Delete(ctDonHang obj)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cenCommon.GlobalVariables.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(ctDonHang.deleteProcedureName, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter[] sqlParameters = new SqlParameter[2];
                            sqlParameters[0] = new SqlParameter("@ID", obj.ID);
                            sqlParameters[1] = new SqlParameter("@IDDanhMucNguoiSuDungDelete", cenCommon.GlobalVariables.IDDanhMucNguoiSuDung);
                            sqlCommand.Parameters.AddRange(sqlParameters);
                            int rowAffected = sqlCommand.ExecuteNonQuery();
                            if (NhatKyDuLieuDAO.Insert(cenCommon.cenCommon.AllPropertiesAndValues(obj), obj.ID, obj.ID, null, cenCommon.ThaoTacDuLieu.Xoa, cenCommon.cenCommon.TraTuDien(ctDonHang.tableName), sqlConnection, sqlTransaction))
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
        public object GetIDctChotDoanhThuGuiKeToan(object IDctDonHang)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@IDctDonHang", IDctDonHang);
            sqlParameters[1] = new SqlParameter("@IDctChotDoanhThuGuiKeToan", null)
            {
                Size = 8,
                Direction = ParameterDirection.InputOutput
            };
            dao.ExecNonQuery(sqlParameters, ctDonHang.getIDctChotDoanhThuGuiKeToanProcedureName);
            return sqlParameters[1].Value;
        }
        public object GetIDctChotChiPhiVanTaiGuiKeToan(object IDctDonHang)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@IDctDonHang", IDctDonHang);
            sqlParameters[1] = new SqlParameter("@IDctChotChiPhiVanTaiGuiKeToan", null)
            {
                Size = 8,
                Direction = ParameterDirection.InputOutput
            };
            dao.ExecNonQuery(sqlParameters, ctChiPhiVanTai.getIDctChotChiPhiVanTaiGuiKeToanProcedureName);
            return sqlParameters[1].Value;
        }
        public object GetIDctChotChiPhiVanTaiBoSungGuiKeToan(object IDctDonHang)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@IDctDonHang", IDctDonHang);
            sqlParameters[1] = new SqlParameter("@IDctChotChiPhiVanTaiBoSungGuiKeToan", null)
            {
                Size = 8,
                Direction = ParameterDirection.InputOutput
            };
            dao.ExecNonQuery(sqlParameters, ctChiPhiVanTaiBoSung.getIDctChotChiPhiVanTaiBoSungGuiKeToanProcedureName);
            return sqlParameters[1].Value;
        }
    }
}

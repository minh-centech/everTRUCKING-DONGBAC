using cenDTO.cenLogistics;
using System.Data;
using System.Data.SqlClient;
namespace cenDAO.cenLogistics
{
    public class ctKeHoachVanTaiDAO
    {
        public DataSet List(object TuNgay, object DenNgay)
        {
            ConnectionDAO dao = new ConnectionDAO();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@TuNgay", TuNgay);
            sqlParameters[1] = new SqlParameter("@DenNgay", DenNgay);
            DataSet ds = dao.dsList(sqlParameters, ctKeHoachVanTai.listProcedureName);
            if (ds != null && ds.Tables.Count > 1)
            {
                ds.Tables[0].TableName = "Detail";
                ds.Tables[1].TableName = "Total";
            }
            return ds;
        }
    }
}

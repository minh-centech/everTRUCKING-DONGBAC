using cenDAO.cenLogistics;
using System;
using System.Data;
namespace cenBUS.cenLogistics
{
    public class ctKeHoachVanTaiBUS
    {
        public ctKeHoachVanTaiBUS()
        {
        }

        public DataSet List(object TuNgay, object DenNgay)
        {
            try
            {
                ctKeHoachVanTaiDAO dao = new ctKeHoachVanTaiDAO();
                return dao.List(TuNgay, DenNgay);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

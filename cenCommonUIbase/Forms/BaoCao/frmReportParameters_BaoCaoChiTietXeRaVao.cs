using System;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIbase.Forms.BaoCao
{
    public partial class frmReportParameters_BaoCaoChiTietXeRaVao : Form
    {
        //Thông tin trả về
        public DataSet dsData = null; //Dataset chứa dữ liệu
        public Boolean OK = false;//Ok hay Cancel


        public frmReportParameters_BaoCaoChiTietXeRaVao()
        {
            InitializeComponent();
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            OK = true;

            Close();
        }

        private void frmReportParameters_BaoCaoChiTietXeRaVao_Load(object sender, EventArgs e)
        {
            cboBai.Items.Add("%", "Tất cả");
            cboBai.Items.Add("BAIA", "Bãi A");
            cboBai.Items.Add("BAIB", "Bãi B");
            cboBai.Value = "%";
            txtTuNgay.MaskInput = cenCommon.GlobalVariables.MaskInputDate;
            txtTuNgay.DateTime = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01");
            txtDenNgay.MaskInput = cenCommon.GlobalVariables.MaskInputDate;
            txtDenNgay.DateTime = DateTime.Now;
        }
    }
}

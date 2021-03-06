using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
namespace cenCommonUIbase.Forms.BaoCao
{
    public partial class frmReportViewer_BaoCaoChiTietXeRaVao : Form
    {
        public string TuNgay = string.Empty, DenNgay = string.Empty, Bai = string.Empty;

        public frmReportViewer_BaoCaoChiTietXeRaVao()
        {
            InitializeComponent();
            ugBaoCao.HiddenColumnsList = "[LoaiManHinh][MaDanhMucChungTu][TenDanhMucChungTu]";
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadReport()
        {
            string URL = "http://45.119.82.200/cenTANDAIDUONG/api/BaoCaoChiTietXeRaVao";
            string urlParameters = "?BAIKIEMHOA=" + Bai + "&FROMDATE=" + TuNgay + "&TODATE=" + DenNgay;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<BaoCaoChiTietXeRaVao>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                DataTable dt = ConvertToDataTable(dataObjects);
                ugBaoCao.DataSource = dt;
                ugBaoCao.DisplayLayout.Bands[0].Columns["TongTien"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            }
            else
            {
                MessageBox.Show("Mã lỗi: " + response.StatusCode + "\n" + response.ReasonPhrase);
            }
            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
        private void frmReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        private void ugBaoCao_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
        }
        /// <summary>
        /// Lọc theo giá trị trong box Quickfilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterBoxValueChanged(object sender, EventArgs e)
        {
            cenCommon.cenCommon.filterGrid(ugBaoCao, txtfilterBox.Text);
        }
        private void frmReportViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                txtfilterBox.SelectAll();
                txtfilterBox.Focus();
            }
        }
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToUpper())
            {
                case "BTEXCEL":
                    cenCommon.cenCommon.ExportGrid2Excel((UltraGrid)ugBaoCao);
                    break;
                case "BTTAILAI":
                    break;
            }
        }

        public static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, System.Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
    class BaoCaoChiTietXeRaVao
    {
        public string XeRaVaoID { get; set; }
        public string SoXe { get; set; }
        public string TenLoaiXe { get; set; }
        public string TenNguonGocXe { get; set; }
        public string TenChuHang { get; set; }
        public string LoaiHang { get; set; }
        public string TongTien { get; set; }
        public string NgayVao { get; set; }
        public string GioVao { get; set; }
        public string NgayRa { get; set; }
        public string GioRa { get; set; }
        public string TinhTrang { get; set; }
        public string User_IN { get; set; }
        public string User_OUT { get; set; }
    }
}

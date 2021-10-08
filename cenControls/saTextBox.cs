using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cenControls
{
    public partial class saTextBox : Infragistics.Win.UltraWinEditors.UltraTextEditor
    {
        public DataTable dtValid = null; //Bảng chứa dữ liệu valid
        public string procedureName = string.Empty; //Tên store lấy dữ liệu valid

        public Object IDDanhMucLoaiDoiTuong = null; //ID Danh mục loại đối tượng cần valid
        public Object ID = null; //ID Danh mục cần valid
        public Object Code = ""; //Mã danh mục cần valid, sẽ phải kết hợp với mã đơn vị khi valid

        public String ValidColumnName = "Ma"; //Tên cột dùng để valid
        public String ValueColumnName = "ID"; //Tên cột chứa giá trị trả về
        public String ReturnedColumnsList = String.Empty; //Danh sách cột chứa dữ liệu trả về
        public saTextBox[] txtMoRong = null; //TextBox chứa các cột lấy thông tin trả về

        public Boolean IsNullable = true; //Có cho phép null hay không
        public Boolean IsValid = false; //Có valid hay không
        public Boolean IsModified = false; //Có thay đổi dữ liệu hay chưa
        public Boolean IsMultiSelect = false; //Có cho phép valid nhiều mã số (phân cách bằng dấu ;) hay không
        public Boolean showLookUp = true; //Có show form tìm kiếm hay không

        public saTextBox()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Nullable = true;
            this.Appearance.ForeColorDisabled = Color.Black;
            Text = "";
            ID = null;
        }
        /// <summary>
        /// Ghi nhận sự thay đổi dữ liệu
        /// </summary>
        /// <param name="args"></param>
        protected override void OnValueChanged(EventArgs args)
        {
            this.ID = null;
            this.IsModified = true;
            base.OnValueChanged(args);
        }

        protected override void OnEnter(EventArgs e)
        {
            this.SelectAll();
            base.OnEnter(e);
        }
        public void SetDataBinding(object DataSource, string TextColumnName, bool format = false, DataSourceUpdateMode mode = DataSourceUpdateMode.OnValidation)
        {
            this.DataBindings.Clear();
            this.DataBindings.Add("Value", DataSource, TextColumnName, format, mode);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData == Keys.Enter | keyData == Keys.Tab) && keyData != Keys.Shift)
            {
                return base.ProcessDialogKey(Keys.Tab);
            }
            else
                return base.ProcessDialogKey(keyData);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            this.ID = null;
            this.IsModified = true;
            if (this.DataBindings.Count > 0)
            {
                foreach (Binding bd in this.DataBindings)
                    bd.WriteValue();
            }
            base.OnTextChanged(e);
        }
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            this.Text = this.Text.Trim();
        }
    }
}

using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucChungTuQuyTrinhUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucChungTu = null;
        cenDTO.DatabaseCore.DanhMucChungTuQuyTrinh obj = null;
        public frmDanhMucChungTuQuyTrinhUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucDonVi";
        }
        protected override void SaveData(bool AddNew)
        {
            if (Save())
            {
                if (!AddNew)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    CapNhat = 1;
                    //Xóa text box
                    txtMaDanhMucChungTuQuyTrinh.Value = null;
                    txtTenDanhMucChungTuQuyTrinh.Value = null;
                    txtMaDanhMucChungTuQuyTrinh.Focus();
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTuQuyTrinh
                {
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    IDDanhMucChungTuQuyTrinh = txtMaDanhMucChungTuQuyTrinh.ID,
                    MaDanhMucChungTuQuyTrinh = txtMaDanhMucChungTuQuyTrinh.Value,
                    TenDanhMucChungTuQuyTrinh = txtTenDanhMucChungTuQuyTrinh.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTuQuyTrinh
                {
                    ID = dataRow["ID"],
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    IDDanhMucChungTuQuyTrinh = txtMaDanhMucChungTuQuyTrinh.ID,
                    MaDanhMucChungTuQuyTrinh = txtMaDanhMucChungTuQuyTrinh.Value,
                    TenDanhMucChungTuQuyTrinh = txtTenDanhMucChungTuQuyTrinh.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucChungTuQuyTrinhBUS _BUS = new cenBUS.DatabaseCore.DanhMucChungTuQuyTrinhBUS();
            bool OK = (CapNhat == 1 || CapNhat == 3) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
            if (OK && obj != null && Int64.TryParse(obj.ID.ToString(), out Int64 _ID) && _ID > 0)
            {
                if (dataTable != null)
                {
                    if (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy)
                    {
                        DataRow dr = dataTable.NewRow();
                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            if (dataTable.Columns.Contains(prop.Name))
                                dr[prop.Name] = !cenCommon.cenCommon.IsNull(prop.GetValue(obj, null)) ? prop.GetValue(obj, null) : DBNull.Value;
                        }
                        dataTable.Rows.Add(dr);
                        dataTable.AcceptChanges();
                    }
                    else
                    {
                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            if (dataTable.Columns.Contains(prop.Name))
                                dataRow[prop.Name] = !cenCommon.cenCommon.IsNull(prop.GetValue(obj, null)) ? prop.GetValue(obj, null) : DBNull.Value;
                        }
                    }
                }
                ID = _ID;
                return true;
            }
            else
            {
                ID = null;
                return false;
            }
        }



        private void frmDanhMucDonViUpdate_Load(object sender, EventArgs e)
        {
            //Load danh mục chứng từ
            cenBUS.DatabaseCore.DanhMucChungTuBUS bus = new cenBUS.DatabaseCore.DanhMucChungTuBUS();
            DataSet dsChungTu = bus.List(null);
            txtMaDanhMucChungTuQuyTrinh.dtValid = dsChungTu.Tables[cenDTO.DatabaseCore.DanhMucChungTu.tableName];
            saTextBox[] txtChungTuMoRong = new saTextBox[1];
            txtChungTuMoRong[0] = txtTenDanhMucChungTuQuyTrinh;
            txtTenDanhMucChungTuQuyTrinh.Enabled = false;
            txtMaDanhMucChungTuQuyTrinh.txtMoRong = txtChungTuMoRong;
            txtMaDanhMucChungTuQuyTrinh.ReturnedColumnsList = "Ten";
            txtMaDanhMucChungTuQuyTrinh.IsValid = true;
            txtMaDanhMucChungTuQuyTrinh.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            if (CapNhat >= 2)
            {
                IDDanhMucChungTu = dataRow["IDDanhMucChungTu"];
                txtMaDanhMucChungTuQuyTrinh.Value = dataRow["MaDanhMucChungTuQuyTrinh"];
                txtMaDanhMucChungTuQuyTrinh.ID = dataRow["IDDanhMucChungTuQuytrinh"];
                txtTenDanhMucChungTuQuyTrinh.Value = dataRow["TenDanhMucChungTuQuyTrinh"];
            }
        }
    }
}

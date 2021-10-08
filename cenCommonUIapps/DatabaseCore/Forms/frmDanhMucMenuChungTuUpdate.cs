using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucMenuChungTuUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucMenu = null;
        cenDTO.DatabaseCore.DanhMucMenuChungTu obj = null;
        public frmDanhMucMenuChungTuUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucChungTu";
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
                    txtMaDanhMucChungTu.Value = null;
                    txtTenDanhMucChungTu.Value = null;
                    txtNoiDungHienThi.Value = null;
                    txtThuTuHienThi.Value = 1;
                    txtMaDanhMucChungTu.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucMenuChungTu
                {
                    IDDanhMucMenu = IDDanhMucMenu,
                    IDDanhMucChungTu = txtMaDanhMucChungTu.ID,
                    MaDanhMucChungTu = txtMaDanhMucChungTu.Value,
                    TenDanhMucChungTu = txtTenDanhMucChungTu.Value,
                    NoiDungHienThi = txtNoiDungHienThi.Value,
                    ThuTuHienThi = txtThuTuHienThi.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucMenuChungTu
                {
                    ID = dataRow["ID"],
                    IDDanhMucMenu = IDDanhMucMenu,
                    IDDanhMucChungTu = txtMaDanhMucChungTu.ID,
                    MaDanhMucChungTu = txtMaDanhMucChungTu.Value,
                    TenDanhMucChungTu = txtTenDanhMucChungTu.Value,
                    NoiDungHienThi = txtNoiDungHienThi.Value,
                    ThuTuHienThi = txtThuTuHienThi.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucMenuChungTuBUS _BUS = new cenBUS.DatabaseCore.DanhMucMenuChungTuBUS();
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



        private void frmDanhMucChungTuUpdate_Load(object sender, EventArgs e)
        {
            //Load danh mục chứng từ
            cenBUS.DatabaseCore.DanhMucChungTuBUS bus = new cenBUS.DatabaseCore.DanhMucChungTuBUS();
            DataSet dsChungTu = bus.List(null);
            txtMaDanhMucChungTu.dtValid = dsChungTu.Tables[cenDTO.DatabaseCore.DanhMucChungTu.tableName];
            saTextBox[] txtMoRong = new saTextBox[1];
            txtMoRong[0] = txtTenDanhMucChungTu;
            txtTenDanhMucChungTu.Enabled = false;
            txtMaDanhMucChungTu.txtMoRong = txtMoRong;
            txtMaDanhMucChungTu.ReturnedColumnsList = "Ten";
            txtMaDanhMucChungTu.IsValid = true;
            txtMaDanhMucChungTu.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            if (CapNhat >= 2)
            {
                IDDanhMucMenu = dataRow["IDDanhMucMenu"];
                txtMaDanhMucChungTu.Value = dataRow["MaDanhMucChungTu"];
                txtMaDanhMucChungTu.ID = dataRow["IDDanhMucChungTu"];
                txtTenDanhMucChungTu.Value = dataRow["TenDanhMucChungTu"];
                txtNoiDungHienThi.Value = dataRow["NoiDungHienThi"];
                txtThuTuHienThi.Value = int.Parse(dataRow["ThuTuHienThi"].ToString());
            }
        }

        private void txtTenDanhMucChungTu_ValueChanged(object sender, EventArgs e)
        {
            if (CapNhat == 1) txtNoiDungHienThi.Value = txtTenDanhMucChungTu.Value;
        }
    }
}

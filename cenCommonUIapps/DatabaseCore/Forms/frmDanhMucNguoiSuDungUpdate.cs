using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucNguoiSuDungUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucMenu = null;
        Object Password = null;
        cenDTO.DatabaseCore.DanhMucNguoiSuDung obj = null;
        public frmDanhMucNguoiSuDungUpdate()
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
                    txtMaDanhMucPhanQuyen.Value = null;
                    txtTenDanhMucPhanQuyen.Value = null;
                    txtMa.Value = null;
                    txtTen.Value = null;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucNguoiSuDung
                {
                    IDDanhMucPhanQuyen = txtMaDanhMucPhanQuyen.ID,
                    MaDanhMucPhanQuyen = txtMaDanhMucPhanQuyen.Value,
                    TenDanhMucPhanQuyen = txtTenDanhMucPhanQuyen.Value,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    isAdmin = chkAdmin.Checked,
                    Password = cenCommon.cenCommon.EncryptString(""),
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucNguoiSuDung
                {
                    ID = dataRow["ID"],
                    IDDanhMucPhanQuyen = txtMaDanhMucPhanQuyen.ID,
                    MaDanhMucPhanQuyen = txtMaDanhMucPhanQuyen.Value,
                    TenDanhMucPhanQuyen = txtTenDanhMucPhanQuyen.Value,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    isAdmin = chkAdmin.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucNguoiSuDungBUS _BUS = new cenBUS.DatabaseCore.DanhMucNguoiSuDungBUS();
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
            //Load danh mục phân quyền
            cenBUS.DatabaseCore.DanhMucPhanQuyenBUS bus = new cenBUS.DatabaseCore.DanhMucPhanQuyenBUS();
            DataSet dsPhanQuyen = bus.List(null);
            txtMaDanhMucPhanQuyen.dtValid = dsPhanQuyen.Tables[cenDTO.DatabaseCore.DanhMucPhanQuyen.tableName];
            saTextBox[] txtMoRong = new saTextBox[1];
            txtMoRong[0] = txtTenDanhMucPhanQuyen;
            txtTenDanhMucPhanQuyen.Enabled = false;
            txtMaDanhMucPhanQuyen.txtMoRong = txtMoRong;
            txtMaDanhMucPhanQuyen.ReturnedColumnsList = "Ten";
            txtMaDanhMucPhanQuyen.IsValid = true;
            txtMaDanhMucPhanQuyen.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            //
            if (CapNhat >= 2)
            {
                txtMaDanhMucPhanQuyen.Value = dataRow["MaDanhMucPhanQuyen"];
                txtMaDanhMucPhanQuyen.ID = dataRow["IDDanhMucPhanQuyen"];
                txtTenDanhMucPhanQuyen.Value = dataRow["TenDanhMucPhanQuyen"];
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                chkAdmin.Checked = Boolean.Parse(dataRow["isAdmin"].ToString());
                Password = dataRow["Password"];
            }
        }
    }
}

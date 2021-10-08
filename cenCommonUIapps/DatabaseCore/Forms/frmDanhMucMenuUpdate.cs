using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucMenuUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        cenDTO.DatabaseCore.DanhMucMenu obj = null;
        public frmDanhMucMenuUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucMenu";
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
                    txtMa.Value = null;
                    txtTen.Value = null;
                    txtThuTuHienThi.Value = 1;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucMenu
                {
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    ThuTuHienThi = txtThuTuHienThi.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucMenu
                {
                    ID = dataRow["ID"],
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    ThuTuHienThi = txtThuTuHienThi.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucMenuBUS _BUS = new cenBUS.DatabaseCore.DanhMucMenuBUS();
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

        private void frmDanhMucMenuUpdate_Load(object sender, EventArgs e)
        {
            if (CapNhat >= 2)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                txtThuTuHienThi.Value = int.Parse(dataRow["ThuTuHienThi"].ToString());
            }
        }
    }
}

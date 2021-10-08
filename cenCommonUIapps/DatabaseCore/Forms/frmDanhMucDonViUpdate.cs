using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucDonViUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        cenDTO.DatabaseCore.DanhMucDonVi obj = null;
        public frmDanhMucDonViUpdate()
        {
            InitializeComponent();
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
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {
            obj = new cenDTO.DatabaseCore.DanhMucDonVi
            {
                ID = dataRow != null ? dataRow["ID"] : null,
                Ma = txtMa.Value,
                Ten = txtTen.Value,
                CreateDate = null,
                EditDate = null
            };
            cenBUS.DatabaseCore.DanhMucDonViBUS _BUS = new cenBUS.DatabaseCore.DanhMucDonViBUS();
            bool OK = (CapNhat == cenCommon.ThaoTacDuLieu.Them || CapNhat == cenCommon.ThaoTacDuLieu.Copy) ? _BUS.Insert(ref obj) : _BUS.Update(ref obj);
            //
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
            if (CapNhat == cenCommon.ThaoTacDuLieu.Sua)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
            }
        }
    }
}

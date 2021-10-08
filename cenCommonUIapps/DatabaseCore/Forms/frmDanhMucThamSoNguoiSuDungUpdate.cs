using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucThamSoNguoiSuDungUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        cenDTO.DatabaseCore.DanhMucThamSoNguoiSuDung obj = null;
        public frmDanhMucThamSoNguoiSuDungUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucThamSoNguoiSuDung";
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
                    txtGiaTri.Value = null;
                    txtGhiChu.Value = null;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucThamSoNguoiSuDung
                {
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    GiaTri = txtGiaTri.Value,
                    GhiChu = txtGhiChu.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucThamSoNguoiSuDung
                {
                    ID = dataRow["ID"],
                    IDDanhMucDonVi = cenCommon.GlobalVariables.IDDonVi,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    GiaTri = txtGiaTri.Value,
                    GhiChu = txtGhiChu.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucThamSoNguoiSuDungBUS _BUS = new cenBUS.DatabaseCore.DanhMucThamSoNguoiSuDungBUS();
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

        private void frmDanhMucThamSoNguoiSuDungUpdate_Load(object sender, EventArgs e)
        {
            if (CapNhat >= 2)
            {
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                txtGiaTri.Value = dataRow["GiaTri"];
                txtGhiChu.Value = dataRow["GhiChu"];
            }
        }
    }
}

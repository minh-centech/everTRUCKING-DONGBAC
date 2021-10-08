using System;
using System.Data;
using System.Windows.Forms;
namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucChungTuTrangThaiUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucChungTu = null;
        cenDTO.DatabaseCore.DanhMucChungTuTrangThai obj = null;
        public frmDanhMucChungTuTrangThaiUpdate()
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
                    txtMa.Value = null;
                    txtTen.Value = null;
                    chkHachToan.Checked = false;
                    chkSua.Checked = false;
                    chkXoa.Checked = false;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTuTrangThai
                {
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    HachToan = chkHachToan.Checked,
                    Sua = chkSua.Checked,
                    Xoa = chkXoa.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTuTrangThai
                {
                    ID = dataRow["ID"],
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    HachToan = chkHachToan.Checked,
                    Sua = chkSua.Checked,
                    Xoa = chkXoa.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucChungTuTrangThaiBUS _BUS = new cenBUS.DatabaseCore.DanhMucChungTuTrangThaiBUS();
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
            if (CapNhat >= 2)
            {
                IDDanhMucChungTu = dataRow["IDDanhMucChungTu"];
                txtMa.Value = dataRow["Ma"];
                txtTen.Value = dataRow["Ten"];
                chkHachToan.Checked = Boolean.Parse(dataRow["HachToan"].ToString());
                chkSua.Checked = Boolean.Parse(dataRow["Sua"].ToString());
                chkXoa.Checked = Boolean.Parse(dataRow["Xoa"].ToString());
            }
        }
    }
}

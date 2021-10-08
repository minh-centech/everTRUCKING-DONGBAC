using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucPhanQuyenChungTuUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucPhanQuyen = null;
        cenDTO.DatabaseCore.DanhMucPhanQuyenChungTu obj = null;
        public frmDanhMucPhanQuyenChungTuUpdate()
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
                    chkXem.Checked = false;
                    chkThem.Checked = false;
                    chkSua.Checked = false;
                    chkXoa.Checked = false;
                    txtMaDanhMucChungTu.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucPhanQuyenChungTu
                {
                    IDDanhMucPhanQuyen = IDDanhMucPhanQuyen,
                    IDDanhMucChungTu = txtMaDanhMucChungTu.ID,
                    MaDanhMucChungTu = txtMaDanhMucChungTu.Value,
                    TenDanhMucChungTu = txtTenDanhMucChungTu.Value,
                    Xem = chkXem.Checked,
                    Them = chkThem.Checked,
                    Sua = chkSua.Checked,
                    Xoa = chkXoa.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucPhanQuyenChungTu
                {
                    ID = dataRow["ID"],
                    IDDanhMucPhanQuyen = IDDanhMucPhanQuyen,
                    IDDanhMucChungTu = txtMaDanhMucChungTu.ID,
                    MaDanhMucChungTu = txtMaDanhMucChungTu.Value,
                    TenDanhMucChungTu = txtTenDanhMucChungTu.Value,
                    Xem = chkXem.Checked,
                    Them = chkThem.Checked,
                    Sua = chkSua.Checked,
                    Xoa = chkXoa.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucPhanQuyenChungTuBUS _BUS = new cenBUS.DatabaseCore.DanhMucPhanQuyenChungTuBUS();
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
                IDDanhMucPhanQuyen = dataRow["IDDanhMucPhanQuyen"];
                txtMaDanhMucChungTu.Value = dataRow["MaDanhMucChungTu"];
                txtMaDanhMucChungTu.ID = dataRow["IDDanhMucChungTu"];
                txtTenDanhMucChungTu.Value = dataRow["TenDanhMucChungTu"];
                chkXem.Checked = bool.Parse(dataRow["Xem"].ToString());
                chkThem.Checked = bool.Parse(dataRow["Them"].ToString());
                chkSua.Checked = bool.Parse(dataRow["Sua"].ToString());
                chkXoa.Checked = bool.Parse(dataRow["Xoa"].ToString());
            }
        }

        private void chkXem_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkXem.Checked)
            {
                chkThem.Checked = false;
                chkSua.Checked = false;
                chkXoa.Checked = false;
            }
            chkThem.Enabled = chkXem.Checked;
            chkSua.Enabled = chkXem.Checked;
            chkXoa.Enabled = chkXem.Checked;
        }
    }
}

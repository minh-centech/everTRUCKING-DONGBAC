using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucPhanQuyenDonViUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucPhanQuyen = null;
        cenDTO.DatabaseCore.DanhMucPhanQuyenDonVi obj = null;
        public frmDanhMucPhanQuyenDonViUpdate()
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
                    txtMaDanhMucDonVi.Value = null;
                    txtTenDanhMucDonVi.Value = null;
                    chkXem.Checked = false;
                    txtMaDanhMucDonVi.Focus();
                }
            }
        }
        private bool Save()
        {
            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucPhanQuyenDonVi
                {
                    IDDanhMucPhanQuyen = IDDanhMucPhanQuyen,
                    IDDanhMucDonVi = txtMaDanhMucDonVi.ID,
                    MaDanhMucDonVi = txtMaDanhMucDonVi.Value,
                    TenDanhMucDonVi = txtTenDanhMucDonVi.Value,
                    Xem = chkXem.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucPhanQuyenDonVi
                {
                    ID = dataRow["ID"],
                    IDDanhMucPhanQuyen = IDDanhMucPhanQuyen,
                    IDDanhMucDonVi = txtMaDanhMucDonVi.ID,
                    MaDanhMucDonVi = txtMaDanhMucDonVi.Value,
                    TenDanhMucDonVi = txtTenDanhMucDonVi.Value,
                    Xem = chkXem.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucPhanQuyenDonViBUS _BUS = new cenBUS.DatabaseCore.DanhMucPhanQuyenDonViBUS();
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
            cenBUS.DatabaseCore.DanhMucDonViBUS bus = new cenBUS.DatabaseCore.DanhMucDonViBUS();
            DataTable dtChungTu = bus.List(null);
            txtMaDanhMucDonVi.dtValid = dtChungTu;
            saTextBox[] txtChungTuMoRong = new saTextBox[1];
            txtChungTuMoRong[0] = txtTenDanhMucDonVi;
            txtTenDanhMucDonVi.Enabled = false;
            txtMaDanhMucDonVi.txtMoRong = txtChungTuMoRong;
            txtMaDanhMucDonVi.ReturnedColumnsList = "Ten";
            txtMaDanhMucDonVi.IsValid = true;
            txtMaDanhMucDonVi.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            if (CapNhat >= 2)
            {
                IDDanhMucPhanQuyen = dataRow["IDDanhMucPhanQuyen"];
                txtMaDanhMucDonVi.Value = dataRow["MaDanhMucDonVi"];
                txtMaDanhMucDonVi.ID = dataRow["IDDanhMucDonVi"];
                txtTenDanhMucDonVi.Value = dataRow["TenDanhMucDonVi"];
                chkXem.Checked = bool.Parse(dataRow["Xem"].ToString());
            }
        }
    }
}

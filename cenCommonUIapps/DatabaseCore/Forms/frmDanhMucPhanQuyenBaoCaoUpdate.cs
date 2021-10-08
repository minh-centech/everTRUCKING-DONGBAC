using cenControls;
using cenDTO.DatabaseCore;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucPhanQuyenBaoCaoUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucPhanQuyen = null;
        cenDTO.DatabaseCore.DanhMucPhanQuyenBaoCao obj = null;
        public frmDanhMucPhanQuyenBaoCaoUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucBaoCao";
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
                    txtMaDanhMucBaoCao.Value = null;
                    txtTenDanhMucBaoCao.Value = null;
                    chkXem.Checked = false;
                    txtMaDanhMucBaoCao.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucPhanQuyenBaoCao
                {
                    IDDanhMucPhanQuyen = IDDanhMucPhanQuyen,
                    IDDanhMucBaoCao = txtMaDanhMucBaoCao.ID,
                    MaDanhMucBaoCao = txtMaDanhMucBaoCao.Value,
                    TenDanhMucBaoCao = txtTenDanhMucBaoCao.Value,
                    Xem = chkXem.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucPhanQuyenBaoCao
                {
                    ID = dataRow["ID"],
                    IDDanhMucPhanQuyen = IDDanhMucPhanQuyen,
                    IDDanhMucBaoCao = txtMaDanhMucBaoCao.ID,
                    MaDanhMucBaoCao = txtMaDanhMucBaoCao.Value,
                    TenDanhMucBaoCao = txtTenDanhMucBaoCao.Value,
                    Xem = chkXem.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucPhanQuyenBaoCaoBUS _BUS = new cenBUS.DatabaseCore.DanhMucPhanQuyenBaoCaoBUS();
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



        private void frmDanhMucBaoCaoUpdate_Load(object sender, EventArgs e)
        {
            //Load danh mục chứng từ
            cenBUS.DatabaseCore.DanhMucBaoCaoBUS bus = new cenBUS.DatabaseCore.DanhMucBaoCaoBUS();
            DataSet dsBaoCao = bus.List(null);
            txtMaDanhMucBaoCao.dtValid = dsBaoCao.Tables[DanhMucBaoCao.tableName];
            saTextBox[] txtMoRong = new saTextBox[1];
            txtMoRong[0] = txtTenDanhMucBaoCao;
            txtTenDanhMucBaoCao.Enabled = false;
            txtMaDanhMucBaoCao.txtMoRong = txtMoRong;
            txtMaDanhMucBaoCao.ReturnedColumnsList = "Ten";
            txtMaDanhMucBaoCao.IsValid = true;
            txtMaDanhMucBaoCao.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            if (CapNhat >= 2)
            {
                IDDanhMucPhanQuyen = dataRow["IDDanhMucPhanQuyen"];
                txtMaDanhMucBaoCao.Value = dataRow["MaDanhMucBaoCao"];
                txtMaDanhMucBaoCao.ID = dataRow["IDDanhMucBaoCao"];
                txtTenDanhMucBaoCao.Value = dataRow["TenDanhMucBaoCao"];
                chkXem.Checked = bool.Parse(dataRow["Xem"].ToString());
            }
        }
    }
}

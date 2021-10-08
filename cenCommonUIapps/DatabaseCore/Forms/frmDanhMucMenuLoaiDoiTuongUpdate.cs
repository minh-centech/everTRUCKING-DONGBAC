using cenControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static cenCommonUIapps.cenCommonUIapps;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucMenuLoaiDoiTuongUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucMenu = null;
        cenDTO.DatabaseCore.DanhMucMenuLoaiDoiTuong obj = null;
        public frmDanhMucMenuLoaiDoiTuongUpdate()
        {
            InitializeComponent();
            //LoaiDanhMuc = "DanhMucLoaiDoiTuong";
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
                    txtMaDanhMucLoaiDoiTuong.Value = null;
                    txtTenDanhMucLoaiDoiTuong.Value = null;
                    txtNoiDungHienThi.Value = null;
                    txtThuTuHienThi.Value = 1;
                    txtMaDanhMucLoaiDoiTuong.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucMenuLoaiDoiTuong
                {
                    IDDanhMucMenu = IDDanhMucMenu,
                    IDDanhMucLoaiDoiTuong = txtMaDanhMucLoaiDoiTuong.ID,
                    MaDanhMucLoaiDoiTuong = txtMaDanhMucLoaiDoiTuong.Value,
                    TenDanhMucLoaiDoiTuong = txtTenDanhMucLoaiDoiTuong.Value,
                    NoiDungHienThi = txtNoiDungHienThi.Value,
                    ThuTuHienThi = txtThuTuHienThi.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucMenuLoaiDoiTuong
                {
                    ID = dataRow["ID"],
                    IDDanhMucMenu = IDDanhMucMenu,
                    IDDanhMucLoaiDoiTuong = txtMaDanhMucLoaiDoiTuong.ID,
                    MaDanhMucLoaiDoiTuong = txtMaDanhMucLoaiDoiTuong.Value,
                    TenDanhMucLoaiDoiTuong = txtTenDanhMucLoaiDoiTuong.Value,
                    NoiDungHienThi = txtNoiDungHienThi.Value,
                    ThuTuHienThi = txtThuTuHienThi.Value,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucMenuLoaiDoiTuongBUS _BUS = new cenBUS.DatabaseCore.DanhMucMenuLoaiDoiTuongBUS();
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



        private void frmDanhMucLoaiDoiTuongUpdate_Load(object sender, EventArgs e)
        {
            //Load danh mục chứng từ
            cenBUS.DatabaseCore.DanhMucLoaiDoiTuongBUS bus = new cenBUS.DatabaseCore.DanhMucLoaiDoiTuongBUS();
            DataTable dtLoaiDoiTuong = bus.List(null);
            txtMaDanhMucLoaiDoiTuong.dtValid = dtLoaiDoiTuong;
            saTextBox[] txtMoRong = new saTextBox[1];
            txtMoRong[0] = txtTenDanhMucLoaiDoiTuong;
            txtTenDanhMucLoaiDoiTuong.Enabled = false;
            txtMaDanhMucLoaiDoiTuong.txtMoRong = txtMoRong;
            txtMaDanhMucLoaiDoiTuong.ReturnedColumnsList = "Ten";
            txtMaDanhMucLoaiDoiTuong.IsValid = true;
            txtMaDanhMucLoaiDoiTuong.Validating += new CancelEventHandler(validDanhMuc.txtBox_Validating);
            if (CapNhat >= 2)
            {
                IDDanhMucMenu = dataRow["IDDanhMucMenu"];
                txtMaDanhMucLoaiDoiTuong.Value = dataRow["MaDanhMucLoaiDoiTuong"];
                txtMaDanhMucLoaiDoiTuong.ID = dataRow["IDDanhMucLoaiDoiTuong"];
                txtTenDanhMucLoaiDoiTuong.Value = dataRow["TenDanhMucLoaiDoiTuong"];
                txtNoiDungHienThi.Value = dataRow["NoiDungHienThi"];
                txtThuTuHienThi.Value = int.Parse(dataRow["ThuTuHienThi"].ToString());
            }
        }

        private void txtTenDanhMucLoaiDoiTuong_ValueChanged(object sender, EventArgs e)
        {
            if (CapNhat == 1) txtNoiDungHienThi.Value = txtTenDanhMucLoaiDoiTuong.Value;
        }
    }
}

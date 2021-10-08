﻿using System;
using System.Data;
using System.Windows.Forms;

namespace cenCommonUIapps.DatabaseCore.Forms
{
    public partial class frmDanhMucChungTuInUpdate : cenBase.BaseForms.frmBaseDanhMucUpdate
    {
        public Object IDDanhMucChungTu = null;
        cenDTO.DatabaseCore.DanhMucChungTuIn obj = null;
        public frmDanhMucChungTuInUpdate()
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
                    txtListProcedureName.Value = null;
                    txtFileMauIn.Value = null;
                    txtSoLien.Value = 0;
                    chkPrintPreview.Checked = true;
                    txtMa.Focus();
                }
            }
        }
        private bool Save()
        {

            if (CapNhat == 1 || CapNhat == 3)
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTuIn
                {
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    ListProcedureName = txtListProcedureName.Value,
                    FileMauIn = txtFileMauIn.Value,
                    SoLien = txtSoLien.Value,
                    PrintPreview = chkPrintPreview.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            else
            {
                obj = new cenDTO.DatabaseCore.DanhMucChungTuIn
                {
                    ID = dataRow["ID"],
                    IDDanhMucChungTu = IDDanhMucChungTu,
                    Ma = txtMa.Value,
                    Ten = txtTen.Value,
                    ListProcedureName = txtListProcedureName.Value,
                    FileMauIn = txtFileMauIn.Value,
                    SoLien = txtSoLien.Value,
                    PrintPreview = chkPrintPreview.Checked,
                    CreateDate = null,
                    EditDate = null
                };
            }
            cenBUS.DatabaseCore.DanhMucChungTuInBUS _BUS = new cenBUS.DatabaseCore.DanhMucChungTuInBUS();
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
                txtListProcedureName.Value = dataRow["ListProcedureName"];
                txtFileMauIn.Value = dataRow["FileMauIn"];
                txtSoLien.Value = dataRow["SoLien"];
                chkPrintPreview.Checked = Boolean.Parse(dataRow["PrintPreview"].ToString());
            }
        }
    }
}

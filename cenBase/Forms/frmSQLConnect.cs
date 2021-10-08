﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace cenBase.Forms
{
    public partial class frmSQLConnect : cenBase.BaseForms.frmBaseDanhMuc
    {
        DataTable dtConnects;
        public frmSQLConnect()
        {
            InitializeComponent();
        }
        protected override void List()
        {
            dtConnects = new DataTable("DataConnects");
            dtConnects.Columns.Add(new DataColumn("ID", typeof(long)));
            dtConnects.Columns["ID"].AutoIncrement = true;
            dtConnects.Columns["ID"].AutoIncrementStep = 1;
            dtConnects.Columns.Add(new DataColumn("Name", typeof(String)));
            dtConnects.Columns.Add(new DataColumn("ConnectionString", typeof(String)));
            if (!File.Exists(Application.StartupPath + @"\DataConnects.xml"))
            {
                dtConnects.WriteXml(cenCommon.GlobalVariables.ConnectionFileName, XmlWriteMode.WriteSchema);
            }
            if (File.Exists(Application.StartupPath + @"\DataConnects.xml"))
            {
                dtConnects.ReadXml(cenCommon.GlobalVariables.ConnectionFileName);
            }
            foreach (DataRow dr in dtConnects.Rows)
            {
                dr["Name"] = cenCommon.cenCommon.DecryptString(dr["Name"].ToString());
                dr["ConnectionString"] = cenCommon.cenCommon.DecryptString(dr["ConnectionString"].ToString());
            }

            bsDanhMuc = new BindingSource
            {
                DataSource = dtConnects
            };
            ug.DataSource = bsDanhMuc;
        }
        protected override void InsertDanhMuc()
        {
            frmSQLConnectUpdate frmSQLConnectUpdate = new frmSQLConnectUpdate();
            frmSQLConnectUpdate.ShowDialog();
            List();
        }
        protected override void DeleteDanhMuc()
        {
            if (bsDanhMuc.Current == null) return;
            if (cenCommon.cenCommon.QuestionMessage("Bạn có chắc chắn muốn xóa không?", 0) == DialogResult.Yes)
            {
                bsDanhMuc.RemoveCurrent();
            }
            SaveData();
        }
        private void SaveData()
        {
            DataTable dtConnectCopy = dtConnects.Copy();
            foreach (DataRow dr in dtConnectCopy.Rows)
            {
                dr["Name"] = cenCommon.cenCommon.EncryptString(dr["Name"].ToString());
                dr["ConnectionString"] = cenCommon.cenCommon.EncryptString(dr["ConnectionString"].ToString());
            }
            dtConnectCopy.WriteXml(cenCommon.GlobalVariables.ConnectionFileName, XmlWriteMode.WriteSchema);
        }

    }
}

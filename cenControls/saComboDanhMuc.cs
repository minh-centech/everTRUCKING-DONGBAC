using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace cenControls
{
    public partial class saComboDanhMuc : Infragistics.Win.UltraWinEditors.UltraComboEditor
    {
        public Boolean IsNullable = false;
        public Boolean IsModified = false;
        //
        public object IDDanhMucLoaiDoiTuong = null;
        public string procedureName = null;
        public DataTable dtValid = null;
        public saComboDanhMuc()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Nullable = true;
            this.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            this.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.Appearance.ForeColorDisabled = Color.Black;
        }
        protected override void OnEnter(EventArgs e)
        {
            this.SelectAll();
            base.OnEnter(e);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData == Keys.Enter | keyData == Keys.Tab) && keyData != Keys.Shift)
            {
                return base.ProcessDialogKey(Keys.Tab);
            }
            else
                return base.ProcessDialogKey(keyData);
        }
        //protected override void OnValueChanged(EventArgs args)
        //{
        //    IsModified = true;
        //    if (this.DataBindings.Count > 0)
        //    {
        //        foreach (Binding bd in this.DataBindings)
        //            bd.WriteValue();
        //    }
        //    base.OnValueChanged(args);
        //}
        //protected override void OnTextChanged(EventArgs e)
        //{
        //    IsModified = true;
        //    if (this.DataBindings.Count > 0)
        //    {
        //        foreach (Binding bd in this.DataBindings)
        //            bd.WriteValue();
        //    }
        //    base.OnTextChanged(e);
        //}
    }
}

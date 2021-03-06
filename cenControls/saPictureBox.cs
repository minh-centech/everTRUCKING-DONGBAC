using System;
using System.Windows.Forms;
namespace cenControls
{
    public partial class saPictureBox : Infragistics.Win.UltraWinEditors.UltraPictureBox
    {
        public saPictureBox()
        {
            InitializeComponent();
            this.UseAppStyling = true;
            this.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            //this.Appearance.BorderColor = Color.Gray;
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            LoadImage();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.Enabled) return;
            switch (e.KeyCode)
            {
                case Keys.F5:
                    LoadImage();
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    this.Image = null;
                    e.Handled = true;
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }

        }
        private void LoadImage()
        {
            if (!this.Enabled) return;
            OpenFileDialog openImageDlg = new OpenFileDialog
            {
                Filter = "All image files (*.gif, *.bmp, *.png, *.jpg)|*.gif;*.bmp;*.png;*.jpg",
                RestoreDirectory = true
            };
            if (openImageDlg.ShowDialog() == DialogResult.OK)
            {
                this.Image = System.Drawing.Image.FromFile(openImageDlg.FileName);
                openImageDlg.Dispose();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }
    }
}

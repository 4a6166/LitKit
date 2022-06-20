using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LitKit1.Controls
{
    public partial class frmLoading : Form
    {
        public frmLoading(Window window)
        {
            var rect = new System.Drawing.Rectangle(window.Left, window.Top, window.Width, window.Height); //Screen.PrimaryScreen.WorkingArea;
            int x = (rect.Right - this.Width) / 2;
            int y = rect.Bottom- this.Height;
            this.Location = new System.Drawing.Point(x, y);

            InitializeComponent();
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {

        }
    }
}

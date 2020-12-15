using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WPF = System.Windows.Controls;

namespace LitKit1.ControlsWPF
{
    public partial class HoldingForm : Form
    {
        /// <summary>
        /// Opens a WPF control in a Winforms control
        /// </summary>
        /// <param name="WPF"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public HoldingForm(WPF.UserControl WPF, int height = 400, int width = 800)
        {
            InitializeComponent();
            AddWPF(WPF);

            this.Height = height;
            this.Width = width;
            
        }

        private void AddWPF(WPF.UserControl WPF)
        {
            
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;

            host.Child = WPF;

            this.Controls.Add(host);
            this.AutoSize = true;
        }


    }
}

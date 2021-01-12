using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Controls;


namespace LitKit1.ControlsWPF
{
    public partial class HoldingControl : System.Windows.Forms.UserControl
    {
        public System.Windows.Controls.UserControl WPFUserControl;
        public HoldingControl()
        {
            InitializeComponent();
        }

        public HoldingControl(System.Windows.Controls.UserControl _WPF)
        {
            InitializeComponent();
            AddWPF(_WPF);
        }

        public void AddWPF(System.Windows.Controls.UserControl _WPF)
        {
            WPFUserControl = _WPF;
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;

            host.Child = _WPF;

            this.Controls.Add(host);
            this.AutoSize = true;
        }

    }
}

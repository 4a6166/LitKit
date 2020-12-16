﻿using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WPF = System.Windows.Controls;


namespace LitKit1.ControlsWPF
{
    public partial class HoldingControl : UserControl
    {
        public HoldingControl(WPF.UserControl WPF)
        {
            InitializeComponent();
            AddWPF(WPF);
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

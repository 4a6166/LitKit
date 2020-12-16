using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tools.Exhibit;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteMain.xaml
    /// </summary>
    public partial class CiteMain : UserControl
    {
        public CiteMain()
        {
            InitializeComponent();
            AddTestExhibits();
        }

        public void AddTestExhibits()
        {
            for (int i = 0; i < 15; i++)
            {
                Exhibit exhibit = new Exhibit("Test Exhibit " + i, "ABC0000" + i);
                CiteBlock citeBlock = new CiteBlock(exhibit, CiteBlockStackPanel);
                citeBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                CiteBlockStackPanel.Children.Add(citeBlock);
            }
        }
    }
}

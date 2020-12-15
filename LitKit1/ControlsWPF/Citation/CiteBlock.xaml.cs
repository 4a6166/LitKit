using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteBlock.xaml
    /// </summary>
    public partial class CiteBlock : UserControl
    {
        public CiteBlock()
        {
            InitializeComponent();
            Flyout.Visibility = Visibility.Collapsed;
        }

        static BrushConverter bc = new BrushConverter();
        Brush GridSelectedBrush = (Brush) bc.ConvertFrom("#00FFFF00"); //Does not work.

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MainGrid.Background = Brushes.LightSlateGray;
            Flyout.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            MainGrid.Background = Brushes.WhiteSmoke;
            Flyout.Visibility = Visibility.Collapsed;
        }

    }
}

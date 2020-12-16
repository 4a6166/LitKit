using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tools.Exhibit;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteBlock.xaml
    /// </summary>
    public partial class CiteBlock : UserControl
    {
        public Exhibit exhibit { get; private set; }
        public Word.Application _app { get; private set; }

        public CiteBlock(Exhibit exhibit)
        {
            this.exhibit = exhibit;
            this._app = Globals.ThisAddIn.Application;

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

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _app.Selection.TypeText(exhibit.Description);
        }
    }
}

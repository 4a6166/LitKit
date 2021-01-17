
using LitKit1.ControlsWPF.Citation.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteFlyout.xaml
    /// </summary>
    public partial class CiteFlyout : UserControl
    {
        CiteMainVM ViewModel;

        public CiteFlyout()
        {
            ViewModel = Globals.Ribbons.Ribbon1.citeVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            InitializeComponent();
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;

            ViewModel.DeleteCite(cite);

        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;

            ViewModel.InsertCite(cite);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;

            ViewModel.OpenEditCite(cite);
        }


        private void ShowButtonText(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Visible;
        }
        private void HideButtonText(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Collapsed;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}

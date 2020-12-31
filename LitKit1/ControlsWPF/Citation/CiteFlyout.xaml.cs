
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
        Tools.Citation.Citation citation;
        CiteMainVM ViewModel;

        public CiteFlyout()
        {
            citation = (Tools.Citation.Citation)DataContext;
            ViewModel = Globals.Ribbons.Ribbon1.citeVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            InitializeComponent();
        }


        private void StackPanel_MouseEnter_1(object sender, MouseEventArgs e)
        {
            btnEdit.Background = Brushes.Transparent;
            btnEdit.BorderBrush = Brushes.Transparent;
            TextEdit.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseLeave_1(object sender, MouseEventArgs e)
        {
            TextEdit.Visibility = Visibility.Collapsed;
        }

        private void StackPanel_MouseEnter_2(object sender, MouseEventArgs e)
        {
            TextInsert.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseLeave_2(object sender, MouseEventArgs e)
        {
            TextInsert.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            TextDelete.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            TextDelete.Visibility = Visibility.Collapsed;
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mb = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this citation from the document?", "Confirm",System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (mb == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.DeleteCite(citation);
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.InsertCite(citation);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditCite(citation);
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

    }
}

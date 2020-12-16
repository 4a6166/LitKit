
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
        CiteBlock parentCiteBlock;
        StackPanel parentStackPanel;
        public CiteFlyout(CiteBlock ParentCiteBlock, StackPanel ParentStackPanel)
        {
            this.parentCiteBlock = ParentCiteBlock;
            this.parentStackPanel = ParentStackPanel;
            InitializeComponent();

        }

        private void StackPanel_MouseEnter_1(object sender, MouseEventArgs e)
        {
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
            parentStackPanel.Children.Remove(parentCiteBlock);
        }

        private void ShowButtonText(object sender, MouseEventArgs e)
        {
            
            var button = (Button)sender;
            button.BorderBrush = Brushes.Transparent;
            button.Background = Brushes.Transparent;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Visible;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

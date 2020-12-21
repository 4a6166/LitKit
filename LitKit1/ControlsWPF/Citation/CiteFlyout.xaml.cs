
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
        public CiteBlock parentCiteBlock { get; private set; }
        public StackPanel parentStackPanel { get; private set; }
        public CiteMain CiteMain { get; private set; }
        public CiteFlyout(CiteBlock ParentCiteBlock, StackPanel ParentStackPanel, CiteMain citeMain)
        {
            this.parentCiteBlock = ParentCiteBlock;
            this.parentStackPanel = ParentStackPanel;
            this.CiteMain = citeMain;
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
            //CiteMain.helper.DeleteCite(parentCiteBlock.citation);
            parentStackPanel.Children.Remove(parentCiteBlock);
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            CiteMain.helper.InsertCiteAtSelection(parentCiteBlock.citation);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //CiteMain.helper.EditCite(parentCiteBlock.citation);

        }


        private void ShowButtonText(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            //button.BorderBrush = Brushes.Transparent;
            //button.Background = Brushes.Transparent;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Visible;
        }
        private void HideButtonText(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            //button.BorderBrush = Brushes.Transparent;
            //button.Background = Brushes.Transparent;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Collapsed;
        }

    }
}

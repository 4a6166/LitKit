
using System.Collections.Generic;
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

        List<Tools.Citation.Citation> parentList;

        public CiteFlyout()
        {
            citation = (Tools.Citation.Citation)DataContext;

            InitializeComponent();
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            parentList.Remove(citation);
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //CiteMain.helper.InsertCiteAtSelection(parentCiteBlock.citation);
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

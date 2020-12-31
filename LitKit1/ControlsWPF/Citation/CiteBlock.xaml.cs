using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tools.Citation;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.ControlsWPF.Citation
{
    public partial class CiteBlock : UserControl
    {
        Tools.Citation.Citation citation;

        string CiteID;

        public CiteBlock()
        {
            InitializeComponent();
            citation = (Tools.Citation.Citation)this.DataContext;
        }


        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Flyout.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Flyout.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void CiteButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext; // this cannot be set when the control is intialized for some reason

            System.Windows.Forms.MessageBox.Show(cite.LongDescription);

        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;

            //parentList.Remove(citation);
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;

            //CiteMain.helper.InsertCiteAtSelection(parentCiteBlock.citation);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;

            //CiteMain.helper.EditCite(parentCiteBlock.citation);

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

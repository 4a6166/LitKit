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

namespace LitKit1.ControlsWPF
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        string SearchText { get; set; }
        public StackPanel PanelToFilter { get; private set; }
        public SearchBar(StackPanel panelToFilter)
        {
            SearchText = "";

            InitializeComponent();
            this.PanelToFilter = panelToFilter;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if(SearchText == "Search")
            //{
            //    SearchTextBox.Text = "";
            //}
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            //if(SearchText == "Search")
            //{
            //    SearchTextBox.Text = "Search";
            //}
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SearchText = SearchTextBox.Text;
                if (SearchText != "")
                {
                    imgMagGlass.Visibility = Visibility.Collapsed;
                    imgClear.Visibility = Visibility.Visible;
                } 
                else
                {
                    imgMagGlass.Visibility = Visibility.Visible;
                    imgClear.Visibility = Visibility.Collapsed;
                }
            }
            catch { };

        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchLabel.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchText == "")
            {
                SearchLabel.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SearchText = "";
            SearchTextBox.Text = "";
            SearchLabel.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                //Search
            }
        }
    }
}

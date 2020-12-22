using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Citation.TESTResources;

namespace Citation.UserControls
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private ObservableCollection<Cite> citations = new ObservableCollection<Cite>();

        private CiteRepository repository;

        private string SearchText;

        public MainView()
        {
            repository = new CiteRepository();
            InitializeComponent();

            CitesListView.ItemsSource = citations;
            SetCitations("All");
        }

        private void SetCitations(string CiteType)
        {
            citations.Clear();

            var cites = repository.GetCites();
            if (CiteType == "All")
            {
                foreach (Cite cite in cites)
                {
                    citations.Add(cite);
                }
            }
            else
            {
                foreach (Cite cite in cites)
                {
                    if (cite.CiteType == CiteType)
                    {
                        citations.Add(cite);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var proceed = MessageBox.Show("This will remove all citations from the document. Do you want to proceed?","Confirm", MessageBoxButton.YesNo);

            if (proceed == MessageBoxResult.Yes)
            {
                Button button = (Button)e.Source;
                var parentStackPanel = VisualTreeHelper.GetParent(button);
                var parentGrid = VisualTreeHelper.GetParent(parentStackPanel);
                var parentBorder = VisualTreeHelper.GetParent(parentGrid);
                ContentPresenter parent = (ContentPresenter)VisualTreeHelper.GetParent(parentBorder);
                Cite cite = (Cite)parent.Content;

                citations.Remove(cite);

                //repository.RemoveCiteFromDB;
                //DocumentInteractionLayer Remove all mentions of cite
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            var parentStackPanel = VisualTreeHelper.GetParent(button);
            var parentGrid = VisualTreeHelper.GetParent(parentStackPanel);
            var parentBorder = VisualTreeHelper.GetParent(parentGrid);
            ContentPresenter parent = (ContentPresenter)VisualTreeHelper.GetParent(parentBorder);
            Cite cite = (Cite)parent.Content;

            

            cite.LongDescription = "[Updated] "+cite.LongDescription;

            //repository.UpdateCiteInDB;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            var parentStackPanel = VisualTreeHelper.GetParent(button);
            var parentGrid = VisualTreeHelper.GetParent(parentStackPanel);
            var parentBorder = VisualTreeHelper.GetParent(parentGrid);
            ContentPresenter parent = (ContentPresenter)VisualTreeHelper.GetParent(parentBorder);
            Cite cite = (Cite)parent.Content;

            //DocumentInteractionLayer.InsertCiteAtSelection

            button.Background = Brushes.BurlyWood;

        }

        private void btnAllCites_Click(object sender, RoutedEventArgs e)
        {
            SetCitations("All");
        }

        private void btnExhibit_Click(object sender, RoutedEventArgs e)
        {
            SetCitations("Exhibit");
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            SetCitations("Record");
        }

        private void btnLegal_Click(object sender, RoutedEventArgs e)
        {
            SetCitations("Legal");
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            SetCitations("Other");
        }


        #region Search Bar
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //SearchText = "";
            SearchTextBox.Text = "";
            SearchLabel.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //Search
            }
        }
        #endregion
    }
}

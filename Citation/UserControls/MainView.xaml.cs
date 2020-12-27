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
using Citation.ViewModels;


namespace WPF.Citation.UserControls
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        #region Properties
        public MainViewVM ViewModel { get; private set; } 

        private ObservableCollection<Cite> citationsAll;

/// <summary>
        /// Binding property for the List View, separated from all tp allow for filtering
        /// </summary>
        private ObservableCollection<Cite> citationsVisible = new ObservableCollection<Cite>();

        private CiteRepository repository;

        private string SearchText;

        #endregion

        public MainView()
        {
            ViewModel = new MainViewVM();

            repository = ViewModel.Repository;
            InitializeComponent();
            LoadCitations();
            
            CitesListView.ItemsSource = citationsVisible;
        }


        private void LoadCitations()
        {
            citationsAll = ViewModel.Citations;
            foreach (Cite cite in citationsAll)
            {
                citationsVisible.Add(cite);
            }

        }

        #region Flyout Buttons
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

                citationsVisible.Remove(cite);

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


        #endregion

        #region CiteListFilter
        private void FilterCiteList(string CiteType)
        {
            //ObservableCollection<Cite> c = citationsVisible;

            citationsVisible.Clear();

            if (CiteType == "All")
            {
                foreach (Cite cite in citationsAll)
                {
                    citationsVisible.Add(cite);
                }
            }
            else
            {
                foreach (Cite cite in citationsAll)
                {
                    if (cite.CiteType == CiteType)
                    {
                        citationsVisible.Add(cite);
                    }
                }
            }
        }

        private void btnAllCites_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList("All");
        }

        private void btnExhibit_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList("Exhibit");
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList("Record");
        }

        private void btnLegal_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList("Legal");
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList("Other");
        }

        #endregion

        #region Search Bar
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = SearchTextBox.Text;
            if (SearchText != "")
            {
                imgMagGlass.Visibility = Visibility.Collapsed;
                imgClear.Visibility = Visibility.Visible;

                var searchBox = (TextBox)sender;

                var _citations = citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
                citationsVisible.Clear();

                foreach (Cite cite in _citations)
                {
                    citationsVisible.Add(cite);
                }
            }
            else
            {
                imgMagGlass.Visibility = Visibility.Visible;
                imgClear.Visibility = Visibility.Collapsed;

                FilterCiteList("All");
            }
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
                var searchBox = (TextBox)sender;

                var _citations = citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
                citationsVisible.Clear();

                foreach (Cite cite in _citations)
                {
                    citationsVisible.Add(cite);
                }
            }
        }
        #endregion

        private void SearchTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SBStackPlanelImage.Opacity == 1)
            {
                SBStackPlanelImage.Opacity = .5;
            }
            else SBStackPlanelImage.Opacity = 1;
        }

        private void SBStackPlanelImageDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SBStackPlanelImageDropDown.ContextMenu.IsOpen = true;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            SBPanelImage.Background = Brushes.LightGray;
            SBPanelImage.BorderBrush = Brushes.DimGray;
            SBDropDownBorder.Background = Brushes.LightGray;
            SBDropDownBorder.BorderBrush = Brushes.DimGray;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            SBPanelImage.Background = Brushes.Transparent;
            SBPanelImage.BorderBrush = Brushes.Transparent;
            SBDropDownBorder.Background = Brushes.Transparent;
            SBDropDownBorder.BorderBrush = Brushes.Transparent;
        }

    }
}

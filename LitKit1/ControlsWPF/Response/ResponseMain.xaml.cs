using LitKit1.ControlsWPF.Response.ViewModels;
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

namespace LitKit1.ControlsWPF.Response
{
    /// <summary>
    /// Interaction logic for ResponseMain.xaml
    /// </summary>
    public partial class ResponseMain : UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region properties
        private ResponseMainVM ViewModel;
        CollectionView view;

        #endregion
        public ResponseMain()
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Debug("CiteMain started");

            ViewModel = Globals.Ribbons.Ribbon1.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            this.DataContext = ViewModel;

            InitializeComponent();

            view = (CollectionView)CollectionViewSource.GetDefaultView(CiteBlockStackPanel.ItemsSource);
        }

        #region ListFilter

        private bool TextFilter(object item)
        {
                if (String.IsNullOrEmpty(SearchTextBox.Text))
                    return true;
                else
                    return ((item as Tools.Citation.Citation).LongDescription.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        #endregion

        #region Search Bar
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(CiteBlockStackPanel.ItemsSource).Refresh();
            view.Refresh();
            view.Filter = TextFilter;

            if (SearchTextBox.Text != "")
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

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchLabel.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
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
            //if (e.Key == Key.Return)
            //{
            //    var searchBox = (TextBox)sender;

            //    var _citations = ViewModel.citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
            //    ViewModel.citationsVisible.Clear();

            //    foreach (Tools.Citation.Citation cite in _citations)
            //    {
            //        ViewModel.citationsVisible.Add(cite);
            //    }
            //}
        }
        #endregion


        private void SBPanelImage_MouseEnter(object sender, MouseEventArgs e)
        {
            AddCiteLabel.Visibility = Visibility.Visible;
            SBDropDownBorder.Visibility = Visibility.Visible;
            //SBPanelImage.Background = Brushes.DimGray;
            SBStackPlanelImageDropDown.Opacity = 1;

        }

        private void SBPanelImage_MouseLeave(object sender, MouseEventArgs e)
        {
            AddCiteLabel.Visibility = Visibility.Collapsed;
            SBDropDownBorder.Visibility = Visibility.Collapsed;
            //SBPanelImage.Background = Brushes.LightGray;
            SBStackPlanelImageDropDown.Opacity = .5;

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //CiteAdd.Visibility = Visibility.Visible;
        }

        private void SBStackPlanelImageDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SBDropDownBorder.ContextMenu.IsOpen)
            {
                SBDropDownBorder.ContextMenu.IsOpen = false;
            }
            else SBDropDownBorder.ContextMenu.IsOpen = true;

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            //CiteAdd.Visibility = Visibility.Visible;
        }

        private void BatchAddCites_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.BatchImportResponses();
        }
        private void ExportCites_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ExportResponses();
        }

        private void RefreshBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            RefreshLabel.Visibility = Visibility.Visible;
        }

        private void RefreshBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            RefreshLabel.Visibility = Visibility.Collapsed;
        }

        private void RefreshBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ViewModel.RefreshCites();
        }
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            AddCiteLabel.Visibility = Visibility.Visible;
            SBDropDownBorder.Visibility = Visibility.Visible;
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            AddCiteLabel.Visibility = Visibility.Collapsed;
            SBDropDownBorder.Visibility = Visibility.Collapsed;

        }

    }
}

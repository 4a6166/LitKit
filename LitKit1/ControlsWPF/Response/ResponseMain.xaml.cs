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
using Tools.Response;

namespace LitKit1.ControlsWPF.Response
{
    /// <summary>
    /// Interaction logic for ResponseMain.xaml
    /// </summary>
    public partial class ResponseMain : UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool isInitialized = false;
        #region properties
        private ResponseMainVM ViewModel;
        CollectionView view;


        #endregion
        public ResponseMain()
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Debug("RespopnseMain started");

            ViewModel = Globals.Ribbons.Ribbon1.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            this.DataContext = ViewModel;

            InitializeComponent();

            view = (CollectionView)CollectionViewSource.GetDefaultView(ResponseBlockStackPanel.ItemsSource);

            
            isInitialized = true;
            view.Refresh();
            view.Filter = TextFilter;
        }

        #region ListFilter

        private bool TextFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchTextBox.Text))
                return (item as Tools.Response.Response).DocTypes.Contains((DocType)ResponseTypeCB.SelectedItem); /*ViewModel.DocType*/
            else

                return (
                    (item as Tools.Response.Response).DocTypes.Contains((DocType)ResponseTypeCB.SelectedItem) /*ViewModel.DocType*/
                    &&
                    (
                        (item as Tools.Response.Response).DisplayText.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                        ||
                        (item as Tools.Response.Response).Name.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    )
                    );
        }

        private void ResponseTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInitialized)
            {
                view.Refresh();
                view.Filter = TextFilter;
            }
        }

        private void UpdateListGrid_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (isInitialized)
            {
                view = (CollectionView)CollectionViewSource.GetDefaultView(ResponseBlockStackPanel.ItemsSource);

                view.Refresh();
                view.Filter = TextFilter;
            }
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




        private void BatchAddCites_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.BatchImportResponses();
        }
        private void ExportCites_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ExportResponses();
        }

        #region Top Panel Visibility Triggers
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
            ResponseAdd.Visibility = Visibility.Visible;
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
            ResponseAdd.Visibility = Visibility.Visible;
        }


        private void SettingsBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            SettingsLabel.Visibility = Visibility.Visible;
        }

        private void SettingsBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            SettingsLabel.Visibility = Visibility.Collapsed;
        }

        private void SettingsBorder_MouseUp(object sender, MouseButtonEventArgs e)
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

        #endregion

        private void RespondingTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnUpdateParties_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.updateDocProperties();
            view.Refresh();
            DocInfoExpander.IsExpanded = false;

        }

    }
}

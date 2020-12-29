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
using Microsoft.Office.Interop.Word;
using Tools.Citation;
using LitKit1.ControlsWPF.Citation.ViewModels;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Collections.ObjectModel;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteMain.xaml
    /// </summary>
    public partial class CiteMain : UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Properties
        private CiteMainVM ViewModel;
        private string SearchText;

        #endregion

        public CiteMain()
        {
            log.Debug("CiteMain started");

            ViewModel = new CiteMainVM();

            LoadCitations();

            this.DataContext = ViewModel;
            InitializeComponent();

            CiteBlockStackPanel.ItemsSource = ViewModel.citationsVisible;

        }
        private void LoadCitations()
        {
            foreach (Tools.Citation.Citation cite in ViewModel.Citations)
            {
                ViewModel.citationsVisible.Add(cite);
            }
        }

        //public void AddCitesToPanel(ObservableCollection<Tools.Citation.Citation> citations)
        //{
        //    foreach (Tools.Citation.Citation citation in citations)
        //    {
        //        //string LongExample = citation.CiteType.ToString() + " 1, "+citation.LongDescription;  //repository.CiteFormatting.FormatCiteText(citation, CitePlacementType.Long, null, 1);
        //        //CiteBlock citeBlock = new CiteBlock(this, CiteBlockStackPanel, citation, LongExample, 0, 1);
        //        //citeBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
        //        //CiteBlockStackPanel.Children.Add(citeBlock);

        //        string LongExample = citation.CiteType.ToString() + " 1, " + citation.LongDescription;  //repository.CiteFormatting.FormatCiteText(citation, CitePlacementType.Long, null, 1);
        //        CiteBlock citeBlock = new CiteBlock(this, CiteBlockStackPanel, citation, LongExample, 0, 1);
        //        citeBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
        //        CiteBlockStackPanel.Items.Add(citeBlock);
        //    }
        //}


        public List<Tools.Citation.Citation>TestCites()
        {
            log.Debug("Test Cites Created");
            List<Tools.Citation.Citation> citations = new List<Tools.Citation.Citation>();

            Tools.Citation.Citation citationFirst = new Tools.Citation.Citation("TESTID1", CiteType.Other, "Test First Citation LongDescription");
            citations.Add(citationFirst);

            for (int i = 0; i< 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID"+i.ToString(), CiteType.Exhibit, "Test Long EXHIBIT " +i.ToString());
                citations.Add(citation);
            }

            for (int i = 0; i < 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID" + i.ToString(), CiteType.Legal, "Test Long LEGAL " + i.ToString());
                citations.Add(citation);
            }

            for (int i = 0; i < 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID" + i.ToString(), CiteType.Record, "Test Long RECORD " + i.ToString());
                citations.Add(citation);
            }
            for (int i = 0; i < 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID" + i.ToString(), CiteType.Other, "Test Long OTHER " + i.ToString());
                citations.Add(citation);
            }

            return citations;

        }

        #region CiteListFilter
        private void FilterCiteList(CiteType CiteType)
        {
            //ObservableCollection<Cite> c = citationsVisible;

            ViewModel.citationsVisible.Clear();

            if (CiteType == CiteType.All)
            {
                foreach (Tools.Citation.Citation cite in ViewModel.Citations)
                {
                    ViewModel.citationsVisible.Add(cite);
                }
            }
            else
            {
                foreach (Tools.Citation.Citation cite in ViewModel.Citations)
                {
                    if (cite.CiteType == CiteType)
                    {
                        ViewModel.citationsVisible.Add(cite);
                    }
                }
            }
        }

        private void btnAllCites_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList(CiteType.All);
        }

        private void btnExhibit_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList(CiteType.Exhibit);
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList(CiteType.Record);
        }

        private void btnLegal_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList(CiteType.Legal);
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            FilterCiteList(CiteType.Other);
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

                var _citations = ViewModel.citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
                ViewModel.citationsVisible.Clear();

                foreach (Tools.Citation.Citation cite in _citations)
                {
                    ViewModel.citationsVisible.Add(cite);
                }
            }
            else
            {
                imgMagGlass.Visibility = Visibility.Visible;
                imgClear.Visibility = Visibility.Collapsed;

                FilterCiteList(CiteType.All);
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

                var _citations = ViewModel.citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
                ViewModel.citationsVisible.Clear();

                foreach (Tools.Citation.Citation cite in _citations)
                {
                    ViewModel.citationsVisible.Add(cite);
                }
            }
        }
        #endregion

        private void SBStackPlanelImageDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SBDropDownBorder.ContextMenu.IsOpen)
            {
                SBDropDownBorder.ContextMenu.IsOpen = false;
            }
            else SBDropDownBorder.ContextMenu.IsOpen = true;
        }

        private void SBStackPlanelImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TODO: open new cite panel

            if (SBStackPlanelImage.Opacity == 1)
            { 
                SBStackPlanelImage.Opacity = .5; 
            }
            else SBStackPlanelImage.Opacity = 1;
        }

        private void SBStackPlanelImage_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

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

        private void RefreshBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            RefreshLabel.Visibility = Visibility.Visible;
        }

        private void RefreshBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            RefreshLabel.Visibility = Visibility.Collapsed;
        }


        #region Formatting drag and drop

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IntroLabel.Content = "Exhibit";
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            IntroLabel.Content = "Ex.";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            IntroLabel.Content = "Exh.";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            IntroLabel.Content = "Appendix";
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            IntroLabel.Content = "Appx.";
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            IntroLabel.Content = "Tab";
        }


        private void IntroDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            IntroDropDown.Visibility = Visibility.Visible;
        }

        private void IntroDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            IntroDropDown.Visibility = Visibility.Collapsed;
        }

        private void IntroDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IntroDropDown.Visibility = Visibility.Visible;
            IntroDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void IndexDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            IndexDropDown.Visibility = Visibility.Visible;
        }

        private void IndexDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            IndexDropDown.Visibility = Visibility.Collapsed;
        }

        private void IndexDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IndexDropDown.Visibility = Visibility.Visible;
            IndexDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void PinDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            PinDropDown.Visibility = Visibility.Visible;
        }

        private void PinDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            PinDropDown.Visibility = Visibility.Collapsed;
        }

        private void PinDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PinDropDown.Visibility = Visibility.Visible;
            PinDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void OpenParenDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            OpenParenDropDown.Visibility = Visibility.Visible;
        }

        private void OpenParenDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            OpenParenDropDown.Visibility = Visibility.Collapsed;
        }

        private void OpenParenDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenParenDropDown.Visibility = Visibility.Visible;
            OpenParenDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void DescDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            DescDropDown.Visibility = Visibility.Visible;
        }

        private void DescDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            DescDropDown.Visibility = Visibility.Collapsed;
        }

        private void DescDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DescDropDown.Visibility = Visibility.Visible;
            DescDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void CloseParenDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseParenDropDown.Visibility = Visibility.Visible;
        }

        private void CloseParenDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseParenDropDown.Visibility = Visibility.Collapsed;
        }

        private void CloseParenDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseParenDropDown.Visibility = Visibility.Visible;
            CloseParenDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void CommaDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            CommaDropDown.Visibility = Visibility.Visible;
        }

        private void CommaDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            CommaDropDown.Visibility = Visibility.Collapsed;
        }

        private void CommaDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CommaDropDown.Visibility = Visibility.Visible;
            CommaDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void FreeTextDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            FreeTextDropDown.Visibility = Visibility.Visible;
        }

        private void FreeTextDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            FreeTextDropDown.Visibility = Visibility.Collapsed;
        }

        private void FreeTextDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FreeTextDropDown.Visibility = Visibility.Visible;
            FreeTextDragDropGrid.ContextMenu.IsOpen = true;
        }

        private void IDTextDragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            IDTextDropDown.Visibility = Visibility.Visible;
        }

        private void IDTextDragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            IDTextDropDown.Visibility = Visibility.Collapsed;
        }

        private void IDTextDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IDTextDropDown.Visibility = Visibility.Visible;
            IDTextDragDropGrid.ContextMenu.IsOpen = true;
        }


        #endregion

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

        private void ResetLongCite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SBPanelImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

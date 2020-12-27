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
        public CiteMainVM ViewModel { get; private set; }

        private ObservableCollection<Tools.Citation.Citation> citationsAll;

        /// <summary>
        /// Binding property for the List View, separated from all tp allow for filtering
        /// </summary>
        private ObservableCollection<Tools.Citation.Citation> citationsVisible = new ObservableCollection<Tools.Citation.Citation>();

        private CitationRepository repository;

        private string SearchText;

        #endregion

        public Application _app { get; private set; }
        public CiteHelper helper { get; private set; }

        public CiteMain()
        {
            ViewModel = new CiteMainVM();

            log.Debug("CiteMain started");

            this._app = Globals.ThisAddIn.Application;

            repository = ViewModel.Repository;
            InitializeComponent();
            LoadCitations();

            //CitesListView.ItemsSource = citationsVisible;


            repository.AddTestCitations();
            AddCitesToPanel(repository.Citations);
        }
        private void LoadCitations()
        {
            citationsAll = ViewModel.Citations;
            foreach (Tools.Citation.Citation cite in citationsAll)
            {
                citationsVisible.Add(cite);
            }

        }

        public void AddCitesToPanel(List<Tools.Citation.Citation> citations)
        {
            foreach (Tools.Citation.Citation citation in citations)
            {
                string LongExample = citation.CiteType.ToString() + " 1, "+citation.LongDescription;  //repository.CiteFormatting.FormatCiteText(citation, CitePlacementType.Long, null, 1);
                CiteBlock citeBlock = new CiteBlock(this, CiteBlockStackPanel, citation, LongExample, 0, 1);
                citeBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                CiteBlockStackPanel.Children.Add(citeBlock);
            }
        }


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

            citationsVisible.Clear();

            if (CiteType == CiteType.All)
            {
                foreach (Tools.Citation.Citation cite in citationsAll)
                {
                    citationsVisible.Add(cite);
                }
            }
            else
            {
                foreach (Tools.Citation.Citation cite in citationsAll)
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

                var _citations = citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
                citationsVisible.Clear();

                foreach (Tools.Citation.Citation cite in _citations)
                {
                    citationsVisible.Add(cite);
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

                var _citations = citationsVisible.Where(n => n.LongDescription.Contains(searchBox.Text)).ToList();
                citationsVisible.Clear();

                foreach (Tools.Citation.Citation cite in _citations)
                {
                    citationsVisible.Add(cite);
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

        private void SBDropDownBorder_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ExhibitDragDropImageDropDown_MouseEnter(object sender, MouseEventArgs e)
        {
            IntroDragAndDrop.ContextMenu.IsOpen = true;
        }

        private void ExhibitDragDropImageDropDown_MouseLeave(object sender, MouseEventArgs e)
        {
            IntroDragAndDrop.ContextMenu.IsOpen = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ExhibitIntroLabel.Content = "Exhibit";
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ExhibitIntroLabel.Content = "Ex.";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ExhibitIntroLabel.Content = "Exh.";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ExhibitIntroLabel.Content = "Appendix";
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            ExhibitIntroLabel.Content = "Appx.";
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            ExhibitIntroLabel.Content = "Tab";
        }
    }
}

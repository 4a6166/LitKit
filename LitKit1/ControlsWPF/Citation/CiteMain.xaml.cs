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
        CollectionView view;
        CiteType SearchCiteType = CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other;

        #endregion

        public CiteMain()
        {
            log.Debug("CiteMain started");

            ViewModel = Globals.Ribbons.Ribbon1.citeVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            this.DataContext = ViewModel;

            InitializeComponent();

            view = (CollectionView)CollectionViewSource.GetDefaultView(CiteBlockStackPanel.ItemsSource);

        }

        #region CiteListFilter

        private bool TextFilter(object item)
        {
            if(SearchCiteType == (CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other))
            {
                if (String.IsNullOrEmpty(SearchTextBox.Text))
                    return true;
                else
                    return ((item as Tools.Citation.Citation).LongDescription.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            }
            else
            {
                if (String.IsNullOrEmpty(SearchTextBox.Text))
                    return (item as Tools.Citation.Citation).CiteType == SearchCiteType;
                else
                    return ((item as Tools.Citation.Citation).LongDescription.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 && (item as Tools.Citation.Citation).CiteType == SearchCiteType);
            }
        }


        private void btnAllCites_Click(object sender, RoutedEventArgs e)
        {
            SearchCiteType = CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other;
            view.Refresh();
            view.Filter = TextFilter;

        }

        private void btnExhibit_Click(object sender, RoutedEventArgs e)
        {

            SearchCiteType = CiteType.Exhibit;
            view.Refresh();
            view.Filter = TextFilter;


        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            SearchCiteType = CiteType.Record;
            view.Refresh();
            view.Filter = TextFilter;

        }

        private void btnLegal_Click(object sender, RoutedEventArgs e)
        {
            SearchCiteType = CiteType.Legal;
            view.Refresh();
            view.Filter = TextFilter;

        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            SearchCiteType = CiteType.Other;
            view.Refresh();
            view.Filter = TextFilter;

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
            CiteAdd.Visibility = Visibility.Visible;
        }


        private void RefreshBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.RefreshCites();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            CiteAdd.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {

        }

        private void FormattingReset_Click(object sender, RoutedEventArgs e)
        {
            var aa = ViewModel.FormatList_Long;
            var a = DragAndDropListBox.DataContext;
            var c = DragAndDropListBox.ItemsSource;
            var b = DragAndDropListBox.Items;

            throw new NotImplementedException();

            //ViewModel.ResetFormatList();

            //IdCheckBox.IsChecked = true;
            //IndexStartNumUpDown.Value = 1;

        }

        #region Add/Remove Format Blocks
        private void LongCiteAddBlock_Click(object sender, RoutedEventArgs e)
        {
            LongCiteAddBlock.ContextMenu.IsOpen = true;
        }

        private void AddIntroBlock_Click(object sender, RoutedEventArgs e)
        {
            var count = ViewModel.FormatList_Long.Where(n => n.Type == CiteFormatPieceType.INTRO).ToList().Count;
            if (count == 0)
            {
                ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.INTRO));
            }
            else System.Windows.Forms.MessageBox.Show("Exhibit Formatting already contains an Intro Block.");
        }
        private void AddIndexBlock_Click(object sender, RoutedEventArgs e)
        {
            var count = ViewModel.FormatList_Long.Where(n => n.Type == CiteFormatPieceType.INDEX).ToList().Count;
            if (count == 0)
            {
                ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.INDEX));
            }
            else System.Windows.Forms.MessageBox.Show("Exhibit Formatting already contains an Intro Block.");
        }

        private void AddDescBlock_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.DESC));

        }

        private void AddPinBlock_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.PIN));

        }

        private void AddOtherIDBlock_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.OTHERID));

        }

        private void AddParensBlocks_click(object sender, RoutedEventArgs e)
        {
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.LPARENS));
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.RPARENS));
        }

        private void AddCommaBlock_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.COMMA));

        }
        private void AddFreeTextBloc_Click(object sender, RoutedEventArgs e)
        {
            //TODO: source for adding text to be included in free text block, add block to the repository
            ViewModel.FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.FREETEXT, "EXAMPLE FREE TEXT"));
        }

        #endregion
    }
}

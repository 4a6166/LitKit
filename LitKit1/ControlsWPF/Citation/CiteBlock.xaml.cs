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
        #region Attributes
        public Tools.Citation.Citation citation { get; private set; }
        public string CiteFormat { get; private set; }
        public int citeCountInt { get; set; }
        public Brush TypeIndicatorColor { get; set; }
        public Word.Application _app { get; private set; }

        public CiteMain CiteMain { get; private set; }
        public StackPanel StackPanelParent { get; private set; }
        public CiteFlyout Flyout { get; private set; }



        #endregion

        public CiteBlock(CiteMain citeMain, StackPanel Parent, Tools.Citation.Citation citation, string LongExampleText, int citeCount, int ExhibitNumber=1)
        {
            this.citation = citation;
            this._app = Globals.ThisAddIn.Application;
            this.CiteMain = citeMain;
            this.StackPanelParent = Parent;

            InitializeComponent();
            this.HorizontalAlignment = HorizontalAlignment.Stretch;


            this.Flyout = AddFlyout();
            CiteRefName.Text = citation.LongDescription;
            CiteLongExample.Text = LongExampleText;


            this.citeCountInt = citeCount;
            this.CiteCount.Text = citeCount.ToString();

            setTypeIndicatorColor();
            CiteTypeIndicator.BorderBrush = TypeIndicatorColor;
            setTypeIndicatorFill();

        }

        private CiteFlyout AddFlyout()
        {
            var flyout = new CiteFlyout(this, StackPanelParent, CiteMain);
            Grid.SetColumn(flyout, 1);
            Grid.SetRow(flyout, 0);
            Grid.SetRowSpan(flyout, 2);
            flyout.Width = 100;
            flyout.Visibility = Visibility.Collapsed;

            MainGrid.Children.Add(flyout);

            return flyout;
        }

        private void setTypeIndicatorColor()
        {
            switch (citation.CiteType)
            {
                case Tools.Citation.CiteType.Exhibit:
                    TypeIndicatorColor = SolutionBrushes.Exhibit;
                    break;
                case Tools.Citation.CiteType.Legal:
                    TypeIndicatorColor = SolutionBrushes.LegalCite;
                    break;
                case Tools.Citation.CiteType.Record:
                    TypeIndicatorColor = SolutionBrushes.RecordCite;
                    break;
                case Tools.Citation.CiteType.Other:
                    TypeIndicatorColor = SolutionBrushes.OtherCite;
                    break;
                default:
                    TypeIndicatorColor = SolutionBrushes.OtherCite;
                    break;
            }
        }

        private void setTypeIndicatorFill()
        {
            CiteTypeIndicator.Background = TypeIndicatorColor;

            //if (citeCountInt > 0)
            //{
            //    CiteTypeIndicator.Background = TypeIndicatorColor;
            //}
            //else
            //{ 
            //    CiteTypeIndicator.Background = Brushes.Transparent; 
            //}
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MainGrid.Background = SolutionBrushes.Primary_LightGrey;
            Flyout.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            MainGrid.Background = SolutionBrushes.RibbonBackground; //#f3f2f1
            Flyout.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _app.Selection.TypeText(citation.LongDescription);
        }

        private void CiteButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CiteMain.helper.InsertCiteAtSelection(citation);
        }
    }
}

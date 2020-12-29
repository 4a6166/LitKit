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
        public CiteType citeType { get; set; }
        public Brush TypeIndicatorColor { get; set; }

        public CiteBlock()
        {
            
            InitializeComponent();

            setTypeIndicatorColor();
            CiteTypeIndicator.BorderBrush = TypeIndicatorColor;
            CiteTypeIndicator.Background = TypeIndicatorColor;

        }

        private void setTypeIndicatorColor()
        {
            //    var type = ColorHolder.Text;

            //    switch (type.ToString())
            //    {
            //        case "Exhibit":
            //            TypeIndicatorColor = SolutionBrushes.Exhibit;
            //            break;
            //        case "Legal":
            //            TypeIndicatorColor = SolutionBrushes.LegalCite;
            //            break;
            //        case "Record":
            //            TypeIndicatorColor = SolutionBrushes.RecordCite;
            //            break;
            //        case "Other":
            //            TypeIndicatorColor = SolutionBrushes.OtherCite;
            //            break;
            //        default:
            //            TypeIndicatorColor = SolutionBrushes.OtherCite;
            //            break;
            //    }
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
        }

        private void CiteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

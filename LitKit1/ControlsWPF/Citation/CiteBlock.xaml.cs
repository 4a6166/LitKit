using LitKit1.ControlsWPF.Citation.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tools.Citation;

namespace LitKit1.ControlsWPF.Citation
{
    public partial class CiteBlock : UserControl
    {
        //Tools.Citation.Citation citation;
        CiteMainVM ViewModel;
         public CiteBlock()
        {
            
            ViewModel = Globals.Ribbons.Ribbon1.citeVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            InitializeComponent();

        }


        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Flyout.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Flyout.Visibility = Visibility.Collapsed;
        }

        private void CiteButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cite = (Tools.Citation.Citation)DataContext;
            ViewModel.InsertCite(cite);
        }

        private void CiteButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

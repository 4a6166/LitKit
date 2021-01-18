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
    /// Interaction logic for ResponseBlock.xaml
    /// </summary>
    public partial class ResponseBlock : UserControl
    {
        ResponseMainVM ViewModel;
        public ResponseBlock()
        {
            ViewModel = Globals.Ribbons.Ribbon1.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];
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
            var response = (Tools.Response.Response)DataContext;
            ViewModel.InsertResponse(response);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ResponseAddVisibility = Visibility.Visible;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var response = (Tools.Response.Response)DataContext;

            ViewModel.OpenEditResponse(response);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var response = (Tools.Response.Response)DataContext;

            ViewModel.DeleteResponse(response);
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var response = (Tools.Response.Response)DataContext;

            ViewModel.InsertResponse(response);

        }

    }
}

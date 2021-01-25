using LitKit1.ControlsWPF.Response.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LitKit1.ControlsWPF.Response
{
    /// <summary>
    /// Interaction logic for ResponseFlyout.xaml
    /// </summary>
    public partial class ResponseFlyout : UserControl
    {
        ResponseMainVM ViewModel;
        public ResponseFlyout()
        {
            ViewModel = Globals.ThisAddIn.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var response = (Tools.Response.Response)DataContext;

            ViewModel.DeleteResponse(response);

        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var response = (Tools.Response.Response)DataContext;

            ViewModel.InsertResponse(response);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var response = (Tools.Response.Response)DataContext;

            ViewModel.OpenEditResponse(response);
        }


        private void ShowButtonText(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Visible;
        }
        private void HideButtonText(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;

            var grid = (Grid)button.Content;
            grid.Children[1].Visibility = Visibility.Collapsed;
        }

    }
}

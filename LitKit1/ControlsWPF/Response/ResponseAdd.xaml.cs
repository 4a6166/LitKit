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
    /// Interaction logic for ResponseAdd.xaml
    /// </summary>
    public partial class ResponseAdd : UserControl
    {
        #region properties
        private ResponseMainVM ViewModel;


        #endregion
        public ResponseAdd()
        {
            ViewModel = Globals.Ribbons.Ribbon1.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            InitializeComponent();

        }

        private void AddFormattingMarks(TextBox TB, string FormatMark)
        {
            if (TB.SelectionLength == 0)
            {
                var first = TB.Text.Substring(0, TB.SelectionStart);
                var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);
                TB.Text = first + FormatMark + " " + FormatMark + last;

            }
            else
            {
                var first = TB.Text.Substring(0, TB.SelectionStart);
                var sel = TB.Text.Substring(TB.SelectionStart, TB.SelectionLength);
                var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);

                TB.Text = first + FormatMark + sel + FormatMark + last;
            }
        }


        private void Boldbtn_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(CustomLanguageTextBox, "**");

        }

        private void Italicsbtn_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(CustomLanguageTextBox, "//");

        }

        private void Underlinebtn_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(CustomLanguageTextBox, "__");

        }

        private void btnAddResponse_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

            string ID = Guid.NewGuid().ToString();
            string Name = NameTextBox.Text;

            List<DocType> docTypes = new List<DocType>()
            {
                ViewModel.DocType,
            };

            string text = CustomLanguageTextBox.Text;

            Tools.Response.Response response = new Tools.Response.Response(ID, Name, docTypes, text);
            ViewModel.AddNewResponse(response);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            NameTextBox.Text = "";
            CustomLanguageTextBox.Text = "";
        }
    }
}

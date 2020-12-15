using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteFlyout.xaml
    /// </summary>
    public partial class CiteFlyout : UserControl
    {
        public CiteFlyout()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            TextDelete.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            TextDelete.Visibility = Visibility.Collapsed;
        }

        private void StackPanel_MouseEnter_1(object sender, MouseEventArgs e)
        {
            TextEdit.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseLeave_1(object sender, MouseEventArgs e)
        {
            TextEdit.Visibility = Visibility.Collapsed;
        }

        private void StackPanel_MouseEnter_2(object sender, MouseEventArgs e)
        {
            TextInsert.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseLeave_2(object sender, MouseEventArgs e)
        {
            TextInsert.Visibility = Visibility.Collapsed;
        }

    }
}

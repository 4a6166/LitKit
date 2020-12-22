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

namespace Citation.UserControls
{
    /// <summary>
    /// Interaction logic for InvisibleButton.xaml
    /// </summary>
    public partial class InvisibleButton : UserControl
    {
        public InvisibleButton()
        {
            InitializeComponent();
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonImage.Height = 40;

            buttonLablel.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonImage.Height = 60;

            buttonLablel.Visibility = Visibility.Collapsed;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

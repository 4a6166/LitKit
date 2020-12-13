using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;

namespace LitKit1.ControlsWPF
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : UserControl
    {
        public Loading()
        {
            InitializeComponent();
        }



        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.WorkerReportsProgress = true;
            //worker.DoWork += worker_DoWork;
            //worker.ProgressChanged += worker_ProgressChanged;

            //worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Status.Value++;
                Thread.Sleep(100);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Status.Value = e.ProgressPercentage;
        }

        private void Status_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}

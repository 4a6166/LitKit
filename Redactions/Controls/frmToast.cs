using Microsoft.Office.Interop.Word;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LitKit1.Controls
{
    public partial class frmToast : Form
    {

        

        public frmToast(Window window)
        {
            //var rect = new System.Drawing.Rectangle(window.Left, window.Top, window.Width, window.Height); //Screen.PrimaryScreen.WorkingArea;
            //int x = (rect.Right - this.Width);
            //int y = rect.Top;
            //this.Location = new System.Drawing.Point(x, y);

            InitializeComponent();
        }

        public void timer1_Tick(object sender, EventArgs e)
        { 
            timer1.Stop();
            this.Close();
        }

        public void FirstLine(string firstLine)
        {
            title.Text = firstLine;
        }

        public void SecondLine(string secondLine)
        {
            subtitle.Text = secondLine;
        }

        public void OpenToast(string firstLine, string secondLine, int timerInterval = 5000)
        {
            title.Text = firstLine;
            subtitle.Text = secondLine;
            Show();

            timer1.Interval = timerInterval;
            timer1.Start();
            timer1.Tick += timer1_Tick;
        }
    }
}

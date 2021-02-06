using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;


namespace Services.License
{
    public partial class TrialForm : Form
    {
        public TrialForm()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            string link = "www.prelimine.com";


            new Microsoft.Office.Interop.Word.Application().Selection.Hyperlinks.Add(null, link).Follow();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            KeyEntryForm keyEntry = new KeyEntryForm();
            this.Close();
            keyEntry.Show();
        }

        public bool UpdateDays(int days)
        {
            try
            {
                textBox1.Text = "Days remaining in trial period: " + days + Environment.NewLine +
                    "Please go to www.prelimine.com to purchase a full license for LitKit.";

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

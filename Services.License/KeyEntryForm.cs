using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.License
{
    public partial class KeyEntryForm : Form
    {
        public bool KeyEntered = false;
        public KeyEntryForm()
        {
            InitializeComponent();
            tbErrorMessage.Visible = false;
        }

        private void SendLicenseKey()
        {
            string key = tbLicenseKey.Text;
            if (KeyFormatIsValid(key))
            {

                var roamingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var filePath = Path.Combine(roamingDirectory, "Prelimine\\LicenseKey.txt");

                File.WriteAllText(filePath, key);

                KeyEntered = true;
                this.Visible = false;
            }
            else
            {
                tbErrorMessage.Text = "Please enter a key with a valid format (e.g. AAA0-000A-A000-0AAA)";
                tbErrorMessage.Visible = true;
            }

        }

        private bool KeyFormatIsValid(string key)
        {

            bool testLength = key.Length == 19;

            var regex = new Regex(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$");
            bool testRegex = regex.Match(key).Groups.Count == 1;

            return testLength && testRegex;
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbLicenseKey.Paste();
        }

        private void tbLicenseKey_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendLicenseKey();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SendLicenseKey();
        }

        private void KeyEntryForm_Load(object sender, EventArgs e)
        {

        }

        private void tbErrorMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

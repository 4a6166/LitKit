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
            btnSubmit.Enabled = false;

        }

        private void SendLicenseKey()
        {
            string key = tbLicenseKey.Text;
            if (KeyFormatIsValid(key))
            {
                try
                {
                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                    MessageBox.Show(filePath);

                    filePath = (filePath + @"\Prelimine\LicenseKey.txt");

                    MessageBox.Show(filePath);


                    string path = Convert.ToString(filePath);
                    StreamWriter file = new StreamWriter(filePath, false);
                    file.WriteLine(key);
                    file.Close();


                    KeyEntered = true;
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Error creating License Key file");
                }
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
            bool testRegex = regex.Match(key).Success;

            return testLength && testRegex;
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbLicenseKey.Paste();
        }

        private void tbLicenseKey_KeyUp(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //{
            //    SendLicenseKey();
            //}
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

        private void tbLicenseKey_TextChanged(object sender, EventArgs e)
        {
            if (KeyFormatIsValid(tbLicenseKey.Text))
            {
                btnSubmit.Enabled = true;
                //tbValidMessage.Text = "Valid Format";
                //tbValidMessage.ForeColor = Color.Green;
            }
            else
            {
                btnSubmit.Enabled = false;
                //tbValidMessage.Text = "Invalid Format";
                //tbValidMessage.ForeColor = Color.Red;
            }
        }
    }
}

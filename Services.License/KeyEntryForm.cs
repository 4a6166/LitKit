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
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool KeyEntered = false;

        public string Key;
        public KeyEntryForm()
        {
            InitializeComponent();
            btnSubmit.Enabled = false;

            Key = "";
        }

        private void SendLicenseKey()
        {
            Log.Info("New Key inputted.");

            string key = tbLicenseKey.Text;
            if (KeyFormatIsValid(key))
            {
               
                Key = key;

                KeyEntered = true;
                Close();


                LicenseChecker.WriteKeyFile(key);
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

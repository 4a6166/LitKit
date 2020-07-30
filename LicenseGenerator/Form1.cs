using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rhino.Licensing;

namespace LicenseGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            crypto = new Crypto();
        }

        Crypto crypto;
        string PrivateKeyPath;
        string PublicKeyPath;

        private void ClearInputs()
        {
            tbCustName.Text = string.Empty;
            tbExpiration.Text = string.Empty;
            btnRadioTest.Checked = false;
            btnRadioStandard.Checked = false;
            btnRadioTrial.Checked = false;
            labelResult.Visible = false;
        }

        private DateTime expDate()
        {
            DateTime dateTime = DateTime.Now;

            if (btnRadioStandard.Checked)
            {
                dateTime = dateTime.AddYears(1);
            }
            else if (btnRadioTrial.Checked)
            {
                dateTime = dateTime.AddDays(30);
            }
            else if (btnRadioTest.Checked)
            {
                dateTime = dateTime.AddDays(90);
            }
            return dateTime;
        }

        private LicenseType licType()
        {
            if (btnRadioStandard.Checked)
            {
                return LicenseType.Standard;
            }
            else if (btnRadioTrial.Checked)
            {
                return LicenseType.Trial;
            }
            else if (btnRadioTest.Checked)
            {
                return LicenseType.Floating;
            }
            else return LicenseType.None;
        }

        


        private void btnNewLicense_Click(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "XML files (*.xml)|*.xml";
                fileDialog.ShowDialog();
                if (fileDialog.FileName != null)
                {
                    PrivateKeyPath = fileDialog.FileName;
                    crypto.CreateLicense(PrivateKeyPath, tbCustName.Text, expDate(), licType());

                    labelResult.Text = "New License Created.";
                    labelResult.Visible = true;
                }
            }));
        }

        private void GenerateNewKeys(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Filter = "XML files (*.xml)|*.xml";
                fileDialog.ShowDialog();
                if (fileDialog.FileName != null)
                {
                    crypto.GenerateKeys(fileDialog.FileName);
                }
            }));
        }

        private void btnValidateLicense_Click(object sender, EventArgs e)
        {
            if(PublicKeyPath != null)
            {
                Thread t = new Thread((ThreadStart)(() =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "XML files (*.xml)|*.xml";
                    fileDialog.ShowDialog();
                    if (fileDialog.FileName != null)
                    {
                        try
                        {
                            crypto.ValidateLicense(fileDialog.FileName, PublicKeyPath);
                            labelResult.Text = "License is valid";
                            labelResult.Visible = true;
                        }
                        catch
                        {
                            labelResult.Text = "License is NOT valid";
                            labelResult.Visible = true;
                        }
                    }
                }));
            }
            else
            {
                labelResult.Text = "Keys have not been imported.";
                labelResult.Visible = true;
            }

        }

        private void btnGetKeys_Click(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog fileDialogPublic = new OpenFileDialog();
                fileDialogPublic.Filter = "XML files (*.xml)|*.xml";
                fileDialogPublic.ShowDialog();
                if (fileDialogPublic.FileName != null)
                {
                    PublicKeyPath = fileDialogPublic.FileName;
                }
            }));

            t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog fileDialogPrivate = new OpenFileDialog();
                fileDialogPrivate.Filter = "XML files (*.xml)|*.xml";
                fileDialogPrivate.ShowDialog();
                if (fileDialogPrivate.FileName != null)
                {
                    PrivateKeyPath = fileDialogPrivate.FileName;
                }
            }));
        }
    }
}

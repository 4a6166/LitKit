using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Tools.Response;

namespace LitKit1.Controls.AnsResControls
{
    public partial class ctrlAnsResCustomize : UserControl
    {
        public ctrlAnsResCustomize(Response response, string docType, string respondingParty, string respondingPlural, string propoundingParty)
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
            repository = new ResponseRepository(_app);

            this.activeResponse = response;
            this.docType = docType;
            this.respondingParty = respondingParty;
            this.respondingPlural = respondingPlural;
            this.propoundingParty = propoundingParty;

            LoadComboBox1(activeResponse, docType);
            LoadResponseStandardTexts();
            LoadDocText(activeResponse);

            label1.Text = "Customize Language: " +docType;
        }

        private void EnBoldenX() //TODO: fix: not making the [X] bold, but not essential
        {

            //richTextBox1.Rtf = @"{\rtfl\ansi To reference the preceeding paragraph, type \b[X]\b0 including brackets in place of the paragraph number in custom langauge.";

            richTextBox1.Text = "To reference the preceeding paragraph, type [X] including brackets in place of the paragraph number in custom langauge.";

            //richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            //richTextBox1.AppendText("To reference the preceeding paragraph, type ");

            richTextBox1.SelectionStart = 45;
            richTextBox1.SelectionLength = 3;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            //richTextBox1.AppendText("[X]");

            //richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            //richTextBox1.AppendText(" including brackets in place of the paragraph number in custom langauge.");
        }

        Response activeResponse;
        ResponseRepository repository;
        string docType;
        string respondingParty;
        string respondingPlural;
        string propoundingParty;

        Word.Application _app;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctrlAnsResView AnsResCtrl = new ctrlAnsResView();
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(AnsResCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            AnsResCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Add new response...")
            {
                ctrlAnsResNewName newName = new ctrlAnsResNewName(repository, textBox1.Text, docType);
                frmPopup frm = new frmPopup();
                frm.Controls.Add(newName);
                newName.Dock = DockStyle.Fill;
                frm.Height = 154;
                frm.Width = 460;

                frm.StartPosition = FormStartPosition.CenterParent;

                frm.ShowDialog();

            }
            else
            {
                Response resp = (Response)comboBox1.SelectedItem;
                repository.UpdateResponse(resp.ID, resp.Name, textBox1.Text);

                ctrlAnsResView AnsResCtrl = new ctrlAnsResView();
                Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow];
                ActivePane.Control.Controls.Clear();
                //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

                ActivePane.Control.Controls.Add(AnsResCtrl);
                //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
                AnsResCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

                ActivePane.Visible = true;
                //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
            }

        }

        private List<Response> LoadResponsesByDocType(string docType)
        {
            int docTypeNode = 0;
            switch (docType)
            {
                case "Answer a Complaint":
                    docTypeNode = 0;
                    break;
                case "Respond to Requests for Admission":
                    docTypeNode = 1;
                    break;
                case "Respond to Requests for Production of Documents":
                    docTypeNode = 2;
                    break;
                case "Respond to Interrogatories":
                    docTypeNode = 3;
                    break;
                default:
                    throw new Exception("docType incorrect");
            }

            List<Response> responses = new List<Response>();

            foreach (Response res in repository.GetResponses().Where(n => n.DocTypes[docTypeNode]))
            {
                    responses.Add(res);
            }
            return responses;
        }
        private void LoadResponseStandardTexts()
        {
            listBox1.Items.Clear();
            if (comboBox1.SelectedItem.ToString() == "Add new response...")
            {
                button1.Text = "Add";
                textBox1.Clear();
            }
            else
            {
                button1.Text = "Save Changes";
                Response response = comboBox1.SelectedItem as Response;
                if (Int32.TryParse(response.ID, out int result))
                {
                    ResponseStandard responseStandard = ResponseStandardRepository.GetResponseByID(response.ID);

                    foreach (string text in responseStandard.Texts)
                    {
                        string t = ResponseStandardRepository.FillString(response.ID, text, respondingParty, respondingPlural, propoundingParty, docType);
                        listBox1.Items.Add(t);
                    }

                    listBox1.HorizontalScrollbar = true;
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadResponseStandardTexts();
            var response = comboBox1.SelectedItem as Response;
            string text = string.Empty;
            {
                if (response != null)
                {
                    text = ResponseStandardRepository.FillString(response.ID, response.DisplayText, respondingParty, respondingPlural, propoundingParty, docType);
                }

                if (comboBox1.Text == "Add new response...")
                {
                    listBox1.Visible = false;
                    label2.Visible = false;
                }
                else
                {
                    listBox1.Visible = true;
                    label2.Visible = true;
                }
            }
            textBox1.Text = text;
        }

        private void LoadComboBox1(Response response, string docType)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            List<Response> responses = new List<Response>();
            foreach (Response res in LoadResponsesByDocType(docType))
            {
                comboBox1.Items.Add(res);
                responses.Add(res);
            }
            comboBox1.DisplayMember = "Name";

            var newRes = comboBox1.Items.Add("Add new response...");

            if (response == null)
            {
                comboBox1.SelectedItem = "Add new response...";
            }
            else
            {
                var selectedResponse = responses.Where(n => n.ID == response.ID).FirstOrDefault();
                comboBox1.SelectedItem = selectedResponse;
            }
        }

        private void LoadDocText(Response response)
        {
            string text = string.Empty;
            if (response != null)
            {
                text = ResponseStandardRepository.FillString(response.ID, response.DisplayText, respondingParty, respondingPlural, propoundingParty, docType);
            }
            textBox1.Text = text;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((string)listBox1.SelectedItem == "Add new response...")
                {
                    //MessageBox.Show("Add pop up to add new response"); 
                }
                if (listBox1.SelectedItem != null)
                {
                    textBox1.Text = listBox1.SelectedItem.ToString();
                }
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #301"); }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

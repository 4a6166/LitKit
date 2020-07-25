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
using Services.Response;

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

            LoadComboBox1(response, respondingParty, respondingPlural, propoundingParty);
        }


        public ctrlAnsResCustomize()
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
            repository = new ResponseRepository(_app);

            activeResponse = null;
            this.docType = null;
            this.respondingParty = "[Responding]";
            this.respondingPlural = "Singular";
            this.propoundingParty = "[Propounding]";

            LoadComboBox1(activeResponse, respondingParty, respondingPlural, propoundingParty);
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
                string name = $"Custom Response {comboBox1.Items.Count + 1}";
                bool c = false;
                bool a = false;
                bool p = false;
                bool i = false;
                switch (docType)
                {
                    case "Complaint":
                        c = true;
                        a = false;
                        p = false;
                        i = false;
                        break;
                    case "Admission":
                        c = false;
                        a = false;
                        p = true;
                        i = false;
                        break;
                    case "Production":
                        c = false;
                        a = false;
                        p = true;
                        i = false;
                        break;
                    case "Interrogatory":
                        c = false;
                        a = false;
                        p = false;
                        i = true;
                        break;
                    default:
                        throw new Exception("docType incorrect");
                }

                ResponseRepository repository = new ResponseRepository(_app);
                repository.AddCustomResponse(name, c, a, p, i, textBox1.Text);

            }
            else
            {
                Response resp = (Response)comboBox1.SelectedItem;
                repository.UpdateResponse(resp.ID, resp.Name, textBox1.Text);
            }
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

        private void LoadCustomLanguage(Word.Application _app)
        {

        }
        private List<Response> LoadResponsesByDocType(string docType)
        {
            List<bool> types = new List<bool>();
            switch (docType)
            {
                case "Complaint":
                    types.Add(true);
                    types.Add(false);
                    types.Add(false);
                    types.Add(false);
                    break;
                case "Admission":
                    types.Add(false);
                    types.Add(true);
                    types.Add(false);
                    types.Add(false);
                    break;
                case "Production":
                    types.Add(false);
                    types.Add(false);
                    types.Add(true);
                    types.Add(false);
                    break;
                case "Interrogatory":
                    types.Add(false);
                    types.Add(false);
                    types.Add(false);
                    types.Add(true);
                    break;
                default:
                    throw new Exception("docType incorrect");
            }

            List<Response> responses = new List<Response>();

            ResponseRepository repository = new ResponseRepository(_app);
            foreach (Response res in repository.GetResponses().Where(n => n.DocTypes == types))
            {
                responses.Add(res);
            }
            return responses;
        }
        private void LoadResponseStandardTexts(string name, string respondingParty, string respondingPlural, string propoundingParty)
        {
            //listBox1.Items.Clear();
            //if (comboBox1.Text == "Add new response...")
            //{
            //    button1.Text = "Add";

            //}
            //else
            //{
            //    ResponseStandardRepository repository = new ResponseStandardRepository();
            //    ResponseStandard response = repository.GetResponseByName(comboBox1.Text);
            //    response = repository.FillStrings(response, respondingParty, respondingPlural, propoundingParty, docType);

            //    var texts = response.Texts;
            //    foreach (string text in texts)
            //    {
            //        listBox1.Items.Add(text);
            //    }
            //}

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadResponseStandardTexts(comboBox1.Text, respondingParty, respondingPlural, propoundingParty);

        }

        private void LoadComboBox1(Response response, string respondingParty, string respondingPlural, string propoundingParty)
        {
            comboBox1.Text = response.Name;

            //foreach (Response res in LoadResponsesByDocType(docType))
            //{
            //    comboBox1.Items.Add(res);
            //}

            //LoadResponseStandardTexts(ResponseName, respondingParty, respondingPlural, propoundingParty);

            //comboBox1.Items.Add("Add new response...");

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)listBox1.SelectedItem == "Add new response...")
            {
                MessageBox.Show("Add pop up to add new response"); //TODO
            }
            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

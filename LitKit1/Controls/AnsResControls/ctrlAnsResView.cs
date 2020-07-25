using Services.Response;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.Controls.AnsResControls
{
    public partial class ctrlAnsResView : UserControl
    {
        public ctrlAnsResView()
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
            repository = new ResponseRepository(_app);
            responses = repository.GetResponses();
            loadCurrentDocProperties(_app);
            loadComboBoxItems();
            LoadListBoxItems();

            UpdateViewVars();
        }


        Word.Application _app;
        string docType;
        string respondingParty;
        string respondingPlural;
        string propoundingParty;
        ResponseRepository repository;
        IEnumerable<Response> responses;


        private void loadCurrentDocProperties(Word.Application _app)
        {

            comboBox1.Text = repository.GetDocProps(_app, DocPropsNode.DocType);
            textBox1.Text = repository.GetDocProps(_app, DocPropsNode.Responding);
            textBox2.Text = repository.GetDocProps(_app, DocPropsNode.Propounding);
            if (repository.GetDocProps(_app, DocPropsNode.RespondingPlural) == "True")
            {
                checkBox1.Checked = true;
            }
            else checkBox1.Checked = false;
            
        }

        private void UpdateViewVars()
        {
            docType = comboBox1.Text;
            respondingParty = textBox1.Text;
            respondingPlural = checkBox1.Checked.ToString();
            propoundingParty = textBox2.Text;
        }

        private void loadComboBoxItems()
        {
            string complaint = "Answer a Complaint";
            string admission = "Respond to Requests for Admission";
            string production = "Respond to Requests for Production of Documents";
            string interrogatory = "Respond to Interrogatories";

            comboBox1.Items.Clear();
            comboBox1.Items.Add(complaint);
            comboBox1.Items.Add(admission);
            comboBox1.Items.Add(production);
            comboBox1.Items.Add(interrogatory);

        }

        private void LoadListBoxItems()
        {
            listBox1.Items.Clear();
            foreach (var t in responses)
            {
                int type = 0;

                switch (comboBox1.Text)
                {
                    case "Answer a Complaint":
                        type = 0;
                        break;
                    case "Respond to Requests for Admission":
                        type = 1;
                        break;
                    case "Respond to Requests for Production of Documents":
                        type = 2;
                        break;
                    case "Respond to Interrogatories":
                        type = 3;
                        break;
                    default:
                        throw new Exception("DocType incorrect");
                }

                if (t.DocTypes[type])
                {
                    var item = listBox1.Items.Add(t);
                }
                listBox1.DisplayMember = "Name";

            }
        }



        public void button1_Click(object sender, EventArgs e)
        {
            ctrlAnsResCustomize AnsResCtrl;
            if (listBox1.SelectedItem != null)
            { 
                AnsResCtrl = new ctrlAnsResCustomize(listBox1.SelectedItem as Response, docType, respondingParty, respondingPlural, propoundingParty);
            }
            else 
            {
                AnsResCtrl = new ctrlAnsResCustomize(null, docType, respondingParty, respondingPlural, propoundingParty);
            }
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(AnsResCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            AnsResCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems != null)
            {
                var listboxItem = listBox1.SelectedItem as Response;
                string insertText = listboxItem.DisplayText;

                insertText = ResponseStandardRepository.FillString(listboxItem.ID, insertText, respondingParty, respondingPlural, propoundingParty, docType);

                _app.Selection.TypeText(insertText);

                Globals.ThisAddIn.ReturnFocus();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadListBoxItems();
            docType = comboBox1.Text;
            repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            respondingParty = textBox1.Text;
            repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            propoundingParty = textBox2.Text;
            repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                respondingPlural = "True";
            }
            else respondingPlural = "False";
            repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
        }


    }
}

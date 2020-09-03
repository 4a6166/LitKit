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
            try
            {
                InitializeComponent();
                _app = Globals.ThisAddIn.Application;
                repository = new ResponseRepository(_app);
                responses = repository.GetResponses();
                loadCurrentDocProperties(_app);

                loadComboBoxItems();

                LoadListBoxItems();
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #312"); }


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
            try
            {
                UpdateViewVars();
                comboBox1.Text = docType;
                textBox1.Text = respondingParty;
                textBox2.Text = propoundingParty;
                checkBox1.Checked = bool.Parse(respondingPlural);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #313"); }


        }

        private void UpdateViewVars()
        {
            try
            {
                docType = repository.GetDocProps(_app, DocPropsNode.DocType);
                respondingParty = repository.GetDocProps(_app, DocPropsNode.Responding);
                respondingPlural = repository.GetDocProps(_app, DocPropsNode.RespondingPlural).ToString();
                propoundingParty = repository.GetDocProps(_app, DocPropsNode.Propounding);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #314"); }

        }

        private void loadComboBoxItems()
        {
            try
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
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

                comboBox1.SelectedItem = docType;
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #315"); }


        }

        private void LoadListBoxItems()
        {
            try
            {
                listBox1.Items.Clear();
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

                foreach (var t in responses)
                {
                    if (t.DocTypes[type])
                    {
                        var item = listBox1.Items.Add(t);
                    }
                }
                listBox1.DisplayMember = "Name";

                try
                {
                    listBox1.SelectedIndex = 0;
                }
                catch { }
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #316"); }

        }





        public void button1_Click(object sender, EventArgs e)
        {
            try
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
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #303"); }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItems != null)
                {
                    _app.UndoRecord.StartCustomRecord("Response Inserted");

                    var listboxItem = listBox1.SelectedItem as Response;
                    string insertText = listboxItem.DisplayText;

                    insertText = ResponseStandardRepository.FillString(listboxItem.ID, insertText, respondingParty, respondingPlural, propoundingParty, docType);


                    insertText = insertText.Replace("[X]", FillParaNumberForX(_app.Selection));

                    _app.Selection.TypeText(insertText);

                    var selEnd = _app.Selection.Start;

                    _app.Selection.SetRange(selEnd - insertText.Length, selEnd);
                    _app.Selection.Find.Execute(FindText: "\"", ReplaceWith: "\"", Replace: Word.WdReplace.wdReplaceAll);
                    _app.Selection.Find.Execute(FindText: "\'", ReplaceWith: "\'", Replace: Word.WdReplace.wdReplaceAll);

                    _app.Selection.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                    Globals.ThisAddIn.ReturnFocus();

                    _app.UndoRecord.EndCustomRecord();

                }
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #304"); }

        }

        private List<string> ParaNumberLanguages = new List<string>()
        {
            "RESPONSE TO REQUEST FOR ADMISSION",
            "RESPONSE TO PARAGRAPH",
            "RESPONSE TO INTERROGATORY",
            "RESPONSE TO REQUEST",
            "RESPONSE TO REQUEST FOR PRODUCTION OF DOCUMENTS",
            "ANSWER TO PARAGRAPH",
            "RESPONSE TO RFA",
            "RESPOSNSE TO RFP",
            "RESPONSE TO REQUEST FOR PRODUCTION"
        };

        private string GetParaNumbers(string text, Word.Paragraph paragraph)
        {
            try
            {
                string result = string.Empty;
                int languageEndLength;
                foreach (string language in ParaNumberLanguages)
                {
                    if (text.Length < language.Length + 15)
                    {
                        languageEndLength = text.Length - 1;
                    }
                    else
                    {
                        languageEndLength = language.Length + 15;
                    }
                    try
                    {
                        if (language == text.Substring(0, language.Length))
                        {
                            for (int i = language.Length; i <= languageEndLength; i++)
                            {

                                try
                                {
                                    if (char.IsDigit(text[i]))
                                    {
                                        result += text[i];
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }

                if (result == string.Empty || result == "")
                {
                    if (paragraph.Range.ListParagraphs.Count > 0)
                    {
                        for (int i = 0; i <= paragraph.Range.ListFormat.ListString.Length - 1; i++)
                        {
                            if (char.IsDigit(paragraph.Range.ListFormat.ListString[i]))
                            {
                                result += paragraph.Range.ListFormat.ListString[i];
                            }
                        }
                    }
                    else
                    {
                        int ctLen;
                        if (text.Length > 5)
                        { ctLen = 5; }
                        else { ctLen = text.Length; }
                        for (int i = 0; i <= ctLen; i++)
                        {
                            try
                            {
                                if (char.IsDigit(text[i]))
                                {
                                    result += text[i];
                                }
                            }
                            catch { }
                        }
                    }
                }

                return result;
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #306"); return null; }

        }

        private string FillParaNumberForX(Word.Selection selection)
        {
            try
            {
                string result = string.Empty;

                // Current Paragraph
                result = GetParaNumbers(selection.Paragraphs.First.Range.Text.ToUpper(), selection.Paragraphs.First);

                // Previous paragraph
                if (result == string.Empty || result == "")
                {
                    try
                    {
                        //var previousParagraph = selection.Paragraphs.First.Previous(1);
                        //string prevText = previousParagraph.Range.Text.ToUpper();

                        result = GetParaNumbers(selection.Paragraphs.First.Previous(1).Range.Text.ToUpper(), selection.Paragraphs.First.Previous(1));
                    }
                    catch
                    {

                    }
                }

                // If above do not work
                if (result == string.Empty || result == "")
                { result = "[X]"; }

                return result;
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #307"); return null; }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadListBoxItems();
                docType = comboBox1.Text;
                repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #308"); }

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
            try
            {
                respondingParty = textBox1.Text;
                repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #309"); }


        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                propoundingParty = textBox2.Text;
                repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #310"); }

        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    respondingPlural = "True";
                }
                else respondingPlural = "False";
                repository.UpdateDocProps(_app, respondingParty, respondingPlural, propoundingParty, docType);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #311"); }

        }

        private void toolTipPropoundingParty_Popup(object sender, PopupEventArgs e)
        {

        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(comboBox1, "Select the type of response you will be drafting");
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(textBox1, "Enter the Responding Party or Parties as you would like them to appear in the document");
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(textBox2, "Enter the Propounding Party as you would like it to appear in the document");

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(button1, "Select to customize the language that is inserted when a Response option is double clicked or to add a new Response option for the selected Response type.");

        }

        private void listBox1_MouseHover(object sender, EventArgs e)
        {
            string text = "the selected";
            if (listBox1.SelectedItem != null)
            {
                Response response = listBox1.SelectedItem as Response;
                text = "\""+response.Name +"\"";
            }
            toolTip.SetToolTip(listBox1, $"Insert language for the {text} Response option into the document");
        }
    }
}

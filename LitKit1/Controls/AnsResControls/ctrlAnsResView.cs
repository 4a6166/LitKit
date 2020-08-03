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
            UpdateViewVars();
            comboBox1.Text = docType;
            textBox1.Text = respondingParty;
            textBox2.Text = propoundingParty;
            checkBox1.Checked = bool.Parse(respondingPlural);

        }

        private void UpdateViewVars()
        {
            docType = repository.GetDocProps(_app, DocPropsNode.DocType);
            respondingParty = repository.GetDocProps(_app, DocPropsNode.Responding);
            respondingPlural = repository.GetDocProps(_app, DocPropsNode.RespondingPlural).ToString();
            propoundingParty = repository.GetDocProps(_app, DocPropsNode.Propounding);
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
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox1.SelectedItem = docType;

        }

        private void LoadListBoxItems()
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


                insertText = insertText.Replace("[X]", FillParaNumberForX(_app.Selection));

                _app.Selection.TypeText(insertText);

                var selEnd = _app.Selection.Start;

                _app.Selection.SetRange(selEnd-insertText.Length, selEnd);
                _app.Selection.Find.Execute(FindText: "\"", ReplaceWith: "\"", Replace: Word.WdReplace.wdReplaceAll);
                _app.Selection.Find.Execute(FindText: "\'", ReplaceWith: "\'", Replace: Word.WdReplace.wdReplaceAll);

                _app.Selection.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                Globals.ThisAddIn.ReturnFocus();
            }
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

        private string FillParaNumberForX(Word.Selection selection)
        {
            string result = string.Empty;

            #region Current Paragraph
            var currentParagraph = selection.Paragraphs.First;
            string currentText = selection.Paragraphs.First.Range.Text;
            int languageEndLength;
            foreach (string language in ParaNumberLanguages)
            {
                if (currentText.Length < language.Length + 15)
                {
                    languageEndLength = currentText.Length - 1;
                }
                else
                {
                    languageEndLength = language.Length + 15;
                }
                try
                {
                    if (language == currentText.Substring(0, language.Length))
                    {
                        for (int i = language.Length; i <= languageEndLength; i++)
                        {

                            try
                            {
                                if (char.IsDigit(currentText[i]))
                                {
                                    result += currentText[i];
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
                if (selection.Range.ListParagraphs.Count > 0)
                {
                    result = selection.Range.ListFormat.ListString;
                }
                else
                {
                    int ctLen;
                    if(currentText.Length>5)
                    { ctLen = 5; }
                    else { ctLen = currentText.Length; }
                    for (int i = 0; i <= ctLen; i++)
                    {
                        try
                        {
                            if (char.IsDigit(currentText[i]))
                            {
                                result += currentText[i];
                            }
                        }
                        catch { }
                    }
                }
            }
            #endregion

            #region Previous paragraphs
            if (result == string.Empty || result == "")
            {
                try
                {
                    var previousParagraph = selection.Paragraphs.First.Previous(1);
                    string prevText = previousParagraph.Range.Text.ToUpper();

                    foreach (string language in ParaNumberLanguages)
                    {
                        if (prevText.Length < language.Length + 15)
                        {
                            languageEndLength = currentText.Length - 1;
                        }
                        else
                        {
                            languageEndLength = language.Length + 15;
                        }
                        try
                        {
                            if (language == prevText.Substring(0, language.Length))
                            {
                                for (int i = language.Length; i <= languageEndLength; i++)
                                {

                                    try
                                    {
                                        if (char.IsDigit(prevText[i]))
                                        {
                                            result += prevText[i];
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
                        if (previousParagraph.Range.ListParagraphs.Count > 0)
                        {
                            result = selection.Paragraphs.First.Previous(1).Range.ListFormat.ListString;
                        }
                        else
                        {
                            int ctLen;
                            if (currentText.Length > 5)
                            { ctLen = 5; }
                            else { ctLen = currentText.Length; }
                            for (int i = 0; i <= ctLen; i++)
                            {
                                try
                                {
                                    if (char.IsDigit(prevText[i]))
                                    {
                                        result += prevText[i];
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            #endregion

            if (result == string.Empty || result == "")
            { result = "[X]"; }

            return result;
        }

        // Inaequate reqirements : commented out and replaced with FillParaNumberForX(selection)
        //private string GetPrecedingParaNumber(Word.Selection selection)
        //{
        //    try
        //    {
        //        string result = string.Empty;

        //        var previousParagraph = _app.Selection.Paragraphs.First.Previous(1);
        //        string prevText = previousParagraph.Range.Text.ToUpper();

        //        #region possible para start strings
        //        string AdmissionLang = "RESPONSE TO REQUEST FOR ADMISSION";
        //        int AdmissionEndLength;
        //        if (prevText.Length < AdmissionLang.Length + 15)
        //        { AdmissionEndLength = prevText.Length - 1; }
        //        else { AdmissionEndLength = AdmissionLang.Length + 15; }

        //        string ParagraphLang = "RESPONSE TO PARAGRAPH";
        //        int ParagraphEndLength;
        //        if (prevText.Length < ParagraphLang.Length + 15)
        //        { ParagraphEndLength = prevText.Length - 1; }
        //        else { ParagraphEndLength = ParagraphLang.Length + 15; }

        //        string InterrogatoryLang = "RESPONSE TO INTERROGATORY";
        //        int InterrogatoryEndLength;
        //        if (prevText.Length < InterrogatoryLang.Length + 15)
        //        { InterrogatoryEndLength = prevText.Length - 1; }
        //        else { InterrogatoryEndLength = InterrogatoryLang.Length + 15; }

        //        string RequestLang = "RESPONSE TO REQUEST";
        //        int RequestEndLength;
        //        if (prevText.Length < RequestLang.Length + 15)
        //        { RequestEndLength = prevText.Length - 1; }
        //        else { RequestEndLength = RequestLang.Length + 15; }

        //        string DocsLang = "RESPONSE TO REQUEST FOR PRODUCTION OF DOCUMENTS";
        //        int DocsEndLength;
        //        if (prevText.Length < DocsLang.Length + 15)
        //        { DocsEndLength = prevText.Length - 1; }
        //        else { DocsEndLength = DocsLang.Length + 15; }

        //        string ParagraphANSLang = "ANSWER TO PARAGRAPH";
        //        int ParagraphANSLength;
        //        if (prevText.Length < ParagraphANSLang.Length + 15)
        //        { ParagraphANSLength = prevText.Length - 1; }
        //        else { ParagraphANSLength = ParagraphANSLang.Length + 15; }

        //        string RFALang = "RESPONSE TO RFA";
        //        int RFAEndLength;
        //        if (prevText.Length < RFALang.Length + 15)
        //        { RFAEndLength = prevText.Length - 1; }
        //        else { RFAEndLength = RFALang.Length + 15; }

        //        string RFPLang = "RESPOSNSE TO RFP";
        //        int RFPEndLength;
        //        if (prevText.Length < RFPLang.Length + 15)
        //        { RFPEndLength = prevText.Length - 1; }
        //        else { RFPEndLength = RFPLang.Length + 15; }

        //        string ProdLang = "RESPONSE TO REQUEST FOR PRODUCTION";
        //        int ProdEndLength;
        //        if (prevText.Length < ProdLang.Length + 15)
        //        { ProdEndLength = prevText.Length - 1; }
        //        else { ProdEndLength = ProdLang.Length + 15; }
        //        #endregion

        //        #region current para starts with string
        //        if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(AdmissionLang))
        //        {
        //            for (int i = AdmissionLang.Length; i <= AdmissionEndLength; i++)
        //            {
        //                var a = _app.Selection.Paragraphs[1].Range.Text.Substring(0, AdmissionEndLength);
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(ParagraphLang))
        //        {
        //            for (int i = ParagraphLang.Length; i <= ParagraphEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(InterrogatoryLang))
        //        {
        //            for (int i = InterrogatoryLang.Length; i <= InterrogatoryEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(RequestLang))
        //        {
        //            for (int i = RequestLang.Length; i <= RequestEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(DocsLang))
        //        {
        //            for (int i = DocsLang.Length; i <= DocsEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(ParagraphANSLang))
        //        {
        //            for (int i = ParagraphANSLang.Length; i <= ParagraphANSLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(RFALang))
        //        {
        //            for (int i = RFALang.Length; i <= RFAEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(RFPLang))
        //        {
        //            for (int i = RFPLang.Length; i <= RFPEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (_app.Selection.Paragraphs[1].Range.Text.StartsWith(ProdLang))
        //        {
        //            for (int i = ProdLang.Length; i <= ProdEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        #endregion

        //        #region previous para starts with string
        //        else if (prevText.StartsWith(AdmissionLang))
        //        {
                    
        //            for (int i = AdmissionLang.Length; i <= AdmissionEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(ParagraphLang))
        //        {
                    
        //            for (int i = ParagraphLang.Length; i <= ParagraphEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(InterrogatoryLang))
        //        {
                    
        //            for (int i = InterrogatoryLang.Length; i <= InterrogatoryEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(RequestLang))
        //        {
                    
        //            for (int i = RequestLang.Length; i <= RequestEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(DocsLang))
        //        {
                    
        //            for (int i = DocsLang.Length; i <= DocsEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(ParagraphANSLang))
        //        {
                    
        //            for (int i = ParagraphANSLang.Length; i <= ParagraphANSLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(RFALang))
        //        {
                    
        //            for (int i = RFALang.Length; i <= RFAEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(RFPLang))
        //        {
                    
        //            for (int i = RFPLang.Length; i <= RFPEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        else if (prevText.StartsWith(ProdLang))
        //        {
                    
        //            for (int i = ProdLang.Length; i <= ProdEndLength; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }
        //        }
        //        #endregion

        //        else if (_app.Selection.Range.ListParagraphs.Count > 0)
        //        {
        //            result = _app.Selection.Range.ListFormat.ListString;
        //        }

        //        else if (previousParagraph.Range.ListParagraphs.Count>0)
        //        {
        //            result = _app.Selection.Paragraphs.First.Previous(1).Range.ListFormat.ListString;
        //        }
        //        else
        //        {
        //            for (int i = 0; i <= 5; i++)
        //            {
        //                if (char.IsDigit(prevText[i]))
        //                {
        //                    result += prevText[i];
        //                }
        //            }

        //        }
        //        if (result == string.Empty || result == "")
        //        { result = "[X]"; }
        //        return result;
        //    }
        //    catch
        //    { return "[X]"; } 
        //}
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

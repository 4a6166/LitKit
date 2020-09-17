using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LitKit1.Controls;
using LitKit1.Controls.RedactionControls;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using Tools.Exhibit;
using Tools.RedactionTool;
using Ribbon = Ribbon_0._0._1;
using Tools.Simple;
using LitKit1.Controls.AnsResControls;
using Tools.Response;
using Services.Licensing;
using System.IO;

namespace LitKit1
{
    public partial class MainRibbon
    {
        public Microsoft.Office.Interop.Word.Application _app; //This is necessary for passing ThisAddIn.Application to the Services project
        public CustomXMLParts XMLParts => Globals.ThisAddIn.Application.ActiveDocument.CustomXMLParts;

        // Set designer properties of tab: ContorlID Type: Custom, Position: AfterOfficeId TabHome
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            /// may have to export to XML to add an image to the shrunken button groups. More here: https://stackoverflow.com/questions/45805664/how-to-set-icon-for-resized-buttom-group-in-excel-ribbon and https://docs.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-templates
            _app = Globals.ThisAddIn.Application;

            //MessageBox.Show(ClassLibrary1.Class1.test());

            btnInsertNBS.SuperTip = NBSSuperTip();

            licenseIsValid = LicenseChecker.LicenseIsValid();
            //licenseIsValid = true;
        }

        private bool licenseIsValid;
        private void ShowLicenseNotValidMessage()
        {
            MessageBox.Show("Your Prelimine LitKit License key is not valid. Please contact your IT administrator or Prelimine for a new license.") ;
        }


        #region Insert Symbols Button Click

        private void btnPilcrow_Click(object sender, RibbonControlEventArgs e)
        {
            //_app.Selection.TypeText("¶");
            _app.Selection.InsertSymbol(182);
        }

        private void insertCopyright_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0169);
        }

        private void insertNBS_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(160);
        }
        private void btnNBHyphen_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.TypeText("\u2011");
        }

        private void insertTM_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0153);
        }

        private void insertSectionMark_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(167);
        }

        private void insertNDash_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0150);
        }

        private void insertMDash_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0151);
        }

        #endregion

        private string NBSSuperTip()
        {
            try
            {
                string result = "Inserts Non-Breaking Spaces after " + InsertNBS.ExpressionsSpaceAfter.First();
                for (var i = 1; i <= InsertNBS.ExpressionsSpaceAfter.Count - 2; i++)
                {
                    result += ", " + InsertNBS.ExpressionsSpaceAfter[i];
                }
                result += " and " + InsertNBS.ExpressionsSpaceAfter.Last();
                return result;
            }
            catch { return null; }
        }

        private void ClipboardButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                _app.ShowClipboard();
            }
        }

        private void ExhibitTool_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    //AddExhibtsForTest();

                    ctrlExhibitView exhibitCtrl = new ctrlExhibitView();
                    Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
                    ActivePane.Control.Controls.Clear();
                    //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

                    ActivePane.Control.Controls.Add(exhibitCtrl);
                    //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
                    exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
                    exhibitCtrl.LoadListView();

                    if (!ActivePane.Visible)
                    {
                        ActivePane.Visible = true;
                    }
                    else
                    {
                        ActivePane.Visible = false;
                    }
                    //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #201");}
            }
        }


        private void AddExhibtsForTest(object sender, RibbonControlEventArgs e)
        {
            ExhibitHelper helper = new ExhibitHelper();
            IExhibitRepository repository = ExhibitRepositoryFactory.GetRepository("XML", _app);

            if (repository.GetExhibits().Count() == 0)
            {
                repository.AddExhibit("A" +" " +Guid.NewGuid().ToString("N").Substring(16), Guid.NewGuid().ToString("N").Substring(8));
            }
            
            repository.AddExhibit(helper.ToAlphabet(repository.GetExhibits().Count() + 1) + " " + Guid.NewGuid().ToString("N").Substring(16), Guid.NewGuid().ToString("N").Substring(8));
            repository.AddExhibit(helper.ToAlphabet(repository.GetExhibits().Count() + 1) + " " + Guid.NewGuid().ToString("N").Substring(16), Guid.NewGuid().ToString("N").Substring(8));
            repository.AddExhibit(helper.ToAlphabet(repository.GetExhibits().Count() + 1) + " " + Guid.NewGuid().ToString("N").Substring(16), Guid.NewGuid().ToString("N").Substring(8));
            repository.AddExhibit(helper.ToAlphabet(repository.GetExhibits().Count() + 1) + " " + Guid.NewGuid().ToString("N").Substring(16), Guid.NewGuid().ToString("N").Substring(8));

            frmToast toast = new frmToast(_app.ActiveWindow);
            toast.OpenToast("Test Exhibits Added", "Remove before production.",1000);
        }
        

        private void Test_Button_Click(object sender, RibbonControlEventArgs e)
        {

            _app.UndoRecord.StartCustomRecord("Test Button Stuff");

            ResponseRepository repository = new ResponseRepository(_app);

            //repository.AddCustomResponse("Test Add", false, true, false, false, "Test display text");
            repository.GetDocProps(_app, DocPropsNode.Propounding);

            ResponseStandardRepository repoStandard = new ResponseStandardRepository();
            //_app.Selection.TypeText(repoStandard.GetAllTexts());

            _app.UndoRecord.EndCustomRecord();
        }

        private void btnKeepWithNext_Click(object sender, RibbonControlEventArgs e)
        {

            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    if (_app.Selection.Paragraphs.KeepWithNext == 0)
                    {
                        _app.Selection.Paragraphs.KeepWithNext = -1;
                    }
                    else
                    {
                        _app.Selection.Paragraphs.KeepWithNext = 0;
                    }
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #202"); }
            }
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {

            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    ctrlAnsResView AnsResCtrl = new ctrlAnsResView();
                    Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow];
                    ActivePane.Control.Controls.Clear();
                    //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

                    ActivePane.Control.Controls.Add(AnsResCtrl);
                    //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
                    AnsResCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

                    if (!ActivePane.Visible)
                    {
                        ActivePane.Visible = true;
                    }
                    else
                    {
                        ActivePane.Visible = false;
                    }
                    //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #203"); }

            }

        }

        private void btnPinCite_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Add Pincite");

                    ExhibitHelper helper = new ExhibitHelper();
                    helper.AddPincite(_app.Selection);
                    Globals.ThisAddIn.ReturnFocus();

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #204"); }
            }
        }

        private void btnRemovePinCite_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Remove Pincite");

                    ExhibitHelper helper = new ExhibitHelper();
                    helper.RemovePinCite(_app.Selection);

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #205"); }
            }
        }

        private void IndexOfExhibits_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Exhibit Index");

                    new ExhibitHelper().InsertExhibitIndex(_app);
                    Globals.ThisAddIn.ReturnFocus();

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("Please select an editable range."); }
            }
        }

        private void CustomerSupport_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("mailto://support@prelimine.com");
        }


        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    ctrlAnsResView view = new ctrlAnsResView();
                    EventArgs eventArgs = new EventArgs();
                    view.button1_Click(sender, eventArgs);
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #206"); }
            }
        }

        private void markRedact_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try 
                { 
                    /// consider RelationshipsHideTable ImageMSO
                    _app.UndoRecord.StartCustomRecord("Mark Redaction");
                    Ribbon.Redactions.MarkRedaction(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #207"); }
            }
        }

        private void unmarkRedact_Click(object sender, RibbonControlEventArgs e)
        {
            _app.UndoRecord.StartCustomRecord("Mark Redaction");
            try
            {
                Ribbon.Redactions.UnmarkRedactions(_app);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #209"); }
            _app.UndoRecord.EndCustomRecord();
        }

        private void btnClearAllRedactions_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                _app.UndoRecord.StartCustomRecord("Redactions Cleared");

                ContentControls contentControls = null;
                ContentControl contentControl = null;

                for (int k = 1; k <= 10; k++) // loops k times just to ensure it ran on all content controls
                {
                    contentControls = Globals.ThisAddIn.Application.ActiveDocument.ContentControls;
                    if (contentControls.Count > 0)
                    {
                        for (int i = 1; i <= contentControls.Count; i++)
                        {
                            contentControl = contentControls[i];
                            if (contentControl.Title == "Redaction")
                            {
                                contentControl.Range.Font.ColorIndex = WdColorIndex.wdAuto;
                                contentControl.Range.HighlightColorIndex = WdColorIndex.wdNoHighlight;
                                contentControl.Delete(false);
                            }
                            if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                        }
                    }
                }

                Ribbon.Redactions.UnmarkRedactionsFooter(_app);
                Ribbon.Redactions.UnmakrRedactionsEndNote(_app);
                Ribbon.Redactions.UnmarkRedactionImageFloatAll(_app);

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #210"); }

        }

        private void redactedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    Ribbon.Redactions.SaveRedactedPDF(_app);
                    Globals.ThisAddIn.Application.ActiveDocument.UndoClear();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #211"); }
            }

        }

        private void unredactedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    ///////// Services.RedactionTool.Redactions lead-in
                    frmPopup frm = new frmPopup();
                    frm.Text = "Create Unredacted PDF";
                    frm.ControlBox = false;
                    ctrlConfidentialMarker confidentialMarker = new ctrlConfidentialMarker();

                    Redactions redactions = new Redactions(_app);

                    frm.Controls.Add(confidentialMarker);
                    confidentialMarker.Visible = true;

                    frm.ShowDialog();

                    if (Redactions.cancel)
                    {

                    }
                    else
                    {
                        redactions.SaveUnRedactedPDF();

                        Globals.ThisAddIn.Application.ActiveDocument.UndoClear();
                    }
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #212"); }
            }
        }

        private void btnBlockTranscript_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Inserted Block Quote");

                    frmTranscript form = new frmTranscript(InLineOrBlock.Block);
                    form.Text = "Insert Block Quote";
                    form.label1.Text = "Insert Transcript Text to Paste as Block Quote";
                    form.Show();

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #213"); }

            }
        }

        private void btnInLineTranscript_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Inserted In-Line Quote");

                    frmTranscript form = new frmTranscript(InLineOrBlock.InLine);
                    form.Text = "Insert In-Text Quote";
                    form.label1.Text = "Insert Transcript Text to Paste As In-Text Quote";
                    form.Show();

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #214"); }

            }
        }

        private void btnShowHide_Click_1(object sender, RibbonControlEventArgs e)
        {
            if (_app.ActiveWindow.View.ShowAll)
            {
                btnShowHide.Checked = false;
                _app.ActiveWindow.View.ShowAll = false;
            }
            else
            {
                btnShowHide.Checked = true;
                _app.ActiveWindow.View.ShowAll = true;
            }
        }

        private void btnLatin_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Italicized Latin");

                    LatinExpressions.Italicize(_app);

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #215"); }

            }
        }

        private void btnInsertNBS_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Inserted Non-Breaking Spaces");
                    InsertNBS.Insert(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #216"); }

            }
        }

        private void btnSmrtQuotes_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Set Quotes to Smart");
                    SmartQuotesAndApostrophes.SetSmartQuotes(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #217"); }

            }
        }

        private void btnDoubleSpace_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Set Spaces to Double");
                    SpaceBetweenSentences.AddSpace(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #218"); }

            }
        }

        private void btnSingleSpace_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Set Spaces to Single");
                    SpaceBetweenSentences.RemoveSpace(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #219"); }

            }
        }

        private void btnBlockQuotes_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Blocked long quotes");
                    BlockQuotes.FindQuotesToBlock(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #220"); }

            }
        }

        private void btnOxfordComma_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Added Oxford Commas");
                    OxfordComma.AddOxfordComma(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #221"); }

            }
        }

        private void btnRemoveOxfordComma_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Removed Oxford Commas");
                    OxfordComma.RemoveOxfordComma(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #222"); }

            }
        }

        private void ExhibitChangeControl_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void button1_Click_1(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid)
            { ShowLicenseNotValidMessage(); }
            else
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Unitalicized Latin");
                    LatinExpressions.UnItalicize(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #223"); }

            }
        }

        private void group1_DialogLauncherClick(object sender, RibbonControlEventArgs e)
        {

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            var Files = Directory.EnumerateFileSystemEntries(Root);

            string filesString = "All Files:" +Environment.NewLine;
            foreach (var file in Files)
            {
                filesString += file + Environment.NewLine;
            }


            string licPath = string.Empty;
            try
            {
                licPath = Files.Where(n => n.Contains("license.xml")).SingleOrDefault();
            }
            catch
            { licPath = "Files.Where failed"; }


            string lic = new StreamReader(licPath).ReadToEnd();


            MessageBox.Show("License is valid: "+LicenseChecker.LicenseIsValid() +Environment.NewLine + "Licensed to: " +LicenseChecker.Name() + Environment.NewLine + "Expiration: " + LicenseChecker.Expiration());

        }

        private void ReportBug_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("https://forms.gle/HkqXuHyjJhzcVjJE6");
        }

        private void togglebtnSmallCaps_Click(object sender, RibbonControlEventArgs e)
        {
            _app.UndoRecord.StartCustomRecord("Change Small Caps");

            SmallCaps sc = new SmallCaps(_app);
            sc.ChangeSmallCaps(_app.Selection, togglebtnSmallCaps);
            _app.UndoRecord.EndCustomRecord();
        }

        private void btnReplace_Click(object sender, RibbonControlEventArgs e)
        {
            Replace replace = new Tools.Simple.Replace(_app);
            replace.SendKey();
        }
    }
}

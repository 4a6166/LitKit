using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using LitKit1.Controls;
using LitKit1.Controls.ExhibitControls;
using LitKit1.Controls.RedactionControls;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using Services;  //Remember to add the reference so this using statement can be picked up
using Services.Exhibit;
using Services.RedactionTool;
using Ribbon = Ribbon_0._0._1;
using Services.RibbonButtons;
using LitKit1.Controls.AnsResControls;
using Services.Response;

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


        private void ClipboardButton_Click(object sender, RibbonControlEventArgs e)
        {
            _app.ShowClipboard();
        }

        private void ExhibitTool_Click(object sender, RibbonControlEventArgs e)
        {
            AddExhibts();

            ctrlExhibitView exhibitCtrl = new ctrlExhibitView();
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(exhibitCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            exhibitCtrl.LoadListView();

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;

        }

        private void AddExhibts()
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

            ResponseRespository repository = new ResponseRespository(_app);

            repository.AddCustomResponse("Test Add", false, true, false, false, "Test display text");

            ResponseStandardRepository repoStandard = new ResponseStandardRepository();
            _app.Selection.TypeText(repoStandard.GetTexts());

            _app.UndoRecord.EndCustomRecord();
        }

        private void btnKeepWithNext_Click(object sender, RibbonControlEventArgs e)
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

        private void button1_Click(object sender, RibbonControlEventArgs e)
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

        private void btnPinCite_Click(object sender, RibbonControlEventArgs e)
        {
            _app.UndoRecord.StartCustomRecord("Add Pincite");

            ExhibitHelper helper = new ExhibitHelper();
            helper.AddPincite(_app.Selection);
            Globals.ThisAddIn.ReturnFocus();

            _app.UndoRecord.EndCustomRecord();

        }

        private void btnRemovePinCite_Click(object sender, RibbonControlEventArgs e)
        {
            _app.UndoRecord.StartCustomRecord("Remove Pincite");

            ExhibitHelper helper = new ExhibitHelper();
            helper.RemovePinCite(_app.Selection);

            _app.UndoRecord.EndCustomRecord();
        }

        private void IndexOfExhibits_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                _app.UndoRecord.StartCustomRecord("Exhibit Index");

                new ExhibitHelper().InsertExhibitIndex(_app);
                Globals.ThisAddIn.ReturnFocus();

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("Please select an editable range.");}
        }

        private void CustomerSupport_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("mailto://support@prelimine.com");
        }


      

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            ctrlAnsResView view = new ctrlAnsResView();
            EventArgs eventArgs = new EventArgs();
            view.button1_Click(sender, eventArgs);

        }

        private void markRedact_Click(object sender, RibbonControlEventArgs e)
        {
            /// consider RelationshipsHideTable ImageMSO
            _app.UndoRecord.StartCustomRecord("Mark Redaction");
            Ribbon.Redactions.MarkRedaction(_app);
            _app.UndoRecord.EndCustomRecord();
        }

        private void unmarkRedact_Click(object sender, RibbonControlEventArgs e)
        {
            _app.UndoRecord.StartCustomRecord("Mark Redaction");
            Ribbon.Redactions.UnmarkRedactions(_app);
            _app.UndoRecord.EndCustomRecord();
        }

        private void btnClearAllRedactions_Click(object sender, RibbonControlEventArgs e)
        {
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

        }

        private void redactedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            Ribbon.Redactions.SaveRedactedPDF(_app);
            Globals.ThisAddIn.Application.ActiveDocument.UndoClear();

        }

        private void unredactedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            ///////// Services.RedactionTool.Redactions lead-in
            frmPopup frm = new frmPopup();
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

        private void btnBlockTranscript_Click(object sender, RibbonControlEventArgs e)
        {
            frmTranscript form = new frmTranscript(InLineOrBlock.Block);
            form.Text = "Insert Block Quote";
            form.label1.Text = "Insert Transcript Text to Paste as Block Quote";
            form.Show();
        }

        private void btnInLineTranscript_Click(object sender, RibbonControlEventArgs e)
        {
            frmTranscript form = new frmTranscript(InLineOrBlock.InLine);
            form.Text = "Insert In-Text Quote";
            form.label1.Text = "Insert Transcript Text to Paste As In-Text Quote";
            form.Show();
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
            LatinExpressions.Italicize(_app);
        }

        private void btnInsertNBS_Click(object sender, RibbonControlEventArgs e)
        {
            InsertNBS.Insert(_app);
        }

        private void btnSmrtQuotes_Click(object sender, RibbonControlEventArgs e)
        {
            SmartQuotesAndApostrophes.SetSmartQuotes(_app);
        }

        private void btnDoubleSpace_Click(object sender, RibbonControlEventArgs e)
        {
            SpaceBetweenSentences.AddSpace(_app);
        }

        private void btnSingleSpace_Click(object sender, RibbonControlEventArgs e)
        {
            SpaceBetweenSentences.RemoveSpace(_app);
        }

        private void btnBlockQuotes_Click(object sender, RibbonControlEventArgs e)
        {
            BlockQuotes.FindQuotesToBlock(_app);
        }

        private void btnOxfordComma_Click(object sender, RibbonControlEventArgs e)
        {
            OxfordComma.AddOxfordComma(_app);
        }

        private void btnRemoveOxfordComma_Click(object sender, RibbonControlEventArgs e)
        {
            OxfordComma.RemoveOxfordComma(_app);
        }

        private void ExhibitChangeControl_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}

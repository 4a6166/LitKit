using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using LitKit1.Controls;
using LitKit1.Controls.RedactionControls;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using Tools.RedactionTool;
using Tools.Simple;
using System.IO;
using System.Text.RegularExpressions;
using Services.Base;
using LitKit1.ControlsWPF;
using LitKit1.ControlsWPF.Citation.ViewModels;
using System.Collections.Generic;
using Tools.Citation;
using Services.License;
using System.Security;
using LitKit1.ControlsWPF.Response.ViewModels;

namespace LitKit1
{
    public partial class MainRibbon
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Microsoft.Office.Interop.Word.Application _app; //This is necessary for passing ThisAddIn.Application to the Services project
        public CustomXMLParts XMLParts => Globals.ThisAddIn.Application.ActiveDocument.CustomXMLParts;

        private ToggleToolSelected toggleToolSelected;

        private enum ToggleToolSelected
        {
            None,
            Test,
            MarkRedaction,
            UnMarkRedaction,
            AddCitation,
            AddResponse,

        }


        // Set designer properties of tab: ContorlID Type: Custom, Position: AfterOfficeId TabHome
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            /// may have to export to XML to add an image to the shrunken button groups. More here: https://stackoverflow.com/questions/45805664/how-to-set-icon-for-resized-buttom-group-in-excel-ribbon and https://docs.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-templates
            _app = Globals.ThisAddIn.Application;

            btnInsertNBS.SuperTip = NBSSuperTip();

            toggleToolSelected = ToggleToolSelected.None;

            _app.WindowSelectionChange += new ApplicationEvents4_WindowSelectionChangeEventHandler(Application_WindowSelectionChange); 
            //Event handler for selecting text after clicking a button. To use: add case to Application_WindowSelectionChange, add option to ToggleToolSelected enum, and have toggle set toggleToolSelected to the new enum option 

        }

        private bool licenseIsValid = false;

        public bool checkLicenseIsValid()
        {
            if (!licenseIsValid)
            {
                licenseIsValid = LicenseChecker.CheckValidity();
            }

            return licenseIsValid;
        }

        #region Redactions
        private void markRedact_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    /// consider RelationshipsHideTable ImageMSO
                    _app.UndoRecord.StartCustomRecord("Mark Redaction");
                    Redactions.Mark(_app.Selection);
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
                Redactions.UnMark(_app.Selection.Range);
                //Redactions.UnMarkImagesFloat(_app.Selection.Range);
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #209"); }
            _app.UndoRecord.EndCustomRecord();
        }

        private void btnClearAllRedactions_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                _app.UndoRecord.StartCustomRecord("Redactions Cleared");

                Redactions.UnMarkAll(_app.ActiveDocument, ShowWarning: true);

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #210"); }

        }

        private void redactedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    Redactions.SaveRedactedPDF(_app);
                    Globals.ThisAddIn.Application.ActiveDocument.UndoClear();
                }
                catch (ArgumentException) //For if the save file dialog is cancelled
                {

                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #211"); }
            }

        }

        private void unredactedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    ///////// Services.RedactionTool.Redactions lead-in
                    frmPopup frm = new frmPopup();
                    frm.Text = "Create Unredacted PDF";
                    frm.ControlBox = false;
                    ctrlConfidentialMarker confidentialMarker = new ctrlConfidentialMarker(false);


                    frm.Controls.Add(confidentialMarker);
                    confidentialMarker.Visible = true;

                    frm.ShowDialog();

                    if (confidentialMarker.Aborted)
                    {

                    }
                    else
                    {
                        Redactions.SaveUnredactedPDF(_app.ActiveDocument, confidentialMarker.Marker);

                        Globals.ThisAddIn.Application.ActiveDocument.UndoClear();
                    }
                }
                catch (ArgumentException)
                {

                }
                catch
                { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #212"); }
            }
        }
        private void btnHighlightedPDF_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    ///////// Services.RedactionTool.Redactions lead-in
                    frmPopup frm = new frmPopup();
                    frm.Text = "Create Unredacted PDF";
                    frm.ControlBox = false;
                    ctrlConfidentialMarker confidentialMarker = new ctrlConfidentialMarker(true);


                    frm.Controls.Add(confidentialMarker);
                    confidentialMarker.Visible = true;

                    frm.ShowDialog();

                    if (confidentialMarker.Aborted)
                    {

                    }
                    else
                    {
                        Redactions.SaveUnredactedPDF(_app.ActiveDocument, confidentialMarker.Marker, confidentialMarker.Highlight);

                        Globals.ThisAddIn.Application.ActiveDocument.UndoClear();
                    }
                }
                catch (ArgumentException)
                {

                }
                catch
                { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #212"); }
            }
        }

        private void tglMarkRedaction_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) 
            { 
                tglMarkRedaction.Checked = false; 
                checkLicenseIsValid(); 
            }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                if (_app.Selection.Text.Length > 1 && tglMarkRedaction.Checked)
                {
                    try
                    {
                        _app.UndoRecord.StartCustomRecord("Mark Redaction");
                        Redactions.Mark(_app.Selection);
                        _app.UndoRecord.EndCustomRecord();
                    }
                    catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #207"); }
                    finally { tglMarkRedaction.Checked = false;  }
                }
                else if (tglMarkRedaction.Checked)
                {
                    toggleToolSelected = ToggleToolSelected.MarkRedaction;
                    ChangeCursor_MarkRedaction(sender, (EventArgs)e);
                }
                else
                {
                    toggleToolSelected = ToggleToolSelected.None;
                }
            }
        }


        private void ChangeCursor_MarkRedaction(object sender, EventArgs e)
        {
            string c = @"C:\Users\Jake\Google Drive (jacob.field@prelimine.com)\repos\LitKit1_git\LitKit1\LitKit1\Resources\Redact Cursor.cur";
            Cursor.Current = new Cursor(c);
            //Input.Mouse.SetCursor(new Input.Cursor(c));
            
        }



        #endregion

        #region Citations

        public Dictionary<Window, CiteMainVM> citeVMDict = new Dictionary<Window, CiteMainVM>();
        private void CitationsTool_Click(object sender, RibbonControlEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.CitationPanes[_app.ActiveWindow];

                    HoldingControl holdingControl = (HoldingControl)ActivePane.Control;

                    if (holdingControl.WPFUserControl == null)
                    {
                        citeVMDict.Add(Globals.ThisAddIn.Application.ActiveWindow, new CiteMainVM());

                        ControlsWPF.Citation.CiteMain cm = new ControlsWPF.Citation.CiteMain();

                        holdingControl.AddWPF(cm);
                    }

                    if (!ActivePane.Visible)
                    {
                        ActivePane.Visible = true;
                    }
                    else
                    {
                        ActivePane.Visible = false;
                    }

                }
                catch 
                {
                    Log.Error("Error loading/showing Active Citation Pane");
                    ErrorHandling.ShowErrorMessage();
                }

                Cursor.Current = Cursors.Default;
            }

            stopwatch.Stop();
            //MessageBox.Show("Seconds: " + stopwatch.Elapsed.TotalSeconds);

        }

        private void AddTestCitations(object sender, RibbonControlEventArgs e)
        {
            try
            {
                citeVMDict[_app.ActiveWindow].Repository.AddTestCitations();
            }
            catch { MessageBox.Show("Load the Citation Tool First");}


        }

        private void btnPinCite_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Add Pincite");

                    var _docLayer = new CiteDocLayer(_app);
                    _docLayer.AddPincite(_docLayer.GrabCiteContentControl(_app.Selection));

                    Globals.ThisAddIn.ReturnFocus();

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #204"); }
            }
        }

        private void btnRemovePinCite_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Remove Pincite");

                    var _docLayer = new CiteDocLayer(_app);
                    _docLayer.RemovePincite(_docLayer.GrabCiteContentControl(_app.Selection));

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { Log.Error("Could not remove Pincite. CC count:"+_app.Selection.ContentControls.Count + " Parent Tag:" + _app.Selection.ParentContentControl?.Tag); }
            }
        }

        private void IndexOfExhibits_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                _app.UndoRecord.StartCustomRecord("Insert Exhibit Index");

                var doclayer = new CiteDocLayer(_app);
                doclayer.InsertExhibitIndex();
                Globals.ThisAddIn.ReturnFocus();

                _app.UndoRecord.EndCustomRecord();

            }
        }

        private void btnRemoveCiteLocks_Click(object sender, RibbonControlEventArgs e)
        {
            _app.UndoRecord.StartCustomRecord();

            try
            {
                var _docLayer = new CiteDocLayer(_app);
                if (_app.Selection.Range.Characters.Count > 2)
                {
                    _app.UndoRecord.StartCustomRecord("Remove Citations");
                    _docLayer.RemoveCitesFromDoc(_app.Selection);
                    _app.UndoRecord.EndCustomRecord();
                    Log.Info("Cites removed from selection.");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to remove the references to all citations in the document? The text will remain but will no longer update when adjustments to the Citation Tool are made."+Environment.NewLine + Environment.NewLine+ "Note: If you want to remove references to citations from a certain selection, highlight that selection and click Remove Locks again.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        _app.UndoRecord.StartCustomRecord("Remove Citations");
                        _docLayer.RemoveCitesFromDoc();
                        _app.UndoRecord.EndCustomRecord();
                        Log.Info("Cites removed from Document.");
                    }
                }
            }
            catch { Log.Error("Error removing citations"); }

            _app.UndoRecord.EndCustomRecord();
        }


        #endregion

        #region Responses
        public Dictionary<Window, ResponseMainVM> responseVMDict = new Dictionary<Window, ResponseMainVM>();

        private void ResponseTool_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ResponsePanes[_app.ActiveWindow];

                    HoldingControl holdingControl = (HoldingControl)ActivePane.Control;

                    if (holdingControl.WPFUserControl == null)
                    {

                        responseVMDict.Add(Globals.ThisAddIn.Application.ActiveWindow, new ResponseMainVM());

                        ControlsWPF.Response.ResponseMain rm = new ControlsWPF.Response.ResponseMain();

                        holdingControl.AddWPF(rm);

                    }

                    if (!ActivePane.Visible)
                    {
                        ActivePane.Visible = true;
                    }
                    else
                    {
                        ActivePane.Visible = false;
                    }

                }
                catch
                {
                    Log.Error("An Error Occurred. Please contact Prelimine with this error code: #203");
                    ErrorHandling.ShowErrorMessage();
                }

                Cursor.Current = Cursors.Default;
            }
        }



        #endregion

        #region Formatting

        private void UnItalicizeLatin_Click_1(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Unitalicized Latin");
                    LatinExpressions latin = new LatinExpressions();
                    latin.Italicize(_app, 0);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #223"); }

            }
        }

        private void btnLatin_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Italicized Latin");
                    LatinExpressions latin = new LatinExpressions();
                    latin.Italicize(_app, -1);

                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #215"); }

            }
        }

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

        private void btnInsertNBS_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Set Spaces to Double");
                    SpaceBetweenSentences space = new SpaceBetweenSentences();
                    space.AddSpace(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #218"); }

            }
        }

        private void btnSingleSpace_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    _app.UndoRecord.StartCustomRecord("Set Spaces to Single");
                    SpaceBetweenSentences space = new SpaceBetweenSentences();
                    space.RemoveSpace(_app);
                    _app.UndoRecord.EndCustomRecord();
                }
                catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #219"); }

            }
        }

        private void btnBlockQuotes_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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

        private void btnBlockTranscript_Click(object sender, RibbonControlEventArgs e)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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

        #endregion

        #region Shortcuts

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
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                _app.ShowClipboard();
            }
        }

        private void btnKeepWithNext_Click(object sender, RibbonControlEventArgs e)
        {

            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
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

        #endregion

        #region Toggle Events

        private void Application_WindowSelectionChange(Selection Sel)
        {
            switch (toggleToolSelected)
            {
                case (ToggleToolSelected.None):
                    break;

                case (ToggleToolSelected.Test):
                    Sel.Range.HighlightColorIndex = WdColorIndex.wdBlue;
                    break;

                case (ToggleToolSelected.MarkRedaction):
                    _app.UndoRecord.StartCustomRecord("Mark Redaction");
                    Redactions.Mark(_app.Selection);
                    _app.UndoRecord.EndCustomRecord();
                    tglMarkRedaction.Checked = false;
                    toggleToolSelected = ToggleToolSelected.None;
                    break;

                case (ToggleToolSelected.UnMarkRedaction):
                    break;

                case (ToggleToolSelected.AddCitation):
                    break;

                case (ToggleToolSelected.AddResponse):
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Support
        private void CustomerSupport_Click(object sender, RibbonControlEventArgs e)
        {
            string link = "mailto://support@prelimine.com";
            //Process.Start("link");

            _app.ActiveDocument.FollowHyperlink(Address: link);
        }

        private void HowTo_Click(object sender, RibbonControlEventArgs e)
        {
            string link = @"https://www.prelimine.com/user-guide";
            _app.ActiveDocument.FollowHyperlink(Address: link);
        }

        private void Support_DialogLauncherClick(object sender, RibbonControlEventArgs e)
        {
            
            MessageBox.Show(LicenseChecker.ReadLicense(), "Prelimine LitKit User License", MessageBoxButtons.OK);

        }

        #endregion

        private void Test_Button_Click(object sender, RibbonControlEventArgs e)
        {

            _app.UndoRecord.StartCustomRecord("Test Action");
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();


            //stopwatch.Stop();
            //MessageBox.Show("Time: " + stopwatch.Elapsed);
            _app.UndoRecord.EndCustomRecord();

        }

    }


}

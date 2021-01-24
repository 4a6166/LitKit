﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
//using Microsoft.Office.Tools.Ribbon;
using System.Diagnostics;
using System.Windows.Forms;
using LitKit1.Controls;
using LitKit1.Controls.RedactionControls;
using LitKit1.ControlsWPF;
using Tools.RedactionTool;
using Tools.Simple;
using Tools.Citation;
using Tools.Response;
using Services.Base;
using Services.License;
using LitKit1.ControlsWPF.Citation.ViewModels;
using LitKit1.ControlsWPF.Response.ViewModels;
using LitKit1.Properties;
using System.Drawing;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


namespace LitKit1
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        #region properties
        public Office.IRibbonUI ribbon;
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Microsoft.Office.Interop.Word.Application _app;

        public ToggleToolSelected toggleToolSelected;
        public enum ToggleToolSelected
        {
            None,
            Test,
            MarkRedaction,
            UnMarkRedaction,
            AddCitation,
            AddResponse,

        }

        public bool licenseIsValid = false;

        public Dictionary<Window, CiteMainVM> citeVMDict = new Dictionary<Window, CiteMainVM>();
        public Dictionary<Window, ResponseMainVM> responseVMDict = new Dictionary<Window, ResponseMainVM>();



        #endregion

        public Ribbon()
        {

        }


        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("LitKit1.Ribbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;

            log4net.Config.XmlConfigurator.Configure();

            _app = Globals.ThisAddIn.Application;

            //btnInsertNBS.SuperTip = NBSSuperTip();

            toggleToolSelected = ToggleToolSelected.None;

            _app.WindowSelectionChange += new ApplicationEvents4_WindowSelectionChangeEventHandler(Application_WindowSelectionChange); //Event handler for selecting text after clicking a button. To use: add case to Application_WindowSelectionChange, add option to ToggleToolSelected enum, and have toggle set toggleToolSelected to the new enum option 

        }


        #region Custom Ribbon Actions
        public bool checkLicenseIsValid()
        {
            if (!licenseIsValid)
            {
                licenseIsValid = LicenseChecker.CheckValidity();
            }

            return licenseIsValid;
        }

        public void Application_WindowSelectionChange(Selection Sel)
        {
            ribbon.Invalidate();

            switch (toggleToolSelected)
            {
                case (ToggleToolSelected.None):
                    break;

                case (ToggleToolSelected.MarkRedaction):
                    _app.UndoRecord.StartCustomRecord("Mark Redaction");
                    Redactions.Mark(_app.Selection);
                    _app.UndoRecord.EndCustomRecord();

                    //tglMarkRedaction.Checked = false;
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

        #region Redactions
        public Bitmap grpRedactions_Image(Office.IRibbonControl control)
        {
            return Resources.MarkSelection_32px;
        }

        #region Mark
        public Bitmap MarkRedaction_Image(Office.IRibbonControl control)
        {
            return Resources.MarkSelection_32px;
        }

        public bool tglMarkRedaction_Pressed(Office.IRibbonControl control)
        {
            return false;
            //if(toggleToolSelected == ToggleToolSelected.MarkRedaction)
            //{ return true; } 
        }

        public void tglMarkRedaction_Click(Office.IRibbonControl control, bool pressed)
        {
            if (!licenseIsValid)
            {
                pressed = false;
                checkLicenseIsValid();
            }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                if (_app.Selection.Text.Length > 1 && pressed)
                {
                    _app.UndoRecord.StartCustomRecord("Mark Redaction");

                    try
                    {
                        Redactions.Mark(_app.Selection);
                    }
                    catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #207"); }
                    finally { pressed = false; }

                    _app.UndoRecord.EndCustomRecord();

                }
                else if (pressed)
                {
                    toggleToolSelected = ToggleToolSelected.MarkRedaction;
                    ChangeCursor_MarkRedaction();
                }
                else
                {
                    toggleToolSelected = ToggleToolSelected.None;
                }
            }
        }

        public void ChangeCursor_MarkRedaction()
        {
            Cursor.Current = Cursors.Cross;
            //Input.Mouse.SetCursor(new Input.Cursor(c));

        }

        #endregion
        #region UnMark
        public Bitmap UnMarkRedaction_Image(Office.IRibbonControl control)
        {
            return Resources.UnmarkSelection_16px;
        }
        public bool UnMarkRedaction_Enabled(Office.IRibbonControl control)
        {
            return true;
            //var sel = _app.Selection;

            //if (sel.ContentControls.Count < 1 && sel.ParentContentControl != null && sel.ParentContentControl.Title != null && sel.ParentContentControl.Title.StartsWith("Redaction"))
            //{
            //    return true;
            //}
            //else if (sel.ContentControls[1].Title != null && sel.ContentControls[1].Title.StartsWith("Redaction"))
            //{
            //    return true;
            //}
            //else return false;
        }

        public bool unmarkRedact_Click(Office.IRibbonControl control)
        {
            bool result;
            _app.UndoRecord.StartCustomRecord("Mark Redaction");
            try
            {
                Redactions.UnMark(_app.Selection.Range);
                result = true;
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #209"); result = false; }
            _app.UndoRecord.EndCustomRecord();
            return result;
        }
        #endregion
        #region Clear
        public Bitmap btnClearAllRedactions_Image(Office.IRibbonControl control)
        {
            return Resources.ClearAllRedactions_32px;
        }
        public void btnClearAllRedactions_Click(Office.IRibbonControl control)
        {
            try
            {
                _app.UndoRecord.StartCustomRecord("Redactions Cleared");

                Redactions.UnMarkAll(_app.ActiveDocument, ShowWarning: true);

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #210"); }

        }

        #endregion

        public Bitmap PrintPDF_Image(Office.IRibbonControl control)
        {
            return Resources.CreatePDF_32px;
        }
        #region RedactedPDF
        public Bitmap RedactedPDF_Image(Office.IRibbonControl control)
        {
            return Resources.CreateRedactedPDF_32px;
        }
        public void redactedPDF_Click(Office.IRibbonControl control)
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


        #endregion
        #region UnRedactedPDF
        public Bitmap UnredactedPDF_Image(Office.IRibbonControl control)
        {
            return Resources.CreateUnredactedPDF_32px;
        }
        public void unredactedPDF_Click(Office.IRibbonControl control)
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
        #endregion
        #region HighlightedPDF
        public Bitmap HighlightedPDF_Image(Office.IRibbonControl control)
        {
            return Resources.CreateHighlightedPDF_32px_PrelimEdit3;
        }
        public void btnHighlightedPDF_Click(Office.IRibbonControl control)
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
        #endregion

        #endregion

        #region Citations
        public Bitmap grpCitations_Image(Office.IRibbonControl control)
        {
            return Resources.ExhibitTool_32px;
        }

        public void AddTestCitations(Office.IRibbonControl control)
        {
            try
            {
                citeVMDict[_app.ActiveWindow].Repository.AddTestCitations();
            }
            catch { MessageBox.Show("Load the Citation Tool First"); }
        }

        #region Tool Open
        public Bitmap CitationsTool_Image(Office.IRibbonControl control)
        {
            return Resources.ExhibitTool_32px;
        }
        public void CitationsTool_Click(Office.IRibbonControl control)
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

        #endregion

        #region Pincite Menu

        public Bitmap PinciteMenu_Image(Office.IRibbonControl control)
        {
            return Resources.AddPincite_32px;
        }

        public bool PinciteMenu_Enabled(Office.IRibbonControl control)
        {
            return true;
            //var sel = _app.Selection;

            //if (sel.ContentControls.Count < 1 && sel.ParentContentControl != null && sel.ParentContentControl.Title != null && sel.ParentContentControl.Title.StartsWith("CITE"))
            //{
            //    return true;
            //}
            //else if (sel.ContentControls[1].Title != null && sel.ContentControls[1].Title.StartsWith("CITE"))
            //{
            //    return true;
            //}
            //else return false;
        }

        #endregion
        #region Add Pincite
        public Bitmap btnAddPincite_Image(Office.IRibbonControl control)
        {
            return Resources.AddPincite_32px_PrelimEdit;
        }
        public bool btnPinCite_Click(Office.IRibbonControl control)
        {
            bool result = false;

            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                _app.UndoRecord.StartCustomRecord("Add Pincite");

                try
                {

                    var _docLayer = new CiteDocLayer(_app);
                    _docLayer.AddPincite(_docLayer.GrabCiteContentControl(_app.Selection));

                    Globals.ThisAddIn.ReturnFocus();

                    result = true;

                }
                catch
                {
                    MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #204");
                    result = false;
                }

                _app.UndoRecord.EndCustomRecord();
            }
            return result;

        }

        #endregion
        #region Remove Pincite
        public Bitmap btnRemovePinCite_Image(Office.IRibbonControl control)
        {
            return Resources.RemovePincite_32px;
        }

        public bool btnRemovePinCite_Enabled(Office.IRibbonControl control)
        {
            var sel = _app.Selection;

            if (sel.ContentControls.Count < 1 && sel.ParentContentControl != null && sel.ParentContentControl.Title != null && sel.ParentContentControl.Title.StartsWith("PIN"))
            {
                return true;
            }
            else if (sel.ContentControls[1].Title != null && sel.ContentControls[1].Title.StartsWith("PIN"))
            {
                return true;
            }
            else return false;
        }
        public bool btnRemovePinCite_Click(Office.IRibbonControl control)
        {
            var result = false;
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                _app.UndoRecord.StartCustomRecord("Remove Pincite");

                try
                {
                    var _docLayer = new CiteDocLayer(_app);
                    _docLayer.RemovePincite(_docLayer.GrabCiteContentControl(_app.Selection));
                    result = true;
                }
                catch { Log.Error("Could not remove Pincite. CC count:" + _app.Selection.ContentControls.Count + " Parent Tag:" + _app.Selection.ParentContentControl?.Tag); }

                _app.UndoRecord.EndCustomRecord();

            }
            return result;
        }
        #endregion
        #region Index of Exhibits

        public Bitmap IndexOfExhibits_Image(Office.IRibbonControl control)
        {
            return Resources.IndexOfExhibits_16px;
        }
        public void IndexOfExhibits_Click(Office.IRibbonControl control)
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
        #endregion

        #region remove cite locks
        public Bitmap btnRemoveCiteLocks_Image(Office.IRibbonControl control)
        {
            return null;
        }
        public void btnRemoveCiteLocks_Click(Office.IRibbonControl control)
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
                    DialogResult result = MessageBox.Show("Are you sure you want to remove the references to all citations in the document? The text will remain but will no longer update when adjustments to the Citation Tool are made." + Environment.NewLine + Environment.NewLine + "Note: If you want to remove references to citations from a certain selection, highlight that selection and click Remove Locks again.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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

        #endregion

        #region Responses
        public Bitmap grpResponses_Image(Office.IRibbonControl control)
        {
            return Resources.ResponseTool_32px;
        }

        #region Tool Open
        public void ResponseTool_Click(Office.IRibbonControl control)
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
        #endregion

        #region Formatting
        public Bitmap grpFormatting_Image(Office.IRibbonControl control)
        {
            return Resources.PasteTranscript_32px;
        }
        public Bitmap Transcript_Image(Office.IRibbonControl control)
        {
            return Resources.PasteTranscript_32px;

        }
        #region Block Transcript
        public void btnBlockTranscript_Click(Office.IRibbonControl control)
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
        #endregion

        #region Inline Transcript
        public void btnInLineTranscript_Click(Office.IRibbonControl control)
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

        public Bitmap LatinMenu_Image(Office.IRibbonControl control)
        {
            return Resources.LatinWords_16px;
        }
        #region Italicize Latin
        public void btnLatin_Click(Office.IRibbonControl control)
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
        #endregion

        #region Unitalicize Latin
        public void UnItalicizeLatin_Click_1(Office.IRibbonControl control)
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

        #endregion

        public Bitmap SentenceSpacing_Image(Office.IRibbonControl control)
        {
            return Resources.SenteceSpacing_16px;
        }

        #region Single space
        public void btnSingleSpace_Click(Office.IRibbonControl control)
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
        #endregion

        #region Double Spacee
        public void btnDoubleSpace_Click(Office.IRibbonControl control)
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
        #endregion

        #region Smart Quotes
        public Bitmap btnSmrtQuotes_Image(Office.IRibbonControl control)
        {
            return Resources.SmartQuotes_16px;
        }
        public void btnSmrtQuotes_Click(Office.IRibbonControl control)
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
        #endregion

        #region InsertNBS
        public Bitmap InsertNBS_Image(Office.IRibbonControl control)
        {
            return Resources.InsertNBS_16px;
        }

        public string InsertNBS_SuperTip(Office.IRibbonControl control)
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

        public void btnInsertNBS_Click(Office.IRibbonControl control)
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
        #endregion


        #region Block Quotes
        public Bitmap BlockQuote_Image(Office.IRibbonControl control)
        {
            return null;
        }
        public void btnBlockQuotes_Click(Office.IRibbonControl control)
        {
            //if (!licenseIsValid) { checkLicenseIsValid(); }
            //if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            //{
            //    try
            //    {
            //        _app.UndoRecord.StartCustomRecord("Blocked long quotes");
            //        BlockQuotes.FindQuotesToBlock(_app);
            //        _app.UndoRecord.EndCustomRecord();
            //    }
            //    catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #220"); }

            //}
        }
        #endregion

        #region Oxford Comma
        public Bitmap OxfordComma_Image(Office.IRibbonControl control)
        {
            return Resources.OxfordComma_16px;
        }

        public void btnOxfordComma_Click(Office.IRibbonControl control)
        {
            //if (!licenseIsValid) { checkLicenseIsValid(); }
            //if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            //{
            //    try
            //    {
            //        _app.UndoRecord.StartCustomRecord("Added Oxford Commas");
            //        OxfordComma.AddOxfordComma(_app);
            //        _app.UndoRecord.EndCustomRecord();
            //    }
            //    catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #221"); }

            //}
        }
        #endregion

        #region Remove Oxford Comma
        public void btnRemoveOxfordComma_Click(Office.IRibbonControl control)
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
        #endregion

        #region Remove Line Breaks
        public Bitmap RemoveLineBreaks_Image(Office.IRibbonControl control)
        {
            return null;
        }
        public void btnRemoveLineBreaks_Click(Office.IRibbonControl control)
        {
            _app.UndoRecord.StartCustomRecord("Remove Line Breaks");

            LineBreaks.RemoveBreaks(_app.Selection);

            _app.UndoRecord.EndCustomRecord();
        }
        #endregion

        #region Hyphen to En-Dashes
        public Bitmap HyphenToEnDashbtn_Image(Office.IRibbonControl control)
        {
            return null;
        }

        public void HyphenToEnDashbtn_Click(Office.IRibbonControl control)
        {
            _app.UndoRecord.StartCustomRecord("Replace Hyphens with En-Dashes");

            HyphenToEnDash.ReplaceWithEnDash(_app);

            _app.UndoRecord.EndCustomRecord();

        }
        #endregion

        #endregion

        #region Shortcuts
        public Bitmap grpShortcuts_Image(Office.IRibbonControl control)
        {
            return Resources.LegalSymbol_32px;
        }


        #region Insert Symbols Button Click

        public void btnPilcrow_Click(Office.IRibbonControl control)
        {
            //_app.Selection.TypeText("¶");
            _app.Selection.InsertSymbol(182);
        }

        public void insertCopyright_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0169);
        }

        public void insertNBS_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(160);
        }
        public void btnNBHyphen_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.TypeText("\u2011");
        }

        public void insertTM_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0153);
        }

        public void insertSectionMark_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(167);
        }

        public void insertNDash_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0150);
        }

        public void insertMDash_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Selection.InsertSymbol(0151);
        }

        #endregion
        #region Formatting Marks
        public Bitmap btnShowHide_Image(Office.IRibbonControl control)
        {
            return Resources.ShowHideFMarks_16px;
        }
        public void btnShowHide_Click_1(Office.IRibbonControl control, bool pressed)
        {
            if (pressed)
            {
                _app.ActiveWindow.View.ShowAll = true;
            }
            else
            {
                _app.ActiveWindow.View.ShowAll = false;
            }
        }

        public bool btnShowHide_Pressed(Office.IRibbonControl control)
        {
            if (_app.ActiveWindow.View.ShowAll)
            {
                return true;
            }
            else return false;
        }
        #endregion
        #region Keep with next
        public Bitmap btnKeepWithNext_Image(Office.IRibbonControl control)
        {
            return Resources.KeepWithNext_16px;
        }
        public void btnKeepWithNext_Click(Office.IRibbonControl control)
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
        #endregion

        #region Clipboard
        public void ClipboardButton_Click(Office.IRibbonControl control)
        {
            if (!licenseIsValid) { checkLicenseIsValid(); }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                _app.ShowClipboard();
            }
        }

        #endregion
        #region small caps

        public void togglebtnSmallCaps_Click(Office.IRibbonControl control, bool pressed)
        {
            _app.UndoRecord.StartCustomRecord("Change Small Caps");

            if (pressed)
            {
                _app.Selection.Font.SmallCaps = -1;
            }
            else _app.Selection.Font.SmallCaps = 0;
            _app.UndoRecord.EndCustomRecord();
        }

        public bool SmallCaps_Pressed(Office.IRibbonControl control)
        {
            if (_app.Selection.Font.SmallCaps == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion
        #region Find and Replace
        public void btnReplace_Click(Office.IRibbonControl control)
        {
            Replace replace = new Tools.Simple.Replace(_app);
            replace.SendKey();
        }
        #endregion
        #endregion

        #region Support
        public Bitmap grpSupport_Image(Office.IRibbonControl control)
        {
            return Resources.Support_16px;
        }

        #region Customer Support
        public Bitmap CustomerSupport_Image(Office.IRibbonControl control)
        {
            return Resources.Support_16px;
        }
        public void CustomerSupport_Click(Office.IRibbonControl control)
        {
            string link = "mailto://support@prelimine.com";
            //Process.Start("link");

            _app.ActiveDocument.FollowHyperlink(Address: link);
        }
        #endregion

        public Bitmap HowTo_Image(Office.IRibbonControl control)
        {
            return null;
        }
        public void HowTo_Click(Office.IRibbonControl control)
        {
            string link = @"https://www.prelimine.com/user-guide";
            _app.ActiveDocument.FollowHyperlink(Address: link);
        }

        public void Support_DialogLauncherClick(Office.IRibbonControl control)
        {

            MessageBox.Show(LicenseChecker.ReadLicense(), "Prelimine LitKit User License", MessageBoxButtons.OK);

        }

        #endregion

        #region ContextMenu
        public Bitmap menuAddCite_Image(Office.IRibbonControl control)
        {
            return Resources.ExhibitTool_32px;
        }
        #region Add Exhibit
        public Bitmap menubtnAddExhibt_Image(Office.IRibbonControl control)
        {
            return Resources.ExhibitTool_32px;
        }

        public bool menubtnAddExhibit_Click(Office.IRibbonControl control)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Add Other Cite
        public Bitmap menubtnAddOther_Image(Office.IRibbonControl control)
        {
            return Resources.ExhibitTool_32px;
        }

        public bool menubtnAddOther_Click(Office.IRibbonControl control)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Mark Redaction
        public Bitmap menuRedactions_Image(Office.IRibbonControl control)
        {
            return Resources.CreateRedactedPDF_32px;
        }

        public bool menuMarkRedaction_Click(Office.IRibbonControl control)
        {
            if (!licenseIsValid)
            {
                checkLicenseIsValid();
            }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                try
                {
                    if (_app.Selection.Text.Length > 1)
                    {
                        try
                        {
                            _app.UndoRecord.StartCustomRecord("Mark Redaction");
                            Redactions.Mark(_app.Selection);
                            _app.UndoRecord.EndCustomRecord();
                        }
                        catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #207"); }
                    }
                    return true;
                }
                catch { return false; }

            }

            return false;

        }

        #endregion



        #endregion
        #endregion

        #region Helpers

        public static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}

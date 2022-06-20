using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Tools.RedactionTool;
using Services.Base;
using Services.License;
using LitKit1.Controls;
using LitKit1.Controls.RedactionControls;
using Redactions.Properties;

namespace Redactions
{
    [ComVisible(true)]
    public class RedactionsRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        #region copied from LitKit1.Ribbon.cs
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
        //TODO: Implement licensing

        #endregion

        public RedactionsRibbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("Redactions.RedactionsRibbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;

            //copied from LitKit1.Ribbon.cs
            _app = Globals.ThisAddIn.Application;

            toggleToolSelected = ToggleToolSelected.None;

            _app.WindowSelectionChange += new ApplicationEvents4_WindowSelectionChangeEventHandler(Application_WindowSelectionChange);
            //Event handler for selecting text after clicking a button. To use: add case to Application_WindowSelectionChange, add option to ToggleToolSelected enum, and have toggle set toggleToolSelected to the new enum option 

        }

        #region Custom Ribbon Actions
        // copied rom LitKit1.Ribbon.cs

        public bool checkLicenseIsValid()
        {
            if (!licenseIsValid)
            {
                licenseIsValid = (bool)LicenseChecker.CheckValidity();
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
                    Tools.RedactionTool.Redactions.Mark(_app.Selection);
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
                        Tools.RedactionTool.Redactions.Mark(_app.Selection);
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
            try
            {
                var sel = _app.Selection;

                if (sel.ContentControls.Count < 1 && sel.ParentContentControl != null && sel.ParentContentControl.Title != null && sel.ParentContentControl.Title.StartsWith("Redaction"))
                {
                    return true;
                }
                else if (sel.ContentControls.Count > 0 && sel.ContentControls[1].Tag != null && sel.ContentControls[1].Tag.StartsWith("Redaction"))
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool unmarkRedact_Click(Office.IRibbonControl control)
        {
            bool result;
            _app.UndoRecord.StartCustomRecord("Mark Redaction");
            try
            {
                Tools.RedactionTool.Redactions.UnMark(_app.Selection.Range);
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

                Tools.RedactionTool.Redactions.UnMarkAll(_app.ActiveDocument, ShowWarning: true);

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
                    Tools.RedactionTool.Redactions.SaveRedactedPDF(_app);
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
                        Tools.RedactionTool.Redactions.SaveUnredactedPDF(_app.ActiveDocument, confidentialMarker.Marker);

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
                    frm.Text = "Create Highlighted PDF";
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
                        Tools.RedactionTool.Redactions.SaveUnredactedPDF(_app.ActiveDocument, confidentialMarker.Marker, confidentialMarker.Highlight);

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

        #region Context Menu
        public Bitmap menuRedactions_Image(Office.IRibbonControl control)
        {
            return Resources.CreateRedactedPDF_32px;
        }

        public bool menuMarkRedaction_Click(Office.IRibbonControl control)
        {
            bool result = false;
            if (!licenseIsValid)
            {
                checkLicenseIsValid();
            }
            if (licenseIsValid) //Second check so if license is valid, the user won't have to hit the button a second time
            {
                _app.UndoRecord.StartCustomRecord("Mark Redaction");

                try
                {
                    if (_app.Selection.Text.Length > 1)
                    {
                        Tools.RedactionTool.Redactions.Mark(_app.Selection);
                        result = true;
                    }
                    else
                    {
                        _app.Selection.MoveStartUntil(Cset: " \r\t", WdConstants.wdBackward);
                        _app.Selection.MoveEndUntil(Cset: " \r\t", WdConstants.wdForward);
                        Tools.RedactionTool.Redactions.Mark(_app.Selection);
                        result = true;
                    }
                }
                catch { result = false; MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #207"); }


            }
            _app.UndoRecord.EndCustomRecord();

            return result;

        }

        #endregion

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
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

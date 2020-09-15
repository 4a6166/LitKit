using System;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using System.IO;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Services.Base;

namespace Tools.RedactionTool
{
    public class Redactions: BaseService
    {
        public event RedactionCalledDelegate RedactionCalled;

        public Redactions(Application _app)
        {
            this._app = _app;
            this.RedactionCalled += RedactionCalledMethods.OnRedactionAdded;
            if (RedactionCalled != null)
            {
                RedactionCalled(this, new EventArgs());
            }
        }

        private readonly Application _app;

        private readonly List<IRedaction> redactions = new List<IRedaction>();

       
       public int Count => redactions.Count;

               


        private Word.Document CloneDocument(Word.Document inputDocument)
        {
            object missing = Type.Missing;
            object normalTemplate = inputDocument.Application.NormalTemplate;
            object tempFile = Path.GetTempFileName();

            using (var sw = new StreamWriter((string)tempFile))
                sw.Write(inputDocument.WordOpenXML);

            var fileToRedact = inputDocument.Application.Documents.Add(ref tempFile, ref missing, ref missing, ref missing);

            fileToRedact.set_AttachedTemplate(ref normalTemplate);
            fileToRedact.Activate();

            fileToRedact.TrackRevisions = false;

            //fileToRedact.Fields.Unlink();

            return fileToRedact;
        }

        public void SaveRedactedPDF(string Path, bool FileAvailable)
        {
            if (Path != null && FileAvailable)
            {
                var doc = CloneDocument(_app.ActiveDocument);
                RedactRedactions();

                _app.ActiveDocument.ExportAsFixedFormat(Path, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

                doc.Close(WdSaveOptions.wdDoNotSaveChanges);
            }
        }

        public static string ConfidentialityLabel = null;
        public static bool cancel = false;

        public void SaveUnRedactedPDF()
        {
            SaveFile saveFile = new SaveFile();

            if (saveFile.Path != null && saveFile.FileAvailable)
            {
                var doc = CloneDocument(_app.ActiveDocument);
                {
                    ContentControls contentControls = null;
                    Word.ContentControl contentControl = null;
                    

                    for (int k = 1; k <= 10; k++) // loops k times just to ensure it ran on all content controls
                    {
                        contentControls = doc.ContentControls;
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

                    Ribbon_0._0._1.Redactions.UnmarkRedactionsFooter(_app);
                    Ribbon_0._0._1.Redactions.UnmakrRedactionsEndNote(_app);
                    Ribbon_0._0._1.Redactions.UnmarkRedactionImageFloatAll(_app);
                }
                saveFile.FileMarking = ConfidentialityLabel;

                // makes the file marking the same font as the document or Times New Roman
                var headerfont = "times new roman";
                if (_app.ActiveDocument.Sections.First.Range.Font.Name != null)
                {
                    headerfont = _app.ActiveDocument.Sections.First.Range.Font.Name;
                }

                // marks the header with "confidential," updated to add a floating text box to the header rather than replace the header text
                foreach (Section section in _app.ActiveDocument.Sections)
                {
                    var header = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Shapes.AddTextbox(
                        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                        _app.ActiveDocument.PageSetup.PageWidth - 525,
                        10,
                        500,
                        20);
                    header.TextFrame.TextRange.Text = saveFile.FileMarking.ToUpper();

                    header.TextFrame.TextRange.Font.Name = headerfont;

                    header.TextFrame.TextRange.Font.Size = 12;
                    header.TextFrame.TextRange.Font.Bold = -1;

                    header.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                    header.TextFrame.TextRange.HighlightColorIndex = WdColorIndex.wdWhite;
                    header.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                }

                _app.ActiveDocument.ExportAsFixedFormat(saveFile.Path, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

                doc.Close(WdSaveOptions.wdDoNotSaveChanges);
            }
        }

        #region Get Redactions
        private void GetRedactionsText()
        {
            foreach (Word.ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag.StartsWith("Redaction"))
                {
                    RedactionText redactionText = new RedactionText(_app)
                    {
                        ContentControl = cc,
                        State = RedactionState.Marked,
                        Type = RedactionType.Text
                    };
                    redactions.Add(redactionText);
                }
            }
        }

        private void GetRedactions()
        {
            GetRedactionsText();
        }
        #endregion

        #region UnMark
        public void UnMarkRedactionsText() //// Update to fit with rest of the class
        {
            Word.Selection selection = null;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            try
            {
                selection = _app.Selection;
                contentControls = selection.ContentControls;

                // selects parent CC and removes marks for redaction
                if (contentControls.Count < 1 && _app.Selection.Range.ParentContentControl != null)
                {
                    contentControl = _app.Selection.Range.ParentContentControl;

                    if (contentControl.Title == "Redaction")
                    {
                        contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                        contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
                        contentControl.Delete(false);
                    }
                    if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                }
                else
                {
                    // removes marks for all redactions within a selection
                    for (int i = 1; i <= contentControls.Count;)
                    {
                        contentControl = contentControls[i];
                        if (contentControl.Title == "Redaction")
                        {
                            contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                            contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
                            contentControl.Delete(false);
                        }
                        if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                    }

                }
            }

            // releases all selected content controls
            finally
            {
                if (contentControls != null) Marshal.ReleaseComObject(contentControls);
                if (selection != null) Marshal.ReleaseComObject(selection);
            }
        }
        public void UnmarkRedactionImageFloat()  //// Update to fit with rest of the class
        {
            for (int shape = 1; shape <= _app.Selection.ShapeRange.Count; shape++)
            {
                var redaction = _app.Selection.ShapeRange[shape];
                if (redaction.Title.StartsWith("R-pic"))
                {
                    redaction.Title = redaction.ID.ToString();
                    redaction.AlternativeText = redaction.Title;

                    redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureAutomatic;
                    redaction.PictureFormat.Brightness = 0.5f;
                }
            }
        }   

        public void UnmarkRedactionImageFloatAll()  //// Update to fit with rest of the class
        {
            var ShapesFloat = _app.ActiveDocument.Shapes;
            for (int shape = 1; shape <= ShapesFloat.Count; shape++)
            {
                var redaction = ShapesFloat[shape];
                if (redaction.Title.StartsWith("R-pic") && redaction.HasChart == MsoTriState.msoFalse)
                {
                    redaction.PictureFormat.Brightness = 0.5f;
                    redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureAutomatic;
                }

            }
        } 

        public void UnmarkRedactionsFooter()  //// Update to fit with rest of the class
        {
            Word.Document doc = null;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            try
            {
                doc = _app.ActiveDocument;

                for (int footNote = 1; footNote <= doc.Footnotes.Count; footNote++)   ///////////////////////////////Footnote content controls
                {
                    contentControls = doc.Footnotes[footNote].Range.ContentControls;
                    for (int i = 1; i <= contentControls.Count; i++)
                    {
                        contentControl = contentControls[i];

                        if (contentControl.Title == "Redaction")
                        {
                            contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                            contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
                            contentControl.Delete(false);
                        }

                        if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                    }
                }
            }
            finally
            {

                if (contentControls != null) Marshal.ReleaseComObject(contentControls);
                if (doc != null) Marshal.ReleaseComObject(doc);
            }

        }

        public void UnmarkRedactionsEndNote()  //// Update to fit with rest of the class
        {
            Word.Document doc = null;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            try
            {
                doc = _app.ActiveDocument;

                for (int endNote = 1; endNote <= doc.Endnotes.Count; endNote++)   ///////////////////////////////Endnote content controls
                {
                    contentControls = doc.Endnotes[endNote].Range.ContentControls;
                    for (int i = 1; i <= contentControls.Count; i++)
                    {
                        contentControl = contentControls[i];

                        if (contentControl.Title == "Redaction")
                        {
                            contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                            contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
                            contentControl.Delete(false);
                        }

                        if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                    }
                }
            }
            finally
            {

                if (contentControls != null) Marshal.ReleaseComObject(contentControls);
                if (doc != null) Marshal.ReleaseComObject(doc);
            }

        }
        #endregion
        public void UnMarkRedactions()
        {
            UnMarkRedactionsText();
        }

        #region Mark

        private void RedactRedactionsText()
        {
            GetRedactionsText();
            var redactions_text =
                from redacts in redactions
                where redacts.Type == RedactionType.Text
                select redacts
                ;

            foreach (var redact in redactions_text)
            {
                redact.ContentControl.Range.Font.Fill.Transparency = 1;
                redact.State = RedactionState.Applied;
                redact.ContentControl.Tag = redact.ContentControl.Tag + redact.State;
            }
        }

        private void MarkRedactionsText()
        {
            var redaction = _app.Selection.ContentControls.Add(Word.WdContentControlType.wdContentControlText);

            redaction.Title = "Redaction";
            redaction.Tag = "Redaction-" + redaction.ID;
            redaction.Color = Word.WdColor.wdColorDarkRed;

            redaction.Range.HighlightColorIndex = Word.WdColorIndex.wdBlack;
            redaction.Range.Font.ColorIndex = Word.WdColorIndex.wdWhite;
        }
        #endregion
        public void MarkRedactions()
        {
            MarkRedactionsText();
        }

        public void MarkRedactions(RedactionType type)
        {
            

        }


        

        public void RedactRedactions()
        {
            RedactRedactionsText();
        }

        public void ClearRedactionList()
        {
            redactions.Clear();
        }
    }
}



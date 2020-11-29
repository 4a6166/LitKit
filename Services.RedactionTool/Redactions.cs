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
    public class Redactions : BaseService
    {
        public event RedactionCalledDelegate RedactionCalled;

        private readonly Application _app;

        private readonly List<IRedaction> redactions;



        public Redactions(Application _app)
        {
            this._app = _app;
            this.RedactionCalled += RedactionCalledMethods.OnRedactionAdded;
            if (RedactionCalled != null)
            {
                RedactionCalled(this, new EventArgs());
            }
            redactions = GetAll(_app);
        }




        //public static void UnmarkRedactionsFooter(Application _app)
        //{
        //    Word.Document doc = null;
        //    Word.ContentControls contentControls = null;
        //    Word.ContentControl contentControl = null;

        //    try
        //    {
        //        doc = _app.ActiveDocument;

        //        for (int footNote = 1; footNote <= doc.Footnotes.Count; footNote++)   ///////////////////////////////Footnote content controls
        //        {
        //            contentControls = doc.Footnotes[footNote].Range.ContentControls;
        //            for (int i = 1; i <= contentControls.Count; i++)
        //            {
        //                contentControl = contentControls[i];

        //                if (contentControl.Title == "Redaction")
        //                {
        //                    for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
        //                    {
        //                        contentControl.Range.ContentControls[j].LockContents = false;
        //                    }

        //                    contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
        //                    contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;

        //                    for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
        //                    {
        //                        contentControl.Range.ContentControls[j].LockContents = true;
        //                    }

        //                    contentControl.Delete(false);
        //                }

        //                if (contentControl != null) Marshal.ReleaseComObject(contentControl);
        //            }
        //        }
        //    }
        //    finally
        //    {

        //        if (contentControls != null) Marshal.ReleaseComObject(contentControls);
        //        if (doc != null) Marshal.ReleaseComObject(doc);
        //    }
        //}
        //public static void UnmakrRedactionsEndNote(Application _app)
        //{
        //    Word.Document doc = null;
        //    Word.ContentControls contentControls = null;
        //    Word.ContentControl contentControl = null;

        //    try
        //    {
        //        doc = _app.ActiveDocument;

        //        for (int endNote = 1; endNote <= doc.Endnotes.Count; endNote++)   ///////////////////////////////Endnote content controls
        //        {
        //            contentControls = doc.Endnotes[endNote].Range.ContentControls;
        //            for (int i = 1; i <= contentControls.Count; i++)
        //            {
        //                contentControl = contentControls[i];

        //                if (contentControl.Title == "Redaction")
        //                {
        //                    for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
        //                    {
        //                        contentControl.Range.ContentControls[j].LockContents = false;
        //                    }

        //                    contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
        //                    contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;

        //                    for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
        //                    {
        //                        contentControl.Range.ContentControls[j].LockContents = true;
        //                    }

        //                    contentControl.Delete(false);
        //                }

        //                if (contentControl != null) Marshal.ReleaseComObject(contentControl);
        //            }
        //        }
        //    }
        //    finally
        //    {

        //        if (contentControls != null) Marshal.ReleaseComObject(contentControls);
        //        if (doc != null) Marshal.ReleaseComObject(doc);
        //    }
        //}





        //public static bool ApplyRedactionsFooter(Word.Application _app)
        //{
        //    bool successful = true;
        //    Word.Document doc = null;
        //    Word.ContentControls contentControls = null;
        //    Word.ContentControl contentControl = null;

        //    try
        //    {
        //        doc = _app.ActiveDocument;

        //        for (int footNote = 1; footNote <= doc.Footnotes.Count; footNote++)   ///////////////////////////////Footnote content controls
        //        {
        //            contentControls = doc.Footnotes[footNote].Range.ContentControls;
        //            for (int i = 1; i <= contentControls.Count; i++)
        //            {
        //                contentControl = contentControls[i];

        //                if (contentControl.Title == "Redaction")
        //                {
        //                    for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
        //                    {
        //                        contentControl.Range.ContentControls[j].LockContents = false;
        //                    }

        //                    contentControl.Range.Font.Fill.Transparency = 1;

        //                    if (contentControl.Range.Font.Fill.Transparency != 1)
        //                    {
        //                        successful = false;
        //                    }
        //                }

        //                if (contentControl != null) Marshal.ReleaseComObject(contentControl);
        //            }
        //        }
        //    }
        //    finally
        //    {

        //        if (contentControls != null) Marshal.ReleaseComObject(contentControls);
        //        if (doc != null) Marshal.ReleaseComObject(doc);
        //    }
        //    return successful;
        //}
        //public static bool ApplyRedactionsEndNote(Word.Application _app)
        //{
        //    bool successful = true;
        //    Word.Document doc = null;
        //    Word.ContentControls contentControls = null;
        //    Word.ContentControl contentControl = null;

        //    try
        //    {
        //        doc = _app.ActiveDocument;

        //        for (int endNote = 1; endNote <= doc.Endnotes.Count; endNote++)   ///////////////////////////////Endnote content controls
        //        {
        //            contentControls = doc.Endnotes[endNote].Range.ContentControls;
        //            for (int i = 1; i <= contentControls.Count; i++)
        //            {
        //                contentControl = contentControls[i];

        //                if (contentControl.Title == "Redaction")
        //                {
        //                    for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
        //                    {
        //                        contentControl.Range.ContentControls[j].LockContents = false;
        //                    }

        //                    contentControl.Range.Font.Fill.Transparency = 1;

        //                    if (contentControl.Range.Font.Fill.Transparency != 1)
        //                    {
        //                        successful = false;
        //                    }
        //                }

        //                if (contentControl != null) Marshal.ReleaseComObject(contentControl);
        //            }
        //        }
        //    }
        //    finally
        //    {

        //        if (contentControls != null) Marshal.ReleaseComObject(contentControls);
        //        if (doc != null) Marshal.ReleaseComObject(doc);
        //    }
        //    return successful;
        //}



        //---------------------------------------------------------------------------------------------------------------------------------Refactor
        #region Public Actions
        public static void SaveRedactedPDF(Application _app)
        {
            SaveFile saveFile = new SaveFile();

            if (saveFile.Path != null && saveFile.FileAvailable)
            {
                bool successful = true;

                var doc = CloneDocument(_app.ActiveDocument);
                while (successful)
                {
                    foreach (Range story in doc.StoryRanges)
                    {
                        successful = RedactInLine(story);
                        successful = RedactImageFloat(story);
                    }

                    successful = RedactSpecialTables(_app);
                    //successful = RedactCharts();
                }

                if (successful)
                {
                    doc.ExportAsFixedFormat(saveFile.Path, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);
                }
                else { MessageBox.Show("There was an error redacting your document.", "Error Redacting Document", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                doc.Close(WdSaveOptions.wdDoNotSaveChanges);
                //MessageBox.Show(
                //    text: $"Redacted PDF exported to {saveFileDialog1.FileName}",
                //    caption: "Export Complete",
                //    buttons: MessageBoxButtons.OK
                //    );

            }
        }

        public static void SaveUnredactedPDF(Document document, string ConfidentialityLabel, WdColorIndex highlight = WdColorIndex.wdNoHighlight) //leave non static so it can be called once 
        {
            {
                SaveFile saveFile = new SaveFile();

                if (saveFile.Path != null && saveFile.FileAvailable)
                {
                    var doc = CloneDocument(document);
                    {
                        //ContentControls contentControls = null;
                        //Word.ContentControl contentControl = null;


                        //for (int k = 1; k <= 10; k++) // loops k times just to ensure it ran on all content controls
                        //{
                        //    contentControls = doc.ContentControls;
                        //    if (contentControls.Count > 0)
                        //    {
                        //        for (int i = 1; i <= contentControls.Count; i++)
                        //        {
                        //            contentControl = contentControls[i];
                        //            if (contentControl.Title == "Redaction")
                        //            {
                        //                contentControl.Range.Font.ColorIndex = WdColorIndex.wdAuto;
                        //                contentControl.Range.HighlightColorIndex = highlight;
                        //                contentControl.Delete(false);
                        //            }
                        //            if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                        //        }
                        //    }
                        //}

                        UnMarkAll(doc, highlight);
                    }
                    saveFile.FileMarking = ConfidentialityLabel;

                    // makes the file marking the same font as the document or Times New Roman
                    AddConfidentialityHeader(saveFile, doc);

                    UpdateTables(doc);

                    doc.ExportAsFixedFormat(saveFile.Path, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

                    doc.Close(WdSaveOptions.wdDoNotSaveChanges);
                }

            }

        }

        public static void UnMarkAll(Document doc, WdColorIndex highlight = WdColorIndex.wdNoHighlight)
        {
            //foreach (Range story in doc.StoryRanges)
            //{
            //    UnMark(story, highlight);
            //}

            UnMark(doc.StoryRanges[WdStoryType.wdMainTextStory]);

            UnMark(doc.StoryRanges[WdStoryType.wdFootnotesStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdEndnotesStory]);

            UnMark(doc.StoryRanges[WdStoryType.wdFirstPageFooterStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdFirstPageHeaderStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdEvenPagesFooterStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdEvenPagesHeaderStory]);

            UnMark(doc.StoryRanges[WdStoryType.wdPrimaryFooterStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdPrimaryHeaderStory]);

            UnMark(doc.StoryRanges[WdStoryType.wdTextFrameStory]);

            UnMark(doc.StoryRanges[WdStoryType.wdCommentsStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdEndnoteContinuationNoticeStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdEndnoteContinuationSeparatorStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdEndnoteSeparatorStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdFootnoteContinuationNoticeStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdFootnoteContinuationSeparatorStory]);
            UnMark(doc.StoryRanges[WdStoryType.wdFootnoteSeparatorStory]);


        }

        private static void AddConfidentialityHeader(SaveFile saveFile, Document doc, string headerfont = "times new roman")
        {
            if (doc.Sections.First.Range.Font.Name != null)
            {
                headerfont = doc.Sections.First.Range.Font.Name;
            }

            // marks the header with "confidential," updated to add a floating text box to the header rather than replace the header text
            foreach (Section section in doc.Sections)
            {
                var header = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Shapes.AddTextbox(
                    Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                    doc.PageSetup.PageWidth - 525,
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
        }

        public static void Mark(Selection selection)
        {
            try
            {
                if (selection.ShapeRange.Count > 0)
                {
                    bool success = MarkImageFloat(selection);
                }
                else if (selection.InlineShapes.Count > 0)
                {
                    bool success = MarkImageInline(selection);
                }
                else if (selection.Text != "" && selection.Text.Length > 1)
                {
                    bool success = MarkText(selection);
                }
            }
            catch
            {
                throw new Exception("Error marking item for redaction.");
            }

        }

        public static void UnMark(Range range, WdColorIndex highlight = WdColorIndex.wdNoHighlight)
        {
            bool successful = false;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            try
            {
                contentControls = range.ContentControls;

                // selects parent CC and removes marks for redaction
                if (contentControls.Count < 1 && range.ParentContentControl != null)
                {
                    contentControl = range.ParentContentControl;
                    bool success = UnMarkInLine(contentControl, highlight);
                }
                else //removes marks for all redactions within a selection
                {
                    for (int i = 1; i <= contentControls.Count; i++)
                    {
                        contentControl = contentControls[i];
                        successful = UnMarkInLine(contentControl, highlight);
                    }
                }
            }
            // releases all selected content controls
            finally
            {
                if (contentControls != null) Marshal.ReleaseComObject(contentControls);
                if (range != null) Marshal.ReleaseComObject(range);
            }


            successful = UnMarkImagesFloat(range);
            //UnmarkRedactionsChart();
        }

        public static List<IRedaction> GetAll(Application _app)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Mark Redactions
        private static bool MarkText(Selection selection)
        {
            if (selection.Text != "" && selection.Text.Length > 1)
            {

                var redaction = selection.ContentControls.Add(Word.WdContentControlType.wdContentControlRichText);

                redaction.Title = "Redaction";
                redaction.Tag = "R-" + redaction.ID;
                redaction.Color = Word.WdColor.wdColorDarkRed;

                for (var i = 1; i <= redaction.Range.ContentControls.Count; i++)
                {
                    redaction.Range.ContentControls[i].LockContents = false;
                }

                redaction.Range.HighlightColorIndex = Word.WdColorIndex.wdBlack;
                redaction.Range.Font.ColorIndex = Word.WdColorIndex.wdWhite;

                for (var i = 1; i <= redaction.Range.ContentControls.Count; i++)
                {
                    redaction.Range.ContentControls[i].LockContents = true;
                }
                return true;
            }
            else return false;
        }

        private static bool MarkImageInline(Selection selection)
        {
            if (selection.InlineShapes.Count > 0)
            {
                var redaction = selection.ContentControls.Add(Word.WdContentControlType.wdContentControlRichText);

                redaction.Title = "Redaction";
                redaction.Tag = "R-" + redaction.ID;
                redaction.Color = Word.WdColor.wdColorDarkRed;

                redaction.Range.HighlightColorIndex = Word.WdColorIndex.wdBlack;
                redaction.Range.Font.ColorIndex = Word.WdColorIndex.wdWhite;

                return true;
            }
            else return false;
        }

        private static bool MarkImageFloat(Selection selection)
        {
            if (selection.ShapeRange.Count > 0)
            {

                for (int shape = 1; shape <= selection.ShapeRange.Count; shape++)
                {
                    if (selection.ShapeRange[shape].HasChart == MsoTriState.msoFalse
                        //&& _app.Selection.ShapeRange[shape].HasDiagram == MsoTriState.msoFalse
                        //&& _app.Selection.ShapeRange[shape].HasDiagramNode == MsoTriState.msoFalse
                        //&& _app.Selection.ShapeRange[shape].HasSmartArt == MsoTriState.msoFalse
                        )
                    {
                        var redaction = selection.ShapeRange[shape];

                        redaction.Title = "R-pic" + redaction.ID;
                        redaction.AlternativeText = redaction.Title;

                        redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureGrayscale;
                        redaction.PictureFormat.Brightness = 0.23f;

                    }
                }
                return true;
            }
            else return false;
        }

        private static bool MarkChart()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Unmark Redactions

        private static bool UnMarkInLine(ContentControl contentControl, WdColorIndex highlight = WdColorIndex.wdNoHighlight)
        {
            if (contentControl.Title == "Redaction")
            {
                for (var i = 1; i <= contentControl.Range.ContentControls.Count; i++)
                {
                    contentControl.Range.ContentControls[i].LockContents = false;
                }
                contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                contentControl.Range.HighlightColorIndex = highlight;

                contentControl.Delete(false);
            }
            if (contentControl != null) Marshal.ReleaseComObject(contentControl);

            return true;
        }

        /// <summary>
        /// Unmarks all floating images in the selection
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static bool UnMarkImagesFloat(Range range)
        {
            //TODO: Need to fix. range.ShapeRange keeps throwing an error. May not be able to get shapes from range.
            
            try
            { 
                for (int shape = 1; shape <= range.ShapeRange.Count; shape++)
                {
                    var redaction = range.ShapeRange[shape];
                    if (redaction.Title.StartsWith("R-pic"))
                    {
                        redaction.Title = redaction.ID.ToString();
                        redaction.AlternativeText = redaction.Title;

                        redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureAutomatic;
                        redaction.PictureFormat.Brightness = 0.5f;
                    }
                }

                return true;

            }
            catch
            {
                return false;
            }
            
        }
        /// <summary>
        /// Unmarks all floating images in a document
        /// </summary>
        /// <param name="document">Should be the active document</param>
        /// <returns></returns>
        private static bool UnMarkImagesFloat(Document document)
        {
            var ShapesFloat = document.Shapes;
            for (int shape = 1; shape <= ShapesFloat.Count; shape++)
            {
                var redaction = ShapesFloat[shape];
                if (redaction.Title.StartsWith("R-pic") && redaction.HasChart == MsoTriState.msoFalse)
                {
                    redaction.PictureFormat.Brightness = 0.5f;
                    redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureAutomatic;
                }

            }
            return true;
        }
        private static bool UnMarkChart()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Process Redactions
        private static bool RedactInLine(Range range)
        {
            bool successful = true;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            //string controlsList = string.Empty;

            try
            {
                contentControls = range.ContentControls;
                for (int i = 1; i <= contentControls.Count; i++)
                {
                    contentControl = contentControls[i];

                    if (contentControl.Title == "Redaction")
                    {
                        for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
                        {
                            contentControl.Range.ContentControls[j].LockContents = false;
                        }

                        contentControl.Range.Font.Fill.Transparency = 1;
                        
                        successful = RemoveHyperlinks(contentControl.Range);

                        successful = RedactInlineImage(contentControl.Range);

                        if (contentControl.Range.Font.Fill.Transparency != 1)
                        {
                            successful = false;
                        }

                    }

                    if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                }
            }
            finally
            {

                if (contentControls != null) Marshal.ReleaseComObject(contentControls);
                if (range != null) Marshal.ReleaseComObject(range);
            }
            return successful;
        }

        private static bool RemoveHyperlinks(Range range)
        {
            for (int j = 1; j <= range.Hyperlinks.Count; j++)
            {
                range.Hyperlinks[j].Delete();
            }
            return true;
        }

        private static bool RedactInlineImage(Range range)
        {
            for (int j = 1; j <= range.InlineShapes.Count; j++)
            {
                range.InlineShapes[j].PictureFormat.Brightness = 0f;
            }
            return true;
        }

        private static bool RedactImageFloat(Range range)
        {
            bool successful = true;
            RemoveHyperlinks(range);

            var ShapesFloat = range.ShapeRange;
            for (int shape = 1; shape <= ShapesFloat.Count; shape++)
            {
                var redaction = ShapesFloat[shape];
                if (redaction.Title.StartsWith("R-pic") && redaction.HasChart == MsoTriState.msoFalse)
                {
                    redaction.PictureFormat.Brightness = 0f;

                    if (redaction.PictureFormat.Brightness != 0f)
                    {
                        successful = false;
                    }
                }
            }
            return successful;
        }

        private static bool RedactSpecialTables(Word.Application app)
        {
            bool successful = true;
            Word.Document doc = app.ActiveDocument;

            foreach (TableOfContents toc in doc.TablesOfContents)
            {
                toc.Update();
                for (int i = 1; i <= toc.Range.Words.Count; i++)
                {
                    var word = toc.Range.Words[i];
                    if (word.HighlightColorIndex == WdColorIndex.wdBlack)
                    {
                        toc.Range.Words[i].Font.Fill.Transparency = 1;
                        RemoveHyperlinks(toc.Range.Words[i]);
                    }
                }
            }

            foreach (TableOfAuthorities toa in doc.TablesOfAuthorities)
            {
                toa.Update();
                for (int i = 1; i <= toa.Range.Words.Count; i++)
                {
                    var word =  toa.Range.Words[i];
                    if (word.HighlightColorIndex == WdColorIndex.wdBlack)
                    {
                        toa.Range.Words[i].Font.Fill.Transparency = 1;
                        RemoveHyperlinks(toa.Range.Words[i]);
                    }
                }
            }

            foreach (TableOfFigures tof in doc.TablesOfFigures)
            {
                tof.Update();
                for (int i = 1; i <= tof.Range.Words.Count; i++)
                {
                    var word = tof.Range.Words[i];
                    if (word.HighlightColorIndex == WdColorIndex.wdBlack)
                    {
                        tof.Range.Words[i].Font.Fill.Transparency = 1;
                        RemoveHyperlinks(tof.Range.Words[i]);
                    }
                }
            }

            foreach (Index index in doc.Indexes)
            {
                index.Update();
                for (int i = 1; i <= index.Range.Words.Count; i++)
                {
                    var word = index.Range.Words[i];
                    if (word.HighlightColorIndex == WdColorIndex.wdBlack)
                    {
                        index.Range.Words[i].Font.Fill.Transparency = 1;
                        RemoveHyperlinks(index.Range.Words[i]);
                    }
                }
            }

            return successful;
        }

        private static bool RedactCharts()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Document Processes
        private static bool UpdateTables(Document document)
        {
            foreach (TableOfContents toc in document.TablesOfContents)
            {
                toc.Update();
            }

            foreach (TableOfAuthorities toa in document.TablesOfAuthorities)
            {
                toa.Update();
            }

            foreach (TableOfFigures tof in document.TablesOfFigures)
            {
                tof.Update();
            }

            foreach (Index index in document.Indexes)
            {
                index.Update();
            }

            return true;
        }

        private static Word.Document CloneDocument(Word.Document inputDocument)
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

        #endregion


    }
}



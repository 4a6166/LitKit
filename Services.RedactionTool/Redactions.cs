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
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event RedactionCalledDelegate RedactionCalled;

        private readonly Application _app;

        private readonly List<IRedaction> redactions;



        public Redactions(Application _app)
        {
            //log4net.Config.XmlConfigurator.Configure();

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
                var doc = CloneDocument(_app.ActiveDocument);

                try
                {
                    var successful = ApplyRedactions(doc);

                    if (successful)
                    {
                        doc.ExportAsFixedFormat(saveFile.Path, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

                        //MessageBox.Show(
                        //    text: $"Redacted PDF exported to {saveFileDialog1.FileName}",
                        //    caption: "Export Complete",
                        //    buttons: MessageBoxButtons.OK
                        //    );
                    }
                    else { MessageBox.Show("There was an error redacting your document.", "Error Redacting Document", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                finally
                {
                    doc.Close(WdSaveOptions.wdDoNotSaveChanges);
                }
            }
        }

        private static bool ApplyRedactions(Document doc)
        {
            bool InLineSuccess = false;
            bool ImageFloatSuccess = false;
            bool SpecialTablesSuccess = false;

            foreach (Range story in doc.StoryRanges)
            {
                InLineSuccess = RedactInLine(story);
                ImageFloatSuccess = RedactImageFloat(story);
            }

            SpecialTablesSuccess = RedactSpecialTables(doc);
            //successful = RedactCharts();

            return (InLineSuccess && ImageFloatSuccess && SpecialTablesSuccess);
        }

        public static void SaveUnredactedPDF(Document document, string ConfidentialityLabel, WdColorIndex highlight = WdColorIndex.wdNoHighlight) //leave non static so it can be called once 
        {
            {
                SaveFile saveFile = new SaveFile();

                if (saveFile.Path != null && saveFile.FileAvailable)
                {
                    var doc = CloneDocument(document);

                    UnMarkAll(doc, highlight);
                    
                    saveFile.FileMarking = ConfidentialityLabel;

                    // makes the file marking the same font as the document or Times New Roman
                    AddConfidentialityHeader(saveFile, doc);

                    UpdateTables(doc);

                    doc.ExportAsFixedFormat(saveFile.Path, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

                    doc.Close(WdSaveOptions.wdDoNotSaveChanges);
                }
            }
        }
        public static void UnMark(Range range, WdColorIndex highlight = WdColorIndex.wdNoHighlight)
        {
            bool successful = false;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            contentControls = range.ContentControls;

            // selects parent CC and removes marks for redaction
            if (contentControls.Count < 1 && range.ParentContentControl != null)
            {
                contentControl = range.ParentContentControl;
                successful = UnMarkInLine(contentControl, highlight);
            }
            else //removes marks for all redactions within a selection
            {
                foreach (ContentControl cc in contentControls)
                {
                    successful = UnMarkInLine(cc, highlight);
                }
            }

            successful = UnMarkImagesFloat(range);

            //UnmarkRedactionsChart();
        }

        public static void UnMarkAll(Document doc, WdColorIndex highlight = WdColorIndex.wdNoHighlight, bool ShowWarning = false)
        {
            DialogResult confirm = DialogResult.Cancel;
            if (ShowWarning)
            {
                confirm = MessageBox.Show("This action will remove all the marked redactions in the document. Continue with removing all redaction marks?", "Confirm", MessageBoxButtons.OKCancel);
            }

            if (!ShowWarning || confirm == DialogResult.OK)
            {
                foreach (Range story in doc.StoryRanges)
                {
                    UnMark(story, highlight);
                }

                UpdateTables(doc);
            }
        }

        public static void Mark(Selection selection)
        {
            bool successful = false;
            string UnsupportedTypes = "";

            if (!HasUnsupportedType(selection, out UnsupportedTypes))
            {
                try
                {
                    if (selection.ShapeRange.Count > 0) 
                    { 
                        successful = MarkImageFloat(selection);
                    }
                    else successful = MarkInline(selection);

                }
                catch
                {
                    successful = false;
                }

                //log.Info("Redaction Mark: successful = " + successful);

            }
            else if (HasUnsupportedType(selection, out UnsupportedTypes)) 
            {
                //log.Info("Redaction Mark: selection has unsupported types.");

                MessageBox.Show("Please first select an item or range to mark for redaction. "+UnsupportedTypes);
            } 
        }


        public static List<IRedaction> GetAll(Application _app)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Mark Redactions
        private static bool MarkInline(Selection selection)
        {
            //bool hasRedactionAlready = false;
            foreach (ContentControl cc in selection.ContentControls)
            {
                if (cc.Tag.StartsWith("R-"))
                {
                    UnMarkInLine(cc);

                }
            }
            

            if (selection.ParentContentControl == null || !selection.ParentContentControl.Tag.StartsWith("R-"))
            {
                if (selection.InlineShapes.Count > 0 || selection.Text.Length > 1)
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

        public static bool UnMarkImagesFloat(Range range)
        {
            foreach (Word.Shape shape in range.ShapeRange)
            {
                if (shape.Title.StartsWith("R-pic"))
                {
                    shape.Title = shape.ID.ToString();
                    shape.AlternativeText = shape.Title;

                    shape.PictureFormat.ColorType = MsoPictureColorType.msoPictureAutomatic;
                    shape.PictureFormat.Brightness = 0.5f;
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
            var text = range.Text;
            bool successful = true;

            try
            {
                foreach(ContentControl contentControl in range.ContentControls)
                {
                    if (contentControl.Title == "Redaction")
                    {
                        for (var j = 1; j <= contentControl.Range.ContentControls.Count; j++)
                        {
                            contentControl.Range.ContentControls[j].LockContents = false;
                        }

                        contentControl.Range.Font.Fill.Transparency = 1;
                        
                        successful = RemoveHyperlinks(contentControl.Range);

                        successful = RedactInlineImage(contentControl.Range);
                    }

                    //if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                }
            }
            finally
            {
                //if (range.ContentControls != null) Marshal.ReleaseComObject(range.ContentControls);
                //if (range != null) Marshal.ReleaseComObject(range);
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

            foreach (Word.Shape shape in range.ShapeRange)
            {
                if (shape.Title.StartsWith("R-pic"))
                {

                    shape.Title = shape.ID.ToString();
                    shape.AlternativeText = shape.Title;

                    shape.PictureFormat.Brightness = 0f;
                    try //shape.Hyperlink does not evaluate as null but if not present, any method used throws an exception.
                    {
                        shape.Hyperlink.Delete();
                    }
                    catch { }
                }
            }

            return true;
        }

        private static bool RedactSpecialTables(Document document)
        {
            bool successful = true;

            foreach (TableOfContents toc in document.TablesOfContents)
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

            foreach (TableOfAuthorities toa in document.TablesOfAuthorities)
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

            foreach (TableOfFigures tof in document.TablesOfFigures)
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

            foreach (Index index in document.Indexes)
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

        private static void AddConfidentialityHeader(SaveFile saveFile, Document doc, string headerfont = "times new roman")
        {
            if (doc.Sections.First.Range.Font.Name != null)
            {
                headerfont = doc.Sections.First.Range.Font.Name;
            }

            // marks the header with "confidential," updated to add a floating text box to the header rather than replace the header text
            foreach (Section section in doc.Sections)
            {
                if (section.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Exists)
                {
                    CreateHeader(saveFile, doc, headerfont, section, WdHeaderFooterIndex.wdHeaderFooterFirstPage);
                }
                if (section.Headers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].Exists)
                {
                    CreateHeader(saveFile, doc, headerfont, section, WdHeaderFooterIndex.wdHeaderFooterEvenPages);
                }
                if (section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Exists)
                {
                    CreateHeader(saveFile, doc, headerfont, section, WdHeaderFooterIndex.wdHeaderFooterPrimary);
                }
            }
        }

        private static void CreateHeader(SaveFile saveFile, Document doc, string headerfont, Section section, WdHeaderFooterIndex headerIndex)
        {
            var header = section.Headers[headerIndex].Shapes.AddTextbox(
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

        private static bool HasUnsupportedType(Selection selection, out string UnsupportedTypes)
        {
            //TODO: add textboxes, etc

            UnsupportedTypes = "Items currently not supporting redaction include: " +
                                "Text Boxes" + ", " +
                                "SmartArt" + ", " +
                                "Charts" + ", " +
                                "Diagrams" + ", " +
                                "Icons" + ", " +
                                "3D Models";
            bool hasUnsupportedType = false;

            foreach (InlineShape inlineShape in selection.InlineShapes)
            {
                if (
                    inlineShape.HasSmartArt != MsoTriState.msoFalse ||
                    inlineShape.HasChart != MsoTriState.msoFalse
                    )
                {
                    hasUnsupportedType = true;
                }

            }

            foreach (Word.Shape shape in selection.ShapeRange)
            {
                if (hasUnsupportedType
                    || shape.HasSmartArt != MsoTriState.msoFalse
                    || shape.HasChart != MsoTriState.msoFalse
                    //|| shape.HasDiagram != MsoTriState.msoFalse
                    //|| shape.HasDiagramNode != MsoTriState.msoFalse
                    )
                {
                    hasUnsupportedType = true;
                }
            }

            return hasUnsupportedType;
        }


        #endregion


    }
}



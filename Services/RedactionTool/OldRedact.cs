using System;
//---< Word Adddin >-----
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
//---</ Word Addin >-----

namespace Ribbon_0._0._1
{
    public static class Redactions
    {

        public static void MarkRedaction(Word.Application _app)
        {
            try
            {
                //MessageBox.Show(_app.Selection.ShapeRange.Count.ToString());
                if (_app.Selection.ShapeRange.Count > 0)
                {

                    MarkRedactionImageFloat(_app);
                    //MarkRedactionChart();
                }

                else if (_app.Selection.InlineShapes.Count > 0)
                {
                    var redaction = _app.Selection.ContentControls.Add(Word.WdContentControlType.wdContentControlRichText);

                    redaction.Title = "Redaction";
                    redaction.Tag = "R-" + redaction.ID;
                    redaction.Color = Word.WdColor.wdColorDarkRed;

                    redaction.Range.HighlightColorIndex = Word.WdColorIndex.wdBlack;
                    redaction.Range.Font.ColorIndex = Word.WdColorIndex.wdWhite;
                }
                else if (_app.Selection.Text != "" && _app.Selection.Text.Length > 1)
                {
                    //foreach (var wrd in _app.Selection.Words)
                    //MessageBox.Show(_app.Selection.Text);
                    {
                        var redaction = _app.Selection.ContentControls.Add(Word.WdContentControlType.wdContentControlRichText);

                        redaction.Title = "Redaction";
                        redaction.Tag = "R-" + redaction.ID;
                        redaction.Color = Word.WdColor.wdColorDarkRed;

                        redaction.Range.HighlightColorIndex = Word.WdColorIndex.wdBlack;
                        redaction.Range.Font.ColorIndex = Word.WdColorIndex.wdWhite;
                    }




                }
            }
            catch
            {
            }

        }

        public static void UnmarkRedactions(Word.Application _app)
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
            UnmarkRedactionImageFloat(_app);
            //UnmarkRedactionsChart(_app);
        }

        public static void MarkRedactionChart(Word.Application _app)
        {
            // not yet working. Use to select non-inline pictures for redaction

            if (_app.Selection.ShapeRange.Count > 0)
            {
                var redaction = _app.Selection.ShapeRange;

                var left = redaction[1].Left;
                var top = redaction[1].Top;
                var width = redaction[1].Width;
                var height = redaction[1].Height;
                var anchor = redaction[1].Anchor;


                redaction[1].Name = "R-chart" + redaction[1].ID;
                //redaction[1].Line.Visible = MsoTriState.msoTrue;
                //redaction[1].Line.Weight = 10;
                //redaction[1].Line.InsetPen = MsoTriState.msoTrue;
                ////redaction[1].Line.ForeColor = WdColorIndex.wdRed;
                //redaction[1].Line.DashStyle = MsoLineDashStyle.msoLineDashDot;

                redaction[1].Select();
                var redactionRect = _app.ActiveDocument.Shapes.AddShape(1, left, top, width, height, anchor);
                redactionRect.Fill.Transparency = 0.5f;
                redactionRect.Name = "R-Rect" + redactionRect.ID;


                //redaction[1].Select();
                //redaction[1].Chart.CopyPicture();
                //_app.Selection.Paste();
                //redaction[1].Delete();







                //MessageBox.Show("Selection marked for Redaction");

            }
            else
            {
                //MessageBox.Show("Nothing has been selected for redaction.");
            }

        }

        public static void ApplyRedactionChart(Word.Application _app)
        {
            var redaction = _app.Selection.ShapeRange;

        }

        public static void UnmarkRedactionChart(Word.Application _app)
        {

        }

        public static void UnmarkRedactionChartAll(Word.Application _app)
        {
            for (int c = 1; c <= _app.ActiveDocument.Shapes.Count; c++)
            {
                UnmarkRedactionChart(_app);
            }
        }

        public static void MarkRedactionImageFloat(Word.Application _app)
        {

            //MessageBox.Show("Selection Shape Count: "+
            //_app.Selection.ShapeRange
            //.Count.ToString()
            //+ "\n" + "All Shapes Count: "+
            //_app.ActiveDocument.Shapes.Count.ToString());


            if (_app.Selection.ShapeRange.Count > 0)
            {

                for (int shape = 1; shape <= _app.Selection.ShapeRange.Count; shape++)
                {
                    if (_app.Selection.ShapeRange[shape].HasChart == MsoTriState.msoFalse
                        //&& _app.Selection.ShapeRange[shape].HasDiagram == MsoTriState.msoFalse
                        //&& _app.Selection.ShapeRange[shape].HasDiagramNode == MsoTriState.msoFalse
                        //&& _app.Selection.ShapeRange[shape].HasSmartArt == MsoTriState.msoFalse
                        )
                    {
                        var redaction = _app.Selection.ShapeRange[shape];

                        redaction.Title = "R-pic" + redaction.ID;
                        redaction.AlternativeText = redaction.Title;

                        redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureGrayscale;
                        redaction.PictureFormat.Brightness = 0.23f;

                    }
                    else
                    {
                    }

                }

                //MessageBox.Show("Selection marked for Redaction");
            }
            else
            { //MessageBox.Show("Nothing has been selected for redaction.");
            }
        }

        public static void ApplyRedactionImageFloat(Word.Application _app)
        {
            var ShapesFloat = _app.ActiveDocument.Shapes;
            for (int shape = 1; shape <= ShapesFloat.Count; shape++)
            {
                var redaction = ShapesFloat[shape];
                if (redaction.Title.StartsWith("R-pic") && redaction.HasChart == MsoTriState.msoFalse)
                {
                    redaction.PictureFormat.Brightness = 0f;
                }

            }
        }

        public static void UnmarkRedactionImageFloat(Word.Application _app)
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

        public static void UnmarkRedactionImageFloatAll(Word.Application _app)
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

        public static void SaveRedactedPDF(Word.Application _app)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.Title = "Export Redacted PDF";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                bool fileAvailable = true;
                FileInfo file = new FileInfo(saveFileDialog1.FileName);
                if (file.Exists == true)
                {
                    try
                    {
                        using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            stream.Close();
                            fileAvailable = true;
                        }
                    }
                    catch (IOException)
                    {
                        //the file is unavailable because it is:
                        //still being written to
                        //or being processed by another thread
                        //or does not exist (has already been processed)
                        fileAvailable = false;
                        MessageBox.Show("File is open in another window or program. Please close the file and try again.");

                    }
                }
                if (fileAvailable)
                {


                    var doc = CloneDocument(_app.ActiveDocument);
                    ApplyRedactions(_app);
                    ApplyRedactionsFooter(_app);
                    ApplyRedactionsEndNote(_app);
                    ApplyRedactionImageFloat(_app);
                    //ApplyRedactionsChart(_app);

                    _app.ActiveDocument.ExportAsFixedFormat(saveFileDialog1.FileName, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

                    doc.Close(WdSaveOptions.wdDoNotSaveChanges);

                    //MessageBox.Show(
                    //    text: $"Redacted PDF exported to {saveFileDialog1.FileName}",
                    //    caption: "Export Complete",
                    //    buttons: MessageBoxButtons.OK
                    //    );
                }
            }
        }

        public static string ConfidentialityLabel = null;
        public static void SaveUnredactedPDF(Word.Application _app)
        {
            // Currently turns all text to Automatic Black font (unlikely that it will neeed to be in other color)

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.Title = "Export Unredacted PDF";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                bool fileAvailable = true;
                FileInfo file = new FileInfo(saveFileDialog1.FileName);
                if (file.Exists == true)
                {
                    try
                    {
                        using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            stream.Close();
                            fileAvailable = true;
                        }
                    }
                    catch (IOException)
                    {
                        //the file is unavailable because it is:
                        //still being written to
                        //or being processed by another thread
                        //or does not exist (has already been processed)
                        fileAvailable = false;
                        MessageBox.Show("File is open in another window or program. Please close the file and try again.");
                    }
                }

                if (fileAvailable)
                {
                    // TODO: add link to confidentiality control

                    if (ConfidentialityLabel == null)
                    {

                    }
                    else
                    {
                        var doc = CloneDocument(_app.ActiveDocument);
                        var headerfont = "Times New Roman";

                        if (_app.ActiveDocument.Sections.First.Range.Font.Name != null)
                        {
                            headerfont = _app.ActiveDocument.Sections.First.Range.Font.Name;
                        }

                        /// Marks the header with "confidential," Updated to add a floating text box to the header rather than replace the header text
                        foreach (Section section in _app.ActiveDocument.Sections)
                        {
                            var header = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Shapes.AddTextbox(
                                Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
                                _app.ActiveDocument.PageSetup.PageWidth - 525,
                                10,
                                500,
                                20);
                            header.TextFrame.TextRange.Text = Redactions.ConfidentialityLabel.ToUpper();

                            header.TextFrame.TextRange.Font.Name = headerfont;

                            header.TextFrame.TextRange.Font.Size = 12;
                            header.TextFrame.TextRange.Font.Bold = -1;

                            header.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            header.TextFrame.TextRange.HighlightColorIndex = WdColorIndex.wdWhite;
                            header.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        }

                        Word.ContentControls contentControls = null;
                        Word.ContentControl contentControl = null;


                        contentControls = _app.ActiveDocument.ContentControls;
                        for (int i = 1; i <= contentControls.Count; i++)
                        {
                            contentControl = contentControls[i];
                            if (contentControl.Title == "Redaction")
                            {
                                contentControl.Range.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                                contentControl.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
                                //contentControl.Delete(false);
                            }
                            if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                        }

                        UnmarkRedactionsFooter(_app);
                        UnmakrRedactionsEndNote(_app);
                        UnmarkRedactionImageFloatAll(_app);

                        _app.ActiveDocument.ExportAsFixedFormat(saveFileDialog1.FileName, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true);

                        doc.Close(WdSaveOptions.wdDoNotSaveChanges);


                    }
                }
            }
        }


        #region Methods without Ribbon Access (nested methods)
        public static void ApplyRedactions(Word.Application _app)
        {
            Word.Document doc = null;
            Word.ContentControls contentControls = null;
            Word.ContentControl contentControl = null;

            string controlsList = string.Empty;

            try
            {
                doc = _app.ActiveDocument;
                contentControls = doc.ContentControls;
                for (int i = 1; i <= contentControls.Count; i++)
                {
                    contentControl = contentControls[i];

                    if (contentControl.Title == "Redaction")
                    {
                        //var ccString = contentControl.Range.Text.ToString();

                        #region Characters to be replaced in redaction Times New Roman
                        //ccString = ccString.
                        //    Replace("A", "||||").
                        //    Replace("B", "|||").
                        //    Replace("C", "|||").
                        //    Replace("D", "||||").
                        //    Replace("E", "|||").
                        //    Replace("F", "|||").
                        //    Replace("G", "||||").
                        //    Replace("H", "||||").
                        //    Replace("I", "||").
                        //    Replace("J", "||").
                        //    Replace("K", "||||").
                        //    Replace("L", "|||").
                        //    Replace("M", "||||").
                        //    Replace("N", "|||").
                        //    Replace("O", "||||").
                        //    Replace("P", "|||").
                        //    Replace("Q", "|||").
                        //    Replace("R", "||||").
                        //    Replace("S", "|||").
                        //    Replace("T", "|||").
                        //    Replace("U", "|||").
                        //    Replace("V", "|||").
                        //    Replace("W", "|||||").
                        //    Replace("X", "||||").
                        //    Replace("Y", "||||").
                        //    Replace("Z", "|||").

                        //    Replace("a", "||").
                        //    Replace("b", "|||").
                        //    Replace("c", "||").
                        //    Replace("d", "|||").
                        //    Replace("e", "||").
                        //    Replace("f", "|").
                        //    Replace("g", "||").
                        //    Replace("h", "||").
                        //    Replace("i", "|").
                        //    Replace("j", "|").
                        //    Replace("k", "|||").
                        //    Replace("l", "|").
                        //    Replace("m", "||||").
                        //    Replace("n", "|||").
                        //    Replace("o", "|||").
                        //    Replace("p", "|||").
                        //    Replace("q", "|||").
                        //    Replace("r", "||").
                        //    Replace("s", "||").
                        //    Replace("t", "|").
                        //    Replace("u", "||").
                        //    Replace("v", "||").
                        //    Replace("w", "||||").
                        //    Replace("x", "||").
                        //    Replace("y", "|||").
                        //    Replace("z", "||").

                        //    Replace("0", "|||").
                        //    Replace("1", "||").
                        //    Replace("2", "|||").
                        //    Replace("3", "||").
                        //    Replace("4", "|||").
                        //    Replace("5", "|||").
                        //    Replace("6", "|||").
                        //    Replace("7", "|||").
                        //    Replace("8", "|||").
                        //    Replace("9", "|||").

                        //    Replace(".", "|").
                        //    Replace("%", "||||").
                        //    Replace(",", "|").
                        //    Replace("$", "|||").
                        //    Replace("?", "||").
                        //    Replace(";", "|").
                        //    Replace("'", "|").
                        //    Replace("Quote", "||"). //Placeholder
                        //    Replace("TM", "|||||"). //Placeholder
                        //    Replace("Pilcrow", "|||"). //Placeholder
                        //    Replace("Copyright", "||||"). //Placeholder
                        //    Replace("Section", "|||"). //Placeholder
                        //    Replace("-", "||").
                        //    Replace("N-Dash", "|||"). //Placeholder
                        //    Replace("M-Dash", "||||||"). //Placeholder
                        //    Replace("(", "||").
                        //    Replace(")", "||");
                        //;
                        #endregion

                        //contentControl.Range.Text = ccString;
                        //contentControl.Range.Font.ColorIndex = iWord.WdColorIndex.wdBlack;
                        //contentControl.Range.HighlightColorIndex = iWord.WdColorIndex.wdBlack;
                        contentControl.Range.Font.Fill.Transparency = 1;
                        for (var shape = 1; shape <= contentControl.Range.InlineShapes.Count; shape++)
                        {
                            contentControl.Range.InlineShapes[shape].PictureFormat.Brightness = 0f;
                        }
                    }

                    if (contentControl != null) Marshal.ReleaseComObject(contentControl);
                }
            }
            finally
            {

                if (contentControls != null) Marshal.ReleaseComObject(contentControls);
                if (doc != null) Marshal.ReleaseComObject(doc);
            }

        }

        public static void ApplyRedactionsFooter(Word.Application _app)
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
                            contentControl.Range.Font.Fill.Transparency = 1;
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

        public static void ApplyRedactionsEndNote(Word.Application _app)
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
                            contentControl.Range.Font.Fill.Transparency = 1;
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

        public static void UnmarkRedactionsFooter(Word.Application _app)
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

        public static void UnmakrRedactionsEndNote(Word.Application _app)
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

        private static Document CloneDocument(Document inputDocument)
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

            //fileToRedact.Fields.Unlink(); //used to replace the content controls with hard-coded data. Looks like it isn't necesary and gets in the way of the Exhibit tool (content controls are locked).

            return fileToRedact;
        }

        //public static void ApplyRedactionsTNR()
        //{
        //    iWord.Document doc = null;
        //    iWord.ContentControls contentControls = null;
        //    iWord.ContentControl contentControl = null;

        //    string controlsList = string.Empty;

        //    try
        //    {
        //        doc = _app.ActiveDocument;
        //        contentControls = doc.ContentControls;
        //        for (int i = 1; i <= contentControls.Count; i++)
        //        {
        //            contentControl = contentControls[i];

        //            if (contentControl.Title == "Redaction")
        //            {
        //                var ccString = contentControl.Range.Text.ToString();

        //                if (contentControl.Range.Font.Name == "Times New Roman")
        //                {
        //                    redacted = ccString;
        //                    ccString = TimesNR_piping();
        //                }
        //                else MessageBox.Show("Relative redaction spacing is only held with Times New Roman");


        //                contentControl.Range.Text = ccString;
        //                contentControl.Range.Font.ColorIndex = iWord.WdColorIndex.wdBlack;
        //                contentControl.Range.HighlightColorIndex = iWord.WdColorIndex.wdBlack;
        //            }

        //            if (contentControl != null) Marshal.ReleaseComObject(contentControl);
        //        }
        //    }
        //    finally
        //    {

        //        if (contentControls != null) Marshal.ReleaseComObject(contentControls);
        //        if (doc != null) Marshal.ReleaseComObject(doc);
        //    }

        //}

        //public static string redacted = null;

        //public static string TimesNR_piping()
        //{

        //    return redacted.
        //        Replace("A", "||||").
        //        Replace("B", "|||").
        //        Replace("C", "|||").
        //        Replace("D", "||||").
        //        Replace("E", "|||").
        //        Replace("F", "|||").
        //        Replace("G", "||||").
        //        Replace("H", "||||").
        //        Replace("I", "||").
        //        Replace("J", "||").
        //        Replace("K", "||||").
        //        Replace("L", "|||").
        //        Replace("M", "||||").
        //        Replace("N", "|||").
        //        Replace("O", "||||").
        //        Replace("P", "|||").
        //        Replace("Q", "|||").
        //        Replace("R", "||||").
        //        Replace("S", "|||").
        //        Replace("T", "|||").
        //        Replace("U", "|||").
        //        Replace("V", "|||").
        //        Replace("W", "|||||").
        //        Replace("X", "||||").
        //        Replace("Y", "||||").
        //        Replace("Z", "|||").

        //        Replace("a", "||").
        //        Replace("b", "|||").
        //        Replace("c", "||").
        //        Replace("d", "|||").
        //        Replace("e", "||").
        //        Replace("f", "|").
        //        Replace("g", "||").
        //        Replace("h", "||").
        //        Replace("i", "|").
        //        Replace("j", "|").
        //        Replace("k", "|||").
        //        Replace("l", "|").
        //        Replace("m", "||||").
        //        Replace("n", "|||").
        //        Replace("o", "|||").
        //        Replace("p", "|||").
        //        Replace("q", "|||").
        //        Replace("r", "||").
        //        Replace("s", "||").
        //        Replace("t", "|").
        //        Replace("u", "||").
        //        Replace("v", "||").
        //        Replace("w", "||||").
        //        Replace("x", "||").
        //        Replace("y", "|||").
        //        Replace("z", "||").

        //        Replace("0", "|||").
        //        Replace("1", "||").
        //        Replace("2", "|||").
        //        Replace("3", "||").
        //        Replace("4", "|||").
        //        Replace("5", "|||").
        //        Replace("6", "|||").
        //        Replace("7", "|||").
        //        Replace("8", "|||").
        //        Replace("9", "|||").

        //        Replace(".", "|").
        //        Replace("%", "||||").
        //        Replace(",", "|").
        //        Replace("$", "|||").
        //        Replace("?", "||").
        //        Replace(";", "|").
        //        Replace("'", "|").
        //        Replace("Quote", "||"). //Placeholder
        //        Replace("TM", "|||||"). //Placeholder
        //        Replace("Pilcrow", "|||"). //Placeholder
        //        Replace("Copyright", "||||"). //Placeholder
        //        Replace("Section", "|||"). //Placeholder
        //        Replace("-", "||").
        //        Replace("N-Dash", "|||"). //Placeholder
        //        Replace("M-Dash", "||||||"). //Placeholder
        //        Replace("(", "||").
        //        Replace(")", "||");
        //}

        //public static void RedactionsBox()
        //{
        //    iWord.Document doc = null;
        //    iWord.ContentControls contentControls = null;
        //    iWord.ContentControl contentControl = null;
        //    doc = _app.ActiveDocument;
        //    contentControls = doc.ContentControls;

        //    try
        //    {
        //        for (int i = 1; i <= contentControls.Count; i++)
        //        {
        //            contentControl = contentControls[i];

        //            if (contentControl.Title == "Redaction")
        //            {


        //            }

        //            if (contentControl != null) Marshal.ReleaseComObject(contentControl);
        //        }
        //    }
        //    finally
        //    {
        //        if (contentControls != null) Marshal.ReleaseComObject(contentControls);
        //        if (doc != null) Marshal.ReleaseComObject(doc);
        //    }
        //}
        #endregion
    }
}

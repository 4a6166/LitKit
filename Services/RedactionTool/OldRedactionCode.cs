using System;
using System.IO;
using System.Linq;
//---< Word Adddin >-----
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using Services;
//---</ Word Addin >-----


/*
namespace OldCode
{
    public class OldRedactionCode : BaseService
    {
        private readonly Word.Application _app;
        public OldRedactionCode()
        {
        }
        public OldRedactionCode(Application _app)
          : this() => this._app = _app;

        public void MarkRedaction()
        {
            try
            {
                //MessageBox.Show(_app.Selection.ShapeRange.Count.ToString());
                if (_app.Selection.ShapeRange.Count > 0)
                {

                    MarkRedactionImageFloat();
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
                        var redaction = _app.Selection.ContentControls.Add(Word.WdContentControlType.wdContentControlText);

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
                //MessageBox.Show("Selection Error");
                //Form1 toast = new Form1();
                //toast.MainTitle("Selection Error");
                //toast.Subtitle("Please select either entire paragraphs or individual sections withing discrete paragraphs.");
                //toast.Show();
            }

        }

        public void UnmarkRedactions()
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
            UnmarkRedactionImageFloat();
            //UnmarkRedactionsChart();
        }

        public void MarkRedactionChart()
        {
            // not yet working. Use to select non-inline pictures for redaction

            if (_app.Selection.ShapeRange.Count > 0)
            {
                var redaction = _app.Selection.ShapeRange;

                var type = MsoAutoShapeType.msoShapeRectangle;
                var left = redaction[1].Left;
                var top = redaction[1].Top;
                var width = redaction[1].Width;
                var height = redaction[1].Height;
                var anchor = redaction[1].Anchor;


                redaction[1].Name = "R-chart" + redaction[1].ID;
                //redaction[1].Line.Visible = MsoTriState.msotrue;
                //redaction[1].Line.Weight = 10;
                //redaction[1].Line.InsetPen = MsoTriState.msotrue;
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
                //Form1 Toast = new Form1();

                //Toast.MainTitle("Selection marked for Redaction");
                //Toast.Subtitle("The chart marked for redaction.");
                //Toast.Show();

            }
            else
            {
                //MessageBox.Show("Nothing has been selected for redaction.");
                //Form1 toast = new Form1();
                //toast.MainTitle("Selection Error");
                //toast.Subtitle("Please select the entire chart before applying the redaction.");
                //toast.Show();
            }

        }

        public void ApplyRedactionChart()
        {
            var redaction = _app.Selection.ShapeRange;

        }

        public void UnmarkRedactionChart()
        {

        }

        public void UnmarkRedactionChartAll()
        {
            for (int c = 1; c <= _app.ActiveDocument.Shapes.Count; c++)
            {
                UnmarkRedactionChart();
            }
        }

        public void MarkRedactionImageFloat()
        {

            //MessageBox.Show("Selection Shape Count: "+
            //_app.Selection.ShapeRange
            //.Count.ToString()
            //+ "\n" + "All Shapes Count: "+
            //_app.ActiveDocument.Shapes.Count.ToString());


            if (_app.Selection.ShapeRange.Count > 0)
            {
                var toastTitle = "";
                var toastSub = "";

                for (int shape = 1; shape <= _app.Selection.ShapeRange.Count; shape++)
                {
                    if (_app.Selection.ShapeRange[shape].HasChart == MsoTriState.msofalse
                        //&& _app.Selection.ShapeRange[shape].HasDiagram == MsoTriState.msofalse
                        //&& _app.Selection.ShapeRange[shape].HasDiagramNode == MsoTriState.msofalse
                        //&& _app.Selection.ShapeRange[shape].HasSmartArt == MsoTriState.msofalse
                        )
                    {
                        var redaction = _app.Selection.ShapeRange[shape];

                        redaction.Title = "R-pic" + redaction.ID;
                        redaction.AlternativeText = redaction.Title;

                        redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureGrayscale;
                        redaction.PictureFormat.Brightness = 0.23f;

                        toastTitle = "Selection marked for Redaction";
                        toastSub = "Note: Charts, Diagrams, and SmartArt cannot be marked for redaction.";
                    }
                    else
                    {
                        toastTitle = "Selection Error";
                        toastSub = "Note: Charts, Diagrams, and SmartArt cannot be marked for redaction.";
                    }

                }

                //MessageBox.Show("Selection marked for Redaction");
                //Form1 toast = new Form1();
                //toast.MainTitle(toastTitle);
                //toast.Subtitle(toastSub);
                //toast.Show();


            }
            else
            { //MessageBox.Show("Nothing has been selected for redaction.");
                //Form1 toast = new Form1();
                //toast.MainTitle("Selection Error");
                //toast.Subtitle("Note: Charts, Diagrams, and SmartArt cannot be marked for redaction.");
                //toast.Show();
            }
        }

        public void ApplyRedactionImageFloat()
        {
            var ShapesFloat = _app.ActiveDocument.Shapes;
            for (int shape = 1; shape <= ShapesFloat.Count; shape++)
            {
                var redaction = ShapesFloat[shape];
                if (redaction.Title.StartsWith("R-pic") && redaction.HasChart == MsoTriState.msofalse)
                {
                    redaction.PictureFormat.Brightness = 0f;
                }

            }
        }

        public void UnmarkRedactionImageFloat()
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

        public void UnmarkRedactionImageFloatAll()
        {
            var ShapesFloat = _app.ActiveDocument.Shapes;
            for (int shape = 1; shape <= ShapesFloat.Count; shape++)
            {
                var redaction = ShapesFloat[shape];
                if (redaction.Title.StartsWith("R-pic") && redaction.HasChart == MsoTriState.msofalse)
                {
                    redaction.PictureFormat.Brightness = 0.5f;
                    redaction.PictureFormat.ColorType = MsoPictureColorType.msoPictureAutomatic;
                }

            }
        }

        //public void SaveRedactedPDF()
        //{
        //    var frmLoading = new Ribbon_0._0._1.Forms_Ours.frmLoading();

        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        //    saveFileDialog1.Filter = "PDF|*.pdf";
        //    saveFileDialog1.Title = "Export Redacted PDF";
        //    saveFileDialog1.ShowDialog();

        //    if (saveFileDialog1.FileName != "")
        //    {
        //        bool fileAvailable = true;
        //        FileInfo file = new FileInfo(saveFileDialog1.FileName);
        //        if (file.Exists == true)
        //        {
        //            try
        //            {
        //                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
        //                {
        //                    stream.Close();
        //                    fileAvailable = true;
        //                }
        //            }
        //            catch (IOException)
        //            {
        //                //the file is unavailable because it is:
        //                //still being written to
        //                //or being processed by another thread
        //                //or does not exist (has already been processed)
        //                fileAvailable = false;
        //                MessageBox.Show("File is open in another window or program. Please close the file and try again.");

        //            }
        //        }
        //        if (fileAvailable)
        //        {
        //            frmLoading.TopMost = true;
        //            frmLoading.Show();

        //            var doc = CloneDocument(_app.ActiveDocument);
        //            ApplyRedactions();
        //            ApplyRedactionsFooter();
        //            ApplyRedactionsEndNote();
        //            ApplyRedactionImageFloat();
        //            //ApplyRedactionsChart();

        //            _app.ActiveDocument.ExportAsFixedFormat(saveFileDialog1.FileName, Word.WdExportFormat.wdExportFormatPDF, OpenAfterExport: true, IncludeDocProps: false, KeepIRM: false, DocStructureTags: false, BitmapMissingFonts: true, UseISO19005_1: false);

        //            doc.Close(WdSaveOptions.wdDoNotSaveChanges);

        //            frmLoading.Close();

        //            //MessageBox.Show(
        //            //    text: $"Redacted PDF exported to {saveFileDialog1.FileName}",
        //            //    caption: "Export Complete",
        //            //    buttons: MessageBoxButtons.OK
        //            //    );
        //            Form1 toast = new Form1();
        //            toast.MainTitle("Export Complete");
        //            toast.Subtitle($"Redacted PDF exported to {saveFileDialog1.FileName}");
        //            toast.Show();
        //        }
        //    }
        //}

        public string ConfidentialityLabel = null;
        //public void saveunredactedpdf()
        //{
        //    var frmloading = new ribbon_0._0._1.forms_ours.frmloading();
        //    // currently turns all text to automatic black font (unlikely that it will neeed to be in other color)

        //    savefiledialog savefiledialog1 = new savefiledialog();
        //    savefiledialog1.filter = "pdf|*.pdf";
        //    savefiledialog1.title = "export unredacted pdf";
        //    savefiledialog1.showdialog();

        //    if (savefiledialog1.filename != "")
        //    {
        //        bool fileavailable = true;
        //        fileinfo file = new fileinfo(savefiledialog1.filename);
        //        if (file.exists == true)
        //        {
        //            try
        //            {
        //                using (filestream stream = file.open(filemode.open, fileaccess.read, fileshare.none))
        //                {
        //                    stream.close();
        //                    fileavailable = true;
        //                }
        //            }
        //            catch (ioexception)
        //            {
        //                //the file is unavailable because it is:
        //                //still being written to
        //                //or being processed by another thread
        //                //or does not exist (has already been processed)
        //                fileavailable = false;
        //                messagebox.show("file is open in another window or program. please close the file and try again.");
        //            }
        //        }

        //        if (fileavailable)
        //        {
        //            frmconfidentialmarker confidentialmarker = new frmconfidentialmarker();
        //            confidentialmarker.showdialog();

        //            if (confidentialitylabel == null)
        //            {

        //            }
        //            else
        //            {
        //                frmloading.topmost = true;
        //                frmloading.show();

        //                var doc = clonedocument(_app.activedocument);
        //                var headerfont = "times new roman";

        //                if (_app.activedocument.sections.first.range.font.name != null)
        //                {
        //                    headerfont = _app.activedocument.sections.first.range.font.name;
        //                }

        //                /// marks the header with "confidential," updated to add a floating text box to the header rather than replace the header text
        //                foreach (section section in _app.activedocument.sections)
        //                {
        //                    var header = section.headers[wdheaderfooterindex.wdheaderfooterprimary].shapes.addtextbox(
        //                        microsoft.office.core.msotextorientation.msotextorientationhorizontal,
        //                        _app.activedocument.pagesetup.pagewidth - 525,
        //                        10,
        //                        500,
        //                        20);
        //                    header.textframe.textrange.text = confidentialitylabel.toupper();

        //                    header.textframe.textrange.font.name = headerfont;

        //                    header.textframe.textrange.font.size = 12;
        //                    header.textframe.textrange.font.bold = -1;

        //                    header.line.visible = microsoft.office.core.msotristate.msofalse;
        //                    header.textframe.textrange.highlightcolorindex = wdcolorindex.wdwhite;
        //                    header.textframe.textrange.paragraphformat.alignment = wdparagraphalignment.wdalignparagraphright;
        //                }

        //                word.selection selection = null;
        //                word.contentcontrols contentcontrols = null;
        //                word.contentcontrol contentcontrol = null;


        //                contentcontrols = _app.activedocument.contentcontrols;
        //                for (int i = 1; i <= contentcontrols.count; i++)
        //                {
        //                    contentcontrol = contentcontrols[i];
        //                    if (contentcontrol.title == "redaction")
        //                    {
        //                        contentcontrol.range.font.colorindex = word.wdcolorindex.wdauto;
        //                        contentcontrol.range.highlightcolorindex = word.wdcolorindex.wdnohighlight;
        //                        //contentcontrol.delete(false);
        //                    }
        //                    if (contentcontrol != null) marshal.releasecomobject(contentcontrol);
        //                }

        //                unmarkredactionsfooter();
        //                unmakrredactionsendnote();
        //                unmarkredactionimagefloatall();

        //                _app.activedocument.exportasfixedformat(savefiledialog1.filename, word.wdexportformat.wdexportformatpdf, openafterexport: true);

        //                doc.close(wdsaveoptions.wddonotsavechanges);

        //                frmloading.close();

        //                ////messagebox.show($"unredacted pdf exported to {savefiledialog1.filename}");
        //                //form1 toast = new form1();
        //                //toast.maintitle("export complete");
        //                //toast.subtitle($"unredacted pdf exported to {savefiledialog1.filename}");
        //                //toast.show();
        //            }
        //        }
        //    }
        //}


        #region Methods without Ribbon Access (nested methods)

        public void ApplyRedactions()
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

        public void ApplyRedactionsFooter()
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

        public void ApplyRedactionsEndNote()
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

        public void UnmarkRedactionsFooter()
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

        public void UnmakrRedactionsEndNote()
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

        private Document CloneDocument(Document inputDocument)
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

            fileToRedact.Fields.Unlink();

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




    */
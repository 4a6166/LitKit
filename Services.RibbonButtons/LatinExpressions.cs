using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Microsoft.Office.Interop.Word;
using Services.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;


namespace Tools.Simple
{
    /// <summary>
    /// Italicize latin expressions
    /// </summary>
    public class LatinExpressions
    {
        private List<string> Expressions = new List<string>();
        private bool DictionaryLoaded = false;
        private bool _pulledStandardDict;
        public bool pulledStandardDict { get { return _pulledStandardDict; } }
        private string filename = @"LatinDict.dic";

        public LatinExpressions()
        {
            DictionaryLoaded = ExpressionsRepository.ReadRepository(path: Dicts.GetExpressionFilePath(filename, out _pulledStandardDict), Expressions);
        }


        public bool UpdateExpressionFile(string ExpressionsList)
        {
            return Dicts.UpdatePersonalDict(filename, ExpressionsList, pulledStandardDict);
        }

        public bool Italicize(Word.Application _app, int italics)
        {
            bool result = false;
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;
            try
            {
                //_app.ActiveDocument.Select();
                //_app.Selection.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                //foreach (Range rng in _app.ActiveDocument.StoryRanges)
                //{
                //    foreach (string expression in Expressions)
                //    {
                //        string expression_firstLetter = "[" + expression.Substring(0, 1).ToLower() + expression.Substring(0, 1).ToUpper() + "]";
                //        string expression_rest = expression.Substring(1);
                //        rng.Find.ClearFormatting();
                //        rng.Find.Replacement.ClearFormatting();

                //        rng.Find.Replacement.Font.Italic = italics;
                //        //rng.Find.Text = "(" + expression_firstLetter + expression_rest + ")";
                //        //rng.Find.Replacement.Text = @"\1";
                //        rng.Find.Text = "(" + expression + ")";
                //        rng.Find.Replacement.Text = expression;

                //        rng.Find.MatchWholeWord = true;
                //        rng.Find.MatchWildcards = true;

                //        rng.Find.Execute(Replace: WdReplace.wdReplaceAll);
                //    }
                //}

                LatinByOpenXML(_app, italics);

                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            return result;
        }

        private void LatinByOpenXML(Word.Application _app, int italics)
        {
            Cursor.Current = Cursors.WaitCursor;

            string RangesNotItalicized = "";
            //var mainStoryRange = _app.ActiveDocument.StoryRanges[WdStoryType.wdMainTextStory];
            foreach (Range range in _app.ActiveDocument.StoryRanges)
            {
                try
                {
                    WordprocessingDocument doc = WordprocessingDocument.FromFlatOpcString(range.WordOpenXML);

                    var body = doc.MainDocumentPart.Document.Body;
                    var paras = body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>();

                    foreach (var para in paras)
                    {
                        var runs = para.Elements<Run>();

                        foreach (var run in runs)
                        {
                            foreach (string expression in Expressions)
                            {
                                if (run.InnerText.Contains(expression))
                                {

                                    var start = run.InnerText.IndexOf(expression);
                                    var firstPart = new Text(run.InnerText.Substring(0, start));
                                    firstPart.Space = SpaceProcessingModeValues.Preserve;
                                    var secondPart = new Text(run.InnerText.Substring(start + expression.Length));
                                    secondPart.Space = SpaceProcessingModeValues.Preserve;
                                    var latinText = new Text(expression);
                                    latinText.Space = SpaceProcessingModeValues.Preserve;


                                    var run1 = run.InsertBeforeSelf<Run>(new Run(firstPart));
                                    var run2 = run.InsertBeforeSelf<Run>(new Run(latinText));

                                    var run3 = run.InsertBeforeSelf<Run>(new Run(secondPart));

                                    RunProperties runProps = new RunProperties();
                                    if (run.Elements<RunProperties>().Count() > 0)
                                    {
                                        runProps = (RunProperties)run.Elements<RunProperties>().FirstOrDefault().Clone();

                                        run1.AddChild((RunProperties)run.Elements<RunProperties>().FirstOrDefault().Clone());
                                        run3.AddChild((RunProperties)run.Elements<RunProperties>().FirstOrDefault().Clone());

                                    }

                                    var italic = new Italic();

                                    if (italics == -1)
                                    {
                                        italic.Val = true;
                                        runProps.Italic = italic;
                                        run2.AddChild(runProps);
                                    }
                                    else {
                                        italic.Val = false;
                                        runProps.Italic = italic;
                                        run2.AddChild(runProps); }


                                    run.RemoveAllChildren();
                                }
                            }
                        }
                    }
                    range.InsertXML(doc.ToFlatOpcString());
                }
                catch { RangesNotItalicized += Environment.NewLine + " - "+range.StoryType; }
            }

            Cursor.Current = Cursors.Default;

            if (RangesNotItalicized != "")
            {
                MessageBox.Show("LitKit was unable to interact with the following document areas: "+RangesNotItalicized);
            }

        }
    }
}

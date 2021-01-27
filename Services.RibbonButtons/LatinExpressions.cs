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
                _app.ActiveDocument.Select();
                _app.Selection.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                foreach (Range rng in _app.ActiveDocument.StoryRanges)
                {
                    foreach (string expression in Expressions)
                    {
                        string expression_firstLetter = "["+expression.Substring(0,1).ToLower() + expression.Substring(0, 1).ToUpper()+"]";
                        string expression_rest = expression.Substring(1);
                        rng.Find.Replacement.Font.Italic = italics;
                        rng.Find.Text = "("+expression_firstLetter+expression_rest+")";
                        rng.Find.Replacement.Text = @"\1";
                        rng.Find.MatchWholeWord = true;
                        rng.Find.MatchWildcards = true;

                        rng.Find.Execute(Replace: WdReplace.wdReplaceAll);
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            return result;
        }
    }
}

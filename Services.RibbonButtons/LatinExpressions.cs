using Microsoft.Office.Interop.Word;
using Services.Base;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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

        public LatinExpressions()
        {
            DictionaryLoaded = ExpressionsRepository.ReadRepository(getExpressionFilePath(), Expressions);
        }

        private string getExpressionFilePath()
        {
            return @"C:\Users\Jake\Google Drive (jacob.field@prelimine.com)\repos\LitKit1_git\LitKit1\Services.RibbonButtons\Dictionaries\LatinDict.dic";

            /*TODO:
             * if file is in roaming data/prelimine, get path
             * else get file from program files / prelimine
             */

        }

        public bool UpdateExpressionFile(string ExpressionsList)
        {
            return true;

            /*TODO:
             * if roaming data/prelimine doesn't exist, make it
             * overwrite roaming data/prelimine to ExpressionsList
             */
        }

        public bool Italicize(Word.Application _app, int italics)
        {
            bool result = false;
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;
            try
            {
                _app.ActiveDocument.Select();
                _app.Selection.Find.Execute(FindText: " ", ReplaceWith: " "); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                foreach (Range rng in _app.ActiveDocument.StoryRanges)
                {
                    foreach (string expression in Expressions)
                    {
                        rng.Find.Replacement.Font.Italic = italics;
                        rng.Find.Text = expression;
                        rng.Find.Replacement.Text = expression;
                        rng.Find.MatchWholeWord = true;

                        rng.Find.Execute(Replace: WdReplace.wdReplaceAll);
                    }
                }

                result = true;
            }
            catch { };

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            return result;
        }
    }
}

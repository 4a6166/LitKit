﻿using Microsoft.Office.Interop.Word;
using System.Linq;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;


namespace Tools.Simple
{
    /// <summary>
    /// Adds or removes Oxford Commas (if there is a comma before "and" and there is a commad between andy of the preceding 4 words.)
    /// </summary>
    public static class OxfordComma
    {
        public static string AddOxfordComma(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            string result = string.Empty;

            _app.ActiveDocument.Select();
            var sentences = _app.ActiveDocument.Sentences;
            for (var i = 1; i <= sentences.Count; i++)
            {
                var words = sentences[i].Words;

                for (var j = 7; j <= words.Count; j++)
                {
                    string preceedingWords = words[j - 6]?.Text + words[j - 5]?.Text + words[j - 4]?.Text + words[j - 3]?.Text + words[j - 2]?.Text + words[j - 1].Text + words[j].Text;
                    var chars = preceedingWords.Split();
                    int commaCount = chars.Where(n => n.Contains(",")).Count();

                    if (words[j].Text.Trim() == "and" && words[j - 1].Text.Trim() != "," && commaCount >= 1)
                    {
                        words[j - 1].Text = words[j - 1].Text.Trim() + ", ";
                    }

                    if (words[j].Text.Trim() == "or" && words[j - 1].Text.Trim() != "," && commaCount >= 1)
                    {
                        words[j - 1].Text = words[j - 1].Text.Trim() + ", ";
                    }
                }
                result = sentences[i].Text;
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            return result;           
        }
        public static string RemoveOxfordComma(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            string result = string.Empty;

            _app.ActiveDocument.Select();
            var sentences = _app.ActiveDocument.Sentences;
            for (var i = 1; i <= sentences.Count; i++)
            {
                var words = sentences[i].Words;

                for (var j = 7; j <= words.Count; j++)
                {
                    string preceedingWords = words[j - 6]?.Text + words[j - 5]?.Text + words[j - 4]?.Text + words[j - 3]?.Text + words[j - 2]?.Text + words[j - 1].Text + words[j].Text;
                    var chars = preceedingWords.Split();
                    int commaCount = chars.Where(n => n.Contains(",")).Count();

                    if (words[j].Text.Trim() == "and" && words[j - 1].Text.Trim() == "," && commaCount >= 1)
                    {
                        words[j - 1].Text = words[j - 1].Text.Substring(1);
                    }

                    if (words[j].Text.Trim() == "or" && words[j - 1].Text.Trim() == "," && commaCount >= 1)
                    {
                        words[j - 1].Text = words[j - 1].Text.Substring(1);
                    }
                }
                result = sentences[i].Text;
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            return result;
        }

        //https://regex101.com/
        static Regex hasOxfordMatch = new Regex(@"\w+, \w+, and ");
        static Regex noOxfordMatch = new Regex(@"\w+, \w+ and ");
        static Regex noOxfordMatchDeep = new Regex(@"((?:[\w'-]+,\s+)+(?!which|when|where|who|whom|that|whether|if)(?:[\w'-]+\s){0,2}[\w'-]+)(\s+(?:and|or)\s+[\w'-]+)");
        static Regex hasOxfordMatchDeep = new Regex(@"((?:[\w'-]+,\s+)+(?!which|when|where|who|whom|that|whether|if)(?:[\w'-]+\s){0,2}[\w'-]+,)(\s+(?:and|or)\s+[\w'-]+)");
    }
}

using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;


namespace Tools.Simple
{
    /// <summary>
    /// Replaces the space with a non-breaking space after common abbreviations (Mr., Mrs., Dr., $, etc.)
    /// </summary>
    public class InsertNBS
    {
        static readonly string nbs = "\u00A0";
        public static void Insert(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            InsertSpaceAfterText(rng);
            InsertSpaceBeforeText(rng);
            FixLawyerEllipses(rng);

            if (_app.ActiveDocument.Footnotes.Count > 0)
            {
                foreach (Footnote footnote in _app.ActiveDocument.Footnotes)
                {
                    rng = footnote.Range;
                    InsertSpaceAfterText(rng);
                    InsertSpaceBeforeText(rng);
                    FixLawyerEllipses(rng);
                }
            }

            if (_app.ActiveDocument.Endnotes.Count >0)
            {
                foreach(Endnote endnote in _app.ActiveDocument.Endnotes)
                {
                    rng = endnote.Range;
                    InsertSpaceAfterText(rng);
                    InsertSpaceBeforeText(rng);
                    FixLawyerEllipses(rng);

                }
            }

                _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        private static void InsertSpaceAfterText(Range rng)
        {
            foreach (string expression in ExpressionsSpaceAfter)
            {
                string expr = expression.Substring(0, expression.Length) + nbs;
                rng.Find.Execute(FindText: " "+expression +" ", ReplaceWith: " "+expr, MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);
            }
        }

        static void InsertSpaceBeforeText(Range rng)
        {
            foreach (string expression in ExpressionsSpaceBefore)
            {
                string expr = nbs + expression;
                rng.Find.Execute(FindText: " "+ expression, ReplaceWith: expr, MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);
            }

        }

        static void FixLawyerEllipses(Range rng)
        {
            rng.Find.Execute(FindText: " . . . .", ReplaceWith: $"{nbs}.{nbs}.{nbs}.{nbs}.", MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: " . . .", ReplaceWith: $"{nbs}.{nbs}.{nbs}.", MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);
        }

        public static List<string> ExpressionsSpaceAfter = new List<string>()
        {
            "Dr.",
            "Mr.",
            "Ms.",
            "Mrs.",
            "Prof.",

            "at",

            "No.",
            "¥", "\u00A5",
            "£", "\u00A3",
            "€", "\u20AC",
            "$", "\u0024",
            "¶", "\u00B6",
            "¶¶", "\u00B6\u00B6",
            "§", "\u00A7",

            "Section",
            "Exh.",
            "Ex.",

            #region Months
            "January", "Jan", "Jan.",
            "February", "Feb", "Feb.",
            "March", "Mar", "Mar.",
            "April", "Apr", "Apr.",
            "May",
            "June",
            "July",
            "August", "Aug", "Aug.",
            "September", "Sept", "Sept.",
            "October", "Oct", "Oct",
            "November", "Nov", "Nov.",
            "December", "Dec", "Dec.",
            #endregion

        };
        public static List<string> ExpressionsSpaceBefore = new List<string>()
        {
            "million",
            "billion",
            "trillion",
        };
    }

}

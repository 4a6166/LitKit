using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;


namespace Services.RibbonButtons
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

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        private static void InsertSpaceAfterText(Range rng)
        {
            foreach (string expression in ExpressionsSpaceAfter)
            {
                string expr = expression.Substring(0, expression.Length) + nbs;
                rng.Find.Execute(FindText: expression + " ", ReplaceWith: expr, Replace: WdReplace.wdReplaceAll);
            }
        }

        static void InsertSpaceBeforeText(Range rng)
        {
            foreach (string expression in ExpressionsSpaceBefore)
            {
                string expr = nbs + expression;
                rng.Find.Execute(FindText: " " + expression, ReplaceWith: expr, Replace: WdReplace.wdReplaceAll);
            }

        }

        static void FixLawyerEllipses(Range rng)
        {
            rng.Find.Execute(FindText: " . . . .", ReplaceWith: $"{nbs}.{nbs}.{nbs}.{nbs}.", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: " . . .", ReplaceWith: $"{nbs}.{nbs}.{nbs}.", Replace: WdReplace.wdReplaceAll);
        }

        public static List<string> ExpressionsSpaceAfter = new List<string>()
        {
            "Dr.",
            "Mr.",
            "Ms.",
            "Mrs.",
            "Prof.",

            "No.",
            "¥", "\u00A5",
            "£", "\u00A3",
            "€", "\u20AC",
            "$", "\u0024",
            "¶", "\u00B6",
            "§", "\u00A7",

            "Section",
            "Exh.",
            "Ex.",

        };
        public static List<string> ExpressionsSpaceBefore = new List<string>()
        {
            "million",
            "billion",
            "trillion",
        };
    }

}

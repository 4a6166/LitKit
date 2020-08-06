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
        public static void Insert(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;
            foreach (string expression in Expressions)
            {
                string expr = expression.Substring(0, expression.Length) + "\u00A0";
                rng.Find.Execute(FindText: expression + " ", ReplaceWith: expr, Replace: WdReplace.wdReplaceAll);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        public static List<string> Expressions = new List<string>()
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

        };

    }

}

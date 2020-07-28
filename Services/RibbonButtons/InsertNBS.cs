using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string expr = expression.Substring(0, expression.Length - 1) + "\u00A0";
                rng.Find.Execute(FindText: expression, ReplaceWith: expr, Replace: WdReplace.wdReplaceAll);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        public static List<string> Expressions = new List<string>()
        {
            "Dr. ",
            "Mr. ",
            "Ms. ",
            "Mrs. ",
            "Prof. ",
            "$ ",
            "No. ",
            "¶ ",
            "§ "

        };

    }

}

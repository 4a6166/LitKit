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
    /// Adds or removes Oxford Commas (if there is a comma before "and" and there is a commad between andy of the preceding 4 words.)
    /// </summary>
    public static class OxfordComma
    {
        public static void AddOxfordComma(Word.Application _app)
        {
            System.Windows.Forms.MessageBox.Show("Test");

        }
        public static void RemoveOxfordComma(Word.Application _app)
        {
            System.Windows.Forms.MessageBox.Show("Test");

            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            string findText = "";
            string replaceText = "";

            rng.Find.Execute(FindText: findText, ReplaceWith: replaceText, Replace: WdReplace.wdReplaceAll);

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }
    }
}

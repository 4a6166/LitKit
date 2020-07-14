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
    /// Adds or removes double space between sentences
    /// </summary>
    public static class SpaceBetweenSentences
    {
        public static void AddSpace(Word.Application _app)
        {

            _app.Application.System.Cursor = WdCursorType.wdCursorWait;


            for (int i=1; i<= _app.ActiveDocument.Sentences.Count; i++)
            {
                _app.ActiveDocument.Sentences[i].Select();
                var rng = _app.Selection;
                //rng.Find.Execute(FindText: ". ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
                //rng.Find.Execute(FindText: ".   ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
                if (rng.Text.Substring(rng.Text.Length - 3) != ".  ")
                {
                    rng.Text = rng.Text + " ";
                }
            }
            

            //TODO: figure out how to exempt abbreviations

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;

        }
        public static void RemoveSpace(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            rng.Find.Execute(FindText: ".  ", ReplaceWith: ". ", Replace: WdReplace.wdReplaceAll);

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }
    }
}

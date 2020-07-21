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


            //for (int i=1; i<= _app.ActiveDocument.Sentences.Count; i++)
            //{
            //    _app.ActiveDocument.Sentences[i].Select();
            //    var rng = _app.Selection;
            //    //rng.Find.Execute(FindText: ". ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
            //    //rng.Find.Execute(FindText: ".   ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
            //    if (rng.Text.Substring(rng.Text.Length - 3) != ".  ")
            //    {
            //        rng.Text = rng.Text + " ";
            //    }
            //}


            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            rng.Find.Execute(FindText: ". ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: ".   ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);

            #region abbreviations
            rng.Find.Execute(FindText: "Mr.  ", ReplaceWith: "Mr. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "Mrs.  ", ReplaceWith: "Mrs. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "Ms.  ", ReplaceWith: "Ms. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "Dr.  ", ReplaceWith: "Dr. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "Jr.  ", ReplaceWith: "Jr. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "Sr.  ", ReplaceWith: "Sr. ", Replace: WdReplace.wdReplaceAll);

            rng.Find.Execute(FindText: "i.e.  ", ReplaceWith: "i.e. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "e.g.  ", ReplaceWith: "e.g. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "etc.  ", ReplaceWith: "etc. ", Replace: WdReplace.wdReplaceAll);


            rng.Find.Execute(FindText: "St.  ", ReplaceWith: "St. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "D.C.  ", ReplaceWith: "D.C. ", Replace: WdReplace.wdReplaceAll);

            rng.Find.Execute(FindText: "a.m.  ", ReplaceWith: "a.m. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "A.M.  ", ReplaceWith: "A.M. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "p.m.  ", ReplaceWith: "p.m. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "P.M.  ", ReplaceWith: "P.M. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "hr.  ", ReplaceWith: "hr. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "sec.  ", ReplaceWith: "sec. ", Replace: WdReplace.wdReplaceAll);

            rng.Find.Execute(FindText: "oz.  ", ReplaceWith: "oz. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "in.  ", ReplaceWith: "in. ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "ft.  ", ReplaceWith: "ft. ", Replace: WdReplace.wdReplaceAll);

            #endregion

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;

        }
        public static void RemoveSpace(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            rng.Find.Execute(FindText: ".  ", ReplaceWith: ". ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: ".   ", ReplaceWith: ". ", Replace: WdReplace.wdReplaceAll);

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }
    }
}

using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Tools.Simple
{
    /// <summary>
    /// Adds or removes double space between sentences
    /// </summary>
    public static class SpaceBetweenSentences
    {
        public static void AddSpace(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;


            //for (int i = 1; i <= _app.ActiveDocument.Sentences.Count; i++)
            //{
            //    var sentence = _app.ActiveDocument.Sentences[i];
            //    if (sentence.Text.Contains('.'))
            //    {
            //        sentence.Text = sentence.Text + " ";
            //    }
            //    if (sentence.Text.Contains(".   "))
            //    {
            //        sentence.Text = sentence.Text.Substring(0, sentence.Text.Length - 1);
            //    }
            //}


            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;
            rng.Find.Execute(FindText: ". ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: ".   ", ReplaceWith: ".  ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "? ", ReplaceWith: "?  ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "! ", ReplaceWith: "!  ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "?   ", ReplaceWith: "?  ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "!   ", ReplaceWith: "!  ", Replace: WdReplace.wdReplaceAll);

            foreach(var text in abbreviations)
            {
                rng.Find.Execute(FindText: " "+text + "  ", ReplaceWith: " "+text + " ", Replace: WdReplace.wdReplaceAll);
            }

            for (int i=1; i<=9; i++)
            {
                rng.Find.Execute(FindText: $"No.  {i}", ReplaceWith: $"No. {i}", Replace: WdReplace.wdReplaceAll);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;

        }
        public static void RemoveSpace(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            rng.Find.Execute(FindText: ".  ", ReplaceWith: ". ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: ".   ", ReplaceWith: ". ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "?  ", ReplaceWith: "? ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "!  ", ReplaceWith: "! ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "?   ", ReplaceWith: "? ", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "!   ", ReplaceWith: "! ", Replace: WdReplace.wdReplaceAll);

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        private static readonly List<string> abbreviations = new List<string>()
        {
            #region name prefix/suffix
            "Mr.",
            "Mrs.",
            "Ms.",
            "Dr.",
            "Jr.",
            "Sr.",
#endregion

            "i.e.",
            "e.g.",
            "etc.",
            "No.",
            "ex.",

            "St.",
            "Ave.",
            "Rd.",
            "D.C.",
            "U.S.",
            "U.S.A.",

            #region time
            "a.m.",
            "A.M.",
            "p.m.",
            "P.M.",
            "hr.",
            "sec.",
            #endregion

            #region distance
            "oz.",
            "in.",
            "ft.",
#endregion

            #region districts
            "E.D.",
            "W.D.",
            "M.D.",
            "S.D.",
            "N.D.",
            "D.",
            "Ala.",
            "Ark.",
            "Ariz.",
            "Cal.",
            "Colo.",
            "Conn.",
            "Del.",
            "Fla.",
            "Ga.",
            "Haw.",
            "Ill.",
            "Ind.",
            "Ky.",
            "La.",
            "Me.",
            "Md.",
            "Mass.",
            "Mich.",
            "Minn.",
            "Miss.",
            "Mo.",
            "Neb.",
            "Nev.",
            "N.H.",
            "N.J.",
            "N.M.",
            "N.Y.",
            "N.C.",
            "N.D.",
            "Okla.",
            "Or.",
            "Pa.",
            "R.I.",
            "S.C.",
            "Tenn.",
            "Tex.",
            "Va.",
            "Wash.",
            "Wisc.",
            "Wyo.",
#endregion

            #region months
            "Jan.",
            "Feb.",
            "Mar.",
            "Apr.",
            "Aug.",
            "Sept.",
            "Oct.",
            "Nov.",
            "Dec.",
#endregion

            "U.S.C.",
            "C.F.R.",
            "A.B.A.",
            "Fed.",
            "App.",
            "Supp.",
            "F.",
            "Ex.",
            "Exh.",
            "Pl.",
            "Def.",
            "Defs.",
            "Pls.",
            "Br.",
            "Resp.",
            "Opp.",
            "Mot.",

            "Co.",
            "L.L.C.",
            "L.L.P.",
            "etc.",
            "Inc.",
            "P.C.",

            #region letters
            "A.",
            "B.",
            "C.",
            "D.",
            "E.",
            "F.",
            "G.",
            "H.",
            "I.",
            "J.",
            "K.",
            "L.",
            "M.",
            "N.",
            "O.",
            "P.",
            "Q.",
            "R.",
            "S.",
            "T.",
            "U.",
            "V.",
            "X.",
            "Y.",
            "Z.",
#endregion
        };
    }
}

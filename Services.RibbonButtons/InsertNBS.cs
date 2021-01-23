using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;


namespace Tools.Simple
{
    /// <summary>
    /// Replaces the space with a non-breaking space after common abbreviations (Mr., Mrs., Dr., $, etc.)
    /// </summary>
    public class InsertNBS
    {
        static readonly string WordRegexNot = @")[!^s0-a,.;:'?\!\)\]" + "\""+"]"; //This is Word regex to remove non-breaking space, comma, period, etc.

        static readonly string nbs = "\u00A0";
        public static bool Insert(Word.Application _app)
        {
            DialogResult mb = DialogResult.Yes;
            if (_app.ActiveDocument.TrackRevisions == true && _app.ActiveDocument.Revisions.Count > 0)
            {
                mb = MessageBox.Show("This action requires that track changes be off. Do you want to accept any currently tracked changes now?.", "Accept Tracked Changes", MessageBoxButtons.YesNo);
            }
            if (mb == DialogResult.Yes)
            {
                bool result = false;
                _app.Application.System.Cursor = WdCursorType.wdCursorWait;

                try
                {
                    _app.ActiveDocument.Select();
                    _app.ActiveDocument.AcceptAllRevisions();
                    _app.ActiveDocument.TrackRevisions = false;

                    var rng = _app.Selection.Range;

                    _app.Selection.Find.Execute(FindText: " ", ReplaceWith: " "); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                    foreach (Range story in _app.ActiveDocument.StoryRanges)
                    {
                        InsertSpaceAfterText(story);
                        InsertSpaceBeforeText(story);
                        InsertSpaceInsideAt(story);
                        FixLawyerEllipses(story);
                    }
                }
                catch { }

                _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
                return result;
            }
            else return false;
        }

        private static void InsertSpaceAfterText(Range rng)
        {
            foreach (string expression in ExpressionsSpaceAfter)
            {
                //string expr = expression.Substring(0, expression.Length) + nbs;
                rng.Find.MatchCase = true;
                rng.Find.MatchWholeWord = true;
                rng.Find.MatchWildcards = true;
                rng.Find.Forward = true;
                rng.Find.Text = "( " + expression + WordRegexNot; 
                rng.Find.Replacement.Text = @"\1^s";

                rng.Find.Replacement.ClearFormatting(); //prevents "at" from getting italicized in pincites. Test auto replacement.

                rng.Find.Execute(Replace: WdReplace.wdReplaceAll);


                // Same find/Replace but with all caps
                rng.Find.MatchCase = true;
                rng.Find.MatchWholeWord = true;
                rng.Find.MatchWildcards = true;
                rng.Find.Forward = true;
                rng.Find.Text = "( " + expression.ToUpper() + WordRegexNot;
                rng.Find.Replacement.Text = @"\1^s";

                rng.Find.Replacement.ClearFormatting(); //prevents "at" from getting italicized in pincites. Test auto replacement.

                rng.Find.Execute(Replace: WdReplace.wdReplaceAll);

            }
        }

        static void InsertSpaceBeforeText(Range rng)
        {
            foreach (string expression in ExpressionsSpaceBefore)
            {
                string expr = nbs + expression;
                rng.Find.MatchCase = true;
                rng.Find.MatchWholeWord = true;
                rng.Find.MatchWildcards = true;
                rng.Find.Text = " " + expression;
                rng.Find.Replacement.Text = expr;
                rng.Find.Forward = true;


                rng.Find.Execute(Replace: WdReplace.wdReplaceAll);
            }

        }

        static void InsertSpaceInsideAt(Range rng)
        {
            for (int i = 1; i<= 9; i++)
            {
                string expr1 = "at"+nbs+i.ToString();
                rng.Find.MatchCase = true;
                rng.Find.MatchWholeWord = true;
                rng.Find.MatchWildcards = true;
                rng.Find.Text = "at " + i.ToString(); ;
                rng.Find.Replacement.Text = expr1;

                rng.Find.Execute(Replace: WdReplace.wdReplaceAll);

                string expr2 = "At" + nbs + i.ToString();
                rng.Find.MatchCase = true;
                rng.Find.MatchWholeWord = true;
                rng.Find.MatchWildcards = true;
                rng.Find.Text = "At " + i.ToString(); ;
                rng.Find.Replacement.Text = expr2;

                rng.Find.Execute(Replace: WdReplace.wdReplaceAll);

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
            "Messrs.",
            "Prof.",

            "No.",
            "¥", /*"\u00A5",*/
            "£", /*"\u00A3",*/
            "€", /*"\u20AC",*/
            "$", /*"\u0024",*/
            "¶", /*"\u00B6",*/
            "¶¶", /*"\u00B6\u00B6",*/
            "§", /*"\u00A7",*/

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
            "October", "Oct", "Oct.",
            "November", "Nov", "Nov.",
            "December", "Dec", "Dec.",
            #endregion

        };
        public static List<string> ExpressionsSpaceBefore = new List<string>()
        {
            "million",
            "billion",
            "trillion",
            "– ",
        };
    }

}

using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Simple
{
    public class LineBreaks
    {
        public static bool RemoveBreaks(Selection selection)
        {
            bool result = false;

            if (selection.Text.Count() > 2)
            {
                try
                {
                    //selection.Range.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                    var find = selection.Find;
                    find.ClearFormatting();
                    find.Replacement.ClearFormatting();

                    find.Text = @"[^13^l^n^m]";
                    find.Replacement.Text = " ";
                    find.Forward = true;
                    find.Wrap = WdFindWrap.wdFindStop;
                    find.Format = false;
                    find.MatchCase = false;
                    find.MatchWholeWord = false;
                    find.MatchAllWordForms = false;
                    find.MatchSoundsLike = false;
                    
                    find.MatchWildcards = true;
                    
                    find.Execute(Replace: WdReplace.wdReplaceAll);

                    return true;
                }
                catch { return false; }
            }
            else
            {
                //selection.Application.System.Cursor = WdCursorType.wdCursorWait;
                //try
                //{
                //    selection.Range.SetRange(selection.Paragraphs.First.Range.Start, selection.Paragraphs.Last.Range.End);
                //    selection.Application.Selection.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                //    foreach (Range rng in selection.Application.ActiveDocument.StoryRanges)
                //    {
                //        rng.Find.Text = @"[^13^l^n^m]";
                //        rng.Find.Replacement.Text = @" ";
                //        rng.Find.MatchWholeWord = true;
                //        rng.Find.MatchWildcards = true;

                //        rng.Find.Execute(Replace: WdReplace.wdReplaceAll);
                //    }

                //    result = true;
                //}
                //catch { };

                //selection.Application.System.Cursor = WdCursorType.wdCursorNormal;
                return result;

            }
        }
    }
}

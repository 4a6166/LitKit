using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms = System.Windows.Forms;
using Services.Base;

namespace Tools.Simple
{
    public class HyphenToEnDash
    {
        public static bool ReplaceWithEnDash(Application _app)
        {
            bool result = false;
            TrackChanges tc = new TrackChanges();

            if (tc.AcceptTrackChanges(_app.ActiveDocument))
            {
                _app.Application.System.Cursor = WdCursorType.wdCursorWait;
                try
                {
                    _app.ActiveDocument.Select();
                    _app.Selection.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                    foreach (Range rng in _app.ActiveDocument.StoryRanges)
                    {
                        rng.Find.Text = @"([0-9)])(-)([0-9])";
                        rng.Find.Replacement.Text = @"\1^=\3";
                        rng.Find.MatchWholeWord = true;
                        rng.Find.MatchWildcards = true;

                        rng.Find.Execute(Replace: WdReplace.wdReplaceAll);
                    }

                    result = true;
                }
                catch { };

                tc.RelockCCs();
                _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            }
            return result;

        }
    }
}

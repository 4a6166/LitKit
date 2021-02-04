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
    /// Searches for all apostrophes and turn them to smart quotes or vice versa
    /// </summary>
    public class SmartQuotesAndApostrophes
    {
        public static void SetSmartQuotes(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            //_app.ActiveDocument.Select();
            //var rng = _app.Selection;

            _app.Selection.Find.Execute(FindText: " ", ReplaceWith: " ", Replace: WdReplace.wdReplaceOne); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

            foreach (Range story in _app.ActiveDocument.StoryRanges)
            {
                Replace(story, "\'");
                Replace(story, "\"");
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        private static void Replace(Range rng, string character)
        {
            rng.Find.Execute(FindText: character, ReplaceWith: character, Replace: WdReplace.wdReplaceAll);
        }

        /*
        
        '	straight single quote   '	        '	                '
        "	straight double quote	"	        "	                "
        ‘	opening single quote    alt 0145	option + ]	        &lsquo;
        ’	closing single quote    alt 0146	option + shift + ]	&rsquo;
        “	opening double quote    alt 0147	option + [          &ldquo;
        ”	closing double quote    alt 0148	option + shift + [  &rdquo;
        
         */

    }
}

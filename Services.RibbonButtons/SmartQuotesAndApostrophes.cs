﻿using Microsoft.Office.Interop.Word;
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

            _app.ActiveDocument.Select();
            var rng = _app.Selection;
            //rng.InsertAfter(" ");
            rng.Find.Execute(FindText: " ", ReplaceWith: " "); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

            rng.Find.Execute(FindText: "\"", ReplaceWith: "\"", Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "\'", ReplaceWith: "\'", Replace: WdReplace.wdReplaceAll);

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        /*
        '	straight single quote	'	'	'
        "	straight double quote	"	"	"
        ‘	opening single quote alt 0145	option + ]	&lsquo;
        ’	closing single quote alt 0146	option + shift + ]	&rsquo;
        “	opening double quote    alt 0147	option + [  &ldquo;
        ”	closing double quote    alt 0148	option + shift + [  &rdquo;
        */

    }
}

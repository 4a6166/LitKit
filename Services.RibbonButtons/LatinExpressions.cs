﻿using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;


namespace Tools.Simple
{
    /// <summary>
    /// Italicize latin expressions
    /// </summary>
    public static class LatinExpressions
    {
        public static void Italicize(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            _app.Selection.Find.Execute(FindText: " ", ReplaceWith: " "); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs


            foreach (string expression in Expressions)
            {
                rng.Find.Replacement.Font.Italic = 1;

                rng.Find.Execute(FindText: expression, ReplaceWith: expression, MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);


            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        public static void UnItalicize(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;

            _app.Selection.Find.Execute(FindText: " ", ReplaceWith: " "); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

            for (int i =1; i< Expressions.Count; i++)
            {
                rng.Find.Replacement.Font.Italic = 0;

                rng.Find.Execute(FindText:Expressions[i], ReplaceWith: Expressions[i], MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }


        public static List<string> Expressions = new List<string>()
        {
            "id.",
            "supra",
            "infra",

            "res ipsa",
            "res ipsa loquitur",
            "pro se",
            "pro bono",
            "per se",
            "prima facie",
            "a fortiori",
            "a posteriori",
            "ab initio",
            "ad hoc",
            "ad litem",
            "alter ego",
            "amici curiae",
            "amicus curiae",
            "ante",
            "arguendo",
            "caveat emptor",
            "certiorari",
            "corpus delicti",
            "corpus juris",
            "corpus juris civilis",
            "de facto",
            "de jure",
            "de novo",
            "dictum",
            "duces tecum",
            "et al.",
            "ex ante",
            "ex delicto",
            "ex post",
            "ex post facto",
            "post facto",
            "ex rel",
            "ex tunc",
            "ex nunc",
            "forum non conveniens",
            "habeas corpus",
            "in camera",
            "in forma pauperis",
            "in curia",
            "in flagrante delicto",
            "in limine",
            "in personam",
            "in re",
            "in rem",
            "in toto",
            "inter alia",
            "inter vivos",
            "ipso facto",
            "ipso jure",
            "lex loci",
            "lex scripta",
            "lis pendens",
            "malum in se",
            "malum prohibitum",
            "modus operandi",
            "nolle prosequi",
            "nolo contendere",
            "nunc pro tunc",
            "nota bene",
            "dictum",
            "obiter dictum",
            "parens patriae",
            "pari passu",
            "pendente lite",
            "per capita",
            "per curiam",
            "persona non grata",
            "post mortem",
            "prima facie",
            "pro forma",
            "pro rata",
            "pro hac vice",
            "pro tempore",
            "quantum meruit",
            "quasi",
            "qui tam",
            "quid pro quo",
            "res judicata",
            "respondeat superior",
            "scienter",
            "sine qua non",
            "stare decisis",
            "situs",
            "sua sponte",
            "sub judice",
            "sub nomine",
            "de novo",
            "veto",
            "vice versa",
            "ultra vires",
            "mens rea",

        };
    }
}

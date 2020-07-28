using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
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
    /// Italicize latin expressions
    /// </summary>
    public static class LatinExpressions
    {
        public static void Italicize(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;
            foreach (string expression in Expressions)
            {
                 rng.Find.Replacement.Font.Italic = 1;
                
                rng.Find.Execute(FindText: expression, ReplaceWith: expression, Replace: WdReplace.wdReplaceAll);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        public static void UnItalicize(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            var rng = _app.Selection.Range;
            foreach (string expression in Expressions)
            {
                rng.Find.Replacement.Font.Italic = 0;

                rng.Find.Execute(FindText: expression, ReplaceWith: expression, Replace: WdReplace.wdReplaceAll);
            }

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }


        public static List<string> Expressions = new List<string>()
        {
            "Res ipsa",
            "Res ipsa loquitur",
            "Pro se",
            "Pro bono",
            "Per se",
            "Prima facie",
            "A fortiori",
            "A posteriori",
            "Ab initio",
            "Ad hoc",
            "Ad litem",
            "Alter ego",
            "Amici Curiae",
            "Amicus Curiae",
            "Ante",
            "Arguendo",
            "Caveat emptor",
            "Certiorari",
            "Corpus Delicti",
            "Corpus Juris",
            "Corpus Juris Civilis",
            "De facto",
            "De jure",
            "De novo",
            "Dictum",
            "Duces tecum",
            "Et al.",
            "Ex ante",
            "Ex delicto",
            "Ex post",
            "Ex post facto",
            "Post facto",
            "Ex rel",
            "Ex tunc",
            "Ex nunc",
            "Forum non conveniens",
            "Habeas corpus",
            "In camera",
            "In forma pauperis",
            "In curia",
            "In flagrante delicto",
            "In limine",
            "In personam",
            "In re",
            "In rem",
            "In toto",
            "Inter alia",
            "Inter vivos",
            "Ipso facto",
            "Ipso jure",
            "Lex loci",
            "Lex scripta",
            "Lis pendens",
            "Malum in se",
            "Malum prohibitum",
            "Modus operandi",
            "nolle prosequi",
            "nolo contendere",
            "nunc pro tunc",
            "Nota bene",
            "Dictum",
            "Obiter dictum",
            "parens patriae",
            "pari passu",
            "pendente lite",
            "Per capita",
            "Per curiam",
            "persona non grata",
            "post mortem",
            "prima facie",
            "Pro forma",
            "Pro rata",
            "Pro hac vice",
            "pro tempore",
            "quantum meruit",
            "Quasi",
            "Qui tam",
            "quid pro quo",
            "res judicata",
            "respondeat superior",
            "Scienter",
            "sine qua non",
            "stare decisis",
            "Situs",
            "Sua sponte",
            "Sub judice",
            "Sub nomine",
            "De novo",
            "Veto",
            "Vice versa",
            "ultra vires",
            "Mens rea",
        };
    }
}

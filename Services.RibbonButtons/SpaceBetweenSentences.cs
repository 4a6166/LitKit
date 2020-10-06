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
            DoubleSpace(_app.Selection.Range);

            //// For testing, not going through footnotes because of how long it takes
            //if (_app.ActiveDocument.Footnotes.Count > 0)
            //{
            //    foreach (Footnote footnote in _app.ActiveDocument.Footnotes)
            //    {
            //        footnote.Range.Select();
            //        DoubleSpace(footnote.Range);
            //    }
            //}

            //if (_app.ActiveDocument.Endnotes.Count >0)
            //{
            //    foreach (Endnote endnote in _app.ActiveDocument.Endnotes)
            //    {
            //        endnote.Range.Select();
            //        DoubleSpace(endnote.Range);
            //    }
            //}

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;

        }

        private static void DoubleSpace(Range rng)
        {
            string oneSpace = " ";
            string twoSpaces = "  ";
            string threeSpaces = "   ";
            string quote = "\"";

            foreach (var symbol in Symbols)
            {
                rng.Find.Execute(FindText: symbol + oneSpace, ReplaceWith: symbol + twoSpaces, Replace: WdReplace.wdReplaceAll);
                rng.Find.Execute(FindText: symbol+ threeSpaces, ReplaceWith: symbol + twoSpaces, Replace: WdReplace.wdReplaceAll);

                rng.Find.Execute(FindText: symbol + quote + oneSpace, ReplaceWith: symbol + quote + twoSpaces, Replace: WdReplace.wdReplaceAll);
                rng.Find.Execute(FindText: symbol + quote + threeSpaces, ReplaceWith: symbol + quote + twoSpaces, Replace: WdReplace.wdReplaceAll);
            }


            foreach (var text in abbreviations)
            {
                rng.Find.Execute(FindText: " "+text + "  ", ReplaceWith: " "+text + " ", MatchCase: true, Replace: WdReplace.wdReplaceAll);
            }

            for (int i = 0; i <= 9; i++)
            {
                rng.Find.Execute(FindText: $"No.  {i}", ReplaceWith: $"No. {i}", Replace: WdReplace.wdReplaceAll);
            }

            rng.Find.Execute(FindText: "id." + twoSpaces + "at", ReplaceWith: "id." + oneSpace + "at", MatchCase: true, Replace: WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "Id." + twoSpaces + "at", ReplaceWith: "Id." + oneSpace + "at", MatchCase: true, Replace: WdReplace.wdReplaceAll);

        }

        public static void RemoveSpace(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            SingleSpace(_app.Selection.Range);

            //// For testing, not going through footnotes because of how long it takes
            //if (_app.ActiveDocument.Footnotes.Count > 0)
            //{
            //    foreach (Footnote footnote in _app.ActiveDocument.Footnotes)
            //    {
            //        footnote.Range.Select();
            //        SingleSpace(footnote.Range);
            //    }
            //}

            //if (_app.ActiveDocument.Endnotes.Count > 0)
            //{
            //    foreach (Endnote endnote in _app.ActiveDocument.Endnotes)
            //    {
            //        endnote.Range.Select();
            //        SingleSpace(endnote.Range);
            //    }
            //}

            _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
        }

        private static void SingleSpace(Range rng)
        {
            string oneSpace = " ";
            string twoSpaces = "  ";
            string threeSpaces = "   ";
            string quote = "\"";

            foreach (var symbol in Symbols)
            {
                rng.Find.Execute(FindText: symbol + twoSpaces, ReplaceWith: symbol + oneSpace, Replace: WdReplace.wdReplaceAll);
                rng.Find.Execute(FindText: symbol + threeSpaces, ReplaceWith: symbol + oneSpace, Replace: WdReplace.wdReplaceAll);

                rng.Find.Execute(FindText: symbol + quote + twoSpaces, ReplaceWith: symbol + quote + oneSpace, Replace: WdReplace.wdReplaceAll);
                rng.Find.Execute(FindText: symbol + quote + threeSpaces, ReplaceWith: symbol + quote + oneSpace, Replace: WdReplace.wdReplaceAll);
            }

        }

        private static readonly List<string> Symbols = new List<string>()
        {
            ".",
            "?",
            "!",
        };
        private static readonly List<string> abbreviations = new List<string>()
        {
            #region name prefix/suffix
            "Mr.",
            "Mrs.",
            "Ms.",
            "Dr.",
            "Jr.",
            "Sr.",
            "Messrs.",
            "Prof.",
#endregion

            "i.e.",
            "e.g.",
            "E.g.",
            "etc.",
            "No.",
            "no.",
            "ex.",
            "Ex.",

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
            "SEC.",
            "Sec.",
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
            "n.",
            #endregion

            #region Bluebook

            "Admin.",
            "admin.",
            "Adm.",
            "Agric.",
            "Alder.",
            "A.L.J.",
            "amend.",
            "Amend.",
            "Ann.",
            "art.",
            "Art.",
            "Assem.",
            "Assoc.",
            "Atl.",
            "App.",
            "app.",
            "Arb.",
            "Auth.",
            "Auto.",
            "B.A.P.",
            "Bankr.",
            "B.C.A.",
            "Bd.",
            "B.I.A.",
            "Bldg.",
            "B.P.A.I.",
            "Bros.",
            "Bus.",
            "bus.",
            "Cas.",
            "Ch.",
            "Cl.",
            "Cir.",
            "Civ.",
            "Cont.",
            "cont.",
            "Compl.",
            "Ct.",
            "Ctr.",
            "Chem.",
            "Comm.",
            "Commw.",
            "Concil.",
            "Consol.",
            "Const.",
            "cmt.",
            "Cnty.",
            "Crim.",
            "Cty.",
            "Cust.",
            "Decl.",
            "Dep.",
            "Dev.",
            "dev.",
            "Det.",
            "Dir.",
            "Distrib.",
            "Dist.",
            "Div.",
            "Dom.",
            "Econ.",
            "Ed.",
            "ed.",
            "Educ.",
            "Elec.",
            "Envtl.",
            "Equip.",
            "Exch.",
            "Err.",
            "err.",
            "Evid.",
            "Fam.",
            "fig.",
            "Fig.",
            "Fin.",
            "fin.",
            "Gen.",
            "Guar.",
            "H.R.",
            "Hosp.",
            "Hous.",
            "Imp.",
            "Indep.",
            "Indus.",
            "Inst.",
            "Ins.",
            "Inv.",
            "I.R.C.",
            "I.R.S.",
            "Jud.",
            "Juv.",
            "Legis.",
            "Liab.",
            "Ltd.",
            "ltd.",
            "Litig.",
            "litig.",
            "Mach.",
            "Maint.",
            "Mgmt.",
            "Mfr.",
            "Mfg.",
            "Mil.",
            "mil.",
            "Mkt.",
            "Mktg.",
            "Mech.",
            "Med.",
            "med.",
            "Merch.",
            "merch.",
            "Mun.",
            "Mut.",
            "Ne.",
            "NE.",
            "N.E.",
            "nn.",
            "Nw.",
            "NW.",
            "N.W.",
            "Sw.",
            "SW.",
            "S.W.",
            "Se.",
            "SE.",
            "S.E.",
            "Org.",
            "Pac.",
            "Pers.",
            "Pharm.",
            "Pres.",
            "Prob.",
            "Proc.",
            "Prod.",
            "Prop.",
            "prop.",
            "Prot.",
            "Pub.",
            "pub",
            "R.R.",
            "Ry.",
            "Rec.",
            "rec.",
            "Ref.",
            "ref.",
            "Reg.",
            "Regs.",
            "Rehab.",
            "rehab.",
            "Rep.",
            "rep.",
            "Reprod.",
            "Res.",
            "Ret.",
            "Rev.",
            "Rptr.",
            "Sav.",
            "Sch.",
            "Sci.",
            "Sen.",
            "Serv.",
            "Sess.",
            "So.",
            "so.",
            "Soc.",
            "Spec.",
            "spec.",
            "Stat.",
            "stat.",
            "Subcomm.",
            "Sup.",
            "Super.",
            "super.",
            "Sur.",
            "Sys.",
            "tbl.",
            "Tbl.",
            "Tech.",
            "tech.",
            "Telecomm.",
            "telecomm.",
            "Tel.",
            "Temp.",
            "tit.",
            "Twp.",
            "Transcon.",
            "Transp.",
            "Tr.",
            "Tpk.",
            "U.C.C.",
            "Unemp.",
            "Unif.",
            "Univ.",
            "Util.",
            "Veh.",
            "Vill.",
            "Vol.",


#endregion


            "U.S.C.",
            "C.F.R.",
            "A.B.A.",
            "Fed.",
            "App.",
            "Supp.",
            "Exh.",
            "exh.",
            "Pl.",
            "Def.",
            "Defs.",
            "Pls.",
            "Br.",
            "Resp.",
            "Opp.",
            "Mot.",

            "Co.",
            "co.,",
            "L.L.C.",
            "L.L.P.",
            "etc.",
            "Inc.",
            "P.C.",

        };
    }
}

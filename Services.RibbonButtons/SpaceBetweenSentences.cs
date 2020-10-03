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

            if (_app.ActiveDocument.Footnotes.Count > 0)
            {
                foreach (Footnote footnote in _app.ActiveDocument.Footnotes)
                {
                    footnote.Range.Select();
                    DoubleSpace(footnote.Range);
                }
            }

            if (_app.ActiveDocument.Endnotes.Count >0)
            {
                foreach (Endnote endnote in _app.ActiveDocument.Endnotes)
                {
                    endnote.Range.Select();
                    DoubleSpace(endnote.Range);
                }
            }

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
                rng.Find.Execute(FindText: " "+text + "  ", ReplaceWith: " "+text + " ", MatchWholeWord: true, Replace: WdReplace.wdReplaceAll);
            }

            for (int i = 0; i <= 9; i++)
            {
                rng.Find.Execute(FindText: $"No.  {i}", ReplaceWith: $"No. {i}", Replace: WdReplace.wdReplaceAll);
            }

            rng.Find.Execute(FindText: "id." + twoSpaces + "at", ReplaceWith: "id." + oneSpace + "at", Replace: WdReplace.wdReplaceAll);
        }

        public static void RemoveSpace(Word.Application _app)
        {
            _app.Application.System.Cursor = WdCursorType.wdCursorWait;

            _app.ActiveDocument.Select();
            SingleSpace(_app.Selection.Range);

            if (_app.ActiveDocument.Footnotes.Count > 0)
            {
                foreach (Footnote footnote in _app.ActiveDocument.Footnotes)
                {
                    footnote.Range.Select();
                    SingleSpace(footnote.Range);
                }
            }

            if (_app.ActiveDocument.Endnotes.Count > 0)
            {
                foreach (Endnote endnote in _app.ActiveDocument.Endnotes)
                {
                    endnote.Range.Select();
                    SingleSpace(endnote.Range);
                }
            }

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

            #region Bluebook

            "Admin.",
            "Adm.",
            "Agric.",
            "Alder.",
            "A.L.J.",
            "amend.",
            "Ann.",
            "art.",
            "Assem.",
            "Assoc.",
            "Atl.",
            "App.",
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
            "Cas.",
            "Ch.",
            "Cl.",
            "Cir.",
            "Civ.",
            "Cont.",
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
            "Dev.",
            "Det.",
            "Dir.",
            "Distrib.",
            "Dist.",
            "Div.",
            "Dom.",
            "Econ.",
            "Ed.",
            "Educ.",
            "Elec.",
            "Envtl.",
            "Equip.",
            "Exch.",
            "Err.",
            "Evid.",
            "Fam.",
            "fig.",
            "Fin.",
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
            "Litig.",
            "Mach.",
            "Maint.",
            "Mgmt.",
            "Mfr.",
            "Mfg.",
            "Mil.",
            "Mkt.",
            "Mktg.",
            "Mech.",
            "Med.",
            "Merch.",
            "Mun.",
            "Mut.",
            "Ne.",
            "nn.",
            "Nw.",
            "Sw.",
            "Se.",
            "Org.",
            "Pac.",
            "Pers.",
            "Pharm.",
            "Pres.",
            "Prob.",
            "Proc.",
            "Prod.",
            "Prop.",
            "Prot.",
            "Pub.",
            "R.R.",
            "Ry.",
            "Rec.",
            "Ref.",
            "Reg.",
            "Regs.",
            "Rehab.",
            "Rep.",
            "Reprod.",
            "Res.",
            "Rest.",
            "Ret.",
            "Rev.",
            "Rptr.",
            "Sav.",
            "Sch.",
            "Sci.",
            "Sec.",
            "Sen.",
            "Serv.",
            "Sess.",
            "So.",
            "Soc.",
            "Spec.",
            "Stat.",
            "Subcomm",
            "Sup.",
            "Super.",
            "Sur.",
            "Sys.",
            "tbl.",
            "Tech.",
            "Telecomm.",
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

        };
    }
}

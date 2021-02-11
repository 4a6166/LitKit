using Microsoft.Office.Interop.Word;
using OpenXmlPowerTools;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace Tools.Simple
{
    /// <summary>
    /// Adds or removes double space between sentences
    /// </summary>
    public class SpaceBetweenSentences
    {
        private List<string> abbreviations = new List<string>();
        private bool DictionaryLoaded = false;
        private string filename = @"SentenceSpacingDict.dic";
        private bool _pulledStandardDict;
        public bool pulledStandardDict { get { return _pulledStandardDict; } }

        public SpaceBetweenSentences()
        {
            DictionaryLoaded = ExpressionsRepository.ReadRepository(Dicts.GetExpressionFilePath(filename, out _pulledStandardDict), abbreviations);
        }

        public bool UpdateAbbreviationsFile(string AbbreviationsList)
        {
            return Dicts.UpdatePersonalDict(filename, AbbreviationsList, pulledStandardDict);

        }

        public void AddSpace(Word.Application _app)
        {
            TrackChanges tc = new TrackChanges();
            if (tc.AcceptTrackChanges(_app.ActiveDocument))
            {
                _app.Application.System.Cursor = WdCursorType.wdCursorWait;

                var layoutType = _app.ActiveWindow.View.Type;

                Regex regex = SentenceSpacingRegex();

                string exceptions = "";
                //// For some reason, iterating through all the stories does not work but the wdMainTextStory catches footnotes and endnotes? Iterates through all the Story Ranges(header, footer, footnotes, end notes, etc. if they are present in the document.
                //foreach (Range story in _app.ActiveDocument.StoryRanges)
                //{
                //    try
                //    {
                //        DoubleSpace(story, regex);
                //    }
                //    catch (Exception e)
                //    {
                //        exceptions += Environment.NewLine + story.StoryType;
                //    }
                //}

                DoubleSpace(_app.ActiveDocument.StoryRanges[WdStoryType.wdMainTextStory], regex);

                tc.RelockCCs();

                _app.ActiveWindow.View.Type = layoutType;

                _app.Application.System.Cursor = WdCursorType.wdCursorNormal;

                if(exceptions.Length >1)
                {
                    MessageBox.Show("LitKit was unable to affect the following Document Parts: " + exceptions);
                }
            }
        }

        public void DoubleSpace(Range range, Regex regex) 
        {

            XElement document = XElement.Parse(range.WordOpenXML);
            var content = document.Descendants(W.p);

            OpenXmlRegex.Replace(content, regex, "$& ", null, trackRevisions: true, author: "Prelimine LitKit");

            range.InsertXML(document.ToString());
        }

        private Regex SentenceSpacingRegex()
        {
            string regString = ")(\\!|\\?|\\.)[\"”’]*( )(?! |\\.)";

            if (abbreviations.Count != 0)
            {
                for (int s = 0; s < abbreviations.Count; s++)
                {
                    var sReplaced = "";
                    if (abbreviations[s].Contains("."))
                    {
                        sReplaced = abbreviations[s].Replace(".", @"\.");
                    }
                    else sReplaced = abbreviations[s];

                    if (sReplaced.EndsWith(@"\."))
                    {
                        regString = "|\\b" + sReplaced.Substring(0, sReplaced.Length - 2) + regString;
                    }
                    else regString = "|\\b" + sReplaced + regString;
                }
                regString = "(?<!" + regString.Substring(1);

                Regex regex = new Regex(regString);
                //(?<!\bMr|\bU.S)[\?\!\.]["”’]*( )(?! |\.)
                return regex;
            }
            else
            {
                //if dictionary is empty, just affect every ". "
                return new Regex("(\\!|\\?|\\.)[\"”’]*( )(?! |\\.)");
            }
        }

        public void RemoveSpace(Word.Application _app)
        {
            DialogResult mb = DialogResult.Yes;
            if (_app.ActiveDocument.TrackRevisions == true && _app.ActiveDocument.Revisions.Count > 0)
            {
                mb = MessageBox.Show("This action requires that track changes be off. Do you want to accept any currently tracked changes now?.", "Accept Tracked Changes", MessageBoxButtons.YesNo);
            }
            if (mb == DialogResult.Yes)
            {

                _app.ActiveDocument.Select();
                try
                {
                    _app.ActiveDocument.AcceptAllRevisions();
                }catch { }
                _app.ActiveDocument.TrackRevisions = false;

                _app.Selection.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                _app.Application.System.Cursor = WdCursorType.wdCursorWait;
                var layoutType = _app.ActiveWindow.View.Type;

                foreach (Range story in _app.ActiveDocument.StoryRanges)
                {
                    SingleSpace(story);
                    
                }
                _app.ActiveWindow.View.Type = layoutType;

                _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
            }
        }

        private void SingleSpace(Range range)
        {
            var find = range.Find;
            find.ClearFormatting();
            find.Replacement.ClearFormatting();
            find.MatchWildcards = true;

            find.Text = @"[ ]{2,3}";
            find.Replacement.Text = @" ";
            find.Execute(Replace: WdReplace.wdReplaceAll);
        }


//        private static readonly List<string> abbreviations = new List<string>()
//        {
//            #region name prefix/suffix
//            "Mr.",
//            "Mrs.",
//            "Ms.",
//            "Dr.",
//            "Jr.",
//            "Sr.",
//            "Messrs.",
//            "Prof.",
//#endregion

//            "i.e.",
//            "e.g.",
//            "E.g.",
//            "etc.",
//            "No.",
//            "no.",
//            "ex.",
//            "Ex.",
//            "v.",
//            "id.",
//            "Id.",
//            "Dkt.",

//            "St.",
//            "Ave.",
//            "Rd.",
//            "D.C.",
//            "U.S.",
//            "U.S.A.",

//            #region time
//            "a.m.",
//            "A.M.",
//            "p.m.",
//            "P.M.",
//            "hr.",
//            "sec.",
//            "SEC.",
//            "Sec.",
//            #endregion

//            #region distance
//            "oz.",
//            "in.",
//            "ft.",
//#endregion

//            #region districts
//            "E.D.",
//            "W.D.",
//            "M.D.",
//            "S.D.",
//            "N.D.",
//            "D.",
//            "Ala.",
//            "Ark.",
//            "Ariz.",
//            "Cal.",
//            "Colo.",
//            "Conn.",
//            "Del.",
//            "Fla.",
//            "Ga.",
//            "Haw.",
//            "Ill.",
//            "Ind.",
//            "Ky.",
//            "La.",
//            "Me.",
//            "Md.",
//            "Mass.",
//            "Mich.",
//            "Minn.",
//            "Miss.",
//            "Mo.",
//            "Neb.",
//            "Nev.",
//            "N.H.",
//            "N.J.",
//            "N.M.",
//            "N.Y.",
//            "N.C.",
//            "N.D.",
//            "Okla.",
//            "Or.",
//            "Pa.",
//            "R.I.",
//            "S.C.",
//            "Tenn.",
//            "Tex.",
//            "Va.",
//            "Wash.",
//            "Wisc.",
//            "Wyo.",
//#endregion

//            #region months
//            "Jan.",
//            "Feb.",
//            "Mar.",
//            "Apr.",
//            "Aug.",
//            "Sept.",
//            "Oct.",
//            "Nov.",
//            "Dec.",
//#endregion

//            #region letters
//            "A.",
//            "B.",
//            "C.",
//            "D.",
//            "E.",
//            "F.",
//            "G.",
//            "H.",
//            "I.",
//            "J.",
//            "K.",
//            "L.",
//            "M.",
//            "N.",
//            "O.",
//            "P.",
//            "Q.",
//            "R.",
//            "S.",
//            "T.",
//            "U.",
//            "V.",
//            "X.",
//            "Y.",
//            "Z.",
//            "n.",
//            #endregion

//            #region Bluebook

//            "Admin.",
//            "admin.",
//            "Adm.",
//            "Agric.",
//            "Alder.",
//            "A.L.J.",
//            "amend.",
//            "Amend.",
//            "Ann.",
//            "art.",
//            "Art.",
//            "Assem.",
//            "Assoc.",
//            "Atl.",
//            "App.",
//            "app.",
//            "Arb.",
//            "Auth.",
//            "Auto.",
//            "B.A.P.",
//            "Bankr.",
//            "B.C.A.",
//            "Bd.",
//            "B.I.A.",
//            "Bldg.",
//            "B.P.A.I.",
//            "Bros.",
//            "Bus.",
//            "bus.",
//            "Cas.",
//            "Ch.",
//            "Cl.",
//            "Cir.",
//            "Civ.",
//            "Cont.",
//            "cont.",
//            "Compl.",
//            "Ct.",
//            "Ctr.",
//            "Chem.",
//            "Comm.",
//            "Commw.",
//            "Concil.",
//            "Consol.",
//            "Const.",
//            "cmt.",
//            "Cnty.",
//            "Crim.",
//            "Cty.",
//            "Cust.",
//            "Decl.",
//            "Dep.",
//            "Dev.",
//            "dev.",
//            "Det.",
//            "Dir.",
//            "Distrib.",
//            "Dist.",
//            "Div.",
//            "Dom.",
//            "Econ.",
//            "Ed.",
//            "ed.",
//            "Educ.",
//            "Elec.",
//            "Envtl.",
//            "Equip.",
//            "Exch.",
//            "Err.",
//            "err.",
//            "Evid.",
//            "Fam.",
//            "fig.",
//            "Fig.",
//            "Fin.",
//            "fin.",
//            "Gen.",
//            "Guar.",
//            "H.R.",
//            "Hosp.",
//            "Hous.",
//            "Imp.",
//            "Indep.",
//            "Indus.",
//            "Inst.",
//            "Ins.",
//            "Inv.",
//            "I.R.C.",
//            "I.R.S.",
//            "Jud.",
//            "Juv.",
//            "Legis.",
//            "Liab.",
//            "Ltd.",
//            "ltd.",
//            "Litig.",
//            "litig.",
//            "Mach.",
//            "Maint.",
//            "Mgmt.",
//            "Mfr.",
//            "Mfg.",
//            "Mil.",
//            "mil.",
//            "Mkt.",
//            "Mktg.",
//            "Mech.",
//            "Med.",
//            "med.",
//            "Merch.",
//            "merch.",
//            "Mun.",
//            "Mut.",
//            "Ne.",
//            "NE.",
//            "N.E.",
//            "nn.",
//            "Nw.",
//            "NW.",
//            "N.W.",
//            "Sw.",
//            "SW.",
//            "S.W.",
//            "Se.",
//            "SE.",
//            "S.E.",
//            "Org.",
//            "Pac.",
//            "Pers.",
//            "Pharm.",
//            "Pres.",
//            "Prob.",
//            "Proc.",
//            "Prod.",
//            "Prop.",
//            "prop.",
//            "Prot.",
//            "Pub.",
//            "pub",
//            "R.R.",
//            "Ry.",
//            "Rec.",
//            "rec.",
//            "Ref.",
//            "ref.",
//            "Reg.",
//            "Regs.",
//            "Rehab.",
//            "rehab.",
//            "Rep.",
//            "rep.",
//            "Reprod.",
//            "Res.",
//            "Ret.",
//            "Rev.",
//            "Rptr.",
//            "Sav.",
//            "Sch.",
//            "Sci.",
//            "Sen.",
//            "Serv.",
//            "Sess.",
//            "So.",
//            "so.",
//            "Soc.",
//            "Spec.",
//            "spec.",
//            "Stat.",
//            "stat.",
//            "Subcomm.",
//            "Sup.",
//            "Super.",
//            "super.",
//            "Sur.",
//            "Sys.",
//            "tbl.",
//            "Tbl.",
//            "Tech.",
//            "tech.",
//            "Telecomm.",
//            "telecomm.",
//            "Tel.",
//            "Temp.",
//            "tit.",
//            "Twp.",
//            "Transcon.",
//            "Transp.",
//            "Tr.",
//            "Tpk.",
//            "U.C.C.",
//            "Unemp.",
//            "Unif.",
//            "Univ.",
//            "Util.",
//            "Veh.",
//            "Vill.",
//            "Vol.",


//#endregion


//            "U.S.C.",
//            "C.F.R.",
//            "A.B.A.",
//            "Fed.",
//            "App.",
//            "Supp.",
//            "Exh.",
//            "exh.",
//            "Pl.",
//            "Def.",
//            "Defs.",
//            "Pls.",
//            "Br.",
//            "Resp.",
//            "Opp.",
//            "Mot.",

//            "Co.",
//            "co.,",
//            "L.L.C.",
//            "L.L.P.",
//            "etc.",
//            "Inc.",
//            "P.C.",

//        };
    }
}

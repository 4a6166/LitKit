using Microsoft.Office.Interop.Word;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                }
                catch { }
                _app.ActiveDocument.TrackRevisions = false;

                _app.Selection.Find.Execute(FindText: @"(?)", ReplaceWith: @"\1", MatchWildcards: true); // Something needs to be replaced first or Word 2019/365 closes automatically (exit condition 0) when Replace: WdReplace.wdReplaceAll runs

                int sentenceCount = 0;
                foreach (Range rng in _app.ActiveDocument.StoryRanges)
                {
                    sentenceCount += rng.Sentences.Count;
                }
                var warning = System.Windows.Forms.MessageBox.Show($"Sentences found: {sentenceCount}" + Environment.NewLine + "This may take a while. Do you want to proceed?", "Two Spaces Between Sentences", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (warning == DialogResult.OK)
                {
                    _app.Application.System.Cursor = WdCursorType.wdCursorWait;

                    var layoutType = _app.ActiveWindow.View.Type;

                    // Iterates through all the Story Ranges (header, footer, footnotes, end notes, etc. if they are present in the document.
                    foreach (Range story in _app.ActiveDocument.StoryRanges)
                    {
                        DoubleSpace(story);
                    }
                    _app.ActiveWindow.View.Type = layoutType;

                    _app.Application.System.Cursor = WdCursorType.wdCursorNormal;
                }
            } 
        }

        public void DoubleSpace(Range range) //TODO: Still does not correctly add spaces to sentences that contain a content control. Works fine with sentences where the ". " is followed by a CC.
        {
            string regString = ")(\\!|\\?|\\.)\"*( )(?! |\\.)";

            string leadingBoundariers = @"[\b\(\[\\{'" + "\\\"]";

            foreach (string s in abbreviations)
            {
                var sReplaced = "";
                if (s.Contains("."))
                {
                    sReplaced = s.Replace(".", @"\.");
                }
                else sReplaced = s;

                if (sReplaced.EndsWith(@"\."))
                {
                    regString = "|"+leadingBoundariers + sReplaced.Substring(0, sReplaced.Length - 2) + regString;
                }
                else regString = "|" + sReplaced + regString;
            }
            regString = @"(?<!" + regString.Substring(1);
            Regex regex = new Regex(regString);
            //@"(?<!Mr|Mrs)(\!|\?|\.)( )(?! )");

            foreach (Paragraph paragraph in range.Paragraphs)
            {

                if (paragraph.Range.ContentControls.Count < 1)
                {

                    var location = paragraph.Range;
                    //_app.ActiveDocument.StoryRanges[WdStoryType.wdMainTextStory];

                    var matches = regex.Matches(location.Text);

                    List<int> indexList = new List<int>();
                    int indexShift = 0;
                    for (int i = 0; i < matches.Count; i++)
                    {
                        var matchStart = matches[i].Groups[2].Index;

                        indexList.Add(matchStart);
                    }

                    for (int i = 0; i < indexList.Count; i++)
                    {
                        Range replaceRange = range.Document.Range(paragraph.Range.Start + indexList[i] + indexShift, paragraph.Range.Start + indexList[i] + indexShift);
                        //_app.ActiveDocument.Range(indexList[i] + indexShift, indexList[i] + indexShift);
                        //range.Move(WdUnits.wdCharacter, indexList[i] + indexShift);
                        replaceRange.InsertAfter(" ");
                        indexShift++;
                    }
                }
                else
                {
                    for (int i = 1; i <= paragraph.Range.Sentences.Count; i++)
                    {
                        var sentence = paragraph.Range.Sentences[i];

                        if (sentence.ContentControls.Count < 1)
                        {


                            var location = paragraph.Range.Sentences[i];
                            //_app.ActiveDocument.StoryRanges[WdStoryType.wdMainTextStory];

                            var matches = regex.Matches(location.Text);

                            List<int> indexList = new List<int>();
                            int indexShift = 0;
                            for (int j = 0; j < matches.Count; j++)
                            {
                                var matchStart = matches[j].Groups[2].Index;

                                indexList.Add(matchStart);
                            }

                            for (int j = 0; j < indexList.Count; j++)
                            {
                                int rangeStart = sentence.Start + indexList[j] + indexShift;
                                Range replaceRange = range.Document.Range(rangeStart, rangeStart);
                                //_app.ActiveDocument.Range(indexList[i] + indexShift, indexList[i] + indexShift);
                                //range.Move(WdUnits.wdCharacter, indexList[i] + indexShift);
                                replaceRange.InsertAfter(" ");
                                indexShift++;
                            }
                        }
                        else
                        {
                            try
                            {
                                var find = sentence.Find;
                                find.ClearFormatting();
                                find.Replacement.ClearFormatting();
                                find.Text = @"(.)";
                                find.Replacement.Text = @"^& ";

                                find.Execute(Replace: WdReplace.wdReplaceAll);
                            }
                            catch { };
                        }
                    }
                }
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














        #region Regex
        private static Regex punctuationAfterChars = new Regex("(?<=[a-zA-Z][a-z])[.!?'\"]+");
        private static Regex AcronymsThreeLetters = new Regex(@"\b(?:[a-z]*[A-Z][a-z]*){2,}");

        




        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace Tools.Exhibit
{
    public class Pincite
    {
        public Pincite(Microsoft.Office.Interop.Word.Application _app)
        {
            this._app = _app;
            repository = new ExhibitRepository(_app);
        }

        Microsoft.Office.Interop.Word.Application _app;
        ExhibitRepository repository;


        public void AddPincite(Selection sel)
        {
            bool CiteIsGood = false;
            ContentControl cite = null;
            GetCiteForPINCITE(sel, out cite, out CiteIsGood);

            if (CiteIsGood)
            {
                cite.LockContents = false;
                cite.Range.Font.Italic = 0;

                string CurrentPinText = GetPinciteText(cite);

                int index = new ExhibitHelper(_app).GetPosition(cite);
                int InitialCite = IsInitialCite(cite);
                PrepCiteForPin(cite, index, InitialCite);
                InsertPinciteCC(cite, CurrentPinText);

                cite.LockContents = true;
            }
        }

        public void ReAddPincite(ContentControl cite, int index)
        {
            cite.LockContents = false;
            cite.Range.Font.Italic = 0;
            string CurrentPinText = GetPinciteText(cite);

            //int index = new ExhibitHelper(_app).GetPosition(cite);
            int InitialCite = IsInitialCite(cite);
            PrepCiteForPin(cite, index, InitialCite);
            InsertPinciteCC(cite, CurrentPinText);

            cite.LockContents = true;
        }


        private static string GetPinciteText(ContentControl cite)
        {
            string result = string.Empty;

            if (cite.Title.Contains("|PIN"))
            {
                foreach (ContentControl ccChild in cite.Range.ContentControls)
                {
                    if (ccChild.Tag.Contains("PINCITE:"))
                    {
                        if (ccChild.Range.Text == "{type Pincite text}")
                        {
                            result = string.Empty;
                        }
                        else result = ccChild.Range.Text;
                    }
                }
            }

            return result;
        }



        public void PrepCiteForPin(ContentControl cite, int index, int IsInitialCite)
        {
            string citeID = string.Empty;
            CiteType citeType = CiteType.None;
            switch (cite.Tag.Split(':')[0])
            {
                case "Exhibit":
                    citeID = cite.Tag.Substring(8);
                    citeType = CiteType.Exhibit;
                    break;
                case "Cite":
                    citeID = cite.Tag.Substring(5);
                    citeType = CiteType.LegalOrRecordCitation;
                    break;
                default:
                    throw new Exception("Unhandled cite type in Content Control tag.");
            }

            cite.LockContents = false;

            int switchvar = 0;
            switch (switchvar)
            {
                case 0 when IsInitialCite == 3:
                    cite.Range.Text = ExhibitFormatter.FormatIdCite(cite.Range, "{PINCITE}");
                    break;
                case 0 when citeType == CiteType.Exhibit && IsInitialCite == 1:
                    cite.Range.Text = ExhibitFormatter.FormatCite(repository.GetExhibit(citeID), repository.FirstCite, repository.IndexStyle, repository.IndexStart, index, "{PINCITE}");
                    break;
                case 0 when citeType == CiteType.Exhibit && IsInitialCite == 2:
                    cite.Range.Text = ExhibitFormatter.FormatCite(repository.GetExhibit(citeID), repository.FollowingCites, repository.IndexStyle, repository.IndexStart, index, "{PINCITE}");
                    break;
                case 0 when citeType == CiteType.LegalOrRecordCitation && IsInitialCite == 1:
                    cite.Range.Text = ExhibitFormatter.FormatLRCite(repository.GetLRCite(citeID).LongCite, "{PINCITE}");

                    try
                    {
                        string[] anchors = new string[] { "<i>", "</i>" };
                        var TextParts = cite.Range.Text.Split(anchors, StringSplitOptions.None);

                        Range rng = cite.Range;
                        rng.Start = rng.Start + TextParts[0].Length;
                        rng.End = rng.Start + TextParts[1].Length + 7;
                        rng.Text = TextParts[1];
                        rng.Font.Italic = -1;
                    }
                    catch
                    { }

                    break;
                case 0 when citeType == CiteType.LegalOrRecordCitation && IsInitialCite == 2:
                    cite.Range.Text = ExhibitFormatter.FormatLRCite(repository.GetLRCite(citeID).ShortCite, "{PINCITE}");
                    try
                    {
                        string[] anchors = new string[] { "<i>", "</i>" };
                        var TextParts = cite.Range.Text.Split(anchors, StringSplitOptions.None);

                        Range rng = cite.Range;
                        rng.Start = rng.Start + TextParts[0].Length;
                        rng.End = rng.Start + TextParts[1].Length + 7;
                        rng.Text = TextParts[1];
                        rng.Font.Italic = -1;
                    }
                    catch
                    { }

                    break;

                default:
                    throw new Exception("Unhandled switch type: CiteType and IsInitialCite");
            }

            if (!cite.Title.Contains("|PIN"))
            {
                cite.Title += "|PIN";
            }
            cite.LockContents = true;

        }
        /// <summary>
        /// returns 1 = initial cite, 2 = id, 3 = following non id
        /// </summary>
        /// <param name="index"></param>
        /// <param name="IsInitialCite"></param>
        public int IsInitialCite(ContentControl cite)
        {
            int result = 0;

            ExhibitHelper exhibitHelper = new ExhibitHelper(_app);
            List<ContentControl> AllCites = exhibitHelper.GetAndOrderAllCiteContentControls();
            List<string> AllCiteCCIDs = new List<string>();
            List<string> PreviousCiteTags = new List<string>();


            for (int i = 0; i<AllCites.Count; i++)
            {
                AllCiteCCIDs.Add(AllCites[i].ID);

            }

            for (int i = 0; i<AllCiteCCIDs.IndexOf(cite.ID); i++)
            {
                PreviousCiteTags.Add(AllCites[i].Tag);
            }

            if (PreviousCiteTags.LastOrDefault() == cite.Tag)
            {
                result = 3;
            }
            else if (PreviousCiteTags.Contains(cite.Tag))
            {
                result = 2;
            }
            else result = 1;




            //List<string> PreceedingCiteCCIDs = new List<string>();
            //for (int i = 0; i<AllCites.Count; i++)
            //{
            //    if (AllCites[i].ID != cite.ID)
            //    {
            //        PreceedingCiteCCIDs.Add(AllCites[i].ID);
            //    }
            //    else break;
            //}

            //List<string> AllCiteTags = new List<string>();
            //for (int i = 0; i <= AllCites.Count -1; i++)
            //{
            //    AllCiteTags.Add(AllCites[i].Tag);

            //}

            //List<string> PreceedingCiteTags = new List<string>();

            //for (int i = 0; i< AllCiteTags.IndexOf(cite.Tag); i++)
            //{
            //    var catalogedCiteTag = AllCites[i].Tag;
            //    PreceedingCiteTags.Add(catalogedCiteTag);
            //}


            //string previousCiteTag;
            //try
            //{
            //    previousCiteTag = AllCites.Where(n => n.ID == PreceedingCiteCCIDs.Last()).SingleOrDefault().Tag;
            //}
            //catch { previousCiteTag = null; }

            //int CountOfCiteInPreceedingCites = 0;
            //if (PreceedingCiteTags.Contains(cite.Tag))
            //{
            //    foreach (var c in PreceedingCiteTags)
            //    {
            //        if (c == cite.Tag)
            //        {
            //            CountOfCiteInPreceedingCites++;
            //        }
            //    }
            //}


            //if (CountOfCiteInPreceedingCites > 1)
            //{
            //    result = 1; //initial cite
            //}
            //else if (previousCiteTag == cite.Tag)
            //{
            //    result = 2; // id
            //}
            //else
            //{
            //    result = 3; //following cite
            //}

            return result;
        }


        public void InsertPinciteCC(ContentControl cite, string CurrentPinText = "")
        {
            // Finds Pincite Placeholder and Creates range over it
            object missing = Type.Missing;
            Range CiteRange = cite.Range;
            CiteRange.Select();
            _app.Selection.Find.Execute("{PINCITE}");
            Range pinCiteRange = _app.Selection.Range;

            cite.LockContents = false;

            var pinCiteCC = pinCiteRange.ContentControls.Add(WdContentControlType.wdContentControlRichText, pinCiteRange);
            pinCiteCC.SetPlaceholderText(null, null, "{type Pincite text}");
            pinCiteCC.Range.Text = CurrentPinText;
            pinCiteCC.Range.Italic = 0;
            pinCiteCC.Title = "PINCITE";
            pinCiteCC.Tag = "PINCITE:" + pinCiteCC.ParentContentControl.Tag;
        }



        public void ReAddPincite(Selection sel, string PinCiteText)
        {
            

        }
        public void GetCiteForPINCITE(Selection sel, out ContentControl CiteCC, out bool CiteIsGood)
        {
            int ccCount = sel.ContentControls.Count;
            ContentControl cc = null;
            CiteIsGood = false;

            switch (ccCount)
            {
                case int n when n < 1:
                    cc = sel.ParentContentControl;
                    if (cc == null || cc.Tag.Contains("PINCITE"))
                    {
                        MessageBox.Show("Please select an Exhibit or Citation requiring a PINCITE.", "Warning");
                        CiteIsGood = false;
                    }
                    else if (cc.Tag.Contains("Exhibit:") || cc.Tag.Contains("Cite:"))
                    {
                        CiteIsGood = true;
                    }
                    else CiteIsGood = false;
                    break;
                case 1:
                    if (sel.ContentControls[1].Tag.Contains("Exhibit:") || sel.ContentControls[1].Tag.Contains("Cite:"))
                    {
                        cc = sel.ContentControls[1];
                        CiteIsGood = true;
                    }
                    else
                    {
                        MessageBox.Show("Please select an Exhibit or Citation requiring a PINCITE.", "Warning");
                        CiteIsGood = false;
                    }
                    break;
                case int n when n > 1:
                    MessageBox.Show("Please select one Exhibit or Citation per PINCITE.", "Warning");
                    CiteIsGood = false;
                    break;
                default:
                    throw new Exception("error in selecting Exhibits/Citations");
            }

            if (cc != null)
            {
                CiteCC = cc;
            }
            else
            {
                CiteCC = null;
                CiteIsGood = false;
            }
        }
        public void RemovePinCite(Selection selection)
        {
            bool CiteIsGood = false;
            ContentControl cc = null;
            GetCiteForPINCITE(selection, out cc, out CiteIsGood);

            if (selection.ContentControls.Count > 0 && selection.ContentControls[1].Tag.Contains("PINCITE"))
            {
                cc = selection.ContentControls[1].ParentContentControl;
                cc.LockContents = false;
                selection.ContentControls[1].Delete(true);
                cc.Title = cc.Title.Split('|')[0];
                cc.LockContents = true;
            }
            else if (cc.Tag.Contains("PINCITE"))
            {
                ContentControl ChildCC = cc;
                cc = cc.ParentContentControl;
                cc.LockContents = false;
                ChildCC.Delete(true);
                cc.Title = cc.Title.Split('|')[0];
                cc.LockContents = true;
            }
            else if (cc.Title.Contains("|PIN"))
            {
                var ChildCCs = cc.Range.ContentControls;
                foreach (ContentControl ChildCC in ChildCCs)
                {
                    if (ChildCC.Tag.Contains("PINCITE"))
                    {
                        cc.LockContents = false;
                        ChildCC.Delete(true);
                        cc.Title = cc.Title.Split('|')[0];
                        cc.LockContents = true;
                    }
                }
            }

        }
    }
}

using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Tools.Exhibit
{
    public class ExhibitHelper
    {
        public ExhibitHelper(Word.Application _app)
        {
            repository = new ExhibitRepository(_app);
            this._app = _app;

        }
        Word.Application _app;
        public ExhibitRepository repository;




        public int GetPosition(ContentControl cite)
        {
            List<CitationReference> references = OrderOnlyExhibitsRefs();
            int index = 0;

            string citeID = string.Empty;
            CiteType citeType = CiteType.None;
            GetCCIDAndCiteType(cite, out citeID, out citeType);
            CitationReference refer = references.Where(n => n.ID == citeID).FirstOrDefault();

            try
            {
                index = references.IndexOf(refer);
            }
            catch { index = -1; }

            return index + 1;

        }

        public List<ContentControl> GetAllCitesFromDoc()
        {

            List<ContentControl> ccs = new List<ContentControl>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag != null && (cc.Tag.StartsWith("Exhibit:") || cc.Tag.StartsWith("Cite:")))
                {
                    ccs.Add(cc);
                }
            }


            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    if (cc.Tag != null && (cc.Tag.StartsWith("Exhibit:") || cc.Tag.StartsWith("Cite:")))
                    {
                        ccs.Add(cc);
                    }
                }
            }

            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    if (cc.Tag != null && (cc.Tag.StartsWith("Exhibit:") || cc.Tag.StartsWith("Cite:")))
                    {
                        ccs.Add(cc);
                    }
                }
            }

            return ccs;

        }

        public List<ContentControl> GetAllExhibitsFromDoc()
        {
            return GetAllCitesFromDoc()
                .Where(n => n.Tag.Contains("Exhibit:"))
                .ToList();
        }

        private List<CitationReference> OrderAllCitationRefs()
        {
            List<CitationReference> references = new List<CitationReference>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                string refID = string.Empty;
                CiteType citeType = CiteType.None;

                if (cc.Tag == null || cc.Tag.StartsWith("PINCITE"))
                {

                }
                else if (cc.Tag.StartsWith("Exhibit:"))
                {
                    refID = cc.Tag.Substring(8);
                    citeType = CiteType.Exhibit;

                    CitationReference r = new CitationReference(refID, cc.Range.Start, 0, cc.ID, citeType);
                    references.Add(r);

                }
                else if (cc.Tag.StartsWith("Cite:"))
                {
                    refID = cc.Tag.Substring(5);
                    citeType = CiteType.LegalOrRecordCitation;

                    CitationReference r = new CitationReference(refID, cc.Range.Start, 0, cc.ID, citeType);
                    references.Add(r);

                }
            }

            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    string refID = string.Empty;
                    CiteType citeType = CiteType.None;

                    if (cc.Tag == null || cc.Tag.StartsWith("PINCITE"))
                    {

                    }
                    else if (cc.Tag.StartsWith("Exhibit:"))
                    {
                        refID = cc.Tag.Substring(8);
                        citeType = CiteType.Exhibit;

                        CitationReference r = new CitationReference(refID, fn.Reference.Start, 0, cc.ID, citeType);
                        references.Add(r);

                    }
                    else if (cc.Tag.StartsWith("Cite:"))
                    {
                        refID = cc.Tag.Substring(5);
                        citeType = CiteType.LegalOrRecordCitation;

                        CitationReference r = new CitationReference(refID, fn.Reference.Start, 0, cc.ID, citeType);
                        references.Add(r);

                    }
                    // fn.Reference.Start should give the start position of the footnote holding the exhibit. Need to figure out what to do if there are multiple exhibits in the same footnote/endnote.

                }
            }

            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    string refID = string.Empty;
                    CiteType citeType = CiteType.None;

                    if (cc.Tag == null || cc.Tag.StartsWith("PINCITE"))
                    {

                    }
                    else if (cc.Tag.StartsWith("Exhibit:"))
                    {
                        refID = cc.Tag.Substring(8);
                        citeType = CiteType.Exhibit;

                        CitationReference r = new CitationReference(refID, en.Reference.Start, 0, cc.ID, citeType);
                        references.Add(r);

                    }
                    else if (cc.Tag.StartsWith("Cite:"))
                    {
                        refID = cc.Tag.Substring(5);
                        citeType = CiteType.LegalOrRecordCitation;

                        CitationReference r = new CitationReference(refID, en.Reference.Start, 0, cc.ID, citeType);
                        references.Add(r);

                    }
                    // fn.Reference.Start should give the start position of the footnote holding the exhibit. Need to figure out what to do if there are multiple exhibits in the same footnote/endnote.

                }
            }

            return references = references.OrderBy(reference => reference.RangeStart).ThenBy(note => note.NoteRangeStart).ToList();

        }

        public List<CitationReference> OrderOnlyExhibitsRefs()
        {
            return OrderAllCitationRefs()
                .Where(n => n.citeType == CiteType.Exhibit)
                .ToList();
        }

        public void UpdateInsertedCites()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<ContentControl> AllCites = GetAndOrderAllCiteContentControls();

            List<string> IDsForFormatChoice = new List<string>() { "Fill Item" };
            List<string> ExhibitIndex = new List<string>() { "Fill Item" };


            foreach (ContentControl cite in AllCites)
            {

                cite.LockContents = false;

                _app.Selection.SetRange(cite.Range.Start, cite.Range.End); //needed to bring the selection in/out of the footnotes/endnotes, which have their own range start

                string citeID = string.Empty;
                CiteType citeType = CiteType.None;
                GetCCIDAndCiteType(cite, out citeID, out citeType);

                Exhibit exhibit;
                string FirstCite = repository.FirstCite;
                string FollowingCites = repository.FollowingCites;
                NumberingOptions IndexStyle = repository.IndexStyle;
                int IndexStart = repository.IndexStart;

                int FormatChoiceIndex = IDsForFormatChoice.FindIndex(n => n == citeID); // returns -1 if index not found
                int index = ExhibitIndex.FindIndex(n => n == citeID);


                bool idCite = bool.Parse(repository.GetFormatting(FormatNodes.IdCite));

                int switchInt = 0;
                switch (switchInt)
                {
                    case 0 when citeType == CiteType.LegalOrRecordCitation && cite.Title.Contains("|PIN"):
                        new Pincite(_app).ReAddPincite(cite, index);
                        break;
                    case 0 when citeType == CiteType.Exhibit && cite.Title.Contains("|PIN") && FormatChoiceIndex == -1:
                        index = ExhibitIndex.Count();
                        new Pincite(_app).ReAddPincite(cite, index);
                        ExhibitIndex.Add(citeID);
                        break;
                    case 0 when citeType == CiteType.Exhibit && cite.Title.Contains("|PIN") && FormatChoiceIndex >0:
                        new Pincite(_app).ReAddPincite(cite, index);
                        break;

                    case 0 when idCite && citeID == IDsForFormatChoice.Last():
                        cite.Range.Text = ExhibitFormatter.FormatIdCite(cite.Range);
                        cite.Range.Italic = -1;
                        break;

                    case 0 when citeType == CiteType.LegalOrRecordCitation && FormatChoiceIndex == -1: //when citeID is not found in IDsForFormatChoice (initial cite)
                        cite.Range.Text = ExhibitFormatter.FormatLRCite(repository.GetLRCite(citeID).LongCite);
                        cite.Range.Italic = 0;

                        break;
                    case 0 when citeType == CiteType.LegalOrRecordCitation && FormatChoiceIndex > 0:
                        cite.Range.Text = ExhibitFormatter.FormatLRCite(repository.GetLRCite(citeID).ShortCite);
                        cite.Range.Italic = 0;
                        break;

                    case 0 when citeType == CiteType.Exhibit && FormatChoiceIndex == -1:
                        index = ExhibitIndex.Count();
                        exhibit = repository.GetExhibit(citeID);
                        cite.Range.Text = ExhibitFormatter.FormatCite(exhibit, FirstCite, IndexStyle, IndexStart, index);
                        cite.Range.Italic = 0;
                        ExhibitIndex.Add(citeID);
                        break;
                    case 0 when citeType == CiteType.Exhibit && FormatChoiceIndex > 0:
                        exhibit = repository.GetExhibit(citeID);
                        cite.Range.Text = ExhibitFormatter.FormatCite(exhibit, FollowingCites, IndexStyle, IndexStart, index);
                        cite.Range.Italic = 0;
                        break;
                    default:
                        throw new Exception("Error when determining Cite type or index associated with Content Control");
                }

                {
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
                }

                IDsForFormatChoice.Add(citeID);

                cite.LockContents = true;
            }

        }

        public List<ContentControl> GetAndOrderAllCiteContentControls()
        {
            List<ContentControl> AllCites = GetAllCitesFromDoc();
            List<CitationReference> AllCiteRefs = OrderAllCitationRefs();

            List<string> AllCiteIDsInOrder = new List<string>();
            foreach (CitationReference citeRef in AllCiteRefs)
            {
                AllCiteIDsInOrder.Add(citeRef.CcId);
            }
            AllCites = AllCites.OrderBy(cc => AllCiteIDsInOrder.IndexOf(cc.ID)).ToList();

            //List<CitationReference> ExhibitRefs = OrderOnlyExhibitsRefs();
            //List<string> ExhibitIDsInOrder = new List<string>();
            //foreach (CitationReference citeRef in ExhibitRefs)
            //{
            //    ExhibitIDsInOrder.Add(citeRef.CcId);
            //}

            return AllCites;
        }

        public void GetCCIDAndCiteType(ContentControl cite, out string citeID, out CiteType citeType)
        {
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
                case "PINCITE":
                    ContentControl parent = cite.ParentContentControl;
                    GetCCIDAndCiteType(parent, out citeID, out citeType);
                    break;
                default:
                    throw new Exception("Unhandled cite type in Content Control tag.");
            }
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
                        if (ccChild.Range.Text == "{type PinCite text}")
                        {
                            result = string.Empty;
                        }
                        else result = ccChild.Range.Text;
                    }
                }
            }

            return result;
        }

        public void RemoveAllCitesFromDoc()
        {
            List<ContentControl> ccs = GetAllCitesFromDoc();
            foreach (ContentControl cc in ccs)
            {
                cc.LockContents = false;
                foreach (ContentControl pincite in cc.Range.ContentControls)
                {
                    pincite.Delete(false);
                }

                cc.Delete(false);
            }

        }
        public void RemoveSelectedCitesFromDoc(Word.Selection Selection)
        {
            ContentControls ccs = Selection.ContentControls;
            foreach (ContentControl cc in ccs)
            {
                if (cc.Tag.Contains("Exhibit:") || cc.Tag.Contains("Cite:"))
                {
                    cc.LockContents = false;
                    foreach (ContentControl pincite in cc.Range.ContentControls)
                    {
                        pincite.Delete(false);
                    }
                    cc.Delete(false);
                }
            }
        }







    }
}

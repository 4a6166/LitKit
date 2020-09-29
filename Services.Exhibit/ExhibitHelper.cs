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




        public int GetPosition(string tag)
        {
            List<CitationReference> references = OrderAllCitations();
            int index = 0;

            string ID = tag.Substring(8);
            CitationReference refer = references.Where(n => n.ID == ID).FirstOrDefault();

            index = references.IndexOf(refer);

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

        private List<CitationReference> OrderAllCitations()
        {
            List<CitationReference> references = new List<CitationReference>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                string refID = string.Empty;
                CiteType citeType = CiteType.None;

                if (cc.Tag == null)
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

                    if (cc.Tag == null)
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

                    if (cc.Tag == null)
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

        public List<CitationReference> OrderOnlyExhibits()
        {
            return OrderAllCitations()
                .Where(n => n.citeType == CiteType.Exhibit)
                .ToList();
        }

        public void UpdateInsertedCites()
        {
            Cursor.Current = Cursors.WaitCursor;

            List<ContentControl> AllCites = GetAllCitesFromDoc();
            List<CitationReference> AllCiteRefs = OrderAllCitations();

            List<string> AllCiteIDsInOrder = new List<string>();
            foreach (CitationReference citeRef in AllCiteRefs)
            {
                AllCiteIDsInOrder.Add(citeRef.CcId);
            }
            AllCites = AllCites.OrderBy(cc => AllCiteIDsInOrder.IndexOf(cc.ID)).ToList();

            List<CitationReference> ExhibitRefs = OrderOnlyExhibits();
            List<string> ExhibitIDsInOrder = new List<string>();
            foreach (CitationReference citeRef in ExhibitRefs)
            {
                ExhibitIDsInOrder.Add(citeRef.CcId);
            }

            List<string> IDsForFormatChoice = new List<string>() { "Fill Item" };


            foreach (ContentControl cite in AllCites)
            {

                cite.LockContents = false;

                _app.Selection.SetRange(cite.Range.Start, cite.Range.End); //needed to bring the selection in/out of the footnotes/endnotes, which have their own range start


                string PinCiteText = GetPinciteText(cite);

                string citeID = string.Empty;
                CiteType citeType = CiteType.None;
                GetCCIDAndCiteType(cite, out citeID, out citeType);

                Exhibit exhibit;
                string FirstCite = repository.GetFormatting(FormatNodes.FirstCite);
                string FollowingCites = repository.GetFormatting(FormatNodes.FollowingCites);
                NumberingOptions IndexStyle = new EnumSwitch().NumberingOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.IndexStyle));
                int IndexStart = Int32.Parse(repository.GetFormatting(FormatNodes.IndexStart));

                bool idCite = bool.Parse(repository.GetFormatting(FormatNodes.IdCite));
                int index = IDsForFormatChoice.FindIndex(n => n == citeID); // returns -1 if index not found
                int switchInt = 0;
                switch (switchInt)
                {
                    case 0 when idCite && citeID == IDsForFormatChoice.Last():
                        cite.Range.Text = ExhibitFormatter.FormatIdCite(cite.Range);
                        cite.Range.Italic = -1;
                        break;

                    case 0 when citeType == CiteType.LegalOrRecordCitation && index == -1: //when citeID is not found in IDsForFormatChoice (initial cite)
                        cite.Range.Text = repository.GetLRCite(citeID).LongCite;
                        cite.Range.Italic = 0;
                        break;
                    case 0 when citeType == CiteType.LegalOrRecordCitation && index > 0:
                        cite.Range.Text = repository.GetLRCite(citeID).ShortCite;
                        cite.Range.Italic = 0;
                        break;

                    case 0 when citeType == CiteType.Exhibit && index == -1:
                        index = IDsForFormatChoice.Count();
                        exhibit = repository.GetExhibit(citeID);
                        cite.Range.Text = ExhibitFormatter.FormatCite(exhibit, FirstCite, IndexStyle, IndexStart, index, PinCiteText);
                        cite.Range.Italic = 0;
                        break;
                    case 0 when citeType == CiteType.Exhibit && index > 0:
                        exhibit = repository.GetExhibit(citeID);
                        cite.Range.Text = ExhibitFormatter.FormatCite(exhibit, FollowingCites, IndexStyle, IndexStart, index, PinCiteText);
                        cite.Range.Italic = 0;
                        break;
                    default:
                        throw new Exception("Error when determining Cite type or index associated with Content Control");
                }

                //if (cite.Title.Contains("|PIN"))
                //{
                //    cite.Range.Select();
                //    Word.Selection sel = _app.Selection;
                //    sel.SetRange(cc.Range.Start, cc.Range.End);
                //    ReAddPincite(sel, PinCiteText);
                //}

                IDsForFormatChoice.Add(citeID);

                cite.LockContents = true;
            }

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

        //public void UpdateInsertedCites(string a)
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    List<ContentControl> ccs = GetAllCitesFromDoc();
        //    List<CitationReference> ccRefs = OrderAllCitations();
        //    List<string> CcIDsInOrder = new List<string>();
        //    foreach (CitationReference eRef in ccRefs)
        //    {
        //        CcIDsInOrder.Add(eRef.CcId);
        //    }

        //    ccs = ccs.OrderBy(cc => CcIDsInOrder.IndexOf(cc.ID)).ToList();

        //    string idCite = repository.GetFormatting(FormatNodes.IdCite);

        //    List<string> insertedCiteTags = new List<string> { "FillItem" };
        //    List<string> CiteTagsIndex = new List<string> { "FillItem" };

        //    List<string> allBodyIDs = new List<string>();
        //    foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
        //    {
        //        if (cc.Tag != null && (cc.Tag.StartsWith("Exhibit:") || cc.Tag.StartsWith("Cite:")))
        //        {
        //            allBodyIDs.Add(cc.ID);
        //        }
        //    }
        //    List<string> insertedBodyTags = new List<string> { "FillItem" };

        //    List<string> allFootNoteIDs = new List<string>();
        //    foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
        //    {
        //        foreach (ContentControl cc in fn.Range.ContentControls)
        //        {
        //            if (cc.Tag != null && (cc.Tag.StartsWith("Exhibit:") || cc.Tag.StartsWith("Cite:")))
        //            {
        //                allFootNoteIDs.Add(cc.ID);
        //            }
        //        }
        //    }
        //    List<string> insertedFootNoteTags = new List<string> { "FillItem" };

        //    List<string> allEndNoteIDs = new List<string>();
        //    foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
        //    {
        //        foreach (ContentControl cc in en.Range.ContentControls)
        //        {
        //            if (cc.Tag != null && (cc.Tag.StartsWith("Exhibit:") || cc.Tag.StartsWith("Cite:")))
        //            {
        //                allEndNoteIDs.Add(cc.ID);
        //            }
        //        }
        //    }
        //    List<string> insertedEndNoteTags = new List<string> { "FillItem" };

        //    string text = string.Empty;
        //    string ID = string.Empty;

        //    foreach (ContentControl cc in ccs)
        //    {
        //            string PinCiteText = string.Empty;
        //            if (cc.Title.Contains("|PIN"))
        //            {
        //                foreach (ContentControl ccChild in cc.Range.ContentControls)
        //                {
        //                    if (ccChild.Tag.Contains("PINCITE:"))
        //                    {
        //                        if (ccChild.Range.Text == "{type PinCite text}")
        //                        {
        //                            PinCiteText = string.Empty;
        //                        }
        //                        else PinCiteText = ccChild.Range.Text;
        //                    }
        //                }
        //            }

        //        #region exhibit is in footnotes
        //        if (allFootNoteIDs.Contains(cc.ID))
        //        {
        //            // Start formatting Exhibit text, PINCITE is re-added after
        //            if (idCite == "True" && cc.Tag == insertedFootNoteTags.Last())  //Id cites
        //            {
        //                int index = CiteTagsIndex.IndexOf(cc.Tag);
        //                Word.Selection sel = _app.Selection;
        //                sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
        //                text = FormatIdExhibit(sel.Range, index);
        //                insertedCiteTags.Add(cc.Tag);
        //                insertedFootNoteTags.Add(cc.Tag);
        //            }
        //            else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
        //            {
        //                int index = CiteTagsIndex.IndexOf(cc.Tag);
        //                insertedFootNoteTags.Add(cc.Tag);

        //                ID = cc.Tag.Substring(8);
        //                Exhibit exhibit = repository.GetExhibit(ID);

        //                text = FormatFollowingCite(exhibit, index);
        //            }
        //            else // first cites
        //            {
        //                int index = CiteTagsIndex.Count;
        //                CiteTagsIndex.Add(cc.Tag);
        //                insertedFootNoteTags.Add(cc.Tag);

        //                ID = cc.Tag.Substring(8);
        //                Exhibit exhibit = repository.GetExhibit(ID);

        //                text = FormatFirstCite(exhibit, index);
        //            }

        //            cc.LockContents = false;
        //            cc.Range.Text = text;

        //            if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
        //            {
        //                cc.Range.Italic = -1;
        //            }
        //            else cc.Range.Italic = 0;

        //            cc.LockContents = true;

        //            //Re-adds PINCITE following full formatting of text in Exhibit
        //            if (cc.Title.Contains("|PIN"))
        //            {
        //                cc.Range.Select();
        //                Word.Selection sel = _app.Selection;
        //                sel.SetRange(cc.Range.Start, cc.Range.End);
        //                ReAddPincite(sel, PinCiteText);
        //            }
        //        }

        //        #endregion
        //        #region exhibit is in endnotes
        //        if (allEndNoteIDs.Contains(cc.ID))
        //        {
        //            // Start formatting Exhibit text, PINCITE is re-added after
        //            if (idCite == "True" && cc.Tag == insertedEndNoteTags.Last())  //Id cites
        //            {
        //                int index = CiteTagsIndex.IndexOf(cc.Tag);
        //                Word.Selection sel = _app.Selection;
        //                sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
        //                text = FormatIdExhibit(sel.Range, index);
        //                insertedCiteTags.Add(cc.Tag);
        //                insertedEndNoteTags.Add(cc.Tag);
        //            }
        //            else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
        //            {
        //                int index = CiteTagsIndex.IndexOf(cc.Tag);
        //                insertedEndNoteTags.Add(cc.Tag);

        //                ID = cc.Tag.Substring(8);
        //                Exhibit exhibit = repository.GetExhibit(ID);

        //                text = FormatFollowingCite(exhibit, index);
        //            }
        //            else // first cites
        //            {
        //                int index = CiteTagsIndex.Count;
        //                CiteTagsIndex.Add(cc.Tag);
        //                insertedEndNoteTags.Add(cc.Tag);

        //                ID = cc.Tag.Substring(8);
        //                Exhibit exhibit = repository.GetExhibit(ID);

        //                text = FormatFirstCite(exhibit, index);
        //            }

        //            cc.LockContents = false;
        //            cc.Range.Text = text;

        //            if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
        //            {
        //                cc.Range.Italic = -1;
        //            }
        //            else cc.Range.Italic = 0;

        //            cc.LockContents = true;

        //            //Re-adds PINCITE following full formatting of text in Exhibit
        //            if (cc.Title.Contains("|PIN"))
        //            {
        //                cc.Range.Select();
        //                Word.Selection sel = _app.Selection;
        //                sel.SetRange(cc.Range.Start, cc.Range.End);
        //                ReAddPincite(sel, PinCiteText);
        //            }
        //        }

        //        #endregion

        //        #region exhibit is in body text
        //        if (allBodyIDs.Contains(cc.ID))
        //        {
        //            // Start formatting Exhibit text, PINCITE is re-added after
        //            if (idCite == "True" && cc.Tag == insertedBodyTags.Last())  //Id cites
        //            {
        //                int index = CiteTagsIndex.IndexOf(cc.Tag);
        //                Word.Selection sel = _app.Selection;
        //                sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
        //                text = FormatIdExhibit(sel.Range, index);
        //                insertedCiteTags.Add(cc.Tag);
        //                insertedBodyTags.Add(cc.Tag);
        //            }
        //            else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
        //            {
        //                int index = CiteTagsIndex.IndexOf(cc.Tag);
        //                insertedBodyTags.Add(cc.Tag);

        //                ID = cc.Tag.Substring(8);
        //                Exhibit exhibit = repository.GetExhibit(ID);

        //                text = FormatFollowingCite(exhibit, index);
        //            }
        //            else // first cites
        //            {
        //                int index = CiteTagsIndex.Count;
        //                CiteTagsIndex.Add(cc.Tag);
        //                insertedBodyTags.Add(cc.Tag);

        //                ID = cc.Tag.Substring(8);
        //                Exhibit exhibit = repository.GetExhibit(ID);

        //                text = FormatFirstCite(exhibit, index);
        //            }

        //            cc.LockContents = false;
        //            cc.Range.Text = text;

        //            if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
        //            {
        //                cc.Range.Italic = -1;
        //            }
        //            else cc.Range.Italic = 0;

        //            cc.LockContents = true;

        //            //Re-adds PINCITE following full formatting of text in Exhibit
        //            if (cc.Title.Contains("|PIN"))
        //            {
        //                cc.Range.Select();
        //                Word.Selection sel = _app.Selection;
        //                sel.SetRange(cc.Range.Start, cc.Range.End);
        //                ReAddPincite(sel, PinCiteText);
        //            }
        //        }
        //        #endregion
        //    }

        //    Cursor.Current = Cursors.Default;
        //}



        public void RemoveAllCitesFromDoc()
        {
            List<ContentControl> ccs = GetAllCitesFromDoc();
            foreach (ContentControl cc in ccs)
            {
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
                    cc.Delete(false);
                }
            }
        }







    }
}

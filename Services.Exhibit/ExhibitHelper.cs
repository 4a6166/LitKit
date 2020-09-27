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
            List<ExhibitsReference> references = OrderExhibits();
            int index = 0;

            string ID = tag.Substring(8);
            ExhibitsReference refer = references.Where(n => n.ExhibtId == ID).FirstOrDefault();

            index = references.IndexOf(refer);

            return index + 1;

        }

        private List<ExhibitsReference> OrderExhibits()
        {

            List<ExhibitsReference> references = new List<ExhibitsReference>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                {
                    ExhibitsReference r = new ExhibitsReference(cc.Tag.Substring(8), cc.Range.Start, 0, cc.ID);
                    references.Add(r);
                }
            }


            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ExhibitsReference r = new ExhibitsReference(cc.Tag.Substring(8), fn.Reference.Start, fn.Range.Start, cc.ID);
                        references.Add(r);
                        // fn.Reference.Start should give the start position of the footnote holding the exhibit. Need to figure out what to do if there are multiple exhibits in the same footnote. Also need to make any changes her to the Endnote search as well.
                    }
                }
            }

            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ExhibitsReference r = new ExhibitsReference(cc.Tag.Substring(8), en.Reference.Start, en.Range.Start, cc.ID);
                        references.Add(r);
                    }
                }
            }

            return references = references.OrderBy(reference => reference.RangeStart).ThenBy(note => note.NoteRangeStart).ToList();

        }

        public List<ContentControl> GetExhibitsInDocument()
        {

            List<ContentControl> ccs = new List<ContentControl>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                {
                    ccs.Add(cc);
                }
            }


            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ccs.Add(cc);
                    }
                }
            }

            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ccs.Add(cc);
                    }
                }
            }

            return ccs;

        }

        public void RefreshInsertedExhibits()
        {
            //Cursor.Current = Cursors.WaitCursor;

            //List<ContentControl> ccs = GetExhibitsInDocument();
            //List<ExhibitsReference> ccRefs = OrderExhibits();
            //List<string> CcIDsInOrder = new List<string>();
            //foreach (ExhibitsReference eRef in ccRefs)
            //{
            //    CcIDsInOrder.Add(eRef.CcId);
            //}

            //ccs = ccs.OrderBy(cc => CcIDsInOrder.IndexOf(cc.ID)).ToList();

            //string idCite = repository.GetFormatting(FormatNodes.IdCite);

            //List<string> insertedCiteTags = new List<string> { "FillItem" };
            //List<string> CiteTagsIndex = new List<string> { "FillItem" };

            //List<string> allBodyIDs = new List<string>();
            //foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            //{
            //    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
            //    {
            //        allBodyIDs.Add(cc.ID);
            //    }
            //}
            //List<string> insertedBodyTags = new List<string> { "FillItem" };

            //List<string> allFootNoteIDs = new List<string>();
            //foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            //{
            //    foreach (ContentControl cc in fn.Range.ContentControls)
            //    {
            //        if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
            //        {
            //            allFootNoteIDs.Add(cc.ID);
            //        }
            //    }
            //}
            //List<string> insertedFootNoteTags = new List<string> { "FillItem" };

            //List<string> allEndNoteIDs = new List<string>();
            //foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            //{
            //    foreach (ContentControl cc in en.Range.ContentControls)
            //    {
            //        if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
            //        {
            //            allEndNoteIDs.Add(cc.ID);
            //        }
            //    }
            //}
            //List<string> insertedEndNoteTags = new List<string> { "FillItem" };

            //string text = string.Empty;
            //string ID = string.Empty;

            //foreach (ContentControl cc in ccs)
            //{
            //    string PinCiteText = string.Empty;
            //    if (cc.Title.Contains("|PIN"))
            //    {
            //        foreach (ContentControl ccChild in cc.Range.ContentControls)
            //        {
            //            if (ccChild.Tag.Contains("PINCITE:"))
            //            {
            //                if (ccChild.Range.Text == "{type PinCite text}")
            //                {
            //                    PinCiteText = string.Empty;
            //                }
            //                else PinCiteText = ccChild.Range.Text;
            //            }
            //        }
            //    }

            //    #region exhibit is in footnotes
            //    if (allFootNoteIDs.Contains(cc.ID))
            //    {
            //        // Start formatting Exhibit text, PINCITE is re-added after
            //        if (idCite == "True" && cc.Tag == insertedFootNoteTags.Last())  //Id cites
            //        {
            //            int index = CiteTagsIndex.IndexOf(cc.Tag);
            //            Word.Selection sel = _app.Selection;
            //            sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
            //            text = FormatIdExhibit(sel.Range, index);
            //            insertedCiteTags.Add(cc.Tag);
            //            insertedFootNoteTags.Add(cc.Tag);
            //        }
            //        else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
            //        {
            //            int index = CiteTagsIndex.IndexOf(cc.Tag);
            //            insertedFootNoteTags.Add(cc.Tag);

            //            ID = cc.Tag.Substring(8);
            //            Exhibit exhibit = repository.GetExhibit(ID);

            //            text = FormatFollowingCite(exhibit, index);
            //        }
            //        else // first cites
            //        {
            //            int index = CiteTagsIndex.Count;
            //            CiteTagsIndex.Add(cc.Tag);
            //            insertedFootNoteTags.Add(cc.Tag);

            //            ID = cc.Tag.Substring(8);
            //            Exhibit exhibit = repository.GetExhibit(ID);

            //            text = FormatFirstCite(exhibit, index);
            //        }

            //        cc.LockContents = false;
            //        cc.Range.Text = text;

            //        if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
            //        {
            //            cc.Range.Italic = -1;
            //        }
            //        else cc.Range.Italic = 0;

            //        cc.LockContents = true;

            //        //Re-adds PINCITE following full formatting of text in Exhibit
            //        if (cc.Title.Contains("|PIN"))
            //        {
            //            cc.Range.Select();
            //            Word.Selection sel = _app.Selection;
            //            sel.SetRange(cc.Range.Start, cc.Range.End);
            //            ReAddPincite(sel, PinCiteText);
            //        }
            //    }

            //    #endregion
            //    #region exhibit is in endnotes
            //    if (allEndNoteIDs.Contains(cc.ID))
            //    {
            //        // Start formatting Exhibit text, PINCITE is re-added after
            //        if (idCite == "True" && cc.Tag == insertedEndNoteTags.Last())  //Id cites
            //        {
            //            int index = CiteTagsIndex.IndexOf(cc.Tag);
            //            Word.Selection sel = _app.Selection;
            //            sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
            //            text = FormatIdExhibit(sel.Range, index);
            //            insertedCiteTags.Add(cc.Tag);
            //            insertedEndNoteTags.Add(cc.Tag);
            //        }
            //        else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
            //        {
            //            int index = CiteTagsIndex.IndexOf(cc.Tag);
            //            insertedEndNoteTags.Add(cc.Tag);

            //            ID = cc.Tag.Substring(8);
            //            Exhibit exhibit = repository.GetExhibit(ID);

            //            text = FormatFollowingCite(exhibit, index);
            //        }
            //        else // first cites
            //        {
            //            int index = CiteTagsIndex.Count;
            //            CiteTagsIndex.Add(cc.Tag);
            //            insertedEndNoteTags.Add(cc.Tag);

            //            ID = cc.Tag.Substring(8);
            //            Exhibit exhibit = repository.GetExhibit(ID);

            //            text = FormatFirstCite(exhibit, index);
            //        }

            //        cc.LockContents = false;
            //        cc.Range.Text = text;

            //        if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
            //        {
            //            cc.Range.Italic = -1;
            //        }
            //        else cc.Range.Italic = 0;

            //        cc.LockContents = true;

            //        //Re-adds PINCITE following full formatting of text in Exhibit
            //        if (cc.Title.Contains("|PIN"))
            //        {
            //            cc.Range.Select();
            //            Word.Selection sel = _app.Selection;
            //            sel.SetRange(cc.Range.Start, cc.Range.End);
            //            ReAddPincite(sel, PinCiteText);
            //        }
            //    }

            //    #endregion

            //    #region exhibit is in body text
            //    if (allBodyIDs.Contains(cc.ID))
            //    {
            //        // Start formatting Exhibit text, PINCITE is re-added after
            //        if (idCite == "True" && cc.Tag == insertedBodyTags.Last())  //Id cites
            //        {
            //            int index = CiteTagsIndex.IndexOf(cc.Tag);
            //            Word.Selection sel = _app.Selection;
            //            sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
            //            text = FormatIdExhibit(sel.Range, index);
            //            insertedCiteTags.Add(cc.Tag);
            //            insertedBodyTags.Add(cc.Tag);
            //        }
            //        else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
            //        {
            //            int index = CiteTagsIndex.IndexOf(cc.Tag);
            //            insertedBodyTags.Add(cc.Tag);

            //            ID = cc.Tag.Substring(8);
            //            Exhibit exhibit = repository.GetExhibit(ID);

            //            text = FormatFollowingCite(exhibit, index);
            //        }
            //        else // first cites
            //        {
            //            int index = CiteTagsIndex.Count;
            //            CiteTagsIndex.Add(cc.Tag);
            //            insertedBodyTags.Add(cc.Tag);

            //            ID = cc.Tag.Substring(8);
            //            Exhibit exhibit = repository.GetExhibit(ID);

            //            text = FormatFirstCite(exhibit, index);
            //        }

            //        cc.LockContents = false;
            //        cc.Range.Text = text;

            //        if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
            //        {
            //            cc.Range.Italic = -1;
            //        }
            //        else cc.Range.Italic = 0;

            //        cc.LockContents = true;

            //        //Re-adds PINCITE following full formatting of text in Exhibit
            //        if (cc.Title.Contains("|PIN"))
            //        {
            //            cc.Range.Select();
            //            Word.Selection sel = _app.Selection;
            //            sel.SetRange(cc.Range.Start, cc.Range.End);
            //            ReAddPincite(sel, PinCiteText);
            //        }
            //    }
            //    #endregion
            //}

            //Cursor.Current = Cursors.Default;
        }



        public void RemoveExhibitsFromDoc()
        {
            List<ContentControl> ccs = GetExhibitsInDocument();
            foreach (ContentControl cc in ccs)
            {
                cc.Delete(false);
            }
        }
        public void RemoveExhibitsFromDoc(Word.Selection Selection)
        {
            ContentControls ccs = Selection.ContentControls;
            foreach (ContentControl cc in ccs)
            {
                if (cc.Tag.Contains("Exhibit:"))
                {
                    cc.Delete(false);
                }
            }
        }




    }
}

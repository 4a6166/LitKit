using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Tools.Citation
{
    /// <summary>
    /// Handles interactions between the document display and the database
    /// </summary>
    public class CiteDocLayer
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Application _app { get; private set; }

        public CiteDocLayer(Application application)
        {
            this._app = application;
        }

        #region Utilities
        public string GetCitationIDFromContentControl(ContentControl contentControl)
        {
            try
            {
                return contentControl.Tag.Split('|')[1];
            }
            catch
            {
                log.Error("ContentControl " + contentControl.ID + " Tag = null");
                return "";
            }
        }

        public string SetCiteCCTagTitleColor(ContentControl contentControl, Citation citation, bool HasPincite)
        {
            var tag = "CITE:" + citation.CiteType.ToString() + "|" + citation.ID + "|PIN:" + HasPincite.ToString();
            contentControl.Tag = tag;
            contentControl.Title = citation.CiteType.ToString() + ": " + citation.LongDescription;
            contentControl.Color = WdColor.wdColorLightBlue;
            return tag;
        }

        public string SetPinCCTagTitleColor(ContentControl PinCC)
        {
            string tag = "PIN";
            PinCC.Tag = tag;
            PinCC.Title = "PIN";
            PinCC.Color = WdColor.wdColorDarkBlue;

            return tag;
        }

        private bool CCHasPincite(ContentControl cc)
        {
            try
            {
                return bool.Parse(cc.Tag.Split('|')[2]);
            }
            catch { return false; }
        }

        private void AddHyperlink(ContentControl contentControl, Citation citation, string ScreenTip = "")
        {
            if (citation.Hyperlink != "")
            {
                contentControl.LockContents = false;

                if (ScreenTip != "")
                {
                    contentControl.Range.Hyperlinks.Add(Anchor: contentControl.Range, Address: citation.Hyperlink, ScreenTip: ScreenTip);
                }
                else
                {
                    contentControl.Range.Hyperlinks.Add(Anchor: contentControl.Range, Address: citation.Hyperlink);
                }

                contentControl.LockContents = true;
            }
        }

        public ContentControl GrabCiteContentControl(Selection selection)
        {
            ContentControl CiteCC = null;
            if (selection.ContentControls.Count < 1)
            {
                CiteCC = selection.ParentContentControl;
                if (CiteCC.Tag == "PIN")
                {
                    CiteCC = CiteCC.ParentContentControl;
                }
            }
            else if (selection.ContentControls[1].Tag == "PIN")
            {
                CiteCC = selection.ParentContentControl;
            }
            else if (selection.ContentControls[1].Tag.StartsWith("CITE"))
            {
                CiteCC = selection.ContentControls[1];
            }

            return CiteCC;
        }

        /// <summary>
        /// Index of citation in list is the position it first appears in the document
        /// </summary>
        public List<ContentControl> GetUniqueListOfCites(List<ContentControl> CCList)
        {
            List<ContentControl> UniqueList = new List<ContentControl>();
            List<string> AllIDs = new List<string>();
            foreach (ContentControl contentControl in CCList)
            {
                string ID = contentControl.Tag.Split('|')[1];
                if (!AllIDs.Contains(ID))
                {
                    UniqueList.Add(contentControl);
                }
                AllIDs.Add(ID);

            }
            return UniqueList;
        }

        #endregion

        #region Get from doc
        /// <summary>
        /// Gets a list of ContentControls representing all citations in the document, including thoes outside main body, footnotes, and endnotes
        /// </summary>
        public List<ContentControl> GetAllCitesFromDoc_Unordered()
        {
            List<ContentControl> citationCCs = new List<ContentControl>();

            string StartsWithString = "CITE:";

            foreach (Range story in _app.ActiveDocument.StoryRanges)
            {
                foreach (ContentControl contentControl in story.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        citationCCs.Add(contentControl);
                    }
                }
            }
            return citationCCs;
        }

        /// <summary>
        /// Gets a list of ContentControls from the main body, footnotes, and endnotes, ordered by location reference
        /// </summary>
        public List<object> GetCitesFromDoc_Ordered(CitationRepository Repository)
        {
            var PosRefList = new List<CitePositionReference>();

            string StartsWithString = "CITE:";

            foreach (ContentControl contentControl in _app.ActiveDocument.ContentControls)
            {
                if (contentControl.Tag != null && contentControl.Tag.StartsWith(StartsWithString))
                {
                    int CCReference = contentControl.Range.Start;
                    string CiteID = contentControl.Tag.Split('|')[1];
                    Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                    PosRefList.Add(new CitePositionReference(contentControl, CCReference, citation: cite));
                }
            }

            foreach (Footnote note in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int DocReference = note.Reference.Start;
                        int RangeReference = contentControl.Range.Start;
                        string CiteID = contentControl.Tag.Split('|')[1];
                        Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                        PosRefList.Add(new CitePositionReference(contentControl, DocReference, RangeReference, cite));
                    }
                }
            }

            foreach (Endnote note in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int DocReference = note.Reference.Start;
                        int RangeReference = contentControl.Range.Start;
                        string CiteID = contentControl.Tag.Split('|')[1];
                        Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                        PosRefList.Add(new CitePositionReference(contentControl, DocReference, RangeReference, cite));
                    }
                }
            }

            PosRefList = PosRefList.OrderBy(n => n.DocumentReferencePoint).ThenBy(n => n.RangeReferencePoint).ToList();
            var CCList = new List<object>();
            foreach (CitePositionReference positionReference in PosRefList)
            {
                CCList.Add(positionReference.contentControl);
            }
            return CCList;
        }

        /// <summary>
        /// Gets a list of unique Citations type Exhibit from ContnetControls in the main body, footnotes, and endnotes and orders them by location reference so indexof(Citation) provides zero-based Cite Formatting index
        /// </summary>
        public List<Citation> GetListForExhibitIndex( CitationRepository Repository)
        {
            var PosRefList = new List<CitePositionReference>();

            string StartsWithString = "CITE:Exhibit";

            foreach (ContentControl contentControl in _app.ActiveDocument.ContentControls)
            {
                if (contentControl.Tag != null && contentControl.Tag.StartsWith(StartsWithString))
                {
                    int CCReference = contentControl.Range.Start;
                    string CiteID = contentControl.Tag.Split('|')[1];
                    Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                    PosRefList.Add(new CitePositionReference(contentControl, CCReference, citation: cite));
                }
            }

            foreach (Footnote note in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int DocReference = note.Reference.Start;
                        int RangeReference = contentControl.Range.Start;
                        string CiteID = contentControl.Tag.Split('|')[1];
                        Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                        PosRefList.Add(new CitePositionReference(contentControl, DocReference, RangeReference, cite));
                    }
                }
            }

            foreach (Endnote note in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int DocReference = note.Reference.Start;
                        int RangeReference = contentControl.Range.Start;
                        string CiteID = contentControl.Tag.Split('|')[1];
                        Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                        PosRefList.Add(new CitePositionReference(contentControl, DocReference, RangeReference, cite));
                    }
                }
            }

            PosRefList = PosRefList.OrderBy(n => n.DocumentReferencePoint).ThenBy(n => n.RangeReferencePoint).ToList();
            List<string> IDs = new List<string>();
            var ExhibitList = new List<Citation>();
            for (int i = 0; i<PosRefList.Count; i++)
            {
                if (!IDs.Contains(PosRefList[i].citation.ID))
                {
                    ExhibitList.Add(PosRefList[i].citation);
                }
                IDs.Add(PosRefList[i].citation.ID);
            }
            return ExhibitList;
        }

        /// <summary>
        /// Returns the index for an Exhibit cite to be used for Exhibit numbering
        /// </summary>
        public int GetExhibitIndex(Citation citation, CitationRepository Repository)
        {
            if (citation.CiteType == CiteType.Exhibit)
            {
                try
                {
                    List<string> ExhibitIds = new List<string>();
                    foreach (Citation exhibit in GetListForExhibitIndex(Repository))
                    {
                        ExhibitIds.Add(exhibit.ID);
                    }
                    return ExhibitIds.IndexOf(citation.ID);
                }
                catch
                { return 0; }
            }
            else
            { return 0; }
        }

        /// <summary>
        /// Returns the CitePlacementType (Enum: Long, Short, Id) for the given ContentControl
        /// </summary>
        public CitePlacementType GetLongShorOrId(ContentControl contentControl, CitationRepository Repository)
        {
            try
            {
                var InputCiteID = GetCitationIDFromContentControl(contentControl);
                var OrderedCiteContentControls = GetCitesFromDoc_Ordered(Repository);

                List<string> CCIDsList = new List<string>();
                foreach (ContentControl cc in OrderedCiteContentControls)
                {
                    CCIDsList.Add(cc.ID);
                }

                var contentControlIndex = CCIDsList.IndexOf(contentControl.ID); //TODO: This seems to always return -1 (not found)


                List<string> PreceedingIDs = new List<string>();
                for (int i = 0; i < contentControlIndex; i++)
                {
                    var CCCiteID = GetCitationIDFromContentControl((ContentControl)OrderedCiteContentControls[i]);
                    PreceedingIDs.Add(CCCiteID);
                }

                if (PreceedingIDs.Last() == InputCiteID)
                {
                    return CitePlacementType.Id;
                }
                else if (PreceedingIDs.Contains(InputCiteID))
                {
                    return CitePlacementType.Short;
                }
                else
                {
                    return CitePlacementType.Long;
                }
            }
            catch
            {
                log.Error("ContentControl " + contentControl.ID + " caused error and format could not be determined.");
                return CitePlacementType.Long;
            }
        }

        /// <summary>
        /// Gets all the ContentControls associated with a specific citation
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        public List<ContentControl> GetContentControlsForCite(Citation citation)
        {
            List<ContentControl> citationCCs = GetAllCitesFromDoc_Unordered().Where(n => GetCitationIDFromContentControl(n) == citation.ID).ToList(); ;

            return citationCCs;
        }

        /// <summary>
        /// Grabs all Citation ContentControls in the specified range
        /// </summary>
        public List<ContentControl> FindCiteCCsInRange(Range range)
        {
            List<ContentControl> contentControls = new List<ContentControl>();
            foreach (ContentControl CC in range.ContentControls)
            {
                if (CC.Tag!=null && CC.Tag.Contains("CITE:"))
                {
                    contentControls.Add(CC);
                }
            }
            return contentControls;
        }

        #endregion
        #region Change doc

        /// <summary>
        /// Inserts a Citation ContentControl at the Active Document selection
        /// </summary>
        public ContentControl InsertCiteAtSelection(Citation citation, CitationRepository Repository)
        {
            log.Info("Citation Inserted: " + citation.ID);

            int index = GetExhibitIndex(citation, Repository);

            ContentControl CC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
            SetCiteCCTagTitleColor(CC, citation, false);

            CitePlacementType placementType = CitePlacementType.Long; /*GetLongShorOrId(CC, Repository); //set to Long becuase CC is not inserted into the doc yet. Refresh must be called before it can be found in GetCitesFromDoc_Ordered */

            Range LeadingForId = CC.Range;

            CC.Range.Text = Repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index);
            CiteFormatting.FormatFont(CC);

            //Pincite omitted from the formatting as inital cites should not have it
            SetPincite(CC, null);

            AddHyperlink(CC, citation);

            return CC;
        }

        /// <summary>
        /// Updates all citation ContentControls in the document
        /// </summary>
        public void UpdateCitesInDoc(CitationRepository repository)
        {
            log.Info("Updating Citations in Doc. Name: " + _app.ActiveDocument.FullName + " ActiveDoc CC Count: " + _app.ActiveDocument.ContentControls.Count);

            var allCites = GetAllCitesFromDoc_Unordered();
            foreach (ContentControl cc in allCites)
            {
                var CCCiteID = GetCitationIDFromContentControl(cc);
                Citation citation = repository.Citations.Where(n => n.ID == CCCiteID).FirstOrDefault();

                CitePlacementType placementType = GetLongShorOrId(cc, repository);

                Range LeadingForId = cc.Range;

                int index = 0;
                if (citation.CiteType == CiteType.Exhibit)
                {
                    index = GetExhibitIndex(citation, repository);
                }

                cc.Range.Text = repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index);

                ContentControl Pincite = null;
                if (CCHasPincite(cc))
                {
                    Pincite = cc.Range.ContentControls[1];
                }

                CiteFormatting.FormatFont(cc);
                SetPincite(cc, Pincite);
            }
        }

        /// <summary>
        /// If Selection == null, removes all citation CCs from document, leaving the text, after a pop-up to confirm
        /// </summary>
        public void RemoveCitesFromDoc(Selection Selection = null)
        {
            List<ContentControl> ccs = new List<ContentControl>();

            if (Selection == null)
            {
                foreach (Range story in _app.ActiveDocument.StoryRanges)
                {
                    foreach (ContentControl cc in story.ContentControls)
                    {
                        ccs.Add(cc);
                    }
                }
            } else
            {
                foreach (ContentControl cc in Selection.ContentControls)
                {
                    ccs.Add(cc);
                }
            }

            foreach (ContentControl cc in ccs)
            {
                if (cc.Tag != null && cc.Tag.Contains("CITE:"))
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

        #region Pincite

        private void SetPincite(ContentControl citeCC, ContentControl pinCC = null )
        {
            citeCC.LockContents = false;
            citeCC.Range.Select();
            var find = _app.Selection.Find;
            find.ClearFormatting();
            find.Replacement.ClearFormatting();
            find.Text = @"{{PIN}}";

            if (citeCC.Tag.ToUpper().Contains("PIN:FALSE"))
            {
                find.Replacement.Text = "";
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            else if (pinCC == null)
            {
                find.Execute();
                var newPinCC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
                newPinCC.SetPlaceholderText(Text: "{ type Pincite text }");
                SetPinCCTagTitleColor(newPinCC);
                newPinCC.Range.Text = "";
                newPinCC.LockContents = false;
            }
            else
            {
                pinCC.Copy();
                find.Execute();
                
                var newPinCC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
                newPinCC.SetPlaceholderText(Text: "{ type Pincite text }");
                SetPinCCTagTitleColor(newPinCC);
                newPinCC.Range.Paste();
                newPinCC.LockContents = false;

            }
            citeCC.LockContents = true;
        }

        public void AddPincite(ContentControl CiteCC, CitationRepository Repository = null)
        {
            if (Repository == null)
            {
                Repository = new CitationRepository(_app);
            }

            var citeCCID = CiteCC.Tag.Split('|')[1];
            var citation = Repository.Citations.FirstOrDefault(n => n.ID == citeCCID);

            CiteCC.LockContents = false;
            int index = GetExhibitIndex(citation, Repository);

            SetCiteCCTagTitleColor(CiteCC, citation, true);

            CitePlacementType placementType = GetLongShorOrId(CiteCC, Repository);

            Range LeadingForId = CiteCC.Range;

            CiteCC.Range.Text = Repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index);
            CiteFormatting.FormatFont(CiteCC);
            SetPincite(CiteCC);

            AddHyperlink(CiteCC, citation);

            CiteCC.LockContents = true;
        }

        public void RemovePincite(ContentControl CiteCC)
        {
            if (CiteCC != null)
            {
                bool hasPinciteCC = bool.Parse(CiteCC.Tag.Split('|')[2].Substring(4));
                if (hasPinciteCC)
                {
                    CiteCC.LockContents = false;

                    ContentControl Pincite = CiteCC.Range.ContentControls[1];
                    Pincite.Delete(true);

                    CiteCC.Tag = CiteCC.Tag.Replace("PIN:True", "PIN:False");
                    CiteCC.LockContents = true;

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No Pincite was found in the selection.");
                }
            }
        }

        #endregion

        #endregion

        #region Exhibit Index
        public void InsertExhibitIndex(CitationRepository Repository = null)
        {
            if(Repository == null)
            {
                Repository = new CitationRepository(_app);
            }

            try
            {
                var exhibits = GetListForExhibitIndex(Repository);

                _app.ActiveDocument.Tables.Add(_app.Selection.Range, 2, 2, WdDefaultTableBehavior.wdWord9TableBehavior, WdAutoFitBehavior.wdAutoFitFixed);
                _app.Selection.Font.Bold = (int)WdConstants.wdToggle;
                _app.Selection.TypeText("Exhibit No.");
                _app.Selection.MoveRight(WdUnits.wdCell);
                _app.Selection.Font.Bold = (int)WdConstants.wdToggle;
                _app.Selection.TypeText("Exhibit Description");
                _app.Selection.MoveRight(WdUnits.wdCell);

                var Description = string.Empty;
                var Numbering = Repository.CiteFormatting.ExhibitIndexStyle;
                int IndexStart = Repository.CiteFormatting.ExhibitIndexStart;
                

                foreach (var exhibit in exhibits)
                {
                    Description = exhibit.LongDescription;
                    int index = GetExhibitIndex(exhibit, Repository);

                    _app.Selection.TypeText(CiteFormatting.ApplyNumFormat(index, Numbering));
                    _app.Selection.MoveRight(WdUnits.wdCell);
                    _app.Selection.TypeText(Description);

                }
            }
            catch 
            {
                log.Error("Error Adding Exhibit Index");
                System.Windows.Forms.MessageBox.Show("An error occurred. Please contact Prelimine if the error persists."); 
            }


        }

        #endregion
    }
}
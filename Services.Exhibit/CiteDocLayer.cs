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
        public CitationRepository repository { get; private set; }

        public CiteDocLayer(Application application)
        {
            this._app = application;
            this.repository = new CitationRepository(_app);

        }

        #region Get from doc
        public List<ContentControl> GetAllCitesFromDoc_Unordered(CiteType citeType)
        {
            List<ContentControl> citationCCs = new List<ContentControl>();
            string type = "";
            // Allows for CiteType.None and CiteType.All to return all Cite types. Is the default case. Else, Function will return only cites of CiteType selected.
            
                type = citeType.ToString();
            
            string StartsWithString = "CITE:" + type;


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
        /// Gets an ordered list of all Cite Content Controls of certain CiteType from the main body, footnotes, and endnotes
        /// </summary>
        /// <param name="citeType"></param>
        /// <returns>CitePositionReference contains ContentControl and location reference. Citation = null.</returns>
        public List<ContentControl> GetCitesFromDoc_Ordered(CiteType citeType)
        {
            var PosRefList = new List<CitePositionReference>();

            string type = "";
            // Allows for CiteType.None and CiteType.All to return all Cite types. Is the default case. Else, Function will return only cites of CiteType selected.

                type = citeType.ToString();

            string StartsWithString = "CITE:" + type;

            foreach (ContentControl contentControl in _app.ActiveDocument.ContentControls)
            {
                if (contentControl.Tag.StartsWith(StartsWithString))
                {
                    int CCReference = contentControl.Range.Start;
                    string CiteID = contentControl.Tag.Split('|')[1];
                    Citation cite = repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                    PosRefList.Add(new CitePositionReference(contentControl, CCReference, cite));
                }
            }

            foreach (Footnote note in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int CCReference = note.Reference.Start + contentControl.Range.Start;
                        string CiteID = contentControl.Tag.Split('|')[1];
                        Citation cite = repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                        PosRefList.Add(new CitePositionReference(contentControl, CCReference, cite));
                    }
                }
            }

            foreach (Endnote note in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int CCReference = note.Reference.Start + contentControl.Range.Start;
                        string CiteID = contentControl.Tag.Split('|')[1];
                        Citation cite = repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
                        PosRefList.Add(new CitePositionReference(contentControl, CCReference, cite));
                    }
                }
            }

            PosRefList = PosRefList.OrderBy(n => n.DocumentReferencePoint).ToList();
            var CCList = new List<ContentControl>();
            foreach (CitePositionReference positionReference in PosRefList)
            {
                CCList.Add(positionReference.contentControl);
            }
            return CCList;
        }

        /// <summary>
        /// Index of citation in list is the position it first appears in the document
        /// </summary>
        public List<ContentControl> GetUniqueCitesFromDoc_Ordered(List<ContentControl> CCList)
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

        /// <summary>
        /// Gets the collects the index of the first time each exhibit is entered in the document
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        public CitePlacementType GetCitePlacementTypeFromDoc(ContentControl contentControl)
        {
            var InputCiteID = GetCitationIDFromContentControl(contentControl);
            var OrderedCiteContentControls = GetCitesFromDoc_Ordered(CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other);
            var contentControlIndex = OrderedCiteContentControls.IndexOf(contentControl);

            List<string> TagsTrimmed = new List<string>();
            for (int i = 0; i < contentControlIndex; i++)
            {
                var CCCiteID = GetCitationIDFromContentControl(OrderedCiteContentControls[i]);
                TagsTrimmed.Add(CCCiteID);
            }

            if (TagsTrimmed.Last() == InputCiteID)
            {
                return CitePlacementType.Id;
            }
            else if (TagsTrimmed.Contains(InputCiteID))
            {
                return CitePlacementType.Short;
            }
            else
            {
                return CitePlacementType.Long;
            }

        }

        public string GetCitationIDFromContentControl(ContentControl contentControl)
        {
            return contentControl.Tag.Split('|')[1];
        }


        public int GetCitationIndexFromDoc(Citation citation)
        {
            if (citation.CiteType == CiteType.Exhibit)
            {
                var CitationsList = GetCitesFromDoc_Ordered(citation.CiteType);
                var UniqueCitationsList = GetUniqueCitesFromDoc_Ordered(CitationsList);

                var FirstCiteRef = UniqueCitationsList.Where(n => n.Tag.Split('|')[1] == citation.ID).FirstOrDefault();
                if (FirstCiteRef != default)
                {
                    return UniqueCitationsList.IndexOf(FirstCiteRef);
                }
                else return 0;
            }
            else return 0;
        }

        public List<ContentControl> GetAllCiteContentControls(Citation citation)
        {
            List<ContentControl> citationCCs = GetAllCitesFromDoc_Unordered(citation.CiteType).Where(n => n.Tag.Split('|')[1] == citation.ID).ToList(); ;

            return citationCCs;
        }

        public ContentControl FindCiteCCInRange(Range range)
        {
            //List<ContentControl> contentControls = new List<ContentControl>();
            var CCs = (List<ContentControl>)range.ContentControls;
            //foreach (ContentControl CC in CCs)
            //{
            //    contentControls.Add(CC);
            //}

            return CCs.Where(n => n.Tag.StartsWith("CITE:")).FirstOrDefault();
        }

        #endregion
        #region Change doc

        public void SetContentControlTag(ContentControl contentControl, Citation citation, bool HasPincite)
        {
            contentControl.Tag = "CITE:" + citation.CiteType.ToString() + "|" + citation.ID + "|" + HasPincite.ToString();
        }

        public void SetPinciteCCTag(ContentControl PinCC)
        {
            PinCC.Tag = "PIN";
        }
        public ContentControl InsertCiteAtSelection(Citation citation)
        {
            int index = GetCitationIndexFromDoc(citation);

            ContentControl CC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
            SetContentControlTag(CC, citation, false);
            CC.Title = citation.CiteType.ToString() + ": " + citation.LongDescription;
            CC.Color = WdColor.wdColorRed;

            CitePlacementType placementType = CitePlacementType.Long /* GetCitePlacementTypeFromDoc(CC)*/;

            Range LeadingForId = CC.Range;

            CC.Range.Text = repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index);
            repository.CiteFormatting.FormatFont(CC);

            return CC;
        }


        public ContentControl AddPincite(ContentControl CiteCC, Citation citation, string PinciteText)
        {
            CiteCC.LockContents = false;

            int index = GetCitationIndexFromDoc(citation);

            ContentControl CC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
            SetContentControlTag(CC, citation, true);

            CitePlacementType placementType = GetCitePlacementTypeFromDoc(CC);

            Range LeadingForId = CC.Range;

            CiteCC.Range.Text = repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index, true);

            var find = CiteCC.Range.Find;
            find.ClearFormatting();
            find.Text = @"{PINCITE}";
            find.Execute();

            ContentControl Pincite = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
            SetPinciteCCTag(Pincite);
            Pincite.SetPlaceholderText(Text: "Click to edit your Pincite text!");

            CiteCC.LockContents = true;
            return Pincite;
        }

        public void RemovePincite(ContentControl CiteCC)
        {
            bool hasPinciteCC = bool.Parse(CiteCC.Tag.Split('|')[2]);
            if (hasPinciteCC)
            {
                ContentControl Pincite = CiteCC.Range.ContentControls[1];
                Pincite.Delete(true);
                CiteCC.Tag = CiteCC.Tag.Substring(0, CiteCC.Tag.Split('|')[2].Length);
            }
        }

        public void UpdateCiteContentControls()
        {
            log.Info("Updating all Cite Content Controls in " + _app.ActiveDocument.FullName);

            var allCites = GetAllCitesFromDoc_Unordered(CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other);
            foreach (ContentControl cc in allCites)
            {
                var CCCiteID = GetCitationIDFromContentControl(cc);
                Citation citation = repository.Citations.Where(n => n.ID == CCCiteID).FirstOrDefault();

                int index = GetCitationIndexFromDoc(citation);
                CitePlacementType placementType = GetCitePlacementTypeFromDoc(cc);

                bool hasPincite = bool.Parse(cc.Tag.Split('|')[2]);
                ContentControl Pincite = null;
                if (hasPincite)
                {
                    Pincite = cc.Range.ContentControls[1];
                }

                Range LeadingForId = cc.Range;

                cc.Range.Text = repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index, hasPincite);

                repository.CiteFormatting.FormatFont(cc);
                if (hasPincite)
                {
                    Pincite = AddPincite(cc, citation, Pincite.Range.Text);
                }
            }
        }





        #endregion
    }
}
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
        public List<ContentControl> GetCitesFromDoc_Ordered(CiteType citeType, CitationRepository Repository)
        {
            var PosRefList = new List<CitePositionReference>();

            string type = "";
            // Allows for CiteType.None and CiteType.All to return all Cite types. Is the default case. Else, Function will return only cites of CiteType selected.

                type = citeType.ToString();

            string StartsWithString = "CITE:" + type;

            foreach (ContentControl contentControl in _app.ActiveDocument.ContentControls)
            {
                if (contentControl.Tag != null && contentControl.Tag.StartsWith(StartsWithString))
                {
                    int CCReference = contentControl.Range.Start;
                    string CiteID = contentControl.Tag.Split('|')[1];
                    Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
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
                        Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
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
                        Citation cite = Repository.Citations.Where(n => n.ID == CiteID).FirstOrDefault();
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
        public CitePlacementType GetCitePlacementTypeFromDoc(ContentControl contentControl, CitationRepository Repository)
        {
            var InputCiteID = GetCitationIDFromContentControl(contentControl);
            var OrderedCiteContentControls = GetCitesFromDoc_Ordered(CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other, Repository);
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


        public int GetCitationIndexFromDoc(Citation citation, CitationRepository Repository)
        {
            if (citation.CiteType == CiteType.Exhibit)
            {
                var CitationsList = GetCitesFromDoc_Ordered(citation.CiteType, Repository);
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

        public string SetContentControlTagTitle(ContentControl contentControl, Citation citation, bool HasPincite)
        {
            var tag = "CITE:" + citation.CiteType.ToString() + "|" + citation.ID + "|PIN:" + HasPincite.ToString();
            contentControl.Tag = tag;
            contentControl.Title = citation.CiteType.ToString() + ": " + citation.LongDescription;
            contentControl.Color = WdColor.wdColorLightBlue;
            return tag;
        }

        public ContentControl InsertCiteAtSelection(Citation citation, CitationRepository Repository)
        {
            int index = GetCitationIndexFromDoc(citation, Repository);

            ContentControl CC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
            SetContentControlTagTitle(CC, citation, false);

            CitePlacementType placementType = CitePlacementType.Long /*TODO: GetCitePlacementTypeFromDoc(CC)*/;

            Range LeadingForId = CC.Range;

            CC.Range.Text = Repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index);
            CiteFormatting.FormatFont(CC);

            //Pincite omitted from the formatting as inital cites should not have it
            SetPincite(CC, null);

            return CC;
        }



        public void UpdateCiteContentControls()
        {
            throw new Exception("have to add in PINCITE Identifier in the cite formatting -> PINCITE as bool rather than string, replace {{PIN}}");

            //log.Info("Updating all Cite Content Controls in " + _app.ActiveDocument.FullName);

            //var allCites = GetAllCitesFromDoc_Unordered(CiteType.Exhibit | CiteType.Legal | CiteType.Record | CiteType.Other);
            //foreach (ContentControl cc in allCites)
            //{
            //    var CCCiteID = GetCitationIDFromContentControl(cc);
            //    Citation citation = repository.Citations.Where(n => n.ID == CCCiteID).FirstOrDefault();

            //    int index = GetCitationIndexFromDoc(citation);
            //    CitePlacementType placementType = GetCitePlacementTypeFromDoc(cc);

            //    bool hasPincite = bool.Parse(cc.Tag.Split('|')[2]);
            //    ContentControl Pincite = null;
            //    if (hasPincite)
            //    {
            //        Pincite = cc.Range.ContentControls[1];
            //    }

            //    Range LeadingForId = cc.Range;

            //    cc.Range.Text = repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index, hasPincite);

            //    repository.CiteFormatting.FormatFont(cc);
            //    if (hasPincite)
            //    {
            //        Pincite = AddPincite(cc, citation, Pincite.Range.Text);
            //    }
            //}
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
                newPinCC.Color = WdColor.wdColorDarkBlue;
                newPinCC.SetPlaceholderText(Text: "{ type Pincite text }");
                SetPinciteCCTag(newPinCC);
                newPinCC.Range.Text = "";
                newPinCC.LockContents = false;
            }
            else
            {
                pinCC.Copy();
                find.Execute();
                
                var newPinCC = _app.Selection.ContentControls.Add(WdContentControlType.wdContentControlRichText);
                newPinCC.Color = WdColor.wdColorDarkBlue;
                newPinCC.SetPlaceholderText(Text: "{ type Pincite text }");
                SetPinciteCCTag(newPinCC);
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
            int index = GetCitationIndexFromDoc(citation, Repository);

            SetContentControlTagTitle(CiteCC, citation, true);

            CitePlacementType placementType = CitePlacementType.Long /* GetCitePlacementTypeFromDoc(CC)*/;

            Range LeadingForId = CiteCC.Range;

            CiteCC.Range.Text = Repository.CiteFormatting.FormatCiteText(citation, placementType, LeadingForId, index);
            CiteFormatting.FormatFont(CiteCC);
            SetPincite(CiteCC);

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

        public string SetPinciteCCTag(ContentControl PinCC)
        {
            string tag = "PIN";
            PinCC.Tag = tag;
            PinCC.Title = "PIN";
            return tag;
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
            else if(selection.ContentControls[1].Tag.StartsWith("CITE"))
            {
                CiteCC = selection.ContentControls[1];
            }

            return CiteCC;
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
                List<ContentControl> exhibits = GetUniqueCitesFromDoc_Ordered(GetCitesFromDoc_Ordered(CiteType.Exhibit,Repository));
                List<string> tags = new List<string> { "FillItem" };

                _app.ActiveDocument.Tables.Add(_app.Selection.Range, 2, 2, WdDefaultTableBehavior.wdWord9TableBehavior, WdAutoFitBehavior.wdAutoFitFixed);
                _app.Selection.Font.Bold = (int)WdConstants.wdToggle;
                _app.Selection.TypeText("Exhibit No.");
                _app.Selection.MoveRight(WdUnits.wdCell);
                _app.Selection.Font.Bold = (int)WdConstants.wdToggle;
                _app.Selection.TypeText("Exhibit Description");
                _app.Selection.MoveRight(WdUnits.wdCell);

                var exhibitCount = exhibits.Count();
                var Description = string.Empty;
                var Numbering = Repository.CiteFormatting.ExhibitIndexStyle;
                int Index = 0;

                foreach (var exhibit in exhibits)
                {
                    var repoExhibit = Repository.Citations.FirstOrDefault(n => n.ID == exhibit.Tag.Substring(8));

                    Description = repoExhibit.LongDescription;

                    exhibitCount--;

                    tags.Add(exhibit.Tag);
                    Index = tags.Count - 1;

                    _app.Selection.TypeText(CiteFormatting.ApplyNumFormat(Index, Numbering));
                    _app.Selection.MoveRight(WdUnits.wdCell);
                    _app.Selection.TypeText(Description);

                    if (exhibitCount > 0)
                        _app.Selection.MoveRight(WdUnits.wdCell);

                }

            }
            catch { System.Windows.Forms.MessageBox.Show("Please select an editable range."); }


        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Tools.Citation
{
    public class CiteHelper
    {
        public Application _app { get; private set; }

        public CiteHelper(Application application)
        {
            this._app = application;
        }

        /// <summary>
        /// Gets an ordered list of all Cite Content Controls from the main body, footnotes, and endnotes
        /// </summary>
        /// <param name="citeType"></param>
        /// <returns></returns>
        public List<CitePositionReference> GetCites_Ordered(CiteType citeType = CiteType.All)
        {
            var CCList = new List<CitePositionReference>();

            string type = "";
            if(citeType != CiteType.None && citeType != CiteType.All)
            {
                type = citeType.ToString();
            }
            string StartsWithString = "CITE:" + type;

            foreach (ContentControl contentControl in _app.ActiveDocument.ContentControls)
            {
                if (contentControl.Tag.StartsWith(StartsWithString))
                {
                    int CCReference = contentControl.Range.Start;
                    CCList.Add(new CitePositionReference(contentControl, CCReference));
                }
            }

            foreach (Footnote note in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl contentControl in note.Range.ContentControls)
                {
                    if (contentControl.Tag.StartsWith(StartsWithString))
                    {
                        int CCReference = note.Reference.Start + contentControl.Range.Start;
                        CCList.Add(new CitePositionReference(contentControl, CCReference));
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
                        CCList.Add(new CitePositionReference(contentControl, CCReference));
                    }
                }
            }

            CCList = CCList.OrderBy(n => n.DocumentReferencePoint).ToList();
            return CCList;
        }

        /// <summary>
        /// Gets the collects the index of the first time each exhibit is entered in the document
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        public int? GetExhibitNumber(Citation citation)
        {
            if (citation.CiteType != CiteType.Exhibit)
            {
                throw new Exception("Citation is not of CiteType.Exhibit");
            }
            else
            {
                var ExhibitList = GetCites_Ordered(CiteType.Exhibit);

                var FirstCiteRef = ExhibitList.Where(n => n.citation == citation).FirstOrDefault();
                if (FirstCiteRef != default)
                {
                    return ExhibitList.IndexOf(FirstCiteRef);
                }
                else return 0;
            }
        }
    }


    [Flags]
    public enum CiteType
    {
        None = 0,
        Exhibit = 1,
        Legal = 2,
        Record = 4,
        Other = 8,

        All = Exhibit | Legal | Record | Other,
        Outside = Legal | Record | Other,
    }

    [Flags]
    public enum ExhibitIndexStyle
    {
        Empty = 0,
        Numbers = 1,
        Letters = 2,
        Roman = 4,
    }

    [Flags]
    public enum FormatNode
    {
        Intro = 1,
        Long = 2,
        Short = 4,
        IndexStyle = 8,
        IndexStart = 16,
        Parentheses = 32,
        IdCite = 64
    }
}

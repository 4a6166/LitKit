using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Exhibit
{
    public class CitationReference
    {
        public CitationReference (string ID, int RangeStart, int NoteRangeStart, string CcId, CiteType citeType)
        {
            this.ID = ID;
            this.RangeStart = RangeStart;
            this.NoteRangeStart = NoteRangeStart;
            this.CcId = CcId;
            this.citeType = citeType;
        }
        public string ID;
        public int RangeStart;
        public int NoteRangeStart;
        public string CcId;
        public CiteType citeType;
    }
}

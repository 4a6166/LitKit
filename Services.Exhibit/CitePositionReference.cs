using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Citation
{
    public class CitePositionReference
    {
        public ContentControl contentControl { get; private set; }
        public Citation citation { get; private set; }
        public int DocumentReferencePoint { get; private set; }
        public int RangeReferencePoint { get; private set; }


        public CitePositionReference(ContentControl contentControl, int DocumentReferencePoint, int RangeReferencePoint = 0, Citation citation = null)
        {
            this.contentControl = contentControl;
            this.DocumentReferencePoint = DocumentReferencePoint;
            this.RangeReferencePoint = RangeReferencePoint;
            this.citation = citation;
        }
    }
}

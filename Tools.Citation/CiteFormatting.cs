using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Citation
{
    public class CiteFormatting
    {
        public string ExhibitIntro { get; set; }
        public string ExhibitLongFormat { get; set; }
        public string ExhibitShortFormat { get; set; }
        public ExhibitIndexStyle ExhibitIndexStyle { get; set; }
        public int ExhibitIndexStart { get; set; }

        public bool hasSurroundingParentheses { get;  set; }

        public bool hasIdCite { get; set; }

        public CiteFormatting(string ExhibitIntro, string ExhibitLongFormat, string ExhibitShortFormat, ExhibitIndexStyle ExhibitIndexStyle = ExhibitIndexStyle.Numbers, int ExhibitIndexStart = 0, bool HasSurroundingParentheses = false, bool HasIdCite = true)
        {
            this.ExhibitIntro = ExhibitIntro;
            this.ExhibitLongFormat = ExhibitLongFormat;
            this.ExhibitShortFormat = ExhibitShortFormat;
            this.ExhibitIndexStyle = ExhibitIndexStyle;
            this.ExhibitIndexStart = ExhibitIndexStart;
            this.hasSurroundingParentheses = HasSurroundingParentheses;
            this.hasIdCite = HasIdCite;
        }

    }
}

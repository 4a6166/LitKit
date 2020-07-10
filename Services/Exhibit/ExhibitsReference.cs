using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exhibit
{
    public class ExhibitsReference
    {
        public ExhibitsReference()
        {

        }
        public ExhibitsReference (string ExhibtId, int RangeStart, int NoteRangeStart, string CcId)
        {
            this.ExhibtId = ExhibtId;
            this.RangeStart = RangeStart;
            this.NoteRangeStart = NoteRangeStart;
            this.CcId = CcId;
        }
        public string ExhibtId = string.Empty;
        public int RangeStart = 0;
        public int NoteRangeStart = 0;
        public string CcId = string.Empty;
    }
}

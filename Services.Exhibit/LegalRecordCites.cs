using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Tools.Exhibit
{
    public class LegalRecordCite
    {

        public LegalRecordCite(string LongCite, string ShortCite)
        {
            this.ID = Guid.NewGuid().ToString();
            this.LongCite = LongCite;
            this.ShortCite = ShortCite;
        }

        public LegalRecordCite(string ID)
        {
            this.ID = ID;
        }

        public string ID { get; set; }
        public string LongCite { get; set; }
        public string ShortCite { get; set; }


    }
}

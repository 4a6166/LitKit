using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Response
{
    public class ResponseStandard
    {
        public ResponseStandard(string ID, string Name, List<bool> DocType, List<string> Texts, string[,] Verbs)
        {
            this.ID = ID;
            this.Name = Name;
            this.DocType = DocType;
            this.Texts = Texts;
            this.Verbs = Verbs;
        }

        public string ID { get; private set; }
        public string Name { get; set; }
        public List<bool> DocType;

        public List<string> Texts;
        public string[,] Verbs;

    }
}

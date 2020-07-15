using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Services.Answers
{
    public class Answer
    {
        public Answer(string Name, string Text, bool Singular)
        {
            this.Name = Name;
            this.Text = Text;
            this.Singular = Singular;
            this.ID = Guid.NewGuid().ToString();
        }

        public Answer (string ID, Word.Application _app)
        {

            var ans = new AnsRespository(_app).GetAnswer(ID);
            this.ID = ID;
            this.Name = ans.Name;
            this.Text = ans.Text;
            this.Singular = ans.Singular;
        }

        public string ID { get; private set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Singular { get; set; }


    }
}

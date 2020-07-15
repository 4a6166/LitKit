using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Answer (string ID)
        {
            this.ID = ID;
            this.Name = "";
            this.Text = "";
            Singular = false;
            throw new NotImplementedException();
        }
        public string ID;
        public string Name;
        public string Text;
        public bool Singular;


    }
}

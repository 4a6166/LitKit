using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citation.TESTResources
{
    public class CiteRepository
    {
        public List<Cite> GetCites()
        {
            List<Cite> cites = new List<Cite>();

            for (int i = 0; i < 5; i++)
            {
                string type = "Exhibit";
                Cite cite = new Cite()
                {
                    CiteType = type,
                    ID = "123_" + i.ToString(),
                    LongDescription = "Long Description " + type +" " + i.ToString(),
                    ShortDescription = "Short " + type+" " + i.ToString(),
                    OtherIdentifier = "Blank",
                    ReferenceName = "Blank"
                };
                cites.Add(cite);
            }

            for (int i = 0; i < 5; i++)
            {
                string type = "Legal";
                Cite cite = new Cite()
                {
                    CiteType = type,
                    ID = "123_" + i.ToString(),
                    LongDescription = "Long Description " + type + " " + i.ToString(),
                    ShortDescription = "Short " + type + " " + i.ToString(),
                    OtherIdentifier = "Blank",
                    ReferenceName = "Blank"
                };
                cites.Add(cite);
            }

            for (int i = 0; i < 5; i++)
            {
                string type = "Record";
                Cite cite = new Cite()
                {
                    CiteType = type,
                    ID = "123_" + i.ToString(),
                    LongDescription = "Long Description " + type + " " + i.ToString(),
                    ShortDescription = "Short " + type + " " + i.ToString(),
                    OtherIdentifier = "Blank",
                    ReferenceName = "Blank"
                };
                cites.Add(cite);
            }

            return cites;
        }
    }
}

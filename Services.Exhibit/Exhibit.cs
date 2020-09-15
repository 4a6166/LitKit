using System;
using System.Xml.Linq;

namespace Services.Exhibit
{

    public class Exhibit
    {
        /// <summary>
        /// For adding Exhibits to the repository
        /// </summary>
        /// <param name="Desc"></param>
        /// <param name="BatesNo"></param>
        public Exhibit(string Desc, string BatesNo)
        {
            this.Description = Desc;
            this.BatesNumber = BatesNo;
            ID = SetID();     
        }

        /// <summary>
        /// For getting Exhibits from the repository
        /// </summary>
        /// <param name="ID"></param>
        public Exhibit(string ID)
        {
            this.ID = ID;
        }


        public XElement MakeXElement()
        {
            return new XElement("Exhibit",
                        new XElement("ID", ID),
                        new XElement("Description", Description),
                        new XElement("Bates", BatesNumber)
                        );
        }

        private string SetID()
        {
            return Guid.NewGuid().ToString();
        }

        public string ID { get; private set; }
        public string Description { get; set; }
        public string BatesNumber { get; set; }

        
    }
}

using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Application = Microsoft.Office.Interop.Word.Application;

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
            return ShortGuid.NewShortGuid().ToString();
        }

        public string ID { get; private set; }
        public string Description { get; set; }
        public string BatesNumber { get; set; }

        
    }
}

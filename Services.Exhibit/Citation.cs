using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Citation
{
    public class Citation
    {
        public string ID { get; private set; }

        #region User to enter
        /// <summary>
        /// To be used as a quick reference name
        /// </summary>
        public string ReferenceName { get; set; }

        public CiteType CiteType { get; set; }

        /// <summary>
        /// To be inserted with the long cite
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// To be inserted with the short cite
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Not to be inserted into the document (eg. Bates no.)
        /// </summary>
        public string OtherIdentifier { get; set; }
        #endregion

        public Citation(string ID, CiteType CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName="")
        {
            this.ID = ID;
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;
        }

        public Citation (CiteType CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName = "")
        {
            this.ID = Guid.NewGuid().ToString();
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;
        }

        public string GetCCTag()
        {
            return "CITE:" + CiteType.ToString() + "|" + ID;
        }


         
    }
}

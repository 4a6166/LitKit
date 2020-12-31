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

        #region For Cite Block
        //TODO/////////////////////////////////////////////////////////////////////////////////////////////////////
        public string InsertedCount { get; set; }

        public string LongCiteExample { get; set; }

        public string CiteTypeText { get; set; }

        #endregion

        public Citation(string ID, CiteType CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName="")
        {
            this.ID = ID;
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;

            InsertedCount = "1";
            SetExampleCite();

            CiteTypeText = CiteType.ToString();
        }

        public Citation (CiteType CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName = "")
        {
            this.ID = Guid.NewGuid().ToString();
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;

            InsertedCount = "1";
            SetExampleCite();

            CiteTypeText = CiteType.ToString();
        }

        public string GetCCTag()
        {
            return "CITE:" + CiteType.ToString() + "|" + ID;
        }

        private void SetExampleCite()
        {
            //TODO: update to change exhibit intro and formatting

            switch (CiteType)
            {
                case CiteType.Exhibit:
                    LongCiteExample = $"Exhibit {InsertedCount}, {LongDescription} (ABC00001)";
                    break;
                default:
                    LongCiteExample = CiteType.ToString() + ": " + LongDescription;
                    break;
            }
        }
         
    }
}

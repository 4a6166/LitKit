using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Exhibit
{
    /// <summary>
    /// Test since Tools.Citation did not work. Expected issue: downgrading from Framework 4.8
    /// </summary>
    public class Citation
    {
        public string ID { get; private set; }

        #region User to enter
        /// <summary>
        /// To be used as a quick reference name
        /// </summary>
        public string ReferenceName { get; set; }

        public CiteTypea CiteType { get; set; }

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

        public Citation(string ID, CiteTypea CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName="")
        {
            this.ID = ID;
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;
        }

        public Citation (CiteTypea CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName = "")
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

    [Flags]
    public enum CiteTypea
    {
        None = 0,
        Exhibit = 1,
        Legal = 2,
        Record = 4,
        Other = 8,

        All = Exhibit | Legal | Record | Other,
        Outside = Legal | Record | Other,
    }

    [Flags]
    public enum ExhibitIndexStyle
    {
        Empty = 0,
        Numbers = 1,
        Letters = 2,
        Roman = 4,
    }

    [Flags]
    public enum FormatNode
    {
        Intro = 1,
        Long = 2,
        Short = 4,
        IndexStyle = 8,
        IndexStart = 16,
        Parentheses = 32,
        IdCite = 64
    }

    [Flags]
    public enum CitePlacementType
    {
        None = 0,
        Long = 1,
        Short = 2,
        Id = 4
    }

}

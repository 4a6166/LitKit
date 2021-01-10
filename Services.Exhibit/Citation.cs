using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Citation
{
    public class Citation : INotifyPropertyChanged
    {
        #region private properties
        string _ID;
        string _ReferenceName;
        CiteType _CiteType;
        string _LongDescription;
        string _ShortDescription;
        string _OtherIdentifier;
        int _InsertedCount;
        string _LongCiteExample;

        #endregion
        public string ID 
        {
            get
            {
                return _ID;
            }
            private set
            {
                _ID = value;
                OnPropertyChanged("ID");
            }
        }

        #region User to enter
        /// <summary>
        /// To be used as a quick reference name
        /// </summary>
        public string ReferenceName
        {
            get
            {
                return _ReferenceName;
            }
            private set
            {
                _ReferenceName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ReferenceName"));
            }
        }

        public CiteType CiteType
        {
            get
            {
                return _CiteType;
            }
            private set
            {
                _CiteType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CiteType"));
            }
        }

        /// <summary>
        /// To be inserted with the long cite or, for Exhibits, the short cite
        /// </summary>
        public string LongDescription
        {
            get
            {
                return _LongDescription;
            }
            set
            {
                _LongDescription = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LongDescription"));
            }
        }

        /// <summary>
        /// To be inserted with the short cite for other than Exhibit
        /// </summary>
        public string ShortDescription
        {
            get
            {
                return _ShortDescription;
            }
            set
            {
                _ShortDescription = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ShortDescription"));
            }
        }

        /// <summary>
        /// Not to be inserted into the document (eg. Bates no.)
        /// </summary>
        public string OtherIdentifier
        {
            get
            {
                return _OtherIdentifier;
            }
            private set
            {
                _OtherIdentifier = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OtherIdentifier"));
            }
        }

        #endregion


        #region For Cite Block
        //TODO/////////////////////////////////////////////////////////////////////////////////////////////////////
        public int InsertedCount
        {
            get
            {
                return _InsertedCount;
            }
            private set
            {
                _InsertedCount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InsertedCount"));
            }
        }

        public string LongCiteExample
        {
            get
            {
                return _LongCiteExample;
            }
            set
            {
                _LongCiteExample = value;
                OnPropertyChanged("LongCiteExample");
            }
        }

        #endregion

        public Citation(string ID, CiteType CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName="")
        {
            this.ID = ID;
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;

            InsertedCount = 1;
            SetExampleCite();

        }

        public Citation (CiteType CiteType, string LongDescription, string ShortDescription="", string OtherIdentifier="", string ReferenceName = "")
        {
            this.ID = Guid.NewGuid().ToString();
            this.ReferenceName = ReferenceName;
            this.LongDescription = LongDescription;
            this.ShortDescription = ShortDescription;
            this.OtherIdentifier = OtherIdentifier;
            this.CiteType = CiteType;

            InsertedCount = 1;
            SetExampleCite();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
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

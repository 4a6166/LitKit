using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Citation
{
    public class CiteFormatPiece : INotifyPropertyChanged
    {
        public Guid ID { get; private set; }
        public CiteFormatPieceType Type { get; private set; }

        public string _displayText;

        public string DisplayText 
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                if (DisplayInitialized)
                {
                    OnPropertyChanged("DisplayText");
                }
                else DisplayInitialized = true;
            }
        }
        private bool DisplayInitialized = false;


        public CiteFormatPiece(CiteFormatPieceType formatPieceType, string BlockText = "")
        {
            ID = Guid.NewGuid();
            Type = formatPieceType;

            if (BlockText == "")
            {
                setDisplayText();
            }
            else DisplayText = BlockText;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            try
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            catch { }
        }

        private void setDisplayText()
        {
            switch (Type)
            {
                case CiteFormatPieceType.INTROLONG:
                    DisplayText = "Exhibit";
                    break;
                case CiteFormatPieceType.INTROSHORT:
                    DisplayText = "Exhibit";
                    break;
                case CiteFormatPieceType.INDEX:
                    DisplayText = "#";
                    break;
                case CiteFormatPieceType.DESC:
                    DisplayText = "Description";
                    break;
                case CiteFormatPieceType.OTHERID:
                    DisplayText = "ID Number";
                    break;
                case CiteFormatPieceType.PIN:
                    DisplayText = "PINCITE";
                    break;
                case CiteFormatPieceType.FREETEXT:
                    DisplayText = "Edit Me";
                    break;
                case CiteFormatPieceType.LPARENS:
                    DisplayText = "(";
                    break;
                case CiteFormatPieceType.RPARENS:
                    DisplayText = ")";
                    break;
                case CiteFormatPieceType.COMMA:
                    DisplayText = ",";
                    break;
                default:
                    throw new Exception("Unrecognized CiteFormatPieceType entered.");
            }

        }
    }

}

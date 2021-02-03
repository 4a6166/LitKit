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
                //if (DisplayInitialized)
                {
                    OnPropertyChanged("DisplayText");
                }
                //else DisplayInitialized = true;
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
                    _displayText = "Exhibit";
                    break;
                case CiteFormatPieceType.INTROSHORT:
                    _displayText = "Exhibit";
                    break;
                case CiteFormatPieceType.INDEX:
                    _displayText = "#";
                    break;
                case CiteFormatPieceType.DESC:
                    _displayText = "Description";
                    break;
                case CiteFormatPieceType.OTHERID:
                    _displayText = "ID Number";
                    break;
                case CiteFormatPieceType.PIN:
                    _displayText = "PINCITE";
                    break;
                case CiteFormatPieceType.FREETEXT:
                    _displayText = "Edit Me";
                    break;
                case CiteFormatPieceType.LPARENS:
                    _displayText = "(";
                    break;
                case CiteFormatPieceType.RPARENS:
                    _displayText = ")";
                    break;
                case CiteFormatPieceType.COMMA:
                    _displayText = ",";
                    break;
                default:
                    throw new Exception("Unrecognized CiteFormatPieceType entered.");
            }

        }
    }

}

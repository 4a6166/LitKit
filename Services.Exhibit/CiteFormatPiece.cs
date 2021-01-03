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

        private string _displayText;

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
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void setDisplayText()
        {
            switch (Type)
            {
                case CiteFormatPieceType.Intro:
                    DisplayText = "Exhibit";
                    break;
                case CiteFormatPieceType.Index:
                    DisplayText = "#";
                    break;
                case CiteFormatPieceType.Description:
                    DisplayText = "Description";
                    break;
                case CiteFormatPieceType.OtherID:
                    DisplayText = "ID Number";
                    break;
                case CiteFormatPieceType.PincitePlaceholder:
                    DisplayText = "PIN";
                    break;
                case CiteFormatPieceType.FreeText:
                    throw new Exception("Free Text must have a value passed in to BlockText during construction.");
                case CiteFormatPieceType.ParenthesisLeft:
                    DisplayText = "(";
                    break;
                case CiteFormatPieceType.ParenthesisRight:
                    DisplayText = ")";
                    break;
                case CiteFormatPieceType.Comma:
                    DisplayText = ",";
                    break;
                default:
                    throw new Exception("Unrecognized CiteFormatPieceType entered.");
            }

        }
    }

    [Flags]
    public enum CiteFormatPieceType
    {
        Intro = 1,
        Index = 2,
        Description = 4,
        OtherID = 8,
        PincitePlaceholder = 16,

        FreeText = 32,

        ParenthesisLeft = 64,
        ParenthesisRight = 128,
        Comma = 256,
    }
}

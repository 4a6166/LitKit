using Microsoft.Office.Interop.Word;
using Services.Base;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Tools.Citation
{
    public class CiteFormatting : INotifyPropertyChanged
    {
        public string ExhibitIntroLong { get; set; }
        public string ExhibitIntroShort { get; set; }

        public ExhibitIndexStyle ExhibitIndexStyle { get; set; }
        public int ExhibitIndexStart { get; set; }
        public bool hasIdCite { get; set; }

        public ObservableCollection<CiteFormatPiece> ExhibitLongFormat { get; set; }
        public ObservableCollection<CiteFormatPiece> ExhibitShortFormat { get; set; }

        public CiteFormatting(string ExhibitIntroLong, string ExhibitIntroShort, ObservableCollection<CiteFormatPiece> ExhibitLongFormat, ObservableCollection<CiteFormatPiece> ExhibitShortFormat, ExhibitIndexStyle ExhibitIndexStyle = ExhibitIndexStyle.Numbers, int ExhibitIndexStart = 0, bool HasIdCite = true)
        {
            this.ExhibitIntroLong = ExhibitIntroLong;
            this.ExhibitIntroShort = ExhibitIntroShort;
            this.ExhibitLongFormat = ExhibitLongFormat;
            this.ExhibitShortFormat = ExhibitShortFormat;
            this.ExhibitIndexStyle = ExhibitIndexStyle;
            this.ExhibitIndexStart = ExhibitIndexStart; 
            this.hasIdCite = HasIdCite;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        #region Format Cite Text
        public static string ToAlphabet(int number)
        {
            string strAlpha = "";
            switch (number)
            {
                case int n when (0 <= n && n <= 26):
                    strAlpha += ((char)(n + 64)).ToString();
                    break;
                case int n when (26 < n && n <= 26 * 2):
                    strAlpha += "A" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 2 < n && n <= 26 * 3):
                    strAlpha += "B" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 3 < n && n <= 26 * 4):
                    strAlpha += "C" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 4 < n && n <= 26 * 5):
                    strAlpha += "D" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 5 < n && n <= 26 * 6):
                    strAlpha += "E" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 6 < n && n <= 26 * 7):
                    strAlpha += "F" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 7 < n && n <= 26 * 8):
                    strAlpha += "G" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 8 < n && n <= 26 * 9):
                    strAlpha += "H" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 9 < n && n <= 26 * 10):
                    strAlpha += "I" + ((char)(n % 26 + 64)).ToString();
                    break;

                default:
                    strAlpha = "";
                    break;
            }
            return strAlpha;
        }

        public static string ToRoman(int number)
        {
            // https://stackoverflow.com/questions/7040289/converting-integers-to-roman-numerals
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            else return number.ToString();
        }

        public static CiteFormatPiece FormatIdCite(Range range)
        {
            CiteFormatPiece result = new CiteFormatPiece(CiteFormatPieceType.FREETEXT);
            try
            {

                range.SetRange(range.Start - 6, range.Start - 1);

                if (range.Text.Contains(",") || range.Text.Contains("See") || range.Text.Contains("see") || range.Text.Contains("e.g.") || range.Text.Contains("cf.") || range.Text.Contains("Cf.") || range.Text.Contains("CF."))
                {
                    result.DisplayText = "id.";
                }
                else if (range.Text.Contains(".") || range.Text.Contains("\r\n") || range.Text.Contains("\r") || range.Text.Contains("\n"))
                {
                    result.DisplayText = "Id.";
                }
                else
                {
                    result.DisplayText = "id.";
                }
            }
            catch { result.DisplayText = "id."; }

            return result;
        }

        public string FormatCiteText(Citation citation, CitePlacementType placementType, Range InsertRangeForId, int Index = 0)
        {
            string result = "";
            ObservableCollection<CiteFormatPiece> formatPieces = new ObservableCollection<CiteFormatPiece>();
            string description = "";

            if (citation.CiteType == CiteType.Exhibit)
            {
                description = citation.LongDescription;
                switch (placementType)
                {
                    case CitePlacementType.Long:
                        formatPieces = this.ExhibitLongFormat;
                        break;
                    case CitePlacementType.Short:
                        formatPieces = this.ExhibitShortFormat;
                        break;
                    case CitePlacementType.Id:
                        if (hasIdCite)
                        {
                            formatPieces = new ObservableCollection<CiteFormatPiece>() { FormatIdCite(InsertRangeForId), new CiteFormatPiece(CiteFormatPieceType.PIN) };
                        }
                        else
                        {
                            formatPieces = this.ExhibitShortFormat;
                        }
                        break;
                    default:
                        break;
                }

                result = GetStringFromFormatPieces_Exhibit(formatPieces, citation, Index); 
            }
            else
            {
                switch (placementType)
                {
                    case CitePlacementType.Long:
                        result = GetStringFromFormatPieces_Others(citation.LongDescription); 
                        break;
                    case CitePlacementType.Short:
                        result = GetStringFromFormatPieces_Others(citation.ShortDescription);
                        break;
                    case CitePlacementType.Id:
                        if (hasIdCite)
                        {
                            result = FormatIdCite(InsertRangeForId).DisplayText + "{{PIN}}";
                        }
                        else
                        {
                            result = GetStringFromFormatPieces_Others(citation.ShortDescription);
                        }
                        break;
                    default:
                        break;
                }
            }

            // removing non-breaking space capability because it is causing issues and thought it it won't be used
            //result = citation.LongCiteExample.Replace("` `", "\u00a0");
            return result;
        }

        public string GetStringFromFormatPieces_Exhibit( ObservableCollection<CiteFormatPiece> formatPieces, Citation citation, int Index = 0)
        {
            string result = "";
            foreach (CiteFormatPiece piece in formatPieces)
            {
                switch (piece.Type)
                {
                    case CiteFormatPieceType.INTROLONG:

                        result += " "+ExhibitIntroLong;
                        break;
                    case CiteFormatPieceType.INTROSHORT:

                        result += " " + ExhibitIntroShort;
                        break;

                    case CiteFormatPieceType.INDEX:
                        result += "\u00a0";
                        int num = ExhibitIndexStart + Index;
                        if (ExhibitIndexStyle == ExhibitIndexStyle.Numbers)
                            result += num;
                        else if (ExhibitIndexStyle == ExhibitIndexStyle.Letters)
                            result += ToAlphabet(num);
                        else if (ExhibitIndexStyle == ExhibitIndexStyle.Roman)
                            result += ToRoman(num);
                        break;

                    case CiteFormatPieceType.DESC:
                        result += " "+ citation.LongDescription;
                        break;
                    case CiteFormatPieceType.PIN:
                        result += "{{PIN}}";
                        break;
                    case CiteFormatPieceType.FREETEXT:
                        result += " " +piece.DisplayText;
                        break;
                    case CiteFormatPieceType.COMMA:
                        result += ",";
                        break;
                    case CiteFormatPieceType.LPARENS:
                        result += " (";
                        break;
                    case CiteFormatPieceType.RPARENS:
                        result += ")";
                        break;
                    case CiteFormatPieceType.OTHERID:
                        result +=" "+citation.OtherIdentifier;
                        break;
                }
            }

            result = result.Replace("( ", "(");
            result = result.Replace("  ", " ");
            return result.Trim(' ');
        }

        public string GetStringFromFormatPieces_Others(string description)
        {
            if (description.Contains(@"{{PIN}}"))
                {
                //string result = "";
                //var brackets = new string[] { "{{", "}}" };

                //var split = description.Split(brackets, StringSplitOptions.RemoveEmptyEntries);
                //for (int i = 0; i < split.Length; i++)
                //{
                //    if (split[i] == "PIN")
                //    {
                //        result += PIN;
                //    }
                //    else result += split[i];
                //}
                //return result;
                return description;
            }
            else return description + @"{{PIN}}";
        }


        public static void FormatFont(ContentControl contentControl)
        {
            contentControl.LockContents = false;

            FormatTextInDoc.FormatFont(contentControl.Range);

            contentControl.LockContents = true;
        }

        public static void ItalicizeId(ContentControl contentControl)
        {
            contentControl.LockContents = false;

            var find = contentControl.Range.Find;
            find.ClearFormatting();
            find.Replacement.ClearFormatting();

            find.Text = @"<([iI]d.)";
            find.Replacement.Text = @"\1";
            find.Replacement.Font.Italic = -1;
            find.MatchWildcards = true;
            find.Execute(Replace: WdReplace.wdReplaceAll);

            contentControl.LockContents = true;
        }

        #endregion

        public static string ApplyNumFormat(int index, ExhibitIndexStyle NumberFormat)
        {
            string numbering = string.Empty;
            switch (NumberFormat)
            {
                case ExhibitIndexStyle.Numbers:
                    numbering = index.ToString();
                    break;
                case ExhibitIndexStyle.Letters:
                    numbering = ToAlphabet(index);
                    break;
                case ExhibitIndexStyle.Roman:
                    numbering = ToRoman(index);
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return numbering;
        }

    }
}

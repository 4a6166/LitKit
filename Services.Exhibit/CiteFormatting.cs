﻿using Microsoft.Office.Interop.Word;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Tools.Citation
{
    public class CiteFormatting : INotifyPropertyChanged
    {
        public string ExhibitIntro { get; set; }
        public ExhibitIndexStyle ExhibitIndexStyle { get; set; }
        public int ExhibitIndexStart { get; set; }
        public bool hasIdCite { get; set; }

        public ObservableCollection<CiteFormatPiece> ExhibitLongFormat { get; set; }
        public ObservableCollection<CiteFormatPiece> ExhibitShortFormat { get; set; }

        public CiteFormatting(string ExhibitIntro, ObservableCollection<CiteFormatPiece> ExhibitLongFormat, ObservableCollection<CiteFormatPiece> ExhibitShortFormat, ExhibitIndexStyle ExhibitIndexStyle = ExhibitIndexStyle.Numbers, int ExhibitIndexStart = 0, bool HasIdCite = true)
        {
            this.ExhibitIntro = ExhibitIntro;
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
            CiteFormatPiece result = new CiteFormatPiece(CiteFormatPieceType.FreeText);
            try
            {
                var _app = range.Application;
                _app.Selection.SetRange(range.Start - 6, range.Start - 1);

                if (_app.Selection.Range.Text.Contains(",") || _app.Selection.Range.Text.Contains("See") || _app.Selection.Range.Text.Contains("see") || _app.Selection.Range.Text.Contains("e.g.") || _app.Selection.Range.Text.Contains("cf.") || _app.Selection.Range.Text.Contains("Cf.") || _app.Selection.Range.Text.Contains("CF."))
                {
                    result.DisplayText = "id.";
                }
                else if (_app.Selection.Range.Text.Contains(".") || _app.Selection.Range.Text.Contains("\r\n") || _app.Selection.Range.Text.Contains("\r") || _app.Selection.Range.Text.Contains("\n"))
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

        public string FormatCiteText(Citation citation, CitePlacementType placementType, Range LeadingForId, int Index = 0, string Pincite = "")
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
                            formatPieces = new ObservableCollection<CiteFormatPiece>() { FormatIdCite(LeadingForId), new CiteFormatPiece(CiteFormatPieceType.PincitePlaceholder) };
                        }
                        else
                        {
                            formatPieces = this.ExhibitShortFormat;
                        }
                        break;
                    default:
                        break;
                }

                result = GetStringFromFormatPieces_Exhibit(formatPieces, citation, Index, Pincite); 
            }
            else
            {
                switch (placementType)
                {
                    case CitePlacementType.Long:
                        result = GetStringFromFormatPieces_Others(citation.LongDescription, Pincite); 
                        break;
                    case CitePlacementType.Short:
                        result = GetStringFromFormatPieces_Others(citation.ShortDescription, Pincite);
                        break;
                    case CitePlacementType.Id:
                        if (hasIdCite)
                        {
                            result = FormatIdCite(LeadingForId) + Pincite;
                        }
                        else
                        {
                            result = GetStringFromFormatPieces_Others(citation.ShortDescription, Pincite);
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public string GetStringFromFormatPieces_Exhibit( ObservableCollection<CiteFormatPiece> formatPieces, Citation citation, int Index = 0, string PIN = "")
        {
            string result = "";
            foreach (CiteFormatPiece piece in formatPieces)
            {
                switch (piece.Type)
                {
                    case CiteFormatPieceType.Intro:

                        result += ExhibitIntro +" ";
                        break;

                    case CiteFormatPieceType.Index:
                        int num = ExhibitIndexStart + Index;
                        if (ExhibitIndexStyle == ExhibitIndexStyle.Numbers)
                            result += num;
                        else if (ExhibitIndexStyle == ExhibitIndexStyle.Letters)
                            result += ToAlphabet(num);
                        else if (ExhibitIndexStyle == ExhibitIndexStyle.Roman)
                            result += ToRoman(num);

                        result += " ";
                        break;

                    case CiteFormatPieceType.Description:
                        result += citation.LongDescription + " ";
                        break;
                    case CiteFormatPieceType.PincitePlaceholder:
                        result += PIN + " ";
                        break;
                    case CiteFormatPieceType.FreeText:
                        result += piece.DisplayText + " ";
                        break;
                    case CiteFormatPieceType.Comma:
                        result += ", ";
                        break;
                    case CiteFormatPieceType.ParenthesisLeft:
                        result += "(";
                        break;
                    case CiteFormatPieceType.ParenthesisRight:
                        result += ") ";
                        break;
                    case CiteFormatPieceType.OtherID:
                        result +=citation.OtherIdentifier;
                        break;
                }
            }

            return result.Trim(' ');
        }
        public string GetStringFromFormatPieces_Others(string description, string PIN = "")
        {
            string result = "";
            var brackets = new string[] { "{{", "}}" };

            var split = description.Split(brackets, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i++)
            {
                if (split[i] == "PIN")
                {
                    result += PIN;
                }
                else result += split[i];
            }
            return result;
        }


        public void FormatFont(ContentControl contentControl)
        {
            contentControl.LockContents = false;

            var find = contentControl.Range.Find;
            {
                //Bold **&**
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\*\*(*)\*\*";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Bold = -1;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            {
                //Italics //&//
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\/\/(*)\/\/";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Italic = -1;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            {
                //Underline __&__
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\_\_(*)\_\_";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Underline = WdUnderline.wdUnderlineSingle;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            {
                //BoldItalics */&/*
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\*\/(*)\/\*";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Italic = -1;
                find.Replacement.Font.Bold = -1;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }

            contentControl.LockContents = true;
        }

        #endregion

    }
}

using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.Citation
{
    public class CiteFormatting
    {
        static Regex IntroRegex = new Regex(@"{INTRO}");
        static Regex NumRegex = new Regex(@"{INDEX}");
        static Regex DescRegex = new Regex(@"{DESC}");
        static Regex OtherIDRegex = new Regex(@"{OTHERID}");
        static Regex RefNameRegex = new Regex(@"{REFNAME}");
        static Regex PinciteRegex = new Regex(@"{PINCITE}");
        public string ExhibitIntro { get; set; }
        public string ExhibitLongFormat { get; set; }
        public string ExhibitShortFormat { get; set; }
        public ExhibitIndexStyle ExhibitIndexStyle { get; set; }
        public int ExhibitIndexStart { get; set; }

        public bool hasSurroundingParentheses { get; set; }

        public bool hasIdCite { get; set; }

        public CiteFormatting(string ExhibitIntro, string ExhibitLongFormat, string ExhibitShortFormat, ExhibitIndexStyle ExhibitIndexStyle = ExhibitIndexStyle.Numbers, int ExhibitIndexStart = 0, bool HasSurroundingParentheses = false, bool HasIdCite = true)
        {
            this.ExhibitIntro = ExhibitIntro;
            this.ExhibitLongFormat = ExhibitLongFormat;
            this.ExhibitShortFormat = ExhibitShortFormat;
            this.ExhibitIndexStyle = ExhibitIndexStyle;
            this.ExhibitIndexStart = ExhibitIndexStart;
            this.hasSurroundingParentheses = HasSurroundingParentheses;
            this.hasIdCite = HasIdCite;
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

        public static string FormatIdCite(Range range)
        {
            string result = string.Empty;
            try
            {
                var _app = range.Application;
                _app.Selection.SetRange(range.Start - 6, range.Start - 1);

                if (_app.Selection.Range.Text.Contains(",") || _app.Selection.Range.Text.Contains("See") || _app.Selection.Range.Text.Contains("see") || _app.Selection.Range.Text.Contains("e.g.") || _app.Selection.Range.Text.Contains("cf.") || _app.Selection.Range.Text.Contains("Cf.") || _app.Selection.Range.Text.Contains("CF."))
                {
                    result = "id.";
                }
                else if (_app.Selection.Range.Text.Contains(".") || _app.Selection.Range.Text.Contains("\r\n") || _app.Selection.Range.Text.Contains("\r") || _app.Selection.Range.Text.Contains("\n"))
                {
                    result = "Id.";
                }
                else
                {
                    result = "id.";
                }
            }
            catch { result = "id."; }

            return result;
        }

        public string FormatCiteText(Citation citation, CitePlacementType placementType, Range LeadingForId, int Index = 0, bool hasPincite = false)
        {
            string result = "";
            string description = "";

            switch (placementType)
            {
                case CitePlacementType.Long:
                    result = this.ExhibitLongFormat;
                    description = citation.LongDescription;
                    break;
                case CitePlacementType.Short:
                    result = this.ExhibitShortFormat;
                    description = citation.ShortDescription;
                    break;
                case CitePlacementType.Id:
                    if (hasIdCite)
                    {
                        result = FormatIdCite(LeadingForId) + "{PINCITE}";
                    }
                    else
                    {
                        result = this.ExhibitShortFormat;
                        description = citation.ShortDescription;
                    }
                    break;
                default:
                    break;
            }


            if (citation.CiteType == CiteType.Exhibit)
            {
                string indexString = "";
                switch (ExhibitIndexStyle)
                {
                    case ExhibitIndexStyle.Numbers:
                        indexString = Index.ToString();
                        break;
                    case ExhibitIndexStyle.Letters:
                        indexString = ToAlphabet(Index);
                        break;
                    case ExhibitIndexStyle.Roman:
                        indexString = ToRoman(Index);
                        break;
                }

                result = IntroRegex.Replace(result, ExhibitIntro+" ");
                result = NumRegex.Replace(result, indexString+", ");
            }
            else
            {
                result = IntroRegex.Replace(result, "");
                result = NumRegex.Replace(result, "");
            }

            result = DescRegex.Replace(result, description);
            result = OtherIDRegex.Replace(result, citation.OtherIdentifier);
            result = RefNameRegex.Replace(result, citation.ReferenceName);

            if (!hasPincite)
            {
                result = PinciteRegex.Replace(result, "");
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

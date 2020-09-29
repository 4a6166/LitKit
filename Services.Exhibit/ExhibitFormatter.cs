using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Tools.Exhibit
{
    public static class ExhibitFormatter
    {


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
        public static string ApplyNumFormat(int index, NumberingOptions NumberFormat)
        {
            string numbering = string.Empty;
            switch (NumberFormat)
            {
                case NumberingOptions.Numbers:
                    numbering = index.ToString();
                    break;
                case NumberingOptions.Letters:
                    numbering = ToAlphabet(index);
                    break;
                case NumberingOptions.RomanNumerals:
                    numbering = ToRoman(index);
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return numbering;
        }
        public static string ApplyNumFormat(int index, string NumberFormat)
        {
            NumberingOptions numberFormat = new EnumSwitch().NumberingOptions_TextSwitchEnum(NumberFormat);
            return ApplyNumFormat(index, numberFormat);
        }




        /// <summary>
        /// Formats initial and following cites. Does not format Id Cites.
        /// </summary>
        /// <param name="exhibit"></param>
        /// <param name="CiteFormat">Pass in FirstCite or FollowingCites format text.</param>
        /// <param name="IndexStyle"></param>
        /// <param name="IndexStart"></param>
        /// <param name="IndexInDoc"></param>
        /// <param name="Pincite"></param>
        /// <returns></returns>
        public static string FormatCite(Exhibit exhibit, string CiteFormat, NumberingOptions IndexStyle, int IndexStart, int IndexInDoc, string Pincite = "")
        {
            string result = string.Empty;
            int IndexActual = IndexStart - 1 + IndexInDoc;
            string Number = ApplyNumFormat(IndexActual, IndexStyle);

            string Description = exhibit.Description;
            string Bates = exhibit.BatesNumber;


            result = CiteFormat;
            result = result.Replace("{INDEX}", Number);
            result = result.Replace("{DESC}", Description);
            result = result.Replace("{BATES}", Bates);
            result = result.Replace("{PINCITE}", Pincite);

            return result;
        }
        public static string FormatIdCite(Range range, string Pincite = "")
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

            result += Pincite;

            return result;
        }
    }
}

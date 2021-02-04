using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Tools.Citation;

namespace LitKit1.ControlsWPF.Citation.Converters
{
    [ValueConversion(typeof(CiteFormatPieceType), typeof(SolidColorBrush))]
    public class FormatBlockTypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CiteFormatPieceType type = (CiteFormatPieceType)value;
            string color;

            switch (type)
            {
                case CiteFormatPieceType.INTROLONG:
                    color = "#43aa8b";
                    break;
                case CiteFormatPieceType.INTROSHORT:
                    color = "#43aa8b";
                    break;

                case CiteFormatPieceType.INDEX:
                    color = "#f3722c";
                    break;
                case CiteFormatPieceType.DESC:
                    color = "#f8961e";
                    break;
                case CiteFormatPieceType.OTHERID:
                    color = "90be6d";
                    break;
                case CiteFormatPieceType.PIN:
                    color = "#f9c74f";
                    break;

                case CiteFormatPieceType.FREETEXT:
                    color = "#dee2e6";
                    break;

                case CiteFormatPieceType.LPARENS:
                    color = "#577590";
                    break;

                case CiteFormatPieceType.RPARENS:
                    color = "#577590";
                    break;
                case CiteFormatPieceType.COMMA:
                    color = "#577590";
                    break;

                default:
                    color = "#969696";
                    break;
                    //throw new Exception("CiteType not recognized");
            }
            return (Brush) new BrushConverter().ConvertFromString(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

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
                case CiteFormatPieceType.Intro:
                    color = "#43aa8b";
                    break;

                case CiteFormatPieceType.Index:
                    color = "#f3722c";
                    break;
                case CiteFormatPieceType.Description:
                    color = "#f8961e";
                    break;
                case CiteFormatPieceType.OtherID:
                    color = "90be6d";
                    break;
                case CiteFormatPieceType.PincitePlaceholder:
                    color = "#f9c74f";
                    break;

                case CiteFormatPieceType.FreeText:
                    color = "#dee2e6";
                    break;

                case CiteFormatPieceType.ParenthesisLeft:
                    color = "#577590";
                    break;

                case CiteFormatPieceType.ParenthesisRight:
                    color = "#577590";
                    break;
                case CiteFormatPieceType.Comma:
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

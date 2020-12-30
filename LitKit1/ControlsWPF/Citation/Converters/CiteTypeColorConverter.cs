using System;
using System.Windows.Data;
using System.Windows.Media;
using Tools.Citation;

namespace LitKit1.ControlsWPF.Citation.Converters
{
    [ValueConversion(typeof(CiteType), typeof(SolidColorBrush))]
    public class CiteTypeColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CiteType type = (CiteType)value;
            Brush brush;

            switch (type)
            {
                case CiteType.Exhibit:
                    brush = SolutionBrushes.Exhibit;
                    break;
                case CiteType.Legal:
                    brush = SolutionBrushes.LegalCite;
                    break;
                case CiteType.Record:
                    brush = SolutionBrushes.RecordCite;
                    break;
                case CiteType.Other:
                    brush = SolutionBrushes.OtherCite;
                    break;
                default:
                    brush = SolutionBrushes.OtherCite;
                break;
            }

            return brush;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

    }
}
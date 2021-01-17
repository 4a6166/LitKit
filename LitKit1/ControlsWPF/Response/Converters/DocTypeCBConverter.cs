using System;
using System.Windows.Data;
using System.Windows.Media;
using Tools.Response;

namespace LitKit1.ControlsWPF.Response.Converters
{
    [ValueConversion(typeof(DocType), typeof(string))]

    public class DocTypeCBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DocType type = (DocType)value;

            switch (type)
            {
                case DocType.Complaint:
                    return "Answer a Complaint";
                case DocType.Admission:
                    return "Respond to Requests for Admission";
                case DocType.Production:
                    return "Respond to Requests for Production of Documents";
                case DocType.Interrogatory:
                    return "Respond to Interrogatories";
                default:
                    throw new Exception("Error passing DocType Enum");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = (string)value;

            switch (type)
            {
                case "Answer a Complaint":
                    return DocType.Complaint;
                case "Respond to Requests for Admission":
                    return DocType.Admission;
                case "Respond to Requests for Production of Documents":
                    return DocType.Production;
                case "Respond to Interrogatories":
                    return DocType.Interrogatory;
                default:
                    throw new Exception("Error passing DocType CB text");
            }
        }
    }
}


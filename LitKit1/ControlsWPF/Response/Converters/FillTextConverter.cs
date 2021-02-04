using LitKit1.ControlsWPF.Response.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tools.Response;

namespace LitKit1.ControlsWPF.Response.Converters
{
    /// <summary>
    /// For converting response block
    /// </summary>
    [ValueConversion(typeof(Tools.Response.Response), typeof(string))]
    public class FillTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ResponseMainVM ViewModel = Globals.ThisAddIn.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            Tools.Response.Response response = (Tools.Response.Response)value;

            try
            {
                return ResponseStandardRepository.FillString(response.ID, response.DisplayText, ViewModel.Responding, ViewModel.RespondingIsPlural.ToString(), ViewModel.Propounding, ViewModel.DocType.ToString());
            }
            catch { return response.DisplayText; }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

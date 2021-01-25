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
    /// For filling the standard texts in the standard response repository
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class FillTextConverterString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ResponseMainVM ViewModel = Globals.ThisAddIn.responseVMDict[Globals.ThisAddIn.Application.ActiveWindow];

            Tools.Response.Response response = ViewModel.EditResponseVM.EditResponseRsp;

            string standardUnfilled = (string)value;

            try
            {
                return ResponseStandardRepository.FillString(response.ID, standardUnfilled, ViewModel.Responding, ViewModel.RespondingIsPlural.ToString(), ViewModel.Propounding, ViewModel.DocType.ToString());
            }
            catch { return standardUnfilled; }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}

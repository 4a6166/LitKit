using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public static class ErrorHandling
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ShowErrorMessage()
        {
            System.Windows.Forms.MessageBox.Show("Something went wrong. Please contact Prelimine if the problem persits");
        }

        public static void ShowErrorMessage(string message)
        {
            System.Windows.Forms.MessageBox.Show(message);
        }
        
    }
}

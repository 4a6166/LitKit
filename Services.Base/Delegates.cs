using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    /// <summary>
    /// Use to create an event that publishes when creating a new Redaction in Redaction.cs > public event RedactionCalledDelegate RedactionCalled;
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void RedactionCalledDelegate(object sender, EventArgs args);

    public class RedactionCalledMethods
    {
        public static void OnRedactionAdded(object sender, EventArgs e)
        { 
            //System.Windows.Forms.MessageBox.Show("Redaction Added"); 
        }
    }

    public delegate void LogActionDelegate(object sender, EventArgs args);

    public class LogActionClass
    {
        public static void OnActionLogged(object sender, EventArgs e)
        {
            // Add code here to log an action somewhere
        }
    }
}

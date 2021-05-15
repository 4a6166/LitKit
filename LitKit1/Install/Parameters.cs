using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Text;

namespace LitKit1.Install
{
    public class Parameters
    {
        /// <summary>
        /// This inner class maintains the key names
        /// for the parameter values that may be passed on the 
        /// command line.
        /// </summary>
        public class Keys
        {
            public const string MyCustomParameter =
                               "LicenseKey";

            ///Custom Actions Data | LicenseKey="[LICENSEKEY]"
        }

        private string _parameter = null;
        public string Parameter
        {
            get { return _parameter; }
        }

        /// <summary>
        /// This constructor is invoked by Install class
        /// methods that have an Install Context built from 
        /// parameters specified in the command line.
        /// Rollback, Install, Commit, and intermediate methods like
        /// OnAfterInstall will all be able to use this constructor.
        /// </summary>
        /// <param name="installContext">The install context
        /// containing the command line parameters to set
        /// the strong types variables to.</param>
        public Parameters(InstallContext installContext)
        {
            this._parameter =
              installContext.Parameters[Keys.MyCustomParameter];
        }

        /// <summary>
        /// This constructor is used by the Install class
        /// methods that don't have an Install Context built
        /// from the command line. This method is primarily
        /// used by the Uninstall method.
        /// </summary>
        /// <param name="savedState">An IDictionary object
        /// that contains the parameters that were
        /// saved from a prior installation.</param>
        public Parameters(IDictionary savedState)
        {
            if (savedState.Contains(Keys.MyCustomParameter) == true)
                this._parameter =
                  (string)savedState[Keys.MyCustomParameter];

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LitKit1.Install
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }
        // <summary>
        /// To cause this method to be invoked, I added the primary project output to the 
        /// setup project's custom actions, under the "Install" folder.
        /// </summary>
        /// <param name="stateSaver">A dictionary object that will be retrievable during the uninstall process.</param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            Console.WriteLine("Install triggered");
            Console.ReadLine();
            // Get the custom parameters from the install context.
            Parameters customParameters = new Parameters(this.Context);

            WriteKeyFile(customParameters.Parameter);


            SaveCustomParametersInStateSaverDictionary(
                            stateSaver, customParameters);

            //PrintMessage("The application is being installed.",
            //             customParameters);

            base.Install(stateSaver);
        }

        /// <summary>
        /// Adds or updates the state dictionary so that custom
        /// parameter values can be retrieved when 
        /// the application is uninstalled.
        /// </summary>
        /// <param name="stateSaver">An IDictionary object
        /// that will contain all the objects who's state
        /// is to be persisted across installations.</param>
        /// <param name="customParameters">A strong typed
        /// object of custom parameters that will be saved.</param>
        private void SaveCustomParametersInStateSaverDictionary(
                System.Collections.IDictionary stateSaver,
                Parameters customParameters)
        {
            // Add/update the "Parameter" entry in the
            // state saver so that it may be accessed on uninstall.
            if (stateSaver.Contains(Parameters.Keys.MyCustomParameter) == true)
                stateSaver[Parameters.Keys.MyCustomParameter] =
                                  customParameters.Parameter;
            else
                stateSaver.Add(Parameters.Keys.MyCustomParameter,
                               customParameters.Parameter);

        }

        /// <summary>
        /// copy of Serices.License.LicenseChecker.WriteKeyFile. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string WriteKeyFile(string key)
        {
            try
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                dir += @"\Prelimine";
                string path = dir + @"\LicenseKey.txt";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                StreamWriter file = new StreamWriter(path, false);
                file.WriteLine(key);
                file.Close();

                return path;
            }
            catch
            {
                return null;
            }
        }
    }
}


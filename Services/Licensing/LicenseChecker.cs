using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Extensions;
using Rhino.Licensing;

namespace Services.Licensing
{
    public class LicenseChecker
    {

        private static string PublicKeyPath;
        private static string LicensePath;

        public static bool LicenseIsValid(string licpath)
        {


            bool result = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            PublicKeyPath = Root + @"\Licensing\publicKey.xml";
            LicensePath = licpath;
                //@"C:\Prelimine\license.xml";
                // Root + @"\Licensing\license.xml";
                //@"C:\Users\Jake\OneDrive\Desktop\LicenseTests\license.xml";

            try
            {
                var publicKey = File.ReadAllText(PublicKeyPath);

                //Throws an exception if license has been modified
                new LicenseValidator(publicKey, LicensePath).AssertValidLicense();
                result = true;
            }
            catch
            {
            }

            return result;
        }
    }
}

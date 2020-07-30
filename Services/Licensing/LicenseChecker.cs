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

        private static string PublicKeyPath = @"C:\Users\Jake\OneDrive\Desktop\LicenseTests\publicKey.xml";
        private static string LicensePath = @"C:\Users\Jake\OneDrive\Desktop\LicenseTests\license.xml";

        public static bool LicenseIsValid()
        {
            bool result = false;

            try
            {
                var publicKey = File.ReadAllText(PublicKeyPath);

                //Throws an exception if license has been modified
                new LicenseValidator(publicKey, LicensePath).AssertValidLicense();
                result = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Your Prelimine LitKit License key is not valid. Please contact your IT administrator or Prelimine for a new license.");
            }

            return result;
        }
    }
}

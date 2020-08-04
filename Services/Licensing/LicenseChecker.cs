using System;
using System.IO;
using Rhino.Licensing;

namespace Services.Licensing
{
    public class LicenseChecker
    {
        private static string PublicKeyPath;
        private static string LicensePath;

        public static bool LicenseIsValid(string licPath)
        {
            bool result = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            PublicKeyPath = Root + @"\Licensing\publicKey.xml";
            LicensePath = Root + @"\Licensing\license.xml";  //licPath;
                // not working on INSTALL, runs fine in debug

            try
            {
                var publicKey = File.ReadAllText(PublicKeyPath);

                //Throws an exception if license has been modified
                LicenseValidator validator = new LicenseValidator(publicKey, LicensePath);
                validator.AssertValidLicense();
                
                if (validator.ExpirationDate > DateTime.Now)
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }
    }
}

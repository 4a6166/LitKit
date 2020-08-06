using System;
using System.IO;
using Rhino.Licensing;

namespace Services.Licensing
{
    public class LicenseChecker
    {
        private static string PublicKeyPath;
        private static string LicensePath;

        public static bool LicenseIsValid()
        {
            bool result = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory() + @"\..\litkit.dll*";
            PublicKeyPath = Root + @"\Services\Licensing\publicKey.xml";  
                //@"C:\Users\Jake\Google Drive (jacob.field@prelimine.com)\repos\LitKit1_git\LitKit1\LitKit1\Services\Licensing\publicKey.xml";
            LicensePath = Root + @"\Services\Licensing\license.xml"; 
                //@"C:\Users\Jake\Google Drive (jacob.field@prelimine.com)\repos\LitKit1_git\LitKit1\LitKit1\Services\Licensing\license.xml";

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

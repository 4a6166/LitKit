using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Rhino.Licensing;

namespace Services.Licensing
{
    public class LicenseChecker
    {
        private static string publicKey = @"<RSAKeyValue><Modulus>v17shViD7bFwTSpNjJcxEdQ2JGncp8F8TjBp7+2uZzzBRLDV2du2s2LTbTEHAJW5yr0UhWj4MhAsjsAMD3Vi9QhTV4vhgVIZchfiGeEL9M0lMLm2uWAio9hAWV2yM10JS5mqFZfiX4EM1ltAsBpqXOrk04mvQCmf7J8Z81l1UAU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static string LicensePath;

        public static bool LicenseIsValid()
        {
            bool result = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();


            LicensePath = Root + @"\Services\Licensing\license.xml";  // For use during debug

            // For use during user testing
            //{
            //    string Parent = Directory.GetCurrentDirectory() + @"\..\";
            //    var Dirs = Directory.EnumerateDirectories(Parent);

            //    string Rootdll = Dirs.Where(n => n.Contains("litkit.dll")).SingleOrDefault();

            //    LicensePath = Rootdll + @"\Services\Licensing\license.xml";
            //}


            try
            {
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

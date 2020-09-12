using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Rhino.Licensing;

using System.Reflection;

[assembly: Obfuscation(Feature = "apply to type Services.Licensing.*: all", Exclude = true, ApplyToMembers = true)]

namespace Services.Licensing
{
    public class LicenseChecker
    {
        private static string publicKey
            = @"<RSAKeyValue><Modulus>v17shViD7bFwTSpNjJcxEdQ2JGncp8F8TjBp7+2uZzzBRLDV2du2s2LTbTEHAJW5yr0UhWj4MhAsjsAMD3Vi9QhTV4vhgVIZchfiGeEL9M0lMLm2uWAio9hAWV2yM10JS5mqFZfiX4EM1ltAsBpqXOrk04mvQCmf7J8Z81l1UAU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


        private static string LicensePath;

        public static bool LicenseIsValid()
        {
            bool result = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();

            //var Files = Directory.EnumerateFileSystemEntries(Root);

            //string licPath = Files.Where(n => n.Contains("license.xml")).SingleOrDefault();
            //string pubPath = Files.Where(n => n.Contains("publicKey.xml")).SingleOrDefault();

            string licPath = Root + @"\license.xml";
            //string pubPath = Root + @"\publicKey.xml";

            //publicKey = new StreamReader(pubPath).ReadToEnd();




            LicensePath = licPath;
            //publicKey = @"<RSAKeyValue><Modulus>v17shViD7bFwTSpNjJcxEdQ2JGncp85F8TjBp7+2uZzzBRLDV2du2s2LTbTEHAJW5yr0UhWj4MhAsjsAMD3Vi9QhTV4vhgVIZchfiGeEL9M0lMLm2uWAio9hAWV2yM10JS5mqFZfiX4EM1ltAsBpqXOrk04mvQCmf7J8Z81l1UAU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";




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

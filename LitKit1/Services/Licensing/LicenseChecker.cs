﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Rhino.Licensing;

using System.Reflection;
using System.Xml;

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

            string licPath = Root + @"\license.xml";

            LicensePath = licPath;

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

        public static string Expiration()
        {
            string result = string.Empty;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();

            string licPath = Root + @"\license.xml";

            XmlDocument license = new XmlDocument();
            license.Load(licPath);

            var test = license.SelectNodes("license");
            var test1 = test[0];
            var test2 = test1.Attributes;
            var test3 = test2[0];
            var test4 = test3.Value;
            var test5 = test3.InnerText;

            result= license.SelectNodes("license")[0].Attributes[1].InnerText;
            return result;
        }

        public static string Name()
        {
            string result = string.Empty;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();

            string licPath = Root + @"\license.xml";

            XmlDocument license = new XmlDocument();
            license.Load(licPath);
            
            result = license.SelectSingleNode(".//name").InnerText;
            return result;
            
        }

    }
}

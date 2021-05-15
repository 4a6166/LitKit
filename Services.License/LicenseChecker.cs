
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Security;
using System.Windows.Forms;
using LicenseSpring;

[assembly: Obfuscation(Feature = "apply to type Services.Licensing.*: all", Exclude = true, ApplyToMembers = true)]

namespace Services.License
{
    public class LicenseChecker
    {
        public static bool? CheckValidity()
        {
            
            // LicenseSpring
            LS ls = new LS();
            var license = ls.GetLicense(); //not online check

            if (license != null)
            {
                var lastCheckDate = license.LastCheckDate();
                if (DateTime.Now > lastCheckDate.AddMonths(1))
                {
                    //online check
                    license.Check(); //online check
                }

                if (!license.IsValid() && !(license.ValidityPeriod()>= DateTime.Now))
                {
                    license = ActivateKey(ls, license);
                }

                if (license.IsTrial())
                {
                    TrialForm trialForm = new TrialForm();
                    trialForm.UpdateDays(license.DaysRemaining());

                    trialForm.ShowDialog();

                }
            }
            else
            {
                license = ActivateKey(ls, license);
            }

            return license.IsValid();
        }

        private static LicenseSpring.ILicense ActivateKey(LS ls, LicenseSpring.ILicense license, bool FirstTime = true)
        {
            string key = GetLicKeyFromFile();

            try
            {
                ls.ActivateLicenseKey(key);
                license = ls.GetLicense();
                return license;
            }
            catch
            {
                
                var keyEntryForm = new KeyEntryForm();
                if (!FirstTime)
                {
                    keyEntryForm.ChangeErrorMessage("License Key entered is not valid");
                }
                keyEntryForm.Show();

                if (keyEntryForm.DialogResult != DialogResult.None)
                {
                    return ActivateKey(ls, license, false);

                }
                else
                {
                    throw new Exception("License Exception: Valid license key not found.");
                }


            }
        }

        private static string GetLicKeyFromFile()
        {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                filePath = (filePath + @"\Prelimine\LicenseKey.txt");
                string path = Convert.ToString(filePath);

                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    StreamReader reader = new StreamReader(path);
                    string key = reader.ReadToEnd().Trim();
                    reader.Close();
                    return key;
                }
                else return null;
            }
            catch (SecurityException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            catch
            {
                MessageBox.Show("There was an error reading this file. Please contact your administrator if the problem persists.");
                return null;
            }
        }

        /// <summary>
        /// Writes passed key to a keyfile and returns the path
        /// </summary>
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

        public static string ReadLicense()
        {
            LS ls = new LS();
            ILicense license = ls.GetLicense();

            if (license != null)
            {
                try
                {
                    string result = "";
                    result += "Product: " + license.GetProductDetails().ProductName + Environment.NewLine;
                    result += "Product Code: " + license.GetProductDetails().ProductCode + Environment.NewLine;
                    result += "Company License: " + license.Owner().Company + Environment.NewLine;
                    result += Environment.NewLine;
                    if (license.IsValid())
                    {
                        result += "License is valid";
                    }
                    else
                    {
                        result += "License is not valid.";
                    }

                    result += Environment.NewLine;
                    DateTime expiration = DateTime.Now.AddDays(license.DaysRemaining());
                    if (license.IsExpired())
                    {
                        result += "License Expired " + expiration.ToShortDateString();
                    }
                    else
                    {
                        result += "License Expires on " + expiration.ToShortDateString();
                    }

                    return result;
                }
                catch
                {
                    string result = "";
                    if (license.IsValid())
                    {
                        result += "License is valid";
                    }
                    else
                    {
                        result += "License is not valid.";
                    }
                    return result;
                }
            }
            else { return "No license was found on your computer. Please contact your system administrator or IT service."; }
        }
    }
}










//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
////using Rhino.Licensing;

//using System.Reflection;
//using System.Xml;

//[assembly: Obfuscation(Feature = "apply to type Services.Licensing.*: all", Exclude = true, ApplyToMembers = true)]

//namespace Services.License
//{
//    public class LicenseChecker
//    {
//        private static string publicKey
//            = @"<RSAKeyValue><Modulus>v17shViD7bFwTSpNjJcxEdQ2JGncp8F8TjBp7+2uZzzBRLDV2du2s2LTbTEHAJW5yr0UhWj4MhAsjsAMD3Vi9QhTV4vhgVIZchfiGeEL9M0lMLm2uWAio9hAWV2yM10JS5mqFZfiX4EM1ltAsBpqXOrk04mvQCmf7J8Z81l1UAU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


//        private static string LicensePath;

//        private static string GetLicensePath()
//        {
//            string licPath = string.Empty;

//            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
//            String Root = Directory.GetCurrentDirectory();


//            var files = Directory.EnumerateFiles(Root);
//            licPath = files.Where(n => n.Contains("license.xml")).First();

//            return licPath;
//        }
//        public static bool LicenseIsValid()
//        {
//            bool result = false;
//            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
//            //String Root = Directory.GetCurrentDirectory();

//            //string licPath = Root + @"\license.xml";

//            //LicensePath = licPath;
//            LicensePath = GetLicensePath();

//            try
//            {
//                //Throws an exception if license has been modified
//                LicenseValidator validator = new LicenseValidator(publicKey, LicensePath);
//                validator.AssertValidLicense();

//                if (validator.ExpirationDate > DateTime.Now)
//                {
//                    result = true;
//                }
//            }
//            catch
//            { }

//            return result;
//        }

//        public static string Expiration()
//        {
//            string result = string.Empty;
//            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
//            //String Root = Directory.GetCurrentDirectory();

//            //string licPath = Root + @"\license.xml";

//            string licPath = GetLicensePath();

//            XmlDocument license = new XmlDocument();
//            license.Load(licPath);

//            var test = license.SelectNodes("license");
//            var test1 = test[0];
//            var test2 = test1.Attributes;
//            var test3 = test2[0];
//            var test4 = test3.Value;
//            var test5 = test3.InnerText;

//            result= license.SelectNodes("license")[0].Attributes[1].InnerText;
//            return result;
//        }

//        public static string Name()
//        {
//            string result = string.Empty;
//            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
//            String Root = Directory.GetCurrentDirectory();

//            string licPath = Root + @"\license.xml";

//            XmlDocument license = new XmlDocument();
//            license.Load(licPath);

//            result = license.SelectSingleNode(".//name").InnerText;
//            return result;

//        }

//    }
//}

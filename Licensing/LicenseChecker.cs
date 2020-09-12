using System;
using System.IO;

using Rhino.Licensing;

//using System.Reflection;
//[assembly: Obfuscation(Feature = "apply to type Services.Licensing.*: all", Exclude = true, ApplyToMembers = true)]

namespace Lic
{
    public class LicenseChecker
    {
        private static string publicKey
            = @"<RSAKeyValue><Modulus>v17shViD7bFwTSpNjJcxEdQ2JGncp8F8TjBp7+2uZzzBRLDV2du2s2LTbTEHAJW5yr0UhWj4MhAsjsAMD3Vi9QhTV4vhgVIZchfiGeEL9M0lMLm2uWAio9hAWV2yM10JS5mqFZfiX4EM1ltAsBpqXOrk04mvQCmf7J8Z81l1UAU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


        private static string LicensePath;

        public static bool LicenseIsValid()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();

            string licPath = Root + @"\license.xml";

            LicensePath = licPath;


            try
            {

                //Throws an exception if license has been modified
                LicenseValidator validator = new LicenseValidator(publicKey, LicensePath);

                validator.AssertValidLicense();

                return true;

            }
            catch (LicenseNotFoundException e)
            { 
                //System.Windows.Forms.MessageBox.Show("License not found"); 
                return false; 
            }
            catch (LicenseFileNotFoundException e)
            { 
                //Console.WriteLine("License FILE not found"); 
                return false; 
            }
            catch (LicenseExpiredException e)
            { 
                //Console.WriteLine("License Expired"); 
                return false; 
            }
            catch (Exception e)
            { 
                //Console.WriteLine("Generic Exceoption"); 
                return false; 
            }
        }
    }
}

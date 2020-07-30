using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Rhino.Licensing;

namespace LicenseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Intro();



        }

        static string customer;
        static LicenseType licenseType;
        static DateTime expirationDate;


        static void Intro()
        {
            Console.WriteLine("Welcome to the Prelimine License Generator");
            Console.WriteLine("Do you need a (N)ew set of keys, a new (L)icense, or license (V)alidation? (q to quit)");
            var line = Console.ReadLine().ToUpper().First();

            if (line == 'N')
            {
                Console.WriteLine("Where should they be saved?");
                string path = Console.ReadLine();
                Crypto crypto = new Crypto();
                crypto.GenerateKeys(path);
                Console.WriteLine("New Keys saved");
                Console.WriteLine();
            }
            else if (line == 'L')
            {

                Console.WriteLine("Customer: ");
                customer = Console.ReadLine();
                AskLicenseType();
                if (ConfirmInfo())
                {
                    GenerateLicense();
                    Console.WriteLine("The license has been generated.");
                }
                else Console.WriteLine("License was not generated.");

            }
            else if (line == 'V')
            {
                Crypto crypto = new Crypto();
                try
                {
                    crypto.ValidateLicense(@"C:\Users\Jake\OneDrive\Desktop\LicenseTests\license.xml", @"C:\Users\Jake\OneDrive\Desktop\LicenseTests\publicKey.xml");
                    Console.WriteLine("This license is valid");
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("The license appears to be invlaid.");
                    Console.WriteLine();
                }
            }
            else if (line == 'Q')
            {
                Environment.Exit(0);
            }

            Intro();
        }

        private static void AskLicenseType()
        {
            Console.WriteLine("License Type (Trial or Standard): ");
            char LicType = Console.ReadLine().ToUpper().First();
            if (LicType == 'T')
            {
                licenseType = LicenseType.Trial;
                expirationDate = DateTime.Now.AddDays(30);
            }
            else if (LicType == 'S')
            {
                licenseType = LicenseType.Standard;
                expirationDate = DateTime.Now.AddYears(1);
            }
            else
            {
                Console.WriteLine("Please choose T or S.");
                AskLicenseType();
            }
        }

        static bool ConfirmInfo()
        {
            Console.WriteLine();
            string confirm = "Please Confirm the following is accurate (Y/N)." + Environment.NewLine +
                "Customer: " + customer + Environment.NewLine +
                "License Type: " + licenseType + Environment.NewLine +
                "Expiration Date: " + expirationDate.ToString("yyyy-MM-dd")
                ;
            Console.WriteLine(confirm);

            string str = Console.ReadLine();
            if (str.ToUpper().First() == 'Y')
            { return true; }
            else return false;
        }

        static void GenerateLicense()
        {

            string path = @"C:\Users\Jake\OneDrive\Desktop\LicenseTests";

            Crypto crypto = new Crypto();
            crypto.CreateLicenses(path, customer, expirationDate, licenseType);
        }
    }
}

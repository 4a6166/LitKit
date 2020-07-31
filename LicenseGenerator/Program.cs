using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Console.WriteLine("Welcome to the Prelimine License Generator" + Environment.NewLine);
            Intro();

            //Form1 form = new Form1();
            //form.ShowDialog();
        }

        static string customer;
        static LicenseType licenseType;
        static DateTime expirationDate;
        static string path = @"C:\Prelimine LitKit Licenses\New";


        static void Intro()
        {

            Console.WriteLine("Do you need a new set of (K)eys, a new (L)icense, or license (V)alidation?");
            Console.WriteLine($"Warning: this write to {path} and will overwrite files named license, publicKey, and privateKey. Press q to quit)");
            var line = Console.ReadLine().ToUpper().First();

            if (line == 'K')
            {
                Console.WriteLine($"This will overwrite any keys in {path}. Continue (Y/N):");
                char yesNo = Console.ReadLine().ToUpper().First() ;
                if (yesNo == 'Y')
                {
                    Crypto crypto = new Crypto();
                    crypto.GenerateKeys(path);
                    Console.WriteLine("New Keys saved");
                    Console.WriteLine();
                    Process.Start(path);
                }
            }
            else if (line == 'L')
            {
                Console.WriteLine($"This will overwrite any license file in {path}.");
                Console.WriteLine("Customer: ");
                customer = Console.ReadLine();
                AskLicenseType();
                if (ConfirmInfo())
                {
                    GenerateLicense();
                    Console.WriteLine("The license has been generated.");
                    Process.Start(path);
                }
                else Console.WriteLine("License was not generated.");

            }
            else if (line == 'V')
            {
                Crypto crypto = new Crypto();
                try
                {
                    crypto.ValidateLicense(path+@"\license.xml", path+@"\publicKey.xml");
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

            Console.WriteLine();
            Console.WriteLine();
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

            Crypto crypto = new Crypto();
            crypto.CreateLicense(path, customer, expirationDate, licenseType);
        }
    }
}

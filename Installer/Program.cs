using System;
using System.IO;
//using Services.License;

namespace SetupHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //CollectKey();
        }

        private static void CollectKey()
        {
            Console.Write("Enter your license key:");
            string key = "";
            key = Console.ReadLine().ToUpper();
            bool keyValid = CheckKeyFormat(key);

            while (!keyValid)
            {
                Console.WriteLine("Incorrect key format.");
                Console.Write("Enter your license key:");
                key = Console.ReadLine();
                keyValid = CheckKeyFormat(key);
            }


            //var path = LicenseChecker.WriteKeyFile(key);

            //if (path != null)
            //{
            //    Console.WriteLine("Key written to " + path);
            //}
            //else Console.WriteLine("Error writing licese key to file");
        }

        public static void CollectKey(string key)
        {
            //var path = LicenseChecker.WriteKeyFile(key);
        }

        private static bool CheckKeyFormat(string key)
        {

            var keyParts = key.Split('-');

            if (key.Length != 19) { return false; }
            if (keyParts.Length != 4) { return false; }
            foreach(var part in keyParts)
            {
                if (part.Length != 4) { return false; }
                if (!char.IsLetterOrDigit(part[0])) { return false; }
                if (!char.IsLetterOrDigit(part[1])) { return false; }
                if (!char.IsLetterOrDigit(part[2])) { return false; }
                if (!char.IsLetterOrDigit(part[3])) { return false; }
            }

            return true;
        }

    }
}

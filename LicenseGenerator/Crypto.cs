using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Rhino.Licensing;

namespace LicenseGenerator
{
    public class Crypto
    {
        // REFERENCE: https://thinktanksoft.wordpress.com/2015/03/12/simple-c-licencing-rhino-licensing/
        public Crypto()
        {


        }

        public void GenerateKeys(string path)
        {
            try
            {
                var rsa = new RSACryptoServiceProvider(1024);
                var publicKey = rsa.ToXmlString(false);
                var privateKey = rsa.ToXmlString(true);

                File.WriteAllText(path + @"\publicKey.xml", publicKey);
                File.WriteAllText(path + @"\privateKey.xml", privateKey);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //public string GetHardwareId(string key, string propertyVale)
        //{
        //    var value = string.Empty;
        //    var searcher = new ManagementObjectSearcher("select * from " + key);
        //    foreach (ManagementObject share in searcher.Get())
        //    {
        //        value = (string)share.GetPropertyValue(propertyValue);
        //    }
        //    return value;
        //}

        public void CreateLicense(string MainPath, string Name, DateTime ExpirationDate, LicenseType LicenseType)
        {
            try
            {
                var privateKey = File.ReadAllText(MainPath + @"\privateKey.xml");
                var id = Guid.NewGuid();
                var generator = new Rhino.Licensing.LicenseGenerator(privateKey);

                var name = Name;
                var expirationDate = ExpirationDate;
                var licenseType = LicenseType;

                //var options = new Dictionary<string, string>
                //{
                //    {"MachineName", Environment.MachineName },
                //    {"CpuID", GetHardwareId("Win32_Processor", "processorID") },
                //    {"SerialNo", GetHardwareId("Win32_BIOS", "SerialNumber") }
                //};

                var license = generator.Generate(name, id, expirationDate, licenseType);

                File.WriteAllText(MainPath + @"\license.xml", license);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ValidateLicense(string LicensePath, string PublicKeyPath)
        {
            var publicKey = File.ReadAllText(PublicKeyPath);

            //Throws an exception if license has been modified
            new LicenseValidator(publicKey, LicensePath).AssertValidLicense();
        }

    }
}

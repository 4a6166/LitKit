using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Licensing;

namespace LicenseGenerator
{
    public class License
    {
        public string version { get; private set; }
        public string customer { get; private set; }
        public string emailAddress { get; private set; }
        public DateTime expirationDate { get; private set; }
        public LicenseType licenseType { get; private set; }

        public License(string version, string customer, string emailAddress, DateTime expirationDate, LicenseType licenseType)
        {
            this.version = version;
            this.customer = customer;
            this.emailAddress = emailAddress;
            this.expirationDate = expirationDate;
            this.licenseType = licenseType;
        }
    }
}

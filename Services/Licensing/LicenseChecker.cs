using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portable.Licensing;
using Portable.Licensing.Validation;
using Services.Extensions;

namespace Services.Licensing
{
    public class LicenseChecker
    {

        private static readonly string PublicKey = "MIIBKjCB4wYHKoZIzj0CATCB1wIBATAsBgcqhkjOPQEBAiEA/////wAAAAEAAAAAAAAAAAAAAAD///////////////8wWwQg/////wAAAAEAAAAAAAAAAAAAAAD///////////////wEIFrGNdiqOpPns+u9VXaYhrxlHQawzFOw9jvOPD4n0mBLAxUAxJ02CIbnBJNqZnjhE50mt4GffpAEIQNrF9Hy4SxCR/i85uVjpEDydwN9gS3rM6D0oTlF2JjClgIhAP////8AAAAA//////////+85vqtpxeehPO5ysL8YyVRAgEBA0IABPHak/gc0kWL/BByRAXAuSaAy7sTEM2pM41SPExouqzzwEiwnmC8NJqgGBqwJekC9ERybwmWYWP0+Jlehra73gA=";
        private static string path = @"C:\Users\Jake\OneDrive\Desktop\license.lic";
        private static string customerName;
        private static string customerEmail;
        private static string expirationDate;

        private static string validationErrors;

        public static bool LicenseIsValid()
        {
            bool result = false;

            License license;
            using (var streamReader = new StreamReader(path))
            {
                license = License.Load(streamReader);
            }

            customerName = license.Customer.Name;
            customerEmail = license.Customer.Email;
            expirationDate = license.Expiration.ToString();

            var validationFailures = license.Validate()
                                            .IsLicensedTo(customerName, customerEmail)
                                            .And()
                                            .ExpirationDate()
                                            .And()
                                            .Signature(PublicKey)
                                            .AssertValidLicense();

            var enumerable = validationFailures as IValidationFailure[] ?? validationFailures.ToArray();

            foreach (var validationFailure in enumerable)
            {
                validationErrors += validationFailure.Message + ": " + validationFailure.HowToResolve + "\n";
            }

            if(!enumerable.Any())
            {
                result = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("License key not valid." + Environment.NewLine + validationErrors);
            }

            return result;
        }
    }
}

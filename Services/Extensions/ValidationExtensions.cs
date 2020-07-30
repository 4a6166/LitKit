using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portable.Licensing;
using Portable.Licensing.Validation;

namespace Services.Extensions
{
    public static class ValidationExtensions
    {
        public static IValidationChain IsLicensedTo(this IStartValidationChain validationChain, string name, string email)
        {
            return validationChain.AssertThat(license => CheckCustomer(license, name, email),
                new GeneralValidationFailure()
                {
                    Message = "Dear customer, you got the wrong license file!",
                    HowToResolve = "Please call our support team!"
                });
        }

        private static bool CheckCustomer(License license, string name, string email)
        {
            if (license.Customer == null)
            {
                return false;
            }

            return license.Customer.Name == name
                && license.Customer.Email == email;
        }
    }
}
